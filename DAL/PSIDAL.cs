using com.eppi.utils;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FGWHSEClient.DAL
{
    public class PSIDAL : BaseDAL
    {
        public PSIDAL()
        {

        }

        public DataSet PSI_GET_SUPPLIER_PARTS_DETAILS(string strSUPPLIERID, string strPARTCODE)
        {
            SqlCommand cmd = new SqlCommand("PSI_GET_SUPPLIER_PARTS_DETAILS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@SUPPLIERID", SqlDbType.NVarChar, 16)).Value = strSUPPLIERID;
            cmd.Parameters.Add(new SqlParameter("@PARTCODE", SqlDbType.NVarChar, 16)).Value = strPARTCODE;

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



        public DataSet PSI_GET_SUPPLIER(string strSUPPLIERorSUPPLIERID)
        {
            SqlCommand cmd = new SqlCommand("PSI_GET_SUPPLIER", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@SUPPLIERNAMEORID", SqlDbType.NVarChar, 16)).Value = strSUPPLIERorSUPPLIERID;
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


        public void PSI_ADD_BASIS_MASTER_DETAILS
        (
              string strPLANT
            , string strMATERIALCODE
            , string strSUPPLIERCODE
            , string strSPQ
            , string strPARTSIZE
            , string strPARTTYPE
            , string strDOSLEVEL
            , string strCATEGORY
            , string strMODELCODE
            , string strDELIVERYTYPE
            , string strUSERID

        )
        {
            SqlCommand cmd = new SqlCommand("PSI_ADD_BASIS_MASTER_DETAILS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PLANT", SqlDbType.NVarChar)).Value = strPLANT;
            cmd.Parameters.Add(new SqlParameter("@MATERIALCODE", SqlDbType.NVarChar)).Value = strMATERIALCODE;
            cmd.Parameters.Add(new SqlParameter("@SUPPLIERCODE", SqlDbType.NVarChar)).Value = strSUPPLIERCODE;
            cmd.Parameters.Add(new SqlParameter("@SPQ", SqlDbType.NVarChar)).Value = strSPQ;
            cmd.Parameters.Add(new SqlParameter("@PARTSIZE", SqlDbType.NVarChar)).Value = strPARTSIZE;
            cmd.Parameters.Add(new SqlParameter("@PARTTYPE", SqlDbType.NVarChar)).Value = strPARTTYPE;
            cmd.Parameters.Add(new SqlParameter("@DOSLEVEL", SqlDbType.NVarChar)).Value = strDOSLEVEL;
            cmd.Parameters.Add(new SqlParameter("@CATEGORY", SqlDbType.NVarChar)).Value = strCATEGORY;
            cmd.Parameters.Add(new SqlParameter("@MODELCODE", SqlDbType.NVarChar)).Value = strMODELCODE;
            cmd.Parameters.Add(new SqlParameter("@DELIVERYTYPE", SqlDbType.NVarChar)).Value = strDELIVERYTYPE;
            cmd.Parameters.Add(new SqlParameter("@USERID", SqlDbType.NVarChar)).Value = strUSERID;

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




        public void PSI_DELETE_SUPPLIER_PARTS_DETAILS
        (
              string strPLANT
            , string strMATERIALCODE
            , string strSUPPLIERCODE

        )
        {
            SqlCommand cmd = new SqlCommand("PSI_DELETE_SUPPLIER_PARTS_DETAILS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PLANT", SqlDbType.NVarChar)).Value = strPLANT;
            cmd.Parameters.Add(new SqlParameter("@MATERIALCODE", SqlDbType.NVarChar)).Value = strMATERIALCODE;
            cmd.Parameters.Add(new SqlParameter("@SUPPLIERCODE", SqlDbType.NVarChar)).Value = strSUPPLIERCODE;
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

        public DataSet PSI_GET_SUPPLIER_STOCKS(string strPARTCODE, string strSUPPLIERID)
        {
            SqlCommand cmd = new SqlCommand("PSI_GET_SUPPLIER_STOCKS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@SUPPLIERID", SqlDbType.NVarChar, 16)).Value = strSUPPLIERID;
            cmd.Parameters.Add(new SqlParameter("@PARTCODE", SqlDbType.NVarChar, 16)).Value = strPARTCODE;

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



        public DataSet PSI_GET_SUPPLIERS_WITH_STOCKS()
        {
            SqlCommand cmd = new SqlCommand("PSI_GET_SUPPLIERS_WITH_STOCKS", conn);
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


        public DataSet PSI_UPLOAD_MANUAL_SUPPLIER_STOCKS(string strSUPPLIERID, string strITEMCODE, string strQTY,string strPROBLEMCATEGORY, string strREASON, string strPIC, string strCOUNTERMEASURE, string strUSERID)
        {
            SqlCommand cmd = new SqlCommand("PSI_UPLOAD_MANUAL_SUPPLIER_STOCKS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@SUPPLIERID", SqlDbType.NVarChar)).Value = strSUPPLIERID;
            cmd.Parameters.Add(new SqlParameter("@ITEMCODE", SqlDbType.NVarChar)).Value = strITEMCODE;
            cmd.Parameters.Add(new SqlParameter("@QTY", SqlDbType.NVarChar)).Value = strQTY;
            cmd.Parameters.Add(new SqlParameter("@PROBLEMCATEGORY", SqlDbType.NVarChar)).Value = strPROBLEMCATEGORY;
            cmd.Parameters.Add(new SqlParameter("@REASON", SqlDbType.NVarChar)).Value = strREASON;
            cmd.Parameters.Add(new SqlParameter("@PIC", SqlDbType.NVarChar)).Value = strPIC;
            cmd.Parameters.Add(new SqlParameter("@COUNTERMEASURE", SqlDbType.NVarChar)).Value = strCOUNTERMEASURE;

            cmd.Parameters.Add(new SqlParameter("@USERID", SqlDbType.NVarChar)).Value = strUSERID;

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


        public void PSI_DELETE_SUPPLIER_STOCKS(string strITEMCODE, string strSUPPLIERID)
        {
            SqlCommand cmd = new SqlCommand("PSI_DELETE_SUPPLIER_STOCKS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@SUPPLIERID", SqlDbType.NVarChar, 16)).Value = strSUPPLIERID;
            cmd.Parameters.Add(new SqlParameter("@ITEMCODE", SqlDbType.NVarChar, 16)).Value = strITEMCODE;

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

        public DataSet PSI_GET_PROBLEM_CATEGORY()
        {
            SqlCommand cmd = new SqlCommand("PSI_GET_PROBLEM_CATEGORY", conn);
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

        public DataTable GET_TBL_M_SUPPLIER_CATEGORY(string strVENDOR)
        {
            SqlCommand cmd = new SqlCommand("GET_TBL_M_SUPPLIER_CATEGORY", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SUPPLIERID", strVENDOR);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                conn.Open();
                da.Fill(dt);

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

            return dt;
        }

    }
}