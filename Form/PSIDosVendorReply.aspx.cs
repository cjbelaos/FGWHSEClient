using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using FGWHSEClient.Classes;

namespace FGWHSEClient.Form
{
    public partial class PSIDosVendorReply : System.Web.UI.Page
    {
        static string UserID;
        static string SupplierCode;
        public string strPageSubsystem = "";
        public string strAccessLevel = "";

        static CtrlDOS MyControl;
        protected string _SupplierCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            MyControl = new CtrlDOS();
            _SupplierCode = "";
            Session["Message"] = "--";
            if (Request.QueryString.Count > 0) {
                if (Request.QueryString["SupplierCode"] != null) {
                    _SupplierCode = Request.QueryString["SupplierCode"];
                }
            }
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
                    strPageSubsystem = "FGWHSE_042";
                    if (!checkAuthority(strPageSubsystem) && Session["UserName"].ToString() != "GUEST")
                    {
                        Response.Write("<script>");
                        Response.Write("alert('You are not authorized to access the page.');");
                        Response.Write("window.location = 'Default.aspx';");
                        Response.Write("</script>");
                    }
                }
                UserID = Session["UserID"].ToString();
                SupplierCode = new OtherFunctions().GetVendorBasedOnUser(UserID);
                //if (!this.IsPostBack)
                //{
                //    GETSUPPLIER(txtVendorCode.Text.Trim());
                //}
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
        public static string GetData(string PlantID,string[] VendorCode,string[] MaterialNumber) {
            string[] p = { PlantID };
            return MyControl.GetData(p,VendorCode,MaterialNumber,SupplierCode);
        }
        [WebMethod]
        public static string SaveData(List<ObjectDOS> DOS) {
            return MyControl.SaveData(DOS);
        }
        [WebMethod]
        public static string SendEmailToBuyer(string SupplierID = "", string PIC = "") {
            return MyControl.SendEmailToBuyer(SupplierID, PIC);
        }
    }
}