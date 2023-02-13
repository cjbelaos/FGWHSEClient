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
    public partial class HT_PAIRING_UPDATE : System.Web.UI.Page
    {
        protected PairedDAL pDAL = new PairedDAL();
        DataTable dtScannedList = new DataTable();


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
                    //GET_MODEL(ddArea);

                    if (dtScannedList.Columns.Count == 0)
                    {
                        dtScannedList.Columns.Add("RFID", typeof(string));
                        dtScannedList.Columns.Add("ItemCode", typeof(string));
                        dtScannedList.Columns.Add("LotNo", typeof(string));
                        dtScannedList.Columns.Add("RefNo", typeof(string));
                        dtScannedList.Columns.Add("Qty", typeof(string));
                        dtScannedList.Columns.Add("Remarks", typeof(string));
                    }

                    txtLot.Focus();


                }
                else
                {
                    dtScannedList = (DataTable)ViewState["dtScanned"];
                }
                ViewState["dtScanned"] = dtScannedList;
            }
        }



        public bool scannQRLot(DataTable dtScanned, TextBox txtScnLot, TextBox txtMsgTextBox, TextBox txtscnRFID, TextBox txtRFIDcount, TextBox txtLotQuantity, GridView grd)
        {
            bool isOK = true;

            txtMsgTextBox.Attributes.Add("style", "color:green");
            txtMsgTextBox.Text = "";

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

            if (dtScanned.Rows.Count == 0)
            {
                lblLastPartcode.Text = "";
            }

            if (lblLastPartcode.Text == "")
            {
                lblLastPartcode.Text = strItemCode;
            }

            if (checkIfScannedInDatatable(dtScanned, strRefNo) == true ) //|| checkIfScannedInDatabase(strRefNo) == true
            {
                txtMsgTextBox.Attributes.Add("style", "color:red");
                txtMsgTextBox.Text = "Already Scanned";
                return false;
            }

            if(checkIfScannedInDatabase(strRefNo) == false)
            {
                txtMsgTextBox.Attributes.Add("style", "color:red");
                txtMsgTextBox.Text = "Refno is not yet paired!";
                return false;
            }


            if (lblLastPartcode.Text != strItemCode)
            {
                txtMsgTextBox.Attributes.Add("style", "color:red");
                txtMsgTextBox.Text = "INVALID PARTCODE FOR RFID";
                return false;
            }




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

            string strLotNoError = checkIfValidLotNo(strLotNo);
            if (strLotNoError != "")
            {
                txtMsgTextBox.Attributes.Add("style", "color:red");
                txtMsgTextBox.Text = strLotNoError;
                return false;
            }


            DataRow dr;

            dr = dtScanned.NewRow();
            dr["RFID"] = txtscnRFID.Text;
            dr["ItemCode"] = strItemCode;
            dr["LotNo"] = strLotNo;
            dr["RefNo"] = strRefNo;
            dr["Qty"] = strQty;
            dr["Remarks"] = strRemarks;
            dtScanned.Rows.Add(dr);
            dtScanned.AcceptChanges();

            bindGrid(dtScanned, grd);

            getCountAndQty(dtScanned, txtRFIDTotalcount, txtTotalQty);
            return isOK;
        }

        private void bindGrid(DataTable dt, GridView Grd)
        {

            Grd.DataSource = null;
            Grd.DataSource = dt;
            Grd.DataBind();
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

        public string checkIfValidLotNo(string strLotNo)
        {
            string strError = "";

            string firstThree = "";

            firstThree = strLotNo.Substring(0, 3);

            if (firstThree == "NLI")
            {
                if (strLotNo.Length < 9 || strLotNo.Length > 30)
                {
                    strError = "LOT NO LENGTH!";
                }
            }
            else
            {
                if (strLotNo.Trim() == "")
                {
                    strError = "INVALID LOT NO!";
                }
                else if (isValidText(@"[A-Za-z0-9]", strLotNo) == false)
                {
                    strError = "INVALID LOT NO!";
                }
                else if (strLotNo.Length != 15)
                {
                    strError = "LOT NO LENGTH!";
                }
            }

            return strError;

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



        public string checkScannedRFID(DataTable dt, TextBox txtScnRFID, TextBox txtMsgTextBox)
        {
            string strError = "", strRFID = txtScnRFID.Text.Trim();
            txtMsgTextBox.Text = "";

            string strLot = "|" + txtLot.Text + "|Z";

            string strItemCode = getBetween(strLot, "|Z1", "|Z");


            if (isValidText(@"[A-Za-z0-9]", strRFID) == false)
            {
                return "INVALID LOT NO!";
            }

            if ((strRFID.Trim().Length > 30) || (strRFID.Trim().Length < 24))
            {
                return "INVALID RFID TAG";
            }

            if (dt.Rows.Count == 0)
            {
                return "NO SCANNED QRCODE";
            }

            if (strError != "")
            {
                txtMsgTextBox.Attributes.Add("style", "color:red");
                txtMsgTextBox.Text = strError;
            }


            bool isOk = GET_RFID_PARTCODE(strRFID, strItemCode);
            if (isOk == false)
            {
                txtMsgTextBox.Attributes.Add("style", "color:red");
                return "INVALID PARTCODE FOR RFID";
            }

            DataView dvRFIDmaster = new DataView();

            dvRFIDmaster = pDAL.CHECK_RFID_EXISTS_IN_MASTER(txtScnRFID.Text).Tables[0].DefaultView;
            if (dvRFIDmaster.Count == 0)
            {
                txtMsgTextBox.Attributes.Add("style", "color:red");
                return "RFID does not Exist in Master Data!";
            }

            return strError;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Write("<script>");
            Response.Write("window.location = 'HT_PAIRING_UPDATE.aspx';");
            Response.Write("</script>");
        }

        protected void btnLot_Click(object sender, EventArgs e)
        {
            bool isOk = scannQRLot(dtScannedList, txtLot, txtMSG, txtScannedRFID, txtRFIDTotalcount, txtTotalQty, grdPair);


            txtLot.Text = "";
            txtLot.Focus();
        }

        protected void btnRFID_Click(object sender, EventArgs e)
        {
            checkScannedRFID(dtScannedList, txtScannedRFID, txtMSG);
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


        public void deleteGRD(GridView grd, DataTable dt, GridViewDeleteEventArgs e)
        {

            GridViewRow row = grd.Rows[e.RowIndex];
            Label lblRefNo = (Label)row.FindControl("lblRefNo");

            doDeleteGrd(row, dt, grd, lblRefNo.Text);
            getCountAndQty(dt, txtRFIDTotalcount, txtTotalQty);

        }


        public void doDeleteGrd(GridViewRow row, DataTable dt, GridView grd, string strREFNO)
        {

            DataRow[] drr = dt.Select("REFNO='" + strREFNO + "'");

            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
            }


            dt.AcceptChanges();
            bindGrid(dt, grd);
        }

        protected void grdPair_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            deleteGRD(grdPair, dtScannedList, e);
        }



        //public void GET_MODEL(DropDownList dd)
        //{

        //    DataTable dt = new DataTable();
        //    DataTable dtList = new DataTable();

        //    dt = pDAL.GET_AREA(Session["UserID"].ToString()).Tables[0];

        //    dtList.Columns.Add("AreaName", typeof(string));
        //    dtList.Columns.Add("AreaId", typeof(string));

        //    foreach (DataRow row in dt.Rows)
        //    {

        //        String AreaId = row[0].ToString();
        //        String AreaName = row[1].ToString();

        //        dtList.Rows.Add(AreaName, AreaId);
        //    }

        //    dd.DataSource = dtList;
        //    dd.DataTextField = "AreaName";
        //    dd.DataValueField = "AreaId";
        //    dd.DataBind();
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {

            string strErrMSG = saveValidation(dtScannedList);

            if (strErrMSG != "")
            {
                txtMSG.Attributes.Add("style", "color:red");
                txtMSG.Text = strErrMSG;
                return;
            }

            string strArea = Convert.ToString(ddArea.SelectedValue);

            //if (strArea.Trim() == "")
            //{
            //    txtMSG.Attributes.Add("style", "color:red");
            //    txtMSG.Text = "NO SELECTED DEFAULT AREA!";
            //    return;
            //}

            string strLot = "|" + txtLot.Text + "|Z";

            string strItemCode = getBetween(strLot, "|Z1", "|Z");

            bool isOk = GET_RFID_PARTCODE(txtScannedRFID.Text.Trim(), strItemCode);
            if (isOk == false)
            {
                txtMSG.Attributes.Add("style", "color:red");
                txtMSG.Text = "INVALID PARTCODE FOR RFID";
                return;
            }


            DataView dvRFIDmaster = new DataView();

            dvRFIDmaster = pDAL.CHECK_RFID_EXISTS_IN_MASTER(txtScannedRFID.Text).Tables[0].DefaultView;
            if (dvRFIDmaster.Count == 0)
            {
                txtMSG.Attributes.Add("style", "color:red");
                txtMSG.Text = "RFID does not Exist in Master Data!";
                return;

            }

            saveList(dtScannedList, txtScannedRFID, Session["UserID"].ToString(), strArea);


            txtMSG.Attributes.Add("style", "color:green");
            txtMSG.Text = "SUCCESSFULLY SAVED";
            dtScannedList.Clear();
            dtScannedList.AcceptChanges();
            bindGrid(dtScannedList, grdPair);
            getCountAndQty(dtScannedList, txtRFIDTotalcount, txtTotalQty);
            txtScannedRFID.Text = "";
            txtLot.Text = "";
            txtLot.Focus();

        }

        public void saveList(DataTable dt, TextBox txtRFIDlabel, string strUID, string strAreaID)
        {
            DataView dv = dt.DefaultView;
            string strRefNo = "", strLotNo = "", strPartCode = "", strQTY = "", strRemarks = "";
            for (int x = 0; x < dv.Count; x++)
            {
                strRefNo = dv[x]["RefNo"].ToString();
                strLotNo = dv[x]["LotNo"].ToString();
                strPartCode = dv[x]["ItemCode"].ToString();
                strQTY = dv[x]["Qty"].ToString();
                strRemarks = dv[x]["Remarks"].ToString();
                pDAL.UPDATE_LotRfidPairing(strRefNo, txtRFIDlabel.Text, strUID);
            }




        }


        public string saveValidation(DataTable dt)
        {
            string strMsgErr = "";
            string strRFID = txtScannedRFID.Text.Trim();
            if (dt.DefaultView.Count == 0)
            {
                strMsgErr = "There are no items to save!";
            }

            if ((strRFID.Trim().Length > 30) || (strRFID.Trim().Length < 24))
            {
                strMsgErr = "INVALID RFID TAG";
            }


            return strMsgErr;
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
    }
}