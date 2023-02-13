using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.IO;
using FGWHSEClient.DAL;
using System.Data.OleDb;

namespace FGWHSEClient
{
    /// <summary>
    /// Summary description for PSI_PARTS_SIMULATION_BY_PLANT_UPLOAD
    /// </summary>
    public class PSI_PARTS_SIMULATION_BY_PLANT_UPLOAD : IHttpHandler, IRequiresSessionState
    {
        DALPSI_PLANT DPSI = new DALPSI_PLANT();
        static string errorMessage = "";

        public void ProcessRequest(HttpContext context)
        {
            string plantcode = System.Convert.ToString(context.Request.QueryString["plantcode"]);
            string userid = System.Convert.ToString(context.Request.QueryString["userid"]);
            context.Response.ContentType = "text/plain";

            try
            {
                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = context.Request.Files[s];
                    DataTable dt = getDataFilteredColumn(file, plantcode);

                    if (dt.Rows.Count > 0)
                    {
                        DPSI.UploadTable(dt, dt.Columns[4].ToString(), userid);
                        context.Session["ErrorMessage"] = "";
                    }
                    else
                    {
                        context.Session["ErrorMessage"] = "Different plantcode detected, please recheck the file!";
                    }
                }
            }
            catch (Exception ac)
            {
                errorMessage = ac.Message.ToString();
                context.Session["ErrorMessage"] = errorMessage;
            }
        }

        public string getColumns(DataTable dt)
        {
            string strColumnNames = "";

            string strComma = ",";

            for (int x = 0; x < dt.Columns.Count; x++)
            {
                if (x == dt.Columns.Count - 1)
                {
                    strComma = "";
                }
                if (x <= 3)
                {
                    strColumnNames = strColumnNames + "[" + dt.Columns[x].ToString() + "]" + strComma;
                }
                else
                {
                    strColumnNames = strColumnNames + "[DAY" + (x - 3).ToString() + "]" + strComma;
                }

            }

            return strColumnNames;
        }

        //public void UploadPartsSimulation(DataTable dt, string strPlant, string strUID)
        //{
        //    DataTable dtUpload = new DataTable();
        //    string strColumnValues;
        //    string strPipe = "|";

        //    for (int x = 0; x < dt.Rows.Count; x++)
        //    {
        //        strColumnValues = "";
        //        strPipe = "|";
        //        for (int y = 0; y < dt.Columns.Count; y++)
        //        {
        //            if (y == dt.Columns.Count - 1)
        //            {
        //                strPipe = "";
        //            }
        //            strColumnValues = strColumnValues + dt.DefaultView[x][y].ToString().Replace("'", "") + strPipe;
        //        }

        //        dtUpload = DPSI.PSI_PARTS_SIMULATION_BY_PLANT(strPlant, getColumns(dt), strColumnValues, dt.Columns[4].ToString(), strUID).Tables[0];
        //    }
        //}

        protected string createTempFolder(String FolderTemp)
        {

            string des = "~/TEMP/";
            string pth = HttpContext.Current.Server.MapPath(Path.Combine(des, FolderTemp));
            Directory.CreateDirectory(pth);

            return pth;
        }

        private void FixAppDomainRestartWhenTouchingFiles()
        {

            System.Reflection.PropertyInfo p = typeof(HttpRuntime).GetProperty("FileChangesMonitor", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

            object o = p.GetValue(null, null);

            System.Reflection.FieldInfo f = o.GetType().GetField("_dirMonSubdirs", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.IgnoreCase);

            object monitor = f.GetValue(o);

            System.Reflection.MethodInfo m = monitor.GetType().GetMethod("StopMonitoring", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            m.Invoke(monitor, new object[0]);

        }

        public DataTable getDataFilteredColumn(HttpPostedFile FU, string strPlant)
        {
            DataTable dt = new DataTable();

            string folderName = Convert.ToString(DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            string strFileName = FU.FileName;
            createTempFolder(folderName);
            string strDir = "~/TEMP/" + folderName;
            string strPath = strDir + "/" + strFileName;
            string FilePath = HttpContext.Current.Server.MapPath(strPath);
            FU.SaveAs(FilePath);
            FixAppDomainRestartWhenTouchingFiles();

            dt = excelToDatatable(FilePath, strDir, strPlant);


            if (dt.Rows.Count > 0)
            {
                dt.Columns["SUPPLIER"].ColumnName = "MAINVENDOR";
                dt.Columns.Remove("DESCRIPTION");
                dt.Columns.Remove("SUPPLIERNAME");
                dt.Columns.Remove("SUPPLIERSTCK");
                dt.Columns.Remove("EPPISTCK");
                dt.Columns.Remove("TOTALSTCK");
                dt.Columns.Remove("PlanLogical");
            }

            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 0;
            column.AutoIncrementStep = 1;
            dt.Columns.Add(column);
            int index = 0;
            foreach (DataRow row in dt.Rows)
            {
                row.SetField(column, ++index);
            }
            dt.Columns["Column1"].ColumnName = "ID";

            FixAppDomainRestartWhenTouchingFiles();
            Directory.Delete(HttpContext.Current.Server.MapPath(strDir), true);

            return dt;
        }

        public DataTable excelToDatatable(string FileName, string strDir, string strPlant)
        {


            DataTable dtResult = null;
            int totalSheet = 0; //No of sheets on excel file  
            using (OleDbConnection objConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';"))
            {
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = string.Empty;
                if (dt != null)
                {
                    var tempDataTable = (from dataRow in dt.AsEnumerable()
                                         where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                                         select dataRow).CopyToDataTable();
                    dt = tempDataTable;
                    totalSheet = dt.Rows.Count;
                    sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
                }
                cmd.Connection = objConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "excelData");
                dtResult = ds.Tables["excelData"];
                objConn.Close();
                //Returning Dattable  
            }

            DataTable dtFiltered = new DataTable();
            DataRow[] drFiltered;
            //FixAppDomainRestartWhenTouchingFiles();
            //Directory.Delete(HttpContext.Current.Server.MapPath(strDir), true);
            dtResult.Columns[8].ColumnName = "PlanLogical";
            string colFiltered = dtResult.Columns[8].ColumnName;
            int intInvalidPlantCount = dtResult.Select("Plant <> '" + strPlant + "'").Count();
            if (intInvalidPlantCount == 0)
            {
                drFiltered = dtResult.Select(colFiltered + " = 'SUPPLIER PLAN/DELIVERY'");
                dtFiltered = drFiltered.CopyToDataTable();
                dtResult.Clear();
                dtResult.AcceptChanges();
            }

            return dtFiltered;
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