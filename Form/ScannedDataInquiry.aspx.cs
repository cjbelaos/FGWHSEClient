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
using System.IO;

namespace FGWHSEClient.Form
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        LotDataScanningDAL LotDataScanningDAL = new LotDataScanningDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string strLoginType = System.Configuration.ConfigurationManager.AppSettings["webFor"];
                if (strLoginType == "OUTSIDE")
                {
                    if (Session["supplierID"] == null)
                    {
                        if (Request.QueryString["DeviceType"] != null)
                        {
                            if (Request.QueryString["DeviceType"].ToString() == "Denso")
                            {
                                Response.Write("<script>");
                                Response.Write("alert('User not authorized! Please log in.');");
                                Response.Write("window.location = '../DENSOLogin.aspx';");
                                Response.Write("</script>");
                            }
                            else if (Request.QueryString["DeviceType"].ToString() == "HT")
                            {
                                Response.Write("<script>");
                                Response.Write("alert('User not authorized! Please log in.');");
                                Response.Write("window.location = '../HTLogin.aspx';");
                                Response.Write("</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>");
                            Response.Write("alert('User not authorized! Please log in.');");
                            Response.Write("window.location = '../Login.aspx';");
                            Response.Write("</script>");
                        }
                    }
                    else
                    {
                        if (!this.IsPostBack)
                        {
                            Initialize();
                        }
                    }
                }
                else
                {
                    if (!this.IsPostBack)
                    {
                        Initialize();
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }
        public void Initialize()
        {
            try
            {
                txtDateFrom.Text = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy")) + " 08:00 AM";
                txtDateTo.Text = Convert.ToString(DateTime.Now.AddDays(1).ToString("dd-MMM-yyyy")) + " 08:00 AM";
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!(isValidText(@"[A-Za-z0-9]", txtItemCode.Text)))
            {
                MsgBox1.alert("INVALID ITEM CODE!");
            }
            else if (!(isValidText(@"[A-Za-z0-9]", txtDNNo.Text)))
            {
                MsgBox1.alert("INVALID DN NO.!");
            }
            else if ((!(isDate(txtDateFrom.Text))) || (!(isDate(txtDateTo.Text))))
            {
                MsgBox1.alert("INVALID DATE!");
            }
            else
            {
                SearchResult();
            }
        }

        private void SearchResult()
        {
            string strSuppID;
            string strLoginType = System.Configuration.ConfigurationManager.AppSettings["webFor"];
            if (strLoginType == "EPPI")
            {

                strSuppID = "";
            }
            else
            {
                strSuppID = Session["supplierID"].ToString();
            }

            DateTime datefrom = Convert.ToDateTime(txtDateFrom.Text);
            DateTime dateto = Convert.ToDateTime(txtDateTo.Text);

            int difDate = (dateto - datefrom).Days;

            string strExecute;
            if (chkExecuted.Checked == false)
            {
                strExecute = "";
            }
            else
            {
                strExecute = "0";
            }

            if (difDate <= 6)
            {


                DataSet ds = new DataSet();
                ds = LotDataScanningDAL.SCANNED_DATA_INQUIRY_NEW(txtDNNo.Text, txtItemCode.Text, txtDateFrom.Text, txtDateTo.Text, strSuppID, strLoginType, strExecute);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvScannedData.DataSource = LotDataScanningDAL.SCANNED_DATA_INQUIRY_NEW(txtDNNo.Text, txtItemCode.Text, txtDateFrom.Text, txtDateTo.Text, strSuppID, strLoginType, strExecute);
                    gvScannedData.DataBind();
                    gvScannedData.Visible = true;
                }
                else
                {
                    MsgBox1.alert("NO DATA FOUND!");
                    //  this.gvScannedData.Rows.Clear();
                    //  gvScannedData.DataSource = null;
                    gvScannedData.DataSource = null;
                    gvScannedData.DataBind();
                    //gvScannedData.Visible = false;
                }
            }
            else
            {
                MsgBox1.alert("SELECTED DATES ARE GREATER THAN MAXIMUM (7 DAYS ONLY).");
            }
        }

        //private void SearchResult()
        //{
        //    string strSuppID;
        //    string strLoginType = System.Configuration.ConfigurationManager.AppSettings["webFor"];
        //    if (strLoginType == "EPPI")
        //    {

        //        strSuppID = "";
        //    }
        //    else
        //    {
        //        strSuppID = Session["supplierID"].ToString();
        //    }

        //    DateTime datefrom = Convert.ToDateTime(txtDateFrom.Text);
        //    DateTime dateto = Convert.ToDateTime(txtDateTo.Text);

        //    int difDate = (dateto - datefrom).Days;

        //    if (difDate <= 29)
        //    {


        //        DataSet ds = new DataSet();
        //        ds = LotDataScanningDAL.SCANNED_DATA_INQUIRY(txtDNNo.Text, txtItemCode.Text, txtDateFrom.Text, txtDateTo.Text, strSuppID, strLoginType);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            gvScannedData.DataSource = LotDataScanningDAL.SCANNED_DATA_INQUIRY(txtDNNo.Text, txtItemCode.Text, txtDateFrom.Text, txtDateTo.Text, strSuppID, strLoginType);
        //            gvScannedData.DataBind();
        //            gvScannedData.Visible = true;
        //        }
        //        else
        //        {
        //            MsgBox1.alert("NO DATA FOUND!");
        //          //  this.gvScannedData.Rows.Clear();
        //          //  gvScannedData.DataSource = null;
        //            gvScannedData.DataSource = null;
        //            gvScannedData.DataBind();
        //            //gvScannedData.Visible = false;
        //        }
        //    }
        //    else
        //    {
        //        MsgBox1.alert("SELECTED DATES ARE GREATER THAN MAXIMUM (30 DAYS ONLY).");
        //    }
        //}


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

        bool isDate(string date)
        {
            try
            {

                DateTime dt = DateTime.Parse(date);
             
                return true;
            }
            catch
            {

                return false;

            }

        }


        public void ExportToExcel()
        {
            try
            {
                string filename = "Scanned Data Inquiry" + DateTime.Now.ToString("(yyyyMMddHHmmss)");
                //Turn off the view stateV 225 55
                this.EnableViewState = false;
                //Remove the charset from the Content-Type header
                Response.Charset = String.Empty;

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
                Response.Charset = "";
                gvScannedData.AllowPaging = false;
                this.SearchResult();
                // If you want the option to open the Excel file without saving then
                // comment out the line below
                // Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.xls";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                grid.RenderControl(htmlWrite);

                //Append CSS file

                System.IO.FileInfo fi = new System.IO.FileInfo(Server.MapPath("StyleSheet.css"));
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.IO.StreamReader sr = fi.OpenText();
                while (sr.Peek() >= 0)
                {
                    sb.Append(sr.ReadLine());
                }
                sr.Close();

                Response.Write("<html><head><style type='text/css'>" + sb.ToString() + "</style><head>" + stringWrite.ToString() + "</html>");

                stringWrite = null;
                htmlWrite = null;

                // Response.Write(stringWrite.ToString());

                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.Message);
                Logger.GetInstance().Fatal(ex.StackTrace);
                throw ex;
            }
        }

        public void Export()
        {
            LotDataScanningDAL LotDataScanningDAL = new LotDataScanningDAL();

            //string YEAR = txtYear.Text;
            //string MONTH = ddlMonths.SelectedValue.ToString();
            //string QUARTER = ddlQuarter.SelectedValue.ToString();
            //string WEEK = "0";

            string strSuppID;
            string strLoginType = System.Configuration.ConfigurationManager.AppSettings["webFor"];
            if (strLoginType == "EPPI")
            {

                strSuppID = "";
            }
            else
            {
                strSuppID = Session["supplierID"].ToString();
            }

            DateTime datefrom = Convert.ToDateTime(txtDateFrom.Text);
            DateTime dateto = Convert.ToDateTime(txtDateTo.Text);

            int difDate = (dateto - datefrom).Days;

            string strExecute;
            if (chkExecuted.Checked == false)
            {
                strExecute = "";
            }
            else
            {
                strExecute = "0";
            }


            /////////////export to EXCEL
            GridView gvScannedData = new GridView();
            DataSet ds = LotDataScanningDAL.SCANNED_DATA_INQUIRY_NEW(txtDNNo.Text, txtItemCode.Text, txtDateFrom.Text, txtDateTo.Text, strSuppID, strLoginType, strExecute);
            DataTable dt = ds.Tables[0];
            DataView view = new DataView(dt);

            //17AUG2019 for edit later
            if (view.Table.Rows.Count > 0)
            {


                DataTable resultTable = view.ToTable(false, "BARCODEDNNO", "RFIDTAG", "ITEMCODE", "MaterialName", "REFNO", "LOTNO", "QTY", "REMARKS", "BYPASSFLAG", "PROCFLAG", "DELFLG", "CREATEDBY", "CREATEDDATE");

                gvScannedData.DataSource = resultTable;
                gvScannedData.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=ScannedDataInquiry-" + DateTime.Now.ToString("(yyyyMMddHHmmss)") + ".xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                //StringWriter sw = new StringWriter();

                using (StringWriter sw = new StringWriter())
                {

                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    foreach (GridViewRow row in gvScannedData.Rows)
                    {
                        foreach (TableCell cell in row.Cells)
                        {
                            cell.CssClass = "textmode";
                        }
                    }


                    gvScannedData.RenderControl(hw);


                    //style to format numbers to string
                    string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();

                }

            }
            else
            {
                //showAlert("error", "Error ", "NO DATA FOUND!");
                MsgBox1.alert("YOU CANNOT DOWNLOAD AN EMPTY RECORD!");
            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }


        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //string strSuppID;
                //string strLoginType = System.Configuration.ConfigurationManager.AppSettings["webFor"];
                //if (strLoginType == "EPPI")
                //{

                //    strSuppID = "";
                //}
                //else
                //{
                //    strSuppID = Session["supplierID"].ToString();
                //}

                //DataSet ds = new DataSet();
                //ds = LotDataScanningDAL.SCANNED_DATA_INQUIRY(txtDNNo.Text, txtItemCode.Text, txtDateFrom.Text, txtDateTo.Text, strSuppID, strLoginType);
                ////if (ds.Tables[0].Rows.Count == 0)
                ////if (gvScannedData.Visible == true && gvScannedData.Rows.Count > 0)



                if (gvScannedData.Rows.Count == 0)
                {
                    MsgBox1.alert("YOU CANNOT DOWNLOAD AN EMPTY RECORD!");
                }
                else
                {
                    //ExportToExcel();
                    Export();
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.Message);
                Logger.GetInstance().Fatal(ex.StackTrace);
                throw ex;
            }


            
        }

        protected void gvScannedData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvScannedData.PageIndex = e.NewPageIndex;
            SearchResult();
        }

        protected void gvScannedData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv["PROCFLAG"].ToString().Equals("YES"))
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#bbf0b1");
                }
            }
        }
    }
}
