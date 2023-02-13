/**
 Created By : Chris John Belaos
 Created Date: 12/06/2022
 */

var MyTable = null;
var NumberFormatString = '#,##0.00_);[Red](#,##0.00)';
var DTOrig = [];

var DTBusCategory = null;
var DTModel = null;
var DTProblemCategory = null;
var DTPlant = null;
var DTSupplier = null;

var DTForExport = null;

var userid;
var userName;
var arrVendors;
var arrParts;
var vendors;
var parts;
var userVendor;
var table_DOSVendorReply;
var columns;
var hasData;
var plant;
var part;
var vendor;


$('.chosen').chosen({ width: "300px" });

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
        url: GlobalURL + "Form/DOSClone.aspx/GetPlantsByUserId",
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
        url: GlobalURL + "Form/DOSClone.aspx/GetVendorsByUserId",
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

function GetProblemCategory(callback) {
    $.ajax({
        type: "POST",
        url: GlobalURL + "Form/DOSClone.aspx/GetProblemCategory",
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
/**
 * get person in charge based on problem category
 */
function GetPIC(Prob) {
    for (var i in DTProblemCategory) {
        if (DTProblemCategory[i]['PROB_CAT_NAME'] == Prob) {
            return DTProblemCategory[i]['PIC'];
        }
    }
    return null;
}

function GetPartsCodeByPlantandVendors(plant, vendors, callback) {
    $.ajax({
        type: "POST",
        url: GlobalURL + "Form/DOSClone.aspx/GetPartsCodeByPlantandVendors",
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

function GetDOSVendorReplyByPlant(plant, vendors, parts, callback) {
    $.ajax({
        type: "POST",
        url: GlobalURL + "Form/DOSClone.aspx/GetDOSVendorReplyByPlant",
        data: JSON.stringify({ strPLANT: plant, strVENDORS: vendors, strPARTS: parts }),
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
            DTForExport = d;
            if (callback !== undefined) {
                callback(d);
            }
            if (d.length > 0) {
                ShowButtons();
                for (var i in d) {
                    var newRow = {
                        Plant: d[i]['PLANT'],
                        SupplierCode: d[i]['SUPPLIERID'],
                        MaterialNumber: d[i]['MATERIALNUMBER'],
                        Problem_Cat: d[i]['PROBLEMCATEGORY'],
                        Reason: d[i]['REASON'],
                        PIC: d[i]['PIC'],
                        CounterMeasure: d[i]['COUNTERMEASURE'],
                        User: userid
                    };
                    DTOrig.push(newRow);
                }
                DrawTable(d);
            }
            else {
                alert('No data has been found!')
                HideButtons();
                DrawTable(d);
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

/**
 * Draw the excel table on page
 */
function DrawTable(data) {
    if (data.length > 0) {
        columnNames = Object.keys(data[0]);
        var columns = [];

        for (var i in columnNames) {
            columns.push(columnNames[i]);
        }

        var arrProblemCategory = [];

        for (var i in DTProblemCategory) {
            arrProblemCategory.push(DTProblemCategory[i]['PROB_CAT_NAME']);
        }

        let d = [data.length];
        let xColWidth = [columns.length];
        let colType = [columns.length];
        for (var i in data) {
            let e = [columns.length];
            for (var j in columns) {
                var colName = columns[j];
                xColWidth[j] = 100;
                e[j] = data[i][colName];
                colType[j] = { type: 'text', readOnly: true, wordWrap: false };
                if (['REASON', 'COUNTERMEASURE'].includes(colName)) {
                    colType[j] = { type: 'text', wordWrap: true };
                }
                if (columns[j] == 'PROBLEMCATEGORY') {
                    colType[j] = {
                        type: 'dropdown',
                        source: arrProblemCategory
                    };
                }
                if (j >= 7 && j <= 12) {
                    colType[j] = {
                        type: 'numeric', numericFormat: {
                            pattern: '0,00',
                            culture: 'en-US'
                        }, readOnly: true
                    };
                }
                if (j >= 17) {
                    colType[j] = {
                        type: 'numeric', numericFormat: {
                            pattern: '0,00',
                            culture: 'en-US'
                        }, readOnly: true
                    };
                }
            }
            d[i] = e;
        }

        var config = {
            data: d,
            colHeaders: columns,
            columns: colType,
            csvHeaders: true,
            tableOverflow: true,
            height: '300px',
            width: $("#MyTable").css("width"),
            colWidths: 100,
            afterChange: AfterChange,
            dropdownMenu: true,
            filters: true,
            filteringCaseSensitive: true,
            licenseKey: 'f8a35-a4d7b-607a2-f2225-39038'
        };
        MyTable = new Handsontable(document.getElementById("MyTable"), config);

        MyTable.updateSettings({
            cells: function (row, col) {
                var cellProperties = {};

                if (MyTable.getDataAtCell(row, col) < 0) {
                    cellProperties.renderer = negativeValueRenderer;
                }

                return cellProperties;
            }
        })
    }
    else {
        if (MyTable !== null) {
            MyTable.destroy();
            var config = {
                data: [],
                licenseKey: 'f8a35-a4d7b-607a2-f2225-39038'
            };
            MyTable = new Handsontable(document.getElementById("MyTable"), config);
        }
    }
    
}
/**
 * event after cell change
 */
function AfterChange(data, hook) {
    if (hook == "edit") {
        hook = [data];
    }
    if (hook[0] !== null) {
        hook = hook[0];
        var row = hook[0][0];
        var col = hook[0][1];
        var ov = hook[0][2];
        var nv = hook[0][3];
        var newVal = '';
        if (MyTable !== null) {
            var headers = MyTable.getColHeader();
            var PCIndex = headers.indexOf('PROBLEMCATEGORY');
            if (col == PCIndex) {
                var ProblemCategory = MyTable.getDataAtCell(row, PCIndex);
                newVal = GetPIC(ProblemCategory);
                MyTable.setDataAtCell(row, col + 2, newVal);
            }
        }
    }
}
/**
 * to change the negative value to red
 */
function negativeValueRenderer(instance, td, row, col, prop, value, cellProperties) {
    Handsontable.renderers.TextRenderer.apply(this, arguments);
    instance.setCellMeta(row, col, 'className', 'htRight htNumeric NegativeCell');
    value = value.toString();
    $(td).text('(' + value.replace('-', '') + ')');
}
/**
 * creating the excel file based on data sent
 */
function Export(callback) {

    var currentDate = new Date();
    var filename = 'DosVendorReply(' + moment(currentDate).format('YYYYMMDDhhmmss') + ').xlsx';

    var data = DTForExport;
    var columns = [];

    var headers = MyTable.getColHeader();

    for (var i in headers) {
        columns[columns.length] = {
            field: headers[i],
            title: headers[i]
        };
    }

    //Create workbook and worksheet
    var workbook = new ExcelJS.Workbook();
    workbook.creator = 'ISD';
    workbook.lastModifiedBy = 'ISD';
    workbook.created = new Date();
    workbook.company = "EPPI";
    workbook.title = "DOS Vendor Reply";
    workbook.modified = new Date();
    workbook.lastPrinted = new Date();
    workbook.properties.date1904 = true;
    // Force workbook calculation on load
    workbook.calcProperties.fullCalcOnLoad = true;
    workbook.views = [
        {
            x: 0, y: 0, width: 10000, height: 20000,
            firstSheet: 0, activeTab: 1, visibility: 'visible'
        }
    ]
    var worksheet = workbook.addWorksheet('DOS Vendor Reply');
    //index for row and column of ExcelJS starts with 1
    var rowIndex = 1;
    var row = worksheet.getRow(rowIndex);
    //initialize the table header
    for (var i in columns) {
        row.getCell(parseInt(i) + 1).value = columns[i].title;
    }

    var ranges = selectRange(worksheet, 1, 1, 1, columns.length);
    ranges.forEach(function (range) {
        range.style = {
            font: { bold: true }
        };
        range.alignment = { vertical: 'middlen', horizontal: 'center' };
    });
    //loop the data
    rowIndex = 0;
    var lastCell = null;
    for (var i in data) {
        if (rowIndex == 0) {
            rowIndex = 2;
        } else {
            rowIndex++;
        }
        row = worksheet.getRow(rowIndex);

        var ctr = 0;
        for (var j in columns) {
            var NumericColumns = ['EPPISTCK', 'EPPIDOS', 'DOSLEVEL', 'SUPPLIERSTCK', 'DOSOVERALL', 'TOTALSTCK'];
            var Title = columns[j].title;
            var val = data[i][columns[j].field];
            lastCell = row.getCell(parseInt(j) + 1);
            if (NumericColumns.includes(Title)) {
                lastCell.numFmt = NumberFormatString;
            }
            lastCell.value = val;
        }
    }

    worksheet.addConditionalFormatting({
        ref: 'G2:' + lastCell._address,
        rules: [
            {
                type: 'cellIs',
                operator: 'lessThan',
                formulae: ['0'],
                style: {
                    fill: {
                        type: 'pattern',
                        pattern: 'solid',
                        bgColor: { argb: 'FFFFC7CE' }
                    }
                },
            }
        ]
    });

    const buffer = workbook.xlsx.writeBuffer();
    console.log(buffer);
    //for downloading the file
    workbook.xlsx.writeBuffer().then(data => {
        const blob = new Blob([data], { type: this.blobType });
        saveAs(blob, filename);
        if (callback !== undefined) {
            callback();
        }
    });
}
/**
 * for saving
 */
function SaveData() {
    var Data = MyTable.getSourceData();
    var Header = MyTable.getColHeader();
    var newData = DataMapping(Header, Data);
    var DataForSaving = [];

    for (var i in newData) {
        var newRow = {
            Plant: newData[i]['PLANT'],
            SupplierCode: newData[i]['SUPPLIERID'],
            MaterialNumber: newData[i]['MATERIALNUMBER'],
            Problem_Cat: newData[i]['PROBLEMCATEGORY'],
            Reason: newData[i]['REASON'] === undefined ? null : newData[i]['REASON'],
            PIC: newData[i]['PIC'] === undefined ? null : newData[i]['PIC'],
            CounterMeasure: newData[i]['COUNTERMEASURE'] === undefined ? null : newData[i]['COUNTERMEASURE'],
            User: userid
        };
        DataForSaving.push(newRow);
    }
    newData = DataForSaving;
    ////for debug mode
    console.log("Old Data",DTOrig);
    console.log("New Data", DataForSaving);
    DataForSaving = CompareData(DTOrig, DataForSaving);
    console.log("DataForSaving : ",DataForSaving);
    //$.ajax({
    //    url: GlobalURL + "Form/DOSClone.aspx/SaveData",
    //    type: "POST",
    //    data: JSON.stringify({ DOS: DataForSaving }),
    //    contentType: 'application/json;charset=utf-8',
    //    dataType: 'json',
    //    success: function (e) {
    //        DTOrig = newData;
    //        console.log(DTOrig);
    //        var d = e.responseJSON;
    //        if (d == null || d == '') {
    //            alert('Data has been saved successfully!');
    //        }
    //        else {
    //            MessageBox(d.ExceptionType, '<p>' + d.Message + '</p><pre style="max-width: 100%;max-height: 300px;overflow:auto;">' + d.StackTrace + '</pre>');
    //        }
            
    //    },
    //    error: function (e) {
    //        var d = e.responseJSON;
    //        MessageBox(d.ExceptionType, '<p>' + d.Message + '</p><pre style="max-width: 100%;max-height: 300px;overflow:auto;">' + d.StackTrace + '</pre>');
    //    }
    //});
}
/**
 * to compare the old and new data
 */
function CompareData(oData, nData) {
    var data = [];
    for (var i in oData) {
        var oPlant = oData[i]['Plant'];
        var oSupplierCode = oData[i]['SupplierCode'];
        var oMaterialNumber = oData[i]['MaterialNumber'];
        var oProblem_Cat = oData[i]['Problem_Cat'];
        var oReason = oData[i]['Reason'];
        var oPIC = oData[i]['PIC'];
        var oCounterMeasure = oData[i]['CounterMeasure'];
        var oUser = oData[i]['User'];
        for (var j in nData) {
            var nPlant = nData[j]['Plant'];
            var nSupplierCode = nData[j]['SupplierCode'];
            var nMaterialNumber = nData[j]['MaterialNumber'];
            var nProblem_Cat = nData[j]['Problem_Cat'];
            var nReason = nData[j]['Reason'];
            var nPIC = nData[j]['PIC'];
            var nCounterMeasure = nData[j]['CounterMeasure'];
            var nUser = nData[j]['User'];
            if (oPlant == nPlant && oSupplierCode == nSupplierCode && oMaterialNumber == nMaterialNumber && oUser == nUser) {
                if (oProblem_Cat != nProblem_Cat || oReason != nReason || oPIC != nPIC || oCounterMeasure != nCounterMeasure) {
                    data[data.length] = nData[j];
                    break;
                }
                break;
            }
        }
    }
    return data;
}
/**
 * map the data based on columns
 */
function DataMapping(header, value) {
    var obj = [];
    for (var row in value) {
        var newRow = {};
        for (var col in header) {
            newRow[header[col]] = value[row][col];
        }
        obj.push(newRow);
    }
    return obj;
}

function Filter() {
    if ($('#selectVendor option:selected').val() === undefined && userVendor !== "ALL") {
        var plant = $('#selectPlant').val();
        var part = $('#selectPartsCode option:selected').val();
        if (part === undefined) {
            part = '';
        }
        var vendor = vendors;
        GetDOSVendorReplyByPlant(plant, vendor, part);
    }
    else if ($('#selectVendor option:selected').val() === undefined && userVendor === "ALL") {
        var plant = $('#selectPlant').val();
        var part = $('#selectPartsCode option:selected').val();
        if (part === undefined) {
            part = '';
        }
        var vendor = userVendor;
        GetDOSVendorReplyByPlant(plant, vendor, part);
    }
    else {
        var plant = $('#selectPlant').val();
        var part = $('#selectPartsCode option:selected').val();
        if (part === undefined) {
            part = '';
        }
        var vendor = $("#selectVendor option:selected").val();
        GetDOSVendorReplyByPlant(plant, vendor, part);
    }
}

function ShowButtons() {
    $('#btnExport').css('display','inline-block');
    $('#btnSave').css('display', 'inline-block');
}

function HideButtons() {
    $('#btnExport').css('display', 'none');
    $('#btnSave').css('display', 'none');
}

$(function () {
    GetSessions(function (e) {
        userid = e["UserID"];
        userName = e["UserName"];
    });

    GetProblemCategory(function (e) {
        DTProblemCategory = e;
    })

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

    $('#btnFilter').click(function () {
        Filter();
    });

    $('#btnSave').click(function () {
        SaveData();
    });

    $('#btnExport').click(function () {
        Export();
    });
});