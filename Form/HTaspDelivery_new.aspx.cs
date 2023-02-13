using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlClient;
using com.eppi.utils;
using System.Net;
using System.ComponentModel;
using System.Runtime.InteropServices;

using FGWHSEClient.DAL;

namespace FGWHSEClient.Form
{
    public partial class HTaspDelivery_new : System.Web.UI.Page
    {
        public ASPDAL aDAL = new ASPDAL();

        DataTable dtScannedList = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //txtbxQty.Text = gvSerialScanned.Rows.Count.ToString();
                txtbxRefNo.Focus();
                addDetails(dtScannedList);
             
            }
            else
            {
                dtScannedList = (DataTable)ViewState["dtScannedList"];
            }

            BindGrid(dtScannedList);
        }


        public void addDetails(DataTable dt)
        {
            dt.Columns.Add("SerialNo", typeof(string));
            dt.Columns.Add("ScannedSerial", typeof(string));
            ViewState["dtScannedList"] = dt;
            BindGrid(dt);

        }

        protected void BindGrid(DataTable dt)
        {
            gvSerialScanned.DataSource = dt;
            gvSerialScanned.DataBind();
        }

       

        protected void txtbxRefNo_TextChanged(object sender, EventArgs e)
        {
            Maintenance maint = new Maintenance();
            DataTable dtValue = aDAL.GET_ASP_FOR_DELIVERY_LIST(txtbxRefNo.Text.Trim()).Tables[0];
            ViewState["dtScannedList"] = dtValue;
            DataView dv = dtValue.DefaultView;
            if (dv.Count > 0)
            {
                BindGrid(dtValue);
                txtbxRefNo.Enabled = false;
                txtbxSerialNo.Enabled = true;
                txtbxSerialNo.Focus();

            }
            else
            {
                MsgBox1.alert("Invalid DS "+ txtbxRefNo.Text.Trim() + "!");
                txtbxRefNo.Text = "";
                txtbxRefNo.Focus();
            }
        }

        protected void txtbxSerialNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow[] drr = dtScannedList.Select("SERIALNO ='" + txtbxSerialNo.Text.Trim() + "'");
                if (drr.Length > 0)
                {
                    DataRow[] drr2 = dtScannedList.Select("SERIALNO ='" + txtbxSerialNo.Text.Trim() + "' AND ScannedSerial = ''");
                    if (drr2.Length > 0)
                    {
                        DataRow[] resultupdate = dtScannedList.Select("SERIALNO ='" + txtbxSerialNo.Text.Trim() + "'");
                        //update row  
                        resultupdate[0]["ScannedSerial"] = txtbxSerialNo.Text.Trim();
                        //Accept Changes  
                        dtScannedList.AcceptChanges();
                        ViewState["dtScannedList"] = dtScannedList;
                        BindGrid(dtScannedList);
                        txtbxSerialNo.Text = "";
                        
                        if(scannComplete() == true)
                        {
                            txtbxSerialNo.Enabled = false;
                            txtTransferSlip.Enabled = true;
                            txtTransferSlip.Focus();
                        }
                        else
                        {
                            txtbxSerialNo.Focus();
                        }
                        
                    }
                    else
                    {
                        MsgBox1.alert("DS already scanned (" + txtbxSerialNo.Text + ")!");
                        txtbxSerialNo.Text = "";
                        txtbxSerialNo.Focus();
                    }
                }
                else
                {

                    MsgBox1.alert("Invalid serial for this DS (" + txtbxSerialNo.Text + ")!");
                    txtbxSerialNo.Text = "";
                    txtbxSerialNo.Focus();
                }


            }
            catch(Exception ex)
            {
                MsgBox1.alert(ex.Message.ToString());
            }
            
        }

        public bool scannComplete()
        {
            bool isComplete = true;

            Label lbl;

            if(gvSerialScanned.Rows.Count == 0)
            {
                isComplete = false;
            }
            for (int x = 0; x < gvSerialScanned.Rows.Count; x++)
            {
                lbl = (Label)gvSerialScanned.Rows[x].FindControl("lblScannedSerial");
                if(lbl.Text.Trim() == "")
                {
                    isComplete = false;
                }
            }
 


            return isComplete;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("HTMenu.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strUser = Session["UserID"].ToString();

                if (scannComplete() == true)
                {
                    if (txtTransferSlip.Text.Length >= 10)
                    {

                        aDAL.UPDATE_ASP_DELIVER(txtbxRefNo.Text.Trim(), txtTransferSlip.Text.Trim(), strUser);

                        Response.Write("<script>");
                        Response.Write("alert('Successfully Saved.');");
                        Response.Write("window.location = 'HTaspDelivery_new.aspx';");
                        Response.Write("</script>");
                    }
                    else
                    {
                        MsgBox1.alert("Transfer slip must not be less than 10 charaters!");
                    }
                }
                else
                {
                    MsgBox1.alert("Serial scanning is not yet complete!");
                }
                

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void txtTransferSlip_TextChanged(object sender, EventArgs e)
        {
            DataView dv = aDAL.CHECK_ASP_DS_OR_TS("", txtTransferSlip.Text).Tables[0].DefaultView;
            string strTS = "";
            if (dv.Count > 0)
            {
                strTS = dv[0]["TS"].ToString();
            }

            if (strTS != "")
            {
                MsgBox1.alert("Transfer slip was already used!");
                txtbxRefNo.Text = "";
                txtbxRefNo.Focus();
                return;
            }
        }
    }
}
