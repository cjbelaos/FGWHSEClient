using com.eppi.utils;
using FGWHSEClient.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OleDb;
using System.IO;
using System.Collections.Generic;

using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions; 

using System.Threading;
namespace FGWHSEClient.Form
{
    //kempee
    public partial class AutoLine_Barcode : System.Web.UI.Page
    {
        public string strPageSubsystem = "";
        public string strAccessLevel = "";

        AutoLineDAL autoDAL = new AutoLineDAL();
        PairedDAL pDAL = new PairedDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Write("<script>");
                Response.Write("alert('Session expired! Please log in again.');");
                Response.Write("window.location = '../Login.aspx';");
                Response.Write("</script>");
            }
            
            if (!this.IsPostBack)
            {
                strPageSubsystem = "FGWHSE_049";
                if (!checkAuthority(strPageSubsystem))
                {
                    Response.Write("<script>");
                    Response.Write("alert('You are not authorized to access the page.');");
                    Response.Write("window.location = 'Default.aspx';");
                    Response.Write("</script>");
                }
                clearAll();

                string clientIp = "";
                clientIp = (Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();
                Session["clientIp"] = clientIp;
                DataView dvStation = autoDAL.GET_AUTOLINE_STATION(clientIp.ToString(),"").Tables[0].DefaultView;
                string strStation = "";
                if (dvStation.Count > 0)
                {
                    strStation = dvStation[0]["STATIONNO"].ToString();
                } 
                else
                {
                    Response.Write("<script>");
                    Response.Write("alert('This PC is not registered as Auto-line Barcode Label station!');");
                    Response.Write("window.location = 'Default.aspx';");
                    Response.Write("</script>");
                }
               

                Session["STATION"] = strStation;
              
               
                

            }
            lblRemainingQTY.Text = txtRemainingQTY.Text;

            if (Request.Form["printConfirm"] != null)
            {
                if (Request.Form["printConfirm"].ToString().Equals("1"))
                {
                    btnGenerate.Enabled = false;
                    txtForPrint.Enabled = false;
                    txtKittedQTY.Enabled = false;
                    DataView dv = ((DataTable)Session["dtPRINT"]).DefaultView;
                    processLot(dv, Session["UserID"].ToString());
                    openLink("AutoLinePrint.aspx");
                }
                
            }

        }

        protected void txtRefno_TextChanged(object sender, EventArgs e)
        {
            string strRefNo = txtRefno.Text.Trim();

            if(strRefNo == "")
            {
                msgBoxPrompt("Please Enter valid Ref No!");
            }

            DataView dv = autoDAL.AUTOLINE_GET_LOT_DATA(strRefNo).Tables[0].DefaultView;
            if (dv.Count > 0)
            {
                lblLotNo.Text = dv[0]["LotID"].ToString();
                lblPartsDesc.Text = dv[0]["MaterialName"].ToString();
                lblRFIDTag.Text = dv[0]["RFIDTag"].ToString();
                lblLotNo.Text = dv[0]["LotNo"].ToString();
                lblPartCode.Text = dv[0]["PartCode"].ToString();
                lblOrigQTY.Text = dv[0]["Qty"].ToString();
                lblRemarks.Text = dv[0]["Remarks"].ToString();
                txtArea.Text = dv[0]["AreaID"].ToString();

                txtRefno.Enabled = false;
                txtForPrint.Enabled = true;
                txtKittedQTY.Enabled = true;
                txtKittedQTY.Focus();
                btnGenerate.Enabled = true;

            }
            else
            {
                msgBoxPrompt("No data found");
                clearAll();
                return;
            }
        }


        public void clearAll()
        {

            txtForPrint.Text = "";
            txtKittedQTY.Text = "";
            lblLotNo.Text = "";
            lblOrigQTY.Text = "";
            lblPartCode.Text = "";
            txtRefno.Text = "";
            lblRemainingQTY.Text = "";
            txtRemainingQTY.Text = "";
            lblRemarks.Text = "";
            lblRFIDTag.Text = "";
            txtArea.Text = "";

            lblPartsDesc.Text = "";
            txtRefno.Enabled = true;
            txtForPrint.Enabled = false;
            txtKittedQTY.Enabled = false;
            txtRefno.Focus();
            btnGenerate.Enabled = false;
        }

        public void msgBoxPrompt(string strMessage)
        {
            Response.Write("<script>");
            Response.Write("alert('"+ strMessage + "');");
            Response.Write("</script>");
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            


            if (txtKittedQTY.Text.Trim() == "")
            {
                msgBoxPrompt("No input for Kitted QTY!");
                return;
            }
            if (!(isValidText(@"[0-9]", txtKittedQTY.Text)))
            {
                msgBoxPrompt("Enter valid kitted QTY!");
                return;
            }

         
            int intPrintLoop = 0;
            decimal decLoopInitialWithDecimal = 0;
            decimal decLoopInitialWithoutDecimal = 0;
            Decimal decLastQty = 0;
            string strBgColorLast = "";// strBgColor = "BLACK";
            int intBR = 0, intBG = 0, intBB = 0, intFR = -1, intFG = -1, intFB = -1;

            DataTable dtPrint = new DataTable();
            dtPrint.Columns.Add("OLDREFNO", typeof(System.String));
            dtPrint.Columns.Add("RFIDTAG", typeof(System.String));
            dtPrint.Columns.Add("QRCODE", typeof(System.String));
            dtPrint.Columns.Add("ITEMCODE", typeof(System.String));
            dtPrint.Columns.Add("LOTNO", typeof(System.String));
            dtPrint.Columns.Add("REFNO", typeof(System.String));
            dtPrint.Columns.Add("QTY", typeof(System.String));
            dtPrint.Columns.Add("REMARKS", typeof(System.String));
            dtPrint.Columns.Add("AREA", typeof(System.String));
            dtPrint.Columns.Add("ITEMDESC", typeof(System.String));
            dtPrint.Columns.Add("BR", typeof(System.Int32));
            dtPrint.Columns.Add("BG", typeof(System.Int32));
            dtPrint.Columns.Add("BB", typeof(System.Int32));
            dtPrint.Columns.Add("FR", typeof(System.Int32));
            dtPrint.Columns.Add("FG", typeof(System.Int32));
            dtPrint.Columns.Add("FB", typeof(System.Int32));



            if (txtForPrint.Text.Trim() != "")
            {
                if (!(isValidText(@"[0-9]", txtForPrint.Text)))
                {
                    msgBoxPrompt("Enter valid For Print value!");
                    return;
                }

                int intRemQTY = Convert.ToInt32(txtRemainingQTY.Text);
                if (intRemQTY < 0)
                {
                    msgBoxPrompt("Remaining QTY must not be negative!");
                    return;
                }

                intPrintLoop = Convert.ToInt32(txtForPrint.Text);
                decLastQty = Convert.ToDecimal(lblOrigQTY.Text) - (Convert.ToDecimal(txtKittedQTY.Text) * intPrintLoop);

                if (decLastQty != 0)
                {
                    intPrintLoop = intPrintLoop + 1;
                }

                Session["WITHREMAIN"] = "1";
                strBgColorLast = "RED";
            }
            else
            {
               
                decLoopInitialWithDecimal = Convert.ToDecimal(lblOrigQTY.Text)/ Convert.ToDecimal(txtKittedQTY.Text);
                decLoopInitialWithoutDecimal = Math.Truncate(Convert.ToDecimal(lblOrigQTY.Text) / Convert.ToDecimal(txtKittedQTY.Text));
                if (decLoopInitialWithoutDecimal == 0)
                {
                    msgBoxPrompt("Enter valid kitted QTY!");
                    return;
                }

                intPrintLoop = Convert.ToInt32(decLoopInitialWithoutDecimal);
                if (decLoopInitialWithDecimal > decLoopInitialWithoutDecimal)
                {
                    decLastQty = Convert.ToDecimal(lblOrigQTY.Text) - (Convert.ToDecimal(txtKittedQTY.Text) * intPrintLoop);
                    intPrintLoop = intPrintLoop + 1;
                }
              

            }

            DataRow dr;

            string strQRCode = "", strItemCode = lblPartCode.Text, strLotNo = lblLotNo.Text, strRefNo = "", strQTY = "", strRemarks = lblRemarks.Text, strArea = txtArea.Text;
            for (int x = 1; x <= intPrintLoop; x++)
            {
                strQTY = txtKittedQTY.Text;
                if(decLastQty != 0 && x == intPrintLoop)
                {
                    strQTY = decLastQty.ToString();
                }

                int milliseconds = 100;
                Thread.Sleep(milliseconds);

                strRefNo = genRefno(txtRefno.Text.Trim().Substring(0, 1),strLotNo.Trim().Substring(0, 3));
                strQRCode = getQR(strItemCode,strLotNo,strRefNo,strQTY,strRemarks);
               
                if(strBgColorLast != "" && x == intPrintLoop)
                {
                    DataView dvFColor = autoDAL.GET_AUTOLINE_RGB("", "").Tables[0].DefaultView;
                    if (dvFColor.Count > 0)
                    {
                        intBR = Convert.ToInt32(dvFColor[0]["INTR"].ToString());
                        intBG = Convert.ToInt32(dvFColor[0]["INTG"].ToString());
                        intBB = Convert.ToInt32(dvFColor[0]["INTB"].ToString());
                    }
                }
                dr = dtPrint.NewRow();
                dr["OLDREFNO"] = txtRefno.Text.Trim();
                dr["RFIDTAG"] = lblRFIDTag.Text.Trim();
                dr["QRCODE"] = strQRCode;
                dr["ITEMCODE"] = strItemCode;
                dr["LOTNO"] = strLotNo;
                dr["REFNO"] = strRefNo;
                dr["QTY"] = Convert.ToDecimal(strQTY).ToString("0.##");
                dr["REMARKS"] = strRemarks;
                dr["AREA"] = strArea;
                dr["ITEMDESC"] = lblPartsDesc.Text.Trim();
                dr["BR"] = intBR;
                dr["BG"] = intBG;
                dr["BB"] = intBB;
                dr["FR"] = intFR;
                dr["FG"] = intFG;
                dr["FB"] = intFB;
                dtPrint.Rows.Add(dr);
            }
            Session["dtPRINT"] = dtPrint;
            msgBox.confirm("Printing new Lot will delete the original Lot. Do you want to continue?", "printConfirm");
            
        }

        public void openLink(string lnk)
        {
            string strURL = lnk;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + strURL + "','_newtab');", true);
        }

        public string genRefno(string strIJPorVP, string strSupp)
        {
            string strRefno = "";


            //string strClientIP = (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();
            string strPCStation = Session["STATION"].ToString();//"999";

                    //if (strClientIP != "")
                    //{
                    //    string[] strIP = strClientIP.Split('.');
                    //    if (strIP.Length == 4)
                    //    {
                    //        if (Convert.ToInt32(strIP[3].Trim()) < 10)
                    //        {
                    //            strPCStation = "00" + strIP[3].Trim();
                    //        }
                    //        else if (Convert.ToInt32(strIP[3].Trim()) < 100)
                    //        {
                    //            strPCStation = "0" + strIP[3].Trim();
                    //        }
                    //        else
                    //        {
                    //            strPCStation = strIP[3].Trim();
                    //        }
                    //    }
                    //}



            string year = DateTime.Now.ToString("yy");

                    string month = DateTime.Now.ToString("MM");
                    if (month.Length >= 2)
                    {
                        if (month == "10")
                        {
                            month = "X";
                        }
                        else if (month == "11")
                        {
                            month = "Y";
                        }
                        else if (month == "12")
                        {
                            month = "Z";
                        }
                        else
                        {
                            month = DateTime.Now.ToString("MM").Substring(1, 1);
                        }
                    }
                    string day = DateTime.Now.ToString("dd");
                    string series = DateTime.Now.ToString("HHmmssfff");

                    strRefno = strIJPorVP.Trim() + strSupp.Trim() + strPCStation + year + month + day + series;

            return strRefno;
        }
        public string getQR(string strItemCode, string strLotNo , string strRefNo , string strQTY , string strRemarks)
        {
            string strQR = "";
            strQR = "Z1" + strItemCode +
                         "|Z7" + strLotNo.Trim().Substring(0, 3) +
                         "|Z2" + strLotNo +
                         "|Z3" + strQTY +
                         "|Z4" + strRemarks +
                         "|Z5" + strRefNo +
                         "|Z6MW6DEMO";
            return strQR;
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        public void processLot(DataView dv, string strUID)
        {
            string srtRFIDTag = "",strRefNo = "", strLotNo = "", strItemCode = "", strQTY = "", strRemarks = "", strArea = "";

            for (int x = 0; x < dv.Count; x++)
            {
                strRefNo = dv[x]["REFNO"].ToString();
                strLotNo = dv[x]["LOTNO"].ToString();
                strItemCode = dv[x]["ITEMCODE"].ToString();
                strQTY = dv[x]["QTY"].ToString();
                strRemarks = dv[x]["REMARKS"].ToString();
                strArea = dv[x]["AREA"].ToString();

                if (x == 0)
                {
                    pDAL.UPDATE_RFID_FINISH_FLAG(txtRefno.Text);
                }

                if (x + 1 == dv.Count && Session["WITHREMAIN"] != null)
                {
                    srtRFIDTag = lblRFIDTag.Text;
                    Session["WITHREMAIN"] = null;
                }

                //autoDAL.ADD_AUTOLINE_BARCODE(txtRefno.Text.Trim(), strRefNo, srtRFIDTag, strLotNo, strItemCode, strQTY, strRemarks, strUID, strArea, Session["clientIp"].ToString());
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
                //Response.Write("<script>");
                //Response.Write("alert('" + ex.Message.ToString() + "');");
                //Response.Write("</script>");

                isValid = false;
                return isValid;
            }
        }

      
    }
}