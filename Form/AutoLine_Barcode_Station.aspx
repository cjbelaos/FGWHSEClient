<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Form/MasterPalletMonitoring.Master" CodeBehind="AutoLine_Barcode_Station.aspx.cs" Inherits="FGWHSEClient.Form.AutoLine_Barcode_Station" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align:left;width:1200px">
<div style="margin-left:10px">
    <br />
        <span>AUTO-LINE BARCODE LABEL STATION REGISTRATION</span>
    <br />
    <br />
    <div>
        <table>
            <tr>
                <td style="vertical-align:top">
                    STATATION NO:
                    &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtStationNo" runat="server" MaxLength="3"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top">
                    IP ADDRESS: 
                    
                </td>
                <td>
                    <asp:TextBox ID="txtIP" runat="server" Width="191px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>

                    <asp:Button ID="btnRegister" runat="server" Text="REGISTER" OnClick="btnRegister_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="CANCEL" OnClick="btnCancel_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="SEARCH" OnClick="btnSearch_Click" />

                </td>
            </tr>
        </table>
        <br />
        <div id ="grid" runat = "server" align="center" style="font-size:10px;overflow:auto; height:200px">
            <asp:GridView ID="grdStation" runat="server" Width="1160px" 
                    AutoGenerateColumns="True"  CellPadding="3" BackColor="White"  
                    GridLines="Both" PageSize="15" >
                <RowStyle ForeColor="Black"  BorderColor="black"  BorderWidth="1pt" />
                         
                         
                </asp:GridView>

        </div>
    </div>
</div>

</div>

 <cc1:msgbox id="msgBox" runat="server"></cc1:msgbox>
</asp:Content>
