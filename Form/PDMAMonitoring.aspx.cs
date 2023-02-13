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
using System.IO;
using System.Drawing;
namespace FGWHSEClient.Form
{
    public partial class PDMAMonitoring : System.Web.UI.Page
    {
        public InHouseReceivingDAL InHouseReceivingDAL = new InHouseReceivingDAL();

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

        protected void btnDisplay_Click(object sender, EventArgs e)
        {
            lblLastUpdateDate.Text = DateTime.Now.ToString();
            FillGrid();
        }

        private void FillGrid()
        {
            DataSet ds = new DataSet();
            ds = InHouseReceivingDAL.RFID_GET_INHOUSE_ANTENNA_READ_2(ddlLoadingDock.SelectedValue);

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvParts.DataSource = ds.Tables[0];
                gvParts.DataBind();
            }
            else
            {
                gvParts.DataSource = "";
                gvParts.DataBind();
                //error message
            }

            //DataTable dt = new DataTable();  

            ////Adding columns  

            //dt.Columns.Add("PartCode", typeof(string));
            //dt.Columns.Add("PartName", typeof(string));
            //dt.Columns.Add("BoxCount", typeof(string));
            //dt.Columns.Add("Qty", typeof(string)); 

            ////Adding Row  
            //DataRow dr = dt.NewRow();
            //dr["PartCode"] = "175054800";
            //dr["PartName"] = "INK BOTTLE";
            //dr["BoxCount"] = "3";
            //dr["Qty"] = "105"; 
            ////dt.Rows.Add(dr);  

            ////dr = dt.NewRow();  
            ////dr["Names"] = "Krishna";  
            //dt.Rows.Add(dr);

            //gvParts.DataSource = dt;
            //gvParts.DataBind(); 

        }

        private void FillLoadingDock()
        {
            DataTable dtLoadingDock = new DataTable();
            dtLoadingDock = InHouseReceivingDAL.DN_GETLOADINGDOCK_INHOUSE_MA().Tables[0];

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

        protected void gvParts_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvParts_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            try
            {
                // In template column,
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    DataRowView drv = e.Row.DataItem as DataRowView;
                    if (drv["Difference"].ToString() != "0")
                    // if (dnqty.Text != scannedqty.Text)
                    {
                        //e.Row.Cells[2].Text = "IN";
                        //e.Row.Cells[2].BackColor = Color.Blue;
                        e.Row.Cells[0].ForeColor = Color.Red;
                        e.Row.Cells[1].ForeColor = Color.Red;
                        e.Row.Cells[2].ForeColor = Color.Red;
                        e.Row.Cells[3].ForeColor = Color.Red;
                        e.Row.Cells[4].ForeColor = Color.Red;
                        e.Row.Cells[5].ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.GetInstance().Fatal(ex.StackTrace, ex);
                //MsgBox1.alert(ex.Message);
            }
        }

        protected void ddlLoadingDock_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblLastUpdateDate.Text = DateTime.Now.ToString();
            FillGrid();
        }


        protected void lnk_Click(object sender, EventArgs e)
        {

            int rowIndx;
            var rowTrigger = (Control)sender;
            GridViewRow rowUpdate = (GridViewRow)rowTrigger.NamingContainer;
            rowIndx = rowUpdate.RowIndex;
            Label lbl = (Label)gvParts.Rows[rowIndx].FindControl("lblEKTransferListID");
           
            Response.Redirect("PDAntennaMonitoringScreen.aspx?TRANSFERLISTID=" + lbl.Text);
            //openLink(lnk.Text);

        }
    }
}