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
using System.IO;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;

namespace FGWHSEClient.Form
{
    public partial class AntennaScanHistory : System.Web.UI.Page
    {

        public InHouseReceivingDAL IHR = new InHouseReceivingDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                FillArea();
                txtDateFrom.Text = Convert.ToString(DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy")) + " 08:00 AM";
                txtDateTo.Text = Convert.ToString(DateTime.Now.AddDays(1).ToString("dd-MMM-yyyy")) + " 08:00 AM";

                getdetails();


            }
        }

        public DataTable getdetails()
        {
            
            DataSet ds = IHR.GET_HOLMES_LOT_INQUIRY(txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), txtRFIDTag.Text.Trim(), txtRefNo.Text.Trim(), txtLotNo.Text.Trim(), txtPartCode.Text.Trim(), ddlArea.SelectedValue.ToString());
            DataTable dt = ds.Tables[0];

            grdDetails.DataSource = dt;
            grdDetails.DataBind();
            return dt;
        }

        private void FillArea()
        {
            DataTable dtFillStatus = new DataTable();
            dtFillStatus = IHR.INK_GET_AREA().Tables[0];

            DataTable dtStatus = new DataTable();


            dtStatus.Columns.Add("AreaID", typeof(string));
            dtStatus.Columns.Add("AreaName", typeof(string));
            foreach (DataRow row in dtFillStatus.Rows)
            {
                String id = (string)Convert.ToString(row[0]);
                String name = (string)row[1];
                dtStatus.Rows.Add(id, name);
            }

            ddlArea.DataSource = dtStatus;
            ddlArea.DataTextField = "AreaName";
            ddlArea.DataValueField = "AreaID";

            ddlArea.DataBind();
            ddlArea.Items.Insert(0, "ALL");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            getdetails();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("HOLMESLotInquiry.aspx");

        }


        public void ExportToExcel()
        {
            //try
            //{

            //    string filename = "HOLMES_LOT_LIST_" + DateTime.Now.ToString("ddMMyyyyhhmmssffffff");
            //    //Turn off the view stateV 225 55
            //    this.EnableViewState = false;
            //    //Remove the charset from the Content-Type header
            //    Response.Charset = String.Empty;

            //    Response.Clear();
            //    Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
            //    Response.Charset = "";

            //    this.getdetails();
            //    // If you want the option to open the Excel file without saving then
            //    // comment out the line below
            //    // Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //    Response.ContentType = "application/vnd.xls";
            //    System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            //    System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            //    grdDetails.RenderControl(htmlWrite);

            //    //Append CSS file

            //    System.IO.FileInfo fi = new System.IO.FileInfo(Server.MapPath("StyleSheet.css"));
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    System.IO.StreamReader sr = fi.OpenText();
            //    while (sr.Peek() >= 0)
            //    {
            //        sb.Append(sr.ReadLine());
            //    }
            //    sr.Close();

            //    Response.Write("<html><head><style type='text/css'>" + sb.ToString() + "</style><head>" + stringWrite.ToString() + "</html>");

            //    stringWrite = null;
            //    htmlWrite = null;

            //    // Response.Write(stringWrite.ToString());

            //    Response.Flush();
            //    Response.End();
            //}
            //catch (Exception ex)
            //{
            //    //Logger.GetInstance().Fatal(ex.Message);
            //    //Logger.GetInstance().Fatal(ex.StackTrace);
            //    throw ex;
            //}

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=HOLMES_LotReceive" + DateTime.Now.ToString("MMddyyyyHHmmssff") + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);


                //To Export all pages
                grdDetails.AllowPaging = false;


                foreach (TableCell cell in grdDetails.HeaderRow.Cells)
                {
                    cell.BackColor = grdDetails.HeaderStyle.BackColor;
                }

                foreach (GridViewRow row in grdDetails.Rows)
                {
                    foreach (TableCell cell in row.Cells)
                    {
                        cell.CssClass = "textmode";
                    }

                    row.Cells[1].Width = 150;
                    row.Cells[2].Width = 200;
                }

                grdDetails.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //
        }
    }
}