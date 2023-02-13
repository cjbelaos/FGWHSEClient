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

namespace FGWHSEClient.Form
{
    public partial class HTMenu : System.Web.UI.Page
    {
        public string strLoginType = System.Configuration.ConfigurationManager.AppSettings["webFor"];
        public string strAccessLevel = "";

        void Page_PreInit(Object sender, EventArgs e)
        {
            //if (Session["DeviceType"] != null)
            if (Request.QueryString["DeviceType"] != null)
            {
                if (Request.QueryString["DeviceType"].ToString() == "Denso")
                {
                    this.MasterPageFile = "~/Form/DENSOMasterPalletMonitoring.Master";
                    
                }
                else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                {
                    this.MasterPageFile = "~/Form/HTMasterPalletMonitoring.Master";

                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {

                if (Request.QueryString["DeviceType"] != null)
                {
                    if (Request.QueryString["DeviceType"].ToString() == "Denso")
                    {
                        Response.Write("<script>");
                        Response.Write("alert('Session expired! Please log in again.');");
                        Response.Write("window.location = '../DENSOLogin.aspx';");
                        Response.Write("</script>");
                    }
                    else if (Request.QueryString["DeviceType"].ToString() == "HT")
                    {
                        Response.Write("<script>");
                        Response.Write("alert('Session expired! Please log in again.');");
                        Response.Write("window.location = '../HTLogin.aspx';");
                        Response.Write("</script>");
                    }
                }
                else 
                {
                    Response.Write("<script>");
                    Response.Write("alert('Session expired! Please log in again.');");
                    Response.Write("window.location = '../HTLogin.aspx';");
                    Response.Write("</script>");
                }
             
            }
            
            if (!this.IsPostBack)
            {

                if (Request.QueryString["DeviceType"] != null)
                {
                    if (Request.QueryString["DeviceType"].ToString() == "Denso")
                    {
                        //this.MasterPageFile = "~/Form/DENSOMasterPalletMonitoring.Master";
                        btnLotDataScanning.Attributes.Add("style", "width:250px;font-size:small;height:30px;");
                   //     btnLotDataScanningBypass.Attributes.Add("style", "width:250px;font-size:small;height:30px;");
                        btnDNReceiving.Attributes.Add("style", "width:250px;font-size:small;height:30px;");
                        btnPartsInpectionLocationCheck.Attributes.Add("style", "width:250px;font-size:small;height:30px;");

                        btnContainerAllocation.Attributes.Add("style", "width:250px;font-size:small;height:30px;");
                        btnPalletAllocation.Attributes.Add("style", "width:250px;font-size:small;height:30px;");
                        btnODAllocation.Attributes.Add("style", "width:250px;font-size:small;height:30px;");
                        btnPalletLoading.Attributes.Add("style", "width:250px;font-size:small;height:30px;");
                        btnExitFactory.Attributes.Add("style", "width:250px;font-size:small;height:30px;");
                        btnCartonInformation.Attributes.Add("style", "width:250px;font-size:small;height:30px;");
                        trRFIDReceiving.Attributes.Add("style", "width:250px;font-size:small;height:30px;");
                    }
                }


                tblPair.Attributes.Add("style", "display : none");
                trLotRFID.Attributes.Add("style", "display : none");
                trMultiLotRFID.Attributes.Add("style", "display : none");
                trDeleteLotRFID.Attributes.Add("style", "display : none");
                trPartWhse.Attributes.Add("style", "display : none");
                trFGWHSE.Attributes.Add("style", "display : none");
                trPPLotRFID.Attributes.Add("style", "display : none");
                trMultiLotRFID.Attributes.Add("style", "display : none");
                trPairingHead.Attributes.Add("style", "display : none");

                if (strLoginType == "EPPI")
                {

                    if (checkAuthority("FGWHSE_045") == true)
                    {
                        trLotRFID.Attributes.Add("style", "display : compact");
                        trMultiLotRFID.Attributes.Add("style", "display : compact");
                        
                        trPairingHead.Attributes.Add("style", "display : compact");
                        tblPair.Attributes.Add("style", "display : compact");
                        
                    }

                    if (checkAuthority("FGWHSE_046") == true)
                    {
                        trPPLotRFID.Attributes.Add("style", "display : compact");
                        trPairingHead.Attributes.Add("style", "display : compact");
                        tblPair.Attributes.Add("style", "display : compact");
                    }

                    if (checkAuthority("FGWHSE_047") == true)
                    {
                        trDeleteLotRFID.Attributes.Add("style", "display : compact");
                        trPairingHead.Attributes.Add("style", "display : compact");
                        tblPair.Attributes.Add("style", "display : compact");
                    }


                    string strIsFG = "";

                    if (Request.QueryString["DeviceType"] != null)
                    {
                        strIsFG = Request.QueryString["DeviceType"].ToString();
                    }
                    btnLotDataScanning.Text = "PARTS RECEIVING MANUAL";
                //    btnLotDataScanningBypass.Text = "PARTS RECEIVING MANUAL (DN BYPASS)";
                    //trFGWHSE.Attributes.Add("style", "display : compact"); 
                    trPartWhse.Attributes.Add("style", "display : none");
                    trPalletShipmentLoading.Attributes.Add("style", "display : none");


                    if (!checkAuthority("FGWHSE_022"))
                    {
                        trPalletShipmentLoading.Attributes.Add("style", "display : none");
                    }
                    else
                    {
                        trPalletShipmentLoading.Attributes.Add("style", "display : compact");
                    }

                    if (!checkAuthority("FGWHSE_023"))
                    {
                        trPalletPicking.Attributes.Add("style", "display : none");
                    }
                    else
                    {
                        if (strIsFG == "HTFG")
                        {
                            trPalletPicking.Attributes.Add("style", "display : compact");
                        }
                        else
                        {
                            trPalletPicking.Attributes.Add("style", "display : none");
                        }
                    }


                    if (!checkAuthority("FGWHSE_004"))
                    {
                        trContainerAllocation.Attributes.Add("style", "display : none");
                    }
                    else
                    {
                        trFGWHSE.Attributes.Add("style", "display : compact");
                    }

                    if (!checkAuthority("FGWHSE_005"))
                    {
                        trPalletAllocation.Attributes.Add("style", "display : none");
                    }
                    else
                    {
                        trFGWHSE.Attributes.Add("style", "display : compact");
                    }

                    if (!checkAuthority("FGWHSE_008"))
                    {
                        trODAllocation.Attributes.Add("style", "display : none");
                    }
                    else
                    {
                        trFGWHSE.Attributes.Add("style", "display : compact");
                    }


                    if (!checkAuthority("FGWHSE_009"))
                    {
                        trPalletLoading.Attributes.Add("style", "display : none");
                    }
                    else
                    {
                        trFGWHSE.Attributes.Add("style", "display : compact");
                    }


                    if (!checkAuthority("FGWHSE_010"))
                    {
                        trExitFactory.Attributes.Add("style", "display : none");
                    }
                    else
                    {
                        trFGWHSE.Attributes.Add("style", "display : compact");
                    }


                    if (!checkAuthority("FGWHSE_011"))
                    {
                        trCartonInformation.Attributes.Add("style", "display : none");
                    }
                    else
                    {
                        trFGWHSE.Attributes.Add("style", "display : compact");
                    }



                    if (checkAuthority("FGWHSE_013") == true)
                    {
                        trLotDataScanning.Attributes.Add("style", "display : compact");
                        trPartWhse.Attributes.Add("style", "display : compact");
                    }

                    if (!checkAuthority("FGWHSE_014"))
                    {
                //        trLotDataScanningBypass.Attributes.Add("style", "display : none");
                    }
                    else
                    {
                        trPartWhse.Attributes.Add("style", "display : compact");
                    }

                    if (!checkAuthority("FGWHSE_020"))
                    {
                        trRFIDReceiving.Attributes.Add("style", "display : none");
                        trRFIDDNDelete.Attributes.Add("style", "display : none");
                    }
                    else
                    {
                        trRFIDReceiving.Attributes.Add("style", "display : compact");
                        trPartWhse.Attributes.Add("style", "display : compact");
                        trRFIDDNDelete.Attributes.Add("style", "display : compact");
                    }
                    if (!checkAuthority("FGWHSE_053"))
                    {
                        trReceivePD.Attributes.Add("style", "display : none");
                    }
                    else
                    {
                        trReceivePD.Attributes.Add("style", "display : compact");
                    }
                    if (!checkAuthority("FGWHSE_054"))
                    {
                        trDelivery.Attributes.Add("style", "display : none");
                    }
                    else
                    {
                        trDelivery.Attributes.Add("style", "display : compact");
                    }

                    //if (!checkAuthority("FGWHSE_016"))
                    //{
                    //    trDNReceiving.Attributes.Add("style", "display : none");
                    //}
                    //else
                    //{
                        //trPartWhse.Attributes.Add("style", "display : compact");
                    //}

                    if (!checkAuthority("FGWHSE_028"))
                    {
                        trVanPoolAllocation.Attributes.Add("style", "display : none");
                    }
                    else
                    {
                        trVanPoolAllocation.Attributes.Add("style", "display : compact");
                    }


                    if (checkAuthority("FGWHSE_036") == true)
                    {
                        trDNReceiving.Attributes.Add("style", "display : compact");
                        trPartWhse.Attributes.Add("style", "display : compact");
                        trPartsInspectionLocationCheck.Attributes.Add("style", "display : compact");

                    }


                    if (!checkAuthority("FGWHSE_055"))
                    {
                        trPPDPLC.Attributes.Add("style", "display : none");
                        trPPDReturn.Attributes.Add("style", "display : none");
                        trPPDDelivery.Attributes.Add("style", "display : none");
                        tblPPD.Attributes.Add("style", "display : none");
                    }
                    else
                    {
                        trPPDPLC.Attributes.Add("style", "display : compact");
                        trPPDReturn.Attributes.Add("style", "display : compact");
                        trPPDDelivery.Attributes.Add("style", "display : compact");
                        tblPPD.Attributes.Add("style", "display : compact");

                        string user = Session["UserName"].ToString();

                        if ( user.ToUpper()  != "ADMINISTRATOR")
                        {
                            tbPartWhse.Attributes.Add("style", "display : none");
                            tbFGWHSE.Attributes.Add("style", "display : none");

                        }

                       
                    }



                }
                else  if (strLoginType == "OUTSIDE")
                {
                    btnLotDataScanning.Text = "PARTS LOADING";
                   //btnLotDataScanningBypass.Text = "PARTS LOADING (DN BYPASS)";
                    trPartWhse.Attributes.Add("style", "display : compact");
                    trFGWHSE.Attributes.Add("style", "display : none");
                    trPalletShipmentLoading.Attributes.Add("style", "display : none");

                    if (!checkAuthorityOUTSIDE("FGWHSE_004"))
                    {
                        trContainerAllocation.Attributes.Add("style", "display : none");
                    }
                    else
                    {
                        trFGWHSE.Attributes.Add("style", "display : compact");
                    }

                    if (!checkAuthorityOUTSIDE("FGWHSE_005"))
                    {
                        trPalletAllocation.Attributes.Add("style", "display : none");
                    }
                    else
                    {
                        trFGWHSE.Attributes.Add("style", "display : compact");
                    }

                    if (!checkAuthorityOUTSIDE("FGWHSE_008"))
                    {
                        trODAllocation.Attributes.Add("style", "display : none");
                    }
                    else
                    {
                        trFGWHSE.Attributes.Add("style", "display : compact");
                    }


                    if (!checkAuthorityOUTSIDE("FGWHSE_009"))
                    {
                        trPalletLoading.Attributes.Add("style", "display : none");
                    }
                    else
                    {
                        trFGWHSE.Attributes.Add("style", "display : compact");
                    }


                    if (!checkAuthorityOUTSIDE("FGWHSE_010"))
                    {
                        trExitFactory.Attributes.Add("style", "display : none");
                    }
                    else
                    {
                        trFGWHSE.Attributes.Add("style", "display : compact");
                    }

                    if (!checkAuthorityOUTSIDE("FGWHSE_011"))
                    {
                        trCartonInformation.Attributes.Add("style", "display : none");
                    }
                    else
                    {
                        trFGWHSE.Attributes.Add("style", "display : compact");
                    }



                    if (!checkAuthorityOUTSIDE("FGWHSE_013"))
                    {
                        trLotDataScanning.Attributes.Add("style", "display : none");
                    }

                    if (!checkAuthorityOUTSIDE("FGWHSE_014"))
                    {
                //        trLotDataScanningBypass.Attributes.Add("style", "display : none");
                    }

                    if (!checkAuthorityOUTSIDE("FGWHSE_020"))
                    {
                        trRFIDReceiving.Attributes.Add("style", "display : none");
                    }
                    if (!checkAuthorityOUTSIDE("FGWHSE_053"))
                    {
                        trReceivePD.Attributes.Add("style", "display : none");
                    }
                    {
                        trReceivePD.Attributes.Add("style", "display : compact");
                    }
                    if (!checkAuthorityOUTSIDE("FGWHSE_054"))
                    {
                        trDelivery.Attributes.Add("style", "display : none");
                    }
                    else
                    {
                        trDelivery.Attributes.Add("style", "display : compact");
                    }

                    //if (!checkAuthorityOUTSIDE("FGWHSE_016"))
                    //{
                        trDNReceiving.Attributes.Add("style", "display : none");
                    //}

                        trPartsInspectionLocationCheck.Attributes.Add("style", "display : none");

                }

            }
        }

        protected void btnContainerAllocation_Click(object sender, EventArgs e)
        {
            Response.Redirect("HTContainerAllocation.aspx");
        }

        protected void btnPalletAllocation_Click(object sender, EventArgs e)
        {
            Response.Redirect("HTPalletAllocation.aspx");
        }

        protected void btnODAllocation_Click(object sender, EventArgs e)
        {
            Response.Redirect("HTODAllocation.aspx");
        }

        protected void btnPalletLoading_Click(object sender, EventArgs e)
        {
            Response.Redirect("HTPalletLoading.aspx");
        }

        protected void btnExitFactory_Click(object sender, EventArgs e)
        {
            Response.Redirect("HTExitFactory.aspx");
        }

        protected void btnCartonInformation_Click(object sender, EventArgs e)
        {
            Response.Redirect("HTCartonInformation.aspx");
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




        private bool checkAuthorityOUTSIDE(string strPageSubsystem)
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
                        dvSubsystem.Sort = "subsystem_id";

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

        protected void btnLotDataScanning_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["DeviceType"] != null)
            {
                if (Request.QueryString["DeviceType"].ToString() == "Denso")
                {
                    Response.Redirect("LotDataScanning.aspx?DeviceType=Denso");
                }
                else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                {
                    Response.Redirect("LotDataScanning.aspx?DeviceType=HT");
                }
                else
                {
                    Response.Redirect("LotDataScanning.aspx");
                }
            }
            //ADDED 81718 BY TIN
            else
            {
                Response.Redirect("LotDataScanning.aspx");
            }
            
        }

        protected void btnLotDataScanningBypass_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["DeviceType"] != null)
            {
                if (Request.QueryString["DeviceType"].ToString() == "Denso")
                {
                    Response.Redirect("LotDataScanningByPass.aspx?DeviceType=Denso");
                }
                else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                {
                    Response.Redirect("LotDataScanningByPass.aspx?DeviceType=HT");
                }
                else
                {
                    Response.Redirect("LotDataScanningByPass.aspx");
                }
            }
            //ADDED 81718 BY TIN
            else
            {
                Response.Redirect("LotDataScanningByPass.aspx");
            }
        }

        protected void btnDNReceiving_Click(object sender, EventArgs e)
        {

            if (Request.QueryString["DeviceType"] != null)
            {
                if (Request.QueryString["DeviceType"].ToString() == "Denso")
                {
                    Response.Redirect("PartsLocationCheck.aspx?DeviceType=Denso");
                }
                else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                {
                    Response.Redirect("PartsLocationCheck.aspx?DeviceType=HT");
                }
                else
                {
                    Response.Redirect("PartsLocationCheck.aspx");
                }
            }
            //ADDED 81718 BY TIN
            else
            {
                Response.Redirect("PartsLocationCheck.aspx");
            }

        }

        protected void btnRFIDReceiving_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["DeviceType"] != null)
            {
                if (Request.QueryString["DeviceType"].ToString() == "Denso")
                {
                    Response.Redirect("RFIDReceiving.aspx?DeviceType=Denso");
                }
                else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                {
                    Response.Redirect("RFIDReceiving.aspx?DeviceType=HT");
                }
                else
                {
                    Response.Redirect("RFIDReceiving.aspx");
                }
            }
            //ADDED 81718 BY TIN
            else
            {
                Response.Redirect("RFIDReceiving.aspx");
            }
        }

        protected void btnPartsInpectionLocationCheck_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["DeviceType"] != null)
            {
                if (Request.QueryString["DeviceType"].ToString() == "Denso")
                {
                    Response.Redirect("PartsLocationInspectionCheck.aspx?DeviceType=Denso");
                }
                else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                {
                    Response.Redirect("PartsLocationInspectionCheck.aspx?DeviceType=HT");
                }
                else
                {
                    Response.Redirect("PartsLocationInspectionCheck.aspx");
                }
            }
            //ADDED 13AUG2019 BY TIN
            else
            {
                Response.Redirect("PartsLocationInspectionCheck.aspx");
            }
        }

        protected void btnRFIDDNDelete_Click(object sender, EventArgs e)
        {
            //17FEB2021
            if (Request.QueryString["DeviceType"] != null)
            {
                if (Request.QueryString["DeviceType"].ToString() == "Denso")
                {
                    Response.Redirect("RFIDDNDelete.aspx?DeviceType=Denso");
                }
                else if (Request.QueryString["DeviceType"].ToString() == "HT")
                {
                    Response.Redirect("RFIDDNDelete.aspx?DeviceType=HT");
                }
                else
                {
                    Response.Redirect("RFIDDNDelete.aspx");
                }
            }
            else
            {
                Response.Redirect("RFIDDNDelete.aspx");
            }
        }

        protected void btnReceive_Click(object sender, EventArgs e)
        {
            Response.Redirect("HTaspReceiving_NEW.aspx");
        }

        protected void btnDelivery_Click(object sender, EventArgs e)
        {
            Response.Redirect("HTaspDelivery_New2.aspx"); 
        }

        protected void btnPalletPicking_Click(object sender, EventArgs e)
        {
            Response.Redirect("HTPickingHome.aspx?DeviceType=HTFG");
        }

        protected void btnPalletShipmentLoading_Click(object sender, EventArgs e)
        {
            Response.Redirect("HTPalletShipmentValidation.aspx?DeviceType=HTFG");
        }

        protected void btnVanPoolAllocation_Click(object sender, EventArgs e)
        {
            Response.Redirect("HTVanPoolAllocation.aspx?DeviceType=HTFG");
        }

        protected void btnLotRfidPairing_Click(object sender, EventArgs e)
        {
            Response.Redirect("HTLotRfidPairing.aspx?DeviceType=HT");
        }

        protected void btnPairedLotRfidDeletion_Click(object sender, EventArgs e)
        {
            Response.Redirect("HT_PAIRING_DELETE.aspx?DeviceType=HT");
        }


        protected void btnPayout_Click(object sender, EventArgs e)
        {
            Response.Redirect("HTPayout.aspx?DeviceType=HT");
        }

        protected void btnPartsReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("PartsReturnHolmes.aspx?DeviceType=HT");
        }

        protected void btnPPDPLC_Click(object sender, EventArgs e)
        {
            Response.Redirect("PartsLocationCheck_PPD.aspx?DeviceType=HT");
        }

        protected void btnMultiLotRFID_Click(object sender, EventArgs e)
        {
            Response.Redirect("HT_PAIRING_MULTILOTPARTCODE.aspx?DeviceType=HT");
        }

        protected void btnPPLotRfid_Click(object sender, EventArgs e)
        {
            Response.Redirect("HT_PAIRING_UPDATE.aspx?DeviceType=HT");
        }



        protected void btnDNReceiving2_Click(object sender, EventArgs e)
        {
            Response.Redirect("PLCMenu.aspx?DeviceType=HT");
        }


    }
}
