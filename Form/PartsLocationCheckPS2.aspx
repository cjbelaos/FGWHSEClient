<%@ Page Language="C#" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="PartsLocationCheckPS2.aspx.cs" Inherits="FGWHSEClient.Form.PartsLocationCheckPS2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
 window.history.forward(1);
 </script> 
 
 
    <div style =" left:40px;top:110px; font-weight:600;">
    <center><asp:Label ID="lblHeader" runat="server" style="font-size:20px">PARTS LOCATION CHECK (PS2)</asp:Label>
      </center><asp:Panel id="pnlPassed" runat="server" style="display:block" >
      <table align="left" width="470px" height="260px" bordercolordark="#006666" frame="border" cellpadding="0px" cellspacing="0px" style='border-color:black;border-width:thin; border-style:solid;'>
            <tr align="center" style="background-color:Silver">
                 <td align="center" height="15PX"> 
                  <asp:Label ID="Label1" runat="server" Font-Names="Calibri" Font-Size="15pt"  ForeColor="#2F4F4F"
                        Text="SCAN PARTCODE" Width="400PX"></asp:Label>
                   <br />
                   <asp:TextBox ID="txtPartCode" Height="35px"  style="text-align: center"
                            Width="290px" Font-Bold="True" Font-Size="18pt" runat="server" 
                            MaxLength="15" AutoPostBack="True" ontextchanged="txtPartCode_TextChanged" 
                            BorderStyle="Solid" BorderWidth="1px"  ></asp:TextBox>
                 </td>
            </tr>
            
            <tr>
                <td>
                    <center>
                                     <center>
                                     <asp:Label ID="lblPartCode" runat="server" Font-Names="Calibri" Font-Size="18pt" Font-Bold="true" ForeColor="red"></asp:Label>
                                     <br />
                                     <asp:Label ID="Label2" runat="server" Font-Names="Calibri" Font-Size="11pt"  ForeColor="#2F4F4F" Text="PART DESCRIPTION" ></asp:Label>
                                     </center>


                                      <div  id="partname" style=" border:1; background-color:InfoBackground ; border-style:groove; width:415px; height:70px">
                                       <center>
                                          <asp:Label ID="lblPartname" runat="server" Font-Names="Calibri" Font-Size="14pt"  ForeColor="black" ></asp:Label>
                                      </center>
                                      </div> 
 


                        <table>
                        <tr>
                        <td><asp:Label ID="Label3" runat="server" Font-Names="Calibri" Font-Size="11pt"  ForeColor="#2F4F4F" Text="LANE ID" ></asp:Label></td>
                        </tr>
                         <tr>
                        <td>
                            <div id="divlocation" style=" border:1; background-color:#ffc000 ; border-style:groove; width:430px; height:30px" runat="server" >
                             <asp:Label ID="lblLocation" runat="server" Font-Names="Calibri" Font-Size="18pt"  ForeColor="black" ></asp:Label>
                            </div> 
                        </td>
                        </tr>
                        </table>
                        
                        
                                        
                        <table>
                         <tr>
                        <td style="width:210px"><asp:Label ID="Label4" runat="server" Font-Names="Calibri" Font-Size="11pt"  ForeColor="#2F4F4F" Text="STOCKTAKING LOCATION" ></asp:Label></td>
                        <td style="width:210px"><asp:Label ID="Label6" runat="server" Font-Names="Calibri" Font-Size="11pt"  ForeColor="#2F4F4F" Text="INSPECTION UPDATE" ></asp:Label></td>
                        </tr>
                        <tr>
                        <td>
                            <div id="div1" style=" border:1; background-color:#f2f2f2 ; border-style:groove; width:210px; height:30px" runat="server" >
                             <asp:Label ID="lblLocationST" runat="server" Font-Names="Calibri" Font-Size="14pt"  ForeColor="Black" ></asp:Label>
                            </div> 
                        </td>
                        <td>
                            <div id="divInspection" style=" border:1; background-color:#f2f2f2 ; border-style:groove; width:210px; height:30px" runat="server" >
                             <asp:Label ID="lblInspection" runat="server" Font-Names="Calibri" Font-Size="18pt"  ForeColor="RED" ></asp:Label>
                            </div> 
                        </td>
                        </tr>
                        </table>            
                                        
                                       <%--  <div id="divLocationST" style=" border:1; background-color:#ffc000 ; border-style:groove; width:415px; height:30px" runat="server" >
                                            
                                        </div> --%>
                                        
                                        <div style="margin-top:5px">
                                        <asp:Button ID="btnPartsStoring" runat="server" Text="PARTS STORING" 
                                         Width="200px" Height="50px"  onclick="btnPartsStoring_Click" 
                                                BackColor="Silver"  />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnClear" runat="server" Text="CLEAR" Width="200px" 
                                         Height="50px" BackColor="#ff8080" onclick="btnClear_Click" />
                                        </div>
                    
                        
                           
                    </center>
                    
                </td>
            </tr>  
            <tr>
                <td>
                    <cc1:msgBox ID="msgBox" runat="server" /></cc1:msgbox>
                    <cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
                    <asp:Label ID="lblEKANBANFLAG" runat="server" Font-Names="Calibri" Font-Size="18pt"  ForeColor="Black" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        </asp:Panel>

</div>










</asp:Content>  