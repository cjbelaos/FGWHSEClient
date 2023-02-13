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
    public partial class EmptyBoxPrint : System.Web.UI.Page
    {

        EmptyPcaseDAL epDAL = new EmptyPcaseDAL();
        DataView dvPrint = new DataView();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["dtEMPTYPCASE"] != null)
            {
                printLot();
            }
        }

        public void printLot()
        {


            if (Session["dtEMPTYPCASE"] != null)
            {
                dsEmptyBox dsEBox = new dsEmptyBox();
                ReportDocument crystalReport = new ReportDocument();
                string strPath = Server.MapPath("../Form/EmptyPcase.rpt");
                crystalReport.Load(strPath);

                BarcodeWriter ZX = new BarcodeWriter();

                dvPrint = ((DataTable)Session["dtEMPTYPCASE"]).DefaultView;

                string strCONTROLNO = "", strTRACKINGNO = "", strFACTORY = "", strSUPPLIERNAME = "", strTIMEIN = "", strTIMEOUT = "", strUSERNAME = "", strPLATENO = "", strINTRFIDCOUNT = "", strINTADDITIONALCOUNT = "", strTOTALCOUNT = "", strREMAININGQTY = "";

                foreach (DataRow dr in dvPrint.Table.Rows)
                {

                    strCONTROLNO = dr["CONTROLNO"].ToString().ToUpper();
                    strTRACKINGNO = dr["TRACKINGNO"].ToString().ToUpper();
                    strFACTORY = dr["FACTORY"].ToString().ToUpper();
                    strSUPPLIERNAME = dr["SUPPLIERNAME"].ToString().ToUpper();
                    strTIMEIN = dr["TIMEIN"].ToString().ToUpper();
                    strTIMEOUT = dr["TIMEOUT"].ToString().ToUpper();
                    strUSERNAME = dr["USERNAME"].ToString().ToUpper();
                    strPLATENO = dr["PLATENO"].ToString().ToUpper();
                    strINTRFIDCOUNT = dr["INTRFIDCOUNT"].ToString().ToUpper();
                    strINTADDITIONALCOUNT = dr["INTADDITIONALCOUNT"].ToString().ToUpper();
                    strTOTALCOUNT = dr["TOTALCOUNT"].ToString().ToUpper();
                    strREMAININGQTY = dr["REMAININGQTY"].ToString().ToUpper();

                    dsEBox.dtEmptyBox.AdddtEmptyBoxRow(imageToByteArray(BarcodeDrawFactory.Code128WithChecksum.Draw(strCONTROLNO, 25, 10)),
                                                strFACTORY,
                                                strSUPPLIERNAME,
                                                strTRACKINGNO,
                                                strTIMEIN,
                                                strTIMEOUT,
                                                strUSERNAME,
                                                strPLATENO,
                                                strINTRFIDCOUNT,
                                                strINTADDITIONALCOUNT,
                                                strTOTALCOUNT,
                                                strREMAININGQTY);

                }


                crystalReport.SetDataSource(dsEBox);
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

                crystalReport.Close();
                crystalReport.Dispose();

            }


        }
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}