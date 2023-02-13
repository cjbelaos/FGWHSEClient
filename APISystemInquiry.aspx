<%@ Page Title="" Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="APISystemInquiry.aspx.cs" Inherits="FGWHSEClient.Form.APISystemInquiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link type="text/css" rel="Stylesheet" href="../App_Themes/Stylesheet/Main.css" />


    <script>
        function startTime() {
            var today = new Date();
            var h = today.getHours();
            var m = today.getMinutes();
            var s = today.getSeconds();

            var date = today.toDateString();

            // add a zero in front of numbers<10
            m = checkTime(m);
            s = checkTime(s);
            document.getElementById('ctl00_datetime').innerHTML = "Current Date : " + date + " " + h + ":" + m + ":" + s;



            t = setTimeout(function () { startTime() }, 500);
        }

        function checkTime(i) {
            if (i < 10) {
                i = "0" + i;
            }
            return i;
        }

        function DataSelect() {
            var calendar = $find("calendar1");
            var d = calendar._selectedDate;
            var now = new Date();
            calendar.get_element().value = d.format('MM/dd/yyyy') + " " + now.format("HH:mm:ss");
        }

        function DataSelect2() {
            var calendar = $find("calendar3");
            var d = calendar._selectedDate;
            var now = new Date();
            calendar.get_element().value = d.format('MM/dd/yyyy') + " " + now.format("HH:mm:ss");
        }




        //Menu
        // Copyright 2006-2007 javascript-array.com

        var timeout = 500;
        var closetimer = 0;
        var ddmenuitem = 0;

        // open hidden layer
        function mopen(id) {
            // cancel close timer
            mcancelclosetime();

            // close old layer
            if (ddmenuitem) ddmenuitem.style.visibility = 'hidden';

            // get new layer and show it
            ddmenuitem = document.getElementById(id);
            ddmenuitem.style.visibility = 'visible';

        }
        // close showed layer
        function mclose() {
            if (ddmenuitem) ddmenuitem.style.visibility = 'hidden';
        }

        // go close timer
        function mclosetime() {
            closetimer = window.setTimeout(mclose, timeout);
        }

        // cancel close timer
        function mcancelclosetime() {
            if (closetimer) {
                window.clearTimeout(closetimer);
                closetimer = null;
            }
        }

        // close layer when click-out
        document.onclick = mclose;




    </script>

    <style type="text/css">
        .auto-style1 {
            font-size: 17px;
            color: #000000;
        }
    </style>

    <div style="margin-left: 10px; top: 110px;">
        <br />
        <span class="auto-style1">PARTS INVENTORY SYSTEM INQUIRY</span>
        <br />
        <br />
        <div>
            <table>
                <tr>

                    <td>DIVISION:
                    </td>
                    <td valign="top">

                        <asp:DropDownList ID="ddlDivision" runat="server" Width="150px">
                            <asp:ListItem>SELECT DIVISION</asp:ListItem>
                            <asp:ListItem>PR</asp:ListItem>
                            <asp:ListItem>VP</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>TRANSACTION DATE FROM:</td>
                    <td>
                        <asp:TextBox ID="txtbxFromDate" runat="server"></asp:TextBox>


                        <ajaxToolkit:CalendarExtender ID="txtbxFromDate_Calendarextender" runat="server" BehaviorID="calendar1"
                            TargetControlID="txtbxFromDate"
                            Format="MM/dd/yyyy"
                            PopupButtonID="txtbxFromDate"
                            OnClientDateSelectionChanged="DataSelect">
                        </ajaxToolkit:CalendarExtender>

                    </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td>TRANSACTION DATE TO:</td>
                    <td>
                        <asp:TextBox ID="txtbxToDate" runat="server"></asp:TextBox>

                        <ajaxToolkit:CalendarExtender ID="Calendarextender3" runat="server" BehaviorID="calendar3"
                            TargetControlID="txtbxToDate"
                            Format="MM/dd/yyyy"
                            PopupButtonID="txtbxToDate"
                            OnClientDateSelectionChanged="DataSelect2">
                        </ajaxToolkit:CalendarExtender>

                    </td>
                </tr>
                <tr>
                    <td>PART CODE:</td>
                    <td>
                        <asp:TextBox ID="txtbxPartCode" runat="server"></asp:TextBox></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>

                </tr>
                <tr>
                    <td>MODEL:</td>
                    <td>
                        <asp:TextBox ID="txtbxModel" runat="server"></asp:TextBox>
                    </td>
                    <td></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>LINE:</td>
                    <td>
                        <asp:TextBox ID="txtbxLineID" runat="server"></asp:TextBox>
                    </td>
                    <td></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td valign="top">
                        <asp:Button ID="btnView" runat="server" Text="VIEW REPORT" Width="105px" OnClick="btnView_Click" />
                    </td>
                    <td></td>
                    <td>&nbsp;</td>
                    <td valign="top">
                        <asp:Button ID="btnExport" runat="server" Text="EXPORT" Width="105px" OnClick="btnExport_Click" /></td>
                    <td>&nbsp;</td>
                    <td></td>
                </tr>
            </table>

        </div>
        <div>
            <br />
            <%-- <div style="width: 1150px; height: 150px; overflow: auto">
                <asp:GridView ID="gvInventory" runat="server" AutoGenerateColumns="false" Width="2100px"
                    Font-Size="Medium" Font-Bold="False"
                    ForeColor="Black"
                    BackColor="White" HorizontalAlign="Center"  ShowHeaderWhenEmpty="True">
                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <Columns>
                        <asp:BoundField DataField="itemcode" HeaderText="ITEM CODE" />
                        <asp:BoundField DataField="description" HeaderText="ITEM DESCRIPTION" />
                        <asp:BoundField DataField="modelname" HeaderText="MODEL" />
                        <asp:BoundField DataField="linename" HeaderText="LINE ID" />
                        <asp:BoundField DataField="quantity" HeaderText="QTY" />
                        <asp:BoundField DataField="createddate" HeaderText="CREATED DATE" />
                    </Columns>

                    <HeaderStyle BackColor="#1a66ff" Font-Bold="False" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <AlternatingRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                     <emptydatatemplate>No ELOG/E-KANBAN Data Found.</emptydatatemplate>

                </asp:GridView>
            </div>
            <br />
            <div style="width: 1150px; height: 150px; overflow: auto">
                <asp:GridView ID="gvIJMonosys" runat="server" AutoGenerateColumns="false" Width="2100px"
                    Font-Size="Medium" Font-Bold="False"
                    ForeColor="Black"
                    BackColor="White" HorizontalAlign="Center" ShowHeaderWhenEmpty="True" >
                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <Columns>
                        <asp:BoundField DataField="PartsCode" HeaderText="ITEM CODE" />
                        <asp:BoundField DataField="PrtsCdDescription" HeaderText="ITEM DESCRIPTION" />
                        <asp:BoundField DataField="ModelCode" HeaderText="MODEL ID" />
                        <asp:BoundField DataField="ModelName" HeaderText="MODEL" />
                        <asp:BoundField DataField="LineID" HeaderText="LINE ID" />
                        <asp:BoundField DataField="QTY" HeaderText="QTY" />
                        <asp:BoundField DataField="finish_datetime" HeaderText="CREATED DATE" />

                    </Columns>

                    <HeaderStyle BackColor="#1a66ff" Font-Bold="False" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <AlternatingRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                     <emptydatatemplate>No MONOSYS Data Found.</emptydatatemplate>

                </asp:GridView>
            </div>
            <br />
             <div style="width: 1150px; height: 150px; overflow: auto">
                <asp:GridView ID="gvPDR142" runat="server" AutoGenerateColumns="false" Width="2100px"
                    Font-Size="Medium" Font-Bold="False"
                    ForeColor="Black"
                    BackColor="White" HorizontalAlign="Center" ShowHeaderWhenEmpty="True">
                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <Columns>
                        <asp:BoundField DataField="itemcode" HeaderText="ITEM CODE" />
                        <asp:BoundField DataField="description" HeaderText="ITEM DESCRIPTION" />
                        <asp:BoundField DataField="modelname" HeaderText="MODEL" />
                        <asp:BoundField DataField="linename" HeaderText="LINE ID" />
                        <asp:BoundField DataField="quantity" HeaderText="QTY" />
                        <asp:BoundField DataField="createddate" HeaderText="CREATED DATE" />
                    </Columns>

                    <HeaderStyle BackColor="#1a66ff" Font-Bold="False" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <AlternatingRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <emptydatatemplate>No PD-PACG Data Found.</emptydatatemplate>

                </asp:GridView>
            </div>
            <br />--%>
            <div style="width: 1150px; height: auto; overflow: auto">
                <asp:GridView ID="gvInventory" runat="server" AutoGenerateColumns="FALSE" Width="2200px"
                    Font-Size="Medium" Font-Bold="False"
                    ForeColor="Black"
                    BackColor="White" HorizontalAlign="Center" OnRowDataBound="gvInventory_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#184B8A" Font-Bold="True" ForeColor="White" Font-Size="Small" Wrap="true" Width="1150px" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Middle" />

                    <Columns>

                        <asp:BoundField DataField="StartOfStocks_CreatedDate" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="100px" HeaderText="Start of Shift Stocks Date" ItemStyle-Wrap="False" HeaderStyle-ForeColor="White">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Middle" />
                            <ItemStyle ForeColor="Black" />
                        </asp:BoundField>

                        <asp:BoundField DataField="EndOfStocks_CreatedDate" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="100px" HeaderText="End of Shift Stocks Date" ItemStyle-Wrap="False" HeaderStyle-ForeColor="White">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Middle" />
                            <ItemStyle ForeColor="Black" />
                        </asp:BoundField>

                        <asp:BoundField DataField="PartCode" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="150px" HeaderText="ITEMCODE" ItemStyle-Wrap="False" HeaderStyle-ForeColor="White">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Middle" />
                            <ItemStyle ForeColor="Black" />
                        </asp:BoundField>

                        <asp:BoundField DataField="description" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="150px" HeaderText="DESCRIPTION" ItemStyle-Wrap="False" HeaderStyle-ForeColor="White">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Middle" />
                            <ItemStyle ForeColor="Black" />
                        </asp:BoundField>

                        <asp:BoundField DataField="modelname" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="150px" HeaderText="MODEL NAME" ItemStyle-Wrap="False" HeaderStyle-ForeColor="White">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Middle" />
                            <ItemStyle ForeColor="Black" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Line" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="150px" HeaderText="LINE ID" ItemStyle-Wrap="False" HeaderStyle-ForeColor="White">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Middle" />
                            <ItemStyle ForeColor="Black" />
                        </asp:BoundField>

                        <asp:BoundField DataField="StartOfShiftStocks" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="150px" HeaderText="START OF SHIFT STOCKS" ItemStyle-Wrap="False" HeaderStyle-ForeColor="White">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Middle" />
                            <ItemStyle ForeColor="Black" />
                        </asp:BoundField>

                        <asp:BoundField DataField="TotalPartsDelivered" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="150px" HeaderText="TOTAL PARTS DELIVERED" ItemStyle-Wrap="False" HeaderStyle-ForeColor="White">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Middle" />
                            <ItemStyle ForeColor="Black" />
                        </asp:BoundField>

                        <asp:BoundField DataField="TotalPartsUsed" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="150px" HeaderText="TOTAL PARTS USED" ItemStyle-Wrap="False" HeaderStyle-ForeColor="White">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Middle" />
                            <ItemStyle ForeColor="Black" />
                        </asp:BoundField>

                        <asp:BoundField DataField="TotalPartsUsed" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="150px" HeaderText="PD-PACG" ItemStyle-Wrap="False" HeaderStyle-ForeColor="White">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Middle" />
                            <ItemStyle ForeColor="Black" />
                        </asp:BoundField>

                        <asp:BoundField DataField="ActualStocks" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="150px" HeaderText="REMAINING STOCK" ItemStyle-Wrap="False" HeaderStyle-ForeColor="White">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Middle" />
                            <ItemStyle ForeColor="Black" />
                        </asp:BoundField>

                        <asp:BoundField DataField="EndOfShiftStocks" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="150px" HeaderText="END OF SHIFT STOCKS" ItemStyle-Wrap="False" HeaderStyle-ForeColor="White">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Middle" />
                            <ItemStyle ForeColor="Black" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Variance" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="150px" HeaderText="VARIANCE" ItemStyle-Wrap="False" HeaderStyle-ForeColor="White">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Middle" />
                            <ItemStyle ForeColor="Black" />
                        </asp:BoundField>

                    </Columns>


                </asp:GridView>
            </div>
        </div>

        <br />
    </div>
    <div>
        <table align="center">
            <tr>
                <td align="center">&nbsp;</td>
                <td align="center">&nbsp;</td>
                <td align="center">&nbsp;</td>
                <td align="center" valign="middle">
                    <asp:TextBox ID="txtbxTElogQty" runat="server" Font-Bold="True" Font-Size="Medium" Visible="False"></asp:TextBox>
                </td>
                <td align="center" valign="middle">
                    <asp:TextBox ID="txtbxTMonoQty" runat="server" Font-Bold="True" Font-Size="Medium" Visible="False"></asp:TextBox>
                </td>
                <td align="center" valign="middle">
                    <asp:TextBox ID="txtbxTPDQty" runat="server" Font-Bold="True" Font-Size="Medium" Visible="False"></asp:TextBox>
                </td>
                <td></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>

</asp:Content>
