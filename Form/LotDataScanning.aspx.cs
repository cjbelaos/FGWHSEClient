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

namespace FGWHSEClient.Form
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        LotDataScanningDAL LotDataScanningDAL = new LotDataScanningDAL();
        string procflag;
        string bypassflag;
        string delflag;
        string dnproc;
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
                Page.Title = string.Format("MANUAL PARTS RECEIVING");
            }
            else
            {
                Page.Title = string.Format("PARTS LOADING");
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
                lblHeader.Text = "MANUAL PARTS RECEIVING";
            }
            else
            {
                lblHeader.Text = "PARTS LOADING";
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
                        lblMessage.Attributes.Add("style", "font-size:11px;");
                        tdScan.Attributes.Add("style", "width:100px;text-align:right");
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

                        //  txtDNNo.Focus();
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
                            Response.Redirect("LotDataScanningDeleteRecord.aspx?DNNo=" + txtDNNo.Text + "&DeviceType=Denso");
                        }
                        else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                        {
                            Response.Redirect("LotDataScanningDeleteRecord.aspx?DNNo=" + txtDNNo.Text + "&DeviceType=HT");
                        }
                    }
                    else
                    {
                        Response.Redirect("LotDataScanningDeleteRecord.aspx?DNNo=" + txtDNNo.Text);
                    }




                }
            }
            //  txtDNNo.Focus();
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
                ds = LotDataScanningDAL.GET_SCAN_DETAILS_BASED_FROM_DNNO_AND_SUPP_ID(DNNo, strSuppID, strLoginType);

                int errorcount = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    procflag = ds.Tables[0].Rows[0]["PROCFLAG"].ToString();

                    delflag = ds.Tables[0].Rows[0]["DELFLG"].ToString();
                    dnproc = ds.Tables[0].Rows[0]["DNPROC"].ToString();

                    if (procflag == "1")
                    {
                        lblMessage.Text = "DN ALREADY EXECUTED!";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        errorcount = 1;

                        DisableButtons();
                    }

                    if (delflag == "X")
                    {
                        lblMessage.Text = "DN DATA DELETED.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        errorcount = 1;

                        DataSet ds1 = new DataSet();
                        ds1 = LotDataScanningDAL.GET_SCAN_DETAILS_BASED_FROM_DNNO_AND_SUPP_ID(DNNo, strSuppID, strLoginType);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            int rowsCount = 0;
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string scanned = row["SCANNEDQTY"].ToString();
                                if (row["SCANNEDQTY"].ToString() != "0.000")
                                {
                                    rowsCount++;
                                }
                            }

                            if (rowsCount == 0)
                            {
                                DisableButtons();
                            }
                            else
                            {
                                DisableButtons();
                                grdDNData.Visible = true;
                                btnDelete.Enabled = true;

                                grdDNData.DataSource = LotDataScanningDAL.GET_SCAN_DETAILS_BASED_FROM_DNNO_AND_SUPP_ID(DNNo, strSuppID, strLoginType);
                                grdDNData.DataBind();
                            }
                        }
 
                    }

                    if (dnproc == "X")
                    {
                        lblMessage.Text = "DN ALREADY PROCESSED!";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        errorcount = 1;

                        DisableButtons();
                    }


                }
                else
                {
                    DataSet dsByPass = new DataSet();
                    dsByPass = LotDataScanningDAL.GET_SCAN_DETAILS_BASED_FROM_DNNO(DNNo, strSuppID, strLoginType);

                    if (dsByPass.Tables[0].Rows.Count > 0)
                    {
                        bypassflag = dsByPass.Tables[0].Rows[0]["BYPASSFLAG"].ToString();

                        if (bypassflag == "1")
                        {
                            lblMessage.Text = "INVALID! PLEASE GO TO BYPASS PAGE.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            errorcount = 1;

                            DisableButtons();
                        }
                    }
                    else
                    {

                        lblMessage.Text = "DN DATA NOT FOUND";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        errorcount = 1;

                        DisableButtons();
                        btnClear.Enabled = true;
                    }
                }

                if (errorcount == 1)
                {
                    //grdDNData.Visible = false;

                    //btnDelete.Enabled = false;
                    //btnExecute.Enabled = false;
                    //btnClear.Enabled = false;
                    //btnDNDetails.Enabled = false;

                    //btnDelete.Visible = true;
                    //btnExecute.Visible = true;
                    //btnClear.Visible = true;

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

                    grdDNData.DataSource = LotDataScanningDAL.GET_SCAN_DETAILS_BASED_FROM_DNNO_AND_SUPP_ID(DNNo, strSuppID, strLoginType);
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

        private void DisableButtons()
        {
            grdDNData.Visible = false;

            btnDelete.Enabled = false;
            btnExecute.Enabled = false;
            btnClear.Enabled = false;
            btnDNDetails.Enabled = false;

            btnDelete.Visible = true;
            btnExecute.Visible = true;
            btnClear.Visible = true;
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
                            Response.Redirect("LotDataScanningDetails.aspx?DeviceType=Denso&DNNo=" + txtDNNo.Text.Trim().ToUpper(),false);
                            return;
                        }
                        else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                        {
                            Response.Redirect("LotDataScanningDetails.aspx?DeviceType=HT&DNNo=" + txtDNNo.Text.Trim().ToUpper(), false);
                            return;
                        }
                    }
                    else
                    {
                        Response.Redirect("LotDataScanningDetails.aspx?DNNo=" + txtDNNo.Text.Trim().ToUpper());
                    }   
                   // Response.Redirect("LotDataScanningDetails.aspx?DNNo=" + txtDNNo.Text);
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
                ds = LotDataScanningDAL.GET_SCAN_DETAILS_BASED_FROM_DNNO_AND_SUPP_ID(DNNo, strSuppID, strLoginType);

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
        }

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
                     // Regex re =  new Regex("[A-Z,a-z,0-9]");

                      if (!(isValidText(@"[A-Za-z0-9]", txtDNNo.Text.Trim().ToUpper())))
                     // if (!re.IsMatch(txtDNNo.Text))
                      {
                          lblMessage.Text = "INVALID DN NO.!";
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
                ds = LotDataScanningDAL.GET_SCAN_DETAILS_BASED_FROM_DNNO_AND_SUPP_ID(DNNo, strSuppID, strLoginType);

                int rowsCount = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["STATUS"].ToString() == "NOT TALLY")
                    {
                        rowsCount++;
                    }
                }
                if (rowsCount > 0)
                {
                    lblMessage.Text = "DN QTY & SCANNED QTY NOT TALLY!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    DataSet ds2 = new DataSet();
                    ds = LotDataScanningDAL.GET_SCAN_DETAILS_BASED_FROM_DNNO_AND_SUPP_ID(DNNo, strSuppID, strLoginType);
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
            try
            {
            // In template column,
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
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }
    }
}
