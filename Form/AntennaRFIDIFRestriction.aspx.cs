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
    public partial class AntennaRFIDIFRestriction : System.Web.UI.Page
    {
        InHouseReceivingDAL IHDAL = new InHouseReceivingDAL();
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

                if (!checkAuthority("FGWHSE_059"))
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
                btnADD.OnClientClick = HttpUtility.HtmlDecode("showModal('','','');return false;");

                fillDD(ddArea, true);
                fillDD(ddAreaEdit, false);
                fillDDRestriction(ddStatus, true);
                fillDDRestriction(ddStatusEdit, false);

                if (Request.QueryString["itemcode"] != null)
                {
                    txtItemCode.Text = Request.QueryString["itemcode"].ToString();
                }

                if (Request.QueryString["area"] != null)
                {
                    ddArea.SelectedValue = Request.QueryString["area"].ToString();
                }

                if (Request.QueryString["status"] != null)
                {
                    ddStatus.SelectedValue = Request.QueryString["status"].ToString();
                }

                if (Request.QueryString["dfrom"] != null)
                {
                    txtDateFrom.Text = Request.QueryString["dfrom"].ToString();
                }

                if (Request.QueryString["dto"] != null)
                {
                    txtDateTo.Text = Request.QueryString["dto"].ToString();
                }

                dtList = IHDAL.INHOUSE_GET_IF_RESTRICTION(txtItemCode.Text.Trim(), ddArea.SelectedValue.ToString(), ddStatus.SelectedValue.ToString(), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim()).Tables[0];

                ViewState["dtList"] = dtList;

            }
            else
            {
                dtList = (DataTable)ViewState["dtList"];

            }

            ViewState["dtList"] = dtList;

            getData(dtList);
        }
        private void fillDD(DropDownList dd, bool withdefaultvalue)
        {
            DataTable dt = new DataTable();
            dt = IHDAL.INK_GET_AREA().Tables[0];

            DataTable dtList = new DataTable();


            dtList.Columns.Add("ID", typeof(string));
            dtList.Columns.Add("DESC", typeof(string));

            if (withdefaultvalue == true)
            {
                dtList.Rows.Add("", "--Please Select--");
            }
            foreach (DataRow row in dt.Rows)
            {

                String locID = (string)row[2];
                String locNAME = (string)row[2] + " - " + (string)row[1];
                dtList.Rows.Add(locID, locNAME);
            }

            dd.DataSource = dtList;
            dd.DataTextField = "DESC";
            dd.DataValueField = "ID";
            dd.DataBind();

        }


        private void fillDDRestriction(DropDownList dd, bool withdefaultvalue)
        {
            DataTable dt = new DataTable();
            dt = IHDAL.INHOUSE_GET_RESTRICTION_STATUS().Tables[0];

            DataTable dtList = new DataTable();


            dtList.Columns.Add("ID", typeof(string));
            dtList.Columns.Add("DESC", typeof(string));

            if (withdefaultvalue == true)
            {
                dtList.Rows.Add("", "--Please Select--");
            }
            foreach (DataRow row in dt.Rows)
            {

                String locID = (string)row[0];
                String locNAME = (string)row[1];
                dtList.Rows.Add(locID, locNAME);
            }

            dd.DataSource = dtList;
            dd.DataTextField = "DESC";
            dd.DataValueField = "ID";
            dd.DataBind();

        }

        public void getData(DataTable dt)
        {
            createTableData(dt, tbList, 8, 2, 4, "tableDetailContainer", "TableHeadContainerRFID");
        }


        public void createTableData(DataTable dt, HtmlTable tbTable, int intActualColumnDisplayCount, int intDataColStart, int intDataColEnd, string strCssClass, string strCssClassHeader)
        {

            int intRowCountMax = dt.Rows.Count, intColCountMax = dt.Columns.Count;
            string strColor = "", strText = "", strBtnID = "", strBtnDeleteID = "", strValues = "";
            DataView dv = dt.DefaultView;
            if (dv.Count > 0)
            {
                for (int intRowCount = 0; intRowCount < intRowCountMax; intRowCount++)
                {
                    strBtnID = "BTN_" + intRowCount.ToString();
                    strBtnDeleteID = "BTN_DEL_" + intRowCount.ToString();
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

                                Button btnDel = new Button();
                                btnDel.Text = "Delete";
                                btnDel.ID = strBtnDeleteID;
                                btnDel.Attributes.Add("class", "btn nk - indigo btn - info");
                                tdCol.Controls.Add(btnDel);
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
                        Button btnDel = (Button)tbTable.FindControl(strBtnDeleteID);

                        btnEdit.OnClientClick = HttpUtility.HtmlDecode("showModal('" + strValues + "');return false;");
                        btnDel.OnClientClick = HttpUtility.HtmlDecode("btndeleteClick('" + strValues + "');return false;");
                    }
                    strValues = "";
                }

            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("../form/AntennaRFIDIFRestriction.aspx?itemcode="
                + txtItemCode.Text
                + "&area=" + ddArea.SelectedValue.ToString()
                + "&status=" + ddStatus.SelectedValue.ToString()
                + "&dfrom=" + txtDateFrom.Text
                + "&dto=" + txtDateTo.Text
                );
        }
        //
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtItemCodeEdit.Text.Trim() == "" || ddAreaEdit.SelectedValue.ToString() == "" || ddStatusEdit.SelectedValue.ToString() == "")
            {
                Response.Write("<script>");
                Response.Write("alert('Fill-up required details!');");
                Response.Write("</script>");
            }
            else
            {
                IHDAL.INHOUSE_ADD_RESTRICTION_STATUS(txtItemCodeEdit.Text.Trim(), ddAreaEdit.SelectedValue.ToString(), ddStatusEdit.SelectedValue.ToString(), Session["UserID"].ToString());
                Response.Write("<script>");
                Response.Write("alert('Saved Successfully!');");
                Response.Write("</script>");
            }


            Response.Redirect("../form/AntennaRFIDIFRestriction.aspx?itemcode="
               + txtItemCode.Text
               + "&area=" + ddArea.SelectedValue.ToString()
               + "&status=" + ddStatus.SelectedValue.ToString()
               + "&dfrom=" + txtDateFrom.Text
               + "&dto=" + txtDateTo.Text
               );
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            IHDAL.INHOUSE_DELETE_RESTRICTION_STATUS(txtItemDelete.Text, txtAreaDelete.Text, Session["UserID"].ToString());
            Response.Write("<script>");
            Response.Write("alert('Delete Successfully!');");
            Response.Write("</script>");


            Response.Redirect("../form/AntennaRFIDIFRestriction.aspx?itemcode="
               + txtItemCode.Text
               + "&area=" + ddArea.SelectedValue.ToString()
               + "&status=" + ddStatus.SelectedValue.ToString()
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