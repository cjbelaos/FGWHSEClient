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
using System.Drawing;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using FGWHSEClient.LdapService;
using FGWHSEClient.LogInService;
using com.eppi.utils;


namespace FGWHSEClient
{
    public partial class HTLoginPalletShipmentValidation : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
            //if (!this.IsPostBack)
            //{
            //    txtUserID.Focus();
            //}
        //}

        //protected void txtUserID_TextChanged(object sender, EventArgs e)
        //{
        //    if (txtUserID.Text.Trim() != "")
        //    {
        //        Response.Redirect("~/Form/HTPalletShipmentValidation.aspx?userid=" + txtUserID.Text.Trim());
        //    }
        //    else
        //    {
        //        MsgBox.alert("Please enter USERID!");
        //    }
        //}

        //protected void btnLogin_Click(object sender, EventArgs e)
        //{

        //}


        Maintenance maint = new Maintenance();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                //Clears session
                Session.Clear();

                lblSystemName.Text = System.Configuration.ConfigurationManager.AppSettings["systemName"] + " " + System.Configuration.ConfigurationManager.AppSettings["systemVersion"]; ;

                ////check if compatibility view
                if (IECompatibility.GetIEBrowserMode(Page.Request) != "Compatibility View")
                {
                    //force compatibility to IE6
                    HtmlMeta force = new HtmlMeta();
                    force.HttpEquiv = "X-UA-Compatible";
                    force.Content = "IE=EmulateIE7";
                    Header.Controls.AddAt(0, force);
                }



                if (!this.IsPostBack == true)
                {
                    txtUsername.Attributes.Add("onkeydown", "return (event.keyCode!=13);false;");


                    if (Request.QueryString["ERR"] != null)
                    {
                        lblError.Text = Request.QueryString["ERR"].ToString();
                    }
                    // Get user cookie to set previous user name
                    if (Request.Cookies["UserID"] != null)
                    {
                        txtUsername.Text = Request.Cookies["UserID"]["UserID"];
                    }
                }

                

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void btnLoginGuest_Click(object sender, EventArgs e)
        {
            Session["UserName"] = "GUEST";
            Response.Redirect("Form/HTDefault.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Boolean isLogin = false;

            if (txtUsername.Text == "")
            {
                lblError.Text = "Please enter your username.";
                return;
            }

            if (txtPassword.Text == "")
            {
                lblError.Text = "Please enter your password.";
                return;
            }

            try
            {
                string strSystemName = "FGWHSE Monitoring";
                Maintenance maint = new Maintenance();
                DataView dvUser = new DataView();

                //validate username and password in AD
                bool isAuthenticated = ServiceLocator.GetLdapService().IsAuthenticated(txtUsername.Text, txtPassword.Text);
                if (isAuthenticated)
                {
                    DsUser ds = ServiceLocator.GetLoginService().GetUser(txtUsername.Text);
                        if (ds.SystemUser.Rows.Count <= 0)
                        {
                            lblError.Text = "User profile is incomplete! " +
                                "Please contact the administrator " +
                                "to check if user has all information required. ";
                            txtPassword.Focus();
                            return;
                        }


                        dvUser = maint.GetUsersLDAP(txtUsername.Text, strSystemName);
                        if (dvUser.Count > 0)
                        {
                            Session["UserID"] = Convert.ToString(dvUser[0]["UserID"]);
                            Session["UserName"] = Convert.ToString(dvUser[0]["UserName"]);
                            Session["RoleID"] = Convert.ToString(dvUser[0]["Role"]);
                            GetUserSubsystems();
                            isLogin = true;
                        }
                        else
                        {
                            lblError.Text = "Invalid login!";
                            txtPassword.Focus();
                            return;
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
                        GetUserSubsystems();
                        isLogin = true;
                    }
                    else
                    {
                        lblError.Text = "Invalid login! You have no access rights in the system. Please contact system administrator.";
                        txtPassword.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                lblError.Text = ex.Message.ToString();
                return;
            }
            finally
            {
                if (isLogin)
                {
                    Response.Redirect("~/Form/HTPalletShipmentValidation.aspx?userid=" + txtUsername.Text.Trim());
                }
            }
        }




        private void GetUserSubsystems()
        {
            try
            {
                Maintenance maint = new Maintenance();
                string strSystemName = "FGWHSE Monitoring";

                DataView dv = new DataView();
                dv = maint.GetUsersSubsystems(Session["UserID"].ToString(), strSystemName);

                Session["Subsystem"] = dv;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                lblError.Text = ex.Message.ToString();
            }
        }
      
    }
}
