<%@ Page Title="" Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="PSIDosBuyerConfirm.aspx.cs" Inherits="FGWHSEClient.Form.PSIDosBuyerConfirm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="TableStatus">
        <div id="TableScroll">
            <button type="button" style="width:32px;">X</button>
            <table>
                <thead>
                    <tr>
                        <th>SUPPLIER</th>
                        <th>PARTS WITH NO DOS</th>
                        <th>%</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
                <tfoot>
                </tfoot>
            </table>
        </div>
    </div>
    <div class="PSI_Content">
        <div class="panel-loading">
            <span class="icon-loading"></span>
        </div>
        <table style="width:100%;">
            <thead>
                <tr>
                    <th></th>
                    <th>
                        <center>
                            <h2>PARTS DOS LEVEL MONITORING</h2>
                            Update as of: <span style="color:blue;" id="txtCurrentDate"> </span>
                        </center>
                    </th>
                    <th>
                        <p>Legend:<span style="width:100px;height:50px;display:inline-block;position:relative; background:#3bff3b;top:-15px;">&nbsp;</span>Achieved</p>
                        <p><span style="width:4em;height:10px;display:inline-block;"></span><span style="width:100px;height:50px;display:inline-block;position:relative; background:red;top:-15px;">&nbsp;</span>Not Achieved</p>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <table class="table" border="1">
                            <thead>
                                <tr>
                                    <th style="text-align:center;">ALL LOCAL SUPPLIERS</th>
                                    <th style="text-align:center;">STATUS</th>
                                    <th style="text-align:center;">%</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td style="text-align:center;" id="txtSupplierCount"><span class="icon-exporting"></span></td>
                                    <td style="text-align:center;background:#3bff3b;" id="txtSupplierStatus"><span class="icon-exporting"></span></td>
                                    <td style="text-align:center;font-weight:bold;" id="txtSupplierModulo"><span class="icon-exporting"></span></td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
            </tbody>
        </table>
        <div id="DOSContainer" style="width:100%;">
        </div>
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
    <script type="text/javascript" src="../Scripts/DOSBuyerConfirm.js"></script>
</asp:Content>