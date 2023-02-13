<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PPWPrintPreview.aspx.cs" Inherits="FGWHSEClient.Form.PPWPrintPreview" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>PPW PARTS PRINTING</title>
     <link href="StyleSheet_Font.css" rel="stylesheet" type="text/css" />
</head>
<body>
<script>

        function PrintDivContent() {
        
        var PrintCommand = '<object ID="PrintCommandObject" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></object>';
        document.body.insertAdjacentHTML('beforeEnd', PrintCommand);
        PrintCommandObject.ExecWB(6, 2,0,0);

             }
         </script>
         
         <script type="text/javascript">

             function ShowMaint()
            {
                     var url="BatchTransaction.aspx?Username=";
                     var user= '<%=this.Request.QueryString["Username"]%>';
                     window.location = url.concat(user) ;
            }
            
            
             function GoBack()
            {
                    window.history.back();
            }
            
            
            function GoExit()
            {
                    window.close();
            }
            
</script>


    <form id="form1" runat="server">
    <div style="font-family:Calibri; font-size:24px">
    <%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />--%>
    
    <center>
        <table>
            <tr>
                <td width="180px">
                    <asp:Label ID="Label1" runat="server" Text="Partcode:"></asp:Label></td>
                <td width="350px"  class="BarcodeFont" style="font-size:48px;">
                    <% Response.Write(strPartCode); %></td>
            </tr>
            <tr>
                <td height="10px"></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Total Qty:"></asp:Label></td>
                <td  class="BarcodeFont" style="font-size:48px;">
                    <% Response.Write(strQty); %></td>
            </tr>
        </table>
        <table style ="border: solid 1px black; border-bottom: solid 1px black; border-left: solid 1px black; border-right: solid 1px black; border-top: solid 1px black; border-collapse:collapse">
            <tr>
                <td width="180px" style ="border: solid 1px black; border-bottom: solid 1px black; border-left: solid 1px black; border-right: solid 1px black; border-top: solid 1px black;">
                    <asp:Label ID="Label3" runat="server" Text="Partname:"></asp:Label></td>
                <td width="350px" style ="border: solid 1px black; border-bottom: solid 1px black; border-left: solid 1px black; border-right: solid 1px black; border-top: solid 1px black;">
                    <asp:Label ID="lblPartName" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td style ="border: solid 1px black; border-bottom: solid 1px black; border-left: solid 1px black; border-right: solid 1px black; border-top: solid 1px black;">
                    <asp:Label ID="Label5" runat="server" Text="Date:"></asp:Label></td>
                <td style ="border: solid 1px black; border-bottom: solid 1px black; border-left: solid 1px black; border-right: solid 1px black; border-top: solid 1px black;">
                    <asp:Label ID="lblDate" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td style ="border: solid 1px black; border-bottom: solid 1px black; border-left: solid 1px black; border-right: solid 1px black; border-top: solid 1px black;">
                    <asp:Label ID="Label7" runat="server" Text="Time:"></asp:Label></td>
                <td style ="border: solid 1px black; border-bottom: solid 1px black; border-left: solid 1px black; border-right: solid 1px black; border-top: solid 1px black;">
                    <asp:Label ID="lblTime" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td style ="border: solid 1px black; border-bottom: solid 1px black; border-left: solid 1px black; border-right: solid 1px black; border-top: solid 1px black;">
                    <asp:Label ID="Label9" runat="server" Text="GNS+ In-Charge:"></asp:Label></td>
                <td style ="border: solid 1px black; border-bottom: solid 1px black; border-left: solid 1px black; border-right: solid 1px black; border-top: solid 1px black;"></td>
            </tr>
        </table>
    </center>
    </div>
    </form>
</body>
</html>
