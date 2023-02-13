<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HTLoginFG.aspx.cs" Inherits="FGWHSEClient.HTLoginFG" %>
<%@ Register TagPrefix="cc1" Namespace="BunnyBear" Assembly="msgBox" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>WHSE System - Login</title>
    <link rel="Shortcut Icon" href="Image/iconS.png" type="image/x-icon" />
    <script language="javascript">
			function body_onLoad() 
			{
//				document.all("txtUsername").focus();
			}
			
			function onEnterUsername(e) {
 
            if (e.keyCode == 13) {
                document.all("txtPassword").focus();
                return false;
                }
            }
            
            function onEnterPassword(e) {
 
            if (e.keyCode == 13) {
                document.all("txtRole").focus();
                return false;
                }
            }
	</script>
	<link type="text/css" rel="Stylesheet" href="App_Themes/Stylesheet/Main.css"/>
	
</head>
<body leftMargin="0" rightMargin="0" onload="body_onLoad();" background="Image/bg.png">
		<form id="Form1" method="post" runat="server" style="width:480px;">
            
            <P>&nbsp;</P>
            <P>&nbsp;</P>
			<P>&nbsp;</P>
			<P>&nbsp;</P>
            
            <div style="background-image: url('./Image/htlogin_new.png'); height:240px; width:400px">
                <table>
                    <tr style="text-align:right; height:30px">
                        <td colspan = "2" style="width:480px"><asp:Label ID="lblSystemName" CssClass="lblSystemNameLogin" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan = "2" style ="height:15px"></td>
                    </tr>
                    <tr>
                        <td style ="width:140px"></td>
                        <td><asp:TextBox ID="txtUsername" TabIndex = "1"  runat="server" Height ="20px" Width = "136px" MaxLength="20" Font-Names="Arial" Font-Size="Small" autocomplete="off" style="IME-MODE:disabled" onkeyup="onEnterUsername(event)"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan = "2" style ="height:17px"></td>
                    </tr>
                    <tr>
                        <td style ="width:140px"></td>
                        <td><asp:TextBox ID="txtPassword" TabIndex ="2" runat="server" Height="20px" Width="136px" MaxLength="20" Font-Names="Arial" Font-Size="Small" TextMode="Password" style="IME-MODE:disabled" onkeyup="onEnterPassword(event)"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan = "2" style ="height:20px"></td>
                    </tr>
                    
                    <tr>
                        <td style ="width:140px"></td>
                        <td><asp:TextBox ID="txtRole" tabIndex="3" runat="server" Height="20px" Width="136px" MaxLength="20" Font-Names="Arial" Font-Size="Small"></asp:TextBox></td>
                    </tr>
                    
                    <tr>
                        <td style ="width:140px; height:45px"></td>
                        <td><asp:Button ID="btnLogin" runat="server" Width="65" Text="Login" tabIndex="3" OnClick="btnLogin_Click" /></td>
                    </tr>
                    
                    <tr>
                        <td colspan  = "2" align ="center"><asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                    </tr>
                    
                </table>
            </div>
			
			<%--<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>--%>
		</form>
	</body>
</html>