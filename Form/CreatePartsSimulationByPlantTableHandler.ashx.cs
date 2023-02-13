using System;
using System.Data;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using FGWHSEClient.DAL;
using FGWHSEClient.Form;

namespace FGWHSEClient.Form
{
    /// <summary>
    /// Summary description for CreatePartsSimulationByPlantTableHandler
    /// </summary>
    public class CreatePartsSimulationByPlantTableHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public void createTableData(DataTable dt, HtmlTable tbTable, int intActualColumnDisplayCount, int intDataColStart, int intDataColEnd, string strCssClass, string strCssClassHeader)
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

        public void GetPartsSimulationByPlant(string strPLANT, string strPARTS, string strVENDORS)
        {
            DALPSI_PLANT DPSI = new DALPSI_PLANT();
            DataTable dt = DPSI.PSI_GET_PARTSSIMULATION_BY_PLANT(strPLANT, strPARTS, strVENDORS);
            var StartDate = dt.Rows[0][209].ToString();
            for (var i = 11; i <= 208;)
            {
                for (int j = 0; j <= 197; j++)
                {
                    if (i == 11)
                    {
                        dt.Columns[i].ColumnName = StartDate;
                    }
                    else
                    {
                        DateTime Date = Convert.ToDateTime(StartDate);
                        dt.Columns[i].ColumnName = Date.AddDays(j).ToString("MM/dd/yyyy");
                    }
                    i++;
                }
            }
            dt.Columns.Remove("FIRSTCOLUMNNAME");
            DataTable newDT = dt;

            //createTableData(dt, tbList, 8, 2, 3, "tableDetailContainer", "TableHeadContainerRFID");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}