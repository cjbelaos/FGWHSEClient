<%@ Page Language="C#" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="RFIDDNDelete.aspx.cs" Inherits="FGWHSEClient.Form.RFIDDNDelete" Title="RFID DN Delete Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <script language="javascript">
     function onEnterUsername(e)
     {
        if (e.keyCode == 13) 
        {
            document.all("txtPassword").focus();
            return false;
        }
     }
    </script>
    
    
    <div style =" left:40px;top:110px; font-weight:600;">
        <br />
        &nbsp;&nbsp;<asp:Label ID="lblHeader" runat="server"></asp:Label>
        <br /><br />
        
        <asp:Panel ID="Panel1" runat="server">
            <div id="dvLotDataScan" runat="server" class="divInput2">
                <table style ="text-align:left">
                    <tr style ="color:Black">
                        <td id="tdScan" runat="server" style="text-align:right;">
                           <asp:Label ID="lblScan" runat="server" Text="SCAN DN NO.:"></asp:Label>
                        </td>
                        <td style="padding:5px;text-align:left">
                            <asp:TextBox ID="txtDNNo" runat="server" Width="270px" Height="20px"
                             Font-Size="20px" AutoPostBack="True" 
                                MaxLength="16" ontextchanged="txtDNNo_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="10px"></td>
                    </tr>
                    <tr>
                        <td height="10px"></td>
                    </tr>
                    <tr>
                        <td colspan=2 style="text-align:center">
                        <div id="grdHeight" runat="server" style="overflow:auto; height:220px" >
                            <asp:GridView ID="grdRFID" runat="server" CellPadding="4" ForeColor="#333333" 
                            BorderStyle="Solid"
                            AutoGenerateColumns="False"
                            Font-Size="12px" Font-Bold="false">
            
                        <Columns>
                        <asp:BoundField HeaderText="PART CODE"   DataField="PARTCODE">
                            <HeaderStyle Width="150px">
                              </HeaderStyle>
                         </asp:BoundField>	
                        <asp:BoundField HeaderText="QTY"   DataField="QTY" >
                                <HeaderStyle Width="150px">
                                  </HeaderStyle>
                             </asp:BoundField>	   
                        <asp:BoundField HeaderText="RFID TAG COUNT"   DataField="TAGCOUNT">
                                <HeaderStyle Width="130px">
                                  </HeaderStyle>
                             </asp:BoundField>	     
                        </Columns>
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#4f81bd" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="White" Font-Size="12px" />
                        </asp:GridView></div>
                            </td>
                    </tr>
                    <tr>
                        <td height="10px"></td>
                    </tr>
                    <tr >
                    <td colspan=2 align=center>
                        <asp:Button ID="btnReceive" runat="server" Text="F4 RECEIVE" Height="35px" 
                            Width="70px" CssClass="buttonStyle" Visible="False"/>
                        <asp:Button ID="btnDelete" runat="server" Text="DELETE" Height="35px" 
                            Width="70px" CssClass="buttonStyle" Visible="False"/>
                        <asp:Button ID="btnClear" runat="server" Text="CLEAR" Height="35px" 
                            Width="70px" CssClass="buttonStyle" Visible="False" 
                            onclick="btnClear_Click"/>
                        
                    </td>
                </tr>
                <tr>
                    <td height="10px"></td>
                </tr>
                </table>
                <table>
                 <tr style ="color:Black">
                    <td style="text-align:right; padding-left:10px">
                        <asp:Label ID="lblMessage2" runat="server" Text="MESSAGE"></asp:Label>:</td>
                    <td id="tdMessage" runat="server" 
                                 style="border: 1px solid #385d8a; background-color:#d9d9d9; height:25px; padding:5px;width:320px">
                        <asp:Label ID="lblMessage" runat="server" CssClass="messageStyle" 
                            ></asp:Label></td>
                 </tr>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlApprove" runat="server" style="border:solid 1px gray; width:243px; display:none" DefaultButton="btnApprove">
        <center>
        <table width="320px" style="background-color:White">
        <tr>
            <td colspan=2 style="color:White; text-align:center" bgcolor="#4F81BD">
                <asp:Label ID="Label1" runat="server" Text="RFID DN DELETION APPROVAL" 
                    Font-Size="14px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="10px"></td>
        </tr>
        <tr>
            <td style="margin-left:8px">
                <asp:Label ID="Label2" runat="server" Text="Username" ForeColor="Black"></asp:Label></td>
            <td style="text-align:left">
                <asp:TextBox ID="txtUsername" runat="server" Width="150px"></asp:TextBox></td>
        </tr>
        <tr>
           <td style="margin-left:8px">
                <asp:Label ID="Label3" runat="server" Text="Password:" ForeColor="Black"></asp:Label></td>
              <td style="text-align:left">
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td height="10px" colspan="2">
                <asp:Label ID="lblErrorMsg" runat="server" Font-Italic="True" 
                    Font-Size="10px" ForeColor="Red"></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align:center" colspan=2>
                <asp:Button ID="btnApprove" runat="server" Text="Approve" Width="80px" 
                    onclick="btnApprove_Click"/>
                <asp:Button ID="btnCancel"
                    runat="server" Text="Cancel" Width="80px" onclick="btnCancel_Click" />
                    <asp:Button ID="btnCancelHide" runat="server" Text="Cancel" Width="80px" Visible="false"/></td>
        </tr>
         <tr>
            <td height="10px" colspan="2"></td>
        </tr>
        </table>
        </center>
        </asp:Panel>
        
    </div>
    
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
    BackgroundCssClass="watermarked"
    TargetControlID="btnDelete"
    CancelControlID="btnCancel"
    PopupControlID="pnlApprove">
</ajaxToolkit:ModalPopupExtender> 
    
    <cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>
