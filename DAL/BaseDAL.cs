using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
namespace FGWHSEClient.DAL
{
    public class BaseDAL
    {
        public static string CONNECTION_STRING_FGWHSE = ConfigurationSettings.AppSettings["FGWHSE_ConnectionString"];

        protected SqlConnection conn = new SqlConnection(BaseDAL.CONNECTION_STRING_FGWHSE);

       
        protected SqlCommand cmd = new SqlCommand();
        protected SqlDataAdapter da = new SqlDataAdapter();

        public BaseDAL()
        {
            //
            // TODO: Add constructor logic here
            //
            //this.strConn = System.Configuration.ConfigurationManager.AppSettings["FGWHSE_ConnectionString"];
            //conn = new SqlConnection(strConn);

        }
    }
}
