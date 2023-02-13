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
using System.Data.OleDb;
using System.IO;

using System.Collections.Generic;

using System.Runtime.InteropServices;
using System.Text;

using System.Threading;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;


namespace FGWHSEClient.Form
{
    public partial class WeeklyShipmentSchedule : System.Web.UI.Page
    {
        public string strAccessLevel = "";
        Maintenance maint = new Maintenance();
        DataView dv = new DataView();
        DataTable dtWSS = new DataTable();
        string strUserID;
        public int rowIndx = -1;




        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["UserID"] == null)
                {
                    Response.Write("<script>");
                    Response.Write("alert('Session expired! Please log in again.');");
                    Response.Write("window.location = '../Login.aspx';");
                    Response.Write("</script>");
                }
                else
                {
                    strUserID = Session["UserID"].ToString();
                    getLegend(trLegend);
                    getLegend(trLegendExport);

                    MaintainScrollPositionOnPostBack = true;


                    if (!this.IsPostBack)
                    {
                        //txtExFactDate.Text = DateTime.Now.ToString("dd MMM yyyy");

                        lblUserID.Text = Session["UserID"].ToString();
                        GetWeeklyShipmentSchedule();
                        lblTemp.Text = "_" + DateTime.Now.ToString("ddMMyyyyhhmmssffffff");
                        if (checkAuthority("FGWHSE_024") == true)
                        {
                            tbUpload.Attributes.Add("style", "display:compact");
                        }
                        else
                        {
                            tbUpload.Attributes.Add("style", "display:none");
                        }


                    }

                    if (Request.Form["saveConfirm"] != null)
                    {

                        if (Request.Form["saveConfirm"].ToString().Equals("1"))
                        {
                            rowIndx = Convert.ToInt32(Session["rowIndx"].ToString());
                            updateWSSdb(rowIndx);
                        }
                    }



                    if (Request.Form["uploadConfirm"] != null)
                    {

                        if (Request.Form["uploadConfirm"].ToString().Equals("1"))
                        {
                            //get_WSS_Data(fuWeeklyShipmentSchedule);
                            get_WSS_Data(Session["FILEPATH"].ToString());
                            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdWeeklyShipmentSchedule.ClientID + "', 400, 1150 , 20 ,true); </script>", false);


                        }

                        deleteTempFile(Session["FILEPATH"].ToString());
                        Session["FILEPATH"] = null;
                    }

                    if (Request.Form["postConfirm"] != null)
                    {

                        if (Request.Form["postConfirm"].ToString().Equals("1"))
                        {
                            POSTorDELETEwss("POST");
                            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdWeeklyShipmentSchedule.ClientID + "', 400, 1150 , 20 ,true); </script>", false);
                        }
                    }

                    if (Request.Form["deleteConfirm"] != null)
                    {

                        if (Request.Form["deleteConfirm"].ToString().Equals("1"))
                        {
                            POSTorDELETEwss("DELETE");
                            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdWeeklyShipmentSchedule.ClientID + "', 400, 1150 , 20 ,true); </script>", false);
                        }
                    }

                    gridviewColorOnPostback(grdWeeklyShipmentSchedule);
                    //gridviewColorOnPostback(grdExport);

                    ////ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdWeeklyShipmentSchedule.ClientID + "', 400, 1150 , 20 ,true); </script>", false);
                }
            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message.ToString());
            }
        }

        public void getLegend(HtmlTableRow trLegendRow)
        {
            DataTable dt = new DataTable();
            dt = maint.GET_WEEKLY_SHIPMENT_MAINTAINED_STATUS().Tables[0];
            dv = dt.DefaultView;

            if (chckLStatus.Items.Count == 0)
            {
                chckLStatus.DataSource = dt;
                chckLStatus.DataTextField = "STATUSDESCRIPTION";
                chckLStatus.DataValueField = "STATUSID";

                chckLStatus.DataBind();

                for (int intChk = 0; intChk < chckLStatus.Items.Count; intChk++)
                {
                    chckLStatus.Items[intChk].Selected = true;
                }

            }
            for (int x = 0; x < dv.Count; x++)
            {


                HtmlTableCell tdStatusColor = new HtmlTableCell();
                tdStatusColor.Attributes.Add("style", "border-style:solid; border-width:thin; border-top-color:black;border-left-color:black;border-right-color:lightgray;border-bottom-color:lightgray;width:13px; height:13px;background-color:" + dv[x]["COLORDISPLAY"].ToString());
                trLegendRow.Cells.Add(tdStatusColor);


                Label lblStatusBlank = new Label();
                tdStatusColor.Controls.Add(lblStatusBlank);


                HtmlTableCell tdStatus = new HtmlTableCell();
                trLegendRow.Cells.Add(tdStatus);


                Label lblStatus = new Label();
                lblStatus.Text = dv[x]["STATUSDESCRIPTION"].ToString();
                tdStatus.Controls.Add(lblStatus);

                //CheckBox chck = new CheckBox();
                //chck.Text = dv[x]["STATUSDESCRIPTION"].ToString();
                //tdStatusFilter.Controls.Add(chck);
            }


        }



        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (getSelectedCount() > 1)
                {
                    MsgBox1.alert("Select WSS must no be more than 1!");
                    return;
                }

                if (fuWeeklyShipmentSchedule.HasFile == true)
                {
                    if (checkAuthority("FGWHSE_024") == false)
                    {
                        MsgBox1.alert("You are not authorized to Upload Shipment Schedule!");
                        return;
                    }

                    MsgBox1.confirm("Are you sure you want to upload Shipment Schedule?", "uploadConfirm");

                    //Session["FILEPATH"] = fuWeeklyShipmentSchedule.PostedFile.FileName;

                    string FileName = lblUserID.Text + lblTemp.Text;
                    Session["FILEPATH"] = saveTempFile(FileName, fuWeeklyShipmentSchedule);
                    //get_WSS_Data(fuWeeklyShipmentSchedule);
                }
                else
                {
                    MsgBox1.alert("Please select WSS template file!");
                }

            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message.ToString());
            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }


        #region Excel to Dataset



        private void get_WSS_Data(string FilePath)
        {
            try
            {

                string strIDtoAdd = getIDtoPostOrDelete();

                DataTable dt = new DataTable();

                string Extension = Path.GetExtension(FilePath).ToLower();


                FilePath = Server.MapPath(FilePath);
                if (Extension == ".xls")
                {
                    dt = getExcelXls(FilePath);
                }
                else
                {
                    dt = getExcel(FilePath);
                }


                finalizeDT(dt, dtWSS);




                string strGNSINVOICENUMBER = "";
                string strPRODUCTIONLOCATION = "";
                string strSHIPPINGLOCATION = "";
                string strODNO = "";
                string strLOTNO = "";
                string strSHIPMENTNO = "";
                string strPOMONTH = "";
                string strPONUMBER = "";
                string strCONSIGNEEPONUMBER = "";
                string strPOWK = "";
                string strITEMCODE = "";
                string strMODELDESCRIPTION = "";
                string strMODELNAME = "";
                string strPLANNEDEXFACTDATE = "";
                string strPLANNEDOBDATE = "";
                //string strREVISEDEXFACEDATE = "";
                //string strREVISEDOBDATE = "";
                string strQTY = "";
                string strCUSTOMER = "";
                string strSHIMPENTMOD = "";
                string strPALLETTYPE = "";
                string strDESTINATION = "";
                string strCONTAINERQTYHC = "";
                string strCONTAINERQTY40FT = "";
                string strCONTAINERQTY20FT = "";
                string strNOOFPALLET = "";
                string strSTDCONTLOAD = "";
                string strCONTUSAGE = "";
                string strCONTUSAGERATIO = "";
                string strCAS = "";
                string strCTRNUMBER = "";
                string strCONFIRMEDDATE = "";
                string strSHIPPINGLIN = "";
                string str1STVESSEL = "";
                string str2NDVESSEL = "";
                string strVESSELDESTINATION = "";
                string strPORTOFDISCHARGE = "";
                string strETADISCHARGEPORT = "";
                string strCYCUTOFFEXFACTCUTOFF = "";
                string strLOADINGPORT = "";
                string strREASONOFDELAYEDEXFACT = "";
                string strREASONOFDELAYEDOB = "";
                string strREMARKS = "";
                string strTRADE = "";


                string strITEMCODECHECK = "";
                string strGNSINVOICENUMBERCHECK = "";
                for (int xCheck = 0; xCheck < dtWSS.Rows.Count; xCheck++)
                {
                    strGNSINVOICENUMBER = dtWSS.DefaultView[xCheck]["F1"].ToString();
                    strPRODUCTIONLOCATION = dtWSS.DefaultView[xCheck]["F2"].ToString();
                    strSHIPPINGLOCATION = dtWSS.DefaultView[xCheck]["F3"].ToString();
                    strODNO = dtWSS.DefaultView[xCheck]["F4"].ToString();
                    strLOTNO = dtWSS.DefaultView[xCheck]["F5"].ToString();
                    strSHIPMENTNO = dtWSS.DefaultView[xCheck]["F6"].ToString();
                    strPOMONTH = dtWSS.DefaultView[xCheck]["F7"].ToString();
                    strPONUMBER = dtWSS.DefaultView[xCheck]["F8"].ToString();
                    strCONSIGNEEPONUMBER = dtWSS.DefaultView[xCheck]["F9"].ToString();
                    strPOWK = dtWSS.DefaultView[xCheck]["F10"].ToString();
                    strITEMCODE = dtWSS.DefaultView[xCheck]["F11"].ToString();
                    strMODELDESCRIPTION = dtWSS.DefaultView[xCheck]["F12"].ToString();
                    strMODELNAME = dtWSS.DefaultView[xCheck]["F13"].ToString();
                    strPLANNEDEXFACTDATE = dtWSS.DefaultView[xCheck]["F14"].ToString();
                    strPLANNEDOBDATE = dtWSS.DefaultView[xCheck]["F15"].ToString();
                    strQTY = dtWSS.DefaultView[xCheck]["F16"].ToString();
                    strCUSTOMER = dtWSS.DefaultView[xCheck]["F17"].ToString();
                    strSHIMPENTMOD = dtWSS.DefaultView[xCheck]["F18"].ToString();
                    strPALLETTYPE = dtWSS.DefaultView[xCheck]["F19"].ToString();
                    strDESTINATION = dtWSS.DefaultView[xCheck]["F20"].ToString();
                    strCONTAINERQTYHC = dtWSS.DefaultView[xCheck]["F21"].ToString();
                    strCONTAINERQTY40FT = dtWSS.DefaultView[xCheck]["F22"].ToString();
                    strCONTAINERQTY20FT = dtWSS.DefaultView[xCheck]["F23"].ToString();
                    strNOOFPALLET = dtWSS.DefaultView[xCheck]["F24"].ToString();
                    strSTDCONTLOAD = dtWSS.DefaultView[xCheck]["F25"].ToString();
                    strCONTUSAGE = dtWSS.DefaultView[xCheck]["F26"].ToString();
                    strCONTUSAGERATIO = dtWSS.DefaultView[xCheck]["F27"].ToString();
                    strCAS = dtWSS.DefaultView[xCheck]["F28"].ToString();
                    strCTRNUMBER = dtWSS.DefaultView[xCheck]["F29"].ToString();
                    strCONFIRMEDDATE = dtWSS.DefaultView[xCheck]["F30"].ToString();
                    strSHIPPINGLIN = dtWSS.DefaultView[xCheck]["F31"].ToString();
                    str1STVESSEL = dtWSS.DefaultView[xCheck]["F32"].ToString();
                    str2NDVESSEL = dtWSS.DefaultView[xCheck]["F33"].ToString();
                    strVESSELDESTINATION = dtWSS.DefaultView[xCheck]["F34"].ToString();
                    strPORTOFDISCHARGE = dtWSS.DefaultView[xCheck]["F35"].ToString();
                    strETADISCHARGEPORT = dtWSS.DefaultView[xCheck]["F36"].ToString();
                    strCYCUTOFFEXFACTCUTOFF = dtWSS.DefaultView[xCheck]["F37"].ToString();
                    strLOADINGPORT = dtWSS.DefaultView[xCheck]["F38"].ToString();
                    strREASONOFDELAYEDEXFACT = dtWSS.DefaultView[xCheck]["F39"].ToString();
                    strREASONOFDELAYEDOB = dtWSS.DefaultView[xCheck]["F40"].ToString();
                    strREMARKS = dtWSS.DefaultView[xCheck]["F41"].ToString();
                    strTRADE = dtWSS.DefaultView[xCheck]["F42"].ToString();

                    string strBlankList = "";
                    //if (strGNSINVOICENUMBER.Trim() == "")
                    //{
                    //    strBlankList = strBlankList + " GNS INVOICE NUMBER,";
                    //}


                    if (strCAS.Trim() != "" && checkAuthority("FGWHSE_026") == true)
                    {
                        string strTodayDate, strCASDate;
                        strTodayDate = DateTime.Now.ToString("dd MMM yyyy hh:mm:ss tt");
                        strCASDate = Convert.ToDateTime(strCAS).AddDays(1).ToString("dd MMM yyyy") + " 08:00:00 AM";
                        if (DateTime.Now >= Convert.ToDateTime(strCASDate.Trim()) //||
                                                                                  //strTodayDate == strCASDate
                            )
                        {
                            MsgBox1.alert("Invalid CAS! " + strCAS);
                            return;
                        }
                    }

                    if (strODNO.Trim() == "")
                    {
                        strBlankList = strBlankList + " GNS OD NUMBER,";
                    }

                    if (strSHIPMENTNO.Trim() == "")
                    {
                        strBlankList = strBlankList + " GNS SHIPMENT NUMBER,";
                    }

                    if (strITEMCODE.Trim() == "")
                    {
                        strBlankList = strBlankList + " GNS ITEM CODE,";
                    }

                    if (strBlankList.Length > 2)
                    {
                        strBlankList = strBlankList.Substring(0, strBlankList.Length - 1);
                    }

                    if (strBlankList.Trim() != "" //Validate in GNS
                        )
                    {
                        MsgBox1.alert("Found blank required field! (" + strBlankList.Trim() + ")! Excel Row(" + (xCheck + 3).ToString() + "). Uploading is cancelled!");
                        return;
                    }


                    strITEMCODECHECK = strITEMCODECHECK + strITEMCODE + ",";
                    strGNSINVOICENUMBERCHECK = strGNSINVOICENUMBERCHECK + strGNSINVOICENUMBER + ",";

                }


                DataView dvItemCode = new DataView();
                DataView dvGNSINVOICENUMBER = new DataView();
                DataView dvWSS = new DataView();

                dvWSS = dtWSS.DefaultView;
                string strInvalidItemCode = "";
                int intValid = 0;

                dvItemCode = maint.GET_ITEM_CODE_VALIDATION(strITEMCODECHECK).Tables[0].DefaultView;

                dvGNSINVOICENUMBER = maint.GET_WEEKLY_SHIPMENT_SCHEDULE_DUPLICATE_VALIDATION(strGNSINVOICENUMBERCHECK).Tables[0].DefaultView;


                string dupGNSINVOICENUMBER = "",
                        dupODNO = "",
                        dupLOTNO = "",
                        dupPONUMBER = "",
                        dupITEMCODE = "",
                        dupPLANNEDEXFACTDATE = "",
                        dupPLANNEDOBDATE = "",
                        dupQTY = "",
                        dupCUSTOMER = "",
                        dupSHIMPENTMOD = "",
                        dupPALLETTYPE = "",
                        dupDESTINATION = "",

                        currGNSINVOICENUMBER = "",
                        currODNO = "",
                        currLOTNO = "",
                        currPONUMBER = "",
                        currITEMCODE = "",
                        currPLANNEDEXFACTDATE = "",
                        currPLANNEDOBDATE = "",
                        currQTY = "",
                        currCUSTOMER = "",
                        currSHIMPENTMOD = "",
                        currPALLETTYPE = "",
                        currDESTINATION = "",


                        strDuplicateFields = "";





                if (dvGNSINVOICENUMBER.Count > 0)
                {
                    for (int xii = 0; xii < dvWSS.Count; xii++)
                    {
                        dupGNSINVOICENUMBER = dtWSS.DefaultView[xii]["F1"].ToString();
                        dupODNO = dtWSS.DefaultView[xii]["F4"].ToString();
                        dupLOTNO = dtWSS.DefaultView[xii]["F5"].ToString();
                        dupPONUMBER = dtWSS.DefaultView[xii]["F8"].ToString();
                        dupITEMCODE = dtWSS.DefaultView[xii]["F11"].ToString();
                        dupPLANNEDEXFACTDATE = dtWSS.DefaultView[xii]["F14"].ToString();
                        dupPLANNEDOBDATE = dtWSS.DefaultView[xii]["F15"].ToString();
                        dupQTY = dtWSS.DefaultView[xii]["F16"].ToString();
                        dupCUSTOMER = dtWSS.DefaultView[xii]["F17"].ToString();
                        dupSHIMPENTMOD = dtWSS.DefaultView[xii]["F18"].ToString();
                        dupPALLETTYPE = dtWSS.DefaultView[xii]["F19"].ToString();
                        dupDESTINATION = dtWSS.DefaultView[xii]["F20"].ToString();


                        if (dupPLANNEDEXFACTDATE == "")
                        {
                            MsgBox1.alert("XFACT Date shoul not be blank!");
                            return;
                        }

                        DateTime xfactDate = Convert.ToDateTime(dupPLANNEDEXFACTDATE);
                        Decimal span = Convert.ToDecimal(Math.Round((DateTime.Now - xfactDate).TotalMinutes, 2).ToString());
                        if (span > 960 && span < 9440)
                        {
                            MsgBox1.alert("Uploading of Shipment Schedule for the day is only until 4 pm! Excel Row(" + (xii + 3).ToString() + ")");
                            return;
                        }
                        else if (span > 9440)
                        {
                            MsgBox1.alert("Cannot upload Shipment Schedule of the previous day!  Excel Row(" + (xii + 3).ToString() + ")");
                            return;
                        }



                        strDuplicateFields =
                        "\\n GNS INVOICE NUMBER : " + dupGNSINVOICENUMBER +
                        "\\n OD NO : " + dupODNO +
                        "\\n LOT NO : " + dupLOTNO +
                        "\\n PO NUMBER : " + dupPONUMBER +
                        "\\n ITEM CODE : " + dupITEMCODE +
                        "\\n PLANNED EXFACT DATE : " + dupPLANNEDEXFACTDATE +
                        "\\n PLANNED OB DATE : " + dupPLANNEDOBDATE +
                        "\\n QTY : " + dupQTY +
                        "\\n CUSTOMER : " + dupCUSTOMER +
                        "\\n SHIMPENT MOD : " + dupSHIMPENTMOD +
                        "\\n PALLET TYPE : " + dupPALLETTYPE +
                        "\\n DESTINATION : " + dupDESTINATION +
                        "\\n\\nNO DATA WERE UPLOADED!!!";

                        for (int xi = 0; xi < dvGNSINVOICENUMBER.Count; xi++)
                        {
                            currGNSINVOICENUMBER = dvGNSINVOICENUMBER[xi]["GNSINVOICENUMBER"].ToString();
                            currODNO = dvGNSINVOICENUMBER[xi]["ODNO"].ToString();
                            currLOTNO = dvGNSINVOICENUMBER[xi]["LOTNO"].ToString();
                            currPONUMBER = dvGNSINVOICENUMBER[xi]["PONUMBER"].ToString();
                            currITEMCODE = dvGNSINVOICENUMBER[xi]["ITEMCODE"].ToString();
                            currPLANNEDEXFACTDATE = dvGNSINVOICENUMBER[xi]["PLANNEDEXFACTDATE"].ToString();
                            currPLANNEDOBDATE = dvGNSINVOICENUMBER[xi]["PLANNEDOBDATE"].ToString();
                            currQTY = dvGNSINVOICENUMBER[xi]["QTY"].ToString();
                            currCUSTOMER = dvGNSINVOICENUMBER[xi]["CUSTOMER"].ToString();
                            currSHIMPENTMOD = dvGNSINVOICENUMBER[xi]["SHIMPENTMOD"].ToString();
                            currPALLETTYPE = dvGNSINVOICENUMBER[xi]["PALLETTYPE"].ToString();
                            currDESTINATION = dvGNSINVOICENUMBER[xi]["DESTINATION"].ToString();


                            if
                                (
                                    dupGNSINVOICENUMBER == currGNSINVOICENUMBER &&
                                    currGNSINVOICENUMBER != "" &&
                                    dupODNO == currODNO &&
                                    dupLOTNO == currLOTNO &&
                                    dupPONUMBER == currPONUMBER &&
                                    dupITEMCODE == currITEMCODE &&
                                    dupPLANNEDEXFACTDATE == currPLANNEDEXFACTDATE &&
                                    dupPLANNEDOBDATE == currPLANNEDOBDATE &&
                                    dupQTY == currQTY &&
                                    dupCUSTOMER == currCUSTOMER &&
                                    dupSHIMPENTMOD == currSHIMPENTMOD &&
                                    dupPALLETTYPE == currPALLETTYPE &&
                                    dupDESTINATION == currDESTINATION
                                )
                            {
                                MsgBox1.alert("Uploading was cancelled!\\nDuplicate data was found! Excel Row(" + (xii + 3).ToString() + ")" + strDuplicateFields);
                                return;
                            }

                        }
                    }
                }



                if (dvItemCode.Count > 0)
                {
                    for (int ii = 0; ii < dvWSS.Count; ii++)
                    {
                        intValid = 0;

                        for (int i = 0; i < dvItemCode.Count; i++)
                        {
                            if (dvItemCode[i]["Parent"].ToString().Trim().ToUpper() == dvWSS[ii]["F11"].ToString().Trim().ToUpper())
                            {
                                intValid = 1;
                                i = dvItemCode.Count;
                            }
                        }

                        if (intValid == 0)
                        {
                            strInvalidItemCode = strInvalidItemCode + dvWSS[ii]["F11"].ToString() + ", ";
                        }
                    }
                }
                else
                {
                    MsgBox1.alert("All Item Codes are invalid!");
                    return;
                }

                if (strInvalidItemCode != "")
                {
                    MsgBox1.alert("Invalid Item Code/s! (" + (strInvalidItemCode.Substring(0, strInvalidItemCode.Length - 1)).Trim() + ")");
                    return;
                }





                for (int x = 0; x < dtWSS.Rows.Count; x++)
                {


                    strGNSINVOICENUMBER = dtWSS.DefaultView[x]["F1"].ToString();
                    strPRODUCTIONLOCATION = dtWSS.DefaultView[x]["F2"].ToString();
                    strSHIPPINGLOCATION = dtWSS.DefaultView[x]["F3"].ToString();
                    strODNO = dtWSS.DefaultView[x]["F4"].ToString();
                    strLOTNO = dtWSS.DefaultView[x]["F5"].ToString();
                    strSHIPMENTNO = dtWSS.DefaultView[x]["F6"].ToString();
                    strPOMONTH = dtWSS.DefaultView[x]["F7"].ToString();
                    strPONUMBER = dtWSS.DefaultView[x]["F8"].ToString();
                    strCONSIGNEEPONUMBER = dtWSS.DefaultView[x]["F9"].ToString();
                    strPOWK = dtWSS.DefaultView[x]["F10"].ToString();
                    strITEMCODE = dtWSS.DefaultView[x]["F11"].ToString();
                    strMODELDESCRIPTION = dtWSS.DefaultView[x]["F12"].ToString();
                    strMODELNAME = dtWSS.DefaultView[x]["F13"].ToString();
                    strPLANNEDEXFACTDATE = dtWSS.DefaultView[x]["F14"].ToString();
                    strPLANNEDOBDATE = dtWSS.DefaultView[x]["F15"].ToString();
                    strQTY = dtWSS.DefaultView[x]["F16"].ToString();
                    strCUSTOMER = dtWSS.DefaultView[x]["F17"].ToString();
                    strSHIMPENTMOD = dtWSS.DefaultView[x]["F18"].ToString();
                    strPALLETTYPE = dtWSS.DefaultView[x]["F19"].ToString();
                    strDESTINATION = dtWSS.DefaultView[x]["F20"].ToString();
                    strCONTAINERQTYHC = dtWSS.DefaultView[x]["F21"].ToString();
                    strCONTAINERQTY40FT = dtWSS.DefaultView[x]["F22"].ToString();
                    strCONTAINERQTY20FT = dtWSS.DefaultView[x]["F23"].ToString();
                    strNOOFPALLET = dtWSS.DefaultView[x]["F24"].ToString();
                    strSTDCONTLOAD = dtWSS.DefaultView[x]["F25"].ToString();
                    strCONTUSAGE = dtWSS.DefaultView[x]["F26"].ToString();
                    strCONTUSAGERATIO = dtWSS.DefaultView[x]["F27"].ToString();
                    strCAS = dtWSS.DefaultView[x]["F28"].ToString();
                    strCTRNUMBER = dtWSS.DefaultView[x]["F29"].ToString();
                    strCONFIRMEDDATE = dtWSS.DefaultView[x]["F30"].ToString();
                    strSHIPPINGLIN = dtWSS.DefaultView[x]["F31"].ToString();
                    str1STVESSEL = dtWSS.DefaultView[x]["F32"].ToString();
                    str2NDVESSEL = dtWSS.DefaultView[x]["F33"].ToString();
                    strVESSELDESTINATION = dtWSS.DefaultView[x]["F34"].ToString();
                    strPORTOFDISCHARGE = dtWSS.DefaultView[x]["F35"].ToString();
                    strETADISCHARGEPORT = dtWSS.DefaultView[x]["F36"].ToString();
                    strCYCUTOFFEXFACTCUTOFF = dtWSS.DefaultView[x]["F37"].ToString();
                    strLOADINGPORT = dtWSS.DefaultView[x]["F38"].ToString();
                    strREASONOFDELAYEDEXFACT = dtWSS.DefaultView[x]["F39"].ToString();
                    strREASONOFDELAYEDOB = dtWSS.DefaultView[x]["F40"].ToString();
                    strREMARKS = dtWSS.DefaultView[x]["F41"].ToString();
                    strTRADE = dtWSS.DefaultView[x]["F42"].ToString();


                    maint.UPLOAD_WEEKLY_SHIPMENT_SCHEDULE
                        (
                        strGNSINVOICENUMBER,
                        strPRODUCTIONLOCATION,
                        strSHIPPINGLOCATION,
                        strODNO,
                        strLOTNO,
                        strSHIPMENTNO,
                        strPOMONTH,
                        strPONUMBER,
                        strCONSIGNEEPONUMBER,
                        strPOWK,
                        strITEMCODE,
                        strMODELDESCRIPTION,
                        strMODELNAME,
                        strPLANNEDEXFACTDATE,
                        strPLANNEDOBDATE,
                        strQTY,
                        strCUSTOMER,
                        strSHIMPENTMOD,
                        strPALLETTYPE,
                        strDESTINATION,
                        strCONTAINERQTYHC,
                        strCONTAINERQTY40FT,
                        strCONTAINERQTY20FT,
                        strNOOFPALLET,
                        strSTDCONTLOAD,
                        strCONTUSAGE,
                        strCONTUSAGERATIO,
                        strCAS,
                        strCTRNUMBER,
                        strCONFIRMEDDATE,
                        strSHIPPINGLIN,
                        str1STVESSEL,
                        str2NDVESSEL,
                        strVESSELDESTINATION,
                        strPORTOFDISCHARGE,
                        strETADISCHARGEPORT,
                        strCYCUTOFFEXFACTCUTOFF,
                        strLOADINGPORT,
                        strREASONOFDELAYEDEXFACT,
                        strREASONOFDELAYEDOB,
                        strREMARKS,
                        strTRADE,
                        strUserID,
                        strIDtoAdd.Trim()

                        );
                }

                //MsgBox1.alert("Uploading Successfull!");

                GetWeeklyShipmentSchedule();

                lblUploaded.Text = "Uploading Successfull!";
            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message.ToString());
            }
        }



        private void finalizeDT(DataTable dt, DataTable finalDT)
        {

            string strColumnName = "";
            dv = dt.DefaultView;

            DataRow dr;
            finalDT.Clear();

            if (dt.Rows.Count > 2)
            {
                for (int x = 0; x < dt.Columns.Count; x++)
                {
                    //strColumnName = (dv[0][x].ToString() + " " + dv[1][x].ToString()).Trim();
                    //finalDT.Columns.Add(strColumnName, typeof(String));

                    finalDT.Columns.Add("F" + (x + 1).ToString(), typeof(String));
                }



                for (int y = 2; y < dt.Rows.Count; y++)
                {
                    dr = finalDT.NewRow();
                    for (int z = 0; z < dt.Columns.Count; z++)
                    {
                        strColumnName = ("F" + (z + 1).ToString()).Trim();

                        dr[strColumnName] = dv[y][z].ToString();
                    }

                    finalDT.Rows.Add(dr);
                    finalDT.AcceptChanges();

                }
            }




        }








        #endregion




        public void GetWeeklyShipmentSchedule()
        {
            string strstat = getSelectedStatus();
            if (txtExFactDate.Text.Trim() == "" && txtExFactDateTo.Text.Trim() == "" && txtOBDate.Text.Trim() == "" && txtOBDateTo.Text.Trim() == "")
            {
                MsgBox1.alert("Please enter the input the Ex-fact date OR OB Date!");
                return;
            }

            if ((txtExFactDate.Text.Trim() == "" && txtExFactDateTo.Text.Trim() != "") || (txtExFactDate.Text.Trim() != "" && txtExFactDateTo.Text.Trim() == ""))
            {
                MsgBox1.alert("Please enter the input the Ex-fact date!");
                return;
            }

            if ((txtOBDate.Text.Trim() == "" && txtOBDateTo.Text.Trim() != "") || (txtOBDate.Text.Trim() != "" && txtOBDateTo.Text.Trim() == ""))
            {
                MsgBox1.alert("Please enter the input the Ex-fact date!");
                return;
            }
            if (strstat.Trim() == "")
            {
                MsgBox1.alert("Please select status!");
                return;
            }

            //string strOBDATETO = "", strEXFACTDATETO = "";

            //if (txtOBDate.Text.Trim() != "")
            //{
            //    strOBDATETO = Convert.ToDateTime(txtOBDateTo.Text.Trim()).AddDays(1).ToString("dd MMM yyyy");
            //}

            //if (txtExFactDateTo.Text.Trim() != "")
            //{
            //    strEXFACTDATETO = Convert.ToDateTime(txtExFactDateTo.Text.Trim()).AddDays(1).ToString("dd MMM yyyy");
            //}
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdWeeklyShipmentSchedule.ClientID + "', 400, 1150 , 20 ,true); </script>", false);
            grdWeeklyShipmentSchedule.DataSource = maint.GET_WEEKLY_SHIPMENT_SCHEDULE(txtInvoiceNo.Text.Trim(), txtODNo.Text.Trim(), txtPONo.Text.Trim(), txtItemCode.Text.Trim(), txtOBDate.Text.Trim(), txtOBDateTo.Text.Trim(), txtExFactDate.Text.Trim(), txtExFactDateTo.Text.Trim(), strstat, txtDestination.Text.Trim(), txtContainerNo.Text.Trim()).Tables[0];
            grdWeeklyShipmentSchedule.DataBind();


            //style="overflow:scroll; width:1150px; height:400px; font-size:small; color:black" 
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdWeeklyShipmentSchedule.ClientID + "', 400, 1150 , 20 ,true); </script>", false);

            grdExport.DataSource = grdWeeklyShipmentSchedule.DataSource;
            grdExport.DataBind();

            lblUploaded.Text = "";

        }

        public string getSelectedStatus()
        {
            string strstat = "";

            int intStat = chckLStatus.Items.Count;

            for (int x = 0; x < intStat; x++)
            {

                if (chckLStatus.Items[x].Selected == true)
                {
                    strstat = strstat + chckLStatus.Items[x].Value.ToString() + "/";
                }
            }

            if (strstat.Length > 1)
            {
                strstat = strstat.Substring(0, strstat.Length - 1);
            }
            return strstat;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetWeeklyShipmentSchedule();
        }

        protected void grdWeeklyShipmentSchedule_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //GridViewRow row = grdWeeklyShipmentSchedule.Rows[e.NewEditIndex];

            //int rowIndex = grdWeeklyShipmentSchedule.EditIndex;
            //GridViewRow row = grdWeeklyShipmentSchedule.Rows[rowIndex];

            //updateWSS(row);


        }



        protected void imgEdit_Click(object sender, EventArgs e)
        {



            var rowTrigger = (Control)sender;
            GridViewRow rowUpdate = (GridViewRow)rowTrigger.NamingContainer;
            Session["rowIndx"] = rowUpdate.RowIndex.ToString();


            GridViewRow row = grdWeeklyShipmentSchedule.Rows[Convert.ToInt32(rowUpdate.RowIndex.ToString())];

            Label lblCAS = ((Label)row.FindControl("lblCAS"));

            string strCAS;
            strCAS = lblCAS.Text.Trim();
            if (strCAS.Trim() != "" && checkAuthority("FGWHSE_026") == true)
            {
                string strTodayDate, strCASDate;
                strTodayDate = DateTime.Now.ToString("dd MMM yyyy hh:mm:ss tt");
                strCASDate = Convert.ToDateTime(strCAS).AddDays(1).ToString("dd MMM yyyy") + " 08:00:00 AM";
                if (DateTime.Now >= Convert.ToDateTime(strCASDate.Trim()) //||
                                                                          //strTodayDate == strCASDate
                    )
                {
                    MsgBox1.alert("Cannot Update WSS due to CAS ! " + strCAS);
                    return;
                }
            }

            updateWSS(rowUpdate);

            //ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "CallJS", "SetDivPosition();", true);
            ////ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>SetDivPosition()); </script>", false);
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdWeeklyShipmentSchedule.ClientID + "', 400, 1150 , 20 ,true); </script>", false);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "SetDivPosition", "SetDivPosition()", true);
        }
        public void updateWSS(GridViewRow row)
        {

            ImageButton imgButton = ((ImageButton)row.FindControl("imgEdit"));
            ImageButton imgBack = ((ImageButton)row.FindControl("imgBack"));
            //ImageButton imgButton = row.Cells[0].Controls[0] as ImageButton;

            Label lblID = ((Label)row.FindControl("lblID"));
            Label lblGNSINVOICENUMBER = ((Label)row.FindControl("lblGNSINVOICENUMBER"));
            Label lblPRODUCTIONLOCATION = ((Label)row.FindControl("lblPRODUCTIONLOCATION"));
            Label lblSHIPPINGLOCATION = ((Label)row.FindControl("lblSHIPPINGLOCATION"));
            Label lblODNO = ((Label)row.FindControl("lblODNO"));
            Label lblLOTNO = ((Label)row.FindControl("lblLOTNO"));
            Label lblSHIPMENTNO = ((Label)row.FindControl("lblSHIPMENTNO"));
            Label lblPOMONTH = ((Label)row.FindControl("lblPOMONTH"));
            Label lblPONUMBER = ((Label)row.FindControl("lblPONUMBER"));
            Label lblCONSIGNEEPONUMBER = ((Label)row.FindControl("lblCONSIGNEEPONUMBER"));
            Label lblPOWK = ((Label)row.FindControl("lblPOWK"));
            Label lblITEMCODE = ((Label)row.FindControl("lblITEMCODE"));
            Label lblMODELDESCRIPTION = ((Label)row.FindControl("lblMODELDESCRIPTION"));
            Label lblMODELNAME = ((Label)row.FindControl("lblMODELNAME"));
            Label lblPLANNEDEXFACTDATE = ((Label)row.FindControl("lblPLANNEDEXFACTDATE"));
            Label lblPLANNEDOBDATE = ((Label)row.FindControl("lblPLANNEDOBDATE"));
            //Label lblREVISEDEXFACEDATE = ((Label)row.FindControl("lblREVISEDEXFACEDATE"));
            //Label lblREVISEDOBDATE = ((Label)row.FindControl("lblREVISEDOBDATE"));
            Label lblQTY = ((Label)row.FindControl("lblQTY"));
            Label lblCUSTOMER = ((Label)row.FindControl("lblCUSTOMER"));
            Label lblSHIMPENTMOD = ((Label)row.FindControl("lblSHIMPENTMOD"));
            Label lblPALLETTYPE = ((Label)row.FindControl("lblPALLETTYPE"));
            Label lblDESTINATION = ((Label)row.FindControl("lblDESTINATION"));
            Label lblTRADE = ((Label)row.FindControl("lblTRADE"));
            Label lblCONTAINERQTYHC = ((Label)row.FindControl("lblCONTAINERQTYHC"));
            Label lblCONTAINERQTY40FT = ((Label)row.FindControl("lblCONTAINERQTY40FT"));
            Label lblCONTAINERQTY20FT = ((Label)row.FindControl("lblCONTAINERQTY20FT"));
            Label lblNOOFPALLET = ((Label)row.FindControl("lblNOOFPALLET"));
            Label lblSTDCONTLOAD = ((Label)row.FindControl("lblSTDCONTLOAD"));
            Label lblCONTUSAGE = ((Label)row.FindControl("lblCONTUSAGE"));
            Label lblCONTUSAGERATIO = ((Label)row.FindControl("lblCONTUSAGERATIO"));
            Label lblCAS = ((Label)row.FindControl("lblCAS"));
            Label lblCTRNUMBER = ((Label)row.FindControl("lblCTRNUMBER"));
            Label lblCTRNUMBERARRIVALDATE = ((Label)row.FindControl("lblCTRNUMBERARRIVALDATE"));
            Label lblCONFIRMEDDATE = ((Label)row.FindControl("lblCONFIRMEDDATE"));
            Label lblSHIPPINGLIN = ((Label)row.FindControl("lblSHIPPINGLIN"));
            Label lbl1STVESSEL = ((Label)row.FindControl("lbl1STVESSEL"));
            Label lbl2NDVESSEL = ((Label)row.FindControl("lbl2NDVESSEL"));
            Label lblVESSELDESTINATION = ((Label)row.FindControl("lblVESSELDESTINATION"));
            Label lblPORTOFDISCHARGE = ((Label)row.FindControl("lblPORTOFDISCHARGE"));
            Label lblETADISCHARGEPORT = ((Label)row.FindControl("lblETADISCHARGEPORT"));
            Label lblCYCUTOFFEXFACTCUTOFF = ((Label)row.FindControl("lblCYCUTOFFEXFACTCUTOFF"));
            Label lblLOADINGPORT = ((Label)row.FindControl("lblLOADINGPORT"));
            Label lblREASONOFDELAYEDEXFACT = ((Label)row.FindControl("lblREASONOFDELAYEDEXFACT"));
            Label lblREASONOFDELAYEDOB = ((Label)row.FindControl("lblREASONOFDELAYEDOB"));
            Label lblREMARKS = ((Label)row.FindControl("lblREMARKS"));
            Label lblWSSSTATUSFLAG = ((Label)row.FindControl("lblWSSSTATUSFLAG"));
            //Label lblCREATEDBY = ((Label)row.FindControl("lblCREATEDBY"));
            //Label lblCREATEDDATE = ((Label)row.FindControl("lblCREATEDDATE"));
            //Label lblUPDATEDBY = ((Label)row.FindControl("lblUPDATEDBY"));
            //Label lblUPDATEDDATE = ((Label)row.FindControl("lblUPDATEDDATE"));

            //TextBox txtID = ((TextBox)row.FindControl("txtID"));
            TextBox txtGNSINVOICENUMBER = ((TextBox)row.FindControl("txtGNSINVOICENUMBER"));
            TextBox txtPRODUCTIONLOCATION = ((TextBox)row.FindControl("txtPRODUCTIONLOCATION"));
            TextBox txtSHIPPINGLOCATION = ((TextBox)row.FindControl("txtSHIPPINGLOCATION"));
            TextBox txtODNO = ((TextBox)row.FindControl("txtODNO"));
            TextBox txtLOTNO = ((TextBox)row.FindControl("txtLOTNO"));
            TextBox txtSHIPMENTNO = ((TextBox)row.FindControl("txtSHIPMENTNO"));
            TextBox txtPOMONTH = ((TextBox)row.FindControl("txtPOMONTH"));
            TextBox txtPONUMBER = ((TextBox)row.FindControl("txtPONUMBER"));
            TextBox txtCONSIGNEEPONUMBER = ((TextBox)row.FindControl("txtCONSIGNEEPONUMBER"));
            TextBox txtPOWK = ((TextBox)row.FindControl("txtPOWK"));
            TextBox txtITEMCODE = ((TextBox)row.FindControl("txtITEMCODE"));
            TextBox txtMODELDESCRIPTION = ((TextBox)row.FindControl("txtMODELDESCRIPTION"));
            TextBox txtMODELNAME = ((TextBox)row.FindControl("txtMODELNAME"));
            TextBox txtPLANNEDEXFACTDATE = ((TextBox)row.FindControl("txtPLANNEDEXFACTDATE"));
            TextBox txtPLANNEDOBDATE = ((TextBox)row.FindControl("txtPLANNEDOBDATE"));
            //TextBox txtREVISEDEXFACEDATE = ((TextBox)row.FindControl("txtREVISEDEXFACEDATE"));
            //TextBox txtREVISEDOBDATE = ((TextBox)row.FindControl("txtREVISEDOBDATE"));
            TextBox txtQTY = ((TextBox)row.FindControl("txtQTY"));
            TextBox txtCUSTOMER = ((TextBox)row.FindControl("txtCUSTOMER"));
            TextBox txtSHIMPENTMOD = ((TextBox)row.FindControl("txtSHIMPENTMOD"));
            TextBox txtPALLETTYPE = ((TextBox)row.FindControl("txtPALLETTYPE"));
            TextBox txtDESTINATION = ((TextBox)row.FindControl("txtDESTINATION"));
            TextBox txtTRADE = ((TextBox)row.FindControl("txtTRADE"));
            TextBox txtCONTAINERQTYHC = ((TextBox)row.FindControl("txtCONTAINERQTYHC"));
            TextBox txtCONTAINERQTY40FT = ((TextBox)row.FindControl("txtCONTAINERQTY40FT"));
            TextBox txtCONTAINERQTY20FT = ((TextBox)row.FindControl("txtCONTAINERQTY20FT"));
            TextBox txtNOOFPALLET = ((TextBox)row.FindControl("txtNOOFPALLET"));
            TextBox txtSTDCONTLOAD = ((TextBox)row.FindControl("txtSTDCONTLOAD"));
            TextBox txtCONTUSAGE = ((TextBox)row.FindControl("txtCONTUSAGE"));
            TextBox txtCONTUSAGERATIO = ((TextBox)row.FindControl("txtCONTUSAGERATIO"));
            TextBox txtCAS = ((TextBox)row.FindControl("txtCAS"));
            TextBox txtCTRNUMBER = ((TextBox)row.FindControl("txtCTRNUMBER"));
            TextBox txtCTRNUMBERARRIVALDATE = ((TextBox)row.FindControl("txtCTRNUMBERARRIVALDATE"));
            TextBox txtCONFIRMEDDATE = ((TextBox)row.FindControl("txtCONFIRMEDDATE"));
            TextBox txtSHIPPINGLIN = ((TextBox)row.FindControl("txtSHIPPINGLIN"));
            TextBox txt1STVESSEL = ((TextBox)row.FindControl("txt1STVESSEL"));
            TextBox txt2NDVESSEL = ((TextBox)row.FindControl("txt2NDVESSEL"));
            TextBox txtVESSELDESTINATION = ((TextBox)row.FindControl("txtVESSELDESTINATION"));
            TextBox txtPORTOFDISCHARGE = ((TextBox)row.FindControl("txtPORTOFDISCHARGE"));
            TextBox txtETADISCHARGEPORT = ((TextBox)row.FindControl("txtETADISCHARGEPORT"));
            TextBox txtCYCUTOFFEXFACTCUTOFF = ((TextBox)row.FindControl("txtCYCUTOFFEXFACTCUTOFF"));
            TextBox txtLOADINGPORT = ((TextBox)row.FindControl("txtLOADINGPORT"));
            TextBox txtREASONOFDELAYEDEXFACT = ((TextBox)row.FindControl("txtREASONOFDELAYEDEXFACT"));
            TextBox txtREASONOFDELAYEDOB = ((TextBox)row.FindControl("txtREASONOFDELAYEDOB"));
            TextBox txtREMARKS = ((TextBox)row.FindControl("txtREMARKS"));
            TextBox txtWSSSTATUSFLAG = ((TextBox)row.FindControl("txtWSSSTATUSFLAG"));
            //TextBox txtCREATEDBY = ((TextBox)row.FindControl("txtCREATEDBY"));
            //TextBox txtCREATEDDATE = ((TextBox)row.FindControl("txtCREATEDDATE"));
            //TextBox txtUPDATEDBY = ((TextBox)row.FindControl("txtUPDATEDBY"));
            //TextBox txtUPDATEDDATE = ((TextBox)row.FindControl("txtUPDATEDDATE"));



            if (imgButton.ImageUrl.ToString() == "~/Image/updatelogo.png")
            {
                if (checkAuthority("FGWHSE_024") == true || checkAuthority("FGWHSE_025") == true || checkAuthority("FGWHSE_026") == true)
                {
                    imgButton.ImageUrl = "~/Image/savelogo.png";
                    imgBack.Visible = true;

                }
                else
                {
                    MsgBox1.alert("You are not authorized for editing!");
                    return;
                }

                if (checkAuthority("FGWHSE_025") == true)
                {
                    Session["UPDATETYPE"] = "FGWHSE_025";

                    DisplayLabel(lblGNSINVOICENUMBER, false);
                    DisplayLabel(lblCONFIRMEDDATE, false);
                    DisplayLabel(lblSHIPPINGLIN, false);

                    DisplayLabel(lbl1STVESSEL, false);
                    DisplayLabel(lbl2NDVESSEL, false);
                    DisplayLabel(lblVESSELDESTINATION, false);

                    DisplayLabel(lblPORTOFDISCHARGE, false);
                    DisplayLabel(lblETADISCHARGEPORT, false);
                    DisplayLabel(lblCYCUTOFFEXFACTCUTOFF, false);
                    DisplayLabel(lblLOADINGPORT, false);

                    DisplayLabel(lblREASONOFDELAYEDOB, false);
                    DisplayLabel(lblREMARKS, false);


                    DisplayTextBox(txtGNSINVOICENUMBER, true);
                    DisplayTextBox(txtCONFIRMEDDATE, true);
                    DisplayTextBox(txtSHIPPINGLIN, true);

                    DisplayTextBox(txt1STVESSEL, true);
                    DisplayTextBox(txt2NDVESSEL, true);
                    DisplayTextBox(txtVESSELDESTINATION, true);

                    DisplayTextBox(txtPORTOFDISCHARGE, true);
                    DisplayTextBox(txtETADISCHARGEPORT, true);
                    DisplayTextBox(txtCYCUTOFFEXFACTCUTOFF, true);
                    DisplayTextBox(txtLOADINGPORT, true);

                    DisplayTextBox(txtREASONOFDELAYEDOB, true);
                    DisplayTextBox(txtREMARKS, true);

                }
                else if (checkAuthority("FGWHSE_024") == true)
                {
                    Session["UPDATETYPE"] = "FGWHSE_024";

                    DisplayLabel(lblPRODUCTIONLOCATION, false);
                    DisplayLabel(lblSHIPPINGLOCATION, false);
                    DisplayLabel(lblODNO, false);
                    DisplayLabel(lblLOTNO, false);
                    DisplayLabel(lblSHIPMENTNO, false);
                    DisplayLabel(lblPOMONTH, false);
                    DisplayLabel(lblPONUMBER, false);
                    DisplayLabel(lblCONSIGNEEPONUMBER, false);
                    DisplayLabel(lblPOWK, false);
                    DisplayLabel(lblITEMCODE, false);
                    DisplayLabel(lblMODELDESCRIPTION, false);
                    DisplayLabel(lblMODELNAME, false);
                    DisplayLabel(lblPLANNEDEXFACTDATE, false);
                    DisplayLabel(lblPLANNEDOBDATE, false);
                    //DisplayLabel(lblREVISEDEXFACEDATE, false);
                    //DisplayLabel(lblREVISEDOBDATE, false);
                    DisplayLabel(lblQTY, false);
                    DisplayLabel(lblCUSTOMER, false);
                    DisplayLabel(lblSHIMPENTMOD, false);
                    DisplayLabel(lblPALLETTYPE, false);
                    DisplayLabel(lblDESTINATION, false);
                    DisplayLabel(lblTRADE, false);
                    DisplayLabel(lblCONTAINERQTYHC, false);
                    DisplayLabel(lblCONTAINERQTY40FT, false);
                    DisplayLabel(lblCONTAINERQTY20FT, false);
                    DisplayLabel(lblNOOFPALLET, false);
                    DisplayLabel(lblSTDCONTLOAD, false);
                    DisplayLabel(lblCONTUSAGE, false);
                    DisplayLabel(lblCONTUSAGERATIO, false);
                    DisplayLabel(lblCAS, false);

                    //DisplayLabel(lblCTRNUMBERARRIVALDATE, false);


                    DisplayLabel(lblREASONOFDELAYEDEXFACT, false);
                    DisplayLabel(lblREMARKS, false);

                    DisplayLabel(lblWSSSTATUSFLAG, false);
                    //DisplayLabel(lblCREATEDBY, false);
                    //DisplayLabel(lblCREATEDDATE, false);
                    //DisplayLabel(lblUPDATEDBY, false);
                    //DisplayLabel(lblUPDATEDDATE, false);



                    DisplayTextBox(txtPRODUCTIONLOCATION, true);
                    DisplayTextBox(txtSHIPPINGLOCATION, true);
                    DisplayTextBox(txtODNO, true);
                    DisplayTextBox(txtLOTNO, true);
                    DisplayTextBox(txtSHIPMENTNO, true);
                    DisplayTextBox(txtPOMONTH, true);
                    DisplayTextBox(txtPONUMBER, true);
                    DisplayTextBox(txtCONSIGNEEPONUMBER, true);
                    DisplayTextBox(txtPOWK, true);
                    DisplayTextBox(txtITEMCODE, true);
                    DisplayTextBox(txtMODELDESCRIPTION, true);
                    DisplayTextBox(txtMODELNAME, true);
                    DisplayTextBox(txtPLANNEDEXFACTDATE, true);
                    DisplayTextBox(txtPLANNEDOBDATE, true);
                    //DisplayTextBox(txtREVISEDEXFACEDATE, true);
                    //DisplayTextBox(txtREVISEDOBDATE, true);
                    DisplayTextBox(txtQTY, true);
                    DisplayTextBox(txtCUSTOMER, true);
                    DisplayTextBox(txtSHIMPENTMOD, true);
                    DisplayTextBox(txtPALLETTYPE, true);
                    DisplayTextBox(txtDESTINATION, true);
                    DisplayTextBox(txtTRADE, true);
                    DisplayTextBox(txtCONTAINERQTYHC, true);
                    DisplayTextBox(txtCONTAINERQTY40FT, true);
                    DisplayTextBox(txtCONTAINERQTY20FT, true);
                    DisplayTextBox(txtNOOFPALLET, true);
                    DisplayTextBox(txtSTDCONTLOAD, true);
                    DisplayTextBox(txtCONTUSAGE, true);
                    DisplayTextBox(txtCONTUSAGERATIO, true);
                    DisplayTextBox(txtCAS, true);

                    //DisplayTextBox(txtCTRNUMBERARRIVALDATE, true);


                    DisplayTextBox(txtREASONOFDELAYEDEXFACT, true);
                    DisplayTextBox(txtREMARKS, true);

                    DisplayTextBox(txtWSSSTATUSFLAG, true);
                    //DisplayTextBox(txtCREATEDBY, true);
                    //DisplayTextBox(txtCREATEDDATE, true);
                    //DisplayTextBox(txtUPDATEDBY, true);
                    //DisplayTextBox(txtUPDATEDDATE, true);
                }
                else if (checkAuthority("FGWHSE_026") == true)
                {
                    Session["UPDATETYPE"] = "FGWHSE_026";
                    DisplayLabel(lblCTRNUMBER, false);

                    DisplayTextBox(txtCTRNUMBER, true);
                }


                //txtID.Text = lblID.Text;
                txtGNSINVOICENUMBER.Text = lblGNSINVOICENUMBER.Text;
                txtPRODUCTIONLOCATION.Text = lblPRODUCTIONLOCATION.Text;
                txtSHIPPINGLOCATION.Text = lblSHIPPINGLOCATION.Text;
                txtODNO.Text = lblODNO.Text;
                txtLOTNO.Text = lblLOTNO.Text;
                txtSHIPMENTNO.Text = lblSHIPMENTNO.Text;
                txtPOMONTH.Text = lblPOMONTH.Text;
                txtPONUMBER.Text = lblPONUMBER.Text;
                txtCONSIGNEEPONUMBER.Text = lblCONSIGNEEPONUMBER.Text;
                txtPOWK.Text = lblPOWK.Text;
                txtITEMCODE.Text = lblITEMCODE.Text;
                txtMODELDESCRIPTION.Text = lblMODELDESCRIPTION.Text;
                txtMODELNAME.Text = lblMODELNAME.Text;
                txtPLANNEDEXFACTDATE.Text = lblPLANNEDEXFACTDATE.Text;
                txtPLANNEDOBDATE.Text = lblPLANNEDOBDATE.Text;
                txtQTY.Text = lblQTY.Text;
                txtCUSTOMER.Text = lblCUSTOMER.Text;
                txtSHIMPENTMOD.Text = lblSHIMPENTMOD.Text;
                txtPALLETTYPE.Text = lblPALLETTYPE.Text;
                txtDESTINATION.Text = lblDESTINATION.Text;
                txtTRADE.Text = lblTRADE.Text;
                txtCONTAINERQTYHC.Text = lblCONTAINERQTYHC.Text;
                txtCONTAINERQTY40FT.Text = lblCONTAINERQTY40FT.Text;
                txtCONTAINERQTY20FT.Text = lblCONTAINERQTY20FT.Text;
                txtNOOFPALLET.Text = lblNOOFPALLET.Text;
                txtSTDCONTLOAD.Text = lblSTDCONTLOAD.Text;
                txtCONTUSAGE.Text = lblCONTUSAGE.Text;
                txtCONTUSAGERATIO.Text = lblCONTUSAGERATIO.Text;
                txtCAS.Text = lblCAS.Text;
                txtCTRNUMBER.Text = lblCTRNUMBER.Text;
                //txtCTRNUMBERARRIVALDATE.Text = lblCTRNUMBERARRIVALDATE.Text;
                txtCONFIRMEDDATE.Text = lblCONFIRMEDDATE.Text;
                txtSHIPPINGLIN.Text = lblSHIPPINGLIN.Text;
                txt1STVESSEL.Text = lbl1STVESSEL.Text;
                txt2NDVESSEL.Text = lbl2NDVESSEL.Text;
                txtVESSELDESTINATION.Text = lblVESSELDESTINATION.Text;
                txtPORTOFDISCHARGE.Text = lblPORTOFDISCHARGE.Text;
                txtETADISCHARGEPORT.Text = lblETADISCHARGEPORT.Text;
                txtCYCUTOFFEXFACTCUTOFF.Text = lblCYCUTOFFEXFACTCUTOFF.Text;
                txtLOADINGPORT.Text = lblLOADINGPORT.Text;
                txtREASONOFDELAYEDEXFACT.Text = lblREASONOFDELAYEDEXFACT.Text;
                txtREASONOFDELAYEDOB.Text = lblREASONOFDELAYEDOB.Text;
                txtREMARKS.Text = lblREMARKS.Text;
                txtWSSSTATUSFLAG.Text = lblWSSSTATUSFLAG.Text;





            }
            else
            {
                imgButton.ImageUrl = "~/Image/updatelogo.png";
                imgBack.Visible = false;

                MsgBox1.confirm("Are you sure you want to save updates?", "saveConfirm");



                DisplayLabel(lblGNSINVOICENUMBER, true);
                DisplayLabel(lblPRODUCTIONLOCATION, true);
                DisplayLabel(lblSHIPPINGLOCATION, true);
                DisplayLabel(lblODNO, true);
                DisplayLabel(lblLOTNO, true);
                DisplayLabel(lblSHIPMENTNO, true);
                DisplayLabel(lblPOMONTH, true);
                DisplayLabel(lblPONUMBER, true);
                DisplayLabel(lblCONSIGNEEPONUMBER, true);
                DisplayLabel(lblPOWK, true);
                DisplayLabel(lblITEMCODE, true);
                DisplayLabel(lblMODELDESCRIPTION, true);
                DisplayLabel(lblMODELNAME, true);
                DisplayLabel(lblPLANNEDEXFACTDATE, true);
                DisplayLabel(lblPLANNEDOBDATE, true);
                //DisplayLabel(lblREVISEDEXFACEDATE, true);
                //DisplayLabel(lblREVISEDOBDATE, true);
                DisplayLabel(lblQTY, true);
                DisplayLabel(lblCUSTOMER, true);
                DisplayLabel(lblSHIMPENTMOD, true);
                DisplayLabel(lblPALLETTYPE, true);
                DisplayLabel(lblDESTINATION, true);
                DisplayLabel(lblTRADE, true);
                DisplayLabel(lblCONTAINERQTYHC, true);
                DisplayLabel(lblCONTAINERQTY40FT, true);
                DisplayLabel(lblCONTAINERQTY20FT, true);
                DisplayLabel(lblNOOFPALLET, true);
                DisplayLabel(lblSTDCONTLOAD, true);
                DisplayLabel(lblCONTUSAGE, true);
                DisplayLabel(lblCONTUSAGERATIO, true);
                DisplayLabel(lblCAS, true);
                DisplayLabel(lblCTRNUMBER, true);
                //DisplayLabel(lblCTRNUMBERARRIVALDATE, true);
                DisplayLabel(lblCONFIRMEDDATE, true);
                DisplayLabel(lblSHIPPINGLIN, true);
                DisplayLabel(lbl1STVESSEL, true);
                DisplayLabel(lbl2NDVESSEL, true);
                DisplayLabel(lblVESSELDESTINATION, true);
                DisplayLabel(lblPORTOFDISCHARGE, true);
                DisplayLabel(lblETADISCHARGEPORT, true);
                DisplayLabel(lblCYCUTOFFEXFACTCUTOFF, true);
                DisplayLabel(lblLOADINGPORT, true);
                DisplayLabel(lblREASONOFDELAYEDEXFACT, true);
                DisplayLabel(lblREASONOFDELAYEDOB, true);
                DisplayLabel(lblREMARKS, true);
                DisplayLabel(lblWSSSTATUSFLAG, true);
                //DisplayLabel(lblCREATEDBY, true);
                //DisplayLabel(lblCREATEDDATE, true);
                //DisplayLabel(lblUPDATEDBY, true);
                //DisplayLabel(lblUPDATEDDATE, true);



                DisplayTextBox(txtGNSINVOICENUMBER, false);
                DisplayTextBox(txtPRODUCTIONLOCATION, false);
                DisplayTextBox(txtSHIPPINGLOCATION, false);
                DisplayTextBox(txtODNO, false);
                DisplayTextBox(txtLOTNO, false);
                DisplayTextBox(txtSHIPMENTNO, false);
                DisplayTextBox(txtPOMONTH, false);
                DisplayTextBox(txtPONUMBER, false);
                DisplayTextBox(txtCONSIGNEEPONUMBER, false);
                DisplayTextBox(txtPOWK, false);
                DisplayTextBox(txtITEMCODE, false);
                DisplayTextBox(txtMODELDESCRIPTION, false);
                DisplayTextBox(txtMODELNAME, false);
                DisplayTextBox(txtPLANNEDEXFACTDATE, false);
                DisplayTextBox(txtPLANNEDOBDATE, false);
                //DisplayTextBox(txtREVISEDEXFACEDATE, false);
                //DisplayTextBox(txtREVISEDOBDATE, false);
                DisplayTextBox(txtQTY, false);
                DisplayTextBox(txtCUSTOMER, false);
                DisplayTextBox(txtSHIMPENTMOD, false);
                DisplayTextBox(txtPALLETTYPE, false);
                DisplayTextBox(txtDESTINATION, false);
                DisplayTextBox(txtTRADE, false);
                DisplayTextBox(txtCONTAINERQTYHC, false);
                DisplayTextBox(txtCONTAINERQTY40FT, false);
                DisplayTextBox(txtCONTAINERQTY20FT, false);
                DisplayTextBox(txtNOOFPALLET, false);
                DisplayTextBox(txtSTDCONTLOAD, false);
                DisplayTextBox(txtCONTUSAGE, false);
                DisplayTextBox(txtCONTUSAGERATIO, false);
                DisplayTextBox(txtCAS, false);
                DisplayTextBox(txtCTRNUMBER, false);
                //DisplayTextBox(txtCTRNUMBERARRIVALDATE, false);
                DisplayTextBox(txtCONFIRMEDDATE, false);
                DisplayTextBox(txtSHIPPINGLIN, false);
                DisplayTextBox(txt1STVESSEL, false);
                DisplayTextBox(txt2NDVESSEL, false);
                DisplayTextBox(txtVESSELDESTINATION, false);
                DisplayTextBox(txtPORTOFDISCHARGE, false);
                DisplayTextBox(txtETADISCHARGEPORT, false);
                DisplayTextBox(txtCYCUTOFFEXFACTCUTOFF, false);
                DisplayTextBox(txtLOADINGPORT, false);
                DisplayTextBox(txtREASONOFDELAYEDEXFACT, false);
                DisplayTextBox(txtREASONOFDELAYEDOB, false);
                DisplayTextBox(txtREMARKS, false);
                DisplayTextBox(txtWSSSTATUSFLAG, false);
                //DisplayTextBox(txtCREATEDBY, false);
                //DisplayTextBox(txtCREATEDDATE, false);
                //DisplayTextBox(txtUPDATEDBY, false);
                //DisplayTextBox(txtUPDATEDDATE, false);


            }
        }

        public void DisplayLabel(Label lbl, Boolean boolen)
        {
            lbl.Visible = boolen;
        }

        public void DisplayTextBox(TextBox txt, Boolean boolen)
        {
            txt.Visible = boolen;
        }

        protected void grdWeeklyShipmentSchedule_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lblColorDisplay;
            CheckBox chkSelect;
            ImageButton imgEdit;
            Label lblWSSSTATUSFLAG;
            CheckBox chkCheckAll;

            Label lblUPDATEDCOLUMNS;
            Label lblTEXTCOLORDISPLAY;
            string strRowType = e.Row.RowType.ToString();

            if (strRowType == "Header")
            {

                chkCheckAll = (CheckBox)e.Row.FindControl("chkHeader");

                if (checkAuthority("FGWHSE_024") == false)
                {
                    chkCheckAll.Enabled = false;
                }

            }

            if (strRowType != "Header" && strRowType != "Footer")
            {
                lblUPDATEDCOLUMNS = (Label)e.Row.FindControl("lblUPDATEDCOLUMNS");
                lblTEXTCOLORDISPLAY = (Label)e.Row.FindControl("lblTEXTCOLORDISPLAY");

                lblColorDisplay = (Label)e.Row.FindControl("lblColorDisplay");
                chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
                imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
                lblWSSSTATUSFLAG = (Label)e.Row.FindControl("lblWSSSTATUSFLAG");

                e.Row.BackColor = System.Drawing.Color.FromName(lblColorDisplay.Text);

                if (checkAuthority("FGWHSE_024") == false)
                {
                    chkSelect.Enabled = false;
                }
                else
                {
                    chkSelect.Enabled = true;
                }


                if (checkAuthority("FGWHSE_024") == true || checkAuthority("FGWHSE_025") == true || checkAuthority("FGWHSE_026"))
                {
                    imgEdit.Enabled = true;
                }
                else
                {
                    imgEdit.Enabled = false;
                }

                //if (lblWSSSTATUSFLAG.Text.Trim() == "1" || lblWSSSTATUSFLAG.Text.Trim() == "3")
                if (lblWSSSTATUSFLAG.Text.Trim() == "3")
                {
                    chkSelect.Enabled = false;
                    imgEdit.Enabled = false;
                }


                string strColName = lblUPDATEDCOLUMNS.Text, strCurrentColumn = "";
                int intStrLength = 0, colEditedCount = 0;
                intStrLength = strColName.Length;
                Label lblColumn;

                for (int x = 0; x < intStrLength; x++)
                {
                    colEditedCount = strColName.Split('/').Length - 1;

                    if (strColName.Substring(x, 1) != "/")
                    {
                        strCurrentColumn = strCurrentColumn + strColName.Substring(x, 1);
                    }
                    else
                    {
                        lblColumn = (Label)e.Row.FindControl("lbl" + strCurrentColumn.Trim());
                        lblColumn.ForeColor = System.Drawing.Color.FromName(lblTEXTCOLORDISPLAY.Text);
                        lblColumn.Font.Bold = true;
                        strCurrentColumn = "";
                    }

                }


            }

        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            int selectedCount = getSelectedCount();
            if (selectedCount > 0)
            {
                if (checkAuthority("FGWHSE_024") == false)
                {
                    MsgBox1.alert("You are not authorized to Post Shipment Schedule!");
                    return;
                }

                MsgBox1.confirm("Are you sure you want to Post all selected item/s (" + selectedCount.ToString() + ")?", "postConfirm");
            }
            else
            {
                MsgBox1.alert("No selected item/s to Post!");
                return;
            }




            POSTorDELETEwss("POST");
        }


        public string getIDtoPostOrDelete()
        {
            Label lblID;
            CheckBox chkSelect;
            string SelectedId = "";
            for (int x = 0; x < grdWeeklyShipmentSchedule.Rows.Count; x++)
            {
                lblID = ((Label)grdWeeklyShipmentSchedule.Rows[x].FindControl("lblID"));
                chkSelect = ((CheckBox)grdWeeklyShipmentSchedule.Rows[x].FindControl("chkSelect"));

                if (chkSelect.Checked == true)
                {
                    SelectedId = SelectedId + lblID.Text + "/";
                }

                if (x + 1 == grdWeeklyShipmentSchedule.Rows.Count)
                {
                    if (SelectedId.Trim() != "")
                    {
                        SelectedId = (SelectedId.Substring(0, SelectedId.Length - 1)).Trim();
                    }
                }
            }
            return SelectedId;
        }




        public void POSTorDELETEwss(string PostOrDelete)
        {
            string strID = getIDtoPostOrDelete();
            if (strID == "")
            {
                MsgBox1.alert("Please select item to " + PostOrDelete + "!");
                return;
            }
            maint.POST_DELETE_WEEKLY_SHIPMENT_SCHEDULE(strID, PostOrDelete, txtReason.Text.Trim(), strUserID);
            txtReason.Text = "";
            GetWeeklyShipmentSchedule();

            maint.ALERT_NEWLY_POSTED_REVISED();

            MsgBox1.alert(PostOrDelete + " Successful!");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {


            int selectedCount = getSelectedCount();
            if (selectedCount > 0)
            {

                if (checkAuthority("FGWHSE_024") == false)
                {
                    MsgBox1.alert("You are not authorized to Cancel Shipment Schedule!");
                    return;
                }
                mp.Show();

                //MsgBox1.confirm("Are you sure you want to Delete all selected item/s (" + selectedCount.ToString() + ")?", "deleteConfirm");
            }
            else
            {
                MsgBox1.alert("No selected item/s to Delete!");
                return;
            }
            //POSTorDELETEwss("DELETE");
        }



        public int getSelectedCount()
        {

            CheckBox chkSelect;
            int SelectedIdCount = 0;
            for (int x = 0; x < grdWeeklyShipmentSchedule.Rows.Count; x++)
            {
                chkSelect = ((CheckBox)grdWeeklyShipmentSchedule.Rows[x].FindControl("chkSelect"));

                if (chkSelect.Checked == true)
                {
                    SelectedIdCount = SelectedIdCount + 1;
                }
            }
            return SelectedIdCount;
        }


        public void updateWSSdb(int rowIDX)
        {
            try
            {

                GridViewRow row = grdWeeklyShipmentSchedule.Rows[rowIDX];

                Label lblID = ((Label)row.FindControl("lblID"));
                TextBox txtGNSINVOICENUMBER = ((TextBox)row.FindControl("txtGNSINVOICENUMBER"));
                TextBox txtPRODUCTIONLOCATION = ((TextBox)row.FindControl("txtPRODUCTIONLOCATION"));
                TextBox txtSHIPPINGLOCATION = ((TextBox)row.FindControl("txtSHIPPINGLOCATION"));
                TextBox txtODNO = ((TextBox)row.FindControl("txtODNO"));
                TextBox txtLOTNO = ((TextBox)row.FindControl("txtLOTNO"));
                TextBox txtSHIPMENTNO = ((TextBox)row.FindControl("txtSHIPMENTNO"));
                TextBox txtPOMONTH = ((TextBox)row.FindControl("txtPOMONTH"));
                TextBox txtPONUMBER = ((TextBox)row.FindControl("txtPONUMBER"));
                TextBox txtCONSIGNEEPONUMBER = ((TextBox)row.FindControl("txtCONSIGNEEPONUMBER"));
                TextBox txtPOWK = ((TextBox)row.FindControl("txtPOWK"));
                TextBox txtITEMCODE = ((TextBox)row.FindControl("txtITEMCODE"));
                TextBox txtMODELDESCRIPTION = ((TextBox)row.FindControl("txtMODELDESCRIPTION"));
                TextBox txtMODELNAME = ((TextBox)row.FindControl("txtMODELNAME"));
                TextBox txtPLANNEDEXFACTDATE = ((TextBox)row.FindControl("txtPLANNEDEXFACTDATE"));
                TextBox txtPLANNEDOBDATE = ((TextBox)row.FindControl("txtPLANNEDOBDATE"));
                //TextBox txtREVISEDEXFACEDATE = ((TextBox)row.FindControl("txtREVISEDEXFACEDATE"));
                //TextBox txtREVISEDOBDATE = ((TextBox)row.FindControl("txtREVISEDOBDATE"));
                TextBox txtQTY = ((TextBox)row.FindControl("txtQTY"));
                TextBox txtCUSTOMER = ((TextBox)row.FindControl("txtCUSTOMER"));
                TextBox txtSHIMPENTMOD = ((TextBox)row.FindControl("txtSHIMPENTMOD"));
                TextBox txtPALLETTYPE = ((TextBox)row.FindControl("txtPALLETTYPE"));
                TextBox txtDESTINATION = ((TextBox)row.FindControl("txtDESTINATION"));
                TextBox txtTRADE = ((TextBox)row.FindControl("txtTRADE"));
                TextBox txtCONTAINERQTYHC = ((TextBox)row.FindControl("txtCONTAINERQTYHC"));
                TextBox txtCONTAINERQTY40FT = ((TextBox)row.FindControl("txtCONTAINERQTY40FT"));
                TextBox txtCONTAINERQTY20FT = ((TextBox)row.FindControl("txtCONTAINERQTY20FT"));
                TextBox txtNOOFPALLET = ((TextBox)row.FindControl("txtNOOFPALLET"));
                TextBox txtSTDCONTLOAD = ((TextBox)row.FindControl("txtSTDCONTLOAD"));
                TextBox txtCONTUSAGE = ((TextBox)row.FindControl("txtCONTUSAGE"));
                TextBox txtCONTUSAGERATIO = ((TextBox)row.FindControl("txtCONTUSAGERATIO"));
                TextBox txtCAS = ((TextBox)row.FindControl("txtCAS"));
                TextBox txtCTRNUMBER = ((TextBox)row.FindControl("txtCTRNUMBER"));
                Label lblCTRNUMBERARRIVALDATE = ((Label)row.FindControl("lblCTRNUMBERARRIVALDATE"));
                TextBox txtCONFIRMEDDATE = ((TextBox)row.FindControl("txtCONFIRMEDDATE"));
                TextBox txtSHIPPINGLIN = ((TextBox)row.FindControl("txtSHIPPINGLIN"));
                TextBox txt1STVESSEL = ((TextBox)row.FindControl("txt1STVESSEL"));
                TextBox txt2NDVESSEL = ((TextBox)row.FindControl("txt2NDVESSEL"));
                TextBox txtVESSELDESTINATION = ((TextBox)row.FindControl("txtVESSELDESTINATION"));
                TextBox txtPORTOFDISCHARGE = ((TextBox)row.FindControl("txtPORTOFDISCHARGE"));
                TextBox txtETADISCHARGEPORT = ((TextBox)row.FindControl("txtETADISCHARGEPORT"));
                TextBox txtCYCUTOFFEXFACTCUTOFF = ((TextBox)row.FindControl("txtCYCUTOFFEXFACTCUTOFF"));
                TextBox txtLOADINGPORT = ((TextBox)row.FindControl("txtLOADINGPORT"));
                TextBox txtREASONOFDELAYEDEXFACT = ((TextBox)row.FindControl("txtREASONOFDELAYEDEXFACT"));
                TextBox txtREASONOFDELAYEDOB = ((TextBox)row.FindControl("txtREASONOFDELAYEDOB"));
                TextBox txtREMARKS = ((TextBox)row.FindControl("txtREMARKS"));

                //DataView dvCheckDup = new DataView();
                //string  dupGNSINVOICENUMBER = "",
                //        dupODNO = "",
                //        dupLOTNO = "",
                //        dupPONUMBER = "",
                //        dupITEMCODE = "",
                //        dupPLANNEDEXFACTDATE = "",
                //        dupPLANNEDOBDATE = "",
                //        dupQTY = "",
                //        dupCUSTOMER = "",
                //        dupSHIMPENTMOD = "",
                //        dupPALLETTYPE = "",
                //        dupDESTINATION = "",
                //dvCheckDup = maint.GET_WEEKLY_SHIPMENT_SCHEDULE_DUPLICATE_VALIDATION(strGNSINVOICENUMBERCHECK).Tables[0].DefaultView;

                //if (dvCheckDup.Count > 0)
                //{
                //    for (int i = 0; i < dvCheckDup.Count; i++)
                //    {
                //        dupGNSINVOICENUMBER = dvCheckDup[0]["GNSINVOICENUMBER"].ToString();
                //        dupODNO = dvCheckDup[0]["ODNO"].ToString();
                //        dupLOTNO = dvCheckDup[0]["LOTNO"].ToString();
                //        dupPONUMBER = dvCheckDup[0]["PONUMBER"].ToString();
                //        dupITEMCODE = dvCheckDup[0]["ITEMCODE"].ToString();
                //        dupPLANNEDEXFACTDATE = dvCheckDup[0]["PLANNEDEXFACTDATE"].ToString();
                //        dupPLANNEDOBDATE = dvCheckDup[0]["PLANNEDOBDATE"].ToString();
                //        dupQTY = dvCheckDup[0]["QTY"].ToString();
                //        dupCUSTOMER = dvCheckDup[0]["CUSTOMER"].ToString();
                //        dupSHIMPENTMOD = dvCheckDup[0]["SHIMPENTMOD"].ToString();
                //        dupPALLETTYPE = dvCheckDup[0]["PALLETTYPE"].ToString();
                //        dupDESTINATION = dvCheckDup[0]["DESTINATION"].ToString();

                //    }


                //    MsgBox1.alert("Saving cancelled! Duplicate data has been detected!");
                //    return;
                //}

                maint.UPDATE_WEEKLY_SHIPMENT_SCHEDULE
                    (
                        lblID.Text.Trim(),
                        txtGNSINVOICENUMBER.Text.Trim(),
                        txtPRODUCTIONLOCATION.Text.Trim(),
                        txtSHIPPINGLOCATION.Text.Trim(),
                        txtODNO.Text.Trim(),
                        txtLOTNO.Text.Trim(),
                        txtSHIPMENTNO.Text.Trim(),
                        txtPOMONTH.Text.Trim(),
                        txtPONUMBER.Text.Trim(),
                        txtCONSIGNEEPONUMBER.Text.Trim(),
                        txtPOWK.Text.Trim(),
                        txtITEMCODE.Text.Trim(),
                        txtMODELDESCRIPTION.Text.Trim(),
                        txtMODELNAME.Text.Trim(),
                        txtPLANNEDEXFACTDATE.Text.Trim(),
                        txtPLANNEDOBDATE.Text.Trim(),
                        //txtREVISEDEXFACEDATE.Text.Trim(),
                        //txtREVISEDOBDATE.Text.Trim(),
                        txtQTY.Text.Trim(),
                        txtCUSTOMER.Text.Trim(),
                        txtSHIMPENTMOD.Text.Trim(),
                        txtPALLETTYPE.Text.Trim(),
                        txtDESTINATION.Text.Trim(),
                        txtTRADE.Text.Trim(),
                        txtCONTAINERQTYHC.Text.Trim(),
                        txtCONTAINERQTY40FT.Text.Trim(),
                        txtCONTAINERQTY20FT.Text.Trim(),
                        txtNOOFPALLET.Text.Trim(),
                        txtSTDCONTLOAD.Text.Trim(),
                        txtCONTUSAGE.Text.Trim(),
                        txtCONTUSAGERATIO.Text.Trim(),
                        txtCAS.Text.Trim(),
                        txtCTRNUMBER.Text.Trim(),
                        lblCTRNUMBERARRIVALDATE.Text.Trim(),
                        txtCONFIRMEDDATE.Text.Trim(),
                        txtSHIPPINGLIN.Text.Trim(),
                        txt1STVESSEL.Text.Trim(),
                        txt2NDVESSEL.Text.Trim(),
                        txtVESSELDESTINATION.Text.Trim(),
                        txtPORTOFDISCHARGE.Text.Trim(),
                        txtETADISCHARGEPORT.Text.Trim(),
                        txtCYCUTOFFEXFACTCUTOFF.Text.Trim(),
                        txtLOADINGPORT.Text.Trim(),
                        txtREASONOFDELAYEDEXFACT.Text.Trim(),
                        txtREASONOFDELAYEDOB.Text.Trim(),
                        txtREMARKS.Text.Trim(),
                        strUserID,
                        Session["UPDATETYPE"].ToString()
                    );


                MsgBox1.alert("Successfully saved!");

                GetWeeklyShipmentSchedule();

                maint.ALERT_NEWLY_POSTED_REVISED();
            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message.ToString());
            }


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtReason.Text.Trim() != "")
            {
                int selectedCount = getSelectedCount();
                MsgBox1.confirm("Are you sure you want to Delete all selected item/s (" + selectedCount.ToString() + ")?", "deleteConfirm");
            }
            else
            {
                MsgBox1.alert("Please input reason for cancellation!");
            }
        }



        private void FixAppDomainRestartWhenTouchingFiles()
        {

            System.Reflection.PropertyInfo p = typeof(HttpRuntime).GetProperty("FileChangesMonitor", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

            object o = p.GetValue(null, null);

            System.Reflection.FieldInfo f = o.GetType().GetField("_dirMonSubdirs", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.IgnoreCase);

            object monitor = f.GetValue(o);

            System.Reflection.MethodInfo m = monitor.GetType().GetMethod("StopMonitoring", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            m.Invoke(monitor, new object[0]);

        }

        public void deleteTempFile(string strFilePath)
        {
            string strserverPath = Server.MapPath(strFilePath);
            if (File.Exists(strserverPath))
            {
                string Fname = Server.MapPath(strFilePath);
                FixAppDomainRestartWhenTouchingFiles();
                File.Delete(Fname);
            }
        }



        protected string saveTempFile(string strFileName, FileUpload FU)
        {

            String FilePATH = "~/TEMP/" + strFileName + Path.GetExtension(FU.PostedFile.FileName);

            FU.SaveAs(Server.MapPath(FilePATH));
            FixAppDomainRestartWhenTouchingFiles();
            return FilePATH;
        }



        public DataTable getExcel(string strPath)
        {

            DataTable dtExcel = new DataTable();
            DataRow dr;
            int startRow = 0, startColumn = 0, endRow = 0, endColumn = 0, intSheet = 1, intCreateColumn = 0;
            string strColumnName = "";

            //List<string> excelData = new List<string>();

            byte[] bin = File.ReadAllBytes(strPath);

            //create a new Excel package in a memorystream
            using (MemoryStream stream = new MemoryStream(bin))
            using (ExcelPackage excelPackage = new ExcelPackage(stream))
            {
                //loop all worksheets
                foreach (ExcelWorksheet worksheet in excelPackage.Workbook.Worksheets)
                {

                    if (intSheet == 1)
                    {
                        endRow = worksheet.Dimension.End.Row;
                        endColumn = worksheet.Dimension.End.Column;
                        startRow = worksheet.Dimension.Start.Row;
                        startColumn = worksheet.Dimension.Start.Column;
                        //loop all rows
                        for (int i = startRow; i <= endRow; i++)
                        {
                            dr = dtExcel.NewRow();

                            //loop all columns in a row
                            for (int j = startColumn; j <= endColumn; j++)
                            {
                                strColumnName = "F" + j.ToString();
                                if (intCreateColumn == 0)
                                {
                                    dtExcel.Columns.Add(strColumnName, typeof(String));
                                    if (j == endColumn)
                                    {
                                        intCreateColumn = 1;
                                    }
                                }
                                //add the cell data to the List
                                if (worksheet.Cells[i, j].Value != null)
                                {
                                    dr[strColumnName] = worksheet.Cells[i, j].Value.ToString();

                                    //excelData.Add(worksheet.Cells[i, j].Value.ToString());
                                }
                            }
                            dtExcel.Rows.Add(dr);
                            dtExcel.AcceptChanges();
                        }
                    }

                    intSheet = intSheet + 1;
                }
            }


            return dtExcel;


        }

        public DataTable getExcelXls(string strPath)
        {
            string isHDR = "No";
            string conStr = "";


            conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties='Excel 12.0 Xml;HDR={1}'";


            conStr = String.Format(conStr, @"" + strPath, isHDR);

            OleDbConnection connExcel = new OleDbConnection(conStr);

            OleDbCommand cmdExcel = new OleDbCommand();

            OleDbDataAdapter oda = new OleDbDataAdapter();

            DataTable dt = new DataTable();

            cmdExcel.Connection = connExcel;



            //Get the name of First Sheet
            if (connExcel.State == ConnectionState.Closed) connExcel.Open();


            DataTable dtExcelSchema;

            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

            connExcel.Close();



            //Read Data from First Sheet

            //connExcel.Open();

            if (connExcel.State == ConnectionState.Closed) connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";

            oda.SelectCommand = cmdExcel;

            oda.Fill(dt);
            connExcel.Close();

            return dt;
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
                        if (isValid == true)
                        {
                            string strRole = dvSubsystem.Table.Rows[iRow]["Role"].ToString();

                            if (strRole != "")
                            {
                                strAccessLevel = strRole;
                            }
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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (grdWeeklyShipmentSchedule.Rows.Count < 1)
            {
                MsgBox1.alert("No data to export!");
                return;
            }

            ExportToExcel();


        }



        public void ExportToExcel()
        {

            try
            {
                string filename = "WSS_ " + DateTime.Today.ToString("(MMddyy)");
                //Turn off the view stateV 225 55
                this.EnableViewState = false;
                //Remove the charset from the Content-Type header
                Response.Charset = String.Empty;

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename='" + filename + "'.xls");
                Response.Charset = "";
                this.GetWeeklyShipmentSchedule();
                // If you want the option to open the Excel file without saving then
                // comment out the line below
                // Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.xls";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

                dvExport.RenderControl(htmlWrite);

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
            catch (Exception ex)
            {

                MsgBox1.alert(ex.Message.ToString());
            }
        }

        protected void grdExport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lblColorDisplay;
            Label lblWSSSTATUSFLAG;
            Label lblUPDATEDCOLUMNS;
            Label lblTEXTCOLORDISPLAY;

            string strRowType = e.Row.RowType.ToString();
            if (strRowType != "Header" && strRowType != "Footer")
            {
                lblColorDisplay = (Label)e.Row.FindControl("lblColorDisplay");
                lblWSSSTATUSFLAG = (Label)e.Row.FindControl("lblWSSSTATUSFLAG");

                lblUPDATEDCOLUMNS = (Label)e.Row.FindControl("lblUPDATEDCOLUMNS");
                lblTEXTCOLORDISPLAY = (Label)e.Row.FindControl("lblTEXTCOLORDISPLAY");

                e.Row.BackColor = System.Drawing.Color.FromName(lblColorDisplay.Text);


                string strColName = lblUPDATEDCOLUMNS.Text, strCurrentColumn = "";
                int intStrLength = 0, colEditedCount = 0;
                intStrLength = strColName.Length;
                Label lblColumn;

                for (int x = 0; x < intStrLength; x++)
                {
                    colEditedCount = strColName.Split('/').Length - 1;

                    if (strColName.Substring(x, 1) != "/")
                    {
                        strCurrentColumn = strCurrentColumn + strColName.Substring(x, 1);
                    }
                    else
                    {
                        lblColumn = (Label)e.Row.FindControl("lbl" + strCurrentColumn.Trim());
                        lblColumn.ForeColor = System.Drawing.Color.FromName(lblTEXTCOLORDISPLAY.Text);
                        lblColumn.Font.Bold = true;
                        strCurrentColumn = "";
                    }

                }

            }

        }




        public void chkHeader_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gvr in grdWeeklyShipmentSchedule.Rows)
                {

                    CheckBox chkBox = (CheckBox)gvr.Cells[0].FindControl("chkSelect");
                    if (chkBox.Enabled == true)
                    {
                        if (chkBox.Enabled)
                        {
                            if (((CheckBox)sender).Checked == true)
                            {
                                chkBox.Checked = true;
                            }
                            else
                            {
                                chkBox.Checked = false;
                            }
                        }
                        else
                        {
                            chkBox.Checked = false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                MsgBox1.alert("Error : An unexpected error has occured! " + ex.Message);

            }


        }





        public void gridviewColorOnPostback(GridView grdColor)
        {
            Label lblColorDisplay;
            CheckBox chkSelect;
            ImageButton imgEdit;
            Label lblWSSSTATUSFLAG;
            CheckBox chkCheckAll;

            Label lblUPDATEDCOLUMNS;
            Label lblTEXTCOLORDISPLAY;



            for (int rowIdx = 0; rowIdx < grdColor.Rows.Count; rowIdx++)
            {
                lblUPDATEDCOLUMNS = (Label)grdColor.Rows[rowIdx].FindControl("lblUPDATEDCOLUMNS");


                lblTEXTCOLORDISPLAY = (Label)grdColor.Rows[rowIdx].FindControl("lblTEXTCOLORDISPLAY");

                lblColorDisplay = (Label)grdColor.Rows[rowIdx].FindControl("lblColorDisplay");
                chkSelect = (CheckBox)grdColor.Rows[rowIdx].FindControl("chkSelect");
                imgEdit = (ImageButton)grdColor.Rows[rowIdx].FindControl("imgEdit");
                lblWSSSTATUSFLAG = (Label)grdColor.Rows[rowIdx].FindControl("lblWSSSTATUSFLAG");

                grdColor.Rows[rowIdx].BackColor = System.Drawing.Color.FromName(lblColorDisplay.Text);

                if (checkAuthority("FGWHSE_024") == false)
                {
                    chkSelect.Enabled = false;
                }
                else
                {
                    chkSelect.Enabled = true;
                }


                if (checkAuthority("FGWHSE_024") == true || checkAuthority("FGWHSE_025") == true || checkAuthority("FGWHSE_026"))
                {
                    imgEdit.Enabled = true;
                }
                else
                {
                    imgEdit.Enabled = false;
                }

                //if (lblWSSSTATUSFLAG.Text.Trim() == "1" || lblWSSSTATUSFLAG.Text.Trim() == "3")
                if (lblWSSSTATUSFLAG.Text.Trim() == "3")
                {
                    chkSelect.Enabled = false;
                    imgEdit.Enabled = false;
                }


                string strColName = lblUPDATEDCOLUMNS.Text, strCurrentColumn = "";
                int intStrLength = 0, colEditedCount = 0;
                intStrLength = strColName.Length;
                Label lblColumn;

                for (int x = 0; x < intStrLength; x++)
                {
                    colEditedCount = strColName.Split('/').Length - 1;

                    if (strColName.Substring(x, 1) != "/")
                    {
                        strCurrentColumn = strCurrentColumn + strColName.Substring(x, 1);
                    }
                    else
                    {
                        lblColumn = (Label)grdColor.Rows[rowIdx].FindControl("lbl" + strCurrentColumn.Trim());
                        lblColumn.ForeColor = System.Drawing.Color.FromName(lblTEXTCOLORDISPLAY.Text);
                        lblColumn.Font.Bold = true;
                        strCurrentColumn = "";
                    }

                }

            }
            //}
        }


        protected void imgBack_Click(object sender, EventArgs e)
        {
            GetWeeklyShipmentSchedule();
        }

    }

}
