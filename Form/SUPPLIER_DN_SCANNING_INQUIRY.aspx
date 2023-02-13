<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Form/MasterPalletMonitoring.Master" CodeBehind="SUPPLIER_DN_SCANNING_INQUIRY.aspx.cs" Inherits="FGWHSEClient.Form.SUPPLIER_DN_SCANNING_INQUIRY" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Style.css" rel="stylesheet" />
<div style =" left:40px;top:110px;">
   <br />
   &nbsp; &nbsp; <font style="font-size:17px; color:Black; font-weight:bold"> Supplier DN Loading Inquiry </font>
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
            <td colspan="2" style="padding-top:20px">
                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="150px" OnClick="btnSearch_Click" /> &nbsp; &nbsp;
      
            </td>
        </tr>
   </table>
   </div>
    <br />
    <div style="margin-left:10px">
        <table id="tbList" runat="server"></table>
    </div>

</div> 
</asp:Content>