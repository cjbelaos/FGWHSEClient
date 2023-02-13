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
    public partial class HTPalletLoading : System.Web.UI.Page
    {
        public string strPageSubsystem = "";
        public string strAccessLevel = "";
        public string InkPalletNo = ConfigurationManager.AppSettings["InkPalletNo"].ToString();

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
                    strPageSubsystem = "FGWHSE_009";
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
                    txtContainerID.Focus();
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

        protected void txtContainerID_TextChanged(object sender, EventArgs e)
        {
            try
            {

                DataView dv = new DataView();
                dv = maint.InkCheckContainerLocation(txtContainerID.Text.Trim());

                if (dv.Count > 0)
                {
                    if (dv[0]["Location_ID"].ToString().Trim().ToUpper() == "CY")
                    {
                        MsgBox1.alert("Container '" + txtContainerID.Text.Trim().ToUpper() + "' is currently in CY. Please allocate first the Container to Loading Bay.");
                        txtContainerID.Focus();
                    }
                    else
                    {
                        txtPalletNo.Focus();
                    }
                }
                else
                {
                    MsgBox1.alert("Container does not exist in any location.");
                    txtContainerID.Focus();
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
                txtContainerID.Text = "";
                txtPalletNo.Text = "";
                lblPalCount.Text = "0";
                //rdoAllocate.SelectedIndex = -1;
                txtContainerID.Focus();
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

                int iSave = 0;

                if (dtPallet.DefaultView.Count == 0)
                {
                    MsgBox1.alert("No items to save!");
                    return;
                }
                if (txtContainerID.Text.Trim() == "" || rdoAllocate.SelectedIndex == -1)
                {
                    MsgBox1.alert("Please complete the details before saving.");
                }

                else
                {

                    if (dtPallet.DefaultView.Count > 0)
                    {
                        for (int x = 0; x < dtPallet.DefaultView.Count; x++)
                        {
                            iSave = maint.InkUpdatePalletContainer(dtPallet.DefaultView[x]["PalletNo"].ToString().ToUpper(), txtContainerID.Text.Trim().ToUpper(), rdoAllocate.SelectedValue.ToString(), Session["UserID"].ToString());

                        }
                    }
                    MsgBox1.alert("Successfully Saved.");
                    dtPallet.Clear();
                    dtPallet.AcceptChanges();
                    lblPalCount.Text = "0";
                    txtPalletNo.Focus();
                    txtPalletNo.Attributes.Add("onfocus", "selectText();");

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
            DataView dvCartonInfo = new DataView();
            dvCartonInfo = maint.InkCheckCartonInfo(txtPalletNo.Text.Trim());

            DataRow dr = dtPallet.NewRow();

            DataRow[] foundPallet = dtPallet.Select("PalletNo = '" + txtPalletNo.Text.Trim() + "'");
            if (foundPallet.Length != 0)
            {
                MsgBox1.alert("Pallet Already Scanned!");
                return;
            }



            try
            {
                if (txtPalletNo.Text.Trim().Substring(0, 2) == InkPalletNo)
                {
                    DataView dv = new DataView();
                    dv = maint.InkCheckPalletExist(txtPalletNo.Text.Trim());

                    DataView dv2 = new DataView();
                    dv2 = maint.InkCheckPalletDelivery(txtPalletNo.Text.Trim());

                    DataView dv3 = new DataView();
                    dv3 = maint.InkCheckPalletExpiration(txtPalletNo.Text.Trim());

                    DataView dv4 = new DataView();
                    dv4 = maint.InkCheckPalletContainer(txtContainerID.Text.Trim(), txtPalletNo.Text.Trim());


                    if (txtContainerID.Text.Trim() == "")
                    {

                        MsgBox1.alert("Please input Container No. first.");
                        txtPalletNo.Text = "";
                        txtContainerID.Focus();
                    }
                    else
                    {


                        if (txtPalletNo.Text.Trim() != "")
                        {
                            if (txtPalletNo.Text.Length == 8)
                            {
                                //Check if Pallet Exist
                                if (dv.Count > 0)
                                {

                                    //Check if Pallet already have input in Carton Information - INK BOTTLE
                                    if (dvCartonInfo.Count > 0)
                                    {

                                        //Check if Ink Pallet reached Delivery limit
                                        if (dv2.Count > 0)
                                        {
                                            MsgBox1.alert("Pallet: '" + txtPalletNo.Text.Trim().ToUpper() + "' is not allowed to Deliver (Lot No. '" + dv2[0]["Lot_No"].ToString() + "')");
                                            txtPalletNo.Focus();
                                        }
                                        else
                                        {
                                            //Check if Ink Pallet reached Expiration limit
                                            if (dv3.Count > 0)
                                            {
                                                MsgBox1.alert("Pallet: '" + txtPalletNo.Text.Trim().ToUpper() + "' is Expired (Lot No. '" + dv3[0]["Lot_No"].ToString() + "')");
                                                txtPalletNo.Focus();
                                            }

                                            else
                                            {
                                                //Allocation Mode - Pallet already Loaded in Container
                                                if (dv4.Count > 0 && rdoAllocate.SelectedValue == "Load")
                                                {
                                                    MsgBox1.alert("Pallet No. '" + txtPalletNo.Text.Trim().ToUpper() + "' has already been loaded in '" + txtContainerID.Text.Trim().ToUpper() + "'.");
                                                }
                                                //Unallocation Mode - Pallet does not exist in the Container
                                                else if (dv4.Count == 0 && rdoAllocate.SelectedValue == "Unload")
                                                {
                                                    MsgBox1.alert("Pallet No. '" + txtPalletNo.Text.Trim().ToUpper() + "' is not loaded in any Container.");
                                                }
                                                else
                                                {
                                                    //Input Pallet Successfully
                                                    lblPalCount.Text = Convert.ToString((Convert.ToInt32(lblPalCount.Text) + 1));

                                                    dr["ID"] = lblPalCount.Text;
                                                    dr["PalletNo"] = txtPalletNo.Text.Trim();

                                                    dtPallet.Rows.Add(dr);
                                                    dtPallet.AcceptChanges();
                                                    txtPalletNo.Text = "";
                                                    txtPalletNo.Focus();
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MsgBox1.alert("Please relate first the Pallet at Carton Information page.");
                                        txtPalletNo.Focus();
                                    }

                                }
                                else
                                {
                                    MsgBox1.alert("Pallet Number: '" + txtPalletNo.Text.Trim().ToUpper() + "' does not exist in this Warehouse.");
                                    txtPalletNo.Focus();
                                }

                            }
                            else
                            {
                                MsgBox1.alert("Please Input Correct Pallet No. Format (8 Chars.)");
                                txtPalletNo.Focus();
                            }

                        }
                        else
                        {
                            MsgBox1.alert("Enter Pallet No!");
                            txtPalletNo.Focus();
                        }
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

