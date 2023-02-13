<%@ Page Title="" Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="PSI_MASTER_VENDORS_CATEGORY.aspx.cs" Inherits="FGWHSEClient.Form.PSIMasterList" %>

<asp:Content ID="style" ContentPlaceHolderID="css" runat="server">
    <link href="../font/Poppins.css" rel="stylesheet" type="text/css" />
    <link href="../fontawesome-free-6.2.0-web/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="../DataTables/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <link href="../DataTables/css/fixedColumns.dataTables.min.css" rel="stylesheet" type="text/css" />
    <link href="../DataTables/css/editor.dataTables.min.css" rel="stylesheet" type="text/css" />
    <link href="../Buttons-2.2.3/css/buttons.dataTables.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        * {
            font-family: 'Poppins';
        }

        .row {
            display: -ms-flexbox;
            display: flex;
            -ms-flex-wrap: wrap;
            flex-wrap: wrap;
            margin-right: -7.5px;
            margin-left: -7.5px;
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

        #MyTable_length,
        #MyTable_filter,
        #MyTable_info {
            color: #666;
        }

        div.dataTables_length select {
            color: #666;
        }

        div.dataTables_filter input {
            color: #666;
        }

        .dataTables_wrapper .dataTables_paginate .paginate_button {
            box-sizing: border-box;
            display: inline-block;
            min-width: 1.5em;
            padding: 0.5em 1em;
            margin-left: 2px;
            text-align: center;
            text-decoration: none !important;
            cursor: pointer;
            color: #0d69fd !important;
            border: 1px solid transparent;
            border-radius: 2px;
        }

            .dataTables_wrapper .dataTables_paginate .paginate_button.current, .dataTables_wrapper .dataTables_paginate .paginate_button.current:hover {
                color: white !important;
                border: 1px solid rgba(13, 105, 253);
                background-color: rgba(13, 105, 253);
                background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, rgba(230, 230, 230, 0.1)), color-stop(100%, rgba(0, 0, 0, 0.1)));
                background: -webkit-linear-gradient(top, rgba(13, 105, 253) 0%, rgba(13, 105, 253) 100%);
                background: -moz-linear-gradient(top, rgba(13, 105, 253) 0%, rgba(13, 105, 253) 100%);
                background: -ms-linear-gradient(top, rgba(13, 105, 253) 0%, rgba(13, 105, 253) 100%);
                background: -o-linear-gradient(top, rgba(13, 105, 253) 0%, rgba(13, 105, 253) 100%);
                background: linear-gradient(to bottom, rgba(13, 105, 253) 0%, rgba(13, 105, 253) 100%);
            }

            .dataTables_wrapper .dataTables_paginate .paginate_button:hover {
                color: white !important;
                border: 1px solid #0d69fd;
                background-color: #0d69fd;
                background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, #0d69fd), color-stop(100%, white));
                background: -webkit-linear-gradient(top, #0d69fd 0%, #0d69fd 100%);
                background: -moz-linear-gradient(top, #0d69fd 0%, #0d69fd 100%);
                background: -ms-linear-gradient(top, #0d69fd 0%, #0d69fd 100%);
                background: -o-linear-gradient(top, #0d69fd 0%, #0d69fd 100%);
                background: linear-gradient(to bottom, #0d69fd 0%, #0d69fd 100%);
            }

        table.dataTable th {
            font-size: 13px;
            font-family: 'Poppins';
            color: #666;
        }

        table.dataTable td {
            font-size: 13px;
            font-family: 'Poppins';
            font-weight: normal;
            color: #666;
        }

        table.dataTable thead > tr > th.sorting::before,
        table.dataTable thead > tr > th.sorting::after {
            font-size: 1.9em;
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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="overlay"></div>
    <div class="container-fluid">
        <div class="row mb-4">
            <div class="col-md-6">
                <h3>Vendors Master</h3>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <div>
            <div class="table-responsive">
                <table id="MyTable" class="table table-bordered table-condensed table-hover" style="white-space: nowrap">
                </table>
            </div>
        </div>
    </div>



    <script src="../js/jquery/jquery-3.5.1.js" type="text/javascript"></script>
    <script src="../DataTables/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../DataTables/js/dataTables.fixedColumns.min.js" type="text/javascript"></script>
    <script src="../Buttons-2.2.3/js/dataTables.buttons.min.js" type="text/javascript"></script>
    <script src="../Buttons-2.2.3/js/buttons.html5.min.js" type="text/javascript"></script>
    <script src="../js/moment/moment-with-locales.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.proto.js" type="text/javascript"></script>
    <script src="../Scripts/VendorsMaster.js" type="text/javascript"></script>


</asp:Content>
