using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace FGWHSEClient
{
    public partial class VanningPoolPalletAllocation : System.Web.UI.Page
    {
        Maintenance maint = new Maintenance();
        string strUserID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Write("<script>");
                Response.Write("alert('Session expired! Please log in again.');");
                Response.Write("window.location = '../Login.aspx';");
                Response.Write("</script>");
            }
            else
            {
                strUserID = Session["UserID"].ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLocationID.Text.Trim() != "" && txtPalletNo.Text.Trim() != "")
                {
                    maint.PICKING_VANNING_POOL_PALLET_ALLOCATION(txtLocationID.Text.Trim(), txtPalletNo.Text.Trim(), strUserID);
                    //txtLocationID.Text = "";
                    txtPalletNo.Text = "";
                    txtPalletNo.Focus();
                    MsgBox1.alert("Successfully saved!");
                }
                else
                {
                    MsgBox1.alert("Please fill-up required fields!");
                    return;
                }
            }
            catch (Exception EX)
            {
                MsgBox1.alert(EX.Message.ToString());
            }
        }

        

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtPalletNo.Text = "";
            txtLocationID.Text = "";
        }

        protected void txtPalletNo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtLocationID_TextChanged(object sender, EventArgs e)
        {

        }   
    }
}
