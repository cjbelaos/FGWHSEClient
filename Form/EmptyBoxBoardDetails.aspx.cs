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
    public partial class EmptyBoxBoardDetails : System.Web.UI.Page
    {
        EmptyPcaseDAL epDAL = new EmptyPcaseDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                if (Request.QueryString["SUPPLIERID"] != null)
                {
                    lblSupplierID.Text  = Request.QueryString["SUPPLIERID"].ToString();
                }

                if(Request.QueryString["SUPPLIERNAME"] != null)
                {
                    string strSupplierName = Request.QueryString["SUPPLIERNAME"].ToString();
                    strSupplierName = strSupplierName.Replace("replacewithand", "&");
                    lblSupplierName.Text = strSupplierName;
                }

                if (Request.QueryString["RATE"] != null)
                {
                    lblRate.Text = Request.QueryString["RATE"].ToString();
                }

                getDetails();
            }
           
        }

        public void getDetails()
        {
            lblDate.Text = DateTime.Now.ToString();
            DataTable dt = epDAL.GET_EMPTY_PCASE_BOX_BOARD_DETAILS(lblDate.Text, lblSupplierID.Text).Tables[0];
         
            gvLoad.DataSource = dt;
            gvLoad.DataBind();

        }

        
    }
}