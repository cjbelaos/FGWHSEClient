using com.eppi.utils;
using FGWHSEClient.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OleDb;
using System.IO;
using System.Collections.Generic;

using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using FGWHSEClient.LogInService;
using System.Threading;

namespace FGWHSEClient.Form
{
    public partial class DNMonitoringWithExcess : System.Web.UI.Page
    {
        Maintenance maint = new Maintenance();
        public ExcessDAL drExcess = new ExcessDAL();
        public IDTapDAL IDTap = new IDTapDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                txtUserID.Attributes.Add("onkeydown", "return (event.keyCode!=13);false;");
                txtPassword.Attributes.Add("onkeydown", "return (event.keyCode!=13);false;");
                FillLoadingDock();
                getExcess("YES");
                

            }

            

        }

        private void FillLoadingDock()
        {
            DataTable dtLoadingDock = new DataTable();
            dtLoadingDock = drExcess.DN_GETLOADINGDOCK_EXCESS().Tables[0];

            DataTable dtDock = new DataTable();


            dtDock.Columns.Add("location_id", typeof(string));
            dtDock.Columns.Add("location_name", typeof(string));


            dtDock.Rows.Add("", "--Please Select--");
            foreach (DataRow row in dtLoadingDock.Rows)
            {

                String locID = (string)row[0];
                String locNAME = (string)row[1];
                dtDock.Rows.Add(locID, locNAME);
            }
           
            ddlLoadingDock.DataSource = dtDock;
            ddlLoadingDock.DataTextField = "location_name";
            ddlLoadingDock.DataValueField = "location_id";
            ddlLoadingDock.DataBind();
            fillDropDownColor(ddlLoadingDock, dtLoadingDock.DefaultView);

        }


        public void fillDropDownColor(DropDownList ddl, DataView dv)
        {
            string strSelectedValue = ddl.SelectedValue.ToString();
            foreach (ListItem li in ddl.Items)
            {
                for(int x=0;x<dv.Count;x++) 
                {
                    if (li.Value.ToString() == dv[x]["location_id"].ToString())
                    {
                        li.Attributes.Add("style", "background-color:"+ dv[x]["bg_color"].ToString() + ";");
                        li.Text = dv[x]["LOCATION_NAME"].ToString();
                        li.Value = dv[x]["LOCATION_ID"].ToString();
                    }
                }
                
                    
            }

            ddl.SelectedValue = strSelectedValue;

        }
        public void getExcess(string isRefresh)
        {
            try
            {
                string strLocationID = ddlLoadingDock.SelectedValue.ToString();
                lblRefreshTime.Text = "Last Refreshed Time : " + DateTime.Now.ToString();
                DataSet dsExcess = new DataSet();
                dsExcess = drExcess.GET_EXCESS_MONITORING_DETAILS(strLocationID);
                DataView dvCount = dsExcess.Tables[1].DefaultView;
                DataView dvRead = new DataView();
                lblActualCount.Text = "0";
                lblOverAllCount.Text = "0";
                lblRFIDExcessCount.Text = "0";
                lblNoMaintenance.Text = "0";
               
                //tdNoDN.Attributes.Add("style", "font-size:xx-large;padding:5px");
                if (dvCount.Count > 0)
                {
                    lblActualCount.Text = dvCount[0]["TAGCOUNT"].ToString();
                    lblRFIDExcessCount.Text = dvCount[0]["EXCESSCOUNT"].ToString();
                    lblNoMaintenance.Text = dvCount[0]["NOMAINTENANCECOUNT"].ToString();
                    lblOverAllCount.Text = dvCount[0]["OVERALLCOUNT"].ToString();
                    lblForInspection.Text = dvCount[0]["FORINSPECTIONCOUNT"].ToString();
                    //tdNoDN.Attributes.Add("style", "font-size:xx-large;padding:5px;" + dvCount[0]["BGCOLOR"].ToString());
                }
                populateTable(dsExcess.Tables[0], tbRFIDList, 1, "tableDetailContainer", "TableHeadContainerRFID");

                DataTable dt = dsExcess.Tables[2];
                populateTable(dt, tbDNList, 7, "tableDetailContainerRFID", "TableHeadContainerRFID");

                populateLegend(dsExcess.Tables[3].DefaultView);
                fillDropDownColor(ddlLoadingDock, drExcess.DN_GETLOADINGDOCK_EXCESS().Tables[0].DefaultView);

                dvRead = drExcess.GET_EXCESS_LOCATION_READ_STATUS(strLocationID).Tables[0].DefaultView;
                if(dvRead.Count > 0)
                {
                    txtRead.Text = dvRead[0]["READFLAG"].ToString();
                }

                populatePopup(dsExcess, isRefresh);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        public void populateLegend(DataView dv)
        {
            HtmlTableRow trLegend = new HtmlTableRow();
            tbLegend.Controls.Add(trLegend);
            for ( int x = 0;x< dv.Count; x++)
            {
               

                string strColorDesc = dv[x]["colordesc"].ToString(), strColor = dv[x]["font_color"].ToString();
                HtmlTableCell tdColor = new HtmlTableCell();
                tdColor.Attributes.Add("style", "border-style:solid;border-width:1px;border-color;black;background-color:" + strColor);
                trLegend.Controls.Add(tdColor);

                
                Label lblText = new Label();
                lblText.Text = "color";
                lblText.ForeColor = Color.Transparent;
                lblText.Font.Size = FontUnit.XXSmall;
                tdColor.Controls.Add(lblText);

                HtmlTableCell tdColorName = new HtmlTableCell();
                trLegend.Controls.Add(tdColorName);

                
                tdColorName.Attributes.Add("style", "font-size;x-small");
                Label lblColorName = new Label();
                lblColorName.Text = strColorDesc;
                lblColorName.Font.Size = FontUnit.XXSmall;
                tdColorName.Controls.Add(lblColorName);

                if(strColorDesc == "EXCESS")
                {
                    lblRFIDExcessCount.Attributes.Add("style", "color:" + strColor);
                }

                if (strColorDesc == "INSPECTION")
                {
                    lblForInspection.Attributes.Add("style", "color:" + strColor);
                }
                if (strColorDesc == "NOT IN MASTER")
                {
                    lblNoMaintenance.Attributes.Add("style", "color:" + strColor);
                }

                if (strColorDesc == "GOOD")
                {
                    lblActualCount.Attributes.Add("style", "color:" + strColor);
                }


                //if (lblActualCount.Text != lblOverAllCount.Text)
                //{
                //    lblOverAllCount.ForeColor = lblRFIDExcessCount.ForeColor;
                //}
                //else
                //{
                //    lblOverAllCount.ForeColor = lblActualCount.ForeColor;
                //}


                HtmlTableCell tdSpace = new HtmlTableCell();
                trLegend.Controls.Add(tdSpace);
                Label lblSpace = new Label();
                lblSpace.Text = "SP";
                lblSpace.ForeColor = Color.Transparent;
                lblSpace.Font.Size = FontUnit.XXSmall;
                tdSpace.Controls.Add(lblSpace);
            }
        }
      

        public void populateTable(DataTable dt, HtmlTable tbTable, int intActualColumnDisplayCount, string strCssClass, string strCssClassHeader)
        {
            int intRowCountMax = dt.Rows.Count, intColCountMax = dt.Columns.Count;
            string strColor = "", strText = "";
            DataView dv = dt.DefaultView;
            if(dv.Count> 0)
            {
                for (int intRowCount = 0; intRowCount < intRowCountMax; intRowCount++)
                {
                    
                        HtmlTableRow trRow = new HtmlTableRow();
                        tbTable.Controls.Add(trRow);
                    for (int intColCount = 0; intColCount < intColCountMax; intColCount++)
                    {
                        if (intColCount < intActualColumnDisplayCount)
                        {
                            HtmlTableCell tdCol = new HtmlTableCell();
                            trRow.Controls.Add(tdCol);
                            
                            if (dv[intRowCount]["ROWTYPE"].ToString() == "HEADER")
                            {
                                tdCol.Attributes.Add("class", strCssClassHeader);
                            }
                            else
                            {
                                tdCol.Attributes.Add("class", strCssClass);
                                tdCol.Attributes.Add("style", "background-color:" + dv[intRowCount]["BGCOLOR"].ToString());
                            }


                            strColor = dv[intRowCount]["color"].ToString();
                            strText = dv[intRowCount][intColCount].ToString();

                            if (intRowCount != 0 && intColCount == 0 && strText.Substring(0,1).ToUpper() == "R")
                            {
                                LinkButton lnk = new LinkButton();
                                lnk.Text = strText;

                                lnk.OnClientClick = HttpUtility.HtmlDecode("detailPage('"+ strText + "');return false;");
                                
                                tdCol.Controls.Add(lnk);
                            }
                            else
                            {
                                Label lblText = new Label();
                                lblText.Attributes.Add("style", "max-width:700px;color:" + strColor);

                                lblText.Text = strText;
                                if (strText == "")
                                {
                                    lblText.Height = Unit.Pixel(18);
                                }
                                tdCol.Controls.Add(lblText);
                            }
                        }
                    }
                   
                }
               
            }
            
        }

        public void populatePopup(DataSet  ds, string isRefresh)
        {
            DataView dvExcess = ds.Tables[4].DefaultView;
            DataView dvInspection = ds.Tables[5].DefaultView;
            DataView dvNoMaintenance = ds.Tables[6].DefaultView;

            if(dvExcess.Count > 0 || dvInspection.Count > 0 || dvNoMaintenance.Count > 0 )
            {

                if (isRefresh == "YES")
                {
                    txtIsClicked.Text = "0";
                }
                txtUserID.Focus();
                HtmlTable tb = new HtmlTable();
                tdErrorList.Controls.Add(tb);

               

                createListPopup(dvExcess, tb);
                createListPopup(dvInspection, tb);
                createListPopup(dvNoMaintenance, tb);
            }
        }

        public void createListPopup(DataView dv, HtmlTable tbhEAD)
        {
            if (dv.Count > 1)
            {
                HtmlTableRow tr = new HtmlTableRow();
                tbhEAD.Controls.Add(tr);

                HtmlTableCell td = new HtmlTableCell();
                tr.Controls.Add(td);

                System.Web.UI.HtmlControls.HtmlGenericControl createDivHead =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                createDivHead.Attributes.Add("class", "tablePOPHeader");
                td.Controls.Add(createDivHead);

                Label lblHeader = new Label();
                lblHeader.Font.Size = FontUnit.Medium;
                lblHeader.ForeColor = Color.Black;
                lblHeader.Text = dv[0]["RFIDTAG"].ToString();
                createDivHead.Controls.Add(lblHeader);

                System.Web.UI.HtmlControls.HtmlGenericControl createDiv =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                createDiv.Attributes.Add("class", "tablePOPDetail");
                td.Controls.Add(createDiv);

                HtmlTable tb = new HtmlTable();
                createDiv.Controls.Add(tb);

                for (int x = 1; x< dv.Count;x++)
                {
                    HtmlTableRow trNew = new HtmlTableRow();
                    tb.Controls.Add(trNew);

                    HtmlTableCell tdNew = new HtmlTableCell();
                    trNew.Controls.Add(tdNew);

                    Label lblRFID = new Label();
                    lblRFID.Font.Size = FontUnit.Small;
                    lblRFID.Attributes.Add("style", "color:" + dv[x]["COLOR"].ToString());
                    lblRFID.Text = dv[x]["RFIDTAG"].ToString();
                    tdNew.Controls.Add(lblRFID);
                }

                System.Web.UI.HtmlControls.HtmlGenericControl createDivSpace =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                createDivSpace.Attributes.Add("style", "height:10px");
                td.Controls.Add(createDivSpace);


            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            logInToEnableAntenna();
            getExcess("NO");

        }


        public void logInToEnableAntenna()
        {
            if(txtUserID.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                MsgBox1.alert("Please input username/password!");
                txtIsClicked.Text = "1";
                return;
            }
            bool isValid = false;
            bool isLogin = false;
            DataView dvUser = new DataView();
            string strSystemName = "FGWHSE Monitoring";
            bool isAuthenticated = ServiceLocator.GetLdapService().IsAuthenticated(txtUserID.Text, txtPassword.Text);
            if (isAuthenticated)
            {
                DsUser ds = ServiceLocator.GetLoginService().GetUser(txtUserID.Text);
                if (ds.SystemUser.Rows.Count <= 0)
                {
                    MsgBox1.alert("User profile is incomplete! " +
                        "Please contact the administrator " +
                        "to check if user has all information required. " );
                }
                dvUser = maint.GetUsersLDAP(txtUserID.Text, strSystemName);
                if (dvUser.Count > 0)
                {
                    isLogin = true;
                }
                else
                {
                    MsgBox1.alert( "Invalid username/password!");
                    isLogin = false;
                }

            }
            else
            {
                dvUser = maint.GetUser(strSystemName, txtUserID.Text.Trim(), txtPassword.Text.Trim(), 0);
                if (dvUser.Count > 0)
                {
                    isLogin = true;
                }
                else
                {
                    MsgBox1.alert("Invalid username/password!");
                    isLogin = false;
                }
            }

            if(isLogin == true)
            {
                isValid = checkAuthority("FGWHSE_057",GetUserSubsystems());
                if (isValid == true)
                {
                    drExcess.UPDATE_ANTENNAREAD_FLAG(ddlLoadingDock.SelectedValue.ToString(), "1", txtUserID.Text);
                    //MsgBox1.alert("Log-in successful!");
                    txtUserID.Text = "";
                    txtPassword.Text = "";
                    txtRead.Text = "1";
                    txtIsClicked.Text = "2";
                }
                else
                {
                    MsgBox1.alert("You are not authorized for this log-in!");
                    txtIsClicked.Text = "1";

                }
            }


        }



        private DataView GetUserSubsystems()
        {
            DataView dv = new DataView();
            try
            {
                Maintenance maint = new Maintenance();
                string strSystemName = "FGWHSE Monitoring";


                dv = maint.GetUsersSubsystems(txtUserID.Text, strSystemName);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message.ToString());
           
            }
            finally
            {
                
            }
            return dv;
        }



        private bool checkAuthority(string strPageSubsystem, DataView dvSubsystem)
        {
            bool isValid = false;
            try
            {
                if (dvSubsystem.Count > 0)
                {
                   

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

                    }
                }
                return isValid;
            }
            catch (Exception ex)
            {
            
                isValid = false;
                return isValid;
            }
        }

        protected void btnGetValue_Click(object sender, EventArgs e)
        {
            getExcess("NO");
            if(txtIsClicked.Text == "0")
            {
                txtIsClicked.Text = "1";
            }

        }

        

        protected void btnRefresh_Click(object sender, EventArgs e)
        {

            getExcess("YES");
        }


        protected void lnk_Click(object sender, EventArgs e)
        {
            getExcess("YES");
            MsgBox1.alert("TEST 1");
            //string strURL = "DNReceivingMonitoringScreen.aspx?DNNo=" + VendorCode;

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + strURL + "','_newtab');", true);
        }

        protected void btnLogInTap_Click(object sender, EventArgs e)
        {
            logInTapToEnableAntenna();
        }


        public void logInTapToEnableAntenna()
        {
            string strUID = "",strEmpNo = "";


            bool isValid = false;
            DataView dvUser = new DataView();
            dvUser = IDTap.GET_ID_TAP(txtUserID.Text).DefaultView;
            if(dvUser.Count > 0)
            {
                strUID = dvUser[0]["APOAccount"].ToString();
                strEmpNo = dvUser[0]["EmployeeNo"].ToString();
            }
           
             isValid = checkAuthority("FGWHSE_057", GetUserSubsystemsTap(strUID));
            if (isValid == true)
            {
                drExcess.UPDATE_ANTENNAREAD_FLAG(ddlLoadingDock.SelectedValue.ToString(), "1", strUID);
                MsgBox1.alert("Log-in successful!");
                txtRead.Text = "1";
                txtIsClicked.Text = "2";
                txtUserID.Text = "";
                txtPassword.Text = "";
            }
            else
            {
                isValid = checkAuthority("FGWHSE_057", GetUserSubsystemsTap(strEmpNo));
                if (isValid == true)
                {
                    drExcess.UPDATE_ANTENNAREAD_FLAG(ddlLoadingDock.SelectedValue.ToString(), "1", strEmpNo);
                    MsgBox1.alert("Log-in successful!");
                    txtRead.Text = "1";
                    txtIsClicked.Text = "2";
                    txtUserID.Text = "";
                    txtPassword.Text = "";
                }
                else
                {
                    MsgBox1.alert("You are not authorized for this log-in!");
                    txtIsClicked.Text = "1";
                }

            }
        }

        private DataView GetUserSubsystemsTap(string strUID)
        {
            DataView dv = new DataView();
            try
            {
                Maintenance maint = new Maintenance();
                string strSystemName = "FGWHSE Monitoring";


                dv = maint.GetUsersSubsystems(strUID, strSystemName);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message.ToString());

            }
            finally
            {

            }
            return dv;
        }
    }
}