using FGWHSEClient.DAL;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Data;
using System.Drawing;
using System.Web.Services;

namespace FGWHSEClient.Form
{
    public partial class PartsClone : System.Web.UI.Page
    {
        static DALPSI_PLANT DPSI = new DALPSI_PLANT();

        static string UserID;
        static DataTable dtSupplierCode;
        static DataTable dtPlantCode;
        public static DataTable dtPartsSimulation;
        public static string VendorValue = "";
        public string strPageSubsystem = "";
        public string strAccessLevel = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                    strPageSubsystem = "FGWHSE_040";
                    if (!checkAuthority(strPageSubsystem) && Session["UserName"].ToString() != "GUEST")
                    {
                        Response.Write("<script>");
                        Response.Write("alert('You are not authorized to access the page.');");
                        Response.Write("window.location = 'Default.aspx';");
                        Response.Write("</script>");
                    }
                }
                UserID = Session["UserID"].ToString();
                dtPlantCode = new DALPSI_PLANT().PSI_GET_PLANTS_BY_USERID(UserID);
                dtSupplierCode = new DALPSI_PLANT().PSI_GET_VENDORS_BY_USERID(UserID);

                //GetVendorValue();
                //if (!this.IsPostBack)
                //{
                //    GETSUPPLIER(txtVendorCode.Text.Trim());
                //}
            }
            catch (Exception ex)
            {
                Response.Write("<script>");
                Response.Write("alert('" + ex.Message.ToString() + "');");
                Response.Write("</script>");
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
                //Response.Write("<script>");
                //Response.Write("alert('" + ex.Message.ToString() + "');");
                //Response.Write("</script>");

                isValid = false;
                return isValid;
            }
        }

        [WebMethod]
        public static string GetPlantsByUserId()
        {
            return JsonConvert.SerializeObject(dtPlantCode);
        }

        [WebMethod]
        public static string GetVendorsByUserId()
        {
            return JsonConvert.SerializeObject(dtSupplierCode);
        }

        [WebMethod]
        public static string GetPartsCodeByPlantandVendors(string strPLANT, string strVENDORS)
        {
            return JsonConvert.SerializeObject(DPSI.PSI_GET_PARTSCODE_BY_PLANT_AND_VENDOR(strPLANT, strVENDORS));
        }

        [WebMethod]
        public static string GetPartsSimulationByPlant(string strPLANT, string strPARTS, string strVENDORS)
        {
            DataTable dt = DPSI.PSI_GET_PARTSSIMULATION_BY_PLANT(strPLANT, strPARTS, strVENDORS);
            if (dt.Rows.Count > 0)
            {
                var StartDate = dt.Rows[0][209].ToString();
                for (var i = 11; i <= 208;)
                {
                    for (int j = 0; j <= 197; j++)
                    {
                        if (i == 11)
                        {
                            dt.Columns[i].ColumnName = StartDate;
                        }
                        else
                        {
                            DateTime Date = Convert.ToDateTime(StartDate);
                            dt.Columns[i].ColumnName = Date.AddDays(j).ToString("MM/dd/yyyy");
                        }
                        i++;
                    }
                }
                dt.Columns.Remove("MODEL");
                dt.Columns.Remove("FIRSTCOLUMNNAME");
            }

            if (dtPartsSimulation != null)
            {
                dtPartsSimulation.Clear();
            }

            dtPartsSimulation = dt;

            return JsonConvert.SerializeObject(dtPartsSimulation);
        }

        static string ColumnIndexToColumnLetter(int colIndex)
        {
            int div = colIndex;
            string colLetter = String.Empty;
            int mod = 0;

            while (div > 0)
            {
                mod = (div - 1) % 26;
                colLetter = (char)(65 + mod) + colLetter;
                div = (int)((div - mod) / 26);
            }

            return colLetter;
        }

        public void CreateExcelFirstTemplate()
        {
            var fileName = "PartsSimulation" + DateTime.Now.ToString("(yyyyMMddHHmmss)") + ".xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new OfficeOpenXml.ExcelPackage(fileName))
            {
                //Get a DataTable with data that we will load into the worksheet...
                DataTable dt = dtPartsSimulation;

                //Add a worksheet
                var sheet = package.Workbook.Worksheets.Add("Sheet1");

                //Load the datatable into the worksheet...
                sheet.Cells["A1"].LoadFromDataTable(dt, PrintHeaders: true/*, TableStyles.Light1*/);

                sheet.Cells[1, 1, 1, sheet.Dimension.End.Column].Style.Font.Bold = true;

                sheet.View.FreezePanes(1, 10);

                for (int i = 1; i <= dt.Rows.Count + 1; i++)
                {
                    string strRowNumber = i.ToString();

                    if (strRowNumber != "1")
                    {
                        for (int j = 10; j <= dt.Columns.Count; j++)
                        {
                            string strColumnLetter = ColumnIndexToColumnLetter(j);
                            var cell = sheet.Cells[strColumnLetter + strRowNumber];
                            long cellValue = Convert.ToInt64(cell.Value);

                            if (cellValue < 0)
                            {
                                cell.Style.Font.Color.SetColor(Color.Red);
                                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                cell.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffc7ce"));
                            }
                        }
                    }

                    if (sheet.Cells["I" + strRowNumber].Value.ToString() == "END STOCKS 1")
                    {
                        for (int j = 10; j <= 208; j++)
                        {

                            string ColumnLetter = ColumnIndexToColumnLetter(j);
                            var cell = sheet.Cells[ColumnLetter + strRowNumber];

                            string FormulaColumnLetter = "";
                            int RowNumber = i;

                            if (ColumnLetter == "J")
                            {
                                FormulaColumnLetter = ColumnIndexToColumnLetter(j - 2);

                                cell.Formula = FormulaColumnLetter + strRowNumber + "-" + ColumnLetter + (RowNumber - 2);
                            }
                            else
                            {
                                FormulaColumnLetter = ColumnIndexToColumnLetter(j - 1);

                                cell.Formula = FormulaColumnLetter + strRowNumber + "-" + ColumnLetter + (RowNumber - 2);
                            }
                        }
                    }

                    if (sheet.Cells["I" + strRowNumber].Value.ToString() == "END STOCKS 2")
                    {
                        for (int j = 10; j <= 208; j++)
                        {

                            string ColumnLetter = ColumnIndexToColumnLetter(j);
                            var cell = sheet.Cells[ColumnLetter + strRowNumber];

                            string FormulaColumnLetter = "";
                            int RowNumber = i;

                            if (ColumnLetter == "J")
                            {
                                FormulaColumnLetter = ColumnIndexToColumnLetter(j - 2);

                                cell.Formula = FormulaColumnLetter + strRowNumber + "+" + ColumnLetter + (RowNumber - 2) + "-" + ColumnLetter + (RowNumber - 3);
                            }
                            else
                            {
                                FormulaColumnLetter = ColumnIndexToColumnLetter(j - 1);

                                cell.Formula = FormulaColumnLetter + strRowNumber + "+" + ColumnLetter + (RowNumber - 2) + "-" + ColumnLetter + (RowNumber - 3);
                            }
                        }
                    }
                }

                for (int j = 1; j <= dt.Columns.Count; j++)
                {
                    //string ColumnLetter = ColumnIndexToColumnLetter(j);
                    //var cell = sheet.Cells[ColumnLetter + strRowNumber];
                    sheet.Column(j).AutoFit();
                    //cell.AutoFitColumns();
                }

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=" + fileName);
                Response.BinaryWrite(package.GetAsByteArray());
                Response.End();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            //if (hasData.Value == "true")
            {
                CreateExcelFirstTemplate();
            }
        }

    }
}