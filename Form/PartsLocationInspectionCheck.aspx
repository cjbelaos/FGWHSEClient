<%@ Page Language="C#" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="PartsLocationInspectionCheck.aspx.cs" Inherits="FGWHSEClient.Form.PartsLocationInspectionCheck" Title="Parts Inspection Location Check" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style =" left:40px;top:110px; font-weight:600;">
<br />
&nbsp;&nbsp;<asp:Label ID="lblHeader" runat="server">PARTS LOCATION AND INSPECTION CHECK</asp:Label>
<br /><br />
      <asp:Panel id="pnlPassed" runat="server" style="display:block" DefaultButton="Button1">
      <table align="left" width="470px" height="360px" bordercolordark="#006666" frame="border" cellpadding="0px" cellspacing="0px" style='border-color:black;border-width:thin; border-style:solid;'>
             <tr align="center" style="background-color:Silver">
                 <td align="center" height="15PX" class="style4"> 
                  <asp:Label ID="Label4" runat="server" Font-Names="Calibri" Font-Size="15pt"  ForeColor="#2F4F4F"
                        Text="SCAN DN" Width="400PX"></asp:Label>
                   <br />
                   <asp:TextBox ID="txtDN" Height="35px"  style="text-align: center"
                            Width="290px" Font-Bold="True" Font-Size="18pt" runat="server" 
                            MaxLength="15" AutoPostBack="True"
                            BorderStyle="Solid" BorderWidth="1px" 
                         ontextchanged="txtDN_TextChanged"  ></asp:TextBox> 
                     <asp:Button ID="btnClear" runat="server" Text="CLEAR" Height="37px" 
                         CssClass="buttonStyle" ForeColor="White" onclick="btnClear_Click" /> 
                         <asp:Button ID="Button1" runat="server" Text="CLEAR" Height="37px" 
                         CssClass="buttonStyle" ForeColor="White" Visible ="false" 
                         onclick="Button1_Click" />       
                 </td>
            </tr>
            <tr align="center" style="background-color:Silver">
                 <td align="center" height="15PX" class="style4"> 
                  <asp:Label ID="Label1" runat="server" Font-Names="Calibri" Font-Size="15pt"  ForeColor="#2F4F4F"
                        Text="SCAN LOT LABEL QR" Width="400PX"></asp:Label>
                   <br />
                   <asp:TextBox ID="txtLotQR" Height="35px"  style="text-align: center"
                            Width="450px" Font-Bold="True" Font-Size="18pt" runat="server" 
                            AutoPostBack="True"  
                            BorderStyle="Solid" BorderWidth="1px" 
                         ontextchanged="txtLotQR_TextChanged"  ></asp:TextBox>
                 </td>
            </tr>
            
            <tr>
                <td class="style4">
                    <center>
                                     <center>
                                     <asp:Label ID="lblPartCode" runat="server" Font-Names="Calibri" Font-Size="20pt" Font-Bold="true" ForeColor="red"></asp:Label>
                                     <br />
                                     <asp:Label ID="Label2" runat="server" Font-Names="Calibri" Font-Size="12pt"  ForeColor="#2F4F4F" Text="PART DESCRIPTION" ></asp:Label>
                                     </center>


                                      <div  id="partname" style=" border:1; background-color:InfoBackground ; border-style:groove; width:415px; height:100px">
                                       <center>
                                          <asp:Label ID="lblPartname" runat="server" Font-Names="Calibri" Font-Size="18pt"  ForeColor="black" ></asp:Label>
                                      </center>
                                      </div> 
 

                        <table>
                            <tr>
                                <td>
                                  <asp:Label ID="Label3" runat="server" Font-Names="Calibri" Font-Size="12pt"  ForeColor="#2F4F4F" Text="LOCATION" ></asp:Label>
                                </td>
                            </tr>
                        </table>
                        
                        
                                        <div id="divlocation" style=" border:1; background-color:Silver ; border-style:groove; width:415px; height:100px" runat="server" >
                                            <asp:Label ID="lblLocation" runat="server" Font-Names="Calibri" Font-Size="28pt"  ForeColor="black" ></asp:Label>
                                        </div> 
                                    

                    </center>
                    
                </td>
            </tr>  
            <tr>
                <td class="style4">
                    <cc1:msgBox ID="msgBox" runat="server" /></cc1:msgbox>
                    <cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
                </td>
            </tr>
        </table>
        </asp:Panel>

</div>
</asp:Content>

