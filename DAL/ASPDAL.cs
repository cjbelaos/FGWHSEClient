using com.eppi.utils;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FGWHSEClient.DAL
{
    public class ASPDAL : BaseDAL
    {
        public ASPDAL()
        {

        }

        public DataSet GET_ASP_INQUIRY(string strITEMCODE, string strSERIAL, string strFROMDATE, string strTODATE, string strISEPPI, string strDELIVERYSLIP, string strDATETYPE)
        {
            SqlCommand cmd = new SqlCommand("GET_ASP_INQUIRY_NEW", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@ITEMCODE", SqlDbType.NVarChar)).Value = strITEMCODE;
            cmd.Parameters.Add(new SqlParameter("@SERIAL", SqlDbType.NVarChar)).Value = strSERIAL;
            cmd.Parameters.Add(new SqlParameter("@FROMDATE", SqlDbType.NVarChar)).Value = strFROMDATE;
            cmd.Parameters.Add(new SqlParameter("@TODATE", SqlDbType.NVarChar)).Value = strTODATE;
            cmd.Parameters.Add(new SqlParameter("@DELIVERYSLIP", SqlDbType.NVarChar)).Value = strDELIVERYSLIP;
            cmd.Parameters.Add(new SqlParameter("@DATETYPE", SqlDbType.NVarChar)).Value = strDATETYPE;
            cmd.Parameters.Add(new SqlParameter("@ISEPPI", SqlDbType.NVarChar)).Value = strISEPPI;

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


        public DataSet GET_ASP_ITEMCODE(string strPARTCODE)
        {
            SqlCommand cmd = new SqlCommand("GET_ASP_ITEMCODE", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@PARTCODE", SqlDbType.NVarChar)).Value = strPARTCODE;

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



        public DataSet GET_ASP_FOR_DELIVERY_LIST(string strDS)
        {
            SqlCommand cmd = new SqlCommand("GET_ASP_FOR_DELIVERY_LIST", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@DS", SqlDbType.NVarChar)).Value = strDS;

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


        public void UPDATE_ASP_DELIVER(string strDSRefNo, string strTransferSlip, string strUpdatedBy)
        {
            SqlCommand cmd = new SqlCommand("UPDATE_ASP_DELIVER", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@DSRefNo", SqlDbType.NVarChar)).Value = strDSRefNo;
            cmd.Parameters.Add(new SqlParameter("@TransferSlip", SqlDbType.NVarChar)).Value = strTransferSlip;
            cmd.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.NVarChar)).Value = strUpdatedBy;

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


        public DataSet CHECK_ASP_DS_OR_TS(string strDS, string strTS)
        {
            SqlCommand cmd = new SqlCommand("CHECK_ASP_DS_OR_TS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@DS", SqlDbType.NVarChar)).Value = strDS;
            cmd.Parameters.Add(new SqlParameter("@TS", SqlDbType.NVarChar)).Value = strTS;

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

        public DataSet GET_ASP_DS_SERIAL_LIST(string strDS)
        {
            SqlCommand cmd = new SqlCommand("GET_ASP_DS_SERIAL_LIST", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@DS", SqlDbType.NVarChar)).Value = strDS;

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



        public DataSet GET_ASP_REPORTS_DATE_FILTER()
        {
            SqlCommand cmd = new SqlCommand("GET_ASP_REPORTS_DATE_FILTER", conn);
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

            return ds;
        }
    }
}