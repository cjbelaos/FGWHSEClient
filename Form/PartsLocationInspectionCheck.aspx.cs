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

namespace FGWHSEClient.Form
{
    public partial class PartsLocationInspectionCheck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDN.Focus();
            }
        }

        protected void txtLotQR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strLotQRData = "|" + txtLotQR.Text.Trim().ToUpper() + "|Z";
                string strItemCOde = getBetween(strLotQRData, "|Z1", "|Z");
                string strLotNo = getBetween(strLotQRData, "|Z2", "|Z");

                PartsLocationCheckDAL maint = new PartsLocationCheckDAL();
                
                DataSet dsGetLoc = new DataSet();
                dsGetLoc = maint.GET_PARTSINSPECTIONLOCATIONCHECK(strItemCOde.Trim(), strLotNo.Trim(), txtDN.Text.Trim());
                
                if (dsGetLoc.Tables[0].Rows.Count == 0)
                {
                    msgBox.alert("No record found.");
                    txtLotQR.Text = "";
                    txtLotQR.Focus();
                }
                else
                {

                    lblPartname.Text = dsGetLoc.Tables[0].Rows[0]["PARTNAME"].ToString();
                    lblLocation.Text = dsGetLoc.Tables[0].Rows[0]["LOCATION"].ToString();
                    lblPartCode.Text = dsGetLoc.Tables[0].Rows[0]["ITEMLOT"].ToString();
                    string strJudgeMent = dsGetLoc.Tables[0].Rows[0]["JUDGEMENT"].ToString();
                    //TextBox1.Text = dvGetLocation.Table.Rows[0]["PartDescription"].ToString();


                    if (strJudgeMent == "NG")
                    {
                        divlocation.Style["background-color"] = "Orange";
                        lblLocation.ForeColor = System.Drawing.Color.Black;
                    }
                    //else if (lblLocation.Text == "2F Unique")
                    //{
                    //    divlocation.Style["background-color"] = "Yellow";
                    //    lblLocation.ForeColor = System.Drawing.Color.Black;
                    //}
                    else
                    {
                        divlocation.Style["background-color"] = "Silver";
                        lblLocation.ForeColor = System.Drawing.Color.Black;
                    }
                    txtLotQR.Text = "";
                    txtLotQR.Focus();
                }
            }
            catch (Exception ex)
            {
                msgBox.alert(ex.Message);
            }
        }

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

        protected void txtDN_TextChanged(object sender, EventArgs e)
        {
            txtLotQR.Focus();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtDN.Text = "";
            txtDN.Focus();
            lblPartCode.Text = "";
            lblLocation.Text = "";
            lblPartname.Text = "";
            divlocation.Style["background-color"] = "Silver";
            lblLocation.ForeColor = System.Drawing.Color.Black;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            txtLotQR.Focus();
        }
    }
}
