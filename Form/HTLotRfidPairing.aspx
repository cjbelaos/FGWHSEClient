<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" CodeBehind="HTLotRfidPairing.aspx.cs" Inherits="FGWHSEClient.Form.HTLotRfidPairing" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <script language="javascript" type="text/javascript">
     function onEnterUsername(e)
     {
        if (e.keyCode == 13) 
        {
            document.all("txtPassword").focus();
            return false;
        }
     }
 
     function onScanLot(e) 
     {
      
        if (e.keyCode == 13) 
        {
//            document.getElementById('<%= "ctl00_ContentPlaceHolder1_btnLot" %>').click();
            document.getElementById("<%= btnLot.ClientID %>").click();
        }
        
     }
     
     
     function onScanRFID(e) 
     {
      
        if (e.keyCode == 13) 
        {
//            document.getElementById('<%= "ctl00_ContentPlaceHolder1_btnRFID" %>').click();
              document.getElementById("<%= btnRFID.ClientID %>").click();              
        }
        
     }
     
    </script>
    
    
    <div style =" left:40px;top:110px; font-weight:600;">
        <br />
        &nbsp;&nbsp;<asp:Label ID="lblHeader" runat="server">LOT-RFID PAIRING</asp:Label>
        <br /><br />
        
        
            <div id="dvLotDataScan" runat="server" class="divInput2">
                <table style ="text-align:left">
                
                
                    <tr style ="color:Black">
                        <td id="tdScan" runat="server" style="text-align:right;">
                           <asp:Label ID="lblScanRfid" runat="server" Text="SCAN RFID:" ForeColor="Black"></asp:Label>
                        </td>
                        <td style="padding:5px;text-align:left">
                            <asp:TextBox ID="txtRfid" runat="server" Height="38px" Width="311px"  Font-Size="20px" onkeyup="onScanRFID(event)"></asp:TextBox>
                        </td>
                    </tr>
                   
                   
                    <tr>
                        <td id="td1" runat="server" style="text-align:right;">
                           <asp:Label ID="lblScanLotQr" runat="server" Text="SCAN LOT QR:" ForeColor="Black"></asp:Label>
                        </td>
                        <td style="padding:5px;text-align:left">
                            <asp:TextBox ID="txtLotQr" runat="server" Height="38px" Width="311px"  Font-Size="20px" onkeyup="onScanLot(event)"></asp:TextBox>
                        </td>
                    </tr>
                    
                    
                    
                    <tr>
                        <td id="td2" runat="server" style="text-align:right;">
                           <asp:Label ID="lblMessage" runat="server" Text="MESSAGE:" ForeColor="Black"></asp:Label>
                        </td>
                        <td style="padding:5px;text-align:left">
                            <asp:TextBox ID="txtMessage" runat="server" Height="38px" Width="311px" 
                             Font-Size="20px" Enabled ="false"
                                MaxLength="16"></asp:TextBox>
                        </td>
                    </tr>
                    
                    
                    <tr>
                        <td id="td3" runat="server" style="text-align:right;">
                           <asp:Label ID="lblArea" runat="server" Text="AREA:" ForeColor="Black"></asp:Label>
                        </td>
                        <td style="padding:5px;text-align:left">
                            <asp:DropDownList ID="ddlArea" runat="server" Height="20px" Width="135px">
                            </asp:DropDownList>
                        </td>
                        
                    </tr>
                    
                    
                    
                    
                    <tr>
                        <td height="10px"></td>
                        <td>
                            <table>
                            <tr>
                                <td>RFID Count</td>
                                <td>&nbsp;
                                    <asp:TextBox ID="txtRFIDTotalcount" runat="server" Width="54px" Height="26px" 
                                        BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td>&nbsp;&nbsp;&nbsp;</td>
                                <td>Total Qty</td>
                                <td> &nbsp;<asp:TextBox ID="txtTotalQty" runat="server" Width="54px" Height="26px" 
                                        BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox></td>
                            </tr>
                        </table>
                        </td>
                    </tr>
                    
                    
                    
                    
                    <tr>
                        <td colspan= "2" style="text-align:center">
                        <div style="overflow:auto; height:150px; width:450px; overflow:scroll;" >
                            <asp:GridView ID="gvLotRfidPairing" runat="server" CellPadding="4" ForeColor="#333333" 
                            BorderStyle="Solid"
                            AutoGenerateColumns="False"
                            Font-Size="12px" Font-Bold="false" OnRowDeleting="OnRowDeleting">
            
                        <Columns>
                        
                        <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                CommandArgument='<%# Bind("RefNo") %>' Text="Delete"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                        
                        <asp:BoundField HeaderText="RFIDTAG"   DataField="RFIDTag">
                            <HeaderStyle Width="150px">
                              </HeaderStyle>
                         </asp:BoundField>	
                        <asp:BoundField HeaderText="PARTCODE"   DataField="PartCode" >
                                <HeaderStyle Width="150px">
                                  </HeaderStyle>
                             </asp:BoundField>	   
                        <asp:BoundField HeaderText="LOT NO."   DataField="LotNo">
                                <HeaderStyle Width="130px">
                                  </HeaderStyle>
                             </asp:BoundField>	     
                             
                        <asp:BoundField HeaderText="REF NO."   DataField="RefNo">
                                <HeaderStyle Width="130px">
                                  </HeaderStyle>
                             </asp:BoundField>
                             
                        <asp:BoundField HeaderText="QTY"   DataField="Qty">
                                <HeaderStyle Width="130px">
                                  </HeaderStyle>
                             </asp:BoundField>
                             
                        <asp:BoundField HeaderText="REMARKS"   DataField="Remarks">
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
                    
                        <asp:Button ID="btnClear" runat="server" Text="CLEAR" Height="35px" 
                            Width="70px" CssClass="buttonStyle" Visible="True" 
                            onclick="btnClear_Click"/>
                        
                        <asp:Button ID="btnSave" runat="server" Text="SAVE" Height="35px" 
                            Width="70px" CssClass="buttonStyle" Visible="True" 
                            onclick="btnSave_Click"/>
                            <div style ="display:none">
                            <asp:Button ID="btnLot" runat="server" Text="SAVE" Height="35px" Width="70px" CssClass="buttonStyle" 
                            onclick="btnLot_Click" />
                            <asp:Button ID="btnRFID" runat="server" Text="SAVE" Height="35px" Width="70px" CssClass="buttonStyle" 
                            onclick="btnRFID_Click" />
                            </div>
                    </td>
                </tr>
                
                
                
                
                </table>
            </div>
       
        <asp:Button ID="btnOrderSlipNo" runat="server" OnClick="btnsubmit_click" Text="Button" style="display:none;"/>
    </div>
    
    <cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
     <script type="text/javascript">
    function GetKeyCode(evt) {
        var keyCode;
        if (evt.keyCode == 13) {
            alert('asdasdasdasd');
//            __doPostBack('btnOrderSlipNo','');
        }
    }
    </script>


</asp:Content>