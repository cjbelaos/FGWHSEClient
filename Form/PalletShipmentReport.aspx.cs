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
using com.eppi.utils;


namespace FGWHSEClient.Form
{
    public partial class PalletShipmentReport : System.Web.UI.Page
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
                        txtContainerNo.Focus();
                        GetPalletShipmentCount();


                        if (Session["ContNo"] != null)
                        {
                            this.txtContainerNo.Text = Session["ContNo"].ToString();

                            if (Session["ODNo"] != null)
                            {
                                this.txtODNo.Text = Session["ODNo"].ToString();
                            }

                            Search(txtContainerNo.Text.Trim(), txtODNo.Text.Trim());
                            GetPalletShipmentCount();
                        }

                        



                    }
                    else
                    {
                        if (Request.Form["deleteConfirm"] != null)
                        {
                            if (Request.Form["deleteConfirm"].ToString().Equals("1"))
                            {

                                DeleteRecord();
                                Search(txtContainerNo.Text.Trim(), txtODNo.Text.Trim());
                                GetPalletShipmentCount();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void DeleteRecord()
        {
            try
            {
                string str = maint.DELETE_PALLET_SHIPMENT_VALIDATION(hidID.Value);

                if (str.Length > 0)
                {
                    MsgBox1.alert(str);
                }
                else
                {
                    MsgBox1.alert("Record Successfully Deleted.");
                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtContainerNo.Text.Trim() == "")
                {
                    MsgBox1.alert("Please Input Required Field.");
                    txtContainerNo.Focus();
                }
                else
                {
                    Search(txtContainerNo.Text.Trim(), txtODNo.Text.Trim());
                    GetPalletShipmentCount();
                    Session["ContNo"] = txtContainerNo.Text.Trim();
                    Session["ODNo"] = txtODNo.Text.Trim();
                }


            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        private int Search(string ContainerNo, string ODNo)
        {
            int intCount = 0;
            try
            {
                DataTable dt = new DataTable();
                DataView dv = new DataView();

                dv = maint.GET_PALLET_SHIPMENT_VALIDATION(txtContainerNo.Text.Trim(), txtODNo.Text.Trim()).Tables[0].DefaultView;
                dt = dv.Table;

                GrdPalletValidation.DataSource = dt;
                GrdPalletValidation.DataBind();
                intCount = dv.Count;

            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }

            return intCount;
        }




        protected void GrdPalletValidation_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            e.Row.Cells[9].Visible = false;

            int rownum = 0;
            rownum++;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (e.Row.Cells[8].Text == "X")
                {
                    e.Row.BackColor = System.Drawing.Color.IndianRed;
                    e.Row.ForeColor = System.Drawing.Color.White;
                }
                else if (e.Row.Cells[8].Text == "OK")
                {
                    e.Row.BackColor = System.Drawing.Color.LimeGreen;
                    e.Row.ForeColor = System.Drawing.Color.Black;
                }
                

            }
        }



        protected void GrdPalletValidation_DataBound(object sender, EventArgs e)
        {
            try
            {

                GridView grid = sender as GridView;

                if (grid != null)
                {
                    GridViewRow row = new GridViewRow(0, -1,
                    DataControlRowType.Header, DataControlRowState.Normal);

                    TableCell header = new TableHeaderCell();
                    header.ColumnSpan = 1;
                    header.Text = "";
                    header.Font.Size = 14;
                    header.BackColor = System.Drawing.Color.LightBlue;
                    header.BorderWidth = 1;
                    row.Cells.Add(header);

                    TableCell PDE = new TableHeaderCell();
                    PDE.ColumnSpan = 3;
                    PDE.Text = "GNS+ Data SHAREDB";
                    PDE.Font.Size = 14;
                    PDE.BackColor = System.Drawing.Color.LightBlue;
                    PDE.ForeColor = System.Drawing.Color.Black;
                    PDE.BorderWidth = 1;
                    row.Cells.Add(PDE);

                    TableCell Section = new TableHeaderCell();
                    Section.ColumnSpan = 4;
                    Section.Text = "WHSE System";
                    Section.Font.Size = 14;
                    Section.BackColor = System.Drawing.Color.CornflowerBlue;
                    Section.ForeColor = System.Drawing.Color.Black;
                    Section.BorderWidth = 1;
                    row.Cells.Add(Section);

              

                    Table t = grid.Controls[0] as Table;
                    if (t != null)
                    {
                        t.Rows.AddAt(0, row);
                    }
                }
        }
        catch (Exception ex)
        {
            MsgBox1.alert("An unexpected error has occured! " + ex.Message);
            Logger.GetInstance().Fatal(ex.Message);
        }
    }


        protected void GetPalletShipmentCount()
        {

            try
            {
                DataView dv = new DataView();
                dv = maint.GET_PALLET_SHIPMENT_COUNT(txtContainerNo.Text.Trim());

                lblPalletScanned.Text = dv[0]["CNT"].ToString();

            }
            catch (Exception ex)
            {
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);
                Logger.GetInstance().Fatal(ex.Message);
            }

        }



        protected void GrdPalletValidation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                strPageSubsystem = "FGWHSE_021";
                if (checkAuthority(strPageSubsystem))
                {
                    

                    string ID = GrdPalletValidation.Rows[e.RowIndex+1].Cells[9].Text.ToString();
                    string Pallet = GrdPalletValidation.Rows[e.RowIndex+1].Cells[5].Text.ToString();

                    hidID.Value = ID;

                    if (ID == "0")
                    {
                        MsgBox1.alert("No item to delete.");
                        Search(txtContainerNo.Text.Trim(), txtODNo.Text.Trim());
                    }
                    else
                    {
                        MsgBox1.confirm("Are you sure you want to delete Pallet: " + Pallet + "?", "deleteConfirm");
                        Search(txtContainerNo.Text.Trim(), txtODNo.Text.Trim());
                    }
                }
                else
                {
                    MsgBox1.alert("You are not authorized to delete this record.");
                    Search(txtContainerNo.Text.Trim(), txtODNo.Text.Trim());
                }


            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.Message);
                Logger.GetInstance().Fatal(ex.StackTrace);
                MsgBox1.alert(ex.Message);
            }
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
                            iRow = 0;
                        }

                        string strRole = dvSubsystem.Table.Rows[iRow]["Role"].ToString();

                        if (strRole != "")
                        {
                            strAccessLevel = strRole;
                        }
                    }
                }
                return isValid;
            }
            catch (Exception ex)
            {
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);

                isValid = false;
                return isValid;
            }
        }





        private void GenerateExcel()
        {
            string filename = "Pallet_Shipment_Report";
            //Turn off the view stateV 225 55
            this.EnableViewState = false;
            //Remove the charset from the Content-Type header
            Response.Charset = String.Empty;

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename='" + filename + "'.xls");
            Response.Charset = "";
            GrdPalletValidation.AllowPaging = false;
            this.Search(txtContainerNo.Text.Trim(), txtODNo.Text.Trim());
            // If you want the option to open the Excel file without saving then
            // comment out the line below
            // Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            GrdPalletValidation.Columns[0].Visible = false;
            GrdPalletValidation.Columns[9].Visible = false;

            

            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            divGrdPalletValidation.RenderControl(htmlWrite);
            

            //Append CSS file

            System.IO.FileInfo fi = new System.IO.FileInfo(Server.MapPath("../App_Themes/Stylesheet/Main.css"));
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.IO.StreamReader sr = fi.OpenText();
            while (sr.Peek() >= 0)
            {
                sb.Append(sr.ReadLine());
            }
            sr.Close();

            Response.Write("<html><head><style type='text/css'>" + sb.ToString() + "</style><head>" + stringWrite.ToString() + "</html>");

            stringWrite = null;
            htmlWrite = null;

            // Response.Write(stringWrite.ToString());

            Response.Flush();
            Response.End();
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        protected void btnExcelDownload_Click(object sender, EventArgs e)
        {
            if (txtContainerNo.Text.Trim() == "")
            {
                MsgBox1.alert("Please Input Required Field.");
                Search(txtContainerNo.Text.Trim(), txtODNo.Text.Trim());
            }
            else
            {
                GenerateExcel();
            }
            
        }

        protected void btnPrintReport_Click(object sender, EventArgs e)
        {

            string strURL = "Pallet_Shipment_Reports_Page.aspx?container=" + txtContainerNo.Text.Trim()+"&OD=" +txtODNo.Text.Trim();
            //strURL = Server.MapPath(strURL);
            //Response.Write("<script>");
            //Response.Write("window.open = '../" + strURL + "';");
            //Response.Write("</script>");


            //Response.Write("<script>");
            //Response.Write("window.location = 'Pallet_Shipment_Reports_Page.aspx&container=" + txtContainerNo.Text.Trim() + "&OD=" + txtODNo.Text.Trim());
            //Response.Write("</script>");

            //Response.Write("<script>");
            //Response.Write("window.location = '/Pallet_Shipment_Reports_Page.aspx&container=" + txtContainerNo.Text.Trim() + "&OD=" + txtODNo.Text.Trim());
            //Response.Write("</script>");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('"+strURL+"','_newtab');", true);
            //Response.Redirect("~/Form/Pallet_Shipment_Reports_Page.aspx?container=" + txtContainerNo.Text.Trim()+"&OD=" +txtODNo.Text.Trim());

            Search(txtContainerNo.Text.Trim(), txtODNo.Text.Trim());
        }


    }
}
