using System;
using System.Collections.Generic;
using com.eppi.utils;
using FGWHSEClient.DAL;
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

using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

using System.Threading;

namespace FGWHSEClient.Form
{
    public partial class AutoLine_Barcode_Generate : System.Web.UI.Page
    {
        public string strPageSubsystem = "";
        public string strAccessLevel = "";
        AutoLineDAL autoDAL = new AutoLineDAL();
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
                strPageSubsystem = "FGWHSE_051";
                if (!checkAuthority(strPageSubsystem))
                {
                    Response.Write("<script>");
                    Response.Write("alert('You are not authorized to access the page.');");
                    Response.Write("window.location = 'Default.aspx';");
                    Response.Write("</script>");
                }

                string clientIp = "";
                clientIp = (Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();
                DataView dvStation = autoDAL.GET_AUTOLINE_STATION(clientIp.ToString(), "").Tables[0].DefaultView;
                string strStation = "";
                Session["clientIp"] = clientIp;
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

                GET_FACTORY();
                FillDropDown(ddLine, autoDAL.GET_AUTOLINE_LINE(ddFACTORY.SelectedItem.Text));
                FillDropDown(ddMold, autoDAL.GET_AUTOLINE_MOLD());
                FillDropDown(ddCavity, autoDAL.GET_AUTOLINE_CAVITY());
                FillDropDown(ddShift, autoDAL.GET_AUTOLINE_SHIFT(ddFACTORY.SelectedItem.Text));
                FillDropDown(ddColor, autoDAL.GET_AUTOLINE_COLOR());
                getMoldCavityLine();
                enableDisableAll("dis");
                bindGrid(grdItemCode, autoDAL.GET_AUTOLINE_ITEMCODE(ddFACTORY.SelectedValue.ToString().Trim()).Tables[0].DefaultView);
            }

        }

        public string getMoldCavityLine()
        {
            string strMoldCavityLine = "";
            if (chkLine.Checked == true)
            {
                strMoldCavityLine = getLine();
            }
            else if (chkMold.Checked == true)
            {
                strMoldCavityLine = getMoldCavity();
            }
               

            return strMoldCavityLine;
            
        }

        private string getLine()
        {
            string line = "";

            ddLine.Visible = true;
            ddMold.Visible = false;
            ddCavity.Visible = false;

            line = ddLine.SelectedValue.ToString().Trim();

            if ((ddFACTORY.SelectedItem.Text.Trim() == "IJP" || ddFACTORY.SelectedItem.Text.Trim() == "VP") && line.Length < 2)
            {
                line = "0" + line;

            }
            else if (ddFACTORY.SelectedItem.Text.Trim() == "INK")
            {
                line = line;
            }

            
            return line;

        }
        private string getMoldCavity()
        {
            string moldcavity = "";
            ddLine.Visible = false; ;
            ddMold.Visible = true;
            ddCavity.Visible = true;

            moldcavity = ddMold.SelectedValue.ToString().Trim() + ddCavity.SelectedValue.ToString().Trim();


            return moldcavity;

        }
        public void GET_FACTORY()
        {
            ddFACTORY.DataSource = autoDAL.GET_AUTOLINE_FACTORY();
            ddFACTORY.DataTextField = "FACTORY";
            ddFACTORY.DataValueField = "FACTORY";
            ddFACTORY.DataBind();
            ddFACTORY.SelectedIndex = 0;
        }


        private void FillDropDown(DropDownList dd, DataSet ds)
        {
            dd.DataSource = ds;
            dd.DataTextField = "DESCRIPTION";
            dd.DataValueField = "ID";
            dd.DataBind();
            dd.SelectedIndex = 0;
        }

        private void removeFillDropDown(DropDownList dd)
        {
            dd.DataSource = null;
            dd.DataBind();
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
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            if (chkGen.Checked)
            {
                if ((txtItemCode.Text.Trim() == "") || (txtProdDate.Text.Trim() == "") ||
                    (txtSeries.Text.Trim() == "") || (txtSupplier.Text.Trim() == "") ||
                    (txtQTY.Text.Trim() == "") || (txtPQTY.Text.Trim() == "")
                    || txtRemarks.Text.Trim() == ""
                    )
                {

                    MsgBox1.alert("Please fill required fields.");
                    return;
                }
                else
                {
                    DateTime proddate;
                    bool date;

                    date = DateTime.TryParse(txtProdDate.Text.Trim(), out proddate);

                    if (!date)
                    {
                        MsgBox1.alert("Invalid production date. Please check date");
                        return;
                    }
                    if (Convert.ToInt32(txtQTY.Text) < 1 || Convert.ToInt32(txtPQTY.Text) < 1)
                    {
                        MsgBox1.alert("Quantity/Std. Pkg. Qty. Must be greater than zero(0)");
                        return;
                    }
                }

            }
            else if (chkGen.Checked == false)
            {
                if ((txtSupplier.Text.Trim() == "")
                    || (txtLotNo.Text.Trim() == "")
                    || (txtQTY.Text.Trim() == "") || (txtPQTY.Text.Trim() == "") ||
                    (txtRemarks.Text.Trim() == ""))
                { 
                    MsgBox1.alert("Please fill required fields.");
                    return;
                }
                else if (Convert.ToInt32(txtQTY.Text) < 1 || Convert.ToInt32(txtPQTY.Text) < 1)
                {
                    MsgBox1.alert("Quantity/Std. Pkg. Qty. Must be greater than zero(0)");
                    return;
                }

            }


            printPrev();
        }



        public void printPrev()
        {
            if (!(isValidText(@"[0-9]", txtQTY.Text)))
            {
                MsgBox1.alert("Enter valid QTY!");
                return;
            }

            if (!(isValidText(@"[0-9]", txtPQTY.Text)))
            {
                MsgBox1.alert("Enter valid Packaging QTY!");
                return;
            }


            if (!(isValidText(@"[0-9]", txtSeries.Text)))
            {
                MsgBox1.alert("Enter valid Series!");
                return;
            }

            int intPrintLoop = 0;
            decimal decLoopInitialWithDecimal = 0;
            decimal decLoopInitialWithoutDecimal = 0;
            Decimal decLastQty = 0;
            string strBgColorLast = "", strBgColor = "BLACK";
            int intBR = 0, intBG = 0, intBB = 0, intFR = -1, intFG = -1, intFB = -1;

            if (chkWSpecs.Checked == true)
            {
                DataView dvFColor = autoDAL.GET_AUTOLINE_RGB(ddSpecsColor.SelectedValue, "YES").Tables[0].DefaultView;
                if(dvFColor.Count > 0)
                {
                    intFR = Convert.ToInt32(dvFColor[0]["INTR"].ToString());
                    intFG = Convert.ToInt32(dvFColor[0]["INTG"].ToString());
                    intFB = Convert.ToInt32(dvFColor[0]["INTB"].ToString());
                }
                //strFColor = ddSpecsColor.SelectedValue;
            }

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





            decLoopInitialWithDecimal = Convert.ToDecimal(txtQTY.Text) / Convert.ToDecimal(txtPQTY.Text);
            decLoopInitialWithoutDecimal = Math.Truncate(Convert.ToDecimal(txtQTY.Text) / Convert.ToDecimal(txtPQTY.Text));
            if (decLoopInitialWithoutDecimal == 0)
            {
                MsgBox1.alert("Enter valid QTY!");
                return;
            }

            intPrintLoop = Convert.ToInt32(decLoopInitialWithoutDecimal);
            if (decLoopInitialWithDecimal > decLoopInitialWithoutDecimal)
            {
                decLastQty = Convert.ToDecimal(txtQTY.Text) - (Convert.ToDecimal(txtPQTY.Text) * intPrintLoop);
                intPrintLoop = intPrintLoop + 1;
            }


            DataRow dr;

            string strQRCode = "", strItemCode = txtPartCode.Text.Trim(), strLotNo = txtLotNo.Text.Trim(), strRefNo = "", strQTY = "", strRemarks = txtRemarks.Text.Trim(), strArea = "", strPartsDesc = txtPartsDesc.Text.Trim();

            bool boolInc = false;
            if (chkAutoSeries.Checked == true)
            {
                boolInc = true;
            }
            for (int x = 1; x <= intPrintLoop; x++)
            {
                strQTY = txtPQTY.Text;
                if (decLastQty != 0 && x == intPrintLoop)
                {
                    strQTY = decLastQty.ToString();
                }


                if (chkGen.Checked == true)
                {
                    strLotNo = getLotNumber_Auto() + getSeries(txtSeries.Text.Trim(), boolInc, x - 1);
                }

                int milliseconds = 100;
                Thread.Sleep(milliseconds);

                strRefNo = genRefno();
                strQRCode = getQR(strItemCode, strLotNo, strRefNo, strQTY, strRemarks);

                if (strBgColorLast != "" && x == intPrintLoop)
                {
                    strBgColor = strBgColorLast;
                }
                dr = dtPrint.NewRow();
                dr["OLDREFNO"] = "";
                dr["RFIDTAG"] = "";
                dr["QRCODE"] = strQRCode;
                dr["ITEMCODE"] = strItemCode;
                dr["LOTNO"] = strLotNo;
                dr["REFNO"] = strRefNo;
                dr["QTY"] = Convert.ToDecimal(strQTY).ToString("0.##");
                dr["REMARKS"] = strRemarks;
                dr["AREA"] = strArea;
                dr["ITEMDESC"] = strPartsDesc;
                dr["BR"] = intBR;
                dr["BG"] = intBG;
                dr["BB"] = intBB;
                dr["FR"] = intFR;
                dr["FG"] = intFG;
                dr["FB"] = intFB;
                dtPrint.Rows.Add(dr);
            }

            Session["dtPRINT"] = dtPrint;
            DataView dv = dtPrint.DefaultView;
            //processLot(dv, Session["UserID"].ToString());

            openLink("AutoLinePrint.aspx");
        }

        public void openLink(string lnk)
        {
            string strURL = lnk;

            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + strURL + "','_newtab');", true);
        }
        private string getLotNumber_Auto()
        {
            string lotnumber, itemcode, proddate, shift;
            string mm, dd, yy;
            string moldcavityline = "";

            itemcode = txtItemCode.Text.Trim();
            dd = Convert.ToDateTime(txtProdDate.Text.Trim()).ToString("dd");
            yy = Convert.ToDateTime(txtProdDate.Text.Trim()).ToString("yy");
            mm = Convert.ToDateTime(txtProdDate.Text.Trim()).Month.ToString();

            if (ddFACTORY.SelectedItem.Text == "IJP" || ddFACTORY.SelectedItem.Text == "VP")
            {
                if (mm == "10")
                {
                    mm = "X";
                }
                else if (mm == "11")
                {
                    mm = "Y";
                }
                else if (mm == "12")
                {
                    mm = "Z";
                }
            }
            else if (ddFACTORY.SelectedItem.Text.Trim() == "INK")
            {
                if (mm == "10")
                {
                    mm = "A";
                }
                else if (mm == "11")
                {
                    mm = "B";
                }
                else if (mm == "12")
                {
                    mm = "C";
                }
            }
            proddate = yy + mm + dd;

            shift = ddShift.SelectedValue.ToString().Trim();

            if (chkLine.Checked)
            {
                moldcavityline = getLine();
            }
            else if (chkMold.Checked)
            {
                moldcavityline = ddMold.SelectedValue.ToString().Trim() +
                                ddCavity.SelectedValue.ToString().Trim();
            }
            else
            {
                moldcavityline = getLine();
            }


            lotnumber = itemcode + proddate + shift + moldcavityline;

            return lotnumber;
        }

        public string getQR(string strItemCode, string strLotNo, string strRefNo, string strQTY, string strRemarks)
        { 
            string strQR = "";
            strQR = "Z1" + strItemCode +
                         "|Z7" + txtSupplier.Text.Trim().Substring(0, 3) +
                         "|Z2" + strLotNo +
                         "|Z3" + strQTY +
                         "|Z4" + strRemarks +
                         "|Z5" + strRefNo +
                         "|Z6MW6DEMO";
            return strQR;
        }

        public string genRefno()
        {
            string strRefno = "";

            
            string strPCStation = Session["STATION"].ToString();//"999";
            
            string day = DateTime.Now.ToString("dd");
            string series = DateTime.Now.ToString("HHmmssfff");

            //strRefno = strIJPorVP.Trim() + strSupp.Trim() + strPCStation + year + month + day + series;
            string fac = "";
            if (ddFACTORY.SelectedItem.Text.Trim() == "IJP" || ddFACTORY.SelectedItem.Text.Trim() == "VP")
            {
                fac = ddFACTORY.Text.Substring(0, 1);
            }
            else if (ddFACTORY.SelectedItem.Text.Trim() == "INK")
            {
                fac = "B";
            }
            string sup = txtSupplier.Text.Substring(0, 3);
            DateTime time = DateTime.Now;
            string year = Convert.ToString(time.Year);
            //string yr = year.Substring(3,1);
            string yr = year.Substring(2, 2);
            string mm = Convert.ToString(time.Month);


            if (mm == "10")
            {
                mm = "X";
            }
            else if (mm == "11")
            {
                mm = "Y";
            }
            else if (mm == "12")
            {
                mm = "Z";
            }

            string date1 = time.Date.ToString("MM/dd/yyyy");
            string dd = date1.Substring(3, 2);
            string time1 = Convert.ToString(time.TimeOfDay);
            string hh = time1.Substring(0, 2);
            string min = time1.Substring(3, 2);
            string sec = time1.Substring(6, 2);
            string milli = time1.Substring(9, 3);

            strRefno = fac + sup + strPCStation + yr + mm + dd + hh + min + sec + milli;

            return strRefno;
        }

        public string getSeries(string strSeriesText, bool willIncrement, int intIncrement)
        {
            if(willIncrement == true)
            {
                strSeriesText = (Convert.ToInt32(strSeriesText) + intIncrement).ToString();
            }
            string strSeries = "0000" + strSeriesText;

            return strSeries.Substring(strSeries.Length - 4);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Write("<script>");
            Response.Write("alert('Successfully Cleared');");
            Response.Write("window.location = 'AutoLine_Barcode_Generate.aspx';");
            Response.Write("</script>");
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            getMoldCavityLine();
        }

        
    
        public void validDateGNS(string en)
        {
            bool boolEnable = false;

            if (en == "enable")
            {
                boolEnable = true;
            }
            txtPartsDesc.Enabled = boolEnable;

        }


        public void enableDisableAll(string en)
        {
            enableDisable(en);
            validDateGNS(en);
            enableDisableSpecs();
        }

        public void enableDisableSpecs()
        {
            string en = "dis";
            bool boolEnable = false;
            string strFactory = ddFACTORY.SelectedValue.ToString().Trim();
            //if(strFactory == "INK" && chkGen.Checked == true)
            if (strFactory == "INK")
            {
                en = "enable";
            }
          
            if (en == "enable")
            {
                boolEnable = true;
                FillDropDown(ddSpecs, autoDAL.GET_AUTOLINE_SPECIFICATIONS(ddFACTORY.SelectedItem.Text));
                FillDropDown(ddSpecsColor, autoDAL.GET_AUTOLINE_SPECIFICATIONS_COLOR());
            }
            else
            {
                chkWSpecs.Checked = false;
                removeFillDropDown(ddSpecsColor);
                removeFillDropDown(ddSpecs);
            }

            chkWSpecs.Enabled = boolEnable;

            if (chkWSpecs.Checked == true)
            {
                ddSpecsColor.Enabled = boolEnable;
                ddSpecs.Enabled = boolEnable;
            }
            else
            {
                ddSpecsColor.Enabled = false;
                ddSpecs.Enabled = false;
            }
        }
        public void enableDisable(string en)
        {
            bool boolEnable = false;
            string strDisplay = "none";

            if (en == "enable")
            {
                boolEnable = true;
                strDisplay = "compact";
            }

            tdItem.Attributes.Add("style", "display:" + strDisplay);
            txtItemCode.Enabled = boolEnable;
            txtItemCode.Text = "";
            txtProdDate.Enabled = boolEnable;
            txtProdDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            ddLine.Enabled = boolEnable;
            ddCavity.Enabled = boolEnable;
            ddMold.Enabled = boolEnable;
            ddShift.Enabled = boolEnable;
            txtSeries.Enabled = boolEnable;
            txtSeries.Text = "";
            chkAutoSeries.Enabled = boolEnable;
            chkAutoSeries.Checked = false;
        }

        protected void chkGen_CheckedChanged(object sender, EventArgs e)
        {
            string strEn = "enable";
            if (chkGen.Checked == false)
            {
                strEn = "dis";
            }
            enableDisable(strEn);
            enableDisableSpecs();
        }

        protected void chkValidate_CheckedChanged(object sender, EventArgs e)
        {
            string en = "enable";
            if(chkValidate.Checked == true)
            {
                en = "dis";
            }
            validDateGNS(en);

        }

        protected void ddFACTORY_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindGrid(grdItemCode, autoDAL.GET_AUTOLINE_ITEMCODE(ddFACTORY.SelectedValue.ToString().Trim()).Tables[0].DefaultView);
            enableDisableSpecs();
        }

        protected void chkWSpecs_CheckedChanged(object sender, EventArgs e)
        {
            enableDisableSpecs();
        }

        protected void grdItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdItemCode_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            string itemcode = grdItemCode.Rows[e.NewSelectedIndex].Cells[1].Text.ToString();
            txtItemCode.Text = itemcode;
        }

        public void bindGrid(GridView grd, DataView dv)
        {
            grdItemCode.DataSource = dv;
            grdItemCode.DataBind();

            if (grdItemCode.Rows.Count < 1)
            {
                lblErrorMessage.Text = "No Data Found...";
                pnlItemCode.ScrollBars = ScrollBars.None;
            }
            else
            {
                lblErrorMessage.Text = "Select Item Code...";
                pnlItemCode.ScrollBars = ScrollBars.Vertical;
            }
        }

        protected void txtPartCode_TextChanged(object sender, EventArgs e)
        {
            ValidatePartcode();
        }

        void ValidatePartcode()
        {

            if (chkValidate.Checked)
            {
                try
                {
                    txtPartsDesc.Enabled = false;
                    DataView dvPartcode = new DataView();
                    dvPartcode = autoDAL.GET_AUTOLINE_ITEMCODE_DESC(txtPartCode.Text).Tables[0].DefaultView;

                    if (dvPartcode.Count > 0)
                    {
                        txtPartsDesc.Text = Convert.ToString(dvPartcode[0]["partname"]);
                        txtPartCode.Focus();
                        txtPartCode.Attributes["onfocus"] = "this.select()";
                    }
                    else
                    {
                        MsgBox1.alert("Partcode not found.");
                        txtPartsDesc.Text = "";
                        txtPartCode.Focus();
                    }
                }

                catch (Exception ex)
                {
                    Logger.GetInstance().Fatal(ex.StackTrace, ex);
                    MsgBox1.alert(ex.Message.ToString() + "Please contact ISD.");
                }
            }
            else
            {
                if (txtPartCode.Text != String.Empty)
                {
                    txtPartsDesc.Focus();
                    if (txtPartsDesc.Text != String.Empty)
                    {
                        txtPartsDesc.Attributes["onfocus"] = "this.select()";
                    }
                }
            }
        }

        protected void ddSpecs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddSpecsColor.SelectedValue = ddSpecs.SelectedValue;
        }


        public void processLot(DataView dv, string strUID)
        {
            string srtRFIDTag = "", strRefNo = "", strLotNo = "", strItemCode = "", strQTY = "", strRemarks = "", strArea = "";

            for (int x = 0; x < dv.Count; x++)
            {
                strRefNo = dv[x]["REFNO"].ToString();
                strLotNo = dv[x]["LOTNO"].ToString();
                strItemCode = dv[x]["ITEMCODE"].ToString();
                strQTY = dv[x]["QTY"].ToString();
                strRemarks = dv[x]["REMARKS"].ToString();
                strArea = dv[x]["AREA"].ToString();
               
                autoDAL.ADD_AUTOLINE_BARCODE("", strRefNo, srtRFIDTag, strLotNo, strItemCode, strQTY, strRemarks, strUID, strArea, Session["clientIp"].ToString());
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