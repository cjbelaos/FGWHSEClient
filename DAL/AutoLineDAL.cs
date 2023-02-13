using com.eppi.utils;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FGWHSEClient.DAL
{
    public class AutoLineDAL : BaseDAL
    {

        public AutoLineDAL()
        {

        }
        public DataSet AUTOLINE_GET_LOT_DATA(string strREFNO)
        {

            SqlCommand cmd = new SqlCommand("AUTOLINE_GET_LOT_DATA", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REFNO", SqlDbType.NVarChar)).Value = strREFNO;
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



        public DataSet GET_AUTOLINE_STATION(string strIPADDRESS, string strSTATIONNO)
        {
            

            SqlCommand cmd = new SqlCommand("GET_AUTOLINE_STATION", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IPADDRESS", SqlDbType.NVarChar)).Value = strIPADDRESS;
            cmd.Parameters.Add(new SqlParameter("@STATIONNO", SqlDbType.NVarChar)).Value = strSTATIONNO;
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

        public void ADD_AUTOLINE_STATION(string strIPADDRESS, string strSTATIONNO, string strUID)
        {
            SqlCommand cmd = new SqlCommand("ADD_AUTOLINE_STATION", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IPADDRESS", SqlDbType.NVarChar)).Value = strIPADDRESS;
            cmd.Parameters.Add(new SqlParameter("@STATIONNO", SqlDbType.NVarChar)).Value = strSTATIONNO;
            cmd.Parameters.Add(new SqlParameter("@UID", SqlDbType.NVarChar)).Value = strUID;
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

        }


        public void ADD_AUTOLINE_BARCODE
        (
            string strOldRefNo,
            string strRefNo,
            string strRFIDTag,
            string strLotNo,
            string strPartCode,
            string strQTY,
            string strRemarks,
            string strUserID,
            string strAreaID,
            string strIPAdd
        )
        {
            SqlCommand cmd = new SqlCommand("ADD_AUTOLINE_BARCODE", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@OldRefno", SqlDbType.NVarChar)).Value = strOldRefNo;
            cmd.Parameters.Add(new SqlParameter("@RefNo", SqlDbType.NVarChar)).Value = strRefNo;
            cmd.Parameters.Add(new SqlParameter("@RFIDTag", SqlDbType.NVarChar)).Value = strRFIDTag;
            cmd.Parameters.Add(new SqlParameter("@LotNo", SqlDbType.NVarChar)).Value = strLotNo;
            cmd.Parameters.Add(new SqlParameter("@PartCode", SqlDbType.NVarChar)).Value = strPartCode;

            cmd.Parameters.Add(new SqlParameter("@QTY", SqlDbType.NVarChar)).Value = strQTY;
            cmd.Parameters.Add(new SqlParameter("@Remarks", SqlDbType.NVarChar)).Value = strRemarks;
            cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.NVarChar)).Value = strUserID;
            cmd.Parameters.Add(new SqlParameter("@AreaID", SqlDbType.NVarChar)).Value = strAreaID;
            cmd.Parameters.Add(new SqlParameter("@IPAddress", SqlDbType.NVarChar)).Value = strIPAdd    ;
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

        }


        public DataSet GET_AUTOLINE_BARCODE_LIST
        (
         string strOLDREFNO, string strREFNO, string strLOTNO, string strPARTCODE, string strDATEFROM, string strDATETO
        )
        {
            SqlCommand cmd = new SqlCommand("GET_AUTOLINE_BARCODE_LIST", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@OLDREFNO", SqlDbType.NVarChar)).Value = strOLDREFNO;
            cmd.Parameters.Add(new SqlParameter("@REFNO", SqlDbType.NVarChar)).Value = strREFNO;
            cmd.Parameters.Add(new SqlParameter("@LOTNO", SqlDbType.NVarChar)).Value = strLOTNO;
            cmd.Parameters.Add(new SqlParameter("@PARTCODE", SqlDbType.NVarChar)).Value = strPARTCODE;
            cmd.Parameters.Add(new SqlParameter("@DATEFROM", SqlDbType.NVarChar)).Value = strDATEFROM;
            cmd.Parameters.Add(new SqlParameter("@DATETO", SqlDbType.NVarChar)).Value = strDATETO;
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


        public DataSet GET_AUTOLINE_FACTORY()
        {
            SqlCommand cmd = new SqlCommand("GET_AUTOLINE_FACTORY", conn);
            cmd.CommandType = CommandType.StoredProcedure;
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



        public DataSet GET_AUTOLINE_LINE(string strDIV)
        {
            SqlCommand cmd = new SqlCommand("GET_AUTOLINE_LINE", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@DIVISION", SqlDbType.NVarChar)).Value = strDIV;
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


        public DataSet GET_AUTOLINE_MOLD()
        {
            SqlCommand cmd = new SqlCommand("GET_AUTOLINE_MOLD", conn);
            cmd.CommandType = CommandType.StoredProcedure;
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


        public DataSet GET_AUTOLINE_CAVITY()
        {
            SqlCommand cmd = new SqlCommand("GET_AUTOLINE_CAVITY", conn);
            cmd.CommandType = CommandType.StoredProcedure;
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


        public DataSet GET_AUTOLINE_SHIFT(string strDIV)
        {
            SqlCommand cmd = new SqlCommand("GET_AUTOLINE_SHIFT", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@DIVISION", SqlDbType.NVarChar)).Value = strDIV;
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

        public DataSet GET_AUTOLINE_COLOR()
        {
            SqlCommand cmd = new SqlCommand("GET_AUTOLINE_COLOR", conn);
            cmd.CommandType = CommandType.StoredProcedure;
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


        public DataSet GET_AUTOLINE_SPECIFICATIONS(string strDIV)
        {
            SqlCommand cmd = new SqlCommand("GET_AUTOLINE_SPECIFICATIONS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@DIVISION", SqlDbType.NVarChar)).Value = strDIV;
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

        public DataSet GET_AUTOLINE_SPECIFICATIONS_COLOR()
        {
            SqlCommand cmd = new SqlCommand("GET_AUTOLINE_SPECIFICATIONS_COLOR", conn);
            cmd.CommandType = CommandType.StoredProcedure;
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

        public DataSet GET_AUTOLINE_ITEMCODE(string strDIV)
        {
            SqlCommand cmd = new SqlCommand("GET_AUTOLINE_ITEMCODE", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@DIVISION", SqlDbType.NVarChar)).Value = strDIV;
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

        public DataSet GET_AUTOLINE_ITEMCODE_DESC(string strItemcode)
        {
            SqlCommand cmd = new SqlCommand("GET_AUTOLINE_ITEMCODE_DESC", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@partcode", SqlDbType.NVarChar)).Value = strItemcode;
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


        public DataSet GET_AUTOLINE_RGB(string strCOLOR, string strIsTransparrent)
        {
            SqlCommand cmd = new SqlCommand("GET_AUTOLINE_RGB", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@COLOR", SqlDbType.NVarChar)).Value = strCOLOR;
            cmd.Parameters.Add(new SqlParameter("@ISTRANSPARRENT", SqlDbType.NVarChar)).Value = strIsTransparrent;
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
    }
}