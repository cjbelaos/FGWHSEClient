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
using System.Drawing;

namespace FGWHSEClient.Form
{
    public partial class RFIDLotInquiry : System.Web.UI.Page
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
            try
            {
                if ((!(isDate(txtDateFrom.Text))) || (!(isDate(txtDateTo.Text))))
                {
                    MsgBox1.alert("INVALID DATE!");
                }
                else
                {
                    SearchResult();
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
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

            if (difDate <= 6)
            {


                DataSet ds = new DataSet();
                ds = LotDataScanningDAL.GET_RFIDLOT_INQUIRY(txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), strSuppID, txtRFIDTag.Text.Trim(), txtQRCode.Text.Trim(), strLoginType, txtItemCode.Text.Trim());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvRFIDLot.DataSource = LotDataScanningDAL.GET_RFIDLOT_INQUIRY(txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), strSuppID, txtRFIDTag.Text.Trim(), txtQRCode.Text.Trim(), strLoginType, txtItemCode.Text.Trim());
                    gvRFIDLot.DataBind();
                    gvRFIDLot.Visible = true;
                }
                else
                {
                    MsgBox1.alert("NO DATA FOUND!");
                    //  this.gvScannedData.Rows.Clear();
                    //  gvScannedData.DataSource = null;
                    gvRFIDLot.DataSource = null;
                    gvRFIDLot.DataBind();
                    //gvScannedData.Visible = false;
                }
            }
            else
            {
                MsgBox1.alert("SELECTED DATES ARE GREATER THAN MAXIMUM (7 DAYS ONLY).");
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
                gvRFIDLot.AllowPaging = false;
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

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvRFIDLot.Rows.Count == 0)
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

        protected void gvRFIDLot_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRFIDLot.PageIndex = e.NewPageIndex;
            SearchResult();
        }
    }
}
