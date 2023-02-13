<%@ Page Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="PartsDeliveryInspection.aspx.cs" Inherits="FGWHSEClient.Form.PartsDeliveryInspection" Title="Parts Delivery-Inspection Matrix" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style =" left:40px;top:50px;">

   &nbsp; &nbsp; <font style="font-size:15px; color:Black"> Parts Delivery-Inspection Matrix</font>
   <br />
    <div style="margin-left:16px; margin-top:20px; color:Black">
   <table border=0 cellspacing=2 style="font-size:13px; font-weight:normal">
        <tr>
            <td style="width:100px">
                DN NO:<asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDNNo" runat="server" Width="220px"></asp:TextBox>
            </td>
            <td colspan="2" style="padding-left:10px">
                <asp:Button ID="btnView" runat="server" Text="View" Width="80px" 
                    onclick="btnView_Click" 
                    />
                  <asp:Button ID="btnPrint" runat="server" Text="Print" Width="80px" onclick="btnPrint_Click"  
                     /> 
            </td>
        </tr>
        </table>
        
          <br />
        <br />
        <asp:LinkButton ID="lnkRefresh" runat="server" Font-Names="Calibri" 
            Font-Size="12pt" PostBackUrl="~/Form/EWHSInterface.aspx">Refresh Master</asp:LinkButton>
            
   </div>

</div>
<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>

