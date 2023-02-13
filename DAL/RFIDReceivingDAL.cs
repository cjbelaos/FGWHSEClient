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
    public class RFIDReceivingDAL : BaseDAL
    {
        public RFIDReceivingDAL()
        {

        }

        public DataSet GET_NOTRECEIVEDRFID(string strDNNo)
        {
            //SqlCommand cmd = new SqlCommand("GET_NOTRECEIVEDRFID", conn);
            SqlCommand cmd = new SqlCommand("GET_NOTRECEIVEDRFID_2", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar, 16)).Value = strDNNo;

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

        public string ADD_RFIDRECEIVE(string strDNNo, string strRFIDTag, string strCreatedBy)
        {
            int i = 0;
            string strMsg = "";
            //conn.Open();

            SqlCommand cmd = new SqlCommand("ADD_RFIDRECEIVE", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlTransaction transaction = null;

            cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar, 16)).Value = strDNNo;
            cmd.Parameters.Add(new SqlParameter("@RFIDTag", SqlDbType.NVarChar, 30)).Value = strRFIDTag;
            cmd.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.VarChar, 20)).Value = strCreatedBy;

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

        public DataSet RFIDTAG_INQUIRY(string strDate1, string strDate2, string strRFIDTag)
        {
            SqlCommand cmd = new SqlCommand("RFIDTAG_INQUIRY", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@DATE1", SqlDbType.NVarChar, 20)).Value = strDate1;
            cmd.Parameters.Add(new SqlParameter("@DATE2", SqlDbType.NVarChar, 20)).Value = strDate2;
            cmd.Parameters.Add(new SqlParameter("@RFIDTAG", SqlDbType.NVarChar, 50)).Value = strRFIDTag;

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
