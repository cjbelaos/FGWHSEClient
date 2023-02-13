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
    public class DeleteDNDAL : BaseDAL
    {
        public DeleteDNDAL()
        {

        }

        public DataSet GET_DNSUMMARY(string strDNNo)
        {
            SqlCommand cmd = new SqlCommand("GET_DNSUMMARY", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@BARCODEDNNO", SqlDbType.NVarChar, 20)).Value = strDNNo;

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

        public string DN_UPDATE_DELETEDDN(string strDNNo, string strUser)
        {
            int i = 0;
            string strMsg = "";
            //conn.Open();

            SqlCommand cmd = new SqlCommand("DN_UPDATE_DELETEDDN", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlTransaction transaction = null;

            cmd.Parameters.Add(new SqlParameter("@BARCODEDNNO", SqlDbType.NVarChar, 20)).Value = strDNNo;
            cmd.Parameters.Add(new SqlParameter("@USER", SqlDbType.NVarChar, 20)).Value = strUser;

            try
            {
                conn.Open();
                transaction = conn.BeginTransaction();
                cmd.Transaction = transaction;
                i = cmd.ExecuteNonQuery();
                transaction.Commit();
                strMsg = "SUCCESSFULLY SAVED!";
            }
            catch (SqlException se)
            {
                strMsg = se.Message.ToString();
                transaction.Rollback();
                if (se.Number == 2627)
                {
                    throw new ApplicationException("The record you are saving was already been saved or marked as deleted");
                }

            }
            catch (Exception ex)
            {
                strMsg = ex.Message.ToString();
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return strMsg;
        }

        public string DN_CHANGE_DN_FUNCTION(string strNewDN, string strOldDN, string strUser)
        {
            int i = 0;
            string strMsg = "";
            //conn.Open();

            SqlCommand cmd = new SqlCommand("DN_CHANGE_DN_FUNCTION", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlTransaction transaction = null;

            cmd.Parameters.Add(new SqlParameter("@NEWDN", SqlDbType.NVarChar, 20)).Value = strNewDN;
            cmd.Parameters.Add(new SqlParameter("@OLDDN", SqlDbType.NVarChar, 20)).Value = strOldDN;
            cmd.Parameters.Add(new SqlParameter("@USER", SqlDbType.NVarChar, 20)).Value = strUser;

            try
            {
                conn.Open();
                transaction = conn.BeginTransaction();
                cmd.Transaction = transaction;
                i = cmd.ExecuteNonQuery();
                transaction.Commit();
                strMsg = "SUCCESSFULLY SAVED!";
            }
            catch (SqlException se)
            {
                strMsg = se.Message.ToString();
                transaction.Rollback();
                if (se.Number == 2627)
                {
                    throw new ApplicationException("The record you are saving was already been saved or marked as deleted");
                }

            }
            catch (Exception ex)
            {
                strMsg = ex.Message.ToString();
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return strMsg;
        }

        public DataSet CHECK_DN_IF_EXISTS(string strNewDN)
        {
            SqlCommand cmd = new SqlCommand("CHECK_DN_IF_EXISTS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@NEWDN", SqlDbType.NVarChar, 20)).Value = strNewDN;

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

        public string DN_UPDATE_DNNO_CHANGELOG_2(string strOldDN, string strNewDN, string strReason, string strUser)
        {
            int i = 0;
            string strMsg = "";
            //conn.Open();

            SqlCommand cmd = new SqlCommand("DN_UPDATE_DNNO_CHANGELOG_2", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlTransaction transaction = null;

            cmd.Parameters.Add(new SqlParameter("@OLDDNNO", SqlDbType.NVarChar, 20)).Value = strOldDN;
            cmd.Parameters.Add(new SqlParameter("@NEWDNNO", SqlDbType.NVarChar, 20)).Value = strNewDN;
            cmd.Parameters.Add(new SqlParameter("@REASON", SqlDbType.NVarChar, 150)).Value = strReason;
            cmd.Parameters.Add(new SqlParameter("@USER", SqlDbType.NVarChar, 20)).Value = strUser;

            try
            {
                conn.Open();
                transaction = conn.BeginTransaction();
                cmd.Transaction = transaction;
                i = cmd.ExecuteNonQuery();
                transaction.Commit();
                strMsg = "SUCCESSFULLY SAVED!";
            }
            catch (SqlException se)
            {
                strMsg = se.Message.ToString();
                transaction.Rollback();
                if (se.Number == 2627)
                {
                    throw new ApplicationException("The record you are saving was already been saved or marked as deleted");
                }

            }
            catch (Exception ex)
            {
                strMsg = ex.Message.ToString();
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return strMsg;
        }


        public DataSet CHECK_RFID_NOT_RECEIVED(string strDN)
        {
            SqlCommand cmd = new SqlCommand("CHECK_RFID_NOT_RECEIVED", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@BARCODEDNNO", SqlDbType.NVarChar, 20)).Value = strDN;

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

        public string UPDATE_DELETEFLAG(string strDN, string strRFID)
        {
            int i = 0;
            string strMsg = "";
            //conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE_DELETEFLAG", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlTransaction transaction = null;

            cmd.Parameters.Add(new SqlParameter("@BARCODEDNNO", SqlDbType.NVarChar, 20)).Value = strDN;
            cmd.Parameters.Add(new SqlParameter("@RFIDTAG", SqlDbType.NVarChar, 30)).Value = strRFID;

            try
            {
                conn.Open();
                transaction = conn.BeginTransaction();
                cmd.Transaction = transaction;
                i = cmd.ExecuteNonQuery();
                transaction.Commit();
                strMsg = "SUCCESSFULLY SAVED!";
            }
            catch (SqlException se)
            {
                strMsg = se.Message.ToString();
                transaction.Rollback();
                if (se.Number == 2627)
                {
                    throw new ApplicationException("The record you are saving was already been saved or marked as deleted");
                }

            }
            catch (Exception ex)
            {
                strMsg = ex.Message.ToString();
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return strMsg;
        }
        
    }

    
}
