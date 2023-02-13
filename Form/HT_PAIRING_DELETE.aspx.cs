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

using System.Text.RegularExpressions;
using FGWHSEClient.DAL;

namespace FGWHSEClient.Form
{
    public partial class HT_PAIRING_DELETE : System.Web.UI.Page
    {
        protected PairedDAL pDAL = new PairedDAL();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Write("<script>");
                Response.Write("alert('Session expired! Please log in again.');");
                Response.Write("window.location = '../HTLogin.aspx';");
                Response.Write("</script>");
            }
            else
            {
                if (!this.IsPostBack)
                {

                    txtLot.Attributes.Add("onkeydown", "return (event.keyCode!=13);false;");
                    txtScannedRFID.Attributes.Add("onkeydown", "return (event.keyCode!=13);false;");
                }
            }
        }


        protected void btnLot_Click(object sender, EventArgs e)
        {
            string strErrMSG = scannLot(txtLot);
            if (strErrMSG != "")
            {
                txtMSG.Attributes.Add("style", "color:red");
                txtMSG.Text = strErrMSG;
                txtLot.Text = "";
                txtLot.Focus();
                return;
            }

            txtMSG.Text = "";

        }


        protected void btnRFID_Click(object sender, EventArgs e)
        {
            string strErrMSG = scannRFID(txtScannedRFID);
            if (strErrMSG != "")
            {
                txtMSG.Attributes.Add("style", "color:red");
                txtMSG.Text = strErrMSG;
                txtScannedRFID.Text = "";
                txtScannedRFID.Focus();
                return;
            }
            txtMSG.Text = "";
            txtLot.Focus();
        }

        public string scannRFID(TextBox txtScnRFID)
        {
            string strErrorMSG = "";

            string strRFID = txtScnRFID.Text.Trim();
            if (isValidText("[A-Za-z0-9]", strRFID) == false)
            {
                return "INVALID RFID!";
            }

            if ((strRFID.Length > 30) || (strRFID.Length < 24))
            {
                return "INVALID RFID TAG";
            }

            DataView dv = new DataView();
            dv = pDAL.CHECK_TAGS_IF_PAIRED(strRFID, "").Tables[0].DefaultView;
            if (dv.Count == 0)
            {
                return "RFID TAG IS NOT YET PAIRED";
            }
            return strErrorMSG;
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

        public string scannLot(TextBox txtscnLot)
        {
            string strErrorMSG = "";
            string strLot = "|" + txtscnLot.Text + "|Z";

            string strItemCode = getBetween(strLot, "|Z1", "|Z");
            string strLotNo = getBetween(strLot, "|Z2", "|Z");
            string strRefNo = getBetween(strLot, "|Z5", "|Z");
            string strQty = getBetween(strLot, "|Z3", "|Z");
            string strRemarks = getBetween(strLot, "|Z4", "|Z");

            if (strLot.Length == 0)
            {
                return "NO SCANNED LOT";
            }

            if (txtscnLot.Text != "" && (strItemCode == "" || strLotNo == "" || strRefNo == "" || strQty == "" || strRemarks == ""))
            {
                return "INVALID QR";
            }

            DataView dv = new DataView();
            dv = pDAL.CHECK_TAGS_IF_PAIRED(txtScannedRFID.Text.Trim(), strRefNo).Tables[0].DefaultView;
            if (dv.Count == 0)
            {
                return "LOT NO. IS NOT YET PAIRED";
            }

            return strErrorMSG;
        }

        protected void btnDELETE_Click(object sender, EventArgs e)
        {
            string strErrMSG = scannLot(txtLot);
            if (strErrMSG != "")
            {
                txtMSG.Attributes.Add("style", "color:red");
                txtMSG.Text = strErrMSG;
                txtLot.Text = "";
                txtLot.Focus();
                return;
            }

            if (strErrMSG != "")
            {
                txtMSG.Attributes.Add("style", "color:red");
                txtMSG.Text = strErrMSG;
                txtScannedRFID.Text = "";
                txtScannedRFID.Focus();
                return;
            }

            string strRefNo = getBetween(txtLot.Text.Trim(), "|Z5", "|Z");

            pDAL.DELETE_PAIRED_TAGS(txtScannedRFID.Text.Trim(), strRefNo, Session["UserID"].ToString());


            txtMSG.Attributes.Add("style", "color:green");
            txtMSG.Text = "SUCCESSFULLY DELETED";
            txtLot.Text = "";
            txtScannedRFID.Text = "";
            txtScannedRFID.Focus();
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtScannedRFID.Text = "";
            txtLot.Text = "";
            txtScannedRFID.Focus();
            txtMSG.Text = "";
        }
    }
}