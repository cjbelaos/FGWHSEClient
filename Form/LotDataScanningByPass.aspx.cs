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
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing; 


namespace FGWHSEClient.Form
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        LotDataScanningDAL LotDataScanningDAL = new LotDataScanningDAL();
        string procflag;
        string bypassflag;
        string delflag;
        string status;
        string strLoginType;
        public string strAccessLevel = "";

        void Page_PreInit(Object sender, EventArgs e)
        {
            
            if (Request.QueryString["DeviceType"] != null)
            {
                if (Request.QueryString["DeviceType"].ToString() == "Denso")
                {
                    this.MasterPageFile = "~/Form/DENSOMasterPalletMonitoring.Master";
                }
                else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                {
                    this.MasterPageFile = "~/Form/HTMasterPalletMonitoring.Master";
                }
            }

            string strLoginType2 = System.Configuration.ConfigurationManager.AppSettings["webFor"];
            if (strLoginType2 == "EPPI")
            {
                Page.Title = string.Format("MANUAL PARTS RECEIVING (DN BYPASS)");
            }
            else
            {
                Page.Title = string.Format("PARTS LOADING (DN BYPASS)");
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
                    if (!checkAuthority("FGWHSE_013"))
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
                else
                {
                    string strLoginPage = "Login.aspx";
                    if (!checkAuthorityOUTSIDE("FGWHSE_013"))
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
            string strLoginType2 = System.Configuration.ConfigurationManager.AppSettings["webFor"];
            if (strLoginType2 == "EPPI")
            {
                lblHeader.Text = "MANUAL PARTS RECEIVING (DN BYPASS)";
            }
            else
            {
                lblHeader.Text = "PARTS LOADING (DN BYPASS)";
            }

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
                    else if (Request.QueryString["DeviceType"].ToString() == "HT")
                    {
                        Response.Write("<script>");
                        Response.Write("alert('Session expired! Please log in again.');");
                        Response.Write("window.location = '../HTLogin.aspx';");
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
                        btnDNDetails.Attributes.Add("style", "font-size:x-small;width:30px;");
                        //grdDNData.Attributes.Add("style", "font-size:x-small;width:230px;");
                        grdDNData.Attributes.Add("style", "font-size:10px;width:245px;");
                        grdHeight.Attributes.Add("style", "overflow:auto;height:105px;");
                        //lblScan.Attributes.Add("style", "font-size:8px;");
                        lblScan.Attributes.Add("style", "font-size:11px;");
                        //btnDelete.Attributes.Add("style", "font-size:x-small;width:60px;height:25px");
                        //btnExecute.Attributes.Add("style", "font-size:x-small;width:60px;height:25px");
                        //btnClear.Attributes.Add("style", "font-size:x-small;width:60px;height:25px");

                        btnDelete.Attributes.Add("style", "font-size:14px;width:75px;height:25px");
                        btnExecute.Attributes.Add("style", "font-size:14px;width:75px;height:25px");
                        btnClear.Attributes.Add("style", "font-size:14px;width:75px;height:25px");
                        tdMessage.Attributes.Add("style", "border: 1px solid #385d8a; background-color:#d9d9d9;padding:5px;font-size:x-small;height:20px;width:180px");
                        lblMessage2.Attributes.Add("style", "font-size:11px;width:180px;");
                        tdScan.Attributes.Add("style", "width:100px;text-align:right");
                        lblMessage.Attributes.Add("style", "font-size:11px;");
                        //dvLotDataScan.Attributes.Add("style", "font-size:x-small;width:270px");
                        //txtDNNo.Attributes.Add("style", "font-size:13px;width:150px;");
                        //btnDNDetails.Attributes.Add("style", "font-size:x-small;width:30px;");
                        //grdDNData.Attributes.Add("style", "font-size:x-small;width:230px;");
                        //grdHeight.Attributes.Add("style", "overflow:auto;height:105px;");
                        //lblScan.Attributes.Add("style", "font-size:8px;");
                        //btnDelete.Attributes.Add("style", "font-size:x-small;width:60px;height:25px");
                        //btnExecute.Attributes.Add("style", "font-size:x-small;width:60px;height:25px");
                        //btnClear.Attributes.Add("style", "font-size:x-small;width:60px;height:25px");
                        //tdMessage.Attributes.Add("style", "border: 1px solid #385d8a; background-color:#d9d9d9;padding:5px;font-size:x-small;height:20px");
                        //lblMessage.Attributes.Add("style", "font-size:x-small;width:180px;");
                    }
                }




                if (Request.QueryString["DNNo"] != null)
                {
                    if (!(isValidText(@"[A-Za-z0-9]", Request.QueryString["DNNo"].ToString())))
                    {
                        MsgBox1.alert("INVALID DN NO.");

                        btnDNDetails.Enabled = false;
                        btnDelete.Enabled = false;
                        btnExecute.Enabled = false;

                        return;
                    }
                    else
                    {
                        btnDNDetails.Enabled = true;
                        btnDelete.Enabled = true;
                        btnExecute.Enabled = true;

                        txtDNNo.Text = Request.QueryString["DNNo"].ToString();

                        string DNNo;
                        if (Request.QueryString["DNNo"] != null)
                        {
                            DNNo = Request.QueryString["DNNo"].ToString();
                        }
                        else
                        {
                            DNNo = txtDNNo.Text.Trim().ToUpper();
                        }

                        GetScannedData();

                        //txtDNNo.Focus();
                        btnDNDetails.Focus();
                    }
                }
            }

            if (Request.Form["Delete"] != null)
            {
                if (Request.Form["Delete"].ToString().Equals("1"))
                {
                    if (Request.QueryString["DeviceType"] != null)
                    {
                        if (Request.QueryString["DeviceType"].ToString() == "Denso")
                        {
                            Response.Redirect("LotDataScanningDeleteRecordByPass.aspx?DNNo=" + txtDNNo.Text + "&DeviceType=Denso");
                        }
                        else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                        {
                            Response.Redirect("LotDataScanningDeleteRecordByPass.aspx?DNNo=" + txtDNNo.Text + "&DeviceType=HT");
                        }

                    }
                    else
                    {
                        Response.Redirect("LotDataScanningDeleteRecordByPass.aspx?DNNo=" + txtDNNo.Text);
                    }

                }
            }
            //txtDNNo.Focus();
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

        private bool checkAuthorityOUTSIDE(string strPageSubsystem)
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
                        dvSubsystem.Sort = "subsystem_id";

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

        private void GetScannedData()
        {
            try
            {
                grdDNData.Visible = true;
                strLoginType = System.Configuration.ConfigurationManager.AppSettings["webFor"];
                string strSuppID;
                string DNNo;

                if (txtDNNo.Text.Trim() == "")
                {
                    if (Request.QueryString["DNNo"] != null)
                    {
                        DNNo = Request.QueryString["DNNo"].ToString();
                    }
                    else
                    {
                        DNNo = txtDNNo.Text.Trim().ToUpper();
                    }
                }
                else
                {
                    DNNo = txtDNNo.Text.Trim().ToUpper();
                }

                DataSet ds = new DataSet();
                if (strLoginType == "EPPI")
                {

                    strSuppID = "";
                }
                else
                {
                    strSuppID = Session["supplierID"].ToString();
                }
                ds = LotDataScanningDAL.GET_SCAN_DETAILS_BASED_FROM_DNNO_AND_SUPP_ID_BYPASS(DNNo, strSuppID, strLoginType);

                int errorcount = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    procflag = ds.Tables[0].Rows[0]["PROCFLAG"].ToString();

                    bypassflag = ds.Tables[0].Rows[0]["BYPASSFLAG"].ToString();
                    //dnproc = ds.Tables[0].Rows[0]["DNPROC"].ToString();

                    if (procflag == "1")
                    {
                        lblMessage.Text = "DN ALREADY EXECUTED!";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        errorcount = 1;
                    }

                    if (bypassflag == "0")
                    {
                        lblMessage.Text = "INVALID! PLEASE GO TO NORMAL SCANNING PAGE.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        errorcount = 1;
                    }


                    int rowsCount = 0;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (row["STATUS"].ToString() == "TALLY")
                        {
                            rowsCount++;
                        }
                    }
                    if (rowsCount == ds.Tables[0].Rows.Count)
                    {
                        btnExecute.Enabled = true;
                        //lblMessage.Text = "NO RECORD TO DELETE!";
                        //lblMessage.ForeColor = System.Drawing.Color.Red;
                    }

                    //if (delflag == "X")
                    //{
                    //    lblMessage.Text = "DN DATA DELETED.";
                    //    lblMessage.ForeColor = System.Drawing.Color.Red;
                    //    errorcount = 1;
                    //}

                    //if (dnproc == "X")
                    //{
                    //    lblMessage.Text = "DN ALREADY PROCESSED!";
                    //    lblMessage.ForeColor = System.Drawing.Color.Red;
                    //    errorcount = 1;
                    //}
                }
                //else
                //{
                //    lblMessage.Text = "DN NOT FOUND";
                //    lblMessage.ForeColor = System.Drawing.Color.Red;
                //    errorcount = 1;
                //}

                if (errorcount == 1)
                {
                    grdDNData.Visible = false;

                    btnDelete.Enabled = false;
                    btnExecute.Enabled = false;
                    //btnClear.Enabled = false;
                    btnDNDetails.Enabled = false;

                    btnDelete.Visible = true;
                    btnExecute.Visible = true;
                    btnClear.Visible = true;

                }
                else
                {
                    lblMessage.Text = "";
                    if (strLoginType == "EPPI")
                    {

                        strSuppID = "";
                    }
                    else
                    {
                        strSuppID = Session["supplierID"].ToString();
                    }

                    grdDNData.DataSource = LotDataScanningDAL.GET_SCAN_DETAILS_BASED_FROM_DNNO_AND_SUPP_ID_BYPASS(DNNo, strSuppID, strLoginType);
                    grdDNData.DataBind();

                    btnDelete.Visible = true;
                    btnExecute.Visible = true;
                    btnClear.Visible = true;

                    btnDelete.Enabled = true;
                    btnExecute.Enabled = true;
                    btnDNDetails.Enabled = true;
                    btnClear.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void btnDNDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDNNo.Text.Trim() == "")
                {
                    lblMessage.Text = "NO DN DATA INPUT!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    if (Request.QueryString["DeviceType"] != null)
                    {
                        if (Request.QueryString["DeviceType"].ToString() == "Denso")
                        {
                            Response.Redirect("LotDataScanningDetailsByPass.aspx?DeviceType=Denso&DNNo=" + txtDNNo.Text.Trim().ToUpper());
                        }
                        else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                        {
                            Response.Redirect("LotDataScanningDetailsByPass.aspx?DeviceType=HT&DNNo=" + txtDNNo.Text.Trim().ToUpper());
                        }

                    }
                    else
                    {
                        Response.Redirect("LotDataScanningDetailsByPass.aspx?DNNo=" + txtDNNo.Text.Trim().ToUpper());
                    }



                    
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                string DNNo;
                string strSuppID;
                string strLoginType = System.Configuration.ConfigurationManager.AppSettings["webFor"];

                if (Request.QueryString["DNNo"] != null)
                {
                    if (!(isValidText(@"[A-Za-z0-9]", Request.QueryString["DNNo"].ToString())))
                    {
                        MsgBox1.alert("INVALID DN NO.");
                        btnDNDetails.Enabled = false;
                        btnDelete.Enabled = false;
                        btnExecute.Enabled = false;
                        return;
                    }
                    else
                    {
                        DNNo = Request.QueryString["DNNo"].ToString();
                        btnDNDetails.Enabled = true;
                        btnDelete.Enabled = true;
                        btnExecute.Enabled = true;
                    }
                }
                else
                {
                    DNNo = txtDNNo.Text.Trim().ToUpper();
                }

                if (strLoginType == "EPPI")
                {

                    strSuppID = "";
                }
                else
                {
                    strSuppID = Session["supplierID"].ToString();
                }

                DataSet ds = new DataSet();
                ds = LotDataScanningDAL.GET_SCAN_DETAILS_BASED_FROM_DNNO_AND_SUPP_ID_BYPASS(DNNo, strSuppID, strLoginType);

                int rowsCount = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["SCANNEDQTY"].ToString() == "0.000")
                    {
                        rowsCount++;
                    }
                }
                if (rowsCount == ds.Tables[0].Rows.Count)
                {
                    lblMessage.Text = "NO RECORD TO DELETE!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    MsgBox1.confirm("Delete scanned data of this DN?", "Delete");
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
            //MsgBox1.confirm("Delete existing record?", "Delete");
        }

        //bool isValidText(string strRegEx, string strText)
        //{
        //    bool isValid = true;

        //    Regex regex = new Regex(strRegEx);

        //    foreach (char c in strText)
        //    {
        //        if (!regex.IsMatch(c.ToString()))
        //        {
        //            isValid = false;
        //        }
        //    }

        //    return isValid;
        //}

        protected void txtDNNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDNNo.Text.Trim() == "")
                {
                    grdDNData.SelectedIndex = -1;
                    grdDNData.Visible = false;
                    btnDelete.Visible = false;
                    btnExecute.Visible = false;
                    btnClear.Visible = false;
                }
                else
                {
                    if (!(isValidText(@"[A-Za-z0-9]", txtDNNo.Text.Trim().ToUpper())))
                    {
                        lblMessage.Text = "PLS INPUT ALPHANUMERIC CHAR ONLY!";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lblMessage.Text = "";
                        GetScannedData();
                        btnDNDetails.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                string DNNo;
                string strSuppID;
                string strLoginType = System.Configuration.ConfigurationManager.AppSettings["webFor"];

                if (Request.QueryString["DNNo"] != null)
                {
                    if (!(isValidText(@"[A-Za-z0-9]", Request.QueryString["DNNo"].ToString())))
                    {
                        MsgBox1.alert("INVALID DN NO.");
                        btnDNDetails.Enabled = false;
                        btnDelete.Enabled = false;
                        btnExecute.Enabled = false;
                        return;
                    }
                    else
                    {
                        DNNo = Request.QueryString["DNNo"].ToString();
                        btnDNDetails.Enabled = true;
                        btnDelete.Enabled = true;
                        btnExecute.Enabled = true;
                    }
                }
                else
                {
                    DNNo = txtDNNo.Text.Trim().ToUpper();
                }

                if (strLoginType == "EPPI")
                {

                    strSuppID = "";
                }
                else
                {
                    strSuppID = Session["supplierID"].ToString();
                }

                DataSet ds = new DataSet();
                ds = LotDataScanningDAL.GET_SCAN_DETAILS_BASED_FROM_DNNO_AND_SUPP_ID_BYPASS(DNNo, strSuppID, strLoginType);

                int rowsCount = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["STATUS"].ToString() == "TALLY")
                    {
                        rowsCount++;
                    }
                }
                //if (rowsCount > 0)
                //{
                //    lblMessage.Text = "DN QTY AND SCANNED QTY NOT TALLY!";
                //    lblMessage.ForeColor = System.Drawing.Color.Red;
                //}
                if (ds.Tables[0].Rows.Count == 0)
                {
                    lblMessage.Text = "NO RECORD TO EXECUTE!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }

                if (rowsCount == ds.Tables[0].Rows.Count)
                {
                    DataSet ds2 = new DataSet();
                    ds = LotDataScanningDAL.GET_SCAN_DETAILS_BASED_FROM_DNNO_AND_SUPP_ID_BYPASS(DNNo, strSuppID, strLoginType);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        procflag = ds.Tables[0].Rows[0]["PROCFLAG"].ToString();
                    }

                    if (procflag == "1")
                    {
                        lblMessage.Text = "DN ALREADY EXECUTED";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        string strLoginType2 = System.Configuration.ConfigurationManager.AppSettings["webFor"];
                        LotDataScanningDAL.UPDATE_PROCFLAG(DNNo, strSuppID, Session["UserID"].ToString(), strLoginType2);
                        lblMessage.Text = "DN SUCCESSFULLY EXECUTED";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        btnDelete.Enabled = false;
                    }
                }
                else
                {
                    lblMessage.Text = "DN QTY & SCANNED QTY NOT TALLY!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
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
                grdDNData.SelectedIndex = -1;
                grdDNData.Visible = false;
                btnDelete.Visible = false;
                btnClear.Visible = false;
                btnExecute.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void grdDNData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //var dnqty = (Label)e.Row.FindControl("DN QTY");
                //var scannedqty = (Label)e.Row.FindControl("SCANNED QTY");
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv["DNQTY"].ToString() != drv["SCANNEDQTY"].ToString())
                // if (dnqty.Text != scannedqty.Text)
                {
                    //e.Row.Cells[2].Text = "IN";
                    //e.Row.Cells[2].BackColor = Color.Blue;
                    e.Row.Cells[2].ForeColor = Color.Red;
                }
            }
        }
    }
}
