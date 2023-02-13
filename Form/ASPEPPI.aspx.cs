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
    public partial class ASPEPPI : System.Web.UI.Page
    {
        public ASPDAL aDAL = new ASPDAL();
        protected DataTable dtASP = new DataTable();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.IsPostBack)
            {
                txtFromDate.Text = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy")) + " 08:00 AM";
                txtToDate.Text = Convert.ToString(DateTime.Now.AddDays(1).ToString("dd-MMM-yyyy")) + " 08:00 AM";

                FillDropDown(ddDateType, aDAL.GET_ASP_REPORTS_DATE_FILTER());
                getASPdetails();
            }
            else
            {
                
                dtASP = (DataTable)ViewState["dtASP"];
            }
            ViewState["dtASP"] = dtASP;

        }

        public DataTable getASPdetails()
        {
            DataSet ds = aDAL.GET_ASP_INQUIRY(txtItemCode.Text.Trim(), txtSerialNo.Text.Trim(), txtFromDate.Text.Trim(), txtToDate.Text.Trim(), "EPPI", txtDS.Text.Trim(), ddDateType.SelectedValue.ToString());
            DataTable dt = ds.Tables[0];
            dtASP = dt;
            ViewState["dtASP"] = dt;

            //grdASP.DataSource = dt;
            //grdASP.DataBind();
            createTableData(dt, tbASP,15, 1, 15, "tableDetailContainer", "TableHeadContainerRFID");
            return dt;
        }




        protected void btnSearch_Click(object sender, EventArgs e)
        {
            getASPdetails();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            //if (grdASP.Rows.Count > 0)
            //{
                ExportToExcel();
            //}
        }

        public void ExportToExcel()
        {
            try
            {

                string filename = "ASP_" + DateTime.Now.ToString("ddMMyyyyhhmmssffffff");
                //Turn off the view stateV 225 55
                this.EnableViewState = false;
                //Remove the charset from the Content-Type header
                Response.Charset = String.Empty;

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
                Response.Charset = "";
                
                this.getASPdetails();
                // If you want the option to open the Excel file without saving then
                // comment out the line below
                // Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.xls";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                tbASP.RenderControl(htmlWrite);
                //grdASP.RenderControl(htmlWrite);

                //Append CSS file

                System.IO.FileInfo fi = new System.IO.FileInfo(Server.MapPath("StyleSheet.css"));
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.IO.StreamReader sr = fi.OpenText();
                while (sr.Peek() >= 0)
                {
                    sb.Append(sr.ReadLine());
                }
                sr.Close();

                //Response.Write("<html><head><style type='text/css'>" + sb.ToString() + "</style><head>" + stringWrite.ToString() + "</html>");

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

        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            txtDesc.Text = "";
            DataView dv = aDAL.GET_ASP_ITEMCODE(txtItemCode.Text.Trim()).Tables[0].DefaultView;
            if (dv.Count > 0)
            {
                txtDesc.Text = dv[0]["ITEMCODE"].ToString();
            }
            else
            {
                txtItemCode.Text = "";
                msgBox.alert("Invalid Itemcode!");
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //
        }

        private void FillDropDown(DropDownList dd, DataSet ds)
        {
            dd.DataSource = ds;
            dd.DataTextField = "DESCRIPTION";
            dd.DataValueField = "ID";
            dd.DataBind();
            dd.SelectedIndex = 0;
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
                            if (intRowCount == 0)
                            {
                                strColor = "WHITE";
                                tdCol.Attributes.Add("class", strCssClassHeader);
                            }
                            else
                            {
                                strColor = "BLACK";
                                tdCol.Attributes.Add("class", strCssClass);
                            }

                            trRow.Controls.Add(tdCol);


                            Label lblText = new Label();

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

                   
                    strValues = "";
                }

            }

        }


    }
}