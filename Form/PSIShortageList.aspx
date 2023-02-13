<%@ Page Title="" Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="PSIShortageList.aspx.cs" Inherits="FGWHSEClient.Form.PSIShortageList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PSI_Content">
        <div class="panel-loading">
            <span class="icon-loading"></span>
        </div>
        <div style="left: 40px; top: 110px;">
            <table>
                <tr>
                    <td colspan="3">
                        <h3>Shortage List <small id="label-last-update">(Last Update - 00/00/0000 00:00:00)</small></h3><span><small style="color:cadetblue;" id="currentStatus"></small></span>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td><label>Plant <select id="txtPlant"></select></label></td>
                    <td><label>Vendor <select id="search-vendor" multiple></select></label></td>
                    <td>
                        <input type="file" accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel" id="txtUploadFile" name="txtUploadFile" />
                        <button type="button" class="btn btn-primary" id="btnUploadFile">Upload</button>
                        <span id="lblMessage" style="color: Green"></span>
                    </td>
                </tr>
            </table>
            <hr />
            <div id="MyTable"></div>
        </div>
    </div>
    <script type="text/javascript" src="../Scripts/jquery-3.5.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/handsontable.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.mask.min.js"></script>
    <script type="text/javascript" src="../Scripts/OtherFunctions.js"></script>
    <script type="text/javascript" src="../Scripts/xlsx.full.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.modal.min.js"></script>
    <script type="text/javascript" src="../Scripts/jexcel.js"></script>
    <script type="text/javascript" src="../Scripts/jsuites.js"></script>
    <script type="text/javascript" src="../Scripts/chosen.proto.js"></script>
    <script type="text/javascript" src="../Scripts/ShortageList.js"></script>
</asp:Content>
