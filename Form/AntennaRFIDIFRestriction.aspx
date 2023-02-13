<%@ Page Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="AntennaRFIDIFRestriction.aspx.cs" Inherits="FGWHSEClient.Form.AntennaRFIDIFRestriction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cc1:msgBox ID="MsgBox1" runat="server"></cc1:msgBox>
    <link href="../Style.css" rel="stylesheet" />
    <div style="width: 100%;">
        <div style="margin-left: 10px; vertical-align: top">
            <table>
                <tr>
                    <td>ITEM CODE :
                    </td>
                    <td>
                        <asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox>
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
                    <td>AREA LOCATION :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddArea" runat="server"></asp:DropDownList>
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
                                                    <td>Item Code:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtItemCodeEdit" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>I/F Status :</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddStatusEdit" runat="server"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Area :</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddAreaEdit" runat="server"></asp:DropDownList>
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


    <div class="modal fade" id="myModaLDelete" data-keyboard="false" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <div style="text-align: left">
                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: compact" Width="400px" BackColor="#EAE9EB" Height="500px">
                            <div style="background-color: darkseagreen; height: 100%;">
                                <table>
                                    <tr>
                                        <td>
                                            <br />
                                            <div>
                                                <asp:Label ID="Label2" runat="server" ForeColor="White" Font-Bold="True" Font-Size="Large" Text="Are you sure you want to delete?"></asp:Label><br />

                                            </div>
                                            <br />
                                            <table>
                                                <tr>
                                                    <td>Item Code: &nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lblItemDelete" runat="server" Text="Label" ForeColor="Black"></asp:Label>
                                                        <div style="display: none">
                                                            <asp:TextBox ID="txtItemDelete" runat="server"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Area :&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lblAreaDelete" runat="server" Text="Label" ForeColor="Black"></asp:Label>
                                                        <div style="display: none">
                                                            <asp:TextBox ID="txtAreaDelete" runat="server"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <br />
                                                        <asp:Button ID="btnDeleteModal" runat="server" class="btn nk-indigo btn-info" Text="DELETE" OnClick="btnDelete_Click" />&nbsp;
                                                        <asp:Button ID="btnDeleteCancel" runat="server" class="btn nk-indigo btn-info" Text="CANCEL" />
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
        <asp:Button ID="btnDoublePost" runat="server" Text="Button" OnClick="btnDelete_Click" />
        <button type="button" class="btn nk-indigo btn-info" data-toggle="modal" data-target="#myModaLDelete" id="btnModalDelete">Indigo</button>

    </div>

    <script type="text/javascript">
        function btndeleteClick(tcode, arealoc, resstat) {
            document.getElementById('ctl00_ContentPlaceHolder1_lblItemDelete').innerHTML = tcode;
            document.getElementById('ctl00_ContentPlaceHolder1_lblAreaDelete').innerHTML = arealoc;
            document.getElementById('ctl00_ContentPlaceHolder1_txtItemDelete').value = tcode;
            document.getElementById('ctl00_ContentPlaceHolder1_txtAreaDelete').value = arealoc;
            document.getElementById('btnModalDelete').click();
        }
        function showModal(tcode, arealoc, resstat) {
            var resstatvalue = "0";
            if (resstat == "RESTRICTED") {
                resstatvalue = "1";
            }
            else {
                resstatvalue = "0";
            }
            document.getElementById('ctl00_ContentPlaceHolder1_txtItemCodeEdit').value = tcode;
            document.getElementById('ctl00_ContentPlaceHolder1_ddAreaEdit').value = arealoc;
            document.getElementById('ctl00_ContentPlaceHolder1_ddStatusEdit').value = resstatvalue;
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

</asp:Content>
