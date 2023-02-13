<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HTPalletShipmentValidation.aspx.cs" Inherits="FGWHSEClient.Form.HTPalletShipmentValidation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PALLET/CONTAINER SCANNING</title>
    <link href="../css/Stylesheet.css" rel="stylesheet" type="text/css" />
    
    <script type ="text/jscript">
		function onEnterUsername(e) {
            if (e.keyCode == 13) {
                document.all("txtPassword").focus();
                return false;
                }
            }
	</script>
    
</head>
<body >
    <form id="form1" runat="server" style="width:455px; font-family:Arial" >
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true"></ajaxToolkit:ToolkitScriptManager>
    
    
    
    <div style ="background-color:#0070C0 ; color:White;height:30px; ">&nbsp;
    <strong>PALLET CONTAINER SCANNING</strong>
    </div>
    <div id ="dvBG" runat ="server" style =" border-color:Black;border-style:solid; border-width:thin; height: 390px">
    <br />
    <div style ="text-align :right">WELCOME, <asp:Label ID="lblUser" runat="server" Text=""></asp:Label>&nbsp;&nbsp;</div>
    <br />
        <table style="margin-left:90px">
            <tr>
                <td>PALLET NO</td>
            </tr>
            <tr>
                <td><asp:TextBox ID="txtPalletNo" onpaste="return false" runat="server" Font-Size ="Larger" Width ="200px" Height="30px" onkeyup ="GetStart(event,'txtPalletNo','hTimeStart','hTimeEnd','btnPallet','lblScanPalletStatus')" ></asp:TextBox></td> <%--ontextchanged="txtPalletNo_TextChanged"--%>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblScanPalletStatus" runat="server" Font-Size="Small" 
                        Font-Bold="True"></asp:Label>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>CONTAINER</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtContainerNo" runat="server" onpaste="return false" Font-Size ="Larger" Width ="200px" Height="30px" onkeyup ="GetStart(event,'txtContainerNo','hTimeStart','hTimeEnd','btnContainer','lblScanContainerStatus')"></asp:TextBox>
                    
                </td> <%--ontextchanged="txtContainerNo_TextChanged"--%>
                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblScanContainerStatus" runat="server" Font-Size="Small" Font-Bold="True"></asp:Label>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                <div  style ="width:285px">
                    <asp:Label ID="lblStatus" runat="server" Text="" Font-Bold = "true"></asp:Label>
                </div>
                <br />
                
                <table>
                    <tr>
                        <td>PALLET SCANNED :</td>
                        <td><asp:Label ID="lblPalletScanned" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td>CONTAINER NO :</td>
                        <td><asp:Label ID="lblContainerNo" runat="server" Text=""></asp:Label></td>
                    </tr>
                </table>
                
                
                </td>
            </tr>
            <tr>
                <td>
                        <%--<asp:Button ID="btnBack" runat="server" Height="34px" Width="76px" Text="BACK" onclick="btnBack_Click" />--%>
                        <asp:LinkButton ID="lnkBack" runat="server" onclick="lnkBack_Click"><strong>BACK</strong></asp:LinkButton>
                </td>
                
            </tr>
            <tr>
                <td>
                    <div style="display:none">
                            <input id="hType" type="hidden" runat="server" />
                            <input id="chType" type="hidden" runat="server" />
                            
                            <input id="hTimeStart" type="hidden" runat="server" />
                            <input id="hTimeEnd" type="hidden" runat="server" />
                            
                            <asp:Label ID="lblControl" runat="server" Text="Label"></asp:Label>
                            
                            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
                            <asp:Button ID="btnPallet" runat="server" Text="Pallet" onclick="btnPallet_Click" />
                            <asp:Button ID="btnContainer" runat="server" Text="Container"   onclick="btnContainer_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    

    
    
<asp:Panel ID="pnlApprove" runat="server" CssClass="modalPopup" align="center" BackColor="#EAE9EB">
<div style="text-align:left;">
    <div style="margin-left:15px">
        <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Width="300px" 
                            Text="MANUAL INPUT APPROVAL" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Width="303px" 
                            Text="Please log-in Approver account" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        
                        <table>
                            <tr>
                                <td style="width:110px"><strong>USER ID:</strong></td>
                                <td><asp:TextBox ID="txtUsername" runat="server" onkeyup="onEnterUsername(event)"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><strong>PASSWORD :</strong></td>
                                <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan = "2">
                                    <asp:Label ID="lblLogStat" Width="249px" runat="server" Font-Size="Small" 
                                        Height="18px" ForeColor="#FF3300" ></asp:Label>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnLogin" runat="server" Text="Login" onclick="btnLogin_Click" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                                </td>
                            </tr>
                        </table>
                    </td>
                
                </tr>
            </table>
        <br />
    </div>
</div>
</asp:Panel>

<ajax:modalpopupextender ID="mp" runat="server" PopupControlID="pnlApprove" TargetControlID="lblControl" BackgroundCssClass="modalBackground">
</ajax:modalpopupextender>



<cc1:msgBox id="MsgBox" runat="server"></cc1:msgBox>
    

        
        
        
    
    </form>
           <script type= "text/javascript" src="../scripts/ScanCheck.js"></script>
           
           <script type= "text/javascript">
               function body_onLoad() 
			    {
			        document.all("txtPalletNo").focus();
                }
           </script>
</body>

</html>
