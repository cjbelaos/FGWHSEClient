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
    public partial class PartsLocationCheckStoring : System.Web.UI.Page
    {

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
                    if (Session["PartsCodePLC"] == null)
                    {
                        Response.Redirect("PartsLocationCheckv2.aspx");
                    }
                    else
                    {
                        lblPartCode.Text = Session["PartsCodePLC"].ToString();
                        hiddenEKANBAN.Value = Session["EKANBANFLAG"].ToString();
                        //txtPartCode.Focus();
                    }
                }

                if (txtLocationID.Text.Trim() == "")
                {
                    txtLocationID.Focus();
                }
                else
                {
                    txtLotNo.Focus();
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("PartsLocationCheckv2.aspx");
        }

        protected void txtLocationID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtLocationID.Text.Trim() == "")
                {
                    txtLocationID.Text = "";
                    txtLocationID.Focus();
                }
                else
                {
                    PartsLocationCheckDAL maint = new PartsLocationCheckDAL();
                    DataView dvGetLocation = new DataView();

                    //check If Part Code and Location Matches in the Master
                    dvGetLocation = maint.GetPartsLocationList(lblPartCode.Text.Trim(), txtLocationID.Text.Trim());

                    if (dvGetLocation.Table.Rows.Count == 0)
                    {
                        string errormessage = "";
                        string createdby = Session["UserName"].ToString();
                        errormessage = "ERROR: Incorrect Location ID " + txtLocationID.Text + " for Part Code " + lblPartCode.Text.Trim();

                        //add in storing logs table
                        int result = maint.AddStoring(txtLotNo.Text.Trim(), txtLocationID.Text.Trim().ToUpper(), errormessage, createdby, lblPartCode.Text.Trim());


                        lblMessage.Text = "ERROR: Incorrect Location ID " + txtLocationID.Text + " for Part Code " + lblPartCode.Text.Trim() + ". Please scan correct Location ID or Check Warehouse Location Master";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Font.Size = 10;
                        lblMessage.Font.Bold = false;
                        txtLocationID.Text = "";
                        txtLocationID.Focus();

                    }
                    else
                    {
                        lblMessage.Text = "";
                        txtLocationID.ReadOnly = true;
                        txtLotNo.Focus();

                    }


                }

            }
            catch (Exception ex)
            {
                msgBox.alert(ex.Message);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtLocationID.Text = "";
            txtLotNo.Text = "";
            lblMessage.Text = "";
            txtLocationID.ReadOnly = false;
            txtLocationID.Focus();


        }

        protected void txtLotNo_TextChanged(object sender, EventArgs e)
        //kempeerenz2
        {

            try
            {
                if (txtLotNo.Text.Trim() == "")
                {
                    txtLotNo.Text = "";
                    txtLotNo.Focus();
                }
                else
                {

                    string errormessage = "";
                    string createdby = Session["UserName"].ToString();
                    PartsLocationCheckDAL maint = new PartsLocationCheckDAL();
                    DataView dvGetLotList = new DataView();
                    DataView dvGetMaxLoc = new DataView();

                    //check If Part Code and Lot Ref No match in the database table
                    //dvGetLotList = maint.GetLotList(txtLotNo.Text.Trim(), lblPartCode.Text.Trim());
                    dvGetLotList = maint.GetLotListRef(txtLotNo.Text.Trim());

                    dvGetMaxLoc = maint.GetPLC_Max(txtLotNo.Text.Trim());

                    if (dvGetLotList.Table.Rows.Count == 0)
                    {
                        //if not exist
                        errormessage = "";
                        lblMessage.Text = "";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Font.Size = 10;
                        lblMessage.Font.Bold = false;

                        txtLotNo.Text = "";
                        txtLotNo.Text = "";
                        txtApprover.Focus();

                        ModalPopupExtender2.Show();
                        txtApprover.Text = "";
                        txtPass.Text = "";
                    }
                    else
                    {
                        string CurrPartCode = dvGetLotList.Table.Rows[0]["PartCode"].ToString().Trim();
                        
                        //check if Part Code and Lot Ref No match in the database table
                        if (CurrPartCode == lblPartCode.Text.Trim())
                        {

                            //get reference data in lot list
                            hiddenLotID.Value = dvGetLotList.Table.Rows[0]["LotID"].ToString();
                            string currentLocation = "";

                            //get max location of current
                            if (dvGetMaxLoc.Table.Rows.Count > 0)
                            {
                                currentLocation = dvGetMaxLoc.Table.Rows[0]["Location"].ToString();
                            }

                            if (currentLocation.Trim().ToUpper() == txtLocationID.Text.Trim().ToUpper())
                            {
                                //if same location
                                errormessage = "ERROR: " + txtLotNo.Text.Trim() + " already stored to " + txtLocationID.Text.Trim();
                                lblMessage.Text = txtLotNo.Text.Trim() + " already stored to " + txtLocationID.Text.Trim();
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                lblMessage.Font.Size = 10;
                                lblMessage.Font.Bold = false;

                            }
                            else
                            {
                                //if not same location
                                errormessage = txtLotNo.Text.Trim() + " successfully stored to " + txtLocationID.Text.Trim();
                                lblMessage.Text = txtLotNo.Text.Trim() + " successfully stored to " + txtLocationID.Text.Trim();
                                lblMessage.ForeColor = System.Drawing.Color.Blue;
                                lblMessage.Font.Size = 10;
                                lblMessage.Font.Bold = false;

                                //if Partcode has EKANBANRFIDIFFLAG = 1 from TBL_M_PARTSLOCATION
                                if (hiddenEKANBAN.Value.ToUpper().Trim() == "TRUE")
                                {
                                    //update rfid tag in lot list
                                    int updateLot = maint.UpdateLotList(txtLocationID.Text.Trim().ToUpper(), txtLotNo.Text.Trim().ToUpper(), createdby);
                                    //add logs in lotupdate table
                                    int addLogs = maint.AddLotUpdate(hiddenLotID.Value.ToUpper(), "RFIDTAG", currentLocation.ToUpper(), txtLocationID.Text.Trim().ToUpper(), createdby);
                                }
                            }
                        }
                        else
                        {
                            //if not match
                            errormessage = "ERROR: RefNo mismatch for Part Code " + lblPartCode.Text.Trim();
                            lblMessage.Text = "ERROR: Lot RefNo mismatch for Part Code " + lblPartCode.Text.Trim() + ". Please scan correct RefNo";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            lblMessage.Font.Size = 10;
                            lblMessage.Font.Bold = false;
                        }

                        txtLotNo.Text = "";
                        txtLotNo.Focus();

                    }
                    
                    //add in storing logs table
                    int result = maint.AddStoring(txtLotNo.Text.Trim(), txtLocationID.Text.Trim().ToUpper(), errormessage, createdby, lblPartCode.Text.Trim());

                }
            }
            catch (Exception ex)
            {
                msgBox.alert(ex.Message);
            }
        }
        

        protected void btnShowPopup_Click(object sender, EventArgs e)
        {


        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Maintenance maint = new Maintenance();
            string strCheckApprover = "";


            strCheckApprover = maint.checkApprover(txtApprover.Text.Trim(), txtPass.Text.Trim());
            if (strCheckApprover == string.Empty)
            {
                //btnProceed.Enabled = true;

                ModalPopupExtender2.Hide();
                txtLotNo.Focus();
            }
            else
            {

                //Response.Redirect("PartsLocationCheckv2.aspx");
                txtApprover.Focus();
                ModalPopupExtender2.Show();
                //txtApprover.Focus();
                ErrorMessageAccount.Text = "INVALID ACCOUNT!";

                btnConfirm.Enabled = true;
                return;
            }

        }

    }
}

