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
    /// Summary description for PSI_SHORTAGELIST_BY_PLANT_UPLOAD
    /// </summary>
    public class PSI_SHORTAGELIST_BY_PLANT_UPLOAD : IHttpHandler, IRequiresSessionState
    {
        DALPSI_PLANT DPSI = new DALPSI_PLANT();
        static string errorMessage = "";

        public void ProcessRequest(HttpContext context)
        {
            string plantcode = System.Convert.ToString(context.Request.QueryString["plant"]);
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
                        UploadShortagelist(dt, plantcode, userid);
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
                if (x <= 6)
                {
                    strColumnNames = strColumnNames + "[" + dt.Columns[x].ToString() + "]" + strComma;
                }
                else
                {
                    strColumnNames = strColumnNames + "[DAY" + (x - 6).ToString() + "]" + strComma;
                }

            }

            return strColumnNames;
        }

        public void UploadShortagelist(DataTable dt, string strPlant, string strUID)
        {
            DataTable dtUpload = new DataTable();
            string strColumnValues;
            string strPipe = "|";

            DPSI.PSI_TRUNCATE_SHORTAGE_LIST_BY_PLANT(strPlant);
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                strColumnValues = "";
                strPipe = "|";
                for (int y = 0; y < dt.Columns.Count; y++)
                {
                    if (y == dt.Columns.Count - 1)
                    {
                        strPipe = "";
                    }
                    strColumnValues = strColumnValues + dt.DefaultView[x][y].ToString().Replace("'", "") + strPipe;
                }

                dtUpload = DPSI.PSI_UPLOAD_SHORTAGE_LIST_BY_PLANT(strPlant, getColumns(dt), strColumnValues, dt.Columns[7].ToString(), strUID).Tables[0];
            }
        }

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



            //formatExcel(strfilePath);

            dt = excelToDatatable(FilePath, strDir, strPlant);

            string oldColname = "", strColname = "";

            if (dt.Rows.Count > 0 )
            {
                DataTable dtList = new DataTable();
                DataTable dtColumnRetain = new DataTable();
                dtColumnRetain.Columns.Add("colName", typeof(string));

                for (int y = 0; y < dt.Columns.Count; y++)
                {
                    if (y == 0 || y == 2 || y == 3 || y == 5 || y == 14 || y >= 55)
                    {
                        DataRow drRet = dtColumnRetain.NewRow();
                        oldColname = dt.Columns[y].ToString();
                        strColname = oldColname.Replace(" ", "").Replace(".", "").Replace("#", "");
                        try
                        {
                            Convert.ToDateTime(strColname);
                            //strColname = "_" + strColname.Replace("/", "_");

                        }
                        catch (Exception ex)
                        {
                            ex.ToString();
                            strColname = strColname.Replace("/", "");
                        }
                        dt.Columns[oldColname].ColumnName = strColname;

                        drRet["colName"] = strColname;
                        dtColumnRetain.Rows.Add(drRet);
                        dtColumnRetain.AcceptChanges();

                    }


                }

                string strColvalue = "";
                int numberOfRecords = 0;
                for (int x = 0; x < dt.Columns.Count; x++)
                {
                    strColvalue = dt.Columns[x].ToString();
                    numberOfRecords = dtColumnRetain.Select("colName = '" + strColvalue + "'").Length;
                    if (numberOfRecords == 0)
                    {
                        dt.Columns.Remove(strColvalue);
                        x = 0;
                    }
                }

            }

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

            int intInvalidPlantCount = dtResult.Select("Plant <> '" + strPlant + "'").Count();
            if (intInvalidPlantCount == 0)
            {
                drFiltered = dtResult.Select("[Rece#/issue] = 'issue'");
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