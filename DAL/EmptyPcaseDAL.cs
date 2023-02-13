using com.eppi.utils;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FGWHSEClient.DAL
{
    public class EmptyPcaseDAL : BaseDAL
    {
        public EmptyPcaseDAL()
        {

        }

        public DataSet GET_EMPTY_PCASE_ANTENNA()
        {

            SqlCommand cmd = new SqlCommand("GET_EMPTY_PCASE_ANTENNA", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add(new SqlParameter("@REFNO", SqlDbType.NVarChar)).Value = strREFNO;
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



        public DataSet GET_EMPTY_PCASE_LOADING_DETAILS(string strLOCATIONID)
        {

            SqlCommand cmd = new SqlCommand("GET_EMPTY_PCASE_LOADING_DETAILS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@LOCATIONID", SqlDbType.NVarChar)).Value = strLOCATIONID;
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


        public DataSet EMPTY_PCASE_ADD_START_READING(string strCONTROLNO, string strTRACKINGNO, string strSUPPLIERID, string stLOCATIONID, string strPLATENO, string strSENDSTATUS, string strUID, string strABBRE)
        {
            SqlCommand cmd = new SqlCommand("EMPTY_PCASE_ADD_START_READING", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CONTROLNO", SqlDbType.NVarChar)).Value = strCONTROLNO;
            cmd.Parameters.Add(new SqlParameter("@TRACKINGNO", SqlDbType.NVarChar)).Value = strTRACKINGNO;
            cmd.Parameters.Add(new SqlParameter("@SUPPLIERID", SqlDbType.NVarChar)).Value = strSUPPLIERID;
            cmd.Parameters.Add(new SqlParameter("@LOCATIONID", SqlDbType.NVarChar)).Value = stLOCATIONID;
            cmd.Parameters.Add(new SqlParameter("@PLATENO", SqlDbType.NVarChar)).Value = strPLATENO;
            cmd.Parameters.Add(new SqlParameter("@SENDSTATUS", SqlDbType.NVarChar)).Value = strSENDSTATUS;
            cmd.Parameters.Add(new SqlParameter("@UID", SqlDbType.NVarChar)).Value = strUID;
            cmd.Parameters.Add(new SqlParameter("@ABBRE", SqlDbType.NVarChar)).Value = strABBRE;
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

        public DataSet GET_EMPTY_PCASE_LOADING_STATUS_LIST()
        {

            SqlCommand cmd = new SqlCommand("GET_EMPTY_PCASE_LOADING_STATUS_LIST", conn);
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


        public DataSet GET_EMPTY_PCASE_RECEIVER_DETAILS(string strUID)
        {

            SqlCommand cmd = new SqlCommand("GET_EMPTY_PCASE_RECEIVER_DETAILS", conn);
            cmd.Parameters.Add(new SqlParameter("@UID", SqlDbType.NVarChar)).Value = strUID;
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

        public DataSet GET_EMPTY_PCASE_LOADING_DETAILS_BY_CONTROLNO(string strCONTROLNO)
        {

            SqlCommand cmd = new SqlCommand("GET_EMPTY_PCASE_LOADING_DETAILS_BY_CONTROLNO", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CONTROLNO", SqlDbType.NVarChar)).Value = strCONTROLNO;
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

        public DataSet GET_EMPTY_PCASE_RECEIVED_LIST(string strCONTROLNO)
        {
            SqlCommand cmd = new SqlCommand("GET_EMPTY_PCASE_RECEIVED_LIST", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CONTROLNO", SqlDbType.NVarChar)).Value = strCONTROLNO;
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


        public void DELETE_EMPTY_PCASE_RFID(string strCONTROLNO, string strEPC, string strDELETIONAPPROVER)
        {
            SqlCommand cmd = new SqlCommand("DELETE_EMPTY_PCASE_RFID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CONTROLNO", SqlDbType.NVarChar)).Value = strCONTROLNO;
            cmd.Parameters.Add(new SqlParameter("@EPC", SqlDbType.NVarChar)).Value = strEPC;
            cmd.Parameters.Add(new SqlParameter("@DELETIONAPPROVER", SqlDbType.NVarChar)).Value = strDELETIONAPPROVER;
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


        public void ADD_EMPTY_PCASE_ADDITIONAL(string strCONTROLNO, string strADDITIONALPCASECOUNT, string strREMARKS)
        {
            SqlCommand cmd = new SqlCommand("ADD_EMPTY_PCASE_ADDITIONAL", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CONTROLNO", SqlDbType.NVarChar)).Value = strCONTROLNO;
            cmd.Parameters.Add(new SqlParameter("@ADDITIONALPCASECOUNT", SqlDbType.NVarChar)).Value = strADDITIONALPCASECOUNT;
            cmd.Parameters.Add(new SqlParameter("@REMARKS", SqlDbType.NVarChar)).Value = strREMARKS;
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



        public DataSet GET_EMPTY_PCASE_ADDITIONAL(string strCONTROLNO)
        {
            SqlCommand cmd = new SqlCommand("GET_EMPTY_PCASE_ADDITIONAL", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CONTROLNO", SqlDbType.NVarChar)).Value = strCONTROLNO;
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




        public DataSet GET_EMPTY_PCASE_PRINT_DETAILS(string strCONTROLNO)
        {
            SqlCommand cmd = new SqlCommand("GET_EMPTY_PCASE_PRINT_DETAILS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CONTROLNO", SqlDbType.NVarChar)).Value = strCONTROLNO;
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



        public DataSet GET_EMPTY_PCASE_BOX_BOARD()
        {
            SqlCommand cmd = new SqlCommand("GET_EMPTY_PCASE_BOX_BOARD", conn);
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



        public DataSet GET_EMPTY_PCASE_BOX_BOARD_DETAILS(string strDATE, string strSUPPLIERID)
        {
            SqlCommand cmd = new SqlCommand("GET_EMPTY_PCASE_BOX_BOARD_DETAILS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@SUPPLIERID", SqlDbType.NVarChar)).Value = strSUPPLIERID;
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



        public DataSet EMPTY_PCASE_BOARD_TO_STAGING_INQUIRY(string strDFROM,string strDTO,string strSUPPLIERID,string strLOCATIONID)
        {
            SqlCommand cmd = new SqlCommand("EMPTY_PCASE_BOARD_TO_STAGING_INQUIRY", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@DFROM", SqlDbType.NVarChar)).Value = strDFROM;
            cmd.Parameters.Add(new SqlParameter("@DTO", SqlDbType.NVarChar)).Value = strDTO;
            cmd.Parameters.Add(new SqlParameter("@SUPPLIERID", SqlDbType.NVarChar)).Value = strSUPPLIERID;
            cmd.Parameters.Add(new SqlParameter("@LOCATIONID", SqlDbType.NVarChar)).Value = strLOCATIONID;
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

        public DataSet GET_SUPPLIER_LOCATION()
        {
            SqlCommand cmd = new SqlCommand("GET_SUPPLIER_LOCATION", conn);
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
        

        public DataSet EMPTY_PCASE_GET_ANTENNA_CONVEYOR_RETURNABLES()
        {
            SqlCommand cmd = new SqlCommand("EMPTY_PCASE_GET_ANTENNA_CONVEYOR_RETURNABLES", conn);
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



        public DataSet EMPTY_BOX_GET_MONITORING_DETAILS(string strLOCATIONID)
        {
            SqlCommand cmd = new SqlCommand("EMPTY_BOX_GET_MONITORING_DETAILS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@LOCATIONID", SqlDbType.NVarChar)).Value = strLOCATIONID;
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