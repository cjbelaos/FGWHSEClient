<%@ Page Language="C#" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="LotDataScanningByPass.aspx.cs" Inherits="FGWHSEClient.Form.WebForm5" %>

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
                case 115:
                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_btnExecute" %>').click();
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

<div id="dvLotDataScan" runat="server" class="divInput2">
        <table style ="text-align:left">
                <tr style ="color:Black">
                  <td id="tdScan" runat="server" style="text-align:right;">
                       <asp:Label ID="lblScan" runat="server" Text="SCAN DN NO.:"></asp:Label>
                    </td>
                    <td style="padding:5px;text-align:left">
                        <asp:TextBox ID="txtDNNo" runat="server" Width="270px" Height="25px" AutoPostBack="true"
                         Font-Size="20px" ontextchanged="txtDNNo_TextChanged" MaxLength="16" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnDNDetails" runat="server" Text=">>" CssClass="buttonStyle" 
                            onclick="btnDNDetails_Click"/></td>
                </tr>
                <tr>
                    <td height="10px"></td>
                </tr>
                <tr>
                    <td colspan=3 style="text-align:center">
                    <div id="grdHeight" runat="server" style="overflow:auto; height:220px" >
                        <asp:GridView ID="grdDNData" runat="server" CellPadding="4" ForeColor="#333333" 
                        BorderStyle="Solid"
                        AutoGenerateColumns="False"
                        Font-Size="12px" Font-Bold="false" onrowdatabound="grdDNData_RowDataBound">
        
                    <Columns>

                    <asp:BoundField HeaderText="PARTCODE"   DataField="PARTCODE" >
                            <HeaderStyle Width="150px">
                              </HeaderStyle>
                         </asp:BoundField>	   
                    <asp:BoundField HeaderText="DN QTY"   DataField="DNQTY">
                            <HeaderStyle Width="130px">
                              </HeaderStyle>
                         </asp:BoundField>	   
                    <asp:BoundField HeaderText="SCANNED QTY"   DataField="SCANNEDQTY">
                            <HeaderStyle Width="130px">
                              </HeaderStyle>
                         </asp:BoundField>
                     <asp:BoundField HeaderText="STATUS"   DataField="STATUS" Visible="false">
                        <HeaderStyle Width="90px">
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
                    <td colspan=3 align=right>
                        <asp:Button ID="btnDelete" runat="server" Text="F6 DELETE" Height="35px" 
                            Width="70px" CssClass="buttonStyle" Visible="False" 
                            onclick="btnDelete_Click"/>
                        <asp:Button ID="btnExecute" runat="server" Text="F4 EXECUTE" Height="35px" 
                            Width="70px" CssClass="buttonStyle" Visible="False" 
                            onclick="btnExecute_Click"/>
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
            <td id="tdMessage" runat="server" style="border: 1px solid #385d8a; background-color:#d9d9d9; height:25px; padding:5px;width:320px" colspan="2">
                <asp:Label ID="lblMessage" runat="server" CssClass="messageStyle" 
                    ></asp:Label></td>
          </tr>
          </table>
         </div>
</div>
<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>
