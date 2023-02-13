using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.eppi.utils;
using System.Xml.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using FGWHSEClient.LdapService;
using FGWHSEClient.LogInService;
using FGWHSEClient.DAL;

namespace FGWHSEClient.Form
{
    public partial class DNReceivingScreen : System.Web.UI.Page
    {
        public DeliveryReceivingDAL drDal = new DeliveryReceivingDAL();
        public DNReceivingScreenDAL dnRcvScreenDal = new DNReceivingScreenDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                FillLoadingDock();
               

                if (Request.QueryString.Count > 0)
                {
                    if (Request["loadingdock"] != null)
                    {
                        string strLoadingDockSelected = Request["loadingdock"].ToString().Trim().ToUpper();
                        ddlLoadingDock.SelectedValue = strLoadingDockSelected;
                    }
                }

                lblLastUpdateDate.Text = DateTime.Now.ToString();
                FillGrid();
                
            }
        }

        private void FillLoadingDock()
        {
            DataTable dtLoadingDock = new DataTable();
            dtLoadingDock = drDal.WH_GetLoadingDock().Tables[0];

            DataTable dtDock = new DataTable();


            dtDock.Columns.Add("location_id", typeof(string));
            dtDock.Columns.Add("location_name", typeof(string));

            foreach (DataRow row in dtLoadingDock.Rows)
            {

                String locID = (string)row[0];
                String locNAME = (string)row[1];
                dtDock.Rows.Add(locID, locNAME);
            }

            ddlLoadingDock.DataSource = dtDock;
            ddlLoadingDock.DataTextField = "location_name";
            ddlLoadingDock.DataValueField = "location_id";
            ddlLoadingDock.DataBind();
        }

        protected void ddlLoadingDock_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FillGrid();
        }

        protected void gvDNReceiving_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // DITO DAW
            try
            {
                int rownum = 0;
                rownum++;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int vColumnCnt = -1;
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    if (drv["BYPASSFLAG"].ToString().Equals("1"))
                    {
                        e.Row.BackColor = Color.Gold;
                    }

                    else e.Row.BackColor = Color.White;

                    foreach (TableCell vCell in e.Row.Cells)
                    {
                        vColumnCnt++;
                        if (gvDNReceiving.HeaderRow.Cells[vColumnCnt].Text.ToString() == "DN No.")
                        {

                            string Location = ResolveUrl("~/Form/DNReceivingMonitoringScreen.aspx?DNNo=" + e.Row.Cells[0].Text);
                            e.Row.Cells[vColumnCnt].Attributes["onClick"] = string.Format("javascript:window.location='{0}';", Location);
                            e.Row.Cells[vColumnCnt].Style["cursor"] = "pointer";
                            e.Row.Cells[vColumnCnt].Style["color"] = "blue";
                            e.Row.Cells[vColumnCnt].Style["text-decoration"] = "underline";
                        }
                        
                        else if (gvDNReceiving.HeaderRow.Cells[vColumnCnt].Text.ToString() == "Difference")
                        {
                            if (e.Row.Cells[vColumnCnt].Text != "0")
                            {
                                e.Row.Cells[vColumnCnt].ForeColor = Color.Red;
                            }
                            //if (e.Row.Cells[vColumnCnt].Text == "ONGOING UNLOADING")
                            //{
                            //    e.Row.BackColor = Color.FromArgb(204, 255, 204);
                            //    
                            //}
                            //else if (e.Row.Cells[vColumnCnt].Text == "COMPLETE DELIVERY")
                            //{

                            //    e.Row.BackColor = Color.FromArgb(163, 189, 221);

                            //}
                            //else
                            //{
                            //    e.Row.BackColor = Color.White;
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                //MsgBox1.alert(ex.Message);
            }
        }

        private void FillGrid()
        {

            //string strDNNo = "";
            //string strDateFrom = "";
            //string strDateTo = "";
            //string strSupplier = "";
            //string strStatus = "";

            //strDNNo = "%" + txtDNNo.Text.ToString().Trim().ToUpper() + "%";
            //strDateFrom = txtDateFrom.Text.ToString().Trim().ToUpper();
            //strDateTo = txtDateTo.Text.ToString().Trim().ToUpper();

            //if (ddlSupplier.SelectedItem.Text.ToString().Trim().ToUpper() == "ALL")
            //{
            //    strSupplier = "%%";
            //}
            //else
            //{
            //    strSupplier = ddlSupplier.SelectedValue.ToString().Trim().ToUpper();
            //}

            //if (ddlStatus.SelectedItem.Text.ToString().Trim().ToUpper() == "ALL")
            //{
            //    strStatus = "%%";
            //}
            //else
            //{
            //    strStatus = ddlStatus.SelectedValue.ToString().Trim().ToUpper();
            //}

            //DataView dv = new DataView();
            //dv = drDal.DN_GetDNReceivingExecuteScreen(strDNNo,
            //                                           strDateFrom,
            //                                           strDateTo,
            //                                           strSupplier,
            //                                           strStatus);

            DataSet dsDnRcvScreen = new DataSet();

            dsDnRcvScreen = dnRcvScreenDal.DN_DisplayReceivingMonitoringScreen(ddlLoadingDock.SelectedValue);


            if (dsDnRcvScreen.Tables[0].Rows.Count > 0)
            {
                gvDNReceiving.DataSource = dsDnRcvScreen.Tables[0];
                gvDNReceiving.DataBind();
            }
            else
            {
                gvDNReceiving.DataSource = "";
                gvDNReceiving.DataBind();
                //error message
            }
        }

        protected void btnDisplay_Click(object sender, EventArgs e)
        {
            lblLastUpdateDate.Text = DateTime.Now.ToString();
            FillGrid();
        }
    }
}