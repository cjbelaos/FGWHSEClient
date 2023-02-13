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
using System.Text.RegularExpressions;

using System.Threading;

namespace FGWHSEClient.Form
{
    public partial class AutoLine_Barcode_Station : System.Web.UI.Page
    {
        AutoLineDAL autoDAL = new AutoLineDAL();
        public string strAccessLevel = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Write("<script>");
                Response.Write("alert('Session expired! Please log in again.');");
                Response.Write("window.location = '../Login.aspx';");
                Response.Write("</script>");
            }

            if (!this.IsPostBack)
            {
                if (!checkAuthority("FGWHSE_050"))
                {
                    Response.Write("<script>");
                    Response.Write("alert('You are not authorized to access the page.');");
                    Response.Write("window.location = 'Default.aspx';");
                    Response.Write("</script>");
                }
                clearALL();
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

        public void msgBoxPrompt(string strMessage)
        {
            Response.Write("<script>");
            Response.Write("alert('" + strMessage + "');");
            Response.Write("</script>");
        }

        public void getGrid()
        {
            DataSet DS = new DataSet();
            DS = autoDAL.GET_AUTOLINE_STATION(txtIP.Text.Trim(), txtStationNo.Text.Trim());
            grdStation.DataSource = DS.Tables[0];
            grdStation.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            getGrid();
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                autoDAL.ADD_AUTOLINE_STATION(txtIP.Text.ToString(), txtStationNo.Text.ToString(), Session["UserID"].ToString());
                clearALL();
                getGrid();
                
            }
            catch(Exception ex)
            {
                msgBoxPrompt(ex.Message.ToString());
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
        public void clearALL()
        {
            txtStationNo.Text = "";
            txtIP.Text = "";
            txtStationNo.Focus();
        }
    }
}