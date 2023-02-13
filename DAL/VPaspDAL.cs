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
using com.eppi.utils;

namespace FGWHSEClient.DAL
{
    public class VPaspDAL : BaseDAL
    {
        public VPaspDAL()
        {

        }

        public DataSet VP_ASP_GET_SERIAL_SCAN_MASTER(string strPRODUCTCODE, string strDATEFROM, string strDATETO, string strREQUIREDFLAG)
        {
            SqlCommand cmd = new SqlCommand("VP_ASP_GET_SERIAL_SCAN_MASTER", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@PRODUCTCODE", SqlDbType.NVarChar)).Value = strPRODUCTCODE;
            cmd.Parameters.Add(new SqlParameter("@DATEFROM", SqlDbType.NVarChar)).Value = strDATEFROM;
            cmd.Parameters.Add(new SqlParameter("@DATETO", SqlDbType.NVarChar)).Value = strDATETO;
            cmd.Parameters.Add(new SqlParameter("@REQUIREDFLAG", SqlDbType.NVarChar)).Value = strREQUIREDFLAG;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                conn.Open();
                da.Fill(ds);

            }
            catch (SqlException oe)
            {
                Logger.GetInstance().Error(oe.Message);
                Logger.GetInstance().Error(oe.StackTrace);
                throw oe;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.Message);
                Logger.GetInstance().Fatal(ex.StackTrace);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return ds;
        }

        
        public DataTable VP_ASP_GET_STATUS()
        {
            SqlCommand cmd = new SqlCommand("VP_ASP_GET_STATUS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                conn.Open();
                da.Fill(ds);

            }
            catch (SqlException oe)
            {
                Logger.GetInstance().Error(oe.Message);
                Logger.GetInstance().Error(oe.StackTrace);
                throw oe;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.Message);
                Logger.GetInstance().Fatal(ex.StackTrace);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return ds.Tables[0];
        }


        public void VP_ASP_ADD_UPDATE_SERIAL_MASTER(string strPRODUCTCODE, string strREQUIREDFLAG, string strUID)
        {
            SqlCommand cmd = new SqlCommand("VP_ASP_ADD_UPDATE_SERIAL_MASTER", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@PRODUCTCODE", SqlDbType.NVarChar)).Value = strPRODUCTCODE;
            cmd.Parameters.Add(new SqlParameter("@REQUIREDFLAG", SqlDbType.NVarChar)).Value = strREQUIREDFLAG;
            cmd.Parameters.Add(new SqlParameter("@UID", SqlDbType.NVarChar)).Value = strUID;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                conn.Open();
                da.Fill(ds);

            }
            catch (SqlException oe)
            {
                Logger.GetInstance().Error(oe.Message);
                Logger.GetInstance().Error(oe.StackTrace);
                throw oe;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.Message);
                Logger.GetInstance().Fatal(ex.StackTrace);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

        }
    }
}
