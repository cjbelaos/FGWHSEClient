using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;

namespace FGWHSEClient.Classes
{
    public class CtrlPartsSimulation
    {
        Connection DBWorker;
        private SqlConnection conn;
        private DataTable CurrentData;
        public SqlConnection GetConnection() {
            return conn;
        }
        public CtrlPartsSimulation()
        {
            DBWorker = new Connection();
            conn = DBWorker.SetConnectionSettings();
        }
        public void LoadData(string[] PlantID,string[] VendorCode,string[] MaterialNumber,Boolean Formulated = false,string SupplierCode = "")
        {
            CurrentData = new DataTable();
            DataTable dt = new DataTable();
            string where = "",_VC = "",_MC = "";
            if (PlantID.Length > 0)
            {
                where += string.Format(" AND a.PlantID IN({0})",string.Join(",",PlantID));
            }
            if (VendorCode.Length > 0)
            {
                string q = "";
                foreach (string _q in VendorCode)
                {
                    q += string.Format(",'{0}'", _q);
                    _VC += string.Format(",'{0}'", _q);
                }
                _VC = _VC.Substring(1);
                where += string.Format(" AND c.MainVendor IN({0})", q.Substring(1));
            }
            else
            {
                where += string.Format(" AND c.MainVendor = '{0}'", SupplierCode);
                _VC = SupplierCode;
            }
            if (MaterialNumber.Length > 0)
            {
                string q = "";
                foreach (string _q in MaterialNumber)
                {
                    q += string.Format(",'{0}'",_q);
                    _MC += string.Format(",'{0}'", _q);
                }
                _MC = _MC.Substring(1);
                where += string.Format(" AND a.MaterialNumber IN({0})",q.Substring(1));
            }
            if (where != "")
            {
                where = "WHERE " + where.Substring(5) + " AND a.ForDos = 0";
            }
            else
            {
                where = "WHERE a.ForDos = 0 AND a.Value != 0 AND (a.DateInput = 'PAST' OR CONVERT(DATE,a.DateInput,101) >= CONVERT(DATE,GetDate(),101))";
            }
            string sql = string.Format("SELECT a.* " +
                "FROM [dbo].[PSI_TBL_T_PartsSimulation] a " +
                "LEFT JOIN PSI_TBL_M_PLANTS b ON b.PlantID = a.PlantID " +
                "LEFT JOIN PSI_TBL_T_ShortageList_V2 c ON c.Plant = b.PlantCode  AND c.MaterialNumber = a.MaterialNumber " +
                where +
                " ");
            //string sql = string.Format("SELECT * FROM [dbo].[PSIPartsSimulation] " +
            //    "WHERE (MaterialNumber = '{0}' OR '{0}' = '') AND (PlantID = '{1}' OR '{1}' = '') AND ForDos = 0",MaterialNumber,PlantID);
            //throw new Exception(sql);
            DBWorker.ExecuteReader(conn,sql,dt);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@PlantID",string.Join(",",PlantID));
            dic.Add("@VendorCode",_VC);
            dic.Add("@MaterialNumber",_MC);
            //dic.Add("@MaterialCode",MaterialNumber);
            DataTable dtShortageList = new DataTable();
            DBWorker.ExecuteSP(conn, dtShortageList, "PSI_GetShortageList_V2", dic);
            //CurrentData = dtShortageList;
            TriRowV2(ref CurrentData, dtShortageList, dt, Formulated);
            //Exception ex = new Exception(CurrentData.Rows.Count.ToString());
            //throw ex;

            //string sql = string.Format("SELECT ", PlantID,MaterialNumber);
            //DBWorker.ExecuteReader(conn, sql, dtHead);
        }
        public DataTable GetCurrentData() {
            return CurrentData;
        }
        public string SaveData(List<ObjectPartsSimulation> d) {
            Boolean useTransaction = false;
            //return JsonConvert.SerializeObject(d);
            if (useTransaction)
            {
                string msg = "";
                msg = string.Format("Save Parts Simulation...");
                StaticInfo.Message = msg;
                conn.Open();
                //SqlTransaction transaction;
                //transaction = conn.BeginTransaction();
                int ctr = 0;
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        foreach (var item in d)
                        {
                            SqlCommand command = new SqlCommand("PSI_PartsSimulationSave", conn);
                            command.CommandType = CommandType.StoredProcedure;

                            ctr++;
                            msg = string.Format("Processing : [{0}/{1}] for Parts Simulation", ctr, d.Count);
                            StaticInfo.Message = msg;
                            command.Parameters.AddWithValue("@PlantID", item.PlantID);
                            command.Parameters.AddWithValue("@MaterialNumber", item.MaterialNumber);
                            command.Parameters.AddWithValue("@ForDos", item.ForDos);
                            command.Parameters.AddWithValue("@DateInput", item.DateInput);
                            command.Parameters.AddWithValue("@VALUE", item.Value);
                            command.ExecuteNonQuery();
                        }
                        scope.Complete();
                    }
                    //transaction.Commit();
                    conn.Close();
                    return ("Parts Simulation Save.");
                }
                catch (Exception ex)
                {
                    conn.Close();
                    return ex.Message;
                }
            }
            else
            {
                int ctr = 0;
                string msg = "";
                msg = string.Format("Save Parts Simulation...");
                StaticInfo.Message = msg;
                foreach (var item in d)
                {
                    ctr++;
                    msg = string.Format("Processing : [{0}/{1}] for Parts Simulation", ctr, d.Count);
                    StaticInfo.Message = msg;
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("@PlantID",item.PlantID);
                    dic.Add("@MaterialNumber",item.MaterialNumber);
                    dic.Add("@ForDos",item.ForDos);
                    dic.Add("@DateInput",item.DateInput);
                    dic.Add("@VALUE",item.Value);
                    DBWorker.ExecuteSP(conn, new DataTable(), "PSI_PartsSimulationSave", dic);
                }
                return "Parts Simulation Save";
            }
        }
        public string GetData() {
            return JsonConvert.SerializeObject(CurrentData);
        }
        public DataTable GetRecurseData(int page,int count) {
            DataTable dtPage = CurrentData.Rows.Cast<System.Data.DataRow>().Skip((page - 1) * count).Take(count).CopyToDataTable();
            return dtPage;
        }
        public string GetMaterial() {
            string sql = string.Format("SELECT MaterialNumber,MaterialDescription FROM [dbo].[PSI_TBL_T_ShortageList_V2] GROUP BY MaterialNumber,MaterialDescription");
            DataTable dt = new DataTable();
            DBWorker.ExecuteReader(conn, sql, dt);
            return JsonConvert.SerializeObject(dt);
        }
        public string GetSupplier(string SupplierCode = "") {
            string sql = string.Format("SELECT a.MainVendor AS SupplierCode,ISNULL(b.SupplierName,a.MainVendor) AS SupplierName " +
                "FROM (" +
                "   SELECT a.MainVendor " +
                "   FROM PSI_TBL_T_ShortageList_V2 a " +
                "   GROUP BY a.MainVendor " +
                ") a " +
                "LEFT JOIN TBL_M_Supplier b ON b.SupplierID = a.MainVendor " +
                "");
            if (SupplierCode != "")
            {
                sql += string.Format(" WHERE a.MainVendor IN ({0})", SupplierCode);
            }
            DataTable dt = new DataTable();
            DBWorker.ExecuteReader(conn, sql, dt);
            return JsonConvert.SerializeObject(dt);
        }
        private void TriRowV2(ref DataTable dt, DataTable ShortageList, DataTable dtPSI, Boolean Formulated = false)
        {
            int currentRow = 0;
            dt = new DataTable();
            dt = ShortageList.Clone();
            dt.Clear();
            //DataColumn dcModel = new DataColumn("Model", Type.GetType("System.String"));
            dt.Columns["MainVendor"].ColumnName = "Supplier";
            dt.Columns["OwnStock"].ColumnName = "EPPISTCK";
            dt.Columns["Stock"].ColumnName = "SupplierSTCK";
            //DataColumn dcSupplierSTCK = new DataColumn("SupplierSTCK",Type.GetType("System.Int32"));
            DataColumn dcTotalSTCK = new DataColumn("TotalSTCK", Type.GetType("System.String"));
            DataColumn dcPlanLogical = new DataColumn("PlanLogical", Type.GetType("System.String"));
            //dt.Columns.Add(dcModel);
            //dt.Columns.Add(dcSupplierSTCK);
            dt.Columns.Add(dcTotalSTCK);
            dt.Columns.Add(dcPlanLogical);
            if (Formulated)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    dc.DataType = Type.GetType("System.String");
                }
            }
            currentRow = 2;
            foreach (DataRow dr in ShortageList.Rows)
            {
                DataRow dr1 = dt.NewRow();
                DataRow dr2 = dt.NewRow();
                DataRow dr3 = dt.NewRow();
                DataRow dr4 = dt.NewRow();
                //dr1["Model"] = "";
                //dr1["SupplierSTCK"] = 0;
                dr1["TotalSTCK"] = float.Parse(OtherFunctions.ifNull<string>(dr["OwnStock"].ToString(), "0").ToString()) + float.Parse(OtherFunctions.ifNull<string>(dr["Stock"].ToString(), "0").ToString());
                //dr1["TotalSTCK"] = "1";
                //dr2["TotalSTCK"] = "2";
                //dr3["TotalSTCK"] = "3";
                //dr4["TotalSTCK"] = "4";
                //dr2["SupplierSTCK"] = 0;
                dr2["TotalSTCK"] = float.Parse(OtherFunctions.ifNull<string>(dr["OwnStock"].ToString(), "0").ToString()) + float.Parse(OtherFunctions.ifNull<string>(dr["Stock"].ToString(), "0").ToString());
                //dr3["SupplierSTCK"] = 0;
                dr3["TotalSTCK"] = float.Parse(OtherFunctions.ifNull<string>(dr["OwnStock"].ToString(), "0").ToString()) + float.Parse(OtherFunctions.ifNull<string>(dr["Stock"].ToString(), "0").ToString());
                dr4["TotalSTCK"] = float.Parse(OtherFunctions.ifNull<string>(dr["OwnStock"].ToString(), "0").ToString()) + float.Parse(OtherFunctions.ifNull<string>(dr["Stock"].ToString(), "0").ToString());
                dr1["PlanLogical"] = "EPPI PLAN";
                dr2["PlanLogical"] = "SUPPLIER PLAN/DELIVERY";
                dr3["PlanLogical"] = "END STOCKS 1";
                dr4["PlanLogical"] = "END STOCKS 2";
                int colIndex = 10;
                float oldVal3 = 0;
                float oldVal4 = 0;
                foreach (DataColumn dc in ShortageList.Columns)
                {
                    var val = dr[dc.ColumnName];
                    string columnName = dc.ColumnName;
                    if (columnName == "OwnStock")
                    {
                        columnName = "EPPISTCK";
                    }
                    if (columnName == "MainVendor")
                    {
                        columnName = "Supplier";
                    }
                    if (columnName == "Stock")
                    {
                        columnName = "SupplierSTCK";
                    }
                    dr1[columnName] = val;
                    dr2[columnName] = val;
                    dr3[columnName] = val;
                    dr4[columnName] = val;
                    if (columnName == "Past" || ExcelReader.IsDate(columnName.Replace("_", "/").Substring(1)))
                    {
                        dr1[columnName] = OtherFunctions.ifNull<string>(val.ToString(), "0").ToString();
                        dr2[columnName] = 0;
                        if (dtPSI.Rows.Count > 0)
                        {
                            string condition = string.Format("DateInput = '{0}' AND MaterialNumber = '{1}'", columnName, dr2["MaterialNumber"]);
                            if (ExcelReader.IsDate(columnName.Replace("_", "/").Substring(1)))
                            {
                                condition = string.Format("DateInput = '{0}' AND MaterialNumber = '{1}'", columnName.Replace("_", "/").Substring(1), dr2["MaterialNumber"]);
                            }
                            var drs = dtPSI.Select(condition);
                            if (drs.Length > 0)
                            {
                                dr2[columnName] = drs[0]["Value"].ToString();
                            }
                        }
                        if (columnName == "Past")
                        {
                            oldVal3 = float.Parse(OtherFunctions.ifNull<string>(dr3["TotalSTCK"].ToString(), "0").ToString()) - float.Parse(OtherFunctions.ifNull<string>(dr1["Past"].ToString(), "0").ToString());
                            oldVal4 = float.Parse(OtherFunctions.ifNull<string>(dr3["TotalSTCK"].ToString(), "0").ToString()) + float.Parse(OtherFunctions.ifNull<string>(dr2["Past"].ToString(), "0").ToString()) - float.Parse(OtherFunctions.ifNull<string>(dr1["Past"].ToString(), "0").ToString());
                            dr3[columnName] = oldVal3;
                            dr4[columnName] = oldVal4;
                            if (Formulated)
                            {
                                dr3[columnName] = string.Format("={0}{1}-{2}{3}", GetExcelColumnName(colIndex - 1), currentRow + 2, GetExcelColumnName(colIndex + 1), currentRow);
                                dr4[columnName] = string.Format("={0}{1}+{2}{3}-{2}{4}", GetExcelColumnName(colIndex - 1), currentRow + 3, GetExcelColumnName(colIndex + 1), currentRow + 1, currentRow);
                            }
                            colIndex++;
                        }
                        else
                        {
                            var dr1v = float.Parse(dr1[columnName].ToString() == "" ? "0" : dr1[columnName].ToString());
                            oldVal3 = oldVal3 - float.Parse(dr1[columnName].ToString() == "" ? "0" : dr1[columnName].ToString());
                            oldVal4 = oldVal4 + float.Parse(dr2[columnName].ToString() == "" ? "0" : dr2[columnName].ToString()) - float.Parse(dr1[columnName].ToString() == "" ? "0" : dr1[columnName].ToString());
                            dr3[columnName] = oldVal3;
                            dr4[columnName] = oldVal4;
                            if (Formulated)
                            {
                                dr3[columnName] = string.Format("={0}{1}-{2}{3}", GetExcelColumnName(colIndex), currentRow + 2, GetExcelColumnName(colIndex + 1), currentRow);
                                dr4[columnName] = string.Format("={0}{1}+{2}{3}-{2}{4}", GetExcelColumnName(colIndex), currentRow + 3, GetExcelColumnName(colIndex + 1), currentRow + 1, currentRow);
                            }
                            //oldVal3 = float.Parse(dr3[columnName].ToString());
                            //oldVal4 = float.Parse(dr4[columnName].ToString());
                            colIndex++;
                        }
                    }
                }
                if (Formulated)
                {
                    dr1["TotalSTCK"] = string.Format("=G{0}+H{0}", currentRow);
                    dr2["TotalSTCK"] = string.Format("=G{0}+H{0}", currentRow + 1);
                    dr3["TotalSTCK"] = string.Format("=G{0}+H{0}", currentRow + 2);
                    dr4["TotalSTCK"] = string.Format("=G{0}+H{0}", currentRow + 3);
                }
                currentRow += 4;
                dt.Rows.Add(dr1);
                dt.Rows.Add(dr2);
                dt.Rows.Add(dr3);
                dt.Rows.Add(dr4);
            }
        }


        public DataTable DosData_buyer_confirm(string[] PlantID, string[] VendorCode, string[] MaterialNumber, string SupplierCode = "")
        {
            DataTable dt = new DataTable();
            DataTable dtPSI = new DataTable();
            string where = "", _VC = "", _MC = "";
            if (PlantID.Length > 0)
            {
                where += string.Format(" AND PlantID IN({0})", string.Join(",", PlantID));
            }
            if (VendorCode.Length > 0)
            {
                if (VendorCode[0].ToString() != "")
                {
                    string q = "";
                    foreach (string _q in VendorCode)
                    {
                        q += string.Format(",'{0}'", _q);
                        _VC += string.Format(",'{0}'", _q);
                    }
                    _VC = _VC.Substring(1);
                }
                else
                {
                    _VC = SupplierCode;
                }
                //where += string.Format(" AND MainVendor IN({0})", q.Substring(1));
            }
            else
            {
                _VC = SupplierCode;
            }
            if (MaterialNumber.Length > 0)
            {
                string q = "";
                foreach (string _q in MaterialNumber)
                {
                    q += string.Format(",'{0}'", _q);
                    _MC += string.Format(",'{0}'", _q);
                }
                _MC = _MC.Substring(1);
                where += string.Format(" AND MaterialNumber IN({0})", q.Substring(1));
            }
            if (where != "")
            {
                where = "WHERE " + where.Substring(5) + " AND ForDos = 0";
            }
            else
            {
                where = "WHERE ForDos = 0";
            }
            string sql = string.Format("SELECT * FROM [dbo].[PSI_TBL_T_PartsSimulation] " +
                where +
                " ", MaterialNumber, PlantID);
      
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@PlantID", string.Join(",", PlantID));
            dic.Add("@VendorCode", _VC);
            dic.Add("@MaterialNumber", _MC);
  
            DataTable dtShortageList = new DataTable();
            DBWorker.ExecuteSP(conn, dtShortageList, "PSI_GetShortageList_V2_BUYER_CONFIRM", dic);
            
            return dtShortageList;
        }

        public DataTable DosData(string[] PlantID, string[] VendorCode, string[] MaterialNumber,string SupplierCode = "") {
            DataTable dt = new DataTable();
            DataTable dtPSI = new DataTable();
            string where = "", _VC = "", _MC = "";
            if (PlantID.Length > 0)
            {
                where += string.Format(" AND PlantID IN({0})", string.Join(",", PlantID));
            }
            if (VendorCode.Length > 0)
            {
                if (VendorCode[0].ToString() != "")
                {
                    string q = "";
                    foreach (string _q in VendorCode)
                    {
                        q += string.Format(",'{0}'", _q);
                        _VC += string.Format(",'{0}'", _q);
                    }
                    _VC = _VC.Substring(1);
                }
                else {
                    _VC = SupplierCode;
                }
                //where += string.Format(" AND MainVendor IN({0})", q.Substring(1));
            }
            else
            {
                _VC = SupplierCode;
            }
            if (MaterialNumber.Length > 0)
            {
                string q = "";
                foreach (string _q in MaterialNumber)
                {
                    q += string.Format(",'{0}'", _q);
                    _MC += string.Format(",'{0}'", _q);
                }
                _MC = _MC.Substring(1);
                where += string.Format(" AND MaterialNumber IN({0})", q.Substring(1));
            }
            if (where != "")
            {
                where = "WHERE " + where.Substring(5) + " AND ForDos = 0";
            }
            else
            {
                where = "WHERE ForDos = 0";
            }
            string sql = string.Format("SELECT * FROM [dbo].[PSI_TBL_T_PartsSimulation] " +
                where + 
                " ", MaterialNumber, PlantID);
            //string sql = string.Format("SELECT * FROM [dbo].[PSIPartsSimulation] " +
            //    "WHERE (MaterialNumber = '{0}' OR '{0}' = '') AND (PlantID = '{1}' OR '{1}' = '') AND ForDos = 0",MaterialNumber,PlantID);
            DBWorker.ExecuteReader(conn, sql, dtPSI);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@PlantID", string.Join(",",PlantID));
            dic.Add("@VendorCode", _VC);
            dic.Add("@MaterialNumber", _MC);
            //dic.Add("@MaterialCode",MaterialNumber);
            DataTable dtShortageList = new DataTable();
            DBWorker.ExecuteSP(conn, dtShortageList, "PSI_GetShortageList_V2", dic);
            dt = DOSRow(dtShortageList,dtPSI);
            return dt;
        }
        private DataTable DOSRow(DataTable ShortageList, DataTable dtPSI) {
            DataTable dt = new DataTable();
            dt = ShortageList.Clone();
            dt.Clear();
            //DataColumn dcModel = new DataColumn("Model", Type.GetType("System.String"));
            dt.Columns["MainVendor"].ColumnName = "Supplier";
            dt.Columns["OwnStock"].ColumnName = "EPPISTCK";
            dt.Columns["Stock"].ColumnName = "SupplierSTCK";
            //DataColumn dcSupplierSTCK = new DataColumn("SupplierSTCK",Type.GetType("System.Int32"));
            DataColumn dcTotalSTCK = new DataColumn("TotalSTCK", Type.GetType("System.Decimal"));
            DataColumn dcPlanLogical = new DataColumn("PlanLogical", Type.GetType("System.String"));
            //dt.Columns.Add(dcModel);
            //dt.Columns.Add(dcSupplierSTCK);
            dt.Columns.Add(dcTotalSTCK);
            dt.Columns.Add(dcPlanLogical);
            foreach (DataRow dr in ShortageList.Rows)
            {
                DataRow dr1 = dt.NewRow();
                DataRow dr2 = dt.NewRow();
                DataRow dr3 = dt.NewRow();
                DataRow dr4 = dt.NewRow();
                //dr1["Model"] = "";
                //dr1["SupplierSTCK"] = 0;
                dr1["TotalSTCK"] = float.Parse(OtherFunctions.ifNull<string>(dr["OwnStock"].ToString(), "0").ToString()) + float.Parse(OtherFunctions.ifNull<string>(dr["Stock"].ToString(), "0").ToString());
                //dr2["SupplierSTCK"] = 0;
                dr2["TotalSTCK"] = float.Parse(OtherFunctions.ifNull<string>(dr["OwnStock"].ToString(), "0").ToString()) + float.Parse(OtherFunctions.ifNull<string>(dr["Stock"].ToString(), "0").ToString());
                //dr3["SupplierSTCK"] = 0;
                dr3["TotalSTCK"] = float.Parse(OtherFunctions.ifNull<string>(dr["OwnStock"].ToString(), "0").ToString()) + float.Parse(OtherFunctions.ifNull<string>(dr["Stock"].ToString(), "0").ToString());
                dr4["TotalSTCK"] = float.Parse(OtherFunctions.ifNull<string>(dr["OwnStock"].ToString(), "0").ToString()) + float.Parse(OtherFunctions.ifNull<string>(dr["Stock"].ToString(), "0").ToString());
                dr1["PlanLogical"] = "EPPI PLAN";
                dr2["PlanLogical"] = "SUPPLIER PLAN/DELIVERY";
                dr3["PlanLogical"] = "END STOCKS 1";
                dr4["PlanLogical"] = "END STOCKS 2";
                float oldVal3 = 0;
                float oldVal4 = 0;
                foreach (DataColumn dc in ShortageList.Columns)
                {
                    var val = dr[dc.ColumnName];
                    string columnName = dc.ColumnName;
                    if (columnName == "OwnStock")
                    {
                        columnName = "EPPISTCK";
                    }
                    if (columnName == "MainVendor")
                    {
                        columnName = "Supplier";
                    }
                    if (columnName == "Stock")
                    {
                        columnName = "SupplierSTCK";
                    }
                    dr1[columnName] = val;
                    dr2[columnName] = val;
                    dr3[columnName] = val;
                    dr4[columnName] = val;
                    if (columnName == "Past" || ExcelReader.IsDate(columnName.Replace("_", "/").Substring(1)))
                    {
                        dr1[columnName] = OtherFunctions.ifNull<string>(val.ToString(), "0").ToString();
                        dr2[columnName] = 0;
                        if (dtPSI.Rows.Count > 0)
                        {
                            string condition = string.Format("DateInput = '{0}' AND MaterialNumber = '{1}'", columnName, dr2["MaterialNumber"]);
                            if (ExcelReader.IsDate(columnName.Replace("_", "/").Substring(1)))
                            {
                                condition = string.Format("DateInput = '{0}' AND MaterialNumber = '{1}'", columnName.Replace("_", "/").Substring(1), dr2["MaterialNumber"]);
                            }
                            var drs = dtPSI.Select(condition);
                            if (drs.Length > 0)
                            {
                                string strPSIVal = drs[0]["Value"].ToString();
                                if (strPSIVal.Trim() == "")
                                {
                                    strPSIVal = "0";
                                }
                                dr2[columnName] = strPSIVal;
                            }
                        }
                        if (columnName == "Past")
                        {
                            oldVal3 = float.Parse(OtherFunctions.ifNull<string>(dr3["TotalSTCK"].ToString(), "0").ToString()) - float.Parse(OtherFunctions.ifNull<string>(dr1["Past"].ToString(), "0").ToString());
                            oldVal4 = float.Parse(OtherFunctions.ifNull<string>(dr3["TotalSTCK"].ToString(), "0").ToString()) + float.Parse(OtherFunctions.ifNull<string>(dr2["Past"].ToString(), "0").ToString()) - float.Parse(OtherFunctions.ifNull<string>(dr1["Past"].ToString(), "0").ToString());
                            dr3[columnName] = oldVal3;
                            dr4[columnName] = oldVal4;
                        }
                        else
                        {
                            var dr1v = float.Parse(dr1[columnName].ToString() == "" ? "0" : dr1[columnName].ToString());
                            oldVal3 = oldVal3 - float.Parse(dr1[columnName].ToString() == "" ? "0" : dr1[columnName].ToString());
                            oldVal4 = oldVal4 + float.Parse(dr2[columnName].ToString() == "" ? "0" : dr2[columnName].ToString()) - float.Parse(dr1[columnName].ToString() == "" ? "0" : dr1[columnName].ToString());
                            dr3[columnName] = oldVal3;
                            dr4[columnName] = oldVal4;
                            //oldVal3 = float.Parse(dr3[columnName].ToString());
                            //oldVal4 = float.Parse(dr4[columnName].ToString());
                        }
                    }
                }
                dt.Rows.Add(dr4);
            }
            return dt;
        }
        private void TriRow1(ref DataTable dt,DataTable ShortageList,DataTable dtPSI) {
            dt = new DataTable();
            dt = ShortageList.Clone();
            dt.Clear();
            DataColumn dcModel = new DataColumn("Model",Type.GetType("System.String"));
            dt.Columns["MainVendor"].ColumnName = "Supplier";
            dt.Columns["OwnStock"].ColumnName = "EPPISTCK";
            dt.Columns["Stock"].ColumnName = "SupplierSTCK";
            //DataColumn dcSupplierSTCK = new DataColumn("SupplierSTCK",Type.GetType("System.Int32"));
            DataColumn dcTotalSTCK = new DataColumn("TotalSTCK",Type.GetType("System.Int32"));
            DataColumn dcPlanLogical = new DataColumn("PlanLogical",Type.GetType("System.String"));
            dt.Columns.Add(dcModel);
            //dt.Columns.Add(dcSupplierSTCK);
            dt.Columns.Add(dcTotalSTCK);
            dt.Columns.Add(dcPlanLogical);
            foreach (DataRow dr in ShortageList.Rows)
            {
                DataRow dr1 = dt.NewRow();
                DataRow dr2 = dt.NewRow();
                DataRow dr3 = dt.NewRow();
                DataRow dr4 = dt.NewRow();
                dr1["Model"] = "";
                //dr1["SupplierSTCK"] = 0;
                dr1["TotalSTCK"] = Int32.Parse(dr["OwnStock"].ToString()) + Int32.Parse(dr["Stock"].ToString());
                //dr2["SupplierSTCK"] = 0;
                dr2["TotalSTCK"] = Int32.Parse(dr["OwnStock"].ToString()) + Int32.Parse(dr["Stock"].ToString());
                //dr3["SupplierSTCK"] = 0;
                dr3["TotalSTCK"] = Int32.Parse(dr["OwnStock"].ToString()) + Int32.Parse(dr["Stock"].ToString());
                dr4["TotalSTCK"] = Int32.Parse(dr["OwnStock"].ToString()) + Int32.Parse(dr["Stock"].ToString());
                dr1["PlanLogical"] = "EPPI PLAN";
                dr2["PlanLogical"] = "SUPPLIER PLAN/DELIVERY";
                dr3["PlanLogical"] = "END STOCKS 1";
                dr4["PlanLogical"] = "END STOCKS 2";
                int colIndex = 10;
                float oldVal3 = 0;
                float oldVal4 = 0;
                foreach (DataColumn dc in ShortageList.Columns)
                {
                    var val = dr[dc.ColumnName];
                    string columnName = dc.ColumnName;
                    if (columnName == "OwnStock")
                    {
                        columnName = "EPPISTCK";
                    }
                    if (columnName == "MainVendor")
                    {
                        columnName = "Supplier";
                    }
                    if (columnName == "Stock")
                    {
                        columnName = "SupplierSTCK";
                    }
                    dr1[columnName] = val;
                    dr2[columnName] = val;
                    dr3[columnName] = val;
                    dr4[columnName] = val;
                    if (columnName == "PastDue" || ExcelReader.IsDate(columnName.Replace("_","/").Substring(1)))
                    {
                        dr2[columnName] = 0;
                        if (dtPSI.Rows.Count > 0)
                        {
                            string condition = string.Format("DateInput = '{0}'", columnName);
                            if (ExcelReader.IsDate(columnName.Replace("_", "/").Substring(1)))
                            {
                                condition = string.Format("DateInput = '{0}'", columnName.Replace("_", "/").Substring(1));
                            }
                            var drs = dtPSI.Select(condition);
                            if (drs.Length > 0)
                            {
                                dr2[columnName] = drs[0]["Value"].ToString();
                            }
                        }
                        if (columnName == "PastDue")
                        {
                            oldVal3 = float.Parse(dr3["TotalSTCK"].ToString()) - float.Parse(dr1["PastDue"].ToString());
                            dr3[columnName] = oldVal3;
                            //dr4[columnName] = float.Parse(dr4["TotalSTCK"].ToString()) - float.Parse(dr1["PastDue"].ToString());
                            //dr3[columnName] = string.Format("={0}2-{1}1",GetExcelColumnName(colIndex-1),GetExcelColumnName(colIndex+1));
                            oldVal4 = float.Parse(dr3["TotalSTCK"].ToString()) + float.Parse(dr2["PastDue"].ToString()) - float.Parse(dr1["PastDue"].ToString());
                            dr4[columnName] = oldVal4;
                            colIndex++;
                        }
                        else {
                            var dr1v = float.Parse(dr1[columnName].ToString());
                            oldVal3 = oldVal3 - float.Parse(dr1[columnName].ToString());
                            dr3[columnName] = oldVal3;
                            oldVal4 = oldVal4 + float.Parse(dr2[columnName].ToString()) - float.Parse(dr1[columnName].ToString());
                            dr4[columnName] = oldVal4;
                            ////dr3[columnName] = string.Format("={0}3-{1}1", GetExcelColumnName(colIndex), GetExcelColumnName(colIndex + 1));
                            ////dr4[columnName] = string.Format("={0}4+{1}2-{1}1", GetExcelColumnName(colIndex), GetExcelColumnName(colIndex + 1));
                            //oldVal3 = float.Parse(dr3[columnName].ToString());
                            //oldVal4 = float.Parse(dr4[columnName].ToString());
                            colIndex++;
                        }
                    }
                }
                dt.Rows.Add(dr1);
                dt.Rows.Add(dr2);
                dt.Rows.Add(dr3);
                dt.Rows.Add(dr4);
            }
        }
        private void TriRow(ref DataTable dt,DataTable ShortageList,DataTable dtPSI,Boolean Formulated = false) {
            int currentRow = 0;
            dt = new DataTable();
            dt = ShortageList.Clone();
            dt.Clear();
            DataColumn dcModel = new DataColumn("Model",Type.GetType("System.String"));
            dt.Columns["MainVendor"].ColumnName = "Supplier";
            dt.Columns["OwnStock"].ColumnName = "EPPISTCK";
            dt.Columns["Stock"].ColumnName = "SupplierSTCK";
            //DataColumn dcSupplierSTCK = new DataColumn("SupplierSTCK",Type.GetType("System.Int32"));
            DataColumn dcTotalSTCK = new DataColumn("TotalSTCK",Type.GetType("System.String"));
            DataColumn dcPlanLogical = new DataColumn("PlanLogical",Type.GetType("System.String"));
            dt.Columns.Add(dcModel);
            //dt.Columns.Add(dcSupplierSTCK);
            dt.Columns.Add(dcTotalSTCK);
            dt.Columns.Add(dcPlanLogical);
            if (Formulated)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    dc.DataType = Type.GetType("System.String");
                }
            }
            currentRow = 2;
            foreach (DataRow dr in ShortageList.Rows)
            {
                DataRow dr1 = dt.NewRow();
                DataRow dr2 = dt.NewRow();
                DataRow dr3 = dt.NewRow();
                DataRow dr4 = dt.NewRow();
                dr1["Model"] = "";
                //dr1["SupplierSTCK"] = 0;
                dr1["TotalSTCK"] = float.Parse(OtherFunctions.ifNull<string>(dr["OwnStock"].ToString(),"0").ToString()) + float.Parse(OtherFunctions.ifNull<string>(dr["Stock"].ToString(),"0").ToString());
                //dr2["SupplierSTCK"] = 0;
                dr2["TotalSTCK"] = float.Parse(OtherFunctions.ifNull<string>(dr["OwnStock"].ToString(),"0").ToString()) + float.Parse(OtherFunctions.ifNull<string>(dr["Stock"].ToString(),"0").ToString());
                //dr3["SupplierSTCK"] = 0;
                dr3["TotalSTCK"] = float.Parse(OtherFunctions.ifNull<string>(dr["OwnStock"].ToString(), "0").ToString()) + float.Parse(OtherFunctions.ifNull<string>(dr["Stock"].ToString(), "0").ToString());
                dr4["TotalSTCK"] = float.Parse(OtherFunctions.ifNull<string>(dr["OwnStock"].ToString(), "0").ToString()) + float.Parse(OtherFunctions.ifNull<string>(dr["Stock"].ToString(), "0").ToString());
                dr1["PlanLogical"] = "EPPI PLAN";
                dr2["PlanLogical"] = "SUPPLIER PLAN/DELIVERY";
                dr3["PlanLogical"] = "END STOCKS 1";
                dr4["PlanLogical"] = "END STOCKS 2";
                int colIndex = 10;
                float oldVal3 = 0;
                float oldVal4 = 0;
                foreach (DataColumn dc in ShortageList.Columns)
                {
                    var val = dr[dc.ColumnName];
                    string columnName = dc.ColumnName;
                    if (columnName == "OwnStock")
                    {
                        columnName = "EPPISTCK";
                    }
                    if (columnName == "MainVendor")
                    {
                        columnName = "Supplier";
                    }
                    if (columnName == "Stock")
                    {
                        columnName = "SupplierSTCK";
                    }
                    dr1[columnName] = val;
                    dr2[columnName] = val;
                    dr3[columnName] = val;
                    dr4[columnName] = val;
                    if (columnName == "PastDue" || ExcelReader.IsDate(columnName.Replace("_","/").Substring(1)))
                    {
                        dr1[columnName] = OtherFunctions.ifNull<string>(val.ToString(), "0").ToString();
                        dr2[columnName] = 0;
                        if (dtPSI.Rows.Count > 0)
                        {
                            string condition = string.Format("DateInput = '{0}' AND MaterialNumber = '{1}'", columnName,dr2["MaterialNumber"]);
                            if (ExcelReader.IsDate(columnName.Replace("_", "/").Substring(1)))
                            {
                                condition = string.Format("DateInput = '{0}' AND MaterialNumber = '{1}'", columnName.Replace("_", "/").Substring(1),dr2["MaterialNumber"]);
                            }
                            var drs = dtPSI.Select(condition);
                            if (drs.Length > 0)
                            {
                                dr2[columnName] = drs[0]["Value"].ToString();
                            }
                        }
                        if (columnName == "PastDue")
                        {
                            oldVal3 = float.Parse(OtherFunctions.ifNull<string>(dr3["TotalSTCK"].ToString(),"0").ToString()) - float.Parse(OtherFunctions.ifNull<string>(dr1["PastDue"].ToString(),"0").ToString());
                            oldVal4 = float.Parse(OtherFunctions.ifNull<string>(dr3["TotalSTCK"].ToString(),"0").ToString()) + float.Parse(OtherFunctions.ifNull<string>(dr2["PastDue"].ToString(),"0").ToString()) - float.Parse(OtherFunctions.ifNull<string>(dr1["PastDue"].ToString(),"0").ToString());
                            dr3[columnName] = oldVal3;
                            dr4[columnName] = oldVal4;
                            if (Formulated)
                            {
                                dr3[columnName] = string.Format("={0}{1}-{2}{3}", GetExcelColumnName(colIndex - 1),currentRow+2, GetExcelColumnName(colIndex+1),currentRow);
                                dr4[columnName] = string.Format("={0}{1}+{2}{3}-{2}{4}", GetExcelColumnName(colIndex - 1),currentRow+3, GetExcelColumnName(colIndex+1),currentRow+1,currentRow);
                            }
                            colIndex++;
                        }
                        else {
                            var dr1v = float.Parse(dr1[columnName].ToString() == "" ? "0" : dr1[columnName].ToString());
                            oldVal3 = oldVal3 - float.Parse(dr1[columnName].ToString() == "" ? "0" : dr1[columnName].ToString());
                            oldVal4 = oldVal4 + float.Parse(dr2[columnName].ToString() == "" ? "0" : dr2[columnName].ToString()) - float.Parse(dr1[columnName].ToString() == "" ? "0" : dr1[columnName].ToString());
                            dr3[columnName] = oldVal3;
                            dr4[columnName] = oldVal4;
                            if (Formulated)
                            {
                                dr3[columnName] = string.Format("={0}{1}-{2}{3}", GetExcelColumnName(colIndex), currentRow + 2, GetExcelColumnName(colIndex+1), currentRow);
                                dr4[columnName] = string.Format("={0}{1}+{2}{3}-{2}{4}", GetExcelColumnName(colIndex), currentRow + 3, GetExcelColumnName(colIndex+1), currentRow + 1, currentRow);
                            }
                            //oldVal3 = float.Parse(dr3[columnName].ToString());
                            //oldVal4 = float.Parse(dr4[columnName].ToString());
                            colIndex++;
                        }
                    }
                }
                currentRow += 4;
                dt.Rows.Add(dr1);
                dt.Rows.Add(dr2);
                dt.Rows.Add(dr3);
                dt.Rows.Add(dr4);
            }
        }
        private string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }
    }
}