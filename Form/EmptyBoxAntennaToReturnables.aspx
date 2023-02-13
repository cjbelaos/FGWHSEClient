<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Form/MasterPalletMonitoring.Master" CodeBehind="EmptyBoxAntennaToReturnables.aspx.cs" Inherits="FGWHSEClient.Form.EmptyBoxAntennaToReturnables" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Style.css" rel="stylesheet" />
    <div style="margin-left: 5px">
        <table>
            <tr>
                <td>
                    <table cellspacing="0">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>Loading Dock: &nbsp; </td>
                                        <td>
                                            <asp:DropDownList ID="ddlLoadingDock" runat="server" Width="250px" AutoPostBack="True"></asp:DropDownList></td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblRefreshTime" runat="server" Text=""></asp:Label></td>

                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                        </tr>
                        <tr>
                            <td valign="top">

                                <!-- RFID DN READ---->
                                <br />
                                <div style="margin-left:150px">
                                    <div style="display: inline-block">
                                        <div class="dvBoardContainer">
                                            <div style="font-weight: normal">
                                                <table id="tbTable" runat="server">
                                                </table>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <!-- RFID DN READ END---->

                            </td>
                        </tr>

                    </table>

                </td>
            </tr>
        </table>

    </div>
    <div style="display: none">
        <asp:Button ID="btnRefresh" runat="server" Text="Button" OnClick="btnRefresh_Click" />
    </div>

    <script type="text/javascript">

        var TIMER_INTERVAL = 2000;
        setTimeout(function () { refreshPage() }, TIMER_INTERVAL);
        function refreshPage() {
            var displayButton = document.getElementById("ctl00_ContentPlaceHolder1_btnRefresh");
            var check = document.getElementById("ctl00_ContentPlaceHolder1_ddlLoadingDock").value;
            if (check != "") {
                displayButton.click();
            }


            setTimeout(function () { refreshPage() }, TIMER_INTERVAL);
        }
    </script>
</asp:Content>
