<%@ Page Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="VPASP_SERIAL_SCAN_MASTER.aspx.cs" Inherits="FGWHSEClient.Form.VPASP_SERIAL_SCAN_MASTER" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Style.css" rel="stylesheet" />
    <div style="width: 100%;">
        <div style="margin-left: 10px; vertical-align: top">
            <table>
                <tr>
                    <td>PRODUCT CODE :
                    </td>
                    <td>
                        <asp:TextBox ID="txtProductCode" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>STATUS :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddStatus" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>DATE FROM :
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateFrom" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;
                    </td>

                    <td>DATE TO :
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateTo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnADD" class="btn nk-indigo btn-info" runat="server" Text="ADD" />
                        &nbsp;
                        <asp:Button ID="btnSearch" class="btn nk-indigo btn-info" runat="server" Text="SEARCH" OnClick="btnSearch_Click" />

                    </td>
                </tr>
            </table>
            <br />
            <div style="height: 450px; overflow: auto">
                <table id="tbList" runat="server">
                </table>
            </div>

        </div>

    </div>




    <div class="modal fade" id="myModaL" data-keyboard="false" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <div style="text-align: left">
                        <asp:Panel ID="pnLogin" runat="server" CssClass="modalPopup" align="center" Style="display: compact" Width="400px" BackColor="#EAE9EB" Height="500px">
                            <div style="background-color: darkseagreen; height: 100%;">
                                <table>
                                    <tr>
                                        <td>
                                            <br />
                                            <div>
                                                <asp:Label ID="Label1" runat="server" ForeColor="White" Font-Bold="True" Font-Size="Large" Text="ADD/UPDATE"></asp:Label><br />

                                            </div>
                                            <br />
                                            <table>
                                                <tr>
                                                    <td>Product Code:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtProductCodeEdit" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Required Flag:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddStatusEdit" runat="server"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <br />
                                                        <asp:Button ID="btnSave" runat="server" class="btn nk-indigo btn-info" Text="SAVE" OnClick="btnSave_Click" />&nbsp;
                                                        <asp:Button ID="btnCancel" runat="server" class="btn nk-indigo btn-info" Text="CANCEL" />
                                                        <br />
                                                        <br />
                                                    </td>

                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>

                            </div>

                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="display: none">
        <button type="button" class="btn nk-indigo btn-info" data-toggle="modal" data-target="#myModaL" id="btnModal">Indigo</button>
        <asp:Button ID="btnDoublePost" runat="server" Text="Button" />

    </div>

    <script type="text/javascript">
        function showModal(pcode, reqstat) {

            var reqstatvalue = "0";
            if (reqstat == "REQUIRED") {
                reqstatvalue = "1";
            }
            else {
                reqstatvalue = "0";
            }
            document.getElementById('ctl00_ContentPlaceHolder1_txtProductCodeEdit').value = pcode;
            document.getElementById('ctl00_ContentPlaceHolder1_ddStatusEdit').value = reqstatvalue;
            document.getElementById('btnModal').click();
        }
    </script>
    <ajaxToolkit:CalendarExtender ID="Calendarextender1" runat="server" BehaviorID="calendar1"
        TargetControlID="txtDateFrom"
        Format="dd-MMM-yyyy HH:mm tt"
        PopupButtonID="imgCalendar3">
    </ajaxToolkit:CalendarExtender>

    <ajaxToolkit:CalendarExtender ID="Calendarextender2" runat="server" BehaviorID="calendar2"
        TargetControlID="txtDateTo"
        Format="dd-MMM-yyyy HH:mm tt"
        PopupButtonID="imgCalendar3">
    </ajaxToolkit:CalendarExtender>
    <cc1:msgBox ID="MsgBox1" runat="server"></cc1:msgBox>
</asp:Content>
