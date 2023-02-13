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
    public partial class DENSOMasterPalletMonitoring : System.Web.UI.MasterPage
    {
        protected string strName = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Write("<script>");
                    Response.Write("alert('Session expired! Please log in again.');");
                    Response.Write("window.location = '../DENSOLogin.aspx';");
                    Response.Write("</script>");
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
                if (currentPage == "HTMenu.aspx")
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
            Response.Redirect("HTMenu.aspx?DeviceType=Denso");
        }


        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DENSOLogin.aspx");
        }
    }
}
