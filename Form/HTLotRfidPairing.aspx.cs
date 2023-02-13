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
using FGWHSEClient.DAL;
using System.Text.RegularExpressions;

namespace FGWHSEClient.Form
{
    public partial class HTLotRfidPairing : System.Web.UI.Page
    {
        DataTable dtMain = new DataTable();
        GridView grd = new GridView();
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
                    fillArea();

                    txtRfid.Attributes.Add("onkeydown", "return (event.keyCode!=13);false;");
                    txtLotQr.Attributes.Add("onkeydown", "return (event.keyCode!=13);false;");


                    if (dtMain.Columns.Count == 0)
                    {
                        dtMain.Columns.Add("RFIDTAG", typeof(string));
                        dtMain.Columns.Add("PARTCODE", typeof(string));
                        dtMain.Columns.Add("LOTNO", typeof(string));
                        dtMain.Columns.Add("REFNO", typeof(string));
                        dtMain.Columns.Add("QTY", typeof(string));
                        dtMain.Columns.Add("REMARKS", typeof(string));
                    }

                    txtRfid.Focus();

                }

                else
                {
                    dtMain = (DataTable)ViewState["dtScanned"];
                }

                ViewState["dtScanned"] = dtMain;
            }

        }
        public bool scannQRLot(DataTable dtScanned, TextBox txtScnLot, TextBox txtMsgTextBox, TextBox txtscnRFID, GridView grd)
        {
            bool isOK = true;

            txtMessage.Attributes.Add("style", "color:green");
            txtMessage.Text = "";

            string strLot = "|" + txtScnLot.Text + "|Z";

            string strItemCode = getBetween(strLot, "|Z1", "|Z");
            string strLotNo = getBetween(strLot, "|Z2", "|Z");
            string strRefNo = getBetween(strLot, "|Z5", "|Z");
            string strQty = getBetween(strLot, "|Z3", "|Z");
            string strRemarks = getBetween(strLot, "|Z4", "|Z");



            if (strItemCode.Trim() == "" || strLotNo.Trim() == "" || strRefNo.Trim() == "" || strQty.Trim() == "" || strRemarks.Trim() == "")
            {
                txtMsgTextBox.Attributes.Add("style", "color:red");
                txtMsgTextBox.Text = "Invalid QR Code";
                return false;
            }


            string strErrDiffCode = checkifCorrectItemcode(txtscnRFID.Text.Trim(), dtScanned, txtScnLot.Text.Trim());
            if (strErrDiffCode != "")
            {
                txtMsgTextBox.Attributes.Add("style", "color:red");
                txtMsgTextBox.Text = strErrDiffCode;
                return false;
            }
            //if (dtScanned.Rows.Count == 0)
            //{
            //    lblLastPartcode.Text = "";
            //}

            //if (lblLastPartcode.Text == "")
            //{
            //    lblLastPartcode.Text = strItemCode;
            //}

            if (checkIfScannedInDatatable(dtScanned, strRefNo) == true || checkIfScannedInDatabase(strRefNo) == true)
            {
                txtMessage.Attributes.Add("style", "color:red");
                txtMessage.Text = "Already Scanned";
                return false;
            }

            //if (lblLastPartcode.Text != strItemCode)
            //{
            //    txtMessage.Attributes.Add("style", "color:red");
            //    txtMessage.Text = "Different Partcode scanned";
            //    return false;
            //}


            if (
                isValidText(@"[A-Za-z0-9]", strItemCode) == false ||
                isValidText(@"[A-Za-z0-9]", strRefNo) == false ||
                isValidText(@"[A-Za-z0-9]", strQty) == false ||
                isValidText(@"[A-Za-z0-9\s#%+\(\)/\-_;,.\\]", strRemarks) == false
                )
            {
                txtMsgTextBox.Attributes.Add("style", "color:red");
                txtMsgTextBox.Text = "INVALID LOT QR!";
                return false;
            }

            //string strLotNoError = checkIfValidLotNo(strLotNo);
            //if (strLotNoError != "")
            //{
            //    txtMessage.Attributes.Add("style", "color:red");
            //    txtMessage.Text = strLotNoError;
            //    return false;
            //}

            if (txtscnRFID.Text.Trim() == "")
            {
                txtMsgTextBox.Attributes.Add("style", "color:red");
                txtMsgTextBox.Text = "NO RFID SCANNED!";
                txtScnLot.Text = "";
                txtscnRFID.Focus();

                return false;
            }

            DataRow dr;

            dr = dtScanned.NewRow();
            dr["RFIDTAG"] = txtscnRFID.Text;
            dr["PARTCODE"] = strItemCode;
            dr["LOTNO"] = strLotNo;
            dr["REFNO"] = strRefNo;
            dr["QTY"] = strQty;
            dr["REMARKS"] = strRemarks;
            dtScanned.Rows.Add(dr);
            dtScanned.AcceptChanges();

            bindGrid(dtScanned, grd);

            getCountAndQty(dtScanned, txtRFIDTotalcount, txtTotalQty);
            return isOK;
        }

        public bool checkIfScannedInDatatable(DataTable dt, string strRefno)
        {
            bool isScanned = false;

            for (int x = 0; x < dt.DefaultView.Count; x++)
            {
                if (strRefno == dt.DefaultView[x]["REFNO"].ToString())
                {
                    isScanned = true;
                    x = dt.Rows.Count;
                }

            }
            return isScanned;

        }

        public bool checkIfScannedInDatabase(string strRefno)
        {
            bool strScanned = false;
            if (pDAL.GET_LOT_LIST_REF(strRefno).Tables[0].DefaultView.Count > 0)
            {
                strScanned = true;
            }
            return strScanned;

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

        //protected void txtLotQr_TextChanged(object sender, EventArgs e)
        //{

        //    //if (txtRfid.Text == "" || txtLotQr.Text == "")
        //    //{
        //    //    MsgBox1.alert("Please complete the details before scanning.");
        //    //}
        //    //else
        //    //{
        //    if (txtRfid.Text.Trim() != "")
        //    {
        //        string strLotQRData = "|" + txtLotQr.Text.Trim().ToUpper() + "|Z";
        //        string strPartCode = getBetween(strLotQRData, "|Z1", "|Z");     
        //        string strLotNo = getBetween(strLotQRData, "|Z2", "|Z");
        //        string strRefNo = getBetween(strLotQRData, "|Z5", "|Z");
        //        string strQtyTotal = getBetween(strLotQRData, "|Z3", "|Z");
        //        string strRemarks = getBetween(strLotQRData, "|Z4", "|Z");

        //        // create data table
        //        //DataTable dtApplication = new DataTable();


        //        //dtApplication.Rows.Add(txtRfid.Text, strPartCode, strLotNo, strRefNo, strQtyTotal, strRemarks);

        //        //gvLotRfidPairing.DataSource = dtApplication;

        //        //gvLotRfidPairing.DataBind();

        //        //if (txtRfid.Text != "" && txtLotQr.Text != "")

        //        DataRow dr;

        //        dr = dtMain.NewRow();
        //        dr["RFIDTAG"] = txtRfid.Text;
        //        dr["PARTCODE"] = strPartCode;
        //        dr["LOTNO"] = strLotNo;
        //        dr["REFNO"] = strRefNo;
        //        dr["QTY"] = strQtyTotal;
        //        dr["REMARKS"] = strRemarks;
        //        dtMain.Rows.Add(dr);
        //        dtMain.AcceptChanges();


        //        bindGrid(dtMain, grd);

        //        txtLotQr.Text = "";
        //        txtRfid.Text = "";
        //    }




        //    //}


        //    //bindData(); 

        //}
        private void bindGrid(DataTable dt, GridView Grd)
        {

            gvLotRfidPairing.DataSource = null;
            gvLotRfidPairing.DataSource = dt;
            gvLotRfidPairing.DataBind();
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


        //        private void bindData()
        //        {
        //            //DataTable dtMain = new DataTable();

        //            //dtMain.Columns.Add("RFIDTAG", typeof(string));
        //            //dtMain.Columns.Add("PARTCODE", typeof(string));
        //            //dtMain.Columns.Add("LOTNO", typeof(string));
        //            //dtMain.Columns.Add("REFNO", typeof(string));
        //            //dtMain.Columns.Add("QTY", typeof(string));
        //            //dtMain.Columns.Add("REMARKS", typeof(string));

        //            foreach (DataRow dr in dtApplication.Rows) {
        //             dtMain.Rows.Add(dr.ItemArray);
        //}

        //            gvLotRfidPairing.DataSource = dtMain;

        //            gvLotRfidPairing.DataBind();
        //        }


        protected void fillArea()
        {

            //PairedDAL PairedDAL = new PairedDAL();


            //ddlArea.DataSource = PairedDAL.GET_AREA();
            //ddlArea.DataTextField = "AreaName";
            //ddlArea.DataValueField = "AreaID";
            ////ddlReason.DataValueField = "ID";
            //ddlArea.DataBind();


            DataTable dt = new DataTable();
            DataTable dtList = new DataTable();

            dt = pDAL.GET_AREA(Session["UserID"].ToString()).Tables[0];

            dtList.Columns.Add("AreaName", typeof(string));
            dtList.Columns.Add("AreaId", typeof(string));
            foreach (DataRow row in dt.Rows)
            {

                String AreaId = row[0].ToString();
                String AreaName = row[1].ToString();

                dtList.Rows.Add(AreaName, AreaId);
            }

            ddlArea.DataSource = dtList;
            ddlArea.DataTextField = "AreaName";
            ddlArea.DataValueField = "AreaId";
            ddlArea.DataBind();



            //ddlArea.Items.Insert(0, "");
            //ddlArea.SelectedItem.Text = "";



        }


        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = ViewState["dtScanned"] as DataTable;
            dt.Rows[index].Delete();
            ViewState["dtScanned"] = dt;
            bindGrid(dt, grd);

            getCountAndQty(dt, txtRFIDTotalcount, txtTotalQty);
        }



        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Write("<script>");
            Response.Write("window.location = 'HTLotRfidPairing.aspx';");
            Response.Write("</script>");
            //txtRfid.Text = "";
            //txtLotQr.Text = "";


        }





        protected void btnSave_Click(object sender, EventArgs e)
        {
            //string strRfidTag;
            //string strPartCode;
            //string strLotNo;
            //string strRefNo;
            //string strQty;
            //string strRemarks;
            //string strArea;
            //string strUserID = Session["UserID"].ToString();

            //strArea = Convert.ToString(ddlArea.SelectedValue);

            //if (strArea.Trim() == "")
            //{
            //    txtMessage.Text = "NO SELECTED DEFAULT AREA!";
            //    return;
            //}
            ////if (gvLotRfidPairing.Rows.Count < 1)
            ////{
            ////    MsgBox1.alert("Please complete the details before saving.");
            ////}

            ////else
            ////{

            //foreach (GridViewRow row in gvLotRfidPairing.Rows)
            //{


            //    strRfidTag = row.Cells[1].Text.Trim().ToString();
            //    strPartCode = row.Cells[2].Text.Trim().ToString();
            //    strLotNo = row.Cells[3].Text.Trim().ToString();
            //    strRefNo = row.Cells[4].Text.Trim().ToString();
            //    strQty = row.Cells[5].Text.Trim().ToString();
            //    strRemarks = row.Cells[6].Text.Trim().ToString();


            //    PairedDAL PairedDAL = new PairedDAL();
            //    PairedDAL.Save(strRfidTag, strPartCode, strLotNo, strRefNo, strQty, strRemarks, strArea, strUserID);


            //    //for (int i = 1; i <= gvLotRfidPairing.Columns.Count; i++)
            //    //{
            //    //    string strRFIDTAG = row.Cells[i].Text;
            //    //}
            //    //}
            //}



            string strErrMSG = saveValidation(dtMain);

            if (strErrMSG != "")
            {
                txtMessage.Attributes.Add("style", "color:red");
                txtMessage.Text = strErrMSG;
                return;
            }

            string strArea = Convert.ToString(ddlArea.SelectedValue);

            if (strArea.Trim() == "")
            {
                txtMessage.Attributes.Add("style", "color:red");
                txtMessage.Text = "NO SELECTED DEFAULT AREA!";
                return;
            }


            DataView dvRFIDmaster = new DataView();

            dvRFIDmaster = pDAL.CHECK_RFID_EXISTS_IN_MASTER(txtRfid.Text).Tables[0].DefaultView;
            if (dvRFIDmaster.Count == 0)
            {
                txtMessage.Attributes.Add("style", "color:red");
                txtMessage.Text = "RFID does not Exist in Master Data!";
                return;

            }

            saveList(dtMain, Session["UserID"].ToString(), strArea);


            txtMessage.Attributes.Add("style", "color:green");
            txtMessage.Text = "SUCCESSFULLY SAVED";
            dtMain.Clear();
            dtMain.AcceptChanges();
            bindGrid(dtMain, gvLotRfidPairing);
            getCountAndQty(dtMain, txtRFIDTotalcount, txtTotalQty);
            txtRfid.Text = "";
            txtLotQr.Text = "";
            txtLotQr.Focus();
        }


        public void saveList(DataTable dt, string strUID, string strAreaID)
        {
            DataView dv = dt.DefaultView;
            string strRefNo = "", strRFID = "", strLotNo = "", strPartCode = "", strQTY = "", strRemarks = "";
            for (int x = 0; x < dv.Count; x++)
            {



                strRFID = dv[x]["RFIDTAG"].ToString();
                strRefNo = dv[x]["REFNO"].ToString();
                strLotNo = dv[x]["LOTNO"].ToString();
                strPartCode = dv[x]["PARTCODE"].ToString();
                strQTY = dv[x]["QTY"].ToString();
                strRemarks = dv[x]["REMARKS"].ToString();
                pDAL.AddLotRfidPairing(strRefNo, strRFID, strLotNo, strPartCode, strQTY, strRemarks, strUID, strAreaID);
            }




        }
        public string saveValidation(DataTable dt)
        {
            string strMsgErr = "";
            //string strRFID = txtRfid.Text.Trim();
            if (dt.DefaultView.Count == 0)
            {
                strMsgErr = "There are no items to save!";
            }

            //if ((strRFID.Trim().Length > 30) || (strRFID.Trim().Length < 24))
            //{
            //    strMsgErr = "INVALID RFID TAG";
            //}


            return strMsgErr;
        }

        protected void btnsubmit_click(object sender, System.EventArgs e)
        {
            Response.Write("<script>alert('asdasdasd')</script>");
        }

        protected void btnLot_Click(object sender, System.EventArgs e)
        {
            //Response.Write("<script>alert('asdasdasd')</script>");
            //string txtLotQr = txtLotQr.Text;

            bool isreturn = scannQRLot(dtMain, txtLotQr, txtMessage, txtRfid, gvLotRfidPairing);
            txtLotQr.Text = "";
            if (isreturn == true)
            {
                txtRfid.Text = "";

                txtRfid.Focus();
            }
            else
            {
                txtLotQr.Focus();
            }

        }


        protected void btnRFID_Click(object sender, System.EventArgs e)
        {
            //Response.Write("<script>alert('asdasdasd')</script>");
            //string txtLotQr = txtLotQr.Text;

            string strERR = checkScannedRFID(txtRfid, txtMessage);
            if (strERR == "")
            {
                txtLotQr.Focus();
            }
            else
            {

                txtMessage.Attributes.Add("style", "color:red");
                txtMessage.Text = strERR;
                txtRfid.Text = "";
                txtRfid.Focus();
            }

        }

        public string checkScannedRFID(TextBox txtScnRFID, TextBox txtMsgTextBox)
        {
            string strError = "", strRFID = txtScnRFID.Text.Trim();
            txtMsgTextBox.Text = "";
            if (isValidText(@"[A-Za-z0-9]", strRFID) == false)
            {
                return "INVALID LOT NO!";
            }

            if ((strRFID.Trim().Length > 30) || (strRFID.Trim().Length < 24))
            {
                return "INVALID RFID TAG";
            }

            //if (dt.Rows.Count == 0)
            //{
            //    return "NO SCANNED QRCODE";
            //}

            DataView dvRFIDmaster = new DataView();

            dvRFIDmaster = pDAL.CHECK_RFID_EXISTS_IN_MASTER(txtScnRFID.Text).Tables[0].DefaultView;
            if (dvRFIDmaster.Count == 0)
            {
                txtMsgTextBox.Attributes.Add("style", "color:red");
                return "RFID does not Exist in Master Data!";
            }

            if (strError != "")
            {
                txtMsgTextBox.Attributes.Add("style", "color:red");
                txtMsgTextBox.Text = strError;
            }
            else
            {
                txtMsgTextBox.Attributes.Add("style", "color:green");
            }
            return strError;
        }

        public string checkifCorrectItemcode(string strRFID, DataTable dt, string strQRLot)
        {
            string strErr = "";


            string strLot = "|" + strQRLot + "|Z";

            string strItemCode = getBetween(strLot, "|Z1", "|Z");

            DataView dv = dt.DefaultView;

            if (strRFID != "")
            {
                for (int x = 0; x < dv.Count; x++)
                {
                    if (strRFID == dv[x]["RFIDTAG"].ToString())
                    {
                        if (strItemCode != dv[x]["PARTCODE"].ToString())
                        {
                            return "INVALID PARTCODE FOR RFID";

                        }
                    }
                }
            }

            bool isOk = GET_RFID_PARTCODE(strRFID, strItemCode);
            if (isOk == false)
            {
                return "INVALID PARTCODE FOR RFID";
            }

            return strErr;
        }


        public bool GET_RFID_PARTCODE(string strRFID, string strPartcode)
        {
            bool isOK = true;

            DataView dv = pDAL.GET_LOT_LIST_RFID_PARTCODE(strRFID).Tables[0].DefaultView;
            if (dv.Count > 0)
            {
                if (strPartcode != dv[0]["PARTCODE"].ToString())
                {
                    isOK = false;
                }
            }
            return isOK;

        }

        public void getCountAndQty(DataTable dt, TextBox txtRFIDctr, TextBox txtLotCtr)
        {
            int dtCnt = dt.DefaultView.Count;
            int intTotalQty = 0;
            txtRFIDctr.Text = "";
            txtLotCtr.Text = "";

            for (int x = 0; x < dtCnt; x++)
            {
                intTotalQty = intTotalQty + Convert.ToInt32(dt.DefaultView[x]["QTY"].ToString());
                txtRFIDctr.Text = dtCnt.ToString();
                txtLotCtr.Text = intTotalQty.ToString();
            }
        }
    }
}