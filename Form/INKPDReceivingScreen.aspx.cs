using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.html;
//using iTextSharp.text.html.simpleparser;
using System.Drawing;
using FGWHSEClient.DAL;


namespace FGWHSEClient.Form
{
    public partial class INKPDReceivingScreen : System.Web.UI.Page
    {
        public InHouseReceivingDAL InHouseReceivingDAL = new InHouseReceivingDAL();
        Label lblPartCode, lblBoxCount, lblQty, lblDate, lblTime, lblID;
        string sNewDate;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {

                //FillLoadingDock();


                //if (Request.QueryString.Count > 0)
                //{
                //    if (Request["loadingdock"] != null)
                //    {
                //        string strLoadingDockSelected = Request["loadingdock"].ToString().Trim().ToUpper();
                //        ddlLoadingDock.SelectedValue = strLoadingDockSelected;
                //    }
                //}

                lblLastUpdateDate.Text = DateTime.Now.ToString();
                FillGrid();

            }
        }

        protected void btnDisplay_Click(object sender, EventArgs e)
        {
            lblLastUpdateDate.Text = DateTime.Now.ToString();
            FillGrid();
        }

        private void FillGrid()
        {
            ////TRIAL
            ////DataTable dt = new DataTable();

            //////Adding columns  

            ////dt.Columns.Add("PartCode", typeof(string));
            ////dt.Columns.Add("PartName", typeof(string));
            ////dt.Columns.Add("BoxCount", typeof(string));
            ////dt.Columns.Add("Qty", typeof(string));
            ////dt.Columns.Add("Date", typeof(string));
            ////dt.Columns.Add("Time", typeof(string));

            //////Adding Row  
            ////DataRow dr = dt.NewRow();
            ////dr["PartCode"] = "175054800";
            ////dr["PartName"] = "INK BOTTLE";
            ////dr["BoxCount"] = "3";
            ////dr["Qty"] = "105";
            ////dr["Date"] = "4/20/2021";
            ////dr["Time"] = "16:28";
            //////dt.Rows.Add(dr);  

            //////dr = dt.NewRow();  
            //////dr["Names"] = "Krishna";  
            ////dt.Rows.Add(dr);

            ////gvReceive.DataSource = dt;
            ////gvReceive.DataBind();


            ////////FOR LIVE/////////
            DataSet ds = new DataSet();
            ds = InHouseReceivingDAL.RFID_GET_INK_ANTENNA_READ_2("IHINKWH01");
            ////////FOR LIVE/////////

            ////FOR TRIAL
            //DataSet ds = new DataSet();
            //ds = InHouseReceivingDAL.RFID_DUMMY_GET_DATE();
            ////FOR TRIAL



            if (ds.Tables[0].Rows.Count > 0)
            {
                gvReceive.DataSource = ds.Tables[0];
                gvReceive.DataBind();
            }
            else
            {
                gvReceive.DataSource = "";
                gvReceive.DataBind();
                //error message
            }
            /////////////////////////

        }

        protected void lnkEmpNo_Click(object sender, EventArgs e)
        {
            var TLink = (Control)sender;
            GridViewRow row = (GridViewRow)TLink.NamingContainer;
            LinkButton lnk = sender as LinkButton;

            lblPartCode = (Label)row.FindControl("lblPartCode");
            lblBoxCount = (Label)row.FindControl("lblBoxCount");
            lblQty = (Label)row.FindControl("lblQty");
            lblTime = (Label)row.FindControl("lblTime");
            lblDate = (Label)row.FindControl("lblDate");
            lblID = (Label)row.FindControl("lblID");
            string sDate = lblDate.Text;
            DateTime dt = Convert.ToDateTime(sDate);
            sNewDate = dt.ToString("MM/dd/yyyy");

            PrintPreview();


        }

        private void PrintPreview()
        {


            //Response.Redirect("INKPrintPreview.aspx?DNNO=" + txtDNNo.Text.Trim() + "&print=2");
            //Response.Redirect("INKPrintPreview.aspx?PartCode=" + txtDNNo.Text.Trim() + "&print=2");

            //Response.Write("<script type='text/javascript'>");
            ////Response.Write("window.open('INKPrintPreview.aspx?PartCode=" + lblPartCode.Text + "&Qty=" + lblQty.Text + "&Date=" + sNewDate + "&Time=" + lblTime.Text + "&LotMovementID=" + lblID.Text + "','_blank');");
            //Response.Write("window.location = 'INKPrintPreview.aspx?PartCode=" + lblPartCode.Text + "&Qty=" + lblQty.Text + "&Date=" + sNewDate + "&Time=" + lblTime.Text + "&LotMovementID=" + lblID.Text + "','_blank';");
            //  Response.Write("</script>");

            Response.Redirect("INKPrintPreview.aspx?PartCode=" + lblPartCode.Text + "&Qty=" + lblQty.Text + "&Date=" + sNewDate + "&Time=" + lblTime.Text + "&LotMovementID=" + lblID.Text);


            //string strURL = "INKPrintPreview.aspx?PartCode=" + lblPartCode.Text + "&Qty=" + lblQty.Text + "&Date=" + sNewDate + "&Time=" + lblTime.Text + "&LotMovementID=" + lblID.Text;

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + strURL + "','_newtab');", true);

        }

        protected void gvReceive_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[7].Visible = false;
        }

        //private void PrintCrystal()
        //{
        //    dsMechaLabel ds = new dsMechaLabel();

        //    dsINKParts ds = new dsINKParts();



        //    foreach (GridViewRow row in grdDetails.Rows)
        //    {
        //        CheckBox chkBox = (CheckBox)row.Cells[0].FindControl("chkItem");
        //        if (chkBox.Checked == true)
        //        {
        //            string mechaheader = row.Cells[1].Text.Trim().ToUpper();
        //            string mechabarcode = row.Cells[3].Text.Trim().ToUpper();

        //            ds.MechaLabel.AddMechaLabelRow(mechaheader + mechabarcode);

        //            ds.dTINKParts.AdddtINKPartsRow("172744203","INK Parts", "1920", "04/16/2021", "9:34 AM");

        //            //put in dataset

        //           // maint.UpdatePrintStat_Mecha(mechaheader + mechabarcode, Session["Username"].ToString(), txtFGKanban.Text.Trim(), rBtnBarMecha.SelectedValue);
        //        }
        //    }

        //    Session["dsINKParts"] = ds;
        //    Response.Redirect("INKPrintPreview.aspx");
        //}

        //private void Print()
        //{
        //        try
        //        {
        //            string strPartCode = "1";
        //            string strQty = "1";
        //            string strPartName = "1";
        //            string strDate = "1";
        //            string strTime = "1";


        //            string strPcode = "";
        //            string strPname = "";
        //            string strLotNo = "";
        //            string strRemarks = "";
        //            string strReason = "";
        //            string strLine = "";
        //            string strArea = "";
        //            int iQty = 1;
        //            //Decimal iQty = 1;
        //            string strOthers = "";
        //            string strLoc = "";
        //            // string strFactory = Convert.ToString(ddlFac.SelectedItem);
        //            string strHeader = "Replacement Lot Label";
        //            int pagecount = 1;
        //            //int stdperpage = 1;
        //            int stdperpage = 8;

        //            //Initialize Color
        //            string strTextColor = "BLACK";
        //            string strBGColor = "WHITE";

        //            StringWriter sw = new StringWriter();
        //            HtmlTextWriter hw = new HtmlTextWriter(sw);


        //            StringReader sr = new StringReader(sw.ToString());
        //            Document pdfDoc = new Document(PageSize.A4, 20f, 20f, 5f, 10f);

        //            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //            pdfDoc.Open();

        //            //foreach (GridViewRow row in gvLot.Rows)
        //            int irow = 0;

        //            pdfDoc.NewPage();


        //            int top = 9;
        //            int g = 1;
        //            float y = 637f;
        //            //j is for line count per page?

        //            for (int j = 0; j < 1; j++)
        //            {

        //                int right = 0;
        //                float x = 580f;
        //                //i is for left and right


        //                //GetColor
        //                strBGColor = "White";
        //                DataView dvColor = new DataView();
        //                dvColor = maint.GetTextColors(strBGColor);
        //                strTextColor = Convert.ToString(dvColor[0]["text_value"]);

        //                strPcode = txtPartCode.Text;
        //                strPname = txtPartName.Text;
        //                strLotNo = txtLotNo.Text;
        //                strRemarks = txtRemarks.Text.Trim();
        //                strReason = ddlreason_manual.SelectedValue;
        //                strArea = ddlarea1.SelectedValue;
        //                iQty = Decimal.ToInt32(Convert.ToDecimal(txtQty.Text));

        //                ////19JAN2021

        //                DateTime time = DateTime.Now;
        //                string year = Convert.ToString(time.Year);
        //                string yy = year.Substring(2, 2);
        //                string mm = Convert.ToString(time.Month);
        //                string date1 = time.Date.ToString("MM/dd/yyyy");
        //                string dd = date1.Substring(3, 2);
        //                string time1 = Convert.ToString(time.TimeOfDay);
        //                string hh = time1.Substring(0, 2);
        //                string min = time1.Substring(3, 2);
        //                string sec = time1.Substring(6, 2);
        //                //Change msec to 3 characters
        //                string msec = time1.Substring(10, 3);


        //                //string strRefNo = fac + strLine + yy + mm + dd + hh + min + sec + strHex;

        //                //F Baria
        //                //14 May 2015
        //                //Added PC Station, Month to Z,Y,Z
        //                if (mm.Trim() == "10")
        //                {
        //                    mm = "X";
        //                }
        //                else if (mm.Trim() == "11")
        //                {
        //                    mm = "Y";
        //                }
        //                else if (mm.Trim() == "12")
        //                {
        //                    mm = "Z";
        //                }

        //                string strClientIP = (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();
        //                string strPCStation = "999";

        //                if (strClientIP != "")
        //                {
        //                    string[] strIP = strClientIP.Split('.');
        //                    if (strIP.Length == 4)
        //                    {
        //                        if (Convert.ToInt32(strIP[3].Trim()) < 10)
        //                        {
        //                            strPCStation = "00" + strIP[3].Trim();
        //                        }
        //                        else if (Convert.ToInt32(strIP[3].Trim()) < 100)
        //                        {
        //                            strPCStation = "0" + strIP[3].Trim();
        //                        }
        //                        else
        //                        {
        //                            strPCStation = strIP[3].Trim();
        //                        }
        //                    }
        //                }

        //                //********************** END ADD ***************************

        //                string strOldRefNo = txtOldRefNo.Text;
        //                string strOldRefNoSubstring = strOldRefNo.Substring(0, 4);
        //                string strSupHeader = strOldRefNo.Substring(1, 3);

        //                string strRefNoNew = strOldRefNoSubstring + strPCStation + yy + mm + dd + hh + min + sec + msec;

        //                ////19JAN2021

        //                string strRefNo = strRefNoNew;
        //                string fac = strRefNo.Substring(0, 1);
        //                string headerfac = "";
        //                string strQty = "";
        //                if (iQty < 10)
        //                {
        //                    //iQty = Convert.ToInt32( iQty);
        //                    strQty = Convert.ToString("0" + iQty);
        //                }
        //                else
        //                {
        //                    strQty = Convert.ToString(iQty);
        //                }
        //                if (fac == "I")
        //                {
        //                    headerfac = "IJP";
        //                }
        //                else if (fac == "V")
        //                {
        //                    headerfac = "VP";
        //                }
        //                if (strRemarks == "&nbsp;")
        //                {
        //                    strRemarks = "";
        //                }
        //                if (strOthers == "&nbsp;")
        //                {
        //                    strOthers = "";
        //                }

        //                maint.AddLotData_ReplaceLot(strRefNo, strLotNo, strPcode, strPname, iQty, strRemarks, strReason, Session["UserName"].ToString());
        //                maint.AddLotList_ReplaceLot(txtOldRefNo.Text, strRefNo, strLotNo, strPcode, strPname, iQty, strRemarks, strReason, Session["UserName"].ToString(), strArea);
        //                maint.UpdateRFIDFinishFlag(txtOldRefNo.Text, strLotNo, Session["UserName"].ToString());

        //                //**********************LIVE SERVER
        //                //int totalfonts = FontFactory.RegisterDirectory(@"C:\Windows\Fonts");
        //                //string fontpath = Environment.GetEnvironmentVariable("SystemRoot");
        //                //BaseFont customfont = BaseFont.CreateFont(fontpath.ToString() + "\\fonts\\V100005_.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

        //                //**********************TEST - DEVELOPMENT
        //                int totalfonts = FontFactory.RegisterDirectory(@"C:\inetpub\wwwroot\BarcodeLabel2\BarcodeLabel\");
        //                BaseFont customfont = BaseFont.CreateFont("C:\\inetpub\\wwwroot\\BarcodeLabel2\\BarcodeLabel\\V100005_.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);


        //                iTextSharp.text.Font fontstyles = FontFactory.GetFont("Arial", 8, Font.BOLD, new Color(System.Drawing.Color.FromName(strTextColor)));
        //                iTextSharp.text.Font fontstyles1 = FontFactory.GetFont("Arial", 10, Font.BOLD, new Color(System.Drawing.Color.FromName(strTextColor)));

        //                iTextSharp.text.Font fontstylebody = FontFactory.GetFont("Arial", 5, Font.BOLD, new Color(System.Drawing.Color.FromName("BLACK")));
        //                iTextSharp.text.Font fontstylebody2 = FontFactory.GetFont("Arial", 6, Font.BOLD, new Color(System.Drawing.Color.FromName("BLACK")));
        //                iTextSharp.text.Font fontstylebody4 = FontFactory.GetFont("Arial", 9, Font.BOLD, new Color(System.Drawing.Color.FromName("RED"))); //FEB17
        //                iTextSharp.text.Font fontstylebody6 = FontFactory.GetFont("Arial", 6, Font.BOLD, new Color(System.Drawing.Color.FromName("BLACK"))); //FEB17
        //                iTextSharp.text.Font barcodefont = new iTextSharp.text.Font(customfont, 23); //16feb2021 24
        //                 //C39P24DhTt
        //                iTextSharp.text.Font barcodelabel = FontFactory.GetFont("Verdana", 6);
        //                iTextSharp.text.Font barcodelabel2 = FontFactory.GetFont("Verdana", 7); //16feb2021 5
        //                iTextSharp.text.Font remarkslabel = FontFactory.GetFont("Verdana", 6);

        //                //HEADER BGCOLOR
        //                PdfPTable tableBG = new PdfPTable(1);
        //                tableBG.TotalWidth = 279f;
        //                tableBG.LockedWidth = true;
        //                tableBG.DefaultCell.Border = (PdfCell.NO_BORDER);

        //                PdfPCell cellBG = new PdfPCell(new Phrase("", fontstyles1));
        //                cellBG.Border = 0;
        //                cellBG.Colspan = 3;

        //                cellBG.FixedHeight = 18f;

        //                cellBG.HorizontalAlignment = 1;

        //                cellBG.BackgroundColor = new Color(System.Drawing.Color.FromName(strBGColor));

        //                tableBG.AddCell(cellBG);

        //                tableBG.WriteSelectedRows(0, -1, pdfDoc.Left + right - 1, pdfDoc.Top - 10 - top, writer.DirectContent);

        //                //HEADER
        //                PdfPTable table3 = new PdfPTable(1);
        //                //table3.TotalWidth = 270f;
        //                table3.TotalWidth = 278f;
        //                table3.LockedWidth = true;
        //                table3.DefaultCell.Border = (PdfCell.NO_BORDER);

        //                PdfPCell cell = new PdfPCell(new Phrase("REPLACEMENT LOT LABEL", fontstyles1));
        //                cell.Border = 0;
        //                cell.Colspan = 3;
        //                cell.FixedHeight = 18f;
        //                cell.HorizontalAlignment = 1;
        //                table3.AddCell(cell);
        //                table3.WriteSelectedRows(0, -1, pdfDoc.Left + right, pdfDoc.Top - 7 - top, writer.DirectContent);

        //                // Labels
        //                PdfPTable table = new PdfPTable(3);
        //                table.TotalWidth = 300f;
        //                table.LockedWidth = true;
        //                table.SetWidths(new float[] { 1.5f, 9f, 4f });
        //                table.DefaultCell.Border = (PdfPCell.NO_BORDER);
        //                table.DefaultCell.PaddingBottom = 1;

        //                //RefNo
        //                table.AddCell(new Phrase("Partcode:", fontstylebody));
        //                PdfPCell cellRefno = new PdfPCell(new Phrase("*" + strPartCode + "*", barcodefont));
        //                cellRefno.PaddingTop = -1;
        //                cellRefno.Border = 0;
        //                table.AddCell(cellRefno);
        //                table.AddCell(new Phrase("", barcodelabel));
        //                table.AddCell(new Phrase("", barcodelabel));
        //                table.AddCell(new Phrase(strPartCode, barcodelabel2));
        //                table.AddCell(new Phrase("", barcodelabel));
        //                table.WriteSelectedRows(0, -1, pdfDoc.Left + right, pdfDoc.Top - 32 - top, writer.DirectContent);


        //                /////       ////SEPARATE PARTCODE FROM PDFPTABLE TABLE
        //                PdfPTable table_1 = new PdfPTable(3);
        //                // table_1.TotalWidth = 270f;
        //                table_1.TotalWidth = 285f;
        //                table_1.LockedWidth = true;

        //                table_1.SetWidths(new float[] { 1.5f, 8.5f, 4.5f });
        //                //table_1.SetWidths(new float[] { 1.5f, 8.5f, 5.5f });
        //                table_1.DefaultCell.Border = (PdfPCell.NO_BORDER);
        //                table_1.DefaultCell.PaddingRight = 4;
        //                table_1.DefaultCell.PaddingBottom = 1;

        //                table_1.AddCell(new Phrase("Total Qty:", fontstylebody));

        //                PdfPCell cellPartcode = new PdfPCell(new Phrase("*" + strQty + "*", barcodefont));
        //                cellPartcode.PaddingTop = -1;
        //                cellPartcode.Border = 0;
        //                table_1.AddCell(cellPartcode);

        //                //table_1.AddCell(new Phrase("Partname: " + strPname, fontstylebody6));

        //                table_1.AddCell(new Phrase("", barcodelabel));
        //                table_1.AddCell(new Phrase(strQty, barcodelabel2));
        //                //table_1.AddCell(new Phrase("Reason: " + strReason, fontstylebody4));

        //                table_1.WriteSelectedRows(0, -1, pdfDoc.Left + right, pdfDoc.Top - 65 - top, writer.DirectContent);

        //                PdfPTable table_2 = new PdfPTable(3);
        //                table_2.TotalWidth = 285f;
        //                table_2.LockedWidth = true;

        //                table_2.SetWidths(new float[] { 1.5f, 10f, 3f });
        //                table_2.DefaultCell.Border = (PdfPCell.NO_BORDER);
        //                table_2.DefaultCell.PaddingBottom = 1;

        //                table_2.AddCell(new Phrase("PartName:", fontstylebody));
        //                PdfPCell cellLotno = new PdfPCell(new Phrase(strPartName.ToUpper(), fontstylebody));
        //                cellLotno.PaddingTop = -1;
        //                cellLotno.Border = 0;
        //                table_2.AddCell(cellLotno);
        //                table_2.AddCell(new Phrase("", barcodelabel));
        //                table_2.AddCell(new Phrase("", barcodelabel));
        //                table_2.AddCell(new Phrase(strPartName.ToUpper(), barcodelabel2));
        //                table_2.AddCell(new Phrase("", barcodelabel));
        //                table_2.AddCell(new Phrase("Date:", fontstylebody));

        //                PdfPCell cellQty = new PdfPCell(new Phrase(strDate, fontstylebody));
        //                cellQty.PaddingTop = -1;
        //                cellQty.Border = 0;
        //                table_2.AddCell(cellQty);
        //                table_2.AddCell(new Phrase("", barcodelabel));

        //                table_2.AddCell(new Phrase("", barcodelabel));
        //                table_2.AddCell(new Phrase(strDate, barcodelabel2));
        //                table_2.AddCell(new Phrase("", barcodelabel));

        //                //till here
        //                string strRemarks1 = "";
        //                if (strRemarks != "")
        //                {
        //                    strRemarks1 = "*" + strRemarks + "*";
        //                }
        //                else
        //                {
        //                    strRemarks1 = "";
        //                }

        //                table_2.AddCell(new Phrase("Time:", fontstylebody));
        //                PdfPCell cellRemarks = new PdfPCell(new Phrase(strTime, fontstylebody));
        //                cellRemarks.PaddingTop = -1;
        //                cellRemarks.Border = 0;
        //                table_2.AddCell(cellRemarks);
        //                table_2.AddCell(new Phrase("", barcodelabel));
        //                table_2.AddCell(new Phrase("", barcodelabel));
        //                table_2.AddCell(new Phrase(strTime, barcodelabel2));
        //                table_2.AddCell(new Phrase("", barcodelabel));
        //                table_2.WriteSelectedRows(0, -1, pdfDoc.Left + right, pdfDoc.Top - 96 - top, writer.DirectContent);

        //                cell.UseVariableBorders = true;

        //                table_2.AddCell(new Phrase("GNS+ In-Charge:", fontstylebody));
        //                PdfPCell cellRemarks = new PdfPCell(new Phrase("", fontstylebody));
        //                cellRemarks.PaddingTop = -1;
        //                cellRemarks.Border = 0;
        //                table_2.AddCell(cellRemarks);
        //                table_2.AddCell(new Phrase("", barcodelabel));
        //                table_2.AddCell(new Phrase("", barcodelabel));
        //                table_2.AddCell(new Phrase("", barcodelabel2));
        //                table_2.AddCell(new Phrase("", barcodelabel));
        //                table_2.WriteSelectedRows(0, -1, pdfDoc.Left + right, pdfDoc.Top - 96 - top, writer.DirectContent);


        //                PdfPTable table2 = new PdfPTable(5);
        //                table2.TotalWidth = 270f;
        //                table2.LockedWidth = true;
        //                table2.SetWidths(new int[5] { 1, 1, 1, 1, 1 });
        //                table2.DefaultCell.HorizontalAlignment = 1;
        //                table2.DefaultCell.PaddingBottom = 33;



        //                iTextSharp.text.Font fontstyle = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 7f, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);

        //                table2.WriteSelectedRows(0, -1, pdfDoc.Left + right, pdfDoc.Top - 5 - top, writer.DirectContent);
        //                int lrc = 0;


        //                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
        //                string value = "Z1" + strPcode + "|" + "Z7" + strSupHeader + "|" + "Z2" + strLotNo.ToUpper() + "|" + "Z3" + strQty + "|" + "Z4" + strRemarks + "|" + "Z5" + strRefNo + "|" + "Z6MW6DEMO";

        //                //TO CONVERT QR CODE IMAGE INTO BASE64STRING
        //                string base64String = Convert.ToBase64String(imageToByteArray(BarcodeDrawFactory.CodeQr.Draw(value, 30)));

        //                //TO SET IMAGE VALUE TO QR CODE
        //                Byte[] bytes = Convert.FromBase64String(base64String);
        //                // add a image
        //                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(bytes);

        //                PdfContentByte cb = writer.DirectContent;
        //                cb.AddImage(jpg, 35, 0, 0, 35, pdfDoc.PageSize.Width - x + 230 + lrc, y + 125, true);



        //                lrc = lrc + 44;
        //                PdfContentByte cb7 = writer.DirectContent;
        //                cb7.Rectangle(pdfDoc.PageSize.Width - x - 41 + lrc, y - 5, 280, 187);
        //                cb7.SetLineWidth(1f);
        //                cb7.SetColorStroke(iTextSharp.text.Color.RED);
        //                cb7.Stroke();
        //                lrc = lrc + 44;

        //                PdfContentByte cbLoc = writer.DirectContent;
        //                cbLoc.BeginText();

        //                BaseFont bfLoc = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);

        //                cbLoc.SetFontAndSize(bf, 12);
        //                cbLoc.SetColorFill(new Color(System.Drawing.Color.FromName(strTextColor)));

        //                cbLoc.ShowTextAligned(PdfContentByte.ALIGN_CENTER, strLoc, 35 + right, 805 - (j * 205), 0);

        //                cbLoc.EndText();
        //                irow++;


        //                //end of i

        //                if (j >= 2)
        //                {

        //                    top = top + 205;
        //                }
        //                else
        //                {
        //                    top = top + 205;
        //                }
        //                y = y - 205f;


        //            }//end of j

        //            htmlparser.Parse(sr);
        //            pdfDoc.Close();
        //            ClearData();

        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.GetInstance().Fatal(ex.StackTrace, ex);
        //            MsgBox1.alert(ex.Message.ToString() + "Please contact ISD.");
        //        }
        //}
    }
}