using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FGWHSEClient.Classes
{
    public class OtherFunctions
    {
        private static SqlConnection conn;
        private static SqlConnection connOutside;
        private static Connection mc;
        private static Connection mcOutside;
        /// <summary>
        /// Initialie the class
        /// </summary>
        private static void Init() {
            mc = new Connection();
            conn = mc.SetConnectionSettings(ConfigurationManager.AppSettings["FGWHSE_ConnectionString"]);
            mcOutside = new Connection();
            connOutside = mcOutside.SetConnectionSettings(ConfigurationManager.AppSettings["Outside_ConnectionString"]);
        }
        public string GetVendorBasedOnUser(string UserID) {
            Init();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@UserID", UserID);
            DataTable dt = new DataTable();
            mc.ExecuteSP(conn, dt, "PSI_GetVendorBasedOnUser", dic);
            List<string> lst = new List<string>();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["SupplierCode"].ToString().ToLower() == "all")
                {
                    DataTable dtSupplier = GetAllSupplierCode();
                    foreach (DataRow dr in dtSupplier.Rows)
                    {
                        lst.Add(string.Format("'{0}'", dr["MainVendor"].ToString()));
                    }
                }
                else {
                    var Codes = dt.Rows[0]["SupplierCode"].ToString().Split('/');
                    foreach (string SupplierCode in Codes)
                    {
                        lst.Add(string.Format("'{0}'", SupplierCode));
                    }
                }
            }
            return string.Join(",", lst);
        }
        private DataTable GetAllSupplierCode() {
            Init();
            DataTable dt = new DataTable();
            mc.ExecuteSP(conn, dt, "PSI_GetAllSupplierCode");
            return dt;
        }
        #region "ShortageList"
        /// <summary>
        /// get data from shortage list
        /// </summary>
        /// <returns></returns>
        public static DataTable ShortageList_GetData(string PlantID) {
            Init();
            DataTable dt = new DataTable();
            string sql = string.Format("SELECT * FROM [dbo].[PSI_TBL_T_ShortageList_V2] WHERE PlantID = '{0}'",PlantID);
            mc.ExecuteReader(conn,sql,dt);
            return dt;
        }
        /// <summary>
        /// DropTable from database
        /// </summary>
        public static void ShortageList_DropTable()
        {
            Handler.SharedInfo info = new Handler.SharedInfo();
            info.SetMessage("Drop the Table");
            Init();
            string sql = string.Format("IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PSI_TBL_T_ShortageList_V2' AND TABLE_SCHEMA = 'dbo') DROP TABLE dbo.PSI_TBL_T_ShortageList_V2; ");
            mc.ExecuteNonQuery(conn, sql);
            string sqlOutside = string.Format("IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PSI_TBL_T_ShortageList_V2' AND TABLE_SCHEMA = 'dbo') DROP TABLE dbo.PSI_TBL_T_ShortageList_V2; ");
            mcOutside.ExecuteNonQuery(connOutside, sqlOutside);
        }
        /// <summary>
        /// Create table for ShortageList
        /// </summary>
        /// <param name="cols"></param>
        public static void ShortageList_CreateTable(string cols)
        {
            Handler.SharedInfo info = new Handler.SharedInfo();
            info.SetMessage("Create the Table");
            Init();
            string sql = string.Format("CREATE TABLE [dbo].[PSI_TBL_T_ShortageList_V2](" +
                    "[ShortageListID][int] NULL," +
                    "[Plant][nvarchar](60) NULL," +
                    "[MaterialNumber][nvarchar](60) NULL," +
                    "[MaterialDescription][nvarchar](250) NULL," +
                    "[MainVendor][nvarchar](60) NULL," +
                    "[OwnStock][DECIMAL](18,2) NULL," +
                    "[ReceIssue][nvarchar](20) NULL," +
                    "[Past][DECIMAL](18,2) NULL" +
                cols + ",[FLAG] tinyint)");
            mc.ExecuteNonQuery(conn,sql);
            string sqlOutside = string.Format("CREATE TABLE [dbo].[PSI_TBL_T_ShortageList_V2](" +
                    "[ShortageListID][int] NULL," +
                    "[Plant][nvarchar](60) NULL," +
                    "[MaterialNumber][nvarchar](60) NULL," +
                    "[MaterialDescription][nvarchar](250) NULL," +
                    "[MainVendor][nvarchar](60) NULL," +
                    "[OwnStock][DECIMAL](18,2) NULL," +
                    "[ReceIssue][nvarchar](20) NULL," +
                    "[Past][DECIMAL](18,2) NULL" +
                cols + ",[FLAG] tinyint)");
            mcOutside.ExecuteNonQuery(connOutside,sqlOutside);
        }
        /// <summary>
        /// Insert data for ShortageList
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="cols"></param>
        public static void ShortageList_InsertData(DataTable dt,string cols) {
            FileWriter fw = new FileWriter();
            fw.Write(string.Format("Upload Shortage List"));
            fw.Write(string.Format("Number of Rows : {0}",dt.Rows.Count));
            Handler.SharedInfo info = new Handler.SharedInfo();
            Connection DBWorker = new Connection();
            SqlConnection conn = DBWorker.SetConnectionSettings();
            string sql = "";
            int ctr = 0;
            try
            {
                int ShortageListID = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    ctr++;
                    fw.Write(string.Format("Current row {0} out of {1}",ctr,dt.Rows.Count));
                    info.SetMessage(string.Format("Processing [{0}/{1}] ShortageList", ctr, dt.Rows.Count));
                    if (dr["Rece#/issue"].ToString() == "issue")
                    {
                        ShortageListID++;
                        string vals = string.Format("'{0}',", ShortageListID);
                        vals += string.Format("'{0}',", dr["Plant"]);
                        vals += string.Format("'{0}',", dr["Material Number"]);
                        vals += string.Format("'{0}',", dr["Material Description"].ToString().Replace("'","''"));
                        vals += string.Format("'{0}',", dr["Main Vendor"]);
                        vals += string.Format("'{0}',", dr["Own Stock"].ToString() == "" ? "0" : dr["Own Stock"]);
                        vals += string.Format("'{0}',", dr["Rece#/issue"]);
                        vals += string.Format("'{0}'", dr["Past"].ToString() == "" ? "0" : dr["Past"]);
                        foreach (DataColumn col in dt.Columns)
                        {
                            if (ExcelReader.IsDate(col.ColumnName))
                            {
                                string colName = DateTime.Parse(col.ColumnName).ToString("M/d/yyyy");
                                vals += string.Format(",'{0}'", dr[colName].ToString() == ""?0: dr[colName]);
                            }
                        }
                        sql = string.Format("INSERT INTO dbo.PSI_TBL_T_ShortageList_V2(ShortageListID,Plant,MaterialNumber,MaterialDescription,MainVendor,OwnStock,ReceIssue,Past{0},FLAG) VALUES({1},0)", cols, vals);
                        //fw.Write(sql);
                        DBWorker.ExecuteNonQuery(conn, sql);

                        //command.CommandText = sql;
                        //command.ExecuteNonQuery();
                    }
                }

                // Attempt to commit the transaction.
                //transaction.Commit();
                info.SetMessage("Done...");
                Console.WriteLine("Both records are written to database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);
                info.SetMessage("Error: " + sql);
                fw.Write(string.Format("An Error Encounter while running the query"));
                fw.Write(string.Format(sql));
                throw new Exception("Error: "+sql);
            }
            try
            {
                DBWorker.ExecuteSP(conn, new DataTable(), "PSI_ShortageListToAWS");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region "PartsSimulation"
        /// <summary>
        /// GetData from shortage list in parts simulation format
        /// </summary>
        /// <param name="min">First date based on shortagelist</param>
        /// <param name="max">Last date based on shortagelist</param>
        /// <returns>data in Datatable format</returns>
        public static DataTable PartsSimulation_GetData(DateTime min,DateTime max) {
            Init();
            DataTable dt = new DataTable();
            //List<string> dates = new List<string>();

            string[] dates = new string[Int32.Parse(((max - min).TotalDays + 1).ToString())];
            int index = 0;

            while (min <= max)
            {
                dates[index] = (string.Format(" SUM(CAST([{0}] AS INT)) AS {0} ", min.ToString("_MM_dd_yyyy")));
                index++;
                min = min.AddDays(1);
            }
            string sql = string.Format("SELECT * " +
                "FROM( " +
                "  SELECT MIN(CAST([ShortageListID] AS INT)) AS ShortageListID " +
                "      ,'' AS Model " +
                "      ,[MaterialNumber] " +
                "      ,[MaterialDescription] " +
                "      ,[MainVendor] AS Supplier " +
                "      ,[OwnStock] AS EPPISTCK" +
                "      ,0 AS SupplierSTCK" +
                "      ,[OwnStock] AS TotalSTCK " +
                "      ,'' AS PlanLogical " +
                "      , SUM(CAST([Past] AS INT)) AS PastDue, " +
                string.Join(",", dates) +
                "  FROM[db_EPPIIIP].[dbo].[ShortageList] " +
                "  WHERE MaterialNumber<> '' " +
                "  GROUP BY MaterialNumber,MaterialDescription,MainVendor,OwnStock " +
                ") a " +
                "ORDER BY CAST(a.ShortageListID AS INT)" +
                "");
            mc.ExecuteReader(conn, sql, dt);
            return dt;
        }
        /// <summary>
        /// Get data from own table(PSIPartsSimulation)
        /// </summary>
        /// <returns>data in Datatable format</returns>
        public static DataTable PartsSimulation_LoadSimulation(string filter) {
            Init();
            DataTable dt = new DataTable();
            string sql = string.Format("SELECT a.* " +
                "FROM  [dbo].[PSI_TBL_T_PARTSSIMULATION] a WITH(NOLOCK) " +
                "LEFT JOIN [dbo].[PSI_TBL_M_PLANTS] c  WITH(NOLOCK) ON c.PlantID = a.PlantID " +
                "LEFT JOIN [dbo].[PSI_TBL_T_ShortageList_V2] b  WITH(NOLOCK) ON b.Plant = c.PlantCode  AND b.MaterialNumber = a.MaterialNumber " +
                "AND b.MaterialNumber = a.MaterialNumber " +
                "WHERE a.Value != 0 AND (a.DateInput = 'PAST' OR CONVERT(DATE,a.DateInput,101) >= CONVERT(DATE,GetDate(),101)) ");
            if (filter != "")
            {
                sql += string.Format(" AND {0}",filter);
            }
            mc.ExecuteReader(conn, sql, dt);
            return dt;
        }
        /// <summary>
        /// to get the list of material
        /// code, code | name (supplier)
        /// </summary>
        /// <param name="Supplier">Optional | supplier code</param>
        /// <returns>data in datatable format</returns>
        public static DataTable PartsSimulation_GetMaterial(string Supplier = "") {
            Init();
            DataTable dt = new DataTable();
            string where = "";
            if (Supplier != "")
            {
                where = string.Format("AND MainVendor = '{0}'",Supplier);
            }
            string sql = "SELECT MaterialNumber AS Value,CONCAT(MaterialDescription,' (',MainVendor,')') AS Name " +
                "FROM [dbo].[ShortageList] " +
                "WHERE MaterialNumber<> '' " + where + " " +
                "GROUP BY MaterialNumber,MaterialDescription,MainVendor " +
                "ORDER BY MaterialNumber ";
            mc.ExecuteReader(conn,sql,dt);
            return dt;
        }
        /// <summary>
        /// Capture the first and last date from shortagelist
        /// </summary>
        /// <param name="min">ByRef - FirstDate</param>
        /// <param name="max">ByRef - LastDate</param>
        public static void PartsSimulation_SetMinMaxDate(ref DateTime min,ref DateTime max) {
            Init();
            string sql = string.Format("SELECT TOP 1 * FROM ShortageList");
            DataTable dt = new DataTable();
            mc.ExecuteReader(conn, sql, dt);
            foreach (DataColumn dc in dt.Columns)
            {
                string col = dc.ColumnName.ToString().Substring(1).Replace("_", "/");
                if (ExcelReader.IsDate(col))
                {
                    min = DateTime.Parse(col);
                    break;
                }
            }
            DataColumn lastColumn = dt.Columns[dt.Columns.Count - 1];
            max = DateTime.Parse(lastColumn.ColumnName.ToString().Substring(1).Replace("_", "/"));
        }
        /// <summary>
        /// Save data for parts simulation only without changing the shortagelist
        /// </summary>
        /// <param name="obj">JSON Object from AJAX</param>
        public static void PartsSimulation_SaveData(List<ObjectPartsSimulation> obj) {
            Init();
            conn.Open();
            SqlTransaction transaction;
            transaction = conn.BeginTransaction("InsertData");
            try
            {
                foreach (ObjectPartsSimulation item in obj)
                {
                    SqlCommand command = new SqlCommand("SavePartsSimulation", conn)
                    {
                        Transaction = transaction,
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@MaterialNumber", item.MaterialNumber);
                    command.Parameters.AddWithValue("@DateInput", item.DateInput);
                    command.Parameters.AddWithValue("@Value", item.Value);
                    command.ExecuteNonQuery();
                }
                transaction.Commit();
                conn.Close();
                Console.WriteLine("Both records are written to database.");
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);
                // Attempt to roll back the transaction.
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    conn.Close();
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                }
            }
        }
        #endregion
        public static object ifNull<T>(T obj,T ret) {
            return obj == null || obj.ToString() == "" ?ret:obj;
        }
        public static void DeleteStockEarlierToday() {
            string sql = string.Format("DELETE FROM PSI_TBL_M_SUPPLIER_STOCK WHERE LastUpdate < '{0}'",DateTime.Now.ToString("yyyy-MM-dd"));
            Init();
            mc.ExecuteNonQuery(conn, sql);
        }
    }
}