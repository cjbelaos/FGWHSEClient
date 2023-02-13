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



using System.Xml.Linq;

using FGWHSEClient.LdapService;
using FGWHSEClient.LogInService;

namespace FGWHSEClient.Form
{
    public partial class EmptyBoxMonitoringScreen : System.Web.UI.Page
    {
        EmptyPcaseDAL epDAL = new EmptyPcaseDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                
                txtUserID.Attributes.Add("onkeydown", "return (event.keyCode!=13);false;");
                txtPassword.Attributes.Add("onkeydown", "return (event.keyCode!=13);false;");
                btnADDITIONAL.OnClientClick = HttpUtility.HtmlDecode("showModalAdditional();return false;");
                btnPRINT.OnClientClick = HttpUtility.HtmlDecode("showModalPrint();return false;");
                getAdditional();
                fillDetails();
            }
        }

        public void fillDetails()
        {
            //lblCONTROLNO.Text = "";
            if (Request.QueryString["CONTROLNO"] != null)
            {
                lblCONTROLNO.Text = Request.QueryString["CONTROLNO"].ToString();
            }
            

            DataSet DS = epDAL.GET_EMPTY_PCASE_RECEIVED_LIST(lblCONTROLNO.Text);
            DataView dv = DS.Tables[0].DefaultView;
            if (dv.Count > 0)
            {
                lblRFIDTagCount.Text = dv[0]["PCASECOUNT"].ToString();
                lblTotal.Text = dv[0]["PCASETOTALCOUNT"].ToString();
                lblAdditional.Text = dv[0]["ADDITIONALPCASECOUNT"].ToString();
                lblTrackingNo.Text = dv[0]["TRACKINGNO"].ToString();
                lblLOADINGSTATUS.Text = dv[0]["LOADINGSTATUS"].ToString();
                lblSTARTRECEIVEFLAG.Text = dv[0]["STARTRECEIVEFLAG"].ToString();
            }

            DataTable dt = DS.Tables[1];
            gvLoad.DataSource = dt;
            gvLoad.DataBind();
        }

        protected void gvLoad_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblEPC = (Label)e.Row.FindControl("lblEPC");
                LinkButton lnk = (LinkButton)e.Row.FindControl("lnkDELETE");

                string strValues = HttpUtility.HtmlDecode(lblEPC.Text);

                lnk.OnClientClick = HttpUtility.HtmlDecode("showModal('" + strValues + "');return false;");

            }
        }

        public bool isLogged()
        {
            bool isOK = false;
            
            
            if (txtUserID.Text == "")
            {
                MsgBox1.alert("Please enter your username.");
                return false;
            }

            if (txtPassword.Text == "")
            {
                MsgBox1.alert("Please enter your password.");
                return false;
            }

            string strSystemName = "FGWHSE Monitoring";
            Maintenance maint = new Maintenance();
            DataView dvUser = new DataView();


            bool isAuthenticated = ServiceLocator.GetLdapService().IsAuthenticated(txtUserID.Text, txtPassword.Text);
            if (isAuthenticated)
            {
                DsUser ds = ServiceLocator.GetLoginService().GetUser(txtUserID.Text);
                if (ds.SystemUser.Rows.Count <= 0)
                {
                    MsgBox1.alert("User profile is incomplete! " +
                        "Please contact the administrator " +
                        "to check if user has all information required. ");
                }


                dvUser = maint.GetUsersLDAP(txtUserID.Text, strSystemName);
                if (dvUser.Count > 0)
                {
                    isOK = true;
                
                }
                else
                {
                    MsgBox1.alert("Invalid username/password!");
                    return false;
                }

            }
            else
            {
                dvUser = maint.GetUser(strSystemName, txtUserID.Text.Trim(), txtPassword.Text.Trim(), 0);
                if (dvUser.Count > 0)
                {
                    isOK = true;
                }
                else
                {
                    MsgBox1.alert("Invalid username/password!");
                    return false;
                }
            }

            if (isOK == true)
            {
                isOK = GetUserSubsystems();
            }
            return isOK;
        }


        private bool GetUserSubsystems()
        {
            bool isOK = false;
            try
            {
                Maintenance maint = new Maintenance();
                string strSystemName = "FGWHSE Monitoring";

                DataView dv = new DataView();
                dv = maint.GetUsersSubsystems(txtUserID.Text.Trim(), strSystemName);
                string strRole = "";
                for (int x = 0; x < dv.Count; x++)
                {
                    strRole = dv[x]["Subsystem"].ToString();
                    if (strRole == "FGWHSE_052")
                    {
                        isOK = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message.ToString());
            }

            return isOK;
        }

        protected void btnOKlogin_Click(object sender, EventArgs e)
        {
            bool isOK = isLogged();
            if (isOK == true)
            {

                epDAL.DELETE_EMPTY_PCASE_RFID(lblCONTROLNO.Text, txtEPC.Text.Trim(), txtUserID.Text.Trim());
                txtEPC.Text = "";
                txtUserID.Text = "";
                txtPassword.Text = "";
                fillDetails();
                MsgBox1.alert("SUCCESSFULLY DELETED!");
            }
            else
            {
                MsgBox1.alert("Invalid Log-in!");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(txtQuantity.Text.Trim() == "")
            {
                MsgBox1.alert("Please input QTY");
                return;
            }
            epDAL.ADD_EMPTY_PCASE_ADDITIONAL(lblCONTROLNO.Text, txtQuantity.Text.Trim(), txtRemarks.Text.Trim());
            getAdditional();
            fillDetails();
            MsgBox1.alert("Successfully saved!");
        }

        public void getAdditional()
        {
            DataView dv = epDAL.GET_EMPTY_PCASE_ADDITIONAL(lblCONTROLNO.Text).Tables[0].DefaultView;
            if (dv.Count > 0)
            {
                txtQuantity.Text = dv[0]["ADDITIONALPCASECOUNT"].ToString();
                txtRemarks.Text = dv[0]["REMARKS"].ToString();
            }
            else
            {
                txtQuantity.Text = "0";
                txtRemarks.Text = "";

            }

        }

        public void printEmptyPcase()
        {
            if (lblLOADINGSTATUS.Text == "2")
            {
                MsgBox1.alert("Unable to Print! Dectected Mixing Pcase!");
                return;
            }

            if (lblSTARTRECEIVEFLAG.Text == "0")
            {
                MsgBox1.alert("Pcase receving is still on-going! Click stop button before printing.(Close this page to refresh)");
                return;
            }
            DataTable dtPrint = epDAL.GET_EMPTY_PCASE_PRINT_DETAILS(lblCONTROLNO.Text).Tables[0];
            Session["dtEMPTYPCASE"] = dtPrint;
            openLink("EmptyBoxPrint.aspx");
        }


        public void openLink(string lnk)
        {
            string strURL = lnk;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + strURL + "','_newtab');", true);
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            printEmptyPcase();
        }
    }
}