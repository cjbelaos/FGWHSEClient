<%@ Page Title="" Language="C#" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="PartsReturnHolmes.aspx.cs" Inherits="FGWHSEClient.Form.PartsReturnHolmes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <script type="text/javascript">
 window.history.forward(1);
 </script> 


    <div style =" left:40px;top:110px; font-weight:600;">
    <center><asp:Label ID="lblHeader" runat="server" style="font-size:18px">HOLMES PARTS RETURN</asp:Label>
      </center><asp:Panel id="pnlPassed" runat="server" style="display:block" >
      <table align="left" width="470px" height="260px" bordercolordark="#006666" frame="border" cellpadding="0px" cellspacing="0px" style='border-color:black;border-width:thin; border-style:solid;'>
          
            <tr>
                <td height="42px">&nbsp;<asp:Label ID="Label3" runat="server" Font-Names="Calibri" Font-Size="12pt"  ForeColor="#2F4F4F" Text="REF NO" ></asp:Label></td>
                <td> &nbsp;<asp:TextBox ID="txtLotNo" Height="35px"  style="text-align: left" Width="360px" 
                                             Font-Bold="True" Font-Size="18pt" runat="server"  AutoPostBack="True"  
                                             BorderStyle="Solid" BorderWidth="1px" ontextchanged="txtLotNo_TextChanged"  ></asp:TextBox></td>

            </tr>

            <tr>
               <td height="42px"> &nbsp;<asp:Label ID="Label6" runat="server" Font-Names="Calibri" Font-Size="11pt"  ForeColor="#2F4F4F" Text="PARTCODE" ></asp:Label></td>
                <td> &nbsp;<asp:TextBox ID="txtPartCode" Height="35px"  style="text-align: left"
                                        Width="360px" Font-Bold="True" Font-Size="18pt" runat="server" 
                                             AutoPostBack="True" BorderStyle="Solid" BorderWidth="1px"  ReadOnly="True"></asp:TextBox></td>

          </tr>


            <tr>
               <td height="42px">&nbsp;<asp:Label ID="Label2" runat="server" Font-Names="Calibri" Font-Size="11pt"  ForeColor="#2F4F4F" Text="PARTNAME" ></asp:Label></td>
                <td>&nbsp;<asp:TextBox ID="txtPartName" Height="35px"  style="text-align: left"
                                        Width="360px" Font-Bold="True" Font-Size="18pt" runat="server" 
                                             AutoPostBack="True" BorderStyle="Solid" BorderWidth="1px"  ReadOnly="True"></asp:TextBox>
                </td>

          </tr>

          <tr>
               <td height="42px">&nbsp;<asp:Label ID="Label7" runat="server" Font-Names="Calibri" Font-Size="12pt"  ForeColor="#2F4F4F" Text="LOT NO" ></asp:Label></td>
                <td>&nbsp;<asp:TextBox ID="txtLot" Height="35px"  style="text-align: left" Width="360px" 
                                             Font-Bold="True" Font-Size="18pt" runat="server"  AutoPostBack="True"  
                                             BorderStyle="Solid" BorderWidth="1px" ReadOnly="True"></asp:TextBox>
                </td>

          </tr>

          <tr>
               <td height="42px">&nbsp;<asp:Label ID="Label8" runat="server" Font-Names="Calibri" Font-Size="12pt"  ForeColor="#2F4F4F" Text="QTY" ></asp:Label></td>
                <td>&nbsp;<asp:TextBox ID="txtQty" Height="35px"  style="text-align: left" Width="360px" 
                                             Font-Bold="True" Font-Size="18pt" runat="server"  AutoPostBack="True"  
                                             BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" ></asp:TextBox>
                </td>

          </tr>


          


            <tr>
               <td height="42px">&nbsp;<asp:Label ID="Label1" runat="server" Font-Names="Calibri" Font-Size="12pt"  ForeColor="#2F4F4F" Text="HOLMES PROCESS" ></asp:Label></td>
                <td> &nbsp;<asp:TextBox ID="txtHolmesProcess" Height="35px"  style="text-align: left" Width="360px" 
                                             Font-Bold="True" Font-Size="18pt" runat="server"  AutoPostBack="True"  
                                             BorderStyle="Solid" BorderWidth="1px"  ></asp:TextBox></td>

          </tr>


            <tr>
               <td height="42px"> &nbsp;<asp:Label ID="Label5" runat="server" Font-Names="Calibri" Font-Size="12pt"  ForeColor="#2F4F4F" Text="REASON" ></asp:Label></td>
                <td>&nbsp;<asp:DropDownList ID="ddlReason" runat="server" Height="35px" Width="360px" Font-Size="18pt" >
                                             <asp:ListItem>Rework</asp:ListItem>
                                              <asp:ListItem>Disposal</asp:ListItem>
                                              <asp:ListItem>ASP</asp:ListItem>
                                               <asp:ListItem>EOL</asp:ListItem>
                                              <asp:ListItem>Sorting</asp:ListItem>
                                              <asp:ListItem>Others</asp:ListItem>
                                              <asp:ListItem>Reprint_Lot</asp:ListItem>

                                           </asp:DropDownList></td>

          </tr>

             <tr>
               <td height="42px">&nbsp;<asp:Label ID="Label9" runat="server" Font-Names="Calibri" Font-Size="12pt"  ForeColor="#2F4F4F" Text="OTHER REMARKS" ></asp:Label></td>
                <td> &nbsp;<asp:TextBox ID="txtRemarks" Height="35px"  style="text-align: left" Width="360px" 
                                             Font-Bold="True" Font-Size="18pt" runat="server"  AutoPostBack="True"  
                                             BorderStyle="Solid" BorderWidth="1px"  ></asp:TextBox></td>

          </tr>


            <tr>
               <td></td>
                <td></td>

          </tr>



            <tr >
                <td colspan="2">
                    <center>
                                        <table>
                                            <tr>
                                                <td>
                                                  <asp:Label ID="Label4" runat="server" Font-Names="Calibri" Font-Size="10pt"  ForeColor="#2F4F4F" Text="MESSAGE" ></asp:Label>
                                                </td>
                                            </tr>
                                        </table>   
                                        
                                      <div  id="partname" style=" border:1; background-color:InfoBackground ; border-style:groove; width:450px; height:40px">
                                          <asp:Label ID="lblMessage" runat="server" Font-Names="Calibri" Font-Size="12pt"  ForeColor="black" ></asp:Label>
                                      </div>          
                                        
                                        
                       
                        <asp:Button ID="btnClear" runat="server" Text="CLEAR" Width="172px" Height="50px" 
                                         BackColor="#ff8080" onclick="btnClear_Click" />
                         &nbsp;&nbsp; 
                                     <asp:Button ID="btnSave" runat="server" BackColor="Silver" Height="50px" onclick="btnSave_Click" Text="SAVE" Width="172px" Enabled="false" OnClientClick="this.disabled=true;"  UseSubmitBehavior="false"/>

                    </center>
                    
                </td>
            </tr>  
            <tr>
                <td>
                    <cc1:msgBox ID="msgBox" runat="server" /></cc1:msgbox>
                    <cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
                    <asp:HiddenField ID="hiddenEKANBAN" runat="server" />  
                    <asp:HiddenField ID="hiddenLotID" runat="server" />  
                </td>
            </tr>
        </table>
        </asp:Panel>

</div>










</asp:Content>  
