//const ExcelJS = require('/Scripts/exceljs.js');
/*
 * Script for Parts Simulation
 * emon
 * 02/01/2021
 */

/**
 * to get the list of material
 * then append to datalist control
 * it will act as combobox
 */
var MyTable = null;
var NumberFormatString = '#,##0_);[Red](#,##0)';
/**
 * get list of supplier
 */
function GetVendor(callback) {
    $.ajax({
        url: GlobalURL + "Form/PSIPartsSimulation.aspx/GetVendor", type: "POST",
        data: '',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            e = JSON.parse(e.d);
            if (callback !== undefined) {
                callback(e);
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
 * get list of materials
 */
function GetMaterial(callback) {
    $.ajax({
        url: GlobalURL + "Form/PSIPartsSimulation.aspx/GetMaterial", type: "POST",
        data: '',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            e = JSON.parse(e.d);
            if (callback !== undefined) {
                callback(e);
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
 * get data based on filter
 */
function GetData(PlantID, MaterialNumber,VendorCode,callback) {
     $.ajax({
        url: GlobalURL + "Form/PSIPartsSimulation.aspx/GetData", type: "POST",
        data: JSON.stringify({ PlantID: PlantID, MaterialNumber: MaterialNumber,VendorCode:VendorCode,Formulated:false }),
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            e = JSON.parse(e.d);
            if (callback !== undefined) {
                callback(e);
            }
        },
        failed: function (e) {
            alert('Request Failed');
            console.log(e);
        },
        error: function (e) {
            var d = e.responseJSON;
            MessageBox(d.ExceptionType, '<p>' + d.Message + '</p><pre style="max-width: 100%;max-height: 300px;overflow:auto;">' + d.StackTrace+'</pre>');
        }
    });
}
/**
 * draw excel table to screen(old)
 */
function DrawTable(iColumns, iTitles, data, forHide) {
    //console.log(iColumns, iTitles);
    //return false;
    forHide = forHide === undefined ? {} : forHide;
    for (var i in data[0]) {
        if (IsDate(i)) {
            iColumns[iColumns.length] = i;
            iTitles[iTitles.length] = HumanDateFormat(i);
        } else if (IsDate(i.replace(/_/g, "/").substr(1))) {
            iColumns[iColumns.length] = i;
            iTitles[iTitles.length] = HumanDateFormat(i.replace(/_/g, "/").substr(1));
        }
    }
    //console.log(iColumns,iTitles);
    //console.log(data);
    let d = [data.length];
    let xColWidth = [iColumns.length];
    let colType = [iColumns.length];
    for (var i in data) {
        let e = [iColumns.length];
        for (var j in iColumns) {
            var colName = iColumns[j];
            xColWidth[j] = 100;
            e[j] = data[i][colName];
            colType[j] = { type: 'text', readOnly: true };
            if (IsDate(iTitles[j]) || colName == "Past Due") {
                colType[j] = {
                    type: 'numeric', numericFormat: {
                        pattern: '0,00',
                        culture: 'en-US'
                    }
                };
                if (data[i]['Plan/Logical'] == 'END STOCKS 1') {
                    if (colName == 'Past Due') {
                        e[j] = e[j - 2] - data[i - 2][iColumns[j]];
                    } else {
                        e[j] = e[j - 1] - data[i - 2][iColumns[j]];
                    }
                }
                if (data[i]['Plan/Logical'] == 'END STOCKS 2') {
                    if (colName == 'Past Due') {
                        e[j] = e[j - 2] + data[i - 2][iColumns[j]] - data[i - 3][iColumns[j]];
                    } else {
                        e[j] = e[j - 1] + data[i - 2][iColumns[j]] - data[i - 3][iColumns[j]];
                    }
                }
            }
        }
        d[i] = e;
    }
    if ($("#MyTable").hasClass("jexcel_container")) {
        $("#MyTable").jexcel('destroy');
        $("#MyTable").removeClass('jexcel_container');
    }
    //console.log(d);
    var config = {
        data: d,
        colHeaders: iTitles,
        colWidths: xColWidth,
        columns: colType,
        hiddenColumns: forHide,
        csvHeaders: true,
        tableOverflow: true,
        beforeChange:BeforeChange,
        afterChange:AfterChange,
        height: '300px',
        width: $("#MyTable").css("width"),
        licenseKey: 'f8a35-a4d7b-607a2-f2225-39038'
    };
    MyTable = Handsontable(document.getElementById("MyTable"), config);
    //MyTable.updateSettings({
    //    cells: function (row, col) {
    //        var cellProperties = {};
    //        if (MyTable.getData()[row][9] == 'SUPPLIER PLAN/DELIVERY' && col > 9) {
    //            cellProperties.readOnly = false;
    //        }
    //        return cellProperties;
    //    }
    //});
    //Handsontable.hooks.add('afterChange', AfterChange, MyTable);
}
/**
 * draw excel table to screen
 */
function DrawNewTable(data, forHide,callback) {
    forHide = forHide === undefined ? {} : forHide;
    //console.log(data);
    var InitialColumns = ['Model', 'Plant', 'PlantCode', 'MaterialNumber',
        'MaterialDescription','Supplier', 'SupplierName', 'EPPISTCK', 'SupplierSTCK', 'TotalSTCK', 'PlanLogical', 'Past'];
    for (var i in data[0]) {
        if (IsDate(i.replace(/_/g, "/").substr(1))) {
            InitialColumns[InitialColumns.length] = i;
        }
    }
    console.log(InitialColumns);
    for (var i in InitialColumns) {
        var colName = InitialColumns[i];
        if (IsDate(colName.replace(/_/g, "/").substr(1))) {
            colName = HumanDateFormat(colName.replace(/_/g, "/").substr(1));
        }
    }
    let d = [data.length];
    let xColWidth = [InitialColumns.length];
    let lstCol = [InitialColumns.length];
    let colType = [InitialColumns.length];
    for (var i in data) {
        let e = [InitialColumns.length];
        for (var j in InitialColumns) {
            var colName = InitialColumns[j];
            xColWidth[j] = 100;
            e[j] = data[i][colName];
            colType[j] = { type: 'text', readOnly: true,wordWrap:false };
            if (colName == "EPPISTCK" || colName == "SupplierSTCK" || colName == "TotalSTCK") {
                colType[j] = {
                    type: 'numeric', numericFormat: {
                        pattern: '0,00',
                        culture: 'en-US'
                    },readOnly:true
                };
            }
            if (colName == "Past") {
                colType[j] = {
                    type: 'numeric', numericFormat: {
                        pattern: '0,00',
                        culture: 'en-US'
                    }
                };
            }
            if (IsDate(colName.replace(/_/g, '/').substr(1))) {
                colType[j] = {
                    type: 'numeric', numericFormat: {
                        pattern: '0,00',
                        culture: 'en-US'
                    }
                };
                colName = HumanDateFormat(colName.replace(/_/g, '/').substr(1));
            }
            lstCol[j] = colName;
        }
        d[i] = e;
    }
    var config = {
        data: d,
        colHeaders: lstCol,
        colWidths: xColWidth,
        hiddenColumns: forHide,
        columns: colType,
        csvHeaders: true,
        beforeChange: BeforeChange,
        afterChange: AfterChange,
        tableOverflow: true,
        height: '300px',
        width: $("#MyTable").css("width"),
        licenseKey: 'f8a35-a4d7b-607a2-f2225-39038'
    };
    MyTable = Handsontable(document.getElementById("MyTable"), config);
    if (callback !== undefined) {
        callback();
    }
}
/**
 * event before the changes of cell
 */
function BeforeChange(data, hook) {
    if (hook == "edit") {
        hook = [data];
    }
    if (hook[0] !== null) {
        hook = hook[0];
        var row = hook[0][0];
        var col = hook[0][1];
        var ov = hook[0][2];
        var nv = hook[0][3];
        var newVal = 0;
        if (MyTable !== null) {
            var headers = MyTable.getColHeader();
            var PLIndex = headers.indexOf('Plan/Logical') < 0 ? headers.indexOf('PlanLogical') : headers.indexOf('Plan/Logical');
            var PlanLogical = MyTable.getDataAtCell(row, PLIndex);
            if (PlanLogical != 'SUPPLIER PLAN/DELIVERY' && PlanLogical !== 'END STOCKS 2') {
                return false;
            }
        }
    }
}
/**
 * event after the changes of cell
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
        var newVal = 0;
        if (MyTable !== null) {
            var headers = MyTable.getColHeader();
            var PLIndex = headers.indexOf('Plan/Logical') < 0 ? headers.indexOf('PlanLogical') : headers.indexOf('Plan/Logical');
            var PlanLogical = MyTable.getDataAtCell(row,PLIndex);
            if (PlanLogical == 'SUPPLIER PLAN/DELIVERY') {
                if (headers[col] == 'Past' || headers[col] == 'PastDue') {
                    console.log(MyTable.getDataAtCell(row + 2, col - 2),nv,MyTable.getDataAtCell(row - 1, col));
                    newVal = parseFloat(MyTable.getDataAtCell(row + 2, col - 2)) + parseFloat(nv) - parseFloat(MyTable.getDataAtCell(row - 1, col));
                    MyTable.setDataAtCell(row + 2, col, newVal);
                } else {
                    newVal = parseFloat(MyTable.getDataAtCell(row + 2, col - 1)) + parseFloat(nv) - parseFloat(MyTable.getDataAtCell(row - 1, col));
                    MyTable.setDataAtCell(row + 2, col, newVal);
                }
            }
            if (PlanLogical == 'END STOCKS 2') {
                if (headers.length - 1 > col) {
                    var currentCellValue = MyTable.getDataAtCell(row - 2, col + 1);
                    MyTable.setDataAtCell(row - 2, col + 1, currentCellValue);
                }
            }
        }
    }
}
/**
 * formatting the negative value in cell
 */
function negativeValueRenderer(instance, td, row, col, prop, value, cellProperties) {
    Handsontable.renderers.TextRenderer.apply(this, arguments);
    instance.setCellMeta(row, col, 'className', 'htRight htNumeric NegativeCell');
    $(td).text('(' + value.replace('-', '') + ')');
}
/**
 * backup code for old drawing of excel table to screen
 */
function DrawNewTable1(data) {
    //console.log(data);
    var InitialColumns = ['Model', 'PlantID', 'PlantCode', 'MaterialNumber',
        'MaterialDescription', 'Supplier', 'EPPISTCK', 'SupplierSTCK', 'TotalSTCK', 'PlanLogical', 'Past'];
    for (var i in data[0]) {
        if (IsDate(i.replace(/_/g, "/").substr(1))) {
            InitialColumns[InitialColumns.length] = i;
        }
    }

    for (var i in InitialColumns) {
        var colName = InitialColumns[i];
        if (IsDate(colName.replace(/_/g, "/").substr(1))) {
            colName = HumanDateFormat(colName.replace(/_/g, "/").substr(1));
        }
    }
    let d = [3];
    let xColWidth = [InitialColumns.length];
    let lstCol = [InitialColumns.length];
    let colType = [InitialColumns.length];
    for (var i in data) {
        let e = [InitialColumns.length];
        for (var j in InitialColumns) {
            var colName = InitialColumns[j];
            xColWidth[j] = 100;
            e[j] = data[i][colName];
            colType[j] = { type: 'text', readOnly: true };
            if (colName == "Past") {
                colType[j] = { type: 'numeric', mask: '[-]#,###'};
            }
            if (IsDate(colName.replace(/_/g, '/').substr(1))) {
                colType[j] = { type: 'numeric', mask: '[-]#,###' };
                colName = HumanDateFormat(colName.replace(/_/g, '/').substr(1));
            }
            lstCol[j] = colName;
        }
        d[i] = e;
    }

    if ($("#MyTable").hasClass("jexcel_container")) {
        $("#MyTable").jexcel('destroy');
        $("#MyTable").removeClass('jexcel_container');
    }
    console.log(d);
    var excelTable = jexcel(document.getElementById("MyTable"), {
        data: d,
        colHeaders: lstCol,
        colWidths: xColWidth,
        columns: colType,
        csvHeaders: true,
        tableOverflow: true,
        tableHeight: '300px',
        tableWidth: $("#MyTable").css("width"),
        updateTable: function (instance, cell, col, row, val, label, cellName) {
            if ((col >= 6 && col <= 8) || col >= 10) {
                $(cell).css('text-align','right');
                var value = parseFloat(label.replace(/,/g,''));
                if (value < 0) {
                    $(cell).css({
                        background: '#F00',
                        color: '#FFF'
                    }).text('('+label.replace('-','')+')');
                } else {
                    $(cell).css({
                        background: '#FFF',
                        color: '#000'
                    });
                }
            }
        }
    });
    $(excelTable.records).each(function (e) {
        var td = $(this);
        if (e == 0 || e == 2 || e == 3) {
            $(td).each(function (e) {
                $(this).addClass("readonly");
            });
        }
    });
}
/**
 * drawing of the excel table to screen(old)
 */
function DrawExcelTable(data) {
    var InitialColumns = [];
    for (var i in data[0]) {
        if (data[0][i] == "Plant Code") {
            data[0][i] = "PlantCode";
        }
        if (data[0][i] == "MODEL") {
            data[0][i] = "Model";
        }
        if (data[0][i] == "Material Number") {
            data[0][i] = "MaterialNumber";
        }
        if (data[0][i] == "Material Description") {
            data[0][i] = "MaterialDescription";
        }
        if (data[0][i] == "EPPI STCK") {
            data[0][i] = "EPPISTCK";
        }
        if (data[0][i] == "Supplier STCK") {
            data[0][i] = "SupplierSTCK";
        }
        if (data[0][i] == "TOTAL STCK") {
            data[0][i] = "TotalSTCK";
        }
        if (data[0][i] == "PLAN/LOGICAL") {
            data[0][i] = "PlanLogical";
        }
        if (data[0][i] == "PAST DUE") {
            data[0][i] = "Past";
        }
        InitialColumns[InitialColumns.length] = data[0][i];
    }
    //for (var i in InitialColumns) {
    //    var colName = InitialColumns[i];
    //    if (IsDate(colName.replace(/_/g, "/").substr(1))) {
    //        colName = HumanDateFormat(colName.replace(/_/g, "/").substr(1));
    //    }
    //}
    let d = [3];
    var PlanLogicalIndex = InitialColumns.indexOf("PlanLogical");
    let xColWidth = [InitialColumns.length];
    let lstCol = [InitialColumns.length];
    let colType = [InitialColumns.length];
    for (var i = 1 in data) {
        var currentRow = i;
        let e = [InitialColumns.length];
        for (var j in InitialColumns) {
            var colName = InitialColumns[j];
            xColWidth[j] = 100;
            e[j] = data[i][j];
            colType[j] = { type: 'text', readOnly: true };
            if (colName == "Past") {
                colType[j] = { type: 'text' };
                if (e[PlanLogicalIndex] == "END STOCKS") {
                    e[j] = String.format("={0}{1}+{2}{3}-{4}{5}", ExcelColumn(PlanLogicalIndex - 1), currentRow - 1, ExcelColumn(j), currentRow - 1, ExcelColumn(j), currentRow - 2);
                }
            }
            if (IsDate(colName)) {
                colType[j] = { type: 'text' };
                colName = HumanDateFormat(colName);
                if (e[PlanLogicalIndex] == "END STOCKS") {
                    e[j] = String.format("={0}{1}+{2}{3}-{4}{5}", ExcelColumn(j - 1), currentRow, ExcelColumn(j), currentRow-1, ExcelColumn(j), currentRow - 2);
                }
            }
            lstCol[j] = colName;
        }
        d[i-1] = e;
    }
    //console.log(lstCol,data);
    if ($("#MyTable").hasClass("jexcel_container")) {
        $("#MyTable").jexcel('destroy');
    }
    //console.log(d);
    var excelTable = jexcel(document.getElementById("MyTable"), {
        data: d,
        colHeaders: lstCol,
        colWidths: xColWidth,
        columns: colType,
        csvHeaders: true
    });
    $(excelTable.records).each(function (row, cells) {
        if ($(cells[PlanLogicalIndex]).text() != "SUPPLIER PLAN/DELIVERY") {
            $(cells).each(function () {
                $(this).addClass("readonly");
            });
        }
        //if (e == 0 || e == 2) {
        //    $(td).each(function (e) {
        //        $(this).addClass("readonly");
        //    });
        //}
    });
}
/**
 * get list of plants
 */
function GetPlants(callback) {
    $.ajax({
        url: GlobalURL + "Form/PSIPartsSimulation.aspx/GetPlants", type: "POST",
        data: '',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            e = JSON.parse(e.d);
            if (callback !== undefined) {
                callback(e);
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
 * export to excel based on filter
 */
function ExportToExcel(PlantID, MaterialNumber, VendorCode,callback) {
    var columns = [
        { field: 'Model', title: 'Model' },
        { field: 'PlantCode', title: 'Plant' },
        { field: 'MaterialNumber', title: 'Material' },
        { field: 'MaterialDescription', title: 'Description' },
        { field: 'Supplier', title: 'Supplier' },
        { field: 'SupplierName', title: 'SupplierName' },
        { field: 'EPPISTCK', title: 'EPPISTCK' },
        { field: 'SupplierSTCK', title: 'SupplierSTCK' },
        { field: 'TotalSTCK', title: 'TotalSTCK' },
        { field: 'PlanLogical', title: 'Plan/Logical' },
        { field: 'Past', title: 'Past Due' }
    ];
    var headers = MyTable.getColHeader();
    for (var i in headers) {
        if (IsDate(headers[i])) {
            columns[columns.length] = {
                field: UnderDateFormat(headers[i]),
                title: headers[i]
            };
        }
    }
    $.ajax({
        url: GlobalURL + "Form/PSIPartsSimulation.aspx/GetData", type: "POST",
        data: JSON.stringify({ PlantID: PlantID,VendorCode:VendorCode, MaterialNumber: MaterialNumber ,Formulated:true}),
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            var data = JSON.parse(e.d);
            console.log(data);
            if (data.length > 0) {
                CreateExcelFile(columns,data,'Parts Simulation.xlsx',callback);
            } else {
                alert("No Records Found");
            }
        },
        failed: function (e) {
            console.log(e);
        },
        error: function (e) {
            var d = e.responseJSON;
            MessageBox(d.ExceptionType, '<p>' + d.Message + '</p><pre style="max-width: 100%;max-height: 300px;overflow:auto;">' + d.StackTrace + '</pre>');
        }
    });
}
/**
 * creating excel file based on the data received
 */
function CreateExcelFile(columns, data, filename,callback) {
    //console.log(columns,data);
    //file initialization
    var workbook = new ExcelJS.Workbook();
    workbook.creator = 'ISD';
    workbook.lastModifiedBy = 'ISD';
    workbook.created = new Date();
    workbook.company = "EPPI";
    workbook.title = "Parts Simulation";
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
    var worksheet = workbook.addWorksheet('Parts Simulation');
    //index for row and column of ExcelJS starts with 1
    var rowIndex = 1;
    var row = worksheet.getRow(rowIndex);
    var row2, row3, row4, colTotalStock, strFormula = "";
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
    console.log(row);
    console.log(columns);
    console.log(data);
    var lastCell = null;
    for (var i in data) {
        if (rowIndex == 0) {
            rowIndex = 2;
        } else {
            rowIndex++;
        }
        row = worksheet.getRow(rowIndex);
        var ctr = 0;
        //var MaterialNumber = data[i]['MaterialNumber'];
        for (var j in columns) {
            var NumericColumns = ['EPPISTCK', 'SupplierSTCK', 'TotalSTCK','Plan/Logical','Past Due'];
            var Title = columns[j].title;
            var val = data[i][columns[j].field];
            lastCell = row.getCell(parseInt(j) + 1);
            if (NumericColumns.includes(Title)) {
                lastCell.numFmt = NumberFormatString;
            }
            if (val !== null && val !== undefined) {
                if (val.charAt(0) == '=') {
                    val = { formula: val.substr(1) };
                    lastCell.numFmt = NumberFormatString;
                } else {
                    if (parseInt(val).toString() != 'NaN') {
                        val = parseInt(val);
                    }
                }
            }
            lastCell.value = val;
        }
    }
    worksheet.addConditionalFormatting({
        ref: 'F2:' + lastCell._address,
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
 * save the simulation in the database
 */
function SaveSimulation(callback) {
    var headers = MyTable.getColHeader();
    var data = MyTable.getSourceData();
    var newData = DataMapping(headers, data);
    //console.log(newData);
    var DataForSaving = [];
    for (var i in newData) {
        for (var j in headers) {
            var Title = headers[j];
            var Value = newData[i][Title];
            if (newData[i]['PlanLogical'] == 'END STOCKS 2' || newData[i]['PlanLogical'] == 'SUPPLIER PLAN/DELIVERY' || newData[i]['Plan/Logical'] == 'END STOCKS 2' || newData[i]['Plan/Logical'] == 'SUPPLIER PLAN/DELIVERY') {
                var NewRow = {
                    MaterialNumber: newData[i]['MaterialNumber'] === undefined ? newData[i]['Material'] : newData[i]['MaterialNumber'],
                    PlantID: newData[i]['PlantID'] === undefined?GetPlantID(newData[i]['Plant']):newData[i]['PlantID'],
                    ForDos: newData[i]['PlanLogical'] == 'END STOCKS 2' || newData[i]['Plan/Logical'] == 'END STOCKS 2'?1:0
                };
                if (Title !== undefined) {
                    if (Title == 'Past' || IsDate(Title)) {
                        NewRow['DateInput'] = Title;
                        NewRow['Value'] = Value;
                        DataForSaving.push(NewRow);
                    }
                }
            }
        }
    }
    console.log(DataForSaving);
    var interval = window.setInterval(function (e) {
        GetMessage(function (m) {
            $("#currentStatus").text(m.d);
        });
    }, 1000);
    $.ajax({
        url: GlobalURL + "Form/PSIPartsSimulation.aspx/SaveData", type: "POST",
        data: JSON.stringify({ obj: DataForSaving }),
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            clearInterval(interval);
            $("#currentStatus").text("Done...");
            if (callback !== undefined) {
                callback(e);
            }
        },
        failed: function (e) {
            console.log(e);
            alert("Failed to save");
        },
        error: function (e) {
            var d = e.responseJSON;
            MessageBox(d.ExceptionType, '<p>' + d.Message + '</p><pre style="max-width: 100%;max-height: 300px;overflow:auto;">' + d.StackTrace + '</pre>');
        }
    });
}
/**
 * get current status based on action taken
 */
function GetMessage(callback) {
    $.ajax({
        url: GlobalURL + "Classes/StaticInfo.asmx/GetMessage", type: "POST",
        data: '',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e, a, x) {
            if (callback !== undefined) {
                callback(e);
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
 * mapping the data based on columns
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
/**
 * backup code for saving the simulation
 */
function SaveSimulation1() {
    var data = GetJExcelValue();
    //var data = $('#MyTable').jexcel('getData');
    //console.log(data);
    //return false;
    var headers = $('#MyTable').jexcel('getHeaders', true);
    var PLI = headers.indexOf('PlanLogical');
    var forSaving = [];
    if (data.length > 0) {
        for (var i in data) {
            var j = data[i];
            console.log(j[PLI]);
            if (j[PLI] == "SUPPLIER PLAN/DELIVERY") {
                for (var k in j) {
                    if (headers[k] == "Past" || IsDate(headers[k])) {
                        if (headers.includes("PlantID")) {
                            forSaving[forSaving.length] = {
                                'MaterialNumber': j[headers.indexOf("MaterialNumber")],
                                'PlantID': j[headers.indexOf("PlantID")],
                                'ForDos': 0,
                                'DateInput': headers[k],
                                'Value': j[k]
                            };
                        } else {
                            if (headers.includes("PlantCode")) {
                                forSaving[forSaving.length] = {
                                    'MaterialNumber': j[headers.indexOf("MaterialNumber")],
                                    'PlantID': GetPlantID(j[headers.indexOf("PlantCode")]),
                                    'ForDos': 0,
                                    'DateInput': headers[k],
                                    'Value': j[k]
                                };
                            }
                        }
                    }
                }
            }
            if (j[PLI] == "END STOCKS 2") {
                for (var k in j) {
                    if (headers[k] == "Past" || IsDate(headers[k])) {
                        if (headers.includes("PlantID")) {
                            forSaving[forSaving.length] = {
                                'MaterialNumber': j[headers.indexOf("MaterialNumber")],
                                'PlantID': j[headers.indexOf("PlantID")],
                                'ForDos': 1,
                                'DateInput': headers[k],
                                'Value': j[k]
                            };
                        } else {
                            if (headers.includes("PlantCode")) {
                                forSaving[forSaving.length] = {
                                    'MaterialNumber': j[headers.indexOf("MaterialNumber")],
                                    'PlantID': GetPlantID(j[headers.indexOf("PlantCode")]),
                                    'ForDos': 1,
                                    'DateInput': headers[k],
                                    'Value': j[k]
                                };
                            }
                        }
                    }
                }
            }
        }
    }
    console.log(forSaving);
    //return false;
    $('.panel-loading').show();
    $.ajax({
        url: GlobalURL + "Form/PSIPartsSimulation.aspx/SaveData", type: "POST",
        data: JSON.stringify({ obj: forSaving }),
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            //console.log(e);
            $('.panel-loading').hide();
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
 * get plantid based on code
 */
function GetPlantID(PlantCode) {
    var data = $("#search-plant").data("data");
    for (var i in data) {
        if (data[i].PlantCode == PlantCode) {
            return data[i].PlantID;
        }
    }
}
/**
 * import excel file
 */
function ImportXLSX() {
    var f = $("#txtFile")[0].files[0];
    var workbook = new ExcelJS.Workbook();
    var fileReader = new FileReader();
    fileReader.onload = (e) => {
        const buffer = e.target.result;
        workbook.xlsx.load(buffer).then(async (wb) => {
            var obj = XLSXToJSON(wb.model.sheets[0].rows);
            DrawExcelTable(obj);
        }).catch((error) => {
            console.log("readFile fail", error);
        });
    };
    fileReader.readAsArrayBuffer(f);
}
//--------------
/**
 * get the datatable as reference
 * to save the current activity in temporary variable
 * before the final saving
 */
function GetDataTable() {
    var json = Sys.Serialization.JavaScriptSerializer.serialize({ "MaterialNumber": "-1" });
    $.ajax({
        url: GlobalURL + "Form/PSIPartsSimulation.aspx/GetSimulation", type: "POST",
        data: '',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            var data = JSON.parse(e.d);
            var d = GroupSimulation(data);
            $('.table_excel').data("table",d);
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
 * format the json by grouping based on material number
 * @param  {JSON}   data - data to simulate
 */
function GroupSimulation(data) {
    var Simulated = {};
    for (var i in data) {
        if (Simulated[data[i].MaterialNumber] === undefined) {
            Simulated[data[i].MaterialNumber] = {};
        }
        //console.log(data[i]);
        Simulated[data[i].MaterialNumber][data[i].DateInput] = data[i].Value;
    }
    return Simulated;
}
/**
 * get data based on material number
 * @param {String} MaterialNumber - the material number for filter
 */
function GetSimulation(MaterialNumber) {
    if (MaterialNumber != "") {
        var json = Sys.Serialization.JavaScriptSerializer.serialize({ "MaterialNumber": MaterialNumber ,Formulated:false});
        $.ajax({
            url: GlobalURL + "Form/PSIPartsSimulation.aspx/GetData", type: "POST",
            data: json,
            contentType: 'application/json;charset=utf-8',
            dataType: 'json',
            success: function (e) {
                var data = JSON.parse(e.d);
                if (data.length > 0) {
                    DrawTable(data[0]);
                } else {
                    var dt = $('.table_excel').data('table');
                    var dImport = $('.table_excel').data('import');
                    var cols = $('.table_excel').data('columns');
                    //search the current record
                    
                    var ItemArray = [];
                    for (var i in dImport) {
                        var currentRow = dImport[i];
                        if (currentRow["MaterialNumber"] == MaterialNumber) {
                            for (var j in currentRow) {
                                ItemArray[ItemArray.length] = currentRow[j];
                            }
                            break;
                        }
                    }
                    var newData = {Table:dImport,ItemArray:ItemArray};
                    DrawTable(newData);
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
}
/**
 * to append the data into table
 * @param {JSON} data - the data that will show on table
 */
function DrawTable1(data) {

    var dt = data.Table;
    var dr = data.ItemArray;
    if (dt === undefined) {
        return;
    }
    //console.log(dt,dr);
    var cols = [];
    var table = $('.table_excel');
    var MaterialNumber = $('input[list="search-material"]').val();
    var tableSimulation = table.data("table");
    var currentData = tableSimulation[MaterialNumber];
    table.html("");
    var thead = $('<thead/>').appendTo(table);
    var tbody = $('<tbody/>').appendTo(table);
    var tr_head = $('<tr/>').appendTo(thead);
    if (dt.length > 0) {
        //console.log(dt);
        for (var i in dt[0]) {
            var dataField = i;
            cols[cols.length] = i;
            if (IsDate(i.replace(/_/g,'/').substr(1))) {
                i = i.substr(1).replace(/_/g,'/');
            }
            $('<th/>', {
                text:i,
                title: i,
                "data-field":dataField,
                tabindex:0
            }).appendTo(tr_head);
        }
        var pl = ["EPPI PLAN","SUPPLIER PLAN/DELIVERY","END STOCKS"];
        for (var a in pl) {
            var tr = $('<tr/>').appendTo(tbody);
            for (var i in dr) {
                if (cols[i] == "PlanLogical") {
                    dr[i] = pl[a];
                }
                cols[i] = cols[i] === undefined ?"":cols[i];
                if (a == 1 && (cols[i] == "Past" || IsDate(cols[i].replace(/_/g, '/').substr(1)))) {
                    var ClassName = "AutoComputePastDue";
                    if (IsDate(i.replace(/_/g, '/').substr(1))) {
                        ClassName = "AutoComputeField";
                    }
                    var td = $('<td/>').appendTo(tr);
                    var val = 0;
                    if (currentData == null) {
                        currentData = {};
                        currentData[cols[i]] = 0;
                    }
                    if (currentData != null) {
                        val = currentData[cols[i]];
                        val = val == null ? 0 : val;
                    }
                    $('<input/>', {
                        "data-value": dr[i],
                        "data-field": cols[i],
                        type: 'text',
                        value: val,
                        class:ClassName
                    }).appendTo(td);
                } else {
                    $('<td/>', {
                        tabindex:0,
                        text:dr[i],
                        title: dr[i],
                        "data-value": dr[i],
                        "data-field":cols[i]
                    }).appendTo(tr);
                }
            }
        }
        InputAutoCompute();
        $('.AutoComputePastDue').trigger("change");
    }
}
/**
 * adding change event for the inputs inside the table
 */
function InputAutoCompute() {
    //AutoComputeField,AutoComputePastDue
    $(".AutoComputeField,.AutoComputePastDue").off("change").on("change", function (e) {
        var input = $(this);
        var tr = input.closest("tr");
        var td = input.closest("td");
        var table = tr.closest("table");
        var TotalSTCK = $('[data-field="TotalSTCK"]', tr);
        var colIndex = $('td', tr).index(td);
        var rowIndex = $('tr', table).index(tr);
        var nextRow = $('tr:eq(' + (rowIndex + 1) + ')', table);
        var prevRow = $('tr:eq(' + (rowIndex - 1) + ')', table);
        var nextCol = $('td:eq(' + (colIndex+1) + ')', tr);
        var targetCol = $('td:eq(' + colIndex + ')', nextRow);
        var thisVal = parseInt($(this).val());
        var value = "";
        if (input.hasClass("AutoComputePastDue")) {
            value = TotalSTCK.data("value") + thisVal - $('td:eq(' + colIndex + ')', prevRow).data("value");
        } else {
            value = $('td:eq(' + (colIndex - 1) + ')', nextRow).data("value") + thisVal - $('td:eq(' + colIndex + ')', prevRow).data("value");
        }
        targetCol.text(value);
        targetCol.data("value", value);
        $('input', nextCol).trigger("change");
    });
}
/**
 * get value of excel
 */
function GetJExcelValue() {
    var cells = [];
    var rows = [];
    $('#MyTable tbody tr').each(function () {
        var tr = $(this);
        cells = [];
        $('td', tr).each(function () {
            if (!$(this).hasClass('jexcel_row')) {
                cells.push($(this).text());
            }
        });
        rows.push(cells);
    });
    return rows;
}
/**
 * backup code for savin simulation
 */
function SaveSimulation1() {
    var table = $('.table_excel');
    var loading = $('.panel-loading');
    //var controlLength = $('input', table).length;
    //var materialLength = $('.ControlMaterialNumber', table).length;
    var Materials = [];
    if (confirm("Save Simulation?")) {
        loading.show();
        var currentData = table.data('table');
        for (var i in currentData) {
            var HeaderInputs = {};
            var Inputs = {};
            for (var j in currentData[i]) {
                Inputs[j] = currentData[i][j];
            }
            Materials[Materials.length] = {
                "MaterialNumber": i,
                "Values": Inputs
            }
        }
        var json = Sys.Serialization.JavaScriptSerializer.serialize({ "obj": Materials });
        $.ajax({
            url: "/Form/PSIPartsSimulation.aspx/SaveData", type: "POST",
            data: json,
            contentType: 'application/json;charset=utf-8',
            dataType: 'json',
            success: function (e) {
                //Parse the XML and extract the records.
                
                loading.hide();
            },
            failed: function (e) {
                console.log(e);
            },
            error: function (e) {
                console.log(e);
            }
        });
    }
}
/**
 * this function will use the ExcelJs.min.js
 * this will export the data into excel file
 */
function ExportToExcel1() {
    var json = Sys.Serialization.JavaScriptSerializer.serialize({ "MaterialNumber": "-1",Formulated:false });
    $.ajax({
        url: GlobalURL + "Form/PSIPartsSimulation.aspx/GetData", type: "POST",
        data: json,
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            var data = JSON.parse(e.d);
            var columns = [];
            for (var i in data[0]) {
                if (IsDate(i.replace(/_/g,'/').substr(1))) {
                    i = i.replace(/_/g, '/').substr(1);
                }
                columns[columns.length] = i;
            }
            CreateExcel(columns,data,$('.table_excel').data("table"));
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
 * to create the excel file
 * @param {string[]} columns - column list
 * @param {JSON} data - the data that will be shown in excel file
 * @param {JSON} simulation - the data that will be shown before computation
 */
function CreateExcel1(columns, data, simulation) {
    //file initialization
    var workbook = new ExcelJS.Workbook();
    workbook.creator = 'ISD';
    workbook.lastModifiedBy = 'ISD';
    workbook.created = new Date();
    workbook.company = "EPPI";
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
    var worksheet = workbook.addWorksheet('Parts Simulation');
    //index for row and column of ExcelJS starts with 1
    var rowIndex = 1;
    var row = worksheet.getRow(rowIndex);
    var row2, row3, colTotalStock, strFormula = "";
    //initialize the table header
    for (var i in columns) {
        row.getCell(parseInt(i) + 1).value = columns[i];
    }
    var ranges = selectRange(worksheet, 1, 1, 1, columns.length);
    ranges.forEach(function (range) {
        range.style = {
            font: { bold: true }
        };
        range.alignment = {vertical:'middlen',horizontal:'center'};
    });
    //loop the data
    rowIndex = 0;
    for (var i in data) {
        if (rowIndex == 0) {
            rowIndex = 2;
        } else {
            rowIndex+=3;
        }
        row = worksheet.getRow(rowIndex);
        row2 = worksheet.getRow(rowIndex+1);
        row3 = worksheet.getRow(rowIndex+2);
        var ctr = 0;
        var MaterialNumber = data[i]['MaterialNumber'];
        for (var j in data[i]) {
            row.getCell(ctr + 1).value = data[i][j];
            if (j != "Past" && !IsDate(j.replace(/_/g,'/').substr(1))) {
                row2.getCell(ctr + 1).value = data[i][j];
                row3.getCell(ctr + 1).value = data[i][j];
            }
            if (j == "PlanLogical") {
                row.getCell(ctr + 1).value = "EPPI PLAN";
                row2.getCell(ctr + 1).value = "SUPPLIER PLAN/DELIVERY";
                row3.getCell(ctr + 1).value = "END STOCKS";
            }
            if (simulation.hasOwnProperty(MaterialNumber)) {
                if (simulation[MaterialNumber].hasOwnProperty(j)) {
                    row2.getCell(ctr + 1).value = simulation[MaterialNumber][j];
                }
            } else {
                if (j == "Past" || IsDate(j.replace(/_/g, '/').substr(1))) {
                    row2.getCell(ctr + 1).value = 0;
                }
            }
            if (j == "TotalSTCK") {
                colTotalStock = ctr;
            }
            if (j == "Past") {
                strFormula = String.format("{0}{1}+{2}{3}-{4}{5}",ExcelColumn(colTotalStock),rowIndex+1,ExcelColumn(ctr),rowIndex+1,ExcelColumn(ctr),rowIndex);
                row3.getCell(ctr + 1).value = { formula : strFormula };
            }
            if (IsDate(j.replace(/_/g, '/').substr(1))) {
                strFormula = String.format("{0}{1}+{2}{3}-{4}{5}", ExcelColumn(ctr-1), rowIndex + 2, ExcelColumn(ctr), rowIndex + 1, ExcelColumn(ctr), rowIndex);
                row3.getCell(ctr + 1).value = { formula : strFormula };
            }
            ctr++;
        }
    }
    //for downloading the file
    workbook.xlsx.writeBuffer().then(data => {
        const blob = new Blob([data], { type: this.blobType });
        saveAs(blob, "PartsSimulation.xlsx");
    });
}

/**
 * get the letter of column by number
 * @param {int} n - the index of column
 */
function ExcelColumn(n) {
    var ordA = 'A'.charCodeAt(0);
    var ordZ = 'Z'.charCodeAt(0);
    var len = ordZ - ordA + 1;

    var s = "";
    while (n >= 0) {
        s = String.fromCharCode(n % len + ordA) + s;
        n = Math.floor(n / len) - 1;
    }
    return s;
}
/**
 * This function will use the exceljs.min.js
 * this is the library which will be use
 * for reading the excel file
 * in this function it will convert the file
 * into a JSON object
 * then draw the data to table
 */
function ReadExcelFile(callback) {
    var selectedFile = $("#txtUploadFile")[0].files[0];
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
/**
 * reading the excel file
 */
function ReadExcelFile1() {
    var f = $("#txtFile")[0].files[0];
    var workbook = new ExcelJS.Workbook();
    var fileReader = new FileReader();
    fileReader.onload = (e) => {
        const buffer = e.target.result;
        workbook.xlsx.load(buffer).then(async (wb) => {
            var obj = XLSXToJSON(wb.model.sheets[0].rows);
            DrawXLSXTable(obj);
        }).catch((error) => {
            console.log("readFile fail", error);
        });
    };
    fileReader.readAsArrayBuffer(f);
}
/**
 * converting the data received from excel to json
 */
function XLSXToJSON(data) {
    var object = [];
    var canStart = false;
    for (var i in data) {
        var row = data[i];
        var _r = [];
        for (var j in row.cells) {
            var cell = row.cells[j];
            if (cell.value != null) {
                if (cell.value.toString().toLowerCase() == "model") {
                    canStart = true;
                }
                if (canStart) {
                    if (cell.value !== undefined) {
                        _r[j] = cell.value;
                    } else {
                        _r[j] = "";
                    }
                }
            } else {
                if (canStart) {
                    if (cell.result !== undefined) {
                        _r[j] = cell.result;
                    } else {
                        _r[j] = "";
                    }
                }
            }
        }
        if (_r.length > 0) {
            object[object.length] = _r;
        }
    }
    return object;
}
/**
 * drawing of excel table
 */
function DrawXLSXTable(data) {
    var dataTable = $('.table_excel').data('table');
    var dataRow = [];
    var shortageRow = {};
    var dataImportTable = [];
    var dataImportRow = [];
    var cols = [];
    var plan_logical_index = 0;
    var table = $('.table_excel');
    table.html('');
    var thead = $('<thead/>').appendTo(table);
    var tbody = $('<tbody/>').appendTo(table);
    var trhead = $('<tr/>').appendTo(thead);
    for (var i in data[0]) {
        var col = data[0][i];
        if (col == "TOTAL STCK") {
            col = "TotalSTCK";
        }
        if (col == "Material Number") {
            col = "MaterialNumber";
        }
        if (col == "Material Description") {
            col = "MaterialDescription";
        }
        if (col == "Supplier STCK") {
            col = "SupplierSTCK";
        }
        if (col == "PLAN/LOGICAL") {
            col = "PlanLogical";
        }
        if (col == "EPPI STCK") {
            col = "EPPISTCK";
        }
        if (col == "PAST DUE") {
            col = "Past";
        }
        if (IsDate(col)) {
            col = HumanDateFormat(col);
        }
        if (col.toLowerCase() == "planlogical") {
            plan_logical_index = i;
        }
        cols[cols.length] = col;
        $('<th/>', {
            tabindex: 0,
            title:col,
            text: col,
            "data-field":col
        }).appendTo(trhead);
    }
    for (var i = 1; i < data.length; i++) {
        shortageRow = {};
        dataRow = {};
        dataImportRow = {};
        var MaterialNumber = "";
        var row = data[i];
        var tr = $('<tr/>').appendTo(tbody);
        for (var j = 0; j < row.length; j++) {
            var td = $('<td/>', {
                tabindex: 0,
                title: row[j],
                text: row[j],
                "data-field": cols[j],
                "data-value":row[j]
            });
            if (cols[j] == "MaterialNumber") {
                MaterialNumber = row[j];
            }
            if (row[plan_logical_index].toLowerCase() == "supplier plan/delivery") {
                var val = row[j];
                if (cols[j].toLowerCase() == "past" || IsDate(cols[j])) {
                    var className = 'AutoComputePast';
                    if (IsDate(cols[j])) {
                        className = 'AutoComputeField';
                    }
                    val = val == "" ? 0 : row[j];
                    td = $('<td/>');
                    $('<input/>', {
                        value: val,
                        class: className,
                        "data-field":cols[j]
                    }).appendTo(td);
                    if (cols[j].toLowerCase() == "past") {
                        dataRow["Past"] = val;
                    } else {
                        dataRow[UnderDateFormat(cols[j])] = val;
                    }
                }
            }
            if (row[plan_logical_index].toLowerCase() == "eppi plan") {
                //act as shortagelist row
                var _col = cols[j];
                if (IsDate(_col)) {
                    _col = UnderDateFormat(_col);
                }
                shortageRow[_col] = row[j];
            }
            td.appendTo(tr);
        }
        if (dataRow["Past"] !== undefined) {
            dataTable[MaterialNumber] = dataRow;
        }
        //console.log(!isEmptyObject(shortageRow));
        if (!isEmptyObject(shortageRow)) {
            dataImportTable[dataImportTable.length] = shortageRow;
        }
    }
    $('.table_excel').data('table',dataTable);
    $('.table_excel').data('import',dataImportTable);
    $('.table_excel').data('columns',cols);
    InputAutoCompute();
}
/**
 * to test if the json object is empty
 * @param {Object} obj - the object to test
 * @return {boolean}
 */
function isEmptyObject(obj) {
    return JSON.stringify(obj) === '{}';
}