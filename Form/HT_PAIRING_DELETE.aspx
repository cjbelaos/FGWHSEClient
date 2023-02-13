<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Form/HTMasterPalletMonitoring.Master"  CodeBehind="HT_PAIRING_DELETE.aspx.cs" Inherits="FGWHSEClient.Form.HT_PAIRING_DELETE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script language="javascript" type="text/javascript">
 function onScanLot(e) 
 {
  
    if (e.keyCode == 13) 
    {
        document.getElementById('<%= "ctl00_ContentPlaceHolder1_btnLot" %>').click();
    }
    
 }
 
 
 function onScanRFID(e) 
 {
  
    if (e.keyCode == 13) 
    {
        document.getElementById('<%= "ctl00_ContentPlaceHolder1_btnRFID" %>').click();
    }
    
 }
</script>

<div style =" left:40px;top:110px; font-weight:600;">
<table>
<tr>
    <td>&nbsp;&nbsp;PAIRED LOT-RFID DELETION</td>
</tr>
<tr style="font-size:small">
    <td></td>
    <td>
        <table>
            <tr>
                <td>&nbsp;&nbsp;&nbsp;</td>
                <td></td>
               
            </tr>
            
        </table>
    </td>
</tr>
</table>
        <div style="display:none;">
        
            <asp:Button ID="btnLot" runat="server" Text="" onclick="btnLot_Click" />
            <asp:Button ID="btnRFID" runat="server" Text="" onclick="btnRFID_Click" />
            
        </div>
        <div class="divInputContainer">
        <table style ="text-align:left; font-size:small">
        
              <tr>
                <td class="style4">
                    SCAN RFID :
                </td>
                <td>
                    <asp:TextBox ID="txtScannedRFID" runat="server" Height="38px" Width="311px" onkeyup="onScanRFID(event)"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td class="style4">
                    SCAN LOT QR :
                </td>
                <td>
                    <asp:TextBox ID="txtLot" runat="server" Height="38px" Width="311px" onkeyup="onScanLot(event)"></asp:TextBox>
                </td>
              </tr>
             
             
              
              
              <tr>
                <td class="style4">
                    MESSAGE :
                </td>
                <td>
                    <asp:TextBox ID="txtMSG" runat="server" Height="38px" Width="311px" 
                        BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                </td>
              </tr>
              <tr style ="text-align:center">
                <td></td>
                <td>
                    <table>
                        <tr>
                            <td><asp:Button ID="btnClear" runat="server" Text="CLEAR" onclick="btnClear_Click" /></td>
                            <td>&nbsp;&nbsp;&nbsp;</td>
                            <td><asp:Button ID="btnDELETE" runat="server" Text="DELETE" 
                                    onclick="btnDELETE_Click"  /></td>
                        </tr>
                    </table>
                
                </td>
              </tr>
          </table>
         </div>
          
          
        </div>

</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <link type="text/css" rel="Stylesheet" href="../App_Themes/Stylesheet/Main.css"/>  
    
     

    
    <style type="text/css">
        .style4
        {
            width: 146px;
        }
    </style>





</asp:Content>