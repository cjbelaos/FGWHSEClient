using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlTypes;
using com.eppi.utils;

using FGWHSEClient.DAL;
namespace FGWHSEClient.Form
{
    public partial class SUPPLIER_DN_SCANNING_INQUIRY : System.Web.UI.Page
    {
        DNInquiryDAL DNI = new DNInquiryDAL();
        protected DataTable dtList = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request.QueryString["DNNO"] != null)
                {
                    txtDNNo.Text = Request.QueryString["DNNO"].ToString();
                }

                dtList = DNI.GET_SUPPLIER_SCANNED_DN(txtDNNo.Text.Trim()).Tables[0];

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
            createTableData(dt, tbList, 9, 2, 3, "tableDetailContainer", "TableHeadContainerRFID");
        }

        public void createTableData(DataTable dt, HtmlTable tbTable, int intActualColumnDisplayCount, int intDataColStart, int intDataColEnd, string strCssClass, string strCssClassHeader)
        {

            int intRowCountMax = dt.Rows.Count, intColCountMax = dt.Columns.Count;
            string strColor = "", strText = "";
            DataView dv = dt.DefaultView;
            if (dv.Count > 0)
            {
                for (int intRowCount = 0; intRowCount < intRowCountMax; intRowCount++)
                {
                
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
                                string strBgColor = dv[intRowCount]["BGCOLOR"].ToString();
                                tdCol.Attributes.Add("class", strCssClass);
                                tdCol.Attributes.Add("style", "background-color:" + strBgColor);
                            }

                            trRow.Controls.Add(tdCol);


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

            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("../form/SUPPLIER_DN_SCANNING_INQUIRY.aspx?DNNO="
             + txtDNNo.Text);

        }
    }
}