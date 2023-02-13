<%@ Page Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="PartsLocationInquiry.aspx.cs" Inherits="FGWHSEClient.Form.PartsLocationInquiry" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style =" left:40px;top:110px;">

  <br />
   &nbsp; &nbsp; <font style="font-size:17px; color:Black; font-weight:bold"> Parts Location Inquiry </font>
   <br />

    <div style="margin-left:16px; margin-top:20px; color:Black">
     <table border=0 cellspacing=2 style="font-size:13px; font-weight:normal">
       <%-- <tr>
             <td>
                DATE: 
                 <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDateFrom" Width="150px" runat="server"></asp:TextBox> &nbsp;
                 <asp:ImageButton ID="imgCalendar3" runat="server" ImageUrl="../Image/calendar_1.png" Height="20px" /> &nbsp; - &nbsp;
        <asp:TextBox ID="txtDateTo" Width="150px" runat="server"></asp:TextBox> &nbsp;
          <asp:ImageButton ID="imgCalendar2" runat="server" ImageUrl="../Image/calendar_1.png" Height="20px" />
            </td>
        </tr>--%>
         
        
        
        <tr>
            <td>
                WH LOCATION ID:
            </td>
            <td>
                <asp:TextBox ID="txtLaneID" runat="server" Width="200px"></asp:TextBox>
            </td>
           
        </tr>
        <tr>
            <td>
                REFNO:
            </td>
            <td>
                <asp:TextBox ID="txtRefNo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                PARTCODE:
            </td>
            <td>
                <asp:TextBox ID="txtItemCode" runat="server" Width="200px"></asp:TextBox>
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
   
   <asp:Panel ID="pnlDN" runat="server">
    <br />

    <div id ="grid" runat = "server" align="center" style="font-size:10px;overflow:auto;">
    <table>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td>
    <asp:GridView ID="gvPLC" runat="server" Width="960px" 
            AutoGenerateColumns="False"  CellPadding="3" BackColor="White"  
             GridLines="Both" AllowPaging="true" PageSize="15" 
                    Onpageindexchanging='gvPLC_PageIndexChanging' 
                    onrowdatabound="gvPLC_RowDataBound" >
             <RowStyle ForeColor="Black"  BorderColor="black"  BorderWidth="1pt" />
             <Columns>
                 <asp:BoundField DataField="PARTCODE" ItemStyle-BorderColor="Black" HeaderText="PART CODE" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="LOTNO" ItemStyle-BorderColor="Black" HeaderText="LOT NO" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="150px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="REFNO" ItemStyle-BorderColor="Black" HeaderText="REF NO." 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="200px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="QTY" ItemStyle-BorderColor="Black" HeaderText="QTY" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                <%--  <asp:BoundField DataField="REMARKS" ItemStyle-BorderColor="Black" HeaderText="REMARKS" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="150px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>--%>
                  <asp:BoundField DataField="RFIDTAG" ItemStyle-BorderColor="Black" HeaderText="WHSE LOATION ID" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="150px"
                   ItemStyle-Font-Size="11px"  ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="UPDATEDBY" ItemStyle-BorderColor="Black" HeaderText="LAST UPDATED BY" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="250px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="UPDATEDDATE" ItemStyle-BorderColor="Black" HeaderText="LAST UPDATED DATE" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="200px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
             </Columns>
             
    </asp:GridView>
    </td>
    </tr>
    </table>
    </div>
    
   </asp:Panel>
   <br />
   
</div>

<%--<ajaxToolkit:Calendarextender ID="Calendarextender1" runat="server" BehaviorID="calendar1"
TargetControlID="txtDateFrom"
Format="dd-MMM-yyyy HH:mm tt"
PopupButtonID="imgCalendar3"  >
</ajaxToolkit:Calendarextender>


<ajaxToolkit:Calendarextender ID="Calendarextender3" runat="server" BehaviorID="calendar2"
TargetControlID="txtDateTo"
Format="dd-MMM-yyyy HH:mm tt"
PopupButtonID="imgCalendar2">
</ajaxToolkit:Calendarextender>--%>

<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>

