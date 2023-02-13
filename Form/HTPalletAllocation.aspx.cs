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
    public partial class HTPalletAllocation : System.Web.UI.Page
    {
        public string strPageSubsystem = "";
        public string strAccessLevel = "";

        Maintenance maint = new Maintenance();

        protected DataTable dtPallet = new DataTable();

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
                    strPageSubsystem = "FGWHSE_005";
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
                    MakePalletDataTable(dtPallet);
                }
                else
                {
                    dtPallet = (DataTable)ViewState["dtPallet"];
                }
                ViewState["dtPallet"] = dtPallet;

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

        protected void txtLocationID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int intmaxLocationLength = Convert.ToInt32(ConfigurationManager.AppSettings["maxLocationLength"].ToString());

                string strLocationID = txtLocationID.Text.Trim();
                if (strLocationID.Length > intmaxLocationLength)
                {
                    strLocationID = strLocationID.Substring(0, intmaxLocationLength);
                }


                DataView dv = new DataView();
                dv = maint.CheckLocationIDMaster(strLocationID);

                if (dv.Count > 0)
                {
                    txtPalletNo.Focus();
                }
                else
                {
                    if (txtLocationID.Text.Trim() != "")
                    {


                        MsgBox1.alert("Location ID '" + strLocationID + "' does not exists in Location Master!");
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
                lblPalCount.Text = "0";
                //rdoAllocate.SelectedIndex = -1;
                txtLocationID.Focus();
                dtPallet.Clear();
                dtPallet.AcceptChanges();
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


                if (dtPallet.DefaultView.Count == 0)
                {
                    MsgBox1.alert("No items to save!");
                    return;


                }
                int iSave = 0;
                int iSave2 = 0;
                if (txtLocationID.Text.Trim() == "" || rdoAllocate.SelectedIndex == -1)
                {
                    MsgBox1.alert("Please complete the details before saving.");
                }

                else if (rdoAllocate.SelectedValue == "Allocate")
                {
                    if (dv.Count == 0)
                    {
                        if (dtPallet.DefaultView.Count > 0)
                        {
                            for (int x = 0; x < dtPallet.DefaultView.Count; x++)
                            {
                                iSave = maint.AddPalletAllocation(txtLocationID.Text.Trim().ToUpper(), dtPallet.DefaultView[x]["PalletNo"].ToString().ToUpper(), "P", Session["UserID"].ToString());
                                iSave2 = maint.AddOQAStatus(dtPallet.DefaultView[x]["PalletNo"].ToString().ToUpper());
                            }
                        }
                        MsgBox1.alert("Successfully Saved.");
                        dtPallet.Clear();
                        dtPallet.AcceptChanges();
                        txtPalletNo.Focus();
                        txtPalletNo.Attributes.Add("onfocus", "selectText();");

                    }
                    else
                    {
                        if (dv[0]["PalletNo"].ToString().Trim().ToUpper() == txtPalletNo.Text.ToString().Trim().ToUpper())
                        {
                            MsgBox1.alert("Pallet No. '" + txtPalletNo.Text.Trim().ToUpper() + "' has already allocated in Location '" + txtLocationID.Text.Trim().ToUpper() + "'.");
                            txtPalletNo.Focus();
                        }
                        else
                        {
                            if (dtPallet.DefaultView.Count > 0)
                            {
                                for (int x = 0; x < dtPallet.DefaultView.Count; x++)
                                {
                                    iSave = maint.AddPalletAllocation(txtLocationID.Text.Trim().ToUpper(), dtPallet.DefaultView[x]["PalletNo"].ToString().ToUpper(), "P", Session["UserID"].ToString());
                                    iSave2 = maint.AddOQAStatus(dtPallet.DefaultView[x]["PalletNo"].ToString().ToUpper());
                                }
                                MsgBox1.alert("Successfully Saved.");
                                dtPallet.Clear();
                                dtPallet.AcceptChanges();
                                txtPalletNo.Focus();
                                txtPalletNo.Attributes.Add("onfocus", "selectText();");
                            }
                            else
                            {
                                MsgBox1.alert("There is no scanned Pallet");
                                txtPalletNo.Focus();
                            }
                        }
                    }
                }
                else if (rdoAllocate.SelectedValue == "Unallocate")
                {
                    if (dtPallet.DefaultView.Count == 0)
                    {
                        //MsgBox1.alert("Pallet No. '" + txtPalletNo.Text.Trim() + "' is not allocated in Location '" + txtLocationID.Text.Trim() + "'.");
                        MsgBox1.alert("No Pallet to Unallocate");
                        txtPalletNo.Focus();
                    }
                    else
                    {
                        //if (dv[0]["PalletNo"].ToString().Trim().ToUpper() == txtPalletNo.Text.ToString().Trim().ToUpper())
                        //{
                        if (dtPallet.DefaultView.Count > 0)
                        {
                            for (int x = 0; x < dtPallet.DefaultView.Count; x++)
                            {
                                iSave = maint.AddPalletAllocation("", dtPallet.DefaultView[x]["PalletNo"].ToString().ToUpper(), "P", Session["UserID"].ToString());
                            }
                            MsgBox1.alert("Successfully Saved.");
                            dtPallet.Clear();
                            dtPallet.AcceptChanges();
                            txtPalletNo.Focus();
                            txtPalletNo.Attributes.Add("onfocus", "selectText();");
                        }
                        else
                        {
                            MsgBox1.alert("There is no scanned Pallet");
                            txtPalletNo.Focus();
                        }
                        //}
                        //else
                        //{
                        //    MsgBox1.alert("Pallet No. '" + txtPalletNo.Text.Trim() + "' is not allocated in Location '" + txtLocationID.Text.Trim() + "'.");
                        //}
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
            DataRow dr = dtPallet.NewRow();

            DataRow[] foundPallet = dtPallet.Select("PalletNo = '" + txtPalletNo.Text.Trim() + "'");
            if (foundPallet.Length != 0)
            {
                MsgBox1.alert("Pallet Already Scanned!");
                return;
            }




            try
            {
                DataView dv = new DataView();
                dv = maint.CheckPalletIDEPASS(txtPalletNo.Text.Trim());

                DataView dv2 = new DataView();
                dv2 = maint.GetVanningLaneCount(txtLocationID.Text.Trim());


                if (txtLocationID.Text.Trim() == "")
                {
                    MsgBox1.alert("Please input Location ID first.");
                    txtPalletNo.Text = "";
                    txtLocationID.Focus();
                }
                else
                {

                    //DataView dvPalletCheck = new DataView();
                    if (txtPalletNo.Text.Trim() != "")
                    {
                        //dvPalletCheck = maint.CHECK_PALLET_IF_ALREADY_INTERFACED(txtPalletNo.Text.Trim()).Tables[0].DefaultView;
                        //if (dvPalletCheck[0][0].ToString() == "0")
                        //{
                        //    MsgBox1.alert("Pallet has not yet interfaced in the System!");
                        //    return;
                        //}
                        //}
                        //if (dv.Count > 0)
                        // {

                        if (txtPalletNo.Text.Length == 8)
                        {

                            lblPalCount.Text = Convert.ToString((Convert.ToInt32(lblPalCount.Text) + 1));


                            if (Convert.ToInt32(dv2[0]["CNT"].ToString()) >= Convert.ToInt32(lblPalCount.Text))
                            {
                                dr["ID"] = lblPalCount.Text;
                                dr["PalletNo"] = txtPalletNo.Text.Trim();

                                dtPallet.Rows.Add(dr);
                                dtPallet.AcceptChanges();
                                txtPalletNo.Text = "";
                                txtPalletNo.Focus();
                            }
                            else
                            {
                                MsgBox1.alert("Failed to input Pallet. Already reached the maximum allowed number of Pallets for Location: " + txtLocationID.Text.Trim().ToUpper() + ".");
                                txtPalletNo.Focus();
                            }

                        }
                        else
                        {
                            MsgBox1.alert("Please Input Correct Pallet No. Format (8 Chars.)");
                            txtPalletNo.Focus();
                        }

                        //btnSave.Focus();
                        // }
                        //else
                        //{
                        //    if (txtLocationID.Text.Trim() != "")
                        //    {
                        //        MsgBox1.alert("Pallet No. '" + txtPalletNo.Text.Trim() + "' does not exists in Codeshot Production Result!");
                        //        txtPalletNo.Focus();
                        //    }

                    }
                    else
                    {
                        MsgBox1.alert("Enter Pallet No!");
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


        private void MakePalletDataTable(DataTable Ldt)
        {
            try
            {
                Ldt.Columns.Add("ID", typeof(String));
                Ldt.Columns.Add("PalletNo", typeof(String));

            }
            catch (Exception ex)
            {
                string x = (ex.ToString());
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lblPalCount.Text) == 0)
            {
                MsgBox1.alert("There is no Pallet Number to delete!");
                txtPalletNo.Focus();
            }
            else
            {
                doDeleteFile(lblPalCount.Text, dtPallet);
                lblPalCount.Text = Convert.ToString((Convert.ToInt32(lblPalCount.Text) - 1));

                MsgBox1.alert("Last Scanned Pallet has been removed to list!");
                txtPalletNo.Focus();
            }



        }


        public void doDeleteFile(string strID, DataTable Ldt)
        {


            DataRow[] drr = Ldt.Select("ID='" + strID + "'");

            for (int i = 0; i < drr.Length; i++)
            {
                //FixAppDomainRestartWhenTouchingFiles();
                drr[i].Delete();
            }

            Ldt.AcceptChanges();

            //bindGrid(Ldt, grdUpload);
        }




    }
}
