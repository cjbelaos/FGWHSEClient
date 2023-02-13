//var NumberFormatString = '#,##0.00_);[Red](#,##0.00)';
var NumberFormatString = '#,##0);[Red](#,##0)';
/**
 * get list of dos based on date(per month)
 */
function GetDOSMonthly(date, callback) {
    $.ajax({
        url: GlobalURL + "Form/PSIDOSMonthly.aspx/GetMonthly",
        type: "POST",
        data: JSON.stringify({ date:date }),
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            if (callback !== undefined) {
                callback(e);
            }
        },
        error: function (e) {
            var d = e.responseJSON;
            MessageBox(d.ExceptionType, '<p>' + d.Message + '</p><pre style="max-width: 100%;max-height: 300px;overflow:auto;">' + d.StackTrace + '</pre>');
        }
    });
}
/**
 * draw the excel table to screen
 */
function DrawTable(data) {
    console.log(data);
    var iTitles = ['SUPPLIERS', 'VENDORCODE', 'NO OF PARTS'];
    var iColumns = ['SupplierName', 'SupplierCode', 'NoOfParts'];
    for (var i in data[0]) {
        if (IsDate(i.replace(/_/g, "/").substr(1))) {
            iTitles[iTitles.length] = HumanDateFormat(i.replace(/_/g, "/").substr(1));
            iColumns[iColumns.length] = i;
        }
    }
    iTitles[iTitles.length] = "AVERAGE";
    iTitles[iTitles.length] = "%";
    
    let d = [data.length];
    let xColWidth = [iColumns.length];
    let colType = [iColumns.length];
    for (var i in data) {
        let e = [iColumns.length+2];
        for (var j in iColumns) {
            var colName = iColumns[j];
            xColWidth[j] = 100;
            e[j] = data[i][colName];
            colType[j] = { type: 'text', readOnly: true, wordWrap: false };
            
            if (j >= 4) {
                colType[j] = {
                    type: 'numeric', numericFormat: {
                        pattern: '0,00',
                        culture: 'en-US'
                    }, readOnly: true
                };
            }
        }
        colType[iColumns.length] = {
            type: 'numeric', numericFormat: {
                pattern: { mantissa: 0},
                culture: 'en-US'
            }, readOnly: true
        };
        colType[iColumns.length + 1] = {
            type: 'numeric', numericFormat: {
                pattern: { mantissa: 0},
                culture: 'en-US'
            }, readOnly: true
        };
        e[iColumns.length] = String.format('=AVERAGE(D{0}:{1}{0})',(parseInt(i)+1),toColumnName(iColumns.length));
        e[iColumns.length + 1] = String.format('=100-({1}{0}/C{0})',(parseInt(i)+1),toColumnName(iColumns.length));
        d[i] = e;
    }
    console.log(d,iTitles);
    MyTable = new Handsontable(document.getElementById("MyTable"), {
        data: d,
        colHeaders: iTitles,
        columns: colType,
        csvHeaders: true,
        tableOverflow: true,
        height: '300px',
        width: $("#MyTable").css("width"),
        colWidths: 100,
        licenseKey: 'f8a35-a4d7b-607a2-f2225-39038',
        formulas:true
    });
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
/**
 * export the data to excel
 */
function ExportToExcel(data, callback) {
    if (data.length == 0) {
        alert("No Records found");
        return false;
    }
    var columns = [
        { field: "SupplierName", title: "SUPPLIERS" },
        { field: "SupplierCode", title: "VENDOR CODE" },
        { field: "NoOfParts", title: "NO OF PARTS" }
    ];
    for (var i in data[0]) {
        var col = i.replace(/_/g, '/').substring(1);
        if (IsDate(i.replace(/_/g, '/').substring(1))) {
            columns[columns.length] = {
                field: i,
                title: HumanDateFormat(col)
            };
        }
    }
    columns[columns.length] = {
        field:null,
        title:"AVERAGE",
    };
    columns[columns.length] = {
        field:null,
        title:"%",
    };
    //console.log(data,columns);
    //return false;
    var workbook = new ExcelJS.Workbook();
    workbook.creator = 'ISD';
    workbook.lastModifiedBy = 'ISD';
    workbook.created = new Date();
    workbook.company = "EPPI";
    workbook.title = "DOS Monthly Report";
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
    var worksheet = workbook.addWorksheet('DOS Monthly Report');
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
            var NumericColumns = ['NO OF PARTS'];
            var Title = columns[j].title;
            var val = data[i][columns[j].field];
            lastCell = row.getCell(parseInt(j) + 1);
            if (NumericColumns.includes(Title) || IsDate(Title)) {
                lastCell.numFmt = NumberFormatString;
            }
            lastCell.value = val;
            if (Title == "AVERAGE") {
                val = String.format('=AVERAGE(D{0}:{1}{0})', (parseInt(i) + 2), toColumnName(columns.length-2));
                lastCell.numFmt = NumberFormatString;
                lastCell.value = { formula:val };
            }
            if (Title == "%") {
                val = String.format('=100-({1}{0}/C{0})', (parseInt(i) + 2), toColumnName(columns.length-1));
                lastCell.numFmt = NumberFormatString;
                lastCell.value = { formula:val };
            }
        }
    }
    //Generate Excel File with given name
    workbook.xlsx.writeBuffer().then((data) => {
        let blob = new Blob([data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
        saveAs(blob, "DOS Monthly Report.xlsx");
    });
    if (callback !== undefined) {
        callback();
    }
}
$(document).ready(function () {
    $('.panel-loading').hide();
    $('.icon-exporting').hide();
    $('#search-date').val(HumanDateFormat(new Date())).datepicker();
    $('#btnSearch').on('click', function () {
        var date = $('#search-date').val();
        if (date != "" && IsDate(date)) {
            GetDOSMonthly(date, function (e) {
                e = JSON.parse(e.d);
                DrawTable(e);
            });
        }
    });
    $('#btnExport').on('click', function () {
        $('#btnExport').hide();
        $('.icon-exporting').show();
        var date = $('#search-date').val();
        if (date != "" && IsDate(date)) {
            GetDOSMonthly(date, function (e) {
                e = JSON.parse(e.d);
                ExportToExcel(e, function () {
                    $('#btnExport').show();
                    $('.icon-exporting').hide();
                });
            });
        }
    });
});