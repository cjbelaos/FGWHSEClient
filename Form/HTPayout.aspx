<%@ Page Title="" Language="C#" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="HTPayout.aspx.cs" Inherits="FGWHSEClient.Form.HTPayout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
 window.history.forward(1);
 </script> 


    <div style =" left:40px;top:110px; font-weight:600;">
    <center><asp:Label ID="lblHeader" runat="server" style="font-size:18px">PAYOUT / DELIVERY</asp:Label>
      </center><asp:Panel id="pnlPassed" runat="server" style="display:block" >
      <table align="left" width="470px"  bordercolordark="#006666" frame="border" cellpadding="0px" cellspacing="0px" style='border-color:black;border-width:thin; border-style:solid;'>
          
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

           <tr style="display:none">
               <td height="42px">&nbsp;<asp:Label ID="Label7" runat="server" Font-Names="Calibri" Font-Size="12pt"  ForeColor="#2F4F4F" Text="LOT NO" ></asp:Label></td>
                <td>&nbsp;<asp:TextBox ID="txtLot" Height="35px"  style="text-align: left" Width="360px" 
                                             Font-Bold="True" Font-Size="18pt" runat="server"  AutoPostBack="True"  
                                             BorderStyle="Solid" BorderWidth="1px" ReadOnly="True"></asp:TextBox>
                </td>

          </tr>

          <tr style="display:none">
               <td height="42px">&nbsp;<asp:Label ID="Label8" runat="server" Font-Names="Calibri" Font-Size="12pt"  ForeColor="#2F4F4F" Text="QTY" ></asp:Label></td>
                <td>&nbsp;<asp:TextBox ID="txtQty" Height="35px"  style="text-align: left" Width="360px" 
                                             Font-Bold="True" Font-Size="18pt" runat="server"  AutoPostBack="True"  
                                             BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" ></asp:TextBox>
                </td>

          </tr>



            


            <tr>
               <td></td>
                <td></td>

          </tr>


          <tr style="height:180px; vertical-align:top">
              <td colspan="2">

                    <div class="grdClass" style= "overflow: auto; height:180px; border-style:solid; border-width:1px;">
                      <asp:GridView ID="grdPo" runat="server"
                        BorderStyle="None" AutoGenerateColumns="False" 
                        BorderWidth="1px" CellPadding="0"  
                        style="text-align: center; margin-left: 1px; margin-top: 0px" 
                        Width="400px" Font-Size="11px" OnRowDeleting="grdPo_RowDeleting" 
                        >

                        <PagerSettings PageButtonCount="5" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#c3bebe" Font-Bold="True" ForeColor="black" HorizontalAlign=Center />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />


                        <Columns>


                        <asp:CommandField ShowDeleteButton="True" ButtonType="Link" DeleteText="<img src='../Image/Delete.png' alt='Delete this Record' border='0' />" ItemStyle-Width="10">
                        </asp:CommandField>

                                    <asp:BoundField DataField="LOT" HeaderText="LOT"  >
                                    <ItemStyle Width="120px" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="ReferenceNo" HeaderText="ReferenceNo" >
                                    <ItemStyle Width="150px"/>
                                    </asp:BoundField>

                                   
                                    <asp:BoundField DataField="QTY" HeaderText="QTY" >
                                    <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    
                                                  
                                    </Columns>
                                            
                                    </asp:GridView>
                      </div>

              </td>


          </tr>

          <tr>

               <td height="42px">&nbsp;<asp:Label ID="Label1" runat="server" Font-Names="Calibri" Font-Size="12pt"  ForeColor="#2F4F4F" Text="Total Count" ></asp:Label></td>
                <td>&nbsp;<asp:TextBox ID="txtTotalCount" Height="35px"  style="text-align: left" Width="360px" 
                                             Font-Bold="True" Font-Size="18pt" runat="server"  AutoPostBack="True"  
                                             BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" ></asp:TextBox>
                </td>
          </tr>

           <tr>

               <td height="42px">&nbsp;<asp:Label ID="Label5" runat="server" Font-Names="Calibri" Font-Size="12pt"  ForeColor="#2F4F4F" Text="Total Qty" ></asp:Label></td>
                <td>&nbsp;<asp:TextBox ID="txtTotalQty" Height="35px"  style="text-align: left" Width="360px" 
                                             Font-Bold="True" Font-Size="18pt" runat="server"  AutoPostBack="True"  
                                             BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" ></asp:TextBox>
                </td>
          </tr>



            <tr>
                <td colspan="2">
                    <center>
                                        <table style="display:none" >
                                            <tr>
                                                <td>
                                                  <asp:Label ID="Label4" runat="server" Font-Names="Calibri" Font-Size="14pt"  ForeColor="#2F4F4F" Text="MESSAGE" ></asp:Label>
                                                </td>
                                            </tr>
                                        </table>   
                                        
                                      <div  id="partname" style=" border:1; background-color:InfoBackground ; border-style:groove; width:400px; height:70px; display:none">
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
