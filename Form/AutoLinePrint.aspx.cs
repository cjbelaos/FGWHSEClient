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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
//using System.Drawing;
using System.Text;
using com.eppi.utils;
using System.Threading;
//using System.Drawing;
using ZXing.Common;
using ZXing;
using ZXing.QrCode;
using ZXing.OneD;
using Zen.Barcode;
using FGWHSEClient;
using FGWHSEClient.DAL;
using System.Text.RegularExpressions;


using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;


namespace FGWHSEClient.Form
{
    public partial class AutoLinePrint : System.Web.UI.Page
    {
        AutoLineDAL autoDAL = new AutoLineDAL();
        DataView dvLot = new DataView();
        protected void Page_Load(object sender, EventArgs e)
        {
            //generateLot();
            //DataTable dtPrint = (DataTable)Session["dtPRINT"];
            //DataView dv = dtPrint.DefaultView;
            //if (dv.Count > 0)
            //{
            //    printPreview(dv);
            //}

            if (Session["dtPRINT"] != null)
            {
                printLot();
            }


        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }




        public void printLot()
        {
            dsAutoLine dsAuto = new dsAutoLine();
            ReportDocument crystalReport = new ReportDocument();

            string strPath = Server.MapPath("../Form/AutoLineBarcode.rpt");
            crystalReport.Load(strPath);
            string strOldRefno = "", strRIFD = "", strQRCODE = "", strITEMCODE = "", strLOTNO = "", strREFNO = "", strQTY = "", strREMARKS = "", strArea = "", strItemDesc = "";
            int intBR = 0, intBG = 0, intBB = 0, intFR = -1, intFG = -1, intFB = -1;
            if (Session["dtPRINT"] != null)
            {
                BarcodeWriter ZX = new BarcodeWriter();
               
                dvLot = ((DataTable)Session["dtPRINT"]).DefaultView;
                foreach (DataRow dr in dvLot.Table.Rows)
                {
                    strOldRefno = dr["OLDREFNO"].ToString().ToUpper();
                    strRIFD = dr["RFIDTAG"].ToString().ToUpper();
                    strQRCODE = dr["QRCODE"].ToString().ToUpper();
                    strITEMCODE = dr["ITEMCODE"].ToString().ToUpper();
                    strLOTNO = dr["LOTNO"].ToString().ToUpper();
                    strREFNO = dr["REFNO"].ToString().ToUpper();
                    strQTY = dr["QTY"].ToString().ToUpper();
                    strREMARKS = dr["REMARKS"].ToString().ToUpper();
                    strItemDesc = dr["ITEMDESC"].ToString().ToUpper();
                    strArea = dr["AREA"].ToString().ToUpper();

                    intBR = Convert.ToInt32(dr["BR"].ToString().ToUpper());
                    intBG = Convert.ToInt32(dr["BG"].ToString().ToUpper());
                    intBB = Convert.ToInt32(dr["BB"].ToString().ToUpper());
                    intFR = Convert.ToInt32(dr["FR"].ToString().ToUpper());
                    intFG = Convert.ToInt32(dr["FG"].ToString().ToUpper());
                    intFB = Convert.ToInt32(dr["FB"].ToString().ToUpper());

                    autoDAL.ADD_AUTOLINE_BARCODE(strOldRefno, strREFNO, strRIFD, strLOTNO, strITEMCODE, strQTY, strREMARKS, Session["UserID"].ToString(), strArea, Session["clientIp"].ToString());

                    if (strREMARKS == "")
                    {
                        strREMARKS = "N/A";
                    }


                   
                    //ZXing.QrCode.QrCodeEncodingOptions opt = new ZXing.QrCode.QrCodeEncodingOptions();
                    //opt.ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.L;
                    //opt.PureBarcode = true;
                    //ZX.Format = ZXing.BarcodeFormat.QR_CODE;
                    //ZX.Options = opt;


                    dsAuto.dtAutoLine.AdddtAutoLineRow(
                        strQRCODE,
                        strITEMCODE,
                        strLOTNO,
                        strREFNO,
                        strQTY,
                        strREMARKS,
                        imageToByteArray(BarcodeDrawFactory.CodeQr.Draw(strQRCODE, 25)),
                        //imageToByteArray(ZX.Write(strQRCODE)),
                        imageToByteArray(BarcodeDrawFactory.Code128WithChecksum.Draw(strITEMCODE, 25, 10)),
                        imageToByteArray(BarcodeDrawFactory.Code128WithChecksum.Draw(strLOTNO, 25, 10)),
                        imageToByteArray(BarcodeDrawFactory.Code128WithChecksum.Draw(strREFNO, 25, 10)),
                        imageToByteArray(BarcodeDrawFactory.Code128WithChecksum.Draw(strQTY, 25, 10)),
                        imageToByteArray(BarcodeDrawFactory.Code128WithChecksum.Draw(strREMARKS, 25, 10)),
                        strItemDesc,
                        intBR,
                        intBG,
                        intBB,
                        intFR,
                        intFG,
                        intFB
                        );
                }

              
                crystalReport.SetDataSource(dsAuto);
                CrystalReportViewer1.ReportSource = crystalReport;

                ExportOptions options = new ExportOptions();

                options.ExportFormatType = ExportFormatType.PortableDocFormat;

                options.FormatOptions = new PdfRtfWordFormatOptions();

                ExportRequestContext req = new ExportRequestContext();

                req.ExportInfo = options;


                Stream s = crystalReport.FormatEngine.ExportToStream(req);

                Response.ClearHeaders();

                Response.ClearContent();

                Response.ContentType = "application/pdf";


                s.Seek(0, SeekOrigin.Begin);

                byte[] buffer = new byte[s.Length];

                s.Read(buffer, 0, (int)s.Length);

                Response.BinaryWrite(buffer);

                Response.End();

            }


            crystalReport.Close();
            crystalReport.Dispose();
            
        }
        


















































        public void printPreview(DataView dv)
        {
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 20f, 20f, 5f, 10f);

            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();


            int totalfonts = FontFactory.RegisterDirectory(@"C:\Windows\Fonts");
            string fontpath = Environment.GetEnvironmentVariable("SystemRoot");
            BaseFont customfont = BaseFont.CreateFont("C:\\inetpub\\wwwroot\\EWHS\\FGWHSE\\FGWHSEClient\\V100001_.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font fontstyles = FontFactory.GetFont("Arial", 8, Font.ITALIC, new Color(System.Drawing.Color.FromName("BLACK")));
            iTextSharp.text.Font fontstylebody = FontFactory.GetFont("Arial", 5, Font.NORMAL, new Color(System.Drawing.Color.FromName("BLACK")));
            iTextSharp.text.Font barcodefont = new iTextSharp.text.Font(customfont, 9);
            //C39P24DhTt
            iTextSharp.text.Font barcodelabel = FontFactory.GetFont("Verdana", 4);
            iTextSharp.text.Font barcodelabel_1 = FontFactory.GetFont("Verdana", 6);
            iTextSharp.text.Font remarkslabel = FontFactory.GetFont("Verdana", 40);

            int intMaxCount = dv.Count;

            for (int x = 0; x < 1; x++)
            {
                PdfPTable tableMain = new PdfPTable(4);
                tableMain.TotalWidth = 152;
                tableMain.LockedWidth = true;
                tableMain.DefaultCell.PaddingBottom = 1;
                tableMain.DefaultCell.Border = (PdfPCell.NO_BORDER);


                tableMain.AddCell(new Phrase("Ref No:", fontstylebody));
                PdfPCell cellRefno = new PdfPCell(new Phrase("*" + dv[x]["refno"].ToString() + "*", barcodefont));
                cellRefno.PaddingTop = -1;
                cellRefno.Border = 0;
                cellRefno.Colspan = 3;
                tableMain.AddCell(cellRefno);



                tableMain.AddCell(new Phrase("Parts Code:", fontstylebody));
                PdfPCell cellPartsCode = new PdfPCell(new Phrase("*" + dv[x]["ITEMCODE"].ToString() + "*", barcodefont));
                cellPartsCode.PaddingTop = -1;
                cellPartsCode.Border = 0;
                cellPartsCode.Colspan = 3;
                tableMain.AddCell(cellPartsCode);


                //string base64String = Convert.ToBase64String(imageToByteArray(BarcodeDrawFactory.CodeQr.Draw("*"+ dv[x]["QRCODE"].ToString() +"*", 30)));
                //Byte[] bytes = Convert.FromBase64String(base64String);
                //iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(bytes);
                //jpg.ScaleAbsoluteHeight(35);
                //jpg.ScaleAbsoluteWidth(35);
                //jpg.Alignment = iTextSharp.text.Image.ALIGN_CENTER;


                //PdfPCell imageCell = new PdfPCell(jpg);
                //imageCell.Colspan = 2; // either 1 if you need to insert one cell
                //imageCell.Border = 1;
                //imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                //imageCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //tableMain.AddCell(imageCell);

                tableMain.AddCell(new Phrase("Lot No :", fontstylebody));
                PdfPCell cellLotNo = new PdfPCell(new Phrase("*" + dv[x]["LOTNO"].ToString() + "*", barcodefont));
                cellLotNo.PaddingTop = -1;
                cellLotNo.Border = 0;
                cellLotNo.Colspan = 3;
                tableMain.AddCell(cellLotNo);


                tableMain.AddCell(new Phrase("QTY :", fontstylebody));
                PdfPCell cellQty = new PdfPCell(new Phrase("*" + dv[x]["QTY"].ToString() + "*", barcodefont));
                cellQty.PaddingTop = -1;
                cellQty.Border = 0;
                cellQty.Colspan = 1;
                tableMain.AddCell(cellQty);


                tableMain.AddCell(new Phrase("Remarks :", fontstylebody));
                PdfPCell cellRemarks = new PdfPCell(new Phrase("*" + dv[x]["REMARKS"].ToString() + "*", barcodefont));
                cellRemarks.PaddingTop = -1;
                cellRemarks.Border = 0;
                cellRemarks.Colspan = 1;
                tableMain.AddCell(cellRemarks);







                tableMain.WriteSelectedRows(0, -1, pdfDoc.Left + 1, pdfDoc.Top - 1, writer.DirectContent);


                //PdfPTable table = new PdfPTable(2);
                //table.TotalWidth  = 152;
                //table.LockedWidth = true;
                //table.SetWidths(new int[2] { 1, 8 });
                //table.DefaultCell.Border = (PdfPCell.NO_BORDER);
                //table.


                //table.DefaultCell.PaddingBottom = 1;
                //table.AddCell(new Phrase("Ref No:", fontstylebody));
                ////PdfPCell cellRefno = new PdfPCell(new Phrase("*"+ dv[x]["refno"] +"*", barcodefont));
                //PdfPCell cellRefno = new PdfPCell(new Phrase("*REFNO123*", barcodefont));
                //cellRefno.PaddingTop = -1;
                //cellRefno.Border = 0;

                //table.AddCell(cellRefno);

                //table.WriteSelectedRows(0, -1, pdfDoc.Left + 1, pdfDoc.Top - 1, writer.DirectContent);
            }

            pdfDoc.Close();
        }













        public void generateLot()
        {
            int intPrintLoop = 0;
            decimal decLoopInitialWithDecimal = 0;
            decimal decLoopInitialWithoutDecimal = 0;
            Decimal decLastQty = 0;
            string strBgColorLast = "", strBgColor = "BLACK";

            DataTable dtPrint = new DataTable();
            dtPrint.Columns.Add("QRCODE", typeof(System.String));
            dtPrint.Columns.Add("ITEMCODE", typeof(System.String));
            dtPrint.Columns.Add("LOTNO", typeof(System.String));
            dtPrint.Columns.Add("REFNO", typeof(System.String));
            dtPrint.Columns.Add("QTY", typeof(System.String));
            dtPrint.Columns.Add("REMARKS", typeof(System.String));
            dtPrint.Columns.Add("AREA", typeof(System.String));
            dtPrint.Columns.Add("ITEMDESC", typeof(System.String));
            dtPrint.Columns.Add("BCOLOR", typeof(System.String));

            TextBox txtForPrint = new TextBox();
            TextBox txtRefno = new TextBox();
            Label lblOrigQTY = new Label();
            TextBox txtKittedQTY = new TextBox();
            TextBox txtArea = new TextBox();
            Label lblPartsDesc = new Label();
            Label lblPartCode = new Label();
            Label lblLotNo = new Label();
            Label lblRemarks = new Label();
            
            txtRefno.Text = "IDLIPCC21726165575008";
            lblLotNo.Text = "DLI21726D110000";
            lblRemarks.Text = "";
            lblPartCode.Text = "11111";
            txtKittedQTY.Text = "10";
            lblOrigQTY.Text = "100";
            txtForPrint.Text = "5";
            lblPartsDesc.Text = "PROCESSED PLYWOOD #42 1100MMX800MMX120MM";
            txtArea.Text = "3";

            if (txtForPrint.Text.Trim() != "")
            {
                if (!(isValidText(@"[0-9]", txtForPrint.Text)))
                {
                    msgBoxPrompt("Enter valid kitted QTY!");
                    return;
                }

                intPrintLoop = Convert.ToInt32(txtForPrint.Text);
                decLastQty = Convert.ToDecimal(lblOrigQTY.Text) - (Convert.ToDecimal(txtKittedQTY.Text) * intPrintLoop);

                if (decLastQty != 0)
                {
                    intPrintLoop = intPrintLoop + 1;
                }

                strBgColorLast = "RED";
            }
            else
            {
                decLoopInitialWithDecimal = Convert.ToDecimal(lblOrigQTY.Text) / Convert.ToDecimal(txtKittedQTY.Text);
                decLoopInitialWithoutDecimal = Math.Truncate(Convert.ToDecimal(lblOrigQTY.Text) / Convert.ToDecimal(txtKittedQTY.Text));

                intPrintLoop = Convert.ToInt32(decLoopInitialWithoutDecimal);
                if (decLoopInitialWithDecimal > decLoopInitialWithoutDecimal)
                {
                    decLastQty = Convert.ToDecimal(lblOrigQTY.Text) - (Convert.ToDecimal(txtKittedQTY.Text) * intPrintLoop);
                    intPrintLoop = intPrintLoop + 1;
                }


            }

            DataRow dr;

            string strQRCode = "", strItemCode = lblPartCode.Text, strLotNo = lblLotNo.Text, strRefNo = "", strQTY = "", strRemarks = lblRemarks.Text, strArea = txtArea.Text;
            for (int x = 1; x <= intPrintLoop; x++)
            {
                strQTY = txtKittedQTY.Text;
                if (decLastQty != 0 && x == intPrintLoop)
                {
                    strQTY = decLastQty.ToString();
                }

                int milliseconds = 50;
                Thread.Sleep(milliseconds);

                strRefNo = genRefno(txtRefno.Text.Substring(0, 1), strLotNo.Substring(0, 3));
                strQRCode = getQR(strItemCode, strLotNo, strRefNo, strQTY, strRemarks);
                if (strBgColorLast != "" && x == intPrintLoop)
                {
                    strBgColor = strBgColorLast;
                }

                dr = dtPrint.NewRow();
                dr["QRCODE"] = strQRCode;
                dr["ITEMCODE"] = strItemCode;
                dr["LOTNO"] = strLotNo;
                dr["REFNO"] = strRefNo;
                dr["QTY"] = Convert.ToDecimal(strQTY).ToString("0.##");
                dr["REMARKS"] = strRemarks;
                dr["AREA"] = strArea;
                dr["ITEMDESC"] = lblPartsDesc.Text.Trim();
                dr["BCOLOR"] = strBgColor;
                dtPrint.Rows.Add(dr);
            }
            Session["dtPRINT"] = dtPrint;
        }



        public void msgBoxPrompt(string strMessage)
        {
            Response.Write("<script>");
            Response.Write("alert('" + strMessage + "');");
            Response.Write("</script>");
        }
        bool isValidText(string strRegEx, string strText)
        {
            bool isValid = true;

            Regex regex = new Regex(strRegEx);

            foreach (char c in strText)
            {
                if (!regex.IsMatch(c.ToString()))
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        public string genRefno(string strIJPorVP, string strSupp)
        {
            string strRefno = "";


            string strClientIP = (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();
            string strPCStation = "999";

            if (strClientIP != "")
            {
                string[] strIP = strClientIP.Split('.');
                if (strIP.Length == 4)
                {
                    if (Convert.ToInt32(strIP[3].Trim()) < 10)
                    {
                        strPCStation = "00" + strIP[3].Trim();
                    }
                    else if (Convert.ToInt32(strIP[3].Trim()) < 100)
                    {
                        strPCStation = "0" + strIP[3].Trim();
                    }
                    else
                    {
                        strPCStation = strIP[3].Trim();
                    }
                }
            }



            string year = DateTime.Now.ToString("yy");

            string month = DateTime.Now.ToString("MM");
            if (month.Length >= 2)
            {
                if (month == "10")
                {
                    month = "X";
                }
                else if (month == "11")
                {
                    month = "Y";
                }
                else if (month == "12")
                {
                    month = "Z";
                }
                else
                {
                    month = DateTime.Now.ToString("MM").Substring(1, 1);
                }
            }
            string day = DateTime.Now.ToString("dd");
            string series = DateTime.Now.ToString("HHmmssfff");

            strRefno = strIJPorVP.Trim() + strSupp.Trim() + strPCStation + year + month + day + series;

            return strRefno;
        }
        public string getQR(string strItemCode, string strLotNo, string strRefNo, string strQTY, string strRemarks)
        {
            string strQR = "";
            strQR = "Z1" + strItemCode +
                         "|Z7" + strLotNo.Substring(0, 3) +
                         "|Z2" + strLotNo +
                         "|Z3" + strQTY +
                         "|Z4" + strRemarks +
                         "|Z5" + strRefNo +
                         "|Z6MW6DEMO";
            return strQR;
        }

    }
}