<%@ Page Title="SHIPMENT STATUS" Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="ReportShipmentStatus.aspx.cs" Inherits="FGWHSEClient.Form.ReportShipmentStatus"  EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

<div style =" left:40px;top:110px; font-weight:600;">

<br />
&nbsp;&nbsp;SHIPMENT STATUS
<br /><br />

        <div>
        
        <table width="600px">
        <tr>
        <td>
                DATE: 
                 <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            <td>
                 <asp:TextBox ID="txtDateFrom" Width="150px" runat="server"></asp:TextBox> &nbsp;
            </td>
            <td>
                <asp:ImageButton ID="imgCalendar1" runat="server" ImageUrl="../Image/calendar_1.png" Height="20px" />
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
            
       
           
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="SEARCH" 
                    onclick="btnSearch_Click" /> &nbsp;
                
            </td>
        </tr>
        </table>
        
        
        
        
        
                
        <br />

        <table style="margin-left:10px;width:1190px">
            

        <tr>
        <td align="center">
                <asp:Chart ID="chartShipmentStatus" runat="server" Width="1050px" Height="550px"
                        Visible="false">
                        
                        <Titles>
                        <asp:Title Name="Title1" Docking="Top" Alignment="MiddleCenter"  ></asp:Title>
                        </Titles>
                        <Legends>  
                            <asp:Legend Name="Legend1" Enabled="true" Alignment="Center">  
                            </asp:Legend>  
                        </Legends>  
                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                        <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                        
                                <AxisY>
                                    
                                </AxisY>
                                
                        </asp:ChartArea>
                        </ChartAreas>
                        
                        </asp:Chart>
        </td>


        </tr>

        </table>
        
        
        </div>
          
          
          
</div>       

<ajaxToolkit:Calendarextender ID="Calendarextender1" runat="server" BehaviorID="calendar1"
TargetControlID="txtDateFrom"
Format="MM/dd/yyyy"
PopupButtonID="imgCalendar1"  >
</ajaxToolkit:Calendarextender>


<ajaxToolkit:Calendarextender ID="Calendarextender3" runat="server" BehaviorID="calendar2"
TargetControlID="txtDateTo"
Format="MM/dd/yyyy"
PopupButtonID="imgCalendar2">
</ajaxToolkit:Calendarextender>

<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>
