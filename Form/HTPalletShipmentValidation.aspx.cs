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
using System.Drawing;
using System.Globalization;
using FGWHSEClient.LdapService;
using FGWHSEClient.LogInService;
using com.eppi.utils;



namespace FGWHSEClient.Form
{

    public partial class HTPalletShipmentValidation : System.Web.UI.Page
    {
        Maintenance maint = new Maintenance();
        public string strPageSubsystem = "";
        public string strAccessLevel = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                txtPalletNo.Focus();
                txtPalletNo.Attributes.Add("onkeydown", "return (event.keyCode!=13);false;");
                txtContainerNo.Attributes.Add("onkeydown", "return (event.keyCode!=13);false;");
                txtUsername.Attributes.Add("onkeydown", "return (event.keyCode!=13);false;");
                if (Request.QueryString["userid"] != null)
                {
                    strPageSubsystem = "FGWHSE_032";
                    if (!checkAuthority(strPageSubsystem))
                    {
                        Response.Write("<script>");
                        Response.Write("window.location = '../HTLoginPalletShipmentValidation.aspx?ERR=You are not authorized to access the page.';");
                        Response.Write("</script>");
                    }


                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "CheckTime()", true);
                    lblUser.Text = Request.QueryString["userid"].ToString();
                }
                else
                {
                    MsgBox.alert("Please login Again!");
                    Response.Redirect("~/HTLoginPalletShipmentValidation.aspx");
                }
             
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

                        string strRole = dvSubsystem.Table.Rows[iRow]["Role"].ToString();

                        if (strRole != "")
                        {
                            strAccessLevel = strRole;
                        }

                    }
                }
                return isValid;
            }
            catch (Exception ex)
            {
                //Response.Write("<script>");
                //Response.Write("alert('An unexpected error has occured!"+ ex.Message  +"');");
                //Response.Write("</script>");

                isValid = false;
                return isValid;
            }
        }

        protected void txtPalletNo_TextChanged(object sender, EventArgs e)
        {
            
            lblStatus.Text = "";
               DataView dv = new DataView();
               dvBG.Attributes.Add("style", "background-color:white; border-color:Black;border-style:solid; border-width:thin; height: 390px");

               dv = maint.CHECK_IF_PALLET_EXISTS(txtPalletNo.Text.Trim()).Tables[0].DefaultView;
               string strPalletCount = "";
               strPalletCount = dv[0][0].ToString();
               if (strPalletCount == "0")
               {
                   //MsgBox.alert("Pallet (" + txtPalletNo.Text.Trim() + ") does not exists!");
                   lblStatus.Text = "Pallet (" + txtPalletNo.Text.Trim() + ") does not exists!";
                   lblStatus.ForeColor = Color.White;
                   txtPalletNo.Text = "";
                   dvBG.Attributes.Clear();
                   dvBG.Attributes.Add("style", "background-color:red; border-color:Black;border-style:solid; border-width:thin; height: 390px");
                   txtPalletNo.Focus();
                   return;
               }
             

               if (txtPalletNo.Text.Trim() == "")
               {
                   lblStatus.Text = "Please scan pallet number!";
                   lblStatus.ForeColor = Color.White;
                   //MsgBox.alert("Please scan pallet number!");
                   dvBG.Attributes.Add("style", "background-color:red; border-color:Black;border-style:solid; border-width:thin; height: 390px");
                   dvBG.Attributes.Clear();
                   txtPalletNo.Focus();
                   return;
               }
               else
               {
                   txtPalletNo.Text = txtPalletNo.Text.ToUpper();
                   txtContainerNo.Focus();
               }

         
        }

        protected void txtContainerNo_TextChanged(object sender, EventArgs e)
        {

            DataView dv = new DataView();
            lblStatus.Text = "";
            dvBG.Attributes.Add("style", "background-color:white; border-color:Black;border-style:solid; border-width:thin; height: 390px");

            dv = maint.CHECK_IF_CONTAINER_EXISTS(txtContainerNo.Text.Trim()).Tables[0].DefaultView;
            string strContainer = "";
            strContainer = dv[0][0].ToString();
            if (strContainer == "0")
            {

                lblStatus.Text = "Container (" + txtContainerNo.Text.Trim() + ") does not exists!";
                //MsgBox.alert("Container (" + txtContainerNo.Text.Trim() + ") does not exists!");
                txtContainerNo.Text = "";
                dvBG.Attributes.Clear();
                lblStatus.ForeColor = Color.White;
                dvBG.Attributes.Add("style", "background-color:red; border-color:Black;border-style:solid; border-width:thin; height: 390px");
		        txtContainerNo.Focus();
                return;
            }
            


            if (lblUser.Text == "")
            {
                MsgBox.alert("Please log-in again!");
                Response.Redirect("~/HTLoginPalletShipmentValidation.aspx");
                return;
            }
            if (txtPalletNo.Text.Trim() == "")
            {
                lblStatus.Text = "Please scan pallet number!";
                //MsgBox.alert("Please scan pallet number!");
                dvBG.Attributes.Clear();
                dvBG.Attributes.Add("style", "background-color:red; border-color:Black;border-style:solid; border-width:thin; height: 390px");
                lblStatus.ForeColor = Color.White;
                txtPalletNo.Focus();
                return;
            }
            else if (txtContainerNo.Text.Trim() == "")
            {
                dvBG.Attributes.Clear();
                dvBG.Attributes.Add("style", "background-color:red; border-color:Black;border-style:solid; border-width:thin; height: 390px");
                txtContainerNo.Focus();
                lblStatus.ForeColor = Color.White;
                lblStatus.Text = "Please scan container!";
                //MsgBox.alert("Please scan container!");
                return;
            }
            else
            {
                try
                {

                    txtContainerNo.Text = txtContainerNo.Text.ToUpper();
                    txtPalletNo.Text = txtPalletNo.Text.ToUpper();
                    string strContainerNo = checkValidContainerNo(txtContainerNo.Text.Trim());

                    if (strContainerNo == "")
                    {
                        dvBG.Attributes.Clear();
                        dvBG.Attributes.Add("style", "background-color:red; border-color:Black;border-style:solid; border-width:thin; height: 390px");
                        txtContainerNo.Text = "";
                        txtContainerNo.Focus();
                        lblStatus.ForeColor = Color.White;
                        lblStatus.Text = "Please enter valid Container Number!";
                        return;
                    }

                    maint.ADD_PALLET_SHIPMENT_VALIDATION(strContainerNo.Trim(), txtPalletNo.Text.Trim(), lblUser.Text);
                    
                    txtPalletNo.Text = "";
                    txtContainerNo.Text = "";
                    txtPalletNo.Focus();
                    dv = maint.GET_PALLET_SHIPMENT_VALIDATION_SCANNED_COUNT(strContainerNo.Trim()).Tables[0].DefaultView;
                    if (dv.Count > 0)
                    {
                        lblPalletScanned.Text = dv[0]["PALLETCOUNT"].ToString();
                        lblContainerNo.Text = dv[0]["CONTAINERNO"].ToString();
                    }

                    lblStatus.ForeColor = Color.DarkGreen;
                    lblStatus.Text = "Successfully saved!";
                    //MsgBox.alert("Successfully saved!");
                }
                catch (Exception ex)
                {
                    lblStatus.Text = ex.Message.ToString();
                    //MsgBox.alert(ex.Message.ToString());
                    dvBG.Attributes.Clear();
		            dvBG.Attributes.Add("style", "background-color:red; border-color:Black;border-style:solid; border-width:thin; height: 390px");
                    lblStatus.ForeColor = Color.White;
                    txtPalletNo.Text = "";
                    txtContainerNo.Text = "";
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HTLoginPalletShipmentValidation.aspx");
        }

        public string checkValidContainerNo(string strContainerNo)
        {
            string strContNo = "",strValidity = "";

            if (strContainerNo.Length > 4)
            {   
                strValidity = strContainerNo.Substring(0, 2).ToUpper();
                if (strValidity == "Z1")
                {
                    strContNo = strContainerNo.Substring(2, strContainerNo.Length - 2);
                }
            }
            return strContNo;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            lblStatus.Text = hTimeStart.Value.ToString() + " / " + hTimeEnd.Value.ToString();
        }

        protected void btnPallet_Click(object sender, EventArgs e)
        {
            double DateTimeDiff = 0;
            if (hTimeStart.Value != "" && hTimeEnd.Value != "")
            {
                DateTime startTime = Convert.ToDateTime(hTimeStart.Value);
                DateTime endTime = Convert.ToDateTime(hTimeEnd.Value);

                DateTimeDiff = System.Math.Abs((endTime - startTime).TotalMilliseconds);
                
                hTimeStart.Value = "";
                hTimeEnd.Value = "";
            }


            if (DateTimeDiff.ToString() == "0")
            {
                lblScanPalletStatus.Text = "SCANNED";
            }
            else
            {
                lblScanPalletStatus.Text = "MANUAL";
            }
           palletClick();
        }
        public void palletClick()
        {
            lblStatus.Text = "";
            DataView dv = new DataView();
            dvBG.Attributes.Add("style", "background-color:white; border-color:Black;border-style:solid; border-width:thin; height: 390px");

            dv = maint.CHECK_IF_PALLET_EXISTS(txtPalletNo.Text.Trim()).Tables[0].DefaultView;
            string strPalletCount = "";
            strPalletCount = dv[0][0].ToString();

            if (strPalletCount == "0")
            {
                //MsgBox.alert("Pallet (" + txtPalletNo.Text.Trim() + ") does not exists!");
                lblStatus.Text = "Pallet (" + txtPalletNo.Text.Trim() + ") does not exists!";
                lblStatus.ForeColor = Color.White;
                txtPalletNo.Text = "";
                dvBG.Attributes.Clear();
                dvBG.Attributes.Add("style", "background-color:red; border-color:Black;border-style:solid; border-width:thin; height: 390px");
                lblScanPalletStatus.Text = "";
                txtPalletNo.Focus();
                return;
            }


            if (txtPalletNo.Text.Trim() == "")
            {
                lblStatus.Text = "Please scan pallet number!";
                lblStatus.ForeColor = Color.White;
                //MsgBox.alert("Please scan pallet number!");
                dvBG.Attributes.Add("style", "background-color:red; border-color:Black;border-style:solid; border-width:thin; height: 390px");
                dvBG.Attributes.Clear();
                lblScanPalletStatus.Text = "";
                txtPalletNo.Focus();
                return;
            }
            else
            {
                txtPalletNo.Text = txtPalletNo.Text.ToUpper();
                txtContainerNo.Focus();
            }
        }

        protected void btnContainer_Click(object sender, EventArgs e)
        {

            DataView dv = new DataView();
            lblStatus.Text = "";
            dvBG.Attributes.Add("style", "background-color:white; border-color:Black;border-style:solid; border-width:thin; height: 390px");

            dv = maint.CHECK_IF_CONTAINER_EXISTS(txtContainerNo.Text.Trim()).Tables[0].DefaultView;
            string strContainer = "";
            strContainer = dv[0][0].ToString();
            if (strContainer == "0")
            {

                lblStatus.Text = "Container (" + txtContainerNo.Text.Trim() + ") does not exists!";
                //MsgBox.alert("Container (" + txtContainerNo.Text.Trim() + ") does not exists!");
                txtContainerNo.Text = "";
                dvBG.Attributes.Clear();
                lblStatus.ForeColor = Color.White;
                dvBG.Attributes.Add("style", "background-color:red; border-color:Black;border-style:solid; border-width:thin; height: 390px");
                txtContainerNo.Focus();
                return;
            }



            if (lblUser.Text == "")
            {
                MsgBox.alert("Please log-in again!");
                Response.Redirect("~/HTLoginPalletShipmentValidation.aspx");
                return;
            }
            if (txtPalletNo.Text.Trim() == "")
            {
                lblStatus.Text = "Please scan pallet number!";
                //MsgBox.alert("Please scan pallet number!");
                dvBG.Attributes.Clear();
                dvBG.Attributes.Add("style", "background-color:red; border-color:Black;border-style:solid; border-width:thin; height: 390px");
                lblStatus.ForeColor = Color.White;
                txtPalletNo.Focus();
                return;
            }
            else if (txtContainerNo.Text.Trim() == "")
            {
                dvBG.Attributes.Clear();
                dvBG.Attributes.Add("style", "background-color:red; border-color:Black;border-style:solid; border-width:thin; height: 390px");
                txtContainerNo.Focus();
                lblStatus.ForeColor = Color.White;
                lblStatus.Text = "Please scan container!";
                //MsgBox.alert("Please scan container!");
                return;
            }
            else
            {
                double DateTimeDiff = 0;
                if (hTimeStart.Value != "" && hTimeEnd.Value != "")
                {
                    DateTime startTime = Convert.ToDateTime(hTimeStart.Value);
                    DateTime endTime = Convert.ToDateTime(hTimeEnd.Value);

                    DateTimeDiff = System.Math.Abs((endTime - startTime).TotalMilliseconds);

                    hTimeStart.Value = "";
                    hTimeEnd.Value = "";
                }

                txtContainerNo.Text = txtContainerNo.Text.ToUpper();
                txtPalletNo.Text = txtPalletNo.Text.ToUpper();
                string strContainerNo = checkValidContainerNo(txtContainerNo.Text.Trim());

                if (strContainerNo == "")
                {
                    dvBG.Attributes.Clear();
                    dvBG.Attributes.Add("style", "background-color:red; border-color:Black;border-style:solid; border-width:thin; height: 390px");
                    txtContainerNo.Text = "";
                    txtContainerNo.Focus();
                    lblStatus.ForeColor = Color.White;
                    lblStatus.Text = "Please enter valid Container Number!";
                    return;
                }


                if (DateTimeDiff.ToString() == "0")
                {
                    lblScanContainerStatus.Text = "SCANNED";
                }
                else
                {
                    lblScanContainerStatus.Text = "MANUAL";
                }

                if (lblScanContainerStatus.Text == "SCANNED" && lblScanPalletStatus.Text == "SCANNED")
                {

                    try
                    {

                        

                            maint.ADD_PALLET_SHIPMENT_VALIDATION(strContainerNo.Trim(), txtPalletNo.Text.Trim(), lblUser.Text);

                        txtPalletNo.Text = "";
                        txtContainerNo.Text = "";
                        txtPalletNo.Focus();
                        dv = maint.GET_PALLET_SHIPMENT_VALIDATION_SCANNED_COUNT(strContainerNo.Trim()).Tables[0].DefaultView;
                        if (dv.Count > 0)
                        {
                            lblPalletScanned.Text = dv[0]["PALLETCOUNT"].ToString();
                            lblContainerNo.Text = dv[0]["CONTAINERNO"].ToString();
                        }

                        lblStatus.ForeColor = Color.DarkGreen;
                        lblScanPalletStatus.Text = "";
                        lblScanContainerStatus.Text = "";
                        lblStatus.Text = "Successfully saved!";
                        //MsgBox.alert("Successfully saved!");
                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = ex.Message.ToString();
                        //MsgBox.alert(ex.Message.ToString());
                        dvBG.Attributes.Clear();
                        dvBG.Attributes.Add("style", "background-color:red; border-color:Black;border-style:solid; border-width:thin; height: 390px");
                        lblStatus.ForeColor = Color.White;
                        txtPalletNo.Text = "";
                        txtContainerNo.Text = "";
                    }
                }
                else
                {
                    mp.Show();
                }
            }
            

        }

        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HTLoginPalletShipmentValidation.aspx");
        }

        protected void mp_Load(object sender, EventArgs e)
        {
            //AjaxControlToolkit.Utility.SetFocusOnLoad(txtcomments);
            txtUsername.Focus();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Boolean isLogin = false;

            if (txtUsername.Text == "")
            {
                lblLogStat.Text = "Please enter your username.";
                mp.Show();
                return;
            }

            if (txtPassword.Text == "")
            {
                lblLogStat.Text = "Please enter your password.";
                mp.Show();
                return;
            }

            try
            {
                string strSystemName = "FGWHSE Monitoring";
                Maintenance maint = new Maintenance();
                DataView dvUser = new DataView();

                //validate username and password in AD
                bool isAuthenticated = ServiceLocator.GetLdapService().IsAuthenticated(txtUsername.Text, txtPassword.Text);
                if (isAuthenticated)
                {
                    DsUser ds = ServiceLocator.GetLoginService().GetUser(txtUsername.Text);
                    if (ds.SystemUser.Rows.Count <= 0)
                    {
                        lblLogStat.Text = "User profile is incomplete! " +
                            "Please contact the administrator " +
                            "to check if user has all information required. ";
                        txtPassword.Focus();
                        return;
                    }


                    dvUser = maint.GetUsersLDAP(txtUsername.Text, strSystemName);
                    if (dvUser.Count > 0)
                    {
                        Session["UserID"] = Convert.ToString(dvUser[0]["UserID"]);
                        Session["UserName"] = Convert.ToString(dvUser[0]["UserName"]);
                        Session["RoleID"] = Convert.ToString(dvUser[0]["Role"]);
                        GetUserSubsystems();
                        isLogin = true;
                    }
                    else
                    {
                        lblLogStat.Text = "Invalid login! You have no access rights in the system. Please contact system administrator.";
                        txtPassword.Focus();
                        mp.Show();
                        return;
                    }

                }
                else
                {
                    dvUser = maint.GetUser(strSystemName, txtUsername.Text.Trim(), txtPassword.Text.Trim(), 0);
                    if (dvUser.Count > 0)
                    {
                        Session["UserID"] = Convert.ToString(dvUser[0]["User_ID"]);
                        Session["UserName"] = Convert.ToString(dvUser[0]["User_Name"]);
                        Session["RoleID"] = Convert.ToString(dvUser[0]["Role"]);
                        GetUserSubsystems();
                        isLogin = true;
                    }
                    else
                    {
                        lblLogStat.Text = "Invalid login!";
                        txtPassword.Focus();
                        mp.Show();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                lblLogStat.Text = ex.Message.ToString();
                mp.Show();
                return;
            }
            finally
            {
                if (isLogin)
                {
                    if (!checkAuthority(strPageSubsystem))
                    {
                        if (!checkAuthority("FGWHSE_033"))
                        {
                            lblLogStat.Text = "User is not an approver!";
                            mp.Show();
                        }
                        else
                        {


                            lblLogStat.Text = "";

                            try
                            {

                                string strContainerNo = checkValidContainerNo(txtContainerNo.Text.Trim());

                                DataView dv = new DataView();
                                maint.ADD_PALLET_SHIPMENT_VALIDATION_WITH_APPROVAL(strContainerNo.Trim(), txtPalletNo.Text.Trim(), lblUser.Text, txtUsername.Text);

                                txtPalletNo.Text = "";
                                txtContainerNo.Text = "";
                                txtPalletNo.Focus();
                                dv = maint.GET_PALLET_SHIPMENT_VALIDATION_SCANNED_COUNT(strContainerNo.Trim()).Tables[0].DefaultView;
                                if (dv.Count > 0)
                                {
                                    lblPalletScanned.Text = dv[0]["PALLETCOUNT"].ToString();
                                    lblContainerNo.Text = dv[0]["CONTAINERNO"].ToString();
                                }

                                txtUsername.Text = "";
                                txtPassword.Text = "";
                                lblStatus.ForeColor = Color.DarkGreen;
                                lblScanPalletStatus.Text = "";
                                lblScanContainerStatus.Text = "";
                                lblStatus.Text = "Successfully saved!";
                                //MsgBox.alert("Successfully saved!");
                            }
                            catch (Exception ex)
                            {
                                lblStatus.Text = ex.Message.ToString();
                                //MsgBox.alert(ex.Message.ToString());
                                dvBG.Attributes.Clear();
                                dvBG.Attributes.Add("style", "background-color:red; border-color:Black;border-style:solid; border-width:thin; height: 390px");
                                lblStatus.ForeColor = Color.White;
                                txtPalletNo.Text = "";
                                txtContainerNo.Text = "";
                                mp.Show();
                            }
                        }
                    }
                    //Response.Redirect("~/Form/HTPalletShipmentValidation.aspx?userid=" + txtUsername.Text.Trim());
                }
            }
        }



        private void GetUserSubsystems()
        {
            try
            {
                Maintenance maint = new Maintenance();
                string strSystemName = "FGWHSE Monitoring";

                DataView dv = new DataView();
                dv = maint.GetUsersSubsystems(Session["UserID"].ToString(), strSystemName);

                Session["Subsystem"] = dv;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                //MsgBox1.alert(ex.Message.ToString());
            }
        }


    }
}
