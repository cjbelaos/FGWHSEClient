<%@ Page Title="" Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="PSIPartsSimulation.aspx.cs" Inherits="FGWHSEClient.Form.PSIPartsSimulation" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="PSI_Content">
        <div class="panel-loading">
            <span class="icon-loading"></span>
        </div>
        <table>
            <tr>
                <td colspan="4">
                    <h3>Parts Simulation</h3><span><small style="color:cadetblue;" id="currentStatus"></small></span>
                </td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <label>
                        Plant
                        <select id="search-plant"></select>
                    </label>
                </td>
                <td>
                    <label>
                        PartsCode
                        <select id="search-material" multiple></select>
                    </label>
                </td>
                <td>
                    <label>
                        Vendor
                        <select id="search-vendor" multiple></select>
                    </label>
                </td>
                <td>
                    <button type="button" class="btn btn-primary" id="btnSavePartsSimulation">Save</button>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="2" style="text-align:right;padding-right: 10px;padding-top: 4px;">
                    <label style="display:inline-block;">Import<input type="file" id="txtUploadFile" /></label>
                    <button type="button" class="btn btn-primary" id="btnImport">Import</button>
                </td>
                <td><span class="icon-exporting"></span><button type="button" class="btn btn-primary" id="btnExportToExcel">Export</button></td>
            </tr>
        </table>

        <%--<telerik:RadGrid AllowSorting="true" ID="RadGrid1" runat="server">
            <ClientSettings>
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" FrozenColumnsCount="2"/>
            </ClientSettings>
        </telerik:RadGrid>--%>
        <%--<asp:GridView ID="gv_excel" runat="server" AutoGenerateColumns="true" AllowCustomPaging="True" AllowPaging="True" AllowSorting="True" AutoGenerateEditButton="True" AutoGenerateSelectButton="True"></asp:GridView>--%>
        <%--<div style="max-width:100%;overflow:auto;max-height:250px;">
        </div>--%>


        <div id="MyTable"></div>
        <%--<table id="table_excel" class="table_excel" runat="server"></table>--%>
    </div>
    <script type="text/javascript" src="../Scripts/jquery-3.5.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/handsontable.js"></script>
    <script type="text/javascript" src="../Scripts/FileSaver.min.js"></script>
    <script type="text/javascript" src="../Scripts/exceljs.js"></script>
    <script type="text/javascript" src="../Scripts/xlsx.full.min.js"></script>
    <script type="text/javascript" src="../Scripts/jexcel.js"></script>
    <script type="text/javascript" src="../Scripts/jsuites.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.mask.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.modal.min.js"></script>
    <script type="text/javascript" src="../Scripts/OtherFunctions.js"></script>
    <script type="text/javascript" src="../Scripts/chosen.proto.js"></script>
    <script type="text/javascript" src="../Scripts/PartsSimulation.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            GetPlants(function (e) {
                var select = $("#search-plant");
                select.html('');
                select.data("data",e);
                //var input = $('input', label);
                for (var i in e) {
                    $('<option/>', {
                        value: e[i].PlantID,
                        text: e[i].PlantCode + " | " + e[i].PlantName
                    }).appendTo(select);
                }
                GetMaterial(function (e) {
                    var datalist = $("#search-material");
                    datalist.html('');
                    //var control = $("<select/>").appendTo(datalist);
                    //var label = control.closest('label');
                    //var input = $('input',label);
                    $('<option/>', {
                        value: null,
                        text: null
                    }).appendTo(datalist);
                    for (var i in e) {
                        $('<option/>', {
                            value: e[i].MaterialNumber,
                            text: e[i].MaterialNumber+' | '+e[i].MaterialDescription
                        }).appendTo(datalist);
                    }
                    GetVendor(function (e) {
                        $('#search-vendor').html();
                        $('<option/>').appendTo('#search-vendor');
                        for (var i in e) {
                            $('<option/>', {
                                value:e[i]['SupplierCode'],
                                text:e[i]['SupplierCode']+' | '+e[i]['SupplierName']
                            }).appendTo('#search-vendor');
                        }
                        $("#search-material,#search-plant,#search-vendor").chosen({allow_single_deselect: true});
                        $("#search-material,#search-plant,#search-vendor").off("change").on("change", function (e) {
                            var PlantID = $("#search-plant").val();
                            var MaterialNumber = $("#search-material").val();
                            var VendorCode = $("#search-vendor").val();
                            if (PlantID.length > 0 || MaterialNumber.length > 0 || VendorCode.length > 0) {
                                $('.panel-loading').show();
                                GetData(PlantID, MaterialNumber, VendorCode, function (e) {
                                    console.log(e);
                                    var hiddenColumns = {
                                        columns: [1, 5],
                                        indicators: false
                                    };
                                    if (e.length > 0) {
                                        DrawNewTable(e, hiddenColumns, function () {
                                            $('.panel-loading').hide();
                                        });
                                    } else {
                                        alert("No Record Found.");
                                        $('.panel-loading').hide();
                                    }
                                });
                            } else {
                                alert("Choose atleast one option");
                            }
                        });
                        GetSession(function (e) {
                            if (e['UserID'].toString() == 'GUEST') {
                                $('#btnSavePartsSimulation,#txtUploadFile,#btnImport,#btnExportToExcel').remove();
                            }
                        });
                        //$("#search-material").trigger('change');
                    });
                });
            });
            //GetDataTable();
            $('.panel-loading').hide();
            $('#btnSavePartsSimulation').on('click', function (e) {
                e.preventDefault();
                $('.panel-loading').show();
                SaveSimulation(function (e) {
                    alert(e.d);
                    $('.panel-loading').hide();
                });
            });
            $(".icon-exporting").hide();
            $("#btnExportToExcel").on("click", function () {
                $(".icon-exporting").show();
                $("#btnExportToExcel").hide();
                var PlantID = $("#search-plant").val();
                var MaterialNumber = $("#search-material").val();
                var VendorCode = $("#search-vendor").val();
                ExportToExcel(PlantID, MaterialNumber, VendorCode, function () {
                    $("#btnExportToExcel").show();
                    $(".icon-exporting").hide();
                });
            });
            $("#txtUploadFile").on("change", function (e) {
                ReadExcelFile(function (e) {
                    var iColumns = ['Model', 'Plant', 'Material', 'Description', 'Supplier', 'SupplierName', 'EPPISTCK', 'SupplierSTCK', 'TotalSTCK',
                        'Plan/Logical','Past Due'];
                    var iTitles = ['Model', 'Plant', 'Material', 'Description', 'Supplier', 'SupplierName', 'EPPISTCK', 'SupplierSTCK', 'TotalSTCK',
                        'Plan/Logical', 'PastDue'];
                    var hiddenColumns = {
                        columns: [4],
                        indicators: false
                    };
                    DrawTable(iColumns,iTitles,e,hiddenColumns);
                });
            });
        });
    </script>
</asp:Content>
