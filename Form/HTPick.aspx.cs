using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace FGWHSEClient.Form
{
    public partial class HTPick : System.Web.UI.Page
    {
        Maintenance maint = new Maintenance();

        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (Session["UserID"] == null)
            {
                Response.Write("<script>");
                Response.Write("alert('Session expired! Please log in again.');");
                Response.Write("window.location = '../HTLogin.aspx';");
                Response.Write("</script>");

                //strUserID = "d016023";
                //getPickingList();
            }
            else
            {
                if (Request.QueryString["PALLET_UNIT"] != null)
                {
                    if (!this.IsPostBack)
                    {
                        txtPallet.Attributes.Add("onkeydown", "return (event.keyCode!=13);false;");
                        txtLocation.Attributes.Add("onkeydown", "return (event.keyCode!=13);false;");
                        txtPallet.Focus();
                        lblPalletUnit.Text = Request.QueryString["PALLET_UNIT"].ToString();
                        getList(lblPalletUnit.Text);
                    }
                }
                else
                {
                    Response.Redirect("HTPickingHome.aspx?DeviceType=HT");
                }
            }
        }

        public void getList(string PALLET_UNIT)
        {
            DataView dv = new DataView();
            DataTable dt = new DataTable();
            dt = maint.PICKING_GET_ONGOING_PICKING_LIST(PALLET_UNIT,Session["UserID"].ToString()).Table;
            grdPickingList.DataSource = dt;
            grdPickingList.DataBind();

            dv = dt.DefaultView;
            if (dv.Count > 0)
            {

                lblContainer.Text = dv[0]["CONTAINERNO"].ToString();
                lblODNO.Text = dv[0]["ODNO"].ToString();
                lblVLANE.Text = dv[0]["PICKINGLOCATIONID"].ToString() + dv[0]["POSITIONID"].ToString();
                lblLBAY.Text = dv[0]["DESTINATIONLOCATIONID"].ToString();

                lblPalletGuide.Text = dv[0]["PALLETNO"].ToString();
                lblLocationGuide.Text = dv[0]["STAGINGBAYID"].ToString();

                //extPallet.WatermarkText = dv[0]["PALLETNO"].ToString();
                //extLocation.WatermarkText = dv[0]["STAGINGBAYID"].ToString();
                if (dv[0]["PALLETPICKINGSTATUS"].ToString() == "3")
                {
                    txtPallet.Text = dv[0]["PALLETNO"].ToString();
                    txtLocation.Focus();
                    txtPallet.Enabled = false;
                }

                lblPallet.Text = dv[0]["PALLETNO"].ToString();
                lblLocation.Text = dv[0]["STAGINGBAYID"].ToString();

            }
        }

        
        protected void btnExecute_Click(object sender, EventArgs e)
        {
            DeliverPallet();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            cancelPickingList();
        }


        public void DeliverPallet()
        {
            if (txtPallet.Text.Trim() == "" || txtLocation.Text == "")
            {
                lblError.Text = "Please input value on the required fields!";
                return;
            }

            maint.PICKING_DELIVER_PALLET(lblPalletUnit.Text, Session["UserID"].ToString());
            getList(lblPalletUnit.Text);
            lblOK.Text = "Pallet Successfully Delivered!";
        }

        protected void btnPick_Click(object sender, EventArgs e)
        {
            pickPallet();
        }


        public void pickPallet()
        {
            maint.PICKING_PALLET(lblPalletUnit.Text, Session["UserID"].ToString());
            getList(lblPalletUnit.Text);
            txtPallet.Enabled = false;
            txtLocation.Enabled = true;
            txtLocation.Focus();
            lblOK.Text = "Successfully picked!";
            //                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_txtPallet" %>').disabled = true;
            //                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_txtLocation" %>').disabled = false;  
            //                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_txtLocation" %>').focus();
            //                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_lblError" %>').innerHTML = ""
            //lblOK.Text = "Pallet Successfully picked!";

       
        }




        public void cancelPickingList()
        {
            try
            {

                
                maint.PICKING_CANCEL_ASSIGNED_PICKING(lblPalletUnit.Text, Session["UserID"].ToString());
              

                Response.Write("<script>");
                Response.Write("alert('Successfully cancelled!');");
                Response.Write("window.location = 'HTMenu.aspx?DeviceType=HT';");
                Response.Write("</script>");
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message.ToString();
            }
        }

    }
}
