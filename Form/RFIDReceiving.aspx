<%@ Page Language="C#" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="RFIDReceiving.aspx.cs" Inherits="FGWHSEClient.RFIDReceiving" Title="Untitled Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                    <tr style ="color:Black">
                         <td id="td1" runat="server" style="text-align:left;" colspan=2>
                           <asp:Label ID="Label1" runat="server" Text="LIST OF NOT RECEIVED RFID:" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="10px"></td>
                    </tr>
                    <tr>
                        <td colspan=3 style="text-align:center">
                        <div id="grdHeight" runat="server" style="overflow:auto; height:220px" >
                            <asp:GridView ID="grdRFID" runat="server" CellPadding="4" ForeColor="#333333" 
                            BorderStyle="Solid"
                            AutoGenerateColumns="False"
                            Font-Size="12px" Font-Bold="false" onrowdatabound="grdRFID_RowDataBound">
            
                        <Columns>
                        <asp:TemplateField HeaderText="Select" >
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" oncheckedchanged="chkHeader_CheckedChanged" Checked="false"/>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server" Checked="false"/>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#4f81bd" Font-Bold="True" ForeColor="MediumVioletRed"
                                HorizontalAlign="Center" BorderStyle="Solid" 
                                BorderWidth="1px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                        <asp:BoundField HeaderText="DN NO."   DataField="BARCODEDNNO">
                            <HeaderStyle Width="150px">
                              </HeaderStyle>
                         </asp:BoundField>	
                        <asp:BoundField HeaderText="RFID TAG"   DataField="RFIDTAG" >
                                <HeaderStyle Width="150px">
                                  </HeaderStyle>
                             </asp:BoundField>	   
                        <asp:BoundField HeaderText="REF NO."   DataField="REFNO">
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
                    <td colspan=3 align=center>
                        <asp:Button ID="btnReceive" runat="server" Text="F4 RECEIVE" Height="35px" 
                            Width="70px" CssClass="buttonStyle" Visible="False" 
                            onclick="btnReceive_Click"/>
                        <asp:Button ID="btnClear" runat="server" Text="F8 CLEAR" Height="35px" 
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
    </div>
    <cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>
