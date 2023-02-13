<%@ Page Title="" Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="PSIDOSMonthly.aspx.cs" Inherits="FGWHSEClient.Form.PSIDOSMonthly" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PSI_Content">
        <div class="panel-loading">
            <span class="icon-loading"></span>
        </div>
        <table>
            <tr>
                <td colspan="4">
                    <h3>DOS Monthly Report</h3><span><small style="color:cadetblue;" id="currentStatus"></small></span>
                </td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <label>
                        Date
                        <input type="text" id="search-date"/>
                    </label>
                </td>
                <td><button type="button" class="btn btn-primary" id="btnSearch">Search</button></td>
                <td><span class="icon-exporting"></span><button type="button" class="btn btn-primary" id="btnExport">Export</button></td>
            </tr>
        </table>
        <div id="MyTable"></div>
    </div>
    <script type="text/javascript" src="../Scripts/HandsOnTableKey.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-3.5.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/handsontable.js"></script>
    <script type="text/javascript" src="../Scripts/FileSaver.min.js"></script>
    <script type="text/javascript" src="../Scripts/exceljs.js"></script>
    <script type="text/javascript" src="../Scripts/js-xlsx-validation.js"></script>
    <script type="text/javascript" src="../Scripts/xlsx.full.min.js"></script>
    <script type="text/javascript" src="../Scripts/jexcel.js"></script>
    <script type="text/javascript" src="../Scripts/jsuites.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.mask.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.modal.min.js"></script>
    <script type="text/javascript" src="../Scripts/OtherFunctions.js"></script>
    <script type="text/javascript" src="../Scripts/DOSMonthly.js"></script>
</asp:Content>
