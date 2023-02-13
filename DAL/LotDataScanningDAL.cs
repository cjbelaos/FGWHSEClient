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
    public class LotDataScanningDAL : BaseDAL
    {
        public LotDataScanningDAL()
        {

        }

        public DataSet CHECK_RFID_DETAILS(string strRFID, string strLoginType)
        {
            SqlCommand cmd = new SqlCommand("CHECK_RFID_DETAILS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@RFIDTAG", SqlDbType.NVarChar, 30)).Value = strRFID;
            cmd.Parameters.Add(new SqlParameter("@LoginType", SqlDbType.NVarChar, 10)).Value = strLoginType;

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

        public DataSet GET_QRDATA_BASED_FROM_DN_AND_RFID(string strRFID, string strDNNo, string strLoginType)
        {
            SqlCommand cmd = new SqlCommand("GET_QRDATA_BASED_FROM_DN_AND_RFID", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@RFIDTAG", SqlDbType.NVarChar, 30)).Value = strRFID;
            cmd.Parameters.Add(new SqlParameter("@DNNO", SqlDbType.NVarChar, 30)).Value = strDNNo;
            cmd.Parameters.Add(new SqlParameter("@LOGINTYPE", SqlDbType.NVarChar, 30)).Value = strLoginType;

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

        public DataSet GET_TOTALSCANNEDDNQTY(string strDNNo, string strPartCode, string strLoginType)
        {
            SqlCommand cmd = new SqlCommand("GET_TOTALSCANNEDDNQTY", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar, 25)).Value = strDNNo;
            cmd.Parameters.Add(new SqlParameter("@PartCode", SqlDbType.NVarChar, 20)).Value = strPartCode;
            cmd.Parameters.Add(new SqlParameter("@LoginType", SqlDbType.NVarChar, 20)).Value = strLoginType;

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

        public DataSet CHECK_REFNO_IF_ALREADY_EXISTS(string strRefNo, string strLoginType)
        {
            SqlCommand cmd = new SqlCommand("CHECK_REFNO_IF_ALREADY_EXISTS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@REFNO", SqlDbType.NVarChar, 50)).Value = strRefNo;
            cmd.Parameters.Add(new SqlParameter("@LoginType", SqlDbType.NVarChar, 50)).Value = strLoginType;

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

        public string ADD_SCANNED_DNDATA_EPPI(string strDNNo, string strRFIDTag, string strQRCode, string strPartCode, string strLotNo,
            string strRefNo, decimal iQty, string strRemarks, decimal iDNQty, string strSupplierID, int iProcFlag, int iByPassFlag,
            string strScannedBy, string strCreatedBy)
        {
            int i = 0;
            string strMsg ="";
            //conn.Open();

            SqlCommand cmd = new SqlCommand("ADD_SCANNED_DNDATA_EPPI", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlTransaction transaction = null;

            cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar, 16)).Value = strDNNo;
            cmd.Parameters.Add(new SqlParameter("@RFIDTag", SqlDbType.NVarChar, 30)).Value = strRFIDTag;
            cmd.Parameters.Add(new SqlParameter("@QRCOde", SqlDbType.NVarChar, 200)).Value = strQRCode;
            cmd.Parameters.Add(new SqlParameter("@PartCode", SqlDbType.NVarChar, 20)).Value = strPartCode;
            cmd.Parameters.Add(new SqlParameter("@LotNo", SqlDbType.NVarChar, 50)).Value = strLotNo;
            cmd.Parameters.Add(new SqlParameter("@RefNo", SqlDbType.NVarChar, 50)).Value = strRefNo;
            cmd.Parameters.Add(new SqlParameter("@Qty", SqlDbType.Decimal)).Value = iQty;
            cmd.Parameters.Add(new SqlParameter("@Remarks", SqlDbType.NVarChar, 50)).Value = strRemarks;
            cmd.Parameters.Add(new SqlParameter("@DNQty", SqlDbType.Decimal)).Value = iDNQty;
            cmd.Parameters.Add(new SqlParameter("@SupplierID", SqlDbType.NVarChar, 20)).Value = strSupplierID;
            cmd.Parameters.Add(new SqlParameter("@ProcFlag", SqlDbType.Int)).Value = iProcFlag;
            cmd.Parameters.Add(new SqlParameter("@ByPassFlag", SqlDbType.Int)).Value = iByPassFlag;
            cmd.Parameters.Add(new SqlParameter("@ScannedBy", SqlDbType.VarChar, 20)).Value = strScannedBy;
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


        public string ADD_SCANNED_DNDATA_SUPPLIER(string strDNNo, string strRFIDTag, string strQRCode, string strPartCode, string strLotNo,
            string strRefNo, decimal iQty, string strRemarks, decimal iDNQty, string strSupplierID, int iProcFlag, int iByPassFlag,
            string strScannedBy, string strCreatedBy)
        {
            int i = 0;
            string strMsg = "";
            //conn.Open();

            SqlCommand cmd = new SqlCommand("ADD_SCANNED_DNDATA_SUPPLIER", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlTransaction transaction = null;

            cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar, 16)).Value = strDNNo;
            cmd.Parameters.Add(new SqlParameter("@RFIDTag", SqlDbType.NVarChar, 30)).Value = strRFIDTag;
            cmd.Parameters.Add(new SqlParameter("@QRCOde", SqlDbType.NVarChar, 200)).Value = strQRCode;
            cmd.Parameters.Add(new SqlParameter("@PartCode", SqlDbType.NVarChar, 20)).Value = strPartCode;
            cmd.Parameters.Add(new SqlParameter("@LotNo", SqlDbType.NVarChar, 50)).Value = strLotNo;
            cmd.Parameters.Add(new SqlParameter("@RefNo", SqlDbType.NVarChar, 50)).Value = strRefNo;
            cmd.Parameters.Add(new SqlParameter("@Qty", SqlDbType.Decimal)).Value = iQty;
            cmd.Parameters.Add(new SqlParameter("@Remarks", SqlDbType.NVarChar, 50)).Value = strRemarks;
            cmd.Parameters.Add(new SqlParameter("@DNQty", SqlDbType.Decimal)).Value = iDNQty;
            cmd.Parameters.Add(new SqlParameter("@SupplierID", SqlDbType.NVarChar, 20)).Value = strSupplierID;
            cmd.Parameters.Add(new SqlParameter("@ProcFlag", SqlDbType.Int)).Value = iProcFlag;
            cmd.Parameters.Add(new SqlParameter("@ByPassFlag", SqlDbType.Int)).Value = iByPassFlag;
            cmd.Parameters.Add(new SqlParameter("@ScannedBy", SqlDbType.VarChar, 20)).Value = strScannedBy;
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

        //public int ADD_SCANNED_DNDATA_SUPPLIER(string strDNNo, string strRFIDTag, string strQRCode, string strPartCode, string strLotNo,
        //    string strRefNo, decimal iQty, string strRemarks, decimal iDNQty, string strSupplierID, int iProcFlag, int iByPassFlag,
        //    string strScannedBy, string strCreatedBy)
        //{
        //    int i = 0;
        //    //conn.Open();

        //    SqlCommand cmd = new SqlCommand("ADD_SCANNED_DNDATA_SUPPLIER", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    SqlTransaction transaction = null;

        //    cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar, 16)).Value = strDNNo;
        //    cmd.Parameters.Add(new SqlParameter("@RFIDTag", SqlDbType.NVarChar, 30)).Value = strRFIDTag;
        //    cmd.Parameters.Add(new SqlParameter("@QRCOde", SqlDbType.NVarChar, 200)).Value = strQRCode;
        //    cmd.Parameters.Add(new SqlParameter("@PartCode", SqlDbType.NVarChar, 20)).Value = strPartCode;
        //    cmd.Parameters.Add(new SqlParameter("@LotNo", SqlDbType.NVarChar, 50)).Value = strLotNo;
        //    cmd.Parameters.Add(new SqlParameter("@RefNo", SqlDbType.NVarChar, 50)).Value = strRefNo;
        //    cmd.Parameters.Add(new SqlParameter("@Qty", SqlDbType.Decimal)).Value = iQty;
        //    cmd.Parameters.Add(new SqlParameter("@Remarks", SqlDbType.NVarChar, 50)).Value = strRemarks;
        //    cmd.Parameters.Add(new SqlParameter("@DNQty", SqlDbType.Decimal)).Value = iDNQty;
        //    cmd.Parameters.Add(new SqlParameter("@SupplierID", SqlDbType.NVarChar, 20)).Value = strSupplierID;
        //    cmd.Parameters.Add(new SqlParameter("@ProcFlag", SqlDbType.Int)).Value = iProcFlag;
        //    cmd.Parameters.Add(new SqlParameter("@ByPassFlag", SqlDbType.Int)).Value = iByPassFlag;
        //    cmd.Parameters.Add(new SqlParameter("@ScannedBy", SqlDbType.VarChar, 20)).Value = strScannedBy;
        //    cmd.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.VarChar, 20)).Value = strCreatedBy;

        //    try
        //    {
        //        conn.Open();
        //        transaction = conn.BeginTransaction();
        //        cmd.Transaction = transaction;
        //        i = cmd.ExecuteNonQuery();
        //        transaction.Commit();
        //    }
        //    catch (SqlException se)
        //    {
        //        transaction.Rollback();
        //        if (se.Number == 2627)
        //        {
        //            throw new ApplicationException("The record you are saving was already been saved or marked as deleted");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        transaction.Rollback();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //    return i;
        //}

        public DataSet GET_TOTALDNQTY(string strDNNo, string strPartCode)
        {
            SqlCommand cmd = new SqlCommand("GET_TOTALDNQTY", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar, 25)).Value = strDNNo;
            cmd.Parameters.Add(new SqlParameter("@PartCode", SqlDbType.NVarChar, 20)).Value = strPartCode;

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

        //public DataSet GET_DETAILS_BASED_FROM_DNNO_AND_RFID(string strDNNo, string strRFID)
        //{
        //    SqlCommand cmd = new SqlCommand("GET_DETAILS_BASED_FROM_DNNO_AND_RFID", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar, 16)).Value = strDNNo;
        //    cmd.Parameters.Add(new SqlParameter("@RFID", SqlDbType.NVarChar, 30)).Value = strRFID;

        //    DataSet ds = new DataSet();
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);

        //    try
        //    {
        //        conn.Open();
        //        da.Fill(ds);

        //    }
        //    catch (SqlException oe)
        //    {
        //        Logger.GetInstance().Error(oe.Message);
        //        Logger.GetInstance().Error(oe.StackTrace);
        //        throw oe;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.GetInstance().Fatal(ex.Message);
        //        Logger.GetInstance().Fatal(ex.StackTrace);
        //        throw ex;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //    return ds;
        //}

        public DataSet GET_SCAN_DETAILS_BASED_FROM_DNNO_AND_SUPP_ID(string strDNNo, string strSupplierID, string strLoginType)
        {
            SqlCommand cmd = new SqlCommand("GET_SCAN_DETAILS_BASED_FROM_DNNO_AND_SUPP_ID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar, 16)).Value = strDNNo;
            cmd.Parameters.Add(new SqlParameter("@SupplierID", SqlDbType.NVarChar, 16)).Value = strSupplierID;
            cmd.Parameters.Add(new SqlParameter("@LoginType", SqlDbType.NVarChar, 16)).Value = strLoginType;

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

       

        //public DataSet GET_DN_DETAILS(string strDNNo)
        //{
        //    SqlCommand cmd = new SqlCommand("GET_DN_DETAILS", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar, 16)).Value = strDNNo;

        //    DataSet ds = new DataSet();
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);

        //    try
        //    {
        //        conn.Open();
        //        da.Fill(ds);

        //    }
        //    catch (SqlException oe)
        //    {
        //        Logger.GetInstance().Error(oe.Message);
        //        Logger.GetInstance().Error(oe.StackTrace);
        //        throw oe;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.GetInstance().Fatal(ex.Message);
        //        Logger.GetInstance().Fatal(ex.StackTrace);
        //        throw ex;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //    return ds;
        //}

        public int UPDATE_PROCFLAG(string strDNNo, string strSupplierID, string strUpdatedBy, string strLoginType)
        {
            int i = 0;
            //conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE_PROCFLAG", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlTransaction transaction = null;

            cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar, 16)).Value = strDNNo;
            cmd.Parameters.Add(new SqlParameter("@SupplierID", SqlDbType.NVarChar, 20)).Value = strSupplierID;
            cmd.Parameters.Add(new SqlParameter("@UPDATEDBY", SqlDbType.NVarChar, 20)).Value = strUpdatedBy;
            cmd.Parameters.Add(new SqlParameter("@LoginType", SqlDbType.NVarChar, 10)).Value = strLoginType;
            
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

        public int DELETE_SCANNEDDATA_BASED_FROM_DN(string strDNNo, string strLoginType)
        {
            int i = 0;
            //conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE_SCANNEDDATA_BASED_FROM_DN", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlTransaction transaction = null;

            cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar, 16)).Value = strDNNo;
            cmd.Parameters.Add(new SqlParameter("@LoginType", SqlDbType.NVarChar, 16)).Value = strLoginType;
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

        //public DataSet GET_QRDATA_BASED_FROM_RFID_NOT_EQUAL_TO_SCANNED_QR(string strRFID, string strQR, string strLoginType)
        //{

        //    SqlCommand cmd = new SqlCommand("GET_QRDATA_BASED_FROM_RFID_NOT_EQUAL_TO_SCANNED_QR", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.Add(new SqlParameter("@RFIDTAG", SqlDbType.NVarChar, 30)).Value = strRFID;
        //    //cmd.Parameters.Add(new SqlParameter("@DNNO", SqlDbType.NVarChar, 16)).Value = strDNNo;
        //    cmd.Parameters.Add(new SqlParameter("@QRCODE", SqlDbType.NVarChar, 200)).Value = strQR;
        //    cmd.Parameters.Add(new SqlParameter("@LoginType", SqlDbType.NVarChar, 10)).Value = strLoginType;
           
        //    DataSet ds = new DataSet();
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);

        //    try
        //    {
        //        conn.Open();
        //        da.Fill(ds);

        //    }
        //    catch (SqlException oe)
        //    {
        //        Logger.GetInstance().Error(oe.Message);
        //        Logger.GetInstance().Error(oe.StackTrace);
        //        throw oe;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.GetInstance().Fatal(ex.Message);
        //        Logger.GetInstance().Fatal(ex.StackTrace);
        //        throw ex;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //    return ds;
        //}


        public DataSet GET_DETAILS_BASED_FROM_DNNO(string strDNNo, string strLoginType)
        {
            SqlCommand cmd = new SqlCommand("GET_DETAILS_BASED_FROM_DNNO", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar, 16)).Value = strDNNo;
            cmd.Parameters.Add(new SqlParameter("@LoginType", SqlDbType.NVarChar, 16)).Value = strLoginType;

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

        public int DELETE_SCANNEDDATA_BASED_FROM_DN_AND_RFID(string strDNNo, string strRFID, string strLoginType)
        {
            int i = 0;
            //conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE_SCANNEDDATA_BASED_FROM_DN_AND_RFID", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlTransaction transaction = null;

            cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar, 16)).Value = strDNNo;
            cmd.Parameters.Add(new SqlParameter("@RFID", SqlDbType.NVarChar, 30)).Value = strRFID;
            cmd.Parameters.Add(new SqlParameter("@LoginType", SqlDbType.NVarChar, 10)).Value = strLoginType;
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

        public DataSet GET_SCAN_DETAILS_BASED_FROM_DNNO(string strDNNo, string strSuppID, string strLoginType)
        {
            SqlCommand cmd = new SqlCommand("GET_SCAN_DETAILS_BASED_FROM_DNNO", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar, 16)).Value = strDNNo;
            cmd.Parameters.Add(new SqlParameter("@SupplierID", SqlDbType.NVarChar, 16)).Value = strSuppID;
            cmd.Parameters.Add(new SqlParameter("@LoginType", SqlDbType.NVarChar, 16)).Value = strLoginType;

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

        public DataSet CHECK_DN_AND_QR_DATA(string strDNNo, string strPartCode, string strSuppID, string strLoginType)
        {
            SqlCommand cmd = new SqlCommand("CHECK_DN_AND_QR_DATA", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar, 16)).Value = strDNNo;
            cmd.Parameters.Add(new SqlParameter("@PartCode", SqlDbType.NVarChar, 16)).Value = strPartCode;
            cmd.Parameters.Add(new SqlParameter("@SupplierID", SqlDbType.NVarChar, 16)).Value = strSuppID;
            cmd.Parameters.Add(new SqlParameter("@LoginType", SqlDbType.NVarChar, 16)).Value = strLoginType;

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

        public DataSet CHECK_MAX_DN_QTY_FROM_SCANNED_TBL(string strDNNo, string strPartCode, string strSuppID, string strLoginType)
        {
            SqlCommand cmd = new SqlCommand("CHECK_MAX_DN_QTY_FROM_SCANNED_TBL", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar, 16)).Value = strDNNo;
            cmd.Parameters.Add(new SqlParameter("@PartCode", SqlDbType.NVarChar, 16)).Value = strPartCode;
            cmd.Parameters.Add(new SqlParameter("@SupplierID", SqlDbType.NVarChar, 16)).Value = strSuppID;
            cmd.Parameters.Add(new SqlParameter("@LoginType", SqlDbType.NVarChar, 16)).Value = strLoginType;

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

        public DataSet GET_SCAN_DETAILS_BASED_FROM_DNNO_AND_SUPP_ID_BYPASS(string strDNNo, string strSupplierID, string strLoginType)
        {
            SqlCommand cmd = new SqlCommand("GET_SCAN_DETAILS_BASED_FROM_DNNO_AND_SUPP_ID_BYPASS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar, 16)).Value = strDNNo;
            cmd.Parameters.Add(new SqlParameter("@SupplierID", SqlDbType.NVarChar, 16)).Value = strSupplierID;
            cmd.Parameters.Add(new SqlParameter("@LoginType", SqlDbType.NVarChar, 16)).Value = strLoginType;

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
        public DataSet CHECK_RFID_IF_ALREADY_EXISTS(string strRFID, string strLoginType)
        {
            SqlCommand cmd = new SqlCommand("CHECK_RFID_IF_ALREADY_EXISTS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@RFIDTAG", SqlDbType.NVarChar, 30)).Value = strRFID;
            cmd.Parameters.Add(new SqlParameter("@LoginType", SqlDbType.NVarChar, 30)).Value = strLoginType;
            

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

        public DataSet SCANNED_DATA_INQUIRY(string strDNNo, string strPartCode, string strDate1, string strDate2, string strSupplierID, string strLoginType)
        {
            SqlCommand cmd = new SqlCommand("SCANNED_DATA_INQUIRY", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@BARCODEDNNO", SqlDbType.NVarChar, 16)).Value = strDNNo;
            cmd.Parameters.Add(new SqlParameter("@ITEMCODE", SqlDbType.NVarChar, 20)).Value = strPartCode;
            cmd.Parameters.Add(new SqlParameter("@DATE1", SqlDbType.NVarChar, 20)).Value = strDate1;
            cmd.Parameters.Add(new SqlParameter("@DATE2", SqlDbType.NVarChar, 20)).Value = strDate2;
            cmd.Parameters.Add(new SqlParameter("@SUPPLIERID", SqlDbType.NVarChar, 20)).Value = strSupplierID;
            cmd.Parameters.Add(new SqlParameter("@LOGINTYPE", SqlDbType.NVarChar, 20)).Value = strLoginType;

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

        public DataSet OUTSTANDING_DN_INQUIRY(string strSupplierID, string strDNNo)
        {
            SqlCommand cmd = new SqlCommand("OUTSTANDING_DN_INQUIRY", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@SUPPLIERID", SqlDbType.NVarChar, 10)).Value = strSupplierID;
            cmd.Parameters.Add(new SqlParameter("@BARCODE", SqlDbType.NVarChar, 20)).Value = strDNNo;


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
  
        ///////////////////////02Aug2019
        public DataSet SCANNED_DATA_INQUIRY_NEW(string strDNNo, string strPartCode, string strDate1, string strDate2, string strSupplierID, string strLoginType, string strExecuted)
        {
            SqlCommand cmd = new SqlCommand("SCANNED_DATA_INQUIRY_NEW", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@BARCODEDNNO", SqlDbType.NVarChar, 20)).Value = strDNNo;
            cmd.Parameters.Add(new SqlParameter("@ITEMCODE", SqlDbType.NVarChar, 20)).Value = strPartCode;
            cmd.Parameters.Add(new SqlParameter("@DATE1", SqlDbType.NVarChar, 20)).Value = strDate1;
            cmd.Parameters.Add(new SqlParameter("@DATE2", SqlDbType.NVarChar, 20)).Value = strDate2;
            cmd.Parameters.Add(new SqlParameter("@SUPPLIERID", SqlDbType.NVarChar, 20)).Value = strSupplierID;
            cmd.Parameters.Add(new SqlParameter("@LOGINTYPE", SqlDbType.NVarChar, 20)).Value = strLoginType;
            cmd.Parameters.Add(new SqlParameter("@EXECUTED", SqlDbType.NVarChar, 20)).Value = strExecuted;

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

        public DataSet GET_RFIDLOT_INQUIRY(string strDate1, string strDate2, string strSupplierID, string strRFIDTag, string strLot, string strLoginType, string strItemCode)
        {
            SqlCommand cmd = new SqlCommand("GET_RFIDLOT_INQUIRY", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@DATE1", SqlDbType.NVarChar, 20)).Value = strDate1;
            cmd.Parameters.Add(new SqlParameter("@DATE2", SqlDbType.NVarChar, 20)).Value = strDate2;
            cmd.Parameters.Add(new SqlParameter("@SUPPLIERID", SqlDbType.NVarChar, 20)).Value = strSupplierID;
            cmd.Parameters.Add(new SqlParameter("@RFIDTAG", SqlDbType.NVarChar, 50)).Value = strRFIDTag;
            cmd.Parameters.Add(new SqlParameter("@QRCODE", SqlDbType.NVarChar)).Value = strLot;
            cmd.Parameters.Add(new SqlParameter("@LOGINTYPE", SqlDbType.NVarChar)).Value = strLoginType;
            cmd.Parameters.Add(new SqlParameter("@ITEMCODE", SqlDbType.NVarChar)).Value = strItemCode;

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
        ///////////////////////
        
        

    }

  
}
