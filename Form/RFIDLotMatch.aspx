<%@ Page Language="C#" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="RFIDLotMatch.aspx.cs" Inherits="FGWHSEClient.Form.RFIDLotMatch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript">
 window.history.forward(1);
 </script> 

    <div style =" left:40px;top:110px; font-weight:600;">
    <center><asp:Label ID="lblHeader" runat="server" style="font-size:18px">RFID LOT LABEL PAIRING</asp:Label>
      </center><asp:Panel id="pnlPassed" runat="server" style="display:block" >
      <table align="left" width="470px" height="260px" bordercolordark="#006666" frame="border" cellpadding="0px" cellspacing="0px" style='border-color:black;border-width:thin; border-style:solid;'>
            <tr>
                <td>
                    <center>
                                     <center>
                                     
                                     <br />
                                     <asp:Label ID="Label2" runat="server" Font-Names="Calibri" Font-Size="14pt"  ForeColor="#2F4F4F" Text="RFID TAG NO" ></asp:Label>
                                     <br />
                                     <asp:TextBox ID="txtRFID" Height="35px"  style="text-align: center"
                                        Width="360px" Font-Bold="True" Font-Size="18pt" runat="server" 
                                             AutoPostBack="True" BorderStyle="Solid" BorderWidth="1px" ontextchanged="txtRFID_TextChanged"  ></asp:TextBox>
                                        
                                        <br />
                                        <br />
                                      <asp:Label ID="Label3" runat="server" Font-Names="Calibri" Font-Size="14pt"  ForeColor="#2F4F4F" Text="LOT LABEL REF NO" ></asp:Label>
                                     <br />
                                     <asp:TextBox ID="txtRefNo" Height="35px"  style="text-align: center" Width="360px" 
                                             Font-Bold="True" Font-Size="18pt" runat="server"  AutoPostBack="True"  
                                             BorderStyle="Solid" BorderWidth="1px" ontextchanged="txtRefNo_TextChanged"  ></asp:TextBox>

                                     </center>
                        
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                  <asp:Label ID="Label4" runat="server" Font-Names="Calibri" Font-Size="14pt"  ForeColor="#2F4F4F" Text="MESSAGE" ></asp:Label>
                                                </td>
                                            </tr>
                                        </table>   
                                        
                                      <div  id="partname" style=" border:1; background-color:InfoBackground ; border-style:groove; width:360px; height:85px">
                                          <asp:Label ID="lblMessage" runat="server" Font-Names="Calibri" Font-Size="12pt"  ForeColor="black" ></asp:Label>
                                      </div>          
                                        
                                        <br />
                                        <br />
                        <asp:Button ID="btnSave" runat="server" Text="SAVE" Width="172px" Height="50px" 
                                         BackColor="Silver" onclick="btnSave_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnClear" runat="server" Text="CLEAR" Width="172px" Height="50px" 
                                         BackColor="#ff8080" onclick="btnClear_Click" />

                    </center>
                    
                </td>
            </tr>  
            <tr>
                <td>
                    <cc1:msgBox ID="msgBox" runat="server" /></cc1:msgbox>
                    <cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
                     <asp:HiddenField ID="hiddenLotID" runat="server" />  
                     <asp:HiddenField ID="hiddenOldRFID" runat="server" />  
                     <asp:HiddenField ID="hiddenOldRefNo" runat="server" /> 
                     <asp:HiddenField ID="hiddenOldPartcode" runat="server" />  
                </td>
            </tr>
        </table>
        </asp:Panel>

</div>










</asp:Content>  
