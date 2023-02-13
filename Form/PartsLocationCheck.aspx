<%@ Page Title=""  Language="C#" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="PartsLocationCheck.aspx.cs" Inherits="FGWHSEClient.PartsLocationCheck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style =" left:40px;top:110px; font-weight:600;">
    <center><asp:Label ID="lblHeader" runat="server" style="font-size:12px">PARTS LOCATION CHECK</asp:Label>
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
                                     <asp:Label ID="lblPartCode" runat="server" Font-Names="Calibri" Font-Size="20pt" Font-Bold="true" ForeColor="red"></asp:Label>
                                     <br />
                                     <asp:Label ID="Label2" runat="server" Font-Names="Calibri" Font-Size="12pt"  ForeColor="#2F4F4F" Text="PART DESCRIPTION" ></asp:Label>
                                     </center>


                                      <div  id="partname" style=" border:1; background-color:InfoBackground ; border-style:groove; width:415px; height:70px">
                                       <center>
                                          <asp:Label ID="lblPartname" runat="server" Font-Names="Calibri" Font-Size="14pt"  ForeColor="black" ></asp:Label>
                                      </center>
                                      </div> 
 

                        <table>
                            <tr>
                                <td>
                                  <asp:Label ID="Label3" runat="server" Font-Names="Calibri" Font-Size="12pt"  ForeColor="#2F4F4F" Text="NORMAL LOCATION" ></asp:Label>
                                </td>
                            </tr>
                        </table>
                        
                        
                                        <div id="divlocation" style=" border:1; background-color:Silver ; border-style:groove; width:415px; height:85px" runat="server" >
                                            <asp:Label ID="lblLocation" runat="server" Font-Names="Calibri" Font-Size="24pt"  ForeColor="black" ></asp:Label>
                                        </div> 
                        <table>
                            <tr>
                                <td>
                                  <asp:Label ID="Label4" runat="server" Font-Names="Calibri" Font-Size="12pt"  ForeColor="#2F4F4F" Text="STOCKTAKING LOCATION" ></asp:Label>
                                </td>
                            </tr>
                        </table>            
                                        
                                         <div id="divLocationST" style=" border:1; background-color:Black ; border-style:groove; width:415px; height:50px" runat="server" >
                                            <asp:Label ID="lblLocationST" runat="server" Font-Names="Calibri" Font-Size="22pt"  ForeColor="White" ></asp:Label>
                                        </div> 
                                    

                    </center>
                    
                </td>
            </tr>  
            <tr>
                <td>
                    <cc1:msgBox ID="msgBox" runat="server" /></cc1:msgbox>
                    <cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
                </td>
            </tr>
        </table>
        </asp:Panel>

</div>










</asp:Content>  
    
    
    
