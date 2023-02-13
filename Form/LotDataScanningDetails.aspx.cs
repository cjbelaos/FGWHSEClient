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
using FGWHSEClient.DAL;
using com.eppi.utils;
using System.Text.RegularExpressions;
using System.Text; 

namespace FGWHSEClient.Form
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        LotDataScanningDAL LotDataScanningDAL = new LotDataScanningDAL();
        string substring;
        string substring2;
        string supplierid;
        int iTotalScanned = 0;

        void Page_PreInit(Object sender, EventArgs e)
        {
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

            string strLoginType2 = System.Configuration.ConfigurationManager.AppSettings["webFor"];
            if (strLoginType2 == "EPPI")
            {
                Page.Title = string.Format("MANUAL PARTS RECEIVING - DETAILS");
            }
            else
            {
                Page.Title = string.Format("PARTS LOADING - DETAILS");
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string strLoginType2 = System.Configuration.ConfigurationManager.AppSettings["webFor"];
                if (strLoginType2 == "EPPI")
                {
                    lblHeader.Text = "MANUAL PARTS RECEIVING - DETAILS";
                }
                else
                {
                    lblHeader.Text = "PARTS LOADING - DETAILS";
                }

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
                        else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
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
                            dvLotDataScan.Attributes.Add("style", "font-size:x-small;width:270px");
                            lblDN.Attributes.Add("style", "font-size:12px;");
                            lblRFID.Attributes.Add("style", "font-size:12px;");
                            lblLotQR.Attributes.Add("style", "font-size:11px;");
                            tdQR.Attributes.Add("style", "font-size:x-small;width:85px;text-align:right;");
                            lblPCode.Attributes.Add("style", "font-size:12px;");
                            lblLotNo.Attributes.Add("style", "font-size:12px;");
                            lblRefNo.Attributes.Add("style", "font-size:12px;");
                            lblQty.Attributes.Add("style", "font-size:12px;");
                            lblRemarks.Attributes.Add("style", "font-size:12px;");
                            lblMessage2.Attributes.Add("style", "font-size:12px;;color:Black");
                            txtDNNo.Attributes.Add("style", "font-size:18px;width:165px;height:18px;");
                            txtRFID.Attributes.Add("style", "font-size:18px;width:165px;height:18px;");
                            txtLotQR.Attributes.Add("style", "font-size:18px;width:165px;height:18px;");
                            txtPartCode.Attributes.Add("style", "font-size:18px;width:165px;height:18px;");
                            txtLotNo.Attributes.Add("style", "font-size:18px;width:165px;height:18px;");
                            txtRefNo.Attributes.Add("style", "font-size:18px;width:165px;height:18px;");
                            txtRemarks.Attributes.Add("style", "font-size:18px;width:165px;height:18px;");
                            txtQtyTotal.Attributes.Add("style", "font-size:18px;width:48px;height:18px;");
                            txtQty1.Attributes.Add("style", "font-size:18px;width:48px;height:18px;");
                            txtQty2.Attributes.Add("style", "font-size:18px;width:48px;height:18px;");
                            tdMessage.Attributes.Add("style", "border: 1px solid #385d8a; background-color:#d9d9d9;padding:5px;font-size:x-small;height:20px");
                            lblMessage.Attributes.Add("style", "font-size:11px;width:180px");
                            btnDelete.Attributes.Add("style", "font-size:14px;width:75px;height:25px");
                            btnBack.Attributes.Add("style", "font-size:14px;width:75px;height:25px");
                            btnClear.Attributes.Add("style", "font-size:14px;width:75px;height:25px");
                            tablestyle.Attributes.Add("style", "cellpadding:0;cellspacing:0");
                        }
                    }





                    if (Request.QueryString["DNNo"] != null)
                    {
                        if (!(isValidText(@"[A-Za-z0-9]", Request.QueryString["DNNo"].ToString())))
                        {
                            if (Request.QueryString["DeviceType"] != null)
                            {
                                if (Request.QueryString["DeviceType"].ToString() == "Denso")
                                {
                                    Response.Write("<script>");
                                    Response.Write("alert('INVALID DN NOS.!');");
                                    Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&DeviceType=Denso';");
                                    Response.Write("</script>");
                                //    Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString() + "&DeviceType=Denso");
                                }
                                else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                                {
                                    Response.Write("<script>");
                                    Response.Write("alert('INVALID DN NOS.!');");
                                    Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&DeviceType=HT';");
                                    Response.Write("</script>");
                                    //    Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString() + "&DeviceType=Denso");
                                }
                            }
                            else
                            {
                                Response.Write("<script>");
                                Response.Write("alert('INVALID DN NO.!');");
                                Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "';");
                                Response.Write("</script>");

                             //   Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString());
                            } 

                        }
                        else
                        {
                            txtDNNo.Text = Request.QueryString["DNNo"].ToString();
                            txtRFID.Focus();
                        }
                    }

                    //else if (Request.QueryString["DNNo"] != null && Request.QueryString["RFIDTag"] != null)
                    if (Request.QueryString["DNNo"] != null && Request.QueryString["RFIDTag"] != null)
                    {
                        if (!(isValidText(@"[A-Za-z0-9]", Request.QueryString["DNNo"].ToString())))
                        {
                            if (Request.QueryString["DeviceType"] != null)
                            {
                                if (Request.QueryString["DeviceType"].ToString() == "Denso")
                                {
                                    Response.Write("<script>");
                                    Response.Write("alert('INVALID DN NO.!');");
                                    Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "&DeviceType=Denso';");
                                    Response.Write("</script>");
                                    //    Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString() + "&DeviceType=Denso");
                                }
                                else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                                {
                                    Response.Write("<script>");
                                    Response.Write("alert('INVALID DN NO.!');");
                                    Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "&DeviceType=HT';");
                                    Response.Write("</script>");
                                    //    Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString() + "&DeviceType=Denso");
                                }
                            }
                            else
                            {
                                Response.Write("<script>");
                                Response.Write("alert('INVALID DN NO.!');");
                                Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString())+ "';");
                                Response.Write("</script>");

                                //   Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString());
                            } 
                        }
                        else if (!(isValidText(@"[A-Za-z0-9]", Request.QueryString["RFIDTag"].ToString())))
                        {
                            if (Request.QueryString["DeviceType"] != null)
                            {
                                if (Request.QueryString["DeviceType"].ToString() == "Denso")
                                {
                                    Response.Write("<script>");
                                    Response.Write("alert('INVALID RFID TAG.!');");
                                    Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "&DeviceType=Denso';");
                                    Response.Write("</script>");
                                    //    Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString() + "&DeviceType=Denso");
                                }
                                else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                                {
                                    Response.Write("<script>");
                                    Response.Write("alert('INVALID RFID TAG.!');");
                                    Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "&DeviceType=HT';");
                                    Response.Write("</script>");
                                    //    Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString() + "&DeviceType=Denso");
                                }
                            }
                            else
                            {
                                Response.Write("<script>");
                                Response.Write("alert('INVALID RFID TAG.!');");
                                Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "';");
                                Response.Write("</script>");

                                //   Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString());
                            } 

                        }
                        else
                        {
                            txtDNNo.Text = Request.QueryString["DNNo"].ToString();
                            txtRFID.Text = Request.QueryString["RFIDTag"].ToString();
                            txtRFID_TextChanged(this, EventArgs.Empty);
                        }
                    }

                    //else if (Request.QueryString["DNNo"] != null && Request.QueryString["RFIDTag"] != null && Request.QueryString["QRCode"] != null)
                    if (Request.QueryString["DNNo"] != null && Request.QueryString["RFIDTag"] != null && Request.QueryString["QRCode"] != null)
                    {
                        if (!(isValidText(@"[A-Za-z0-9]", Request.QueryString["DNNo"].ToString())))
                        {
                            if (Request.QueryString["DeviceType"] != null)
                            {
                                if (Request.QueryString["DeviceType"].ToString() == "Denso")
                                {
                                    Response.Write("<script>");
                                    Response.Write("alert('INVALID DN NO.!');");
                                    Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "&QRCode=" + HttpUtility.UrlEncode(Request.QueryString["QRCode"].ToString()) + "&DeviceType=Denso';");
                                    Response.Write("</script>");
                                    //    Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString() + "&DeviceType=Denso");
                                }
                                else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                                {
                                    Response.Write("<script>");
                                    Response.Write("alert('INVALID DN NO.!');");
                                    Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "&QRCode=" + HttpUtility.UrlEncode(Request.QueryString["QRCode"].ToString()) + "&DeviceType=HT';");
                                    Response.Write("</script>");
                                    //    Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString() + "&DeviceType=Denso");
                                }
                            }
                            else
                            {
                                Response.Write("<script>");
                                Response.Write("alert('INVALID DN NO.!');");
                                Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "&QRCode=" + HttpUtility.UrlEncode(Request.QueryString["QRCode"].ToString()) + "';");
                                Response.Write("</script>");

                                //   Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString());
                            } 
                        }
                        else if (!(isValidText(@"[A-Za-z0-9]", Request.QueryString["RFIDTag"].ToString())))
                        {
                            if (Request.QueryString["DeviceType"] != null)
                            {
                                if (Request.QueryString["DeviceType"].ToString() == "Denso")
                                {
                                    Response.Write("<script>");
                                    Response.Write("alert('INVALID RFID TAG!');");
                                    Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "&QRCode=" + HttpUtility.UrlEncode(Request.QueryString["QRCode"].ToString()) + "&DeviceType=Denso';");
                                    Response.Write("</script>");
                                    //    Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString() + "&DeviceType=Denso");
                                }
                                else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                                {
                                    Response.Write("<script>");
                                    Response.Write("alert('INVALID RFID TAG!');");
                                    Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "&QRCode=" + HttpUtility.UrlEncode(Request.QueryString["QRCode"].ToString()) + "&DeviceType=HT';");
                                    Response.Write("</script>");
                                    //    Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString() + "&DeviceType=Denso");
                                }
                            }
                            else
                            {
                                Response.Write("<script>");
                                Response.Write("alert('INVALID RFID TAG!');");
                                Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "&QRCode=" + HttpUtility.UrlEncode(Request.QueryString["QRCode"].ToString()) + "';");
                                Response.Write("</script>");

                                //   Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString());
                            } 
                        }
                        else if (!(isValidText(@"[A-Za-z0-9]", Request.QueryString["QRCode"].ToString())))
                        {
                            if (Request.QueryString["DeviceType"] != null)
                            {
                                if (Request.QueryString["DeviceType"].ToString() == "Denso")
                                {
                                    Response.Write("<script>");
                                    Response.Write("alert('INVALID QR CODE!');");
                                    Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "&QRCode=" + HttpUtility.UrlEncode(Request.QueryString["QRCode"].ToString()) + "&DeviceType=Denso';");
                                    Response.Write("</script>");
                                    //    Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString() + "&DeviceType=Denso");
                                }
                                else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                                {
                                    Response.Write("<script>");
                                    Response.Write("alert('INVALID QR CODE!');");
                                    Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "&QRCode=" + HttpUtility.UrlEncode(Request.QueryString["QRCode"].ToString()) + "&DeviceType=HT';");
                                    Response.Write("</script>");
                                    //    Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString() + "&DeviceType=Denso");
                                }
                            }
                            else
                            {
                                Response.Write("<script>");
                                Response.Write("alert('INVALID QR CODE!');");
                                Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "&QRCode=" + HttpUtility.UrlEncode(Request.QueryString["QRCode"].ToString()) + "';");
                                Response.Write("</script>");

                                //   Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString());
                            } 
                        }
                        else
                        {
                            txtDNNo.Text = Request.QueryString["DNNo"].ToString();
                            txtRFID.Text = Request.QueryString["RFIDTag"].ToString();
                            txtLotQR.Text = Request.QueryString["QRCode"].ToString();
                            txtLotQR_TextChanged(this, EventArgs.Empty);
                        }
                    }

                }

                if (Request.Form["Delete"] != null)
                {
                    if (Request.Form["Delete"].ToString().Equals("1"))
                    {
                        if (Request.QueryString["DeviceType"] != null)
                        {
                            if (Request.QueryString["DeviceType"].ToString() == "Denso")
                            {
                                Response.Redirect("LotDataScanningDeleteRecord.aspx?DNNo=" + txtDNNo.Text.Trim().ToUpper() + "&RFIDTag=" + txtRFID.Text.Trim().ToUpper() + "&DeviceType=Denso");
                            }
                            else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                            {
                                Response.Redirect("LotDataScanningDeleteRecord.aspx?DNNo=" + txtDNNo.Text.Trim().ToUpper() + "&RFIDTag=" + txtRFID.Text.Trim().ToUpper() + "&DeviceType=HT");
                            }
                        }
                        else
                        {
                            Response.Redirect("LotDataScanningDeleteRecord.aspx?DNNo=" + txtDNNo.Text.Trim().ToUpper() + "&RFIDTag=" + txtRFID.Text.Trim().ToUpper());
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

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
        //    try
        //    {
                int Start, End;
                if (strSource.Contains(strStart) && strSource.Contains(strEnd))
                {
                    Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                    End = strSource.IndexOf(strEnd, Start);
                    return strSource.Substring(Start, End - Start);
                }
                else
                {
                    return "";
                }
            //}
            //catch (Exception ex)
            //{
            //    Logger.GetInstance().Fatal(ex.StackTrace, ex);
            //    MsgBox1.alert(ex.Message);
            //}
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

        public static string getPipeCount(string strSource)
        {
            int iPipeCount = 0;

            foreach (char c in strSource)
            {
                if (c == '|')
                {
                    iPipeCount++;
                    //     return iPipeCount.ToString();
                }
            }
            return iPipeCount.ToString();
        }


        protected void txtLotQR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strLotQRData = "|" + txtLotQR.Text.Trim().ToUpper() + "|Z";

                int iPipeCount = Convert.ToInt32(getPipeCount(txtLotQR.Text.Trim().ToUpper()));

                if (iPipeCount >= 5)
                {
                    lblMessage.Text = "";
                    // MsgBox1.alert("YES");
                    // string strLotQRData = "|"+txtLotQR.Text+"|Z";
                    if (strLotQRData.Contains("|Z1") && strLotQRData.Contains("|Z7") && strLotQRData.Contains("|Z2") && strLotQRData.Contains("|Z3") && strLotQRData.Contains("|Z4") && strLotQRData.Contains("|Z5"))
                    {
                        lblMessage.Text = "";
                        string strPartCode = getBetween(strLotQRData, "|Z1", "|Z");
                        Maintenance maint = new Maintenance();
                        DataSet ds = new DataSet();
                        ds = maint.GET_PARTCODE_IFEXISTS_IN_DN(txtDNNo.Text.Trim(), strPartCode);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblMessage.Text = "";

                            AutoSaveQR();
                        }
                        else
                        {
                            lblMessage.Text = "PARTCODE DOES NOT EXIST IN DN!";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            txtLotQR.Text = "";
                            txtLotQR.Focus();
                        }
                    }
                    else
                    {
                        lblMessage.Text = "INVALID QR DATA!";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        //MsgBox1.alert("INVALID QR DATA!");
                    }
                }
                else
                {
                    lblMessage.Text = "INVALID QR DATA!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    // MsgBox1.alert("INVALID QR DATA!");
                }




            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }


            //COMMENTED 27FEB2019
            //try
            //{
            //    string strLotQRData = "|" + txtLotQR.Text.Trim().ToUpper() + "|Z";

            //    int iPipeCount = Convert.ToInt32(getPipeCount(txtLotQR.Text.Trim().ToUpper()));

            //    if (iPipeCount >= 5)
            //    {
            //       // MsgBox1.alert("YES");
            //       // string strLotQRData = "|"+txtLotQR.Text+"|Z";
            //        if (strLotQRData.Contains("|Z1") && strLotQRData.Contains("|Z7") && strLotQRData.Contains("|Z2") && strLotQRData.Contains("|Z3") && strLotQRData.Contains("|Z4") && strLotQRData.Contains("|Z5"))
            //        {
            //            AutoSaveQR();
            //        }
            //        else
            //        {
            //            MsgBox1.alert("INVALID QR DATA!");
            //        }
            //    }
            //    else
            //    {
            //        MsgBox1.alert("INVALID QR DATA!");
            //    }



                
            //}
            //catch (Exception ex)
            //{
            //    Logger.GetInstance().Fatal(ex.StackTrace, ex);
            //    MsgBox1.alert(ex.Message);
            //}
        }

        private void AutoSaveQR()
        {
            try
            {
                string strLotQRData = "|" + txtLotQR.Text.Trim().ToUpper() + "|Z";

                if (txtRFID.Text.Trim() == "")
                {
                    lblMessage.Text = "PLEASE INPUT RFID FIRST";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblMessage.Text = "";

                    //SEPARATE DETAILS
                    string refno = getBetween(strLotQRData, "|Z5", "|Z");
                    string strLoginType = System.Configuration.ConfigurationManager.AppSettings["webFor"];

                    DataSet ds4 = new DataSet();
                    ds4 = LotDataScanningDAL.CHECK_REFNO_IF_ALREADY_EXISTS(refno, strLoginType);
                    if (ds4.Tables[0].Rows.Count > 0)
                    {
                        lblMessage.Text = "REF. NO. ALREADY EXISTS!";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {

                        //   Regex regex2 = new Regex("[A-Z,a-z,0-9,#%+()/-_;,.\]");

                        if (!(getBetween(strLotQRData, "|Z1", "|Z").Length == 9 || getBetween(strLotQRData, "|Z1", "|Z").Length == 11))
                        {
                            lblMessage.Text = "INVALID PARTCODE!";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        //else if (!regex.IsMatch(getBetween(txtLotQR.Text, "Z1", "|")))
                        else if (!(isValidText(@"[A-Za-z0-9]", getBetween(strLotQRData, "|Z1", "|Z"))))
                        {
                            lblMessage.Text = "INVALID PARTCODE!";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        else if (!(getBetween(strLotQRData, "|Z2", "|Z").Length == 15))
                        {
                            lblMessage.Text = "INVALID LOT NO.!";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        //else if (!regex.IsMatch(getBetween(txtLotQR.Text, "Z2", "|")))
                        else if (!(isValidText(@"[A-Za-z0-9]", getBetween(strLotQRData, "|Z2", "|Z"))))
                        {
                            lblMessage.Text = "INVALID LOT NO.!";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        else if (!(getBetween(strLotQRData, "|Z5", "|Z").Length == 21))
                        {
                            lblMessage.Text = "INVALID REF NO.!";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        //else if (!regex.IsMatch(getBetween(txtLotQR.Text, "Z5", "|")))
                        else if (!(isValidText(@"[A-Za-z0-9]", getBetween(strLotQRData, "|Z5", "|Z"))))
                        {
                            lblMessage.Text = "INVALID REF NO.!";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        //else if (!re.IsMatch(getBetween(txtLotQR.Text, "Z3", "|")))
                        else if (!(isValidText(@"[0-9]", getBetween(strLotQRData, "|Z3", "|Z"))))
                        {
                            lblMessage.Text = "INVALID QTY!";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        //COMMENTED 15MAR2019 else if (!(getBetween(strLotQRData, "|Z4", "|Z").Length <= 15))
                        else if (!(getBetween(strLotQRData, "|Z4", "|Z").Length <= 20))
                        {
                            lblMessage.Text = "INVALID REMARKS!";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        //else if (!regex2.IsMatch(getBetween(txtLotQR.Text, "Z4", "|")))
                        //COMMENTED 27FEB2019 else if (!(isValidText(@"[A-Za-z0-9#%+\(\)/\-_;,.\\]", getBetween(strLotQRData, "|Z4", "|Z"))))
                        else if (!(isValidText(@"[A-Za-z0-9\s#%+\(\)/\-_;,.\\]", getBetween(strLotQRData, "|Z4", "|Z"))))
                        {
                            lblMessage.Text = "INVALID REMARKS!";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        else
                        {
                            txtPartCode.Text = getBetween(strLotQRData, "|Z1", "|Z");
                            txtLotNo.Text = getBetween(strLotQRData, "|Z2", "|Z");
                            txtRefNo.Text = getBetween(strLotQRData, "|Z5", "|Z");
                            txtQtyTotal.Text = getBetween(strLotQRData, "|Z3", "|Z");
                            txtRemarks.Text = getBetween(strLotQRData, "|Z4", "|Z");
                            if (strLoginType == "EPPI")
                            {
                                supplierid = "";
                            }
                            else if (strLoginType == "OUTSIDE")
                            {
                                supplierid = Session["supplierID"].ToString();
                            }

                            int procflag = 0;
                            int bypassflag = 0;
                            string scannedby = ConfigurationManager.AppSettings["webFor"].ToString();

                            DataSet ds5 = new DataSet();
                            ds5 = LotDataScanningDAL.GET_TOTALDNQTY(txtDNNo.Text.Trim().ToUpper(), txtPartCode.Text.Trim().ToUpper());
                            //GET TOTAL SCANNED QTY OF PARTCODE
                            if (ds5.Tables[0].Rows.Count > 0)
                            {
                                string dnqty = ds5.Tables[0].Rows[0]["DNQTY"].ToString();
                                substring = dnqty.Split('.')[0];
                                txtQty2.Text = substring;
                            }

                            DataSet ds7 = new DataSet();
                            ds7 = LotDataScanningDAL.GET_TOTALSCANNEDDNQTY(txtDNNo.Text.Trim().ToUpper(), txtPartCode.Text.Trim().ToUpper(), strLoginType);
                            //GET TOTAL SCANNED QTY OF PARTCODE
                            if (ds7.Tables[0].Rows.Count > 0)
                            {
                                txtQty1.Text = ds7.Tables[0].Rows[0]["TOTAL"].ToString();

                            }
                            else
                            {
                                txtQty1.Text = "0";
                            }

                            string createdby = Session["UserID"].ToString();
                            //SAVED DNDATA TO SCANNED TABLE
                            decimal iQtyTotal = 0;
                            decimal iScanned = 0;

                            iQtyTotal = Convert.ToDecimal(txtQty2.Text);
                            iScanned = Convert.ToDecimal(txtQtyTotal.Text) + Convert.ToDecimal(txtQty1.Text);

                            if (iScanned > iQtyTotal)
                            {
                                lblMessage.Text = "EXCEEDED QTY CANNOT PROCEED!";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                if (strLoginType == "EPPI")
                                {
                                    //Encode Remarks
                                    byte[] strToEncode = UTF8Encoding.UTF8.GetBytes(txtRemarks.Text);
                                    //Decode Remarks
                                    string strRemarksDecoded = UTF8Encoding.UTF8.GetString(strToEncode);


                                    string strResult = "";
                                    strResult = LotDataScanningDAL.ADD_SCANNED_DNDATA_EPPI(txtDNNo.Text.Trim().ToUpper(), txtRFID.Text.Trim().ToUpper(), strLotQRData, txtPartCode.Text.Trim().ToUpper(), txtLotNo.Text.Trim().ToUpper(),
                                         txtRefNo.Text.Trim().ToUpper(), Convert.ToDecimal(txtQtyTotal.Text), strRemarksDecoded, Convert.ToDecimal(txtQty2.Text), supplierid,
                                         0, bypassflag, scannedby, createdby);

                                    lblMessage.Text = strResult.ToString();

                                    if (strResult == "SUCCESSFULLY SAVED!")
                                    {
                                        lblMessage.ForeColor = System.Drawing.Color.Green;
                                    }
                                    else
                                    {
                                        lblMessage.ForeColor = System.Drawing.Color.Red;
                                    }
                                    txtRFID.Text = "";
                                    txtLotQR.Text = "";
                                    txtRFID.Focus();

                                }
                                else if (strLoginType == "OUTSIDE")
                                {
                                    //Encode Remarks
                                    byte[] strToEncode = UTF8Encoding.UTF8.GetBytes(txtRemarks.Text);
                                    //Decode Remarks
                                    string strRemarksDecoded = UTF8Encoding.UTF8.GetString(strToEncode);

                                    string strResult = "";
                                    strResult = LotDataScanningDAL.ADD_SCANNED_DNDATA_SUPPLIER(txtDNNo.Text.Trim().ToUpper(), txtRFID.Text.Trim().ToUpper(), strLotQRData, txtPartCode.Text.Trim().ToUpper(), txtLotNo.Text.Trim().ToUpper(),
                                       txtRefNo.Text.Trim().ToUpper(), Convert.ToDecimal(txtQtyTotal.Text), strRemarksDecoded, Convert.ToDecimal(txtQty2.Text), supplierid,
                                       0, bypassflag, scannedby, createdby);

                                    lblMessage.Text = strResult.ToString();

                                    if (strResult == "SUCCESSFULLY SAVED!")
                                    {
                                        lblMessage.ForeColor = System.Drawing.Color.Green;
                                    }
                                    else
                                    {
                                        lblMessage.ForeColor = System.Drawing.Color.Red;
                                    }
                                    txtRFID.Text = "";
                                    txtLotQR.Text = "";
                                    txtRFID.Focus();
                                }
                            }

                            DataSet ds6 = new DataSet();
                            ds6 = LotDataScanningDAL.GET_TOTALSCANNEDDNQTY(txtDNNo.Text.Trim().ToUpper(), txtPartCode.Text.Trim().ToUpper(), strLoginType);
                            //GET TOTAL SCANNED QTY OF PARTCODE
                            if (ds6.Tables[0].Rows.Count > 0)
                            {
                                //txtQty1.Text = ds6.Tables[0].Rows[0]["TOTAL"].ToString();

                                string scannedtotal = ds6.Tables[0].Rows[0]["TOTAL"].ToString();
                                substring2 = scannedtotal.Split('.')[0];
                                txtQty1.Text = substring2;
                            }
                        }
                    }
                    txtRFID.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void txtRFID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (txtRFID.Text.Length > 24 || txtRFID.Text.Length < 24)
                if (txtRFID.Text.Length != 28 && txtRFID.Text.Length != 24)
                {
                    lblMessage.Text = "INVALID RFID TAG!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    txtRFID.Text = "";
                    txtRFID.Focus();
                }
                else
                {
                    lblMessage.Text = "";
                    string strLoginType = System.Configuration.ConfigurationManager.AppSettings["webFor"];
                    DataSet ds = new DataSet();

                    ds = LotDataScanningDAL.CHECK_RFID_IF_ALREADY_EXISTS(txtRFID.Text.Trim().ToUpper(), strLoginType);

                    //GET RFID DETAILS
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //GET PROCFLAG AND DNDATA DETAILS OF RFID
                        string procflag = ds.Tables[0].Rows[0]["PROCFLAG"].ToString();
                        string dndata = ds.Tables[0].Rows[0]["BARCODEDNNO"].ToString();
                        if (procflag == "0")
                        {
                            //CHECK IF SAME DNDATA
                            if (dndata == txtDNNo.Text.Trim().ToUpper())
                            {
                                lblMessage.Text = "RFID TAG ALREADY EXISTS!";
                                lblMessage.ForeColor = System.Drawing.Color.Red;

                                DataSet ds2 = new DataSet();


                                ds2 = LotDataScanningDAL.GET_QRDATA_BASED_FROM_DN_AND_RFID(txtRFID.Text.Trim().ToUpper(), txtDNNo.Text.Trim().ToUpper(), strLoginType);

                                //GET QR DETAILS IF RFID HAS SAME DN DATA
                                if (ds2.Tables[0].Rows.Count > 0)
                                {
                                    txtLotQR.Text = ds2.Tables[0].Rows[0]["QRCODE"].ToString();
                                    txtPartCode.Text = ds2.Tables[0].Rows[0]["ITEMCODE"].ToString();
                                    txtLotNo.Text = ds2.Tables[0].Rows[0]["LOTNO"].ToString();
                                    txtRefNo.Text = ds2.Tables[0].Rows[0]["REFNO"].ToString();
                                    txtQtyTotal.Text = ds2.Tables[0].Rows[0]["QTY"].ToString();
                                    txtRemarks.Text = ds2.Tables[0].Rows[0]["REMARKS"].ToString();
                                    txtQty2.Text = ds2.Tables[0].Rows[0]["DNQTY"].ToString();

                                    DataSet ds3 = new DataSet();
                                    ds3 = LotDataScanningDAL.GET_TOTALSCANNEDDNQTY(txtDNNo.Text.Trim().ToUpper(), txtPartCode.Text.Trim().ToUpper(), strLoginType);
                                    //GET TOTAL SCANNED QTY OF PARTCODE
                                    if (ds3.Tables[0].Rows.Count > 0)
                                    {
                                        txtQty1.Text = ds3.Tables[0].Rows[0]["TOTAL"].ToString();

                                    }
                                }
                                btnDelete.Enabled = true;
                            }
                            else
                            {
                                lblMessage.Text = " RFID TAG IS EXISTING ON OTHER DN!";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                btnDelete.Enabled = true;
                            }
                        }
                        else
                        {
                            txtLotQR.Focus();
                            lblMessage.Text = "";
                        }
                    }
                    else
                    {
                        txtLotQR.Text = "";
                        txtPartCode.Text = "";
                        txtLotNo.Text = "";
                        txtRefNo.Text = "";
                        txtQtyTotal.Text = "";
                        txtRemarks.Text = "";
                        txtQty2.Text = "";
                        txtQty1.Text = "";

                        txtLotQR.Focus();
                        lblMessage.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtLotQR.Text = "";
                txtPartCode.Text = "";
                txtLotNo.Text = "";
                txtRefNo.Text = "";
                txtQtyTotal.Text = "";
                txtRemarks.Text = "";
                txtQty2.Text = "";
                txtQty1.Text = "";
                lblMessage.Text = "";
                txtRFID.Text = "";
                btnDelete.Enabled = false;
                txtRFID.Focus();
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                MsgBox1.confirm("Delete existing record?", "Delete");
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["DeviceType"] != null)
                {
                    if (Request.QueryString["DeviceType"].ToString() == "Denso")
                    {
                        Response.Redirect("LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&DeviceType=Denso");
                    }
                    else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                    {
                        Response.Redirect("LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&DeviceType=HT");
                    }
                }
                else
                {
                    Response.Redirect("LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()));
                } 

            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }
    }
}
