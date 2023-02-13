using FGWHSEClient.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FGWHSEClient.Form
{
    public partial class PSIPartsSimulation : System.Web.UI.Page
    {
        static string UserID;
        static string SupplierCode;
        public string strPageSubsystem = "";
        public string strAccessLevel = "";

        private static CtrlPartsSimulation MyControl;
        static string TableName = "ShortageList";//this will be the name of table in database
        //static DataTable ListMaterial;
        static DataTable CurrentData;
        static DataTable DTSimulation;
        static SqlConnection MyConnection;
        static DateTime min;
        static DateTime max;
        protected void Page_Load(object sender, EventArgs e)
        {
            max = DateTime.Now;
            CurrentData = new DataTable();
            MyControl = new CtrlPartsSimulation();
            MyConnection = MyControl.GetConnection();
            //MyControl.LoadData();
            DTSimulation = new DataTable();
            //CurrentData = new DataTable();
            //ListMaterial = new DataTable();
            //MyConnection = new Connection().SetConnectionSettings(ConfigurationManager.AppSettings["connLogin"]);
            //min = new DateTime();
            //max = new DateTime();
            //OtherFunctions.PartsSimulation_SetMinMaxDate(ref min, ref max);
            ////LoadSimulation();
            //DTSimulation = OtherFunctions.PartsSimulation_LoadSimulation();
            ////LoadData();
            //CurrentData = OtherFunctions.PartsSimulation_GetData(min, max);
            ////GenerateTable(CurrentData, min, max);
            //ListMaterial = OtherFunctions.PartsSimulation_GetMaterial();
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
                    strPageSubsystem = "FGWHSE_041";
                    if (!checkAuthority(strPageSubsystem) && Session["UserName"].ToString() != "GUEST")
                    {
                        Response.Write("<script>");
                        Response.Write("alert('You are not authorized to access the page.');");
                        Response.Write("window.location = 'Default.aspx';");
                        Response.Write("</script>");
                    }
                }
                UserID = Session["UserID"].ToString();
                SupplierCode = new OtherFunctions().GetVendorBasedOnUser(UserID);
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
        /// <summary>
        /// this will initialize the data from the database
        /// </summary>
        private void LoadData() {
            CurrentData = new DataTable();
            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["FGWHSE_ConnectionString"]);
            //List<string> dates = new List<string>();
            
            string[] dates = new string[Int32.Parse(((max - min).TotalDays + 1).ToString())];
            int index = 0;

            while (min <= max)
            {
                Console.WriteLine(string.Format("{0} = {1} < {2},{3}", min < max, min, max, index));
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
                "", TableName);
            SqlCommand cmd = new SqlCommand(sql, connection)
            {
                CommandType = CommandType.Text
            };
            connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(CurrentData);
            connection.Close();
        }
        private void LoadSimulation() {
            string sql = string.Format("SELECT * FROM dbo.PSIPartsSimulation");
            //DTSimulation = new DataTable();
            //new Connection().ExecuteReader(MyConnection, sql, DTSimulation);
        }
        /// <summary>
        /// This will search the columns from the table of shortage list
        /// </summary>
        /// <param name="min">The first day of shortage list</param>
        /// <param name="max">and the last day</param>
        private void GetMinMaxDate(ref DateTime min,ref DateTime max) {
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["FGWHSE_ConnectionString"]);
            string sql = string.Format("SELECT TOP 1 * FROM {0}",TableName);
            DataTable dt = new DataTable();
            new Connection().ExecuteReader(conn,sql,dt);
            foreach (DataColumn dc in dt.Columns)
            {
                string col = dc.ColumnName.ToString().Substring(1).Replace("_","/");
                if (ExcelReader.IsDate(col))
                {
                    min = DateTime.Parse(col);
                    break;
                }
            }
            DataColumn lastColumn = dt.Columns[dt.Columns.Count - 1];
            max = DateTime.Parse(lastColumn.ColumnName.ToString().Substring(1).Replace("_", "/"));
        }
        private void GenerateTable(DataTable dt,DateTime min,DateTime max)
        {
            DataTable clone = dt.Clone();
            clone.Clear();


            HtmlTableRow row = new HtmlTableRow();
            foreach (DataColumn dc in dt.Columns)
            {
                string colName = dc.ColumnName;
                HtmlTableCell cell = new HtmlTableCell();
                if (ExcelReader.IsDate(colName.Substring(1).Replace("_", "/")))
                {
                    colName = colName.Substring(1).Replace("_", "/");
                    colName = DateTime.Parse(colName).ToString("MM/dd/yyyy");
                }
                cell.InnerHtml = "<b>" + colName + "</b>";
                cell.Attributes.Add("Title",colName);
                row.Cells.Add(cell);
            }
            //table_excel.Rows.Add(row);
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < 3; i++)
                {
                    DataRow newRow = clone.NewRow();
                    string prevColumn = "";
                    foreach (DataColumn dc in dt.Columns)
                    {
                        string colName = dc.ColumnName;
                        newRow[colName] = dr[colName];
                        if (colName == "PlanLogical")
                        {
                            switch (i)
                            {
                                case 0:
                                    newRow[dc.ColumnName] = "EPPI PLAN";
                                    break;
                                case 1:
                                    newRow[dc.ColumnName] = "SUPPLIER PLAN/DELIVERY";
                                    break;
                                default:
                                    newRow[dc.ColumnName] = "END STOCKS";
                                    break;
                            }
                        }
                        if (ExcelReader.IsDate(colName.Substring(1).Replace("_","/")))
                        {
                            string query = string.Format("MaterialNumber = '{0}' AND DateInput = '{1}' ", newRow["MaterialNumber"], colName);
                            //DTSimulation = new DataTable();
                            //DTSimulation = OtherFunctions.PartsSimulation_LoadSimulation(query);
                            var selectedSimulation = DTSimulation.Select(query);
                            /*
                             * past_due = total + input - past
                             * monthly = past + input - month
                             */
                            var simulatedValue = 0;
                            if (selectedSimulation.Length > 0)
                            {
                                simulatedValue = Int32.Parse(selectedSimulation[0]["Value"].ToString());
                            }
                            if (i == 1)
                            {
                                newRow[colName] = simulatedValue;
                            }
                            if (i == 2)
                            {
                                newRow[colName] = Int32.Parse(newRow[prevColumn].ToString()) + simulatedValue - Int32.Parse(newRow[colName].ToString());
                            }
                        }else if (colName == "PastDue")
                        {
                            string query = string.Format("MaterialNumber = '{0}' AND DateInput = '{1}' ", newRow["MaterialNumber"], colName);
                            //DTSimulation = new DataTable();
                            //DTSimulation = OtherFunctions.PartsSimulation_LoadSimulation(query);
                            var selectedSimulation = DTSimulation.Select(query);
                            var simulatedValue = 0;
                            if (selectedSimulation.Length > 0)
                            {
                                simulatedValue = Int32.Parse(selectedSimulation[0]["Value"].ToString());
                            }
                            if (i == 1)
                            {
                                newRow[colName] = simulatedValue;
                            }
                            if (i == 2)
                            {
                                newRow[colName] = Int32.Parse(newRow["TotalSTCK"].ToString()) + simulatedValue - Int32.Parse(newRow[colName].ToString());
                            }
                        }
                        prevColumn = colName;
                    }
                    clone.Rows.Add(newRow);
                }
            }
            for (int i = 0; i < clone.Rows.Count; i++)
            {
                row = new HtmlTableRow();
                for (int j = 0; j < clone.Columns.Count; j++)
                {
                    string colName = clone.Columns[j].ColumnName;
                    HtmlTableCell cell = new HtmlTableCell
                    {
                        InnerText = clone.Rows[i][j].ToString()
                    };
                    cell.Attributes.Add("Title", clone.Rows[i][j].ToString());
                    cell.Attributes.Add("data-value", clone.Rows[i][j].ToString());
                    cell.Attributes.Add("data-field", colName);
                    if (clone.Rows[i]["PlanLogical"].ToString() == "SUPPLIER PLAN/DELIVERY")
                    {
                        if (colName == "PastDue" || ExcelReader.IsDate(colName.Substring(1).Replace("_", "/")))
                        {
                            TextBox txt = new TextBox
                            {
                                Text = clone.Rows[i][j].ToString(),
                                ID = string.Format("txt_{0}_{1}", clone.Rows[i]["MaterialNumber"].ToString(), colName),
                                CssClass = "AutoComputeField"
                            };
                            if (colName == "PastDue")
                            {
                                txt.CssClass = "AutoComputePastDue";
                            }
                            cell.InnerText = "";
                            cell.Controls.Add(txt);
                        } else {
                            cell.Attributes.Add("tabindex", "0");
                        }
                        if (colName == "MaterialNumber")
                        {
                            cell.InnerText = "";
                            cell.InnerHtml = "<span class=\"ControlMaterialNumber\">"+ clone.Rows[i][j].ToString() + "</span>";
                        }
                    }
                    else
                    {
                        cell.Attributes.Add("tabindex", "0");
                    }
                    row.Cells.Add(cell);
                }
                //table_excel.Rows.Add(row);
            }
        }
        /// <summary>
        /// use this method to retreive data in xml format
        /// [note] bigger data would give an error in json maximum number of character or out of memory problem
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string GetVendor() {
            return MyControl.GetSupplier(SupplierCode);
        }
        [WebMethod]
        public static string GetMaterial() {
            return MyControl.GetMaterial();
        }
        [WebMethod]
        public static string GetPlants() {
            return new CtrlShortageList().GetPlants();
        }
        [WebMethod]
        public static string GetData(string PlantID,string[] VendorCode,string[] MaterialNumber,Boolean Formulated = false) {
            string[] p = { PlantID };
            string m = "",v = "";
            foreach (string item in MaterialNumber)
            {
                m += string.Format(",'{0}'",item);
            }
            foreach (string item in VendorCode)
            {
                v += string.Format(",'{0}'", item);
            }
            string filter = "";
            if (m != "")
            {
                filter = string.Format("a.MaterialNumber IN ({0})",m.Substring(1));
                if (v != "")
                {
                    filter += string.Format(" AND b.MainVendor IN ({0})",v.Substring(1));
                }
            }
            else
            {
                if (v != "")
                {
                    filter = string.Format("b.MainVendor IN ({0})", v.Substring(1));
                }
            }
            if (filter != "")
            {
                DTSimulation = OtherFunctions.PartsSimulation_LoadSimulation(filter);
            }
            else
            {
                DTSimulation = OtherFunctions.PartsSimulation_LoadSimulation("");
            }
            MyControl.LoadData(p, VendorCode, MaterialNumber, Formulated, SupplierCode);
            //return "[]";
            return MyControl.GetData();
        }
        [WebMethod]
        public static string GetSimulation() {
            return JsonConvert.SerializeObject(DTSimulation);
        }
        [WebMethod]
        public static string SaveData(List<ObjectPartsSimulation> obj) {
            return MyControl.SaveData(obj);
            //MyConnection.Open();
            //SqlTransaction transaction;
            //transaction = MyConnection.BeginTransaction();
            //try
            //{
            //    foreach (ObjectPartsSimulation item in obj)
            //    {
            //        SqlCommand command = new SqlCommand("PSIPartsSimulationSave", MyConnection)
            //        {
            //            Transaction = transaction,
            //            CommandType = CommandType.StoredProcedure
            //        };
            //        command.Parameters.AddWithValue("@PlantID", item.PlantID);
            //        command.Parameters.AddWithValue("@MaterialNumber", item.MaterialNumber);
            //        command.Parameters.AddWithValue("@DateInput", item.DateInput);
            //        command.Parameters.AddWithValue("@VALUE", item.Value);
            //        command.ExecuteNonQuery();
            //    }
            //    transaction.Commit();
            //    MyConnection.Close();
            //    return ("Both records are written to database.");
            //}
            //catch (Exception ex)
            //{
            //    MyConnection.Close();
            //    try
            //    {
            //        transaction.Rollback();
            //    }
            //    catch (Exception ex2)
            //    {
            //        MyConnection.Close();
            //        return ex2.Message;
            //    }
            //    return ex.Message;
            //}
            //return JsonConvert.SerializeObject(obj);
        }
    }
}