/**
 Created By : emon
 Created Date: 02/26/2021 10:35 am
 */

var MyTable = null;
var NumberFormatString = '#,##0.00_);[Red](#,##0.00)';
var DTOrig = null;

var DTBusCategory = null;
var DTModel = null;
var DTProblemCategory = null;
var DTPlant = null;
var DTSupplier = null;

/**
 * Get data based on filter
 */
function GetData(PlantID, VendorCode, MaterialNumber, callback) {
    $.ajax({
        url: GlobalURL + "Form/PSIDosVendorReply.aspx/GetData",
        type: "POST",
        data: JSON.stringify({PlantID:PlantID,VendorCode:VendorCode,MaterialNumber:MaterialNumber}),
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            //console.log(JSON.parse(e.d));
            if (callback !== undefined) {
                callback(e);
            }
            console.log(e);
        },
        error: function (e) {
            var d = e.responseJSON;
            MessageBox(d.ExceptionType, '<p>' + d.Message + '</p><pre style="max-width: 100%;max-height: 300px;overflow:auto;">' + d.StackTrace + '</pre>');
        }
    });
}
/**
 * Draw the excel table on page
 */
function DrawTable(data) {
    //console.log(data);
    var iTitles = ['Plant','Category', 'MODEL', 'Material Number', 'Material Description'
        , 'Supplier', 'EPPI STCK', 'DOS(EPPI Stock Only)', 'DOS LEVEL', 'Supplier STCK'
        , 'DOS(Over - all stocks)', 'TOTAL STCK', 'PROBLEM CATEGORY', 'REASON', 'PIC', 'COUNTERMEASURE'];
    var iColumns = ['PlantCode', 'Category', 'Model', 'MaterialNumber', 'MaterialDescription',
        'SupplierName', 'EPPISTCK', 'EPPI_DOS', 'DOS_Level', 'Supplier_Stock', 'DOS_Stock', 'TotalSTCK', 'Prob_Cat_name', 'Reason', 'PIC', 'CounterMeasure'];
    for (var i in data[0]) {
        if (IsDate(i.replace(/_/g, "/").substr(1))) {
            iTitles[iTitles.length] = HumanDateFormat(i.replace(/_/g, "/").substr(1));
            iColumns[iColumns.length] = i;
        }
    }

    //var arrBusCategory = ['CAT1','CAT2','CAT3'];
    //var arrModel = ['MOD1','MOD2','MOD3'];
    //var arrProblemCategory = ['PRO1','PRO2','PRO3'];

    var arrBusCategory = [];
    var arrModel = [];
    var arrProblemCategory = [];

    for (var i in DTBusCategory) {
        arrBusCategory.push(DTBusCategory[i]['CAT_NAME']);
    }
    for (var i in DTModel) {
        arrModel.push(DTModel[i]['MODEL_NAME']);
    }
    for (var i in DTProblemCategory) {
        arrProblemCategory.push(DTProblemCategory[i]['PROB_CAT_NAME']);
    }

    let d = [data.length];
    let xColWidth = [iColumns.length];
    let colType = [iColumns.length];
    for (var i in data) {
        let e = [iColumns.length];
        for (var j in iColumns) {
            var colName = iColumns[j];
            xColWidth[j] = 100;
            e[j] = data[i][colName];
            colType[j] = { type: 'text', readOnly: true, wordWrap: false };
            if (['Reason','CounterMeasure'].includes(colName)) {
                colType[j] = { type: 'text', wordWrap: true };
            }
            //if (iTitles[j] == 'Category') {
            //    colType[j] = {
            //        type: 'dropdown',
            //        source: arrBusCategory
            //    };
            //}
            //if (iTitles[j] == 'MODEL') {
            //    colType[j] = {
            //        type: 'dropdown',
            //        source: arrModel
            //    };
            //}
            if (iTitles[j] == 'PROBLEM CATEGORY') {
                colType[j] = {
                    type: 'dropdown',
                    source: arrProblemCategory
                };
            }
            if (j >= 6 && j <= 11) {
                colType[j] = {
                    type: 'numeric', numericFormat: {
                        pattern: '0,00',
                        culture: 'en-US'
                    }, readOnly: true };
            }
            if (j >= 16) {
                colType[j] = {
                    type: 'numeric', numericFormat: {
                        pattern: '0,00',
                        culture: 'en-US'
                    }, readOnly: true };
            }
        }
        d[i] = e;
    }
    //console.log(d);
    MyTable = new Handsontable(document.getElementById("MyTable"), {
        data: d,
        colHeaders: iTitles,
        columns: colType,
        csvHeaders: true,
        tableOverflow: true,
        height: '300px',
        width: $("#MyTable").css("width"),
        colWidths: 100,
        afterChange: AfterChange,
        dropdownMenu: true,
        filters:true,
        licenseKey: 'f8a35-a4d7b-607a2-f2225-39038'
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
            var PCIndex = headers.indexOf('PROBLEM CATEGORY');
            if (col == PCIndex) {
                var ProblemCategory = MyTable.getDataAtCell(row, PCIndex);
                newVal = GetPIC(ProblemCategory);
                MyTable.setDataAtCell(row, col+2, newVal);
            }
        }
    }
}
/**
 * Backup code for drawing of table
 */
function DrawTable1(data) {
    var iTitles = ['Category','MODEL','Material Number','Material Description'
        , 'Supplier', 'EPPI STCK','DOS(EPPI Stock Only)','DOS LEVEL','Supplier STCK'
        ,'DOS(Over - all stocks)','TOTAL STCK','PROBLEM CATEGORY','REASON','PIC','COUNTERMEASURE'];
    var iColumns = [null, null, 'MaterialNumber', 'MaterialDescription',
        'MainVendor', 'OwnStock', 'EPPI_DOS', 'DOS_Level', 'Supplier_Stock', 'DOS_Stock', 'TotalSTCK','Prob_Cat_Name',null,null,null];
    for (var i in data[0]) {
        if (IsDate(i.replace(/_/g, "/").substr(1))) {
            iTitles[iTitles.length] = HumanDateFormat(i.replace(/_/g, "/").substr(1));
            iColumns[iColumns.length] = i;
        }
    }

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
            if (colName == null) {
                colType[j] = { type: 'text',wordWrap:true };
            }
            if (j == 5 || j == 8 || j == 10) {
                colType[j] = { type: 'numeric', mask: '[-]#,###.00', decimal: '.',readOnly:true};
            }
            if (j == 6 || j == 7 || j == 9) {
                colType[j] = { type: 'numeric'};
            }
            if (j >= 15) {
                colType[j] = { type: 'numeric', mask: '[-]#,###.00', decimal: '.',readOnly:true};
            }
        }
        d[i] = e;
    }

    if ($("#MyTable").hasClass("jexcel_container")) {
        $("#MyTable").jexcel('destroy');
        $("#MyTable").removeClass('jexcel_container');
    }
    var excelTable = jexcel(document.getElementById("MyTable"), {
        data: d,
        colHeaders: iTitles,
        colWidths: xColWidth,
        columns: colType,
        csvHeaders: true,
        tableOverflow: true,
        tableHeight: '300px',
        tableWidth: $("#MyTable").css("width"),
        updateTable: function (instance, cell, col, row, val, label, cellName) {
            if (col == 6 || col == 7 || col == 9) {
                val = val == '' ?'0':val;
                cell.innerHTML = Number.parseFloat(val).toFixed(2);
            }
        }
    });
    $(excelTable.records).each(function (rowIndex, arr) {
        for (var i in arr) {
            if ((i >= 5 && i <= 10) || i >= 15) {
                $(arr[i]).css('text-align', 'right');
                var label = $(arr[i]).text();
                var value = parseFloat(label.replace(/,/g, ''));
                if (value < 0) {
                    $(arr[i]).css({
                        background: '#F00',
                        color: '#FFF'
                    }).text('(' + label.replace('-', '') + ')');
                }
            }
        }
    });
    //$(excelTable.records).each(function (e) {
    //    var td = $(this);
    //    if (e == 0 || e == 2) {
    //        $(td).each(function (e) {
    //            $(this).addClass("readonly");
    //        });
    //    }
    //});
}
/**
 * Get list of plants
 */
function GetPlants(callback) {
    $.ajax({
        url: GlobalURL + "Classes/SharedService.asmx/GetPlants",
        type: "POST",
        data: JSON.stringify({}),
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            DTPlant = JSON.parse(e.d);
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
 * get PlantID based on PlantCode
 */
function GetPlantID(PlantCode) {
    for (var i in DTPlant) {
        if (DTPlant[i]['PlantCode'] == PlantCode) {
            return DTPlant[i]['PlantID'];
        }
    }
}
/**
 * get list of supplier
 */
function GetVendors(callback){
    $.ajax({
        url: GlobalURL + "Classes/SharedService.asmx/GetVendors",
        type: "POST",
        data: JSON.stringify({}),
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            DTSupplier = JSON.parse(e.d);

            if (callback !== undefined) {
                callback(e);
            }
        },
        error: function (e) {
            console.log(e);
        }
    });
}
/**
 * get supplier code based on supplier name
 */
function GetSupplierCode(SupplierName) {
    for (var i in DTSupplier) {
        if (DTSupplier[i]['SUPPLIERNAME'] == SupplierName) {
            return DTSupplier[i]['SUPPLIERCODE'];
        }
    }
    return null;
}
/**
 * get list of materials
 */
function GetMaterial(callback) {
    $.ajax({
        url: GlobalURL + "Classes/SharedService.asmx/GetMaterial",
        type: "POST",
        data: JSON.stringify({}),
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            if (callback !== undefined) {
                callback(e);
            }
        },
        error: function (e) {
            console.log(e);
        }
    });
}
/**
 * get list of category
 */
function GetBusCategory(callback) {
    $.ajax({
        url: GlobalURL + "Classes/SharedService.asmx/GetBusCategory",
        type: "POST",
        data: JSON.stringify({}),
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
 * get BusCategoryCode based on category name
 */
function GetBusCategoryCode(Cat_name) {
    for (var i in DTBusCategory) {
        if (DTBusCategory[i]['CAT_NAME'] == Cat_name) {
            return DTBusCategory[i]['CATEGORY'];
        }
    }
    return null;
}
/**
 * get list of models
 */
function GetModel(callback) {
    $.ajax({
        url: GlobalURL + "Classes/SharedService.asmx/GetModel",
        type: "POST",
        data: JSON.stringify({}),
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
 * get list of problem category
 */
function GetProblemCategory(callback) {
    $.ajax({
        url: GlobalURL + "Classes/SharedService.asmx/GetProblemCategory",
        type: "POST",
        data: JSON.stringify({}),
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
/**
 * exporting to excel based on filter
 */
function ExportToExcel(PlantID, VendorCode,MaterialNumber, callback) {
    var columns = [
        { field: 'PlantCode', title: 'Plant' },
        { field: 'Category', title: 'Category' },
        { field: 'Model', title: 'MODEL' },
        { field: 'MaterialNumber', title: 'Material Number' },
        { field: 'MaterialDescription', title: 'Material Description' },
        { field: 'SupplierID', title: 'Supplier' },
        { field: 'SupplierName', title: 'SupplierName' },
        { field: 'EPPISTCK', title: 'EPPI STCK' },
        { field: 'EPPI_DOS', title: 'DOS(EPPI Stock Only)' },
        { field: 'DOS_Level', title: 'DOS LEVEL' },
        { field: 'SupplierSTCK', title: 'Supplier STCK' },
        { field: 'DOS_Stock', title: 'DOS(Over - all stocks)' },
        { field: 'TotalSTCK', title: 'TOTAL STCK' },
        { field: 'Prob_Cat_name', title: 'PROBLEM CATEGORY' },
        { field: 'Reason', title: 'REASON' },
        { field: 'PIC', title: 'PIC' },
        { field: 'CounterMeasure', title: 'COUNTERMEASURE' }
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
    GetData(PlantID, VendorCode, MaterialNumber, function (e) {
        var data = JSON.parse(e.d);
        if (data.length > 0) {
            CreateExcelFile(columns, data, 'DOS Report.xlsx', callback);
        } else {
            alert("No Records Found");
        }
    });
}
/**
 * creating the excel file based on data sent
 */
function CreateExcelFile(columns, data, filename, callback) {
    //var arrBusCategory = ['CAT1', 'CAT2', 'CAT3'];
    //var arrModel = ['MOD1', 'MOD2', 'MOD3'];
    //var arrProblemCategory = ['PRO1', 'PRO2', 'PRO3'];

    var arrBusCategory = [];
    var arrModel = [];
    var arrProblemCategory = [];

    //for (var i in DTBusCategory) {
    //    arrBusCategory.push(DTBusCategory[i]['CAT_NAME']);
    //}
    //for (var i in DTModel) {
    //    arrModel.push(DTModel[i]['MODEL_NAME']);
    //}
    for (var i in DTProblemCategory) {
        arrProblemCategory.push(DTProblemCategory[i]['PROB_CAT_NAME']);
    }
    
    console.log(data);
    //return false;
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
        console.log(row);
        console.log(columns);
        console.log(data['0']);
        //for (var i in data['0']) {
        //    console.log(i);
        //}
        var ctr = 0;
        for (var j in columns) {
            var NumericColumns = ['EPPI STCK', 'DOS(EPPI Stock Only)', 'DOS LEVEL', 'Supplier STCK', 'DOS(Over - all stocks)','TOTAL STCK'];
            var Title = columns[j].title;
            var val = data[i][columns[j].field];
            lastCell = row.getCell(parseInt(j) + 1);
            if (NumericColumns.includes(Title) || IsDate(Title)) {
                lastCell.numFmt = NumberFormatString;
            }
            lastCell.value = val;
        }
    }

    //data.forEach((element, index) => {
    //    //worksheet.getCell('A' + (+index + 2)).dataValidation = {
    //    //    type: 'list',
    //    //    allowBlank: true,
    //    //    formulae: ['"'+arrBusCategory.join()+'"']
    //    //};
    //    //worksheet.getCell('B' + (+index + 2)).dataValidation = {
    //    //    type: 'list',
    //    //    allowBlank: true,
    //    //    formulae: ['"' + arrModel.join() + '"']
    //    //};
    //    worksheet.getCell('M' + (+index + 2)).dataValidation = {
    //        type: 'list',
    //        allowBlank: true,
    //        formulae: ['"' + arrProblemCategory.join() + '"']
    //    };
    //})

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

    //workbook.xlsx.writeBuffer().then((data) => {
    //    const blob = new Blob([data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
    //    saveAs(blob, filename);
    //});
    //if (callback !== undefined) {
    //    callback();
    //}
}
/**
 * for saving
 */
function SaveData(callback) {
    $('.panel-loading').show();
    var Data = MyTable.getSourceData();
    var Header = MyTable.getColHeader();
    var newData = DataMapping(Header, Data);
    var DataForSaving = [];
    for (var i in newData) {
        var PlantID = GetPlantID(newData[i]['Plant']);
        var newRow = {
            PlantID:PlantID,
            Category: GetBusCategoryCode(newData[i]['Category']),
            Cat_Name: newData[i]['Category'] === undefined?null: newData[i]['Category'],
            ModelName: newData[i]['MODEL'] == undefined?null:newData[i]['MODEL'],
            MaterialCode: newData[i]['Material Number'],
            Description: newData[i]['Material Description'],
            SupplierCode: GetSupplierCode(newData[i]['Supplier']),
            EPPIStock: newData[i]['EPPI STCK'],
            EPPI_DOS: newData[i]['DOS(EPPI Stock Only)'],
            DOS_Level: newData[i]['DOS LEVEL'],
            Supplier_Stock: newData[i]['Supplier STCK'],
            DOS_Stock: newData[i]['DOS(Over - all stocks)'],
            Total_Stock: newData[i]['TOTAL STCK'],
            Problem_Cat: newData[i]['PROBLEM CATEGORY'],
            Reason: newData[i]['REASON'] === undefined?null: newData[i]['REASON'],
            PIC: newData[i]['PIC'] === undefined?null: newData[i]['PIC'],
            CounterMeasure: newData[i]['COUNTERMEASURE'] === undefined?null: newData[i]['COUNTERMEASURE']
            //Det: []
        };
        //for (var j in Header) {
        //    var colName = Header[j];
        //    if (IsDate(colName)) {
        //        var det = {
        //            PlantID: PlantID,
        //            MaterialCode: newData[i]['Material Number'],
        //            SupplierCode: newData[i]['Supplier'],
        //            DateInput: SQLDateFormat(new Date(colName)),
        //            Value: newData[i][colName]
        //        };
        //        newRow['Det'].push(det);
        //    }
        //}
        DataForSaving.push(newRow);
    }
    //for debug mode
    //console.log("Old Data",DTOrig);
    //console.log("New Data", DataForSaving);
    DataForSaving = CompareData(DTOrig, DataForSaving);
    var PICs = [];
    var Suppliers = [];
    var Supplier_PIC = [];
    for (var i in DataForSaving) {
        var sup = DataForSaving[i]['SupplierCode'];
        var s_p = DataForSaving[i]['SupplierCode'] + '-' + DataForSaving[i]['PIC'];
        var pic = DataForSaving[i]['PIC'];
        if (!PICs.includes(pic)) {
            PICs[PICs.length] = DataForSaving[i]['PIC'];
        }
        if (!Suppliers.includes(sup)) {
            Suppliers[Suppliers.length] = sup;
        }
        if (!Supplier_PIC.includes(s_p)) {
            Supplier_PIC[Supplier_PIC.length] = s_p;
        }
    }
    //console.log("DataForSaving : ",DataForSaving);
    //console.log("PIC : ",PICs.join('/'));
    //console.log("Supplier : ",Suppliers.join('/'));
    //console.log("Supplier_PIC : ",Supplier_PIC.join(';'));
    //console.log("Data for Saving",DataForSaving);
    //return false;
    //end for debug mode
    var interval = window.setInterval(function (e) {
        GetMessage(function (m) {
            $("#currentStatus").text(m.d);
        });
    }, 1000);
    $.ajax({
        url: GlobalURL + "Form/PSIDosVendorReply.aspx/SaveData",
        type: "POST",
        data: JSON.stringify({ DOS: DataForSaving }),
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            SendEmailToBuyer(Suppliers.join('/'), PICs.join("/"), function (e) {
                clearInterval(interval);
                $("#currentStatus").text('Done...');
                $('.panel-loading').hide();
                if (callback !== undefined) {
                    callback(e);
                }
            });
        },
        error: function (e) {
            clearInterval(interval);
            var d = e.responseJSON;
            MessageBox(d.ExceptionType, '<p>' + d.Message + '</p><pre style="max-width: 100%;max-height: 300px;overflow:auto;">' + d.StackTrace + '</pre>');
        }
    });
}
/**
 * to compare the old and new data
 */
function CompareData(oData, nData) {
    var data = [];
    for (var i in oData) {
        var oPlantID = oData[i]['PlantID'];
        var oSupplierCode = oData[i]['SupplierID'];
        var oMateialCode = oData[i]['MaterialNumber'];
        var oProblem_Cat = oData[i]['Prob_Cat_name'];
        var oReason = oData[i]['Reason'];
        var oPIC = oData[i]['PIC'];
        var oCounterMeasure = oData[i]['CounterMeasure'];
        for (var j in nData) {
            var nPlantID = nData[j]['PlantID'];
            var nSupplierCode = nData[j]['SupplierCode'];
            var nMateialCode = nData[j]['MaterialCode'];
            var nProblem_Cat = nData[j]['Problem_Cat'];
            var nReason = nData[j]['Reason'];
            var nPIC = nData[j]['PIC'];
            var nCounterMeasure = nData[i]['CounterMeasure'];
            if (oPlantID == nPlantID && oSupplierCode == nSupplierCode && oMateialCode == nMateialCode) {
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
/**
 * get current status based on action created
 */
function GetMessage(callback) {
    $.ajax({
        url: GlobalURL + "Classes/Handler/SharedInfo.asmx/GetMessage", type: "POST",
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
 * to change the negative value to red
 */
function negativeValueRenderer(instance, td, row, col, prop, value, cellProperties) {
    Handsontable.renderers.TextRenderer.apply(this, arguments);
    instance.setCellMeta(row, col, 'className', 'htRight htNumeric NegativeCell');
    value = value.toString();
    $(td).text('('+value.replace('-','')+')');
}
/**
 * send the email to buyer after saving the dos
 */
function SendEmailToBuyer(SupplierID,PIC,callback) {
    $.ajax({
        url: GlobalURL + "Form/PSIDosVendorReply.aspx/SendEmailToBuyer", type: "POST",
        data: JSON.stringify({SupplierID:SupplierID,PIC:PIC}),
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            if (callback !== undefined) {
                callback(e);
            }
        },
        failed: function (e) {
            console.log(e);
        },
        error: function (e) {
            alert("An Error Occured");
            console.log(e);
        }
    });
}
/*code test*/
function CodeTest() {
    let data = [];
    var list = [
        { CATEGORY: 'CAT1', CAT_NAME: 'CATEGORY1' },
        { CATEGORY: 'CAT2', CAT_NAME: 'CATEGORY2' },
        { CATEGORY: 'CAT3', CAT_NAME: 'CATEGORY3' },
    ];
    var header = ['CODE','NAME'];
    for (let i = 0; i < list.length; i++) {
        let arr = [list[i].CATEGORY, list[i].CAT_NAME];
        data.push(arr);
    }
    console.log(data);
    //Create workbook and worksheet
    let workbook = new ExcelJS.Workbook();
    let worksheet = workbook.addWorksheet('BUS CATEGORY');

    //Add Header Row
    let headerRow = worksheet.addRow(header);

    // Cell Style : Fill and Border
    headerRow.eachCell((cell, number) => {
        cell.fill = {
            type: 'pattern',
            pattern: 'solid',
            fgColor: { argb: 'FFFFFF00' },
            bgColor: { argb: 'FF0000FF' }
        }
        cell.border = { top: { style: 'thin' }, left: { style: 'thin' }, bottom: { style: 'thin' }, right: { style: 'thin' } }
    })
    worksheet.getColumn(3).width = 30;
    data.forEach(d => {
        let row = worksheet.addRow(d);
    }
    );
    list.forEach((element, index) => {
        worksheet.getCell('E' + (+index + 2)).dataValidation = {
            type: 'list',
            allowBlank: true,
            formulae: ['"Selected,Rejected,On-hold"']
        };
    })
    //Generate Excel File with given name
    workbook.xlsx.writeBuffer().then((data) => {
        let blob = new Blob([data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
        saveAs(blob, 'Category.xlsx');
    })

}