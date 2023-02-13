using System;
using System.Data;
using Newtonsoft.Json;
using FGWHSEClient.DAL;
using System.Web.Services;

namespace FGWHSEClient.Form
{
    public partial class PSI_SHORTAGELIST_BY_PLANT : System.Web.UI.Page
    {
        static string UserID;
        static DataTable dtSupplierCode;
        static DataTable dtPlantCode;
        static string strShortageListByPlantUpdatedDate;
        static bool isForViewing = false;
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
        public static string GetPartsCodeByPlantandVendors(string strPLANT, string strVENDORS)
        {
            DALPSI_PLANT DPSI = new DALPSI_PLANT();
            return JsonConvert.SerializeObject(DPSI.PSI_GET_PARTSCODE_BY_PLANT_AND_VENDOR(strPLANT, strVENDORS));
        }

        [WebMethod]
        public static string GetShortageListByPlant(string strPLANT, string strSupplierId)
        {
            DALPSI_PLANT DPSI = new DALPSI_PLANT();
            DataTable dt = DPSI.PSI_GET_SHORTAGE_LIST_BY_PLANT(strPLANT, strSupplierId);
            if (dt.Rows.Count > 0)
            {
                strShortageListByPlantUpdatedDate = dt.Rows[dt.Rows.Count - 1][207].ToString();
                var StartDate = dt.Rows[0][206].ToString();
                for (var i = 8; i < 206;)
                {
                    for (int j = 0; j <= 197; j++)
                    {
                        if (i == 8)
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
                dt.Columns.Remove("FIRSTCOLUMNNAME");
                dt.Columns.Remove("CREATEDDATE");
            }
            return JsonConvert.SerializeObject(dt);
        }

        [WebMethod]
        public static string GetShortageListByPlantUpdatedDate()
        {
            return JsonConvert.SerializeObject(strShortageListByPlantUpdatedDate);
        }
    }
}