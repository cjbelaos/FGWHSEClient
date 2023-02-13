using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using FGWHSEClient.DAL;
using com.eppi.utils;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using FGWHSEClient.LdapService;
using FGWHSEClient.LogInService;


namespace FGWHSEClient.Form
{
    public partial class RFIDDNDelete : System.Web.UI.Page
    {
        DeleteDNDAL DeleteDNDAL = new DeleteDNDAL();
        Maintenance maint = new Maintenance();
        public string strAccessLevel = "";

        void Page_PreInit(Object sender, EventArgs e)
        {
            if (Request.QueryString["DeviceType"] != null)
            {
                if (Request.QueryString["DeviceType"].ToString() == "Denso")
                {
                    this.MasterPageFile = "~/Form/DENSOMasterPalletMonitoring.Master";
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string strLoginType = System.Configuration.ConfigurationManager.AppSettings["webFor"];
                if (strLoginType == "EPPI")
                {
                    string strLoginPage = "Login.aspx";
                    if (!checkAuthority("FGWHSE_038"))
                    {
                        if (Request.QueryString["DeviceType"] != null)
                        {
                            if (Request.QueryString["DeviceType"].ToString() == "Denso")
                            {
                                strLoginPage = "DENSOLogin.aspx";
                            }
                            else
                            {
                                strLoginPage = "HTLogin.aspx";
                            }
                        }
                        Response.Write("<script>");
                        Response.Write("alert('You are not authorized to access this web page! ');");
                        Response.Write("window.location = '../" + strLoginPage + "?returnurl=Maintenance/ControlSegment.aspx';self.close();");
                        Response.Write("</script>");

                    }
                    else
                    {
                        OnLoad();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        private void OnLoad()
        {
            txtDNNo.Focus();
            if (Session["UserName"] == null)
            {

                if (Request.QueryString["DeviceType"] != null)
                {
                    if (Request.QueryString["DeviceType"].ToString() == "Denso")
                    {
                        Response.Write("<script>");
                        Response.Write("alert('Session expired! Please log in again.');");
                        Response.Write("window.location = '../DENSOLogin.aspx';");
                        Response.Write("</script>");
                    }
                }
                else
                {
                    Response.Write("<script>");
                    Response.Write("alert('Session expired! Please log in again.');");
                    Response.Write("window.location = '../HTLogin.aspx';");
                    Response.Write("</script>");
                }

            }

            if (!this.IsPostBack)
            {
                if (Request.QueryString["DeviceType"] != null)
                {
                    if (Request.QueryString["DeviceType"].ToString() == "Denso")
                    {
                        dvLotDataScan.Attributes.Add("style", "font-size:x-small;width:270px");
                        //txtDNNo.Attributes.Add("style", "font-size:13px;width:150px;");
                        txtDNNo.Attributes.Add("style", "font-size:18px;width:120px;");
                        //btnDNDetails.Attributes.Add("style", "font-size:x-small;width:30px;");
                        //grdDNData.Attributes.Add("style", "font-size:x-small;width:230px;");
                        grdRFID.Attributes.Add("style", "font-size:10px;width:245px;");
                        grdHeight.Attributes.Add("style", "overflow:auto;height:105px;");
                        //lblScan.Attributes.Add("style", "font-size:8px;");
                        lblScan.Attributes.Add("style", "font-size:11px;");
                        //btnDelete.Attributes.Add("style", "font-size:x-small;width:60px;height:25px");
                        //btnExecute.Attributes.Add("style", "font-size:x-small;width:60px;height:25px");
                        //btnClear.Attributes.Add("style", "font-size:x-small;width:60px;height:25px");

                        btnReceive.Attributes.Add("style", "font-size:14px;width:75px;height:25px");
                        // btnExecute.Attributes.Add("style", "font-size:14px;width:75px;height:25px");
                        btnClear.Attributes.Add("style", "font-size:14px;width:75px;height:25px");
                        tdMessage.Attributes.Add("style", "border: 1px solid #385d8a; background-color:#d9d9d9;padding:5px;font-size:x-small;height:20px;width:180px");
                        lblMessage2.Attributes.Add("style", "font-size:11px;width:180px;");
                        lblMessage.Attributes.Add("style", "font-size:11px;");
                        tdScan.Attributes.Add("style", "width:100px;text-align:right");
                    }
                    else if (Request.QueryString["DeviceType"].ToString() == "HT")
                    {
                        txtDNNo.Attributes.Add("style", "font-size:14px;");
                    }
                }

            }
        }

        protected void txtDNNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = DeleteDNDAL.GET_DNSUMMARY(txtDNNo.Text.Trim());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdRFID.DataSource = DeleteDNDAL.GET_DNSUMMARY(txtDNNo.Text.Trim());
                    grdRFID.DataBind();

                    //btnReceive.Visible = true;
                    btnClear.Visible = true;
                    //btnReceive.Enabled = true;
                    btnClear.Enabled = true;

                    btnDelete.Visible = true;
                    btnDelete.Enabled = true;

                    Label1.Visible = true;

                    lblMessage.Text = "";
                }
                else
                {
                    lblMessage.Text = "DN DOES NOT EXIST, PLEASE CHECK!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;

                    Label1.Visible = false;
                    //btnReceive.Visible = true;
                    btnClear.Visible = true;
                    //btnReceive.Enabled = false;
                    btnClear.Enabled = true;

                    btnDelete.Visible = true;
                    btnDelete.Enabled = false;

                    grdRFID.DataSource = null;
                    grdRFID.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
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
                        if (isValid == true)
                        {
                            string strRole = dvSubsystem.Table.Rows[iRow]["Role"].ToString();

                            if (strRole != "")
                            {
                                strAccessLevel = strRole;
                            }
                        }
                    }
                }
                return isValid;
            }
            catch (Exception ex)
            {
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);

                isValid = false;
                return isValid;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtDNNo.Text = "";
                lblMessage.Text = "";

                grdRFID.DataSource = null;
                grdRFID.DataBind();
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
             bool isAuthorized = ServiceLocator.GetLoginService().IsAuthorized("FGWHSE Monitoring", "FGWHSE_038", txtUsername.Text.Trim());
             string strSubSystemRole = "";
             string strSystemName = "FGWHSE Monitoring";
             DataView dvUser = new DataView();

            if (isAuthorized)
            {
                 //validate username and password in AD
                 bool isAuthenticated = ServiceLocator.GetLdapService().IsAuthenticated(txtUsername.Text, txtPassword.Text);
                 if (isAuthenticated)
                 {
                     DsUser ds = ServiceLocator.GetLoginService().GetUser(txtUsername.Text);
                     if (ds.SystemUser.Rows.Count <= 0)
                     {
                         MsgBox1.alert("User profile is incomplete! " +
                             "Please contact the administrator " +
                             "to check if user has all information required. ");
                         return;
                     }


                     dvUser = maint.GetUsersLDAP(txtUsername.Text, strSystemName);
                     if (dvUser.Count > 0)
                     {
                         Session["UserID"] = Convert.ToString(dvUser[0]["UserID"]);
                         Session["UserName"] = Convert.ToString(dvUser[0]["UserName"]);
                         Session["RoleID"] = Convert.ToString(dvUser[0]["Role"]);
                         //GetUserSubsystems();
                         //isLogin = true;

                         GetAccess();
                     }
                     else
                     {
                         //MsgBox1.alert("Invalid username/password!");
                         lblErrorMsg.Text = "Invalid username/password!";
                         ModalPopupExtender1.Show();
                         //return;
                     }

                 }
                 else
                 {
                     dvUser = maint.GetUser(strSystemName, txtUsername.Text.Trim(), txtPassword.Text.Trim(), 0);
                     if (dvUser.Count > 0)
                     {
                         Session["UserID"] = Convert.ToString(dvUser[0]["User_ID"]);
                         Session["UserName"] = Convert.ToString(dvUser[0]["User_Name"]);
                         Session["RoleID"] = Convert.ToString(dvUser[0]["Role"]);
                         //GetUserSubsystems();
                         //isLogin = true;
                         GetAccess();
                     }
                     else
                     {
                         //MsgBox1.alert("Invalid username/password!");
                         lblErrorMsg.Text = "Invalid username/password!";
                         ModalPopupExtender1.Show();
                         //return;
                     }
                 }
            }
            else
            {

                lblErrorMsg.Text = "User not authorized to delete, please input user with authority.";
                ModalPopupExtender1.Show();
            }





            //////if (isAuthorized)
            //////{
            //////    //check user role
            //////    DataSet ds = maint.GetUserSubsystemRole(txtUsername.Text.Trim(), "FGWHSE Monitoring", "FGWHSE_038");
            //////    if (ds.Tables.Count > 0)
            //////    {
            //////        if (ds.Tables[0].Rows.Count > 0)
            //////        {
            //////            strSubSystemRole = ds.Tables[0].Rows[0]["ROLE"].ToString();
            //////            Session["ROLEID"] = strSubSystemRole;

            //////            if (strSubSystemRole == "4") //ROLE IS USER
            //////            {
            //////                //btnNewApp.Visible = false;
            //////                //GetSectionByUser();

            //////                lblErrorMsg.Text = "User not authorized to delete, please input user with authority.";
            //////                ModalPopupExtender1.Show();
            //////            }
            //////            else if (strSubSystemRole == "3") //ROLE IS KEY USER
            //////            {
            //////                //btnNewApp.Visible = true;
            //////                //GetSectionByUser();

            //////                lblMessage.Text = "SUCCESSFULLY DELETED";
            //////                //ModalPopupExtender1.Show();
            //////            }
            //////            //else //ROLE IS ADMIN - 1
            //////            //{
            //////            //    //btnNewApp.Visible = true;
            //////            //    //GetSection();
            //////            //}

            //////        }

            //////    }
            //////}
            //////else
            //////{
               
            //////    lblErrorMsg.Text = "Wrong User ID or Password, please try again.";
            //////    ModalPopupExtender1.Show();
            //////}
            //txtUsername.Text = "";
            //txtPassword.Text = "";
            //lblErrorMsg.Text = "";
        }

        private void GetAccess()
        {
            string strSubSystemRole = "";

            //check user role
            DataSet ds = maint.GetUserSubsystemRole(txtUsername.Text.Trim(), "FGWHSE Monitoring", "FGWHSE_038");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    strSubSystemRole = ds.Tables[0].Rows[0]["ROLE"].ToString();
                    Session["ROLEID"] = strSubSystemRole;

                    if (strSubSystemRole == "4")//ROLE IS USER
                    {
                        //btnNewApp.Visible = false;
                        //GetSectionByUser();

                        lblErrorMsg.Text = "User not authorized to delete, please input user with authority.";
                        ModalPopupExtender1.Show();
                    }
                    else if (strSubSystemRole == "3" || strSubSystemRole == "1") //ROLE IS KEY USER  OR ADMIN
                    {
                        //btnNewApp.Visible = true;
                        //GetSectionByUser();
                        string strexec = DeleteDNDAL.DN_UPDATE_DELETEDDN(txtDNNo.Text.Trim(), Session["UserID"].ToString());

                        lblMessage.Text = "SUCCESSFULLY DELETED";
                        lblMessage.ForeColor = Color.Green;

                        grdRFID.DataSource = null;
                        grdRFID.DataBind();

                        txtUsername.Text = "";
                        txtPassword.Text = "";
                        lblErrorMsg.Text = "";
                        txtDNNo.Text = "";
                        //ModalPopupExtender1.Show();
                    }
                    //else //ROLE IS ADMIN - 1
                    //{
                    //    //btnNewApp.Visible = true;
                    //    //GetSection();
                    //}
                }
                else
                {
                    lblErrorMsg.Text = "User not authorized to delete, please input user with authority.";
                    ModalPopupExtender1.Show();
                }

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            lblErrorMsg.Text = "";
        }
    }
}
