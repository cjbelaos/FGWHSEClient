using com.eppi.utils;
using FGWHSEClient.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OleDb;
using System.IO;
using System.Collections.Generic;

using System.Runtime.InteropServices;
using System.Text;

using System.Threading;

namespace FGWHSEClient.Form
{
    public partial class PSI_MASTER_PARTS : System.Web.UI.Page
    {

        public string strPageSubsystem = "";
        public string strAccessLevel = "";

        public PSIDAL dalPSI = new PSIDAL();
        int rowIndx;
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
                    strPageSubsystem = "FGWHSE_039";
                    if (!checkAuthority(strPageSubsystem))
                    {
                        Response.Write("<script>");
                        Response.Write("alert('You are not authorized to access the page.');");
                        Response.Write("window.location = 'Default.aspx';");
                        Response.Write("</script>");
                    }
                }

                if (!this.IsPostBack)
                {
                    GETSUPPLIER(txtVendorCode.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>");
                Response.Write("alert('"+ ex.Message.ToString() + "');");
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


        protected void lnk_Click(object sender, EventArgs e)
        {

            var rowTrigger = (Control)sender;
            GridViewRow rowUpdate = (GridViewRow)rowTrigger.NamingContainer;
            rowIndx = rowUpdate.RowIndex;
            LinkButton lnk = (LinkButton)grdSupplier.Rows[rowIndx].FindControl("lnkSupplierCode");
            openLink(lnk.Text);

        }

        public void openLink(string VendorCode)
        {
            string strURL = "PSI_MASTER_PARTS_VENDOR.aspx?vendorCode=" + VendorCode;

            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + strURL + "','_newtab');", true);
        }

        public void GETSUPPLIER(string strSUPPLIER)
        {
            try
            {
                grdSupplier.DataSource = dalPSI.PSI_GET_SUPPLIER(strSUPPLIER).Tables[0];
                grdSupplier.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>");
                Response.Write("alert('" + ex.Message.ToString() + "');");
                Response.Write("</script>");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                
                    GETSUPPLIER(txtVendorCode.Text.Trim());
               
            }
            catch (Exception ex)
            {
                Response.Write("<script>");
                Response.Write("alert('" + ex.Message.ToString() + "');");
                Response.Write("</script>");
            }
        }

        protected void grdSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSupplier.PageIndex = e.NewPageIndex;
            GETSUPPLIER(txtVendorCode.Text.Trim());
        }
    }
}