using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;


namespace FGWHSEClient.Form
{
    public partial class ReportShipmentStatus : System.Web.UI.Page
    {
        Maintenance maint = new Maintenance();
        public string strPageSubsystem = "";
        public string strAccessLevel = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
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

                    if (!this.IsPostBack)
                    {
                        txtDateFrom.Text = DateTime.Now.ToString("MM/dd/yyyy");
                        txtDateTo.Text = DateTime.Now.ToString("MM/dd/yyyy");

                        DataView dv = maint.PICKING_GET_SHIPMENT_STATUS_GRAPH(Convert.ToDateTime(txtDateFrom.Text.ToString()), Convert.ToDateTime(txtDateTo.Text.ToString()));

                        if (dv.Count > 0)
                        {
                            chartShipmentStatus.Visible = true;

                            doCreateChart(dv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message);
            }
        }


        private void HideChart()
        {
            try
            {
                chartShipmentStatus.Visible = false;
            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message);
            }

        }


        private void doCreateChart(DataView dv)
        {
            if (dv.Count > 0)
            {

                DataRowCollection dc = dv.Table.Rows;
                // GET SETTINGS
                Maintenance maint = new Maintenance();
                chartShipmentStatus.Palette = ChartColorPalette.BrightPastel;
                chartShipmentStatus.BackGradientStyle = GradientStyle.TopBottom;
                chartShipmentStatus.BorderlineWidth = 2;
                chartShipmentStatus.BorderlineDashStyle = ChartDashStyle.Solid;
                chartShipmentStatus.BackColor = System.Drawing.Color.FromArgb(239, 243, 251);
                chartShipmentStatus.BorderlineColor = System.Drawing.Color.FromArgb(26, 59, 105);
                chartShipmentStatus.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;

           
                //Legend
                chartShipmentStatus.Legends["Legend1"].IsTextAutoFit = false;
                chartShipmentStatus.Legends["Legend1"].Docking = Docking.Top;
                chartShipmentStatus.Legends["Legend1"].Enabled = true;
                chartShipmentStatus.Legends["Legend1"].Alignment = System.Drawing.StringAlignment.Center;
                chartShipmentStatus.Legends["Legend1"].BorderColor = System.Drawing.Color.Black;
                chartShipmentStatus.Legends["Legend1"].BackGradientStyle = GradientStyle.TopBottom;
                chartShipmentStatus.Legends["Legend1"].BackColor = System.Drawing.Color.LightSteelBlue;
                chartShipmentStatus.ChartAreas["ChartArea1"].BackSecondaryColor = System.Drawing.Color.FromArgb(220, 228, 239);
                chartShipmentStatus.ChartAreas["ChartArea1"].BackColor = System.Drawing.Color.LightSteelBlue;
                chartShipmentStatus.ChartAreas["ChartArea1"].BackGradientStyle = GradientStyle.HorizontalCenter;
                chartShipmentStatus.ChartAreas["ChartArea1"].Area3DStyle.Rotation = 10;
                chartShipmentStatus.ChartAreas["ChartArea1"].Area3DStyle.Perspective = 10;
                chartShipmentStatus.ChartAreas["ChartArea1"].Area3DStyle.Inclination = 15;
                chartShipmentStatus.ChartAreas["ChartArea1"].Area3DStyle.IsRightAngleAxes = false;
                chartShipmentStatus.ChartAreas["ChartArea1"].Area3DStyle.WallWidth = 0;
                chartShipmentStatus.ChartAreas["ChartArea1"].Area3DStyle.IsClustered = false;
                chartShipmentStatus.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;
                chartShipmentStatus.ChartAreas["ChartArea1"].AxisX.MajorTickMark.Interval = 1;
                //chartShipmentStatus.AxisX.IntervalType = DateTimeIntervalType.Days;
                chartShipmentStatus.ChartAreas["ChartArea1"].AxisX.Interval = 0;
                chartShipmentStatus.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
                chartShipmentStatus.ChartAreas["ChartArea1"].AxisX.IsLabelAutoFit = false;
                chartShipmentStatus.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 90;
                chartShipmentStatus.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;
                //chartShipmentStatus.ChartAreas["ChartArea1"].AxisX.Title = "Month";
                chartShipmentStatus.ChartAreas["ChartArea1"].AxisX.TitleFont = new System.Drawing.Font("Calibri", 15f, System.Drawing.FontStyle.Bold);
                chartShipmentStatus.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                chartShipmentStatus.ChartAreas["ChartArea1"].AxisY.LineColor = System.Drawing.Color.DeepSkyBlue;

               
                //chartShipmentStatus.ChartAreas["ChartArea1"].AxisY.Title = "QTY.";
             

                chartShipmentStatus.ChartAreas["ChartArea1"].AxisY.TitleFont = new System.Drawing.Font("Calibri", 13f, System.Drawing.FontStyle.Bold);
                chartShipmentStatus.ChartAreas["ChartArea1"].InnerPlotPosition.Width = 99f;
                chartShipmentStatus.ChartAreas["ChartArea1"].InnerPlotPosition.Height = 77f;
                chartShipmentStatus.ChartAreas["ChartArea1"].InnerPlotPosition.X = 3f;
                chartShipmentStatus.ChartAreas["ChartArea1"].InnerPlotPosition.Y = 3f;

                // MINIMUM AND MAXIMUM
                Series Completed = new Series("Completed");
                Series Ongoing = new Series("Ongoing");
                Series WithProblem = new Series("With Problem");
                Series Cancelled = new Series("Cancelled");

                chartShipmentStatus.Titles["Title1"].Docking = Docking.Top;
                chartShipmentStatus.Titles["Title1"].Font = new System.Drawing.Font("Calibri", 18f,
                System.Drawing.FontStyle.Bold);
                //chartShipmentStatus.Titles["Title1"].Text = "Today's PT Result - " + DateTime.Now.ToString("MMMM dd, yyyy");


                
                chartShipmentStatus.Titles["Title1"].Text = "Shipment Status by OD No.";




                chartShipmentStatus.Series.Add(Completed);
                chartShipmentStatus.Series.Add(Ongoing);
                chartShipmentStatus.Series.Add(WithProblem);
                chartShipmentStatus.Series.Add(Cancelled);



                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {

                    Completed.ChartType = SeriesChartType.StackedColumn;
                    Completed.Points.AddXY(dc[i]["MonthDays"].ToString(), dc[i]["Completed"].ToString());
                    Completed.ToolTip = "#VALY{}";

                    Ongoing.ChartType = SeriesChartType.StackedColumn;
                    Ongoing.Points.AddXY(dc[i]["MonthDays"].ToString(), dc[i]["Ongoing"].ToString());
                    Ongoing.ToolTip = "#VALY{}";

                    WithProblem.ChartType = SeriesChartType.StackedColumn;
                    WithProblem.Points.AddXY(dc[i]["MonthDays"].ToString(), dc[i]["WithProblem"].ToString());
                    WithProblem.ToolTip = "#VALY{}";

                    Cancelled.ChartType = SeriesChartType.StackedColumn;
                    Cancelled.Points.AddXY(dc[i]["MonthDays"].ToString(), dc[i]["Cancelled"].ToString());
                    Cancelled.ToolTip = "#VALY{}";

                }
                Completed["PixelPointWidth"] = "25";
                Completed.BorderWidth = 3;
                // Completed.ShadowOffset = 1;
                Completed.Color = System.Drawing.Color.RoyalBlue;


                Ongoing["PixelPointWidth"] = "25";
                Ongoing.BorderWidth = 3;
                // Ongoing.ShadowOffset = 1;
                Ongoing.Color = System.Drawing.Color.LimeGreen;


                WithProblem["PixelPointWidth"] = "25";
                WithProblem.BorderWidth = 3;
                // WithProblem.ShadowOffset = 1;
                WithProblem.Color = System.Drawing.Color.Red;


                Cancelled["PixelPointWidth"] = "25";
                Cancelled.BorderWidth = 3;
                // Cancelled.ShadowOffset = 1;
                Cancelled.Color = System.Drawing.Color.Black;


                chartShipmentStatus.ChartAreas["ChartArea1"].AxisY.LabelStyle.Font = new Font("Calibri", 10, FontStyle.Regular);
                chartShipmentStatus.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new Font("Calibri", 10, FontStyle.Bold);
                chartShipmentStatus.Visible = true;



                //foreach (Series series in chartShipmentStatus.Series)
                //{

                //    //add here 03/14/2016
                //    for (int pointIndex = 0; pointIndex < series.Points.Count; pointIndex++)
                //    {
                //        series.Points[pointIndex].Url = "ChartDetails.aspx?Category=" + ddlCategory.SelectedValue.ToString() + "&Date=" + series.Points[pointIndex].AxisLabel + dt.Table.Rows[0][0].ToString() + "&Status=" + series.Name;
                //        //series.Points[pointIndex].Url = "MainHome.aspx?Graph=2&Section=" + ddlSection.SelectedItem.Text.Trim() + "&Plant=" + ddlPlant.SelectedItem.Text.Trim() + "&PartCode=" + ddlPcode.SelectedValue.Trim() + "&Machine=" + ddlMachine.SelectedItem.Text.Trim() + "&Frequency=" + ddlFrequency.SelectedItem.Text.Trim() + "&FDate=" + txtDateFrom.Text.Trim() + "&TDate=" + txtDateTo.Text.Trim() + "&SDate=''&Status=" + series.Points[pointIndex].AxisLabel;
                //    }
                //    //to here 03/14/2016
                //}


            }

            else
            {
                MsgBox1.alert("No Data Found.");
            }
        }

       
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dv = new DataView();


                if (txtDateTo.Text != "" && txtDateFrom.Text != "")
                {

                    DateTime dtDateFrom;
                    DateTime dtDateTo;

                    dtDateFrom = Convert.ToDateTime(txtDateFrom.Text);
                    dtDateTo = Convert.ToDateTime(txtDateTo.Text);

                    if ((Convert.ToDateTime(txtDateTo.Text) - Convert.ToDateTime(txtDateFrom.Text)).TotalDays > 31)
                    {
                        MsgBox1.alert("Date Range must be within 31 days!");
                    }
                    else
                    {
                        dv = maint.PICKING_GET_SHIPMENT_STATUS_GRAPH(Convert.ToDateTime(txtDateFrom.Text.ToString()), Convert.ToDateTime(txtDateTo.Text.ToString()));

                        if (dv.Count > 0)
                        {
                            chartShipmentStatus.Visible = true;
                            doCreateChart(dv);
                        }

                        else
                        {
                            HideChart();
                        }

                    }
                }
                else
                {

                    MsgBox1.alert("Please input Date");
                    txtDateFrom.Focus();
                }

            }
            catch (Exception ex)
            {

                MsgBox1.alert(ex.Message);
            }
        }




    }
}
