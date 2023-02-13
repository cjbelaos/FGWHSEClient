<%@ Page Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="PartsStoringInquiry.aspx.cs" Inherits="FGWHSEClient.Form.PartsStoringInquiry" EnableEventValidation="false"  %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style =" left:40px;top:110px;">

  <br />
   &nbsp; &nbsp; <font style="font-size:17px; color:Black; font-weight:bold"> Parts Storing Logs (PLC) </font>
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
        <%--<tr>
            <td>
                LANE:
            </td>
            <td>
                <asp:TextBox ID="txtLaneID" runat="server" Width="250px"></asp:TextBox>
            </td>
           
        </tr>--%>
        
         <tr>
             <td>
                DATE: 
                 <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDateFrom" Width="150px" runat="server"></asp:TextBox> &nbsp;
                 <asp:ImageButton ID="imgCalendar3" runat="server" ImageUrl="../Image/calendar_1.png" Height="20px" /> &nbsp; - &nbsp;
        <asp:TextBox ID="txtDateTo" Width="150px" runat="server"></asp:TextBox> &nbsp;
          <asp:ImageButton ID="imgCalendar2" runat="server" ImageUrl="../Image/calendar_1.png" Height="20px" />
            (1 day date range) 
            
            </td>
        </tr>
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
    <asp:GridView ID="gvPLC" runat="server" Width="1460px" 
            AutoGenerateColumns="False"  CellPadding="3" BackColor="White"  
             GridLines="Both" AllowPaging="true" PageSize="15" 
                    Onpageindexchanging='gvPLC_PageIndexChanging' 
                    onrowdatabound="gvPLC_RowDataBound" >
             <RowStyle ForeColor="Black"  BorderColor="black"  BorderWidth="1pt" />
             <Columns>
                   <%-- <asp:BoundField DataField="RFIDTAG" ItemStyle-BorderColor="Black" HeaderText="RFID TAG" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="250px"
                   ItemStyle-Font-Size="11px"  ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>--%>
                 <asp:BoundField DataField="PLC_PARTCODE" ItemStyle-BorderColor="Black" HeaderText="PLC PARTCODE" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="LOTNO" ItemStyle-BorderColor="Black" HeaderText="LOT NO" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="150px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                   <asp:BoundField DataField="LOT_PARTCODE" ItemStyle-BorderColor="Black" HeaderText="LOT PARTCODE" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                 <%--<asp:BoundField DataField="REFNO" ItemStyle-BorderColor="Black" HeaderText="REF NO." 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="200px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>--%>
                 <asp:TemplateField HeaderText="REFERENCE NO"  ItemStyle-HorizontalAlign="Center">  
                            <ItemTemplate> 
                                <asp:LinkButton ID="lblReferenceNo" Font-Bold="True" runat="server"
                                Font-Size="Small" ForeColor="#0066FF" Width = "182px" Font-Underline="true" OnClick="lblReferenceNo_Click"
                                Text='<%# Bind("REFNO") %>' ></asp:LinkButton>
                            </ItemTemplate> 
                            <HeaderStyle BackColor="#538dd5" Font-Size="10px" HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Width="200px" Font-Size="11px" ForeColor="Black" BorderColor="Black"></ItemStyle>
                      </asp:TemplateField> 

                  <asp:BoundField DataField="QTY" ItemStyle-BorderColor="Black" HeaderText="Mother Lot QTY" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="#0066FF"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="ScannedQTY" ItemStyle-BorderColor="Black" HeaderText="Scanned QTY" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="#0066FF"  >
                 </asp:BoundField>
                   <asp:BoundField DataField="Variance" ItemStyle-BorderColor="Black" HeaderText="Variance" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="#0066FF"  >
                 </asp:BoundField>
                <%--  <asp:BoundField DataField="REMARKS" ItemStyle-BorderColor="Black" HeaderText="REMARKS" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="150px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>--%>
                 <asp:BoundField DataField="ERRORMSG" ItemStyle-BorderColor="Black" HeaderText="MESSAGE" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="450px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="CREATEDBY" ItemStyle-BorderColor="Black" HeaderText="CREATED BY" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="250px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="CREATEDDATE" ItemStyle-BorderColor="Black" HeaderText="CREATEDDATE" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="200px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>

                                   <asp:BoundField DataField="ByPassBy" ItemStyle-BorderColor="Black" HeaderText="Bypass BY" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="250px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="ByPassDate" ItemStyle-BorderColor="Black" HeaderText="ByPass Date" 
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

