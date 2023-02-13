<%@ Page Title="" Language="C#" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="HTODAllocation.aspx.cs" Inherits="FGWHSEClient.Form.HTODAllocation" %>

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
&nbsp;&nbsp;OD ALLOCATION
<br /><br />

        <div class="divInput">
        <table style ="text-align:left">
                <tr style ="color:Black">
                   <td style="padding:5px; text-align:right;">
                        Pallet No.
                    </td>
                    <td style="padding:5px;text-align:left">
                        <asp:TextBox ID="txtPalletNo" runat="server" Width="200px" Height="25px" AutoPostBack="true"
                            MaxLength=8 Font-Size="20px" ontextchanged="txtPalletNo_TextChanged" ></asp:TextBox>
                    </td>
                </tr>
                <tr style ="color:Black"><td style="padding:5px; text-align:right">
                        O/D No.
                    </td>
                    <td style="padding:5px">
                        <asp:TextBox ID="txtODNo" runat="server" Width="200px" Height="25px" AutoPostBack="true"
                            Font-Size="20px" ontextchanged="txtODNo_TextChanged"></asp:TextBox></td></tr>
                        
                
                <tr style ="color:Black"><td style="padding:5px; text-align:right">
                       Case No.
                    </td>
                    <td style="padding:5px">
                        <asp:TextBox ID="txtCaseNo" runat="server" Width="200px" Height="25px" AutoPostBack="true"
                            Font-Size="20px" ontextchanged="txtCaseNo_TextChanged"></asp:TextBox></td></tr>
                                
                        
                <tr>
                <td></td>
                <td>
                    <asp:RadioButtonList ID="rdoAllocate" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">Allocate</asp:ListItem><asp:ListItem>Unallocate</asp:ListItem></asp:RadioButtonList></td></tr><tr><td></td></tr>
                 
                 
                <tr>
                
                
                <td></td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="SAVE" Height="35px" Width="70px" onclick="btnSave_Click"/>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnClear" runat="server" Text="CLEAR" Height="35px" Width="70px" onclick="btnClear_Click"/>
                    
                   
                </td>
                </tr>
                
          </table>
         </div>
          <br />
          
 
<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
 TargetControlID="txtCaseNo"  FilterType="Numbers" />
 
 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
 TargetControlID="txtPalletNo"  FilterType="Numbers" />
 
 
<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>

</asp:Content>