<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HTLoginPalletShipmentValidation.aspx.cs" Inherits="FGWHSEClient.HTLoginPalletShipmentValidation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PALLET/CONTAINER SCANNING</title>
    
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
	<%--<link type="text/css" rel="Stylesheet" href="App_Themes/Stylesheet/Main.css"/>--%>
</head>
<body onload="body_onLoad();" >
     <form id="form1" runat="server" style="width:455px; font-family:Arial; " >
            <div style ="background-color:#0070C0 ; color:White ; height:30px;">&nbsp;
            <strong>PALLET CONTAINER SCANNING</strong>
            </div>
            <div style =" border-color:Black;border-style:solid; border-width:thin; height: 320px">
            <br />
            <br />
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
									<TD style="WIDTH: 238px; HEIGHT: 26px" colspan="2">
                                        <asp:Label ID="lblSystemName" CssClass="lblSystemNameLogin" runat="server"  Width="370px"></asp:Label></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 238px; HEIGHT: 33px"></TD>
									<TD style="HEIGHT: 33px">
										<asp:textbox id="txtUsername" tabIndex="1" runat="server" CssClass="text" Height="20px" Width="136px" MaxLength="20" Font-Names="Arial" Font-Size="Small" autocomplete="off" style="IME-MODE:disabled" onkeyup="onEnterUsername(event)"></asp:textbox></TD>
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
                                        &nbsp;<asp:button id="btnLogin" runat="server" Width="65" Text="Login" 
                                            tabIndex="3" onclick="btnLogin_Click" ></asp:button></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				<br />
				<br />
                <asp:Label ID="lblError" runat="server" ForeColor ="Red" Text=""></asp:Label>
				
				
				<%--<asp:Button ID="btnLoginGuest" CssClass="btnLoginGuest" runat="server" Text="Login As Guest" OnClick="btnLoginGuest_Click" />--%>
			</center>
            </div>
    <cc1:msgBox id="MsgBox" runat="server"></cc1:msgBox>
    </form>
</body>
</html>
