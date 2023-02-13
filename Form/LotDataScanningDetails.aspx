<%@ Page Language="C#" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="LotDataScanningDetails.aspx.cs" Inherits="FGWHSEClient.Form.WebForm3" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        document.onkeydown = function (evt) {
            if (navigator.userAgent.indexOf("Opera") == -1) {
                evt = evt || window.event;
            }
            DoTask(evt.keyCode);
        };
        function DoTask(keyCode) {
            switch (keyCode) {
                case 117:
                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_btnDelete" %>').click();
                    break;
                case 118:
                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_btnBack" %>').click();
                     break;
                case 119:
                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_btnClear" %>').click();
                    break;
               
            }
        }
    </script>
<div style =" left:40px;top:110px; font-weight:600;">
<br />
&nbsp;&nbsp;<asp:Label ID="lblHeader" runat="server"></asp:Label>
<br /><br />

<div id="dvLotDataScan" runat="server" class="divInput2">
        <table id ="tablestyle" runat="server" style ="text-align:left;" cellpadding="0" cellspacing="0">
                <tr style ="color:Black">
                   <td style="text-align:right;">
                       <asp:Label ID="lblDN" runat="server" Text="DN NO.:"></asp:Label>
                    </td>
                    <td style="padding-left:5px; text-align:left" colspan=4>
                        <asp:TextBox ID="txtDNNo" runat="server" Width="260px"
                            Font-Size="20px" BackColor="#d9d9d9" BorderColor="#333333" 
                            BorderStyle="Solid" BorderWidth="1px" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                 <tr style ="color:Black">
                   <td style="text-align:right;">
                       <asp:Label ID="lblRFID" runat="server" Text="SCAN RFID:"></asp:Label>
                    </td>
                    <td style="padding-left:5px;text-align:left" colspan=4>
                        <asp:TextBox ID="txtRFID" runat="server" Width="260px"
                            Font-Size="20px" AutoPostBack="True" ontextchanged="txtRFID_TextChanged" ></asp:TextBox>
                    </td>
                </tr>
                <tr style ="color:Black">
                   <td id ="tdQR" runat="server" style="text-align:right;">
                       <asp:Label ID="lblLotQR" runat="server" Text="SCAN LOT QR:"></asp:Label>
                    </td>
                    <td style="padding-left:5px;text-align:left" colspan=4>
                        <asp:TextBox ID="txtLotQR" runat="server" Width="260px"
                            Font-Size="20px" AutoPostBack="True" ontextchanged="txtLotQR_TextChanged" ></asp:TextBox>
                    </td>
                </tr>
                <tr style ="color:Black">
                   <td style="text-align:right;">
                       <asp:Label ID="lblPCode" runat="server" Text="PARTCODE:"></asp:Label>
                    </td>
                    <td style="padding-left:5px;text-align:left" colspan=4>
                        <asp:TextBox ID="txtPartCode" runat="server" Width="260px"
                            Font-Size="20px" BackColor="#ffffcc" Enabled="False" BorderStyle="Solid" 
                            BorderWidth="1px" ></asp:TextBox>
                    </td>
                </tr>
                <tr style ="color:Black">
                   <td style="text-align:right;">
                       <asp:Label ID="lblLotNo" runat="server" Text="LOT NO.:"></asp:Label>
                    </td>
                    <td style="padding-left:5px;text-align:left" colspan=4>
                        <asp:TextBox ID="txtLotNo" runat="server" Width="260px"
                            Font-Size="20px" BackColor="#ffffcc" Enabled="False" BorderStyle="Solid" 
                            BorderWidth="1px" ></asp:TextBox>
                    </td>
                </tr>
                  <tr style ="color:Black">
                   <td style="text-align:right;">
                       <asp:Label ID="lblRefNo" runat="server" Text="REF. NO.:"></asp:Label>
                    </td>
                    <td style="padding-left:5px;text-align:left" colspan=4>
                        <asp:TextBox ID="txtRefNo" runat="server" Width="260px"
                            Font-Size="20px" BackColor="#ffffcc" Enabled="False" BorderStyle="Solid" 
                            BorderWidth="1px" ></asp:TextBox>
                    </td>
                </tr>
                <tr style ="color:Black">
                   <td style="text-align:right;">
                       <asp:Label ID="lblQty" runat="server" Text="QUANTITY:"></asp:Label>
                    </td>
                    <td style="padding-left:5px;text-align:left">
                        <asp:TextBox ID="txtQtyTotal" runat="server" Width="75px"
                            Font-Size="20px" BackColor="#ffffcc" Enabled="False" BorderStyle="Solid" 
                            BorderWidth="1px" ></asp:TextBox>

                        <asp:TextBox ID="txtQty1" runat="server" Width="63px"
                            Font-Size="20px" BackColor="#99ccff" Enabled="False" BorderStyle="Solid" 
                            BorderWidth="1px" ></asp:TextBox>
                    /
                        <asp:TextBox ID="txtQty2" runat="server" Width="63px"
                            Font-Size="20px" BackColor="#99ccff" Enabled="False" BorderStyle="Solid" 
                            BorderWidth="1px" ></asp:TextBox></td>
                </tr>
                 <tr style ="color:Black">
                   <td style="text-align:right;">
                       <asp:Label ID="lblRemarks" runat="server" Text="REMARKS:"></asp:Label>
                    </td>
                    <td style="padding-left:5px;text-align:left" colspan=4>
                        <asp:TextBox ID="txtRemarks" runat="server" Width="260px"
                            Font-Size="20px" BackColor="#ffffcc" Enabled="False" BorderStyle="Solid" 
                            BorderWidth="1px" ></asp:TextBox>
                    </td>
                </tr>
                 <tr style ="color:Black">
                 <td height="10px"></td>
                 </tr>
                  <tr>
            <td style="text-align:right;">
                <asp:Label ID="lblMessage2" runat="server" Text="MESSAGE:" style="color:Black"></asp:Label></td>
           <td id="tdMessage" runat="server" style="border: 1px solid #385d8a; background-color:#d9d9d9; height:25px; padding:5px;">
                <asp:Label ID="lblMessage" runat="server" CssClass="messageStyle" 
                    ></asp:Label></td>
          </tr>
                <tr>
                    <td height="10px"></td>
                </tr>
                <tr >
                    <td colspan=4 align=center>
                        <asp:Button ID="btnDelete" runat="server" Text="F6 DELETE" Height="35px" 
                            Width="70px" CssClass="buttonStyle" Enabled="False" 
                            onclick="btnDelete_Click" />
                        <asp:Button ID="btnBack" runat="server" Text="F7 BACK" Height="35px" 
                            Width="70px" CssClass="buttonStyle" onclick="btnBack_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="F8 CLEAR" Height="35px" 
                            Width="70px" CssClass="buttonStyle" onclick="btnClear_Click" />
                    </td>
                </tr>
                <tr>
                    <td height="10px"></td>
                </tr>
          </table>
         </div>
</div>
<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>
