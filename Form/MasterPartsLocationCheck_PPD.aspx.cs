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
using FGWHSEClient.DAL;

namespace FGWHSEClient.Form
{
    public partial class MasterPartsLocationCheck_PPD : System.Web.UI.Page
    {
        public string strPageSubsystem = "";
        public string strAccessLevel = "";

        PartsLocationCheckDAL maint = new PartsLocationCheckDAL();

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
                    strPageSubsystem = "FGWHSE_056";
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
                string result = "";

                DataTable dt = new DataTable();
                DataView dv = new DataView();

                //dv = maint.GET_PARTS_LOCATION(txtPcode.Text).Tables[0].DefaultView;
                dv = maint.GetPartName_rev1(txtPcode.Text);


                iSave = dv.Count;
                if (iSave > 0 && hidIsEdit.Value == "0")
                {
                    //MsgBox1.alert("Partcode Already Exists");

                   // return;
                }
                if (txtPcode.Text.Trim() == "" || txtDescription.Text.Trim() == "" || txtLocation.Text.Trim() == "" || txtGNSLocation.Text == "" || txtWhseName.Text == "") //|| txtLocationST.Text.Trim() == "")
                {
                    MsgBox1.alert("Please complete the details before saving.");
                }
                else
                {

                    int IQA = 0;
                    int EKANBAN = 0;
                    int id = Convert.ToInt32(HiddenField1.Value.ToString());
                    string action = "";

                    if (hidIsEdit.Value == "0")
                    {
                        action = "INSERT";
                    }

                    //if (chKIQA.Checked==true) { IQA=1;}
                    if (chkEKANBAN.Checked == true) { EKANBAN = 1; }

                    if (txtCapacity.Text == "")
                    {
                        txtCapacity.Text = "0";
                    }

                    string updateby = Session["UserName"].ToString();

                    result = maint.SAVE_PARTS_LOCATION_PPD(txtPcode.Text.Trim(), txtLocation.Text.Trim(), "", txtGNSLocation.Text.Trim(), ddlIQA.SelectedValue.ToString(), EKANBAN, Convert.ToDecimal(txtCapacity.Text), updateby, id, txtWhseName.Text.Trim(), action);


                    if (result == "")
                    {
                        MsgBox1.alert("Successfully Saved.");
                        grdItemControLimitMaster.EditIndex = -1;
                        Search(txtPartCode.Text);

                    }
                    else
                    {
                        MsgBox1.alert("ERROR: " + result);
                        ModalPopupExtender1.Show();

                    }


                    
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

                //dv = maint.GET_PARTS_LOCATION(partcode).Tables[0].DefaultView;
                dv = maint.GetPartsLocationList_PPD(partcode);
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
                HiddenField1.Value = grdItemControLimitMaster.Rows[e.RowIndex].Cells[9].Text;
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
                string str = maint.DELETE_PARTS_LOCATION_PPD(HiddenField1.Value);

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
                txtDescription.Enabled = false;
                txtLocation.Enabled = true;
                txtWhseName.Enabled = true;
                txtPcode.Text = HttpUtility.HtmlDecode(grdItemControLimitMaster.Rows[e.NewEditIndex].Cells[1].Text);
                txtDescription.Text = HttpUtility.HtmlDecode(grdItemControLimitMaster.Rows[e.NewEditIndex].Cells[2].Text);
                txtWhseName.Text = HttpUtility.HtmlDecode(grdItemControLimitMaster.Rows[e.NewEditIndex].Cells[5].Text);
                txtLocation.Text = HttpUtility.HtmlDecode(grdItemControLimitMaster.Rows[e.NewEditIndex].Cells[3].Text);
                //txtLocationST.Text = HttpUtility.HtmlDecode(grdItemControLimitMaster.Rows[e.NewEditIndex].Cells[4].Text);
                txtGNSLocation.Text = HttpUtility.HtmlDecode(grdItemControLimitMaster.Rows[e.NewEditIndex].Cells[4].Text);
                string IQA = HttpUtility.HtmlDecode(grdItemControLimitMaster.Rows[e.NewEditIndex].Cells[6].Text);
                string EKANBAN = HttpUtility.HtmlDecode(grdItemControLimitMaster.Rows[e.NewEditIndex].Cells[7].Text);

                HiddenField1.Value = HttpUtility.HtmlDecode(grdItemControLimitMaster.Rows[e.NewEditIndex].Cells[9].Text);

                ddlIQA.SelectedValue = IQA.Trim();


                //if (IQA.ToUpper().Trim() == "TRUE")
                //{
                //    chKIQA.Checked = true;
                //}
                //else
                //{
                //    chKIQA.Checked = false;
                //}

                if (EKANBAN.ToUpper().Trim() == "YES")
                {
                    chkEKANBAN.Checked = true;
                }
                else
                {
                    chkEKANBAN.Checked = false;
                }

                txtCapacity.Text = HttpUtility.HtmlDecode(grdItemControLimitMaster.Rows[e.NewEditIndex].Cells[8].Text);

                ModalPopupExtender1.Show();
                hidIsEdit.Value = "1";
                grdItemControLimitMaster.EditIndex = -1;
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
                txtDescription.Enabled = false;
                txtLocation.Text = "";
                txtGNSLocation.Text = "";
                txtWhseName.Text = "";
                hidIsEdit.Value = "0";
                ModalPopupExtender1.Show();
                HiddenField1.Value = "0";
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
            string filename = "PartsLocationMaster_" + DateTime.Now.ToString("(yyyyMMddHHmmss)");

            DataView dvExport = new DataView();

            DataTable dt = new DataTable();

            //dvExport = maint.GET_PARTS_LOCATION(txtPartCode.Text.Trim()).Tables[0].DefaultView;

            //dvExport = maint.GetPartName_rev1(txtPartCode.Text.Trim());

            dvExport = maint.GetPartsLocationList_PPD(txtPartCode.Text.Trim());

            dt = dvExport.Table;

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                using (System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw))
                {
                    GridView grid = new GridView();
                    grid.DataSource = dt;
                    grid.DataBind();
                    grid.RenderControl(htw);
                    Response.Write(sw.ToString());
                }
            }

            Response.End();



        }





        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        protected void txtPcode_TextChanged(object sender, EventArgs e)
        {
            //remove not allowed to add records
            try
            {
                PartsLocationCheckDAL maint = new PartsLocationCheckDAL();
                DataView dvPartName = new DataView();

                dvPartName = maint.GetMaterialName(txtPcode.Text.Trim());

                if (dvPartName.Table.Rows.Count == 0)
                {
                    MsgBox1.alert("Partcode not exist in Material Master");
                    ModalPopupExtender1.Show();
                    txtPcode.Text = "";
                    txtDescription.Text = "";
                }
                else
                {
                    txtDescription.Text = dvPartName.Table.Rows[0]["MaterialName"].ToString();
                    ModalPopupExtender1.Show();
                    txtPcode.Enabled = false;
                    txtWhseName.Focus();
                }


            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            grdItemControLimitMaster.EditIndex = -1;
            Search(txtPartCode.Text);
        }
    }
}
