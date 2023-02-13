<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoLinePrint.aspx.cs" Inherits="FGWHSEClient.Form.AutoLinePrint" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <script type="text/javascript">
         
        function fixform() {
            if (opener.document.getElementById("aspnetForm").target != "_blank") return;
            opener.document.getElementById("aspnetForm").target = "";
            opener.document.getElementById("aspnetForm").action = opener.location.href;
        }
    </script>
    <title></title>
</head>
<body onload="fixform()">
    <form id="form1" runat="server">
        <div id ="dv" runat ="server">
           
             <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
        </div>
       
       
    </form>
</body>
</html>
