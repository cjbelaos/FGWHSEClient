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
using System.Text.RegularExpressions;
using FGWHSEClient.LogInService;
using System.Threading;

namespace FGWHSEClient.Form
{
    public partial class VPASP_SERIAL_SCAN_MASTER : System.Web.UI.Page
    {
        public VPaspDAL drVPasp = new VPaspDAL();
        protected DataTable dtList = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
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
               
                if (!checkAuthority("FGWHSE_058"))
                {
                    Response.Write("<script>");
                    Response.Write("alert('You are not authorized to access the page.');");
                    Response.Write("window.location = 'Default.aspx';");
                    Response.Write("</script>");
                }
            }


            this.MaintainScrollPositionOnPostBack = true;

            if (!this.IsPostBack)
            {
                btnADD.OnClientClick = HttpUtility.HtmlDecode("showModal('','');return false;");
                fillDD(ddStatus, true);
                fillDD(ddStatusEdit, true);
                if (Request.QueryString["pcode"] != null)
                {
                    txtProductCode.Text = Request.QueryString["pcode"].ToString();
                }

                if (Request.QueryString["stat"] != null)
                {
                    ddStatus.SelectedValue = Request.QueryString["stat"].ToString();
                }

                if (Request.QueryString["dfrom"] != null)
                {
                    txtDateFrom.Text = Request.QueryString["dfrom"].ToString();
                }

                if (Request.QueryString["dto"] != null)
                {
                    txtDateTo.Text = Request.QueryString["dto"].ToString();
                }
               

                dtList = drVPasp.VP_ASP_GET_SERIAL_SCAN_MASTER(txtProductCode.Text, txtDateFrom.Text, txtDateTo.Text, ddStatus.SelectedValue.ToString()).Tables[0];
              

                ViewState["dtList"] = dtList;
            }
            else
            {
                dtList = (DataTable)ViewState["dtList"];
            }


            ViewState["dtList"] = dtList;
            
            getData(dtList);
        }



        public void getData(DataTable dt)
        {
            createTableData(dt, tbList,8,2 ,3 , "tableDetailContainer", "TableHeadContainerRFID");
        }
        public void createTableData(DataTable dt, HtmlTable tbTable, int intActualColumnDisplayCount,int intDataColStart, int intDataColEnd, string strCssClass, string strCssClassHeader)
        {

            int intRowCountMax = dt.Rows.Count, intColCountMax = dt.Columns.Count;
            string strColor = "", strText = "", strBtnID = "", strValues = "";
            DataView dv = dt.DefaultView;
            if (dv.Count > 0)
            {
                for (int intRowCount = 0; intRowCount < intRowCountMax; intRowCount++)
                {
                    strBtnID = "BTN_" + intRowCount.ToString();
                    HtmlTableRow trRow = new HtmlTableRow();
                    tbTable.Controls.Add(trRow);
                    for (int intColCount = 0; intColCount < intColCountMax; intColCount++)
                    {
                        if (intColCount < intActualColumnDisplayCount)
                        {
                            HtmlTableCell tdCol = new HtmlTableCell();
                            if (dv[intRowCount]["ROWTYPE"].ToString() == "HEADER")
                            {
                                tdCol.Attributes.Add("class", strCssClassHeader);
                            }
                            else
                            {
                                tdCol.Attributes.Add("class", strCssClass);
                            }

                            trRow.Controls.Add(tdCol);

                            if (intColCount == 0 && intRowCount > 0)
                            {
                                Button btn = new Button();
                                btn.Text = "Edit";
                                btn.ID = strBtnID;
                                btn.Attributes.Add("class", "btn nk - indigo btn - info");
                                tdCol.Controls.Add(btn);
                            }
                            else
                            { 
                                Label lblText = new Label();
                                strColor = dv[intRowCount]["color"].ToString();
                                strText = dv[intRowCount][intColCount].ToString();

                                if (intColCount >= intDataColStart - 1 && intColCount <= intDataColEnd - 1)
                                {
                                    string strNext = "','";
                                    if (intColCount + 1 == intColCountMax)
                                    {
                                        strNext = "";
                                    }
                                    strValues = strValues + HttpUtility.HtmlDecode(strText) + strNext;
                                }

                                lblText.Attributes.Add("style", "max-width:700px;color:" + strColor);
                                
                                lblText.Text = strText;
                                if (strText == "")
                                {
                                    lblText.Height = Unit.Pixel(18);
                                }
                                tdCol.Controls.Add(lblText);
                            }
                        }
                    }

                    if (intRowCount != 0)
                    {
                        Button btnEdit = (Button)tbTable.FindControl(strBtnID);
                        btnEdit.OnClientClick = HttpUtility.HtmlDecode("showModal('" + strValues + "');return false;");
                    }
                    strValues = "";
                }

            }

        }


        private void fillDD(DropDownList dd, bool withdefaultvalue)
        {
            DataTable dtLoadingDock = new DataTable();
            dtLoadingDock = drVPasp.VP_ASP_GET_STATUS();

            DataTable dtDock = new DataTable();


            dtDock.Columns.Add("ID", typeof(string));
            dtDock.Columns.Add("DESC", typeof(string));

            if (withdefaultvalue == true)
            {
                dtDock.Rows.Add("", "--Please Select--");
            }
            foreach (DataRow row in dtLoadingDock.Rows)
            {

                String locID = (string)row[0];
                String locNAME = (string)row[1];
                dtDock.Rows.Add(locID, locNAME);
            }

            dd.DataSource = dtDock;
            dd.DataTextField = "DESC";
            dd.DataValueField = "ID";
            dd.DataBind();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            Response.Redirect("../form/VPASP_SERIAL_SCAN_MASTER.aspx?pcode="
                + txtProductCode.Text
                +   "&stat="+ddStatus.SelectedValue.ToString()
                + "&dfrom=" + txtDateFrom.Text
                + "&dto=" + txtDateTo.Text
                );



        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProductCodeEdit.Text.Trim() == "" || ddStatusEdit.SelectedValue.ToString() == "")
            {
                Response.Write("<script>");
                Response.Write("alert('Fill-up required details!');");
                Response.Write("</script>");
            }
            else
            {
                drVPasp.VP_ASP_ADD_UPDATE_SERIAL_MASTER(txtProductCodeEdit.Text, ddStatusEdit.SelectedValue.ToString(), Session["UserID"].ToString());
                Response.Write("<script>");
                Response.Write("alert('Saved Successfully!');");
                Response.Write("</script>");
            }

            

            Response.Redirect("../form/VPASP_SERIAL_SCAN_MASTER.aspx?pcode="
                + txtProductCode.Text
                + "&stat=" + ddStatus.SelectedValue.ToString()
                + "&dfrom=" + txtDateFrom.Text
                + "&dto=" + txtDateTo.Text
                );
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

                        //if (strRole != "")
                        //{
                        //    strAccessLevel = strRole;
                        //}

                    }
                }
                return isValid;
            }
            catch (Exception ex)
            {
                //Response.Write("<script>");
                //Response.Write("alert('" + ex.Message.ToString() + "');");
                //Response.Write("</script>");

                isValid = false;
                return isValid;
            }
        }

    }

   
}