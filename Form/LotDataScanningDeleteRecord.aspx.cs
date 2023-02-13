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
using FGWHSEClient.LdapService;
using FGWHSEClient.LogInService;
using System.Text.RegularExpressions; 

namespace FGWHSEClient.Form
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        public string strPageSubsystem = "";
        public string strAccessLevel = "";
        LotDataScanningDAL LotDataScanningDAL = new LotDataScanningDAL();
        Maintenance maint = new Maintenance();

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
                Page.Title = string.Format("MANUAL PARTS RECEIVING - DELETE RECORD");
            }
            else
            {
                Page.Title = string.Format("PARTS LOADING - DELETE RECORD");
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string strLoginType2 = System.Configuration.ConfigurationManager.AppSettings["webFor"];
                if (strLoginType2 == "EPPI")
                {
                    lblHeader.Text = "MANUAL PARTS RECEIVING - DELETE RECORD";
                }
                else
                {
                    lblHeader.Text = "PARTS LOADING - DELETE RECORD";
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
                            // dvLotDataScan2.Attributes.Add("style", "font-size:x-small;width:270px");
                            dvLotDataScan.Attributes.Add("style", "font-size:x-small;width:270px");
                            //grdDNData.Attributes.Add("style", "font-size:x-small;overflow:auto;height:105px;width:230px");
                            //grdDNData.Attributes.Add("style", "font-size:x-small;width:230px;");
                            lblHeader.Attributes.Add("style", "font-size:11px;");
                            grdDNData.Attributes.Add("style", "font-size:10px;width:800px;");
                            grdHeight.Attributes.Add("style", "overflow:auto;height:115px;width:250px");
                            txtUsername.Attributes.Add("style", "font-size:18px;width:167px;height:15px;");
                            txtPassword.Attributes.Add("style", "font-size:18px;width:167px;height:15px;");
                            tablestyle.Attributes.Add("style", "cellpadding:0;cellspacing:0");
                            tdUser.Attributes.Add("style", "padding-left:5px");
                            tdPass.Attributes.Add("style", "padding-left:5px");
                            lblUserName.Attributes.Add("style", "font-size:11px;");
                            lblPassword.Attributes.Add("style", "font-size:11px;");
                            lblMessage2.Attributes.Add("style", "font-size:11px;");
                            lblMessage.Attributes.Add("style", "font-size:11px;width:300px;");
                            tdMessage.Attributes.Add("style", "border: 1px solid #385d8a; background-color:#d9d9d9;font-size:x-small;height:20px;width:165px;text-align:left");
                            //btnDelete.Attributes.Add("style", "font-size:x-small;width:55px;height:25px");
                            //btnBack.Attributes.Add("style", "font-size:x-small;width:55px;height:25px");
                            //btnClear.Attributes.Add("style", "font-size:x-small;width:53px;height:25px");
                            btnDelete.Attributes.Add("style", "font-size:14px;width:75px;height:25px");
                            btnBack.Attributes.Add("style", "font-size:14px;width:75px;height:25px");
                            btnClear.Attributes.Add("style", "font-size:14px;width:75px;height:25px");
                            
                            lblProceed.Attributes.Add("style", "font-size:11px;");

                            txtDNNo.Attributes.Add("style", "font-size:18px;width:165px;height:15px;");
                            txtRFID.Attributes.Add("style", "font-size:18px;width:165px;height:15px;");
                            txtPartCode.Attributes.Add("style", "font-size:18px;width:165px;height:15px;");
                            txtLotNo.Attributes.Add("style", "font-size:18px;width:165px;height:15px;");
                            txtRefNo.Attributes.Add("style", "font-size:18px;width:165px;height:15px;");
                            txtQty.Attributes.Add("style", "font-size:18px;width:165px;height:15px;");
                            txtRemarks.Attributes.Add("style", "font-size:18px;width:165px;height:15px;");
                            lblDN.Attributes.Add("style", "font-size:12px;");
                            lblRFID.Attributes.Add("style", "font-size:12px;");
                            lblPCode.Attributes.Add("style", "font-size:12px;");
                            lblLot.Attributes.Add("style", "font-size:12px;");
                            lblRefNo.Attributes.Add("style", "font-size:12px;");
                            lblQty.Attributes.Add("style", "font-size:12px;");
                            lblRemarks.Attributes.Add("style", "font-size:12px;");
                            tablestyle2.Attributes.Add("style", "cellpadding:0;cellspacing:0");
                            //txtUsername.Attributes.Add("onkeypress", "return event.keyCode!=13");
                        }
                    }











                    //if (Request.QueryString["DNNo"] != null && Request.QueryString["RFIDTag"] != null)
                    string strLoginType = System.Configuration.ConfigurationManager.AppSettings["webFor"];
                    //if (Request.QueryString["DNNo"] != null && Request.QueryString["RFIDTag"] != null && Request.QueryString["QRCode"] != null)
                    //{
                    //    pnlDNandRFID.Visible = true;
                    //    pnlDN.Visible = false;
                    //    DataSet ds = new DataSet();
                    //    ds = LotDataScanningDAL.GET_QRDATA_BASED_FROM_RFID_NOT_EQUAL_TO_SCANNED_QR(Request.QueryString["RFIDTag"].ToString(), Request.QueryString["QRCode"].ToString(), strLoginType);
                    //    if (ds.Tables[0].Rows.Count > 0)
                    //    {
                    //        txtDNNo.Text = ds.Tables[0].Rows[0]["BARCODEDNNO"].ToString();
                    //        txtRFID.Text = ds.Tables[0].Rows[0]["RFIDTAG"].ToString();
                    //        txtPartCode.Text = ds.Tables[0].Rows[0]["ITEMCODE"].ToString();
                    //        txtLotNo.Text = ds.Tables[0].Rows[0]["LOTNO"].ToString();
                    //        txtRefNo.Text = ds.Tables[0].Rows[0]["REFNO"].ToString();
                    //        txtQty.Text = ds.Tables[0].Rows[0]["QTY"].ToString();
                    //        txtRemarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString();
                    //    }
                    if (Request.QueryString["RFIDTag"] != null)
                    {
                        if (!(isValidText(@"[A-Za-z0-9]", Request.QueryString["RFIDTag"].ToString())))
                        {
                            if (Request.QueryString["DeviceType"] != null)
                            {
                                if (Request.QueryString["DeviceType"].ToString() == "Denso")
                                {
                                    Response.Write("<script>");
                                    Response.Write("alert('INVALID RFID TAG!');");
                                    Response.Write("window.location = '../Form/LotDataScanning.aspx?RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "&DeviceType=Denso';");
                                    Response.Write("</script>");
                                    //    Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString() + "&DeviceType=Denso");
                                }
                                else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                                {
                                    Response.Write("<script>");
                                    Response.Write("alert('INVALID RFID TAG!');");
                                    Response.Write("window.location = '../Form/LotDataScanning.aspx?RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "&DeviceType=HT';");
                                    Response.Write("</script>");
                                    //    Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString() + "&DeviceType=Denso");
                                }
                            }
                            else
                            {
                                Response.Write("<script>");
                                Response.Write("alert('INVALID RFID TAG.!');");
                                Response.Write("window.location = '../Form/LotDataScanning.aspx?RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "';");
                                Response.Write("</script>");

                                //   Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString());
                            } 
                        }
                        else
                        {
                            DataSet ds = new DataSet();
                            ds = LotDataScanningDAL.CHECK_RFID_DETAILS(Request.QueryString["RFIDTag"].ToString(), strLoginType);
                            {
                                pnlDNandRFID.Visible = true;
                                pnlDN.Visible = false;
                                txtDNNo.Text = ds.Tables[0].Rows[0]["BARCODEDNNO"].ToString();
                                txtRFID.Text = ds.Tables[0].Rows[0]["RFIDTAG"].ToString();
                                txtPartCode.Text = ds.Tables[0].Rows[0]["ITEMCODE"].ToString();
                                txtLotNo.Text = ds.Tables[0].Rows[0]["LOTNO"].ToString();
                                txtRefNo.Text = ds.Tables[0].Rows[0]["REFNO"].ToString();
                                txtQty.Text = ds.Tables[0].Rows[0]["QTY"].ToString();
                                txtRemarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString();
                                txtUsername.Focus();
                            }
                        }
                    }
                    else if (Request.QueryString["DNNo"] != null && Request.QueryString["RFIDTag"] == null)
                    {
                        if (!(isValidText(@"[A-Za-z0-9]", Request.QueryString["DNNo"].ToString()))) 
                        {
                            if (Request.QueryString["DeviceType"] != null)
                            {
                                if (Request.QueryString["DeviceType"].ToString() == "Denso")
                                {
                                    Response.Write("<script>");
                                    Response.Write("alert('INVALID DN NO.!');");
                                    Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "&DeviceType=Denso';");
                                    Response.Write("</script>");
                                    //    Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString() + "&DeviceType=Denso");
                                }
                                else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                                {
                                    Response.Write("<script>");
                                    Response.Write("alert('INVALID DN NO.!');");
                                    Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "&DeviceType=HT';");
                                    Response.Write("</script>");
                                    //    Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString() + "&DeviceType=Denso");
                                }
                            }
                            else
                            {
                                Response.Write("<script>");
                                Response.Write("alert('INVALID DN NO.!');");
                                Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "';");
                                Response.Write("</script>");

                                //   Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString());
                            } 
                        }
                        //else if (!(isValidText(@"[A-Za-z0-9]", Request.QueryString["RFIDTag"].ToString())))
                        //{
                        //    if (Request.QueryString["DeviceType"] != null)
                        //    {
                        //        if (Request.QueryString["DeviceType"].ToString() == "Denso")
                        //        {
                        //            Response.Write("<script>");
                        //            Response.Write("alert('INVALID RFID TAG!');");
                        //            Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "&DeviceType=Denso';");
                        //            Response.Write("</script>");
                        //            //    Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString() + "&DeviceType=Denso");
                        //        }
                        //    }
                        //    else
                        //    {
                        //        Response.Write("<script>");
                        //        Response.Write("alert('INVALID RFID TAG!');");
                        //        Response.Write("window.location = '../Form/LotDataScanning.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "';");
                        //        Response.Write("</script>");

                        //        //   Response.Redirect("LotDataScanning.aspx?DNNo=" + Request.QueryString["DNNo"].ToString());
                        //    } 
                        //}
                        else
                        {
                            pnlDN.Visible = true;
                            pnlDNandRFID.Visible = false;
                            grdDNData.DataSource = LotDataScanningDAL.GET_DETAILS_BASED_FROM_DNNO(Request.QueryString["DNNo"].ToString(), strLoginType);
                            grdDNData.DataBind();
                            txtUsername.Focus();
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["DNNo"] != null && Request.QueryString["RFIDTag"] != null)
                {
                    if (Request.QueryString["DeviceType"] != null)
                    {
                        if (Request.QueryString["DeviceType"].ToString() == "Denso")
                        {
                            //Response.Redirect("LotDataScanningDetails.aspx?DNNo=" + txtDNNo.Text.Trim().ToUpper() + "&RFIDTag=" + txtRFID.Text.Trim().ToUpper() + "&DeviceType=Denso");
                            //8-24-18
                            //Response.Redirect("LotDataScanningDetails.aspx?DNNo=" + txtDNNo.Text.Trim().ToUpper() + "&RFIDTag=" + txtRFID.Text.Trim().ToUpper() + "&DeviceType=Denso");
                            Response.Redirect("LotDataScanningDetails.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "&DeviceType=Denso");
                        }
                        else if (Request.QueryString["DeviceType"].ToString() == "HT")//added by tin 15AUG2019
                        {
                            //Response.Redirect("LotDataScanningDetails.aspx?DNNo=" + txtDNNo.Text.Trim().ToUpper() + "&RFIDTag=" + txtRFID.Text.Trim().ToUpper() + "&DeviceType=Denso");
                            //8-24-18
                            //Response.Redirect("LotDataScanningDetails.aspx?DNNo=" + txtDNNo.Text.Trim().ToUpper() + "&RFIDTag=" + txtRFID.Text.Trim().ToUpper() + "&DeviceType=Denso");
                            Response.Redirect("LotDataScanningDetails.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()) + "&DeviceType=HT");
                        }
                    }
                    else
                    {
                        //8-24-18
                        //Response.Redirect("LotDataScanningDetails.aspx?DNNo=" + txtDNNo.Text.Trim().ToUpper() + "&RFIDTag=" + txtRFID.Text.Trim().ToUpper());
                        Response.Redirect("LotDataScanningDetails.aspx?DNNo=" + HttpUtility.UrlEncode(Request.QueryString["DNNo"].ToString()) + "&RFIDTag=" + HttpUtility.UrlEncode(Request.QueryString["RFIDTag"].ToString()));
                       
                    }

                }
                else if (Request.QueryString["DNNo"] != null && Request.QueryString["RFIDTag"] == null)
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
                if (Request.QueryString["DNNo"] != null && Request.QueryString["RFIDTag"] != null)
                {
                    if (txtPassword.Text == "")
                    {
                        txtPassword.Focus();
                    }
                    else
                    {
                        string strLoginType = System.Configuration.ConfigurationManager.AppSettings["webFor"];
                        string strSystemName = "FGWHSE Monitoring";
                        Maintenance maint = new Maintenance();
                        DataView dvUser = new DataView();

                        if (strLoginType == "EPPI")
                        {
                            //validate username and password in AD
                            bool isAuthenticated = ServiceLocator.GetLdapService().IsAuthenticated(txtUsername.Text, txtPassword.Text);
                            if (isAuthenticated)
                            {
                                DsUser ds = ServiceLocator.GetLoginService().GetUser(txtUsername.Text);
                                if (ds.SystemUser.Rows.Count <= 0)
                                {
                                    //MsgBox1.alert("User profile is incomplete! " +
                                    //    "Please contact the administrator " +
                                    //    "to check if user has all information required. ");
                                    lblMessage.Text = "USER NOT AUTHORIZED!";
                                    lblMessage.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }


                                dvUser = maint.GetUsersLDAP(txtUsername.Text, strSystemName);
                                if (dvUser.Count > 0)
                                {
                                    Session["UserID2"] = Convert.ToString(dvUser[0]["UserID"]);
                                    Session["UserName2"] = Convert.ToString(dvUser[0]["UserName"]);
                                    Session["RoleID2"] = Convert.ToString(dvUser[0]["Role"]);
                                    GetUserSubsystems();
                                    //    isLogin = true;
                                }
                                else
                                {
                                    //MsgBox1.alert("Invalid login! You have no access rights in the system. Please contact system administrator.");
                                    lblMessage.Text = "USER NOT AUTHORIZED!";
                                    lblMessage.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }

                            }
                            else
                            {
                                dvUser = maint.GetUser(strSystemName, txtUsername.Text.Trim(), txtPassword.Text.Trim(), 0);
                                if (dvUser.Count > 0)
                                {
                                    Session["UserID2"] = Convert.ToString(dvUser[0]["User_ID"]);
                                    Session["UserName2"] = Convert.ToString(dvUser[0]["User_Name"]);
                                    Session["RoleID2"] = Convert.ToString(dvUser[0]["Role"]);
                                    GetUserSubsystems();
                                    //    isLogin = true;
                                }
                                else
                                {
                                    lblMessage.Text = "USER NOT AUTHORIZED!";
                                    lblMessage.ForeColor = System.Drawing.Color.Red;
                                    // MsgBox1.alert("Invalid login! You have no access rights in the system. Please contact system administrator.");
                                    return;
                                }
                            }

                            strPageSubsystem = "FGWHSE_015";
                            if (!checkAuthority(strPageSubsystem))
                            {
                                lblMessage.Text = "USER NOT AUTHORIZED!";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                LotDataScanningDAL.DELETE_SCANNEDDATA_BASED_FROM_DN_AND_RFID(txtDNNo.Text, txtRFID.Text, strLoginType);
                                lblMessage.Text = "RECORD SUCCESFULLY DELETED.";
                                lblMessage.ForeColor = System.Drawing.Color.Green;
                            }
                        }
                        else if (strLoginType == "OUTSIDE")
                        {
                            dvUser = maint.LOGIN_SUPPLIER(txtUsername.Text.Trim(), txtPassword.Text.Trim()).Tables[0].DefaultView;
                            if (dvUser.Count > 0)
                            {
                                Session["UserID2"] = Convert.ToString(dvUser[0]["USERID"]);
                                Session["UserName2"] = Convert.ToString(dvUser[0]["USERNAME"]);
                                Session["ACCESSLEVEL2"] = Convert.ToString(dvUser[0]["ACCESSLEVEL"]);
                                Session["subsystem_id2"] = Convert.ToString(dvUser[0]["subsystem_id"]);
                                Session["Subsystem2"] = dvUser;
                                //   isLogin = true;
                            }
                            else
                            {
                                //MsgBox1.alert("Invalid login! You have no access rights in the system. Please contact system administrator.");
                                lblMessage.Text = "USER NOT AUTHORIZED!";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                            strPageSubsystem = "FGWHSE_015";
                            if (!checkAuthority(strPageSubsystem))
                            {
                                lblMessage.Text = "USER NOT AUTHORIZED!";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                LotDataScanningDAL.DELETE_SCANNEDDATA_BASED_FROM_DN_AND_RFID(txtDNNo.Text, txtRFID.Text, strLoginType);
                                lblMessage.Text = "RECORD SUCCESFULLY DELETED.";
                                lblMessage.ForeColor = System.Drawing.Color.Green;
                            }
                        }

                    }
                }
                else if (Request.QueryString["DNNo"] != null && Request.QueryString["RFIDTag"] == null)
                {
                    if (txtPassword.Text == "")
                    {
                        txtPassword.Focus();
                    }
                    else
                    {
                        string strLoginType = System.Configuration.ConfigurationManager.AppSettings["webFor"];
                        string strSystemName = "FGWHSE Monitoring";
                        Maintenance maint = new Maintenance();
                        DataView dvUser = new DataView();

                        if (strLoginType == "EPPI")
                        {
                            //validate username and password in AD
                            bool isAuthenticated = ServiceLocator.GetLdapService().IsAuthenticated(txtUsername.Text, txtPassword.Text);
                            if (isAuthenticated)
                            {
                                DsUser ds = ServiceLocator.GetLoginService().GetUser(txtUsername.Text);
                                if (ds.SystemUser.Rows.Count <= 0)
                                {
                                    //MsgBox1.alert("User profile is incomplete! " +
                                    //    "Please contact the administrator " +
                                    //    "to check if user has all information required. ");
                                    lblMessage.Text = "USER NOT AUTHORIZED!";
                                    lblMessage.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }


                                dvUser = maint.GetUsersLDAP(txtUsername.Text, strSystemName);
                                if (dvUser.Count > 0)
                                {
                                    Session["UserID2"] = Convert.ToString(dvUser[0]["UserID"]);
                                    Session["UserName2"] = Convert.ToString(dvUser[0]["UserName"]);
                                    Session["RoleID2"] = Convert.ToString(dvUser[0]["Role"]);
                                    GetUserSubsystems();
                                    //    isLogin = true;
                                }
                                else
                                {
                                    //MsgBox1.alert("Invalid login! You have no access rights in the system. Please contact system administrator.");
                                    lblMessage.Text = "USER NOT AUTHORIZED!";
                                    lblMessage.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }

                            }
                            else
                            {
                                dvUser = maint.GetUser(strSystemName, txtUsername.Text.Trim(), txtPassword.Text.Trim(), 0);
                                if (dvUser.Count > 0)
                                {
                                    Session["UserID2"] = Convert.ToString(dvUser[0]["User_ID"]);
                                    Session["UserName2"] = Convert.ToString(dvUser[0]["User_Name"]);
                                    Session["RoleID2"] = Convert.ToString(dvUser[0]["Role"]);
                                    GetUserSubsystems();
                                    //    isLogin = true;
                                }
                                else
                                {
                                    lblMessage.Text = "USER NOT AUTHORIZED!";
                                    lblMessage.ForeColor = System.Drawing.Color.Red;
                                    // MsgBox1.alert("Invalid login! You have no access rights in the system. Please contact system administrator.");
                                    return;
                                }
                            }

                            strPageSubsystem = "FGWHSE_015";
                            if (!checkAuthority(strPageSubsystem))
                            {
                                lblMessage.Text = "USER NOT AUTHORIZED!";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                LotDataScanningDAL.DELETE_SCANNEDDATA_BASED_FROM_DN(Request.QueryString["DNNo"].ToString(), strLoginType);
                                lblMessage.Text = "DN DATA SUCCESFULLY DELETED.";
                                lblMessage.ForeColor = System.Drawing.Color.Green;
                            }
                        }
                        else if (strLoginType == "OUTSIDE")
                        {
                            dvUser = maint.LOGIN_SUPPLIER(txtUsername.Text.Trim(), txtPassword.Text.Trim()).Tables[0].DefaultView;
                            if (dvUser.Count > 0)
                            {
                                Session["UserID2"] = Convert.ToString(dvUser[0]["USERID"]);
                                Session["UserName2"] = Convert.ToString(dvUser[0]["USERNAME"]);
                                Session["ACCESSLEVEL2"] = Convert.ToString(dvUser[0]["ACCESSLEVEL"]);
                                Session["subsystem_id2"] = Convert.ToString(dvUser[0]["subsystem_id"]);
                                Session["Subsystem2"] = dvUser;
                                //   isLogin = true;
                            }
                            else
                            {
                                //MsgBox1.alert("Invalid login! You have no access rights in the system. Please contact system administrator.");
                                lblMessage.Text = "USER NOT AUTHORIZED!";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                            strPageSubsystem = "FGWHSE_015";
                            if (!checkAuthority(strPageSubsystem))
                            {
                                lblMessage.Text = "USER NOT AUTHORIZED!";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                LotDataScanningDAL.DELETE_SCANNEDDATA_BASED_FROM_DN(Request.QueryString["DNNo"].ToString(), strLoginType);
                                lblMessage.Text = "DN DATA SUCCESFULLY DELETED.";
                                lblMessage.ForeColor = System.Drawing.Color.Green;
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

        private void GetUserSubsystems()
        {
            try
            {
                Maintenance maint = new Maintenance();
                string strSystemName = "FGWHSE Monitoring";

                DataView dv = new DataView();
                dv = maint.GetUsersSubsystems(Session["UserID2"].ToString(), strSystemName);

                Session["Subsystem2"] = dv;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message.ToString());
            }
        }

        private bool checkAuthority(string strPageSubsystem)
        {
            bool isValid = false;
            try
            {
                if (Session["Subsystem2"] != null)
                {
                    DataView dvSubsystem = new DataView();
                    dvSubsystem = (DataView)Session["Subsystem2"];

                    if (dvSubsystem.Count > 0)
                    {
                        string strLoginType = System.Configuration.ConfigurationManager.AppSettings["webFor"];
                        if (strLoginType == "EPPI")
                        {
                            dvSubsystem.Sort = "Subsystem";
                        }
                        else if (strLoginType == "OUTSIDE")
                        {
                            dvSubsystem.Sort = "Subsystem_id";
                        }
                        
                        

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
