using FGWHSEClient.Classes;
using FGWHSEClient.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;


namespace FGWHSEClient.Form
{
    public partial class PSI_DOS_VENDOR_REPLY_BY_PLANT : System.Web.UI.Page
    {
        static DALPSI_PLANT DPSI = new DALPSI_PLANT();
        static string UserID;
        static DataTable dtSupplierCode;
        static DataTable dtPlantCode;
        static DataSet dsProblemCategory;
        static bool isForViewing = false;
        public static DataTable dtDOSVendorReply;
        public static string VendorValue = "";
        public string strPageSubsystem = "";
        public string strPageSubsystem2 = "";
        public string strAccessLevel = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Write("<script>");
                    Response.Write("alert('Session expired! Please log in again.');");
                    Response.Write("window.location = '../Login.aspx';");
                    Response.Write("</script>");
                }
                else
                {
                    strPageSubsystem = "FGWHSE_040";
                    strPageSubsystem2 = "FGWHSE_062";
                    isForViewing = checkAuthority(strPageSubsystem2);
                    if (!checkAuthority(strPageSubsystem) && Session["UserName"].ToString() != "GUEST")
                    {
                        Response.Write("<script>");
                        Response.Write("alert('You are not authorized to access the page.');");
                        Response.Write("window.location = 'Default.aspx';");
                        Response.Write("</script>");
                    }
                }
                UserID = Session["UserID"].ToString();
                dtPlantCode = new DALPSI_PLANT().PSI_GET_PLANTS_BY_USERID(UserID);
                dtSupplierCode = new DALPSI_PLANT().PSI_GET_VENDORS_BY_USERID(UserID);
                dsProblemCategory = new DALPSI_PLANT().PSI_GET_PROBLEM_CATEGORY();
            }
            catch (Exception ex)
            {
                Response.Write("<script>");
                Response.Write("alert('" + ex.Message.ToString() + "');");
                Response.Write("</script>");
            }
        }

        private bool checkAuthority(string strPageSubsystem)
        {
            bool isValid = false;
            try
            {
                if (Session["Subsystem"] != null)
                {
                    DataView dvSubsystem = new DataView();
                    dvSubsystem = (DataView)Session["Subsystem"];

                    if (dvSubsystem.Count > 0)
                    {
                        dvSubsystem.Sort = "Subsystem";

                        int iRow = dvSubsystem.Find(strPageSubsystem);

                        if (iRow >= 0)
                        {
                            isValid = true;
                        }
                        else
                        {
                            isValid = false;
                        }

                        string strRole = dvSubsystem.Table.Rows[iRow]["Role"].ToString();

                        if (strRole != "")
                        {
                            strAccessLevel = strRole;
                        }

                    }
                }
                return isValid;
            }
            catch (Exception ex)
            {
                //Response.Write("<script>");
                //Response.Write("alert('" + ex.Message.ToString() + "');");
                //Response.Write("</script>");

                isValid = false;
                return isValid;
            }
        }

        [WebMethod]
        public static string CheckIfForViewing()
        {
            return JsonConvert.SerializeObject(isForViewing);
        }

        [WebMethod]
        public static string GetPlantsByUserId()
        {
            return JsonConvert.SerializeObject(dtPlantCode);
        }

        [WebMethod]
        public static string GetVendorsByUserId()
        {
            return JsonConvert.SerializeObject(dtSupplierCode);
        }

        [WebMethod]
        public static string GetProblemCategory()
        {
            return JsonConvert.SerializeObject(dsProblemCategory.Tables[0]);
        }

        [WebMethod]
        public static string GetPartsCodeByPlantandVendors(string strPLANT, string strVENDORS)
        {
            return JsonConvert.SerializeObject(DPSI.PSI_GET_PARTSCODE_BY_PLANT_AND_VENDOR(strPLANT, strVENDORS));
        }

        [WebMethod]
        public static string SaveDOSVendorReplyByPlant(string strPLANT, string strVENDOR, string strPARTCODE, string strPROBLEMCATEGORY,
            string strPERSONINCHARGE, string strREASON, string strCOUNTERMEASURE, string strUSERID)
        {
            return JsonConvert.SerializeObject(DPSI.PSI_SAVE_VENDOR_REPLY_BY_PLANT(strPLANT, strVENDOR, strPARTCODE, strPROBLEMCATEGORY, strPERSONINCHARGE, strREASON,
                strCOUNTERMEASURE, strUSERID));
        }

        [WebMethod]
        public static string GetDOSVendorReplyByPlant(string strPLANT, string strVENDORS, string strPARTS)
        {
            DataTable dt = DPSI.PSI_GET_DOS_VENDOR_REPLY_BY_PLANT(strPLANT, strVENDORS, strPARTS);
            
            if (dt.Rows.Count > 0)
            {
                var StartDate = dt.Rows[0][218].ToString();
                for (var i = 20; i < 218;)
                {
                    for (int j = 0; j < 198; j++)
                    {
                        if (i == 20)
                        {
                            dt.Columns[i].ColumnName = StartDate;
                        }
                        else
                        {
                            DateTime Date = Convert.ToDateTime(StartDate);
                            dt.Columns[i].ColumnName = Date.AddDays(j).ToString("MM/dd/yyyy");
                        }
                        i++;
                    }
                }
                if (dt.Columns.Contains("Column199")){
                    dt.Columns.Remove("Column199");
                }
                else
                {
                    dt.Columns.Remove("Column1");
                }
            }

            if (dtDOSVendorReply != null)
            {
                dtDOSVendorReply.Clear();
            }

            dtDOSVendorReply = dt;

            return JsonConvert.SerializeObject(dtDOSVendorReply);
        }

        [WebMethod]
        public static string SaveData(List<ObjectDOSVendorReply> DOS)
        {
            string Message = "";
            foreach (var item in DOS)
            {
                string strPLANT = item.Plant;
                string strVENDOR = item.SupplierCode;
                string strPARTCODE = item.MaterialNumber;
                string strPROBLEMCATEGORY = item.Problem_Cat == null ? "" : item.Problem_Cat;
                string strPERSONINCHARGE = item.PIC == null ? "" : item.PIC;
                string strREASON = item.Reason == null ? "" : item.Reason;
                string strCOUNTERMEASURE = item.CounterMeasure == null ? "" : item.CounterMeasure;
                string strUSERID = item.User;

                Message = DPSI.PSI_SAVE_VENDOR_REPLY_BY_PLANT(strPLANT, strVENDOR, strPARTCODE, strPROBLEMCATEGORY, strPERSONINCHARGE, strREASON,
                strCOUNTERMEASURE, strUSERID);
            }
            return JsonConvert.SerializeObject(Message);
        }
    }  
}