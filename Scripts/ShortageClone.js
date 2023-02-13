/**
 Created By : Chris John Belaos
 Created Date: 12/06/2022
 */

var MyTable = null;
var Vendors = null;
var AssignedVendor = null;
var DTForExport = null;
var arrDTForExport = [];
var NumberFormatString = '#,##0.00_);[Red](#,##0.00)';

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
        url: GlobalURL + "Form/ShortageClone.aspx/GetPlantsByUserId",
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
        url: GlobalURL + "Form/ShortageClone.aspx/GetVendorsByUserId",
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

function GetShortageListByPlant(strPlant, strSupplierId, callback) {
    $.ajax({
        url: GlobalURL + "Form/ShortageClone.aspx/GetShortageListByPlant",
        type: "POST",
        data: JSON.stringify({ strPLANT: strPlant, strSupplierId: strSupplierId }),
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        beforeSend: function () {
            $('.overlay').show();
        },
        complete: function () {
            $('.overlay').hide();
        },
        success: function (e) {
            var d = JSON.parse(e.d);
            DTForExport = d;
            console.log(DTForExport)
            if (callback !== undefined) {
                callback(d);
            }
            if (d.length > 0) {
            //    ShowButtons();
                DrawTable(d);
                GetShortageListByPlantUpdatedDate(function (e) {
                    var LastUpdatedDate = '(Last Update - ' + e + ')';
                    $('#lblUpdatedDate').text(LastUpdatedDate);
                })
            }
            else {
                alert('No data has been found!')
                //HideButtons();
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

function GetShortageListByPlantUpdatedDate(callback) {
    $.ajax({
        url: GlobalURL + "Form/ShortageClone.aspx/GetShortageListByPlantUpdatedDate",
        type: "POST",
        data: '{}',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
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

function DrawTable(data) {
    if (data.length > 0) {
        columnNames = Object.keys(data[0]);
        var columns = [];

        for (var i in columnNames) {
            columns.push(columnNames[i]);
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
                if (['OWNSTOCK'].includes(colName)) {
                    colType[j] = {
                        type: 'numeric', numericFormat: {
                            pattern: '0,00',
                            culture: 'en-US'
                        }, readOnly: true };
                }
                if (j >= 7) {
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

        var hiddenColumns = {
            columns: [0],
            indicators: false
        };

        var config = {
            data: d,
            colHeaders: columns,
            columns: colType,
            hiddenColumns: hiddenColumns,
            csvHeaders: true,
            tableOverflow: true,
            height: '300px',
            width: $("#MyTable").css("width"),
            contextMenu: {
                items: {
                    'New Row': {
                        name: 'NewRow', callback: function (key, options) {
                            //console.log(key, options);
                            var numberOfRow = (options[0]['end']['row'] - options[0]['start']['row']) + 1;
                            var lastRow = options[0]['end']['row'] + 1;
                            //MyTable.alter(action,index,amount,source,keepEmptyRows);
                            MyTable.alter('insert_row', lastRow, numberOfRow);
                        }
                    }, 'Remove Row': {
                        name: 'RemoveRow', callback: function (key, options) {
                            var numberOfRow = (options[0]['end']['row'] - options[0]['start']['row']) + 1;
                            var firstRow = options[0]['start']['row'];
                            //MyTable.alter(action,index,amount,source,keepEmptyRows);
                            MyTable.alter('remove_row', firstRow, numberOfRow);
                        }
                    }
                }
            },
            colWidths: xColWidth,
            dropdownMenu: true,
            filters: true,
            filteringCaseSensitive: true,
            allowInsertRow: true,
            licenseKey: 'f8a35-a4d7b-607a2-f2225-39038'
        };
        MyTable = new Handsontable(document.getElementById("MyTable"), config);
    }
    else {
        MyTable.destroy();
        var config = {
            data: [],
            licenseKey: 'f8a35-a4d7b-607a2-f2225-39038'
        };
        MyTable = new Handsontable(document.getElementById("MyTable"), config);
    }
}
/**
 * creating the excel file based on data sent
 */
//function Export(callback) {

//    var currentDate = new Date();
//    var filename = 'ShortageList(' + moment(currentDate).format('YYYYMMDDhhmmss') + ').xlsx';

//    var data = DTForExport;
//    //var data = arrDTForExport;
//    var columns = [];

//    var headers = MyTable.getColHeader();

//    for (var i in headers) {
//        if (i != 0) {
//            columns[columns.length] = {
//                field: headers[i],
//                title: headers[i]
//            };
//        }
//    }

//    //Create workbook and worksheet
//    var workbook = new ExcelJS.Workbook();
//    workbook.creator = 'ISD';
//    workbook.lastModifiedBy = 'ISD';
//    workbook.created = new Date();
//    workbook.company = "EPPI";
//    workbook.title = "DOS Vendor Reply";
//    workbook.modified = new Date();
//    workbook.lastPrinted = new Date();
//    workbook.properties.date1904 = true;
//    // Force workbook calculation on load
//    workbook.calcProperties.fullCalcOnLoad = true;
//    workbook.views = [
//        {
//            x: 0, y: 0, width: 10000, height: 20000,
//            firstSheet: 0, activeTab: 1, visibility: 'visible'
//        }
//    ]
//    var worksheet = workbook.addWorksheet('Shortage List');
//    //index for row and column of ExcelJS starts with 1
//    var rowIndex = 1;
//    var row = worksheet.getRow(rowIndex);
//    //initialize the table header
//    for (var i in columns) {
//        row.getCell(parseInt(i) + 1).value = columns[i].title;
//    }

//    var ranges = selectRange(worksheet, 1, 1, 1, columns.length);
//    ranges.forEach(function (range) {
//        range.style = {
//            font: { bold: true }
//        };
//        range.alignment = { vertical: 'middlen', horizontal: 'center' };
//    });
//    //loop the data
//    rowIndex = 0;
//    var lastCell = null;
//    for (var i in data) {
//        if (rowIndex == 0) {
//            rowIndex = 2;
//        } else {
//            rowIndex++;
//        }
//        row = worksheet.getRow(rowIndex);
//        console.log(row);

//        for (var j in columns) {
//                var NumericColumns = ['OWNSTOCK'];
//                var Title = columns[j].title;
//                var val = data[i][columns[j].field];
//                lastCell = row.getCell(parseInt(j) + 1);
//                if (NumericColumns.includes(Title)) {
//                    lastCell.numFmt = NumberFormatString;
//                }
//                lastCell.value = val;
//        }
//    }

//    worksheet.addConditionalFormatting({
//        ref: 'G2:' + lastCell._address,
//        rules: [
//            {
//                type: 'cellIs',
//                operator: 'lessThan',
//                formulae: ['0'],
//                style: {
//                    fill: {
//                        type: 'pattern',
//                        pattern: 'solid',
//                        bgColor: { argb: 'FFFFC7CE' }
//                    }
//                },
//            }
//        ]
//    });

//    const buffer = workbook.xlsx.writeBuffer();
//    console.log(buffer);
//    //for downloading the file
//    workbook.xlsx.writeBuffer().then(data => {
//        const blob = new Blob([data], { type: this.blobType });
//        saveAs(blob, filename);
//        if (callback !== undefined) {
//            callback();
//        }
//    });
//}

function UploadFile(userid, plant, formData) {
    $.ajax({
        xhr: function () {
            var xhr = new window.XMLHttpRequest();
            xhr.upload.addEventListener("progress", function (evt) {
                if (evt.lengthComputable) {
                    var percentComplete = evt.loaded / evt.total;
                    $('.progress').css({
                        width: percentComplete * 100 + '%'
                    });
                    if (percentComplete === 1) {
                        $('.progress').addClass('hide');
                    }
                }
            }, false);
            xhr.addEventListener("progress", function (evt) {
                if (evt.lengthComputable) {
                    var percentComplete = evt.loaded / evt.total;
                    $('.progress').css({
                        width: percentComplete * 100 + '%'
                    });
                }
            }, false);
            return xhr;
        },
        type: "POST",
        url: GlobalURL + "Form/PSI_SHORTAGELIST_BY_PLANT_UPLOAD.ashx?plant=" + plant + "&userid=" + userid,
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
            var plant = $('#selectPlant').val();
            var vendor = $("#selectVendor option:selected").val();
            if (vendor === undefined) {
                vendor = AssignedVendor !== 'ALL' ? Vendors : AssignedVendor;
            }
            if (e !== 'error') {
                GetSessions(function (e) {
                    errorMessage = e["ErrorMessage"];
                    if (errorMessage !== "") {
                        alert(errorMessage);
                    }
                    else {
                        alert("Data successfully uploaded!")
                    }
                    GetShortageListByPlant(plant, vendor);
                });
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}

//function ShowButtons() {
//    $('#btnExport').css('display', 'inline-block');
//    $('#btnSave').css('display', 'inline-block');
//}

//function HideButtons() {
//    $('#btnExport').css('display', 'none');
//    $('#btnSave').css('display', 'none');
//}

$(function () {
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
        GetVendorsByUserId(function (d) {
            if (d.length === 0) {
                $("#selectVendor").attr("data-placeholder", "No Vendor Options").trigger("chosen:updated");
            }
            else {
                AssignedVendor = d[0]['VENDORS'];
                arrVendors = [];
                for (var i in d) {
                    arrVendors.push(d[i]['SupplierCode']); //insert all suppliercode into array
                    $('<option>', {
                        value: d[i]['SupplierCode'],
                        text: d[i]['Supplier']
                    }).appendTo($('#selectVendor'));
                }
                $("#selectVendor").attr("data-placeholder", "Select Vendor Options").trigger("chosen:updated");
                Vendors = arrVendors.join('/');
            }
        });
    });

    $('#btnFilter').click(function () {
        var plant = $('#selectPlant').val();
        var vendor = $("#selectVendor option:selected").val();
        if (vendor === undefined) {
            vendor = AssignedVendor !== 'ALL' ? Vendors : AssignedVendor;
        }
        GetShortageListByPlant(plant, vendor);
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
            UploadFile(userid, plant, formData);
        }
        else {
            alert('Please choose file to upload!');
        }
    });

    //$('#btnExport').click(function () {
    //    Export();
    //});
});