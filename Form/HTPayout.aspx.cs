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


using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Collections.Specialized;
using System.Net;
using System.Collections.Generic;

namespace FGWHSEClient.Form
{
    public partial class HTPayout : System.Web.UI.Page
    {

        public DataTable dtnew1;
        DataTable dt;

        public string strPageSubsystem = "";
        public string strAccessLevel = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Write("<script>");
                Response.Write("alert('Session expired! Please log in again.');");
                Response.Write("window.location = '../HTLogin.aspx';");
                Response.Write("</script>");
            }
            else
            {

                strPageSubsystem = "FGWHSE_055";
                if (!checkAuthority(strPageSubsystem))
                {
                    Response.Write("<script>");
                    Response.Write("alert('You are not authorized to access the page.');");
                    Response.Write("window.location = '../HTLogin.aspx';");
                    Response.Write("</script>");
                }


                if (!IsPostBack)
                {
                  

                    initalize();
                }
                else
                {
                    dtnew1 = (DataTable)ViewState["datatableAttachment"];
                }


            }



            txtLotNo.Focus();

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
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);

                isValid = false;
                return isValid;
            }
        }

        protected void txtPartCode_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            ////Response.Redirect("PartsLocationCheckv2.aspx");
            string errormessage = "";


            int res_success = 0;
            int res_failed = 0;
            string res_message = "";


            //int lotId = Convert.ToInt32(hiddenLotID.Value);
            //string refno = txtLotNo.Text.Trim();
            //string LN = txtLot.Text.Trim();
            //string QT = txtQty.Text.Trim();
            //string PartCode = txtPartCode.Text.Trim();
            //string markings = "";
            string scannedby = Session["UserName"].ToString(); // "ewhsuser";


            //saving in EWHS DB
            if (res_failed == 0)
            {

                HOLMESDAL maint = new HOLMESDAL();
                string result = maint.UpdateDelivery(dtnew1, scannedby);


                if (result == "")
                {
                    //errormessage = txtLotNo.Text.Trim() + " successfully saved!";
                    //lblMessage.Text = txtLotNo.Text.Trim() + " successfully saved!";
                    //lblMessage.ForeColor = System.Drawing.Color.Blue;
                    //lblMessage.Font.Size = 10;
                    //lblMessage.Font.Bold = false;

                    txtLotNo.Text = "";
                    txtLot.Text = "";
                    txtPartCode.Text = "";
                    txtPartName.Text = "";
                    txtQty.Text = "";
                    txtLotNo.Focus();
                    hiddenLotID.Value = "";

                    MsgBox1.alert("[" + txtTotalCount.Text.Trim() + "] successfully saved!");


                    txtTotalCount.Text = "";
                    txtTotalQty.Text = "";
                    dtnew1.Clear();

                    grdPo.DataSource = dtnew1;
                    grdPo.DataBind();
                    grdPo.UseAccessibleHeader = true;
                    grdPo.Visible = true;

                    ViewState["DataTable"] = dtnew1;

                    btnSave.Enabled = false;
                    return;


                }
                else
                {
                    //if not exist
                    //errormessage = "ERROR: " + result;
                    //lblMessage.Text = "ERROR: " + result;
                    //lblMessage.ForeColor = System.Drawing.Color.Red;
                    //lblMessage.Font.Size = 10;
                    //lblMessage.Font.Bold = false;

                    MsgBox1.alert("ERROR: Try again. " + result);

                    txtLotNo.Focus();
                    btnSave.Enabled = false;
                    return;

                }

               


               
            }


        }



        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtLotNo.Text = "";
            lblMessage.Text = "";
            Response.Redirect("HTPayout.aspx");
        }


        public void initalize()
        {

            dtnew1 = new DataTable();
            dtnew1.Columns.Add("LOT", typeof(string));
            dtnew1.Columns.Add("ReferenceNo", typeof(string));
            dtnew1.Columns.Add("QTY", typeof(string));
            dtnew1.Columns.Add("LotId", typeof(int));

            ViewState["datatableAttachment"] = dtnew1;

        }

        protected void txtLotNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtLotNo.Text.Trim() == "")
                {
                    txtLotNo.Text = "";
                    txtLotNo.Focus();
                }
                else
                {

                    string errormessage = "";
                    lblMessage.Text = "";
                    string createdby = Session["UserName"].ToString();
                    PartsLocationCheckDAL maint = new PartsLocationCheckDAL();
                    DataView dvGetLotList = new DataView();

                    //check If Part Code and Lot Ref No match in the database table
                    //dvGetLotList = maint.GetLotList(txtLotNo.Text.Trim(), lblPartCode.Text.Trim());
                    dvGetLotList = maint.GetLotListRefPPD(txtLotNo.Text.Trim());


                    if (dvGetLotList.Table.Rows.Count == 0)
                    {
                        //if not exist
                        //errormessage = "ERROR: Lot Ref No does not exist. Please check again if correct or wait a few minutes and try again.";
                        //lblMessage.Text = "ERROR: Lot Ref No does not exist. Please check again if correct or wait a few minutes and try again.";
                        //lblMessage.ForeColor = System.Drawing.Color.Red;
                        //lblMessage.Font.Size = 10;
                        //lblMessage.Font.Bold = false;


                        MsgBox1.alert("ERROR: Lot Ref No does not exist. Please check again if correct or wait a few minutes and try again.");

                        txtLotNo.Text = "";
                        txtLotNo.Focus();
                        btnSave.Enabled = false;
                        return;
                    }
                    else
                    {

                        string refno = dvGetLotList.Table.Rows[0]["RefNo"].ToString().Trim();
                        string CurrPartCode = dvGetLotList.Table.Rows[0]["PartCode"].ToString().Trim();
                        string CurrPartName = dvGetLotList.Table.Rows[0]["MaterialName"].ToString().Trim();
                        string CurrLotNo = dvGetLotList.Table.Rows[0]["LotNo"].ToString().Trim();
                        string Qty = (Convert.ToInt32(Convert.ToDouble(dvGetLotList.Table.Rows[0]["Qty"].ToString().Trim()))).ToString();
                        string LotId = dvGetLotList.Table.Rows[0]["LotID"].ToString().Trim();
                        string rfidflag = dvGetLotList.Table.Rows[0]["RFIDFinishFlag"].ToString().Trim();
                        string partsreturnflag = dvGetLotList.Table.Rows[0]["PartsReturnFlag"].ToString().Trim();
                        string HoldStatus = dvGetLotList.Table.Rows[0]["HoldStatus"].ToString().Trim();


                        if (rfidflag == "True")
                        {
                            //if not exist
                            //errormessage = "ERROR: Lot Ref No [ " + refno  + " ] is  already delivered. ";
                            //lblMessage.Text = "ERROR: Lot Ref No [ " + refno + " ] is  already delivered. ";
                            //lblMessage.ForeColor = System.Drawing.Color.Red;
                            //lblMessage.Font.Size = 10;
                            //lblMessage.Font.Bold = false;

                            MsgBox1.alert("ERROR: Lot Ref No [ " + refno + " ] is  already delivered. ");


                            txtLotNo.Text = "";
                            txtLotNo.Focus();
                            return;

                        }
                        else if ((rfidflag == "True") && (partsreturnflag == "True"))
                        {
                            MsgBox1.alert("ERROR: Lot Ref No [ " + refno + " ] is transacted as parts return. ");


                            txtLotNo.Text = "";
                            txtLotNo.Focus();
                            return;

                        }
                        else if (HoldStatus == "1")
                        {
                            MsgBox1.alert("ERROR: Lot Ref No [ " + refno + " ] is hold in ELOG.");


                            txtLotNo.Text = "";
                            txtLotNo.Focus();
                            return;
                        }
                        else
                        {

                            if ((CurrPartCode.Trim() != txtPartCode.Text.Trim()) && (txtPartCode.Text.Trim() != ""))
                            {
                                MsgBox1.alert("ERROR: Part Code is different from the first item.");


                                txtLotNo.Text = "";
                                txtLotNo.Focus();
                                return;

                            }
                            else
                            {
                                txtPartCode.Text = CurrPartCode;
                                //txtQty.Text = Qty;
                                //txtLot.Text = CurrLotNo;
                                txtPartName.Text = CurrPartName;
                                //hiddenLotID.Value = LotId;


                                foreach (GridViewRow row in grdPo.Rows)
                                {
                                    string RefNo = Convert.ToString(row.Cells[2].Text).Trim();
                                    string LotNo = Convert.ToString(row.Cells[1].Text).Trim();


                                    if ((LotNo == CurrLotNo) && (RefNo == refno))
                                    {
                                        MsgBox1.alert("Lot No. Already Scanned!");
                                        txtLotNo.Text = "";
                                        return;
                                    }

                                }


                                btnSave.Enabled = true;


                                dtnew1.Rows.Add(CurrLotNo, refno, Qty, LotId);
                                ViewState["datatableAttachment"] = dtnew1;


                                grdPo.DataSource = dtnew1;
                                grdPo.DataBind();
                                grdPo.UseAccessibleHeader = true;
                                grdPo.Visible = true;

                                ViewState["DataTable"] = dtnew1;

                                computeGrid();
                                txtLotNo.Text = "";

                            }





                        }





                    }



                }


            }
            catch (Exception ex)
            {
                //string errormessage = "";

                //errormessage = "ERROR: " + ex.Message;
                //lblMessage.Text = "ERROR: " + ex.Message;
                //lblMessage.ForeColor = System.Drawing.Color.Red;
                //lblMessage.Font.Size = 10;
                //lblMessage.Font.Bold = false;

                MsgBox1.alert("ERROR: " + ex.Message);


                txtLotNo.Text = "";
                txtLotNo.Focus();
                btnSave.Enabled = false;
                return;
            }
        }

        protected void grdPo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)grdPo.Rows[e.RowIndex];
            DataView dv = new DataView();
            string BN = row.Cells[2].Text.ToString();

            DataTable dtt;
            dtt = (DataTable)ViewState["DataTable"];


            dtnew1.Rows.RemoveAt(e.RowIndex);
            dtt.Rows.RemoveAt(e.RowIndex);

            grdPo.DataSource = dtt;
            grdPo.DataBind();


            computeGrid();

        }

        protected void computeGrid()
        {

            int totBox;
            string checkBox = grdPo.Rows.Count.ToString();
            totBox = Convert.ToInt32(checkBox);
            txtTotalCount.Text = totBox.ToString();


            int sum = 0;

            for (int i = 0; i < grdPo.Rows.Count; ++i)
            {
                sum += Convert.ToInt32(grdPo.Rows[i].Cells[3].Text.ToString());
            }

            txtTotalQty.Text = sum.ToString();

            if (totBox == 0)
            {
                btnSave.Enabled = false;
            }


        }
    }
}