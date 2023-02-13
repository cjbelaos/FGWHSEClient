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
    public class DNReceivingScreenDAL : BaseDAL
    {
        public DataSet DN_DisplayReceivingMonitoringScreen(string loadingDock)
        {
            SqlCommand cmd = new SqlCommand("DN_DISPLAY_RECEIVING_MONITORING_SCREEN", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            // cmd.Parameters.Add(loadingDock,SqlDbType.Text);
            cmd.Parameters.Add(new SqlParameter("@LoadingDock", SqlDbType.NVarChar)).Value = loadingDock;
            cmd.CommandTimeout = 360000;
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

        public DataSet DN_DisplayTableReceiving(string dnNo, string itemCode)
        {
            SqlCommand cmd = new SqlCommand("DN_DISPLAY_TABLE_RECEIVING", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // cmd.Parameters.Add(loadingDock,SqlDbType.Text);
            cmd.Parameters.Add(new SqlParameter("@DNno", SqlDbType.NVarChar)).Value = dnNo;
            cmd.Parameters.Add(new SqlParameter("@ItemCode", SqlDbType.NVarChar)).Value = itemCode;

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

        public DataSet DN_DisplayItemCode(string dnNo)
        {
            SqlCommand cmd = new SqlCommand("DN_DISPLAY_ITEMCODE", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // cmd.Parameters.Add(loadingDock,SqlDbType.Text);
            cmd.Parameters.Add(new SqlParameter("@DNno", SqlDbType.NVarChar)).Value = dnNo;

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

        public DataView DN_GetDN_Header(string dnNo)
        {
            SqlCommand cmd = new SqlCommand("DN_GETDN_HEADERDETAILS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // cmd.Parameters.Add(loadingDock,SqlDbType.Text);
            cmd.Parameters.Add(new SqlParameter("@DNNO", SqlDbType.NVarChar)).Value = dnNo;

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
            return ds.Tables[0].DefaultView;
        }

        public DataSet DN_GetDNDetails(string dnNo)
        {
            SqlCommand cmd = new SqlCommand("DN_GETDN_DETAILS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // cmd.Parameters.Add(loadingDock,SqlDbType.Text);
            cmd.Parameters.Add(new SqlParameter("@DNNO", SqlDbType.NVarChar)).Value = dnNo;

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
}