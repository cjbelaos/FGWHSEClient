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
using System.Data.SqlClient;


namespace FGWHSEClient.DAL
{
    public class ExcessDAL : BaseDAL
    {

        public DataSet GET_EXCESS_MONITORING_DETAILS(string strLOCATIONID)
        {

            SqlCommand cmd = new SqlCommand("GET_EXCESS_MONITORING_DETAILS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@LOCATIONID", SqlDbType.NVarChar)).Value = strLOCATIONID;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                da.Fill(ds);
            }
            catch (SqlException sqlex)
            {
                //throw sqlex;
                sqlex.Message.ToString();
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }


        public DataSet DN_GETLOADINGDOCK_EXCESS()
        {

            SqlCommand cmd = new SqlCommand("DN_GETLOADINGDOCK_EXCESS", conn);
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


        public DataSet GET_EXCESS_LOCATION_READ_STATUS(string strLOCATIONID)
        {

            SqlCommand cmd = new SqlCommand("GET_EXCESS_LOCATION_READ_STATUS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@LOCATIONID", SqlDbType.NVarChar)).Value = strLOCATIONID;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                da.Fill(ds);
            }
            catch (SqlException sqlex)
            {
                //throw sqlex;
                sqlex.Message.ToString();
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }


        public DataSet UPDATE_ANTENNAREAD_FLAG(string strLOCATIONID, string strFLAGVALUE, string strUserID)
        {

            SqlCommand cmd = new SqlCommand("UPDATE_ANTENNAREAD_FLAG", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@LOCATIONID", SqlDbType.NVarChar)).Value = strLOCATIONID;
            cmd.Parameters.Add(new SqlParameter("@FLAGVALUE", SqlDbType.NVarChar)).Value = strFLAGVALUE;
            cmd.Parameters.Add(new SqlParameter("@USERID", SqlDbType.NVarChar)).Value = strUserID;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                da.Fill(ds);
            }
            catch (SqlException sqlex)
            {
                //throw sqlex;
                sqlex.Message.ToString();
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
    }
}