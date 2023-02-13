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

/// <summary>
/// Summary description for Maintenance
/// </summary>

public class Maintenance
{
    private string strConn;
    SqlConnection conn;
    private string strConnLogin;
    SqlConnection connLogin;
    private string strConnCurrElog;
    SqlConnection ConnCurrElog;
    private string strConnHstryElog;
    SqlConnection ConnHstryElog;
    // NO NEED NA SIGURO MAGDECLARE NG STRING NA MINSAN MO LANG GAGAMITIN. XD
    SqlConnection ConnNAMES;

    //luwis B.
    public Maintenance()
    {
        this.strConn = System.Configuration.ConfigurationManager.AppSettings["FGWHSE_ConnectionString"];
        conn = new SqlConnection(strConn);

        this.strConnLogin = System.Configuration.ConfigurationManager.AppSettings["connLogin"];
        connLogin = new SqlConnection(strConnLogin);

        this.strConnHstryElog = System.Configuration.ConfigurationManager.AppSettings["ELOGHistory_ConnectionString"];
        ConnHstryElog = new SqlConnection(strConnHstryElog);

        this.strConnCurrElog = System.Configuration.ConfigurationManager.AppSettings["ELOGCurrentmm_ConnectionString"];
        ConnCurrElog = new SqlConnection(strConnCurrElog);

        ConnNAMES = new SqlConnection
            (System.Configuration.ConfigurationManager.AppSettings["NAMES_ConnectionString"]);
    }

    // GET DATA

    public int ADD_RFIDMASTER(
    string RFIDTAG,
    string EPCDATA,
    string SUPPLIER,
    string CREATEDBY
    )
    {
        try
        {
            object oAdd = SqlHelper.ExecuteScalar
             (this.strConn, "RFID_UPLOADRFIDMASTER",
                 RFIDTAG,
                 EPCDATA,
                 SUPPLIER,
                 CREATEDBY
             );
            return 1;
        }
        catch (SqlException ex)
        {
            return ex.Number;
        }
    }

    public DataView GETSUPPLIER(string strSupplier)
    {
        return SqlHelper.ExecuteDataset(this.strConn, "RFID_GETSUPPLIER", strSupplier).Tables[0].DefaultView;
    }

    public DataView GET_RFIDMASTER(string strRFIDTAG, string strSUPPLIER)
    {
        return SqlHelper.ExecuteDataset(this.strConn, "RFID_GETRFIDMASTER", strRFIDTAG, strSUPPLIER).Tables[0].DefaultView;
    }

    public DataView GETRFIDCOUNT()
    {
        return SqlHelper.ExecuteDataset(this.strConn, "RFID_GETCOUNT").Tables[0].DefaultView;
    }
    public DataSet LOGIN_SUPPLIER(string strUsername, string strPassword)
    {

        SqlCommand cmd = new SqlCommand("LOGIN_SUPPLIER", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@userid", SqlDbType.NVarChar)).Value = strUsername;
        cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar)).Value = strPassword;

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

    //public DataView LOGIN_SUPPLIER(string strUsername, string strPassword)
    //{
    //    return SqlHelper.ExecuteDataset(this.strConn, "LOGIN_SUPPLIER", strUsername, strPassword).Tables[0].DefaultView;
    //}

    public DataView GetUsersLDAP(string strUsername, string strSystemName)
    {
        return SqlHelper.ExecuteDataset(this.strConnLogin, "LOGIN_GetUsersLDAP", strUsername, strSystemName).Tables[0].DefaultView;
    }
    public DataView GetUser(string system_name, string user_id, string password, int ldap)
    {
        return SqlHelper.ExecuteDataset(this.strConnLogin, "LOGIN_GetUser", system_name, user_id, password, ldap).Tables[0].DefaultView;
    }
    public DataView GetUsersSubsystems(string strUsername, string strSystemName)
    {
        return SqlHelper.ExecuteDataset(this.strConnLogin, "LOGIN_GetUsersSubsystems", strUsername, strSystemName).Tables[0].DefaultView;
    }
    public DataView CheckLocationIDMaster(string strLocationID)
    {
        return SqlHelper.ExecuteDataset(this.strConn, "CHECK_LOCATION_ID_MASTER", strLocationID).Tables[0].DefaultView;
    }
    public DataView CheckPalletIDEPASS(string strLocationID)
    {
        return SqlHelper.ExecuteDataset(this.strConn, "CHECK_PALLET_ID_EPASS", strLocationID).Tables[0].DefaultView;
    }
    public DataView CheckContainerAllocation(string strLocationID)
    {
        return SqlHelper.ExecuteDataset(this.strConn, "CHECK_CONTAINER_ALLOCATION", strLocationID).Tables[0].DefaultView;
    }
    public DataView CheckLocationTypeMaster(string strLocationID)
    {
        return SqlHelper.ExecuteDataset(this.strConn, "CHECK_LOCATION_TYPE_MASTER", strLocationID).Tables[0].DefaultView;
    }
    public DataView CheckPalletAllocation(string strLocationID, string strPalletNo)
    {
        return SqlHelper.ExecuteDataset(this.strConn, "CHECK_PALLET_ALLOCATION", strLocationID, strPalletNo).Tables[0].DefaultView;
    }

    public DataView GetLocationMaster(string strWarehouseID)
    {
        return SqlHelper.ExecuteDataset(this.strConn, "GET_LOCATIONMASTER", strWarehouseID).Tables[0].DefaultView;
    }
    public DataView GetLocationMasterDetail(string strWarehouseID, string strLocationID)
    {
        return SqlHelper.ExecuteDataset(this.strConn, "GET_LOCATIONMASTER_DETAIL", strWarehouseID, strLocationID).Tables[0].DefaultView;
    }

    public DataView GetLocMasterLocType()
    {
        return SqlHelper.ExecuteDataset(this.strConn, "GET_LOCATIONMASTER_DROPDOWN").Tables[0].DefaultView;
    }

    public DataView GetLocMasterUnit()
    {
        return SqlHelper.ExecuteDataset(this.strConn, "GET_LOCATIONMASTER_DROPDOWN").Tables[1].DefaultView;
    }

    public DataView GetVanningLaneCount(string strLocationID)
    {
        return SqlHelper.ExecuteDataset(this.strConn, "GET_VANNINGLANE_COUNT", strLocationID).Tables[0].DefaultView;
    }

    public DataView CheckPalletContainer(string strContainerID)
    {
        return SqlHelper.ExecuteDataset(this.strConn, "CHECK_PALLET_CONTAINER", strContainerID).Tables[0].DefaultView;
    }

    public DataView InkCheckPalletExist(string strPalletNo)
    {
        return SqlHelper.ExecuteDataset(this.strConn, "INK_CHECK_PALLET_EXIST", strPalletNo).Tables[0].DefaultView;
    }

    public DataView InkCheckPalletContainer(string strContainerID, string strPalletNo)
    {
        return SqlHelper.ExecuteDataset(this.strConn, "INK_CHECK_PALLET_CONTAINER", strContainerID, strPalletNo).Tables[0].DefaultView;
    }

    public DataView InkCheckPalletDelivery(string strPalletNo)
    {
        return SqlHelper.ExecuteDataset(this.strConn, "INK_CHECK_PALLET_DELIVERY_EXPIRATION", strPalletNo).Tables[0].DefaultView;
    }

    public DataView InkCheckPalletExpiration(string strPalletNo)
    {
        return SqlHelper.ExecuteDataset(this.strConn, "INK_CHECK_PALLET_DELIVERY_EXPIRATION", strPalletNo).Tables[1].DefaultView;
    }

    public DataView InkCheckContainerLocation(string strContainerNo)
    {
        return SqlHelper.ExecuteDataset(this.strConn, "INK_CHECK_CONTAINER_LOCATION", strContainerNo).Tables[0].DefaultView;
    }

    public DataView InkCheckContainerDelivery(string strContainerNo)
    {
        return SqlHelper.ExecuteDataset(this.strConn, "INK_CHECK_CONTAINER_DELIVERY_EXPIRATION", strContainerNo).Tables[0].DefaultView;
    }

    public DataView InkCheckContainerExpiration(string strContainerNo)
    {
        return SqlHelper.ExecuteDataset(this.strConn, "INK_CHECK_CONTAINER_DELIVERY_EXPIRATION", strContainerNo).Tables[1].DefaultView;
    }

    public DataView InkCheckCartonInfo(string strPalletNo)
    {
        return SqlHelper.ExecuteDataset(this.strConn, "INK_CHECK_CARTON_INFO", strPalletNo).Tables[0].DefaultView;
    }

    public int InkSetOffExfact(string strContainerNo, string strUpdatedBy)
    {
        try
        {
            object oAdd = SqlHelper.ExecuteScalar(this.strConn, "INK_SETOFF_EXFACT", strContainerNo, strUpdatedBy);
            return 1;
        }
        catch (SqlException ex)
        {
            return ex.Number;
        }
    }

    public int InkReturnExfact(string strContainerNo, string strUpdatedBy)
    {
        try
        {
            object oAdd = SqlHelper.ExecuteScalar(this.strConn, "INK_RETURN_EXFACT", strContainerNo, strUpdatedBy);
            return 1;
        }
        catch (SqlException ex)
        {
            return ex.Number;
        }
    }

    
    public string DeleteMasterLocation(string strWHSE, string strLocationID)
    {
        string error_message = "";
        SqlCommand cmd = new SqlCommand("DELETE_MASTER_LOCATION", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@WHSEID", SqlDbType.VarChar, 20)).Value = strWHSE;
        cmd.Parameters.Add(new SqlParameter("@LocationID", SqlDbType.VarChar, 20)).Value = strLocationID;
        cmd.CommandTimeout = 360000;

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

        return error_message;
    }


    public string DeleteContainer(string strContainer)
    {
        string error_message = "";
        SqlCommand cmd = new SqlCommand("DELETE_CONTAINER", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ContainerID", SqlDbType.VarChar, 20)).Value = strContainer;
        cmd.CommandTimeout = 360000;

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

        return error_message;
    }


    public string DeleteReturned(string strContainer)
    {
        string error_message = "";
        SqlCommand cmd = new SqlCommand("DELETE_RETURNED", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ContainerID", SqlDbType.VarChar, 20)).Value = strContainer;
        cmd.CommandTimeout = 360000;

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

        return error_message;
    }


    //public DataView GetUser(string system_name, string user_id, string password, int ldap)
    //{
    //    SqlCommand cmd = new SqlCommand("LOGIN_GetUser", conn);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.Add(new SqlParameter("@system_name", SqlDbType.VarChar)).Value = system_name;
    //    cmd.Parameters.Add(new SqlParameter("@user_id", SqlDbType.VarChar)).Value = user_id;
    //    cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar)).Value = password;
    //    cmd.Parameters.Add(new SqlParameter("@LDAP", SqlDbType.Int)).Value = ldap;
    //    cmd.CommandTimeout = 360000;

    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    try
    //    {
    //        conn.Open();
    //        da.Fill(ds);
    //    }
    //    catch (SqlException sqlex)
    //    {
    //        throw sqlex;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        conn.Close();
    //    }
    //    return ds.Tables[0].DefaultView;
    //}


    public int AddOQAStatus(string strPalletNo)
    {
        try
        {
            object oAdd = SqlHelper.ExecuteScalar(this.strConn, "ADD_OQA_STATUS", strPalletNo);
            return 1;
        }
        catch (SqlException ex)
        {
            return ex.Number;
        }
    }

    public int InkUpdateODNumber(string strODNo, int iCaseNo, string strControlID, string strUpdatedBy)
    {
        try
        {
            object oAdd = SqlHelper.ExecuteScalar(this.strConn, "INK_UPDATE_OD_NUMBER",
                                                                 strODNo,
                                                                 iCaseNo,
                                                                 strControlID,
                                                                 strUpdatedBy);
            return 1;
        }
        catch (SqlException ex)
        {
            return ex.Number;
        }
    }


    public int InkUpdatePalletContainer(string strPalletNo, string strContainerNo, string strIsLoad, string strUpdatedBy)
    {
        try
        {
            object oAdd = SqlHelper.ExecuteScalar(this.strConn, "INK_UPDATE_PALLET_CONTAINER",
                                                                 strPalletNo,
                                                                 strContainerNo,
                                                                 strIsLoad,
                                                                 strUpdatedBy);
            return 1;
        }
        catch (SqlException ex)
        {
            return ex.Number;
        }
    }


    public int AddPalletAllocation(string strLocationID, string strPalletNo, string strUnitTypeID, string strUserID)
    {
        try
        {
            object oAdd = SqlHelper.ExecuteScalar(this.strConn, "ADD_PALLET_ALLOCATION",
                                                                 strLocationID,
                                                                 strPalletNo,
                                                                 strUnitTypeID,
                                                                 strUserID);
            return 1;
        }
        catch (SqlException ex)
        {
            return ex.Number;
        }
    }


    public int AddContainerAllocation(string strLoadingBay, string strContainerNo, string strUnitTypeID, string strUserID)
    {
        try
        {
            object oAdd = SqlHelper.ExecuteScalar(this.strConn, "ADD_CONTAINER_ALLOCATION",
                                                                 strLoadingBay,
                                                                 strContainerNo,
                                                                 strUnitTypeID,
                                                                 strUserID);
            return 1;
        }
        catch (SqlException ex)
        {
            return ex.Number;
        }
    }




    public int AddMasterLocation(string strWHSEID,
                                    string strLocationID, 
                                    string strLocationName, 
                                    string strLocationType, 
                                    string strDeleteFlag,
                                    string strUnitType,
                                    string iDisplayOrder,
                                    string iLines,
                                    string iRows,
                                    string strCreatedBy)
    {
        try
        {
            object oAdd = SqlHelper.ExecuteScalar(this.strConn, "ADD_MASTER_LOCATION",
                                                                 strWHSEID,
                                                                 strLocationID,
                                                                 strLocationName, 
                                                                 strLocationType, 
                                                                 strDeleteFlag,
                                                                 strUnitType,
                                                                 iDisplayOrder,
                                                                 iLines,
                                                                 iRows,
                                                                 strCreatedBy);
            return 1;
        }
        catch (SqlException ex)
        {
            return ex.Number;
        }
    }



    public DataSet GET_LOCATION_BY_LOCATION_TYPE_ID(string LocationTypeID, string WHID)
    {

        SqlCommand cmd = new SqlCommand("GET_LOCATION_BY_LOCATION_TYPE_ID", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@LocationTypeID", SqlDbType.NVarChar)).Value = LocationTypeID;
        cmd.Parameters.Add(new SqlParameter("@WHID", SqlDbType.NVarChar)).Value = WHID;
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



    public DataSet CHECK_PALLET_IF_ALREADY_EXISTS(string PalletNo)
    {

        SqlCommand cmd = new SqlCommand("CHECK_PALLET_IF_ALREADY_EXISTS", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PalletNo", SqlDbType.NVarChar)).Value = PalletNo;
       

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


    public DataSet CHECK_CONTAINER_IF_ALREADY_INTERFACED(string ContainerNo)
    {

        SqlCommand cmd = new SqlCommand("CHECK_CONTAINER_IF_ALREADY_INTERFACED", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ContainerNo", SqlDbType.NVarChar)).Value = ContainerNo;


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

    public DataSet GET_PALLETS_BY_LOCATIONID(string LoadingBayID)
    {

        SqlCommand cmd = new SqlCommand("GET_PALLETS_BY_LOCATIONID", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@LocationID", SqlDbType.NVarChar)).Value = LoadingBayID;

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


    public DataSet GET_PALLETS_BY_LOCATIONID_FILTERED(string LoadingBayID, string VALUE)
    {

        SqlCommand cmd = new SqlCommand("GET_PALLETS_BY_LOCATIONID_FILTERED", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@LocationID", SqlDbType.NVarChar)).Value = LoadingBayID;
        cmd.Parameters.Add(new SqlParameter("@VALUE", SqlDbType.NVarChar)).Value = VALUE;
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

    public DataSet GET_PALLETS_UNALLOCATED(string WH_ID)
    {

        SqlCommand cmd = new SqlCommand("GET_PALLETS_UNALLOCATED", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@WH_ID", SqlDbType.NVarChar)).Value = WH_ID;

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


    public DataSet GET_PALLETS_UNALLOCATED_FILTERED(string WH_ID, string value)
    {

        SqlCommand cmd = new SqlCommand("GET_PALLETS_UNALLOCATED_FILTERED", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@WH_ID", SqlDbType.NVarChar)).Value = WH_ID;
        cmd.Parameters.Add(new SqlParameter("@VALUE", SqlDbType.NVarChar)).Value = value;
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



    public DataSet GET_GET_WAREHOUSE()
    {

        SqlCommand cmd = new SqlCommand("GET_WAREHOUSE", conn);
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


    public DataSet GET_LOCATION_GROUP(string loctype)
    {

        SqlCommand cmd = new SqlCommand("GET_LOCATION_GROUP", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@loctype", SqlDbType.NVarChar)).Value = loctype;
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




    public DataSet GET_LOCATION_GROUP_LIST(string whid)
    {

        SqlCommand cmd = new SqlCommand("GET_LOCATION_GROUP_LIST", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@whid", SqlDbType.NVarChar)).Value = whid;
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




    public DataSet GET_CONTENT_LIST(string LocationTypeID, string WHID)
    {

        SqlCommand cmd = new SqlCommand("GET_CONTENT_LIST", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.Add(new SqlParameter("@loctype", SqlDbType.NVarChar)).Value = loctype;
        cmd.Parameters.Add(new SqlParameter("@LocationTypeID", SqlDbType.NVarChar)).Value = LocationTypeID;
        cmd.Parameters.Add(new SqlParameter("@WHID", SqlDbType.NVarChar)).Value = WHID;
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

    public DataSet GET_PACKAGE_LIST(string LocationTypeID, string WHID)
    {

        SqlCommand cmd = new SqlCommand("GET_PACKAGE_LIST", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.Add(new SqlParameter("@loctype", SqlDbType.NVarChar)).Value = loctype;
        cmd.Parameters.Add(new SqlParameter("@LocationTypeID", SqlDbType.NVarChar)).Value = LocationTypeID;
        cmd.Parameters.Add(new SqlParameter("@WHID", SqlDbType.NVarChar)).Value = WHID;
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





    public DataSet GET_ITEM_LIMIT_CONTROL(string partcode)
    {

        SqlCommand cmd = new SqlCommand("GET_ITEM_LIMIT_CONTROL", conn);
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





    public int ADD_ITEM_LIMIT_CONTROL(
        string part_code, 
        string description, 
        string warning_limit, 
        string delivery_limit, 
        string expiration_limit, 
        string user_id)
    {
        try
        {
            
            
            
            object oAdd = SqlHelper.ExecuteScalar
             (this.strConn, "ADD_ITEM_LIMIT_CONTROL",
                 part_code,
                 description,
                 warning_limit,
                 delivery_limit,
                 expiration_limit,
                 user_id
             );
            return 1;
        }
        catch (SqlException ex)
        {
            return ex.Number;
        }
    }


    public int ADD_CARTON_INFORMATION(
        string Pallet_No,
        string Part_Code,
        string Lot_No,
        string Qty,
        string user_id
        )
    {
        try
        {
            object oAdd = SqlHelper.ExecuteScalar
             (this.strConn, "ADD_CARTON_INFORMATION",
                 Pallet_No,
                 Part_Code,
                 Lot_No,
                 Qty,
                 user_id
             );
            return 1;
        }
        catch (SqlException ex)
        {
            return ex.Number;
        }
    }


    public string DELETE_ITEM_LIMIT_CONTROL(string PartCode)
    {
        string error_message = "";
        SqlCommand cmd = new SqlCommand("ITEM_LIMIT_CONTROL", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartCode", SqlDbType.VarChar)).Value = PartCode;
        cmd.CommandTimeout = 360000;

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close(); 

        return error_message;
    }


    public string DELETE_CARTON_INFORMATION(string Pallet_No)
    {
        string error_message = "";
        SqlCommand cmd = new SqlCommand("DELETE_CARTON_INFORMATION", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@Pallet_No", SqlDbType.VarChar)).Value = Pallet_No;
        cmd.CommandTimeout = 360000;

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

        return error_message;
    }





    public DataSet CHECK_IF_EXISTS_M_PART_LIMIT_CONTROL(string Part_Code)
    {

        SqlCommand cmd = new SqlCommand("CHECK_IF_EXISTS_M_PART_LIMIT_CONTROL", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@Part_Code", SqlDbType.NVarChar)).Value = Part_Code;
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




    public DataSet GET_SCANNED_CARTON_INFORMATION(string PALLET_NO)
    {

        SqlCommand cmd = new SqlCommand("GET_SCANNED_CARTON_INFORMATION", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.Add(new SqlParameter("@loctype", SqlDbType.NVarChar)).Value = loctype;
        cmd.Parameters.Add(new SqlParameter("@PALLET_NO", SqlDbType.NVarChar)).Value = PALLET_NO;
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

    public DataSet GET_PARTCODE_IFEXISTS_IN_DN(string strDNNo, string strPartCode)
    {

        SqlCommand cmd = new SqlCommand("GET_PARTCODE_IFEXISTS_IN_DN", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DNNo", SqlDbType.NVarChar)).Value = strDNNo;
        cmd.Parameters.Add(new SqlParameter("@PartCode", SqlDbType.NVarChar)).Value = strPartCode;
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

    //check role of user for DN DELETE FUNCTION
    //get role of user
    public DataSet GetUserSubsystemRole(string strUserID, string strSystemID, string strSubSystemID)
    {

        SqlCommand sqlComm = new SqlCommand("LOGIN_GetUsersSubsystemsRole", connLogin);
        sqlComm.Parameters.AddWithValue("@userid", strUserID);
        sqlComm.Parameters.AddWithValue("@system_name", strSystemID);
        sqlComm.Parameters.AddWithValue("@subsystem_id", strSubSystemID);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }

    public DataSet GetUserAuthentication(string strUserID, string strPassword)
    {

        SqlCommand sqlComm = new SqlCommand("GetUserAuthentication", connLogin);
        sqlComm.Parameters.AddWithValue("@userid", strUserID);
        sqlComm.Parameters.AddWithValue("@password", strPassword);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }

    //RECEIVE PD
    public string SaveReceivePD(String DSRefNo, String SerialNo, String Partcode, String qty, String CreatedBy)
    {
        string error_message = "";


        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }

        conn.Open();

        SqlCommand sqlComm = new SqlCommand("RECEIVE_PD", conn);
        sqlComm.CommandType = CommandType.StoredProcedure;


        sqlComm.Parameters.Add(new SqlParameter("@DSRefNo", SqlDbType.NVarChar)).Value = DSRefNo;
        sqlComm.Parameters.Add(new SqlParameter("@SerialNo", SqlDbType.NVarChar)).Value = SerialNo;
        sqlComm.Parameters.Add(new SqlParameter("@Partcode", SqlDbType.NVarChar)).Value = Partcode;
        sqlComm.Parameters.Add(new SqlParameter("@QTY", SqlDbType.Int)).Value = Convert.ToInt32(qty);
        sqlComm.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.NVarChar)).Value = CreatedBy;

        try
        {
            sqlComm.ExecuteNonQuery();
        }

        catch (Exception ex)
        {
            error_message = ex.Message;
            sqlComm.Transaction.Rollback();

        }

        finally
        {
            conn.Close();
        }

        return error_message;
    }

    public DataSet getPCodeDetails(String SerialNo)
    {

        SqlCommand sqlComm = new SqlCommand("GET_PCODE_DETAILS", conn);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;

        sqlComm.Parameters.Add(new SqlParameter("@SerialNo", SqlDbType.NVarChar)).Value = SerialNo;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }

    public DataSet getDSRefNoDetails(String DSRefNo)
    {

        SqlCommand sqlComm = new SqlCommand("GET_DSRefNo_Details", conn);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;

        sqlComm.Parameters.Add(new SqlParameter("@DSRefNo", SqlDbType.NVarChar)).Value = DSRefNo;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }

    public string UpdateReceivePD(string DSRefNo, string UpdatedBy)
    {
       
        int i = 0;
        string strMsg = "";

        SqlCommand cmd = new SqlCommand("UpdateReceivePD", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@DSRefNo", SqlDbType.NVarChar)).Value = DSRefNo;
        cmd.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.NVarChar)).Value = UpdatedBy;
        SqlTransaction transaction = null;
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







    //--------------FG WHSE-------------------------------------------------------------
    public DataSet CHECK_ROLE_VALIDITY(string RoleID)
    {

        SqlCommand cmd = new SqlCommand("CHECK_ROLE_VALIDITY", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RoleID", SqlDbType.NVarChar)).Value = RoleID;
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


    public DataSet PICKING_LOGIN_STATUS_USER(string USERID, string LOGGEDINROLE, string IPADDRESS)
    {

        SqlCommand cmd = new SqlCommand("PICKING_LOGIN_STATUS_USER", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@USERID", SqlDbType.NVarChar)).Value = USERID;
        cmd.Parameters.Add(new SqlParameter("@LOGGEDINROLE", SqlDbType.NVarChar)).Value = LOGGEDINROLE;
        cmd.Parameters.Add(new SqlParameter("@IPADDRESS", SqlDbType.NVarChar)).Value = IPADDRESS;

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


    public DataSet QA_GET_UPLOADED_PALLET(string LOCATIONTYPE)
    {

        SqlCommand cmd = new SqlCommand("QA_GET_UPLOADED_PALLET", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@LOCATIONTYPE", SqlDbType.NVarChar)).Value = LOCATIONTYPE;

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

    public DataSet QA_GET_PREJUDGE_TABLE()
    {

        SqlCommand cmd = new SqlCommand("QA_GET_PREJUDGE_TABLE", conn);
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


    public DataSet QA_GET_PREJUDGE_LANE()
    {

        SqlCommand cmd = new SqlCommand("QA_GET_PREJUDGE_LANE", conn);
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

    public DataView ALERT_NEWLY_POSTED_REVISED()
    {

        SqlCommand cmd = new SqlCommand("ALERT_NEWLY_POSTED_REVISED", conn);

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
        return ds.Tables[0].DefaultView;
    }




    #region Weekly Shipment Schedule

    public DataSet GET_WEEKLY_SHIPMENT_MAINTAINED_STATUS()
    {

        SqlCommand cmd = new SqlCommand("GET_WEEKLY_SHIPMENT_MAINTAINED_STATUS", conn);
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



    public DataSet UPLOAD_WEEKLY_SHIPMENT_SCHEDULE
    (
        string strGNSINVOICENUMBER,
        string strPRODUCTIONLOCATION,
        string strSHIPPINGLOCATION,
        string strODNO,
        string strLOTNO,
        string strSHIPMENTNO,
        string strPOMONTH,
        string strPONUMBER,
        string strCONSIGNEEPONUMBER,
        string strPOWK,
        string strITEMCODE,
        string strMODELDESCRIPTION,
        string strMODELNAME,
        string strPLANNEDEXFACTDATE,
        string strPLANNEDOBDATE,
        string strQTY,
        string strCUSTOMER,
        string strSHIMPENTMOD,
        string strPALLETTYPE,
        string strDESTINATION,
        string strCONTAINERQTYHC,
        string strCONTAINERQTY40FT,
        string strCONTAINERQTY20FT,
        string strNOOFPALLET,
        string strSTDCONTLOAD,
        string strCONTUSAGE,
        string strCONTUSAGERATIO,
        string strCAS,
        string strCTRNUMBER,
        //string strCTRNUMBERARRIVALDATE,
        string strCONFIRMEDDATE,
        string strSHIPPINGLIN,
        string str1STVESSEL,
        string str2NDVESSEL,
        string strVESSELDESTINATION,
        string strPORTOFDISCHARGE,
        string strETADISCHARGEPORT,
        string strCYCUTOFFEXFACTCUTOFF,
        string strLOADINGPORT,
        string strREASONOFDELAYEDEXFACT,
        string strREASONOFDELAYEDOB,
        string strREMARKS,
        string strTRADE,
        string strUserID,
        string strADDTOID
    )
    {

        SqlCommand cmd = new SqlCommand("UPLOAD_WEEKLY_SHIPMENT_SCHEDULE", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@GNSINVOICENUMBER", SqlDbType.NVarChar)).Value = strGNSINVOICENUMBER;
        cmd.Parameters.Add(new SqlParameter("@PRODUCTIONLOCATION", SqlDbType.NVarChar)).Value = strPRODUCTIONLOCATION;
        cmd.Parameters.Add(new SqlParameter("@SHIPPINGLOCATION", SqlDbType.NVarChar)).Value = strSHIPPINGLOCATION;
        cmd.Parameters.Add(new SqlParameter("@ODNO", SqlDbType.NVarChar)).Value = strODNO;
        cmd.Parameters.Add(new SqlParameter("@LOTNO", SqlDbType.NVarChar)).Value = strLOTNO;
        cmd.Parameters.Add(new SqlParameter("@SHIPMENTNO", SqlDbType.NVarChar)).Value = strSHIPMENTNO;
        cmd.Parameters.Add(new SqlParameter("@POMONTH", SqlDbType.NVarChar)).Value = strPOMONTH;
        cmd.Parameters.Add(new SqlParameter("@PONUMBER", SqlDbType.NVarChar)).Value = strPONUMBER;
        cmd.Parameters.Add(new SqlParameter("@CONSIGNEEPONUMBER", SqlDbType.NVarChar)).Value = strCONSIGNEEPONUMBER;
        cmd.Parameters.Add(new SqlParameter("@POWK", SqlDbType.NVarChar)).Value = strPOWK;
        cmd.Parameters.Add(new SqlParameter("@ITEMCODE", SqlDbType.NVarChar)).Value = strITEMCODE;
        cmd.Parameters.Add(new SqlParameter("@MODELDESCRIPTION", SqlDbType.NVarChar)).Value = strMODELDESCRIPTION;
        cmd.Parameters.Add(new SqlParameter("@MODELNAME", SqlDbType.NVarChar)).Value = strMODELNAME;
        cmd.Parameters.Add(new SqlParameter("@PLANNEDEXFACTDATE", SqlDbType.NVarChar)).Value = strPLANNEDEXFACTDATE;
        cmd.Parameters.Add(new SqlParameter("@PLANNEDOBDATE", SqlDbType.NVarChar)).Value = strPLANNEDOBDATE;
        cmd.Parameters.Add(new SqlParameter("@QTY", SqlDbType.NVarChar)).Value = strQTY;
        cmd.Parameters.Add(new SqlParameter("@CUSTOMER", SqlDbType.NVarChar)).Value = strCUSTOMER;
        cmd.Parameters.Add(new SqlParameter("@SHIMPENTMOD", SqlDbType.NVarChar)).Value = strSHIMPENTMOD;
        cmd.Parameters.Add(new SqlParameter("@PALLETTYPE", SqlDbType.NVarChar)).Value = strPALLETTYPE;
        cmd.Parameters.Add(new SqlParameter("@DESTINATION", SqlDbType.NVarChar)).Value = strDESTINATION;
        cmd.Parameters.Add(new SqlParameter("@CONTAINERQTYHC", SqlDbType.NVarChar)).Value = strCONTAINERQTYHC;
        cmd.Parameters.Add(new SqlParameter("@CONTAINERQTY40FT", SqlDbType.NVarChar)).Value = strCONTAINERQTY40FT;
        cmd.Parameters.Add(new SqlParameter("@CONTAINERQTY20FT", SqlDbType.NVarChar)).Value = strCONTAINERQTY20FT;
        cmd.Parameters.Add(new SqlParameter("@NOOFPALLET", SqlDbType.NVarChar)).Value = strNOOFPALLET;
        cmd.Parameters.Add(new SqlParameter("@STDCONTLOAD", SqlDbType.NVarChar)).Value = strSTDCONTLOAD;
        cmd.Parameters.Add(new SqlParameter("@CONTUSAGE", SqlDbType.NVarChar)).Value = strCONTUSAGE;
        cmd.Parameters.Add(new SqlParameter("@CONTUSAGERATIO", SqlDbType.NVarChar)).Value = strCONTUSAGERATIO;
        cmd.Parameters.Add(new SqlParameter("@CAS", SqlDbType.NVarChar)).Value = strCAS;
        cmd.Parameters.Add(new SqlParameter("@CTRNUMBER", SqlDbType.NVarChar)).Value = strCTRNUMBER;
        //cmd.Parameters.Add(new SqlParameter("@CTRNUMBERARRIVALDATE", SqlDbType.NVarChar)).Value = strCTRNUMBERARRIVALDATE;
        cmd.Parameters.Add(new SqlParameter("@CONFIRMEDDATE", SqlDbType.NVarChar)).Value = strCONFIRMEDDATE;
        cmd.Parameters.Add(new SqlParameter("@SHIPPINGLIN", SqlDbType.NVarChar)).Value = strSHIPPINGLIN;
        cmd.Parameters.Add(new SqlParameter("@1STVESSEL", SqlDbType.NVarChar)).Value = str1STVESSEL;
        cmd.Parameters.Add(new SqlParameter("@2NDVESSEL", SqlDbType.NVarChar)).Value = str2NDVESSEL;
        cmd.Parameters.Add(new SqlParameter("@VESSELDESTINATION", SqlDbType.NVarChar)).Value = strVESSELDESTINATION;
        cmd.Parameters.Add(new SqlParameter("@PORTOFDISCHARGE", SqlDbType.NVarChar)).Value = strPORTOFDISCHARGE;
        cmd.Parameters.Add(new SqlParameter("@ETADISCHARGEPORT", SqlDbType.NVarChar)).Value = strETADISCHARGEPORT;
        cmd.Parameters.Add(new SqlParameter("@CYCUTOFFEXFACTCUTOFF", SqlDbType.NVarChar)).Value = strCYCUTOFFEXFACTCUTOFF;
        cmd.Parameters.Add(new SqlParameter("@LOADINGPORT", SqlDbType.NVarChar)).Value = strLOADINGPORT;
        cmd.Parameters.Add(new SqlParameter("@REASONOFDELAYEDEXFACT", SqlDbType.NVarChar)).Value = strREASONOFDELAYEDEXFACT;
        cmd.Parameters.Add(new SqlParameter("@REASONOFDELAYEDOB", SqlDbType.NVarChar)).Value = strREASONOFDELAYEDOB;
        cmd.Parameters.Add(new SqlParameter("@REMARKS", SqlDbType.NVarChar)).Value = strREMARKS;
        cmd.Parameters.Add(new SqlParameter("@TRADE", SqlDbType.NVarChar)).Value = strTRADE;
        cmd.Parameters.Add(new SqlParameter("@USERID", SqlDbType.NVarChar)).Value = strUserID;
        cmd.Parameters.Add(new SqlParameter("@ADDTOID", SqlDbType.NVarChar)).Value = strADDTOID;
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


    public DataSet GET_WEEKLY_SHIPMENT_SCHEDULE_REVISION_HISTORY(string GNSINVOICENUMBER, string ODNO, string PONUMBER, string ITEMCODE, string OBDATE, string OBDATETO, string EXFACTDATE, string EXFACTDATETO, string STRStatus, string strDestination, string strTrade)
    {

        SqlCommand cmd = new SqlCommand("GET_WEEKLY_SHIPMENT_SCHEDULE_REVISION_HISTORY", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@GNSINVOICENUMBER", SqlDbType.NVarChar)).Value = GNSINVOICENUMBER;
        cmd.Parameters.Add(new SqlParameter("@ODNO", SqlDbType.NVarChar)).Value = ODNO;
        cmd.Parameters.Add(new SqlParameter("@PONUMBER", SqlDbType.NVarChar)).Value = PONUMBER;
        cmd.Parameters.Add(new SqlParameter("@ITEMCODE", SqlDbType.NVarChar)).Value = ITEMCODE;
        cmd.Parameters.Add(new SqlParameter("@OBDATE", SqlDbType.NVarChar)).Value = OBDATE;
        cmd.Parameters.Add(new SqlParameter("@OBDATETO", SqlDbType.NVarChar)).Value = OBDATETO;
        cmd.Parameters.Add(new SqlParameter("@EXFACTDATE", SqlDbType.NVarChar)).Value = EXFACTDATE;
        cmd.Parameters.Add(new SqlParameter("@EXFACTDATETO", SqlDbType.NVarChar)).Value = EXFACTDATETO;
        cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar)).Value = STRStatus;
        cmd.Parameters.Add(new SqlParameter("@Destination", SqlDbType.NVarChar)).Value = strDestination;
        cmd.Parameters.Add(new SqlParameter("@Trade", SqlDbType.NVarChar)).Value = strTrade;
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



    public DataSet GET_WEEKLY_SHIPMENT_SCHEDULE(string GNSINVOICENUMBER, string ODNO, string PONUMBER, string ITEMCODE, string OBDATE, string OBDATETO, string EXFACTDATE, string EXFACTDATETO, string STRStatus, string strDestination, string strContainerNo)
    {

        SqlCommand cmd = new SqlCommand("GET_WEEKLY_SHIPMENT_SCHEDULE", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@GNSINVOICENUMBER", SqlDbType.NVarChar)).Value = GNSINVOICENUMBER;
        cmd.Parameters.Add(new SqlParameter("@ODNO", SqlDbType.NVarChar)).Value = ODNO;
        cmd.Parameters.Add(new SqlParameter("@PONUMBER", SqlDbType.NVarChar)).Value = PONUMBER;
        cmd.Parameters.Add(new SqlParameter("@ITEMCODE", SqlDbType.NVarChar)).Value = ITEMCODE;
        cmd.Parameters.Add(new SqlParameter("@OBDATE", SqlDbType.NVarChar)).Value = OBDATE;
        cmd.Parameters.Add(new SqlParameter("@OBDATETO", SqlDbType.NVarChar)).Value = OBDATETO;
        cmd.Parameters.Add(new SqlParameter("@EXFACTDATE", SqlDbType.NVarChar)).Value = EXFACTDATE;
        cmd.Parameters.Add(new SqlParameter("@EXFACTDATETO", SqlDbType.NVarChar)).Value = EXFACTDATETO;
        cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar)).Value = STRStatus;
        cmd.Parameters.Add(new SqlParameter("@Destination", SqlDbType.NVarChar)).Value = strDestination;
        cmd.Parameters.Add(new SqlParameter("@ContainerNo", SqlDbType.NVarChar)).Value = strContainerNo;

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


    public DataSet POST_DELETE_WEEKLY_SHIPMENT_SCHEDULE(string ID, string POSTORDELETE, string Reason, string USERID)
    {

        SqlCommand cmd = new SqlCommand("POST_DELETE_WEEKLY_SHIPMENT_SCHEDULE", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.NVarChar)).Value = ID;
        cmd.Parameters.Add(new SqlParameter("@POSTORDELETE", SqlDbType.NVarChar)).Value = POSTORDELETE;
        cmd.Parameters.Add(new SqlParameter("@Reason", SqlDbType.NVarChar)).Value = Reason;
        cmd.Parameters.Add(new SqlParameter("@USERID", SqlDbType.NVarChar)).Value = USERID;
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



    public DataSet UPDATE_WEEKLY_SHIPMENT_SCHEDULE
    (
        string strID,
        string strGNSINVOICENUMBER,
        string strPRODUCTIONLOCATION,
        string strSHIPPINGLOCATION,
        string strODNO,
        string strLOTNO,
        string strSHIPMENTNO,
        string strPOMONTH,
        string strPONUMBER,
        string strCONSIGNEEPONUMBER,
        string strPOWK,
        string strITEMCODE,
        string strMODELDESCRIPTION,
        string strMODELNAME,
        string strPLANNEDEXFACTDATE,
        string strPLANNEDOBDATE,
        //string strREVISEDEXFACEDATE,
        //string strREVISEDOBDATE,
        string strQTY,
        string strCUSTOMER,
        string strSHIMPENTMOD,
        string strPALLETTYPE,
        string strDESTINATION,
        string strTRADE,
        string strCONTAINERQTYHC,
        string strCONTAINERQTY40FT,
        string strCONTAINERQTY20FT,
        string strNOOFPALLET,
        string strSTDCONTLOAD,
        string strCONTUSAGE,
        string strCONTUSAGERATIO,
        string strCAS,
        string strCTRNUMBER,
        string strCTRNUMBERARRIVALDATE,
        string strCONFIRMEDDATE,
        string strSHIPPINGLIN,
        string str1STVESSEL,
        string str2NDVESSEL,
        string strVESSELDESTINATION,
        string strPORTOFDISCHARGE,
        string strETADISCHARGEPORT,
        string strCYCUTOFFEXFACTCUTOFF,
        string strLOADINGPORT,
        string strREASONOFDELAYEDEXFACT,
        string strREASONOFDELAYEDOB,
        string strREMARKS,
        string strUSERID,
        string strUPDATETYPE
    )
    {

        SqlCommand cmd = new SqlCommand("UPDATE_WEEKLY_SHIPMENT_SCHEDULE", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.NVarChar)).Value = strID;
        cmd.Parameters.Add(new SqlParameter("@GNSINVOICENUMBER", SqlDbType.NVarChar)).Value = strGNSINVOICENUMBER;
        cmd.Parameters.Add(new SqlParameter("@PRODUCTIONLOCATION", SqlDbType.NVarChar)).Value = strPRODUCTIONLOCATION;
        cmd.Parameters.Add(new SqlParameter("@SHIPPINGLOCATION", SqlDbType.NVarChar)).Value = strSHIPPINGLOCATION;
        cmd.Parameters.Add(new SqlParameter("@ODNO", SqlDbType.NVarChar)).Value = strODNO;
        cmd.Parameters.Add(new SqlParameter("@LOTNO", SqlDbType.NVarChar)).Value = strLOTNO;
        cmd.Parameters.Add(new SqlParameter("@SHIPMENTNO", SqlDbType.NVarChar)).Value = strSHIPMENTNO;
        cmd.Parameters.Add(new SqlParameter("@POMONTH", SqlDbType.NVarChar)).Value = strPOMONTH;
        cmd.Parameters.Add(new SqlParameter("@PONUMBER", SqlDbType.NVarChar)).Value = strPONUMBER;
        cmd.Parameters.Add(new SqlParameter("@CONSIGNEEPONUMBER", SqlDbType.NVarChar)).Value = strCONSIGNEEPONUMBER;
        cmd.Parameters.Add(new SqlParameter("@POWK", SqlDbType.NVarChar)).Value = strPOWK;
        cmd.Parameters.Add(new SqlParameter("@ITEMCODE", SqlDbType.NVarChar)).Value = strITEMCODE;
        cmd.Parameters.Add(new SqlParameter("@MODELDESCRIPTION", SqlDbType.NVarChar)).Value = strMODELDESCRIPTION;
        cmd.Parameters.Add(new SqlParameter("@MODELNAME", SqlDbType.NVarChar)).Value = strMODELNAME;
        cmd.Parameters.Add(new SqlParameter("@PLANNEDEXFACTDATE", SqlDbType.NVarChar)).Value = strPLANNEDEXFACTDATE;
        cmd.Parameters.Add(new SqlParameter("@PLANNEDOBDATE", SqlDbType.NVarChar)).Value = strPLANNEDOBDATE;
        //cmd.Parameters.Add(new SqlParameter("@REVISEDEXFACEDATE", SqlDbType.NVarChar)).Value = strREVISEDEXFACEDATE;
        //cmd.Parameters.Add(new SqlParameter("@REVISEDOBDATE", SqlDbType.NVarChar)).Value = strREVISEDOBDATE;
        cmd.Parameters.Add(new SqlParameter("@QTY", SqlDbType.NVarChar)).Value = strQTY;
        cmd.Parameters.Add(new SqlParameter("@CUSTOMER", SqlDbType.NVarChar)).Value = strCUSTOMER;
        cmd.Parameters.Add(new SqlParameter("@SHIMPENTMOD", SqlDbType.NVarChar)).Value = strSHIMPENTMOD;
        cmd.Parameters.Add(new SqlParameter("@PALLETTYPE", SqlDbType.NVarChar)).Value = strPALLETTYPE;
        cmd.Parameters.Add(new SqlParameter("@DESTINATION", SqlDbType.NVarChar)).Value = strDESTINATION;
        cmd.Parameters.Add(new SqlParameter("@TRADE", SqlDbType.NVarChar)).Value = strTRADE;
        cmd.Parameters.Add(new SqlParameter("@CONTAINERQTYHC", SqlDbType.NVarChar)).Value = strCONTAINERQTYHC;
        cmd.Parameters.Add(new SqlParameter("@CONTAINERQTY40FT", SqlDbType.NVarChar)).Value = strCONTAINERQTY40FT;
        cmd.Parameters.Add(new SqlParameter("@CONTAINERQTY20FT", SqlDbType.NVarChar)).Value = strCONTAINERQTY20FT;
        cmd.Parameters.Add(new SqlParameter("@NOOFPALLET", SqlDbType.NVarChar)).Value = strNOOFPALLET;
        cmd.Parameters.Add(new SqlParameter("@STDCONTLOAD", SqlDbType.NVarChar)).Value = strSTDCONTLOAD;
        cmd.Parameters.Add(new SqlParameter("@CONTUSAGE", SqlDbType.NVarChar)).Value = strCONTUSAGE;
        cmd.Parameters.Add(new SqlParameter("@CONTUSAGERATIO", SqlDbType.NVarChar)).Value = strCONTUSAGERATIO;
        cmd.Parameters.Add(new SqlParameter("@CAS", SqlDbType.NVarChar)).Value = strCAS;
        cmd.Parameters.Add(new SqlParameter("@CTRNUMBER", SqlDbType.NVarChar)).Value = strCTRNUMBER;
        cmd.Parameters.Add(new SqlParameter("@CTRNUMBERARRIVALDATE", SqlDbType.NVarChar)).Value = strCTRNUMBERARRIVALDATE;
        cmd.Parameters.Add(new SqlParameter("@CONFIRMEDDATE", SqlDbType.NVarChar)).Value = strCONFIRMEDDATE;
        cmd.Parameters.Add(new SqlParameter("@SHIPPINGLIN", SqlDbType.NVarChar)).Value = strSHIPPINGLIN;
        cmd.Parameters.Add(new SqlParameter("@1STVESSEL", SqlDbType.NVarChar)).Value = str1STVESSEL;
        cmd.Parameters.Add(new SqlParameter("@2NDVESSEL", SqlDbType.NVarChar)).Value = str2NDVESSEL;
        cmd.Parameters.Add(new SqlParameter("@VESSELDESTINATION", SqlDbType.NVarChar)).Value = strVESSELDESTINATION;
        cmd.Parameters.Add(new SqlParameter("@PORTOFDISCHARGE", SqlDbType.NVarChar)).Value = strPORTOFDISCHARGE;
        cmd.Parameters.Add(new SqlParameter("@ETADISCHARGEPORT", SqlDbType.NVarChar)).Value = strETADISCHARGEPORT;
        cmd.Parameters.Add(new SqlParameter("@CYCUTOFFEXFACTCUTOFF", SqlDbType.NVarChar)).Value = strCYCUTOFFEXFACTCUTOFF;
        cmd.Parameters.Add(new SqlParameter("@LOADINGPORT", SqlDbType.NVarChar)).Value = strLOADINGPORT;
        cmd.Parameters.Add(new SqlParameter("@REASONOFDELAYEDEXFACT", SqlDbType.NVarChar)).Value = strREASONOFDELAYEDEXFACT;
        cmd.Parameters.Add(new SqlParameter("@REASONOFDELAYEDOB", SqlDbType.NVarChar)).Value = strREASONOFDELAYEDOB;
        cmd.Parameters.Add(new SqlParameter("@REMARKS", SqlDbType.NVarChar)).Value = strREMARKS;
        cmd.Parameters.Add(new SqlParameter("@USERID", SqlDbType.NVarChar)).Value = strUSERID;
        cmd.Parameters.Add(new SqlParameter("@UPDATETYPE", SqlDbType.NVarChar)).Value = strUPDATETYPE;
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

    public DataSet GET_WEEKLY_SHIPMENT_SCHEDULE_DUPLICATE_VALIDATION
    (
        string GNSINVOICENUMBER
    )
    {

        SqlCommand cmd = new SqlCommand("GET_WEEKLY_SHIPMENT_SCHEDULE_DUPLICATE_VALIDATION", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@GNSINVOICENUMBER", SqlDbType.NVarChar)).Value = GNSINVOICENUMBER;
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



    public DataSet GET_ITEM_CODE_VALIDATION
    (
        string ITEMCODE

    )
    {

        SqlCommand cmd = new SqlCommand("GET_ITEM_CODE_VALIDATION", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@ITEMCODE", SqlDbType.NVarChar)).Value = ITEMCODE;
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


    #endregion


    #region PALLET PICKING

    public DataSet PICKING_VANNING_POOL_PALLET_ALLOCATION(string CONTAINERNO, string PALLETNO, string USERID)
    {

        SqlCommand cmd = new SqlCommand("PICKING_VANNING_POOL_PALLET_ALLOCATION", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@CONTAINERNO", SqlDbType.NVarChar)).Value = CONTAINERNO;
        cmd.Parameters.Add(new SqlParameter("@PALLETNO", SqlDbType.NVarChar)).Value = PALLETNO;
        cmd.Parameters.Add(new SqlParameter("@USERID", SqlDbType.NVarChar)).Value = USERID;
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




    public DataSet PICKING_ADD_VANNING_POOL_ALLOCATION(string CONTAINERNO, string PALLETNO, string USERID, string ISLOAD)
    {

        SqlCommand cmd = new SqlCommand("PICKING_ADD_VANNING_POOL_ALLOCATION", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@LocationID", SqlDbType.NVarChar)).Value = CONTAINERNO;
        cmd.Parameters.Add(new SqlParameter("@PALLETNO", SqlDbType.NVarChar)).Value = PALLETNO;
        cmd.Parameters.Add(new SqlParameter("@USERID", SqlDbType.NVarChar)).Value = USERID;
        cmd.Parameters.Add(new SqlParameter("@IsLoad", SqlDbType.NVarChar)).Value = ISLOAD;
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



    public DataSet PICKING_VANNING_POOL_PALLET_UNALLOCATE(string CONTAINERNO, string PALLETNO, string USERID)
    {

        SqlCommand cmd = new SqlCommand("PICKING_VANNING_POOL_PALLET_UNALLOCATE", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@CONTAINERNO", SqlDbType.NVarChar)).Value = CONTAINERNO;
        cmd.Parameters.Add(new SqlParameter("@PALLETNO", SqlDbType.NVarChar)).Value = PALLETNO;
        cmd.Parameters.Add(new SqlParameter("@USERID", SqlDbType.NVarChar)).Value = USERID;
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

    public DataView PICKING_GET_ASSIGNED_PICKING_LIST(string USERID)
    {

        SqlCommand cmd = new SqlCommand("PICKING_GET_ASSIGNED_PICKING_LIST", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@USERID", SqlDbType.NVarChar)).Value = USERID;
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
        return ds.Tables[0].DefaultView;
    }


    public DataView PICKING_GET_ONGOING_PICKING_LIST(string PALLETUNITNO, string USERID)
    {

        SqlCommand cmd = new SqlCommand("PICKING_GET_ONGOING_PICKING_LIST", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PALLETUNITNO", SqlDbType.NVarChar)).Value = PALLETUNITNO;
        cmd.Parameters.Add(new SqlParameter("@USERID", SqlDbType.NVarChar)).Value = USERID;
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
        return ds.Tables[0].DefaultView;
    }


    public DataView PICKING_GET_ONGOING_PICKING_LIST_USERID(string USERID)
    {

        SqlCommand cmd = new SqlCommand("PICKING_GET_ONGOING_PICKING_LIST_USERID", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@USERID", SqlDbType.NVarChar)).Value = USERID;
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
        return ds.Tables[0].DefaultView;
    }



    public DataSet PICKING_CANCEL_ASSIGNED_PICKING(string PALLETUNITNO, string USERID)
    {

        SqlCommand cmd = new SqlCommand("PICKING_CANCEL_ASSIGNED_PICKING", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PALLETUNITNO", SqlDbType.NVarChar)).Value = PALLETUNITNO;
        cmd.Parameters.Add(new SqlParameter("@USERID", SqlDbType.NVarChar)).Value = USERID;
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


    public DataSet PICKING_BEGIN_PICKING(string PALLETUNITNO, string USERID)
    {

        SqlCommand cmd = new SqlCommand("PICKING_BEGIN_PICKING", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PALLETUNITNO", SqlDbType.NVarChar)).Value = PALLETUNITNO;
        cmd.Parameters.Add(new SqlParameter("@USERID", SqlDbType.NVarChar)).Value = USERID;
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



    public DataSet PICKING_DELIVER_PALLET(string PALLETUNITNO, string USERID)
    {

        SqlCommand cmd = new SqlCommand("PICKING_DELIVER_PALLET", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PALLETUNITNO", SqlDbType.NVarChar)).Value = PALLETUNITNO;
        cmd.Parameters.Add(new SqlParameter("@USERID", SqlDbType.NVarChar)).Value = USERID;
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



    public DataSet PICKING_PALLET(string PALLETUNITNO, string USERID)
    {

        SqlCommand cmd = new SqlCommand("PICKING_PALLET", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PALLETUNITNO", SqlDbType.NVarChar)).Value = PALLETUNITNO;
        cmd.Parameters.Add(new SqlParameter("@USERID", SqlDbType.NVarChar)).Value = USERID;
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


    public DataSet PICKING_GET_COMPLETION_RATE(string DATEFROM, string DATETO)
    {

        SqlCommand cmd = new SqlCommand("PICKING_GET_COMPLETION_RATE", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DATEFROM", SqlDbType.NVarChar)).Value = DATEFROM;
        cmd.Parameters.Add(new SqlParameter("@DATETO", SqlDbType.NVarChar)).Value = DATETO;
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

    #endregion

    public DataView GET_PALLET_SHIPMENT_COUNT(string ContainerNo)
    {

        SqlCommand cmd = new SqlCommand("GET_PALLET_SHIPMENT_COUNT", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ContainerNo", SqlDbType.NVarChar)).Value = ContainerNo;
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
        return ds.Tables[0].DefaultView;
    }

    public DataSet GET_PALLET_SHIPMENT_VALIDATION_SCANNED_COUNT(string ContainerNo)
    {

        SqlCommand cmd = new SqlCommand("GET_PALLET_SHIPMENT_VALIDATION_SCANNED_COUNT", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ContainerNo", SqlDbType.NVarChar)).Value = ContainerNo;
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


    public DataSet GET_PALLET_SHIPMENT_VALIDATION(string strContainerNo, string strODNo)
    {

        SqlCommand cmd = new SqlCommand("GET_PALLET_SHIPMENT_VALIDATION", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ContainerNo", SqlDbType.NVarChar)).Value = strContainerNo;
        cmd.Parameters.Add(new SqlParameter("@ODNo", SqlDbType.NVarChar)).Value = strODNo;
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


    public string DELETE_PALLET_SHIPMENT_VALIDATION(string strID)
    {
        string error_message = "";
        SqlCommand cmd = new SqlCommand("DELETE_PALLET_SHIPMENT_VALIDATION", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.VarChar)).Value = strID;
        cmd.CommandTimeout = 360000;

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

        return error_message;
    }

    public DataView PICKING_GET_SHIPMENT_STATUS_GRAPH(DateTime DateFrom, DateTime DateTo)
    {

        SqlCommand cmd = new SqlCommand("PICKING_GET_SHIPMENT_STATUS_GRAPH", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DATEFROM", SqlDbType.DateTime)).Value = DateFrom;
        cmd.Parameters.Add(new SqlParameter("@DATETO", SqlDbType.DateTime)).Value = DateTo;

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
        return ds.Tables[0].DefaultView;
    }

    public DataSet GET_WEEKLY_SHIPMENT_SCHEDULE_HISTORY(string GNSINVOICENUMBER, string ODNO, string SHIPMENTNO)
    {

        SqlCommand cmd = new SqlCommand("GET_WEEKLY_SHIPMENT_SCHEDULE_HISTORY", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@GNSINVOICENUMBER", SqlDbType.NVarChar)).Value = GNSINVOICENUMBER;
        cmd.Parameters.Add(new SqlParameter("@ODNO", SqlDbType.NVarChar)).Value = ODNO;
        cmd.Parameters.Add(new SqlParameter("@SHIPMENTNO", SqlDbType.NVarChar)).Value = SHIPMENTNO;

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


 
    public DataSet PICKING_USER_LOG_OUT(string USERID)
    {

        SqlCommand cmd = new SqlCommand("PICKING_USER_LOG_OUT", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@USERID", SqlDbType.NVarChar)).Value = USERID;

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

    public DataSet ADD_PALLET_SHIPMENT_VALIDATION(string ContainerNo, string PalletNo, string userID)
    {

        SqlCommand cmd = new SqlCommand("ADD_PALLET_SHIPMENT_VALIDATION", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ContainerNo", SqlDbType.NVarChar)).Value = ContainerNo;
        cmd.Parameters.Add(new SqlParameter("@PalletNo", SqlDbType.NVarChar)).Value = PalletNo;
        cmd.Parameters.Add(new SqlParameter("@userID", SqlDbType.NVarChar)).Value = userID;
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

    public DataSet CHECK_IF_PALLET_EXISTS(string PalletNo)
    {

        SqlCommand cmd = new SqlCommand("CHECK_IF_PALLET_EXISTS", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PalletNo", SqlDbType.NVarChar)).Value = PalletNo;
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

    public DataSet CHECK_IF_CONTAINER_EXISTS(string ContainerNo)
    {

        SqlCommand cmd = new SqlCommand("CHECK_IF_CONTAINER_EXISTS", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ContainerNo", SqlDbType.NVarChar)).Value = ContainerNo;
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



    public DataSet ADD_PALLET_SHIPMENT_VALIDATION_WITH_APPROVAL(string ContainerNo, string PalletNo, string userID, string Approver)
    {

        SqlCommand cmd = new SqlCommand("ADD_PALLET_SHIPMENT_VALIDATION_WITH_APPROVAL", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ContainerNo", SqlDbType.NVarChar)).Value = ContainerNo;
        cmd.Parameters.Add(new SqlParameter("@PalletNo", SqlDbType.NVarChar)).Value = PalletNo;
        cmd.Parameters.Add(new SqlParameter("@userID", SqlDbType.NVarChar)).Value = userID;
        cmd.Parameters.Add(new SqlParameter("@Approver", SqlDbType.NVarChar)).Value = Approver;
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
    #region Barcode Printing

    public DataView GET_CONTAINER_LABEL_DETAILS(string ODNoContainerNo)
    {

        SqlCommand cmd = new SqlCommand("GET_CONTAINER_LABEL_DETAILS", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ODCONTAINER", SqlDbType.NVarChar)).Value = ODNoContainerNo;
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
        return ds.Tables[0].DefaultView;
    }


    public DataView GET_LABEL_DETAILS_FOR_CONTAINER(string ContainerNo)
    {

        SqlCommand cmd = new SqlCommand("GET_LABEL_DETAILS_FOR_CONTAINER", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@CONTAINER", SqlDbType.NVarChar)).Value = ContainerNo;
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
        return ds.Tables[0].DefaultView;
    }


    #endregion


    #region AUTO PARTS INVENTORY

    //CHELLA - 04/04/2022

    public DataSet gvInventory(DateTime StartDate, DateTime EndDate, String Line, String Model, String ItemCode) //ELOG-EKANBAN PR 12
    {

        SqlCommand sqlComm = new SqlCommand("GetPartsInventory", ConnCurrElog);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;

        sqlComm.Parameters.Add(new SqlParameter("@txstart", SqlDbType.DateTime)).Value = StartDate;
        sqlComm.Parameters.Add(new SqlParameter("@txend", SqlDbType.DateTime)).Value = EndDate;
        sqlComm.Parameters.Add(new SqlParameter("@lineID", SqlDbType.NVarChar)).Value = Line;
        sqlComm.Parameters.Add(new SqlParameter("@modelID", SqlDbType.NVarChar)).Value = Model;
        sqlComm.Parameters.Add(new SqlParameter("@itemcode", SqlDbType.NVarChar)).Value = ItemCode;


        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }

    public DataSet gvInventoryLatest(DateTime StartDate, DateTime EndDate, String Line, String Model, String ItemCode) //ELOG-EKANBAN PR 12
    {

        SqlCommand sqlComm = new SqlCommand("GetAutoCountInventoryTransaction", ConnCurrElog);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;

        sqlComm.Parameters.Add(new SqlParameter("@txstart", SqlDbType.DateTime)).Value = StartDate;
        sqlComm.Parameters.Add(new SqlParameter("@txend", SqlDbType.DateTime)).Value = EndDate;
        sqlComm.Parameters.Add(new SqlParameter("@lineID", SqlDbType.NVarChar)).Value = Line;
        sqlComm.Parameters.Add(new SqlParameter("@modelID", SqlDbType.NVarChar)).Value = Model;
        sqlComm.Parameters.Add(new SqlParameter("@itemcode", SqlDbType.NVarChar)).Value = ItemCode;


        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }



    public DataSet gvPACGInventory(DateTime StartDate, DateTime EndDate, String Line, String Model, String ItemCode) //PR PD-PACG R142 12
    {

        SqlCommand sqlComm = new SqlCommand("GetR142PartsInventory", ConnCurrElog);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;

        sqlComm.Parameters.Add(new SqlParameter("@txstart", SqlDbType.DateTime)).Value = StartDate;
        sqlComm.Parameters.Add(new SqlParameter("@txend", SqlDbType.DateTime)).Value = EndDate;
        sqlComm.Parameters.Add(new SqlParameter("@lineID", SqlDbType.NVarChar)).Value = Line;
        sqlComm.Parameters.Add(new SqlParameter("@modelID", SqlDbType.NVarChar)).Value = Model;
        sqlComm.Parameters.Add(new SqlParameter("@itemcode", SqlDbType.NVarChar)).Value = ItemCode;


        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }

    public DataSet gvInventory13(DateTime StartDate, DateTime EndDate, String Line, String Model, String ItemCode) //ELOG-EKANBAN PR 13
    {

        SqlCommand sqlComm = new SqlCommand("GetPartsInventory", ConnHstryElog);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;

        sqlComm.Parameters.Add(new SqlParameter("@txstart", SqlDbType.DateTime)).Value = StartDate;
        sqlComm.Parameters.Add(new SqlParameter("@txend", SqlDbType.DateTime)).Value = EndDate;
        sqlComm.Parameters.Add(new SqlParameter("@lineID", SqlDbType.NVarChar)).Value = Line;
        sqlComm.Parameters.Add(new SqlParameter("@modelID", SqlDbType.NVarChar)).Value = Model;
        sqlComm.Parameters.Add(new SqlParameter("@itemcode", SqlDbType.NVarChar)).Value = ItemCode;


        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }



    public DataSet gvPACGInventory13(DateTime StartDate, DateTime EndDate, String Line, String Model, String ItemCode) //PR PD-PACG R142 13
    {

        SqlCommand sqlComm = new SqlCommand("GetR142PartsInventory", ConnHstryElog);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;

        sqlComm.Parameters.Add(new SqlParameter("@txstart", SqlDbType.DateTime)).Value = StartDate;
        sqlComm.Parameters.Add(new SqlParameter("@txend", SqlDbType.DateTime)).Value = EndDate;
        sqlComm.Parameters.Add(new SqlParameter("@lineID", SqlDbType.NVarChar)).Value = Line;
        sqlComm.Parameters.Add(new SqlParameter("@modelID", SqlDbType.NVarChar)).Value = Model;
        sqlComm.Parameters.Add(new SqlParameter("@itemcode", SqlDbType.NVarChar)).Value = ItemCode;


        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }

    //APISystemDisplay

    public DataSet GetEmployeeName(String EmployeeNo) //ScannedByDetails
    {

        SqlCommand sqlComm = new SqlCommand("GetEmployeeName", ConnCurrElog);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;


        sqlComm.Parameters.Add(new SqlParameter("@EmployeeNo", SqlDbType.NVarChar)).Value = EmployeeNo;


        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }
    public DataSet TotalElog(String Line) //ELOG-EKANBAN PR
    {

        SqlCommand sqlComm = new SqlCommand("GetPartsInventoryDisplay", ConnCurrElog);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;


        sqlComm.Parameters.Add(new SqlParameter("@line", SqlDbType.NVarChar)).Value = Line;


        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }

    public DataSet TotalPDPacg(String Line) //ELOG-EKANBAN PR
    {

        SqlCommand sqlComm = new SqlCommand("GetPartsInventoryDisplayR142", ConnCurrElog);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;


        sqlComm.Parameters.Add(new SqlParameter("@line", SqlDbType.NVarChar)).Value = Line;


        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }

    public DataSet TotalPDPacg2(String Line, String Partcode, DateTime StartDate) //ELOG-EKANBAN PR
    {

        SqlCommand sqlComm = new SqlCommand("GetPartsInventoryDisplayR142v2", ConnCurrElog);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;


        sqlComm.Parameters.Add(new SqlParameter("@line", SqlDbType.NVarChar)).Value = Line;
        sqlComm.Parameters.Add(new SqlParameter("@partcode", SqlDbType.NVarChar)).Value = Partcode;
        sqlComm.Parameters.Add(new SqlParameter("@date", SqlDbType.DateTime)).Value = StartDate;


        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }

    public DataSet TotalElog2(String Line, String Partcode, DateTime StartDate) //ELOG-EKANBAN PR
    {

        SqlCommand sqlComm = new SqlCommand("GetPartsInventoryDisplayv2", ConnCurrElog);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;


        sqlComm.Parameters.Add(new SqlParameter("@line", SqlDbType.NVarChar)).Value = Line;
        sqlComm.Parameters.Add(new SqlParameter("@partcode", SqlDbType.NVarChar)).Value = Partcode;
        sqlComm.Parameters.Add(new SqlParameter("@date", SqlDbType.DateTime)).Value = StartDate;


        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }

    public DataSet CheckPartCode(String Line, String Partcode) //ELOG-EKANBAN PR
    {

        SqlCommand sqlComm = new SqlCommand("CheckPartcodePerLine", ConnCurrElog);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;


        sqlComm.Parameters.Add(new SqlParameter("@line", SqlDbType.NVarChar)).Value = Line;
        sqlComm.Parameters.Add(new SqlParameter("@partcode", SqlDbType.NVarChar)).Value = Partcode;


        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }

    public DataSet CreateRefNo() //ELOG-EKANBAN PR
    {

        SqlCommand sqlComm = new SqlCommand("SP_Create_InventoryRefNo", ConnCurrElog);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }

    public string SaveStocks(string ReferenceNo, string user, string username, string PartCode, string Line, int StartStocks)
    {
        string error_message = "";


        if (ConnCurrElog.State == ConnectionState.Open)
        {
            ConnCurrElog.Close();
        }

        ConnCurrElog.Open();

        SqlCommand sqlComm = new SqlCommand("SP_SAVE_STOCKS", ConnCurrElog);
        sqlComm.CommandType = CommandType.StoredProcedure;

        sqlComm.Parameters.Add(new SqlParameter("@ReferenceNo", SqlDbType.NVarChar)).Value = ReferenceNo;
        sqlComm.Parameters.Add(new SqlParameter("@PartCode", SqlDbType.NVarChar)).Value = PartCode;
        sqlComm.Parameters.Add(new SqlParameter("@Line", SqlDbType.NVarChar)).Value = Line;
        sqlComm.Parameters.Add(new SqlParameter("@StartStocks", SqlDbType.Int)).Value = StartStocks;
        sqlComm.Parameters.Add(new SqlParameter("@user", SqlDbType.NVarChar)).Value = user;
        sqlComm.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar)).Value = username;


        try
        {
            sqlComm.ExecuteNonQuery();
        }

        catch (Exception ex)
        {
            error_message = ex.Message;
            sqlComm.Transaction.Rollback();

        }

        finally
        {
            ConnCurrElog.Close();
        }

        return error_message;


    }

    public DataSet getIventoryTransaction(String ReferenceNo) //ELOG-EKANBAN PR
    {

        SqlCommand sqlComm = new SqlCommand("GetInventoryTransaction", ConnCurrElog);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;


        sqlComm.Parameters.Add(new SqlParameter("@ReferenceNo", SqlDbType.NVarChar)).Value = ReferenceNo;


        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }

    public DataSet UpdateInventoryTrans(string ReferenceNo, int TotalPartsDel, int TotalPartsUsed, int EndStocks, int RemainingStocks, int Variance)
    {

        SqlCommand sqlComm = new SqlCommand("UpdateInventoryTransaction", ConnCurrElog);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;

        sqlComm.Parameters.Add(new SqlParameter("@ReferenceNo", SqlDbType.NVarChar)).Value = ReferenceNo;
        sqlComm.Parameters.Add(new SqlParameter("@TotalPartsDelivered", SqlDbType.Int)).Value = TotalPartsDel;
        sqlComm.Parameters.Add(new SqlParameter("@TotalPartsUsed", SqlDbType.Int)).Value = TotalPartsUsed;
        sqlComm.Parameters.Add(new SqlParameter("@EndOfShiftStocks", SqlDbType.Int)).Value = EndStocks;
        sqlComm.Parameters.Add(new SqlParameter("@RemainingStocks", SqlDbType.Int)).Value = RemainingStocks;
        sqlComm.Parameters.Add(new SqlParameter("@Variance", SqlDbType.Int)).Value = Variance;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;


    }

    #endregion

    #region PLC - Aug 09 2022
    public DataSet PLC_GetRFIDTag(string MotherLot)
    {

        SqlCommand cmd = new SqlCommand("PLC_GET_RFIDTAG", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MotherLot", SqlDbType.NVarChar)).Value = MotherLot;

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
    public string checkApprover(String user, String password)
    {
        
        SqlCommand sqlComm;
        SqlDataAdapter da;
        DataSet ds;

        // EXISTING BA SIYA SA ATING MAINTENANCE?
        sqlComm = new SqlCommand("Check_RFIDApprover", conn);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;

        sqlComm.Parameters.Add(new SqlParameter("@user", SqlDbType.NVarChar)).Value = user;
        
        da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        ds = new DataSet();
        da.Fill(ds);

        if(ds.Tables[0].Rows.Count > 0)
        {
            // EXISTING NGA...PERO TAMA BA ANG USERNAME AT PASSWORD
            
            //SqlConnection connNames = new SqlConnection();
            //connNames = connLogin;
            //connNames.ChangeDatabase("db_EPPIIIP_New");

            sqlComm = new SqlCommand("SPC_ADMIN_KIOSK_CHECK_EMPLOYEE_PASSWORD", ConnNAMES);

            sqlComm.CommandType = CommandType.StoredProcedure;
            sqlComm.CommandTimeout = 0;

            sqlComm.Parameters.Add(new SqlParameter("@EMPLOYEENO", SqlDbType.NVarChar)).Value = user;
            sqlComm.Parameters.Add(new SqlParameter("@PASSWORD", SqlDbType.NVarChar)).Value = password;

            da = new SqlDataAdapter();
            da.SelectCommand = sqlComm;

            ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return string.Empty;
            }
            else
            {
                // MALI TALAGA ANG PASSWORD...PERO SASABIHIN BA NATIN IYON? YES.
                return "You have entered a mismatched username and password!";
            }
        }
        else
        {
            // NOPE. NOT EXISTING. NOT REGISTERED. NO.
            return "User is not a registered approver!";
        }

    }

    public string insertBypass(string RefNo, string TypeOfBypass, int BypassQty, string Approver, string CreatedBy)
    {
        string error_message = "";


        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }

        conn.Open();

        SqlCommand sqlComm = new SqlCommand("Insert_BypassTransaction", conn);
        sqlComm.CommandType = CommandType.StoredProcedure;

        sqlComm.Parameters.Add(new SqlParameter("@RefNo", SqlDbType.NVarChar)).Value = RefNo;
        sqlComm.Parameters.Add(new SqlParameter("@TypeOfBypass", SqlDbType.NVarChar)).Value = TypeOfBypass;
        sqlComm.Parameters.Add(new SqlParameter("@BypassQty", SqlDbType.Int)).Value = BypassQty;
        sqlComm.Parameters.Add(new SqlParameter("@Approver", SqlDbType.NVarChar)).Value = Approver;
        sqlComm.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.NVarChar)).Value = CreatedBy;

        try
        {
            sqlComm.ExecuteNonQuery();
        }

        catch (Exception ex)
        {
            error_message = ex.Message;
            sqlComm.Transaction.Rollback();

        }

        finally
        {
            conn.Close();
        }

        return error_message;


    }

    public string insertBypassApprover(string EmployeeNo, string CreatedBy)
    {
        string error_message = "";


        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }

        conn.Open();

        SqlCommand sqlComm = new SqlCommand("Insert_BypassApprover", conn);
        sqlComm.CommandType = CommandType.StoredProcedure;

        sqlComm.Parameters.Add(new SqlParameter("@EmployeeNo", SqlDbType.NVarChar)).Value = EmployeeNo;
        sqlComm.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.NVarChar)).Value = CreatedBy;

        try
        {
            sqlComm.ExecuteNonQuery();
        }

        catch (Exception ex)
        {
            error_message = ex.Message;
            sqlComm.Transaction.Rollback();

        }

        finally
        {
            conn.Close();
        }

        return error_message;


    }


    public DataSet CheckScannedLocation(string PartCode, string LocationID)
    {
        SqlCommand sqlComm = new SqlCommand("PLC_CHECK_SCANNED_LOCATION", conn);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;

        sqlComm.Parameters.Add(new SqlParameter("@PARTCODE", SqlDbType.NVarChar)).Value = PartCode;
        sqlComm.Parameters.Add(new SqlParameter("@LOCATIONID", SqlDbType.NVarChar)).Value = LocationID;




        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }

    public DataSet CheckIfRefNoSavedInDB(string ReferenceNo)
    {
        SqlCommand sqlComm = new SqlCommand("PLC_CHECK_REFNO_ISSAVED", conn);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;
        sqlComm.Parameters.Add(new SqlParameter("@REFERENCENO", SqlDbType.NVarChar)).Value = ReferenceNo;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }

    public DataSet PLC_InsertChildDetails_LotList(string Refno,string RFIDTag,string LotNo,string PartCode,string QTY,string Remarks,string AreaID,string Createdby)
    {

        SqlCommand cmd = new SqlCommand("PLC_InsertChildDetails_LotList", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@REFNO", SqlDbType.NVarChar)).Value = Refno;
        cmd.Parameters.Add(new SqlParameter("@RFIDTAG", SqlDbType.NVarChar)).Value = RFIDTag;
        cmd.Parameters.Add(new SqlParameter("@LOTNO", SqlDbType.NVarChar)).Value = LotNo;
        cmd.Parameters.Add(new SqlParameter("@PARTCODE", SqlDbType.NVarChar)).Value = PartCode;
        cmd.Parameters.Add(new SqlParameter("@QTY", SqlDbType.NVarChar)).Value = QTY;
        cmd.Parameters.Add(new SqlParameter("@REMARKS", SqlDbType.NVarChar)).Value = Remarks;
        cmd.Parameters.Add(new SqlParameter("@AREAID", SqlDbType.NVarChar)).Value = AreaID;
        cmd.Parameters.Add(new SqlParameter("@CREATEDBY", SqlDbType.NVarChar)).Value = Createdby;


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

    public DataSet PLC_InsertChildDetails_ChildLotList(string Refno, string LotNo, string QTY,string Createdby)
    {

        SqlCommand cmd = new SqlCommand("PLC_InsertChildDetails_ChildLotList", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@REFNO", SqlDbType.NVarChar)).Value = Refno;
        cmd.Parameters.Add(new SqlParameter("@LOTNO", SqlDbType.NVarChar)).Value = LotNo;
        cmd.Parameters.Add(new SqlParameter("@QTY", SqlDbType.NVarChar)).Value = QTY;
        cmd.Parameters.Add(new SqlParameter("@CREATEDBY", SqlDbType.NVarChar)).Value = Createdby;


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

    

    public DataSet CheckBypassDeleteFlag(string ReferenceNo)
    {
        SqlCommand sqlComm = new SqlCommand("PLC_STORING_INQUIRY_BYPASS_APPROVER", conn);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;
        sqlComm.Parameters.Add(new SqlParameter("@REFERENCENO", SqlDbType.NVarChar)).Value = ReferenceNo;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }


    public DataSet CheckIfEmployeeNoIsExisting(string EmployeeNo)
    {
        SqlCommand sqlComm = new SqlCommand("CheckEmployeeNo", ConnNAMES);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;
        sqlComm.Parameters.Add(new SqlParameter("@EMPLOYEENO", SqlDbType.NVarChar)).Value = EmployeeNo;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }

    public DataSet DeleteBypassUser (string EmployeeNo, string UpdatedBy)
    {
        SqlCommand sqlComm = new SqlCommand("PLC_DeleteBypassUser", conn);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;
        sqlComm.Parameters.Add(new SqlParameter("@EMPLOYEENO", SqlDbType.NVarChar)).Value = EmployeeNo;
        sqlComm.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.NVarChar)).Value = UpdatedBy;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;
        //var result = sqlComm.ExecuteNonQuery();

        //try
        //{
        //    conn.Open();

        //}
        //catch (SqlException sqlex)
        //{
        //    throw sqlex;
        //}
        //finally
        //{
        //    conn.Close();
        //}

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;

    }


    public DataSet UpdateByPassUser(string EmployeeNo,string CreatedBy)
    {
        SqlCommand sqlComm = new SqlCommand("CheckEmployeeNo", ConnNAMES);

        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.CommandTimeout = 0;
        sqlComm.Parameters.Add(new SqlParameter("@EmployeeNo", SqlDbType.NVarChar)).Value = EmployeeNo;
        sqlComm.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.NVarChar)).Value = CreatedBy;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;

        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds;
    }

    #endregion
}
