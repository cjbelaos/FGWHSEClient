﻿using System;
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
    public partial class RFIDLotMatchInquiry : System.Web.UI.Page
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


        private void SearchResult()
        {

            DateTime datefrom = Convert.ToDateTime(txtDateFrom.Text);
            DateTime dateto = Convert.ToDateTime(txtDateTo.Text);

            int difDate = (dateto - datefrom).Days;
            //int difDate = ((dateto.Year - datefrom.Year) * 12) + dateto.Month - datefrom.Month;

            if (difDate <= 6)
            {

                //if ((txtRefNo.Text.Trim() != "") || (txtItemCode.Text.Trim() != "") || (txtRFID.Text.Trim() != ""))
                //{
                    PartsLocationCheckDAL maint = new PartsLocationCheckDAL();
                    DataView dvPLC = new DataView();

                    dvPLC = maint.GetRFIDMatchingInquiry(txtRefNo.Text.Trim(), txtRFID.Text.Trim(), txtItemCode.Text.Trim(), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim());

                    if (dvPLC.Table.Rows.Count > 0)
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

                //}
                //else
                //{
                //    MsgBox1.alert("PLEASE INPUT SEARCH DATA");
                //}
            }
            else
            {
                MsgBox1.alert("SELECTED DATES ARE GREATER THAN MAXIMUM (7 DAYS ONLY).");
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
                gvPLC.AllowPaging = false;
                this.SearchResult();

                // If you want the option to open the Excel file without saving then
                // comment out the line below
                // Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.xls";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);


                using (StringWriter sw = new StringWriter())
                {

                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    foreach (GridViewRow row in gvPLC.Rows)
                    {

                        row.Cells[6].CssClass = "textmode";

                        //foreach (TableCell cell in row.Cells)
                        //{
                        //    cell.CssClass = "textmode";
                        //}
                    }

                    grid.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();

                }


                //// If you want the option to open the Excel file without saving then
                //// comment out the line below
                //// Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.ContentType = "application/vnd.xls";
                //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                //grid.RenderControl(htmlWrite);

                ////Append CSS file

                //System.IO.FileInfo fi = new System.IO.FileInfo(Server.MapPath("StyleSheet.css"));
                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //System.IO.StreamReader sr = fi.OpenText();
                //while (sr.Peek() >= 0)
                //{
                //    sb.Append(sr.ReadLine());
                //}
                //sr.Close();

                //Response.Write("<html><head><style type='text/css'>" + sb.ToString() + "</style><head>" + stringWrite.ToString() + "</html>");

                //stringWrite = null;
                //htmlWrite = null;

                //// Response.Write(stringWrite.ToString());

                //Response.Flush();
                //Response.End();
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
            SearchResult();
        }

        protected void gvPLC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (e.Row.Cells[4].Text.Contains("ERROR:") == true)
                {
                    // change the color 
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;

                }

            }

        }
    }
}
