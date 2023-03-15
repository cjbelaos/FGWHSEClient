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
var isForViewing;
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

function CheckIfForViewing(callback) {
    $.ajax({
        type: "POST",
        url: GlobalURL + "Form/PSI_DOS_VENDOR_REPLY_BY_PLANT.aspx/CheckIfForViewing",
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
        url: GlobalURL + "Form/PSI_DOS_VENDOR_REPLY_BY_PLANT.aspx/GetPlantsByUserId",
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
        url: GlobalURL + "Form/PSI_DOS_VENDOR_REPLY_BY_PLANT.aspx/GetVendorsByUserId",
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
        url: GlobalURL + "Form/PSI_DOS_VENDOR_REPLY_BY_PLANT.aspx/GetProblemCategory",
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
        url: GlobalURL + "Form/PSI_DOS_VENDOR_REPLY_BY_PLANT.aspx/GetPartsCodeByPlantandVendors",
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
        url: GlobalURL + "Form/PSI_DOS_VENDOR_REPLY_BY_PLANT.aspx/GetDOSVendorReplyByPlant",
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
                if (userid !== 'GUEST' && isForViewing !== true) {
                    ShowButtons();
                }
                else {
                    HideButtons();
                }
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
function DrawTable(data, callback) {
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
                        source: arrProblemCategory,
                        allowInvalid: false,
                        wordWrap: false
                    };
                }
                if (j >= 7 && j <= 14) {
                    colType[j] = {
                        type: 'numeric', numericFormat: {
                            pattern: '0,00',
                            culture: 'en-US'
                        }, readOnly: true
                    };
                }
                if (j == 11) {
                    colType[j] = {
                        type: 'numeric',
                        mask: '#,###.00',
                        decimal: '.',
                        readOnly: true,
                        renderer: shortageEPPIRenderer
                    };
                }
                if (j == 13) {
                    colType[j] = {
                        type: 'numeric',
                        mask: '#,###.00',
                        decimal: '.',
                        readOnly: true,
                        renderer: shortageSupplierRenderer
                    };
                }

                if (j == 15) {
                    colType[j] = {
                        type: 'numeric',
                        mask: '#,###.00',
                        decimal: '.',
                        readOnly: true,
                        renderer: shortageOverallRenderer
                    };
                }
                if (j > (columns.length - 198)) {
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

        if ($('#MyTable').hasClass('handsontable htColumnHeaders')) {
            MyTable.updateSettings({
                fixedColumnsLeft: false
            })
        }

        var config = {
            data: d,
            colHeaders: columns,
            columns: colType,
            fixedColumnsLeft: 5,
            autoRowSize: true,
            csvHeaders: true,
            tableOverflow: true,
            stretchH: 'all',
            height: '300px',
            width: $("#MyTable").css("width"),
            colWidths: 100,
            afterChange: AfterChange,
            dropdownMenu: true,
            filters: true,
            filteringCaseSensitive: true,
            licenseKey: 'f8a35-a4d7b-607a2-f2225-39038',
        };
        MyTable = new Handsontable(document.getElementById("MyTable"), config);

        MyTable.updateSettings({
            cells: function (row, col) {
                var cellProperties = {};
                if (MyTable.getDataAtCell(row, col) < 0) {
                    cellProperties.renderer = negativeValueRenderer;
                }
                return cellProperties;
            },
        })
    }
    else {
        if ($('#MyTable').hasClass('handsontable htColumnHeaders')) {
            MyTable.destroy();
        }
        var config = {
            data: [],
            licenseKey: 'f8a35-a4d7b-607a2-f2225-39038'
        };
        MyTable = new Handsontable(document.getElementById("MyTable"), config);
    }

    if (callback !== undefined) {
        callback(data.length);
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
            var RIndex = headers.indexOf('REASON');
            var CMIndex = headers.indexOf('COUNTERMEASURE');
            if (col == PCIndex) {
                var ProblemCategory = MyTable.getDataAtCell(row, PCIndex);
                newVal = GetPIC(ProblemCategory);
                MyTable.setDataAtCell(row, col + 2, newVal);
            }
            if (col == RIndex) {
                var newVal = MyTable.getDataAtCell(row, RIndex).toUpperCase();
                MyTable.setDataAtCell(row, col, newVal, nv);
            }
            if (col == CMIndex) {
                var newVal = MyTable.getDataAtCell(row, CMIndex).toUpperCase();
                MyTable.setDataAtCell(row, col, newVal, nv);
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
 * to change the EPPIDOS to red
 */
function shortageEPPIRenderer(instance, td, row, col, prop, value, cellProperties) {
    Handsontable.renderers.TextRenderer.apply(this, arguments);
    if (value < instance.getDataAtCell(row, 10)) {
        instance.setCellMeta(row, 11, 'className', 'htRight htNumeric NegativeCell');
    }
};
/**
 * to change the SUPPLIERDOS to red
 */
function shortageSupplierRenderer(instance, td, row, col, prop, value, cellProperties) {
    Handsontable.renderers.TextRenderer.apply(this, arguments);
    if (value < instance.getDataAtCell(row, 12)) {
        instance.setCellMeta(row, 13, 'className', 'htRight htNumeric NegativeCell');
    }
};
/**

/**
 * to change the OVERALLDOS to red
 */
function shortageOverallRenderer(instance, td, row, col, prop, value, cellProperties) {
    Handsontable.renderers.TextRenderer.apply(this, arguments);
    if (value < instance.getDataAtCell(row, 14)) {
        instance.setCellMeta(row, 15, 'className', 'htRight htNumeric NegativeCell');
    }
};
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

    var arrProblemCategory = [];
    for (var i in DTProblemCategory) {
        arrProblemCategory.push(DTProblemCategory[i]['PROB_CAT_NAME']);
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
            var NumericColumns = ['EPPISTCK', 'SUPPLIERSTCK', 'TOTALSTCK', 'DOSLEVEL', 'EPPIDOS', 'SUPPLIERDOSLEVEL', 'SUPPLIERDOS', 'OVERALLDOSLEVEL', 'DOSOVERALL'];
            var Title = columns[j].title;
            var val = data[i][columns[j].field];
            lastCell = row.getCell(parseInt(j) + 1);
            if (NumericColumns.includes(Title)) {
                lastCell.numFmt = NumberFormatString;
            }
            lastCell.value = val;
        }
    }

    data.forEach((element, index) => {
        worksheet.getCell('Q' + (+index + 2)).dataValidation = {
            type: 'list',
            allowBlank: true,
            formulae: ['"' + arrProblemCategory.join() + '"']
        };

        worksheet.getCell('S' + (+index + 2)).value = {
            formula: '=IF(Q' + (+index + 2) + '="CALAMITY","PR PROC",IF(Q' + (+index + 2) + '="CAPACITY PROBLEM","PE/PR PROC/PCB",IF(Q' + (+index + 2) + '="MACHINE PROBLEM","PE/PR PROC",IF(Q' + (+index + 2) + '="MANPOWER PROBLEM","PR PROC",IF(Q' + (+index + 2) + '="MOLD PROBLEM","PE",IF(Q' + (+index + 2) + '="OTHERS", "PR PROC",IF(Q' + (+index + 2) + '="PACKAGING PROBLEM", "PR PROC",IF(Q' + (+index + 2) + '="POWER INTERRUPTION", "PR PROC",IF(Q' + (+index + 2) + '="QUALITY PROBLEM", "PE/PCB",IF(Q' + (+index + 2) + '="RAW MATERIAL SHORTAGE", "PR PROC",IF(Q' + (+index + 2) + '="SUB-ASSEMBLY CAPACITY","MIS","")))))))))))'
        };
    })

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
    //console.log(buffer);
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
function SaveData(isUploaded) {
    var Data = MyTable.getSourceData();
    var Header = MyTable.getColHeader();
    var newData = DataMapping(Header, Data);

    DTForExport = newData;

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
    //console.log("Old Data", DTOrig);
    //console.log("New Data", DataForSaving);
    DataForSaving = CompareData(DTOrig, DataForSaving);
    if (isUploaded === true) {
        DataForSaving = DataForSaving.length == 0 ? newData : DataForSaving;
    }
    //console.log("DataForSaving : ", DataForSaving);
    if (DataForSaving.length > 0) {
        $.ajax({
            url: GlobalURL + "Form/PSI_DOS_VENDOR_REPLY_BY_PLANT.aspx/SaveData",
            type: "POST",
            data: JSON.stringify({ DOS: DataForSaving }),
            contentType: 'application/json;charset=utf-8',
            dataType: 'json',
            success: function (e) {
                DTOrig = newData;
                var d = e.responseJSON;
                if (d == null || d == '') {
                    alert('Data has been saved successfully!');
                }
                else {
                    MessageBox(d.ExceptionType, '<p>' + d.Message + '</p><pre style="max-width: 100%;max-height: 300px;overflow:auto;">' + d.StackTrace + '</pre>');
                }

            },
            error: function (e) {
                var d = e.responseJSON;
                MessageBox(d.ExceptionType, '<p>' + d.Message + '</p><pre style="max-width: 100%;max-height: 300px;overflow:auto;">' + d.StackTrace + '</pre>');
            }
        });
    }
    else {
        alert('No data changes to be saved!');
    }
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

function ReadExcelFile(callback) {
    var selectedFile = $("#fileUpload")[0].files[0];
    var fileReader = new FileReader();
    fileReader.onload = function (event) {
        var data = event.target.result;
        var workbook = XLSX.read(data, { type: "binary" });
        workbook.SheetNames.forEach(sheet => {
            let rowObject = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheet], {
                defval: ""
            });
            let jsonObject = (rowObject);
            if (callback !== undefined) {
                callback(jsonObject);
            }
        });
    }
    fileReader.readAsBinaryString(selectedFile);
}

function ShowButtons() {
    $('#btnExport').css('display', 'inline-block');
    $('#btnSave').css('display', 'inline-block');
}

function HideButtons() {
    $('#btnExport').css('display', 'none');
    $('#btnSave').css('display', 'none');
}

$(function () {

    //var meta = ["<meta http-equiv='cache-control' content='no-cache'>", "<meta http-equiv='expires' content='0'>", "<meta http-equiv='pragma' content='no-cache'>"];
    //for (var i in meta) {
    //    $("head").append(meta[i]);
    //}
    
    var url = location.search;
    
    GetSessions(function (e) {
        userid = e["UserID"];
        userName = e["UserName"];
    });

    CheckIfForViewing(function (e) {
        isForViewing = e;
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

                    if (url !== "") {
                        var queryString = url.split("=");
                        var vendor = queryString[1];

                        console.log(queryString);
                        console.log(vendor);

                        $("#selectVendor").val(vendor).trigger("chosen:updated");
                        var parts = "";
                        isUploaded = false;
                        GetDOSVendorReplyByPlant(plant, vendor, parts);
                    }
                });
            }
        });
    });

    $("#selectPlant").change(function () {
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

    $('#selectVendor').change(function () {
        $('#selectPartsCode').empty();
        var plant = $('#selectPlant').val();
        var vendor = $.map($("#selectVendor option:selected"), function (e) {
            return $(e).val();
        });
        vendor = vendor.toString().replace(/,/g, "/");
        if (vendor === "") {
            vendor = userVendor === 'ALL' ? vendors : userVendor;
        }
        GetPartsCodeByPlantandVendors(plant, vendor, function (d) {
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
        isUploaded = false;
        var plant = $('#selectPlant').val();

        if ($("#selectVendor").val().length !== 0) {
            if ($("#selectVendor").val().length === 1) {
                vendor = $("#selectVendor option:selected").val();
            }
            else {
                vendor = $.map($("#selectVendor option:selected"), function (e) {
                    return $(e).val();
                });
                vendor = vendor.toString().replace(/,/g, "/");
            }
        }
        else {
            vendor = userVendor !== 'ALL' ? vendors : userVendor;
        }

        if ($("#selectPartsCode").val().length !== 0) {
            if ($("#selectPartsCode").val().length === 1) {
                parts = $("#selectPartsCode option:selected").val();
            }
            else {
                parts = $.map($("#selectPartsCode option:selected"), function (e) {
                    return $(e).val();
                });
                parts = parts.toString().replace(/,/g, "/");
            }
        }
        else {
            parts = "";
        }
        
        GetDOSVendorReplyByPlant(plant, vendor, parts);
    });

    $('#btnSave').click(function () {
        SaveData(false);
    });

    $("#fileUpload").on('change', function () {
        if (this.files[0]['type'] !== 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet') {
            alert('Only excel file can be uploaded!')
            $('#fileUpload').val('');
        }
    });

    $('#btnUpload').click(function () {
        var plant = $('#selectPlant').val();
        if ($("#fileUpload").val() !== '') {
            var formData = new FormData();
            formData.append("file", $("#fileUpload")[0].files[0]);
            ReadExcelFile(function (data) {
                $('#fileUpload').val('');
                DTForExport = data;
                var columns = ['PLANT', 'CATEGORY', 'MODELCODE', 'MATERIALNUMBER', 'MATERIALDESCRIPTION', 'SUPPLIERID', 'SUPPLIERNAME', 'EPPISTCK', 'SUPPLIERSTCK', 'TOTALSTCK', 'DOSLEVEL', 'EPPIDOS', 'SUPPLIERDOSLEVEL', 'SUPPLIERDOS', 'OVERALLDOSLEVEL', 'DOSOVERALL', 'PROBLEMCATEGORY', 'REASON', 'PIC', 'COUNTERMEASURE'];
                var columnNames = Object.keys(data[0]);
                columnNames.splice(columnNames.length - 198);
                var isSame = (columns.length === columnNames.length) && columns.every(function (element, index) {
                    return element === columnNames[index];
                });
                if (isSame != true) {
                    alert('Invalid excel columns format!');
                }
                else {
                    var arrPlant = [];
                    for (var i in data) {
                        arrPlant.push(data[i]['PLANT']);
                    }
                    var isValid = arrPlant.every((val) => val === plant)
                    if (isValid === true) {
                        DrawTable(data, function (e) {
                            if (e > 0) {
                                ShowButtons();
                                SaveData(true);
                            }
                            else {
                                HideButtons();
                            }
                        });
                    }
                    else {
                        alert('Incorrect plant detected!');
                    }
                }
            });
        }
        else {
            alert('Please choose file to upload!');
        }
    });

    $('#btnExport').click(function () {
        Export();
    });
});