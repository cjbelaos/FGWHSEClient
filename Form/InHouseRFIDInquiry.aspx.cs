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
    public partial class InHouseRFIDInquiry : System.Web.UI.Page
    {
        public InHouseReceivingDAL InHouseReceivingDAL = new InHouseReceivingDAL();
        int iQty;
        GridView objGV2 = new GridView();
        GridView objGV = new GridView();
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
                if (!this.IsPostBack)
                {
                    FillArea();
                    txtDateFrom.Text = Convert.ToString(DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy")) + " 08:00 AM";
                    txtDateTo.Text = Convert.ToString(DateTime.Now.AddDays(1).ToString("dd-MMM-yyyy")) + " 08:00 AM";
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            //if (txtRefNo.Text != "")
            //{
            //    Fill();
            //}
            //else 
            if (txtPartCode.Text != "" && txtRFIDTag.Text == "" && txtRefNo.Text == "") //SEARCH BY PARTCODE
            {
                Fill_Per_PartCode();
            }
            else if (txtPartCode.Text == "" && txtRFIDTag.Text != "" && txtRefNo.Text == "") //SEARCH BY RFITAG
            {
                Fill_Per_RFIDTag();
            }
            else
            {
                Fill();
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {


                ExportInTemplate2();

            }
            catch (Exception ex)
            {
                //Logger.GetInstance().Fatal(ex.Message);
                //Logger.GetInstance().Fatal(ex.StackTrace);
                throw ex;
            }
        }

        private void ExportInTemplate2()
        {
            try
            {
                //export to excel
                //COPY EXCEL FILE
                string strFileName = "INHOUSE_INQUIRY_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                string strOriginalPath = Server.MapPath(@"~\Templates\InHouseInquiry_Template.xlsx");
                string strCopyPath = Server.MapPath(@"~\Templates\TEMP\" + strFileName);
                if (File.Exists(strOriginalPath))
                {
                    File.Copy(strOriginalPath, strCopyPath);

                    FileInfo fileExcel = new FileInfo(strCopyPath);
                    ExcelPackage excel = new ExcelPackage(fileExcel);



                    //int iRow = 7;
                    int iRow = 1;

                    DataTable dtApplication = new DataTable();
                    dtApplication.Columns.Add("NO", typeof(string));
                    dtApplication.Columns.Add("RFIDTAG", typeof(string));
                    dtApplication.Columns.Add("PARTCODE", typeof(string));
                    dtApplication.Columns.Add("REFNO", typeof(string));
                    dtApplication.Columns.Add("LOTNO", typeof(string));
                    dtApplication.Columns.Add("QTY", typeof(string));
                    dtApplication.Columns.Add("TOTALQTY", typeof(string));
                    dtApplication.Columns.Add("AREA", typeof(string));
                    dtApplication.Columns.Add("LINE", typeof(string));
                    dtApplication.Columns.Add("DATE", typeof(string));
                    dtApplication.Columns.Add("ITEMNAME", typeof(string));

                    string strArea;
                    if (ddlArea.SelectedItem.Text.ToString().Trim().ToUpper() == "ALL")
                    {
                        strArea = "0";
                    }
                    else
                    {
                        strArea = ddlArea.SelectedValue.ToString().Trim().ToUpper();
                    }

                    DataSet ds = new DataSet();

                    //if (txtRefNo.Text != "")
                    //{
                    //    //Fill();
                    //    ds = InHouseReceivingDAL.GET_RFID_INQUIRY_INHOUSE(txtRFIDTag.Text.Trim(), Convert.ToDateTime(txtDateFrom.Text), Convert.ToDateTime(txtDateTo.Text), Convert.ToInt32(strArea), txtPartCode.Text.Trim(), txtRefNo.Text.Trim());
                    //}
                    if (txtPartCode.Text != "" && txtRFIDTag.Text == "" && txtRefNo.Text == "") //SEARCH BY PARTCODE
                    {
                        //Fill_Per_PartCode();
                        ds = InHouseReceivingDAL.GET_RFID_INQUIRY_INHOUSE_BY_PARTCODE(txtPartCode.Text, Convert.ToDateTime(txtDateFrom.Text), Convert.ToDateTime(txtDateTo.Text), Convert.ToInt32(strArea));
                    }
                    else if (txtPartCode.Text == "" && txtRFIDTag.Text != "" && txtRefNo.Text == "") //SEARCH BY RFITAG
                    {
                        //Fill_Per_RFIDTag();
                        ds = InHouseReceivingDAL.GET_RFID_INQUIRY_INHOUSE_BY_RFID(txtRFIDTag.Text, Convert.ToDateTime(txtDateFrom.Text), Convert.ToDateTime(txtDateTo.Text), Convert.ToInt32(strArea));
                    }
                    else
                    {
                        ds = InHouseReceivingDAL.GET_RFID_INQUIRY_INHOUSE(txtRFIDTag.Text.Trim(), Convert.ToDateTime(txtDateFrom.Text), Convert.ToDateTime(txtDateTo.Text), Convert.ToInt32(strArea), txtPartCode.Text.Trim(), txtRefNo.Text.Trim());
                    }


                    if (ds.Tables.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables.Count; i++)
                        {
                            foreach (DataTable table in ds.Tables)
                            {

                                excel.Workbook.Worksheets["Sheet1"].Cells["A" + iRow.ToString()].Value = table.Rows[0]["PARTCODE"].ToString() + "-" + table.Rows[0]["ITEMNAME"].ToString();
                                iRow++;
                                excel.Workbook.Worksheets["Sheet1"].Cells["A" + iRow.ToString()].Value = table.Rows.Count;
                                excel.Workbook.Worksheets["Sheet1"].Cells["F" + iRow.ToString()].Value = table.Rows[0]["TOTALQTY"].ToString();
                                iRow++;
                                excel.Workbook.Worksheets["Sheet1"].Cells["A" + iRow.ToString()].Value = "NO";
                                excel.Workbook.Worksheets["Sheet1"].Cells["B" + iRow.ToString()].Value = "RFIDTAG";
                                excel.Workbook.Worksheets["Sheet1"].Cells["C" + iRow.ToString()].Value = "PARTCODE";
                                excel.Workbook.Worksheets["Sheet1"].Cells["D" + iRow.ToString()].Value = "REFNO";
                                excel.Workbook.Worksheets["Sheet1"].Cells["E" + iRow.ToString()].Value = "LOTNO";
                                excel.Workbook.Worksheets["Sheet1"].Cells["F" + iRow.ToString()].Value = "QTY";
                                excel.Workbook.Worksheets["Sheet1"].Cells["G" + iRow.ToString()].Value = "AREA";
                                excel.Workbook.Worksheets["Sheet1"].Cells["H" + iRow.ToString()].Value = "LINE";
                                excel.Workbook.Worksheets["Sheet1"].Cells["I" + iRow.ToString()].Value = "DATE";

                                //style of header
                                excel.Workbook.Worksheets["Sheet1"].Cells["A" + iRow.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                excel.Workbook.Worksheets["Sheet1"].Cells["A" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#538DD5"));
                                excel.Workbook.Worksheets["Sheet1"].Cells["A" + iRow.ToString()].Style.Font.Color.SetColor(Color.White);
                                excel.Workbook.Worksheets["Sheet1"].Cells["B" + iRow.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                excel.Workbook.Worksheets["Sheet1"].Cells["B" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#538DD5"));
                                excel.Workbook.Worksheets["Sheet1"].Cells["B" + iRow.ToString()].Style.Font.Color.SetColor(Color.White);
                                excel.Workbook.Worksheets["Sheet1"].Cells["C" + iRow.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                excel.Workbook.Worksheets["Sheet1"].Cells["C" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#538DD5"));
                                excel.Workbook.Worksheets["Sheet1"].Cells["C" + iRow.ToString()].Style.Font.Color.SetColor(Color.White);
                                excel.Workbook.Worksheets["Sheet1"].Cells["D" + iRow.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                excel.Workbook.Worksheets["Sheet1"].Cells["D" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#538DD5"));
                                excel.Workbook.Worksheets["Sheet1"].Cells["D" + iRow.ToString()].Style.Font.Color.SetColor(Color.White);
                                excel.Workbook.Worksheets["Sheet1"].Cells["E" + iRow.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                excel.Workbook.Worksheets["Sheet1"].Cells["E" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#538DD5"));
                                excel.Workbook.Worksheets["Sheet1"].Cells["E" + iRow.ToString()].Style.Font.Color.SetColor(Color.White);
                                excel.Workbook.Worksheets["Sheet1"].Cells["F" + iRow.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                excel.Workbook.Worksheets["Sheet1"].Cells["F" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#538DD5"));
                                excel.Workbook.Worksheets["Sheet1"].Cells["F" + iRow.ToString()].Style.Font.Color.SetColor(Color.White);
                                excel.Workbook.Worksheets["Sheet1"].Cells["G" + iRow.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                excel.Workbook.Worksheets["Sheet1"].Cells["G" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#538DD5"));
                                excel.Workbook.Worksheets["Sheet1"].Cells["G" + iRow.ToString()].Style.Font.Color.SetColor(Color.White);
                                excel.Workbook.Worksheets["Sheet1"].Cells["H" + iRow.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                excel.Workbook.Worksheets["Sheet1"].Cells["H" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#538DD5"));
                                excel.Workbook.Worksheets["Sheet1"].Cells["H" + iRow.ToString()].Style.Font.Color.SetColor(Color.White);
                                excel.Workbook.Worksheets["Sheet1"].Cells["I" + iRow.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                excel.Workbook.Worksheets["Sheet1"].Cells["I" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#538DD5"));
                                excel.Workbook.Worksheets["Sheet1"].Cells["I" + iRow.ToString()].Style.Font.Color.SetColor(Color.White);

                                iRow++;

                                foreach (DataRow dr in table.Rows)
                                {
                                    string NO = dr["NO"].ToString();
                                    string RFIDTAG = dr["RFIDTAG"].ToString();
                                    string PARTCODE = dr["PARTCODE"].ToString();
                                    string REFNO = dr["REFNO"].ToString();
                                    string LOTNO = dr["LOTNO"].ToString();
                                    string QTY = dr["QTY"].ToString();
                                    string TOTALQTY = dr["TOTALQTY"].ToString();
                                    string AREA = dr["AREA"].ToString();
                                    string LINE = dr["LINE"].ToString();
                                    string DATE = dr["DATE"].ToString();
                                    string ITEMNAME = dr["ITEMNAME"].ToString();

                                    dtApplication.Rows.Add(NO, RFIDTAG, PARTCODE, REFNO, LOTNO, QTY, TOTALQTY, AREA, LINE, DATE, ITEMNAME);
                                }

                                foreach (DataRow dr in table.Rows)
                                {

                                    excel.Workbook.Worksheets["Sheet1"].Cells["A" + iRow.ToString()].Value = dr["NO"].ToString().Trim();
                                    excel.Workbook.Worksheets["Sheet1"].Cells["B" + iRow.ToString()].Value = dr["RFIDTAG"].ToString().Trim();
                                    excel.Workbook.Worksheets["Sheet1"].Cells["C" + iRow.ToString()].Value = dr["PARTCODE"].ToString().Trim();
                                    excel.Workbook.Worksheets["Sheet1"].Cells["D" + iRow.ToString()].Value = dr["REFNO"].ToString().Trim();
                                    excel.Workbook.Worksheets["Sheet1"].Cells["E" + iRow.ToString()].Value = dr["LOTNO"].ToString().Trim();
                                    excel.Workbook.Worksheets["Sheet1"].Cells["F" + iRow.ToString()].Value = dr["QTY"].ToString().Trim();
                                    excel.Workbook.Worksheets["Sheet1"].Cells["G" + iRow.ToString()].Value = dr["AREA"].ToString().Trim();
                                    excel.Workbook.Worksheets["Sheet1"].Cells["H" + iRow.ToString()].Value = dr["LINE"].ToString().Trim();
                                    excel.Workbook.Worksheets["Sheet1"].Cells["I" + iRow.ToString()].Value = dr["DATE"].ToString().Trim();
                                    iRow++;


                                }
                                iRow++;
                            }

                            //excel.Workbook.Worksheets["LeaveSummary"].Protection.SetPassword("LEAVESUMMARY");
                            excel.Workbook.Worksheets["Sheet1"].Protection.AllowAutoFilter = true;
                            //excel.Workbook.Worksheets["Sheet1"].Protection.IsProtected = true;
                            excel.Save();
                            //Response.Redirect("./Templates/TEMP/" + strFileName);
                            Response.Redirect(@"~\Templates\TEMP\" + strFileName);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void ExportInTemplate()
        {
            try
            {
                if (txtPartCode.Text != "" && txtRFIDTag.Text == "" && txtRefNo.Text == "") //SEARCH BY PARTCODE
                {
                    Fill_Per_PartCode();
                }
                else if (txtPartCode.Text == "" && txtRFIDTag.Text != "" && txtRefNo.Text == "") //SEARCH BY RFITAG
                {
                    Fill_Per_RFIDTag();
                }

                string strUserID = "";
                string strUsername = "";



                //export to excel
                //COPY EXCEL FILE
                string strFileName = "INHOUSE_INQUIRY_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                string strOriginalPath = Server.MapPath(@"~\Templates\InHouseInquiry_Template.xlsx");
                string strCopyPath = Server.MapPath(@"~\Templates\TEMP\" + strFileName);
                if (File.Exists(strOriginalPath))
                {
                    File.Copy(strOriginalPath, strCopyPath);

                    FileInfo fileExcel = new FileInfo(strCopyPath);
                    ExcelPackage excel = new ExcelPackage(fileExcel);



                    //int iRow = 7;
                    int iRow = 1;

                    DataTable dtApplication = new DataTable();
                    dtApplication.Columns.Add("NO", typeof(string));
                    dtApplication.Columns.Add("RFIDTAG", typeof(string));
                    dtApplication.Columns.Add("PARTCODE", typeof(string));
                    dtApplication.Columns.Add("REFNO", typeof(string));
                    dtApplication.Columns.Add("LOTNO", typeof(string));
                    dtApplication.Columns.Add("QTY", typeof(string));
                    dtApplication.Columns.Add("TOTALQTY", typeof(string));
                    dtApplication.Columns.Add("AREA", typeof(string));
                    dtApplication.Columns.Add("LINE", typeof(string));
                    dtApplication.Columns.Add("DATE", typeof(string));
                    dtApplication.Columns.Add("ITEMNAME", typeof(string));
                    //dtApplication.Columns.Add("Remarks", typeof(string));


                    string strArea;
                    if (ddlArea.SelectedItem.Text.ToString().Trim().ToUpper() == "ALL")
                    {
                        strArea = "0";
                    }
                    else
                    {
                        strArea = ddlArea.SelectedValue.ToString().Trim().ToUpper();
                    }

                    DataSet ds = new DataSet();
                    ds = InHouseReceivingDAL.GET_RFID_INQUIRY_INHOUSE_BY_RFID(txtRFIDTag.Text, Convert.ToDateTime(txtDateFrom.Text), Convert.ToDateTime(txtDateTo.Text), Convert.ToInt32(strArea));

                    if (ds.Tables.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables.Count; i++)
                        {
                            foreach (DataTable table in ds.Tables)
                            {

                                excel.Workbook.Worksheets["Sheet1"].Cells["A" + iRow.ToString()].Value = table.Rows[0]["PARTCODE"].ToString() + "-" + table.Rows[0]["ITEMNAME"].ToString();
                                iRow++;
                                excel.Workbook.Worksheets["Sheet1"].Cells["A" + iRow.ToString()].Value = table.Rows.Count;
                                //excel.Workbook.Worksheets["Sheet1"].Cells["F" + iRow.ToString()].Value = ds.Tables[i].Rows[0]["TOTALQTY"].ToString();
                                excel.Workbook.Worksheets["Sheet1"].Cells["F" + iRow.ToString()].Value = table.Rows[0]["TOTALQTY"].ToString();
                                iRow++;
                                excel.Workbook.Worksheets["Sheet1"].Cells["A" + iRow.ToString()].Value = "NO";
                                excel.Workbook.Worksheets["Sheet1"].Cells["B" + iRow.ToString()].Value = "RFIDTAG";
                                excel.Workbook.Worksheets["Sheet1"].Cells["C" + iRow.ToString()].Value = "PARTCODE";
                                excel.Workbook.Worksheets["Sheet1"].Cells["D" + iRow.ToString()].Value = "REFNO";
                                excel.Workbook.Worksheets["Sheet1"].Cells["E" + iRow.ToString()].Value = "LOTNO";
                                excel.Workbook.Worksheets["Sheet1"].Cells["F" + iRow.ToString()].Value = "QTY";
                                excel.Workbook.Worksheets["Sheet1"].Cells["G" + iRow.ToString()].Value = "AREA";
                                excel.Workbook.Worksheets["Sheet1"].Cells["H" + iRow.ToString()].Value = "LINE";
                                excel.Workbook.Worksheets["Sheet1"].Cells["I" + iRow.ToString()].Value = "DATE";

                                //ws.Row(1).Style.Fill.PatternType = ExcelFillStyle.Solid;
                                excel.Workbook.Worksheets["Sheet1"].Cells["A" + iRow.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                excel.Workbook.Worksheets["Sheet1"].Cells["A" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#538DD5"));
                                excel.Workbook.Worksheets["Sheet1"].Cells["A" + iRow.ToString()].Style.Font.Color.SetColor(Color.White);
                                excel.Workbook.Worksheets["Sheet1"].Cells["B" + iRow.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                excel.Workbook.Worksheets["Sheet1"].Cells["B" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#538DD5"));
                                excel.Workbook.Worksheets["Sheet1"].Cells["B" + iRow.ToString()].Style.Font.Color.SetColor(Color.White);
                                excel.Workbook.Worksheets["Sheet1"].Cells["C" + iRow.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                excel.Workbook.Worksheets["Sheet1"].Cells["C" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#538DD5"));
                                excel.Workbook.Worksheets["Sheet1"].Cells["C" + iRow.ToString()].Style.Font.Color.SetColor(Color.White);
                                excel.Workbook.Worksheets["Sheet1"].Cells["D" + iRow.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                excel.Workbook.Worksheets["Sheet1"].Cells["D" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#538DD5"));
                                excel.Workbook.Worksheets["Sheet1"].Cells["D" + iRow.ToString()].Style.Font.Color.SetColor(Color.White);
                                excel.Workbook.Worksheets["Sheet1"].Cells["E" + iRow.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                excel.Workbook.Worksheets["Sheet1"].Cells["E" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#538DD5"));
                                excel.Workbook.Worksheets["Sheet1"].Cells["E" + iRow.ToString()].Style.Font.Color.SetColor(Color.White);
                                excel.Workbook.Worksheets["Sheet1"].Cells["F" + iRow.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                excel.Workbook.Worksheets["Sheet1"].Cells["F" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#538DD5"));
                                excel.Workbook.Worksheets["Sheet1"].Cells["F" + iRow.ToString()].Style.Font.Color.SetColor(Color.White);
                                excel.Workbook.Worksheets["Sheet1"].Cells["G" + iRow.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                excel.Workbook.Worksheets["Sheet1"].Cells["G" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#538DD5"));
                                excel.Workbook.Worksheets["Sheet1"].Cells["G" + iRow.ToString()].Style.Font.Color.SetColor(Color.White);
                                excel.Workbook.Worksheets["Sheet1"].Cells["H" + iRow.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                excel.Workbook.Worksheets["Sheet1"].Cells["H" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#538DD5"));
                                excel.Workbook.Worksheets["Sheet1"].Cells["H" + iRow.ToString()].Style.Font.Color.SetColor(Color.White);
                                excel.Workbook.Worksheets["Sheet1"].Cells["I" + iRow.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                excel.Workbook.Worksheets["Sheet1"].Cells["I" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#538DD5"));
                                excel.Workbook.Worksheets["Sheet1"].Cells["I" + iRow.ToString()].Style.Font.Color.SetColor(Color.White);

                                iRow++;

                                foreach (DataRow dr in table.Rows)
                                {
                                    string NO = dr["NO"].ToString();
                                    string RFIDTAG = dr["RFIDTAG"].ToString();
                                    string PARTCODE = dr["PARTCODE"].ToString();
                                    string REFNO = dr["REFNO"].ToString();
                                    string LOTNO = dr["LOTNO"].ToString();
                                    string QTY = dr["QTY"].ToString();
                                    string TOTALQTY = dr["TOTALQTY"].ToString();
                                    string AREA = dr["AREA"].ToString();
                                    string LINE = dr["LINE"].ToString();
                                    string DATE = dr["DATE"].ToString();
                                    string ITEMNAME = dr["ITEMNAME"].ToString();

                                    dtApplication.Rows.Add(NO, RFIDTAG, PARTCODE, REFNO, LOTNO, QTY, TOTALQTY, AREA, LINE, DATE, ITEMNAME);
                                }

                                foreach (DataRow dr in table.Rows)
                                {

                                    excel.Workbook.Worksheets["Sheet1"].Cells["A" + iRow.ToString()].Value = dr["NO"].ToString().Trim();
                                    excel.Workbook.Worksheets["Sheet1"].Cells["B" + iRow.ToString()].Value = dr["RFIDTAG"].ToString().Trim();
                                    excel.Workbook.Worksheets["Sheet1"].Cells["C" + iRow.ToString()].Value = dr["PARTCODE"].ToString().Trim();
                                    excel.Workbook.Worksheets["Sheet1"].Cells["D" + iRow.ToString()].Value = dr["REFNO"].ToString().Trim();
                                    excel.Workbook.Worksheets["Sheet1"].Cells["E" + iRow.ToString()].Value = dr["LOTNO"].ToString().Trim();
                                    excel.Workbook.Worksheets["Sheet1"].Cells["F" + iRow.ToString()].Value = dr["QTY"].ToString().Trim();
                                    //excel.Workbook.Worksheets["Sheet1"].Cells["G" + iRow.ToString()].Value = dr["TOTALQTY"].ToString().Trim();
                                    excel.Workbook.Worksheets["Sheet1"].Cells["G" + iRow.ToString()].Value = dr["AREA"].ToString().Trim();
                                    excel.Workbook.Worksheets["Sheet1"].Cells["H" + iRow.ToString()].Value = dr["LINE"].ToString().Trim();
                                    excel.Workbook.Worksheets["Sheet1"].Cells["I" + iRow.ToString()].Value = dr["DATE"].ToString().Trim();
                                    //excel.Workbook.Worksheets["Sheet1"].Cells["K" + iRow.ToString()].Value = dr["ITEMNAME"].ToString().Trim();
                                    //excel.Workbook.Worksheets["Sheet1"].Cells["L" + iRow.ToString()].Value = dr["Remarks"].ToString().Trim();

                                    iRow++;


                                }
                                iRow++;
                            }

                            //excel.Workbook.Worksheets["LeaveSummary"].Protection.SetPassword("LEAVESUMMARY");
                            excel.Workbook.Worksheets["Sheet1"].Protection.AllowAutoFilter = true;
                            //excel.Workbook.Worksheets["Sheet1"].Protection.IsProtected = true;
                            excel.Save();

                            //Response.Redirect("./Templates/TEMP/" + strFileName);
                            Response.Redirect(@"~\Templates\TEMP\" + strFileName);


                        }

                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void ExportToExcel2()
        {
            if (txtPartCode.Text != "" && txtRFIDTag.Text == "" && txtRefNo.Text == "") //SEARCH BY PARTCODE
            {
                Fill_Per_PartCode();
            }
            else if (txtPartCode.Text == "" && txtRFIDTag.Text != "" && txtRefNo.Text == "") //SEARCH BY RFITAG
            {
                Fill_Per_RFIDTag();
            }

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=InHouseInquiry-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            divexport.RenderControl(htmlWrite);
            string style = @"<style> .textmode { } </style>";
            //4/29/2021  Response.Write(stringWrite.ToString());
            Response.Write(style);
            Response.Output.Write(stringWrite.ToString());
            Response.End();


        }

        private void ExportToExcel4()
        {
            if (txtPartCode.Text != "" && txtRFIDTag.Text == "" && txtRefNo.Text == "") //SEARCH BY PARTCODE
            {
                Fill_Per_PartCode();
            }
            else if (txtPartCode.Text == "" && txtRFIDTag.Text != "" && txtRefNo.Text == "") //SEARCH BY RFITAG
            {
                Fill_Per_RFIDTag();
            }

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=InHouseInquiry-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";

            using (StringWriter sw = new StringWriter())
            {

                HtmlTextWriter hw = new HtmlTextWriter(sw);

                objGV.HeaderRow.Cells[0].Visible = false;

                foreach (GridViewRow row in objGV.Rows)
                {
                    row.Cells[0].Visible = false;

                    foreach (TableCell cell in row.Cells)
                    {
                        cell.CssClass = "textmode";
                    }
                }


                divexport.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();

            }
        }

        //don’t forget to add this method , other wise we will get Control gv of type 
        //'GridView' must be placed inside a form tag with runat=server. 

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        private void ExportToExcel3()
        {
            string filename = "Asset Master " + DateTime.Now.ToString("(yyyyMMddHHmmss)");
            //Turn off the view stateV 225 55
            this.EnableViewState = false;
            //Remove the charset from the Content-Type header
            Response.Charset = String.Empty;

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
            Response.Charset = "";
            objGV2.AllowPaging = false;
            //this.Inquiry();
            // If you want the option to open the Excel file without saving then
            // comment out the line below
            // Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);


            using (StringWriter sw = new StringWriter())
            {

                HtmlTextWriter hw = new HtmlTextWriter(sw);

                objGV2.HeaderRow.Cells[0].Visible = false;

                foreach (GridViewRow row in objGV2.Rows)
                {
                    row.Cells[0].Visible = false;

                    foreach (TableCell cell in row.Cells)
                    {
                        cell.CssClass = "textmode";
                    }
                }


                divexport.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();

            }

        }

        private void ExportToExcel()
        {


            string strArea;
            if (ddlArea.SelectedItem.Text.ToString().Trim().ToUpper() == "ALL")
            {
                strArea = "0";
            }
            else
            {
                strArea = ddlArea.SelectedValue.ToString().Trim().ToUpper();
            }

            /////////////export to EXCEL
            if (txtPartCode.Text != "" && txtRFIDTag.Text == "" && txtRefNo.Text == "") //SEARCH BY PARTCODE
            {
                DataSet ds = new DataSet();
                ds = InHouseReceivingDAL.GET_RFID_INQUIRY_INHOUSE_BY_PARTCODE(txtPartCode.Text.Trim(), Convert.ToDateTime(txtDateFrom.Text), Convert.ToDateTime(txtDateTo.Text), Convert.ToInt32(strArea));
                for (int i = 0; i < ds.Tables.Count; i++)
                {

                    DataTable dt = ds.Tables[i];
                    DataView view = new DataView(dt);

                    if (view.Table.Rows.Count > 0)
                    {


                        DataTable resultTable = view.ToTable(false, "NO", "RFIDTAG", "REFNO", "LOTNO", "QTY", "AREA", "LINE", "DATE");

                        objGV2.DataSource = resultTable;
                        objGV2.DataBind();
                        Response.ClearContent();
                        Response.Buffer = true;
                        Response.AddHeader("content-disposition", "attachment; filename=ScannedDataInquiry-" + DateTime.Now.ToString("(yyyyMMddHHmmss)") + ".xls");
                        Response.ContentType = "application/ms-excel";
                        Response.Charset = "";
                        //StringWriter sw = new StringWriter();

                        Label lbl = new Label();
                        lbl.Text = "179110100";

                        using (StringWriter sw = new StringWriter())
                        {

                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            foreach (GridViewRow row in objGV2.Rows)
                            {
                                foreach (TableCell cell in row.Cells)
                                {
                                    cell.CssClass = "textmode";
                                }
                            }

                            objGV2.RenderControl(hw);
                            //divexport.RenderControl(hw);

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
                        //showAlert("error", "Error ", "NO DATA FOUND!");
                        //MsgBox1.alert("YOU CANNOT DOWNLOAD AN EMPTY RECORD!");
                    }
                }

            }
            else if (txtPartCode.Text == "" && txtRFIDTag.Text != "" && txtRefNo.Text == "") //SEARCH BY RFITAG
            {
                DataSet ds = new DataSet();
                ds = InHouseReceivingDAL.GET_RFID_INQUIRY_INHOUSE_BY_RFID(txtRFIDTag.Text.Trim(), Convert.ToDateTime(txtDateFrom.Text), Convert.ToDateTime(txtDateTo.Text), Convert.ToInt32(strArea));
                for (int i = 0; i < ds.Tables.Count; i++)
                {

                    DataTable dt = ds.Tables[i];
                    DataView view = new DataView(dt);

                    DataTable resultTable = view.ToTable(false, "NO", "RFIDTAG", "REFNO", "LOTNO", "QTY", "AREA", "LINE", "DATE");

                    objGV2.DataSource = resultTable;
                    objGV2.DataBind();
                    Response.ClearContent();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment; filename=ScannedDataInquiry-" + DateTime.Now.ToString("(yyyyMMddHHmmss)") + ".xls");
                    Response.ContentType = "application/ms-excel";
                    Response.Charset = "";
                }

                using (StringWriter sw = new StringWriter())
                {

                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    foreach (GridViewRow row in objGV2.Rows)
                    {
                        foreach (TableCell cell in row.Cells)
                        {
                            cell.CssClass = "textmode";
                        }
                    }


                    objGV2.RenderControl(hw);


                    //style to format numbers to string
                    string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();

                }

            }
        }

        private void FillArea()
        {
            DataTable dtFillStatus = new DataTable();
            dtFillStatus = InHouseReceivingDAL.INK_GET_AREA().Tables[0];

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

        private void Fill_Per_RFIDTag()
        {
            try
            {
                string strArea;
                if (ddlArea.SelectedItem.Text.ToString().Trim().ToUpper() == "ALL")
                {
                    strArea = "0";
                }
                else
                {
                    strArea = ddlArea.SelectedValue.ToString().Trim().ToUpper();
                }

                DataSet ds = new DataSet();
                ds = InHouseReceivingDAL.GET_RFID_INQUIRY_INHOUSE_BY_RFID(txtRFIDTag.Text, Convert.ToDateTime(txtDateFrom.Text), Convert.ToDateTime(txtDateTo.Text), Convert.ToInt32(strArea));

                if (ds.Tables.Count > 0)
                {

                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        Label lblHeader = new Label();
                        Label lblFooter = new Label();
                        string strPartCode = "";
                        string strPartName = "";
                        int iRcvCount = 0;
                        int iRcvCountTotal = 0;
                        int iRcvQty = 0;
                        int iRcvQtyTotal = 0;


                        strPartCode = ds.Tables[i].Rows[0][2].ToString();
                        strPartName = ds.Tables[i].Rows[0][10].ToString();
                        iQty = Convert.ToInt32(Convert.ToDecimal(ds.Tables[i].Rows[0][5].ToString()));
                        iRcvQtyTotal = Convert.ToInt32(Convert.ToDecimal(ds.Tables[i].Rows[0][6].ToString()));

                        int iRow = ds.Tables[i].Rows.Count;

                        lblHeader.Text = "<table style='background-color:#ebeded' width='1160px'><tr><td colspan=7 style='font-size:18px;font-weight:bolder;'>" + strPartCode + " - " + strPartName + "</td></tr>" +
                                         "<tr><td width='55px'style='font-weight:normal; font-size:16px;'  align='center'>" + iRow.ToString() + "</td>" +
                                         "<td width='300px'></td>" +
                                         "<td width='350px'></td>" +
                                         "<td width='500px'></td>" +
                                         "<td  width='80px' style='font-weight:normal; font-size:16px;''  align='center'>" + iRcvQtyTotal.ToString() + "</td>" +
                                         "<td width='250px'></td>" +
                                         "<td width='250px'></td>" +
                                         "</tr></table>";

                        lblFooter.Text = "</br>";


                        GridView objGV = new GridView();
                        objGV.ID = "GV" + i;
                        objGV.AutoGenerateColumns = false;
                        objGV.GridLines = GridLines.Both;
                        objGV.Width = 1160;
                        objGV.PreRender += objGV_PreRender;
                        //objGV.DataBound += objGV_DataBound;
                        //objGV.RowDataBound += objGV_RowDataBound;
                        //objGV.RowDeleting += objGV_RowDeleting;
                        //objGV.RowCommand += objGV_RowCommand;
                        objGV.EnableViewState = false;



                        BoundField no = new BoundField();
                        no.HeaderText = "No.";
                        no.DataField = "NO";
                        no.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        no.HeaderStyle.Font.Size = 14;
                        no.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        no.HeaderStyle.ForeColor = Color.White;
                        no.ItemStyle.Font.Size = 11;
                        no.ItemStyle.ForeColor = Color.Black;
                        no.ItemStyle.Width = 50;
                        no.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(no);


                        BoundField rfidtag = new BoundField();
                        rfidtag.HeaderText = "RFID Tag";
                        rfidtag.DataField = "RFIDTAG";
                        rfidtag.DataField.ToString();
                        rfidtag.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        rfidtag.HeaderStyle.Font.Size = 14;
                        rfidtag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        rfidtag.HeaderStyle.ForeColor = Color.White;
                        rfidtag.ItemStyle.Font.Size = 11;
                        rfidtag.ItemStyle.ForeColor = Color.Black;
                        rfidtag.ItemStyle.Width = 250;
                        rfidtag.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        rfidtag.ItemStyle.ToString();
                        objGV.Columns.Add(rfidtag);

                        BoundField refno = new BoundField();
                        refno.HeaderText = "Ref No.";
                        refno.DataField = "REFNO";
                        refno.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        refno.HeaderStyle.Font.Size = 14;
                        refno.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        refno.HeaderStyle.ForeColor = Color.White;
                        refno.ItemStyle.Font.Size = 11;
                        refno.ItemStyle.ForeColor = Color.Black;
                        refno.ItemStyle.Width = 250;
                        refno.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(refno);

                        BoundField lotno = new BoundField();
                        lotno.HeaderText = "Lot No.";
                        lotno.DataField = "LOTNO";
                        lotno.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        lotno.HeaderStyle.Font.Size = 14;
                        lotno.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        lotno.HeaderStyle.ForeColor = Color.White;
                        lotno.ItemStyle.Font.Size = 11;
                        lotno.ItemStyle.ForeColor = Color.Black;
                        lotno.ItemStyle.Width = 200;
                        lotno.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(lotno);


                        BoundField rcvqty = new BoundField();
                        rcvqty.HeaderText = "Qty";
                        rcvqty.DataField = "QTY";
                        rcvqty.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        rcvqty.HeaderStyle.Font.Size = 14;
                        rcvqty.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        rcvqty.HeaderStyle.ForeColor = Color.White;
                        rcvqty.ItemStyle.Font.Size = 11;
                        rcvqty.ItemStyle.ForeColor = Color.Black;
                        rcvqty.ItemStyle.Width = 100;
                        rcvqty.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(rcvqty);

                        BoundField area = new BoundField();
                        area.HeaderText = "Area";
                        area.DataField = "Area";
                        area.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        area.HeaderStyle.Font.Size = 14;
                        area.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        area.HeaderStyle.ForeColor = Color.White;
                        area.ItemStyle.Font.Size = 11;
                        area.ItemStyle.ForeColor = Color.Black;
                        area.ItemStyle.Width = 100;
                        area.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(area);

                        BoundField line = new BoundField();
                        line.HeaderText = "Line";
                        line.DataField = "Line";
                        line.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        line.HeaderStyle.Font.Size = 14;
                        line.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        line.HeaderStyle.ForeColor = Color.White;
                        line.ItemStyle.Font.Size = 11;
                        line.ItemStyle.ForeColor = Color.Black;
                        line.ItemStyle.Width = 100;
                        line.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(line);


                        BoundField date = new BoundField();
                        date.HeaderText = "Date";
                        date.DataField = "Date";
                        date.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        date.HeaderStyle.Font.Size = 14;
                        date.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        date.HeaderStyle.ForeColor = Color.White;
                        date.ItemStyle.Font.Size = 11;
                        date.ItemStyle.ForeColor = Color.Black;
                        date.ItemStyle.Width = 250;
                        //remarks.ItemStyle.Wrap = true;
                        date.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(date);

                        objGV.DataSource = ds.Tables[i];
                        objGV.DataBind();


                        pnl.Controls.Add(lblHeader);
                        pnl.Controls.Add(objGV);
                        pnl.Controls.Add(lblFooter);
                        divexport.Controls.Add(pnl);
                        divexport.Controls.Add(lblHeader);
                        divexport.Controls.Add(objGV);
                        divexport.Controls.Add(lblFooter);
                    }


                }


            }
            catch (Exception ex)
            {
                //Logger.GetInstance().Fatal(ex.StackTrace, ex);
                //MsgBox1.alert(ex.Message);
            }
        }

        private void Fill()
        {
            try
            {
                string strArea;
                if (ddlArea.SelectedItem.Text.ToString().Trim().ToUpper() == "ALL")
                {
                    strArea = "0";
                }
                else
                {
                    strArea = ddlArea.SelectedValue.ToString().Trim().ToUpper();
                }

                DataSet ds = new DataSet();
                ds = InHouseReceivingDAL.GET_RFID_INQUIRY_INHOUSE(txtRFIDTag.Text.Trim(), Convert.ToDateTime(txtDateFrom.Text), Convert.ToDateTime(txtDateTo.Text), Convert.ToInt32(strArea), txtPartCode.Text.Trim(), txtRefNo.Text.Trim());

                if (ds.Tables.Count > 0)
                {

                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        Label lblHeader = new Label();
                        Label lblFooter = new Label();
                        string strPartCode = "";
                        string strPartName = "";
                        int iRcvCount = 0;
                        int iRcvCountTotal = 0;
                        int iRcvQty = 0;
                        int iRcvQtyTotal = 0;


                        strPartCode = ds.Tables[i].Rows[0][2].ToString();
                        strPartName = ds.Tables[i].Rows[0][10].ToString();
                        iQty = Convert.ToInt32(Convert.ToDecimal(ds.Tables[i].Rows[0][5].ToString()));
                        iRcvQtyTotal = Convert.ToInt32(Convert.ToDecimal(ds.Tables[i].Rows[0][6].ToString()));

                        int iRow = ds.Tables[i].Rows.Count;

                        lblHeader.Text = "<table style='background-color:#ebeded' width='1160px'><tr><td colspan=7 style='font-size:18px;font-weight:bolder;'>" + strPartCode + " - " + strPartName + "</td></tr>" +
                                         "<tr><td width='55px'style='font-weight:normal; font-size:16px;'  align='center'>" + iRow.ToString() + "</td>" +
                                         "<td width='300px'></td>" +
                                         "<td width='350px'></td>" +
                                         "<td width='500px'></td>" +
                                         "<td  width='80px' style='font-weight:normal; font-size:16px;''  align='center'>" + iRcvQtyTotal.ToString() + "</td>" +
                                         "<td width='250px'></td>" +
                                         "<td width='250px'></td>" +
                                         "</tr></table>";

                        lblFooter.Text = "</br>";


                        GridView objGV = new GridView();
                        objGV.ID = "GV" + i;
                        objGV.AutoGenerateColumns = false;
                        objGV.GridLines = GridLines.Both;
                        objGV.Width = 1160;
                        objGV.PreRender += objGV_PreRender;
                        objGV.EnableViewState = false;



                        BoundField no = new BoundField();
                        no.HeaderText = "No.";
                        no.DataField = "NO";
                        no.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        no.HeaderStyle.Font.Size = 14;
                        no.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        no.HeaderStyle.ForeColor = Color.White;
                        no.ItemStyle.Font.Size = 11;
                        no.ItemStyle.ForeColor = Color.Black;
                        no.ItemStyle.Width = 50;
                        no.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(no);


                        BoundField rfidtag = new BoundField();
                        rfidtag.HeaderText = "RFID Tag";
                        rfidtag.DataField = "RFIDTAG";
                        rfidtag.DataField.ToString();
                        rfidtag.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        rfidtag.HeaderStyle.Font.Size = 14;
                        rfidtag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        rfidtag.HeaderStyle.ForeColor = Color.White;
                        rfidtag.ItemStyle.Font.Size = 11;
                        rfidtag.ItemStyle.ForeColor = Color.Black;
                        rfidtag.ItemStyle.Width = 250;
                        rfidtag.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        rfidtag.ItemStyle.ToString();
                        objGV.Columns.Add(rfidtag);

                        BoundField refno = new BoundField();
                        refno.HeaderText = "Ref No.";
                        refno.DataField = "REFNO";
                        refno.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        refno.HeaderStyle.Font.Size = 14;
                        refno.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        refno.HeaderStyle.ForeColor = Color.White;
                        refno.ItemStyle.Font.Size = 11;
                        refno.ItemStyle.ForeColor = Color.Black;
                        refno.ItemStyle.Width = 250;
                        refno.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(refno);

                        BoundField lotno = new BoundField();
                        lotno.HeaderText = "Lot No.";
                        lotno.DataField = "LOTNO";
                        lotno.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        lotno.HeaderStyle.Font.Size = 14;
                        lotno.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        lotno.HeaderStyle.ForeColor = Color.White;
                        lotno.ItemStyle.Font.Size = 11;
                        lotno.ItemStyle.ForeColor = Color.Black;
                        lotno.ItemStyle.Width = 200;
                        lotno.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(lotno);


                        BoundField rcvqty = new BoundField();
                        rcvqty.HeaderText = "Qty";
                        rcvqty.DataField = "QTY";
                        rcvqty.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        rcvqty.HeaderStyle.Font.Size = 14;
                        rcvqty.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        rcvqty.HeaderStyle.ForeColor = Color.White;
                        rcvqty.ItemStyle.Font.Size = 11;
                        rcvqty.ItemStyle.ForeColor = Color.Black;
                        rcvqty.ItemStyle.Width = 100;
                        rcvqty.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(rcvqty);

                        BoundField area = new BoundField();
                        area.HeaderText = "Area";
                        area.DataField = "Area";
                        area.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        area.HeaderStyle.Font.Size = 14;
                        area.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        area.HeaderStyle.ForeColor = Color.White;
                        area.ItemStyle.Font.Size = 11;
                        area.ItemStyle.ForeColor = Color.Black;
                        area.ItemStyle.Width = 100;
                        area.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(area);

                        BoundField line = new BoundField();
                        line.HeaderText = "Line";
                        line.DataField = "Line";
                        line.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        line.HeaderStyle.Font.Size = 14;
                        line.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        line.HeaderStyle.ForeColor = Color.White;
                        line.ItemStyle.Font.Size = 11;
                        line.ItemStyle.ForeColor = Color.Black;
                        line.ItemStyle.Width = 100;
                        line.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(line);


                        BoundField date = new BoundField();
                        date.HeaderText = "Date";
                        date.DataField = "Date";
                        date.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        date.HeaderStyle.Font.Size = 14;
                        date.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        date.HeaderStyle.ForeColor = Color.White;
                        date.ItemStyle.Font.Size = 11;
                        date.ItemStyle.ForeColor = Color.Black;
                        date.ItemStyle.Width = 270;
                        //remarks.ItemStyle.Wrap = true;
                        date.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(date);

                        objGV.DataSource = ds.Tables[i];
                        objGV.DataBind();


                        pnl.Controls.Add(lblHeader);
                        pnl.Controls.Add(objGV);
                        pnl.Controls.Add(lblFooter);
                        divexport.Controls.Add(pnl);
                        divexport.Controls.Add(lblHeader);
                        divexport.Controls.Add(objGV);
                        divexport.Controls.Add(lblFooter);

                    }


                }


            }
            catch (Exception ex)
            {
                //Logger.GetInstance().Fatal(ex.StackTrace, ex);
                //MsgBox1.alert(ex.Message);
            }
        }

        private void Fill_Per_PartCode()
        {
            try
            {
                string strArea;
                if (ddlArea.SelectedItem.Text.ToString().Trim().ToUpper() == "ALL")
                {
                    strArea = "0";
                }
                else
                {
                    strArea = ddlArea.SelectedValue.ToString().Trim().ToUpper();
                }

                DataSet ds = new DataSet();
                ds = InHouseReceivingDAL.GET_RFID_INQUIRY_INHOUSE_BY_PARTCODE(txtPartCode.Text, Convert.ToDateTime(txtDateFrom.Text), Convert.ToDateTime(txtDateTo.Text), Convert.ToInt32(strArea));

                if (ds.Tables.Count > 0)
                {

                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        Label lblHeader = new Label();
                        Label lblFooter = new Label();
                        string strPartCode = "";
                        string strPartName = "";
                        int iRcvCount = 0;
                        int iRcvCountTotal = 0;
                        int iRcvQty = 0;
                        int iRcvQtyTotal = 0;


                        strPartCode = ds.Tables[i].Rows[0][2].ToString();
                        strPartName = ds.Tables[i].Rows[0][10].ToString();
                        iQty = Convert.ToInt32(Convert.ToDecimal(ds.Tables[i].Rows[0][5].ToString()));
                        iRcvQtyTotal = Convert.ToInt32(Convert.ToDecimal(ds.Tables[i].Rows[0][6].ToString()));

                        int iRow = ds.Tables[i].Rows.Count;

                        lblHeader.Text = "<table style='background-color:#ebeded' width='1160px'><tr><td colspan=7 style='font-size:18px;font-weight:bolder;'>" + strPartCode + " - " + strPartName + "</td></tr>" +
                                         "<tr><td width='55px'style='font-weight:normal; font-size:16px;'  align='center'>" + iRow.ToString() + "</td>" +
                                         "<td width='300px'></td>" +
                                         "<td width='350px'></td>" +
                                         "<td width='500px'></td>" +
                                         "<td  width='80px' style='font-weight:normal; font-size:16px;''  align='center'>" + iRcvQtyTotal.ToString() + "</td>" +
                                         "<td width='250px'></td>" +
                                         "<td width='250px'></td>" +
                                         "</tr></table>";

                        lblFooter.Text = "</br>";


                        GridView objGV = new GridView();
                        objGV.ID = "GV" + i;
                        objGV.AutoGenerateColumns = false;
                        objGV.GridLines = GridLines.Both;
                        objGV.Width = 1160;
                        objGV.PreRender += objGV_PreRender;
                        objGV.EnableViewState = false;



                        BoundField no = new BoundField();
                        no.HeaderText = "No.";
                        no.DataField = "NO";
                        no.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        no.HeaderStyle.Font.Size = 14;
                        no.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        no.HeaderStyle.ForeColor = Color.White;
                        no.ItemStyle.Font.Size = 11;
                        no.ItemStyle.ForeColor = Color.Black;
                        no.ItemStyle.Width = 50;
                        no.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(no);


                        BoundField rfidtag = new BoundField();
                        rfidtag.HeaderText = "RFID Tag";
                        rfidtag.DataField = "RFIDTAG";
                        rfidtag.DataField.ToString();
                        rfidtag.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        rfidtag.HeaderStyle.Font.Size = 14;
                        rfidtag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        rfidtag.HeaderStyle.ForeColor = Color.White;
                        rfidtag.ItemStyle.Font.Size = 11;
                        rfidtag.ItemStyle.ForeColor = Color.Black;
                        rfidtag.ItemStyle.Width = 250;
                        rfidtag.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        rfidtag.ItemStyle.ToString();
                        objGV.Columns.Add(rfidtag);

                        BoundField refno = new BoundField();
                        refno.HeaderText = "Ref No.";
                        refno.DataField = "REFNO";
                        refno.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        refno.HeaderStyle.Font.Size = 14;
                        refno.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        refno.HeaderStyle.ForeColor = Color.White;
                        refno.ItemStyle.Font.Size = 11;
                        refno.ItemStyle.ForeColor = Color.Black;
                        refno.ItemStyle.Width = 250;
                        refno.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(refno);

                        BoundField lotno = new BoundField();
                        lotno.HeaderText = "Lot No.";
                        lotno.DataField = "LOTNO";
                        lotno.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        lotno.HeaderStyle.Font.Size = 14;
                        lotno.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        lotno.HeaderStyle.ForeColor = Color.White;
                        lotno.ItemStyle.Font.Size = 11;
                        lotno.ItemStyle.ForeColor = Color.Black;
                        lotno.ItemStyle.Width = 200;
                        lotno.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(lotno);


                        BoundField rcvqty = new BoundField();
                        rcvqty.HeaderText = "Qty";
                        rcvqty.DataField = "QTY";
                        rcvqty.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        rcvqty.HeaderStyle.Font.Size = 14;
                        rcvqty.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        rcvqty.HeaderStyle.ForeColor = Color.White;
                        rcvqty.ItemStyle.Font.Size = 11;
                        rcvqty.ItemStyle.ForeColor = Color.Black;
                        rcvqty.ItemStyle.Width = 100;
                        rcvqty.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(rcvqty);

                        BoundField area = new BoundField();
                        area.HeaderText = "Area";
                        area.DataField = "Area";
                        area.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        area.HeaderStyle.Font.Size = 14;
                        area.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        area.HeaderStyle.ForeColor = Color.White;
                        area.ItemStyle.Font.Size = 11;
                        area.ItemStyle.ForeColor = Color.Black;
                        area.ItemStyle.Width = 100;
                        area.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(area);

                        BoundField line = new BoundField();
                        line.HeaderText = "Line";
                        line.DataField = "Line";
                        line.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        line.HeaderStyle.Font.Size = 14;
                        line.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        line.HeaderStyle.ForeColor = Color.White;
                        line.ItemStyle.Font.Size = 11;
                        line.ItemStyle.ForeColor = Color.Black;
                        line.ItemStyle.Width = 100;
                        line.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(line);


                        BoundField date = new BoundField();
                        date.HeaderText = "Date";
                        date.DataField = "Date";
                        date.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        date.HeaderStyle.Font.Size = 14;
                        date.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        date.HeaderStyle.ForeColor = Color.White;
                        date.ItemStyle.Font.Size = 11;
                        date.ItemStyle.ForeColor = Color.Black;
                        date.ItemStyle.Width = 270;
                        //remarks.ItemStyle.Wrap = true;
                        date.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(date);

                        objGV.DataSource = ds.Tables[i];
                        objGV.DataBind();


                        pnl.Controls.Add(lblHeader);
                        pnl.Controls.Add(objGV);
                        pnl.Controls.Add(lblFooter);
                        divexport.Controls.Add(pnl);
                        divexport.Controls.Add(lblHeader);
                        divexport.Controls.Add(objGV);
                        divexport.Controls.Add(lblFooter);

                    }


                }


            }
            catch (Exception ex)
            {
                //Logger.GetInstance().Fatal(ex.StackTrace, ex);
                //MsgBox1.alert(ex.Message);
            }
        }

        public class GridDecorator
        {
            public static void MergeRows(GridView objGV)
            {
                for (int rowIndex = objGV.Rows.Count - 2; rowIndex >= 0; rowIndex--)
                {
                    GridViewRow row = objGV.Rows[rowIndex];
                    GridViewRow previousRow = objGV.Rows[rowIndex + 1];

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

        protected void objGV_PreRender(object sender, EventArgs e)
        {
            GridDecorator.MergeRows(objGV);
        }
    }
}