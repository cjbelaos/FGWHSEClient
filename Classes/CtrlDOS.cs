using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace FGWHSEClient.Classes
{
    public class CtrlDOS
    {
        Connection DBWorker;
        private SqlConnection conn;
        public CtrlDOS(){
            DBWorker = new Connection();
            conn = DBWorker.SetConnectionSettings();
        }
        /// <summary>
        /// get the list of plants
        /// </summary>
        /// <returns></returns>
        public string GetPlants() {
            DataTable dt = new DataTable();
            DBWorker.ExecuteSP(conn, dt, "PSI_PlantsGetData");
            return JsonConvert.SerializeObject(dt);
        }
        /// <summary>
        /// get the list of vendors
        /// </summary>
        /// <returns></returns>
        public string GetVendors(string SupplierCode = "") {
            DataTable dt = new DataTable();
            //DBWorker.ExecuteSP(conn, dt, "PSI_VendorGetData");
            string sql = string.Format("SELECT a.MainVendor AS SUPPLIERCODE,ISNULL(b.SUPPLIERNAME,a.MainVendor) AS SUPPLIERNAME " +
                "FROM (" +
                "   SELECT a.MainVendor " +
                "   FROM [dbo].[PSI_TBL_T_ShortageList_V2] a " +
                "   GROUP BY a.MainVendor " +
                ") a " +
                "LEFT JOIN TBL_M_Supplier b ON b.SupplierID = a.MainVendor " +
                "");
            if (SupplierCode != "")
            {
                sql += string.Format(" WHERE a.MainVendor IN ({0})", SupplierCode);
            }
            DBWorker.ExecuteReader(conn,sql,dt);
            return JsonConvert.SerializeObject(dt);
        }
        /// <summary>
        /// get the list of materials
        /// </summary>
        /// <returns></returns>
        public string GetMaterial() {
            DataTable dt = new DataTable();
            string sql = string.Format("SELECT MaterialNumber,MaterialDescription " +
                "FROM [dbo].[PSI_TBL_T_ShortageList_V2] " +
                "GROUP BY MaterialNumber,MaterialDescription ");
            DBWorker.ExecuteReader(conn,sql,dt);
            return JsonConvert.SerializeObject(dt);
        }
        /// <summary>
        /// get the dos data
        /// </summary>
        /// <param name="PlantID"></param>
        /// <param name="VendorCode"></param>
        /// <param name="MaterialNumber"></param>
        /// <returns></returns>
        public string GetData(string[] PlantID,string[] VendorCode,string[] MaterialNumber,string SupplierCode = "") {
            CtrlPartsSimulation CtrlPS = new CtrlPartsSimulation();
            //CtrlPS.LoadData(PlantID, VendorCode, MaterialNumber, false);
            DataTable dtParts = CtrlPS.DosData(PlantID, VendorCode, MaterialNumber,SupplierCode);
            DataTable Parts = dtParts.Clone();
            Parts.Clear();
            string[] AdditionalStringColumns = { "Cat_Name","Prob_Cat_name","Reason","PIC","CounterMeasure" };
            for (int i = 0; i < AdditionalStringColumns.Length; i++)
            {
                DataColumn col = new DataColumn(AdditionalStringColumns[i],Type.GetType("System.String"));
                Parts.Columns.Add(col);
            }
            foreach (DataRow dr in dtParts.Rows)
            {
                Parts.ImportRow(dr);
            }
            //---
            string where = "";
            if (PlantID.Length > 0)
            {
                where += string.Format(" AND a.PlantID IN({0})", string.Join(",", PlantID));
            }
            if (VendorCode.Length > 0)
            {
                if (VendorCode[0].ToString() != "")
                {
                    string q = "";
                    foreach (string _q in VendorCode)
                    {
                        q += string.Format(",'{0}'", _q);
                    }
                    where += string.Format(" AND a.SupplierCode IN({0})", q.Substring(1));
                }
                else {
                    if (VendorCode.Length > 1)
                    {
                        if (VendorCode[1].ToString() != "")
                        {
                            string q = "";
                            foreach (string _q in VendorCode)
                            {
                                if (_q != "")
                                {
                                    q += string.Format(",'{0}'", _q);
                                }
                            }
                            where += string.Format(" AND a.SupplierCode IN({0})", q.Substring(1));
                        }
                        else {
                            where += string.Format(" AND a.SupplierCode IN ({0})", SupplierCode);
                        }
                    }
                    else
                    {
                        where += string.Format(" AND a.SupplierCode IN ({0})", SupplierCode);
                    }
                }
            }
            else
            {
                where += string.Format(" AND a.SupplierCode IN ({0})", SupplierCode);
            }
            if (MaterialNumber.Length > 0)
            {
                string q = "";
                foreach (string _q in MaterialNumber)
                {
                    q += string.Format(",'{0}'", _q);
                }
                where += string.Format(" AND a.MaterialCode IN({0})", q.Substring(1));
            }
            if (where != "")
            {
                where = "WHERE " + where.Substring(5) + " AND REPLACE(CONVERT(NVARCHAR,a.DateCreated,102),'.','-') = REPLACE(CONVERT(NVARCHAR,GetDate(),102),'.','-')";
            }
            else
            {
                where = "WHERE REPLACE(CONVERT(NVARCHAR,a.DateCreated,102),'.','-') = REPLACE(CONVERT(NVARCHAR,GetDate(),102),'.','-')";
            }
            string sql = string.Format("SELECT a.*,b.PlantCode " +
                ",c.ModelCode,c.Category AS CategoryCode " +
                "FROM PSI_TBL_T_DOS a " +
                "LEFT JOIN PSI_TBL_M_Plants b ON b.PlantID = a.PlantID " +
                "LEFT JOIN PSI_TBL_M_BASIS_MASTER_DETAILS c ON c.Plant = b.PlantCode " +
                " AND c.SupplierCode = a.SupplierCode " +
                " AND c.MaterialCode = a.MaterialCode " +
                where, PlantID,VendorCode,MaterialNumber);
            //return sql;
            DataTable DOS = new DataTable();
            DBWorker.ExecuteReader(conn,sql,DOS);
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("@PlantID",PlantID);
            //dic.Add("@VendorCode",VendorCode);
            //dic.Add("@MaterialNumber",MaterialNumber);
            //DataTable Parts = new DataTable();
            ////DBWorker.ExecuteSP(conn, dt, "PSIDOSGetData",dic);
            //DBWorker.ExecuteSP(conn, Parts, "PSI_GetPartsSimulation", dic);
            //Dictionary<string, DataTable> dic = new Dictionary<string, DataTable>();
            //dic.Add("Parts",Parts);
            //dic.Add("DOS",DOS);
            foreach (DataRow dr in Parts.Rows)
            {
                string _PlantID = dr["PlantID"].ToString();
                string _SupplierCode = dr["SupplierID"].ToString();
                string _MaterialCode = dr["MaterialNumber"].ToString();
                string condition = string.Format("(PlantID = '{0}' OR '{0}' = '') AND (SupplierCode = '{1}' OR '{1}' = '') AND (MaterialCode = '{2}' OR '{2}' = '')", _PlantID, _SupplierCode, _MaterialCode);
                DataRow[] drsDOS = DOS.Select(condition);
                //return drsDOS.Length.ToString();
                if (drsDOS.Length > 0)
                {
                    DataRow drDos = drsDOS[0];
                    //dr["Category"] = drDos["CategoryCode"];
                    dr["Cat_Name"] = drDos["Cat_Name"];
                    //dr["MODEL"] = drDos["ModelCode"];
                    dr["Prob_Cat_Name"] = drDos["Problem_Cat"];
                    dr["Reason"] = drDos["Reason"];
                    dr["PIC"] = drDos["PIC"];
                    dr["CounterMeasure"] = drDos["CounterMeasure"];
                    //Parts.AcceptChanges();
                }
                //foreach (DataColumn dc in Parts.Columns)
                //{
                //}
            }
            return JsonConvert.SerializeObject(Parts);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PlantID"></param>
        /// <param name="VendorCode"></param>
        /// <param name="MaterialNumber"></param>
        /// <returns></returns>
        /// 
        private DataTable GetDataTable_buyer_confirm(string[] PlantID, string[] VendorCode, string[] MaterialNumber, string SupplierCode = "")
        {
            CtrlPartsSimulation CtrlPS = new CtrlPartsSimulation();
            //CtrlPS.LoadData(PlantID, VendorCode, MaterialNumber, false);
            DataTable dtParts = CtrlPS.DosData_buyer_confirm(PlantID, VendorCode, MaterialNumber, SupplierCode);
            DataTable Parts = dtParts.Clone();
            Parts.Clear();
            string[] AdditionalStringColumns = { "Cat_Name", "Prob_Cat_name", "Reason", "PIC", "CounterMeasure" };
            for (int i = 0; i < AdditionalStringColumns.Length; i++)
            {
                DataColumn col = new DataColumn(AdditionalStringColumns[i], Type.GetType("System.String"));
                Parts.Columns.Add(col);
            }
            foreach (DataRow dr in dtParts.Rows)
            {
                Parts.ImportRow(dr);
            }
            //---
            string where = "";
            if (PlantID.Length > 0)
            {
                where += string.Format(" AND a.PlantID IN({0})", string.Join(",", PlantID));
            }
            if (VendorCode.Length > 0)
            {
                if (VendorCode[0].ToString() != "")
                {
                    string q = "";
                    foreach (string _q in VendorCode)
                    {
                        q += string.Format(",'{0}'", _q);
                    }
                    where += string.Format(" AND a.SupplierCode IN({0})", q.Substring(1));
                }
                else
                {
                    if (SupplierCode != "")
                    {
                        where += string.Format(" AND a.SupplierCode IN({0})", SupplierCode);
                    }
                }
            }
            else
            {
                if (SupplierCode != "")
                {
                    where += string.Format(" AND a.SupplierCode IN({0})", SupplierCode);
                }
            }
            if (MaterialNumber.Length > 0)
            {
                string q = "";
                foreach (string _q in MaterialNumber)
                {
                    q += string.Format(",'{0}'", _q);
                }
                where += string.Format(" AND a.MaterialCode IN({0})", q.Substring(1));
            }
            if (where != "")
            {
                where = "WHERE " + where.Substring(5) + " AND REPLACE(CONVERT(NVARCHAR,a.DateCreated,102),'.','-') = REPLACE(CONVERT(NVARCHAR,GetDate(),102),'.','-')";
            }
            else
            {
                where = "WHERE REPLACE(CONVERT(NVARCHAR,a.DateCreated,102),'.','-') = REPLACE(CONVERT(NVARCHAR,GetDate(),102),'.','-')";
            }
            string sql = string.Format("SELECT a.*,b.PlantCode " +
                ",c.ModelCode,c.Category AS CategoryCode " +
                "FROM PSI_TBL_T_DOS a " +
                "LEFT JOIN PSI_TBL_M_Plants b ON b.PlantID = a.PlantID " +
                "LEFT JOIN PSI_TBL_M_BASIS_MASTER_DETAILS c ON c.Plant = b.PlantCode " +
                " AND c.SupplierCode = a.SupplierCode " +
                " AND c.MaterialCode = a.MaterialCode " +
                where, PlantID, VendorCode, MaterialNumber);
            //DataTable temp = new DataTable();
            //DBWorker.StringTODT(sql,ref temp);
            //return temp;
            DataTable DOS = new DataTable();
            DBWorker.ExecuteReader(conn, sql, DOS);
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("@PlantID",PlantID);
            //dic.Add("@VendorCode",VendorCode);
            //dic.Add("@MaterialNumber",MaterialNumber);
            //DataTable Parts = new DataTable();
            ////DBWorker.ExecuteSP(conn, dt, "PSIDOSGetData",dic);
            //DBWorker.ExecuteSP(conn, Parts, "PSI_GetPartsSimulation", dic);
            //Dictionary<string, DataTable> dic = new Dictionary<string, DataTable>();
            //dic.Add("Parts",Parts);
            //dic.Add("DOS",DOS);
            foreach (DataRow dr in Parts.Rows)
            {
                string _PlantID = dr["PlantID"].ToString();
                string _SupplierCode = dr["SupplierID"].ToString();
                string _MaterialCode = dr["MaterialNumber"].ToString();
                string condition = string.Format("(PlantID = '{0}' OR '{0}' = '') AND (SupplierCode = '{1}' OR '{1}' = '') AND (MaterialCode = '{2}' OR '{2}' = '')", _PlantID, _SupplierCode, _MaterialCode);
                DataRow[] drsDOS = DOS.Select(condition);
                //return drsDOS.Length.ToString();
                if (drsDOS.Length > 0)
                {
                    DataRow drDos = drsDOS[0];
                    //dr["Category"] = drDos["CategoryCode"];
                    dr["Cat_Name"] = drDos["Cat_Name"];
                    //dr["MODEL"] = drDos["ModelCode"];
                    dr["Prob_Cat_Name"] = drDos["Problem_Cat"];
                    dr["Reason"] = drDos["Reason"];
                    dr["PIC"] = drDos["PIC"];
                    dr["CounterMeasure"] = drDos["CounterMeasure"];
                    //Parts.AcceptChanges();
                }
                //foreach (DataColumn dc in Parts.Columns)
                //{
                //}
            }
            return Parts;
        }
        private DataTable GetDataTable(string[] PlantID, string[] VendorCode, string[] MaterialNumber,string SupplierCode = "") {
            CtrlPartsSimulation CtrlPS = new CtrlPartsSimulation();
            //CtrlPS.LoadData(PlantID, VendorCode, MaterialNumber, false);
            DataTable dtParts = CtrlPS.DosData(PlantID, VendorCode, MaterialNumber,SupplierCode);
            DataTable Parts = dtParts.Clone();
            Parts.Clear();
            string[] AdditionalStringColumns = { "Cat_Name", "Prob_Cat_name", "Reason", "PIC", "CounterMeasure" };
            for (int i = 0; i < AdditionalStringColumns.Length; i++)
            {
                DataColumn col = new DataColumn(AdditionalStringColumns[i], Type.GetType("System.String"));
                Parts.Columns.Add(col);
            }
            foreach (DataRow dr in dtParts.Rows)
            {
                Parts.ImportRow(dr);
            }
            //---
            string where = "";
            if (PlantID.Length > 0)
            {
                where += string.Format(" AND a.PlantID IN({0})", string.Join(",", PlantID));
            }
            if (VendorCode.Length > 0)
            {
                if (VendorCode[0].ToString() != "")
                {
                    string q = "";
                    foreach (string _q in VendorCode)
                    {
                        q += string.Format(",'{0}'", _q);
                    }
                    where += string.Format(" AND a.SupplierCode IN({0})", q.Substring(1));
                }
                else
                {
                    if (SupplierCode != "")
                    {
                        where += string.Format(" AND a.SupplierCode IN({0})", SupplierCode);
                    }
                }
            }
            else
            {
                if (SupplierCode != "")
                {
                    where += string.Format(" AND a.SupplierCode IN({0})", SupplierCode);
                }
            }
            if (MaterialNumber.Length > 0)
            {
                string q = "";
                foreach (string _q in MaterialNumber)
                {
                    q += string.Format(",'{0}'", _q);
                }
                where += string.Format(" AND a.MaterialCode IN({0})", q.Substring(1));
            }
            if (where != "")
            {
                where = "WHERE " + where.Substring(5) + " AND REPLACE(CONVERT(NVARCHAR,a.DateCreated,102),'.','-') = REPLACE(CONVERT(NVARCHAR,GetDate(),102),'.','-')";
            }
            else
            {
                where = "WHERE REPLACE(CONVERT(NVARCHAR,a.DateCreated,102),'.','-') = REPLACE(CONVERT(NVARCHAR,GetDate(),102),'.','-')";
            }
            string sql = string.Format("SELECT a.*,b.PlantCode " +
                ",c.ModelCode,c.Category AS CategoryCode " +
                "FROM PSI_TBL_T_DOS a " +
                "LEFT JOIN PSI_TBL_M_Plants b ON b.PlantID = a.PlantID " +
                "LEFT JOIN PSI_TBL_M_BASIS_MASTER_DETAILS c ON c.Plant = b.PlantCode " +
                " AND c.SupplierCode = a.SupplierCode " +
                " AND c.MaterialCode = a.MaterialCode " +
                where, PlantID, VendorCode, MaterialNumber);
            //DataTable temp = new DataTable();
            //DBWorker.StringTODT(sql,ref temp);
            //return temp;
            DataTable DOS = new DataTable();
            DBWorker.ExecuteReader(conn, sql, DOS);
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("@PlantID",PlantID);
            //dic.Add("@VendorCode",VendorCode);
            //dic.Add("@MaterialNumber",MaterialNumber);
            //DataTable Parts = new DataTable();
            ////DBWorker.ExecuteSP(conn, dt, "PSIDOSGetData",dic);
            //DBWorker.ExecuteSP(conn, Parts, "PSI_GetPartsSimulation", dic);
            //Dictionary<string, DataTable> dic = new Dictionary<string, DataTable>();
            //dic.Add("Parts",Parts);
            //dic.Add("DOS",DOS);
            foreach (DataRow dr in Parts.Rows)
            {
                string _PlantID = dr["PlantID"].ToString();
                string _SupplierCode = dr["SupplierID"].ToString();
                string _MaterialCode = dr["MaterialNumber"].ToString();
                string condition = string.Format("(PlantID = '{0}' OR '{0}' = '') AND (SupplierCode = '{1}' OR '{1}' = '') AND (MaterialCode = '{2}' OR '{2}' = '')", _PlantID, _SupplierCode, _MaterialCode);
                DataRow[] drsDOS = DOS.Select(condition);
                //return drsDOS.Length.ToString();
                if (drsDOS.Length > 0)
                {
                    DataRow drDos = drsDOS[0];
                    //dr["Category"] = drDos["CategoryCode"];
                    dr["Cat_Name"] = drDos["Cat_Name"];
                    //dr["MODEL"] = drDos["ModelCode"];
                    dr["Prob_Cat_Name"] = drDos["Problem_Cat"];
                    dr["Reason"] = drDos["Reason"];
                    dr["PIC"] = drDos["PIC"];
                    dr["CounterMeasure"] = drDos["CounterMeasure"];
                    //Parts.AcceptChanges();
                }
                //foreach (DataColumn dc in Parts.Columns)
                //{
                //}
            }
            return Parts;
        }
        public string GetBusCategory() {
            DataTable dt = new DataTable();
            string sql = string.Format("SELECT CATEGORY,CAT_NAME FROM [dbo].[PSI_TBL_M_BUS_CATEGORY]");
            DBWorker.ExecuteReader(conn,sql,dt);
            return JsonConvert.SerializeObject(dt);
        }
        public string GetModel() {
            DataTable dt = new DataTable();
            string sql = string.Format("SELECT MODEL,MODEL_NAME FROM [dbo].[PSI_TBL_M_MODEL]");
            DBWorker.ExecuteReader(conn,sql,dt);
            return JsonConvert.SerializeObject(dt);
        }
        public string GetProblemCategory() {
            DataTable dt = new DataTable();
            string sql = string.Format("SELECT PRO_CATEGORY,PROB_CAT_NAME,PIC FROM [dbo].[PSI_TBL_M_PROBLEM_CATEGORY]");
            DBWorker.ExecuteReader(conn,sql,dt);
            return JsonConvert.SerializeObject(dt);
        }
        public string SaveData(List<ObjectDOS> DOS) {
            Handler.SharedInfo msg = new Handler.SharedInfo();

            msg.SetMessage("Save DOS");
            int ctr = 0;
            int ctrdet = 0;
            foreach (var item in DOS)
            {
                ctr++;
                ctrdet = 0;
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("@PlantID", item.PlantID);
                dic.Add("@Category", item.Category);
                dic.Add("@Cat_Name", item.Cat_Name);
                dic.Add("@ModelName", item.ModelName);
                dic.Add("@MaterialCode", item.MaterialCode);
                dic.Add("@Description", item.Description);
                dic.Add("@SupplierCode", item.SupplierCode);
                dic.Add("@EPPIStock", item.EPPIStock);
                dic.Add("@EPPI_DOS", item.EPPI_DOS);
                dic.Add("@DOS_Level", item.DOS_Level);
                dic.Add("@Supplier_Stock", item.Supplier_Stock);
                dic.Add("@DOS_Stock", item.DOS_Stock);
                dic.Add("@Total_Stock", item.Total_Stock);
                dic.Add("@Problem_Cat", item.Problem_Cat);
                dic.Add("@Reason", item.Reason);
                dic.Add("@PIC", item.PIC);
                dic.Add("@CounterMeasure", item.CounterMeasure);
                DataTable dt = new DataTable();
                DBWorker.ExecuteSP(conn, dt, "PSI_SaveDOS", dic);
                //foreach (var det in item.Det)
                //{
                //    ctrdet++;
                //    msg.SetMessage(string.Format("Processing [{0}/{1}][{2}/{3}] : Save DOS",ctr,DOS.Count,ctrdet,item.Det.Count));
                //    Dictionary<string, string> dicDet = new Dictionary<string, string>();
                //    dicDet.Add("@PlantID", det.PlantID);
                //    dicDet.Add("@MaterialCode", det.MaterialCode);
                //    dicDet.Add("@SupplierCode", det.SupplierCode);
                //    dicDet.Add("@DateInput", det.DateInput.ToString("yyyy-MM-dd HH:mm:ss"));
                //    dicDet.Add("@Value", det.Value);
                //    DataTable dtDet = new DataTable();
                //    DBWorker.ExecuteSP(conn,dtDet,"PSI_SaveDOSDet",dicDet);
                //}
            }
            msg.SetMessage("DONE...");
            return "";
        }

        public string GetDOS(string SupplierCode = "") {
            //DataTable dt = new DataTable();
            //DBWorker.ExecuteSP(conn,dt,"PSI_GetDOS");
            //return JsonConvert.SerializeObject(dt);
            string[] plant = {};
            string[] vendor = {};
            string[] material = {};
            DataTable dtParts = this.GetDataTable(plant, vendor, material,SupplierCode);
            //return JsonConvert.SerializeObject(dtParts);
            List<string> lst = new List<string>();
            foreach (DataColumn dc in dtParts.Columns)
            {
                if (ExcelReader.IsDate(dc.ColumnName.Substring(1).Replace("_","/")))
                {
                    lst.Add(dc.ColumnName);
                }
            }
            DataTable dt = new DataTable();
            DataColumn dcSupplierID = new DataColumn("SupplierID", Type.GetType("System.String"));
            DataColumn dcSupplierName = new DataColumn("SupplierName", Type.GetType("System.String"));
            DataColumn dcSCount = new DataColumn("SCount", Type.GetType("System.Int32"));
            DataColumn dcLCount = new DataColumn("LCount", Type.GetType("System.Int32"));
            dt.Columns.Add(dcSupplierID);
            dt.Columns.Add(dcSupplierName);
            dt.Columns.Add(dcSCount);
            dt.Columns.Add(dcLCount);
            foreach (DataRow dr in dtParts.Rows)
            {
                string SupplierID = dr["SupplierID"].ToString();
                string SupplierName = dr["SupplierName"].ToString();
                string MaterialNumber = dr["MaterialNumber"].ToString();
                Boolean Incremental = false;
                int LCount = 0;
                float DOSLevel = float.Parse(dr["DOS_Level"].ToString());
                int DOS_Level = (int)Math.Round(DOSLevel, 0);
                if (DOS_Level > 0)
                {
                    if (float.Parse(dr[lst[DOS_Level-1]].ToString()) > 0)
                    {
                        Incremental = true;
                    }
                }
                var drs = dt.Select(string.Format("SupplierID = '{0}'", SupplierID));
                if (drs.Length == 0)
                {
                    int SCount = dtParts.Select(string.Format("SupplierID = '{0}'", SupplierID)).Length;
                    LCount += Incremental ? 1 : 0;
                    var newRow = dt.NewRow();
                    newRow["SupplierID"] = SupplierID;
                    newRow["SupplierName"] = SupplierName;
                    newRow["SCount"] = SCount;
                    newRow["LCount"] = LCount;
                    dt.Rows.Add(newRow);
                }
                else {
                    DataRow CurrentRow = drs[0];
                    LCount = int.Parse(CurrentRow["LCount"].ToString());
                    LCount += Incremental ?1:0;
                    CurrentRow["LCount"] = LCount;
                }
            }
            return JsonConvert.SerializeObject(dt);
        }


        public string GetDOS_buyer_confirm(string SupplierCode = "")
        {
            //DataTable dt = new DataTable();
            //DBWorker.ExecuteSP(conn,dt,"PSI_GetDOS");
            //return JsonConvert.SerializeObject(dt);
            string[] plant = { };
            string[] vendor = { };
            string[] material = { };
            DataTable dtParts = this.GetDataTable_buyer_confirm(plant, vendor, material, SupplierCode);
            //return JsonConvert.SerializeObject(dtParts);
            List<string> lst = new List<string>();
            foreach (DataColumn dc in dtParts.Columns)
            {
                if (ExcelReader.IsDate(dc.ColumnName.Substring(1).Replace("_", "/")))
                {
                    lst.Add(dc.ColumnName);
                }
            }
            DataTable dt = new DataTable();
            DataColumn dcSupplierID = new DataColumn("SupplierID", Type.GetType("System.String"));
            DataColumn dcSupplierName = new DataColumn("SupplierName", Type.GetType("System.String"));
            DataColumn dcSCount = new DataColumn("SCount", Type.GetType("System.Int32"));
            DataColumn dcLCount = new DataColumn("LCount", Type.GetType("System.Int32"));
            dt.Columns.Add(dcSupplierID);
            dt.Columns.Add(dcSupplierName);
            dt.Columns.Add(dcSCount);
            dt.Columns.Add(dcLCount);
            foreach (DataRow dr in dtParts.Rows)
            {
                string SupplierID = dr["SupplierID"].ToString();
                string SupplierName = dr["SupplierName"].ToString();
                string MaterialNumber = dr["MaterialNumber"].ToString();
                Boolean Incremental = false;
                int LCount = 0;
                float DOSLevel = float.Parse(dr["DOS_Level"].ToString());
                int DOS_Level = (int)Math.Round(DOSLevel, 0);
                if (DOS_Level > 0)
                {
                    if (float.Parse(dr[lst[DOS_Level - 1]].ToString()) > 0)
                    {
                        Incremental = true;
                    }
                }
                var drs = dt.Select(string.Format("SupplierID = '{0}'", SupplierID));
                if (drs.Length == 0)
                {
                    int SCount = dtParts.Select(string.Format("SupplierID = '{0}'", SupplierID)).Length;
                    LCount += Incremental ? 1 : 0;
                    var newRow = dt.NewRow();
                    newRow["SupplierID"] = SupplierID;
                    newRow["SupplierName"] = SupplierName;
                    newRow["SCount"] = SCount;
                    newRow["LCount"] = LCount;
                    dt.Rows.Add(newRow);
                }
                else
                {
                    DataRow CurrentRow = drs[0];
                    LCount = int.Parse(CurrentRow["LCount"].ToString());
                    LCount += Incremental ? 1 : 0;
                    CurrentRow["LCount"] = LCount;
                }
            }
            return JsonConvert.SerializeObject(dt);
        }

        public string GetMonthly(string date,string SupplierCode = "") {
            DataTable dt = new DataTable();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@DateFrom", date);
            dic.Add("@DateTo", "");
            dic.Add("@SupplierCode", SupplierCode);
            DBWorker.ExecuteSP(conn, dt, "PSI_DOSMonthly", dic);
            return JsonConvert.SerializeObject(dt);
        }
        //-----need to update this methods

        public string GetNoDOSMaterial(string SupplierCode) {
            DataTable dt = new DataTable();
            return JsonConvert.SerializeObject(dt);
        }
        public string GetNoDOSMaterialCount(string SupplierCode) {
            DataTable dt = new DataTable();
            return JsonConvert.SerializeObject(dt);
        }
        public string SendEmailToBuyer(string SupplierID = "",string PIC = "")
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@SupplierID",SupplierID);
            dic.Add("@PIC",PIC);
            DataTable dt = new DataTable();
            DBWorker.ExecuteSP(conn, dt, "PSI_SEND_EMAIL_BUYER",dic);
            return "{}";
        }
    }
}