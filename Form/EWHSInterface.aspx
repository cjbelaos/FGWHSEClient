<%@ Page Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="EWHSInterface.aspx.cs" Inherits="FGWHSEClient.Form.EWHSInterface" Title="EWHS Interface Refresh" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style =" left:40px;top:110px;">
     <br />
   &nbsp; &nbsp; <font style="font-size:15px; color:Black"> EKanban VP to EWHS Interface </font>
   <br />
   
       <div style="margin-left:16px; margin-top:20px; color:Black">
       <table border=0 cellspacing=2 style="font-size:13px; font-weight:normal">
       <tr>
       <%-- <td>
            Refresh EWHS:
        </td>--%>
        <td width="80px" style="text-align:right">
            <asp:Button ID="btnRefresh" runat="server" Text="START REFRESH" 
                onclick="btnRefresh_Click" /></td>
       </tr>
       </table>
       </div>
       
       <br />
       <br />
       
       &nbsp; &nbsp; <font style="font-size:15px; color:Black"> VIRTUAL VP to EWHS Interface </font>
         
       <br />
       
       <div style="margin-left:16px; margin-top:20px; color:Black">
       <table border=0 cellspacing=2 style="font-size:13px; font-weight:normal">
       <tr>
       <%-- <td>
            Refresh EWHS:
        </td>--%>
        <td width="80px" style="text-align:right">
            <asp:Button ID="btnRefreshVirtual" runat="server" Text="START REFRESH" 
                onclick="btnRefreshVirtual_Click"/></td>
       </tr>
       </table>
       </div>
       
       <br />
       <br />
   
   </div>
   
   <cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>
