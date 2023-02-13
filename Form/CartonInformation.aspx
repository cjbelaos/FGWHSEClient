<%@ Page Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="CartonInformation.aspx.cs" Inherits="FGWHSEClient.Form.CartonInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <div style =" left:40px;top:110px; font-weight:600;">

<br />
&nbsp;&nbsp;CARTON INFORMATION
<br /><br />
<div style="position:fixed; left:300px;top:105px;">
    SCAN COUNT:&nbsp;<asp:Label ID="lblScanCount" runat="server" Text="0"></asp:Label>

</div>


<div style="position:fixed; left:500px;top:105px;">
<asp:Button ID="btnDownload" runat="server" Text="Download Scanned" 
        onclick="btnDownload_Click" />

</div>

<div style="display:none">
    <asp:TextBox ID="txtPartcodeCheck" runat="server"></asp:TextBox>
</div>

        <table >
            <tr>
                <td style=" vertical-align:top; text-align:left">
                     <div class="divInput">
        <table style ="text-align:left">
                <tr style ="color:Black">
                   <td style="padding:5px; text-align:right;">
                        Pallet No.
                    </td>
                    <td style="padding:5px;text-align:LEFT">
                        <asp:TextBox ID="txtPalletNo" runat="server" Width="200px" Height="25px" AutoPostBack="true"
                            Font-Size="20px" MaxLength="8" ontextchanged="txtPalletNo_TextChanged"></asp:TextBox>
                        <div style=" display:none"><asp:Label ID="lblPalCount" runat="server" Text="0"></asp:Label></div>
                    </td>
                </tr>
                <tr style ="color:Black"><td style="padding:5px; text-align:right">
                        Part Code
                    </td>
                    <td style="padding:5px">
                        <asp:TextBox ID="txtPartCode" runat="server" Width="200px" Height="25px"  AutoPostBack="true"
                            Font-Size="20px" MaxLength="12" ontextchanged="txtPartCode_TextChanged"></asp:TextBox></td></tr>
                  <tr style ="color:Black"><td style="padding:5px; text-align:right">
                      Lot No and Qty
                    </td>
                    <td style="padding:5px">
                        <asp:TextBox ID="txtLotNoAndQty" runat="server" Width="200px" Height="25px"  AutoPostBack="true"
                            Font-Size="20px" MaxLength="30" ontextchanged="txtLotNoAndQty_TextChanged"></asp:TextBox></td></tr>
                 <tr>
                <td></td>
                <td>
                    <asp:RadioButtonList ID="rdoReg" runat="server" RepeatDirection="Horizontal" 
                        onselectedindexchanged="rdoReg_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Selected ="true" Value ="0">Register</asp:ListItem><asp:ListItem Value ="1">Remove</asp:ListItem></asp:RadioButtonList></td></tr><tr><td></td></tr>
                 
                 
                <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="SAVE" Height="35px" Width="61px" 
                        onclick="btnSave_Click"/>
                     &nbsp;
                    <asp:Button ID="btnDelete" runat="server" Text="DELETE" Height="35px" 
                        Width="61px" onclick="btnDelete_Click"/>
                     &nbsp;
                    <asp:Button ID="btnClear" runat="server" Text="CLEAR" Height="35px" 
                        Width="55px" onclick="btnClear_Click"/>
                    
                </td>
                </tr>
                
          </table>  
         </div>
                </td>
                <td style=" vertical-align:top; text-align:left; ">
                    <div style=" border-style:solid; border-color:gray; border-bottom-width:thin;border-right-width:thin; margin-left: 50px; width:630px; min-height:193px;  ">
                        <% 
                            Response.Write("<table>");
                            for (int x = 1; x <= dtParCodeLotNoQty.DefaultView.Count; x++)
                            {
                                if (x == 1 || (x-1) % 10 == 0)
                                {
                                    Response.Write("<tr>");
                                }

                                Response.Write("<td style =  font-size:x-small;border-style:solid;border-color:black;border-width:thin;padding:5px;width:134px;text-align:center>");
                                string strParCodeLotNoQty = dtParCodeLotNoQty.DefaultView[x - 1]["ParCodeLotNoQty"].ToString();
                                Response.Write(strParCodeLotNoQty);
                                Response.Write("</td>");

                                if (x % 4 == 0)
                                {
                                    Response.Write("</tr>");
                                }
                                
                            }

                            Response.Write("</table>"); 
                        %>
                    </div>
                </td>
            </tr>
        </table>      
          
</div>

<div id = "dvPrint" runat = "server">
    
    
        <asp:GridView ID="grdExport" runat="server">                   
        </asp:GridView>
    
        
    </div>
<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
 TargetControlID="txtPalletNo"  FilterType="Numbers" />
 
 
 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
 TargetControlID="txtPartCode"  FilterType="Custom" FilterMode="InvalidChars" InvalidChars="+" />

<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>

</asp:Content>
