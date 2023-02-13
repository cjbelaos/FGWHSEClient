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
    public partial class HTODAllocation : System.Web.UI.Page
    {
        public string strPageSubsystem = "";
        public string strAccessLevel = "";
        public string InkPalletNo = ConfigurationManager.AppSettings["InkPalletNo"].ToString();

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
                    strPageSubsystem = "FGWHSE_008";
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
                    txtPalletNo.Focus();
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
                txtODNo.Text = "";
                txtPalletNo.Text = "";
                txtCaseNo.Text = "";
                //rdoAllocate.SelectedIndex = -1;
                txtPalletNo.Focus();
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

                int iSave = 0;



                if (rdoAllocate.SelectedValue == "Allocate")
                {
                    if (txtODNo.Text.Trim() == "" || txtPalletNo.Text.Trim() == "" || txtCaseNo.Text.Trim() == "" || rdoAllocate.SelectedIndex == -1)
                    {
                        MsgBox1.alert("Please complete the details before saving.");
                    }
                    else
                    {
                        iSave = maint.InkUpdateODNumber(txtODNo.Text.Trim(), Convert.ToInt32(txtCaseNo.Text.Trim()), txtPalletNo.Text.Trim(), Session["UserID"].ToString());
                        MsgBox1.alert("Successfully Saved.");
                        txtPalletNo.Focus();
                    }

                }
                else if (rdoAllocate.SelectedValue == "Unallocate")
                {
                    if (txtPalletNo.Text.Trim() == "" || txtCaseNo.Text.Trim() == "" || rdoAllocate.SelectedIndex == -1)
                    {
                        MsgBox1.alert("Please input Pallet No. and Case No. before saving.");
                        txtPalletNo.Focus();
                    }
                    else
                    {
                        iSave = maint.InkUpdateODNumber("", Convert.ToInt32(txtCaseNo.Text.Trim()), txtPalletNo.Text.Trim(), Session["UserID"].ToString());
                        MsgBox1.alert("Successfully Saved.");
                        txtPalletNo.Focus();
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);
            }
        }

        protected void txtPalletNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPalletNo.Text.Trim().Substring(0, 2) == InkPalletNo)
                {
                    DataView dv = new DataView();

                    dv = maint.InkCheckPalletExist(txtPalletNo.Text.Trim());

                    if (dv.Count > 0)
                    {
                        txtODNo.Focus();
                    }
                    else
                    {
                        MsgBox1.alert("Pallet '" + txtPalletNo.Text.Trim().ToUpper() + "' does not exist in the location.");
                        txtPalletNo.Focus();
                    }
                }
                else
                {
                    MsgBox1.alert("Please check the inputted Pallet No. Only Ink Bottle Pallet No. are allowed to transact.");
                    txtPalletNo.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);
            }
        }

        protected void txtODNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtCaseNo.Focus();
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);
            }
        }

        protected void txtCaseNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                btnSave.Focus();
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);
            }
        }




    }
}

