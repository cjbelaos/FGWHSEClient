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
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;

namespace FGWHSEClient.Form
{
    public partial class AntennaRegistration : System.Web.UI.Page
    {

        InHouseReceivingDAL IHR = new InHouseReceivingDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                FillDropDown(ddAntennaType, IHR.GET_RFID_ANTENNA_TYPE().Tables[0]);
                FillDropDown(ddLocation, IHR.GET_RFID_AREA_LOCATION().Tables[0]);
                IHR.PopulateGrid(grdDetails, IHR.GET_RFID_ANTENNA_LIST());
            }
        }



        private void FillDropDown(DropDownList ddList, DataTable dt)
        {
           
            DataTable dtList = new DataTable();

            dtList.Columns.Add("ID", typeof(string));
            dtList.Columns.Add("DESC", typeof(string));
            foreach (DataRow row in dt.Rows)
            {
                String id = (string)Convert.ToString(row[0]);
                String desc = (string)row[1];
                dtList.Rows.Add(id, desc);
            }

            ddList.DataSource = dtList;
            ddList.DataTextField = "DESC";
            ddList.DataValueField = "ID";
            ddList.DataBind();
        }


        public void findTextFromGrid(string strFIND)
        {

        }


    }

}