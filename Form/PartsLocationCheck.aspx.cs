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
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Drawing;
using FGWHSEClient.DAL;

namespace FGWHSEClient
{
    public partial class PartsLocationCheck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtPartCode.Focus();
            }
        }

        protected void txtPartCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                PartsLocationCheckDAL maint = new PartsLocationCheckDAL();
                DataView dvGetLocation = new DataView();
                //Changed to GetPartName_rev1 instead of GetPartName Aug 24 Aldrin
                dvGetLocation = maint.GetPartName_rev1(txtPartCode.Text.Trim());

                if (dvGetLocation.Table.Rows.Count == 0)
                {
                    msgBox.alert("No record found.");
                    txtPartCode.Text = "";
                    txtPartCode.Focus();
                }
                else
                {

                    lblPartname.Text = dvGetLocation.Table.Rows[0]["PartDescription"].ToString();
                    lblLocation.Text = dvGetLocation.Table.Rows[0]["Location"].ToString();
                    lblLocationST.Text = dvGetLocation.Table.Rows[0]["Location_ST"].ToString();
                    lblPartCode.Text = dvGetLocation.Table.Rows[0]["PartCode"].ToString();
                    //TextBox1.Text = dvGetLocation.Table.Rows[0]["PartDescription"].ToString();


                    if (lblLocation.Text == "2F Common")
                    {
                        divlocation.Style["background-color"] = "Green";
                        lblLocation.ForeColor = System.Drawing.Color.White;
                    }
                    else if (lblLocation.Text == "2F Unique")
                    {
                        divlocation.Style["background-color"] = "Yellow";
                        lblLocation.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        divlocation.Style["background-color"] = "Silver";
                        lblLocation.ForeColor = System.Drawing.Color.Black;
                    }

                    txtPartCode.Text = "";
                    txtPartCode.Focus();
                }





            }
            catch (Exception ex)
            {
                msgBox.alert(ex.Message);
            }
        }
    }
}
