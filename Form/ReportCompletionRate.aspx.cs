using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace FGWHSEClient.Form
{
    public partial class ReportCompletionRate : System.Web.UI.Page
    {

        Maintenance maint = new Maintenance();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                txtDateFrom.Text = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy"));
                txtDateTo.Text = Convert.ToString(DateTime.Now.AddDays(1).ToString("dd-MMM-yyyy"));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            getCompletionRate();
        }

        public void getCompletionRate()
        {
            DataSet ds = new DataSet();

            DataView dvOD = new DataView();
            DataView dvPallet = new DataView();
           
            ds = maint.PICKING_GET_COMPLETION_RATE(txtDateFrom.Text.Trim(), txtDateTo.Text.Trim());

            grdCompletionRate.DataSource = ds.Tables[0];
            grdCompletionRate.DataBind();

            dvOD = ds.Tables[1].DefaultView;
            dvPallet = ds.Tables[2].DefaultView;

            if (dvOD.Count > 0)
            {
                lblCompletionRate.Text  =   dvOD[0]["COMPLETIONRATE"].ToString();
                lblODCompleted.Text     =   dvOD[0]["COMPLETEDCOUNT"].ToString() + "/" + dvOD[0]["ODNOCOUNT"].ToString();
            }
            else
            {
                lblCompletionRate.Text = "";
                lblODCompleted.Text = "";
            }

            if (dvPallet.Count > 0)
            {
                lblPalletCompleted.Text = dvPallet[0]["TOTALACTUALPALLET"].ToString() + "/" + dvPallet[0]["TOTALPALLET"].ToString();
            }
            else
            {
                lblPalletCompleted.Text = "";
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (grdCompletionRate.Rows.Count == 0)
            {
                MsgBox1.alert("YOU CANNOT DOWNLOAD AN EMPTY RECORD!");
            }
            else
            {
                ExportToExcel();
            }
        }


        public void ExportToExcel()
        {
            try
            {
                string filename = "CompletionRate" + DateTime.Now.ToString("(yyyyMMddHHmmss)");
                //Turn off the view stateV 225 55
                this.EnableViewState = false;
                //Remove the charset from the Content-Type header
                Response.Charset = String.Empty;

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
                Response.Charset = "";
                grdCompletionRate.AllowPaging = false;
                this.getCompletionRate();
                // If you want the option to open the Excel file without saving then
                // comment out the line below
                // Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.xls";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                dvExport.RenderControl(htmlWrite);

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


                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
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
