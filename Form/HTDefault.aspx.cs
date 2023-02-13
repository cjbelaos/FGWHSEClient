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
    public partial class HTDefault : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["UserName"] == null)
                {
                    Response.Write("<script>");
                    Response.Write("alert('Session expired! Please log in again.');");
                    Response.Write("window.location = '../HTLogin.aspx';");
                    Response.Write("</script>");
                }

                lblSystemName.Text = ConfigurationManager.AppSettings["systemName"].ToString();
                lblVersion.Text = "Version " + ConfigurationManager.AppSettings["systemVersion"].ToString();
                lblPublishDate.Text = "Published : " + ConfigurationManager.AppSettings["Published"].ToString();

                if ((ConfigurationManager.AppSettings["updateDate"].ToString()) != "")
                {
                    lblUpdateDate.Text = " • Updated : " + ConfigurationManager.AppSettings["updateDate"].ToString();
                }

                lblRemarks.Text = ConfigurationManager.AppSettings["Remarks"].ToString();
            }
            catch (Exception ex)
            {
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);
                Logger.GetInstance().Fatal(ex.Message);
            }
        }
    }
}
