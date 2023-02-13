//Initialize Chosen Elements
$('.chosen').chosen({ width: "300px" });

var userid;
var userName;

var arrVendors;
var arrParts;

var vendors;
var parts;

var userVendor;

var table_PartsSimulation;

var columns;

var hasData;

function getFormattedDate(date) {
    var year = date.getFullYear();

    var month = (1 + date.getMonth()).toString();
    month = month.length > 1 ? month : '0' + month;

    var day = date.getDate().toString();
    day = day.length > 1 ? day : '0' + day;

    return month + '/' + day + '/' + year;
}

function printString(columnNumber) {
    // To store result (Excel column name)
    let columnName = [];
    columnNumber = columnNumber + 1;
    while (columnNumber > 0) {
        // Find remainder
        let rem = columnNumber % 26;

        // If remainder is 0, then a
        // 'Z' must be there in output
        if (rem == 0) {
            columnName.push("Z");
            columnNumber = Math.floor(columnNumber / 26) - 1;
        }
        else // If remainder is non-zero
        {
            columnName.push(String.fromCharCode((rem - 1) + 'A'.charCodeAt(0)));
            columnNumber = Math.floor(columnNumber / 26);
        }
    }

    // Reverse the string and print result
    return columnName.reverse().join("");
}

function GetSessions(callback) {
    $.ajax({
        type: "POST",
        url: GlobalURL + "Form/GetSession.asmx/GetSessions",
        data: "{}",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (e) {
            var d = JSON.parse(e.d);
            if (callback !== undefined) {
                callback(d);
            }
        },
        failed: function (e) {
            console.log(e);
        },
        error: function (e) {
            console.log(e);
        }
    });
}

function GetPlantsByUserId(callback) {
    $.ajax({
        type: "POST",
        url: GlobalURL + "Form/PSI_PARTS_SIMULATION_BY_PLANT.aspx/GetPlantsByUserId",
        data: "{}",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $('.overlay').show();
        },
        complete: function () {
            $('.overlay').hide();
        },
        success: function (e) {
            var d = JSON.parse(e.d);
            if (callback !== undefined) {
                callback(d);
            }
        },
        failed: function (e) {
            console.log(e);
        },
        error: function (e) {
            console.log(e);
        }
    });
}

function GetVendorsByUserId(callback) {
    $.ajax({
        type: "POST",
        url: GlobalURL + "Form/PSI_PARTS_SIMULATION_BY_PLANT.aspx/GetVendorsByUserId",
        data: "{}",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $('.overlay').show();
        },
        complete: function () {
            $('.overlay').hide();
        },
        success: function (e) {
            var d = JSON.parse(e.d);
            if (callback !== undefined) {
                callback(d);
            }
        },
        failed: function (e) {
            console.log(e);
        },
        error: function (e) {
            console.log(e);
        }
    });
}

function GetPartsCodeByPlantandVendors(plant, vendors, callback) {
    $.ajax({
        type: "POST",
        url: GlobalURL + "Form/PSI_PARTS_SIMULATION_BY_PLANT.aspx/GetPartsCodeByPlantandVendors",
        data: JSON.stringify({ strPLANT: plant, strVENDORS: vendors }),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $('.overlay').show();
        },
        complete: function () {
            $('.overlay').hide();
        },
        success: function (e) {
            var d = JSON.parse(e.d);
            if (callback !== undefined) {
                callback(d);
            }
        },
        failed: function (e) {
            console.log(e);
        },
        error: function (e) {
            console.log(e);
        }
    });
}

function GetPartsSimulationByPlant(plant, parts, vendors, callback) {
    $.ajax({
        type: "POST",
        url: GlobalURL + "Form/PSI_PARTS_SIMULATION_BY_PLANT.aspx/GetPartsSimulationByPlant",
        data: JSON.stringify({ strPLANT: plant, strPARTS: parts, strVENDORS: vendors }),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $('.overlay').show();
        },
        complete: function () {
            $('.overlay').hide();
        },
        success: function (e) {
            var d = JSON.parse(e.d);
            if (callback !== undefined) {
                callback(d);
            }
            if (d.length > 0) {
                hasData = true;
                $("[id*=hasData]").val('true');
                console.log(d);
                //DrawTable(d);
                columnNames = Object.keys(d[0]);
                columns = [];
                for (var i in columnNames) {
                    columns.push({
                        title: columnNames[i],
                        data: columnNames[i]
                    });
                }

                var arrTargets = [];
                for (var i = 10; i < 208; i++) {
                    arrTargets.push(i);
                }

                if (table_PartsSimulation !== undefined && table_PartsSimulation !== null) {
                    table_PartsSimulation.clear().destroy();
                }

                var table_PartsSimulation = $('#table_PartsSimulation').DataTable({
                    paging: true,
                    filtering: true,
                    info: true,
                    searching: true,
                    "bLengthChange": true,
                    "deferRender": true,
                    data: d,
                    columns: columns,
                    columnDefs: [
                        { className: "dt-nowrap", targets: "_all" },
                        {
                            targets: arrTargets,
                            createdCell: function (td, cellData, rowData, row, col) {
                                if (cellData < 0) {
                                    $(td).css('background-color', '#ffc7ce')
                                }
                            }
                        },
                    ],
                    //scrollY: "300px",
                    scrollX: true,
                    scrollCollapse: true,
                    fixedColumns: {
                        left: 4
                    },
                });
            }
            else {
                alert('No Data Found');
                hasData = false;
                $("[id*=hasData]").val('false');
            }
        },
        failed: function (e) {
            console.log(e);
        },
        error: function (e) {
            console.log(e);
        }
    });
}


//Backup another process for datatables
function DrawTable(data) {
    //Create a HTML Table element.
    var table = $("<table id='table' class='display nowrap' style='width: 100 %'>");

    //Add the header row.
    var row = $(table[0].insertRow(-1));

    //Add the header cells.
    var headers = Object.keys(data[0]);

    for (var i in headers) {
        var headerCell = $("<th />");
        headerCell.html(headers[i]);
        row.append(headerCell);
    }

    //Add the data rows from Excel file.
    for (var i = 0; i < data.length; i++) {

        //Add the data row.
        var row = $(table[0].insertRow(-1));

        //Add the data cells.
        for (var j in headers) {
            var cellValue = data[i][headers[j]];
            var cell = $("<td />");
            cell.html(cellValue);
            row.append(cell);
        }
    }

    var dvPartsSimulation = $("#dvPartsSimulation");
    dvPartsSimulation.html("");
    dvPartsSimulation.append(table);

    var myTable = jQuery("#table");
    var thead = myTable.find("thead");
    var thRows = myTable.find("tr:has(th)");

    if (thead.length === 0) {  //if there is no thead element, add one.
        thead = jQuery("<thead></thead>").appendTo(myTable);
    }

    var copy = thRows.clone(true).appendTo("thead");
    thRows.remove();

    var arrTargets = [];
    for (var i = 10; i < 208; i++) {
        arrTargets.push(i);
    }

    var tablePS = $('#table').DataTable({
        columnDefs:
            [
                {
                    targets: arrTargets,
                    createdCell: function (td, cellData, rowData, row, col) {
                        if (cellData < 0) {
                            $(td).css('background-color', '#ffc7ce')
                        }
                    }
                },
            ],
        scrollX: true,
        scrollCollapse: true,
        dom: 'Blftirp',
        buttons: [
            {
                text: 'Export',
                action: function (e) {
                    console.log(data);
                    ExportToExcel();
                }
            }
        ]
    });
}
///// End

//Backup for processing excel in JS
function ProcessExcel(data) {
    var dataToUpload = [];
    //Read the Excel File data.
    var workbook = XLSX.read(data, {
        type: 'binary'
    });

    //Fetch the name of First Sheet.
    var firstSheet = workbook.SheetNames[0];

    //Read all rows from First Sheet into an JSON array.
    var excelRows = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[firstSheet]);

    //Create a HTML Table element.
    var table = $("<table id='table' />");
    table[0].border = "1";

    //Add the header row.
    var row = $(table[0].insertRow(-1));

    //Add the header cells.
    var headers = Object.keys(excelRows[0]);

    for (var i in headers) {
        var headerCell = $("<th />");
        headerCell.html(headers[i]);
        row.append(headerCell);
    }

    //Add the data rows from Excel file.
    for (var i = 0; i < excelRows.length; i++) {

        //Add the data row.
        var row = $(table[0].insertRow(-1));

        //Add the data cells.
        for (var j in headers) {
            var cellValue = excelRows[i][headers[j]];
            if (cellValue === 'SUPPLIER PLAN/DELIVERY') {
                var cell = $("<td class='need' />");
                dataToUpload.push(excelRows[i]);
            }
            else {
                var cell = $("<td />");
            }
            cell.html(cellValue);
            row.append(cell);
        }
    }

    var dvPartsSimulation = $("#dvPartsSimulation");
    dvPartsSimulation.html("");
    dvPartsSimulation.append(table);

    $('#table tr').each(function () {
        var test = $(this).find(".need").html();
    });
};
// End


function UploadFile(userid, strPlant, formData) {
    $.ajax({
        type: "POST",
        url: GlobalURL + "Form/PSI_PARTS_SIMULATION_BY_PLANT_UPLOAD.ashx?plantcode=" + strPlant + "&userid=" + userid,
        data: formData,
        cache: false,
        processData: false,
        contentType: false,
        beforeSend: function () {
            $('.overlay').show();
        },
        complete: function () {
            $('.overlay').hide();
        },
        success: function (e) {
            if (e !== 'error') {
                GetSessions(function (e) {
                    errorMessage = e["ErrorMessage"];
                    if (errorMessage !== "") {
                        alert(errorMessage);
                        if (!$.fn.DataTable.isDataTable('#table_PartsSimulation')) {
                            if ($('#selectVendor option:selected').val() === undefined && userVendor !== "ALL") {
                                var plant = $('#selectPlant').val();
                                var part = $('#selectPartsCode option:selected').val();
                                if (part === undefined) {
                                    part = '';
                                }
                                var vendor = vendors;
                                GetPartsSimulationByPlant(plant, part, vendor);
                            }
                            else if ($('#selectVendor option:selected').val() === undefined && userVendor === "ALL") {
                                var plant = $('#selectPlant').val();
                                var part = $('#selectPartsCode option:selected').val();
                                if (part === undefined) {
                                    part = '';
                                }
                                var vendor = userVendor;
                                GetPartsSimulationByPlant(plant, part, vendor);
                            }
                            else {
                                var plant = $('#selectPlant').val();
                                var part = $('#selectPartsCode option:selected').val();
                                if (part === undefined) {
                                    part = '';
                                }
                                var vendor = $("#selectVendor option:selected").val();
                                GetPartsSimulationByPlant(plant, part, vendor);
                            }
                        }
                        else {
                            var table = $('#table_PartsSimulation').DataTable();
                            var info = table.page.info();
                            if (info.recordsDisplay > 0) {
                                ResetDataTables();
                                if ($('#selectVendor option:selected').val() === undefined && userVendor !== "ALL") {
                                    var plant = $('#selectPlant').val();
                                    var part = $('#selectPartsCode option:selected').val();
                                    if (part === undefined) {
                                        part = '';
                                    }
                                    var vendor = vendors;
                                    GetPartsSimulationByPlant(plant, part, vendor);
                                }
                                else if ($('#selectVendor option:selected').val() === undefined && userVendor === "ALL") {
                                    var plant = $('#selectPlant').val();
                                    var part = $('#selectPartsCode option:selected').val();
                                    if (part === undefined) {
                                        part = '';
                                    }
                                    var vendor = userVendor;
                                    GetPartsSimulationByPlant(plant, part, vendor);
                                }
                                else {
                                    var plant = $('#selectPlant').val();
                                    var part = $('#selectPartsCode option:selected').val();
                                    if (part === undefined) {
                                        part = '';
                                    }
                                    var vendor = $("#selectVendor option:selected").val();
                                    GetPartsSimulationByPlant(plant, part, vendor);
                                }
                            }
                            else {
                                if ($('#selectVendor option:selected').val() === undefined && userVendor !== "ALL") {
                                    var plant = $('#selectPlant').val();
                                    var part = $('#selectPartsCode option:selected').val();
                                    if (part === undefined) {
                                        part = '';
                                    }
                                    var vendor = vendors;
                                    GetPartsSimulationByPlant(plant, part, vendor);
                                }
                                else if ($('#selectVendor option:selected').val() === undefined && userVendor === "ALL") {
                                    var plant = $('#selectPlant').val();
                                    var part = $('#selectPartsCode option:selected').val();
                                    if (part === undefined) {
                                        part = '';
                                    }
                                    var vendor = userVendor;
                                    GetPartsSimulationByPlant(plant, part, vendor);
                                }
                                else {
                                    var plant = $('#selectPlant').val();
                                    var part = $('#selectPartsCode option:selected').val();
                                    if (part === undefined) {
                                        part = '';
                                    }
                                    var vendor = $("#selectVendor option:selected").val();
                                    GetPartsSimulationByPlant(plant, part, vendor);
                                }
                            }
                        }
                        $('#fileUpload').val('');
                    }
                    else {
                        alert("Data successfully uploaded!")
                        if (!$.fn.DataTable.isDataTable('#table_PartsSimulation')) {
                            if ($('#selectVendor option:selected').val() === undefined && userVendor !== "ALL") {
                                var plant = $('#selectPlant').val();
                                var part = $('#selectPartsCode option:selected').val();
                                if (part === undefined) {
                                    part = '';
                                }
                                var vendor = vendors;
                                GetPartsSimulationByPlant(plant, part, vendor);
                            }
                            else if ($('#selectVendor option:selected').val() === undefined && userVendor === "ALL") {
                                var plant = $('#selectPlant').val();
                                var part = $('#selectPartsCode option:selected').val();
                                if (part === undefined) {
                                    part = '';
                                }
                                var vendor = userVendor;
                                GetPartsSimulationByPlant(plant, part, vendor);
                            }
                            else {
                                var plant = $('#selectPlant').val();
                                var part = $('#selectPartsCode option:selected').val();
                                if (part === undefined) {
                                    part = '';
                                }
                                var vendor = $("#selectVendor option:selected").val();
                                GetPartsSimulationByPlant(plant, part, vendor);
                            }
                        }
                        else {
                            var table = $('#table_PartsSimulation').DataTable();
                            var info = table.page.info();
                            if (info.recordsDisplay > 0) {
                                ResetDataTables();
                                if ($('#selectVendor option:selected').val() === undefined && userVendor !== "ALL") {
                                    var plant = $('#selectPlant').val();
                                    var part = $('#selectPartsCode option:selected').val();
                                    if (part === undefined) {
                                        part = '';
                                    }
                                    var vendor = vendors;
                                    GetPartsSimulationByPlant(plant, part, vendor);
                                }
                                else if ($('#selectVendor option:selected').val() === undefined && userVendor === "ALL") {
                                    var plant = $('#selectPlant').val();
                                    var part = $('#selectPartsCode option:selected').val();
                                    if (part === undefined) {
                                        part = '';
                                    }
                                    var vendor = userVendor;
                                    GetPartsSimulationByPlant(plant, part, vendor);
                                }
                                else {
                                    var plant = $('#selectPlant').val();
                                    var part = $('#selectPartsCode option:selected').val();
                                    if (part === undefined) {
                                        part = '';
                                    }
                                    var vendor = $("#selectVendor option:selected").val();
                                    GetPartsSimulationByPlant(plant, part, vendor);
                                }
                            }
                            else {
                                if ($('#selectVendor option:selected').val() === undefined && userVendor !== "ALL") {
                                    var plant = $('#selectPlant').val();
                                    var part = $('#selectPartsCode option:selected').val();
                                    if (part === undefined) {
                                        part = '';
                                    }
                                    var vendor = vendors;
                                    GetPartsSimulationByPlant(plant, part, vendor);
                                }
                                else if ($('#selectVendor option:selected').val() === undefined && userVendor === "ALL") {
                                    var plant = $('#selectPlant').val();
                                    var part = $('#selectPartsCode option:selected').val();
                                    if (part === undefined) {
                                        part = '';
                                    }
                                    var vendor = userVendor;
                                    GetPartsSimulationByPlant(plant, part, vendor);
                                }
                                else {
                                    var plant = $('#selectPlant').val();
                                    var part = $('#selectPartsCode option:selected').val();
                                    if (part === undefined) {
                                        part = '';
                                    }
                                    var vendor = $("#selectVendor option:selected").val();
                                    GetPartsSimulationByPlant(plant, part, vendor);
                                }
                            }
                        }
                        $('#fileUpload').val('');
                    }
                });
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function ResetDataTables() {
    $('#table_PartsSimulation').empty();
    var table = $('#table_PartsSimulation').DataTable();
    table.clear().destroy();
    $('#table_PartsSimulation > thead').remove();
    $('#table_PartsSimulation > tbody').remove();
}

$(function () {
    hasData = false;
    $("[id*=hasData]").val('false');

    GetSessions(function (e) {
        userid = e["UserID"];
        userName = e["UserName"];
    });

    GetPlantsByUserId(function (d) {
        for (var i in d) {
            $('<option>', {
                value: d[i]['PlantCode'],
                text: d[i]['Plant']
            }).appendTo($('#selectPlant'));
        }
        $("#selectPlant").trigger("chosen:updated");
        var plant = $('#selectPlant').val();
        GetVendorsByUserId(function (d) {
            if (d.length === 0) {
                $("#selectVendor").attr("data-placeholder", "No Vendor Options").trigger("chosen:updated");
                $("#selectPartsCode").attr("data-placeholder", "No PartsCode Options").trigger("chosen:updated");
            }
            else {
                userVendor = d[0]['VENDORS'];
                arrVendors = [];
                for (var i in d) {
                    arrVendors.push(d[i]['SupplierCode']); //insert all suppliercode into array
                    $('<option>', {
                        value: d[i]['SupplierCode'],
                        text: d[i]['Supplier']
                    }).appendTo($('#selectVendor'));
                }
                $("#selectVendor").attr("data-placeholder", "Select Vendor Options").trigger("chosen:updated");
                vendors = arrVendors.join('/');
                GetPartsCodeByPlantandVendors(plant, vendors, function (d) {
                    if (d.length === 0) {
                        $("#selectPartsCode").attr("data-placeholder", "No PartsCode Options").trigger("chosen:updated");
                    }
                    else {
                        arrParts = [];
                        for (var i in d) {
                            arrParts.push(d[i]['MATERIALCODE']);
                            $('<option>', {
                                value: d[i]['MATERIALCODE'],
                                text: d[i]['MATERIAL']
                            }).appendTo($('#selectPartsCode'));
                        }
                        $("#selectPartsCode").attr("data-placeholder", "Select PartsCode Options").trigger("chosen:updated");
                        parts = arrParts.join('/');
                    }
                });
            }
        });
    });

    $('#selectPlant').change(function () {
        var plant = $(this).val();
        $('#selectPartsCode').empty();
        GetPartsCodeByPlantandVendors(plant, vendors, function (d) {
            if (d.length === 0) {
                $("#selectPartsCode").attr("data-placeholder", "No PartsCode Options").trigger("chosen:updated");
            }
            else {
                arrParts = [];
                for (var i in d) {
                    arrParts.push(d[i]['MATERIALCODE']);
                    $('<option>', {
                        value: d[i]['MATERIALCODE'],
                        text: d[i]['MATERIAL']
                    }).appendTo($('#selectPartsCode'));
                }
                $("#selectPartsCode").attr("data-placeholder", "Select PartsCode Options").trigger("chosen:updated");
                parts = arrParts.join('/');
            }
        });
    });

    $('#selectVendor').change(function () {
        var plant = $('#selectPlant').val();
        $('#selectPartsCode').empty();
        vendor = $(this).val();
        if (vendor === null) {
            vendors = arrVendors.join('/');
        }
        else {
            vendors = vendor + ""
            vendors = vendors.replace(/,/g, "/");
        }
        GetPartsCodeByPlantandVendors(plant, vendors, function (d) {
            if (d.length === 0) {
                $("#selectPartsCode").attr("data-placeholder", "No PartsCode Options").trigger("chosen:updated");
            }
            else {
                for (var i in d) {
                    $('<option>', {
                        value: d[i]['MATERIALCODE'],
                        text: d[i]['MATERIAL']
                    }).appendTo($('#selectPartsCode'));
                }
                $("#selectPartsCode").attr("data-placeholder", "Select PartsCode Options").trigger("chosen:updated");
            }
        });
    });

    $('#btnFilter').click(function () {
        if (!$.fn.DataTable.isDataTable('#table_PartsSimulation')) {
            if ($('#selectVendor option:selected').val() === undefined && userVendor !== "ALL") {
                var plant = $('#selectPlant').val();
                var part = $('#selectPartsCode option:selected').val();
                if (part === undefined) {
                    part = '';
                }
                var vendor = vendors;
                GetPartsSimulationByPlant(plant, part, vendor);
            }
            else if ($('#selectVendor option:selected').val() === undefined && userVendor === "ALL") {
                var plant = $('#selectPlant').val();
                var part = $('#selectPartsCode option:selected').val();
                if (part === undefined) {
                    part = '';
                }
                var vendor = userVendor;
                GetPartsSimulationByPlant(plant, part, vendor);
            }
            else {
                var plant = $('#selectPlant').val();
                var part = $('#selectPartsCode option:selected').val();
                if (part === undefined) {
                    part = '';
                }
                var vendor = $("#selectVendor option:selected").val();
                GetPartsSimulationByPlant(plant, part, vendor);
            }
        }
        else {
            var table = $('#table_PartsSimulation').DataTable();
            var info = table.page.info();
            if (info.recordsDisplay > 0) {
                ResetDataTables();
                if ($('#selectVendor option:selected').val() === undefined && userVendor !== "ALL") {
                    var plant = $('#selectPlant').val();
                    var part = $('#selectPartsCode option:selected').val();
                    if (part === undefined) {
                        part = '';
                    }
                    var vendor = vendors;
                    GetPartsSimulationByPlant(plant, part, vendor);
                }
                else if ($('#selectVendor option:selected').val() === undefined && userVendor === "ALL") {
                    var plant = $('#selectPlant').val();
                    var part = $('#selectPartsCode option:selected').val();
                    if (part === undefined) {
                        part = '';
                    }
                    var vendor = userVendor;
                    GetPartsSimulationByPlant(plant, part, vendor);
                }
                else {
                    var plant = $('#selectPlant').val();
                    var part = $('#selectPartsCode option:selected').val();
                    if (part === undefined) {
                        part = '';
                    }
                    var vendor = $("#selectVendor option:selected").val();
                    GetPartsSimulationByPlant(plant, part, vendor);
                }
            }
            else {
                if ($('#selectVendor option:selected').val() === undefined && userVendor !== "ALL") {
                    var plant = $('#selectPlant').val();
                    var part = $('#selectPartsCode option:selected').val();
                    if (part === undefined) {
                        part = '';
                    }
                    var vendor = vendors;
                    GetPartsSimulationByPlant(plant, part, vendor);
                }
                else if ($('#selectVendor option:selected').val() === undefined && userVendor === "ALL") {
                    var plant = $('#selectPlant').val();
                    var part = $('#selectPartsCode option:selected').val();
                    if (part === undefined) {
                        part = '';
                    }
                    var vendor = userVendor;
                    GetPartsSimulationByPlant(plant, part, vendor);
                }
                else {
                    var plant = $('#selectPlant').val();
                    var part = $('#selectPartsCode option:selected').val();
                    if (part === undefined) {
                        part = '';
                    }
                    var vendor = $("#selectVendor option:selected").val();
                    GetPartsSimulationByPlant(plant, part, vendor);
                }
            }
        }
    });

    $("#fileUpload").on('change', function () {
        if (this.files[0]['type'] !== 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet') {
            alert('Only excel file can be uploaded!')
            $('#fileUpload').val('');
        }
    });

    $('#btnUpload').click(function () {
        var strPlant = $("#selectPlant").val();
        if ($("#fileUpload").val() !== '') {
            var formData = new FormData();
            formData.append("file", $("#fileUpload")[0].files[0]);
            UploadFile(userid, strPlant, formData);
        }
        else {
            alert('Please choose file to upload!');
        }
    });

    $("[id*=btnExport]").click(function () {
        if (hasData == false) {
            alert('Please extract data to export!')
        }
    });
});