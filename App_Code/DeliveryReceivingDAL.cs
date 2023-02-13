using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlTypes;


    public class DeliveryReceivingDAL
    {
        private string strConn;
        SqlConnection conn;
        private string strConnLogin;
        SqlConnection connLogin;

        public DeliveryReceivingDAL()
        {
            this.strConn = System.Configuration.ConfigurationManager.AppSettings["FGWHSE_ConnectionString"];
            conn = new SqlConnection(strConn);

            this.strConnLogin = System.Configuration.ConfigurationManager.AppSettings["connLogin"];
            connLogin = new SqlConnection(strConnLogin);

        }

      
        public DataSet WH_GetLoadingDock()
        {

            SqlCommand cmd = new SqlCommand("WH_GETLOADINGDOCK", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add(new SqlParameter("@LocationTypeID", SqlDbType.NVarChar)).Value = LocationTypeID;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                da.Fill(ds);
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
    }
