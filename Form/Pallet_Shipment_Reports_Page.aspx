<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pallet_Shipment_Reports_Page.aspx.cs" Inherits="FGWHSEClient.Form.Pallet_Shipment_Reports_Page" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Print Pallet Shipment Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:scriptmanager id="ScriptManager1" runat="server" enablepagemethods="true" />
    <div >

   <%-- <asp:Button ID="btnGenerate" runat="server" Text="View Report" 
        onclick="btnGenerate_Click" />--%>
        <div>
            <table>
               <tr>
                    <td><rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                            Font-Size="8pt" Height="400px" ProcessingMode="Remote" Width="95%" 
                            ShowToolBar="false" AsyncRendering="False" ></rsweb:ReportViewer></td>
               </tr> 
            </table>
        </div>
<cc1:msgbox ID="msgBox1" runat="server" />
</div>
    </form>
</body>
</html>
