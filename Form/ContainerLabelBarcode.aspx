<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContainerLabelBarcode.aspx.cs" Inherits="FGWHSEClient.Form.ContainerLabelBarcode" %>
<%@ Register TagPrefix="cc1" Namespace="BunnyBear" Assembly="msgBox" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">


<link href="../Style/jquery-ui-1.8.5.custom.css" rel="stylesheet" type="text/css" />
<script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>


   <script  type="text/javascript" >

 
         function printcontainer(printpage) {

            var newstr = document.all.item(printpage).innerHTML;
            var oldstr = document.body.innerHTML;
            document.body.innerHTML = newstr;
            window.print();
            document.body.innerHTML = oldstr;
            return false;
        }
    </script>
    
    
    
    <%--<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    
    <meta name="vs_targetSchema" 
    content="http://schemas.microsoft.com/intellisense/ie5">--%>
    

</head>
<body>
    <form id="form1" runat="server">
 

    <input name="b_print" type="button"  onclick="printcontainer('divPrint');" value=" PRINT " style="height:50px;font-size:15px;width:80px;font-family:Arial;">
    
    <br /><br />



    <div id="divPrint" runat="server" align="center">

            <table>
             <tr>
             <td align="left">
                <asp:Label ID="Label1" runat="server" Text="OD No.:" Font-Names="Arial" Font-Bold="true" Font-Size="40px"></asp:Label>
             </td>
             <td align="left">&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblODNo" runat="server" Font-Names="Arial" Font-Bold="true" Font-Size="40px"></asp:Label>
             </td>     
             </tr>
             
             <tr>
             <td align="left">
                <asp:Label ID="Label2" runat="server" Text="Container No.:" Font-Names="Arial" Font-Bold="true" Font-Size="40px"></asp:Label>
             </td>
             <td align="left">&nbsp;&nbsp;&nbsp;
                 <asp:Label ID="lblContainerNo" runat="server" Font-Names="Arial" Font-Bold="true" Font-Size="40px"></asp:Label>
             </td>
             </tr>
             </table>

            <br /><br />
            <asp:Image id="imgBarcode"  runat="server"></asp:Image>
    </div>
    

        
        
    </form>
    
<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</body>
</html>
