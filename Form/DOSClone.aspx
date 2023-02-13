<%@ Page Title="" Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="DOSClone.aspx.cs" Inherits="FGWHSEClient.Form.DOSClone" %>

<asp:Content ID="Content1" ContentPlaceHolderID="css" runat="server">
    <link href="../fontawesome-free-6.2.0-web/css/all.min.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .row {
            display: -ms-flexbox;
            display: flex;
            -ms-flex-wrap: wrap;
            flex-wrap: wrap;
            margin-right: -7.5px;
            margin-left: -7.5px;
        }

        .col-md-3 {
            -ms-flex: 0 0 25%;
            flex: 0 0 25%;
            max-width: 25%;
        }

        .col-md-4 {
            -ms-flex: 0 0 33.333333%;
            flex: 0 0 33.333333%;
            max-width: 33.333333%;
        }

        .col-md-6 {
            -ms-flex: 0 0 50%;
            flex: 0 0 50%;
            max-width: 50%;
        }

        .form-group {
            margin-bottom: 1rem;
        }

        .form-control {
            display: block;
            width: 100%;
            height: calc(2.25rem + 2px);
            padding: 0.375rem 0.75rem;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            border-radius: 0.25rem;
            box-shadow: inset 0 0 0 transparent;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        label {
            font-weight: 700;
            margin-bottom: 0.5rem;
        }

        .form-label {
            margin-bottom: .5rem;
            display: inline-block;
        }

        .form-control[type=file] {
            overflow: hidden;
        }

        .form-control {
            display: block;
            width: 100%;
            padding: .375rem .75rem;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #212529;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            border-radius: .375rem;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        .btn-primary {
            color: #fff;
            background-color: #007bff;
            border-color: #007bff;
            box-shadow: none;
        }

        .btn {
            display: inline-block;
            font-weight: 400;
            color: #ffffff;
            text-align: center;
            vertical-align: middle;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-color: transparent;
            border: 1px solid transparent;
            padding: 0.375rem 0.75rem;
            font-size: 1rem;
            line-height: 1.5;
            border-radius: 0.25rem;
            transition: color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        [type=button], [type=reset], [type=submit], button {
            -webkit-appearance: button;
        }

        button, select {
            text-transform: none;
        }

        button, input {
            overflow: visible;
        }

        button, input, optgroup, select, textarea {
            margin: 0;
            font-family: inherit;
            font-size: inherit;
            line-height: inherit;
        }

        button {
            border-radius: 0;
        }

        .btn-sm {
            padding: 0.25rem 0.5rem;
            font-size: .875rem;
            line-height: 1.5;
            border-radius: 0.2rem;
        }


        element.style {
        }

        .custom-file-input {
            position: relative;
            z-index: 2;
            width: 100%;
            height: calc(2.25rem + 2px);
            margin: 0;
            overflow: hidden;
            opacity: 0;
        }

        /**, ::after, ::before {
            box-sizing: border-box;
        }*/

        .mb-2, .my-2 {
            margin-bottom: 0.5rem !important;
        }

        .mb-4, .my-4 {
            margin-bottom: 1.5rem !important;
        }

        div.table-responsive > div.dataTables_wrapper > div.row > div[class^=col-]:last-child {
            padding-right: 0;
        }

        div.table-responsive > div.dataTables_wrapper > div.row > div[class^=col-]:first-child {
            padding-left: 0;
        }

        /* Solid border */
        .solid {
            border-top: 3px solid #bbb;
        }

        table.dataTable th {
            font-size: 10px;
            color: black;
        }

        table.dataTable td {
            font-size: 10px;
            font-weight: normal;
            color: black;
        }

            table.dataTable td.focus {
                outline: 1px solid #ac1212;
                outline-offset: -3px;
                background-color: #f8e6e6 !important;
            }

        .negative {
            color: #fff;
            background-color: #17a2b8 !important;
        }

        .overlay {
            display: none;
            position: fixed;
            width: 100%;
            height: 100%;
            top: 0;
            left: 0;
            z-index: 999;
            background: rgba(255,255,255,0.8) url("/Image/Spinner.gif") center no-repeat;
        }
        /* Turn off scrollbar when body element has the loading class */
        body.loading {
            overflow: hidden;
        }
            /* Make spinner image visible when body element has the loading class */
            body.loading .overlay {
                display: block;
            }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="overlay"></div>

    <div class="container-fluid">
        <div class="row mb-4">
            <div class="col-md-6">
                <h3>DOS Vendor Reply</h3>
                <span><small style="color: cadetblue;" id="currentStatus"></small></span>
            </div>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label>Plant</label>
                    <select id="selectPlant" class="form-control chosen"></select>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Vendor</label>
                    <select id="selectVendor" class="form-control chosen" multiple="multiple"></select>

                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>PartsCode</label>
                    <select id="selectPartsCode" class="form-control chosen" multiple="multiple"></select>
                    <button type="button" id="btnFilter" class="btn btn-primary btn-sm"><i class="fa-solid fa-filter"></i> Filter</button>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
            </div>
            <div class="col-md-6">
                <div class="form-group">
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <button type="button" class="btn btn-primary btn-sm" id="btnExport" style="display:none"><i class="fa-solid fa-file-export"></i> Export</button>
        <button type="button" class="btn btn-primary btn-sm" id="btnSave" style="display:none"><i class="fa-solid fa-share-from-square"></i> Save</button>
        <div id="MyTable"></div>
        
    </div>


    <script type="text/javascript" src="../Scripts/jquery-3.5.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/HandsOnTableKey.js"></script>
    <script type="text/javascript" src="../Scripts/handsontable.js"></script>
    <script type="text/javascript" src="../Scripts/FileSaver.min.js"></script>
    <script type="text/javascript" src="../Scripts/exceljs.js"></script>
    <script type="text/javascript" src="../Scripts/js-xlsx-validation.js"></script>
    <script type="text/javascript" src="../Scripts/xlsx.full.min.js"></script>
    <script type="text/javascript" src="../Scripts/jexcel.js"></script>
    <script type="text/javascript" src="../Scripts/jsuites.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.mask.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.modal.min.js"></script>
    <script type="text/javascript" src="../js/moment/moment.min.js"></script>

    <script type="text/javascript" src="../Scripts/OtherFunctions.js"></script>
    <script type="text/javascript" src="../Scripts/chosen.proto.js"></script>
    <script type="text/javascript" src="../Scripts/DOSClone.js"></script>
</asp:Content>
