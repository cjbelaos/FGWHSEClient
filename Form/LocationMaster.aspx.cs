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
    public partial class LocationMaster : System.Web.UI.Page
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
                    Response.Write("window.location = '../Login.aspx';");
                    Response.Write("</script>");
                }
                else
                {
                    strPageSubsystem = "FGWHSE_007";
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
                    getWarehouse();
                    Search();
                }
                else
                {
                    if (Request.Form["deleteConfirm"] != null)
                    {
                        if (Request.Form["deleteConfirm"].ToString().Equals("1"))
                        {
                            deleteRowRecord();
                            Search();
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



        private void ClearData()
        {
            try
            {
                ddAddWH.SelectedIndex = -1;
                txtLocationID.Text = "";
                txtLocationName.Text = "";
                ddLocationType.SelectedIndex = -1;
                ddUnit.SelectedIndex = -1;
                txtLines.Text = "";
                txtRows.Text = "";
                txtDisplayOrder.Text = "";

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


        private void getWarehouse()
        {
            DataTable dtWH = new DataTable();



            dtWH = maint.GET_GET_WAREHOUSE().Tables[0];

            DataTable dtnewWH = new DataTable();


            dtnewWH.Columns.Add("warehouse_id", typeof(string));
            dtnewWH.Columns.Add("warehouse_name", typeof(string));


            foreach (DataRow row in dtWH.Rows)
            {

                String WHID = (string)row[0];
                String WHNAME = (string)row[1];
                dtnewWH.Rows.Add(WHID, WHNAME);
            }

            ddWH.DataSource = dtnewWH;
            ddWH.DataTextField = "warehouse_name";
            ddWH.DataValueField = "warehouse_id";
            ddWH.DataBind();

        }

        private void getAddWarehouse()
        {
            DataTable dtWH = new DataTable();

            dtWH = maint.GET_GET_WAREHOUSE().Tables[0];

            DataTable dtnewWH = new DataTable();


            dtnewWH.Columns.Add("warehouse_id", typeof(string));
            dtnewWH.Columns.Add("warehouse_name", typeof(string));


            foreach (DataRow row in dtWH.Rows)
            {

                String WHID = (string)row[0];
                String WHNAME = (string)row[1];
                dtnewWH.Rows.Add(WHID, WHNAME);
            }

            ddAddWH.DataSource = dtnewWH;
            ddAddWH.DataTextField = "warehouse_name";
            ddAddWH.DataValueField = "warehouse_id";
            ddAddWH.DataBind();

        }


        private void getAddLocType()
        {
            DataView dv = new DataView();

            dv = maint.GetLocMasterLocType();

            ddLocationType.DataSource = dv;
            ddLocationType.DataTextField = "Location_Type_Name";
            ddLocationType.DataValueField = "Location_Type_ID";
            ddLocationType.DataBind();

        }

        private void getAddUnit()
        {
            DataView dv = new DataView();

            dv = maint.GetLocMasterUnit();

            ddUnit.DataSource = dv;
            ddUnit.DataTextField = "Unit_Type_Name";
            ddUnit.DataValueField = "Unit_Type_ID";
            ddUnit.DataBind();

        }

        protected void grdLocationMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                 MsgBox1.confirm("Are you sure you want to delete this record?", "deleteConfirm");
                 HiddenField1.Value = grdLocationMaster.Rows[e.RowIndex].Cells[1].Text;
            }
            catch (Exception ex)
            {
               Logger.GetInstance().Fatal(ex.StackTrace, ex);
               MsgBox1.alert(ex.Message);
            }
        }

        protected void grdLocationMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {

                DataView dv = new DataView();

                string id = grdLocationMaster.Rows[e.NewEditIndex].Cells[1].Text;


                dv = maint.GetLocationMasterDetail(ddWH.SelectedValue, id);
                if (dv.Count > 0)
                {
                    ddAddWH.SelectedValue = ddWH.SelectedValue;
                    txtLocationID.Text = dv[0]["Location_ID"].ToString();
                    txtLocationName.Text = dv[0]["Location_Name"].ToString();
                    ddLocationType.SelectedValue = dv[0]["Location_Type_ID"].ToString();
                    ddUnit.SelectedValue = dv[0]["Unit_Type_ID"].ToString();
                    txtLines.Text = dv[0]["Lines"].ToString();
                    txtRows.Text = dv[0]["Rows"].ToString();
                    txtDisplayOrder.Text = dv[0]["Display_Order"].ToString();
                    txtLocationID.Enabled = false;
                    ddAddWH.Enabled = false;
                    ModalPopupExtender1.Show();

                    getAddLocType();
                    getAddUnit();
                    getAddWarehouse();
                    hidIsEdit.Value = "1";
                }


            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void grdLocationMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdLocationMaster.PageIndex = e.NewPageIndex;
            Search();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Search();

            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }


        private void Search()
        {
            try
            {
                DataTable dt = new DataTable();
                DataView dv = new DataView();

                dv = maint.GetLocationMaster(ddWH.SelectedValue);
                dt = dv.Table;

                grdLocationMaster.DataSource = dt;
                grdLocationMaster.DataBind();

            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ModalPopupExtender1.Show();
                getAddWarehouse();
                getAddLocType();
                getAddUnit();
                ClearData();
                txtLocationID.Enabled = true;
                ddAddWH.Enabled = true;
                hidIsEdit.Value = "0";
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dv = maint.CheckLocationTypeMaster(ddLocationType.SelectedValue);
                int iSave = 0;

                if (dv.Count > 0)
                {
                    if (dv[0]["Location_Type_ID"].ToString() == "CY" && hidIsEdit.Value == "0")
                    {
                        MsgBox1.alert("Saving Failed. Already have Container Yard in Location.");
                    }
                    else
                    {
                        if (txtLocationID.Text.Trim() == "" || txtLocationName.Text.Trim() == "" ||
                            txtLines.Text.Trim() == "" || txtRows.Text.Trim() == "" || txtDisplayOrder.Text.Trim() == "")
                        {
                            MsgBox1.alert("Please complete the details before saving.");
                        }
                        else
                        {
                            //Save
                            iSave = maint.AddMasterLocation(ddAddWH.SelectedValue, txtLocationID.Text.Trim(),
                                txtLocationName.Text.Trim(), ddLocationType.SelectedValue, "x", ddUnit.SelectedValue,
                                txtDisplayOrder.Text.Trim(), txtLines.Text.Trim(), txtRows.Text.Trim(), Session["UserID"].ToString());

                            MsgBox1.alert("Successfully Saved.");
                            Search();
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



        private void deleteRowRecord()
        {
            try
            {
                string str = maint.DeleteMasterLocation(ddWH.SelectedValue, HiddenField1.Value);

                if (str.Length > 0)
                {
                    MsgBox1.alert(str);
                }
                else
                {
                    Search();
                    MsgBox1.alert("Record Successfully Deleted.");
                }

            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message);
            }

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToExcel();
            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message.ToString());
            }
        }



        public void ExportToExcel()
        {

            DataView dvExport = new DataView();

            DataTable dt = new DataTable();

            dvExport = maint.GetLocationMaster(ddWH.SelectedValue); 

            dt = dvExport.Table;
            grdExport.DataSource = dt;
            grdExport.DataBind();

            grdExport.DataSource = dt;
            grdExport.DataBind();



            int exportDataCount = 0;
            string filename = "Location_Master_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            //msgBox.alert(filename);
            //Turn off the view stateV 225 55
            this.EnableViewState = false;
            //Remove the charset from the Content-Type header
            Response.Charset = String.Empty;

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
            Response.Charset = "";
            //grdTasks.AllowPaging = false;

            exportDataCount = dvExport.Count;




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

            //Append CSS file

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


        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

    }
}
