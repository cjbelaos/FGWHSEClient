using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.eppi.utils;
using System.Xml.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using FGWHSEClient.LdapService;
using FGWHSEClient.LogInService;
using FGWHSEClient.DAL;

namespace FGWHSEClient.Form
{
    public partial class DNReceivingExecuteScreen : System.Web.UI.Page
    {
        public DeliveryReceivingDAL drDal = new DeliveryReceivingDAL();
        public string strPageSubsystem = "";
        public string strAccessLevel = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Write("<script>");
                Response.Write("alert('Session expired! Please log in again.');");
                Response.Write("window.location = '../Login.aspx';");
                Response.Write("</script>");
            }
            else
            {
                //strPageSubsystem = "FGWHSE_016";
                //if (!checkAuthority(strPageSubsystem))
                //{
                //    Response.Write("<script>");
                //    Response.Write("alert('You are not authorized to access the page.');");
                //    Response.Write("window.location = 'Default.aspx';");
                //    Response.Write("</script>");
                //}
                if (!this.IsPostBack)
                {
                    Initialize();
                }
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


        public void Initialize()
        {
            txtDateFrom.Text = Convert.ToString(DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy")) + " 08:00 AM";
            txtDateTo.Text = Convert.ToString(DateTime.Now.AddDays(1).ToString("dd-MMM-yyyy")) + " 08:00 AM";
            
            FillSupplier();
            FillStatus();
            //FillGrid();
            
            
        }

        private void FillSupplier()
        {
            DataTable dtFillSupplier = new DataTable();
            dtFillSupplier = drDal.DN_GetSupplier().Tables[0];

            DataTable dtSupplier = new DataTable();


            dtSupplier.Columns.Add("supplier_id", typeof(string));
            dtSupplier.Columns.Add("supplier_name", typeof(string));
            foreach (DataRow row in dtFillSupplier.Rows)
            {
                String supplierid = (string)row[0];
                String suppliername = (string)row[1];
                dtSupplier.Rows.Add(supplierid, suppliername);
            }

            ddlSupplier.DataSource = dtSupplier;
            ddlSupplier.DataTextField = "supplier_name";
            ddlSupplier.DataValueField = "supplier_id";
           
            ddlSupplier.DataBind();
            ddlSupplier.Items.Insert(0, "ALL");
        }

        private void FillStatus()
        {
            DataTable dtFillStatus = new DataTable();
            dtFillStatus = drDal.DN_GetDNReceiveStatus().Tables[0];

            DataTable dtStatus = new DataTable();


            dtStatus.Columns.Add("status_id", typeof(string));
            dtStatus.Columns.Add("status_name", typeof(string));
            foreach (DataRow row in dtFillStatus.Rows)
            {
                String statusid = (string)Convert.ToString(row[0]);
                String statusname = (string)row[1];
                dtStatus.Rows.Add(statusid, statusname);
            }

            ddlStatus.DataSource = dtStatus;
            ddlStatus.DataTextField = "status_name";
            ddlStatus.DataValueField = "status_id";
           
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, "ALL");
        }
        private void FillGrid()
        {
            string strDNNo = "";
            string strDateFrom = "";
            string strDateTo = "";
            string strSupplier = "";
            string strStatus = "";

            strDNNo = "%" + txtDNNo.Text.ToString().Trim().ToUpper() + "%";
            strDateFrom = txtDateFrom.Text.ToString().Trim().ToUpper();
            strDateTo = txtDateTo.Text.ToString().Trim().ToUpper();

            if (ddlSupplier.SelectedItem.Text.ToString().Trim().ToUpper() == "ALL")
            {
                strSupplier = "%%";
            }
            else
            {
                strSupplier = ddlSupplier.SelectedValue.ToString().Trim().ToUpper();
            }

            if (ddlStatus.SelectedItem.Text.ToString().Trim().ToUpper() =="ALL")
            {
                strStatus = "%%";
            }
            else
            {
                strStatus = ddlStatus.SelectedValue.ToString().Trim().ToUpper();
            }

            DataView dv = new DataView();
            dv = drDal.DN_GetDNReceivingExecuteScreen(strDNNo,
                                                       strDateFrom,
                                                       strDateTo,
                                                       strSupplier,
                                                       strStatus);
            if (dv.Count > 0)
            {
                if (dv.Table.Rows.Count > 0)
                {
                    pnlDN.Visible = true;
                    gvDNReceiving.DataSource = dv;
                    gvDNReceiving.DataBind();
                }
            }
            else
            {
                pnlDN.Visible = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FillGrid();
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
           
        }

        protected void gvDNReceiving_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                int rownum = 0;
                rownum++;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int vColumnCnt = -1;

                    foreach (TableCell vCell in e.Row.Cells)
                    {
                        vColumnCnt++;
                        if (gvDNReceiving.HeaderRow.Cells[vColumnCnt].Text.ToString().ToUpper() == "DN NO.")
                        {
                           
                            string Location = ResolveUrl("~/Form/DNReceivingDetailsScreen.aspx?DNNo=" + e.Row.Cells[0].Text);
                            e.Row.Cells[vColumnCnt].Attributes["onClick"] = string.Format("javascript:window.location='{0}';", Location);
                            e.Row.Cells[vColumnCnt].Style["cursor"] = "pointer";
                            e.Row.Cells[vColumnCnt].Style["color"] = "blue";
                            e.Row.Cells[vColumnCnt].Style["text-decoration"] = "underline";
                        }
                        //else if (gvDNReceiving.HeaderRow.Cells[vColumnCnt].Text.ToString().ToUpper() == "INVOICE NO")
                        //{
                            
                        //    string Location = ResolveUrl("~/CheckItemsSummary.aspx?ID=" + e.Row.Cells[1].Text);
                        //    e.Row.Cells[vColumnCnt].Attributes["onClick"] = string.Format("javascript:window.location='{0}';", Location);
                        //    e.Row.Cells[vColumnCnt].Style["cursor"] = "pointer";
                        //    e.Row.Cells[vColumnCnt].Style["color"] = "blue";
                        //    e.Row.Cells[vColumnCnt].Style["text-decoration"] = "underline";
                        //}
                        else if (gvDNReceiving.HeaderRow.Cells[vColumnCnt].Text.ToString().ToUpper() == "DIFFERENCE")
                        {
                            if (e.Row.Cells[vColumnCnt].Text != "0")
                            {
                                e.Row.Cells[vColumnCnt].ForeColor = Color.Red;
                            }
                        }
                        else if  (gvDNReceiving.HeaderRow.Cells[vColumnCnt].Text.ToString().ToUpper() == "DN BYPASS")
                        {
                            if (e.Row.Cells[vColumnCnt].Text == "YES")
                            {
                                e.Row.Cells[vColumnCnt].ForeColor = Color.Red;
                            }
                        }
                        else if (gvDNReceiving.HeaderRow.Cells[vColumnCnt].Text.ToString().ToUpper() == "STATUS")
                        {
                            if (e.Row.Cells[vColumnCnt].Text == "ONGOING UNLOADING")
                            {

                                e.Row.BackColor = Color.FromArgb(204, 255, 204);
                            }
                            else if (e.Row.Cells[vColumnCnt].Text == "COMPLETE DELIVERY")
                            {

                                e.Row.BackColor = Color.FromArgb(163, 189, 221);
                                
                            }
                            else if (e.Row.Cells[vColumnCnt].Text == "CONFIRMED GR")
                            {

                                e.Row.BackColor = Color.FromArgb(255, 255, 164);

                            }
                            else if (e.Row.Cells[vColumnCnt].Text == "DELETED DN")
                            {

                                e.Row.BackColor = Color.FromArgb(127, 127, 127);

                            }
                            else if (e.Row.Cells[vColumnCnt].Text == "CHANGE DN")
                            {

                               

                                e.Row.BackColor = Color.FromArgb(252, 120, 123);

                            }
                            else
                            {
                                e.Row.BackColor = Color.White;
                            }
                        }
                        else if (gvDNReceiving.HeaderRow.Cells[vColumnCnt].Text.ToString().ToUpper() == "DN BYPASS")
                        {
                            if (e.Row.Cells[vColumnCnt].Text == "YES")
                            {

                                e.Row.ForeColor = Color.Red;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {

                if (gvDNReceiving.Rows.Count == 0)
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
                string filename = "DN Receiving " + DateTime.Now.ToString("(yyyyMMddHHmmss)");
                //Turn off the view stateV 225 55
                this.EnableViewState = false;
                //Remove the charset from the Content-Type header
                Response.Charset = String.Empty;

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
                Response.Charset = "";
                gvDNReceiving.AllowPaging = false;
                //this.SearchResult();
                // If you want the option to open the Excel file without saving then
                // comment out the line below
                // Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.xls";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                gvDNReceiving.RenderControl(htmlWrite);

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

    }
}
