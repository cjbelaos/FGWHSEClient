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
using com.eppi.utils;

namespace FGWHSEClient.Form
{
    public partial class MasterPalletMonitoring : System.Web.UI.MasterPage
    {
        protected string strName = "";
        public string strLoginType = System.Configuration.ConfigurationManager.AppSettings["webFor"];
        public string strAccessLevel = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    //Response.Write("<script>");
                    //Response.Write("alert('Session expired! Please log in again.');");
                    //Response.Write("window.location = '../Login.aspx';");
                    //Response.Write("</script>");
                }
                else
                {
                    strName = Session["UserName"].ToString();
                    lbl_systemName.Text = ConfigurationManager.AppSettings["systemName"].ToString();
                }

                    lblFooter.Text = ConfigurationManager.AppSettings["remarks"].ToString();

                    //check if compatibility view
                    if (IECompatibility.GetIEBrowserMode(Page.Request) != "Compatibility View")
                    {
                        //force compatibility to IE7
                        HtmlMeta force = new HtmlMeta();
                        force.HttpEquiv = "X-UA-Compatible";
                        force.Content = "IE=EmulateIE7";
                        Page.Header.Controls.AddAt(0, force);
                    }



                    checkAccess();


                txtclientIp.Text = (Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();
                //if(clientIp == "::1" || clientIp == "127.0.0.1")
       
           



            }
            catch (Exception ex)
            {
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);
            }
        }


        public void checkAccess()
        {
            liSystemTransaction.Attributes.Add("style", "display : none");
            liAllocation.Attributes.Add("style", "display : none");
            liMasterMaintenance.Attributes.Add("style", "display : none");
            liMaster.Attributes.Add("style", "display : none");

            if (strLoginType == "EPPI")
            {
                if (!checkAuthority("FGWHSE_004"))
                {
                    liContainerAllocation.Attributes.Add("style", "display : none");
                }
                else
                {
                    liAllocation.Attributes.Add("style", "display : compact");
                }

                if (!checkAuthority("FGWHSE_005"))
                {
                    liPalletAllocation.Attributes.Add("style", "display : none");
                }
                else
                {
                    liAllocation.Attributes.Add("style", "display : compact");
                }

                if (!checkAuthority("FGWHSE_008"))
                {
                    liODAllocation.Attributes.Add("style", "display : none");
                }
                else
                {
                    liAllocation.Attributes.Add("style", "display : compact");
                }


                if (!checkAuthority("FGWHSE_009"))
                {
                    liPalletLoading.Attributes.Add("style", "display : none");
                }
                else
                {
                    liAllocation.Attributes.Add("style", "display : compact");
                }


                if (!checkAuthority("FGWHSE_010"))
                {
                    liExitFactory.Attributes.Add("style", "display : none");
                }
                else
                {
                    liAllocation.Attributes.Add("style", "display : compact");
                }


                if (!checkAuthority("FGWHSE_011"))
                {
                    liCartonInformation.Attributes.Add("style", "display : none");
                }
                else
                {
                    liAllocation.Attributes.Add("style", "display : compact");
                }



                if (!checkAuthority("FGWHSE_007"))
                {
                    liLocationMaster.Attributes.Add("style", "display : none");
                }
                else
                {
                    liMasterMaintenance.Attributes.Add("style", "display : compact");
                }


                if (!checkAuthority("FGWHSE_012"))
                {
                    liItemLimitControl.Attributes.Add("style", "display : none");
                }
                else
                {
                    liMasterMaintenance.Attributes.Add("style", "display : compact");
                }



                if (!checkAuthority("FGWHSE_018"))
                {
                    liPartsLocationCheckMaster.Attributes.Add("style", "display : none");
                }
                else
                {
                    liMaster.Attributes.Add("style", "display : compact");
                }

                //if (!checkAuthority("FGWHSE_013"))
                //{
                //    liLotDataScanning.Attributes.Add("style", "display : none");
                //}
                //else
                //{
                //    liSystemTransaction.Attributes.Add("style", "display : compact");
                //}
               

               

                //if (!checkAuthority("FGWHSE_016"))
                //{
                //    liDNChecking.Attributes.Add("style", "display : none");
                //}
                //else
                //{
                    liSystemTransaction.Attributes.Add("style", "display : compact");
                //}
                
            }
            else if (strLoginType == "OUTSIDE")
            {
                liFGwh.Attributes.Add("style", "display : none");
                liDeliveryInquiry.Attributes.Add("style", "display : none");
                liRFIDScanningVerification.Attributes.Add("style", "display : none");
                liReceivedLots.Attributes.Add("style", "display : none");
                liSystemTransaction.Attributes.Add("style", "display : none");
                liMaster.Attributes.Add("style", "display : none");
                liPartsInspectionLocation.Attributes.Add("style", "display : none");
                liIncomingInspection.Attributes.Add("style", "display : none");
                liPartsDeliveryInspection.Attributes.Add("style", "display : none");
                if (!checkAuthorityOUTSIDE("FGWHSE_004"))
                {
                    liContainerAllocation.Attributes.Add("style", "display : none");
                }
                else
                {
                    liAllocation.Attributes.Add("style", "display : compact");
                }

                if (!checkAuthorityOUTSIDE("FGWHSE_005"))
                {
                    liPalletAllocation.Attributes.Add("style", "display : none");
                }
                else
                {
                    liAllocation.Attributes.Add("style", "display : compact");
                }

                if (!checkAuthorityOUTSIDE("FGWHSE_008"))
                {
                    liODAllocation.Attributes.Add("style", "display : none");
                }
                else
                {
                    liAllocation.Attributes.Add("style", "display : compact");
                }


                if (!checkAuthorityOUTSIDE("FGWHSE_009"))
                {
                    liPalletLoading.Attributes.Add("style", "display : none");
                }
                else
                {
                    liAllocation.Attributes.Add("style", "display : compact");
                }


                if (!checkAuthorityOUTSIDE("FGWHSE_010"))
                {
                    liExitFactory.Attributes.Add("style", "display : none");
                }
                else
                {
                    liAllocation.Attributes.Add("style", "display : compact");
                }


                if (!checkAuthorityOUTSIDE("FGWHSE_011"))
                {
                    liCartonInformation.Attributes.Add("style", "display : none");
                }
                else
                {
                    liAllocation.Attributes.Add("style", "display : compact");
                }



                if (!checkAuthorityOUTSIDE("FGWHSE_007"))
                {
                    liLocationMaster.Attributes.Add("style", "display : none");
                }
                else
                {
                    liMasterMaintenance.Attributes.Add("style", "display : compact");
                }


                if (!checkAuthorityOUTSIDE("FGWHSE_012"))
                {
                    liItemLimitControl.Attributes.Add("style", "display : none");
                }
                else
                {
                    liMasterMaintenance.Attributes.Add("style", "display : compact");
                }



                //if (!checkAuthorityOUTSIDE("FGWHSE_013"))
                //{
                //    liLotDataScanning.Attributes.Add("style", "display : none");
                //}
                //else
                //{
                //    liSystemTransaction.Attributes.Add("style", "display : compact");
                //}




                //if (!checkAuthorityOUTSIDE("FGWHSE_016"))
                //{
                //    liDNChecking.Attributes.Add("style", "display : none");
                //}
                //else
                //{
                //    liSystemTransaction.Attributes.Add("style", "display : compact");
                //}
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

    }
}
