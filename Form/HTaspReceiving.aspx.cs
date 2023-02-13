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
    
    public partial class HTaspReceiving : System.Web.UI.Page
    {
        ASPDAL aDAL = new ASPDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //txtbxQty.Text = gvSerialScanned.Rows.Count.ToString();
                txtbxRefNo.Focus();
                addDetails();
            }

        }


        public void addDetails()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("SerialNo", typeof(string));
            dt.Columns.Add("Partcode", typeof(string));
            ViewState["gvSerialScanned"] = dt;
            BindGrid();

        }

        protected void BindGrid()
        {
            gvSerialScanned.DataSource = ViewState["gvSerialScanned"] as DataTable;
            gvSerialScanned.DataBind();
        }

        protected void RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = ViewState["gvSerialScanned"] as DataTable;
            dt.Rows[index].Delete();
            ViewState["gvSerialScanned"] = dt;
            BindGrid();
            txtbxQty.Text = gvSerialScanned.Rows.Count.ToString();

        }

        protected void txtbxRefNo_TextChanged(object sender, EventArgs e)
        {
            DataView dv = aDAL.CHECK_ASP_DS_OR_TS(txtbxRefNo.Text, "").Tables[0].DefaultView;
            string strDS = "";
            if(dv.Count > 0)
            {
                strDS = dv[0]["DS"].ToString();
            }

            if(strDS != "")
            {
                MsgBox1.alert("DS was already used!");
                txtbxRefNo.Text = "";
                txtbxRefNo.Focus();
                return;
            }
            txtbxRefNo.Enabled = false;
            txtbxSerialNo.Focus();
        }

        protected void txtbxSerialNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Maintenance maint = new Maintenance();
                DataSet ds = new DataSet();
                ds = maint.getPCodeDetails(txtbxSerialNo.Text);

                
                DataTable dt = (DataTable)ViewState["gvSerialScanned"];
                if (ds.Tables[0].DefaultView.Count > 0)
                {
                    DataRow[] drr = dt.Select("SERIALNO ='" + txtbxSerialNo.Text.Trim() + "'");

                    if (drr.Length == 0)
                    {
                        dt.Rows.Add(txtbxSerialNo.Text, ds.Tables[0].Rows[0]["Partcode"].ToString());
                        ViewState["gvSerialScanned"] = dt;
                        gvSerialScanned.DataSource = ViewState["gvSerialScanned"];
                        gvSerialScanned.DataBind();
                        gvSerialScanned.SelectedIndex = -1;
                        BindGrid();
                        txtbxQty.Text = gvSerialScanned.Rows.Count.ToString();
                        txtbxSerialNo.Text = "";
                        txtbxSerialNo.Focus();
                    }
                    else
                    {
                        txtbxSerialNo.Text = "";
                        txtbxSerialNo.Focus();
                        MsgBox1.alert("Serial "+ txtbxSerialNo.Text + " already scanned!");
                    }
                }
                else
                {
                    MsgBox1.alert("Serial Data " + txtbxSerialNo.Text + " Not Found!");
                    txtbxSerialNo.Text = "";
                    txtbxSerialNo.Focus();
                }
            }
            catch(Exception ex)
            {
                MsgBox1.alert(ex.Message.ToString());
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("HTMenu.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvSerialScanned.Rows.Count > 0)
                {
                    string strInsert = "";
                    Maintenance maint = new Maintenance();
                    foreach (GridViewRow Row in gvSerialScanned.Rows)
                    {

                        int n = 1;
                        string DSRefNo = txtbxRefNo.Text;
                        string SerialNo = Row.Cells[1].Text;
                        string Partcode = Row.Cells[2].Text;
                        string qty = n.ToString();
                        string strUser = Session["UserID"].ToString();


                        strInsert = maint.SaveReceivePD(DSRefNo, SerialNo, Partcode, qty, strUser);

                    }

                    txtbxQty.Text = "";
                    txtbxRefNo.Text = "";
                    txtbxSerialNo.Text = "";
                    txtbxRefNo.Focus();

                    Response.Write("<script>");
                    Response.Write("alert('Successfully Saved.');");
                    Response.Write("window.location = 'HTaspReceiving.aspx';");
                    Response.Write("</script>");
                }
                else
                {
                    MsgBox1.alert("No serial scanned!");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }


}

