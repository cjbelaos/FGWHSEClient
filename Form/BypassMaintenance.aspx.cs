using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using FGWHSEClient.DAL;
using com.eppi.utils;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;

namespace FGWHSEClient.Form
{
    public partial class BypassMaintenance : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string strLoginType = System.Configuration.ConfigurationManager.AppSettings["webFor"];
                if (strLoginType == "OUTSIDE")
                {
                    if (Session["supplierID"] == null)
                    {
                        if (Request.QueryString["DeviceType"] != null)
                        {
                            if (Request.QueryString["DeviceType"].ToString() == "Denso")
                            {
                                Response.Write("<script>");
                                Response.Write("alert('User not authorized! Please log in.');");
                                Response.Write("window.location = '../DENSOLogin.aspx';");
                                Response.Write("</script>");
                            }
                            else if (Request.QueryString["DeviceType"].ToString() == "HT")
                            {
                                Response.Write("<script>");
                                Response.Write("alert('User not authorized! Please log in.');");
                                Response.Write("window.location = '../HTLogin.aspx';");
                                Response.Write("</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>");
                            Response.Write("alert('User not authorized! Please log in.');");
                            Response.Write("window.location = '../Login.aspx';");
                            Response.Write("</script>");
                        }
                    }
                    else
                    {
                        if (!this.IsPostBack)
                        {
                            SearchResult();

                        }
                    }
                }
                else
                {
                    if (!this.IsPostBack)
                    {
                        SearchResult();

                    }
                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }








        protected void gvBypass_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //PartsLocationCheckDAL maint = new PartsLocationCheckDAL();
                //DataView dvPLC = new DataView();

                //dvPLC = maint.GetBypassApprovers();

                //string deleteValue = "";

                //if (dvPLC.Table.Rows[0]["DeleteFlag"].ToString() == Convert.ToString )
                //{
                //    deleteValue = "YES";
                //}
                //else
                //{
                //    deleteValue = "NO";
                //}

                //((Label)e.Row.FindControl("lblDeleteFlag")).Text = deleteValue;


            }

        }

        private void SearchResult()
        {
            PartsLocationCheckDAL maint = new PartsLocationCheckDAL();
            DataView dvPLC = new DataView();


            dvPLC = maint.GetBypassApprovers();

            if (dvPLC.Table.Rows.Count > 0)
            {
                gvBypass.DataSource = dvPLC;
                gvBypass.DataBind();
                gvBypass.Visible = true;
            }
            else
            {
                MsgBox1.alert("NO DATA FOUND!");
                gvBypass.DataSource = null;
                gvBypass.DataBind();
            }

        }


        //private void OnLoad()
        //{
        //    PartsLocationCheckDAL maint = new PartsLocationCheckDAL();
        //    DataView dvPLC = new DataView();

        //    dvPLC = maint.GetBypassApprovers();

        //    foreach (GridViewRow row in gvBypass.Rows)
        //    {
        //        strSummary = row.Cells[4].Text;
        //        strReferenceNo = row.Cells[3].Text;
        //    }

        //    lblDeleteFlag.Text = dvPLC.Table.Rows[0]["MotherLot"].ToString();

        //}

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {
                PartsLocationCheckDAL maint1 = new PartsLocationCheckDAL();
                var data = maint1.SearchBypassApprovers(TxtUserId1.Text);

                if (data.Table.Rows.Count > 0)
                {
                    Response.Write("<script>alert('Bypass Approver already Exists!');</script>");
                    txtUserId.Text = "";
                    TxtUserId1.Text = "";
                    TxtUserId1.Focus();
                    return;
                }


                Maintenance maint = new Maintenance();
                DataSet ds = new DataSet();
                

                string strInsert = "";

                string EmployeeNo = TxtUserId1.Text.Trim();
                string CreatedBy = Session["UserName"].ToString();

                DataSet dsCheckNames = new DataSet();
                dsCheckNames = maint.CheckIfEmployeeNoIsExisting(EmployeeNo);
                if (dsCheckNames.Tables[0].Rows.Count>0)
                {
                    strInsert = maint.insertBypassApprover(EmployeeNo, CreatedBy);

                    if (strInsert == "")
                    {
                       
                        Response.Write("<script>alert('Bypass Approver Successfully Added');</script>");
                        txtUserId.Text = "";
                        TxtUserId1.Text = "";
                        TxtUserId1.Focus();
                        SearchResult();

                    }
                }
                else
                {
                    Response.Write("<script>alert('Invalid Employee No!');</script>");
                    txtUserId.Text = "";
                    TxtUserId1.Text = "";
                    TxtUserId1.Focus();
                }


                
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PartsLocationCheckDAL maint = new PartsLocationCheckDAL();
            var data = maint.SearchBypassApprovers(txtUserId.Text);

            if (data.Table.Rows.Count > 0)
            {
                gvBypass.DataSource = data;
                gvBypass.DataBind();
                gvBypass.Visible = true;
            }
            else
            {
                MsgBox1.alert("NO DATA FOUND!");
                gvBypass.DataSource = null;
                gvBypass.DataBind();
                SearchResult();
                txtUserId.Text = "";
                TxtUserId1.Text = "";
            }

        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["IDCancel"] = null;
            var btn = (Control)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            LinkButton btnCancel = sender as LinkButton;
            Session["IDCancel"] = row.RowIndex;
            Session["EmployeeNo"] =row.Cells[1].Text;
        }

        protected void gvBypass_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                Maintenance maint = new Maintenance();
                //lblMessage.Text = "";
                LinkButton btnCancel = (LinkButton)gvBypass.Rows[Convert.ToInt32(Session["IDCancel"].ToString())].FindControl("btnCancel");

                string stEmployeeNo = "";

                //foreach (GridViewRow row in gvBypass.Rows)
                //{
                //    stEmployeeNo = row.Cells[1].Text;
                //}

                DataSet dsDeleteFlag = new DataSet();
                dsDeleteFlag = maint.DeleteBypassUser(Session["EmployeeNo"].ToString(), Session["UserName"].ToString());
                if (dsDeleteFlag.Tables.Count == 0)
                {
                    MsgBox1.alert("DELETED SUCCESSFULLY");
                }
                else
                {

                }
                //Lagay k delete function
                //strUpdate = maint.UpdateParameter(Session["Value"].ToString(), txtValue.Text, Session["UserID"].ToString());
                //showAlert("success", "", "Successfully Updated the " + Session["Value"].ToString() + "");
                SearchResult();


                btnCancel.Enabled = false;
            }
               

        }

        protected void gvBypass_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvBypass_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}
