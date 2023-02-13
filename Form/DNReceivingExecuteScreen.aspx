<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Form/MasterPalletMonitoring.Master"  CodeBehind="DNReceivingExecuteScreen.aspx.cs" Inherits="FGWHSEClient.Form.DNReceivingExecuteScreen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style =" left:40px;top:110px;">
     <br />
   &nbsp; &nbsp; <font style="font-size:15px; color:Black"> DN Receiving </font>
   <br />
   
    <div style="margin-left:16px; margin-top:20px; color:Black">
   <table border=0 cellspacing=2 style="font-size:13px; font-weight:normal">
        <tr>
            <td>
                DN NO:
            </td>
            <td>
                <asp:TextBox ID="txtDNNo" runat="server" Width="250px"></asp:TextBox>
            </td>
           
        </tr>
        <tr>
             <td>
                DATE:
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
            <td>
                STATUS:
            </td>
            <td>
                <asp:DropDownList ID ="ddlStatus" runat="server" Width="200px"></asp:DropDownList>
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
   <br />
   <br />
   
   <asp:Panel ID="pnlDN" runat="server">
     <div align="center" style="font-size:10px; color:Gray; ">
        <img src="../Image/whitebox.png" style="width:10px; height:10px" /> Not Yet Arrived &nbsp;&nbsp;
        <img src="../Image/greenbox.png" style="width:10px; height:10px" /> Ongoing Unloading &nbsp;&nbsp;
         <img src="../Image/bluebox.png" style="width:10px; height:10px" /> Complete Delivery&nbsp;&nbsp;
         <img src="../Image/yellowbox2.png" style="width:10px; height:10px" />  Confirmed GR&nbsp;&nbsp;
         <font style="color:Red">Text </font> &nbsp;Bypass DN Data
         <img src="../Image/graybox.png" style="width:10px; height:10px" />  Deleted DN&nbsp;&nbsp;
         <img src="../Image/redbox.png" style="width:10px; height:10px" />  Change DN&nbsp;&nbsp;
    </div>
    <br />
    
    <div align="center" style="font-size:10px; ">
    <asp:GridView ID="gvDNReceiving" runat="server" Width="1160px" 
            AutoGenerateColumns="False"  CellPadding="3" BackColor="White"  
             GridLines="Both" onrowdatabound="gvDNReceiving_RowDataBound" >
             <RowStyle ForeColor="Black"  BorderColor="black"  BorderWidth="1pt" />
             <Columns>
                    <asp:BoundField DataField="DNNO" ItemStyle-BorderColor="Black" HeaderText="DN NO." 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Height="30px"  HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="350px"
                   ItemStyle-Font-Size="11px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="INVOICE" ItemStyle-BorderColor="Black" HeaderText="Invoice No" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Height="30px" HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="250px"
                   ItemStyle-Font-Size="11px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="SUPPLIER" ItemStyle-BorderColor="Black" HeaderText="Supplier" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Height="30px" HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="250px"
                   ItemStyle-Font-Size="11px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="ACTUAL RFID QTY" ItemStyle-BorderColor="Black" HeaderText="Actual RFID Qty" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Height="30px" HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="250px"
                   ItemStyle-Font-Size="11px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="SUPPLIER RFID QTY" ItemStyle-BorderColor="Black" HeaderText="Supplier RFID Qty" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Height="30px" HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="250px"
                   ItemStyle-Font-Size="11px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="DIFFERENCE" ItemStyle-BorderColor="Black" HeaderText="Difference" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Height="30px" HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="150px"
                   ItemStyle-Font-Size="11px"  ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="DNFIRSTRECEIVEDATE" ItemStyle-BorderColor="Black" HeaderText="First Arrival Time" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Height="30px" HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="350px"
                   ItemStyle-Font-Size="11px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="DNLASTRECEIVEDATE" ItemStyle-BorderColor="Black" HeaderText="Last Arrival Time" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Height="30px" HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="350px"
                   ItemStyle-Font-Size="11px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="RECEIVESTATUS" ItemStyle-BorderColor="Black" HeaderText="Status" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Height="30px" HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="350px"
                   ItemStyle-Font-Size="11px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="BYPASSFLAG" ItemStyle-BorderColor="Black" HeaderText="DN Bypass" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Height="30px" HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                   ItemStyle-Font-Size="11px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
             </Columns>
             
    </asp:GridView>
    </div>
    <br />
    
   </asp:Panel>
   <br />
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