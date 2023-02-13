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
    public class PairedDAL : BaseDAL
    {

        public PairedDAL()
        {

        }


        public DataSet GET_AREA(string strUserID)
        {
            //SqlCommand cmd = new SqlCommand("GET_NOTRECEIVEDRFID", conn);
            SqlCommand cmd = new SqlCommand("GET_AREA", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@USERID", SqlDbType.NVarChar)).Value = strUserID;
            //cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar, 16)).Value = strDNNo;

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




        public DataSet GET_LOT_LIST_REF(string strREFNO)
        {
            //SqlCommand cmd = new SqlCommand("GET_NOTRECEIVEDRFID", conn);
            SqlCommand cmd = new SqlCommand("GET_LOT_LIST_REF_UNUSED", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@refno", SqlDbType.NVarChar)).Value = strREFNO;

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


        public DataSet GET_LOT_LIST_RFID_PARTCODE(string strRFID)
        {
            //SqlCommand cmd = new SqlCommand("GET_NOTRECEIVEDRFID", conn);
            SqlCommand cmd = new SqlCommand("GET_LOT_LIST_RFID_PARTCODE", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@RFID", SqlDbType.NVarChar)).Value = strRFID;

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


        public void AddLotRfidPairing(
        string strRefNo,
        string strRFIDTag,
        string strLotNo,
        string strPartCode,
        string strQTY,
        string strRemarks,
        string strUserID,
        string strAreaID
        )
        {
            //SqlCommand cmd = new SqlCommand("GET_NOTRECEIVEDRFID", conn);
            SqlCommand cmd = new SqlCommand("AddLotRfidPairing", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@RefNo", SqlDbType.NVarChar)).Value = strRefNo;
            cmd.Parameters.Add(new SqlParameter("@RFIDTag", SqlDbType.NVarChar)).Value = strRFIDTag;
            cmd.Parameters.Add(new SqlParameter("@LotNo", SqlDbType.NVarChar)).Value = strLotNo;
            cmd.Parameters.Add(new SqlParameter("@PartCode", SqlDbType.NVarChar)).Value = strPartCode;
            
            cmd.Parameters.Add(new SqlParameter("@QTY", SqlDbType.NVarChar)).Value = strQTY;
            cmd.Parameters.Add(new SqlParameter("@Remarks", SqlDbType.NVarChar)).Value = strRemarks;
            cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.NVarChar)).Value = strUserID;
            cmd.Parameters.Add(new SqlParameter("@AreaID", SqlDbType.NVarChar)).Value = strAreaID;
            
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


        public void UPDATE_RFID_FINISH_FLAG(string strRefNo)
        {
            SqlCommand cmd = new SqlCommand("UPDATE_RFID_FINISH_FLAG", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@RefNo", SqlDbType.NVarChar)).Value = strRefNo;

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




        public DataSet CHECK_TAGS_IF_PAIRED(string strRFIDTAG, string strREFNO)
        {
            //SqlCommand cmd = new SqlCommand("GET_NOTRECEIVEDRFID", conn);
            SqlCommand cmd = new SqlCommand("CHECK_TAGS_IF_PAIRED", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@RFIDTAG", SqlDbType.NVarChar)).Value = strRFIDTAG;
            cmd.Parameters.Add(new SqlParameter("@REFNO", SqlDbType.NVarChar)).Value = strREFNO;

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




        public void DELETE_PAIRED_TAGS(string strRFIDTAG, string strREFNO, string strUserID)
        {
            //SqlCommand cmd = new SqlCommand("GET_NOTRECEIVEDRFID", conn);
            SqlCommand cmd = new SqlCommand("DELETE_PAIRED_TAGS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@RFIDTAG", SqlDbType.NVarChar)).Value = strRFIDTAG;
            cmd.Parameters.Add(new SqlParameter("@REFNO", SqlDbType.NVarChar)).Value = strREFNO;
            cmd.Parameters.Add(new SqlParameter("@USERID", SqlDbType.NVarChar)).Value = strUserID;
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

        public void Save(string strRfidTag, string strPartCode, string strLotNo, string strRefNo, string strQty, string strRemarks, string strArea, string strUserID)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("AddLotRfidPairing", conn);
                cmd.CommandType = CommandType.StoredProcedure;


                conn.Open();


                cmd.Parameters.Add(new SqlParameter("@RefNo", strRefNo));
                cmd.Parameters.Add(new SqlParameter("@RFIDTag", strRfidTag));
                cmd.Parameters.Add(new SqlParameter("@LotNo", strLotNo));
                cmd.Parameters.Add(new SqlParameter("@PartCode", strPartCode));
                cmd.Parameters.Add(new SqlParameter("@Remarks", strRemarks));
                cmd.Parameters.Add(new SqlParameter("@QTY", Convert.ToInt32(strQty)));
                cmd.Parameters.Add(new SqlParameter("@AreaID", strArea));
                cmd.Parameters.Add(new SqlParameter("@UserID", strUserID));
                cmd.ExecuteNonQuery();

                conn.Close();

            }

            catch (Exception ex) { throw ex; }

        }


        public DataSet CHECK_RFID_EXISTS_IN_MASTER(string strRfidTag)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("CHECK_RFID_EXISTS_IN_MASTER", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RFIDTag", strRfidTag));

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

            catch (Exception ex) { throw ex; }

        }

        public DataSet UPDATE_LotRfidPairing(string strRefNo,string strRfidTag, string strUserID)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("UPDATE_LotRfidPairing", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RefNo", strRefNo));
                cmd.Parameters.Add(new SqlParameter("@RFIDTag", strRfidTag));
                cmd.Parameters.Add(new SqlParameter("@UserID", strUserID));

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

            catch (Exception ex) { throw ex; }

        }


    }
}
