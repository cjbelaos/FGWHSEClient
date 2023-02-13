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

namespace FGWHSEClient
{
    public partial class OutstandingDNInquiry : System.Web.UI.Page
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
                            txtDNNo.Focus();
                        }
                    }
                }
                else
                {
                    if (Request.QueryString["DeviceType"] != null)
                    {
                        if (Request.QueryString["DeviceType"].ToString() == "Denso")
                        {
                            Response.Write("<script>");
                            Response.Write("alert('User not authorized!');");
                            Response.Write("window.location = '../DENSOLogin.aspx';");
                            Response.Write("</script>");
                        }
                        else if (Request.QueryString["DeviceType"].ToString() == "HT")
                        {
                            Response.Write("<script>");
                            Response.Write("alert('User not authorized!');");
                            Response.Write("window.location = '../HTLogin.aspx';");
                            Response.Write("</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>");
                        Response.Write("alert('User not authorized!');");
                        Response.Write("window.location = '../Login.aspx';");
                        Response.Write("</script>");
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void gvOutstandingData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvOutstandingData.PageIndex = e.NewPageIndex;
                SearchResult();
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
                if (!(isValidText(@"[A-Za-z0-9]", txtDNNo.Text)))
                {
                    MsgBox1.alert("INVALID DN NO.!");
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

        private void SearchResult()
        {
            string strSuppID;
            //string strLoginType = System.Configuration.ConfigurationManager.AppSettings["webFor"]; 
            strSuppID = Session["supplierID"].ToString();

            DataSet ds = new DataSet();
            ds = LotDataScanningDAL.OUTSTANDING_DN_INQUIRY(strSuppID, txtDNNo.Text.Trim());
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvOutstandingData.DataSource = LotDataScanningDAL.OUTSTANDING_DN_INQUIRY(strSuppID, txtDNNo.Text.Trim());
                gvOutstandingData.DataBind();
                gvOutstandingData.Visible = true;
            }
            else
            {
                MsgBox1.alert("NO DATA FOUND!");
                //  this.gvScannedData.Rows.Clear();
                //  gvScannedData.DataSource = null;
                gvOutstandingData.DataSource = null;
                gvOutstandingData.DataBind();
                //gvScannedData.Visible = false;
            }

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvOutstandingData.Rows.Count == 0)
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
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        public void ExportToExcel()
        {
            try
            {
                string filename = "Outstanding DN Inquiry" + DateTime.Now.ToString("(yyyyMMddHHmmss)");
                //Turn off the view stateV 225 55
                this.EnableViewState = false;
                //Remove the charset from the Content-Type header
                Response.Charset = String.Empty;

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
                Response.Charset = "";
                gvOutstandingData.AllowPaging = false;
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
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }
    }
}
