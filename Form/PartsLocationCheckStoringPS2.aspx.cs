using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Drawing;
using FGWHSEClient.DAL;

namespace FGWHSEClient.Form
{
    public partial class PartsLocationCheckStoringPS2 : System.Web.UI.Page
    {
        Maintenance maint = new Maintenance();
        DataTable dtLotList;
        DataTable dtChildLotList;

        public bool Bypass=false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Write("<script>");
                Response.Write("alert('Session expired! Please log in again.');");
                Response.Write("window.location = '../PLCLogin.aspx';");
                Response.Write("</script>");
            }
            else
            {
                if (!IsPostBack)
                {
                    dtLotList = new DataTable();

                    dtLotList.Columns.AddRange(new DataColumn[5]
                    {
                    new DataColumn("MotherLot", typeof(string)),
                    new DataColumn("LotNo", typeof(string)),
                    new DataColumn("RFID", typeof(string)),
                    new DataColumn("Sequence", typeof(string)),
                    new DataColumn("QTY", typeof(string))
                    });

                    dtChildLotList = new DataTable();

                    dtChildLotList.Columns.AddRange(new DataColumn[5]
                    {
                    new DataColumn("MotherLot", typeof(string)),
                    new DataColumn("LotNo", typeof(string)),
                    new DataColumn("RFID", typeof(string)),
                    new DataColumn("Sequence", typeof(string)),
                    new DataColumn("QTY", typeof(string))
                    });


                    if (Session["PartsCodePLC"] == null)
                    {
                        Response.Redirect("PartsLocationCheckv2.aspx");
                    }
                    else
                    {
                        lblPartCode.Text = Session["PartsCodePLC"].ToString();
                        txtLocationID.Focus();
                    }

                     btnSave.Enabled = false;
                }
                else
                {
                    dtLotList = (DataTable)ViewState["LotList"];
                    dtChildLotList = (DataTable)ViewState["ChildLotList"];
                }
            
              
            }

            ViewState["LotList"] = dtLotList;
            ViewState["ChildLotList"] = dtChildLotList;
        }

        protected void txtPartCode_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("PartsLocationCheckPS2.aspx");
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            //txtLocationID.Text = "";
            //txtLotNo.Text = "";
            //lblMessage.Text = "";
            //txtLocationID.ReadOnly = false;
            //txtLocationID.Focus();


        }

        protected void txtMotherLot_TextChanged(object sender, EventArgs e)
        {
            // TEXT CHANGED EVENT OF MOTHER LOT QR:
            string strQR, strReferenceNo, strLotNo, strQty, strPartCode = "";



            const string STR_QRHEADER_PARTCODE = "Z1";
            const string STR_QRHEADER_REFERENCE = "Z5";
            const string STR_QRHEADER_QUANTITY = "Z3";

            strLotNo = strReferenceNo = strQty = "";

            strQR = txtMotherLot.Text.Trim().ToUpper();

            try
            {
                if (strQR.IndexOf("|") < 0)
                {
                    // lblMessage.Text = "Please input Mother Lot QR";
                    this.displayMessage("Please input Mother Lot QR");
                    return;
                }

                string[] strChoppedQR = strQR.Split('|');
                foreach (string _strTemp in strChoppedQR)
                {
                    // KUNIN YUNG REFERENCE NUMBER, LOT NUMBER AND QUANTITY FROM QR
                    if (_strTemp.Trim().ToUpper().Substring(0, 2) == STR_QRHEADER_REFERENCE)
                    {
                        strReferenceNo = _strTemp.Trim().ToUpper().Substring(2);
                    
                    }
                    if (_strTemp.Trim().ToUpper().Substring(0, 2) == STR_QRHEADER_QUANTITY)
                    {
                        strQty = _strTemp.Trim().ToUpper().Substring(2);
                    }
                    if (_strTemp.Trim().ToUpper().Substring(0, 2) == STR_QRHEADER_PARTCODE)
                    {
                        strPartCode = _strTemp.Trim().ToUpper().Substring(2);
                    }

                }



                if (strPartCode.ToString() != lblPartCode.Text.Trim())
                {

                    // lblMessage.Text = "Invalid PartCode!" + " - " + strPartCode.ToString();
                    this.displayMessage("Invalid PartCode!" + " - " + strPartCode.ToString());

                    txtMotherLot.Text = "";
                    txtMotherLot.Focus();

                    if (lblScanQty.Text == "" || lblScanQty.Text=="0")
                    {
                        lblScanQty.Text = "0";

                    }
                    else
                    {
                        lblScanQty.Text = Convert.ToString(Convert.ToInt32(ViewState["prevQty"].ToString()));
                    }

                }
                else
                {
                    /* ---------------------------------------------------------- */
                    DataSet dsGetRFIDTag = new DataSet();
                    dsGetRFIDTag = maint.PLC_GetRFIDTag(strReferenceNo);

                    if (dsGetRFIDTag.Tables[0].Rows.Count > 0)
                    {
                        lblRFIDTag.Text = dsGetRFIDTag.Tables[0].Rows[0]["RFIDTag"].ToString();
                        lblTotQty.Text = strQty;//dsGetRFIDTag.Tables[0].Rows[0]["Qty"].ToString();
                        txtLotRefNo.Focus();
                        // IDISPLAY YUNG NAKUHANG QUANTITY SA lblTotQty

                        txtMotherLot.Text = strReferenceNo;
                        this.clearMessage();
                    }
                    else
                    {
                        //lblMessage.Text = "NO Paired RFID Tag.";
                        this.displayMessage("NO Paired RFID Tag.");
                        
                        txtMotherLot.Text = "";
                        txtMotherLot.Focus();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnBypass_Click(object sender, EventArgs e)
        {
            try
            {
                txtPass.Text = string.Empty; // SHHH!! HINDI NILA DAPAT MALAMAN

                ModalPopupExtender2.Show();
                // btnShowPopup_Click(btnShowPopup, EventArgs.Empty);
                if (Convert.ToInt32(lblTotQty.Text) > Convert.ToInt32(lblScanQty.Text))
                {
                    chkLacking.Checked = true;
                    int lacking = 0;
                    lacking = Convert.ToInt32(lblTotQty.Text) - Convert.ToInt32(lblScanQty.Text);
                    lblLackingQty.Text = Convert.ToString(lacking);

                }
                else if (Convert.ToInt32(lblScanQty.Text) > Convert.ToInt32(lblTotQty.Text))
                {
                    chkExcess.Checked = true;
                    int excess = 0;
                    excess = (Convert.ToInt32(lblTotQty.Text) - Convert.ToInt32(lblScanQty.Text)) * -1;
                    lblExcessQty.Text = Convert.ToString(excess);
                }
                
                btnConfirm.Enabled = true;
                btnProceed.Enabled = false;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void btnShowPopup_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }



        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Maintenance maint = new Maintenance();
            string strCheckApprover = "";


            strCheckApprover = maint.checkApprover(txtApprover.Text.Trim(), txtPass.Text.Trim());
            if (strCheckApprover == string.Empty)
            {
                btnProceed.Enabled = true;
                ModalPopupExtender2.Show();
                btnConfirm.Enabled = false;
            }
            else
            {

                Response.Write("<script>alert('" + strCheckApprover + "');</script>");
            }
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            try
            {

                Maintenance maint = new Maintenance();
                DataSet ds = new DataSet();

                string typeOfBypass = "";
                int bypassQuantity = 0;

                if (chkExcess.Checked == true)
                {
                    typeOfBypass = "E";
                    bypassQuantity = Convert.ToInt32(lblExcessQty.Text);
                }
                else if (chkLacking.Checked == true)
                {
                    typeOfBypass = "L";
                    bypassQuantity = Convert.ToInt32(lblLackingQty.Text);
                }

                string strInsert = "";

                string RefNo = txtMotherLot.Text.Trim(); // lblRFIDTag.... LOLOLOLOL!
                string TypeOfBypass = typeOfBypass.Trim();
                int BypassQty = bypassQuantity;
                string Approver = txtApprover.Text.Trim();
                string CreatedBy = Session["UserName"].ToString();

                strInsert = maint.insertBypass(RefNo, TypeOfBypass, BypassQty, Approver, CreatedBy);

                if (strInsert == "")
                {
                    Bypass = true;
                    Response.Write("<script>alert('Bypass Transaction Successfully Saved');</script>");
                    ModalPopupExtender2.Hide();
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "$('#pnlGetTrans').modal('hide');", true);

                    //Response.Redirect("PartsLocationCheckPS2.aspx");

                    txtApprover.Text = "";
                    txtPass.Text = "";
                    chkLacking.Checked = false;
                    lblLackingQty.Text = "";
                    chkExcess.Checked = false;
                    lblExcessQty.Text = "";
                }
                if (Bypass)
                {
                    btnSave.Enabled = true;
                }
                else
                {
                    btnSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtLotRefNo_TextChanged1(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                string strQR, strLotNo, strPartCode = "", strQty = "", strReferenceNo = "";

                const string STR_QRHEADER_PARTCODE = "Z1";
                const string STR_QRHEADER_LOTNO = "Z2";
                const string STR_QRHEADER_QUANTITY = "Z3";
                const string STR_QRHEADER_REFERENCE = "Z5";

                strLotNo = "";

                strQR = txtLotRefNo.Text.Trim().ToUpper();


                if (strQR.IndexOf("|") < 0)
                {
                    this.displayMessage("Please input/scan LotNo QR");
                    //lblMessage.Text = "Please input/scan LotNo QR";
                    //partname.Style.Add("background-color", "#960018");
                    //lblMessage.ForeColor = Color.White;
                    return;
                }

                string[] strChoppedQR = strQR.Split('|');
                foreach (string _strTemp in strChoppedQR)
                {
                    if (_strTemp.Trim().ToUpper().Substring(0, 2) == STR_QRHEADER_LOTNO)
                    {
                        strLotNo = _strTemp.Trim().ToUpper().Substring(2);
                        txtLotRefNo.Text = strLotNo;
                    }
                    if (_strTemp.Trim().ToUpper().Substring(0, 2) == STR_QRHEADER_QUANTITY)
                    {
                        strQty = _strTemp.Trim().ToUpper().Substring(2);
                    }
                    if (_strTemp.Trim().ToUpper().Substring(0, 2) == STR_QRHEADER_PARTCODE)
                    {
                        strPartCode = _strTemp.Trim().ToUpper().Substring(2);
                    }
                    if (_strTemp.Trim().ToUpper().Substring(0, 2) == STR_QRHEADER_REFERENCE)
                    {
                        strReferenceNo = _strTemp.Trim().ToUpper().Substring(2);
                    }
                }


                /* ---------------------------------------------------------- */



                //Checking if scanned qr has the same partcode running

                if (strPartCode.ToString() != lblPartCode.Text.Trim())
                {

                    //lblMessage.Text = "Invalid PartCode!" + " - " + strPartCode.ToString();
                    this.displayMessage("Invalid PartCode!" + " - " + strPartCode.ToString());
                    txtLotRefNo.Text = "";
                    txtLotRefNo.Focus();

                    if (lblScanQty.Text == "" || lblScanQty.Text == "0")
                    {
                        lblScanQty.Text = "0";

                    }
                    else
                    {
                        lblScanQty.Text = Convert.ToString(Convert.ToInt32(ViewState["prevQty"].ToString()));
                    }
                }
                else
                {
                    //Checking if referenceno is existing

                    DataSet dsCheckIfRefNoIsSaved = new DataSet();
                    dsCheckIfRefNoIsSaved = maint.CheckIfRefNoSavedInDB(strReferenceNo);

                    if (dsCheckIfRefNoIsSaved.Tables[0].Rows.Count > 0)
                    {
                        //lblMessage.Text = "Scanned ReferenceNo already existing!" + " - " + strReferenceNo.ToString();
                        this.displayMessage("Scanned ReferenceNo already existing!" + " - " + strReferenceNo.ToString());

                        txtLotRefNo.Text = "";
                        txtLotRefNo.Focus();

                        if (lblScanQty.Text == "" || lblScanQty.Text == "0")
                        {
                            lblScanQty.Text = "0";
                         
                        }
                        else
                        {
                            lblScanQty.Text = Convert.ToString(Convert.ToInt32(ViewState["prevQty"].ToString()));

                        }
                        
                    }
                    else
                    {
                        lblScanQty.Text = strQty;
                        lblMessage.Text = "";

                        DataRow dr;
                        DataRow drChild;

                        dr = dtLotList.NewRow();
                        dr["MotherLot"] = strReferenceNo;
                        dr["LotNo"] = txtLotRefNo.Text;
                        dr["RFID"] = lblRFIDTag.Text;
                        dr["Sequence"] = "1";
                        dr["QTY"] = lblScanQty.Text;

                        drChild = dtChildLotList.NewRow();
                        drChild["MotherLot"] = txtMotherLot.Text;
                        drChild["LotNo"] = strReferenceNo;
                        drChild["RFID"] = lblRFIDTag.Text;
                        drChild["Sequence"] = "1";
                        drChild["QTY"] = lblScanQty.Text;

                        bool isExist = false;

                        int lotlabelcount = 1;
                        foreach (DataRow drow in dtChildLotList.Rows)
                        {
                            string x = drow["LotNo"].ToString().Trim().ToUpper();
                            if (x == strReferenceNo)
                            {
                                isExist = true;
                                break;
                            }

                            lotlabelcount++;
                        }


                        if (isExist)
                        {

                            DataRow currRow = (DataRow)dtLotList.Rows[dtLotList.Rows.Count - 1];
                            string prevMotherLot = Convert.ToString(currRow["MotherLot"]).Trim().ToUpper();
                            string prevLot = Convert.ToString(currRow["LotNo"]).Trim().ToUpper();
                            string prevSequence = Convert.ToString(currRow["Sequence"]).Trim().ToUpper();
                            string prevQty = Convert.ToString(currRow["Qty"]).Trim().ToUpper();


                            //lblMessage.Text = "Lot No already scanned!";
                            // this.displayMessage("Lot No already scanned!");
                            this.displayMessage("Scanned ReferenceNo already existing!" + " - " + strReferenceNo.ToString());

                            txtLotRefNo.Text = "";
                            txtLotRefNo.Focus();
                            lblScanQty.Text = Convert.ToString(Convert.ToInt32(ViewState["prevQty"].ToString()));

                        }
                        else
                        {


                            dtLotList.Rows.Add(dr);
                            dtLotList.AcceptChanges();

                            dtChildLotList.Rows.Add(drChild);
                            dtChildLotList.AcceptChanges();

                            DataRow currRow = (DataRow)dtLotList.Rows[dtLotList.Rows.Count - 1];
                            string prevMotherLot = Convert.ToString(currRow["MotherLot"]).Trim().ToUpper();
                            string prevLot = Convert.ToString(currRow["LotNo"]).Trim().ToUpper();
                            string prevSequence = Convert.ToString(currRow["Sequence"]).Trim().ToUpper();
                            string prevQty = Convert.ToString(currRow["Qty"]).Trim().ToUpper();

                            lblLotLabelCount.Text = lotlabelcount.ToString();
                            if (lotlabelcount == 1)
                            {
                                ViewState["QTY"] = "0";
                                ViewState["prevQty"] = "0";
                            }
                            else
                            {
                                ViewState["QTY"] = prevQty;
                            }

                            lblScanQty.Text = Convert.ToString(Convert.ToInt32(ViewState["prevQty"].ToString()) + Convert.ToInt32(lblScanQty.Text)).ToString();
                            ViewState["prevQty"] = lblScanQty.Text;

                            txtLotRefNo.Text = "";
                            txtLotRefNo.Focus();
                            partname.Style.Add("background-color", "#eee8cd");
                        }
                    }






                }

                ToCheckbtnSaveEnable();




                //end





            }
            catch (Exception ex)
            {

                if (Convert.ToInt32(lblTotQty.Text) > Convert.ToInt32(lblScanQty.Text))
                {
                    chkLacking.Checked = true;
                    int lacking = 0;
                    lacking = Convert.ToInt32(lblTotQty.Text) - Convert.ToInt32(lblScanQty.Text);
                    lblLackingQty.Text = Convert.ToString(lacking);
                    chkExcess.Enabled = false;

                }
                else if (Convert.ToInt32(lblScanQty.Text) > Convert.ToInt32(lblTotQty.Text))
                {
                    chkExcess.Checked = true;
                    int excess = 0;
                    excess = Convert.ToInt32(lblTotQty.Text) - Convert.ToInt32(lblScanQty.Text);
                    lblExcessQty.Text = Convert.ToString(excess);
                    chkLacking.Enabled = false;

                }

                throw ex;
            }
        }

        protected void txtLocationID_TextChanged(object sender, EventArgs e)
        {
            try
            {
               
                DataSet dsCheckLocation = new DataSet();

                
                if (txtLocationID.Text.ToUpper() == "")
                {
                    //lblMessage.Text = "Please scan/input Location!";
                    this.displayMessage("Please scan/input Location!");
                   
                    txtLocationID.Focus();
                    
                }
                else
                {
                    dsCheckLocation = maint.CheckScannedLocation(lblPartCode.Text, txtLocationID.Text.ToUpper());

                    if (dsCheckLocation.Tables[0].Rows.Count>0)
                    {
                        txtLocationID.Text.Trim().ToUpper();
                        txtMotherLot.Focus();
                        Response.Write(txtLocationID.Text.Trim().ToUpper());

                        //lblMessage.Text = "";
                        //partname.Style.Add("background-color", "#eee8cd");
                        this.clearMessage();
                    }
                    else
                    {
                        // lblMessage.Text = "Invalid scanned location!" + " - " + txtLocationID.Text.Trim() ;
                        this.displayMessage("Invalid scanned location!" + " - " + txtLocationID.Text.Trim().ToUpper());
                        txtLocationID.Focus();
                        txtLocationID.Text="";
                    }
                   
                }

               
            }
            catch (Exception)
            {

                throw;
            }
        }

        // 
        private void ToCheckbtnSaveEnable()
        {

            if (lblScanQty.Text == lblTotQty.Text)
            {
                // enabled
                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
            }
            

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                PartsLocationCheckDAL maint2 = new PartsLocationCheckDAL();
                //Save child data to lotlist
                Maintenance maint = new Maintenance();
                DataSet dsInsertToLotList = new DataSet();
                DataSet dsInsertChild = new DataSet();

                string errormessage = "";

                if (dtLotList.Rows.Count>0)
                {
                    //add in storing logs table.
                    errormessage = txtMotherLot.Text.ToString().Trim().ToUpper() + " successfully stored to " + txtLocationID.Text.Trim().ToUpper();
                    int result = maint2.AddStoring(txtMotherLot.Text.ToString().Trim().ToUpper(), txtLocationID.Text.Trim().ToUpper(), errormessage, Session["Username"].ToString(), lblPartCode.Text.Trim());


                    foreach (DataRow dr in dtLotList.Rows)
                    {
                        //INSERT CHILD DETAILS INTO LOT LIST 
                        dsInsertToLotList = maint.PLC_InsertChildDetails_LotList(dr["MotherLot"].ToString().Trim().ToUpper(), dr["RFID"].ToString().Trim().ToUpper(), dr["LotNo"].ToString().Trim().ToUpper(), lblPartCode.Text, dr["QTY"].ToString().Trim().ToUpper(), lblHeader.Text, "3", Session["Username"].ToString());
                    }
                    foreach (DataRow dr in dtChildLotList.Rows)
                    {
                        //INSERT CHILD DETAILS INTO LOT LIST 
                        dsInsertToLotList = maint.PLC_InsertChildDetails_ChildLotList(txtMotherLot.Text, dr["LotNo"].ToString().Trim().ToUpper(),dr["QTY"].ToString().Trim().ToUpper(), Session["Username"].ToString());

                       
                    }
                    
                    // IF SUCCESSFUL, SHOULD REDIRECT TO PARTS LOCATION CHECK V2. o_o
                    Response.Write("<script>");
                    Response.Write("alert('Successfully saved.');");
                    Response.Write("window.location = 'PartsLocationCheckPS2.aspx';");
                    Response.Write("</script>");
                }
                else
                {

                    lblMessage.Text = "No data to be saved!"; // exception... do not touch
                    partname.Style.Add("background-color", "#eee8cd");
                    lblMessage.ForeColor = Color.Black;
                }

                ClearFields();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void ClearFields()
        {
            DataTable dt = new DataTable();
            dtLotList = dt;
            dtChildLotList = dt;

            txtMotherLot.Text = "";
            lblRFIDTag.Text = "";
            txtLotRefNo.Text = "";
            lblScanQty.Text = "";
            lblTotQty.Text = "";
            lblLotLabelCount.Text = "";
            lblMessage.Text = "";


        }


        protected void btnClear_Click1(object sender, EventArgs e)
        {

            ClearFields();

        }

        private void clearMessage()
        {
            lblMessage.Text = "";
            lblMessage.ForeColor = Color.Black;
            // partname.Style.Remove("background-color");
            partname.Style.Add("background-color", "#eee8cd");
        }

        private void displayMessage(string strMessage)
        {
            lblMessage.Text = strMessage.Trim();
            lblMessage.ForeColor = Color.White;
            partname.Style.Add("background-color", "#960018");
        }
    }
}
