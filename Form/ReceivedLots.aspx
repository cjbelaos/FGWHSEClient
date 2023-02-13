<%@ Page Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="ReceivedLots.aspx.cs" Inherits="FGWHSEClient.ReceivedLots" Title="Received Lots Inquiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style =" left:40px;top:110px;">
     <br />
   &nbsp; &nbsp; <font style="font-size:18px;font-weight:bold; color:Black">RECEIVED LOTS</font>
   <br />
   
    <div style="margin-left:16px; margin-top:20px; color:Black">
   <table border=0 cellspacing=2 style="font-size:13px; font-weight:normal">
        <tr>
             <td>
                WH RECEIVED DATE:
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
                SUPPLIER:
            </td>
            <td>
                <asp:DropDownList ID ="ddlSupplier" runat="server" Width="250px"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-top:20px">
                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="150px" onclick="btnSearch_Click" 
                    /> &nbsp; &nbsp;
                  <asp:Button ID="btnExcel" runat="server" Text="Excel" Width="150px" onclick="btnExcel_Click" 
                    /> 
            </td>
        </tr>
   </table>
   </div>
   <br />
   <br />
   
   <div id = "divRcvd" runat="server" align="center" style="font-size:10px; ">
   <table>
   
    <tr>
        <td colspan=7>
             <asp:GridView ID="gvReceivedLots" runat="server" Width="1160px" 
            AutoGenerateColumns="False"  CellPadding="3" BackColor="White"  
             GridLines="Both" >
             <RowStyle ForeColor="Black"  BorderColor="black"  BorderWidth="1pt" />
             <Columns>
                    <asp:BoundField DataField="SUPPLIER" ItemStyle-BorderColor="Black" HeaderText="Supplier" 
                  HeaderStyle-BackColor="Black" HeaderStyle-Height="30px"  HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="350px"
                   ItemStyle-Font-Size="11px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="PARTCODE" ItemStyle-BorderColor="Black" HeaderText="Part Code" 
                  HeaderStyle-BackColor="Black" HeaderStyle-Height="30px" HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="250px"
                   ItemStyle-Font-Size="11px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="PARTNAME" ItemStyle-BorderColor="Black" HeaderText="Part Name" 
                  HeaderStyle-BackColor="Black" HeaderStyle-Height="30px" HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="250px"
                   ItemStyle-Font-Size="11px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="LOTNO" ItemStyle-BorderColor="Black" HeaderText="Lot No" 
                  HeaderStyle-BackColor="Black" HeaderStyle-Height="30px" HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="250px"
                   ItemStyle-Font-Size="11px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="QTY" ItemStyle-BorderColor="Black" HeaderText="Lot Qty" 
                  HeaderStyle-BackColor="Black" HeaderStyle-Height="30px" HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="250px"
                   ItemStyle-Font-Size="11px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="DNFIRSTRECEIVEDATE" ItemStyle-BorderColor="Black" HeaderText="WH Received Date/Time" 
                  HeaderStyle-BackColor="Black" HeaderStyle-Height="30px" HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="350px"
                   ItemStyle-Font-Size="11px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="BQICSDATA" ItemStyle-BorderColor="Black" HeaderText="BQICS Data" 
                  HeaderStyle-BackColor="Black" HeaderStyle-Height="30px" HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="350px"
                   ItemStyle-Font-Size="11px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
             </Columns>
             
    </asp:GridView>
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
<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>
