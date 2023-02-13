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
    public partial class PDAntennaMonitoringScreen : System.Web.UI.Page
    {

        public InHouseReceivingDAL InHouseReceivingDAL = new InHouseReceivingDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["TRANSFERLISTID"] != null)
            {
                fillDetails(Request.QueryString["TRANSFERLISTID"].ToString());
            }
            //fillDetails("29369773");
        }


        public void fillDetails(string strTRANSFERLISTID)
        {

            DataTable dt = InHouseReceivingDAL.RFID_GET_INK_ANTENNA_READ_DETAILS(strTRANSFERLISTID).Tables[0];

            grdView.DataSource = dt;
            grdView.DataBind();

            DataView dv = dt.DefaultView;
            if (dv.Count > 0)
            {
                lblCount.Text = dv[0]["ANNTENNACOUNT"].ToString() + "/" + dv[0]["EKPICKCOUNT"].ToString();
            }
            else
            {
                lblCount.Text = "";
            }
        }
    }
}