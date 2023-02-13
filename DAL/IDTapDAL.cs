using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace FGWHSEClient.DAL
{
    
    public class IDTapDAL
    {
        public static string CONNECTION_STRING_IDTAP = ConfigurationSettings.AppSettings["connID"];

        protected SqlConnection conn = new SqlConnection(IDTapDAL.CONNECTION_STRING_IDTAP);
        protected SqlCommand cmd = new SqlCommand();
        protected SqlDataAdapter da = new SqlDataAdapter();

        public DataTable GET_ID_TAP(string strcardserialno)
        {


            conn.Open();
            DataTable dt = new DataTable();

            string strQuery = "SELECT [EmployeeID] ,[EmployeeNo] ,[CardNo1] ,[CardNo2] ,[CardSerialNo] ,[APOAccount] FROM[vw_UserMaster_CardSerialNo] with(nolock) where cardserialno = '"+ strcardserialno + "'";

            using (var command = new SqlCommand(strQuery, conn))
            {
                // Loads the query results into the table
                dt.Load(command.ExecuteReader());
            }

            conn.Close();
            return dt;
        }



    }
}