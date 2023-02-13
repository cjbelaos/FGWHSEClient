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
    public partial class RFIDLotMatch : System.Web.UI.Page
    {
        public string strPageSubsystem = "";
        public string strAccessLevel = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Write("<script>");
                Response.Write("alert('Session expired! Please log in again.');");
                Response.Write("window.location = '../RFIDLogin.aspx';");
                Response.Write("</script>");
            }
            else
            {
                strPageSubsystem = "FGWHSE_037";
                if (!checkAuthority(strPageSubsystem))
                {
                    Response.Write("<script>");
                    Response.Write("alert('You are not authorized to access the page.');");
                    Response.Write("window.location = '../RFIDLogin.aspx';");
                    Response.Write("</script>");
                }

                if (!IsPostBack)
                {
                    txtRFID.Focus();
                    btnSave.Enabled = false;
                    //btnSave.BackColor = System.Drawing.Color.Gray;
                }

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
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);

                isValid = false;
                return isValid;
            }
        }

        protected void txtPartCode_TextChanged(object sender, EventArgs e)
        {

        }


        protected void txtRFID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                PartsLocationCheckDAL maint = new PartsLocationCheckDAL();
                DataView dvRFID = new DataView();

                dvRFID = maint.GetRFIDTag(txtRFID.Text.Trim());

                hiddenOldPartcode.Value="";
                hiddenOldRefNo.Value = "";

                if (dvRFID.Table.Rows.Count == 0)
                {
                    lblMessage.Text = "RFID Tag does not exist in RFID Tag Master. Please check RFID Tag Master";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Font.Size = 12;
                    lblMessage.Font.Bold = false;
                    txtRFID.Text = "";
                    txtRFID.Focus();
                }
                else
                {
                    DataView dvLot= new DataView();
                    dvLot = maint.GetLotListRFID(txtRFID.Text.Trim());

                    if (dvLot.Table.Rows.Count > 0)
                    {
                        hiddenOldPartcode.Value = dvLot.Table.Rows[0]["PartCode"].ToString();
                        hiddenOldRefNo.Value = dvLot.Table.Rows[0]["RefNo"].ToString();
                    }

                    lblMessage.Text = "";
                    txtRFID.ReadOnly = true;
                    txtRefNo.Focus();

                }


            }
            catch (Exception ex)
            {
                msgBox.alert(ex.Message);
            }
        }

        protected void txtRefNo_TextChanged(object sender, EventArgs e)
        {
            try
            {

                hiddenLotID.Value = "";
                hiddenOldRFID.Value = "";
                PartsLocationCheckDAL maint = new PartsLocationCheckDAL();
                DataView dvGetLotList = new DataView();

                //check If Part Code and Lot Ref No match in the database table
                dvGetLotList = maint.GetLotListRef(txtRefNo.Text.Trim());

                if (dvGetLotList.Table.Rows.Count == 0)
                {
                    //if not exist
                    lblMessage.Text = "ERROR: Lot Ref No does not exist. Please check again if correct or wait a few minutes and try again.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Font.Size = 10;
                    lblMessage.Font.Bold = false;
                    txtRefNo.Text = "";
                    txtRefNo.Focus();
                }
                else
                {

                    lblMessage.Text = "";
                    string PartCode = dvGetLotList.Table.Rows[0]["PartCode"].ToString().ToUpper();


                    if ((PartCode.ToUpper() == hiddenOldPartcode.Value.Trim().ToUpper()) || (hiddenOldPartcode.Value.Trim().ToUpper() == ""))
                    {
                        //get refno details from lot list
                        hiddenLotID.Value = dvGetLotList.Table.Rows[0]["LotID"].ToString().ToUpper();
                        hiddenOldRFID.Value = dvGetLotList.Table.Rows[0]["RFIDTag"].ToString().ToUpper();

                        if (hiddenOldRFID.Value.ToUpper() == txtRFID.Text.Trim().ToUpper())
                        {
                            //if same rfid
                            lblMessage.Text = "ERROR: RefNo-" + txtRefNo.Text.Trim().ToUpper() + " is already regsitered to this RFID Tag";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            lblMessage.Font.Size = 10;
                            lblMessage.Font.Bold = false;
                            txtRefNo.ReadOnly = true;

                        }
                        else
                        {
                            btnSave.BackColor = System.Drawing.Color.LightGreen;
                            btnSave.Enabled = true;
                        }

                    }
                    else
                    {
                        //if not same partcode
                        lblMessage.Text = "ERROR: PartCode-" + PartCode + " is different from old RefNo Partcode-" + hiddenOldPartcode.Value;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Font.Size = 10;
                        lblMessage.Font.Bold = false;
                        txtRefNo.Text = "";
                        txtRefNo.Focus();
                    
                    }


                   
                }


            }
            catch (Exception ex)
            {
                msgBox.alert(ex.Message);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("RFIDLotMatch.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string createdby = Session["UserName"].ToString();
                PartsLocationCheckDAL maint = new PartsLocationCheckDAL();

                if (hiddenOldRefNo.Value.Trim() != "")
                {
                    //update lot list rfid flag by refno if existing
                    int updateRfidFlagVyRefNo = maint.UpdateLotListRFIDFlagByRefNo(hiddenOldRefNo.Value.Trim().ToUpper());
                }

                //update lot list rfid flag if existing
                int updateRfidFlag = maint.UpdateLotListRFIDFlag(txtRFID.Text.Trim().ToUpper());

                //update lot list rfid tag
                int updateLot = maint.UpdateLotList(txtRFID.Text.Trim().ToUpper(), txtRefNo.Text.Trim().ToUpper(), createdby);

                //add logs in lotupdate
                int addLogs = maint.AddLotUpdate(hiddenLotID.Value.ToUpper(), "RFIDTAG", hiddenOldRFID.Value.ToUpper(), txtRFID.Text.Trim().ToUpper(), createdby);

                //message
                lblMessage.Text = "RFID Tag " + txtRFID.Text.Trim().ToUpper() + " successfully paired to Lot RefNo " + txtRefNo.Text.Trim().ToUpper();
                lblMessage.ForeColor = System.Drawing.Color.Blue;
                lblMessage.Font.Size = 10;
                lblMessage.Font.Bold = false;


                //reset controls
                txtRFID.Text = "";
                txtRefNo.Text = "";
                hiddenLotID.Value = "";
                hiddenOldRFID.Value = "";

                txtRFID.ReadOnly = false;
                txtRFID.Focus();

                txtRefNo.ReadOnly = false;

                btnSave.Enabled = false;
                btnSave.BackColor = System.Drawing.Color.Silver;

            }
            catch (Exception ex)
            {
                msgBox.alert(ex.Message);
            }
        }

      
    }
}
