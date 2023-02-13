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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
//using System.Drawing;
using System.Text;
using com.eppi.utils;
using System.Threading;
//using System.Drawing;
using ZXing.Common;
using ZXing;
using ZXing.QrCode;
using ZXing.OneD;
using Zen.Barcode;
using FGWHSEClient;
using FGWHSEClient.DAL;
using System.Text.RegularExpressions;


namespace FGWHSEClient.Form
{
    public partial class EmptyBoxAntennaToReturnables : System.Web.UI.Page
    {
        EmptyPcaseDAL epDAL = new EmptyPcaseDAL();
        DataSet dsBoard = new DataSet();
        int intDisplayColPattern = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                FillAntenna();
                LoadData();
            }
        }

        public void LoadData()
        {
            try
            {
                dsBoard = epDAL.EMPTY_BOX_GET_MONITORING_DETAILS(ddlLoadingDock.SelectedValue.ToString());
                generateTable(dsBoard, intDisplayColPattern);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                //Response.Write("<script>");
                //Response.Write("alert('" + ex.Message.ToString() + "');");
                //Response.Write("</script>");
            }
        }


        public void generateTable(DataSet ds, int intColumnDisplay)
        {
            
            DataTable dtList = ds.Tables[0];

            createTableData(dtList, tbTable, 3, 1, 3, "tableDetailContainer", "TableHeadContainerRFID", true);

        }

        public void createTableData(DataTable dt, HtmlTable tbTable, int intActualColumnDisplayCount, int intDataColStart, int intDataColEnd, string strCssClass, string strCssClassHeader, bool isColumnEnd)
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
                                //if (intRowCount == 1 && intColCount == 0)
                                //{
                                //    tdCol.RowSpan = intRowCountMax - 2;
                                //}
                                //else 
                                
                                if (intRowCount == intRowCountMax - 1 || intRowCount == intRowCountMax - 2)
                                {
                                    if (intRowCount == intRowCountMax - 2)
                                    {
                                        tdCol.Attributes.Add("style", "background-color:yellow");
                                    }
                                    if (intColCount == 0)
                                    {
                                        tdCol.ColSpan = 2;
                                    }
                                }

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

                            if (intRowCount == 0)
                            {
                                lblText.Font.Size = FontUnit.XXLarge;
                            }
                            else
                            {
                                if (strText.Length > 20)
                                {

                                    lblText.Font.Size = FontUnit.Medium;

                                }
                                else
                                {
                                    lblText.Font.Size = FontUnit.Large;
                                }
                            }

                            lblText.Text = strText;
                            if (strText == "")
                            {
                                lblText.Height = Unit.Pixel(18);
                            }
                            tdCol.Controls.Add(lblText);

                            if ((intRowCount == intRowCountMax - 1 || intRowCount == intRowCountMax - 2) && intColCount == 1 && intRowCountMax != 1)
                            {
                                tdCol.Controls.Remove(lblText);
                                trRow.Controls.Remove(tdCol);
                            }
                            //else if (intColCount == 0 && intRowCount > 1 && intRowCount < intRowCountMax - 1)
                            //{
                            //    tdCol.Controls.Remove(lblText);
                            //    trRow.Controls.Remove(tdCol);
                            //}

                            if (intColCount == intActualColumnDisplayCount - 1)
                            {
                                if (intRowCount == 0)
                                {
                                    if (isColumnEnd == false && intRowCountMax != 1)
                                    {
                                        HtmlTableCell tdSpace = new HtmlTableCell();
                                        tdSpace.RowSpan = dv.Count;
                                        trRow.Controls.Add(tdSpace);

                                        Label lblSpace = new Label();
                                        lblSpace.Width = Unit.Pixel(20);
                                        tdSpace.Controls.Add(lblSpace);
                                    }
                                }
                                else if (intRowCount == intRowCountMax - 1)
                                {
                                    HtmlTableRow trSpaceBottom = new HtmlTableRow();
                                    tbTable.Controls.Add(trSpaceBottom);

                                    HtmlTableCell tdSpaceBottom = new HtmlTableCell();
                                    trSpaceBottom.Controls.Add(tdSpaceBottom);

                                    Label lblSpaceBottom = new Label();
                                    lblSpaceBottom.Height = Unit.Pixel(20);
                                    tdSpaceBottom.Controls.Add(lblSpaceBottom);
                                }

                            }
                        }
                    }


                    strValues = "";
                }

            }

        }

        private void FillAntenna()
        {
            DataTable dtFillAntenna = new DataTable();
            dtFillAntenna = epDAL.EMPTY_PCASE_GET_ANTENNA_CONVEYOR_RETURNABLES().Tables[0];

            DataTable dtAntenna = new DataTable();


            dtAntenna.Columns.Add("id", typeof(string));
            dtAntenna.Columns.Add("name", typeof(string));
            dtAntenna.Rows.Add("--Please Select--","");
            foreach (DataRow row in dtFillAntenna.Rows)
            {
                String id = (string)row[1];
                String name = (string)row[0];
                dtAntenna.Rows.Add(id, name);
            }

            ddlLoadingDock.DataSource = dtAntenna;
            ddlLoadingDock.DataTextField = "id";
            ddlLoadingDock.DataValueField = "name";

            ddlLoadingDock.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}