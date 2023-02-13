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
using System.Drawing;
using FGWHSEClient.DAL;


namespace FGWHSEClient.Form
{
    public partial class PPWMonitoring : System.Web.UI.Page
    {
        public InHouseReceivingDAL InHouseReceivingDAL = new InHouseReceivingDAL();
        Label lblPartCode, lblBoxCount, lblQty, lblDate, lblTime, lblID;
        string sNewDate;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                FillGate();
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

            ////////FOR LIVE/////////
            DataSet ds = new DataSet();
            ds = InHouseReceivingDAL.RFID_GET_PPW_ANTENNA_READ(ddlLoadingDock.SelectedValue);
            ////////FOR LIVE/////////

            ////FOR TRIAL
            //DataSet ds = new DataSet();
            //ds = InHouseReceivingDAL.RFID_DUMMY_GET_DATE();
            ////FOR TRIAL



            if (ds.Tables[0].Rows.Count > 0)
            {
                gvReceive.DataSource = ds.Tables[0];
                gvReceive.DataBind();
            }
            else
            {
                gvReceive.DataSource = null;
                gvReceive.DataBind();
                //error message
            }
            /////////////////////////

        }

        protected void lnkEmpNo_Click(object sender, EventArgs e)
        {
            var TLink = (Control)sender;
            GridViewRow row = (GridViewRow)TLink.NamingContainer;
            LinkButton lnk = sender as LinkButton;

            lblPartCode = (Label)row.FindControl("lblPartCode");
            lblBoxCount = (Label)row.FindControl("lblBoxCount");
            lblQty = (Label)row.FindControl("lblQty");
            lblTime = (Label)row.FindControl("lblTime");
            lblDate = (Label)row.FindControl("lblDate");
            lblID = (Label)row.FindControl("lblID");
            string sDate = lblDate.Text;
            DateTime dt = Convert.ToDateTime(sDate);
            sNewDate = dt.ToString("MM/dd/yyyy");

            PrintPreview();


        }

        private void PrintPreview()
        {
            Response.Redirect("PPWPrintPreview.aspx?PartCode=" + lblPartCode.Text + "&Qty=" + lblQty.Text + "&Date=" + sNewDate + "&Time=" + lblTime.Text + "&LotMovementID=" + lblID.Text);
        }

        protected void gvReceive_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[7].Visible = false;
        }

        protected void ddlLoadingDock_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FillGrid();
        }

        private void FillGate()
        {
            DataTable dtLoadingDock = new DataTable();
            dtLoadingDock = InHouseReceivingDAL.RFID_PPW_GET_LOCATION_LIST().Tables[0];

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
    }
}