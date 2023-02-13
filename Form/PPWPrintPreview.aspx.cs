using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Drawing;
using FGWHSEClient.DAL;


namespace FGWHSEClient.Form
{
    public partial class PPWPrintPreview : System.Web.UI.Page
    {
        public string strPartCode;
        public string strPartCode2;
        public string strQty;
        public string strLotMovementID;

        public InHouseReceivingDAL InHouseReceivingDAL = new InHouseReceivingDAL();
        protected void Page_Load(object sender, EventArgs e)
        {

            UpdatePrintFlag();

            strPartCode = "*" + Request.QueryString["PartCode"].ToString() + "*";
            strQty = "*" + Request.QueryString["Qty"].ToString() + "*";
            strPartCode2 = Request.QueryString["PartCode"].ToString();

            //lblPartName.Text = "INK BOT. UNBOXED,C,DYE:5300LP70NABD_";
            lblDate.Text = Request.QueryString["Date"].ToString();
            lblTime.Text = Request.QueryString["Time"].ToString();

            DataSet ds = new DataSet();
            ds = InHouseReceivingDAL.GET_MATERIAL(strPartCode2);

            lblPartName.Text = ds.Tables[0].Rows[0]["MaterialName"].ToString();

            ScriptManager.RegisterStartupScript(this, GetType(), "ShowRequest", "PrintDivContent();", true);
        }

        private void UpdatePrintFlag()
        {
            strLotMovementID = Request.QueryString["LotMovementID"].ToString();

            string strResult = "";
            strResult = InHouseReceivingDAL.UPDATE_LOTMOVEMENT_PRINTFLAG(strLotMovementID);
        }
    }
}
