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
    public class DNInquiryDAL : BaseDAL
    {
        public DNInquiryDAL()
        {

        }

        public DataSet GET_SUPPLIER_SCANNED_DN(string strDNNo)
        {
            SqlCommand cmd = new SqlCommand("GET_SUPPLIER_SCANNED_DN", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 360000;
            cmd.Parameters.Add(new SqlParameter("@BARCODEDNNO", SqlDbType.NVarChar, 20)).Value = strDNNo;
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
    }
}