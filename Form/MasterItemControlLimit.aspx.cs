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
using com.eppi.utils;

namespace FGWHSEClient.Form
{
    public partial class WebForm1 : System.Web.UI.Page
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
                    strPageSubsystem = "FGWHSE_012";
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
                    Search(txtPartCode.Text);
                }
                else
                {
                    if (Request.Form["deleteConfirm"] != null)
                    {
                        if (Request.Form["deleteConfirm"].ToString().Equals("1"))
                        {
                            deleteRowRecord();
                            Search(txtPartCode.Text);
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Search(txtPartCode.Text);

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
                int iSave = 0;

                DataTable dt = new DataTable();
                DataView dv = new DataView();

                dv = maint.GET_ITEM_LIMIT_CONTROL(txtPcode.Text).Tables[0].DefaultView;



                iSave = dv.Count; 
                if ( iSave > 0 && hidIsEdit.Value == "0")
                {
                    MsgBox1.alert("Partcode Already Exists");
                    
                    return;
                }
                if (txtPcode.Text.Trim() == "" || txtDescription.Text.Trim() == "" || txtWarningLimit.Text.Trim() == "" || txtDeliveryLimit.Text.Trim() == "" || txtExpirationLimit.Text.Trim() == "")
                {
                    MsgBox1.alert("Please complete the details before saving.");
                }
                else
                {
                    //Save
                    iSave = maint.ADD_ITEM_LIMIT_CONTROL(txtPcode.Text.Trim(), txtDescription.Text.Trim(), txtWarningLimit.Text.Trim(), txtDeliveryLimit.Text.Trim(), txtExpirationLimit.Text.Trim(), Session["UserID"].ToString());

                    MsgBox1.alert("Successfully Saved.");
                    Search(txtPartCode.Text);
                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }

        }


        private int Search(string partcode)
        {
            int intCount = 0;
            try
            {
                DataTable dt = new DataTable();
                DataView dv = new DataView();

                dv = maint.GET_ITEM_LIMIT_CONTROL(partcode).Tables[0].DefaultView;
                dt = dv.Table;

                grdItemControLimitMaster.DataSource = dt;
                grdItemControLimitMaster.DataBind();
                intCount = dv.Count;

            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }

            return intCount;
        }

        protected void grdLocationMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdItemControLimitMaster.PageIndex = e.NewPageIndex;
            Search(txtPartCode.Text);
        }

        protected void grdLocationMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                MsgBox1.confirm("Are you sure you want to delete this record?", "deleteConfirm");
                HiddenField1.Value = grdItemControLimitMaster.Rows[e.RowIndex].Cells[1].Text;
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
                string str = maint.DELETE_ITEM_LIMIT_CONTROL(HiddenField1.Value);

                if (str.Length > 0)
                {
                    MsgBox1.alert(str);
                }
                else
                {
                    Search(txtPartCode.Text);
                    MsgBox1.alert("Record Successfully Deleted.");
                }

            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message);
            }

        }

        protected void grdLocationMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                txtPcode.Enabled = false;
                txtPcode.Text = HttpUtility.HtmlDecode(grdItemControLimitMaster.Rows[e.NewEditIndex].Cells[1].Text);
                txtDescription.Text = HttpUtility.HtmlDecode(grdItemControLimitMaster.Rows[e.NewEditIndex].Cells[2].Text);
                txtWarningLimit.Text = HttpUtility.HtmlDecode(grdItemControLimitMaster.Rows[e.NewEditIndex].Cells[3].Text);
                txtDeliveryLimit.Text = HttpUtility.HtmlDecode(grdItemControLimitMaster.Rows[e.NewEditIndex].Cells[4].Text);
                txtExpirationLimit.Text = HttpUtility.HtmlDecode(grdItemControLimitMaster.Rows[e.NewEditIndex].Cells[5].Text);
                ModalPopupExtender1.Show();
                hidIsEdit.Value = "1";
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
                txtPcode.Enabled = true;
                txtPcode.Text = "";
                txtDescription.Text = "";
                txtWarningLimit.Text = "";
                txtDeliveryLimit.Text = "";
                txtExpirationLimit.Text = "";
                hidIsEdit.Value = "0";
                ModalPopupExtender1.Show();
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

            dvExport = maint.GET_ITEM_LIMIT_CONTROL(txtPartCode.Text.Trim()).Tables[0].DefaultView;

            dt = dvExport.Table;
            grdExport.DataSource = dt;
            grdExport.DataBind();

            grdExport.DataSource = dt;
            grdExport.DataBind();



            int exportDataCount = 0;
            string filename = "Item_Limit_Control_" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
 