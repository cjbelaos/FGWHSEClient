using com.eppi.utils;
using FGWHSEClient.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OleDb;
using System.IO;
using System.Collections.Generic;

using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

using System.Threading;



namespace FGWHSEClient.Form
{
    public partial class AutoLine_Barcode_Inquiry : System.Web.UI.Page
    {
        AutoLineDAL autoDAL = new AutoLineDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                txtDateFrom.Text = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy")) + " 08:00 AM";
                txtDateTo.Text = Convert.ToString(DateTime.Now.AddDays(1).ToString("dd-MMM-yyyy")) + " 08:00 AM";

                getGrid();
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            getGrid();
        }

        public void getGrid()
        {

            DateTime datefrom = Convert.ToDateTime(txtDateFrom.Text);
            DateTime dateto = Convert.ToDateTime(txtDateTo.Text);

            int difDate = (dateto - datefrom).Days;

            if (difDate <= 6)
            {
                DataSet DS = new DataSet();
                DS = autoDAL.GET_AUTOLINE_BARCODE_LIST(txtOldRefNo.Text.Trim(),txtRefNo.Text.Trim(), txtLotNo.Text.Trim(), txtPartCode.Text.Trim(), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim());

                grdLot.DataSource = DS.Tables[0];
                grdLot.DataBind();
            }
            else
            {
                Response.Write("<script>");
                Response.Write("alert('SELECTED DATES ARE GREATER THAN MAXIMUM (7 DAYS ONLY).');");
                Response.Write("</script>");
            }

           
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        public void ExportToExcel(GridView grd, string strFilename, DataTable dt)
        {
            try
            {
                //GridView gvScannedData = new GridView();
                //DataSet ds = maint.GET_PAIRING_INFO(txtDateFrom.Text, txtDateTo.Text, Session["supplierID"].ToString(), txtRFIDTAG.Text, txtItemCode.Text, txtLotNo.Text, txtRefno.Text, ddStat.SelectedValue.ToString());
                //DataTable dt = ds.Tables[0];
                DataView view = new DataView(dt);

                if (view.Table.Rows.Count > 0)
                {

                    DataTable resultTable = view.ToTable();
                    grd.DataSource = resultTable;
                    grd.DataBind();
                    Response.ClearContent();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment; filename= " + strFilename + "-" + DateTime.Now.ToString("(yyyyMMddHHmmss)") + ".xls");
                    Response.ContentType = "application/ms-excel";
                    Response.Charset = "";


                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        foreach (GridViewRow row in grd.Rows)
                        {
                            foreach (TableCell cell in row.Cells)
                            {
                                cell.CssClass = "textmode";
                            }
                        }


                        grd.RenderControl(hw);


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
                    Response.Write("<script>");
                    Response.Write("alert('NO RECORDS FOUND!');");
                    Response.Write("</script>");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (grdLot.Rows.Count > 0)
            {
                ExportToExcel(grdLot, "Barcode Label Inquiry", autoDAL.GET_AUTOLINE_BARCODE_LIST(txtOldRefNo.Text.Trim(), txtRefNo.Text.Trim(), txtLotNo.Text.Trim(), txtPartCode.Text.Trim(), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim()).Tables[0]);
            }
        }
    }
}