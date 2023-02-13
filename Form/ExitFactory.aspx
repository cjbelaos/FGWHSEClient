<%@ Page Title="" Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="ExitFactory.aspx.cs" Inherits="FGWHSEClient.Form.ExitFactory" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language ="javascript" type="text/javascript">

    function body_onLoad() {
        document.all("txtLocationID").focus();
    }
    
    
</script>


<div style =" left:40px;top:110px; font-weight:600;">

<br />
&nbsp;&nbsp;EXIT FACTORY
<br /><br />

        <div class="divInput">
        <table style ="text-align:left">
               
                <tr style ="color:Black"><td style="padding:5px; text-align:right">
                        Container No.
                    </td>
                    <td style="padding:5px">
                        <asp:TextBox ID="txtContainerNo" runat="server" Width="200px" Height="25px" Font-Size="20px"></asp:TextBox></td></tr><tr>
                <td></td>
                <td>
                    <asp:RadioButtonList ID="rdoAllocate" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">Set Off</asp:ListItem><asp:ListItem>Return</asp:ListItem></asp:RadioButtonList></td></tr><tr><td></td></tr>
                 
                 
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
          
          
        </div>
 

<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>

