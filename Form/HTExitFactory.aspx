﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="HTExitFactory.aspx.cs" Inherits="FGWHSEClient.Form.HTExitFactory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script language ="javascript" type="text/javascript">

    function body_onLoad() {
        document.all("txtLocationID").focus();
    }
    
    function autosize() {
        
      if (screen.width == 480) {
                document.write ('<body style="zoom: 200%">');
                //onclick="document.location.href='Scan.aspx';"
                document.location.href = 'ContainerAllocationHT.aspx';
          }
         else
         {
             document.location.href = 'ContainerAllocation.aspx';
         }
    }
</script>


<div style =" left:40px;top:110px; font-weight:600;">

<br />
&nbsp;&nbsp;EXIT FACTORY
<br /><br />

        <div class="divInputContainer">
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
