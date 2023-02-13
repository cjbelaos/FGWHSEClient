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
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using FGWHSEClient.CSharp;
namespace FGWHSEClient.Form
{
    public partial class Pallet_Shipment_Reports_Page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string strContainer = "",strOD = "";
                if (Request.QueryString["container"] != null)
                {
                    strContainer = Request.QueryString["container"].ToString();
                    if (Request.QueryString["OD"] != null)
                    {
                        strOD = Request.QueryString["OD"].ToString();
                    }

                    
                }
                
                if (!this.IsPostBack)
                {
                    ReportViewer1.AsyncRendering = false;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    FillData(strContainer, strOD);
                }

            }
            catch (Exception ex)
            {
                msgBox1.alert(ex.Message.ToString());
            }

        }

        private void FillData(string strContainerNo, string strODNo)
        {
            try
            {
                string strReportToolUsername = ConfigurationManager.AppSettings["reportToolUsername"].ToString();
                string strReportToolPassword = ConfigurationManager.AppSettings["reportToolPassword"].ToString();
                string strReportToolDomain = ConfigurationManager.AppSettings["reportToolDomain"].ToString();

                IReportServerCredentials irsc = new CustomReportCredentials(strReportToolUsername, strReportToolPassword, strReportToolDomain);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                //string strLot, strDateFrom, strDateTo = null;
                //strLot = txtLotNo.Text.Trim();
                //strDateFrom = txtFrom.Text.Trim();
                //strDateTo = txtTo.Text.Trim();

                ReportViewer1.ShowToolBar = true;
                ReportViewer1.Width = Unit.Percentage(100);
                ReportViewer1.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["reportServerUrl"].ToString());
                ReportViewer1.ServerReport.ReportPath = "/Reports/GENESYS/SD/Pallet Shipment Report";

                string url = ReportViewer1.ServerReport.ReportServerUrl.ToString();
                string path = ReportViewer1.ServerReport.ReportPath.ToString();
                List<ReportParameter> paramList = new List<ReportParameter>();
                paramList.Add(new ReportParameter("ContainerNo", strContainerNo, false));
                paramList.Add(new ReportParameter("ODNo", strODNo, false));
                //paramList.Add(new ReportParameter("strDateTo", strDateTo, false));
                ReportViewer1.ServerReport.SetParameters(paramList);
                ReportViewer1.ServerReport.Refresh();
            }
            catch (Exception ex)
            {
                msgBox1.alert(ex.Message.ToString());
            }
        }
    }
}
