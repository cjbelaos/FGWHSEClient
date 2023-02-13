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

namespace FGWHSEClient.Form
{
    public partial class PartsDeliveryInspection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtDNNo.Focus();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            if (txtDNNo.Text.Trim() == "")
            {
                MsgBox1.alert("Please input DN No.");
            }
            else
            {
                Response.Redirect("PartsDeliveryInspectionPreview.aspx?DNNO=" + txtDNNo.Text.Trim() + "&print=0");

            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtDNNo.Text.Trim() == "")
            {
                MsgBox1.alert("Please input DN No.");
            }
            else
            {
                Response.Redirect("PartsDeliveryInspectionPreview.aspx?DNNO=" + txtDNNo.Text.Trim() + "&print=2");

            }
        }

       
    }
}
