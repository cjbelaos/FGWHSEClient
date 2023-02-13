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
using System.Text.RegularExpressions; 

namespace FGWHSEClient
{
    public partial class Login : System.Web.UI.Page
    {
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
                    // Get user cookie to set previous user name
                    if (Request.Cookies["UserID"] != null)
                    {
                        txtUsername.Text = Request.Cookies["UserID"]["UserID"];
                    }

                    if (Request.Cookies["IsSized"] == null)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "autosize();", true);
                        HttpCookie hc = new HttpCookie("IsSized ", "Sized");
                        Response.Cookies.Add(hc);
                    }

                }


               


            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message);
            }
        }

        protected void btnLoginGuest_Click(object sender, EventArgs e)
        {
            Session["UserID"] = "GUEST";
            Session["UserName"] = "GUEST";
            Session["RoleID"] = "0";
            Session["UserName"] = "GUEST";
            Maintenance maint = new Maintenance();
            string strSystemName = "FGWHSE Monitoring";

            DataView dv = new DataView();
            dv = maint.GetUsersSubsystems(Session["UserID"].ToString(), strSystemName);

            Session["Subsystem"] = dv;
            Response.Redirect("Form/Default.aspx");
        }

        bool isValidText(string strRegEx, string strText)
        {
            bool isValid = true;

            Regex regex = new Regex(strRegEx);

            foreach (char c in strText)
            {
                if (!regex.IsMatch(c.ToString()))
                {
                    isValid = false;
                }
            }

            return isValid;
        }

	protected void btnDNReceiving_Click(object sender, EventArgs e)
	{
		Response.Redirect("Form/DNReceivingScreen.aspx");
	}

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Boolean isLogin = false;
            string strLoginType2 = System.Configuration.ConfigurationManager.AppSettings["webFor"];

            if (txtUsername.Text == "")
            {
                MsgBox1.alert("Please enter your username.");
                return;
            }

            if (txtPassword.Text == "")
            {
                MsgBox1.alert("Please enter your password.");
                return;
            }

            if (strLoginType2 == "OUTSIDE")
            {
                if (!(isValidText(@"[A-Za-z0-9]", txtUsername.Text)))
                {
                    MsgBox1.alert("Username must be alphanumeric char only!");
                    return;
                }

                if (!(isValidText(@"[A-Za-z0-9]", txtPassword.Text)))
                {
                    MsgBox1.alert("Username must be alphanumeric char only!");
                    return;
                }
            }

            try
            {
                string strSystemName = "FGWHSE Monitoring";
                Maintenance maint = new Maintenance();
                DataView dvUser = new DataView();

                string strLoginType = System.Configuration.ConfigurationManager.AppSettings["webFor"];

                if (strLoginType == "EPPI")
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
                            GetUserSubsystems();
                            isLogin = true;
                        }
                        else
                        {
                            MsgBox1.alert("Invalid username/password!");
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
                            MsgBox1.alert("Invalid username/password!");
                            return;
                        }
                    }


                }
                else if (strLoginType == "OUTSIDE")
                {
                    dvUser = maint.LOGIN_SUPPLIER(txtUsername.Text.Trim(), txtPassword.Text.Trim()).Tables[0].DefaultView;
                    if (dvUser.Count > 0)
                    {
                        Session["UserID"] = Convert.ToString(dvUser[0]["USERID"]);
                        Session["UserName"] = Convert.ToString(dvUser[0]["USERNAME"]);
                        Session["ACCESSLEVEL"] = Convert.ToString(dvUser[0]["ACCESSLEVEL"]);
                        Session["subsystem_id"] = Convert.ToString(dvUser[0]["subsystem_id"]);
                        Session["supplierID"] = Convert.ToString(dvUser[0]["LOCATIONID"]);
                        Session["Subsystem"] = dvUser;
                        isLogin = true;
                    }
                    else
                    {
                        MsgBox1.alert("Invalid username/password!");
                        return;
                    }
                }
                else
                {
                    MsgBox1.alert("Invalid configuration settings! Please contact system administrator.");
                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message.ToString());
                return;
            }
            finally
            {
                if (isLogin)
                {
                    Response.Redirect("Form/Default.aspx");
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
                MsgBox1.alert(ex.Message.ToString());
            }
        }

        protected void btnDNReceiving_Click1(object sender, EventArgs e)
        {
            Response.Redirect("Form/DNReceivingScreen.aspx");
        }

      

       
    }
}
