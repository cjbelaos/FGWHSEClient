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
    public class HOLMESDAL : BaseDAL
    {
        public int UpdatePartsReturn(string strREFNO, int inLotID, string strReason, string strUpdateBy, string strReprint)
        {
            int i = 0;

            SqlCommand cmd = new SqlCommand("HOLMES_UPDATE_PARTSRETURN", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlTransaction transaction = null;

            cmd.Parameters.Add(new SqlParameter("@refno", SqlDbType.NVarChar, 30)).Value = strREFNO;
            cmd.Parameters.Add(new SqlParameter("@lotid", SqlDbType.Int)).Value = inLotID;
            cmd.Parameters.Add(new SqlParameter("@reason", SqlDbType.NVarChar, 100)).Value = strReason;
            cmd.Parameters.Add(new SqlParameter("@updateby", SqlDbType.NVarChar, 50)).Value = strUpdateBy;
            cmd.Parameters.Add(new SqlParameter("@reprint", SqlDbType.NVarChar, 10)).Value = strReprint;

            try
            {
                conn.Open();
                transaction = conn.BeginTransaction();
                cmd.Transaction = transaction;
                i = cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (SqlException se)
            {
                transaction.Rollback();
                if (se.Number == 2627)
                {
                    throw new ApplicationException("The record you are saving was already been saved or marked as deleted");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return i;
        }


        public string UpdateDelivery(DataTable lot_list, string strUpdateBy)
        {
            int i = 0;
            string errmessage = "";

            SqlCommand cmd = new SqlCommand("EWHS_UPDATE_DELIVERY", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlTransaction transaction = null;

            cmd.Parameters.Add(new SqlParameter("@lot_list", SqlDbType.Structured)).Value = lot_list;
            cmd.Parameters.Add(new SqlParameter("@updateby", SqlDbType.NVarChar, 50)).Value = strUpdateBy;
            cmd.Parameters.Add("@return_message", SqlDbType.NVarChar, 200);
            cmd.Parameters["@return_message"].Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                transaction = conn.BeginTransaction();
                cmd.Transaction = transaction;
                i = cmd.ExecuteNonQuery();
                transaction.Commit();

                errmessage = cmd.Parameters["@return_message"].Value.ToString();

            }
            catch (SqlException se)
            {
                //transaction.Rollback();

                if (se.Number == 2627)
                {
                    throw new ApplicationException("The record you are saving was already been saved or marked as deleted");
                }

                errmessage = se.Message;
            }
            catch (Exception ex)
            {
                //transaction.Rollback();

                errmessage = ex.Message;
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return errmessage;
        }


    }
}