<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DENSOLogin.aspx.cs" Inherits="FGWHSEClient.DENSOLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>WHSE System - Login</title>
    <link rel="Shortcut Icon" href="Image/iconS.png" type="image/x-icon" />
    <script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript">
			function body_onLoad() 
			{
				document.all("txtUsername").focus();
			}
			
			function onEnterUsername(e) {
 
            if (e.keyCode == 13) {
                document.all("txtPassword").focus();
                return false;
            }
        }  
	</script>
	<link type="text/css" rel="Stylesheet" href="App_Themes/Stylesheet/Main.css"/>
	
</head>
<body leftMargin="0" rightMargin="0" onload="body_onLoad();" background="Image/bg.png">
		<form id="Form1" method="post" runat="server" style="width:300px;">
            
            <P>&nbsp;</P>
            <P>&nbsp;</P>
			<P>&nbsp;</P>
			<P>&nbsp;</P>
            <table>
                <tr>
                    <td  background="Image/logindenso.png">

							<table width = "295px">
							    <tr style=" font-size:small">
							        <td colspan = "2" style =" text-align:right
							        "><asp:Label ID="lblSystemName" CssClass="lblSystemNameLogin" runat="server"  Width="290px"></asp:Label></td>
							    </tr>
							    <tr>
							        <td style="width:60%"><br /></td>
							    </tr>
							    <tr>
							        <td style="width:60%"></td>
							        <td>
							        <asp:textbox id="txtUsername" tabIndex="1" runat="server" CssClass="text" Height="20px" Width="100px" MaxLength="20" Font-Names="Arial" Font-Size="Medium" autocomplete="off" style="IME-MODE:disabled" onkeyup="onEnterUsername(event)"></asp:textbox></td>
							    </tr>
							    
							    <tr>
							        <td style="width:50%"></td>
							        <td style =" height:20px"><asp:textbox id="txtPassword" tabIndex="2" runat="server" CssClass="text" Height="20px" Width="100px" MaxLength="20" Font-Names="Arial" Font-Size="Medium" TextMode="Password"></asp:textbox></td>
							    </tr>
							    <tr>
							        <td style="width:60%; height:35px; vertical-align:top"></td>
							        <td>&nbsp;&nbsp;&nbsp;<asp:button id="btnLogin" runat="server" Font-Size="x-Small" Width="65" Text="Login" tabIndex="3" OnClick="btnLogin_Click"></asp:button></td>
							    </tr>
							</table>
				 </td>
                </tr>
            </table>
				<br />
				<%--<asp:Button ID="btnLoginGuest" CssClass="btnLoginGuest" runat="server" Text="Login As Guest" OnClick="btnLoginGuest_Click" />--%>
		
			<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
		</form>
	</body>
</html>
