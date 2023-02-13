<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Form/MasterPalletMonitoring.Master" CodeBehind="DNReceivingDetailsScreen.aspx.cs" Inherits="FGWHSEClient.Form.DNReceivingDetailsScreen" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">

//    var TIMER_INTERVAL = 1000;
//    setTimeout(refreshPage, TIMER_INTERVAL);




    function refreshPage() {
        //var e = document.getElementById('ctl00$ContentPlaceHolder1$ddlLoadingDock');
        //var strLoadingDock = e.options[e.selectedIndex].value;
        //window.location = "DNReceivingScreen.aspx?loadingdock=" + strLoadingDock;
        // window.location = "http://172.16.52.100/EWHS/Form/DNReceivingScreen.aspx?loadingdock=LD0101";
        var displayButton = document.getElementById("ctl00_ContentPlaceHolder1_btnDisplay");
       displayButton.click(); // this will trigger the click event
     //   setTimeout(refreshPage, TIMER_INTERVAL);
    }
</script>

<span style="font-size:12px;"><a href="DNReceivingExecuteScreen.aspx"><span style="text-decoration: none;border:0;"><img src="../Image/Back.png" style="border:0;vertical-align:middle;" />Back to Inquiry Screen</span></a></span>
<div style =" left:40px;top:110px;">
    <div style="margin-left:15px; float:left; font-family:Calibri; color:Black">
       <table >
            <tr style="font-size:18px">
                <td>
                    Details of DN No:
                </td>
                <td>
                    <asp:Label ID= "lblDNNo" runat="server" Width="200px"></asp:Label>
                </td>
                <td style="padding-left:20px; font-size:11px"  id="tdChangeDN">
                
                <asp:Label ID="lblImageChangeDN" runat="server" Text="<img width='15px' height='15px' src='../Image/page-refresh-icon.png'/>"></asp:Label>
                &nbsp;&nbsp; 
                    <asp:LinkButton ID="lnlbtnChangeDN" runat="server" Text="Change DN" 
                        onclick="lnlbtnChangeDN_Click"></asp:LinkButton>
                </td>
                <td style="padding-left:565px; font-size:11px" id="tdConfirmDelivery">
                <asp:Label ID="lblImageConfirm" runat="server" Text="<img width='15px' height='15px' src='../Image/page-tick-icon.png'/>"></asp:Label>
                    &nbsp;&nbsp;<asp:LinkButton 
                         ID="lnkbtnConfirmDelivery" runat="server" Text="Confirm Delivery" 
                         onclick="lnkbtnConfirmDelivery_Click"></asp:LinkButton>
                         <asp:Label ID ="lblConfirmedBy" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Invoice No:
                </td>
                <td>
                    <asp:label ID="lblInvoiceNo" runat="server"></asp:label>
                </td>
                <td style="padding-left:20px; font-size:11px"  id="td1">
                
                
                </td>
                <td style="padding-left:565px; font-size:11px; text-align:right;" id="td2">
                    <asp:Label ID="Label1" runat="server" Text="<img width='15px' height='15px' src='../Image/print.png'/>"></asp:Label>
                    &nbsp;&nbsp;<asp:LinkButton 
                         ID="lnkbtnPrint" runat="server" Text="Print" OnClientClick = "PrintDivContent();return false;"></asp:LinkButton>
                </td>
            </tr>
            <tr style="font-size:14px">
                <td>
                    Total RFID Tag Count:
                </td>
                <td>
                    <asp:label ID="lblTotalRFIDCount" runat="server"></asp:label>
                </td>
            </tr>
       </table>
      
       <table border=0 style="margin-top:10px; ">
           
            <tr>
                <td colspan="2" style="margin-top:10px">
                   <asp:Panel ID="pnl" runat="server" Width="1160px" >
                   
                    
                   </asp:Panel> 
                   <%--<asp:UpdatePanel ID="pnl" runat="server">
                    <ContentTemplate>
                    
                    </ContentTemplate>
                     <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnDisplay" EventName="Click" />
                    </Triggers>
                   </asp:UpdatePanel> --%>
                   <br />
                   <br />
                   
                </td>
            </tr>
       </table>
       
    </div>
    
     
</div>
 <div style="display:none;">
        <asp:Button ID="btnDisplay" runat="server" Text="Display" 
            onclick="btnDisplay_Click" /><asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HiddenField2" runat="server" />
    </div>
<br />
  <asp:Panel ID="pnlModalChangeDN" runat="server" ForeColor="Black" Height="200px" BorderColor="Black"  align="center"  
        style="display:none" DefaultButton="btnModalSave">
    
        
   <div style="width:400px; height:300px; border-width:1px; border-style:solid; background-color:#ebeded">     
    
    <table    cellspacing =0 style="font-family:Arial;text-align:left;width:400px ">
        <tr>
            <td colspan="2" align="center" style="padding-top:10px; padding-bottom:10px; background-color:#538DD5; color:White; ">
            CHANGE DN NO
            </td>
        </tr>
        <tr >
            <td style="padding-top:50px; padding-left:10px; font-size:12px">
            Current DN No:
            </td>
            <td style="padding-top:50px; ">
             <asp:Label ID="lblModalDNNo" Font-Size="15px"   runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
             <td style="font-size:12px;padding-left:10px;">
            New DN No:
            </td>
            <td>
            <asp:TextBox  ID="txtModalNewDNNo" Font-Size="15px" runat="server" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <%--<tr>
             <td style="font-size:12px;padding-left:10px;">
            Person in Charge:
            </td>
            <td>
           <asp:TextBox  ID="txtModalPersonIC" Font-Size="15px"  runat="server" Width="250px"></asp:TextBox>
            </td>
        </tr>--%>
        <tr>
             <td style="font-size:12px;padding-left:10px">
           Reason:
            </td>
            <td>
            <asp:TextBox  ID="txtModalReason" runat="server" Font-Size="15px"  Width="250px" TextMode="MultiLine" Height="50px"></asp:TextBox>
            </td>
        </tr>
        <tr>
             <td style="font-size:12px;padding-left:10px">
           Error Message:
            </td>
            <td>
            <asp:Label ID="lblError" runat="server" style="font-size:12px;" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            
            </td>
            <td style="padding-top:10px;padding-left:10px;">
                <asp:Button ID="btnModalSave" runat="server"  Text="Save" Width="100px" 
                    onclick="btnModalSave_Click"/> &nbsp &nbsp; 
                <asp:Button ID="btnModalCancel" runat="server"  Text="Cancel" Width="100px" 
                    onclick="btnModalCancel_Click"/>
            </td>
        </tr>
    </table>
    </div>
</asp:Panel>

<asp:Panel ID="pnlConfirmDelivery" runat="server" ForeColor="Black" Height="150px" BorderColor="Black"  align="center"  
        style="display:none">
        <div style="width:400px; height:250px; border-width:1px; border-style:solid; background-color:#ebeded">     
         <table    cellspacing =0 style="font-family:Arial;text-align:left;width:400px ">
            <tr>
               <td colspan="2" align="center" style="padding-top:10px; padding-bottom:10px; background-color:#538DD5; color:White; ">
                    CONFIRM DELIVERY
                </td>
            </tr>
            <tr>
                <td width="100px" style="padding-top:50px; padding-left:10px; font-size:15px">
                    DN No:
                </td>
                <td width="350px" style="padding-top:50px;font-size:15px">
                    <asp:Label ID="lblModalConfirmDelivery"  runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding-top:20px; text-align:center" >
                     <asp:Button ID="btnModalConfirm" runat="server"   
                         Text="Confirm and Complete Delivery" Width="200px" 
                         onclick="btnModalConfirm_Click" /> 
                </td>
            </tr>
            <tr>
                 <td colspan="2" style="padding-top:10px; text-align:center" >
                 <asp:Button ID="btnModalCancelDelivery" runat="server"  Text="Cancel" Width="200px" 
                         onclick="btnModalCancelDelivery_Click"/>
                 </td>
            </tr>
            
       </table>
       
       </div>
</asp:Panel>

 <asp:Panel ID="pnlApprove" runat="server" style="border:solid 1px gray; width:243px; display:none;" DefaultButton="btnApprove">
    <center>
        <table width="320px" style="background-color:White">
        <tr>
            <td colspan=2 style="color:White; text-align:center" bgcolor="#4F81BD">
                <asp:Label ID="Label2" runat="server" Text="CHANGE DN NO." 
                    Font-Size="14px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="10px"></td>
        </tr>
        <tr>
            <td style="margin-left:8px">
                <asp:Label ID="Label3" runat="server" Text="Username" ForeColor="Black"></asp:Label></td>
            <td style="text-align:left">
                <asp:TextBox ID="txtUsername" runat="server" Width="150px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="margin-left:8px">
                <asp:Label ID="Label4" runat="server" Text="Password:" ForeColor="Black"></asp:Label></td>
             <td style="text-align:left">
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td height="10px" colspan="2">
                <asp:Label ID="lblErrorMsg" runat="server" Font-Italic="True" 
                    Font-Size="10px" ForeColor="Red"></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align:center" colspan=2>
                <asp:Button ID="btnApprove" runat="server" Text="Approve" Width="80px" Height="30px" 
                    onclick="btnApprove_Click"/>
                <asp:Button ID="btnCancelApprove"
                    runat="server" Text="Cancel" Width="80px" Height="30px"  onclick="btnCancel_Click"/></td>
        </tr>
        <tr>
            <td height="10px" colspan="2"></td>
        </tr>
        </table>
   
        </center>
        </asp:Panel>
        
        <asp:Panel ID="pnlGVRFID" runat="server" style="border:solid 1px gray; width:243px; display:none;" DefaultButton="btnDelete">
    <center>
        <table width="320px" style="background-color:White">
        <tr>
            <td colspan=2 style="color:White; text-align:center" bgcolor="#4F81BD">
                <asp:Label ID="Label5" runat="server" Text="LIST OF NOT RECEIVED RFID" 
                    Font-Size="14px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="10px"></td>
        </tr>
        <tr>
            <td style="margin-left:8px">
              <div id="grdHeight" runat="server" style="overflow:auto; height:220px" >
                        <asp:GridView ID="grdDN" runat="server" CellPadding="4" ForeColor="#333333" 
                        BorderStyle="Solid"
                        AutoGenerateColumns="False"
                        Font-Size="12px" Font-Bold="false">
        
                    <Columns>
                        <asp:TemplateField HeaderText="Select" >
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkHeaderItem" runat="server"  AutoPostBack="true" oncheckedchanged="chkHeaderItem_CheckedChanged" Checked="false"/>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkItem" runat="server"/>
                    </ItemTemplate>
                    <HeaderStyle Width="60px" HorizontalAlign="Center" BorderStyle="Solid" BorderWidth="1px" BorderColor="LightGray" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" BorderStyle="Solid" BorderWidth="1px" BorderColor="LightGray" ></ItemStyle>
                </asp:TemplateField>

                    <asp:BoundField HeaderText="BARCODEDNNO"   DataField="BARCODEDNNO" >
                            <HeaderStyle Width="130px">
                              </HeaderStyle>
                         </asp:BoundField>	   
                    <asp:BoundField HeaderText="RFIDTAG"   DataField="RFIDTAG">
                            <HeaderStyle Width="200px">
                              </HeaderStyle>
                         </asp:BoundField>	   	   
                    </Columns>
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#4f81bd" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="White" Font-Size="12px" />
                </asp:GridView></div>
                </td>
        </tr>
        <tr>
            <td height="10px" colspan="2">
                <asp:Label ID="Label8" runat="server" Font-Italic="True" 
                    Font-Size="10px" ForeColor="Red"></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align:center" colspan=2>
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="80px" 
                    Height="30px" onclick="btnDelete_Click"/>
                <asp:Button ID="btnCancel2" runat="server" Text="Cancel" Width="80px" 
                    Height="30px" onclick="btnCancel2_Click"/></td>
        </tr>
        <tr>
            <td height="10px" colspan="2"></td>
        </tr>
        </table>
   
        </center>
        </asp:Panel>


<div style="display:none">
    <asp:Button ID="btnHidden" runat="server" Text="Hidden" Visible="true"/>
</div>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupChangeDN" runat="server"
            TargetControlID="btnHidden"
            PopupControlID="pnlModalChangeDN"
            CancelControlID="btnModalCancel"
            BackgroundCssClass="watermarked"
            X="430"
            Y="200"
            Drag="true"
            
            RepositionMode="None">
    </ajaxToolkit:ModalPopupExtender>
    
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupApprove" runat="server"
            TargetControlID="btnHidden"
            PopupControlID="pnlApprove"
            CancelControlID="btnCancelApprove"
            BackgroundCssClass="watermarked"
            X="430"
            Y="200"
            Drag="true"
            
            RepositionMode="None">
    </ajaxToolkit:ModalPopupExtender>
    
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupDelete" runat="server"
            TargetControlID="btnHidden"
            PopupControlID="pnlGVRFID"
            CancelControlID="btnHidden"
            BackgroundCssClass="watermarked"
            X="430"
            Y="200"
            Drag="true"
            
            RepositionMode="None">
    </ajaxToolkit:ModalPopupExtender>
    
    <div style="display:none">
    <asp:Button ID="btnHidden2" runat="server" Text="Hidden" Visible="true"/>
</div>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupConfirmDN" runat="server"
            TargetControlID="btnHidden2"
            PopupControlID="pnlConfirmDelivery"
            CancelControlID="btnModalCancelDelivery"
            BackgroundCssClass="watermarked"
            X="430"
            Y="250"
            Drag="true"
            
            RepositionMode="None">
    </ajaxToolkit:ModalPopupExtender>
    <script>
        function PrintDivContent() {

            var PrintCommand = '<object ID="PrintCommandObject" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></object>';
            document.body.insertAdjacentHTML('beforeEnd', PrintCommand);
            PrintCommandObject.ExecWB(6,2,0,0);

         }
    </script>


  <cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>