<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HTLogin.aspx.cs" Inherits="FGWHSEClient.HTLogin" %>
<%@ Register TagPrefix="cc1" Namespace="BunnyBear" Assembly="msgBox" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>WHSE System - Login</title>
    <link rel="Shortcut Icon" href="Image/iconS.png" type="image/x-icon" />
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
		<form id="Form1" method="post" runat="server" style="width:480px;">
            
            <P>&nbsp;</P>
            <P>&nbsp;</P>
			<P>&nbsp;</P>
			<P>&nbsp;</P>

			<center>
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="400" border="0" style="WIDTH: 400px; HEIGHT: 176px">
					<TR>
						<TD style="HEIGHT: 87px" background="Image/login.png" vAlign="top">
							<TABLE id="Table2" style="WIDTH: 384px; HEIGHT: 144px" cellSpacing="1" cellPadding="1"
								width="384" border="0">
								<TR>
									<TD style="WIDTH: 238px; HEIGHT: 11px"></TD>
									<TD style="HEIGHT: 11px"></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 370px; HEIGHT: 26px; text-align:right" colspan="2">
                                        <asp:Label ID="lblSystemName" CssClass="lblSystemNameLogin" runat="server"  Width="370px" Font-Size="Small"></asp:Label></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 238px; HEIGHT: 33px"></TD>
									<TD style="HEIGHT: 33px">
										<asp:textbox id="txtUsername" tabIndex="1" runat="server" CssClass="text" Height="20px" Width="136px"
											MaxLength="20" Font-Names="Arial" Font-Size="Small" autocomplete="off" style="IME-MODE:disabled" onkeyup="onEnterUsername(event)"></asp:textbox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 238px; HEIGHT: 8px"></TD>
									<TD style="HEIGHT: 8px"></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 238px; HEIGHT: 35px"></TD>
									<TD style="HEIGHT: 35px" vAlign="top">
										<asp:textbox id="txtPassword" tabIndex="2" runat="server" CssClass="text" Height="20px" Width="136px"
											MaxLength="20" Font-Names="Arial" Font-Size="Small" TextMode="Password"></asp:textbox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 238px; HEIGHT: 1px"></TD>
									<TD style="HEIGHT: 1px">
                                        &nbsp;<asp:button id="btnLogin" runat="server" Width="65" Text="Login" tabIndex="3" OnClick="btnLogin_Click"></asp:button></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				<br />
				<%--<asp:Button ID="btnLoginGuest" CssClass="btnLoginGuest" runat="server" Text="Login As Guest" OnClick="btnLoginGuest_Click" />--%>
			</center>
			<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
		</form>
	</body>
</html>
