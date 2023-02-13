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

namespace FGWHSEClient
{
    public partial class ReceivedLots : System.Web.UI.Page
    {
        public DeliveryReceivingDAL drDal = new DeliveryReceivingDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                FillSupplier();
            }
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string strDateFrom = "";
            string strDateTo = "";
            string strSupplier = "";

            strDateFrom = txtDateFrom.Text.ToString().Trim().ToUpper();
            strDateTo = txtDateTo.Text.ToString().Trim().ToUpper();

            DateTime dtFrom = Convert.ToDateTime(strDateFrom);
            DateTime dtTo = Convert.ToDateTime(strDateTo);

            TimeSpan t = dtTo - dtFrom;
            double NrOfDays = t.TotalDays;

            if (NrOfDays > 30)
            {
                MsgBox1.alert("Exceeded date range! (1 month data only)");
            }
            else
            {
                if (ddlSupplier.SelectedItem.Text.ToString().Trim().ToUpper() == "ALL")
                {
                    strSupplier = "";
                }
                else
                {
                    strSupplier = ddlSupplier.SelectedValue.ToString().Trim().ToUpper();
                }


                DataSet ds = new DataSet();
                ds = drDal.GET_RECEIVED_LOTS(Convert.ToDateTime(strDateFrom), Convert.ToDateTime(strDateTo), strSupplier);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvReceivedLots.DataSource = ds;
                    gvReceivedLots.DataBind();
                }
                else
                {
                    gvReceivedLots.DataSource = null;
                    gvReceivedLots.DataBind();
                }
            }
            

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {

                if (gvReceivedLots.Rows.Count == 0)
                {
                    MsgBox1.alert("You cannot download an empty record!");
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
                string filename = "Received Lots " + DateTime.Now.ToString("(yyyyMMddHHmmss)");
                //Turn off the view stateV 225 55
                this.EnableViewState = false;
                //Remove the charset from the Content-Type header
                Response.Charset = String.Empty;

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
                Response.Charset = "";
                gvReceivedLots.AllowPaging = false;
                //this.SearchResult();
                // If you want the option to open the Excel file without saving then
                // comment out the line below
                // Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.xls";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                divRcvd.RenderControl(htmlWrite);

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
