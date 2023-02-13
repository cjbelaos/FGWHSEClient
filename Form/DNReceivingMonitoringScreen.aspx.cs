using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FGWHSEClient.DAL;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.eppi.utils;
using System.Xml.Linq;
using System.Drawing;
using System.Collections.Specialized;
using System.IO;
using FGWHSEClient.LdapService;
using FGWHSEClient.LogInService;

namespace FGWHSEClient.Form
{
    public partial class DNReceivingMonitoringScreen : System.Web.UI.Page
    {
        public DNReceivingScreenDAL dnRcvDetailsScreen = new DNReceivingScreenDAL();
        public DataTable dtable;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Fill_DN_HeaderDetails();
            }
        }

        private void Fill_DN_HeaderDetails()
        {
            try
            {
                DataView dv = new DataView();
                dv = dnRcvDetailsScreen.DN_GetDN_Header(Request["DNno"].ToString().Trim().ToUpper());

                if (dv.Count > 0)
                {
                    if (dv.Table.Rows.Count > 0)
                    {
                        lblDNNo.Text = dv.Table.Rows[0]["barcode"].ToString().Trim().ToUpper();
                        lblTotalRFIDCount.Text = dv.Table.Rows[0]["rfidqty"].ToString().Trim().ToUpper() + "/" +
                                                dv.Table.Rows[0]["rfidtotal"].ToString().Trim().ToUpper();
                        FillGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        private void FillGrid()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dnRcvDetailsScreen.DN_GetDNDetails(lblDNNo.Text);

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        Label lblHeader = new Label();
                        Label lblFooter = new Label();
                        string strPartCode = "";
                        string strPartName = "";
                        int iRcvCount = 0;
                        int iRcvCountTotal = 0;
                        int iRcvQty = 0;
                        int iRcvQtyTotal = 0;

                        strPartCode = ds.Tables[i].Rows[0][2].ToString();
                        strPartName = ds.Tables[i].Rows[0][10].ToString();

                        foreach (DataRow dr in ds.Tables[i].Rows)
                        {
                            int iQty = Convert.ToInt32(dr["RCVQTY"].ToString().Trim().ToUpper());

                            if (dr["STATUS"].ToString().Trim().ToUpper() == "RECEIVED")
                            {
                                iRcvCount++;
                                iRcvQty += iQty;
                            }

                            iRcvQtyTotal = Convert.ToInt32(dr["DNQTY"].ToString().Trim().ToUpper());
                            iRcvCountTotal++;
                        }

                        lblHeader.Text = "<table  style='background-color:#ebeded' width='1160px'><tr><td colspan=7 style='font-size:18px;font-weight:bolder;'>" + strPartCode + " - " + strPartName + "</td></tr>" +
                                         "<tr><td width='200px'style='font-weight:normal; font-size:16px;'  align='center'>" + iRcvCount.ToString() + "/" + iRcvCountTotal.ToString() + "</td>" +
                                         "<td width='300px'></td>" +
                                         "<td width='350px'></td>" +
                                         "<td width='295px'></td>" +
                                         "<td  width='170px' style='font-weight:normal; font-size:16px;''  align='center'>" + iRcvQty.ToString() + "/" + iRcvQtyTotal.ToString() + "</td>" +
                                         "<td width='250px'></td>" +
                                         "<td width='250px'></td>" +
                                         "</tr></table>";

                        lblFooter.Text = "</br>";

                        GridView objGV = new GridView();
                        objGV.ID = "GV" + i;
                        objGV.AutoGenerateColumns = false;
                        objGV.GridLines = GridLines.Both;
                        objGV.RowDataBound += objGV_RowDataBound;

                        BoundField no = new BoundField();
                        no.HeaderText = "No.";
                        no.DataField = "NO";
                        no.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        no.HeaderStyle.Font.Size = 14;
                        no.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        no.HeaderStyle.ForeColor = Color.White;
                        no.ItemStyle.Font.Size = 11;
                        no.ItemStyle.ForeColor = Color.Black;
                        no.ItemStyle.Width = 250;
                        no.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(no);

                        BoundField rfidtag = new BoundField();
                        rfidtag.HeaderText = "RFID Tag";
                        rfidtag.DataField = "RFIDTAG";
                        rfidtag.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        rfidtag.HeaderStyle.Font.Size = 14;
                        rfidtag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        rfidtag.HeaderStyle.ForeColor = Color.White;
                        rfidtag.ItemStyle.Font.Size = 11;
                        rfidtag.ItemStyle.ForeColor = Color.Black;
                        rfidtag.ItemStyle.Width = 250;
                        rfidtag.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(rfidtag);

                        BoundField refno = new BoundField();
                        refno.HeaderText = "Ref No.";
                        refno.DataField = "REFNO";
                        refno.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        refno.HeaderStyle.Font.Size = 14;
                        refno.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        refno.HeaderStyle.ForeColor = Color.White;
                        refno.ItemStyle.Font.Size = 11;
                        refno.ItemStyle.ForeColor = Color.Black;
                        refno.ItemStyle.Width = 250;
                        refno.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(refno);

                        BoundField lotno = new BoundField();
                        lotno.HeaderText = "Lot No.";
                        lotno.DataField = "LOTNO";
                        lotno.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        lotno.HeaderStyle.Font.Size = 14;
                        lotno.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        lotno.HeaderStyle.ForeColor = Color.White;
                        lotno.ItemStyle.Font.Size = 11;
                        lotno.ItemStyle.ForeColor = Color.Black;
                        lotno.ItemStyle.Width = 250;
                        lotno.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(lotno);

                        BoundField rcvqty = new BoundField();
                        rcvqty.HeaderText = "Qty";
                        rcvqty.DataField = "RCVQTY";
                        rcvqty.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        rcvqty.HeaderStyle.Font.Size = 14;
                        rcvqty.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        rcvqty.HeaderStyle.ForeColor = Color.White;
                        rcvqty.ItemStyle.Font.Size = 11;
                        rcvqty.ItemStyle.ForeColor = Color.Black;
                        rcvqty.ItemStyle.Width = 250;
                        rcvqty.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(rcvqty);

                        BoundField remarks = new BoundField();
                        remarks.HeaderText = "Remarks";
                        remarks.DataField = "REMARKS";
                        remarks.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        remarks.HeaderStyle.Font.Size = 14;
                        remarks.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        remarks.HeaderStyle.ForeColor = Color.White;
                        remarks.ItemStyle.Font.Size = 11;
                        remarks.ItemStyle.ForeColor = Color.Black;
                        remarks.ItemStyle.Width = 250;
                        remarks.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(remarks);

                        BoundField status = new BoundField();
                        status.HeaderText = "Status";
                        status.DataField = "STATUS";
                        status.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        status.HeaderStyle.Font.Size = 14;
                        status.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        status.HeaderStyle.ForeColor = Color.White;
                        status.ItemStyle.Font.Size = 11;
                        status.ItemStyle.ForeColor = Color.Black;
                        status.ItemStyle.Width = 250;
                        status.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(status);

                        objGV.DataSource = ds.Tables[i];
                        objGV.DataBind();
                        pnl.Controls.Add(lblHeader); 
                        pnl.Controls.Add(objGV);
                        pnl.Controls.Add(lblFooter);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }
            void objGV_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                try
                {
                    GridView grid = sender as GridView;

                    int rownum = 0;
                    rownum++;
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        int vColumnCnt = -1;
                        foreach (TableCell vCell in e.Row.Cells)
                        {
                            vColumnCnt++;
                            //if (.Cells[vColumnCnt].Text.ToString().ToUpper() == "DN NO.")
                            //{
                            //}
                            if (grid.HeaderRow.Cells[vColumnCnt].Text.ToString().Trim().ToUpper() == "STATUS")
                            {
                                if (e.Row.Cells[vColumnCnt].Text.ToString().Trim().ToUpper() == "RECEIVED")
                                {
                                    e.Row.BackColor = Color.FromArgb(204, 255, 204);
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
        }

        //private void FillGrid()
        //{
        //    DataSet dsItemCode = new DataSet();
        //    string dnNo = Request.Params["DNNo"];
        //    dsItemCode = dnRcvDetailsScreen.DN_DisplayItemCode(dnNo);

        //    if (dsItemCode.Tables[0].Rows.Count > 0)
        //    {
        //        Label[] lblPCodeNames = new Label[n];

        //        for (int i = 0; i < dsItemCode.Tables[0].Rows.Count; i++)
        //        {
        //            this.Controls.Add(lblPCodeNames[i]);

        //            //    {
        //            //        textBoxes[i] = new TextBox();
        //            //        // Here you can modify the value of the textbox which is at textBoxes[i]

        //            //        labels[i] = new Label();
        //            //        // Here you can modify the value of the label which is at labels[i]
        //            //    }

        //            //    // This adds the controls to the form (you will need to specify thier co-ordinates etc. first)
        //            //    for (int i = 0; i < n; i++)
        //            //    {
        //            //        this.Controls.Add(textBoxes[i]);
        //            //        this.Controls.Add(labels[i]);
        //            //    }
        //            //}
        //        }
        //        // MAKE A LOOP FOR GETTING ITEM/PART CODE FROM DATASET
        //        //// CALL STOREDPROC FOR TABLE
        //        // BIND TABLE
        //        //gvDNReceiving.DataSource = dsDnRcvScreen.Tables[0];
        //        //gvDNReceiving.DataBind();
        //    }
        //    else
        //    {
        //        //gvDNReceiving.DataSource = "";
        //        //gvDNReceiving.DataBind();
        //        //error message
        //    }
        //}
    }