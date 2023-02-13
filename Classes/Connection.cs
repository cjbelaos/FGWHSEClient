using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace FGWHSEClient.Classes
{
    class Connection
    {
        /// <summary>
        /// Use for query with return single data table
        /// </summary>
        /// <param name="conn">Connection</param>
        /// <param name="sql">Query</param>
        /// <param name="dt">DataTable that handles the return value</param>
        public void ExecuteReader(SqlConnection conn,string sql,DataTable dt) {
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                conn.Close();
            }
            catch (Exception ex)
            {
                new FileWriter().Write("Query Error : " + sql);
                throw ex;
            }
        }
        /// <summary>
        /// Use for Query with return multiple data consolidated in dataset
        /// </summary>
        /// <param name="conn">Connection</param>
        /// <param name="sql">Query</param>
        /// <param name="ds">DataSet that handles the return value</param>
        public void ExecuteReader(SqlConnection conn,string sql,DataSet ds) {
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(ds);
            conn.Close();
        }
        /// <summary>
        /// For Executing non return value query. Commonly used for executing Insert/Update/Delete
        /// </summary>
        /// <param name="conn">Connection</param>
        /// <param name="sql">Query</param>
        public void ExecuteNonQuery(SqlConnection conn,string sql) {
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// To Execute the stored procedure with return value as data table
        /// </summary>
        /// <param name="conn">Connection</param>
        /// <param name="dt">DataTable that handles the return value</param>
        /// <param name="sp">StoredProcedure name</param>
        /// <param name="param">Parameters to be send in procedure</param>
        public void ExecuteSP(SqlConnection conn,
            DataTable dt,
            string sp,
            Dictionary<string,string> param = null) {
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand(sp, conn);
                if (param != null)
                {
                    foreach (var item in param)
                    {
                        cmd.Parameters.AddWithValue(item.Key, item.Value);
                    }
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 360000;

                da.SelectCommand = cmd;
                da.Fill(dt);
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
                throw;
            }
        }
        /// <summary>
        /// To Execute the stored procedure with return value as data table
        /// </summary>
        /// <param name="conn">Connection</param>
        /// <param name="ds">DataSet that handles the return value</param>
        /// <param name="sp">StoredProcedure name</param>
        /// <param name="param">Parameters to be send in procedure</param>
        public void ExecuteSP(SqlConnection conn,
            DataSet ds,
            string sp,
            Dictionary<string, string> param = null)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            conn.Open();
            SqlCommand cmd = new SqlCommand(sp, conn);
            if (param != null)
            {
                foreach (var item in param)
                {
                    cmd.Parameters.AddWithValue(item.Key, item.Value);
                }
            }
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.Fill(ds);
            conn.Close();
        }
        /// <summary>
        /// It will return the database connection based on the ConnectionString received
        /// </summary>
        /// <param name="dbConnectionString">Database connection</param>
        /// <returns>SqlConnection based on ConnectionString passed</returns>
        public SqlConnection SetConnectionSettings(string dbConnectionString) {
            SqlConnection conn = new SqlConnection(dbConnectionString);
            return conn;
        }
        /// <summary>
        /// To setup the connection settings. It will capture the connection based on the configuration manager
        /// </summary>
        /// <returns>It will return the connection settings for the database</returns>
        public SqlConnection SetConnectionSettings() {
            var prop = System.Configuration.ConfigurationManager.AppSettings;
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["FGWHSE_ConnectionString"]);
            return conn;
        }
        /// <summary>
        /// To test the database connection
        /// </summary>
        /// <param name="ConnectionString">ConnectionString to test</param>
        /// <returns>True if can connect.</returns>
        public Boolean CanConnect(string ConnectionString) {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            conn.Close();
            return true;
        }
        /// <summary>
        /// Create Update Query
        /// </summary>
        /// <param name="TableName">Tablename to update</param>
        /// <param name="Param">Fields to be update</param>
        /// <param name="PK">Primary key and its value</param>
        /// <returns>Query for update</returns>
        public string CreateUpdateQuery(string TableName,Dictionary<string,string> Param,KeyValuePair<string,string> PK) {
            string sql = string.Format("UPDATE {0} SET",TableName);
            List<string> fields = new List<string>();
            foreach (var item in Param)
            {
                string val = item.Value.ToString().Trim() == "" ?"default":string.Format("'{0}'",item.Value);
                fields.Add(string.Format("{0} = {1}",item.Key,val));
            }
            sql = string.Format("{0} {1} WHERE {2} = '{3}'",sql,string.Join(",",fields), PK.Key, PK.Value);
            return sql;
        }
        /// <summary>
        /// making a string act as datatable
        /// </summary>
        /// <param name="str"></param>
        /// <param name="dt"></param>
        public void StringTODT(string str,ref DataTable dt) {
            dt = new DataTable();
            DataColumn dc = new DataColumn("col01",Type.GetType("System.String"));
            dt.Columns.Add(dc);
            DataRow dr = dt.NewRow();
            dr["col01"] = str;
            dt.Rows.Add(dr);
            dt.AcceptChanges();
        }
    }
}
