<%@ Page Title="" Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="ContainerAllocation.aspx.cs" Inherits="FGWHSEClient.Form.ContainerAllocation" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script language ="javascript" type="text/javascript">

    function body_onLoad() {
        document.all("txtLocationID").focus();
    }
    
    
</script>


<div style =" left:40px;top:110px; font-weight:600;">

<br />
&nbsp;&nbsp;CONTAINER ALLOCATION
<br /><br />

        <div class="divInputContainer">
        <table style ="text-align:left">
                <tr style ="color:Black">
                   <td style="padding:5px; text-align:right;">
                        Loading Bay
                    </td>
                    <td style="padding:5px;text-align:left">
                        <asp:TextBox ID="txtLocationID" runat="server" Width="200px" Height="25px" AutoPostBack="true"
                            Font-Size="20px" ontextchanged="txtLocationID_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr style ="color:Black"><td style="padding:5px; text-align:right">
                        Container No.
                    </td>
                    <td style="padding:5px">
                        <asp:TextBox ID="txtPalletNo" runat="server" Width="200px" Height="25px" Font-Size="20px"></asp:TextBox></td></tr><tr>
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
                     &nbsp;&nbsp;
                    <asp:Button ID="btnDeleteContainer" runat="server" Text="DELETE CONTAINER" Height="35px" Width="140px" onclick="btnDeleteContainer_Click" />
                   
                </td>
                </tr>
                
          </table>  
         </div>
          
          
        </div>
 

<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>

