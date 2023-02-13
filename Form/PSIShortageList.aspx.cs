using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using FGWHSEClient.Classes;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

namespace FGWHSEClient.Form
{
    /// <summary>
    /// emon
    /// created : 01/28/2021
    /// </summary>
    public partial class PSIShortageList : System.Web.UI.Page
    {
        static string UserID;
        static string SupplierCode;
        public string strPageSubsystem = "";
        public string strAccessLevel = "";

        private static CtrlShortageList MyControl;
        static string TableName = "PSI_TBL_T_ShortageList_V2";//this will be the name of table in database
        static DataTable CurrentData;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Message"] = "-";
            CurrentData = new DataTable();
            MyControl = new CtrlShortageList();
            //MyControl.LoadData();

            //SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connLogin"]);
            //string sql = string.Format("SELECT * FROM dbo.{0} ORDER BY CAST(ShortageListID AS INT)", TableName);
            //SqlCommand cmd = new SqlCommand(sql, connection);
            //cmd.CommandType = CommandType.Text;
            //connection.Open();
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //da.Fill(CurrentData);
            //connection.Close();
            //GenerateTable(CurrentData);
            //CurrentData = OtherFunctions.ShortageList_GetData();
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
        //public void GETSUPPLIER(string strSUPPLIER)
        //{
        //    try
        //    {
        //        grdSupplier.DataSource = dalPSI.PSI_GET_SUPPLIER(strSUPPLIER).Tables[0];
        //        grdSupplier.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<script>");
        //        Response.Write("alert('" + ex.Message.ToString() + "');");
        //        Response.Write("</script>");
        //    }
        //}
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
        public static string GetData(string PlantID, string[] VendorCode = null)
        {
            string[] p = { PlantID };
            MyControl.LoadData(p,VendorCode,SupplierCode);
            return MyControl.GetData();
        }
        [WebMethod]
        public static string GetPlants() {
            return MyControl.GetPlants();
        }

        private void GenerateTable(DataTable dt)
        {
            HtmlTableRow row = new HtmlTableRow();
            foreach (DataColumn dc in dt.Columns)
            {
                string colName = dc.ColumnName;
                HtmlTableCell cell = new HtmlTableCell();
                if (ExcelReader.IsDate(colName.Substring(1).Replace("_", "/")))
                {
                    colName = colName.Substring(1).Replace("_","/");
                    colName = DateTime.Parse(colName).ToString("MM/dd/yyyy");
                }
                cell.InnerHtml = "<b>" + colName + "</b>";
                row.Cells.Add(cell);
            }
            //table_excel.Rows.Add(row);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = new HtmlTableRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    HtmlTableCell cell = new HtmlTableCell
                    {
                        InnerText = dt.Rows[i][j].ToString()
                    };
                    row.Cells.Add(cell);
                }
                //table_excel.Rows.Add(row);
            }
        }
        /// <summary>
        /// read the excel file which have been uploaded to the server
        /// first drop the table if exist
        /// then create the table
        /// and add the record
        /// </summary>
        /// <param name="FileName">The filename uploaded to find from the server</param>
        /// <returns>Nothing, just for checking</returns>
        [WebMethod]
        public static string SaveExcelToDatabase(string FileName) {
            Classes.Handler.SharedInfo info = new Classes.Handler.SharedInfo();
            DataTable dt = ExcelReader.GetData(HttpRuntime.AppDomainAppPath + "/Uploaded/" + FileName);
            //return MyControl.SaveData(dt);
            String cols = "";
            string ColumnNames = "";
            List<string> lst = new List<string>();
            foreach (DataColumn col in dt.Columns)
            {
                lst.Add(col.ColumnName);
                if (ExcelReader.IsDate(col.ColumnName))
                {
                    string colName = DateTime.Parse(col.ColumnName).ToString("_MM_dd_yyyy");
                    ColumnNames += "," + colName;
                    cols += string.Format(",[{0}][DECIMAL](18,2) NULL", colName);
                }
            }
            //DropTable();
            info.SetMessage("Drop the Table");
            OtherFunctions.ShortageList_DropTable();
            //CreateTable(cols);
            info.SetMessage("Create the Table");
            OtherFunctions.ShortageList_CreateTable(cols);
            //InsertData(dt, ColumnNames);
            info.SetMessage("Insert Data");
            OtherFunctions.ShortageList_InsertData(dt, ColumnNames);
            //delete stocks earlier than current date
            OtherFunctions.DeleteStockEarlierToday();
            return JsonConvert.SerializeObject(lst);
        }
        /// <summary>
        /// Adding the data to database
        /// </summary>
        /// <param name="dt">The retreived data from excel to DataTable</param>
        /// <param name="cols">column date</param>
        private static void InsertData(DataTable dt,string cols)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["FGWHSE_ConnectionString"]))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("InsertData");

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    int ShortageListID = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        ShortageListID++;
                        string vals = string.Format("'{0}',",ShortageListID);
                        vals += string.Format("'{0}',",dr["Material Number"]);
                        vals += string.Format("'{0}',",dr["Material Description"]);
                        vals += string.Format("'{0}',",dr["Main Vendor"]);
                        vals += string.Format("'{0}',",dr["Own Stock"]);
                        vals += string.Format("'{0}',",dr["Rece#/issue"]);
                        vals += string.Format("'{0}'",dr["Past"]);
                        foreach (DataColumn col in dt.Columns)
                        {
                            if (ExcelReader.IsDate(col.ColumnName))
                            {
                                string colName = DateTime.Parse(col.ColumnName).ToString("M/d/yyyy");
                                vals += string.Format(",'{0}'", dr[colName]);
                            }
                        }
                        string sql = string.Format("INSERT INTO dbo.{0}(ShortageListID,MaterialNumber,MaterialDescription,MainVendor,OwnStock,ReceIssue,Past{1}) VALUES({2})",
                            TableName,cols,vals);
                        command.CommandText = sql;
                        //command.CommandText = "Insert into Region (RegionID, RegionDescription) VALUES (100, 'Description')";
                        command.ExecuteNonQuery();
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    Console.WriteLine("Both records are written to database.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
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
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                }
            }
        }
        /// <summary>
        /// Drop the existing table
        /// </summary>
        private static void DropTable()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["FGWHSE_ConnectionString"]);
            string sql = string.Format("IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{0}' AND TABLE_SCHEMA = 'dbo') DROP TABLE dbo.{0}; ",TableName);
            SqlCommand cmd = new SqlCommand(sql, conn)
            {
                CommandType = CommandType.Text
            };
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        /// <summary>
        /// create the table where the data from excel should be save
        /// </summary>
        /// <param name="cols">column dates</param>
        private static void CreateTable(string cols)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["FGWHSE_ConnectionString"]);
            string sql = string.Format("CREATE TABLE [dbo].[{0}](" +
                    "[ShortageListID][nvarchar](MAX) NULL," +
                    "[MaterialNumber][nvarchar](MAX) NULL," +
                    "[MaterialDescription][nvarchar](MAX) NULL," +
                    "[MainVendor][nvarchar](MAX) NULL," +
                    "[OwnStock][nvarchar](MAX) NULL," +
                    "[ReceIssue][nvarchar](MAX) NULL," +
                    "[Past][nvarchar](MAX) NULL" +
                cols + ")",TableName);
            SqlCommand cmd = new SqlCommand(sql, conn)
            {
                CommandType = CommandType.Text
            };
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        /// <summary>
        /// this is to get the current data from the database
        /// </summary>
        /// <returns>json object</returns>
        [WebMethod]
        public static string GetCurrentData(string CurrentIndex) {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DataColumn col in CurrentData.Columns)
            {
                dic.Add(col.ColumnName, CurrentData.Rows[Int32.Parse(CurrentIndex)][col.ColumnName].ToString());
            }
            return JsonConvert.SerializeObject(dic);
        }
        [WebMethod]
        public static string GetVendor() {
            //return string.Format("UserID: {0} , SupplierCode : {1}",UserID,SupplierCode);
            return MyControl.GetVendor(SupplierCode);
        }
        [WebMethod]
        public static string GetMaterial() {
            return MyControl.GetMaterial();
        }
        [WebMethod]
        public static string GetMessage() {
            return MyControl.msg;
        }
        [WebMethod]
        public static string SetMessage() {
            return MyControl.msg;
        }
        [WebMethod]
        public static string SaveData(List<ObjectShortageList> data) {
            return MyControl.SaveDataObject(data);
        }
        [WebMethod]
        public static string GetCreateDate() {
            return MyControl.GetCreateDate();
        }
        [WebMethod]
        public static string SendEmailToVendor() {
            return MyControl.SendEmailToVendor();
        }
    }
}