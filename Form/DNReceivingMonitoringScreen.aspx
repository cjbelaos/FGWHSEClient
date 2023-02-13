<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Form/MasterPalletMonitoring.Master" CodeBehind="DNReceivingMonitoringScreen.aspx.cs" Inherits="FGWHSEClient.Form.DNReceivingMonitoringScreen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<span style="font-size:12px;"><a href="javascript:history.back()"><span style="text-decoration: none;border:0;"><img src="../Image/Back.png" style="border:0;vertical-align:middle;" />Back to Monitoring Screen</span></a></span>
<div style =" left:40px;top:110px;">
    
    <br />
    <div style="margin-left:15px; float:left; font-family:Calibri; color:Black">
       <table>
            <tr style="font-size:18px">
                <td>
                    Details of DN No:
                </td>
                <td>
                    <asp:Label ID= "lblDNNo" runat="server" Width="200px"></asp:Label>
                </td>
                <%--<td style="padding-left:20px; font-size:11px"  id="tdChangeDN">
                    <img id="Img1" alt="" width="15px" height="15px" src="../Image/page-refresh-icon.png" />&nbsp;&nbsp;<asp:HyperLink ID="lnkChangeDN" runat="server" NavigateUrl="##">Change DN</asp:HyperLink>
                </td>
                <td style="padding-left:650px; font-size:11px" id="tdConfirmDelivery">
                     <img id="Img2" alt="" width="15px" height="15px" src="../Image/page-tick-icon.png" />&nbsp;&nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="##">Confirm Delivery</asp:HyperLink>
                </td>--%>
            </tr>
            <tr>
                <%--<td>
                    sss
                </td>--%>
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
       
       <table border=0 style="margin-top:30px">
            <%--<tr>
                <td colspan="2" style="font-size:12px; color:Black">
                <asp:Label ID="lblPCodeName" runat="server" Width="250px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td  style="font-size:12px; color:Black; font-weight:normal">
                    <asp:Label ID ="lblNoCount" runat="server" Width="100px"></asp:Label>
                </td>
                <td  style="font-size:12px; color:Black; padding-left:565px;font-weight:normal">
                    <asp:Label ID ="lblQtyCount" runat="server" Width="100px"></asp:Label>
                </td>
            </tr>--%>
            
            <tr>
                <td colspan="2" style="margin-top:50px">
                   <asp:Panel ID="pnl" runat="server" Width="1160px" >
                   
                    
                   </asp:Panel> 
                   <br />
                   <br />
                   
                </td>
            </tr>

            <%--<tr>
                <td colspan="2">
                    <asp:GridView ID="gvDNReceiving" runat="server" Width="1160px" 
            AutoGenerateColumns="False" CellPadding="3" BackColor="White" Font-Size="Small" 
             GridLines="Both"  >
             <RowStyle ForeColor="Black" BorderColor="black"  BorderWidth="1pt" />
             <Columns>
                 <asp:BoundField DataField="" ItemStyle-BorderColor="Black" HeaderText="No." 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="12px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="50px"
                   ItemStyle-Font-Size="15px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="" ItemStyle-BorderColor="Black" HeaderText="RFID Tag" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="12px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="250px"
                   ItemStyle-Font-Size="15px" ItemStyle-HorizontalAlign ="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="" ItemStyle-BorderColor="Black" HeaderText="Ref No." 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="12px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="250px"
                   ItemStyle-Font-Size="15px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="" ItemStyle-BorderColor="Black" HeaderText="Lot No." 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="12px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="250px"
                   ItemStyle-Font-Size="15px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="" ItemStyle-BorderColor="Black" HeaderText="Qty" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="12px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                   ItemStyle-Font-Size="15px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="" ItemStyle-BorderColor="Black" HeaderText="Remarks" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="12px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="200px"
                   ItemStyle-Font-Size="15px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="" ItemStyle-BorderColor="Black" HeaderText="Status" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="12px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="200px"
                   ItemStyle-Font-Size="15px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
             </Columns>   
           </asp:GridView>   
                </td>
            </tr>--%>
       </table>
    </div>
</div>
<br />
  <%--<asp:Panel ID="pnlModalChangeDN" runat="server" ForeColor="Black" Height="200px" BorderColor="Black"  align="center"  
        style="display:1">
        
   <div style="width:400px; height:300px; border-style:solid;border-width:1px">     
    <table cellspacing =5>
        <tr>
            <td colspan="2" align="center">
            CHANGE DN NO
            </td>
        </tr>
        <tr>
            <td>
            Current DN No:
            </td>
            <td>
             <asp:Label ID="lblModalDNNo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
             <td>
            New DN No:
            </td>
            <td>
            <asp:TextBox  ID="txtModalNewDNNo" runat="server" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
             <td>
            Person in Charge:
            </td>
            <td>
           <asp:TextBox  ID="txtModalPersonIC" runat="server" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
             <td>
           Reason:
            </td>
            <td>
            <asp:TextBox  ID="TextBox1" runat="server" Width="250px" TextMode="MultiLine" Height="50px"></asp:TextBox>
            </td>
        </tr>
    </table>
    </div>
</asp:Panel>--%>
<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>