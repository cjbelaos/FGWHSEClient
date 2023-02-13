using com.eppi.utils;
using FGWHSEClient.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OleDb;
using System.IO;
using System.Collections.Generic;

using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

using System.Threading;
namespace FGWHSEClient.Form
{
    public partial class EmptyBoxBoard : System.Web.UI.Page
    {
        EmptyPcaseDAL epDAL = new EmptyPcaseDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.MaintainScrollPositionOnPostBack = true;

            if (!this.IsPostBack)
            {
                
                pnlHidden.Attributes.Add("style", "display:none");
                
            }
            getDisplay();
        }

        public void getDisplay()
        {

            lblUpdate.Text = "Updated as of : "+ DateTime.Now.ToString();
            DataSet ds = epDAL.GET_EMPTY_PCASE_BOX_BOARD();
            DataView dvOverAll = ds.Tables[0].DefaultView;
            DataView dvInside = ds.Tables[1].DefaultView;
            DataView dvOutSide = ds.Tables[2].DefaultView;
            DataView dvLegend = ds.Tables[3].DefaultView;




            string strHeader = "", strCapacity = "", strActual = "", strRate = "", strColorDisplay = "", strStatusDescription = "", strSupplierID = "";
            if (dvOverAll.Count > 0)
            {
                strHeader = "OVERALL";
                strCapacity = dvOverAll[0]["OVERALLCAPACITY"].ToString();
                strActual = dvOverAll[0]["ACTUALOVERALLCAPACITY"].ToString();
                strRate = dvOverAll[0]["PICKUPRATE"].ToString();
                strColorDisplay = dvOverAll[0]["COLORDISPLAY"].ToString();
                strSupplierID = dvOverAll[0]["SUPPLIERID"].ToString();
                createTableDisplay(0,"OVERALL",tdOverAll, strHeader, strCapacity, strActual, strRate,12, strColorDisplay, strSupplierID);
            }

         
            for (int L = 0; L < dvLegend.Count; L++)
            {
                strColorDisplay = dvLegend[L]["COLORDISPLAY"].ToString();
                strStatusDescription = dvLegend[L]["STATUSDESCRIPTION"].ToString();
                createLegendDisplay(tbLegend, strColorDisplay, strStatusDescription);
            }



            createDisplayMonitoring("INSIDE", dvInside, "INSIDE LIMA SUPPLIERS", tbDisplay);

            createGap(tbDisplay, 20);

            createDisplayMonitoring("OUTSIDE", dvOutSide, "OUTSIDE LIMA SUPPLIERS", tbDisplay);
        }

        public void createGap(HtmlTable tb, int intGapValue)
        {
            HtmlTableRow trGap = new HtmlTableRow();
            tb.Controls.Add(trGap);

            HtmlTableCell tdGap = new HtmlTableCell();
            trGap.Controls.Add(tdGap);

            Label lblGap = new Label();
            lblGap.Height = intGapValue;
            tdGap.Controls.Add(lblGap);
        }

        public void createDisplayMonitoring(string strNameID, DataView dv, string strTitle, HtmlTable tb)
        {
            if (dv.Count > 0)
            {
                HtmlTableRow trTitle = new HtmlTableRow();
                tb.Controls.Add(trTitle);

                HtmlTableCell tdTitle = new HtmlTableCell();
                trTitle.Controls.Add(tdTitle);

                Label lblTitle = new Label();
                lblTitle.Text = strTitle;
                lblTitle.ForeColor = Color.Black;
                tdTitle.Controls.Add(lblTitle);


                HtmlTableRow trRow = new HtmlTableRow();
                tb.Controls.Add(trRow);
                createTableList(strNameID, dv, trRow, 10, tb);
            }
        }
        public void createTableList(string strNameID, DataView dv, HtmlTableRow trRow, int intFont, HtmlTable tb)
        {
            string strHeader = "", strCapacity = "", strActual = "", strRate = "", strColorDisplay = "", strSupplierID = "";

            
           
        
          
            for (int i = 0; i < dv.Count; i++)
            {
                if (i == 0)
                {
                    
                }

                if (i % 3 == 0 && i != 0)
                {
                    createGap(tb,10);

                    trRow = new HtmlTableRow();
                    tb.Controls.Add(trRow);
                }

                HtmlTableCell tdList = new HtmlTableCell();
                trRow.Controls.Add(tdList);

                strHeader = dv[i]["SUPPLIERNAME"].ToString();
                strCapacity = dv[i]["CAPACITY"].ToString();
                strActual = dv[i]["ACTUALCAPACITY"].ToString();
                strRate = dv[i]["PICKUPRATE"].ToString();
                strColorDisplay = dv[i]["COLORDISPLAY"].ToString();
                strSupplierID = dv[i]["SUPPLIERID"].ToString();
                createTableDisplay(i, strNameID, tdList, strHeader, strCapacity, strActual, strRate, intFont, strColorDisplay, strSupplierID);

                HtmlTableCell tdSpace = new HtmlTableCell();
                trRow.Controls.Add(tdSpace);

                if (i % 3 != 0 || i == 0)
                {
                    Label lblSpace = new Label();
                    lblSpace.Width = 40;
                    tdSpace.Controls.Add(lblSpace);
                }

            }
        }
        public void createLegendDisplay(HtmlTable tb, string strColorDisplay, string strStatusDescription)
        {
            HtmlTableRow trHead = new HtmlTableRow();
            tb.Controls.Add(trHead);

            HtmlTableCell tcColor= new HtmlTableCell();
            trHead.Controls.Add(tcColor);

            Panel pnlColor = new Panel();
            pnlColor.Attributes.Add("style", "background-color:" + strColorDisplay + ";width:20px;height:8px");
            tcColor.Controls.Add(pnlColor);

            HtmlTableCell tdSpace = new HtmlTableCell();
            trHead.Cells.Add(tdSpace);

            HtmlTableCell tdStatus = new HtmlTableCell();
            trHead.Cells.Add(tdStatus);

            Label lblStatus = new Label();
            lblStatus.Font.Size = 8;
            lblStatus.ForeColor = Color.Black;
            lblStatus.Text = strStatusDescription;
            tdStatus.Controls.Add(lblStatus);


        }
        public void createTableDisplay(int intNumID, string strNameID, HtmlTableCell tdDestination, string strHeader, string strCapacity, string strActual, string strRate, int intFontSizePixels,string strColorDisplay, string strSupplierID)
        {
            HtmlTable tb = new HtmlTable();
            tb.Border = 1;
            tb.BorderColor = "Black";
            tb.Attributes.Add("style", "text-align:center;");
            tdDestination.Controls.Add(tb);

            HtmlTableRow trHead = new HtmlTableRow();
            
            tb.Controls.Add(trHead);

            int intFontHeigt = intFontSizePixels + 10;
            int intFontSize = intFontSizePixels + 5;

            if (strHeader.Length > 25)
            {
                intFontSize = intFontSizePixels + 2;
            }

            HtmlTableCell tcHead = new HtmlTableCell();
            tcHead.ColSpan = 3;
            tcHead.Attributes.Add("style", "background-color:blue;color:white;height:" + intFontSizePixels.ToString() + "px;font-size:" + intFontSize.ToString() + "px;text-align:center ");
            trHead.Controls.Add(tcHead);

         
            LinkButton lnkHeader = new LinkButton();
            lnkHeader.ID = intNumID.ToString() + "_" + "lnk" +"_" + strNameID;
            lnkHeader.ForeColor = Color.White;
            lnkHeader.Text = strHeader;
            lnkHeader.Click += lnk_click;
            tcHead.Controls.Add(lnkHeader);


            Label lblSupplierID = new Label();
            lblSupplierID.ID = intNumID.ToString() + "_" + "lbl" + "_" + strNameID;
            lblSupplierID.Text = strSupplierID;
            pnlHidden.Controls.Add(lblSupplierID);


            HtmlTableRow trSubHead= new HtmlTableRow();
            tb.Controls.Add(trSubHead);

            HtmlTableCell tcSubHead1 = new HtmlTableCell();

            tcSubHead1.Attributes.Add("style", "background-color:#CDE0F7;color:black;height:" + (intFontSizePixels + 10).ToString() + "px;font-size:" + (intFontSizePixels).ToString() + "px;text-align:center ");
            trSubHead.Controls.Add(tcSubHead1);



            Label lblSubHeader1 = new Label();
            lblSubHeader1.Text = "Capacity";
            lblSubHeader1.Width = intFontSizePixels + 90;
            tcSubHead1.Controls.Add(lblSubHeader1);

            HtmlTableCell tcSubHead2 = new HtmlTableCell();
            tcSubHead2.Attributes.Add("style", "background-color:#CDE0F7;color:black;height:" + (intFontSizePixels + 10).ToString() + "px;font-size:" + (intFontSizePixels).ToString() + "px;text-align:center ");
            trSubHead.Controls.Add(tcSubHead2);

            Label lblSubHeader2 = new Label();
            lblSubHeader2.Text = "Actual";
            lblSubHeader2.Width = intFontSizePixels + 90;
            tcSubHead2.Controls.Add(lblSubHeader2);

            HtmlTableCell tcSubHead3 = new HtmlTableCell();
            tcSubHead3.Attributes.Add("style", "background-color:#CDE0F7;color:black;height:" + (intFontSizePixels + 10).ToString() + "px;font-size:" + (intFontSizePixels).ToString() + "px;text-align:center ");
            trSubHead.Controls.Add(tcSubHead3);

            Label lblSubHeader3 = new Label();
            lblSubHeader3.Text = "Pick-up Rate";
            lblSubHeader3.Width = intFontSizePixels + 90;
            tcSubHead3.Controls.Add(lblSubHeader3);



            HtmlTableRow trValue = new HtmlTableRow();
            tb.Controls.Add(trValue);


            HtmlTableCell tcCapacity = new HtmlTableCell();
            tcCapacity.Attributes.Add("style", "background-color:#FEF3CB;color:black;height:" + (intFontSizePixels + 20).ToString() + "px;font-size:" + (intFontSizePixels + 15).ToString() + "px;text-align:center ");
            trValue.Controls.Add(tcCapacity);

            Label lblCapacity = new Label();
            lblCapacity.Text = strCapacity;
            tcCapacity.Controls.Add(lblCapacity);




            HtmlTableCell tcActual = new HtmlTableCell();
            tcActual.Attributes.Add("style", "background-color:"+ strColorDisplay +";color:black;height:" + (intFontSizePixels + 20).ToString() + "px;font-size:" + (intFontSizePixels + 15).ToString() + "px;text-align:center ");
            trValue.Controls.Add(tcActual);

            Label lblActual = new Label();
            lblActual.Text = strActual;
            tcActual.Controls.Add(lblActual);


            HtmlTableCell tcRate = new HtmlTableCell();
            tcRate.Attributes.Add("style", "background-color:white;color:black;height:" + (intFontSizePixels + 20).ToString() + "px;font-size:" + (intFontSizePixels + 15).ToString() + "px;text-align:center ");
            trValue.Controls.Add(tcRate);

            Label lblRate = new Label();
            lblRate.ID = intNumID.ToString() + "_" + "lbl" + "_" + strNameID + "_rate";
            lblRate.Text = strRate + "%";
            tcRate.Controls.Add(lblRate);


        }


        protected void lnk_click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            string id = lnk.ID;
            id = id.Replace("lnk", "lbl");
            Label lbl = (Label)pnlHidden.FindControl(id);

            Label lblRate = (Label)tbLegend.FindControl(id+ "_rate");

            string strSupplierName = lnk.Text;
            strSupplierName = strSupplierName.Replace("&", "replacewithand");
            openLink("SUPPLIERID=" + lbl.Text + "&SUPPLIERNAME=" + strSupplierName + "&RATE="+ lblRate.Text);
        }


        public void openLink(string strReqString)
        {
            string strURL = "EmptyBoxBoardDetails.aspx?" + strReqString;

            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + strURL + "','_newtab');", true);
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            //getDisplay();
        }
    }
}