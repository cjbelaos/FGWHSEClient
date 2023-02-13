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
using FGWHSEClient.DAL;
using com.eppi.utils;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;

namespace FGWHSEClient
{
    public partial class RFIDReceiving : System.Web.UI.Page
    {
        RFIDReceivingDAL RFIDReceivingDAL = new RFIDReceivingDAL();
        public string strAccessLevel = "";

        void Page_PreInit(Object sender, EventArgs e)
        {
            if (Request.QueryString["DeviceType"] != null)
            {
                if (Request.QueryString["DeviceType"].ToString() == "Denso")
                {
                    this.MasterPageFile = "~/Form/DENSOMasterPalletMonitoring.Master";
                }
            }
        }
        

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string strLoginType = System.Configuration.ConfigurationManager.AppSettings["webFor"];
                if (strLoginType == "EPPI")
                {
                    string strLoginPage = "Login.aspx";
                    if (!checkAuthority("FGWHSE_020"))
                    {
                        if (Request.QueryString["DeviceType"] != null)
                        {
                            if (Request.QueryString["DeviceType"].ToString() == "Denso")
                            {
                                strLoginPage = "DENSOLogin.aspx";
                            }
                            else
                            {
                                strLoginPage = "HTLogin.aspx";
                            }
                        }
                        Response.Write("<script>");
                        Response.Write("alert('You are not authorized to access this web page! ');");
                        Response.Write("window.location = '../" + strLoginPage + "?returnurl=Maintenance/ControlSegment.aspx';self.close();");
                        Response.Write("</script>");

                    }
                    else
                    {
                        OnLoad();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }


        }

        private void OnLoad()
        {
            txtDNNo.Focus();
            if (Session["UserName"] == null)
            {

                if (Request.QueryString["DeviceType"] != null)
                {
                    if (Request.QueryString["DeviceType"].ToString() == "Denso")
                    {
                        Response.Write("<script>");
                        Response.Write("alert('Session expired! Please log in again.');");
                        Response.Write("window.location = '../DENSOLogin.aspx';");
                        Response.Write("</script>");
                    }
                }
                else
                {
                    Response.Write("<script>");
                    Response.Write("alert('Session expired! Please log in again.');");
                    Response.Write("window.location = '../HTLogin.aspx';");
                    Response.Write("</script>");
                }

            }

            if (!this.IsPostBack)
            {
                if (Request.QueryString["DeviceType"] != null)
                {
                    if (Request.QueryString["DeviceType"].ToString() == "Denso")
                    {
                        dvLotDataScan.Attributes.Add("style", "font-size:x-small;width:270px");
                        //txtDNNo.Attributes.Add("style", "font-size:13px;width:150px;");
                        txtDNNo.Attributes.Add("style", "font-size:18px;width:120px;");
                        //btnDNDetails.Attributes.Add("style", "font-size:x-small;width:30px;");
                        //grdDNData.Attributes.Add("style", "font-size:x-small;width:230px;");
                        grdRFID.Attributes.Add("style", "font-size:10px;width:245px;");
                        grdHeight.Attributes.Add("style", "overflow:auto;height:105px;");
                        //lblScan.Attributes.Add("style", "font-size:8px;");
                        lblScan.Attributes.Add("style", "font-size:11px;");
                        //btnDelete.Attributes.Add("style", "font-size:x-small;width:60px;height:25px");
                        //btnExecute.Attributes.Add("style", "font-size:x-small;width:60px;height:25px");
                        //btnClear.Attributes.Add("style", "font-size:x-small;width:60px;height:25px");

                        btnReceive.Attributes.Add("style", "font-size:14px;width:75px;height:25px");
                       // btnExecute.Attributes.Add("style", "font-size:14px;width:75px;height:25px");
                        btnClear.Attributes.Add("style", "font-size:14px;width:75px;height:25px");
                        tdMessage.Attributes.Add("style", "border: 1px solid #385d8a; background-color:#d9d9d9;padding:5px;font-size:x-small;height:20px;width:180px");
                        lblMessage2.Attributes.Add("style", "font-size:11px;width:180px;");
                        lblMessage.Attributes.Add("style", "font-size:11px;");
                        tdScan.Attributes.Add("style", "width:100px;text-align:right");
                    }
                }

            }
        }

        bool isValidText(string strRegEx, string strText)
        {
            bool isValid = true;

            Regex regex = new Regex(strRegEx);

            foreach (char c in strText)
            {
                if (!regex.IsMatch(c.ToString()))
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        protected void chkHeader_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gvr in grdRFID.Rows)
                {

                    CheckBox chkBox = (CheckBox)gvr.Cells[0].FindControl("chkItem");
                    if (chkBox.Enabled)
                    {
                        chkBox.Checked = ((CheckBox)sender).Checked;
                    }
                    else
                    {
                        chkBox.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void txtDNNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = RFIDReceivingDAL.GET_NOTRECEIVEDRFID(txtDNNo.Text.Trim());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdRFID.DataSource = RFIDReceivingDAL.GET_NOTRECEIVEDRFID(txtDNNo.Text.Trim());
                    grdRFID.DataBind();

                    btnReceive.Visible = true;
                    btnClear.Visible = true;
                    btnReceive.Enabled = true;
                    btnClear.Enabled = true;

                    Label1.Visible = true;
                }
                else
                {
                    lblMessage.Text = "NO DATA FOUND!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;

                    Label1.Visible = false;
                    btnReceive.Visible = true;
                    btnClear.Visible = true;
                    btnReceive.Enabled = false;
                    btnClear.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void btnReceive_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in grdRFID.Rows)
                {
                    CheckBox chkBox = (CheckBox)row.Cells[0].FindControl("chkItem");

                    if (chkBox.Checked == true)
                    {
                        string dnno = row.Cells[1].Text.Trim().ToUpper();
                        string rfid = row.Cells[2].Text.Trim().ToUpper();
                        string createdby = Session["UserID"].ToString();

                        string strResult = "";
                        strResult = RFIDReceivingDAL.ADD_RFIDRECEIVE(dnno, rfid, createdby);

                        lblMessage.Text = "SUCCESSFULLY RECEIVED!";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                }


                DataSet ds = new DataSet();
                ds = RFIDReceivingDAL.GET_NOTRECEIVEDRFID(txtDNNo.Text.Trim());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdRFID.DataSource = RFIDReceivingDAL.GET_NOTRECEIVEDRFID(txtDNNo.Text.Trim());
                    grdRFID.DataBind();
                }
                else
                {
                    grdRFID.DataSource = null;
                    grdRFID.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }

            //DataSet ds = new DataSet();
            //ds = LotDataScanningDAL.GET_SCAN_DETAILS_BASED_FROM_DNNO_AND_SUPP_ID(DNNo, strSuppID, strLoginType);

            //int rowsCount = 0;
            //foreach (DataRow row in ds.Tables[0].Rows)
            //{
            //    CheckBox chkBox = (CheckBox)row.Cells[0].FindControl("chkItem");

            //    if (chkBox.Checked == true)
            //    {

            //        string dnno = row["BARCODEDNNO"].ToString().ToUpper();
            //        string rfid = row["RFIDTag"].ToString().ToUpper();

            //        blMessage.Text = "SUCCESSFULLY RECEIVED!" + rfid + " " + dnno;
            //    }
            //}
        }

        protected void grdRFID_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                e.Row.Cells[1].Visible = false;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }


        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtDNNo.Text = "";
                lblMessage.Text = "";
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
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
                        if (isValid == true)
                        {
                            string strRole = dvSubsystem.Table.Rows[iRow]["Role"].ToString();

                            if (strRole != "")
                            {
                                strAccessLevel = strRole;
                            }
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
    }
}
