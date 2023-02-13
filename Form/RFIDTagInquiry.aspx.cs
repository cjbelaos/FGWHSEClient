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

namespace FGWHSEClient.Form
{
    public partial class RFIDTagInquiry : System.Web.UI.Page
    {
        RFIDReceivingDAL RFIDReceivingDAL = new RFIDReceivingDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    Initialize();
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
            if ((!(isDate(txtDateFrom.Text))) || (!(isDate(txtDateTo.Text))))
            {
                MsgBox1.alert("INVALID DATE!");
            }
            else
            {
                if (isDate(txtDateFrom.Text) && isDate(txtDateTo.Text) && txtRFIDTag.Text.Trim() != "")
                {
                    SearchResult();
                }
                else
                {
                    MsgBox1.alert("INCOMPLETE INPUT IN REQUIRED FIELDS!");
                    gvScannedData.DataSource = null;
                    gvScannedData.DataBind();
                }
            }
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

            //int difDate = (dateto - datefrom).Days;

            int difDate = ((dateto.Year - datefrom.Year) * 12) + dateto.Month - datefrom.Month;

            if (difDate <= 5)
            {


                DataSet ds = new DataSet();
                ds = RFIDReceivingDAL.RFIDTAG_INQUIRY(txtDateFrom.Text, txtDateTo.Text, txtRFIDTag.Text.Trim());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvScannedData.DataSource = RFIDReceivingDAL.RFIDTAG_INQUIRY(txtDateFrom.Text, txtDateTo.Text, txtRFIDTag.Text.Trim());
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
                MsgBox1.alert("SELECTED DATES ARE GREATER THAN MAXIMUM (6 MONTHS ONLY).");
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {

            try
            {
                if (gvScannedData.Rows.Count == 0)
                {
                    MsgBox1.alert("YOU CANNOT DOWNLOAD AN EMPTY RECORD!");
                }
                else
                {
                    ExportToExcel();
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.Message);
                Logger.GetInstance().Fatal(ex.StackTrace);
                throw ex;
            }

        }

        public void ExportToExcel()
        {
            try
            {
                string filename = "RFID Tag Inquiry" + DateTime.Now.ToString("(yyyyMMddHHmmss)");
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        protected void gvScannedData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvScannedData.PageIndex = e.NewPageIndex;
            SearchResult();
        }
    }
}
