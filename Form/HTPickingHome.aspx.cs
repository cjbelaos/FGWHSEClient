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
    public partial class HTPickingHome : System.Web.UI.Page
    {
        public string strUserID = "";
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
               
                strUserID = Session["UserID"].ToString();
                if (!this.IsPostBack)
                {
                    PendingPicking();
                    getPickingList();
                }
            }

        }

        protected void btnPickingList_Click(object sender, EventArgs e)
        {
            getPickingList();
        }

        public void getPickingList()
        {
            DataTable dt = new DataTable();

            //if (grdPickingList.Rows.Count == 0)
            //{
                dt = maint.PICKING_GET_ASSIGNED_PICKING_LIST(strUserID).Table;
                grdPickingList.DataSource = dt;
                grdPickingList.DataBind();

                if (dt.DefaultView.Count > 0)
                {
                    tbPick.Attributes.Add("style", "display:compact");
                    

                }
                else
                {
                    tbPick.Attributes.Add("style", "display:none");

                }
            //}
            lblPickingListCount.Text = grdPickingList.Rows.Count.ToString();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            cancellPickingList();
        }



        public void cancellPickingList()
        {
            try
            {
                Label lblPalletUnit = (Label)grdPickingList.Rows[0].FindControl("lblPALLETUNITNO");
                maint.PICKING_CANCEL_ASSIGNED_PICKING(lblPalletUnit.Text, Session["UserID"].ToString());
                getPickingList();
                MsgBox1.alert("Successfully cancelled!");
            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message.ToString());
            }
        }

        protected void btnExecute_Click(object sender, EventArgs e)
        {
            BeginPicking();
        }

        public void BeginPicking()
        {
            try
            {
                Label lblPalletUnit = (Label)grdPickingList.Rows[0].FindControl("lblPALLETUNITNO");
                maint.PICKING_BEGIN_PICKING(lblPalletUnit.Text, Session["UserID"].ToString());

                Response.Redirect("HTPick.aspx?DeviceType=HT&PALLET_UNIT=" + lblPalletUnit.Text);
            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message.ToString());
            }
        }

        public void PendingPicking()
        {
            try
            {
                DataView dv = new DataView();
               dv = maint.PICKING_GET_ONGOING_PICKING_LIST_USERID(Session["UserID"].ToString()).Table.DefaultView;
               if (dv.Count > 0)
               {
                   Response.Redirect("HTPick.aspx?DeviceType=HT&PALLET_UNIT=" + dv[0]["PALLETUNITNO"].ToString());
               }
            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message.ToString());
            }

        }


        protected void Timer1_Tick(object sender, EventArgs e)
        {
            getPickingList();
        }
    }
}
