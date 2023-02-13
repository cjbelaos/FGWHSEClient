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
    public class PartsLocationCheckDAL : BaseDAL
    {
        //partslocationcheck from here
        public DataView GetPartName(string strPartCode)
        {
            return SqlHelper.ExecuteDataset(CONNECTION_STRING_FGWHSE, "Get_PartsLocation", strPartCode).Tables[0].DefaultView;
        }

        //added 10/13/2020 melvin
        public DataView GetPartName_rev1(string strPartCode)
        {
            return SqlHelper.ExecuteDataset(CONNECTION_STRING_FGWHSE, "GET_PARTS_LOCATION_rev1", strPartCode).Tables[0].DefaultView;
        }

        public DataView GetPLC_Max(string strRefNo)
        {
            return SqlHelper.ExecuteDataset(CONNECTION_STRING_FGWHSE, "GET_PARTS_LOCATION_MAX", strRefNo).Tables[0].DefaultView;
        }
        public DataView GetPartsLocationList(string strPartCode, string strLocation)
        {
            return SqlHelper.ExecuteDataset(CONNECTION_STRING_FGWHSE, "GET_PARTS_LOCATION_LIST", strPartCode, strLocation).Tables[0].DefaultView;
        }
        public DataView GetPartsLocationList_rev1(string strPartCode)
        {
            return SqlHelper.ExecuteDataset(CONNECTION_STRING_FGWHSE, "GET_PARTS_LOCATION_LIST_rev1", strPartCode).Tables[0].DefaultView;
        }
        public DataView GetRFIDTag(string RFID)
        {
            return SqlHelper.ExecuteDataset(CONNECTION_STRING_FGWHSE, "GET_RFIDTAG", RFID).Tables[0].DefaultView;
        }

        public DataView GetLotList(string refno, string partcode)
        {
            return SqlHelper.ExecuteDataset(CONNECTION_STRING_FGWHSE, "GET_LOT_LIST", refno, partcode).Tables[0].DefaultView;
        }

        public DataView GetLotListRef(string refno)
        {
            return SqlHelper.ExecuteDataset(CONNECTION_STRING_FGWHSE, "GET_LOT_LIST_REF", refno).Tables[0].DefaultView;
        }

        public DataView GetLotListRFID(string rfidno)
        {
            return SqlHelper.ExecuteDataset(CONNECTION_STRING_FGWHSE, "GET_LOT_LIST_RFID", rfidno).Tables[0].DefaultView;
        }

        public DataView GetMaterialName(string partcode)
        {
            return SqlHelper.ExecuteDataset(CONNECTION_STRING_FGWHSE, "SPC_PLC_GET_MATERIALNAME", partcode).Tables[0].DefaultView;
        }

        //public DataView GetPartsStoringInquiry(string refno, string location, string partcode, string strDate1, string strDate2)
        //{
        //    return SqlHelper.ExecuteDataset(CONNECTION_STRING_FGWHSE, "PLC_STORING_INQUIRY", refno, location, partcode, strDate1, strDate2).Tables[0].DefaultView;
        //}

        public DataSet GetPartsStoringInquiry(string refno, string location, string partcode, string strDate1, string strDate2)
        {
            SqlCommand sqlComm = new SqlCommand("PLC_STORING_INQUIRY", conn);

            sqlComm.CommandType = CommandType.StoredProcedure;
            sqlComm.CommandTimeout = 0;

            sqlComm.Parameters.Add(new SqlParameter("@REFNO", SqlDbType.NVarChar)).Value = refno;
            sqlComm.Parameters.Add(new SqlParameter("@LOCATION", SqlDbType.NVarChar)).Value = location;
            sqlComm.Parameters.Add(new SqlParameter("@PARTCODE", SqlDbType.NVarChar)).Value = partcode;
            sqlComm.Parameters.Add(new SqlParameter("@DATE1", SqlDbType.DateTime)).Value = strDate1;
            sqlComm.Parameters.Add(new SqlParameter("@DATE2", SqlDbType.DateTime)).Value = strDate2;


            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = sqlComm;

            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }
        public DataView GetRFIDMatchingInquiry(string refno, string location, string partcode, string strDate1, string strDate2)
        {
            return SqlHelper.ExecuteDataset(CONNECTION_STRING_FGWHSE, "PLC_RFID_LOT_UPDATING", refno, location, partcode, strDate1, strDate2).Tables[0].DefaultView;
        }

        public DataView GetPartsLocationInquiry(string refno, string location, string partcode)
        {
            return SqlHelper.ExecuteDataset(CONNECTION_STRING_FGWHSE, "PLC_LANE_INQUIRY", refno, location, partcode).Tables[0].DefaultView;
        }

        public int AddStoring(string strRefNo, string strLocation, string strErrorMessage, string strCreatedBy, string strPartCode)
        {
            int i = 0;

            SqlCommand cmd = new SqlCommand("PLC_ADD_STORING", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlTransaction transaction = null;

            cmd.Parameters.Add(new SqlParameter("@REFNO", SqlDbType.NVarChar, 30)).Value = strRefNo;
            cmd.Parameters.Add(new SqlParameter("@LOCATION", SqlDbType.NVarChar, 50)).Value = strLocation;
            cmd.Parameters.Add(new SqlParameter("@ERRORMESSAGE", SqlDbType.NVarChar, 250)).Value = strErrorMessage;
            cmd.Parameters.Add(new SqlParameter("@CREATEDBY", SqlDbType.NVarChar, 50)).Value = strCreatedBy;
            cmd.Parameters.Add(new SqlParameter("@PARTCODE", SqlDbType.NVarChar, 20)).Value = strPartCode;

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

        public int AddLotUpdate(string strLotID, string strUpdateField, string strOldData, string strNewData, string strUpdateBy)
        {
            int i = 0;

            SqlCommand cmd = new SqlCommand("SPC_PLC_ADD_LOTUPDATE", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlTransaction transaction = null;

            cmd.Parameters.Add(new SqlParameter("@LotID", SqlDbType.Int)).Value = strLotID;
            cmd.Parameters.Add(new SqlParameter("@UpdateField", SqlDbType.NVarChar, 50)).Value = strUpdateField;
            cmd.Parameters.Add(new SqlParameter("@OldData", SqlDbType.NVarChar, 50)).Value = strOldData;
            cmd.Parameters.Add(new SqlParameter("@NewData", SqlDbType.NVarChar, 50)).Value = strNewData;
            cmd.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.NVarChar, 50)).Value = strUpdateBy;

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

        public int UpdateLotList(string strRFIDTAG, string strRefNo, string strUpdateBy)
        {
            int i = 0;

            SqlCommand cmd = new SqlCommand("SPC_PLC_UPDATE_LOTLIST", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlTransaction transaction = null;

            cmd.Parameters.Add(new SqlParameter("@RFIDTAG", SqlDbType.NVarChar, 30)).Value = strRFIDTAG;
            cmd.Parameters.Add(new SqlParameter("@REFNO", SqlDbType.NVarChar, 30)).Value = strRefNo;
            cmd.Parameters.Add(new SqlParameter("@UPDATEDBY", SqlDbType.NVarChar, 50)).Value = strUpdateBy;
            
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


        public int UpdateLotListRFIDFlag(string strRFIDTAG)
        {
            int i = 0;

            SqlCommand cmd = new SqlCommand("SPC_PLC_UPDATE_LOTLIST_RFIDFLAG", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlTransaction transaction = null;

            cmd.Parameters.Add(new SqlParameter("@RFIDTAG", SqlDbType.NVarChar, 30)).Value = strRFIDTAG;

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

        public int UpdateLotListRFIDFlagByRefNo(string strREFNO)
        {
            int i = 0;

            SqlCommand cmd = new SqlCommand("SPC_PLC_UPDATE_LOTLIST_RFIDFLAG_BY_REFNO", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlTransaction transaction = null;

            cmd.Parameters.Add(new SqlParameter("@REFNO", SqlDbType.NVarChar, 30)).Value = strREFNO;

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

        public int UPDATE_PARTS_LOCATION(
          string part_code,
          string location,
           string location_st,
            string IQA,
            int EKANBAN,
            decimal Capacity,
            string updatedby)
        {
            try
            {
                object oAdd = SqlHelper.ExecuteScalar
                 (CONNECTION_STRING_FGWHSE, "UPDATE_PARTS_LOCATION",
                     part_code,
                     location,
                     location_st,
                     IQA,
                     EKANBAN,
                     Capacity,
                     updatedby

                 );
                return 1;
            }
            catch (SqlException ex)
            {
                return ex.Number;
            }
        }

        //to here

        public DataSet GET_PARTS_LOCATION(string partcode)
        {

            SqlCommand cmd = new SqlCommand("GET_PARTS_LOCATION", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@partcode", SqlDbType.NVarChar)).Value = partcode;
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

        public int ADD_PARTS_LOCATION(
           string part_code,
           string description,
           string location,
            string location_st)
        {
            try
            {
                object oAdd = SqlHelper.ExecuteScalar
                 (CONNECTION_STRING_FGWHSE, "ADD_PARTS_LOCATION",
                     part_code,
                     description,
                     location,
                     location_st
                 );
                return 1;
            }
            catch (SqlException ex)
            {
                return ex.Number;
            }
        }

        public string DELETE_PARTS_LOCATION(string PartCode)
        {
            string error_message = "";
            SqlCommand cmd = new SqlCommand("DELETE_PARTS_LOCATION", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PartCode", SqlDbType.VarChar)).Value = PartCode;
            cmd.CommandTimeout = 360000;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return error_message;
        }

        //to here

        ////////////////////////////13AUG2019 ADDED BY TIN
        public DataSet GET_PARTSINSPECTIONLOCATIONCHECK(string strItemCode, string strLotNo, string strDN)
        {

            SqlCommand cmd = new SqlCommand("GET_PARTSINSPECTIONLOCATIONCHECK", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ITEMCODE", SqlDbType.NVarChar)).Value = strItemCode;
            cmd.Parameters.Add(new SqlParameter("@LOTNO", SqlDbType.NVarChar)).Value = strLotNo;
            cmd.Parameters.Add(new SqlParameter("@DN", SqlDbType.NVarChar)).Value = strDN;
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

        public DataSet GET_INCOMINGINSPECTION(string strDN)
        {

            SqlCommand cmd = new SqlCommand("GET_INCOMINGINSPECTION", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@DN", SqlDbType.NVarChar)).Value = strDN;
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


        ////////////////////////////


        ////////////////////////////11SEP2019 ADDED BY TIN
        public DataSet GET_PARTS_DELIVERY_INSPECTION(string strDN)
        {

            SqlCommand cmd = new SqlCommand("GET_PARTS_DELIVERY_INSPECTION", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@DN", SqlDbType.NVarChar)).Value = strDN;
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
        ////////////////////////////

        ///////////////////////////01FEB2020 ADDED BY TIN (FOR EKANBAN TO EWHS I/F UPDATING FUNCTION)
        public string GET_EKANBANVP_PARTSMASTER()
        {
            string error_message = "";
            SqlCommand cmd = new SqlCommand("GET_EKANBANVP_PARTSMASTER", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 360000;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return error_message;
        }

        public string GET_VIRTUALPARTSMASTER_VPPIS()
        {
            string error_message = "";
            SqlCommand cmd = new SqlCommand("GET_VIRTUALPARTSMASTER_VPPIS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 360000;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return error_message;
        }

        public string GET_VIRTUALPARTSMASTER_VPEPI()
        {
            string error_message = "";
            SqlCommand cmd = new SqlCommand("GET_VIRTUALPARTSMASTER_VPEPI", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 360000;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return error_message;
        }
        ///////////////////////////


        ////////PPD PARTS LOC//////////////////////////////////////////////////////////
        //added 10/13/2020 melvin
        public DataView GetPartsLoc_PPD(string strPartCode)
        {
            return SqlHelper.ExecuteDataset(CONNECTION_STRING_FGWHSE, "GET_PARTS_LOCATION_PPD", strPartCode).Tables[0].DefaultView;
        }

        public DataView GetPartsLocationList_PPD(string strPartCode)
        {
            return SqlHelper.ExecuteDataset(CONNECTION_STRING_FGWHSE, "GET_PARTS_LOCATION_LIST_PPD", strPartCode).Tables[0].DefaultView;
        }


        public string SAVE_PARTS_LOCATION_PPD(string part_code,
            string location,
            string location_st,
            string gns_location,
            string IQA,
            int EKANBAN,
            decimal Capacity,
            string updatedby,
            int ID,
            string warehousename,
            string action)
        {
            int i = 0;
            string errmessage = "";

            SqlCommand cmd = new SqlCommand("SAVE_PARTS_LOCATION_PPD", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlTransaction transaction = null;

           
            cmd.Parameters.Add(new SqlParameter("@part_code", SqlDbType.NVarChar, 20)).Value = part_code;
            cmd.Parameters.Add(new SqlParameter("@locatiom", SqlDbType.NVarChar, 30)).Value = location;
            cmd.Parameters.Add(new SqlParameter("@locatiom_ST", SqlDbType.NVarChar, 30)).Value = location_st;
            cmd.Parameters.Add(new SqlParameter("@gnslocation", SqlDbType.NVarChar, 10)).Value = gns_location;
            cmd.Parameters.Add(new SqlParameter("@Inspection", SqlDbType.NVarChar, 30)).Value = IQA;
            cmd.Parameters.Add(new SqlParameter("@EKanbanRFIDIFFlag", SqlDbType.Int)).Value = EKANBAN;
            cmd.Parameters.Add(new SqlParameter("@Capacity", SqlDbType.Decimal)).Value = Capacity;
            cmd.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.NVarChar, 50)).Value = updatedby;
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = ID;
            cmd.Parameters.Add(new SqlParameter("@WarehouseName", SqlDbType.NVarChar, 250)).Value = warehousename;
            cmd.Parameters.Add(new SqlParameter("@action", SqlDbType.NVarChar, 10)).Value = action;

            try
            {
                conn.Open();
                transaction = conn.BeginTransaction();
                cmd.Transaction = transaction;
                i = cmd.ExecuteNonQuery();
                transaction.Commit();

                errmessage = "";

            }
            catch (SqlException se)
            {
                transaction.Rollback();

                if (se.Number == 2627)
                {
                    throw new ApplicationException("The record you are saving was already been saved or marked as deleted");
                }

                errmessage = se.Message;
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                errmessage = ex.Message;
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return errmessage;
        }


        public string DELETE_PARTS_LOCATION_PPD(string ID)
        {
            string error_message = "";
            SqlCommand cmd = new SqlCommand("DELETE_PARTS_LOCATION_PPD", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = ID;
            cmd.CommandTimeout = 360000;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return error_message;
        }

        public DataView GetLotListRefPPD(string refno)
        {
            return SqlHelper.ExecuteDataset(CONNECTION_STRING_FGWHSE, "GET_LOT_LIST_REF_PPD", refno).Tables[0].DefaultView;
        }

        //public DataView GetPartsStoringInquiryChild(string refno)
        //{
        //    return SqlHelper.ExecuteDataset(CONNECTION_STRING_FGWHSE, "PLC_STORING_INQUIRY_CHILDLOT", refno).Tables[0].DefaultView;
        //}

        public DataSet GetPartsStoringInquiryChild(string refno)
        {
            SqlCommand sqlComm = new SqlCommand("PLC_STORING_INQUIRY_CHILDLOT", conn);

            sqlComm.CommandType = CommandType.StoredProcedure;
            sqlComm.CommandTimeout = 0;

            sqlComm.Parameters.Add(new SqlParameter("@REFNO", SqlDbType.NVarChar)).Value = refno;


            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = sqlComm;

            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

        public DataView GetBypassApprovers()
        {
            return SqlHelper.ExecuteDataset(CONNECTION_STRING_FGWHSE, "PLC_STORING_INQUIRY_BYPASS_APPROVER").Tables[0].DefaultView;
        }

        public DataView SearchBypassApprovers(string employeeNo)
        { 
            return SqlHelper.ExecuteDataset(CONNECTION_STRING_FGWHSE, "SEARCH_BYPASS_APPROVER", employeeNo).Tables[0].DefaultView;
        }


    }


}
