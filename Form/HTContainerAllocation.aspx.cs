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
    public partial class HTContainerAllocation : System.Web.UI.Page
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
                    strPageSubsystem = "FGWHSE_004";
                    if (!checkAuthority(strPageSubsystem))
                    {
                        Response.Write("<script>");
                        Response.Write("alert('You are not authorized to access the page.');");
                        Response.Write("window.location = 'HTDefault.aspx';");
                        Response.Write("</script>");
                    }
                }


                if (!this.IsPostBack)
                {
                    txtLocationID.Focus();
                }
                else
                {
                    if (Request.Form["deleteConfirm"] != null)
                    {
                        if (Request.Form["deleteConfirm"].ToString().Equals("1"))
                        {
                            deleteRecord();
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }


        private void deleteRecord()
        {
            try
            {
                string str = maint.DeleteContainer(txtPalletNo.Text.Trim().ToUpper());

                if (str.Length > 0)
                {
                    MsgBox1.alert(str);
                }
                else
                {
                    MsgBox1.alert("Record Successfully Deleted.");
                    txtPalletNo.Text = "";
                    txtLocationID.Focus();
                }

            }
            catch (Exception ex)
            {
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

        protected void txtLocationID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dv = new DataView();
                dv = maint.CheckLocationIDMaster(txtLocationID.Text.Trim());

                if (dv.Count > 0)
                {
                    if (dv[0]["location_type_id"].ToString().Trim() == "VL")
                    {
                        MsgBox1.alert("Cannot allocate Container in Vanning Lane.");
                        txtLocationID.Focus();
                    }
                    else
                    {
                        txtPalletNo.Focus();
                    }
                }
                else
                {
                    if (txtLocationID.Text.Trim() != "")
                    {
                        MsgBox1.alert("Loading Bay '" + txtLocationID.Text.Trim() + "' does not exists in Location Master!");
                        txtLocationID.Focus();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtLocationID.Text = "";
                txtPalletNo.Text = "";
                //rdoAllocate.SelectedIndex = -1;
                txtLocationID.Focus();
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
                dv = maint.CheckPalletAllocation(txtLocationID.Text.Trim(), txtPalletNo.Text.Trim());


                //DataView dvContainerCheck = new DataView();
                //if (txtPalletNo.Text.Trim() != "")
                //{
                //    dvContainerCheck = maint.CHECK_CONTAINER_IF_ALREADY_INTERFACED(txtPalletNo.Text.Trim()).Tables[0].DefaultView;
                //    if (dvContainerCheck[0][0].ToString() == "0")
                //    {
                //        MsgBox1.alert("Container has not yet interfaced in the System!");
                //        return;
                //    }
                //}

                int iSave = 0;

                if (txtLocationID.Text.Trim() == "" || txtPalletNo.Text.Trim() == "" || rdoAllocate.SelectedIndex == -1)
                {
                    MsgBox1.alert("Please complete the details before saving.");
                }

                else if (rdoAllocate.SelectedValue == "Allocate")
                {
                    if (dv.Count == 0)
                    {
                        iSave = maint.AddContainerAllocation(txtLocationID.Text.Trim().ToUpper(), txtPalletNo.Text.Trim().ToUpper(), "C", Session["UserID"].ToString());
                        MsgBox1.alert("Successfully Saved.");
                        txtLocationID.Focus();
                    }
                    else
                    {
                        if (dv[0]["PalletNo"].ToString().Trim().ToUpper() == txtPalletNo.Text.ToString().Trim().ToUpper())
                        {
                            MsgBox1.alert("Container No. '" + txtPalletNo.Text.Trim().ToUpper() + "' has already allocated in '" + txtLocationID.Text.Trim().ToUpper() + "'.");
                            txtPalletNo.Focus();
                        }
                        else
                        {
                            iSave = maint.AddContainerAllocation(txtLocationID.Text.Trim().ToUpper(), txtPalletNo.Text.Trim().ToUpper(), "C", Session["UserID"].ToString());
                            MsgBox1.alert("Successfully Saved.");
                            txtLocationID.Focus();
                        }
                    }
                }
                else if (rdoAllocate.SelectedValue == "Unallocate")
                {
                    if (dv.Count == 0)
                    {
                        MsgBox1.alert("Container No. '" + txtPalletNo.Text.Trim().ToUpper() + "' is not allocated in '" + txtLocationID.Text.Trim().ToUpper() + "'.");
                        txtPalletNo.Focus();
                    }
                    else
                    {
                        if (dv[0]["PalletNo"].ToString().Trim().ToUpper() == txtPalletNo.Text.ToString().Trim().ToUpper())
                        {
                            iSave = maint.AddContainerAllocation("CY", txtPalletNo.Text.Trim().ToUpper(), "C", Session["UserID"].ToString());
                            MsgBox1.alert("Successfully Saved.");
                            txtLocationID.Focus();
                        }
                        else
                        {
                            MsgBox1.alert("Container No. '" + txtPalletNo.Text.Trim().ToUpper() + "' is not allocated in '" + txtLocationID.Text.Trim().ToUpper() + "'.");
                            txtPalletNo.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);
            }
        }



        protected void btnDeleteContainer_Click(object sender, EventArgs e)
        {
            try
            {
                 DataView dv = new DataView();
                 dv = maint.CheckContainerAllocation(txtPalletNo.Text.Trim());

                 DataView dv2 = new DataView();
                 dv2 = maint.CheckPalletContainer(txtPalletNo.Text.Trim());


                if (txtPalletNo.Text.Trim() != "")
                {
                    if (dv.Count > 0)
                    {
                        if (dv2.Count > 0)
                        {
                            MsgBox1.alert("Deleting Failed. Currently have Pallet/s allocated inside Container.");
                            txtPalletNo.Focus();
                        }
                        else
                        {
                            MsgBox1.confirm("Are you sure you want to delete this record?", "deleteConfirm");
                        }
                    }
                    else
                    {
                        MsgBox1.alert("Container No. does not exist.");
                        txtPalletNo.Focus();
                    }
                }
                else
                {
                    MsgBox1.alert("Please input Container No.");
                    txtPalletNo.Focus();
                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }


    }
}
