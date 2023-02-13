<%@ Page Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="AutoLine_Barcode_Inquiry.aspx.cs" Inherits="FGWHSEClient.Form.AutoLine_Barcode_Inquiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align:left;width:1200px">
    <div style="margin-left:10px">
        <br />
        <span>AUTO-LINE BARCODE LABEL INQUIRY</span>
        <br />
        <br />
        <div>
            <table>
                <tr>
                    <td valign="top">OLD REFERENCE NO :</td>
                    <td>&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtOldRefNo" runat="server"></asp:TextBox></td>
                    <td></td>
                    <td valign="top">LOT NO :</td>
                    <td>&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtLotNo" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td valign="top">REFERENCE NO :</td>
                    <td>&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtRefNo" runat="server"></asp:TextBox></td>
                    <td></td>
                    <td valign="top">PART CODE :</td>
                    <td>&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtPartCode" runat="server"></asp:TextBox></td>
                </tr>

               
                <tr>
                    <td valign="top">DATE FROM:</td>
                    <td>&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtDateFrom" runat="server"></asp:TextBox></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td valign="top">DATE TO:</td>
                    <td>&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtDateTo" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="SEARCH" OnClick="btnSearch_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnExport" runat="server" Text="EXPORT" OnClick="btnExport_Click" />
                    </td>
                </tr>
            </table>
            <br />
             <div id ="grid" runat = "server" align="center" style="font-size:10px;overflow:auto; height:200px">
             <asp:GridView ID="grdLot" runat="server" Width="1160px" 
                    AutoGenerateColumns="True"  CellPadding="3" BackColor="White"  
                    GridLines="Both" PageSize="15" >
                <RowStyle ForeColor="Black"  BorderColor="black"  BorderWidth="1pt" />
                         
                         
                </asp:GridView>

            </div>

        </div>



     </div>
 </div>
<ajaxToolkit:Calendarextender ID="Calendarextender1" runat="server" BehaviorID="calendar1"
TargetControlID="txtDateFrom"
Format="dd-MMM-yyyy HH:mm tt"
PopupButtonID="imgCalendar3"  >
</ajaxToolkit:Calendarextender>


<ajaxToolkit:Calendarextender ID="Calendarextender3" runat="server" BehaviorID="calendar2"
TargetControlID="txtDateTo"
Format="dd-MMM-yyyy HH:mm tt"
PopupButtonID="imgCalendar2">
</ajaxToolkit:Calendarextender>
</asp:Content>