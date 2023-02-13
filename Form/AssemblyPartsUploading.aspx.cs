using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.OleDb;
using FGWHSEClient.Classes;
using FGWHSEClient.DAL;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Services;

namespace FGWHSEClient.Form
{
    public partial class AssemblyPartsUploading : System.Web.UI.Page
    {
        static string UserID;
        public string strPageSubsystem = "";
        public string strAccessLevel = "";
        static DataTable dtPlantCode;
        static DataTable dtSupplierCode;
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
                    strPageSubsystem = "FGWHSE_041";
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
                isValid = false;
                return isValid;
            }
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
        public static string GetUploadedBOMAssemblyByPlant(string strPlant, string strVendor)
        {
            DALPSI_PLANT DPSI = new DALPSI_PLANT();
            DataTable dt = DPSI.PSI_GET_UPLOADED_BOM_ASSEMBLY_BY_PLANT(strPlant, strVendor);
            return JsonConvert.SerializeObject(dt);
        }

        [WebMethod]
        public static string SaveData(List<ObjectBOMAssemblyByPlant> BA)
        {
            DALPSI_PLANT DPSI = new DALPSI_PLANT();
            string Message = "";
            foreach (var item in BA)
            {
                string strPLANT = item.Plant;
                string strFG_PARENT = item.FG_Parent;
                string strFG_PARENT_SUPPLIERCODE = item.FG_Parent_Supplier;
                string strPARENT_SUPPLIERCODE = item.Parent_Supplier;
                string strPARENT_LEVEL = item.Parent_Level;
                string strASSY_LEVEL = item.Assy_Level;
                string strPARENT_ITEMCODE = item.Parent_ItemCode;
                string strCHILD_LEVEL = item.Child_Level;
                string strCHILD_ITEMCODE = item.Child_ItemCode;
                string strUSAGE = item.Usage;
                string strCHILD_SUPPLIERCODE = item.Child_SupplierCode;
                string strVALID_FROM = item.Valid_From;
                string strVALID_TO = item.Valid_To;
                string strDOS_LEVEL = item.DOS_Level;
                string strUSER = item.User;

                Message = DPSI.PSI_SAVE_BOM_ASSEMBLY_BY_PLANT(strPLANT, strFG_PARENT, strFG_PARENT_SUPPLIERCODE, strPARENT_SUPPLIERCODE, strPARENT_LEVEL, strASSY_LEVEL, strPARENT_ITEMCODE, strCHILD_LEVEL, strCHILD_ITEMCODE, strUSAGE, strCHILD_SUPPLIERCODE, strVALID_FROM, strVALID_TO, strDOS_LEVEL, strUSER);
            }
            return JsonConvert.SerializeObject(Message);
        }
    }
}