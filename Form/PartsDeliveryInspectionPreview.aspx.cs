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
using FGWHSEClient.DAL;
using System.IO;
using System.Drawing;

namespace FGWHSEClient.Form
{
    public partial class PartsDeliveryInspectionPreview : System.Web.UI.Page
    {
        PartsLocationCheckDAL PartsLocationCheckDAL = new PartsLocationCheckDAL();
        public string strDN;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SearchResult();
                //if (Request.QueryString["print"].ToString().Trim().ToUpper() == "1")
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "ShowRequest", "PrintDivContent();", true);
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "closePage", "self.close();", true);
                //}
                //else 
                if (Request.QueryString["print"].ToString().Trim().ToUpper() == "2")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowRequest", "PrintDivContent();", true);
                }
            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message.ToString());
            }

            
        }


        private void SearchResult()
        {
            try
            {
                string strDNStart =Request.QueryString["DNNO"].ToString().Substring(0,1);

                if (strDNStart == "R" || strDNStart == "r")
                {
                    lblTitle.Text = "LOCAL SUPPLIER'S DELIVERY - PARTS INSPECTION MATRIX";
                    lblTitle.ForeColor = Color.Green;
                }
                else
                {
                    lblTitle.Text = "NLI-B DELIVERY - PARTS INSPECTION MATRIX";
                    lblTitle.ForeColor = Color.Blue;
                }

                DataSet ds = new DataSet();
                ds = PartsLocationCheckDAL.GET_PARTS_DELIVERY_INSPECTION(Request.QueryString["DNNO"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdDNData.DataSource = PartsLocationCheckDAL.GET_PARTS_DELIVERY_INSPECTION(Request.QueryString["DNNO"].ToString());
                    grdDNData.DataBind();
                    grdDNData.Visible = true;

                    //  lblDN.Text = "*"+ds.Tables[0].Rows[0]["DN"].ToString()+"*";
                    strDN = "*" + ds.Tables[0].Rows[0]["DN"].ToString() + "*";
                    lblDRNo.Text = ds.Tables[0].Rows[0]["DR"].ToString();
                    lblInvoice.Text = ds.Tables[0].Rows[0]["INVOICE"].ToString();
                    lblSupplier.Text = ds.Tables[0].Rows[0]["SUPPLIERNAME"].ToString();
                }
                else
                {
                    MsgBox1.alert("NO DATA FOUND!");
                    grdDNData.DataSource = null;
                    grdDNData.DataBind();
                    //txtDNNo.Text = "";
                    //txtDNNo.Focus();
                }
            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message.ToString());
            }

        }

        protected void grdDNData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                // In template column,
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    //if (drv["InspectionReqmt"].ToString() == "NO INSPECTION")
                    //{
                    //    e.Row.BackColor = ColorTranslator.FromHtml("#F2DCDB");
                    //    e.Row.ForeColor = Color.Red;
                    //}
                    if (drv["InspectionReqmt"].ToString().Contains("MCB") || drv["InspectionReqmt"].ToString().Contains("ELECTRICAL"))
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#99FFCC"); //GREEN
                        e.Row.ForeColor = ColorTranslator.FromHtml("#3D652E");
                    }
                    //if (drv["InspectionReqmt"].ToString() == "FOR PIS")
                    //{
                    //    e.Row.BackColor = ColorTranslator.FromHtml("#FFFFCC");
                    //    e.Row.ForeColor = ColorTranslator.FromHtml("#633200");
                    //}
                    else if (drv["InspectionReqmt"].ToString() == "NO MASTER DATA")
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#F2DCDB"); //RED
                        e.Row.ForeColor = Color.Red;
                    }
                    else if (drv["InspectionReqmt"].ToString() == "NO INSPECTION")
                    {
                        e.Row.BackColor = Color.White; //WHITE
                        e.Row.ForeColor = Color.Black;
                    }
                    else if (drv["InspectionReqmt"].ToString().Contains("OPTICAL") || drv["InspectionReqmt"].ToString().Contains("MISCELLANEOUS"))
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#FFCCFF"); //LAVENDER
                        e.Row.ForeColor = Color.Black;
                    }
                    else
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#FFFFCC"); //YELLOW
                        e.Row.ForeColor = ColorTranslator.FromHtml("#633200");
                    }

                    if (drv["TYPE"].ToString() == "PLASTIC" || drv["TYPE"].ToString() == "TRAY")
                    {
                        e.Row.Cells[6].BackColor = ColorTranslator.FromHtml("#8EA9DB"); //LIGHT BLUE
                        e.Row.Cells[6].ForeColor = Color.Black;
                    }

                    if (drv["FLOORID"].ToString() == "2F")
                    {
                        e.Row.Cells[5].BackColor = ColorTranslator.FromHtml("#FC792A"); //ORANGE
                        e.Row.Cells[5].ForeColor = Color.Black;
                    }
                }
            }
            catch (Exception ex)
            {
                // Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }
    }
}
