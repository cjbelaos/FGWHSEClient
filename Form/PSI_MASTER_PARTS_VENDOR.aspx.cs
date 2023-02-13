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

using System.Threading;
namespace FGWHSEClient.Form
{
    public partial class PSI_MASTER_PARTS_VENDOR : System.Web.UI.Page
    {
        public PSIDAL dalPSI = new PSIDAL();

        DataTable dtUpload = new DataTable();

        public string strPageSubsystem = "";
        public string strAccessLevel = "";
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
                    strPageSubsystem = "FGWHSE_039";
                    if (!checkAuthority(strPageSubsystem))
                    {
                        Response.Write("<script>");
                        Response.Write("alert('You are not authorized to access the page.');");
                        Response.Write("window.location = 'Default.aspx';");
                        Response.Write("</script>");
                    }
                }
                lblUID.Text = Session["UserID"].ToString();
                if (!this.IsPostBack)
                {

                    lblVendorCode.Text = "";
                    if (Request.QueryString["vendorCode"] != null)
                    {
                        lblVendorCode.Text = Request.QueryString["vendorCode"].ToString();
                    }

                    GETSUPPLIERPARTS(lblVendorCode.Text, txtMaterial.Text.Trim());
                    btnUpdateList.OnClientClick = "showModal();return false;";
                    txtPartCode.Attributes.Add("readonly", "readonly");
                    lblFileTemp.Text = DateTime.Now.ToString("ddMMyyyyhhmmssffffff");
                    //chkDelCat.Attributes.Add("OnClick", "chkBox();");

                }
                else
                {
                    dtUpload = (DataTable)ViewState["dtUpload"];
                }

                ViewState["dtUpload"] = dtUpload;


                if (Request.Form["deleteConfirm"] != null)
                {

                    if (Request.Form["deleteConfirm"].ToString().Equals("1"))
                    {
                        dalPSI.PSI_DELETE_SUPPLIER_PARTS_DETAILS("R101", lblMaterialDelete.Text, lblVendorCode.Text);
                        string strURL = "PSI_MASTER_PARTS_VENDOR.aspx?vendorCode=" + lblVendorCode.Text;
                        Response.Write("<script>");
                        Response.Write("alert('Successfully updated!');");
                        Response.Write("window.location = '../form/" + strURL + "';");
                        Response.Write("</script>");
                    }

                    Request.Form["deleteConfirm"] = null;
                }

                if (Request.Form["uploadConfirm"] != null)
                {
                    if (Request.Form["uploadConfirm"].ToString().Equals("1"))
                    {
                        uploadFileToDB(dtUpload);
                        string strURL = "PSI_MASTER_PARTS_VENDOR.aspx?vendorCode=" + lblVendorCode.Text;
                        Response.Write("<script>");
                        Response.Write("alert('Successfully updated!');");
                        Response.Write("window.location = '../form/" + strURL + "';");
                        Response.Write("</script>");
                    }

                    Request.Form["uploadConfirm"] = null;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>");
                Response.Write("alert('" + ex.Message.ToString() + "');");
                Response.Write("</script>");
            }

        }



        public void GETSUPPLIERPARTS(string strSUPPLIER, string strMATERIAL)
        {
            try
            {
                
                lblVendorName.Text = "";

                DataTable dt = dalPSI.PSI_GET_SUPPLIER_PARTS_DETAILS(strSUPPLIER, strMATERIAL).Tables[0];

                DataView dv = dt.DefaultView;
                if (dv.Count > 0)
                {
                    lblVendorName.Text = dv[0]["SUPPLIERNAME"].ToString();
                }

                grdParts.DataSource = dt;
                grdParts.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>");
                Response.Write("alert('" + ex.Message.ToString() + "');");
                Response.Write("</script>");
            }
        }

        protected void grdParts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdParts.PageIndex = e.NewPageIndex;
            GETSUPPLIERPARTS(lblVendorCode.Text, txtMaterial.Text.Trim());
        }



        public void GETSUPPLIERPARTSDETAILS(string strSUPPLIER, string strPARTCODE)
        {
            try
            {
                DataTable dt = dalPSI.PSI_GET_SUPPLIER_PARTS_DETAILS(strSUPPLIER, strPARTCODE).Tables[0];

                DataView dv = dt.DefaultView;
                if (dv.Count > 0)
                {
                    lblPVendorCode.Text = lblVendorCode.Text;
                    lblPVendorName.Text = dv[0]["SUPPLIERNAME"].ToString();
                    txtPartCode.Text = dv[0]["MATERIALCODE"].ToString();
                    txtDescription.Text = dv[0]["DESCRIPTION"].ToString();
                    txtDOS.Text = dv[0]["DOSLEVEL"].ToString();
                    txtPArtSize.Text = dv[0]["PARTSIZE"].ToString();
                    txtSPQ.Text = dv[0]["SPQ"].ToString();
                    txtModel.Text = dv[0]["MODELCODE"].ToString();
                    txtPCat.Text = dv[0]["CATEGORY"].ToString();
                }

              
            }
            catch (Exception ex)
            {
                Response.Write("<script>");
                Response.Write("alert('" + ex.Message.ToString() + "');");
                Response.Write("</script>");
            }
        }



        protected void lnk_Click(object sender, EventArgs e)
        {
            int rowIndx;
            var rowTrigger = (Control)sender;
            GridViewRow rowUpdate = (GridViewRow)rowTrigger.NamingContainer;
            rowIndx = rowUpdate.RowIndex;
            Label lblMaterial = (Label)grdParts.Rows[rowIndx].FindControl("lblMATERIALCODE");
            GETSUPPLIERPARTSDETAILS(lblVendorCode.Text, lblMaterial.Text);


            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showModal()", true);
        }

        protected void lnk_delete(object sender, EventArgs e)
        {

            int rowIndx;
            var rowTrigger = (Control)sender;
            GridViewRow rowUpdate = (GridViewRow)rowTrigger.NamingContainer;
            rowIndx = rowUpdate.RowIndex;
            Label lblMaterial = (Label)grdParts.Rows[rowIndx].FindControl("lblMATERIALCODE");
            lblMaterialDelete.Text = lblMaterial.Text;

            msgBox.confirm("Are you sure you want to "+ lblMaterial.Text + " details permanently?", "deleteConfirm");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GETSUPPLIERPARTS(lblVendorCode.Text, txtMaterial.Text.Trim());
        }

        protected void grdParts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //,,,,, , ,
                //Label lblvendorcode = (Label)e.Row.FindControl("lnkUPDATE");
                //Label lblvendname = (Label)e.Row.FindControl("lblSUPPLIERNAME");
                Label lbldelcat = (Label)e.Row.FindControl("lblDELIVERYCATEGORY");
                Label lblpartcode = (Label)e.Row.FindControl("lblMATERIALCODE");
                Label lblpartdesc = (Label)e.Row.FindControl("lblDESCRIPTION");
                Label lblspq = (Label)e.Row.FindControl("lblSPQ");
                Label lblpartsize = (Label)e.Row.FindControl("lblPARTSIZE");
                Label lblDOS = (Label)e.Row.FindControl("lblDOSLEVEL");
                Label lblMODEL = (Label)e.Row.FindControl("lblMODEL");
                Label lblCATEGORY = (Label)e.Row.FindControl("lblCATEGORY");

                LinkButton lnk = (LinkButton)e.Row.FindControl("lnkUPDATE");



                

                // vendorcode,vendorname,delcat,partcode,partdesc, spq, partsize,DOS

                string strValues = HttpUtility.HtmlDecode(lblVendorCode.Text) + "','"
                                                 + HttpUtility.HtmlDecode(lblVendorName.Text) + "','"
                                                 + HttpUtility.HtmlDecode(lbldelcat.Text) + "','"
                                                 + HttpUtility.HtmlDecode(lblpartcode.Text) + "','"
                                                 + HttpUtility.HtmlDecode(lblpartdesc.Text) + "','"
                                                 + HttpUtility.HtmlDecode(lblspq.Text) + "','"
                                                 + HttpUtility.HtmlDecode(lblpartsize.Text) + "','"
                                                 + HttpUtility.HtmlDecode(lblDOS.Text) + "','"
                                                 + HttpUtility.HtmlDecode(lblMODEL.Text) + "','"
                                                 + HttpUtility.HtmlDecode(lblCATEGORY.Text);
                
                lnk.OnClientClick = HttpUtility.HtmlDecode("showModal('" + strValues +  "');return false;");
              
            }
        }

        protected void btnUpdateList_Click(object sender, EventArgs e)
        {
            //if(lblRow.Text != "")
            //{
            //    int grdRow = Convert.ToInt32(lblRow.Text);
            //}

            //lblRow.Text = "";

        }

        public void AddDetails()
        {
            dalPSI.PSI_ADD_BASIS_MASTER_DETAILS
                (
                "R101",
                txtPartCode.Text,
                lblVendorCode.Text,
                txtSPQ.Text,
                txtPArtSize.Text,
                "",
                txtDOS.Text,
                txtPCat.Text.Trim(),
                txtModel.Text.Trim(),
                rdb.SelectedValue.ToString(),
                "UID");

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "dismissModal()", true);
                AddDetails();
                string strURL = "PSI_MASTER_PARTS_VENDOR.aspx?vendorCode=" + lblVendorCode.Text;
                Response.Write("<script>");
                Response.Write("alert('Successfully updated!');");
                Response.Write("window.location = '../form/" + strURL + "';");
                Response.Write("</script>");




                //GETSUPPLIERPARTS(lblVendorCode.Text, txtMaterial.Text.Trim());
                
             

            }
            catch (Exception ex)
            {
                Response.Write("<script>");
                Response.Write("alert('" + ex.Message.ToString() + "');");
                Response.Write("</script>");
            }
        }

        public DataSet ImportExcel(FileUpload fup)
        {

            string FileName = fup.FileName;
            string strDir = "";
            DataSet output = new DataSet();
            string strConn;
            try
            {
                //if (FileName.Substring(FileName.LastIndexOf('.')).ToLower() == ".xlsx")
                //    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";" + "Extended Properties='Excel 12.0 Xml;HDR=NO;'";
                //else
                //    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";" + "Extended Properties='Excel 12.0 Macro;HDR=NO;'";


                if (FileName.Substring(FileName.LastIndexOf('.')).ToLower() == ".xls")
                {
                    strDir = createTempFilenName(fup);
                    FileName = strDir + "/" + FileName;
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";" + "Extended Properties='Excel 12.0 Macro;HDR=NO;'";


                }
                else
                {
                    msgBox.alert("Excel file extension should be .xls!");

                    return output; ;
                }



                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    DataTable schemaTable = conn.GetOleDbSchemaTable(
                        OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                    foreach (DataRow schemaRow in schemaTable.Rows)
                    {
                        string sheet = schemaRow["TABLE_NAME"].ToString();

                        if (!sheet.EndsWith("_"))
                        {
                            try
                            {
                                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", conn);
                                cmd.CommandType = CommandType.Text;

                                DataTable outputTable = new DataTable(sheet);
                                output.Tables.Add(outputTable);
                                new OleDbDataAdapter(cmd).Fill(outputTable);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message + string.Format("Sheet:{0}.File:F{1}", sheet, FileName), ex);
                            }
                        }
                    }
                }

                if (Directory.Exists(strDir))
                {
                    FixAppDomainRestartWhenTouchingFiles();
                    Directory.Delete(strDir, true);
                }

                lblFileTemp.Text = DateTime.Now.ToString("ddMMyyyyhhmmssffffff");
            }
            catch (Exception ex)
            {
                if (Directory.Exists(strDir))
                {
                    FixAppDomainRestartWhenTouchingFiles();
                    Directory.Delete(strDir, true);
                }
                throw ex;
            }

            return output;
        }

        private void FixAppDomainRestartWhenTouchingFiles()
        {

            System.Reflection.PropertyInfo p = typeof(HttpRuntime).GetProperty("FileChangesMonitor", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

            object o = p.GetValue(null, null);

            System.Reflection.FieldInfo f = o.GetType().GetField("_dirMonSubdirs", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.IgnoreCase);

            object monitor = f.GetValue(o);

            System.Reflection.MethodInfo m = monitor.GetType().GetMethod("StopMonitoring", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            m.Invoke(monitor, new object[0]);

        }

        protected string createTempFilenName(FileUpload FU)
        {

            String strFileName = FU.FileName.ToString();
            //int position = strFileName.LastIndexOf(".");
            //strFileName = strFileName.Remove(position, strFileName.Length - position);
            //strFileName = strFileName + "_" + DateTime.Now.ToString("yyMMddHHmmssfff") + Path.GetExtension(FU.FileName);
            createTempFolder(lblFileTemp.Text);
            //String FilePATH = Path.Combine(Server.MapPath(createTempFolder(lblFileTemp.Text)), strFileName);
            String FilePATH = Server.MapPath("~/TEMP/" + lblFileTemp.Text + "/" + strFileName);
            string strDir = Server.MapPath("~/TEMP/" + lblFileTemp.Text + "/");
            FU.SaveAs(FilePATH);
            FixAppDomainRestartWhenTouchingFiles();
            //return strFileName;
            return strDir;
        }

        protected string createTempFolder(String FolderTemp)
        {

            string des = "~/TEMP/";
            string pth = Server.MapPath(Path.Combine(des, FolderTemp));
            Directory.CreateDirectory(pth);

            return pth;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile == true)
            {
                ViewState["dtUpload"] = ImportExcel(FileUpload1).Tables[0];
                msgBox.confirm("All parts in the will be added/updated. Do you wish to continue?", "uploadConfirm");
            }
        }

        public void uploadFileToDB(DataTable DT)
        {
             
            DataView dv = DT.DefaultView;
            string DelType = "", VendorCode = "", Partcode = "", PRODUCTCATEGORY="", MODEL="", SPQ = "", PartSize = "", DOS = "";

            for (int x = 1; x < dv.Count; x++)
            {
                if (dv[x]["F1"].ToString() == "1")
                {
                    DelType = "1";
                }
                else if (dv[x]["F2"].ToString() == "1")
                {
                    DelType = "0";
                }
                else
                {
                    DelType = "";
                }

                VendorCode = dv[x]["F3"].ToString().Replace("\r","").Replace("\n","");
                Partcode = dv[x]["F4"].ToString().Replace("\r", "").Replace("\n", "");
                PRODUCTCATEGORY = dv[x]["F5"].ToString().Replace("\r", "").Replace("\n", "");
                MODEL = dv[x]["F6"].ToString().Replace("\r", "").Replace("\n", "");
                SPQ = dv[x]["F7"].ToString().Replace("\r", "").Replace("\n", "");
                PartSize = dv[x]["F8"].ToString().Replace("\r", "").Replace("\n", "");
                DOS = dv[x]["F9"].ToString().Replace("\r", "").Replace("\n", "");

                dalPSI.PSI_ADD_BASIS_MASTER_DETAILS
                (
                "R101",
                Partcode,
                VendorCode,
                SPQ,
                PartSize,
                "",
                DOS,
                PRODUCTCATEGORY,
                MODEL,
                DelType,
                lblUID.Text);

                DelType = ""; VendorCode = ""; Partcode = ""; SPQ = ""; PartSize = ""; DOS = "";
            }
        }

        protected void lnkTemplate_Click(object sender, EventArgs e)
        {
            Response.Redirect("../TEMPLATE/TEMPLATE.xls");
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
                //Response.Write("<script>");
                //Response.Write("alert('"+ ex.Message.ToString() +"');");
                //Response.Write("</script>");

                isValid = false;
                return isValid;
            }
        }
    }
}