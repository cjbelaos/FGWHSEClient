<%@ Page Title="" Language="C#" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="HTCartonInformation.aspx.cs" Inherits="FGWHSEClient.Form.HTCartonInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language ="javascript" type="text/javascript">

    function body_onLoad() {
        document.all("txtLocationID").focus();
    }
    
   

    
    

     function selectText() {
         document.getElementById("<%=txtPalletNo.ClientID %>").select();
     }
     
</script>


<div style =" left:40px;top:110px; font-weight:600;">

<br />
&nbsp;&nbsp;CARTON INFORMATION
<br /><br />
<div style="position:fixed; left:300px;top:131px;">
    SCAN COUNT:&nbsp;<asp:Label ID="lblScanCount" runat="server" Text="0"></asp:Label>

</div>

<div style="display:none">
    <asp:TextBox ID="txtPartcodeCheck" runat="server"></asp:TextBox>
</div>

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
          <br />
          <div style =" font-size:xx-small ; min-height:200px; margin-left:10px; width:363px; border-style:solid; border-color:Gray;border-right-width:thin;border-bottom-width:thin;">
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

                                if (x % 2 == 0)
                                {
                                    Response.Write("</tr>");
                                }
                                
                            }

                            Response.Write("</table>"); 
                        %>
          
          </div>
        </div>
 
 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
 TargetControlID="txtPalletNo"  FilterType="Numbers" />
 
 
 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
 TargetControlID="txtPartCode"  FilterType="Custom" FilterMode="InvalidChars" InvalidChars="+" />
 
 
<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>