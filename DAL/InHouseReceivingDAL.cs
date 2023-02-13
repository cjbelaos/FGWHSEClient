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
    public class InHouseReceivingDAL : BaseDAL
    {
        public InHouseReceivingDAL()
        {

        }

        public DataSet RFID_GET_INK_ANTENNA_READ(string strAntennaID)
        {
            SqlCommand cmd = new SqlCommand("RFID_GET_INK_ANTENNA_READ", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@ANTENNALOCATIONID", SqlDbType.NVarChar, 20)).Value = strAntennaID;

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

        public DataSet RFID_DUMMY_GET_DATE()
        {
            SqlCommand cmd = new SqlCommand("RFID_DUMMY_GET_DATE", conn);
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

        public DataSet GET_MATERIAL(string strMaterialNo)
        {
            SqlCommand cmd = new SqlCommand("GET_MATERIAL", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@MaterialNo", SqlDbType.NVarChar, 20)).Value = strMaterialNo;

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

        public DataSet GET_RFID_INQUIRY_INHOUSE_BY_RFID(string strRFID, DateTime dDate1, DateTime dDate2, int iArea)
        {
            SqlCommand cmd = new SqlCommand("GET_RFID_INQUIRY_INHOUSE_BY_RFID", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@RFIDTAG", SqlDbType.NVarChar, 30)).Value = strRFID;
            cmd.Parameters.Add(new SqlParameter("@DATE1", SqlDbType.DateTime)).Value = dDate1;
            cmd.Parameters.Add(new SqlParameter("@DATE2", SqlDbType.DateTime)).Value = dDate2;
            cmd.Parameters.Add(new SqlParameter("@AREA", SqlDbType.Int)).Value = iArea;

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

        public DataSet INK_GET_AREA()
        {
            SqlCommand cmd = new SqlCommand("GET_AREA_LIST", conn);
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

        public DataSet GET_RFID_INQUIRY_INHOUSE_BY_PARTCODE(string strPartCode, DateTime dDate1, DateTime dDate2, int iArea)
        {
            SqlCommand cmd = new SqlCommand("GET_RFID_INQUIRY_INHOUSE_BY_PARTCODE", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@PARTCODE_", SqlDbType.NVarChar, 20)).Value = strPartCode;
            cmd.Parameters.Add(new SqlParameter("@DATE1", SqlDbType.DateTime)).Value = dDate1;
            cmd.Parameters.Add(new SqlParameter("@DATE2", SqlDbType.DateTime)).Value = dDate2;
            cmd.Parameters.Add(new SqlParameter("@AREA", SqlDbType.Int)).Value = iArea;

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

        public DataSet GET_RFID_INQUIRY_INHOUSE(string strRFIDTag, DateTime dDate1, DateTime dDate2, int iArea, string strPartCode, string strRefNo)
        {
            SqlCommand cmd = new SqlCommand("GET_RFID_INQUIRY_INHOUSE", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@RFIDTAG", SqlDbType.NVarChar, 20)).Value = strRFIDTag;
            cmd.Parameters.Add(new SqlParameter("@DATE1", SqlDbType.DateTime)).Value = dDate1;
            cmd.Parameters.Add(new SqlParameter("@DATE2", SqlDbType.DateTime)).Value = dDate2;
            cmd.Parameters.Add(new SqlParameter("@AREA", SqlDbType.Int)).Value = iArea;
            cmd.Parameters.Add(new SqlParameter("@PARTCODE_", SqlDbType.NVarChar, 20)).Value = strPartCode;
            cmd.Parameters.Add(new SqlParameter("@REFNO", SqlDbType.NVarChar, 50)).Value = strRefNo;

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


        public string UPDATE_LOTMOVEMENT_PRINTFLAG(string strLotMovementID)
        {
            int i = 0;
            string strMsg = "";
            //conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE_LOTMOVEMENT_PRINTFLAG", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlTransaction transaction = null;

            cmd.Parameters.Add(new SqlParameter("@LotMovementID", SqlDbType.NVarChar, 500)).Value = strLotMovementID;


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

        public DataSet RFID_GET_INK_ANTENNA_READ_2(string strAntennaLocationID)
        {
            SqlCommand cmd = new SqlCommand("RFID_GET_INK_ANTENNA_READ_2", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.Add(new SqlParameter("@ANTENNALOCATIONID", SqlDbType.NVarChar, 20)).Value = strAntennaLocationID;

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

        public DataSet RFID_GET_PPW_ANTENNA_READ(string strAntennaLocationID)
        {
            SqlCommand cmd = new SqlCommand("RFID_GET_PPW_ANTENNA_READ", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.Add(new SqlParameter("@ANTENNALOCATIONID", SqlDbType.NVarChar, 20)).Value = strAntennaLocationID;

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

        public DataSet GET_AREA_LIST()
        {
            SqlCommand cmd = new SqlCommand("GET_AREA_LIST", conn);
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

        public DataSet RFID_PPW_GET_LOCATION_LIST()
        {
            SqlCommand cmd = new SqlCommand("RFID_PPW_GET_LOCATION_LIST", conn);
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

        public DataSet RFID_GET_INHOUSE_ANTENNA_READ_2(string strAntennaID)
        {
            SqlCommand cmd = new SqlCommand("RFID_GET_INHOUSE_ANTENNA_READ_2 ", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@ANTENNALOCATIONID", SqlDbType.NVarChar, 20)).Value = strAntennaID;
            cmd.CommandTimeout = 360000;

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

        public DataSet DN_GETLOADINGDOCK_INHOUSE_MA()
        {

            SqlCommand cmd = new SqlCommand("DN_GETLOADINGDOCK_INHOUSE_MA", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add(new SqlParameter("@LocationTypeID", SqlDbType.NVarChar)).Value = LocationTypeID;
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


        public DataSet RFID_GET_INK_ANTENNA_READ_DETAILS(string strTRANSFERLISTID)
        {
            SqlCommand cmd = new SqlCommand("RFID_GET_INK_ANTENNA_READ_DETAILS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@TRANSFERLISTID", SqlDbType.NVarChar, 20)).Value = strTRANSFERLISTID;
            cmd.CommandTimeout = 360000;

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

        public DataSet GET_HOLMES_LOT_INQUIRY(string strDATEFROM, string strDATETO, string strRFID, string strREFNO, string strLOTNO, string strPARTCODE, string strAREA)
        {
            SqlCommand cmd = new SqlCommand("GET_HOLMES_LOT_INQUIRY", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@DATEFROM", SqlDbType.NVarChar)).Value = strDATEFROM;
            cmd.Parameters.Add(new SqlParameter("@DATETO", SqlDbType.NVarChar)).Value = strDATETO;
            cmd.Parameters.Add(new SqlParameter("@RFID", SqlDbType.NVarChar)).Value = strRFID;
            cmd.Parameters.Add(new SqlParameter("@REFNO", SqlDbType.NVarChar)).Value = strREFNO;
            cmd.Parameters.Add(new SqlParameter("@LOTNO", SqlDbType.NVarChar)).Value = strLOTNO;
            cmd.Parameters.Add(new SqlParameter("@PARTCODE", SqlDbType.NVarChar)).Value = strPARTCODE;
            cmd.Parameters.Add(new SqlParameter("@AREA", SqlDbType.NVarChar)).Value = strAREA;

            cmd.CommandTimeout = 360000;

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


        public DataSet GET_HOLMES_LOT_MOVEMENT(string strDATEFROM, string strDATETO, string strRFID, string strREFNO, string strLOTNO, string strPARTCODE, string strAREA)
        {
            SqlCommand cmd = new SqlCommand("GET_HOLMES_LOT_MOVEMENT", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@DATEFROM", SqlDbType.NVarChar)).Value = strDATEFROM;
            cmd.Parameters.Add(new SqlParameter("@DATETO", SqlDbType.NVarChar)).Value = strDATETO;
            cmd.Parameters.Add(new SqlParameter("@RFID", SqlDbType.NVarChar)).Value = strRFID;
            cmd.Parameters.Add(new SqlParameter("@REFNO", SqlDbType.NVarChar)).Value = strREFNO;
            cmd.Parameters.Add(new SqlParameter("@LOTNO", SqlDbType.NVarChar)).Value = strLOTNO;
            cmd.Parameters.Add(new SqlParameter("@PARTCODE", SqlDbType.NVarChar)).Value = strPARTCODE;
            cmd.Parameters.Add(new SqlParameter("@AREA", SqlDbType.NVarChar)).Value = strAREA;

            cmd.CommandTimeout = 360000;

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



        public DataSet GET_HOLMES_LOT_MOVEMENT_HISTORY_LOGS(string strDATEFROM, string strDATETO, string strRFID, string strREFNO, string strLOTNO, string strPARTCODE, string strAREA)
        {
            SqlCommand cmd = new SqlCommand("GET_HOLMES_LOT_MOVEMENT_HISTORY_LOGS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@DATEFROM", SqlDbType.NVarChar)).Value = strDATEFROM;
            cmd.Parameters.Add(new SqlParameter("@DATETO", SqlDbType.NVarChar)).Value = strDATETO;
            cmd.Parameters.Add(new SqlParameter("@RFID", SqlDbType.NVarChar)).Value = strRFID;
            cmd.Parameters.Add(new SqlParameter("@REFNO", SqlDbType.NVarChar)).Value = strREFNO;
            cmd.Parameters.Add(new SqlParameter("@LOTNO", SqlDbType.NVarChar)).Value = strLOTNO;
            cmd.Parameters.Add(new SqlParameter("@PARTCODE", SqlDbType.NVarChar)).Value = strPARTCODE;
            cmd.Parameters.Add(new SqlParameter("@AREA", SqlDbType.NVarChar)).Value = strAREA;

            cmd.CommandTimeout = 360000;

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




        public DataSet GET_RFID_LOCATION_TYPE()
        {
            SqlCommand cmd = new SqlCommand("GET_RFID_LOCATION_TYPE", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.CommandTimeout = 360000;

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



        public DataSet GET_RFID_AREA_LOCATION()
        {
            SqlCommand cmd = new SqlCommand("GET_RFID_AREA_LOCATION", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.CommandTimeout = 360000;

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


        public DataSet GET_RFID_ANTENNA_TYPE()
        {
            SqlCommand cmd = new SqlCommand("GET_RFID_ANTENNA_TYPE", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.CommandTimeout = 360000;

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


        public DataSet GET_RFID_ANTENNA_LIST()
        {
            SqlCommand cmd = new SqlCommand("GET_RFID_ANTENNA_LIST", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.CommandTimeout = 360000;

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



        public void PopulateGrid(GridView grd, DataSet dsGRD)
        {
            grd.DataSource = dsGRD;
            grd.DataBind();
        }




        public DataSet INHOUSE_GET_IF_RESTRICTION(string strITEMCODE, string strTOLOCATION, string strRESTRICFLAG, string strDATEFROM, string strDATETO)
        {
            SqlCommand cmd = new SqlCommand("INHOUSE_GET_IF_RESTRICTION", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ITEMCODE", SqlDbType.NVarChar)).Value = strITEMCODE;
            cmd.Parameters.Add(new SqlParameter("@TOLOCATION", SqlDbType.NVarChar)).Value = strTOLOCATION;
            cmd.Parameters.Add(new SqlParameter("@RESTRICFLAG", SqlDbType.NVarChar)).Value = strRESTRICFLAG;
            cmd.Parameters.Add(new SqlParameter("@DATEFROM", SqlDbType.NVarChar)).Value = strDATEFROM;
            cmd.Parameters.Add(new SqlParameter("@DATETO", SqlDbType.NVarChar)).Value = strDATETO;

            cmd.CommandTimeout = 360000;

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


        public DataSet INHOUSE_GET_RESTRICTION_STATUS()
        {
            SqlCommand cmd = new SqlCommand("INHOUSE_GET_RESTRICTION_STATUS", conn);
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


        public void INHOUSE_ADD_RESTRICTION_STATUS(string strITEMCODE, string strTOLOCATION, string strRESTRICTFLAG, string strUID)
        {
            SqlCommand cmd = new SqlCommand("INHOUSE_ADD_RESTRICTION_STATUS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ITEMCODE", SqlDbType.NVarChar)).Value = strITEMCODE;
            cmd.Parameters.Add(new SqlParameter("@TOLOCATION", SqlDbType.NVarChar)).Value = strTOLOCATION;
            cmd.Parameters.Add(new SqlParameter("@RESTRICTFLAG", SqlDbType.NVarChar)).Value = strRESTRICTFLAG;
            cmd.Parameters.Add(new SqlParameter("@UID", SqlDbType.NVarChar)).Value = strUID;

            cmd.CommandTimeout = 360000;

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



        public void INHOUSE_DELETE_RESTRICTION_STATUS(string strITEMCODE, string strTOLOCATION, string strUID)
        {
            SqlCommand cmd = new SqlCommand("INHOUSE_DELETE_RESTRICTION_STATUS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ITEMCODE", SqlDbType.NVarChar)).Value = strITEMCODE;
            cmd.Parameters.Add(new SqlParameter("@TOLOCATION", SqlDbType.NVarChar)).Value = strTOLOCATION;
            cmd.Parameters.Add(new SqlParameter("@UID", SqlDbType.NVarChar)).Value = strUID;

            cmd.CommandTimeout = 360000;

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