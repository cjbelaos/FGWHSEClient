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
    public partial class PartsStoringInquiryChildLot : System.Web.UI.Page
    {

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
                            SearchResult(Request.QueryString["ReferenceNo"].ToString());
                            OnLoad();
                        }
                    }
                }
                else
                {
                    if (!this.IsPostBack)
                    {
                        SearchResult(Request.QueryString["ReferenceNo"].ToString());
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
            PartsLocationCheckDAL maint = new PartsLocationCheckDAL();
            DataSet dvPLC = new DataSet();

            dvPLC = maint.GetPartsStoringInquiryChild(Request.QueryString["ReferenceNo"].ToString());

            txtRefNo.Text = dvPLC.Tables[0].Rows[0]["MotherLot"].ToString();
            txtItemCode.Text = dvPLC.Tables[0].Rows[0]["PartCode"].ToString();
        }

        private void SearchResult(string refno)
        {
                    PartsLocationCheckDAL maint = new PartsLocationCheckDAL();
                    DataSet dvPLC = new DataSet();

                    string refNo = Request.QueryString["ReferenceNo"].ToString();
                    dvPLC = maint.GetPartsStoringInquiryChild(Request.QueryString["ReferenceNo"].ToString());

                    if (dvPLC.Tables[0].Rows.Count > 0)
                    {
                        gvPLC.DataSource = dvPLC;
                        gvPLC.DataBind();
                        gvPLC.Visible = true;
                    }
                    else
                    {
                        MsgBox1.alert("NO DATA FOUND!");
                        //  this.gvScannedData.Rows.Clear();
                        //  gvScannedData.DataSource = null;
                        gvPLC.DataSource = null;
                        gvPLC.DataBind();
                        //gvScannedData.Visible = false;
                    }
                 
        }

        public void ExportToExcel()
        {
            try
            {
                string filename = txtRefNo.Text +"_ChildLot_" + DateTime.Now.ToString("(yyyyMMddHHmmss)");
                //Turn off the view stateV 225 55
                this.EnableViewState = false;
                //Remove the charset from the Content-Type header
                Response.Charset = String.Empty;

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
                Response.Charset = "";
                gvPLC.AllowPaging = false;
                this.SearchResult(Request.QueryString["ReferenceNo"].ToString());
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
                if (gvPLC.Rows.Count == 0)
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

        protected void gvPLC_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPLC.PageIndex = e.NewPageIndex;
            SearchResult(Request.QueryString["ReferenceNo"].ToString());
        }

        protected void gvPLC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (e.Row.Cells[5].Text.Contains("ERROR:") == true)
                {
                    // change the color 
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;

                }

            } 

        }
    }
}
