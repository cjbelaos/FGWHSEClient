using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace FGWHSEClient.Form
{
    public partial class ContainerLabelBarcode : System.Web.UI.Page
    {
        private void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
            imgBarcode.ImageUrl = "BarCodes.aspx?ContNo=" + Request.QueryString["ContNo"] + "&Container=" + Request.QueryString["Container"];
            GetContainerDetails();
        }


        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary />
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary />
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion


        protected void GetContainerDetails()
        {
            DataView dv = new DataView();
            Maintenance maint = new Maintenance();

            dv = maint.GET_CONTAINER_LABEL_DETAILS(Request.QueryString["ContNo"].ToString());
            if (dv.Count > 0)
            {
                lblODNo.Text = dv[0]["OD_NO"].ToString();
                lblContainerNo.Text = dv[0]["CONTAINER_NO"].ToString();
            }
            else
            {
                MsgBox1.alert("No Data Found.");
            }
        }


    }
}

