using System;
using System.Data;
using System.Data.OleDb;

namespace FGWHSEClient.Classes
{
    public class ExcelReader
    {

        //public static DataTable GetData1(string FileLocation)
        //{
        //    DataTable dt = new DataTable();
        //    //Create COM Objects. Create a COM object for everything that is referenced
        //    Excel.Application xlApp = new Excel.Application();
        //    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(FileLocation);
        //    Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
        //    Excel.Range xlRange = xlWorksheet.UsedRange;

        //    int rowCount = xlRange.Rows.Count;
        //    int colCount = xlRange.Columns.Count;

        //    //iterate over the rows and columns and print to the console as it appears in the file
        //    //excel is not zero based!!
        //    for (int i = 1; i <= rowCount; i++)
        //    {
        //        DataRow dr = dt.NewRow();
        //        for (int j = 1; j <= colCount; j++)
        //        {
        //            string cellValue = xlRange.Cells[i, j].Value2.ToString();
        //            if (i == 1)//first row must be the columns for datatable
        //            {
        //                DataColumn col = new DataColumn(cellValue, Type.GetType("System.String"));
        //                dt.Columns.Add(col);
        //            }
        //            else
        //            {
        //                dr[j] = cellValue;
        //            }
        //        }
        //        dt.Rows.Add(dr);
        //    }

        //    //cleanup
        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();

        //    //rule of thumb for releasing com objects:
        //    //  never use two dots, all COM objects must be referenced and released individually
        //    //  ex: [somthing].[something].[something] is bad

        //    //release com objects to fully kill excel process from running in the background
        //    Marshal.ReleaseComObject(xlRange);
        //    Marshal.ReleaseComObject(xlWorksheet);

        //    //close and release
        //    xlWorkbook.Close();
        //    Marshal.ReleaseComObject(xlWorkbook);

        //    //quit and release
        //    xlApp.Quit();
        //    Marshal.ReleaseComObject(xlApp);
        //    return dt;
        //}

        /// <summary>
        /// Getting the data from the excel file
        /// </summary>
        /// <param name="FileLocation">Location where the file is located</param>
        /// <returns></returns>
        public static DataTable GetData(string FileLocation) {
            DataTable dt = new DataTable();
            string ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileLocation + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
            string extension = FileLocation.Split('.')[FileLocation.Split('.').Length-1];
            if (extension == "xls")
            {
                //ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
            }
            OleDbConnection conn = new OleDbConnection(ConStr);
            //checking that connection state is closed or not if closed the 
            //open the connection
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            DataTable dbSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (dbSchema == null || dbSchema.Rows.Count < 1)
            {
                throw new Exception("Error: Could not determine the name of the first worksheet.");
            }
            string firstSheetName = dbSchema.Rows[0]["TABLE_NAME"].ToString();
            string query = string.Format("SELECT * FROM [{0}]",firstSheetName);
            //create command object
            OleDbCommand cmd = new OleDbCommand(query, conn);
            // create a data adapter and get the data into dataadapter
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            //fill the excel data to data set
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            return ds.Tables[0];
        }
        /// <summary>
        /// Validate if the object is a valid date
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>True if it is a date, otherwise false</returns>
        /// https://forums.asp.net/post/2309011.aspx
        public static bool IsDate(Object obj)
        {
            string strDate = obj.ToString();
            try
            {
                DateTime dt = DateTime.Parse(strDate);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}