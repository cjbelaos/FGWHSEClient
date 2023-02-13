using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.SessionState;

namespace FGWHSEClient.Form
{
    /// <summary>
    /// Summary description for PSI_PARTS_SIMULATION_BY_PLANT_EXPORT
    /// </summary>
    public class PSI_PARTS_SIMULATION_BY_PLANT_EXPORT : IHttpHandler, IRequiresSessionState
    {
        PSI_PARTS_SIMULATION_BY_PLANT psi = new PSI_PARTS_SIMULATION_BY_PLANT();

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                //string folderName = "Part Simulation";
                //string strFileName = folderName + DateTime.Now.ToString("(yyyyMMddHHmmss)");
                //createTempFolder(folderName);
                //string strDir = "~/TEMP/" + folderName;
                //string strPath = strDir + "/" + strFileName;
                //string FilePath = HttpContext.Current.Server.MapPath(strPath + ".xlsx");

                //var fileName = "ExcellData.xlsx";
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                //using (var package = new ExcelPackage(fileName))
                //{
                //    //Get a DataTable with data that we will load into the worksheet...
                //    DataTable dt = psi.GetPartsSimulation();

                //    //Add a worksheet
                //    var sheet = package.Workbook.Worksheets.Add("Sheet1");

                //    //Load the datatable into the worksheet...
                //    sheet.Cells["A1"].LoadFromDataTable(dt, PrintHeaders: true/*, TableStyles.Medium9*/);


                //    for (int i = 1; i <= dt.Rows.Count + 1; i++)
                //    {
                //        string strRowNumber = i.ToString();

                //        if (strRowNumber != "1")
                //        {
                //            for (int j = 10; j <= dt.Columns.Count; j++)
                //            {
                //                string strColumnLetter = ColumnIndexToColumnLetter(j);
                //                var cell = sheet.Cells[strColumnLetter + strRowNumber];
                //                long cellValue = Convert.ToInt64(cell.Value);

                //                if (cellValue < 0)
                //                {
                //                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                //                    cell.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFF000"));
                //                }
                //            }
                //        }

                //        if (sheet.Cells["I" + strRowNumber].Value.ToString() == "END STOCKS 2")
                //        {
                //            for (int j = 10; j <= 208; j++)
                //            {

                //                string ColumnLetter = ColumnIndexToColumnLetter(j);
                //                var cell = sheet.Cells[ColumnLetter + strRowNumber];

                //                string FormulaColumnLetter = "";
                //                int RowNumber = i;

                //                if (ColumnLetter == "J")
                //                {
                //                    FormulaColumnLetter = ColumnIndexToColumnLetter(j - 2);

                //                    cell.Formula = FormulaColumnLetter + strRowNumber + "+" + ColumnLetter + (RowNumber - 2) + "-" + ColumnLetter + (RowNumber - 3);
                //                }
                //                else
                //                {
                //                    FormulaColumnLetter = ColumnIndexToColumnLetter(j - 1);

                //                    cell.Formula = FormulaColumnLetter + strRowNumber + "+" + ColumnLetter + (RowNumber - 2) + "-" + ColumnLetter + (RowNumber - 3);
                //                }
                //            }
                //        }
                //    }

                //    package.Workbook.Properties.Title = "Attempts";
                //    context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //    context.Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", "ExcellData.xlsx"));
                //    context.Response.BinaryWrite(package.GetAsByteArray());

                //    //if (File.Exists(FilePath))
                //    //    File.Delete(FilePath);

                //    //// Create excel file on physical disk 
                //    //FileStream objFileStrm = File.Create(FilePath);
                //    //objFileStrm.Close();

                //    //// Write content to excel file 
                //    //File.WriteAllBytes(FilePath, package.GetAsByteArray());

                //    ////Close Excel package
                //    //package.Dispose();

                //}

                //context.Response.Redirect(FilePath);
                //context.Session["ExcelFile"] = FilePath;
            }

            catch (Exception ex)

            {
                throw ex;
            }
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



        protected string createTempFolder(String FolderTemp)
        {

            string des = "~/TEMP/";
            string pth = HttpContext.Current.Server.MapPath(Path.Combine(des, FolderTemp));
            Directory.CreateDirectory(pth);

            return pth;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}