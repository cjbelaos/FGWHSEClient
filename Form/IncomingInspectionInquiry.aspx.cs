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
using System.IO;

namespace FGWHSEClient.Form
{
    public partial class IncomingInspectionInquiry : System.Web.UI.Page
    {
        PartsLocationCheckDAL PartsLocationCheckDAL = new PartsLocationCheckDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (Session["UserID"] == null)
                //{
                //        Response.Write("<script>");
                //        Response.Write("alert('User not authorized! Please log in.');");
                //        Response.Write("window.location = '../Login.aspx';");
                //        Response.Write("</script>");
                //}
                //else
                //{
                if (!this.IsPostBack)
                {
                    txtDNNo.Focus();
                }
                //}

            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        //public void Initialize()
        //{
        //    try
        //    {
        //        txtDateFrom.Text = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy")) + " 08:00 AM";
        //        txtDateTo.Text = Convert.ToString(DateTime.Now.AddDays(1).ToString("dd-MMM-yyyy")) + " 08:00 AM";
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.GetInstance().Fatal(ex.StackTrace, ex);
        //        MsgBox1.alert(ex.Message);
        //    }
        //}

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchResult();
        }

        private void SearchResult()
        {
            DataSet ds = new DataSet();
            ds = PartsLocationCheckDAL.GET_INCOMINGINSPECTION(txtDNNo.Text.Trim());
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvIncoming.DataSource = PartsLocationCheckDAL.GET_INCOMINGINSPECTION(txtDNNo.Text.Trim());
                gvIncoming.DataBind();
                gvIncoming.Visible = true;
            }
            else
            {
                MsgBox1.alert("NO DATA FOUND!");
                gvIncoming.DataSource = null;
                gvIncoming.DataBind();
                txtDNNo.Text = "";
                txtDNNo.Focus();
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvIncoming.Rows.Count == 0)
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
                string filename = "Incoming Inspection Inquiry" + DateTime.Now.ToString("(yyyyMMddHHmmss)");
                //Turn off the view stateV 225 55
                this.EnableViewState = false;
                //Remove the charset from the Content-Type header
                Response.Charset = String.Empty;

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
                Response.Charset = "";
                gvIncoming.AllowPaging = false;
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

        protected void gvIncoming_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvIncoming.PageIndex = e.NewPageIndex;
            SearchResult();
        }

        protected void gvIncoming_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv["EPPIINSPECTIONCHECK"].ToString() != "")
                {
                    //e.Row.ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                }

               
                if (drv["PARTCODE_COLOR"].ToString() == "BLACK")
                {
                    //e.Row.ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
 
                }
                else if (drv["PARTCODE_COLOR"].ToString() == "RED")
                {
                    //e.Row.ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;

                }
                else if (drv["PARTCODE_COLOR"].ToString() == "ROYALBLUE")
                {
                    //e.Row.ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.RoyalBlue;

                }




            }
        }

        public class GridDecorator
        {
            public static void MergeRows(GridView gvIncoming)
            {
                for (int rowIndex = gvIncoming.Rows.Count - 2; rowIndex >= 0; rowIndex--)
                {
                    GridViewRow row = gvIncoming.Rows[rowIndex];
                    GridViewRow previousRow = gvIncoming.Rows[rowIndex + 1];

                    //for (int i = 0; i < row.Cells.Count; i++)
                    for (int i = 0; i < 3; i++)
                    {
                        if (row.Cells[i].Text == previousRow.Cells[i].Text)
                        {
                            row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                                   previousRow.Cells[i].RowSpan + 1;
                            previousRow.Cells[i].Visible = false;
                        }
                    }
                }
            }
        }

        protected void gvIncoming_PreRender(object sender, EventArgs e)
        {
            GridDecorator.MergeRows(gvIncoming);
        }

        protected void gvIncoming_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
