/*
 * Script for Uploading of Shortage List
 * emon
 * 03/04/2021
 */
/**
 * List of Plants
 */
var MyTable = null;
var PlantData = null;
function GetPlants(callback) {
    $.ajax({
        url: GlobalURL + "Form/PSIShortageList.aspx/GetPlants", type: "POST",
        data: '',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            if (callback !== undefined) {
                e = JSON.parse(e.d);
                PlantData = e;
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
function GetVendor(callback) {
    $.ajax({
        url: GlobalURL + "Form/PSIShortageList.aspx/GetVendor", type: "POST",
        data: '',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            if (callback !== undefined) {
                e = JSON.parse(e.d);
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
function GetPlantID(PlantCode) {
    for (var i in PlantData) {
        if (PlantData[i]['PlantCode'] == PlantCode) {
            return PlantData[i]['PlantID'];
        }
    }
    return null;
}
/**
 * this will get the initial data from shortage list
 */
function GetData(PlantID,VendorCode, callback) {
    $.ajax({
        url: GlobalURL + "Form/PSIShortageList.aspx/GetData", type: "POST",
        data: JSON.stringify({ PlantID: PlantID, VendorCode: VendorCode }),
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
            $('.panel-loading').hide();
        },
        error: function (e) {
            var d = e.responseJSON;
            MessageBox(d.ExceptionType, '<p>' + d.Message + '</p><pre style="max-width: 100%;max-height: 300px;overflow:auto;">' + d.StackTrace + '</pre>');
        }
    });
}
/**
 * Draw the table for shortage list
 */
function DrawTable(iColumns,iTitles,data,forHide) {
    //console.log(iColumns, iTitles);
    //return false;
    forHide = forHide === undefined ? {}:forHide;
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
            if (IsDate(iTitles[j]) || colName == "PastDue") {
                colType[j] = {
                    type: 'numeric', numericFormat: {
                        pattern: '0,00',
                        culture: 'en-US'
                    },readOnly:true };
            }
        }
        d[i] = e;
    }
    console.log(data);
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
        hiddenColumns:forHide,
        csvHeaders: true,
        tableOverflow: true,
        height: '300px',
        width: $("#MyTable").css("width"),
        licenseKey: 'f8a35-a4d7b-607a2-f2225-39038'
    };
    var hooks = Handsontable.hooks.getRegistered();
    hooks.forEach(function (hook) {
        if (hook == 'afterChange') {
            config[hook] = function () {
                AfterChange(hook,arguments);
            }
        }
    });
    MyTable = Handsontable(document.getElementById("MyTable"), config);
    //$(excelTable.records).each(function (rowIndex,arr) {
    //    for (var i in arr) {
    //        if (i == 7 || i == 8 || i >= 10) {
    //            $(arr[i]).css('text-align','right');
    //        }
    //    }
    //});
}
function AfterChange(hook, data) {
    //console.log(hook,data);
}
function DrawTable1(iColumns,iTitles,data) {
    //console.log(iColumns, iTitles);
    //return false;
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
            if (IsDate(iTitles[j]) || colName == "PastDue") {
                colType[j] = { type: 'text', readOnly: true, mask: '#,###.00' };
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
        tableWidth: $("#MyTable").css("width")
    });
    $(excelTable.records).each(function (rowIndex,arr) {
        for (var i in arr) {
            if (i == 7 || i == 8 || i >= 10) {
                $(arr[i]).css('text-align','right');
            }
        }
    });
}
/**
 * This function will use the xlsx.full.min.js
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
 * 
 */
function UploadExcelFile(callback) {
    var fileUpload = $('#txtUploadFile').get(0);
    var files = fileUpload.files;
    var formData = new FormData();
    for (var i = 0; i < files.length; i++) {
        formData.append(files[i].name, files[i]);
    }
    $.ajax({
        url: GlobalURL + 'Classes/ExcelReader.ashx',
        type: 'POST',
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (file) {
            if (callback !== undefined) {
                callback(file);
            }
        },
        xhr: function () {
            var fileXhr = $.ajaxSettings.xhr();
            if (fileXhr.upload) {
                $("progress").show();
                fileXhr.upload.addEventListener("progress", function (e) {
                    if (e.lengthComputable) {
                        $("#fileProgress").attr({
                            value: e.loaded,
                            max: e.total
                        });
                    }
                }, false);
            }
            return fileXhr;
        }
    });
}
/**
 * Save Data to database
 */
function SaveData(callback) {
    var data = MyTable.getData();
    var iTitles = MyTable.getColHeader();
    var newData = DataMapping(iTitles, data);
    //console.log(newData);
    //return false;
    var DataForSaving = [];
    for (var i in newData) {
        if (newData[i]['Received/Issued'] == 'issue') {
            var PK = newData[i]['Material Code'] + '_' + newData[i]['Plant'];
            if (KeyIsExists(PK, DataForSaving)) {
                for (var j in DataForSaving) {
                    if (DataForSaving[j]['PK'] == PK) {
                        if (newData[i]['Past Due'] == null) {
                            newData[i]['Past Due'] = 0;
                        }
                        DataForSaving[j]['Past Due'] = parseFloat(DataForSaving[j]['Past Due']) + parseFloat(newData[i]['Past Due']);
                    }
                }
            } else {
                var dr = {};
                var det = {};
                dr['PK'] = PK;
                dr['PlantID'] = GetPlantID(newData[i]['Plant']);
                dr['MaterialNumber'] = newData[i]['Material Code'] === undefined ? null : newData[i]['Material Code'];
                dr['MaterialDescription'] = newData[i]['Material Description'] === undefined ? null : newData[i]['Material Description'];
                dr['MainVendor'] = newData[i]['MainVendor'] === undefined ? null : newData[i]['MainVendor'];
                dr['OwnStock'] = newData[i]['EPPI STCK'] === undefined ? null : newData[i]['EPPI STCK'];
                dr['Issued'] = newData[i]['Received/Issued'] === undefined ? null : newData[i]['Received/Issued'];
                dr['PastDue'] = newData[i]['Past Due'] === undefined ? null : newData[i]['Past Due'];
                dr['Stock'] = newData[i]['Supplier STCK'] === undefined ? null : newData[i]['Supplier STCK'];
                for (var j in iTitles) {
                    if (IsDate(iTitles[j])) {
                        det[UnderDateFormat(iTitles[j])] = newData[i][iTitles[j]];
                    }
                }
                dr['Values'] = det;
                DataForSaving[DataForSaving.length] = dr;
            }
        }
    }
    console.log(DataForSaving);
    //return false;
    var interval = window.setInterval(function (e) {
        GetMessage(function (m) {
            $("#currentStatus").text(m.d);
        });
    }, 1000);
    $.ajax({
        url: GlobalURL + "Form/PSIShortageList.aspx/SaveData", type: "POST",
        data: JSON.stringify({ data: DataForSaving }),
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e, a, x) {
            console.log(e, a, x);
            clearInterval(interval);
            $("#currentStatus").text("Done...");
            if (callback !== undefined) {
                callback(e);
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
function GetMessage(callback) {
    $.ajax({
        url: GlobalURL + "Classes/StaticInfo.asmx/GetMessage", type: "POST",
        data: '',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e, a, x) {
            console.log(e, a, x);
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
function StartTheProcess(callback) {
    $.ajax({
        url: GlobalURL + "Classes/SharedService.asmx/StartTheProcess", type: "POST",
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
function StopTheProcess(callback) {
    $.ajax({
        url: GlobalURL + "Classes/SharedService.asmx/StopTheProcess", type: "POST",
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
 * Check if key already exists from the object
 */
function KeyIsExists(key, obj) {
    for (var i in obj) {
        if (obj[i]['PK'] == key) {
            return true;
        }
    }
    return false;
}
$(document).ready(function () {
    GetVendor(function (e) {
        $('#search-vendor').html('');
        $('<option/>').appendTo('#search-vendor');
        for (var i in e) {
            $('<option/>', {
                value:e[i]['SUPPLIERCODE'],
                text: e[i]['SUPPLIERCODE']+' | '+e[i]['SUPPLIERNAME']
            }).appendTo('#search-vendor');
        }
        GetPlants(function (data) {
            $('#txtPlant').html('');
            for (var i in data) {
                $('<option/>', {
                    value: data[i]['PlantID'],
                    text: data[i]['PlantCode'] + " | " + data[i]['PlantName']
                }).appendTo("#txtPlant");
            }
            $("#txtPlant,#search-vendor").off("change").on("change", function () {
                $('.panel-loading').show();
                var PlantID = $('#txtPlant').val();
                var VendorCode = $('#search-vendor').val();
                $('.panel-loading').show();
                GetData(PlantID,VendorCode, function (e) {
                    var iColumns = ['ShortageListID', 'PlantID', 'PlantCode', 'MaterialNumber',
                        'MaterialDescription', 'MainVendor', 'SUPPLIERNAME', 'OwnStock', 'Stock','TotalSTCK', 'Issued', 'PastDue'];
                    var iTitles = ['ShortageListID', 'PlantID', 'Plant', 'Material Code',
                        'Material Description', 'MainVendor', 'Supplier', 'EPPI STCK', 'Supplier STCK','Total Stock', 'Received/Issued', 'Past Due'];
                    var hiddenColumns = {
                        columns: [0, 1, 5, 9],
                        indicators:false
                    };
                    if (e.length == 0) {
                        alert("No Record Found");
                    } else {
                        DrawTable(iColumns, iTitles, e, hiddenColumns);
                    }
                    $('.panel-loading').hide();
                });
            });
            $("#txtPlant,#search-vendor").chosen({ allow_single_deselect: true });
            //$("#txtPlant").trigger("change");
            $('.panel-loading').hide();
        });
    });
    $("#txtUploadFile").on("change", function () {
        ReadExcelFile(function (e) {
            //console.log(e);
            var iColumns = [null,null,'Plant', 'Material Number',
                'Material Description', 'Main Vendor', 'Vendor Name', 'Own stock', 'Stock', null, 'Rece./issue', 'Past'];
            var iTitles = ['ShortageListID', 'PlantID', 'Plant', 'Material Code',
                'Material Description', 'MainVendor', 'Supplier', 'EPPI STCK', 'Supplier STCK', 'Total Stock', 'Received/Issued', 'Past Due'];
            var hiddenColumns = {
                columns: [0, 1, 5, 9],
                indicators: false
            };
            DrawTable(iColumns,iTitles,e,hiddenColumns);
            //console.log(e);
        });
    });
    $("#btnUploadFile").on("click", function () {
        if (confirm("Are you sure to replace the existing list? This can't be undone.")) {
            $('.panel-loading').show();
            UploadExcelFile(function (file) {
                $("#fileProgress").hide();
                $("#lblMessage").html("<b>" + file.name + "</b> has been uploaded.");
                SaveData(function (e) {
                    console.log(e);
                    location.reload();
                });
            });
        }
    });
});