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


using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Collections.Specialized;
using System.Net;
using System.Collections.Generic;


namespace FGWHSEClient.Form
{
    public partial class PartsReturnHolmes : System.Web.UI.Page
    {
        public string strPageSubsystem = "";
        public string strAccessLevel = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Write("<script>");
                Response.Write("alert('Session expired! Please log in again.');");
                Response.Write("window.location = '../HTLogin.aspx';");
                Response.Write("</script>");
            }
            else
            {
                strPageSubsystem = "FGWHSE_055";
                if (!checkAuthority(strPageSubsystem))
                {
                    Response.Write("<script>");
                    Response.Write("alert('You are not authorized to access the page.');");
                    Response.Write("window.location = '../HTLogin.aspx';");
                    Response.Write("</script>");
                }

                if (!IsPostBack)
                {
                    // btnPartsStoring.Enabled = false;
                    //btnPartsStoring.BackColor = System.Drawing.Color.Gray;
                }

                txtLotNo.Focus();


            }

        }


        private bool checkAuthority(string strPageSubsystem)
        {
            bool isValid = false;
            try
            {
                if (Session["Subsystem"] != null)
                {
                    DataView dvSubsystem = new DataView();
                    dvSubsystem = (DataView)Session["Subsystem"];

                    if (dvSubsystem.Count > 0)
                    {
                        dvSubsystem.Sort = "Subsystem";

                        int iRow = dvSubsystem.Find(strPageSubsystem);

                        if (iRow >= 0)
                        {
                            isValid = true;
                        }
                        else
                        {
                            isValid = false;
                        }

                        string strRole = dvSubsystem.Table.Rows[iRow]["Role"].ToString();

                        if (strRole != "")
                        {
                            strAccessLevel = strRole;
                        }

                    }
                }
                return isValid;
            }
            catch (Exception ex)
            {
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);

                isValid = false;
                return isValid;
            }
        }

        protected void txtPartCode_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            ////Response.Redirect("PartsLocationCheckv2.aspx");
            string errormessage = "";

            //added 1/28/2020 melvin
            InParam param = new InParam();
            List<parts> parts = new List<parts>();

            int res_success = 0;
            int res_failed = 0;
            string res_message = "";


            int lotId = Convert.ToInt32(hiddenLotID.Value);
            string refno = txtLotNo.Text.Trim();
            string LN = txtLot.Text.Trim();
            int BN = Convert.ToInt32(txtLotNo.Text.Trim().Substring(16,3));
            int SN = Convert.ToInt32(txtLotNo.Text.Trim().Substring(19, 2));
            string QT = txtQty.Text.Trim();
            string PartCode = txtPartCode.Text.Trim();
            string markings = ddlReason.SelectedItem.Text.Trim();
            string scannedby = Session["UserName"].ToString(); //"ewhsuser";
            string remarks = txtRemarks.Text.Trim();


            parts.Add(new parts()
            {
                ref_no = refno,
                lot_no = LN,
                box_no= BN,
                split_no=  SN,
                part_code = PartCode,
                remarks = markings,
                qty = Convert.ToDecimal(QT)
            });


            param.to_location = txtHolmesProcess.Text.Trim();
            param.scanned_by = "ewhsuser"; //scannedby;
            param.remarks = markings;
            param.returned_parts = parts;

            string strAPI = System.Configuration.ConfigurationManager.AppSettings["HOLMES_PartsReturn_API"];

            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(strAPI);
            webReq.Method = "POST";
            webReq.ContentType = "application/json";

            using (Stream ds = webReq.GetRequestStream())
            {
                DataContractJsonSerializer sw = new DataContractJsonSerializer(typeof(InParam));
                sw.WriteObject(ds, param);
            }

            HttpWebResponse response = null;
            try
            {

                response = (HttpWebResponse)webReq.GetResponse();

                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    using (Stream ds = response.GetResponseStream())
                    {
                        DataContractJsonSerializer sw = new DataContractJsonSerializer(typeof(OutParam));
                        OutParam res = (OutParam)sw.ReadObject(ds);

                        string txt = String.Format("successed: {0}, message: {1}", res.succeeded.ToString(), res.message);
                        if (res.failed > 0)
                        {
                            res_message = res.message;
                            res_success = res.succeeded;
                            res_failed = res.failed;
                            //MsgBox1.alert("Error! HOLMES: " + res.message);

                            errormessage = "Error! HOLMES: " + res.message;
                            lblMessage.Text = "Error! HOLMES: " + res.message;
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            lblMessage.Font.Size = 10;
                            lblMessage.Font.Bold = false;

                            //Logger.GetInstance().Fatal("PartsReturn Holmes IF Error: " + res.message + "," + txtHolmes.Text.Trim() + "," + Doc_Num);
                        }
                        else
                        {
                            res_message = res.message;
                            res_success = res.succeeded;
                            res_failed = res.failed;
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }

            }


            //saving in EWHS DB
            if (res_failed == 0)
            {
                string reprint = "NO";
                if (markings.Contains("Reprint"))
                {
                    reprint = "YES";
                }


                HOLMESDAL maint = new HOLMESDAL();
                int result = maint.UpdatePartsReturn(refno, lotId, markings + "/" + remarks, scannedby, reprint);


                errormessage = txtLotNo.Text.Trim() + " successfully saved!";
                lblMessage.Text = txtLotNo.Text.Trim() + " successfully saved!";
                lblMessage.ForeColor = System.Drawing.Color.Blue;
                lblMessage.Font.Size = 10;
                lblMessage.Font.Bold = false;


                txtLotNo.Text = "";
                txtLot.Text = "";
                txtPartCode.Text = "";
                txtPartName.Text = "";
                txtQty.Text = "";
                txtLotNo.Focus();
                hiddenLotID.Value = "";
            }


        }



        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtLotNo.Text = "";
            lblMessage.Text = "";
            Response.Redirect("PartsReturnHolmes.aspx");
        }

        protected void txtLotNo_TextChanged(object sender, EventArgs e)
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
                    lblMessage.Text = "";
                    string createdby = Session["UserName"].ToString();  //"3037889";//Session["UserName"].ToString();
                    PartsLocationCheckDAL maint = new PartsLocationCheckDAL();
                    DataView dvGetLotList = new DataView();

                    //check If Part Code and Lot Ref No match in the database table
                    //dvGetLotList = maint.GetLotList(txtLotNo.Text.Trim(), lblPartCode.Text.Trim());
                    dvGetLotList = maint.GetLotListRef(txtLotNo.Text.Trim());


                    if (dvGetLotList.Table.Rows.Count == 0)
                    {
                        //if not exist
                        errormessage = "ERROR: Lot Ref No does not exist. Please check again if correct or wait a few minutes and try again.";
                        lblMessage.Text = "ERROR: Lot Ref No does not exist. Please check again if correct or wait a few minutes and try again.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Font.Size = 10;
                        lblMessage.Font.Bold = false;


                        txtLotNo.Text = "";
                        txtLotNo.Focus();
                        btnSave.Enabled = false;
                        return;
                    }
                    else
                    {

                        string CurrPartCode = dvGetLotList.Table.Rows[0]["PartCode"].ToString().Trim();
                        string CurrPartName = dvGetLotList.Table.Rows[0]["MaterialName"].ToString().Trim();
                        string CurrLotNo = dvGetLotList.Table.Rows[0]["LotNo"].ToString().Trim();
                        string Qty = dvGetLotList.Table.Rows[0]["Qty"].ToString().Trim();
                        string LotId = dvGetLotList.Table.Rows[0]["LotID"].ToString().Trim();
                        string rfidflag = dvGetLotList.Table.Rows[0]["RFIDFinishFlag"].ToString().Trim();
                        string partsreturnflag = dvGetLotList.Table.Rows[0]["PartsReturnFlag"].ToString().Trim();



                        if (partsreturnflag == "True")
                        {
                            //if not exist
                            errormessage = "ERROR: Lot Ref No already transacted in parts return. ";
                            lblMessage.Text = "ERROR: Lot Ref No already transacted in parts return. ";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            lblMessage.Font.Size = 10;
                            lblMessage.Font.Bold = false;


                            txtLotNo.Text = "";
                            txtLotNo.Focus();
                            btnSave.Enabled = false;
                            return;

                        }
                        else
                        {

                            txtPartCode.Text = CurrPartCode;
                            txtQty.Text = Qty;
                            txtLot.Text = CurrLotNo;
                            txtPartName.Text = CurrPartName;
                            hiddenLotID.Value = LotId;


                            txtHolmesProcess.Focus();

                            btnSave.Enabled = true;

                        }





                    }


                    //add in storing logs table
                    // int result = maint.AddStoring(txtLotNo.Text.Trim(), txtLocationID.Text.Trim().ToUpper(), errormessage, createdby, lblPartCode.Text.Trim());

                    //txtLotNo.Text = "";
                    //txtLotNo.Focus();
                }


            }
            catch (Exception ex)
            {
                msgBox.alert(ex.Message);
            }
        }
    }
}
