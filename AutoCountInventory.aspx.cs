using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FGWHSEClient
{
    public partial class AutoCountInventory : System.Web.UI.Page
    {

        private string strConnPRMono;
        SqlConnection connPRMono;

        private static bool endStocksSaved;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.strConnPRMono = System.Configuration.ConfigurationManager.AppSettings["PRMonosys_ConnectionString"];
            connPRMono = new SqlConnection(strConnPRMono);

            if (!IsPostBack)
            {
                if (Session["Line"] != null) txtbxLine.Text = Session["Line"].ToString();
                if (Session["Partcode"] != null) txtbxPartCode.Text = Session["Partcode"].ToString();

                //TEXTBOX STYLE
                txtbxUserID.Style["text-align"] = "center";
                txtbxLine.Style["text-align"] = "center";
                txtbxPartCode.Style["text-align"] = "center";
                txtbxStrtstocks.Style["text-align"] = "center";
                txtbxPDelivered.Style["text-align"] = "center";
                txtbxPUsed.Style["text-align"] = "center";
                txtbxRStocks.Style["text-align"] = "center";
                txtbxEndStocks.Style["text-align"] = "center";
                txtbxVariance.Style["text-align"] = "center";

                
            }
        }

        private void totalElog()
        {
            try
            {
                Maintenance maint = new Maintenance();
                DataSet ds = new DataSet();


                ds = maint.TotalElog(txtbxLine.Text);

                DataTable dt = ds.Tables[0];

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Decimal TotalQty = Convert.ToDecimal(dt.Compute("SUM(quantity)", string.Empty));
                    txtbxPartCode.Text = ds.Tables[0].Rows[0]["itemcode"].ToString();
                    txtbxPDelivered.Text = TotalQty.ToString("0.#");
                }
                else
                {

                    //Response.Write("<script>alert('No E-LOG/EKANBAN data to show!');</script>");
                    displayELog.Text = "No E-LOG/EKANBAN data to show!";

                    txtbxPDelivered.Text = "0";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void totalElog2() // UPDATED
        
        {
            try
            {
                Maintenance maint = new Maintenance();
                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                //string date = "2022-05-12 02:05:20";

                ds1 = maint.getIventoryTransaction(lblReferenceNo.Text);
                ds = maint.TotalElog2(txtbxLine.Text, txtbxPartCode.Text, Convert.ToDateTime(ds1.Tables[0].Rows[0]["StartOfStocks_CreatedDate"].ToString()));
                //ds = maint.TotalElog2(txtbxLine.Text, txtbxPartCode.Text, Convert.ToDateTime(date));


                DataTable dt = ds.Tables[0];

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Decimal TotalQty = Convert.ToDecimal(dt.Compute("SUM(quantity)", string.Empty));
                    txtbxPartCode.Text = ds.Tables[0].Rows[0]["itemcode"].ToString();
                    txtbxPDelivered.Text = TotalQty.ToString("0.#");
                }
                else
                {

                    //Response.Write("<script>alert('No E-LOG/EKANBAN data to show!');</script>");
                    displayELog.Text = "No E-LOG/EKANBAN data to show!";

                    txtbxPDelivered.Text = "0";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataSet totalPartsUsed()
        {

            Maintenance maint = new Maintenance();
            DataSet ds1 = new DataSet();

            ds1 = maint.getIventoryTransaction(lblReferenceNo.Text);
            //DateTime date = DateTime.Now.AddDays(-1);

            SqlCommand cmd = new SqlCommand("SELECT			A.parts_cd AS PartsCode, " +
                "D.last_physical_line_cd As LineID," +
                //" convert(varchar, A.[finish_datetime], 120) as  [finish_datetime], " +
                "COUNT(A.parts_cd) as QTY FROM  " +

                "(SELECT * FROM[T_important_parts_result]  A WITH(NOLOCK)  " +
                "WHERE convert(varchar, A.[finish_datetime], 120) >= @date  " +
                "AND parts_cd = @itemcode  " +
                "AND A.[is_history] = '0' " +
                ")A  " +

                "LEFT OUTER JOIN[T_mecha] AS D WITH(NOLOCK) ON D.mecha_no = a.mecha_no  " +
                "WHERE D.last_physical_line_cd = @line  " +

                "group by    A.parts_cd, D.last_physical_line_cd " +
                " OPTION(MAXDOP 1, RECOMPILE); ", connPRMono);
            //"convert(varchar, A. [finish_datetime], 120)  " +
            //"order by convert(varchar, A. [finish_datetime], 120) asc", connPRMono);


            cmd.Parameters.Add("@itemcode", System.Data.SqlDbType.NVarChar);
            cmd.Parameters["@itemcode"].Value = txtbxPartCode.Text;

            cmd.Parameters.Add("@line", System.Data.SqlDbType.NVarChar);
            cmd.Parameters["@line"].Value = txtbxLine.Text;

            cmd.Parameters.Add("@date", System.Data.SqlDbType.DateTime);
            cmd.Parameters["@date"].Value = Convert.ToDateTime(ds1.Tables[0].Rows[0]["StartOfStocks_CreatedDate"].ToString());
            //cmd.Parameters["@date"].Value = Convert.ToDateTime(date);

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

            DataSet pd = new DataSet();

            pd = maint.TotalPDPacg2(txtbxLine.Text, txtbxPartCode.Text, Convert.ToDateTime(ds1.Tables[0].Rows[0]["StartOfStocks_CreatedDate"].ToString()));
            //pd = maint.TotalPDPacg2(txtbxLine.Text, txtbxPartCode.Text, Convert.ToDateTime(date));

            DataTable dtpd = pd.Tables[0];

            if (ds.Tables[0].Rows.Count > 0 && pd.Tables[0].Rows.Count > 0)
            {
                Decimal TotalPD = Convert.ToDecimal(dtpd.Compute("SUM(quantity)", string.Empty));
                Decimal TotalMono = Convert.ToDecimal(ds.Tables[0].Rows[0]["QTY"].ToString());
                Decimal TotalQTY = (TotalPD * -1) + TotalMono;

                txtbxPUsed.Text = TotalQTY.ToString();

            }
            else if (ds.Tables[0].Rows.Count == 0 && pd.Tables[0].Rows.Count > 0)
            {
                Decimal TotalPD = Convert.ToDecimal(dtpd.Compute("SUM(quantity)", string.Empty));
                Decimal TotalMono = 0;
                Decimal TotalQTY = (TotalPD * -1) + TotalMono;

                txtbxPUsed.Text = TotalQTY.ToString();

                //Response.Write("<script>alert('No MONOSYS data to show!');</script>");
                displayMsg.Text = "No MONOSYS data to show!";
            }
            else if (ds.Tables[0].Rows.Count > 0 && pd.Tables[0].Rows.Count == 0)
            {
                Decimal TotalPD = 0;
                Decimal TotalMono = Convert.ToDecimal(ds.Tables[0].Rows[0]["QTY"].ToString());
                Decimal TotalQTY = (TotalPD * -1) + TotalMono;

                txtbxPUsed.Text = TotalQTY.ToString();

                //Response.Write("<script>alert('No PD-PACG data to show!'); </script>");
                displayMsg.Text = "No PD-PACG data to show!";

            }
            else
            {

                //Response.Write("<script>alert('No PARTS USED data to show!');</script>");
                displayMsg.Text = "No PARTS USED data to show!";

                txtbxPUsed.Text = "0";
            }

            return ds;


        }
        protected void txtbxLine_TextChanged(object sender, EventArgs e)
        {
            txtbxStrtstocks.Enabled = true;
          
                if (checkPcode(true))
                {
                    txtbxStrtstocks.Focus();
                }
                else
                {
                    Response.Write("<script>alert('Partcode is not existing in this line! Kindly enter valid Partcode!');</script>");
                }
            

        }
        private void totalStocks()

        {
            try
            {
                //string ElogQty = txtbxPDelivered.Text.Trim();
                //string MonosysQty = txtbxPUsed.Text.Trim();
                //string StartStocks = txtbxStrtstocks.Text.Trim();
                string EndStocks = txtbxEndStocks.Text.Trim();
                string RemainingStocks = txtbxRStocks.Text.Trim();

                //Decimal Variance = Convert.ToDecimal(EndStocks) - (Convert.ToDecimal(StartStocks) +  Convert.ToDecimal(ElogQty)) + (Convert.ToDecimal(MonosysQty) * -1);
                Decimal Variance = Convert.ToDecimal(EndStocks) - Convert.ToDecimal(RemainingStocks);


                txtbxVariance.Text = Variance.ToString("0.#");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void totalRemainingStocks()

        {
            try
            {
                string ElogQty = txtbxPDelivered.Text.Trim();
                string MonosysQty = txtbxPUsed.Text.Trim();
                string StartStocks = txtbxStrtstocks.Text.Trim();
                string EndStocks = txtbxEndStocks.Text.Trim();

                Decimal RemainingStocks = (Convert.ToDecimal(StartStocks) + Convert.ToDecimal(ElogQty)) + (Convert.ToDecimal(MonosysQty) * -1);


                txtbxRStocks.Text = RemainingStocks.ToString("0.#");
               

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void Timer_Tick(object sender, EventArgs e)
        {

            if(txtbxStrtstocks.Text == "")
            {
                Timer.Enabled = false;
            }
            else
            {

                Session["Line"] = txtbxLine.Text;
                Session["Partcode"] = txtbxPartCode.Text;

                totalElog2();
                totalPartsUsed();
                totalRemainingStocks();
            }

        }
        protected void txtbxUserID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Maintenance maint = new Maintenance();
                DataSet ds = new DataSet();


                ds = maint.GetEmployeeName(txtbxUserID.Text);

                DataTable dt = ds.Tables[0];

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblName.Text = ds.Tables[0].Rows[0]["EmployeeName"].ToString();

                    txtbxUserID.Enabled = true;
                    txtbxPartCode.Enabled = true;
                    txtbxLine.Enabled = true;
                    txtbxUserID.Enabled = false;
                }
                else
                {

                    lblName.Text = "Employee No does not exists!";

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool checkPcode(bool success)
        {
            try
            {
                Maintenance maint = new Maintenance();
                DataSet ds = new DataSet();


                ds = maint.CheckPartCode(txtbxLine.Text, txtbxPartCode.Text);

                DataTable dt = ds.Tables[0];

                if (ds.Tables[0].Rows.Count > 0)
                {

                    txtbxStrtstocks.Enabled = true;
                    return success;
                }
                else
                {

                    txtbxPartCode.Text = "";
                    txtbxLine.Text = "";
                    txtbxPDelivered.Text = "";
                    txtbxPUsed.Text = "";
                    txtbxVariance.Text = "";
                    txtbxStrtstocks.Enabled = false;
                    txtbxEndStocks.Enabled = false;
                    success = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return success;
        }
        private void saveStocks()
        
        { 
            try
            {

                Maintenance maint = new Maintenance();
                DataSet ds1 = new DataSet();

                ds1 = maint.CreateRefNo();

                string strInsert = "";
                
                    string ReferenceNo = ds1.Tables[0].Rows[0]["ReferenceNo"].ToString();
                    string user = txtbxUserID.Text;
                    string username = lblName.Text;
                    string PartCode = txtbxPartCode.Text.Trim().ToUpper();
                    string Line = txtbxLine.Text.Trim().ToUpper();
                    int StartStocks = Convert.ToInt32(txtbxStrtstocks.Text);

                    strInsert = maint.SaveStocks(ReferenceNo, user, username, PartCode, Line, StartStocks);
                    if (strInsert=="")
                    {
                      Response.Write("<script>alert('Start of Shift Stocks Successfully Saved');</script>");
                    }
                    else
                    {

                    }
                   
                

                txtbxPartCode.Enabled = false;
                txtbxLine.Enabled = false;
                txtbxStrtstocks.Enabled = false;
                txtbxEndStocks.Enabled = true;
                lblReferenceNo.Text = ds1.Tables[0].Rows[0]["ReferenceNo"].ToString();
                txtbxRStocks.Text = "0";
                txtbxVariance.Text = "0";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void txtbxStrtstocks_TextChanged(object sender, EventArgs e)
        {
                long limit;
               
                if (long.TryParse(txtbxStrtstocks.Text, out limit))
                {
                    saveStocks();
                    totalElog2();
                    totalPartsUsed();
                    totalRemainingStocks();
                    txtbxEndStocks.Focus();
                    Timer.Enabled = true;
                }
                else
                {
                    txtbxStrtstocks.Text = "";
                }
        }
        protected void txtbxPartCode_TextChanged(object sender, EventArgs e)
        {
            txtbxLine.Focus();
        }
        protected void txtbxEndStocks_TextChanged(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                    long limit;

                    if (long.TryParse(txtbxStrtstocks.Text, out limit))
                    {
                        Timer.Enabled = false;

                        totalStocks();
                        Response.Write("<script>alert('Variance is " + txtbxVariance.Text + ");</script>");
                        txtbxEndStocks.Enabled = false;
                        endStocks();
                        Response.Write("<script>alert('End of Shift Stocks Successfully Saved!');</script>");
                        endStocksSaved = true;
                        Timer1.Enabled = true;
                    }
                    else
                    {
                        txtbxEndStocks.Text = "";
                    }
            }
            else
            {
                txtbxEndStocks.Text = "";
            }
        }
        private void endStocks()
        {
            try
            {

                Maintenance maint = new Maintenance();
                DataSet ds = new DataSet();

                    string ReferenceNo = lblReferenceNo.Text;
                    int TotalPartsDel = Convert.ToInt32(txtbxPDelivered.Text);
                    int TotalPartsUsed = Convert.ToInt32(txtbxPUsed.Text);
                    int EndStocks = Convert.ToInt32(txtbxEndStocks.Text);
                    int ActualStocks = Convert.ToInt32(txtbxRStocks.Text);
                    int Variance = Convert.ToInt32(txtbxVariance.Text);


                    ds = maint.UpdateInventoryTrans(ReferenceNo,TotalPartsDel,TotalPartsUsed,EndStocks, ActualStocks, Variance);

               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ClearPage()
        {
            try
            {
                if (endStocksSaved)
                {
                    
                    txtbxUserID.Text = "";
                    txtbxUserID.Enabled = true;

                    lblName.Text = "";

                    txtbxPartCode.Text = "";
                    txtbxPartCode.Enabled = false;

                    txtbxLine.Text = "";
                    txtbxLine.Enabled = false;

                    txtbxStrtstocks.Text = "";
                    txtbxStrtstocks.Enabled = false;

                    txtbxPDelivered.Text = "";
                    txtbxPDelivered.Enabled = false;

                    txtbxPUsed.Text = "";
                    txtbxPUsed.Enabled = false;

                    txtbxRStocks.Text = "";
                    txtbxRStocks.Enabled = false;

                    txtbxEndStocks.Text = "";
                    txtbxEndStocks.Enabled = false;

                    txtbxVariance.Text = "";
                    txtbxVariance.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            
            ClearPage();
            Timer1.Enabled = false;
           
        }
    }
}