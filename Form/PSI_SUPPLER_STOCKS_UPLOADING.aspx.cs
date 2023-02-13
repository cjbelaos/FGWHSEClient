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
    public partial class PSI_SUPPLER_STOCKS_UPLOADING : System.Web.UI.Page
    {
        public string strPageSubsystem = "";
        public string strAccessLevel = "";
        public PSIDAL dalPSI = new PSIDAL();

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

                }

                if (!this.IsPostBack)
                {
                    txtPIC.Attributes.Add("readonly", "readonly");
                    lblUID.Text = Session["UserID"].ToString();
                    if (Request.QueryString["vendorCode"] != null)
                    {
                        ddSupplier.SelectedValue = Request.QueryString["vendorCode"].ToString();

                    }

                    grdParts.DataSource = dalPSI.PSI_GET_SUPPLIER_STOCKS(txtMATERIALCODE.Text.Trim(), ddSupplier.SelectedValue.ToString()).Tables[0];
                    grdParts.DataBind();

                    strPageSubsystem = "FGWHSE_048";
                    if (!checkAuthority(strPageSubsystem))
                    {
                        Response.Write("<script>");
                        Response.Write("alert('You are not authorized to access the page.');");
                        Response.Write("window.location = 'Default.aspx';");
                        Response.Write("</script>");
                    }

                    GET_SUPPLIER(ddSupplier);
                    GET_PROBLEMCATEGORY(ddProblemCat);
                    lblFileTemp.Text = DateTime.Now.ToString("ddMMyyyyhhmmssffffff");
                }


                if (Request.Form["uploadConfirm"] != null)
                {
                    if (Request.Form["uploadConfirm"].ToString().Equals("1"))
                    {
                        DataTable dtUpload = (DataTable)ViewState["dtUpload"];
                        uploadFileToDB(dtUpload);

                        string strURL = "PSI_SUPPLER_STOCKS_UPLOADING.aspx?vendorCode=" + ddSupplier.SelectedValue.ToString();
                        Response.Write("<script>");
                        Response.Write("alert('Successfully updated!');");
                        Response.Write("window.location = '../form/" + strURL + "';");
                        Response.Write("</script>");

                    }

                    //Request.Form["uploadConfirm"] = null;
                }

                if (Request.Form["deleteConfirm"] != null)
                {

                    if (Request.Form["deleteConfirm"].ToString().Equals("1"))
                    {
                        dalPSI.PSI_DELETE_SUPPLIER_STOCKS(lblMaterialHidden.Text, lblSUPPLIERIDHidden.Text);
                        string strURL = "PSI_SUPPLER_STOCKS_UPLOADING.aspx?vendorCode=" + ddSupplier.SelectedValue.ToString();
                        Response.Write("<script>");
                        Response.Write("alert('Successfully Deleted!');");
                        Response.Write("window.location = '../form/" + strURL + "';");
                        Response.Write("</script>");
                    }

                    //Request.Form["deleteConfirm"] = null;
                }


                if (Request.Form["deleteAllConfirm"] != null)
                {

                    if (Request.Form["deleteAllConfirm"].ToString().Equals("1"))
                    {
                        dalPSI.PSI_DELETE_SUPPLIER_STOCKS("", ddSupplier.SelectedValue.ToString());
                        string strURL = "PSI_SUPPLER_STOCKS_UPLOADING.aspx?vendorCode=" + ddSupplier.SelectedValue.ToString();
                        Response.Write("<script>");
                        Response.Write("alert('Successfully Deleted!');");
                        Response.Write("window.location = '../form/" + strURL + "';");
                        Response.Write("</script>");
                    }

                    //Request.Form["deleteConfirm"] = null;
                }
            }
            catch (Exception ex)
            {
                msgBox.alert(ex.Message.ToString());
            }
        }


        public void uploadFileToDB(DataTable DT)
        {
            try
            {

                DataView dv = DT.DefaultView;
                string SupplierID = "", ItemCode = "", QTY = "", PROBLEMCATEGORY = "", REASON = "", PIC = "", COUNTERMEASURE = "";
                ;

                for (int x = 1; x < dv.Count; x++)
                {


                    SupplierID = dv[x]["F1"].ToString().Replace("\r", "").Replace("\n", "");
                    ItemCode = dv[x]["F2"].ToString().Replace("\r", "").Replace("\n", "");
                    QTY = dv[x]["F3"].ToString().Replace("\r", "").Replace("\n", "");
                    PROBLEMCATEGORY = dv[x]["F4"].ToString().Replace("\r", "").Replace("\n", "");
                    REASON = dv[x]["F5"].ToString().Replace("\r", "").Replace("\n", "");
                    PIC = dv[x]["F6"].ToString().Replace("\r", "").Replace("\n", "");
                    COUNTERMEASURE = dv[x]["F7"].ToString().Replace("\r", "").Replace("\n", "");
                    if (SupplierID.Trim() != "")
                    {
                        dalPSI.PSI_UPLOAD_MANUAL_SUPPLIER_STOCKS
                        (
                        SupplierID,
                        ItemCode,
                        QTY,
                        PROBLEMCATEGORY,
                        REASON,
                        PIC,
                        COUNTERMEASURE,
                        lblUID.Text);
                    }

                    SupplierID = ""; ItemCode = ""; QTY = "";
                }
            }
            catch (Exception ex)
            {
                string strx = ex.Message.ToString();

                Response.Write("<script>");
                Response.Write("alert('ERROR Dectected! Some data may not be uploaded!File might contain invalid text format or invalid character length.');");
                Response.Write("</script>");
            }

        }

        public void GET_SUPPLIER(DropDownList dd)
        {

            DataTable dt = new DataTable();
            DataTable dtList = new DataTable();

            dt = dalPSI.PSI_GET_SUPPLIERS_WITH_STOCKS().Tables[0];

            dtList.Columns.Add("SupplierName", typeof(string));
            dtList.Columns.Add("SupplierID", typeof(string));
            dtList.Rows.Add("ALL", "");

            foreach (DataRow row in dt.Rows)
            {

                String AreaId = row[0].ToString();
                String AreaName = row[1].ToString();

                dtList.Rows.Add(AreaName, AreaId);
            }

            dd.DataSource = dtList;
            dd.DataTextField = "SupplierName";
            dd.DataValueField = "SupplierID";
            dd.DataBind();
        }

        public void GET_PROBLEMCATEGORY(DropDownList dd)
        {

            DataTable dt = new DataTable();
            DataTable dtList = new DataTable();

            dt = dalPSI.PSI_GET_PROBLEM_CATEGORY().Tables[0];

            dtList.Columns.Add("PROB_CAT_NAME", typeof(string));
            dtList.Columns.Add("PIC", typeof(string));
            dtList.Rows.Add("PLEASE SELECT", "");

            foreach (DataRow row in dt.Rows)
            {

                String PROB = row["PROB_CAT_NAME"].ToString();
                String PIC = row["PIC"].ToString();

                dtList.Rows.Add(PROB, PIC);
            }

            dd.DataSource = dtList;
            dd.DataTextField = "PROB_CAT_NAME";
            dd.DataValueField = "PIC";
            dd.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                grdParts.DataSource = dalPSI.PSI_GET_SUPPLIER_STOCKS(txtMATERIALCODE.Text.Trim(), ddSupplier.SelectedValue.ToString()).Tables[0];
                grdParts.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>");
                Response.Write("alert('" + ex.Message.ToString() + "');");
                Response.Write("</script>");
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile == true)
            {
                DataTable dtU = ImportExcel(FileUpload1).Tables[0];
                ViewState["dtUpload"] = dtU;
                msgBox.confirm("All parts in the will be added/updated. Do you wish to continue?", "uploadConfirm");
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
        private void FixAppDomainRestartWhenTouchingFiles()
        {

            System.Reflection.PropertyInfo p = typeof(HttpRuntime).GetProperty("FileChangesMonitor", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

            object o = p.GetValue(null, null);

            System.Reflection.FieldInfo f = o.GetType().GetField("_dirMonSubdirs", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.IgnoreCase);

            object monitor = f.GetValue(o);

            System.Reflection.MethodInfo m = monitor.GetType().GetMethod("StopMonitoring", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            m.Invoke(monitor, new object[0]);

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

        protected void lnkTemplate_Click(object sender, EventArgs e)
        {
            Response.Redirect("../TEMPLATE/TEMPLATE_SUPPLIER_STOCKS.xls");
        }


        protected void lnk_delete(object sender, EventArgs e)
        {

            int rowIndx;
            var rowTrigger = (Control)sender;
            GridViewRow rowUpdate = (GridViewRow)rowTrigger.NamingContainer;
            rowIndx = rowUpdate.RowIndex;
            Label lblMaterial = (Label)grdParts.Rows[rowIndx].FindControl("lblMATERIALCODE");
            Label lblSUPPLIERID = (Label)grdParts.Rows[rowIndx].FindControl("lblSUPPLIERID");
            lblMaterialHidden.Text = lblMaterial.Text;
            lblSUPPLIERIDHidden.Text = lblSUPPLIERID.Text; 
            msgBox.confirm("Are you sure you want to delete " + lblMaterial.Text + " details permanently?", "deleteConfirm");
        }

        protected void lnk_update(object sender, EventArgs e)
        {

            int rowIndx;
            var rowTrigger = (Control)sender;
            GridViewRow rowUpdate = (GridViewRow)rowTrigger.NamingContainer;
            rowIndx = rowUpdate.RowIndex;
            Label lblMaterial = (Label)grdParts.Rows[rowIndx].FindControl("lblMATERIALCODE");
            Label lblSUPPLIERID = (Label)grdParts.Rows[rowIndx].FindControl("lblSUPPLIERID");
            lblMaterialHidden.Text = lblMaterial.Text;
            lblSUPPLIERIDHidden.Text = lblSUPPLIERID.Text;
            
        }

        public void ExportToExcel()
        {
            try
            {
              
                string filename = "SUPPLIER STOCKS_" + DateTime.Now.ToString("ddMMyyyyhhmmssffffff");
                //Turn off the view stateV 225 55
                this.EnableViewState = false;
                //Remove the charset from the Content-Type header
                Response.Charset = String.Empty;

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
                Response.Charset = "";

                grdParts.AllowPaging = false;
                this.fillGRD();
                // If you want the option to open the Excel file without saving then
                // comment out the line below
                // Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.xls";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                grdParts.RenderControl(htmlWrite);

                //Append CSS file

                System.IO.FileInfo fi = new System.IO.FileInfo(Server.MapPath("StyleSheet.css"));
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
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.Message);
                Logger.GetInstance().Fatal(ex.StackTrace);
                throw ex;
            }
        }

        public void fillGRD()
        {
            grdParts.DataSource = dalPSI.PSI_GET_SUPPLIER_STOCKS(txtMATERIALCODE.Text.Trim(), ddSupplier.SelectedValue.ToString()).Tables[0];
            grdParts.DataBind();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if(grdParts.Rows.Count > 0)
            {
                ExportToExcel();
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
{
  /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
     server control at run time. */
}

        protected void grdParts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdParts.PageIndex = e.NewPageIndex;
            grdParts.DataSource = dalPSI.PSI_GET_SUPPLIER_STOCKS(txtMATERIALCODE.Text.Trim(), ddSupplier.SelectedValue.ToString()).Tables[0];
            grdParts.DataBind();

        }

        protected void grdParts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //,,,,, , ,
                //Label lblvendorcode = (Label)e.Row.FindControl("lnkUPDATE");
                //Label lblvendname = (Label)e.Row.FindControl("lblSUPPLIERNAME");

                
                
                
                

                Label lblMATERIALCODE = (Label)e.Row.FindControl("lblMATERIALCODE");
                Label lblDESCRIPTION = (Label)e.Row.FindControl("lblDESCRIPTION");
                Label lblQTY = (Label)e.Row.FindControl("lblQTY");
                Label lblProblemCat = (Label)e.Row.FindControl("lblProblemCat");
                Label lblReason = (Label)e.Row.FindControl("lblReason");
                Label lblPIC = (Label)e.Row.FindControl("lblPIC");
                Label lblCounterMeasure = (Label)e.Row.FindControl("lblCounterMeasure");
                Label lblSUPPLIERNAME = (Label)e.Row.FindControl("lblSUPPLIERNAME");
                Label lblSUPPLIERID = (Label)e.Row.FindControl("lblSUPPLIERID");

                LinkButton lnk = (LinkButton)e.Row.FindControl("lnkUPDATE");





                // vendorcode,vendorname,delcat,partcode,partdesc, spq, partsize,DOS

                string strValues = HttpUtility.HtmlDecode(lblMATERIALCODE.Text) + "','"
                                                 + HttpUtility.HtmlDecode(lblDESCRIPTION.Text) + "','"
                                                 + HttpUtility.HtmlDecode(lblQTY.Text) + "','"
                                                 + HttpUtility.HtmlDecode(lblProblemCat.Text) + "','"
                                                 + HttpUtility.HtmlDecode(lblReason.Text) + "','"
                                                 + HttpUtility.HtmlDecode(lblPIC.Text) + "','"
                                                 + HttpUtility.HtmlDecode(lblCounterMeasure.Text) + "','"
                                                 + HttpUtility.HtmlDecode(lblSUPPLIERNAME.Text) + "','"
                                                 + HttpUtility.HtmlDecode(lblSUPPLIERID.Text);

                lnk.OnClientClick = HttpUtility.HtmlDecode("showModal('" + strValues + "');return false;");

            }
        }

        public void updateList()
        {
            
       
            dalPSI.PSI_UPLOAD_MANUAL_SUPPLIER_STOCKS
            (
            txtSUPPLIERIDHidden.Text,
            txtMaterialHidden.Text,
            txtQty.Text,
            ddProblemCat.SelectedItem.Text,
            txtReason.Text,
            txtPIC.Text,
            txtCountermeasure.Text,
            lblUID.Text);


            string strURL = "PSI_SUPPLER_STOCKS_UPLOADING.aspx?vendorCode=" + ddSupplier.SelectedValue.ToString();
            Response.Write("<script>");
            Response.Write("alert('Successfully updated!');");
            Response.Write("window.location = '../form/" + strURL + "';");
            Response.Write("</script>");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                updateList();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddSupplier.SelectedValue.ToString().Trim() != "")
                {
                    msgBox.confirm("Are you sure you want to delete" + ddSupplier.SelectedItem.Text + " stocks permanently?", "deleteAllConfirm");
                }
                else
                {
                    msgBox.alert("Please select supplier!");
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}