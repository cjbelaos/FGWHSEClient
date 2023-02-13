using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Threading;
using System.Threading.Tasks;

namespace FGWHSEClient.Classes
{
    public class CtrlShortageList
    {
        FileWriter fw;
        Connection DBWorker;
        private SqlConnection conn;
        private DataTable CurrentData;

        public string msg;

        public CtrlShortageList() {
            DBWorker = new Connection();
            conn = DBWorker.SetConnectionSettings();
            fw = new FileWriter();
        }

        public DataTable LoadData(string[] PlantID = null,string[] VendorCode = null,string SupplierCode = "") {
            CurrentData = new DataTable();
            DataTable dtHead = new DataTable();
            DataTable dtDetail = new DataTable();
            string q = "";
            foreach (string _q in VendorCode)
            {
                q += string.Format(",'{0}'", _q);
            }
            if (q == "")
            {
                q = "," + SupplierCode;
            }
            string p = "";
            foreach (string _p in PlantID)
            {
                p += string.Format(",'{0}'", _p);
            }
            if (p == "")
            {
                p = ",";
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@PlantID", p.Substring(1));
            dic.Add("@VendorCode", q.Substring(1));
            DBWorker.ExecuteSP(conn, CurrentData, "PSI_SelectShortageList_V2", dic);
            //string sql = string.Format("SELECT * FROM PSIShortageListDet");
            //DBWorker.ExecuteReader(conn,sql,dtDetail);
            //HorizontalDetail(ref CurrentData, dtHead, dtDetail);
            return CurrentData;
        }
        public string GetData() {
            return JsonConvert.SerializeObject(CurrentData);
        }
        public string GetPlants() {
            DataTable dt = new DataTable();
            string sql = string.Format("SELECT * FROM [dbo].[PSI_TBL_M_Plants]");
            DBWorker.ExecuteReader(conn,sql,dt);
            return JsonConvert.SerializeObject(dt);
        }
        public string GetMaterial() {
            DataTable dt = new DataTable();
            string sql = string.Format("SELECT MaterialNumber,MaterialDescription FROM [dbo].[PSI_TBL_T_ShortageList]");
            DBWorker.ExecuteReader(conn,sql,dt);
            return JsonConvert.SerializeObject(dt);
        }
        public string GetVendor(string SupplierCode = "") {
            DataTable dt = new DataTable();
            string sql = string.Format("SELECT a.MainVendor AS SupplierCode,ISNULL(b.SupplierName,a.MainVendor) AS SupplierName " +
                "FROM (" +
                "   SELECT a.MainVendor" +
                "   FROM PSI_TBL_T_ShortageList_V2 a " +
                "   GROUP BY a.MainVendor "+
                ") a " +
                "LEFT JOIN TBL_M_Supplier b ON b.SupplierID = a.MainVendor " +
                "");
            if (SupplierCode != "" && SupplierCode != null)
            {
                sql += string.Format(" WHERE a.MainVendor IN ({0})",SupplierCode);
            }
            //return sql;
            DBWorker.ExecuteReader(conn, sql, dt);
            return JsonConvert.SerializeObject(dt);
        }
        public string GetCreateDate() {
            string sql = string.Format("SELECT create_date FROM sys.tables WHERE name = 'PSI_TBL_T_ShortageList_V2'");
            DataTable dt = new DataTable();
            DBWorker.ExecuteReader(conn, sql, dt);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["create_date"].ToString();
            }
            return "{}";
        }
        private void GetMaxMinDate(ref DateTime dFrom,ref DateTime dTo,string PlantID = "",string MainVendor = "") {
            DataTable dt = new DataTable();
            string sql = string.Format("SELECT MIN(b.DateInput) AS dFrom,MAX(b.DateInput) AS dTo " +
                "FROM [dbo].[PSI_TBL_T_ShortageList] a " +
                "LEFT JOIN [dbo].[PSI_TBL_T_ShortageListDet] b ON b.ShortageListID = a.ShortageListID " +
                "WHERE(a.PlantID = '{0}' OR '{0}' = '') AND(a.MainVendor = '{1}' OR '{1}' = '')",PlantID,MainVendor);
            DBWorker.ExecuteReader(conn, sql, dt);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["dFrom"] != null && dt.Rows[0]["dTo"] != null)
                {
                    dFrom = DateTime.Parse(dt.Rows[0]["dFrom"].ToString());
                    dTo = DateTime.Parse(dt.Rows[0]["dTo"].ToString());
                }
            }
        }
        private void HorizontalDetail(ref DataTable dt,DataTable Head,DataTable Detail) {
            List<string> lstColumns = new List<string>();
            Dictionary<string, Dictionary<string, string>> dicRows = new Dictionary<string, Dictionary<string, string>>();
            dt = new DataTable();
            dt = Head.Clone();
            dt.Clear();
            foreach (DataRow drHead in Head.Rows)
            {
                string ShortageListID = drHead["ShortageListID"].ToString();
                Dictionary<string, string> dicDetails = new Dictionary<string, string>();
                DataRow[] drs = Detail.Select(string.Format("ShortageListID = '{0}'",ShortageListID));
                foreach (DataRow dr in drs)
                {
                    DateTime Date = DateTime.Parse(dr["DateInput"].ToString());
                    string DateColumn = Date.ToString("_MM_dd_yyyy");
                    string Amount = dr["Value"].ToString();
                    //DataColumn dc = new DataColumn(DateColumn, Type.GetType("System.String"));
                    if (!lstColumns.Contains(DateColumn))
                    {
                        lstColumns.Add(DateColumn);
                    }
                    dicDetails.Add(DateColumn,Amount);
                }
                dicRows.Add(ShortageListID, dicDetails);
            }
            lstColumns.Sort();
            foreach (string dc in lstColumns)
            {
                dt.Columns.Add(dc,Type.GetType(("System.String")));
            }
            foreach (DataRow drHead in Head.Rows)
            {
                string ShortageListID = drHead["ShortageListID"].ToString();
                DataRow drNew = dt.NewRow();
                foreach (DataColumn col in dt.Columns)
                {
                    if (Head.Columns.Contains(col.ColumnName))
                    {
                        drNew[col.ColumnName] = drHead[col.ColumnName];
                    }
                    else
                    {
                        if (dicRows.ContainsKey(ShortageListID))
                        {
                            Dictionary<string, string> CurrentRow = dicRows[ShortageListID];
                            foreach (var item in CurrentRow)
                            {
                                drNew[item.Key] = item.Value;
                            }
                        }
                    }
                }
                dt.Rows.Add(drNew);
            }
        }
        public string SaveDataObject(List<ObjectShortageList> data) {
            //DBWorker.ExecuteSP(conn, new DataTable(), "PSI_TRUNCATEPSI");
            DataTable dt = GetPlant();
            fw.Write("Save ShortageList : " + data.Count);
            StaticInfo.Message = "Save ShortageList : " + data.Count;
            int ctr = 0;
            int ctr1 = 0;
            foreach (var item in data)
            {
                if (!StaticInfo.CanProcess)
                {
                    break;
                }
                ctr++;
                ctr1 = 0;
                string ShortageListID = "";
                Dictionary<string, string> dicHead = new Dictionary<string, string>();
                dicHead.Add("@PlantID",item.PlantID);
                dicHead.Add("@MaterialNumber", item.MaterialNumber);
                dicHead.Add("@MaterialDescription", item.MaterialDescription);
                dicHead.Add("@MainVendor", item.MainVendor);
                dicHead.Add("@OwnStock", item.OwnStock);
                dicHead.Add("@Issued", item.Issued);
                dicHead.Add("@PastDue", item.PastDue);
                DataTable dtHead = new DataTable();
                DBWorker.ExecuteSP(conn,dtHead,"PSI_ShortageListSave",dicHead);
                if (dtHead.Rows.Count > 0)
                {
                    ShortageListID = dtHead.Rows[0]["ShortageListID"].ToString();
                    foreach (var value in item.Values)
                    {
                        if (!StaticInfo.CanProcess)
                        {
                            break;
                        }
                        ctr1++;
                        fw.Write(string.Format("[{0}/{1}][{2}/{3}] Inserting Data ...", ctr, data.Count, ctr1, item.Values.Count));
                        StaticInfo.Message = (string.Format("[{0}/{1}][{2}/{3}] Inserting Data ...", ctr, data.Count, ctr1, item.Values.Count));
                        string date = DateTime.Parse(value.Key.Replace("_", "/").Substring(1)).ToString("yyyy-MM-dd");
                        Dictionary<string, string> dicDet = new Dictionary<string, string>();
                        dicDet.Add("@ShortageListID",ShortageListID);
                        dicDet.Add("@DateInput",date);
                        dicDet.Add("@Value",value.Value.ToString());
                        DBWorker.ExecuteSP(conn,new DataTable(),"PSI_ShortageListDetSave",dicDet);
                    }
                }
            }
            fw.Write("Transaction Commit");
            StaticInfo.Message = ("Transaction Commit");

            return "";
        }
        public string SaveDataObject_old(List<ObjectShortageList> data)
        {
            DataTable dt = GetPlant();
            fw.Write("Save ShortageList : " + data.Count);
            StaticInfo.Message = "Save ShortageList : " + data.Count;
            using (conn)
            {
                conn.Open();
                SqlTransaction transaction;
                transaction = conn.BeginTransaction("InsertData");
                try
                {
                    int ctr = 0;
                    int ctr1 = 0;
                    foreach (var item in data)
                    {
                        ctr++;
                        ctr1 = 0;
                        foreach (var value in item.Values)
                        {
                            ctr1++;
                            fw.Write(string.Format("[{0}/{1}][{2}/{3}] Inserting Data ...", ctr, data.Count, ctr1, item.Values.Count));
                            StaticInfo.Message = (string.Format("[{0}/{1}][{2}/{3}] Inserting Data ...", ctr, data.Count, ctr1, item.Values.Count));
                            SqlCommand cmd = conn.CreateCommand();
                            cmd.Connection = conn;
                            cmd.Transaction = transaction;
                            string date = DateTime.Parse(value.Key.Replace("_", "/").Substring(1)).ToString("yyyy-MM-dd");
                            cmd.CommandText = "PSI_ShortageListSave";
                            cmd.Parameters.AddWithValue("@PlantID", item.PlantID);
                            cmd.Parameters.AddWithValue("@MaterialNumber", item.MaterialNumber);
                            cmd.Parameters.AddWithValue("@MaterialDescription", item.MaterialDescription);
                            cmd.Parameters.AddWithValue("@MainVendor", item.MainVendor);
                            cmd.Parameters.AddWithValue("@OwnStock", item.OwnStock);
                            cmd.Parameters.AddWithValue("@Issued", item.Issued);
                            cmd.Parameters.AddWithValue("@PastDue", item.PastDue);
                            cmd.Parameters.AddWithValue("@DateInput", date);
                            cmd.Parameters.AddWithValue("@Value", value.Value);
                            cmd.Parameters.AddWithValue("@Stock", item.Stock);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                        };
                    }
                    fw.Write("Transaction Commit");
                    StaticInfo.Message = ("Transaction Commit");
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    fw.Write(ex.Message);
                    StaticInfo.Message = (ex.Message);
                    return ex.Message;
                }
            }
            return "";
        }
        //-----------
        public string SaveData(DataTable ExcelData) {
            msg = "";
            DataTable Plants = GetPlant();
            Dictionary<string, Dictionary<string, string>> dicHeads = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, Dictionary<DateTime, string>> dicRows = new Dictionary<string, Dictionary<DateTime, string>>();
            List<string> lstColumns = GetDateColumnsFromExcel(ExcelData);
            foreach (DataRow dr in ExcelData.Rows)
            {
                if (dr["Rece#/issue"].ToString() == "issue")
                {
                    Dictionary<string, string> dicHead = new Dictionary<string, string>();
                    Dictionary<DateTime, string> dicRow = new Dictionary<DateTime, string>();
                    string Plant = dr["Plant"].ToString();
                    string PlantID = GetPlantID(Plant,Plants);
                    string MaterialNumber = dr["Material Number"].ToString();
                    dicHead.Add("PlantID", PlantID);
                    dicHead.Add("MaterialNumber", MaterialNumber);
                    dicHead.Add("MaterialDescription", dr["Material Description"].ToString());
                    dicHead.Add("MainVendor", dr["Main Vendor"].ToString());
                    dicHead.Add("OwnStock", dr["Own stock"].ToString());
                    dicHead.Add("Issued", dr["Rece#/issue"].ToString());
                    dicHead.Add("PastDue", dr["Past"].ToString());
                    dicHead.Add("Stock", dr["Stock"].ToString());
                    string DataName = string.Format("P{0}M{1}",PlantID,MaterialNumber);
                    foreach (string col in lstColumns)
                    {
                        DateTime Date = DateTime.Parse(col);
                        string Amount = dr[col].ToString();
                        dicRow.Add(Date,Amount);
                    }
                    if (dicHeads.Keys.Contains(DataName))
                    {
                        dicHeads[DataName]["PastDue"] = (int.Parse(dicHeads[DataName]["PastDue"]) + int.Parse(dicHead["PastDue"].ToString())).ToString();
                        dicHeads[DataName]["Stock"] = (int.Parse(dicHeads[DataName]["Stock"]) + int.Parse(dicHead["Stock"].ToString())).ToString();
                        Dictionary<DateTime, string> tmp = new Dictionary<DateTime, string>();
                        foreach (var item in dicRows[DataName])
                        {
                            tmp.Add(item.Key, (int.Parse(dicRows[DataName][item.Key].ToString()) + int.Parse(dicRow[item.Key].ToString())).ToString());
                        }
                        dicRows[DataName] = tmp;
                    }
                    else {
                        dicHeads.Add(DataName, dicHead);
                        dicRows.Add(DataName, dicRow);
                    }
                }
            }
            return SaveData(dicHeads, dicRows);
        }
        public DataTable MyPlant() {
            return GetPlant();
        }
        private DataTable GetPlant() {
            DataTable dt = new DataTable();
            string sql = string.Format("SELECT * FROM [dbo].[PSI_TBL_M_Plants]");
            DBWorker.ExecuteReader(conn, sql, dt);
            return dt;
        }
        public string GetPlantID(string PlantCode,DataTable dtPlant) {
            DataRow[] SelectedRows = dtPlant.Select(string.Format("PlantCode = '{0}'",PlantCode));
            if (SelectedRows.Length > 0)
            {
                return SelectedRows[0]["PlantID"].ToString();
            }
            return "";
        }
        public List<string> GetDateColumnsFromExcel(DataTable dt) {
            List<string> lstCol = new List<string>();
            foreach (DataColumn dc in dt.Columns)
            {
                string col = dc.ColumnName;
                if (ExcelReader.IsDate(col))
                {
                    lstCol.Add(col);
                }
            }
            return lstCol;
        }
        private string SaveData(Dictionary<string,Dictionary<string,string>> Header,
            Dictionary<string,Dictionary<DateTime,string>> Detail) {
            msg = "Executing Stored Procedure";
            fw.Write(msg);
            using (conn)
            {
                conn.Open();


                SqlTransaction transaction;

                // Start a local transaction.
                transaction = conn.BeginTransaction("InsertData");

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                
                try
                {
                    int ctr = 0;
                    foreach (var item in Header)
                    {
                        ctr++;
                        if (Detail.Keys.Contains(item.Key))
                        {
                            int ctr1 = 0;
                            foreach (var det in Detail[item.Key])
                            {
                                ctr1++;
                                msg = string.Format("Saving {0} : [h:{1}/{2}][d:{3}/{4}]", item.Value["MaterialNumber"],ctr,Header[item.Key].Count,ctr1,Detail[item.Key].Count);
                                fw.Write(msg);
                                SqlCommand command = conn.CreateCommand();
                                command.Connection = conn;
                                command.Transaction = transaction;
                                string date = det.Key.ToString("yyyy-MM-dd");
                                command.CommandText = "PSI_ShortageListSave";
                                command.Parameters.AddWithValue("@PlantID", item.Value["PlantID"]);
                                command.Parameters.AddWithValue("@MaterialNumber", item.Value["MaterialNumber"]);
                                command.Parameters.AddWithValue("@MaterialDescription", item.Value["MaterialDescription"]);
                                command.Parameters.AddWithValue("@MainVendor", item.Value["MainVendor"]);
                                command.Parameters.AddWithValue("@OwnStock", item.Value["OwnStock"]);
                                command.Parameters.AddWithValue("@Issued", item.Value["Issued"]);
                                command.Parameters.AddWithValue("@PastDue", item.Value["PastDue"]);
                                command.Parameters.AddWithValue("@DateInput", date);
                                command.Parameters.AddWithValue("@Value", det.Value);
                                command.Parameters.AddWithValue("@Stock", item.Value["Stock"]);
                                command.CommandType = CommandType.StoredProcedure;
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    fw.Write("Committed");
                    return ("Both records are written to database.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("  Message: {0}", ex.Message);

                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Console.WriteLine("  Message: {0}", ex2.Message);
                        return string.Format("Rollback Exception Type: {0}", ex2.GetType());
                    }
                    return string.Format("Commit Exception Type: {0}", ex.GetType());
                }
            }
        }
        public string SendEmailToVendor() {
            DataTable dt = new DataTable();
            DBWorker.ExecuteSP(conn, dt, "PSI_SEND_EMAIL_VENDOR");
            return "{}";
        }
    }
}