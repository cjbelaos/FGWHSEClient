<%@ Page Title="" Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="PSIDosVendorReply.aspx.cs" Inherits="FGWHSEClient.Form.PSIDosVendorReply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PSI_Content">
        <div class="panel-loading">
            <span class="icon-loading"></span>
        </div>
        <table>
            <tr>
                <td colspan="4">
                    <h3>DOS Vendor Reply</h3>
                    <span><small style="color: cadetblue;" id="currentStatus"></small></span>
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
                        Vendor
                        <select id="search-vendor" multiple value="<% Response.Write(_SupplierCode); %>"></select>
                    </label>
                </td>
                <td>
                    <label>
                        PartsCode
                        <select id="search-material" multiple></select>
                    </label>
                </td>
                <td>
                    <button type="button" class="btn btn-primary" id="btnSaveDOS">Save</button>
                    <span class="icon-exporting"></span>
                    <button type="button" class="btn btn-primary" id="btnExportToExcel">Export</button>
                </td>
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
    <script type="text/javascript" src="../Scripts/chosen.proto.js"></script>
    <script type="text/javascript" src="../Scripts/DOS.js"></script>


    <script type="text/javascript">
        $(document).ready(function () {
            $(".icon-exporting").hide();
            $('#search-vendor,#search-plant,#search-material').on('change', function () {
                var PlantID = $('#search-plant').val();
                var VendorCode = $('#search-vendor').val();
                if (VendorCode.length > 0) {
                    if (VendorCode[0] == "") {
                        VendorCode.splice(0, 1);
                    }
                }

                var MaterialNumber = $('#search-material').val();
                if (PlantID.length > 0 || VendorCode.length > 0 || MaterialNumber.length > 0) {
                    $('.panel-loading').show();
                    GetData(PlantID, VendorCode, MaterialNumber, function (e) {
                        e = JSON.parse(e.d);
                        if (e.length > 0) {
                            DTOrig = e;
                            DrawTable(e);
                        } else {
                            alert('No Records Found');
                        }
                        $('.panel-loading').hide();
                    });
                } else {
                    alert("Choose atleast one option");
                }
            });
            var flag = true;
            if (flag) {
                GetBusCategory(function (e) {
                    e = JSON.parse(e.d);
                    DTBusCategory = e;
                    GetModel(function (e) {
                        e = JSON.parse(e.d);
                        DTModel = e;
                        GetProblemCategory(function (e) {
                            e = JSON.parse(e.d);
                            DTProblemCategory = e;
                            GetPlants(function (e) {
                                e = JSON.parse(e.d);
                                $('#search-plant').html('');
                                for (var i in e) {
                                    $('<option/>', {
                                        value: e[i]['PlantID'],
                                        text: e[i]['PlantCode'] + ' | ' + e[i]['PlantName']
                                    }).appendTo('#search-plant');
                                }
                                GetMaterial(function (e) {
                                    e = JSON.parse(e.d);
                                    $('#search-material').html('');
                                    $('<option/>', {
                                        value: null,
                                        text: null
                                    }).appendTo($('#search-material'));
                                    for (var i in e) {
                                        $('<option/>', {
                                            value: e[i]['MaterialNumber'],
                                            text: e[i]['MaterialNumber'] + ' | ' + e[i]['MaterialDescription']
                                        }).appendTo('#search-material');
                                    }
                                    GetVendors(function (e) {
                                        e = JSON.parse(e.d);
                                        $('#search-vendor').html('');
                                        $('<option/>').appendTo('#search-vendor');
                                        for (var i in e) {
                                            $('<option/>', {
                                                value: e[i]['SUPPLIERCODE'],
                                                text: e[i]['SUPPLIERCODE'] + ' | ' + e[i]['SUPPLIERNAME']
                                            }).appendTo('#search-vendor');
                                        }
                                        var _v = $("#search-vendor").attr('value');
                                        $('#search-vendor').val($("#search-vendor").attr('value'));
                                        $('#search-vendor,#search-plant,#search-material').chosen({ allow_single_deselect: true });
                                        if (_v != "") {
                                            $('#search-vendor').trigger('change');
                                        }
                                        //$('#search-vendor').trigger('change');
                                        $('.panel-loading').hide();
                                        GetSession(function (e) {
                                            if (e['UserID'].toString() == 'GUEST') {
                                                $('#btnExportToExcel,#btnSaveDOS').remove();
                                            }
                                        });
                                    });
                                });
                            });
                        });
                    });
                });
            } else {
                $('.panel-loading').hide();
            }

            $('#btnSaveDOS').on('click', function (e) {
                e.preventDefault();
                SaveData(function (e) {
                    console.log(e);
                });
            });
            $("#btnExportToExcel").on("click", function () {
                $(".icon-exporting").show();
                $("#btnExportToExcel").hide();
                ExportToExcel($('#search-plant').val(), $('#search-vendor').val(), $('#search-material').val(), function () {
                    $("#btnExportToExcel").show();
                    $(".icon-exporting").hide();
                });
            });

            //$('#btnExportToExcel').click(function () {
            //    $(".icon-exporting").show();
            //    $("#btnExportToExcel").hide();
            //    ExportToExcel($('#search-plant').val(), $('#search-vendor').val(), $('#search-material').val(), function (e) {
            //        $("#btnExportToExcel").show();
            //        $(".icon-exporting").hide();
            //    });
            //});
            //$("#txtFile").on("change", function (e) {
            //    ImportXLSX();
            //});
        });
    </script>
</asp:Content>
