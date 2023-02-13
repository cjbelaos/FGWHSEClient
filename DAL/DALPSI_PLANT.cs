using com.eppi.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace FGWHSEClient.DAL
{
    public class DALPSI_PLANT : BaseDAL
    {

        public DALPSI_PLANT()
        {

        }

        public DataSet PSI_UPLOAD_SHORTAGE_LIST_BY_PLANT(string strPLANT, string strColumns, string strColValue, string strFIRSTCOLUMNNAME, string strUID)
        {
            SqlCommand cmd = new SqlCommand("PSI_UPLOAD_SHORTAGE_LIST_BY_PLANT", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add(new SqlParameter("@PLANT", SqlDbType.NVarChar)).Value = strPLANT;
            cmd.Parameters.Add(new SqlParameter("@COLUMNNAMES", SqlDbType.NVarChar)).Value = strColumns;
            cmd.Parameters.Add(new SqlParameter("@COLUMNVALUE", SqlDbType.NVarChar)).Value = strColValue;
            cmd.Parameters.Add(new SqlParameter("@FIRSTCOLUMNNAME", SqlDbType.NVarChar)).Value = strFIRSTCOLUMNNAME;
            cmd.Parameters.Add(new SqlParameter("@UID", SqlDbType.NVarChar)).Value = strUID;


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


        public DataSet PSI_TRUNCATE_SHORTAGE_LIST_BY_PLANT(string strPLANT)
        {
            SqlCommand cmd = new SqlCommand("PSI_TRUNCATE_SHORTAGE_LIST_BY_PLANT", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add(new SqlParameter("@PLANT", SqlDbType.NVarChar)).Value = strPLANT;

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

        public DataTable PSI_GET_SHORTAGE_LIST_BY_PLANT(string strPLANT, string strSUPPLIERID)
        {
            SqlCommand cmd = new SqlCommand("PSI_GET_SHORTAGELIST_BY_PLANT", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PLANT", strPLANT);
            cmd.Parameters.AddWithValue("@SUPPLIERID", strSUPPLIERID);

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

        public DataTable PSI_GET_PLANTS_BY_USERID(string strUID)
        {
            SqlCommand cmd = new SqlCommand("PSI_GET_PLANTS_BY_USERID", conn);
            cmd.Parameters.AddWithValue("@UID", strUID);
            cmd.CommandType = CommandType.StoredProcedure;

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

        public DataTable PSI_GET_VENDORS_BY_USERID(string strUID)
        {
            SqlCommand cmd = new SqlCommand("PSI_GET_VENDORS_BY_USERID", conn);
            cmd.Parameters.AddWithValue("@UID", strUID);
            cmd.CommandType = CommandType.StoredProcedure;

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

        public DataTable PSI_GET_VENDORS_BY_PLANT(string strPLANT)
        {
            SqlCommand cmd = new SqlCommand("PSI_GET_VENDORS_BY_PLANT", conn);
            cmd.Parameters.AddWithValue("@PLANT", strPLANT);
            cmd.CommandType = CommandType.StoredProcedure;

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

        public DataTable PSI_GET_PARTSCODE_BY_PLANT_AND_VENDOR(string strPLANT, string strVENDORS)
        {
            SqlCommand cmd = new SqlCommand("PSI_GET_PARTSCODE_BY_PLANT_AND_VENDOR", conn);
            cmd.Parameters.AddWithValue("@PLANT", strPLANT);
            cmd.Parameters.AddWithValue("@VENDORS", strVENDORS);
            cmd.CommandType = CommandType.StoredProcedure;

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

        public void UploadTable(DataTable dt, string strFIRSTCOLUMNNAME, string strUID)
        {
            SqlCommand cmd = new SqlCommand("InsertSupplierPlanTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@SupplierPlanTable", dt));
            cmd.Parameters.Add(new SqlParameter("@FIRSTCOLUMNNAME", SqlDbType.NVarChar)).Value = strFIRSTCOLUMNNAME;
            cmd.Parameters.Add(new SqlParameter("@UID", SqlDbType.NVarChar)).Value = strUID;

            DataTable data = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                conn.Open();
                da.Fill(data);

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
        

        public DataTable PSI_GET_PARTSSIMULATION_BY_PLANT(string strPLANT, string strPARTCODE, string strSUPPLIERID)
        {
            SqlCommand cmd = new SqlCommand("PSI_GET_PARTSSIMULATION_BY_PLANT", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PLANT", strPLANT);
            cmd.Parameters.AddWithValue("@PARTCODE", strPARTCODE);
            cmd.Parameters.AddWithValue("@SUPPLIERID", strSUPPLIERID);
            cmd.CommandTimeout = 360000;

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

        public DataTable PSI_GET_PARTSSIMULATION_ASSEMBLY_BY_PLANT(string strPLANT, string strPARTCODE, string strSUPPLIERID)
        {
            SqlCommand cmd = new SqlCommand("PSI_GET_PARTSSIMULATION_ASSEMBLY_BY_PLANT", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PLANT", strPLANT);
            cmd.Parameters.AddWithValue("@PARTCODE", strPARTCODE);
            cmd.Parameters.AddWithValue("@SUPPLIERID", strSUPPLIERID);
            cmd.CommandTimeout = 360000;

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

        public DataTable PSI_GET_DOS_VENDOR_REPLY_BY_PLANT(string strPLANT, string strSUPPLIERID, string strPARTCODE)
        {
            SqlCommand cmd = new SqlCommand("PSI_GET_VENDOR_REPLY_BY_PLANT", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PLANT", strPLANT);
            cmd.Parameters.AddWithValue("@SUPPLIERID", strSUPPLIERID);
            cmd.Parameters.AddWithValue("@PARTCODE", strPARTCODE);
           
            cmd.CommandTimeout = 360000;

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

        public DataTable PSI_GET_VENDOR_REPLY_ASSEMBLY_BY_PLANT(string strPLANT, string strPARTCODE, string strSUPPLIERID)
        {
            SqlCommand cmd = new SqlCommand("PSI_GET_VENDOR_REPLY_ASSEMBLY_BY_PLANT", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PLANT", strPLANT);
            cmd.Parameters.AddWithValue("@PARTCODE", strPARTCODE);
            cmd.Parameters.AddWithValue("@SUPPLIERID", strSUPPLIERID);
            cmd.CommandTimeout = 360000;

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

        public string PSI_SAVE_VENDOR_REPLY_BY_PLANT(string strPLANT, string strVENDOR, string strPARTCODE, string strPROBLEMCATEGORY, 
            string strPERSONINCHARGE, string strREASON, string strCOUNTERMEASURE, string strUSERID)
        {
            string Message = "";

            SqlCommand cmd = new SqlCommand("PSI_SAVE_VENDOR_REPLY_BY_PLANT", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PLANT", strPLANT);
            cmd.Parameters.AddWithValue("@SUPPLIERID", strVENDOR);
            cmd.Parameters.AddWithValue("@PARTCODE", strPARTCODE);
            cmd.Parameters.AddWithValue("@PROBLEMCATEGORY", strPROBLEMCATEGORY);
            cmd.Parameters.AddWithValue("@PERSONINCHARGE", strPERSONINCHARGE);
            cmd.Parameters.AddWithValue("@REASON", strREASON);
            cmd.Parameters.AddWithValue("@COUNTERMEASURE", strCOUNTERMEASURE);
            cmd.Parameters.AddWithValue("@USERID", strUSERID);

            cmd.CommandTimeout = 360000;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (SqlException oe)
            {
                Message = oe.Message.ToString();
                Logger.GetInstance().Error(oe.Message);
                Logger.GetInstance().Error(oe.StackTrace);
                throw oe;
            }
            catch (Exception ex)
            {
                Message = ex.Message.ToString();
                Logger.GetInstance().Fatal(ex.Message);
                Logger.GetInstance().Fatal(ex.StackTrace);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return Message;
        }

        public string PSI_SAVE_PARTS_SIMULATION_BY_PLANT(string strPLANT, string strMATERIALNUMBER, string strMAINVENDOR, string strFIRSTCOLUMNNAME, string strPAST, string strDAY1, string strDAY2, string strDAY3, string strDAY4, string strDAY5, string strDAY6, string strDAY7, string strDAY8, string strDAY9, string strDAY10, string strDAY11, string strDAY12, string strDAY13, string strDAY14, string strDAY15, string strDAY16, string strDAY17, string strDAY18, string strDAY19, string strDAY20, string strDAY21, string strDAY22, string strDAY23, string strDAY24, string strDAY25, string strDAY26, string strDAY27, string strDAY28, string strDAY29, string strDAY30, string strDAY31, string strDAY32, string strDAY33, string strDAY34, string strDAY35, string strDAY36, string strDAY37, string strDAY38, string strDAY39, string strDAY40, string strDAY41, string strDAY42, string strDAY43, string strDAY44, string strDAY45, string strDAY46, string strDAY47, string strDAY48, string strDAY49, string strDAY50, string strDAY51, string strDAY52, string strDAY53, string strDAY54, string strDAY55, string strDAY56, string strDAY57, string strDAY58, string strDAY59, string strDAY60, string strDAY61, string strDAY62, string strDAY63, string strDAY64, string strDAY65, string strDAY66, string strDAY67, string strDAY68, string strDAY69, string strDAY70, string strDAY71, string strDAY72, string strDAY73, string strDAY74, string strDAY75, string strDAY76, string strDAY77, string strDAY78, string strDAY79, string strDAY80, string strDAY81, string strDAY82, string strDAY83, string strDAY84, string strDAY85, string strDAY86, string strDAY87, string strDAY88, string strDAY89, string strDAY90, string strDAY91, string strDAY92, string strDAY93, string strDAY94, string strDAY95, string strDAY96, string strDAY97, string strDAY98, string strDAY99, string strDAY100, string strDAY101, string strDAY102, string strDAY103, string strDAY104, string strDAY105, string strDAY106, string strDAY107, string strDAY108, string strDAY109, string strDAY110, string strDAY111, string strDAY112, string strDAY113, string strDAY114, string strDAY115, string strDAY116, string strDAY117, string strDAY118, string strDAY119, string strDAY120, string strDAY121, string strDAY122, string strDAY123, string strDAY124, string strDAY125, string strDAY126, string strDAY127, string strDAY128, string strDAY129, string strDAY130, string strDAY131, string strDAY132, string strDAY133, string strDAY134, string strDAY135, string strDAY136, string strDAY137, string strDAY138, string strDAY139, string strDAY140, string strDAY141, string strDAY142, string strDAY143, string strDAY144, string strDAY145, string strDAY146, string strDAY147, string strDAY148, string strDAY149, string strDAY150, string strDAY151, string strDAY152, string strDAY153, string strDAY154, string strDAY155, string strDAY156, string strDAY157, string strDAY158, string strDAY159, string strDAY160, string strDAY161, string strDAY162, string strDAY163, string strDAY164, string strDAY165, string strDAY166, string strDAY167, string strDAY168, string strDAY169, string strDAY170, string strDAY171, string strDAY172, string strDAY173, string strDAY174, string strDAY175, string strDAY176, string strDAY177, string strDAY178, string strDAY179, string strDAY180, string strDAY181, string strDAY182, string strDAY183, string strDAY184, string strDAY185, string strDAY186, string strDAY187, string strDAY188, string strDAY189, string strDAY190, string strDAY191, string strDAY192, string strDAY193, string strDAY194, string strDAY195, string strDAY196, string strDAY197, string strDAY198, string strUSER)
        {
            string Message = "";

            SqlCommand cmd = new SqlCommand("PSI_SAVE_PARTS_SIMULATION_BY_PLANT", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PLANT", strPLANT);
            cmd.Parameters.AddWithValue("@MATERIALNUMBER", strMATERIALNUMBER);
            cmd.Parameters.AddWithValue("@MAINVENDOR", strMAINVENDOR);
            cmd.Parameters.AddWithValue("@FIRSTCOLUMNNAME", strFIRSTCOLUMNNAME);
            cmd.Parameters.AddWithValue("@PAST", strPAST);
            cmd.Parameters.AddWithValue("@DAY1", strDAY1);
            cmd.Parameters.AddWithValue("@DAY2", strDAY2);
            cmd.Parameters.AddWithValue("@DAY3", strDAY3);
            cmd.Parameters.AddWithValue("@DAY4", strDAY4);
            cmd.Parameters.AddWithValue("@DAY5", strDAY5);
            cmd.Parameters.AddWithValue("@DAY6", strDAY6);
            cmd.Parameters.AddWithValue("@DAY7", strDAY7);
            cmd.Parameters.AddWithValue("@DAY8", strDAY8);
            cmd.Parameters.AddWithValue("@DAY9", strDAY9);
            cmd.Parameters.AddWithValue("@DAY10", strDAY10);
            cmd.Parameters.AddWithValue("@DAY11", strDAY11);
            cmd.Parameters.AddWithValue("@DAY12", strDAY12);
            cmd.Parameters.AddWithValue("@DAY13", strDAY13);
            cmd.Parameters.AddWithValue("@DAY14", strDAY14);
            cmd.Parameters.AddWithValue("@DAY15", strDAY15);
            cmd.Parameters.AddWithValue("@DAY16", strDAY16);
            cmd.Parameters.AddWithValue("@DAY17", strDAY17);
            cmd.Parameters.AddWithValue("@DAY18", strDAY18);
            cmd.Parameters.AddWithValue("@DAY19", strDAY19);
            cmd.Parameters.AddWithValue("@DAY20", strDAY20);
            cmd.Parameters.AddWithValue("@DAY21", strDAY21);
            cmd.Parameters.AddWithValue("@DAY22", strDAY22);
            cmd.Parameters.AddWithValue("@DAY23", strDAY23);
            cmd.Parameters.AddWithValue("@DAY24", strDAY24);
            cmd.Parameters.AddWithValue("@DAY25", strDAY25);
            cmd.Parameters.AddWithValue("@DAY26", strDAY26);
            cmd.Parameters.AddWithValue("@DAY27", strDAY27);
            cmd.Parameters.AddWithValue("@DAY28", strDAY28);
            cmd.Parameters.AddWithValue("@DAY29", strDAY29);
            cmd.Parameters.AddWithValue("@DAY30", strDAY30);
            cmd.Parameters.AddWithValue("@DAY31", strDAY31);
            cmd.Parameters.AddWithValue("@DAY32", strDAY32);
            cmd.Parameters.AddWithValue("@DAY33", strDAY33);
            cmd.Parameters.AddWithValue("@DAY34", strDAY34);
            cmd.Parameters.AddWithValue("@DAY35", strDAY35);
            cmd.Parameters.AddWithValue("@DAY36", strDAY36);
            cmd.Parameters.AddWithValue("@DAY37", strDAY37);
            cmd.Parameters.AddWithValue("@DAY38", strDAY38);
            cmd.Parameters.AddWithValue("@DAY39", strDAY39);
            cmd.Parameters.AddWithValue("@DAY40", strDAY40);
            cmd.Parameters.AddWithValue("@DAY41", strDAY41);
            cmd.Parameters.AddWithValue("@DAY42", strDAY42);
            cmd.Parameters.AddWithValue("@DAY43", strDAY43);
            cmd.Parameters.AddWithValue("@DAY44", strDAY44);
            cmd.Parameters.AddWithValue("@DAY45", strDAY45);
            cmd.Parameters.AddWithValue("@DAY46", strDAY46);
            cmd.Parameters.AddWithValue("@DAY47", strDAY47);
            cmd.Parameters.AddWithValue("@DAY48", strDAY48);
            cmd.Parameters.AddWithValue("@DAY49", strDAY49);
            cmd.Parameters.AddWithValue("@DAY50", strDAY50);
            cmd.Parameters.AddWithValue("@DAY51", strDAY51);
            cmd.Parameters.AddWithValue("@DAY52", strDAY52);
            cmd.Parameters.AddWithValue("@DAY53", strDAY53);
            cmd.Parameters.AddWithValue("@DAY54", strDAY54);
            cmd.Parameters.AddWithValue("@DAY55", strDAY55);
            cmd.Parameters.AddWithValue("@DAY56", strDAY56);
            cmd.Parameters.AddWithValue("@DAY57", strDAY57);
            cmd.Parameters.AddWithValue("@DAY58", strDAY58);
            cmd.Parameters.AddWithValue("@DAY59", strDAY59);
            cmd.Parameters.AddWithValue("@DAY60", strDAY60);
            cmd.Parameters.AddWithValue("@DAY61", strDAY61);
            cmd.Parameters.AddWithValue("@DAY62", strDAY62);
            cmd.Parameters.AddWithValue("@DAY63", strDAY63);
            cmd.Parameters.AddWithValue("@DAY64", strDAY64);
            cmd.Parameters.AddWithValue("@DAY65", strDAY65);
            cmd.Parameters.AddWithValue("@DAY66", strDAY66);
            cmd.Parameters.AddWithValue("@DAY67", strDAY67);
            cmd.Parameters.AddWithValue("@DAY68", strDAY68);
            cmd.Parameters.AddWithValue("@DAY69", strDAY69);
            cmd.Parameters.AddWithValue("@DAY70", strDAY70);
            cmd.Parameters.AddWithValue("@DAY71", strDAY71);
            cmd.Parameters.AddWithValue("@DAY72", strDAY72);
            cmd.Parameters.AddWithValue("@DAY73", strDAY73);
            cmd.Parameters.AddWithValue("@DAY74", strDAY74);
            cmd.Parameters.AddWithValue("@DAY75", strDAY75);
            cmd.Parameters.AddWithValue("@DAY76", strDAY76);
            cmd.Parameters.AddWithValue("@DAY77", strDAY77);
            cmd.Parameters.AddWithValue("@DAY78", strDAY78);
            cmd.Parameters.AddWithValue("@DAY79", strDAY79);
            cmd.Parameters.AddWithValue("@DAY80", strDAY80);
            cmd.Parameters.AddWithValue("@DAY81", strDAY81);
            cmd.Parameters.AddWithValue("@DAY82", strDAY82);
            cmd.Parameters.AddWithValue("@DAY83", strDAY83);
            cmd.Parameters.AddWithValue("@DAY84", strDAY84);
            cmd.Parameters.AddWithValue("@DAY85", strDAY85);
            cmd.Parameters.AddWithValue("@DAY86", strDAY86);
            cmd.Parameters.AddWithValue("@DAY87", strDAY87);
            cmd.Parameters.AddWithValue("@DAY88", strDAY88);
            cmd.Parameters.AddWithValue("@DAY89", strDAY89);
            cmd.Parameters.AddWithValue("@DAY90", strDAY90);
            cmd.Parameters.AddWithValue("@DAY91", strDAY91);
            cmd.Parameters.AddWithValue("@DAY92", strDAY92);
            cmd.Parameters.AddWithValue("@DAY93", strDAY93);
            cmd.Parameters.AddWithValue("@DAY94", strDAY94);
            cmd.Parameters.AddWithValue("@DAY95", strDAY95);
            cmd.Parameters.AddWithValue("@DAY96", strDAY96);
            cmd.Parameters.AddWithValue("@DAY97", strDAY97);
            cmd.Parameters.AddWithValue("@DAY98", strDAY98);
            cmd.Parameters.AddWithValue("@DAY99", strDAY99);
            cmd.Parameters.AddWithValue("@DAY100", strDAY100);
            cmd.Parameters.AddWithValue("@DAY101", strDAY101);
            cmd.Parameters.AddWithValue("@DAY102", strDAY102);
            cmd.Parameters.AddWithValue("@DAY103", strDAY103);
            cmd.Parameters.AddWithValue("@DAY104", strDAY104);
            cmd.Parameters.AddWithValue("@DAY105", strDAY105);
            cmd.Parameters.AddWithValue("@DAY106", strDAY106);
            cmd.Parameters.AddWithValue("@DAY107", strDAY107);
            cmd.Parameters.AddWithValue("@DAY108", strDAY108);
            cmd.Parameters.AddWithValue("@DAY109", strDAY109);
            cmd.Parameters.AddWithValue("@DAY110", strDAY110);
            cmd.Parameters.AddWithValue("@DAY111", strDAY111);
            cmd.Parameters.AddWithValue("@DAY112", strDAY112);
            cmd.Parameters.AddWithValue("@DAY113", strDAY113);
            cmd.Parameters.AddWithValue("@DAY114", strDAY114);
            cmd.Parameters.AddWithValue("@DAY115", strDAY115);
            cmd.Parameters.AddWithValue("@DAY116", strDAY116);
            cmd.Parameters.AddWithValue("@DAY117", strDAY117);
            cmd.Parameters.AddWithValue("@DAY118", strDAY118);
            cmd.Parameters.AddWithValue("@DAY119", strDAY119);
            cmd.Parameters.AddWithValue("@DAY120", strDAY120);
            cmd.Parameters.AddWithValue("@DAY121", strDAY121);
            cmd.Parameters.AddWithValue("@DAY122", strDAY122);
            cmd.Parameters.AddWithValue("@DAY123", strDAY123);
            cmd.Parameters.AddWithValue("@DAY124", strDAY124);
            cmd.Parameters.AddWithValue("@DAY125", strDAY125);
            cmd.Parameters.AddWithValue("@DAY126", strDAY126);
            cmd.Parameters.AddWithValue("@DAY127", strDAY127);
            cmd.Parameters.AddWithValue("@DAY128", strDAY128);
            cmd.Parameters.AddWithValue("@DAY129", strDAY129);
            cmd.Parameters.AddWithValue("@DAY130", strDAY130);
            cmd.Parameters.AddWithValue("@DAY131", strDAY131);
            cmd.Parameters.AddWithValue("@DAY132", strDAY132);
            cmd.Parameters.AddWithValue("@DAY133", strDAY133);
            cmd.Parameters.AddWithValue("@DAY134", strDAY134);
            cmd.Parameters.AddWithValue("@DAY135", strDAY135);
            cmd.Parameters.AddWithValue("@DAY136", strDAY136);
            cmd.Parameters.AddWithValue("@DAY137", strDAY137);
            cmd.Parameters.AddWithValue("@DAY138", strDAY138);
            cmd.Parameters.AddWithValue("@DAY139", strDAY139);
            cmd.Parameters.AddWithValue("@DAY140", strDAY140);
            cmd.Parameters.AddWithValue("@DAY141", strDAY141);
            cmd.Parameters.AddWithValue("@DAY142", strDAY142);
            cmd.Parameters.AddWithValue("@DAY143", strDAY143);
            cmd.Parameters.AddWithValue("@DAY144", strDAY144);
            cmd.Parameters.AddWithValue("@DAY145", strDAY145);
            cmd.Parameters.AddWithValue("@DAY146", strDAY146);
            cmd.Parameters.AddWithValue("@DAY147", strDAY147);
            cmd.Parameters.AddWithValue("@DAY148", strDAY148);
            cmd.Parameters.AddWithValue("@DAY149", strDAY149);
            cmd.Parameters.AddWithValue("@DAY150", strDAY150);
            cmd.Parameters.AddWithValue("@DAY151", strDAY151);
            cmd.Parameters.AddWithValue("@DAY152", strDAY152);
            cmd.Parameters.AddWithValue("@DAY153", strDAY153);
            cmd.Parameters.AddWithValue("@DAY154", strDAY154);
            cmd.Parameters.AddWithValue("@DAY155", strDAY155);
            cmd.Parameters.AddWithValue("@DAY156", strDAY156);
            cmd.Parameters.AddWithValue("@DAY157", strDAY157);
            cmd.Parameters.AddWithValue("@DAY158", strDAY158);
            cmd.Parameters.AddWithValue("@DAY159", strDAY159);
            cmd.Parameters.AddWithValue("@DAY160", strDAY160);
            cmd.Parameters.AddWithValue("@DAY161", strDAY161);
            cmd.Parameters.AddWithValue("@DAY162", strDAY162);
            cmd.Parameters.AddWithValue("@DAY163", strDAY163);
            cmd.Parameters.AddWithValue("@DAY164", strDAY164);
            cmd.Parameters.AddWithValue("@DAY165", strDAY165);
            cmd.Parameters.AddWithValue("@DAY166", strDAY166);
            cmd.Parameters.AddWithValue("@DAY167", strDAY167);
            cmd.Parameters.AddWithValue("@DAY168", strDAY168);
            cmd.Parameters.AddWithValue("@DAY169", strDAY169);
            cmd.Parameters.AddWithValue("@DAY170", strDAY170);
            cmd.Parameters.AddWithValue("@DAY171", strDAY171);
            cmd.Parameters.AddWithValue("@DAY172", strDAY172);
            cmd.Parameters.AddWithValue("@DAY173", strDAY173);
            cmd.Parameters.AddWithValue("@DAY174", strDAY174);
            cmd.Parameters.AddWithValue("@DAY175", strDAY175);
            cmd.Parameters.AddWithValue("@DAY176", strDAY176);
            cmd.Parameters.AddWithValue("@DAY177", strDAY177);
            cmd.Parameters.AddWithValue("@DAY178", strDAY178);
            cmd.Parameters.AddWithValue("@DAY179", strDAY179);
            cmd.Parameters.AddWithValue("@DAY180", strDAY180);
            cmd.Parameters.AddWithValue("@DAY181", strDAY181);
            cmd.Parameters.AddWithValue("@DAY182", strDAY182);
            cmd.Parameters.AddWithValue("@DAY183", strDAY183);
            cmd.Parameters.AddWithValue("@DAY184", strDAY184);
            cmd.Parameters.AddWithValue("@DAY185", strDAY185);
            cmd.Parameters.AddWithValue("@DAY186", strDAY186);
            cmd.Parameters.AddWithValue("@DAY187", strDAY187);
            cmd.Parameters.AddWithValue("@DAY188", strDAY188);
            cmd.Parameters.AddWithValue("@DAY189", strDAY189);
            cmd.Parameters.AddWithValue("@DAY190", strDAY190);
            cmd.Parameters.AddWithValue("@DAY191", strDAY191);
            cmd.Parameters.AddWithValue("@DAY192", strDAY192);
            cmd.Parameters.AddWithValue("@DAY193", strDAY193);
            cmd.Parameters.AddWithValue("@DAY194", strDAY194);
            cmd.Parameters.AddWithValue("@DAY195", strDAY195);
            cmd.Parameters.AddWithValue("@DAY196", strDAY196);
            cmd.Parameters.AddWithValue("@DAY197", strDAY197);
            cmd.Parameters.AddWithValue("@DAY198", strDAY198);
            cmd.Parameters.AddWithValue("@USERID", strUSER);

            cmd.CommandTimeout = 360000;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (SqlException oe)
            {
                Message = oe.Message.ToString();
                Logger.GetInstance().Error(oe.Message);
                Logger.GetInstance().Error(oe.StackTrace);
                throw oe;
            }
            catch (Exception ex)
            {
                Message = ex.Message.ToString();
                Logger.GetInstance().Fatal(ex.Message);
                Logger.GetInstance().Fatal(ex.StackTrace);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return Message;
        }

        public string PSI_SAVE_BOM_ASSEMBLY_BY_PLANT(string strPLANT, string strFG_PARENT, string strFG_PARENT_SUPPLIERCODE, string strPARENT_SUPPLIERCODE, string strPARENT_LEVEL, string strASSY_LEVEL, string strPARENT_ITEMCODE, string strCHILD_LEVEL, string strCHILD_ITEMCODE, string strUSAGE, string strCHILD_SUPPLIERCODE, string strVALID_FROM, string strVALID_TO, string strDOS_LEVEL, string strUSER)
        {
            string Message = "";

            SqlCommand cmd = new SqlCommand("PSI_SAVE_BOM_ASSEMBLY_BY_PLANT", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Plant", strPLANT);
            cmd.Parameters.AddWithValue("@FG_Parent", strFG_PARENT);
            cmd.Parameters.AddWithValue("@FG_Parent_SupplierCode", strFG_PARENT_SUPPLIERCODE);
            cmd.Parameters.AddWithValue("@Parent_SupplierCode", strPARENT_SUPPLIERCODE);
            cmd.Parameters.AddWithValue("@Parent_Level", strPARENT_LEVEL);
            cmd.Parameters.AddWithValue("@Assy_Level", strASSY_LEVEL);
            cmd.Parameters.AddWithValue("@Parent_ItemCode", strPARENT_ITEMCODE);
            cmd.Parameters.AddWithValue("@Child_Level", strCHILD_LEVEL);
            cmd.Parameters.AddWithValue("@Child_ItemCode", strCHILD_ITEMCODE);
            cmd.Parameters.AddWithValue("@Usage", strUSAGE);
            cmd.Parameters.AddWithValue("@Child_SupplierCode", strCHILD_SUPPLIERCODE);
            cmd.Parameters.AddWithValue("@Valid_From", strVALID_FROM);
            cmd.Parameters.AddWithValue("@Valid_To", strVALID_TO);
            cmd.Parameters.AddWithValue("@DOS_Level", strDOS_LEVEL);
            cmd.Parameters.AddWithValue("@User", strUSER);

            cmd.CommandTimeout = 360000;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (SqlException oe)
            {
                Message = oe.Message.ToString();
                Logger.GetInstance().Error(oe.Message);
                Logger.GetInstance().Error(oe.StackTrace);
                throw oe;
            }
            catch (Exception ex)
            {
                Message = ex.Message.ToString();
                Logger.GetInstance().Fatal(ex.Message);
                Logger.GetInstance().Fatal(ex.StackTrace);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return Message;
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

        public DataSet PSI_GET_DOS_BUYER_CONFIRM_BY_PLANT(string strPLANT)
        {
            SqlCommand cmd = new SqlCommand("PSI_GET_DOS_BUYER_CONFIRM_BY_PLANT", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PLANT", strPLANT);
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

        public DataTable PSI_GET_UPLOADED_BOM_ASSEMBLY_BY_PLANT(string strPLANT, string strVENDOR)
        {
            SqlCommand cmd = new SqlCommand("PSI_GET_UPLOADED_BOM_ASSEMBLY_BY_PLANT", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PLANT", strPLANT);
            cmd.Parameters.AddWithValue("@SUPPLIERID", strVENDOR);
            cmd.CommandTimeout = 360000;

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

        public string SAVE_SUPPLIER_CATEGORY(string strVENDOR, string strCATEGORY, string strUSER)
        {
            string Message = "";

            SqlCommand cmd = new SqlCommand("SAVE_SUPPLIER_CATEGORY", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIERID", strVENDOR);
            cmd.Parameters.AddWithValue("@SUPPLIERCATEGORY", strCATEGORY);
            cmd.Parameters.AddWithValue("@USERID", strUSER);
            cmd.CommandTimeout = 360000;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (SqlException oe)
            {
                Message = oe.Message.ToString();
                Logger.GetInstance().Error(oe.Message);
                Logger.GetInstance().Error(oe.StackTrace);
                throw oe;
            }
            catch (Exception ex)
            {
                Message = ex.Message.ToString();
                Logger.GetInstance().Fatal(ex.Message);
                Logger.GetInstance().Fatal(ex.StackTrace);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return Message;
        }
    }
}