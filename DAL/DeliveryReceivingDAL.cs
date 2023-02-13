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
     public class DeliveryReceivingDAL : BaseDAL
    {
        //private string strConn;
        //SqlConnection conn;
        //private string strConnLogin;
        //SqlConnection connLogin;

        //public DeliveryReceivingDAL()
        //{
        //    this.strConn = System.Configuration.ConfigurationManager.AppSettings["FGWHSE_ConnectionString"];
        //    conn = new SqlConnection(strConn);

        //    this.strConnLogin = System.Configuration.ConfigurationManager.AppSettings["connLogin"];
        //    connLogin = new SqlConnection(strConnLogin);

        //}


        public DataSet DN_GetLoadingDock()
        {

            SqlCommand cmd = new SqlCommand("DN_GETLOADINGDOCK", conn);
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
        public DataSet DN_GetSupplier()
        {

            SqlCommand cmd = new SqlCommand("DN_GETSUPPLIER", conn);
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
        public DataSet DN_GetDNReceiveStatus()
        {

            SqlCommand cmd = new SqlCommand("DN_GETDNRECEIVESTATUS", conn);
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

        public DataView DN_GetDNReceivingExecuteScreen(string strDNNo,
                                                        string strDateFrom,
                                                        string strDateTo,
                                                        string strSupplierID,
                                                        string strStatusID)
        {
            //return SqlHelper.ExecuteDataset(this.conn, "DN_GETDNRECEIVE_EXECUTE", strDNNo,
            //                                                                    strDateFrom,
            //                                                                    strDateTo,
            //                                                                    strSupplierID,
            //                                                                    strStatusID).Tables[0].DefaultView;
            SqlCommand cmd = new SqlCommand("DN_GETDNRECEIVE_EXECUTE", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@DNNO", SqlDbType.VarChar)).Value = strDNNo;
            cmd.Parameters.Add(new SqlParameter("@DNDateFrom", SqlDbType.VarChar)).Value = strDateFrom;
            cmd.Parameters.Add(new SqlParameter("@DNDateTo", SqlDbType.VarChar)).Value = strDateTo;
            cmd.Parameters.Add(new SqlParameter("@supplierid", SqlDbType.VarChar)).Value = strSupplierID;
            cmd.Parameters.Add(new SqlParameter("@statusid", SqlDbType.VarChar)).Value = strStatusID;
            cmd.CommandTimeout = 0;
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

        //public DataView DN_GetDHeaderDetails(string strDNNo)
        //{
        //    return SqlHelper.ExecuteDataset(this.conn, "DN_GETDN_HEADERDETAILS", strDNNo).Tables[0].DefaultView;
        //}

        public DataSet DN_GetDNDetails(string strDNNO)
        {

            //SqlCommand cmd = new SqlCommand("DN_GETDN_DETAILS", conn); //BY TIN 3/3/2021
            SqlCommand cmd = new SqlCommand("DN_GETDN_DETAILS_2", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@DNNO", SqlDbType.VarChar)).Value = strDNNO;

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

        public DataView DN_GetDN_Header(string strDNNO)
        {

            //SqlCommand cmd = new SqlCommand("DN_GETDN_HEADERDETAILS", conn); //BY TIN 3/4/2021
            SqlCommand cmd = new SqlCommand("DN_GETDN_HEADERDETAILS_2", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@DNNO", SqlDbType.VarChar)).Value = strDNNO;

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
        public DataSet DN_GetNewDNNO(string strDNNo)
        {

            SqlCommand cmd = new SqlCommand("DN_GET_NEWDNNO", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@DNNO", SqlDbType.NVarChar)).Value = strDNNo;

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

        public int DN_UpdateDNNo(string strDNNo, string strPartCode, int iQty, string strOldDNNo)
        {
            try
            {
                //object oAdd = SqlHelper.ExecuteScalar(this.strConn, "DN_UPDATE_DNNO", strDNNo, strPartCode, iQty);
                //return 1;
                int i = 0;
                //conn.Open();

                SqlCommand cmd = new SqlCommand("DN_UPDATE_DNNO_REV1", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlTransaction transaction = null;

                cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar, 16)).Value = strDNNo;
                cmd.Parameters.Add(new SqlParameter("@ITEMCODE", SqlDbType.NVarChar, 20)).Value = strPartCode;
                cmd.Parameters.Add(new SqlParameter("@QTY", SqlDbType.Int)).Value = iQty;
                cmd.Parameters.Add(new SqlParameter("@OldDNNo", SqlDbType.NVarChar, 16)).Value = strOldDNNo;
               
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
            catch (SqlException ex)
            {
                return ex.Number;
            }
        }
        public int DN_UpdateDNNo_ChangeLog(string strOLDDnNo, string strNewDnNo, string strPersonIC, string strReason)
        {
            try
            {
                //object oAdd = SqlHelper.ExecuteScalar(this.strConn, "DN_UPDATE_DNNO_CHANGELOG",
                //                                                    strOLDDnNo,
                //                                                    strNewDnNo,
                //                                                    strPersonIC,
                //                                                    strReason);
                //return 1;

                 int i = 0;
                //conn.Open();

                SqlCommand cmd = new SqlCommand("DN_UPDATE_DNNO_CHANGELOG", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlTransaction transaction = null;

                cmd.Parameters.Add(new SqlParameter("@OLDDNNO", SqlDbType.NVarChar, 20)).Value = strOLDDnNo;
                cmd.Parameters.Add(new SqlParameter("@NEWDNNO", SqlDbType.NVarChar, 20)).Value = strNewDnNo;
                cmd.Parameters.Add(new SqlParameter("@PERSONIC", SqlDbType.NVarChar, 20)).Value = strPersonIC;
               cmd.Parameters.Add(new SqlParameter("@REASON", SqlDbType.NVarChar, 20)).Value = strReason;
               
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
            catch (SqlException ex)
            {
                return ex.Number;
            }

        }
           
        
        public int DN_UpdateDNNo_Status(string strDNno, string strUserID)
        {
            int i = 0;
            //conn.Open();

            SqlCommand cmd = new SqlCommand("DN_UPDATE_DNNO_STATUS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlTransaction transaction = null;

            cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar, 20)).Value = strDNno;
            cmd.Parameters.Add(new SqlParameter("@USERID", SqlDbType.NVarChar, 50)).Value = strUserID;

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

        public DataSet WH_GetLoadingDock()
        {

            SqlCommand cmd = new SqlCommand("DN_GETLOADINGDOCK", conn);
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

        public DataSet GET_RECEIVED_LOTS(DateTime dTDateFrom, DateTime dTDateTo, string strSupplierID)
        {
            SqlCommand cmd = new SqlCommand("GET_RECEIVED_LOTS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // cmd.Parameters.Add(loadingDock,SqlDbType.Text);
            cmd.Parameters.Add(new SqlParameter("@DateFrom", SqlDbType.DateTime)).Value = dTDateFrom;
            cmd.Parameters.Add(new SqlParameter("@DateTo", SqlDbType.DateTime)).Value = dTDateTo;
            cmd.Parameters.Add(new SqlParameter("@SupplierID", SqlDbType.NVarChar)).Value = strSupplierID;


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

