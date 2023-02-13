<%@ Page Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="ReportCompletionRate.aspx.cs" Inherits="FGWHSEClient.Form.ReportCompletionRate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style ="margin-left:10px">
<br />
 <table>
 <tr>
            <td>
                DATE: 
                 <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            <td>
                 <asp:TextBox ID="txtDateFrom" Width="150px" runat="server"></asp:TextBox> &nbsp;
            </td>
            <td>
                <asp:ImageButton ID="imgCalendar3" runat="server" ImageUrl="../Image/calendar_1.png" Height="20px" />
            </td>
            <td>
                 &nbsp; - &nbsp;
            </td>
            <td>
                 <asp:TextBox ID="txtDateTo" Width="150px" runat="server"></asp:TextBox> &nbsp;
            </td>
            <td>
                <asp:ImageButton ID="imgCalendar2" runat="server" ImageUrl="../Image/calendar_1.png" Height="20px" />
            </td>
            
        </tr>
        <tr>
            <td></td>
            <td>
                <br />
                <asp:Button ID="btnSearch" runat="server" Text="SEARCH" 
                    onclick="btnSearch_Click" /> &nbsp;
                <asp:Button ID="btnExport" runat="server" Text="EXCEL" 
                    onclick="btnExport_Click" />
            </td>
        </tr>
 </table>
 
 <br />
<div id="dvExport" runat="server">
 <table style="color:Black">
    <tr>
        <td><b>COMPLETION RATE&nbsp;</b></td>
        <td><asp:Label ID="lblCompletionRate" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td><br />O/D COMPLETED&nbsp;</td>
        <td><br /><asp:Label ID="lblODCompleted" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>PALLET COMPLETED&nbsp;</td>
        <td><asp:Label ID="lblPalletCompleted" runat="server" Text=""></asp:Label></td>
    </tr>
 </table>
 <br />
 

 <asp:GridView ID="grdCompletionRate" runat="server" Width="1130px"  
        AutoGenerateColumns="False"  CellPadding="3" BackColor="White" GridLines="Both" 
        AllowPaging="true" PageSize="15" BorderColor ="Black">
             <RowStyle ForeColor="Black"  BorderColor="black"  BorderWidth="1pt" />
             <Columns>
                <asp:TemplateField HeaderText = "GNS + INVOICE NUMBER">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:Label ID="lblGNSINVOICENUMBER" runat="server" Text='<%# Eval("GNSINVOICENUMBER") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "O/D">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:Label ID="lblODNO" runat="server" Text='<%# Eval("ODNO") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                
                <asp:TemplateField HeaderText = "PALLET COUNT">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:Label ID="lblPALLETCOUNT" runat="server" Text='<%# Eval("PALLETCOUNT") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "ACTUAL PALLET">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:Label ID="lblACTUALPALLET" runat="server" Text='<%# Eval("ACTUALPALLET") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                
                <asp:TemplateField HeaderText = "REMAINING PALLET">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:Label ID="lblREMAININGPALLET" runat="server" Text='<%# Eval("REMAININGPALLET") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "LOT NO">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:Label ID="lblLOTNO" runat="server" Text='<%# Eval("LOTNO") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                
                <asp:TemplateField HeaderText = "SHIPMENT">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:Label ID="lblSHIPMENT" runat="server" Text='<%# Eval("SHIPMENTNO") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "PONUMBER">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:Label ID="lblPONUMBER" runat="server" Text='<%# Eval("PONUMBER") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
             </Columns>
             
    </asp:GridView>

</div> 
 <ajaxToolkit:Calendarextender ID="Calendarextender1" runat="server" BehaviorID="calendar1"
TargetControlID="txtDateFrom"
Format="dd-MMM-yyyy"
PopupButtonID="imgCalendar3"  >
</ajaxToolkit:Calendarextender>


<ajaxToolkit:Calendarextender ID="Calendarextender3" runat="server" BehaviorID="calendar2"
TargetControlID="txtDateTo"
Format="dd-MMM-yyyy"
PopupButtonID="imgCalendar2">
</ajaxToolkit:Calendarextender>
<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</div>
</asp:Content>
