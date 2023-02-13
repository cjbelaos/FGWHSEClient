using System;
using System.Data;
using Newtonsoft.Json;
using FGWHSEClient.DAL;
using System.Web.Services;
using FGWHSEClient.Classes;
using System.Collections.Generic;

namespace FGWHSEClient.Form
{
    public partial class PSIMasterList : System.Web.UI.Page
    {
        static string UserID;
        static bool isForViewing = false;
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

        //[WebMethod]
        //public static string GetSupplierCategory()
        //{
        //    PSIDAL psi = new PSIDAL();
            //DataTable dt = psi.GET_TBL_M_SUPPLIER_CATEGORY();

            //return JsonConvert.SerializeObject(dt);
        //}
    }
}