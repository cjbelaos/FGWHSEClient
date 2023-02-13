using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Drawing;
using System.Web.Script.Serialization;

using System.Collections.Generic;
using System.Collections.Specialized;
using com.eppi.utils;
namespace FGWHSEClient.Form
{
    public partial class LocationView : System.Web.UI.Page
    {
        protected DataTable dtMassReworkUploaded = new DataTable();
        Maintenance maint = new Maintenance();
        public string strCellHeight = "40px";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    hdfy.Value = "0";
                    hdfy.Value = "0";

                    dtMassReworkUploaded = maint.QA_GET_UPLOADED_PALLET("MR").Tables[0];

                }
                //else
                //{
                //    dtMassReworkUploaded = (DataTable)ViewState["dtMassReworkUploaded"];
                //}

                //ViewState["dtMassReworkUploaded"] = dtMassReworkUploaded;


                DataSet dsPREJUDGETABLE = maint.QA_GET_PREJUDGE_TABLE();
                createTable(dsPREJUDGETABLE.Tables[0].DefaultView, dsPREJUDGETABLE.Tables[1].DefaultView);

                DataSet dsPREJUDGE = maint.QA_GET_PREJUDGE_LANE();
                populateLane(dsPREJUDGE.Tables[0].DefaultView, dsPREJUDGE.Tables[1].DefaultView);

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script> SetScrollPosition(); </script>", false);
            }
            catch (Exception ex)
            {
                //Response.Write("<script>");
                //Response.Write("alert('" + ex.Message.ToString() + "');");
                //Response.Write("</script>");
                MsgBox1.alert(ex.Message.ToString());
            }
        }



        public void createTable(DataView dv, DataView dvCategory)
        {
            string strID = "", strRowID = "", strColID = "";

            string strLabelStatusID = "";

            int intMaintRow = 0, intMaintCol = 0;
            if (dv.Count > 0)
            {
                intMaintRow = Convert.ToInt32(dv[0]["ROWNO"].ToString());
                intMaintCol = Convert.ToInt32(dv[0]["COLNO"].ToString());

                tdHeaderContainer.Attributes.Add("style", "text-align:center;");


                Panel pnlBorder = new Panel();
                pnlBorder.ID = "pnlBorder";
                pnlBorder.Attributes.Add("style", "border-style:solid; border-color:Aqua; border-width:medium; text-align:center; width:1187px; height:635px; overflow:auto");
                pnlBorder.Attributes.Add("onscroll", "getScrollPosition()");
                tdBodyLayOut.Controls.Add(pnlBorder);

                tdBodyLayOut.Controls.Remove(tblLayOut);
                tblLayOut.CellSpacing = 0;
                tblLayOut.Attributes.Add("style", "font-weight:bold");
                pnlBorder.Controls.Add(tblLayOut);


                HtmlTableRow trRowHeader = new HtmlTableRow();
                tbHeader.CellSpacing = 0;
                tbHeader.Attributes.Add("style", "font-weight:bold");
                tbHeader.Rows.Add(trRowHeader);



                HtmlTableCell tdHeader = new HtmlTableCell();
                tdHeader.ColSpan = intMaintCol;
                tdHeader.Attributes.Add("style", "text-align:center;");
                trRowHeader.Cells.Add(tdHeader);


                //Label lblHeader = new Label();
                //lblHeader.Height = Unit.Pixel(50);
                //lblHeader.Text = "PRE LOCATION AREA STATUS";
                //lblHeader.Font.Size = FontUnit.Large;
                //tdHeader.Controls.Add(lblHeader);



                HtmlTableRow trRowLegend = new HtmlTableRow();
                tbHeader.Rows.Add(trRowLegend);


                HtmlTableCell tdLegend = new HtmlTableCell();
                tdLegend.ID = "tdLegend";
                tdLegend.ColSpan = intMaintCol;
                tdLegend.Attributes.Add("style", "text-align:center;");
                trRowLegend.Cells.Add(tdLegend);



                //HtmlTableRow trCategory = new HtmlTableRow();
                //tblLayOut.Rows.Add(trCategory);

                //for (int intCatRow = 0; intCatRow < dvCategory.Count; intCatRow++)
                //{
                //    HtmlTableCell tdCategory = new HtmlTableCell();
                //    tdCategory.ColSpan = Convert.ToInt32(dvCategory[intCatRow]["CELLSPANCOUNT"].ToString());
                //    trCategory.Cells.Add(tdCategory);

                //    if(dvCategory.Count - 1 !=  intCatRow)
                //    {
                //        HtmlTableCell tdCategorySpace = new HtmlTableCell();
                //        trCategory.Cells.Add(tdCategorySpace);
                //    }
                //    Panel pnlCategory = new Panel();
                //    pnlCategory.Attributes.Add("style", "border-bottom-style:solid; border-color:Blue; border-width:thin; text-align:center;");
                //    tdCategory.Controls.Add(pnlCategory);



                //    Label lblCategory = new Label();
                //    lblCategory.Font.Size = FontUnit.XLarge;
                //    lblCategory.Text = dvCategory[intCatRow]["CATEGORYNAME"].ToString();
                //    pnlCategory.Controls.Add(lblCategory);


                //    Label lblCategorySpace = new Label();
                //    lblCategorySpace.Height = Unit.Pixel(20);
                //    tdCategory.Controls.Add(lblCategorySpace);
                //}




                for (int intRow = 0; intRow <= intMaintRow; intRow++)
                {
                    strRowID = "00" + intRow.ToString();
                    HtmlTableRow trRowApproverLabel = new HtmlTableRow();
                    trRowApproverLabel.ID = "ROW_" + strRowID.Substring(strRowID.Length - 2, 2);
                    tblLayOut.Rows.Add(trRowApproverLabel);


                    for (int intCol = 1; intCol <= intMaintCol; intCol++)
                    {
                        strColID = "00" + intCol.ToString();
                        strColID = strColID.Substring(strColID.Length - 2, 2);

                        strRowID = "00" + intRow.ToString();
                        strRowID = strRowID.Substring(strRowID.Length - 2, 2);


                        strID = "CELL_" + strColID + strRowID;


                        HtmlTableCell tdLabelSpace = new HtmlTableCell();
                        tdLabelSpace.ID = strID;
                        tdLabelSpace.Attributes.Add("style", "height:" + strCellHeight + ";vertical-align:text-top;text-align:center");

                        trRowApproverLabel.Cells.Add(tdLabelSpace);


                        Label lblPallet = new Label();
                        lblPallet.Width = Unit.Pixel(50);
                        lblPallet.Height = Unit.Pixel(0);
                        lblPallet.ID = "lbl_" + strColID + strRowID;
                        //lblPallet.Text = tdLabelSpace.ID.ToString();
                        lblPallet.Font.Size = FontUnit.Small;
                        tdLabelSpace.Controls.Add(lblPallet);


                        //LinkButton lnkLine = new LinkButton();
                        //lnkLine.ID = "lnk_" + strColID + strRowID;
                        //lnkLine.Font.Size = FontUnit.Large;
                        //lnkLine.Font.Underline = false;
                        //lnkLine.Click += lnkLine_Click;
                        //tdLabelSpace.Controls.Add(lnkLine);


                        Label lblLine = new Label();
                        lblLine.Font.Size = FontUnit.XSmall;
                        lblLine.ID = "lblLine_" + strColID + strRowID;
                        tdLabelSpace.Controls.Add(lblLine);




                        strLabelStatusID = "lblStatus_" + strColID + strRowID;
                        Label lblStatus = new Label();
                        lblStatus.ID = strLabelStatusID;
                        lblStatus.Attributes.Add("style", "display:none");
                        lblStatus.Font.Size = FontUnit.Small;
                        tdLabelSpace.Controls.Add(lblStatus);


                    }

                }


            }



            HtmlTableCell tdCategory, tdRemove;
            HtmlTableRow trContainer;

            Label lblDELPallet, lblDELLine, lblDELStatus;


            string strIDCONT = "", strRow = "", strColumn = "", strFindColumn = "", delStrID = "";
            int intMaXDel = 0, intColDel;
            for (int intCatRow = 0; intCatRow < dvCategory.Count; intCatRow++)
            {
                strColumn = dvCategory[intCatRow]["RANGEFROM"].ToString();
                strRow = dvCategory[intCatRow]["ROWLOCATION"].ToString();
                strIDCONT = strColumn + strRow;

                tdCategory = (HtmlTableCell)tblLayOut.FindControl("CELL_" + strIDCONT);
                trContainer = (HtmlTableRow)tblLayOut.FindControl("ROW_" + strRow);

                intMaXDel = Convert.ToInt32(dvCategory[intCatRow]["CELLSPANCOUNT"].ToString());

                intColDel = 0;

                for (int intDel = 1; intDel < intMaXDel; intDel++)
                {
                    if (intDel == 1)
                    {
                        intColDel = Convert.ToInt32(strColumn) + 1;
                    }
                    else
                    {
                        intColDel = intColDel + 1;
                    }

                    strFindColumn = "00" + intColDel.ToString();

                    delStrID = strFindColumn.Substring(strFindColumn.Length - 2, 2) + strRow;

                    tdRemove = (HtmlTableCell)tblLayOut.FindControl("CELL_" + delStrID);

                    lblDELPallet = (Label)tblLayOut.FindControl("lbl_" + delStrID);
                    lblDELLine = (Label)tblLayOut.FindControl("lblLine_" + delStrID);
                    lblDELStatus = (Label)tblLayOut.FindControl("lblStatus_" + delStrID);

                    tdRemove.Controls.Remove(lblDELPallet);
                    tdRemove.Controls.Remove(lblDELLine);
                    tdRemove.Controls.Remove(lblDELStatus);
                    trContainer.Controls.Remove(tdRemove);

                }


                tdCategory.ColSpan = intMaXDel;

                if (strRow != "00")
                {
                    Label lblCategorySpaceUpper = new Label();
                    lblCategorySpaceUpper.Height = Unit.Pixel(10);
                    tdCategory.Controls.Add(lblCategorySpaceUpper);
                }

                Panel pnlCategory = new Panel();
                pnlCategory.Attributes.Add("style", "border-bottom-style:solid;color:" + dvCategory[intCatRow]["FORECOLOR"].ToString() + ";background-color:" + dvCategory[intCatRow]["BACKGROUNDCOLOR"].ToString() + "; border-width:thin; text-align:center;");
                tdCategory.Controls.Add(pnlCategory);




                Label lblCategory = new Label();
                lblCategory.Font.Size = FontUnit.XLarge;
                lblCategory.Text = dvCategory[intCatRow]["CATEGORYNAME"].ToString();
                pnlCategory.Controls.Add(lblCategory);


                Label lblCategorySpace = new Label();
                lblCategorySpace.Height = Unit.Pixel(2);
                tdCategory.Controls.Add(lblCategorySpace);
            }


            Panel pnlPop = new Panel();
            pnlPop.ID = "pnlPop";
            pnlPop.Height = Unit.Pixel(400);
            pnlPop.Width = Unit.Pixel(381);
            pnlPop.BackColor = Color.White;
            //pnlPop.Attributes.Add("style","overflow:auto;");
            dvPage.Controls.Add(pnlPop);

            HtmlTable tbPopBody = new HtmlTable();
            pnlPop.Controls.Add(tbPopBody);


            HtmlTableRow trGrid = new HtmlTableRow();
            tbPopBody.Rows.Add(trGrid);

            HtmlTableCell tcGrid = new HtmlTableCell();
            //tcGrid.Height = "350px";
            //tcGrid.Width = "800px";
            tcGrid.Attributes.Add("style", "vertical-align:top");
            trGrid.Cells.Add(tcGrid);

            Panel pnlGrid = new Panel();
            pnlGrid.Height = Unit.Pixel(350);
            pnlGrid.Width = Unit.Pixel(370);
            pnlGrid.BorderStyle = BorderStyle.Solid;
            pnlGrid.BorderColor = Color.Aqua;
            pnlGrid.Attributes.Add("style", "text-align:center;overflow:auto;");
            tcGrid.Controls.Add(pnlGrid);


            GridView grdShow = new GridView();
            grdShow.AutoGenerateColumns = false;
            grdShow.ID = "grdShow";
            grdShow.HeaderStyle.Font.Bold = true;
            grdShow.HeaderStyle.BackColor = ColorTranslator.FromHtml("#C5D9F1");
            grdShow.HeaderStyle.BorderStyle = BorderStyle.Solid;
            grdShow.HeaderStyle.BorderColor = Color.Black;
            grdShow.RowStyle.Font.Bold = false;
            grdShow.RowStyle.BorderStyle = BorderStyle.Solid;
            grdShow.RowStyle.BorderColor = Color.Black;
            grdShow.RowStyle.BorderWidth = Unit.Pixel(10);
            grdShow.BorderColor = Color.Black;
            grdShow.BorderStyle = BorderStyle.Solid;
            pnlGrid.Controls.Add(grdShow);

            if (grdShow.Columns.Count == 0)
            {
                BoundField bfPallet = new BoundField();
                bfPallet.DataField = "PALLETNO";
                bfPallet.HeaderText = "PALLET NO";
                bfPallet.HeaderStyle.Width = Unit.Pixel(150);
                grdShow.Columns.Add(bfPallet);
            }


            if (grdShow.Columns.Count == 1)
            {
                BoundField bfLocation = new BoundField();
                bfLocation.DataField = "LOCATION";
                bfLocation.HeaderText = "LOCATION";
                bfLocation.HeaderStyle.Width = Unit.Pixel(200);
                grdShow.Columns.Add(bfLocation);
            }









            HtmlTableRow trBack = new HtmlTableRow();
            tbPopBody.Rows.Add(trBack);

            HtmlTableCell tcBack = new HtmlTableCell();
            tcBack.VAlign = "center";
            tcBack.Height = "40px";
            trBack.Cells.Add(tcBack);

            Button btnBack = new Button();
            btnBack.Text = "BACK";
            btnBack.Height = Unit.Pixel(25);
            tcBack.Controls.Add(btnBack);
        }


        public void populateLane(DataView dv, DataView dvLegend)
        {
            Label lblPallet;
            Label lblStatus;
            //LinkButton lnkLine;
            Label lblLine;
            HtmlTableCell cellLocation;

            HtmlTableCell tdLegend;
            tdLegend = (HtmlTableCell)tblLayOut.FindControl("tdLegend");
            string strStatusID = "";

            string strID = "", strLaneName = "", stRColor = "", stRForeColor = "", strIDFirstTwo = "", strLocationType = "", strIDLastTwo = "", strcurrCellLastTwo = "", strPalletText = "", strStatus = "";
            int intCellspan = 0, intLastCell = 0, intFirstCell = 0;
            if (dv.Count > 0)
            {
                strPalletText = "";
                strStatus = "";


                if (dvLegend.Count > 0)
                {
                    getLegend(tdLegend, dvLegend);
                }

                for (int intRow = 0; intRow < dv.Count; intRow++)
                {
                    strID = dv[intRow]["LOCATIONPOSITIONID"].ToString();
                    strLaneName = dv[intRow]["LOCATIONDESCRIPTION"].ToString();
                    stRColor = dv[intRow]["COLORDISPLAY"].ToString();
                    stRForeColor = dv[intRow]["TEXTCOLORDISPLAY"].ToString();
                    intCellspan = Convert.ToInt32(dv[intRow]["CELLSPAN"].ToString());
                    strStatus = dv[intRow]["PALLETSTATUS"].ToString();
                    strPalletText = dv[intRow]["PALLETNO"].ToString();
                    strLocationType = dv[intRow]["LOCATIONTYPE"].ToString();


                    cellLocation = (HtmlTableCell)tblLayOut.FindControl("CELL_" + strID);


                    //cellLocation.Attributes.Add("style", "border-top-style:solid; border-width:thin; border-color:black;text-align:center;height:80px;vertical-align:text-top;background-color:" + stRColor);

                    strIDFirstTwo = strID.Substring(0, 2);
                    strIDLastTwo = strID.Substring(strID.Length - 2, 2);
                    intFirstCell = Convert.ToInt32(strIDLastTwo);
                    intLastCell = intFirstCell + intCellspan - 1;

                    while (intFirstCell <= intLastCell)
                    {

                        strcurrCellLastTwo = "00" + intFirstCell.ToString();
                        strcurrCellLastTwo = strcurrCellLastTwo.Substring(strcurrCellLastTwo.Length - 2, 2);

                        cellLocation = (HtmlTableCell)tblLayOut.FindControl("CELL_" + strIDFirstTwo + strcurrCellLastTwo);

                        if (intFirstCell == intLastCell && intLastCell == Convert.ToInt32(strIDLastTwo))
                        {
                            cellLocation.Attributes.Add("style", "border-style:solid; border-width:thin; border-color:black;text-align:center;height:" + strCellHeight + ";vertical-align:text-top;color:" + stRForeColor + ";background-color:" + stRColor);
                        }
                        else
                        {
                            if (intFirstCell == Convert.ToInt32(strIDLastTwo))
                            {
                                cellLocation.Attributes.Add("style", "border-top-style:solid;border-left-style:solid;border-right-style:solid; border-width:thin; border-color:black;text-align:center;height:" + strCellHeight + ";vertical-align:text-top;color:" + stRForeColor + ";background-color:" + stRColor);
                            }
                            else if (intFirstCell == intLastCell)
                            {
                                cellLocation.Attributes.Add("style", "border-bottom-style:solid;border-left-style:solid;border-right-style:solid; border-width:thin; border-color:black;text-align:center;height:" + strCellHeight + ";vertical-align:text-top;color:" + stRForeColor + ";background-color:" + stRColor);
                            }
                            else
                            {
                                cellLocation.Attributes.Add("style", "border-left-style:solid;border-right-style:solid; border-width:thin; border-color:black;text-align:center;height:" + strCellHeight + ";vertical-align:text-top;color:" + stRForeColor + ";background-color:" + stRColor);
                            }
                        }
                        intFirstCell = intFirstCell + 1;
                    }

                    lblPallet = (Label)tblLayOut.FindControl("lbl_" + strID);
                    lblStatus = (Label)tblLayOut.FindControl("lblStatus_" + strID);
                    lblStatus.Text = strStatus;
                    if (strLocationType == "MR")
                    {

                        LinkButton lnkLine = new LinkButton();
                        lnkLine.ID = "lnk_" + strIDFirstTwo + strcurrCellLastTwo;
                        //lnkLine.Font.Size = FontUnit.Large;
                        //lnkLine.Font.Underline = false;
                        lnkLine.Click += lnkLine_Click;
                        //lnkLine = (LinkButton)tblLayOut.FindControl("lnk_" + strID);
                        lnkLine.ForeColor = Color.Black;
                        lnkLine.Font.Size = FontUnit.Large;
                        lnkLine.Font.Underline = false;
                        lnkLine.Text = strLaneName;
                        lnkLine.Width = Unit.Pixel(90);
                        cellLocation.Controls.Add(lnkLine);
                        cellLocation.Controls.Remove(lblPallet);


                    }
                    else
                    {


                        lblLine = (Label)tblLayOut.FindControl("lblLine_" + strID);

                        lblLine.Width = Unit.Pixel(90);
                        lblLine.Text = strLaneName;




                        if (strPalletText.Trim() != "")
                        {
                            lblPallet.Height = Unit.Pixel(20);
                            lblPallet.Text = strPalletText.Trim();
                            lblPallet.Width = Unit.Pixel(90);
                        }
                        else
                        {
                            lblPallet.Height = Unit.Pixel(0);
                            lblPallet.Width = Unit.Pixel(0);


                        }
                    }

                    strStatusID = lblStatus.ID.ToString();
                }



            }



            Panel pnlPop = (Panel)dvPage.FindControl("pnlPop");

            dtMassReworkUploaded = maint.QA_GET_UPLOADED_PALLET("MR").Tables[0];


            AjaxControlToolkit.ModalPopupExtender mp1 = new AjaxControlToolkit.ModalPopupExtender();
            mp1.ID = "mp1";
            mp1.PopupControlID = pnlPop.ID.ToString();
            mp1.TargetControlID = strStatusID;
            mp1.BackgroundCssClass = "modalBackground";
            dvPage.Controls.Add(mp1);
        }




        public void getLegend(HtmlTableCell tdLegendCellSpace, DataView dv)
        {

            Label lb1 = new Label();

            HtmlTable tbLegendTable = new HtmlTable();
            tdLegendCellSpace.Controls.Add(tbLegendTable);

            HtmlTableRow trLegendRow = new HtmlTableRow();
            tbLegendTable.Rows.Add(trLegendRow);

            for (int x = 0; x < dv.Count; x++)
            {


                HtmlTableCell tdStatusColor = new HtmlTableCell();
                tdStatusColor.Attributes.Add("style", "border-style:solid; border-width:thin; border-top-color:black;border-left-color:black;border-right-color:lightgray;border-bottom-color:lightgray;width:13px; height:5px;background-color:" + dv[x]["COLORDISPLAY"].ToString());
                trLegendRow.Cells.Add(tdStatusColor);


                Label lblStatusBlank = new Label();
                tdStatusColor.Controls.Add(lblStatusBlank);


                HtmlTableCell tdStatus = new HtmlTableCell();
                trLegendRow.Cells.Add(tdStatus);


                Label lblStatus = new Label();
                lblStatus.Text = dv[x]["STATUSDEFINITION"].ToString();
                lblStatus.Font.Size = FontUnit.XSmall;
                lblStatus.ForeColor = Color.Gray;
                tdStatus.Controls.Add(lblStatus);

                HtmlTableCell tdSpace = new HtmlTableCell();
                trLegendRow.Cells.Add(tdSpace);

                Label lblSpace = new Label();
                lblSpace.Width = Unit.Pixel(10);
                tdStatus.Controls.Add(lblSpace);


            }


        }



        protected void lnkLine_Click(object sender, EventArgs e)
        {




            LinkButton lnk = (LinkButton)sender;

            string lnkID = lnk.ID.ToString();


            if (lnkID.Trim() == "")
            {

                return;
            }


            string strIDNo = lnkID.Substring(lnkID.Length - 4, 4);

            Label lblPalletLabel = (Label)tblLayOut.FindControl("lbl_" + strIDNo);
            Label lblPalletStatus = (Label)tblLayOut.FindControl("lblStatus_" + strIDNo);

            if (lblPalletStatus.Text == "200")
            {
                //Response.Write("<script>");
                //Response.Write();
                //Response.Write("</script>");
                MsgBox1.alert("There are no Pallets on this Location");

                return;
            }

            AjaxControlToolkit.ModalPopupExtender mp1 = (AjaxControlToolkit.ModalPopupExtender)dvPage.FindControl("mp1");
            GridView grdShow = (GridView)dvPage.FindControl("grdShow");



            DataRow[] results = dtMassReworkUploaded.Select("LOCATION = '" + lnk.Text.Trim() + "'");


            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[2] {
                new DataColumn("PalletNo", typeof(string)),
                new DataColumn("Location", typeof(string))});


            String PalletNo = "", Location = "";

            foreach (DataRow row in results)
            {

                PalletNo = row["PalletNo"].ToString();
                Location = row["Location"].ToString();

                dt.Rows.Add(PalletNo, Location);
            }

            grdShow.DataSource = dt;
            grdShow.DataBind();

            mp1.Show();

        }

    }
}