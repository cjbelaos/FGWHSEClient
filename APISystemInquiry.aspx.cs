using FGWHSEClient.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FGWHSEClient.Form
{
    public partial class APISystemInquiry : System.Web.UI.Page
    {
        private string strConnPRMono;
        SqlConnection connPRMono;


        static Connection MyConnection = new Connection();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.strConnPRMono = System.Configuration.ConfigurationManager.AppSettings["PRMonosys_ConnectionString"];
            connPRMono = new SqlConnection(strConnPRMono);


            if (!this.IsPostBack)
            {


            }
        }
        private void filteredElogGV() //OLD COMPUTATION
        {
            try
            {
                Maintenance maint = new Maintenance();
                DataSet ds = new DataSet();
                DataSet ds2 = new DataSet();


                ds = maint.gvInventory(Convert.ToDateTime(txtbxFromDate.Text), Convert.ToDateTime(txtbxToDate.Text), txtbxLineID.Text, txtbxModel.Text, txtbxPartCode.Text);
                ds2 = maint.gvInventory13(Convert.ToDateTime(txtbxFromDate.Text), Convert.ToDateTime(txtbxToDate.Text), txtbxLineID.Text, txtbxModel.Text, txtbxPartCode.Text);

                DataTable dtds = ds.Tables[0];
                DataTable dtds2 = ds2.Tables[0];

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Decimal TotalElog = Convert.ToDecimal(dtds.Compute("SUM(quantity)", string.Empty));
                    txtbxTElogQty.Text = TotalElog.ToString();

                }
                else if (ds2.Tables[0].Rows.Count > 0)
                {
                    Decimal TotalElog = Convert.ToDecimal(dtds2.Compute("SUM(quantity)", string.Empty));
                    txtbxTElogQty.Text = TotalElog.ToString();
                }
                else
                {
                    Response.Write("<script>alert('No E-LOG/EKANBAN data to show!');</script>");

                    txtbxTElogQty.Text = "0";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void btnView_Click(object sender, EventArgs e)
        {

            if (ddlDivision.SelectedItem.Text == "PR")
            {
                exportData();
            }
            else if (ddlDivision.SelectedItem.Text == "VP")
            {
                Response.Write("<script>alert('On going development!');</script>");
            }
            else
            {
                Response.Write("<script>alert('Please select division!');</script>");
            }
        }
        public DataSet filteredPRMonosysGV() //OLD COMPUTATION
        {
            SqlCommand cmd = new SqlCommand("SELECT A.parts_cd AS PartsCode, " +
                "B.[product_name_eng] AS PrtsCdDescription, " +
                "B.[model_cd] AS ModelCode, " +
                "C.[model_name_eng] AS ModelName, " +
                "D.last_physical_line_cd As LineID, " +
                "convert(varchar, A.[finish_datetime], 101) as  [finish_datetime], " +
                "COUNT(A.parts_cd) as QTY FROM " +

                "(SELECT * FROM[T_important_parts_result]  A WITH(NOLOCK) " +
                "WHERE convert(varchar, A.[finish_datetime], 101) >= @txstart " +
                "AND convert(varchar, A.[finish_datetime], 101) <= @txend " +
                "AND parts_cd = @itemcode " +
                "AND A.[is_history] = '0')a " +

                "LEFT OUTER JOIN [M_product] AS B ON B.[product_cd] = A.parts_cd " +
                "LEFT OUTER JOIN[M_model] AS C ON C.[model_cd] = B.[model_cd] " +
                "LEFT OUTER JOIN[T_mecha] AS D ON D.mecha_no = a.mecha_no 		" +

                "WHERE (D.last_physical_line_cd = @lineID OR @lineID = '') " +
                "AND (B.[model_cd] = @modelID OR @modelID = '')  " +
                "group by 	A.parts_cd, B.[product_name_eng], B.[model_cd], C.[model_name_eng], D.last_physical_line_cd, convert(varchar, A. [finish_datetime], 101) " +
                "order by convert(varchar, A. [finish_datetime], 101) asc", connPRMono);


            cmd.Parameters.Add("@txstart", System.Data.SqlDbType.DateTime);
            cmd.Parameters["@txstart"].Value = Convert.ToDateTime(txtbxFromDate.Text);

            cmd.Parameters.Add("@txend", System.Data.SqlDbType.DateTime);
            cmd.Parameters["@txend"].Value = Convert.ToDateTime(txtbxToDate.Text);

            cmd.Parameters.Add("@lineID", System.Data.SqlDbType.NVarChar);
            cmd.Parameters["@lineID"].Value = txtbxLineID.Text;

            cmd.Parameters.Add("@modelID", System.Data.SqlDbType.NVarChar);
            cmd.Parameters["@modelID"].Value = txtbxModel.Text;

            cmd.Parameters.Add("@itemcode", System.Data.SqlDbType.NVarChar);
            cmd.Parameters["@itemcode"].Value = txtbxPartCode.Text;

            cmd.CommandTimeout = 0;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                connPRMono.Open();
                da.Fill(ds);
            }
            catch (SqlException sqlex) { throw sqlex; }
            catch (Exception ex) { throw ex; }

            finally
            {
                connPRMono.Close();
            }


            if (ds.Tables[0].Rows.Count > 0)
            {
                Decimal TotalMono = Convert.ToDecimal(ds.Tables[0].Rows[0]["QTY"].ToString());
                txtbxTMonoQty.Text = TotalMono.ToString();
            }
            else
            {
                Response.Write("<script>alert('No MONOSYS data to show!');</script>");

                txtbxTMonoQty.Text = "0";
            }

            return ds;


        }
        private void filteredPRPDPACGgv() //OLD COMPUTATION
        {
            try
            {
                Maintenance maint = new Maintenance();
                DataSet ds = new DataSet();
                DataSet ds2 = new DataSet();


                ds = maint.gvPACGInventory(Convert.ToDateTime(txtbxFromDate.Text), Convert.ToDateTime(txtbxToDate.Text), txtbxLineID.Text, txtbxModel.Text, txtbxPartCode.Text);
                ds2 = maint.gvPACGInventory13(Convert.ToDateTime(txtbxFromDate.Text), Convert.ToDateTime(txtbxToDate.Text), txtbxLineID.Text, txtbxModel.Text, txtbxPartCode.Text);

                DataTable dtds = ds.Tables[0];
                DataTable dtds2 = ds2.Tables[0];

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Decimal TotalPD = Convert.ToDecimal(dtds.Compute("SUM(quantity)", string.Empty));
                    txtbxTPDQty.Text = TotalPD.ToString();

                }
                else if (ds2.Tables[0].Rows.Count > 0)
                {
                    Decimal TotalPD = Convert.ToDecimal(dtds2.Compute("SUM(quantity)", string.Empty));
                    txtbxTPDQty.Text = TotalPD.ToString();
                }
                else
                {
                    Response.Write("<script>alert('No PD-PACG data to show!');</script>");

                    txtbxTPDQty.Text = "0";


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void totalQtyGv() //OLD COMPUTATION
        {
            try
            {
                string ElogQty = txtbxTElogQty.Text.Trim();
                string MonosysQty = txtbxTMonoQty.Text.Trim();
                string PDQty = txtbxTPDQty.Text.Trim();



                //int TotalQty = Convert.ToInt32(ElogQty) + (Convert.ToInt32(MonosysQty) * -1) + Convert.ToInt32(PDQty);
                Decimal TotalQty = Convert.ToDecimal(ElogQty) + (Convert.ToDecimal(MonosysQty) * -1) + Convert.ToDecimal(PDQty);



                string Total = TotalQty.ToString();

                DataTable dt = new DataTable();
                dt.Columns.Add("SYSTEM");
                dt.Columns.Add("QTY");
                int col = dt.Columns.Count;
                dt.Rows.Add("ELOG", ElogQty);
                dt.Rows.Add("MONOSYS", MonosysQty);
                dt.Rows.Add("PD-PACG", PDQty);
                dt.Rows.Add("PD STOCKS", Total);

                gvInventory.DataSource = dt;
                gvInventory.DataBind();


                //ADD TOP HEADER
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                TableHeaderCell cell = new TableHeaderCell();

                cell = new TableHeaderCell();
                cell.ColumnSpan = 2;
                cell.Attributes["style"] = "background-color:#003399;text-align:center;Font-size:13px";
                cell.Text = "TOTAL QUANTITY";
                row.Controls.Add(cell);

                gvInventory.HeaderRow.Parent.Controls.AddAt(0, row);



            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void exportData()
        {
            try
            {
                Maintenance maint = new Maintenance();
                DataSet ds = new DataSet();

                ds = maint.gvInventoryLatest(Convert.ToDateTime(txtbxFromDate.Text), Convert.ToDateTime(txtbxToDate.Text), txtbxLineID.Text, txtbxModel.Text, txtbxPartCode.Text);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvInventory.DataSource = ds;
                    gvInventory.DataBind();

                }
                else
                {
                    gvInventory.DataSource = null;
                    gvInventory.DataBind();
                    Response.Write("<script>alert('No data found!');</script>");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvInventory.Rows.Count > 0)
            {
                exportData();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=TotalQty: " + txtbxPartCode.Text + ".xls");
                Response.ContentType = "application/vnd.ms-excel";
                Response.Charset = "";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(stringWrite);
                gvInventory.RenderControl(htw);
                Response.Write(stringWrite.ToString());
                Response.Flush();
                Response.End();
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void gvInventory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //ALIGNMENT
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Center;

                    e.Row.Cells[0].VerticalAlign = VerticalAlign.Middle;
                    e.Row.Cells[1].VerticalAlign = VerticalAlign.Middle;
                    e.Row.Cells[2].VerticalAlign = VerticalAlign.Middle;
                    e.Row.Cells[3].VerticalAlign = VerticalAlign.Middle;
                    e.Row.Cells[4].VerticalAlign = VerticalAlign.Middle;
                    e.Row.Cells[5].VerticalAlign = VerticalAlign.Middle;
                    e.Row.Cells[6].VerticalAlign = VerticalAlign.Middle;
                    e.Row.Cells[7].VerticalAlign = VerticalAlign.Middle;
                    e.Row.Cells[8].VerticalAlign = VerticalAlign.Middle;
                    e.Row.Cells[9].VerticalAlign = VerticalAlign.Middle;
                    e.Row.Cells[10].VerticalAlign = VerticalAlign.Middle;
                    e.Row.Cells[11].VerticalAlign = VerticalAlign.Middle;
                    e.Row.Cells[12].VerticalAlign = VerticalAlign.Middle;

                    //UPPERCASE
                    e.Row.Cells[2].Text = e.Row.Cells[2].Text.ToUpper();
                    e.Row.Cells[3].Text = e.Row.Cells[3].Text.ToUpper();
                    e.Row.Cells[4].Text = e.Row.Cells[4].Text.ToUpper();
                    e.Row.Cells[5].Text = e.Row.Cells[5].Text.ToUpper();

                    //PD PACG AND  TOTAL PARTS USED
                    Maintenance maint = new Maintenance();
                    DataSet ds = new DataSet();

                    ds = maint.gvPACGInventory(Convert.ToDateTime(txtbxFromDate.Text), Convert.ToDateTime(txtbxToDate.Text), txtbxLineID.Text, txtbxModel.Text, txtbxPartCode.Text);

                    DataTable dtds = ds.Tables[0];

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Decimal TotalPD = Convert.ToDecimal(dtds.Compute("SUM(quantity)", string.Empty));
                        Decimal TotalPUsed = Convert.ToDecimal(e.Row.Cells[8].Text) + TotalPD;


                        e.Row.Cells[9].Text = TotalPD.ToString();
                        e.Row.Cells[8].Text = TotalPUsed.ToString();

                    }
                    else
                    {
                        e.Row.Cells[8].Text = "0";
                    }
                    
                    // VARIANCE STYLE
                    if (Convert.ToDecimal(e.Row.Cells[12].Text) < 0 )
                    {
                        e.Row.Cells[12].ForeColor = System.Drawing.Color.Red;
                    }

                    //FOR NULL ITEMS
                    if (e.Row.Cells[1].Text == "&nbsp;")
                    {
                        e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[7].ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[8].ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[9].ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[10].ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[11].ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[12].ForeColor = System.Drawing.Color.Red;
                    }

                    if (e.Row.Cells[4].Text == "&NBSP;")
                    {
                        e.Row.Cells[4].Text = "";
                    }



                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}