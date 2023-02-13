<%@ Page Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="InHouseRFIDInquiry.aspx.cs" Inherits="FGWHSEClient.Form.InHouseRFIDInquiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style =" left:40px;top:110px;">
    <br />
   &nbsp; &nbsp; <font style="font-size:15px; color:Black">IN HOUSE RFID TRANSACTION INQUIRY</font>
   <br />
   
    <div style="margin-left:16px; margin-top:20px; color:Black">
   <table border=0 cellspacing=2 style="font-size:13px; font-weight:normal">
        <tr>
            <td>
                PARTCODE:
            </td>
            <td>
                <asp:TextBox ID="txtPartCode" runat="server" Width="250px"></asp:TextBox>
            </td>
           
        </tr>
          <tr>
            <td>
                RFIDTAG:
            </td>
            <td>
                <asp:TextBox ID="txtRFIDTag" runat="server" Width="250px"></asp:TextBox>
            </td>
           
        </tr>
         <tr>
            <td>
                REF NO:
            </td>
            <td>
                <asp:TextBox ID="txtRefNo" runat="server" Width="250px"></asp:TextBox>
            </td>
           
        </tr>
        <tr>
             <td>
                TRANSACTION DATE:
            </td>
            <td>
                <asp:TextBox ID="txtDateFrom" Width="150px" runat="server"></asp:TextBox> &nbsp;
                 <asp:ImageButton ID="imgCalendar3" runat="server" ImageUrl="../Image/calendar_1.png" Height="20px" /> &nbsp; - &nbsp;
        <asp:TextBox ID="txtDateTo" Width="150px" runat="server"></asp:TextBox> &nbsp;
          <asp:ImageButton ID="imgCalendar2" runat="server" ImageUrl="../Image/calendar_1.png" Height="20px" />
            </td>
        </tr>
        <tr>
            <td>
                AREA:
            </td>
            <td>
                <asp:DropDownList ID ="ddlArea" runat="server" Width="250px"></asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td colspan="2" style="padding-top:20px">
                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="150px" 
                    onclick="btnSearch_Click" /> &nbsp; &nbsp;
                  <asp:Button ID="btnExcel" runat="server" Text="Excel" Width="150px" 
                    onclick="btnExcel_Click" /> 
            </td>
        </tr>
   </table>
   </div>
   
   <%-- Details per RFID Tag--%>  
    <div runat="server" id="divexport">
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

<ajaxToolkit:Calendarextender ID="Calendarextender1" runat="server" BehaviorID="calendar1"
TargetControlID="txtDateFrom"
Format="dd-MMM-yyyy HH:mm tt"
PopupButtonID="imgCalendar3"  >
</ajaxToolkit:Calendarextender>


<ajaxToolkit:Calendarextender ID="Calendarextender3" runat="server" BehaviorID="calendar2"
TargetControlID="txtDateTo"
Format="dd-MMM-yyyy HH:mm tt"
PopupButtonID="imgCalendar2">
</ajaxToolkit:Calendarextender>
</asp:Content>
