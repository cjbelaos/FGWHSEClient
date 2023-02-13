<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Form/MasterPalletMonitoring.Master" CodeBehind="DNMonitoringWithExcess.aspx.cs" Inherits="FGWHSEClient.Form.DNMonitoringWithExcess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link href="../Style.css" rel="stylesheet" />
    
    <div style="margin-left: 5px;">
        <table>
            <tr>
                <td></td>
            </tr>
        </table>
    </div>

    <div style="margin-left: 5px">
        <table>
            <tr>
                <td>
                    <table cellspacing="0">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>Loading Dock: &nbsp; </td>
                                        <td>
                                            <asp:DropDownList ID="ddlLoadingDock" runat="server" Width="250px" AutoPostBack="True"></asp:DropDownList></td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblRefreshTime" runat="server" Text=""></asp:Label></td>

                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td>
                                <table id="tbLegend" runat="server">
                                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">

                                <!-- RFID DN READ---->
                                <div  class="divDNExcessContainer">
                                    <table id="tbDNList" runat="server">
                                    </table>
                                </div>
                                <!-- RFID DN READ END---->
                                <table>
                                    <tr>
                                        <td style="font-size:30px"><center>GOOD RFID COUNT</center><br /></td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td style="font-size:27px"><center>OVERALL RFID COUNT</center><br /></td>
                                    </tr>

                                    <tr>
                                        <td valign="middle">
                                            <div style="height: 140px;width:325px; background-color: lightgray">
                                                <center>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblActualCount" runat="server" Text="0" Font-Size="170px"></asp:Label>

                                                </center>
                                            </div>
                                        </td>
                                        <td></td>
                                        <td valign="middle">
                                            <div style="height: 140px;width:325px; background-color: lightgray">
                                                <center>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblOverAllCount" runat="server" Text="0" Font-Size="170px"> </asp:Label>
                                                </center>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                             <td>&nbsp;&nbsp;&nbsp;</td>
                            <td valign="top" rowspan="3">
                                <div style="width: 470px;">
                                    <table border="1">
                                        <tr>
                                            
                                            <td class="TableHeadContainer"><span style="color: white">EXCESS (NO DN)</span></td>
                                            <td class="TableHeadContainer"><span style="color: white; font-size: xx-small">NOT IN MASTER</span></td>
                                            <td class="TableHeadContainer""><span style="color: white;">INSPECTION</span></td>
                                        </tr>
                                        <tr>

                                            <td style="padding: 5px; text-align: center">
                                                <asp:Label ID="lblRFIDExcessCount" Font-Size="X-Large" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td style="padding: 5px; text-align: center">
                                                <asp:Label ID="lblNoMaintenance" Font-Size="X-Large" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td style="padding: 5px; text-align: center">
                                                <asp:Label ID="lblForInspection" Font-Size="X-Large" runat="server" Text="0"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />


                                    <div style="padding-left: 5px; height: 350px; width: 320px; overflow: auto">
                                        <table id="tbRFIDList" runat="server">
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>

                    </table>

                </td>
            </tr>
        </table>

    </div>


    <div class="modal fade" id="myModaL" data-keyboard="false" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
   <div class="modal-dialog">
        <div class="modal-content">
             <div class="modal-header">
                <div style ="text-align:left">
                   <asp:Panel ID="pnLogin" runat="server" CssClass="modalPopup" align="center" style="display:compact" Width="400px" BackColor="#EAE9EB" Height="500px">
                      <div style="background-color:tomato; height:100%">
                       <table>
                          <tr>
                              <td>
                                  <br />
                                  <div style="text-align:center; width:100%">
                                      <asp:Label ID="Label1" runat="server" ForeColor="White" Font-Bold ="True" Font-Size="Large" Text="Antenna reading has stopped!"></asp:Label><br />
                                      <asp:Label ID="Label2" runat="server" ForeColor="White" Font-Bold ="True" Font-Size="Large" Text="Please log-in!"></asp:Label>
                                  </div>
                                  <br />
                                  <table>
                                      <tr>
                                          <td>User ID:</td>
                                          <td>
                                              <asp:TextBox ID="txtUserID" runat="server" onkeyup="onEnterUsername(event)"></asp:TextBox>
                                          </td>
                                      </tr>
                                      <tr>
                                          <td>Password:</td>
                                          <td>
                                              <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                                          </td>
                                      </tr>
                                      <tr>
                                          <td></td>
                                          <td>
                                               <br />
                                              <asp:Button ID="btnLogin" runat="server" class="btn nk-indigo btn-info" Text="LOG IN" OnClick="btnLogin_Click" />&nbsp;
                                              <br />
                                              <br />
                                          </td>
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                           <tr>
                               <td id="tdErrorList" runat="server">

                               </td>
                           </tr>
                           

                      </table>
                        
                      </div>
                       
                </asp:Panel>
                </div>
            </div>
       </div>
   </div>
</div>
<div id="dvHide" runat ="server" style="display:none">
        <asp:Button ID="btnRefresh" runat="server" Text="Button" OnClick="btnRefresh_Click" />
        <asp:Button ID="btnGetValue" runat="server" Text="Button" OnClick="btnGetValue_Click" />
        <asp:TextBox ID="txtRead" runat="server"  Text="1"></asp:TextBox>
        <asp:TextBox ID="txtIsClicked" runat="server"  Text="0"></asp:TextBox>

        <asp:Button ID="btnLogInTap" runat="server" Text="" OnClick="btnLogInTap_Click" />
        
        <button type="button" class="btn nk-indigo btn-info" data-toggle="modal" data-target="#myModaL" id="btnModal"  onclick ="UIDFocus();" runat ="server">Indigo</button>
</div>
 
    <script type="text/javascript">

        var TIMER_INTERVAL = 2000;
        setTimeout(function () { refreshPage() }, TIMER_INTERVAL);
        function refreshPage() {
            var displayButton = document.getElementById("ctl00_ContentPlaceHolder1_btnRefresh");
            var displayValue = document.getElementById("ctl00_ContentPlaceHolder1_btnGetValue");
            var check = document.getElementById("ctl00_ContentPlaceHolder1_ddlLoadingDock").value;
            var goRead = document.getElementById("ctl00_ContentPlaceHolder1_txtRead").value;
            var isClicked = document.getElementById("ctl00_ContentPlaceHolder1_txtIsClicked").value;
            var txtUID = document.getElementById("ctl00_ContentPlaceHolder1_txtUserID");
            var txtPass = document.getElementById("ctl00_ContentPlaceHolder1_txtPassword");

            if (check != "") {
                if (goRead != "0") {
                    displayButton.click();
                    
                }
                else {
                    
                    if (isClicked == "0") {
                        document.getElementById("ctl00_ContentPlaceHolder1_txtIsClicked").value = "1.5"
                        displayValue.click();
                        //document.getElementById('ctl00_ContentPlaceHolder1_btnModal').click();
                        TIMER_INTERVAL = 300;
                    }
                    else {
                        //alert(isClicked);
                        if (isClicked == "1.5")
                        {
                            document.getElementById("ctl00_ContentPlaceHolder1_txtIsClicked").value = "1";
                        }
                        else if (isClicked == "1") {

                            document.getElementById("ctl00_ContentPlaceHolder1_txtIsClicked").value = "2";
                            //displayValue.click();
                            document.getElementById('ctl00_ContentPlaceHolder1_btnModal').click();
                            //document.getElementById("ctl00_ContentPlaceHolder1_txtPassword").focus();

                        }
                        else if (isClicked == "2")
                        {
                            if (txtUID.value.length == 0)
                            {
                                txtUID.focus();
                            }
                            else
                            {
                                txtPass.focus();
                            }
                            document.getElementById("ctl00_ContentPlaceHolder1_txtIsClicked").value = "3";

                            TIMER_INTERVAL = 2000;
                        }
                    }


                }

            }


            setTimeout(function () { refreshPage() }, TIMER_INTERVAL);
        }

       
        function onEnterUsername(e) {

            var strUID = document.all("ctl00_ContentPlaceHolder1_txtUserID").value;
            var btnLogInTap = document.getElementById("ctl00_ContentPlaceHolder1_btnLogInTap")
            if (e.keyCode == 13) {
             
                var numcheck = $.isNumeric(strUID);
             
                if ([...strUID].length == 10 && numcheck == true) {
                    btnLogInTap.click();
                }
                else {
                    document.all("ctl00_ContentPlaceHolder1_txtPassword").focus();
                }
                return false;
            }
        }

        function UIDFocus()
        {
            document.getElementById("ctl00_ContentPlaceHolder1_txtPassword").focus();
        }

        function detailPage(strURL) {
            window.open('DNReceivingMonitoringScreen.aspx?DNNo=' + strURL, '_newtab');
        }
    </script>
    <cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>
