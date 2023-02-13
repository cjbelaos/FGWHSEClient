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
using com.eppi.utils;

namespace FGWHSEClient.Form
{
    public partial class HTMasterPalletMonitoring : System.Web.UI.MasterPage
    {
        protected string strName = "";
        protected string uPage = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["UserName"] == null)
                {
                    Label lbl = this.ContentPlaceHolder1.FindControl("lblHeader") as Label;

                    uPage = lbl.Text;
                    if (uPage == "PARTS LOCATION CHECK" || uPage == "PARTS LOCATION AND INSPECTION CHECK") //added by melvin 8/24/2018
                    {
                        btnBack.Visible = false;
                    }
                    else
                    {
                        string strLoginPage = getLoginPage();
                        Response.Write("<script>");
                        Response.Write("alert('Session expired! Please log in again.');");
                        Response.Write("window.location = '../"+ strLoginPage + "';");
                        Response.Write("</script>");
                    }
                }
                else
                {
                    strName = Session["UserName"].ToString();
                    lblUserName.Text = strName;
                    lbl_systemName.Text = ConfigurationManager.AppSettings["systemName"].ToString();
                }

                    lblFooter.Text = ConfigurationManager.AppSettings["remarks"].ToString();

                    //check if compatibility view
                    if (IECompatibility.GetIEBrowserMode(Page.Request) != "Compatibility View")
                    {
                        //force compatibility to IE7
                        HtmlMeta force = new HtmlMeta();
                        force.HttpEquiv = "X-UA-Compatible";
                        force.Content = "IE=EmulateIE7";
                        Page.Header.Controls.AddAt(0, force);
                    }
                    string currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                    if(currentPage == "HTMenu.aspx")
                    {
                        dvBack.Visible = false;
                    }
                    else
                    {
                        dvBack.Visible = true;
                    }
            }
            catch (Exception ex)
            {
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("HTMenu.aspx?DeviceType=HT");
            //if (Request.QueryString["DeviceType"] == null)
            //{
            //    Response.Redirect("Default.aspx");
            //}
            //else
            //{
            //    if (Request.QueryString["DeviceType"].ToString() == "Denso")
            //    {
            //        Response.Redirect("HTMenu.aspx?DeviceType=Denso");
            //    }
            //    else if (Request.QueryString["DeviceType"].ToString() == "HT")
            //    {
            //        Response.Redirect("HTMenu.aspx?DeviceType=HT");
            //    } 
            //}
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Label lbl = this.ContentPlaceHolder1.FindControl("lblHeader") as Label;

            if (lbl != null)
            {
                uPage = lbl.Text;
            }

            if (Session["UserPickingRole"] != null)
            {
                if (Session["UserPickingRole"].ToString().Trim() != "")
                {
                    Maintenance maint = new Maintenance();
                    maint.PICKING_USER_LOG_OUT(Session["UserID"].ToString());
                }
            }

            if (Request.QueryString["DeviceType"] == null)
            {
                if (uPage == "PARTS LOCATION CHECK" || uPage == "PARTS LOCATION AND INSPECTION CHECK")
                {
                    Response.Redirect("~/PLCLogin.aspx");
                }
                else if (uPage == "RFID LOT LABEL PAIRING")
                {
                    Response.Redirect("~/RFIDLogin.aspx");
                }
                else
                {
                    Response.Redirect("~/HTLogin.aspx");
                }
                
            }
            else
            {
                if (Request.QueryString["DeviceType"].ToString() == "Denso")
                {
                    if (uPage == "PARTS LOCATION CHECK" || uPage == "PARTS LOCATION AND INSPECTION CHECK")
                    {
                        Response.Redirect("~/PLCLogin.aspx");
                    }
                    else if (uPage == "RFID LOT LABEL PAIRING")
                    {
                        Response.Redirect("~/RFIDLogin.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/HTLogin.aspx?DeviceType=Denso");
                    }

                }
                else if (Request.QueryString["DeviceType"].ToString() == "HT")
                {
                    if (uPage == "PARTS LOCATION CHECK" || uPage == "PARTS LOCATION AND INSPECTION CHECK")
                    {
                        Response.Redirect("~/PLCLogin.aspx");
                    }
                    else if (uPage == "RFID LOT LABEL PAIRING")
                    {
                        Response.Redirect("~/RFIDLogin.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/HTLogin.aspx?DeviceType=HT");
                    }


                }
                else if (Request.QueryString["DeviceType"].ToString() == "HTFG")
                {
                    Response.Redirect("~/HTLoginFG.aspx?DeviceType=HTFG");
                }
            }

        }

        public string getLoginPage()
        {
            string strLogin = "";


            if (Request.QueryString["DeviceType"] == null)
            {
                if (uPage == "PARTS LOCATION CHECK" || uPage == "PARTS LOCATION AND INSPECTION CHECK")
                {
                    strLogin = "PLCLogin.aspx";
                }
                else if (uPage == "RFID LOT LABEL PAIRING")
                {
                    strLogin = "RFIDLogin.aspx";
                }
                else
                {
                    strLogin = "HTLogin.aspx";
                }

            }
            else
            {
                if (Request.QueryString["DeviceType"].ToString() == "Denso")
                {
                    if (uPage == "PARTS LOCATION CHECK" || uPage == "PARTS LOCATION AND INSPECTION CHECK")
                    {
                        strLogin = "PLCLogin.aspx";
                    }
                    else if (uPage == "RFID LOT LABEL PAIRING")
                    {
                        strLogin = "RFIDLogin.aspx";
                    }
                    else
                    {
                        strLogin = "HTLogin.aspx";
                    }

                }
                else if (Request.QueryString["DeviceType"].ToString() == "HT")
                {
                    if (uPage == "PARTS LOCATION CHECK" || uPage == "PARTS LOCATION AND INSPECTION CHECK")
                    {
                        strLogin = "PLCLogin.aspx";
                    }
                    else if (uPage == "RFID LOT LABEL PAIRING")
                    {
                        strLogin = "RFIDLogin.aspx";
                    }
                    else
                    {
                        strLogin = "HTLogin.aspx";
                    }


                }
                else if (Request.QueryString["DeviceType"].ToString() == "HTFG")
                {
                    strLogin = "HTLoginFG.aspx";
                }
            }


            return strLogin;
        }

    }
}
