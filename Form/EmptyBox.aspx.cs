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
    public partial class EmptyBox : System.Web.UI.Page
    {
        EmptyPcaseDAL epDAL = new EmptyPcaseDAL();
        DataTable dtLegend = new DataTable();
        int rowIndx;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                pnlRefresh.Attributes.Add("style", "display:none");
                txtDriverID.Focus();
                dtLegend = DataTableToViewState("dtLegend", epDAL.GET_EMPTY_PCASE_LOADING_STATUS_LIST().Tables[0]);
                btnStart.Text = "START";
                txtButtonValue.Text = btnStart.Text;
                btnStart.BackColor = Color.FromArgb(0, 255, 0);
                //txtDriverName.Enabled = false;
                FillDropDown(ddLoadingDock, epDAL.GET_EMPTY_PCASE_ANTENNA(), "LOADINGDOCK", "LOADINGDOCKID");
                getDetails();

            }
            else
            {
                dtLegend = (DataTable)ViewState["dtLegend"];
            }

            getLegend();

            

        }

        public void getReceiverDetails()
        {
            DataView dv = epDAL.GET_EMPTY_PCASE_RECEIVER_DETAILS(txtDriverID.Text).Tables[0].DefaultView;
            if (dv.Count > 0)
            {
                lblAbbreviation.Text = dv[0]["SUPPLIERABBREVIATION"].ToString();
                txtSupplier.Text = dv[0]["SUPPLIERNAME"].ToString();
                lblSupplierID.Text = dv[0]["SUPPLIERID"].ToString();
                txtDriverID.Enabled = false;
                txtTRACKINGNO.Focus();
                txtDriverName.Text = dv[0]["USERNAME"].ToString();
            }
            else
            {
                txtDriverName.Text = "";
                lblAbbreviation.Text = "";
                txtSupplier.Text = "";
                txtDriverID.Enabled = true;
                MsgBox1.alert("Invalid ID ("+ txtDriverID.Text + ")!");
                txtDriverID.Text = "";
                txtDriverID.Focus();


            }
        }
        public void getLegend()
        {
            DataView dv = dtLegend.DefaultView;
            string strColor = "", strStatus = "";
            for( int x = 0; x < dv.Count; x ++)
            {
                strColor = dv[x]["COLORDISPLAY"].ToString();
                strStatus = dv[x]["EMPTYPCASESTATUS"].ToString();

                HtmlTableCell tdColor = new HtmlTableCell();
                trLegend.Cells.Add(tdColor);


                Panel pnl = new Panel();
                pnl.Attributes.Add("style", "background-color:" + strColor + ";width:20px;height:8px");
                tdColor.Controls.Add(pnl);

                HtmlTableCell tdStatus = new HtmlTableCell();
                trLegend.Cells.Add(tdStatus);

                Label lblStat = new Label();
                lblStat.Text = strStatus;
                tdStatus.Controls.Add(lblStat);


                HtmlTableCell tdSpace = new HtmlTableCell();
                trLegend.Cells.Add(tdSpace);

                if (x + 1 != dv.Count)
                {
                    Label lblSpace = new Label();
                    lblSpace.Width = Unit.Pixel(10);
                    lblSpace.Text = "";
                    tdSpace.Controls.Add(lblSpace);
                }
                
            }

            
        }

        public DataTable DataTableToViewState(String viewstate, DataTable dtSQL)
        {
            ViewState[viewstate] = dtSQL;
            return dtSQL;
        }

        public void getDetails()
        {
            DataSet ds = epDAL.GET_EMPTY_PCASE_LOADING_DETAILS(ddLoadingDock.SelectedValue.ToString());
            DataTable dt = ds.Tables[0];
            DataView dv = dt.DefaultView;

            if (dv.Count > 0)
            {
                txtCONTROLNO.Text = dv[0]["HCONTROLNO"].ToString();
                txtDriverID.Text = dv[0]["HCREATEDBY"].ToString();
                txtSupplier.Text = dv[0]["SUPPLIERNAME"].ToString();
                txtTRACKINGNO.Text = dv[0]["HTRACKINGNO"].ToString();
                txtPlateNo.Text = dv[0]["HPLATENO"].ToString();
                lblSupplierID.Text = dv[0]["HSUPPLIERID"].ToString();
                btnStart.Text = "STOP";
                pnlRefresh.Attributes.Add("style", "display:compact");
                btnStart.BackColor = Color.FromArgb(209, 22, 22);
                txtDriverID.Enabled = false;
                txtPlateNo.Enabled = false;
                txtTRACKINGNO.Enabled = false;

                getReceiverDetails();

                gvLoad.DataSource = ds.Tables[1];
                gvLoad.DataBind();
            }
            else if(txtCONTROLNO.Text != "[AUTOMATIC]")
            {
                getDetailsByCONTROLNO();
            }
            else
            {
                btnStart.Text = "START";
                btnStart.BackColor = Color.FromArgb(0, 255, 0);

                pnlRefresh.Attributes.Add("style", "display:none");

                txtDriverName.Text = "";
                lblAbbreviation.Text = "";
                lblSupplierID.Text = "";
                txtCONTROLNO.Text = "[AUTOMATIC]";
                txtDriverID.Text = "";
                txtSupplier.Text = "";
                txtPlateNo.Text = "";
                txtTRACKINGNO.Text = "";
                lblSupplierID.Text = "";
                txtDriverID.Enabled = true;
                txtTRACKINGNO.Enabled = true;
                txtPlateNo.Enabled = true;
                txtDriverID.Focus();

                gvLoad.DataSource = ds.Tables[1];
                gvLoad.DataBind();
            }

            lblLastUpdateDate.Text = DateTime.Now.ToString();
            txtButtonValue.Text = btnStart.Text;
           
        }

        public void getDetailsByCONTROLNO()
        {
            DataSet ds = epDAL.GET_EMPTY_PCASE_LOADING_DETAILS_BY_CONTROLNO(txtCONTROLNO.Text.Trim());
            DataTable dt = ds.Tables[0];
            DataView dv = dt.DefaultView;

            if (dv.Count > 0)
            {
                txtCONTROLNO.Text = dv[0]["HCONTROLNO"].ToString();
                txtDriverID.Text = dv[0]["HPLATENO"].ToString();
                txtSupplier.Text = dv[0]["SUPPLIERNAME"].ToString();
                txtPlateNo.Text = dv[0]["HPLATENO"].ToString();
                lblSupplierID.Text = dv[0]["HSUPPLIERID"].ToString();
                txtTRACKINGNO.Text = dv[0]["HTRACKINGNO"].ToString();
            }
            else
            {
                txtCONTROLNO.Text = "[AUTOMATIC]";
                txtDriverID.Text = "";
                txtSupplier.Text = "";
                txtPlateNo.Text = "";
                lblSupplierID.Text = "";
                txtTRACKINGNO.Text = "";

            }

            btnStart.Text = "START";
            pnlRefresh.Attributes.Add("style", "display:none");
            btnStart.BackColor = Color.FromArgb(0, 255, 0);
            txtButtonValue.Text = btnStart.Text;
            gvLoad.DataSource = ds.Tables[1];
            gvLoad.DataBind();
        }

        private void FillDropDown(DropDownList dd, DataSet ds, string textField, string strValueField)
        {
            dd.DataSource = ds;
            dd.DataTextField = textField;
            dd.DataValueField = strValueField;
            dd.DataBind();
            dd.SelectedIndex = 0;
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            if (ddLoadingDock.SelectedValue.ToString() == "")
            {
                MsgBox1.alert("Please select "+ lblAntennaCaption.Text + "!");
                return;
            }

            if (txtDriverID.Text.Trim() == "" || txtPlateNo.Text.Trim() == "" || txtTRACKINGNO.Text.Trim() == "")
            {
                MsgBox1.alert("Please complete all required fields!");
                return;
            }

            string strSENDSTATUS = btnStart.Text;
            DataSet ds = epDAL.EMPTY_PCASE_ADD_START_READING(txtCONTROLNO.Text.Trim(), txtTRACKINGNO.Text.Trim(), lblSupplierID.Text.Trim(), ddLoadingDock.SelectedValue.ToString().Trim(), txtPlateNo.Text.Trim(), strSENDSTATUS.Trim(), txtDriverID.Text, lblAbbreviation.Text.ToUpper());

            DataView dv = ds.Tables[0].DefaultView;
            string strStart = "0";
            if (dv.Count > 0)
            {
                strStart = dv[0]["ISRUNNING"].ToString();
            }

            if (strStart == "1")
            {
                MsgBox1.alert(lblAntennaCaption.Text + " " + ddLoadingDock.SelectedItem.Text + " is already being used!Please refresh the page.");
                return;
            }

            if (strSENDSTATUS == "START")
            {
                btnStart.Text = "STOP";
                btnStart.BackColor = Color.FromArgb(209, 22, 22);
                txtPlateNo.Enabled = false;
                txtTRACKINGNO.Enabled = false;
                txtDriverID.Enabled = false;
                getDetails();
            }
            else
            {
                btnStart.Text = "START";
                btnStart.BackColor = Color.FromArgb(0, 255, 0);
                getDetailsByCONTROLNO();
            }

            txtButtonValue.Text = btnStart.Text;

        }

        protected void ddLoadingDock_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCONTROLNO.Text = "[AUTOMATIC]";
            getDetails();
        }

        protected void txtDriverID_TextChanged(object sender, EventArgs e)
        {
            getReceiverDetails();
        }

        protected void gvLoad_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strTextColor = "";
                Label lblColor = (Label)e.Row.FindControl("lblCOLORDISPLAY");
                Label lblEMPTYPCASESTATUS = (Label)e.Row.FindControl("lblEMPTYPCASESTATUS");
                
                strTextColor = lblColor.Text;
                var status = (Label)e.Row.FindControl("lblStatus");
                lblEMPTYPCASESTATUS.ForeColor = Color.FromName(strTextColor);


            }
        }

        protected void lnk_Click(object sender, EventArgs e)
        {

            var rowTrigger = (Control)sender;
            GridViewRow rowUpdate = (GridViewRow)rowTrigger.NamingContainer;
            rowIndx = rowUpdate.RowIndex;
            //LinkButton lnk = (LinkButton)gvLoad.Rows[rowIndx].FindControl("lnkCONTROLNO");
            Label lblCONTROLNO = (Label)gvLoad.Rows[rowIndx].FindControl("lblCONTROLNO");
            openLink(lblCONTROLNO.Text);
        }

        public void openLink(string VendorCode)
        {
            string strURL = "EmptyBoxMonitoringScreen.aspx?CONTROLNO=" + VendorCode;

            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + strURL + "','_newtab');", true);
        }

        protected void btnDisplay_Click(object sender, EventArgs e)
        {
            getDetails();
        }
    }
}