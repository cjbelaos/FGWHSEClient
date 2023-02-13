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
    public partial class EmptyPcaseStagingAreaInquiry : System.Web.UI.Page
    {
        EmptyPcaseDAL epDAL = new EmptyPcaseDAL();
        DataSet dsBoard = new DataSet();
        Maintenance maint = new Maintenance();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                TimeSpan dsstart = new TimeSpan(7, 0, 0); //10 o'clock
                TimeSpan dsend = new TimeSpan(19, 0, 0); //12 o'clock
                TimeSpan nsdsstartSameday = new TimeSpan(19, 0, 0); //10 o'clock
                TimeSpan nsdsendSameday = new TimeSpan(12, 0, 0); //12 o'clock
                TimeSpan now = DateTime.Now.TimeOfDay;

                if ((now > dsstart) && (now < dsend))
                {
                    txtFrom.Text = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy")) + " 07:00 AM";
                    txtTo.Text = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy")) + " 07:00 PM";

                }
                else
                {
                    if ((now > nsdsstartSameday) && (now < nsdsendSameday))
                    {
                        txtFrom.Text = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy")) + " 07:00 PM";
                        txtTo.Text = Convert.ToString(DateTime.Now.AddDays(1).ToString("dd-MMM-yyyy")) + " 07:00 AM";
                    }
                    else
                    {
                        txtFrom.Text = Convert.ToString(DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy")) + " 07:00 PM";
                        txtTo.Text = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy")) + " 07:00 AM";
                    }
                   

                }

                
                FillSupplier();
                FillAntenna();
                LoadData();
            }
        }

        public void LoadData()
        {
            try
            {
                dsBoard = epDAL.EMPTY_PCASE_BOARD_TO_STAGING_INQUIRY(txtFrom.Text.Trim(), txtTo.Text.Trim(), ddSupplier.SelectedValue.ToString(), ddAntenna.SelectedValue.ToString());
                createTableData(dsBoard.Tables[0], tbTable, 1, "tableDetailContainer", "TableHeadContainerRFID");
                lblRefresh.Text = "Last Refreshed Time : " + DateTime.Now.ToString();
            }
            catch(Exception ex)
            {
                Response.Write("<script>");
                Response.Write("alert('"+ ex.Message.ToString() + "');");
                Response.Write("</script>");
            }
        }

        public void createTableData(DataTable dt, HtmlTable tbTable, int intDataColStart, string strCssClass, string strCssClassHeader)
        {


            int intRowCountMax = dt.Rows.Count, intColCountMax = dt.Columns.Count;

            string strRowType = "";
            int intActualColumnDisplayCount = intColCountMax - 2;
            string strColor = "", strText = "", strBtnID = "", strBtnDeleteID = "", strValues = "";
            string strAdd = "";
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
                        strAdd = "";
                        if (intColCount < intActualColumnDisplayCount)
                        {
                            HtmlTableCell tdCol = new HtmlTableCell();
                            strRowType = dv[intRowCount]["ROWTYPE"].ToString();
                            if (strRowType == "HEADER")
                            {
                                tdCol.Attributes.Add("class", strCssClassHeader);
                                if (intRowCount == 0)
                                {
                                    if(intColCount == 0 || intColCount == (intActualColumnDisplayCount - 1))
                                    {
                                        tdCol.RowSpan = 2;
                                        strAdd = "ADD";

                                    }
                                    else if (intColCount == 1)
                                    {
                                        tdCol.ColSpan = intColCountMax - 4;
                                        strAdd = "ADD";
                                    }
                                }
                                if(intRowCount == 1)
                                {
                                    if (intColCount >= 1 && intColCount <= (intActualColumnDisplayCount - 2))
                                    {
                                        strAdd = "ADD";
                                    }
                                }

                                
                            }
                            else
                            {
                                tdCol.Attributes.Add("class", strCssClass);
                                if(strRowType == "FOOTER")
                                {
                                    tdCol.Attributes.Add("style", "background-color:yellow;");
                                }
                                strAdd = "ADD";
                            }

                            if (strAdd == "ADD")
                            {
                                trRow.Controls.Add(tdCol);
                                Label lblText = new Label();
                                strColor = dv[intRowCount]["color"].ToString();
                                strText = dv[intRowCount][intColCount].ToString();



                                lblText.Attributes.Add("style", "max-width:700px;color:" + strColor);

                                if(strText.Length > 15)
                                {
                                    if (intRowCount < 2)
                                    {
                                        lblText.Font.Size = FontUnit.Small;
                                    }
                                    else
                                    {
                                        lblText.Font.Size = FontUnit.XSmall;
                                    }
                                }
                                else
                                {
                                    lblText.Font.Size = FontUnit.Small;
                                }
                                lblText.Text = strText;
                                if (strText == "")
                                {
                                    lblText.Height = Unit.Pixel(18);
                                }
                                tdCol.Controls.Add(lblText);
                            }
                                
                        }
                    }

                    
                    strValues = "";
                }

            }

        }


        private void FillSupplier()
        {
            DataTable dtFillSupplier = new DataTable();
            dtFillSupplier = epDAL.GET_SUPPLIER_LOCATION().Tables[0];

            DataTable dtSupplier = new DataTable();


            dtSupplier.Columns.Add("supplier_id", typeof(string));
            dtSupplier.Columns.Add("supplier_name", typeof(string));
            foreach (DataRow row in dtFillSupplier.Rows)
            {
                String supplierid = (string)row[0];
                String suppliername = (string)row[1];
                dtSupplier.Rows.Add(supplierid, suppliername);
            }

            ddSupplier.DataSource = dtSupplier;
            ddSupplier.DataTextField = "supplier_name";
            ddSupplier.DataValueField = "supplier_id";

            ddSupplier.DataBind();
            ddSupplier.Items.Insert(0, "ALL");
        }


        private void FillAntenna()
        {
            DataTable dtFillAntenna = new DataTable();
            dtFillAntenna = epDAL.EMPTY_PCASE_GET_ANTENNA_CONVEYOR_RETURNABLES().Tables[0];

            DataTable dtAntenna = new DataTable();


            dtAntenna.Columns.Add("id", typeof(string));
            dtAntenna.Columns.Add("name", typeof(string));
            foreach (DataRow row in dtFillAntenna.Rows)
            {
                String id = (string)row[1];
                String name = (string)row[0];
                dtAntenna.Rows.Add(id, name);
            }

            ddAntenna.DataSource = dtAntenna;
            ddAntenna.DataTextField = "id";
            ddAntenna.DataValueField = "name";

            ddAntenna.DataBind();
            ddAntenna.Items.Insert(0, "ALL");
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
            
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void btnAutoRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            if (txtClick.Text == "1")
            {
                txtClick.Text = "0";
                lblStat.Text = "OFF";
                lblStat.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                txtClick.Text = "1";
                lblStat.Text = "ON";
                lblStat.ForeColor = System.Drawing.Color.Green;
            }
        }
    }
}