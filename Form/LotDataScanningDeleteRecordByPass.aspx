<%@ Page Language="C#" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="LotDataScanningDeleteRecordByPass.aspx.cs" Inherits="FGWHSEClient.Form.WebForm7" Title="LOT DATA SCANNING (BYPASS) - DELETE RECORD" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
        document.onkeydown = function (evt) {
            if (navigator.userAgent.indexOf("Opera") == -1) {
                evt = evt || window.event;
            }
            DoTask(evt.keyCode);
        };
        function DoTask(keyCode) {
            switch (keyCode) {
                case 117:
                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_btnDelete" %>').click();
                    break;
                case 118:
                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_btnBack" %>').click();
                     break;
                case 119:
                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_btnClear" %>').click();
                    break;
               
            }
        }
    </script>
<div style =" left:40px;top:110px; font-weight:600; background-color:#e6b9b8">
<br />
&nbsp;&nbsp;<asp:Label ID="lblHeader" runat="server"></asp:Label>
<br /><br /> 
<div id = "dvLotDataScan" runat="server" class="divInput3">
    <table style ="text-align:left">
                <tr style ="color:Black">
                   <td style="text-align:left;">
                        <asp:Label ID="lblProceed" runat="server" Text="Proceed login to delete existing record:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="5px"></td>
                </tr>
                </table>
                <asp:Panel ID="pnlDN" runat="server" Visible="false">
                <center>
                <table>
                <tr>
                    <td style="text-align:center">
                     <div id="grdHeight" runat="server" style="overflow:auto; width:420px; height:220px" >
                        <asp:GridView ID="grdDNData" runat="server" CellPadding="4" ForeColor="#333333" 
                        BorderStyle="Solid"
                        AutoGenerateColumns="False"
                        Font-Size="12px" Font-Bold="false">
        
                    <Columns>

                    <asp:BoundField HeaderText="DN NO."   DataField="BARCODEDNNO" >
                            <HeaderStyle Width="300px">
                              </HeaderStyle>
                         </asp:BoundField>	   
                    <asp:BoundField HeaderText="RFID SERIAL"   DataField="RFIDTAG">
                            <HeaderStyle Width="300px">
                              </HeaderStyle>
                         </asp:BoundField>	   
                    <asp:BoundField HeaderText="PARTCODE"   DataField="ITEMCODE">
                            <HeaderStyle Width="300px">
                              </HeaderStyle>
                         </asp:BoundField>
                         <asp:BoundField HeaderText="LOT NO."   DataField="LOTNO">
                            <HeaderStyle Width="300px">
                              </HeaderStyle>
                         </asp:BoundField>
                         <asp:BoundField HeaderText="REF NO."   DataField="REFNO">
                            <HeaderStyle Width="300px">
                              </HeaderStyle>
                         </asp:BoundField>
                         <asp:BoundField HeaderText="QTY"   DataField="QTY">
                            <HeaderStyle Width="300px">
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
                </table>
                </center>
                </asp:Panel>
                
                <center>
                <asp:Panel ID="pnlDNandRFID" runat="server" Visible="false">
                <table id ="tablestyle2" runat="server" cellpadding="0" cellspacing="0">
                      <tr style ="color:Black">
                   <td style="text-align:right; background-color:#4f81bd; color:White">
                       <asp:Label ID="lblDN" runat="server" Text="DN NO.:"></asp:Label>
                    </td>
                    <td style="text-align:left">
                        <asp:TextBox ID="txtDNNo" runat="server" Width="230px"
                            Font-Size="20px" Enabled="False" BorderStyle="Solid" 
                            BorderWidth="1px"  ></asp:TextBox>
                    </td>
                </tr>
                 <tr style ="color:Black">
                   <td style="text-align:right; background-color:#4f81bd; color:White">
                       <asp:Label ID="lblRFID" runat="server" Text="SCAN RFID:"></asp:Label>
                    </td>
                    <td style="text-align:left">
                        <asp:TextBox ID="txtRFID" runat="server" Width="230px"
                            Font-Size="20px" AutoPostBack="True" Enabled="False" BorderStyle="Solid" 
                            BorderWidth="1px" ></asp:TextBox>
                    </td>
                </tr>
                <tr style ="color:Black">
                   <td style="text-align:right; background-color:#4f81bd; color:White">
                       <asp:Label ID="lblPCode" runat="server" Text="PARTCODE:"></asp:Label>
                    </td>
                    <td style="text-align:left">
                        <asp:TextBox ID="txtPartCode" runat="server" Width="230px"
                            Font-Size="20px" Enabled="False" BorderStyle="Solid" 
                            BorderWidth="1px" ></asp:TextBox>
                    </td>
                </tr>
                <tr style ="color:Black">
                   <td style="text-align:right; background-color:#4f81bd; color:White">
                       <asp:Label ID="lblLot" runat="server" Text="LOT NO.:"></asp:Label>
                    </td>
                    <td style="text-align:left">
                        <asp:TextBox ID="txtLotNo" runat="server" Width="230px"
                            Font-Size="20px" Enabled="False" BorderStyle="Solid" 
                            BorderWidth="1px" ></asp:TextBox>
                    </td>
                </tr>
                  <tr style ="color:Black">
                   <td style="text-align:right; background-color:#4f81bd; color:White">
                       <asp:Label ID="lblRefNo" runat="server" Text="REF. NO.:"></asp:Label>
                    </td>
                    <td style="text-align:left">
                        <asp:TextBox ID="txtRefNo" runat="server" Width="230px"
                            Font-Size="20px" Enabled="False" BorderStyle="Solid" 
                            BorderWidth="1px" ></asp:TextBox>
                    </td>
                </tr>
                <tr style ="color:Black">
                   <td style="text-align:right; background-color:#4f81bd; color:White">
                       <asp:Label ID="lblQty" runat="server" Text="QUANTITY:"></asp:Label>
                    </td>
                     <td style="text-align:left">
                        <asp:TextBox ID="txtQty" runat="server" Width="230px"
                            Font-Size="20px" Enabled="False" BorderStyle="Solid" 
                            BorderWidth="1px" ></asp:TextBox>
                    </td>
                </tr>
                 <tr style ="color:Black">
                   <td style="text-align:right; background-color:#4f81bd; color:White">
                       <asp:Label ID="lblRemarks" runat="server" Text="REMARKS:"></asp:Label>
                    </td>
                    <td style="text-align:left">
                        <asp:TextBox ID="txtRemarks" runat="server" Width="230px"
                            Font-Size="20px" Enabled="False" BorderStyle="Solid" 
                            BorderWidth="1px" ></asp:TextBox>
                    </td>
                </tr>
                </table>
                </asp:Panel>
                </center>
                
                 <asp:Panel ID="Panel1" runat="server" DefaultButton="btnDelete">
                 
                <center>
                 <table id ="tablestyle" runat="server" cellpadding="0" cellspacing="0" style ="text-align:left">
                 <tr>
                    <td height="8px"></td>
                 </tr>
                <tr style ="color:Black">
                   <td id ="tdUser" runat="server" style="text-align:right;">
                       <asp:Label ID="lblUserName" runat="server" Text="USERNAME:"></asp:Label> 
                    </td>
                    <td style="text-align:left" colspan=4>
                        <asp:TextBox ID="txtUsername" runat="server" Width="230px"
                            Font-Size="20px" ></asp:TextBox>
                    </td>
                </tr>
                 <tr style ="color:Black">
                   <td id ="tdPass" runat="server" style="text-align:right;">
                       <asp:Label ID="lblPassword" runat="server" Text="PASSWORD:"></asp:Label>
                    </td>
                    <td style="text-align:left" colspan=4>
                        <asp:TextBox ID="txtPassword" runat="server" Width="230px"
                            Font-Size="20px" TextMode="Password" ></asp:TextBox>
                    </td>
                </tr>
                <tr style ="color:Black">
            <td style="text-align:right;">
                <asp:Label ID="lblMessage2" runat="server" Text="MESSAGE:"></asp:Label></td>
            <td id="tdMessage" runat="server"  style="border: 1px solid #385d8a; background-color:#d9d9d9; height:25px; padding:5px;" colspan=2>
                <asp:Label ID="lblMessage" runat="server" CssClass="messageStyle" 
                    ></asp:Label></td>
          </tr>
                <tr>
                    <td colspan=3 align=left>
                        <asp:Button ID="btnDelete" runat="server" Text="F6 DELETE" Height="35px" 
                            Width="70px" CssClass="buttonStyle" onclick="btnDelete_Click"/>
                        <asp:Button ID="btnBack" runat="server" Text="F7 BACK" Height="35px" 
                            Width="70px" CssClass="buttonStyle" 
                            onclick="btnBack_Click"/>
                        <asp:Button ID="btnClear" runat="server" Text="F8 CLEAR" Height="35px" 
                            Width="70px" CssClass="buttonStyle"/>
                    </td>
                </tr>
                <tr>
                    <td height="5px"></td>
                </tr>
                 
          </table>
          </center>
          </asp:Panel>

</div>    
</div>
<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>
