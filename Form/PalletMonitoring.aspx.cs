using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.eppi.utils;
using System.Xml.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using FGWHSEClient.LdapService;
using FGWHSEClient.LogInService;


namespace FGWHSEClient.Form
{
    public partial class PalletMonitoring : System.Web.UI.Page
    {

        public string strPageSubsystem = "";
        public string strAccessLevel = "";

        Maintenance maint = new Maintenance();

        protected Maintenance maintLB = new Maintenance();
        
        protected DataView dvUnallocatedContent;
        protected DataView dvLane;
        protected DataView dvAnyLane;
        protected DataView dvAnyLaneContent;

        protected DataView dvLocType;

        protected void ddWH_SelectedIndexChanged(object sender, EventArgs e)
        {
            locationGroup();
            getDisplayValues();
            if (ddWH.Text == "CY")
            {
                display1.Attributes.Add("style", "display:none");
                display2.Attributes.Add("style", "display:none");
            }
            else
            {
                display1.Attributes.Add("style", "display:compact");
                display2.Attributes.Add("style", "display:compact");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"].ToString() == "GUEST")
            {
                if (!this.IsPostBack)
                {
                    getWarehouse();
                    locationGroup();
                    getDisplayValues();
                    getPathName();
                }

                lblRefresh.Text = DateTime.Now.ToString();
            }

            else if (Session["UserName"] == null)
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
                    getWarehouse();
                    locationGroup();
                    getDisplayValues();
                    getPathName();
                }

                lblRefresh.Text = DateTime.Now.ToString();
                
            }

        }


        public void getDisplayValues()
        {
            dvUnallocatedContent = maint.GET_PALLETS_UNALLOCATED_FILTERED(ddWH.Text,rdbDisplayMode.SelectedValue.ToString()).Tables[0].DefaultView;
            dvAnyLane = maint.GET_LOCATION_BY_LOCATION_TYPE_ID(ddLocGrp.Text, ddWH.Text).Tables[0].DefaultView;
            dvLocType = maint.GET_LOCATION_GROUP(ddLocGrp.Text).Tables[0].DefaultView;
        }
        private void getWarehouse()
        {
            DataTable dtWH = new DataTable();



            dtWH = maint.GET_GET_WAREHOUSE().Tables[0];

            DataTable dtnewWH = new DataTable();


            dtnewWH.Columns.Add("warehouse_id", typeof(string));
            dtnewWH.Columns.Add("warehouse_name", typeof(string));
           

            foreach (DataRow row in dtWH.Rows)
            {

                String WHID = (string)row[0];
                String WHNAME = (string)row[1];
                dtnewWH.Rows.Add(WHID, WHNAME);
            }
      
                ddWH.DataSource = dtnewWH;
                ddWH.DataTextField = "warehouse_name";
                ddWH.DataValueField = "warehouse_id";
                ddWH.DataBind();
            
        }


        public string getPathName()
        {
            if (rdbDisplayMode.SelectedIndex == 0)
            {
                return imgLegend.ImageUrl = "~/Image/ColorLegend.PNG";
            }
            else if (rdbDisplayMode.SelectedIndex == 1)
            {
                return imgLegend.ImageUrl = "~/Image/Legend_Expiration.PNG";
            }
            else if (rdbDisplayMode.SelectedIndex == 2)
            {
                return imgLegend.ImageUrl = "~/Image/Legend_FIFO.PNG";
            }
            else
            {
                return imgLegend.ImageUrl = "~/Image/ColorLegend.PNG";
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
                        }

                        string strRole = dvSubsystem.Table.Rows[iRow]["Role"].ToString();

                        if (strRole != "")
                        {
                            strAccessLevel = strRole;
                        }

                    }
                }

                if (Session["UserName"].ToString() == "GUEST")
                {
                    isValid = true;
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



        private void locationGroup()
        {
            DataTable dtLOC = new DataTable();



            dtLOC = maint.GET_LOCATION_GROUP_LIST(ddWH.Text).Tables[0];

            DataTable dtnewLOC = new DataTable();


            dtnewLOC.Columns.Add("location_type_id", typeof(string));
            dtnewLOC.Columns.Add("location_type_name", typeof(string));
            
            if (dtLOC.Rows.Count > 1)
            {
                dtnewLOC.Rows.Add("All", "All");
            }
            foreach (DataRow row in dtLOC.Rows)
            {

                String LOCID = (string)row[0];
                String LOCNAME = (string)row[1];
                dtnewLOC.Rows.Add(LOCID, LOCNAME);
            }

            ddLocGrp.DataSource = dtnewLOC;
            ddLocGrp.DataTextField = "location_type_name";
            ddLocGrp.DataValueField = "location_type_id";
            ddLocGrp.DataBind();
           
        }

        public int GET_PACKAGE()
        {
            DataTable dt = new DataTable();
            DataView dv = new DataView();


            dv = maint.GET_PACKAGE_LIST(ddLocGrp.Text, ddWH.Text).Tables[0].DefaultView;

            dt = dv.Table;
            grdExport.DataSource = dt;
            grdExport.DataBind();

            grdExport.DataSource = dt;
            grdExport.DataBind();

            return dv.Count;
        }
        public int GET_CONTENT()
        {
            
            DataTable dt = new DataTable();
            DataView dv = new DataView();


            dv = maint.GET_CONTENT_LIST(ddLocGrp.Text, ddWH.Text).Tables[0].DefaultView;

            dt = dv.Table;
            grdExport.DataSource = dt;
            grdExport.DataBind();

            grdExport.DataSource = dt;
            grdExport.DataBind();

            return dv.Count;
            
        }

        public void ExportToExcel(string fname, string  exportType)
        {
            int exportDataCount = 0;
            string filename = fname +"_"+ DateTime.Now.ToString("yyyyMMddHHmmss");
            //msgBox.alert(filename);
            //Turn off the view stateV 225 55
            this.EnableViewState = false;
            //Remove the charset from the Content-Type header
            Response.Charset = String.Empty;

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
            Response.Charset = "";
            //grdTasks.AllowPaging = false;
            if (exportType == "package")
            {
               exportDataCount = this.GET_PACKAGE();
            }
            else
            {
                exportDataCount = this.GET_CONTENT();
            }

            //if (exportDataCount == 0)
            //{
            //    Response.Write("<script>");
            //    Response.Write("alert('No data found!');");
            //    Response.Write("</script>");
               
            //    return;
            //}
            // If you want the option to open the Excel file without saving then
            // comment out the line below
            // Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            grdExport.RenderControl(htmlWrite);

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
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

      

        protected void btnPackage_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToExcel("Package_List", "package");
            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message.ToString());
            }
        }

        protected void btnContent_Click(object sender, EventArgs e)
        {
            ExportToExcel("Content_List", "content");
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            getDisplayValues();
            getPathName();
        }

        protected void ddLocGrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            getDisplayValues();
        }

       

    }
}
