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
    public partial class RFIDLogin : System.Web.UI.Page
    {
        public string strPageSubsystem = "";
        public string strAccessLevel = "";
        Maintenance maint = new Maintenance();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                //Clears session
                Session.Clear();

                lblSystemName.Text = System.Configuration.ConfigurationManager.AppSettings["RFIDsystemName"] + " " + System.Configuration.ConfigurationManager.AppSettings["RFIDsystemVersion"]; ;

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
                }


            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message);
            }
        }

        protected void btnLoginGuest_Click(object sender, EventArgs e)
        {
            //Session["UserName"] = "GUEST";
            //Response.Redirect("Form/HTMenu.aspx");
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
                    strPageSubsystem = "FGWHSE_037";
                    if (!checkAuthority(strPageSubsystem))
                    {
                        Response.Write("<script>");
                        Response.Write("alert('You are not authorized to access the page.');");
                        Response.Write("window.location = 'RFIDLogin.aspx';");
                        Response.Write("</script>");
                    }
                    else
                    {
                        Response.Redirect("Form/RFIDLotMatch.aspx");
                    }


                   
                }
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
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);

                isValid = false;
                return isValid;
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


    }
}

