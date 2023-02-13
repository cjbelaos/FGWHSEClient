using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.eppi.utils;
using System.Xml.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using FGWHSEClient.LdapService;
using FGWHSEClient.LogInService;

namespace FGWHSEClient.Form
{
    public partial class CartonInformation : System.Web.UI.Page
    {
        protected DataTable dtParCodeLotNoQty = new DataTable();
        Maintenance maint = new Maintenance();
        public string strPageSubsystem = "";
        public string strAccessLevel = "";
        public string InkPalletNo = ConfigurationManager.AppSettings["InkPalletNo"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["UserName"] == null)
                {
                    Response.Write("<script>");
                    Response.Write("alert('Session expired! Please log in again.');");
                    Response.Write("window.location = '../Login.aspx';");
                    Response.Write("</script>");
                }
                else
                {
                    strPageSubsystem = "FGWHSE_011";
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
                    MakePalletDataTable(dtParCodeLotNoQty);
                }
                else
                {
                    dtParCodeLotNoQty = (DataTable)ViewState["dtParCodeLotNoQty"];
                }
                ViewState["dtParCodeLotNoQty"] = dtParCodeLotNoQty;



                if(Request.Form["saveConfirm"] != null)
				{
					if(Request.Form["saveConfirm"].ToString().Equals("1")) 
					{
                        saveScanned();
					}
				}

                if (Request.Form["deleteConfirm"] != null)
                {
                    if (Request.Form["deleteConfirm"].ToString().Equals("1"))
                    {
                        deleteScanned();
                    }
                }


                if (Request.Form["clearConfirm"] != null)
                {
                    if (Request.Form["clearConfirm"].ToString().Equals("1"))
                    {
                        clearScanned();
                    }
                }


            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }

        }


        private void MakePalletDataTable(DataTable Ldt)
        {
            try
            {
                Ldt.Columns.Add("ID", typeof(String));
                Ldt.Columns.Add("ParCodeLotNoQty", typeof(String));

            }
            catch (Exception ex)
            {
                string x = (ex.ToString());
            }

        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            MsgBox1.confirm("Save scanned carton information?", "saveConfirm");
        }

        public void saveScanned()
        {
            try
            {
                if (txtPalletNo.Text.Length < 8)
                {

                    MsgBox1.alert("Pallet number must contain 8 characters!");
                    txtPalletNo.Focus();
                    return;
                }


                string strExtract = "", strPartCode = "", strLotNo = "", strQty = "";

                //strExtract = txtPartCode.Text.Trim();
                int intNavigator = 0;


                if (rdoReg.SelectedValue == "0")
                {
                    if (dtParCodeLotNoQty.DefaultView.Count <= 0)
                    {
                        MsgBox1.alert("There are no scanned details!");
                        return;
                    }
                }
                DataView dvCheckPallet = new DataView();

                dvCheckPallet = maint.CHECK_PALLET_IF_ALREADY_EXISTS(txtPalletNo.Text.Trim()).Tables[0].DefaultView;
                if (dvCheckPallet.Count > 0)
                {
                    if (rdoReg.SelectedValue == "0")
                    {
                        MsgBox1.alert("Pallet No " + dvCheckPallet[0]["PALLETNO"].ToString() + " already exists in warehouse " + dvCheckPallet[0]["WHID"].ToString());
                        return;
                    }
                }
                else
                {
                    if (rdoReg.SelectedValue == "1")
                    {
                        MsgBox1.alert("Pallet No " + txtPalletNo.Text.Trim() + " does not exists!");
                        return;
                    }
                }


                if (rdoReg.SelectedValue == "0")
                {

                    DataView dv = new DataView();
                    dv = dtParCodeLotNoQty.DefaultView;
                    for (int x = 0; x < dtParCodeLotNoQty.DefaultView.Count; x++)
                    {
                        strExtract = "";
                        strPartCode = "";
                        strLotNo = "";
                        strQty = "";
                        intNavigator = 0;
                        strExtract = dv[x][1].ToString();

                        for (int L = 0; L < strExtract.Length; L++)
                        {
                            if (intNavigator == 0)
                            {
                                if (strExtract.Substring(L, 1) != ":")
                                {
                                    strPartCode = strPartCode + strExtract.Substring(L, 1);
                                }
                                else
                                {
                                    intNavigator = 1;
                                }
                            }

                            if (intNavigator == 1)
                            {
                                if (strExtract.Substring(L, 1) != ":")
                                {
                                    strLotNo = strLotNo + strExtract.Substring(L, 1);
                                }
                                else
                                {
                                    if (strLotNo != "")
                                    {
                                        intNavigator = 2;
                                    }
                                }
                            }


                            if (intNavigator == 2)
                            {
                                if (strExtract.Substring(L, 1) != ":")
                                {
                                    strQty = strQty + strExtract.Substring(L, 1);
                                }
                                else
                                {
                                    if (strQty != "")
                                    {
                                        intNavigator = 0;
                                    }
                                }
                            }

                        }


                        maint.ADD_CARTON_INFORMATION(txtPalletNo.Text.Trim(), strPartCode, strLotNo, strQty, Session["UserID"].ToString());
                        //strExtract = strExtract; strPartCode = strPartCode; strLotNo = strLotNo; strQty = strQty;
                    }

                    MsgBox1.alert("Successfully Saved.");
                    dtParCodeLotNoQty.Clear();
                    dtParCodeLotNoQty.AcceptChanges();
                    txtPalletNo.Focus();


                }
                else
                {
                    maint.DELETE_CARTON_INFORMATION(txtPalletNo.Text.Trim());
                    MsgBox1.alert("Successfully Saved.");
                    dtParCodeLotNoQty.Clear();
                    dtParCodeLotNoQty.AcceptChanges();
                    txtPalletNo.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);
            }
        }

        protected void txtLotNoAndQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string PcodeLotnoQty = "";
                string strLotNoAndQty = "";

                strLotNoAndQty = getLotNoAndQty(txtLotNoAndQty.Text.Trim());
                if (dtParCodeLotNoQty.DefaultView.Count > 120)
                {
                    MsgBox1.alert("Maximum scan is 120!");
                    return;
                }
                if (txtPartCode.Text.Trim() == "" || txtLotNoAndQty.Text.Trim() == "")
                {
                    MsgBox1.alert("Please complete the details (Partcode/LotNo and Qty)!");
                    return;
                }

                if (strLotNoAndQty == ":")
                {
                    MsgBox1.alert("Enter valid LotNo and Qty!");

                    return;
                }
                PcodeLotnoQty = txtPartCode.Text.Trim() + ":" + strLotNoAndQty;


                DataRow dr = dtParCodeLotNoQty.NewRow();

                DataRow[] foundPallet = dtParCodeLotNoQty.Select("ParCodeLotNoQty = '" + PcodeLotnoQty + "'");
                //if (foundPallet.Length != 0)
                //{
                //    MsgBox1.alert("Duplicate details has been detected!");
                //    return;
                //}



                if (txtPalletNo.Text.Trim() != "")
                {
                    lblPalCount.Text = Convert.ToString((Convert.ToInt32(lblPalCount.Text) + 1));



                    dr["ID"] = lblPalCount.Text;
                    dr["ParCodeLotNoQty"] = PcodeLotnoQty;

                    dtParCodeLotNoQty.Rows.Add(dr);
                    dtParCodeLotNoQty.AcceptChanges();
                    txtPartCode.Text = "";
                    txtLotNoAndQty.Text = "";
                    txtPartCode.Focus();
                    lblScanCount.Text = dtParCodeLotNoQty.DefaultView.Count.ToString();

                }
                else
                {
                    MsgBox1.alert("Enter Pallet No!");
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);
            }


        }

        public string getLotNoAndQty(string LotNoQty)
        {

            string LotNoAndQty = "";

            int textLength = 0, getLot = 0, getQty = 0, getQtyBegin = 0;
            string strLotNo = LotNoQty.Trim();
            textLength = strLotNo.Length;

            string strLotno = "", strQty = "", strLot = "";

            if (LotNoQty.Substring(0, 1) == "$")
            {
                for (int x = 0; x < textLength; x++)
                {
                    strLot = strLotNo.ToString().Substring(x, 1);



                    if (strLot == "%")
                    {
                        getLot = 0;
                        if (textLength >= x + 3)
                        {
                            if (strLotNo.ToString().Substring(x, 3) == "%++")
                            {
                                getQtyBegin = x + 3;
                            }

                        }
                    }

                    if (getQtyBegin != 0 && getQtyBegin == x)
                    {
                        getQty = 1;
                    }

                    if (getQty == 1 && strLot != "/" && strLot != "%")
                    {

                        strQty = strQty + strLot;
                    }
                    else
                    {
                        getQty = 0;
                    }

                    if (getLot == 1)
                    {
                        strLotno = strLotno + strLot;
                    }


                    if (strLot == "$")
                    {
                        getLot = 1;
                    }

                    LotNoAndQty = strLotno + ":" + strQty;


                }
            }
            else
            {

                getLot = 1;

                for (int x = 0; x < textLength; x++)
                {
                    strLot = strLotNo.ToString().Substring(x, 1);
                    if (getLot == 1)
                    {
                        if (strLotNo.ToString().Substring(x, 1) != "%" && strLotNo.ToString().Substring(x, 1) != "%")
                        {
                            strLotno = strLotno + strLot;
                        }
                        else
                        {
                            getLot = 2;
                        }
                    }

                    if (getLot == 2)
                    {
                        if (strLotNo.ToString().Substring(x, 1) != "+" && strLotNo.ToString().Substring(x, 1) != "%")
                        {
                            strQty = strQty + strLotNo.ToString().Substring(x, 1);
                        }
                    }

                    LotNoAndQty = strLotno + ":" + strQty;


                }

            }

            if (strLotno == "" || strQty == "" || strQty == "0" || strLotno.Length != 6)
            {
                LotNoAndQty = ":";
            }

            return LotNoAndQty;


        }

        protected void txtPalletNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPalletNo.Text.Trim().Substring(0, 2) == InkPalletNo)
                {
                    if (txtPalletNo.Text.Trim() != "")
                    {
                        if (txtPalletNo.Text.Length < 8)
                        {

                            MsgBox1.alert("Pallet number must contain 8 characters!");
                            txtPalletNo.Text = "";
                            txtPalletNo.Focus();
                        }
                        else
                        {
                            txtPartCode.Focus();
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

        protected void txtPartCode_TextChanged(object sender, EventArgs e)
        {
            try
            {


                if (txtPartCode.Text.Trim() != "")
                {
                    if (lblScanCount.Text != "0")
                    {
                        if (txtPartcodeCheck.Text.Trim() != txtPartCode.Text.Trim())
                        {
                            MsgBox1.alert("Part Code is not same with last scanned partcode!");
                            txtPartCode.Text = "";
                            txtPartCode.Focus();
                            return;
                        }
                    }
                    if (txtPartCode.Text.Length != 11 && txtPartCode.Text.Length != 9)
                    {

                        MsgBox1.alert("Product code must contain 9 or 11 characters!");
                        txtPartCode.Text = "";
                        txtPartCode.Focus();

                    }
                    else
                    {
                        DataView dv = new DataView();
                        dv = maint.CHECK_IF_EXISTS_M_PART_LIMIT_CONTROL(txtPartCode.Text.Trim()).Tables[0].DefaultView;
                        if (dv.Count > 0)
                        {
                            txtLotNoAndQty.Focus();
                        }
                        else
                        {

                            MsgBox1.alert("Product code does not exists in Item Limit Control Master");
                            txtPartCode.Text = "";
                            txtPartCode.Focus();
                        }


                        if (lblScanCount.Text == "0")
                        {
                            txtPartcodeCheck.Text = txtPartCode.Text.Trim();
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

        protected void rdoReg_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (rdoReg.SelectedValue.ToString() == "0")
                {
                    //txtPalletNo.Enabled = true;
                    txtPartCode.Enabled = true;
                    txtLotNoAndQty.Enabled = true;


                }
                else
                {
                    //txtPalletNo.Enabled = false;
                    txtPartCode.Enabled = false;
                    txtLotNoAndQty.Enabled = false;
                    txtPartCode.Text = "";
                    txtLotNoAndQty.Text = "";

                }
                txtPalletNo.Focus();
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

            MsgBox1.confirm("Clear scanned carton information?", "clearConfirm");
            
        }
        public void clearScanned()
        {
            try
            {
                txtPalletNo.Text = "";
                txtPartCode.Text = "";
                txtLotNoAndQty.Text = "";
                rdoReg.SelectedValue = "0";
                txtPalletNo.Focus();
                dtParCodeLotNoQty.Clear();
                dtParCodeLotNoQty.AcceptChanges();
                lblScanCount.Text = dtParCodeLotNoQty.DefaultView.Count.ToString();
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            MsgBox1.confirm("Delete scanned carton information?", "deleteConfirm");
        }


        public void deleteScanned()
        {
            try
            {
                if (Convert.ToInt32(lblPalCount.Text) == 0)
                {
                    MsgBox1.alert("There is no scanned item to delete!");
                }
                else
                {

                    doDeleteFile(lblPalCount.Text, dtParCodeLotNoQty);
                    lblPalCount.Text = Convert.ToString((Convert.ToInt32(lblPalCount.Text) - 1));

                    lblScanCount.Text = dtParCodeLotNoQty.DefaultView.Count.ToString();
                    if (lblScanCount.Text == "0")
                    {
                        txtPartcodeCheck.Text = "";
                    }
                    MsgBox1.alert("Last Scanned item has been removed to list!");
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);
            }
        }

        public void doDeleteFile(string strID, DataTable Ldt)
        {
            try
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
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);
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

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPalletNo.Text.Trim() == "")
                {
                    MsgBox1.alert("Please enter pallet no!");
                    return;
                }
                ExportToExcel("SCANNED_CARTON_");
            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message.ToString());
            }
        }


        public void ExportToExcel(string fname)
        {
            int exportDataCount = 0;
            string filename = fname + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            //msgBox.alert(filename);
            //Turn off the view stateV 225 55
            this.EnableViewState = false;
            //Remove the charset from the Content-Type header
            Response.Charset = String.Empty;

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
            Response.Charset = "";
            //grdTasks.AllowPaging = false;

            exportDataCount = this.GET_SCANNED_CARTON_INFORMATION(txtPalletNo.Text.Trim());
        

            //if (exportDataCount == 0)
            //{
            //    Response.Write("<script>");
            //    Response.Write("alert('No data found!');");
            //    Response.Write("</script>");

            //    return;
            //}
            // If you want the option to open the Excel file without saving then
            // comment out the line below
            // Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            grdExport.RenderControl(htmlWrite);

            //Append CSS fileC:\inetpub\wwwroot\FGWHSE\FGWHSEClient\App_Themes\Stylesheet\Main.css

           
            System.IO.FileInfo fi = new System.IO.FileInfo(Server.MapPath("../App_Themes/Stylesheet/Main.css"));
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.IO.StreamReader sr = fi.OpenText();
            while (sr.Peek() >= 0)
            {
                sb.Append(sr.ReadLine());
            }
            sr.Close();

            Response.Write("<html><head><style type='text/css'>" + sb.ToString() + "</style><head>" + stringWrite.ToString() + "</html>");

            stringWrite = null;
            htmlWrite = null;

            // Response.Write(stringWrite.ToString());

            Response.Flush();
            Response.End();
        }



        public int GET_SCANNED_CARTON_INFORMATION(string Pallet_No)
        {
            DataTable dt = new DataTable();
            DataView dv = new DataView();


            dv = maint.GET_SCANNED_CARTON_INFORMATION(Pallet_No).Tables[0].DefaultView;

            dt = dv.Table;
            grdExport.DataSource = dt;
            grdExport.DataBind();

            grdExport.DataSource = dt;
            grdExport.DataBind();

            return dv.Count;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }
        
    }
}
