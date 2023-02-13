using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.eppi.utils;


namespace FGWHSEClient.Form
{
    public partial class HTExitFactory : System.Web.UI.Page
    {
        public string strPageSubsystem = "";
        public string strAccessLevel = "";

        Maintenance maint = new Maintenance();



        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Write("<script>");
                    Response.Write("alert('Session expired! Please log in again.');");
                    Response.Write("window.location = '../HTLogin.aspx';");
                    Response.Write("</script>");
                }
                else
                {
                    strPageSubsystem = "FGWHSE_010";
                    if (!checkAuthority(strPageSubsystem))
                    {
                        Response.Write("<script>");
                        Response.Write("alert('You are not authorized to access the page.');");
                        Response.Write("window.location = 'Default.aspx';");
                        Response.Write("</script>");
                    }
                }


                if (!this.IsPostBack)
                {
                    txtContainerNo.Focus();
                }



            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }



        private bool checkAuthority(string strPageSubsystem)
        {
            bool isValid = false;
            try
            {
                if (Session["Subsystem"] != null)
                {
                    DataView dvSubsystem = new DataView();
                    dvSubsystem = (DataView)Session["Subsystem"];

                    if (dvSubsystem.Count > 0)
                    {
                        dvSubsystem.Sort = "Subsystem";

                        int iRow = dvSubsystem.Find(strPageSubsystem);

                        if (iRow >= 0)
                        {
                            isValid = true;
                        }
                        else
                        {
                            isValid = false;
                        }

                        string strRole = dvSubsystem.Table.Rows[iRow]["Role"].ToString();

                        if (strRole != "")
                        {
                            strAccessLevel = strRole;
                        }

                    }
                }
                return isValid;
            }
            catch (Exception ex)
            {
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);

                isValid = false;
                return isValid;
            }
        }



        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtContainerNo.Text = "";
                //rdoAllocate.SelectedIndex = -1;
                txtContainerNo.Focus();
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dv = new DataView();
                dv = maint.InkCheckContainerLocation(txtContainerNo.Text.Trim());

                DataView dv2 = new DataView();
                dv2 = maint.InkCheckContainerDelivery(txtContainerNo.Text.Trim());

                DataView dv3 = new DataView();
                dv3 = maint.InkCheckContainerExpiration(txtContainerNo.Text.Trim());

                int iSave = 0;

                if (txtContainerNo.Text.Trim() == "" || rdoAllocate.SelectedIndex == -1)
                {
                    MsgBox1.alert("Please complete the details before saving.");
                }

                else if (rdoAllocate.SelectedValue == "Set Off")
                {
                    if (dv.Count > 0)
                    {
                        if (dv2.Count > 0)
                        {
                            MsgBox1.alert("Pallet: '" + dv2[0]["Pallet"].ToString().ToUpper() + "' is not allowed to Deliver (Lot No. '" + dv2[0]["Lot_No"].ToString().ToUpper() + "').");
                            txtContainerNo.Focus();
                        }
                        else
                        {
                            if (dv3.Count > 0)
                            {
                                MsgBox1.alert("Pallet: '" + dv3[0]["Pallet"].ToString().ToUpper() + "' is Expired (Lot No. '" + dv3[0]["Lot_No"].ToString().ToUpper() + "').");
                                txtContainerNo.Focus();
                            }
                            else
                            {
                                iSave = maint.InkSetOffExfact(txtContainerNo.Text.Trim().ToUpper(), Session["UserID"].ToString());
                                MsgBox1.alert("Successfully Saved.");
                                txtContainerNo.Focus();
                            }
                        }
                    }
                    else
                    {
                        MsgBox1.alert("Container '" + txtContainerNo.Text.Trim().ToUpper() + "' has already been delivered.");
                        txtContainerNo.Focus();
                    }
                }
                else if (rdoAllocate.SelectedValue == "Return")
                {
                    if (dv.Count > 0)
                    {
                        MsgBox1.alert("Container No. '" + txtContainerNo.Text.Trim().ToUpper() + "' is not delivered yet.");
                        txtContainerNo.Focus();
                    }
                    else
                    {
                        iSave = maint.InkReturnExfact(txtContainerNo.Text.Trim().ToUpper(), Session["UserID"].ToString());
                        MsgBox1.alert("Successfully Saved.");
                        txtContainerNo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);
            }
        }






    }
}
