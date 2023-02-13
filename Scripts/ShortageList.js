/*
 * Script for Uploading of Shortage List
 * emon
 * 01/28/2021
 */
/**
 * List of Plants
 */

var MyTable = null;
/**
 * get list of supplier
 */
function GetVendor(callback) {
    $.ajax({
        url: GlobalURL + "Form/PSIShortageList.aspx/GetVendor", type: "POST",
        data: '',
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
            console.log(e);
        }
    });
}
/**
 * get list of plants
 */
function GetPlants(callback) {
    $.ajax({
        url: GlobalURL + "Form/PSIShortageList.aspx/GetPlants", type: "POST",
        data: '',
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
            console.log(e);
        }
    });
}
/**
 * Append the Plants to Select Control
 */
function DrawPlants(data) {
    for (var i in data) {
        $('<option/>', {
            value: data[i]['PlantID'],
            text: data[i]['PlantCode'] + " | " + data[i]['PlantName']
        }).appendTo("#txtPlant");
    }
    $("#txtPlant").off("change").on("change", function () {
        var PlantID = $(this).val();
        GetData(PlantID);
    });
    $("#txtPlant").trigger("change");
}
/**
 * this will get the initial data from shortage list
 */
function GetData(PlantID, VendorCode, callback) {
    $.ajax({
        url: GlobalURL + "Form/PSIShortageList.aspx/GetData", type: "POST",
        data: JSON.stringify({ PlantID: PlantID, VendorCode: VendorCode }),
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
            var d = e.responseJSON;
            MessageBox(d.ExceptionType, '<p>' + d.Message + '</p><pre style="max-width: 100%;max-height: 300px;overflow:auto;">' + d.StackTrace + '</pre>');
        }
    });
}
/**
 * sending email to vendor after uploading of shortagelist
 */
function SendEmailToVendor(callback) {
    $.ajax({
        url: GlobalURL + "Form/PSIShortageList.aspx/SendEmailToVendor", type: "POST",
        data: '',
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

/**
 * Upload the file in the server
 * then read the file
 * and save to database
 */
function UploadExcelFile(callback) {
    $('.panel-loading').show();
    var fileUpload = $('#txtUploadFile').get(0);
    var files = fileUpload.files;
    var formData = new FormData();
    for (var i = 0; i < files.length; i++) {
        formData.append(files[i].name, files[i]);
    }
    var interval = window.setInterval(function (e) {
        GetMessage(function (m) {
            $("#currentStatus").text(m.d);
        });
    }, 1000);
    $.ajax({
        url: GlobalURL + 'Classes/ExcelReader.ashx',
        type: 'POST',
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (file) {
            $.ajax({
                url: GlobalURL + "Form/PSIShortageList.aspx/SaveExcelToDatabase", type: "POST",
                data: JSON.stringify({ FileName: file.name }),
                contentType: 'application/json;charset=utf-8',
                dataType: 'json',
                success: function (e) {
                    $("#fileProgress").hide();
                    $("#lblMessage").html("<b>" + file.name + "</b> has been uploaded.");
                    clearInterval(interval);
                    $('.panel-loading').hide();
                    if (callback !== undefined) {
                        callback(e);
                    }
                },
                failed: function (e) {
                    clearInterval(interval);
                    $('.panel-loading').hide();
                    var d = e.responseJSON;
                    MessageBox(d.ExceptionType, '<p>' + d.Message + '</p><pre style="max-width: 100%;max-height: 300px;overflow:auto;">' + d.StackTrace + '</pre>');
                },
                error: function (e) {
                    clearInterval(interval);
                    $('.panel-loading').hide();
                    var d = e.responseJSON;
                    MessageBox(d.ExceptionType, '<p>' + d.Message + '</p><pre style="max-width: 100%;max-height: 300px;overflow:auto;">' + d.StackTrace + '</pre>');
                }
            });
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
        }, error: function (e) {
            clearInterval(interval);
            $('.panel-loading').hide();
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
 * this will allocate the data to the table
 * for viewing
 */
async function AllocateData(data) {
    var cols = [];
    var rows = [];
    for (var i in data) {
        var row = [];
        for (var j in data[i]) {
            if (i == 0) {
                //get the columns on the first index
                cols.push(j);
            }
            row.push(data[i][j]);
        }
        rows.push(row);
    }
    $(".table_excel").html("");
    var thead = $('<thead/>').appendTo(".table_excel");
    var tbody = $('<tbody/>').appendTo(".table_excel");
    var trHead = $("<tr/>").appendTo(thead);
    for (var i in cols) {
        var colName = cols[i];
        if (IsDate(colName.replace(/_/g, '/').substr(1))) {
            colName = colName.replace(/_/g, '/').substr(1);
            colName = HumanDateFormat(colName);
        }
        $('<th/>', {
            title: colName,
            text: colName,
            tabindex: 0
        }).appendTo(trHead);
    }
    var ctr = 0;
    for (var i in rows) {
        var row = rows[i];
        var tr = $('<tr/>').appendTo(tbody);
        for (var j in row) {
            $('<td/>', {
                text: row[j],
                title: row[j],
                tabindex: 0
            }).appendTo(tr);
        }
    }
    $('.panel-loading').hide();
}
/**
 * this will load all the data from the database
 * first get the rowcount
 * then get the data per row
 * this is the work around to avoid the maximum character for JSON
 */
function GetInitialData() {
    GetRowCount(function (e) {
        e = JSON.parse(e.d);
        var currentIndex = 0;
        var RowCount = e.RowCount;
        DrawTheColumn(function () {
            RecurseGetRow(currentIndex, RowCount);
        });
    });
}
/**
 * this will create the table header column
 * @param {void} callback - another method will be called when this method was done
 */
function DrawTheColumn(callback) {
    $.ajax({
        url: "/Form/PSIShortageList.aspx/GetColumns", type: "POST",
        data: '',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            var cols = JSON.parse(e.d);
            $(".table_excel").html("");
            var thead = $('<thead/>').appendTo(".table_excel");
            var tbody = $('<tbody/>').appendTo(".table_excel");
            var trHead = $("<tr/>").appendTo(thead);
            for (var i in cols) {
                $('<th/>', {
                    title: cols[i],
                    text: cols[i],
                    tabindex: 0
                }).appendTo(trHead);
            }
            callback();
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
 * 
 */
function DrawCurrentRow(row) {
    var tbody = $(".table_excel tbody");
    var tr = $('<tr/>').appendTo(tbody);
    for (var j in row) {
        $('<td/>', {
            text: row[j],
            title: row[j],
            tabindex: 0
        }).appendTo(tr);
    }
}
/**
 * recusive loop to get row
 */
function RecurseGetRow(index, max) {
    $.ajax({
        url: "/Form/PSIShortageList.aspx/GetCurrentData", type: "POST",
        data: '{"CurrentIndex":"' + index + '"}',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            e = JSON.parse(e.d);
            DrawCurrentRow(e);
            $("#fileProgress").show().attr({
                value: index,
                max: max
            });
            index++;
            if (index < max) {
                RecurseGetRow(index, max);
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
 * get number of rows
 */
function GetRowCount(callback) {
    $.ajax({
        url: "/Form/PSIShortageList.aspx/GetRowCount", type: "POST",
        data: '',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            callback(e);
        },
        failed: function (e) {
            console.log(e);
        },
        error: function (e) {
            console.log(e);
        }
    });
}
//function GetMessage() {
//    $.ajax({
//        url: "/Classes/StaticInfo.asmx/GetMessage", type: "POST",
//        data: '',
//        contentType: 'application/json;charset=utf-8',
//        dataType: 'json',
//        success: function (e) {
//            console.log(e);
//        },
//        failed: function (e) {
//            console.log(e);
//        },
//        error: function (e) {
//            console.log(e);
//        }
//    });
//}
/**
 * setting the message in session
 */
function SetMessage(msg, callback) {
    $.ajax({
        url: GlobalURL + "Classes/Handler/SharedInfo.asmx/SetMessage", type: "POST",
        data: JSON.stringify({ msg: msg }),
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
            console.log(e);
        }
    });
}
/**
 * drawing of excel table to screen
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
            if (IsDate(iTitles[j]) || iTitles[j] == "Past" || iTitles[j] == "Own stock" || iTitles[j] == "Stock") {
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
        allowInsertRow: true,
        licenseKey: 'f8a35-a4d7b-607a2-f2225-39038'
    };
    MyTable = Handsontable(document.getElementById("MyTable"), config);
    $('.panel-loading').hide();
}
/**
 * get current status based on action taken
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
 * get the date when the table was created
 */
function GetCreateDate(callback) {
    $.ajax({
        url: GlobalURL + "Form/PSIShortageList.aspx/GetCreateDate", type: "POST",
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
$(document).ready(function () {
    GetCreateDate(function (e) {
        e = e.d;
        $('#label-last-update').text('(Last Update - ' + e + ')');
    });
    $('.panel-loading').show();
    GetPlants(function (e) {
        e = JSON.parse(e.d);
        $('#txtPlant').html('');
        for (var i in e) {
            $('<option/>', {
                value: e[i]['PlantID'],
                text: e[i]['PlantCode'] + ' | ' + e[i]['PlantName']
            }).appendTo('#txtPlant');
        }
        GetVendor(function (e) {
            e = JSON.parse(e.d);
            $('#search-vendor').html();
            $('<option/>').appendTo('#search-vendor');
            for (var i in e) {
                $('<option/>', {
                    value: e[i]['SupplierCode'],
                    text: e[i]['SupplierCode'] + ' | ' + e[i]['SupplierName']
                }).appendTo('#search-vendor');
            }
            $('.panel-loading').hide();
            $('#txtPlant,#search-vendor').chosen({ allow_single_deselect: true });
            GetSession(function (e) {
                if (e['UserID'].toString() == 'GUEST') {
                    $('#txtUploadFile,#btnUploadFile').remove();
                }
            });
        });
    });
    $('#txtPlant,#search-vendor').off('change').on('change', function () {
        var PlantID = $('#txtPlant').val();
        var SupplierCode = $('#search-vendor').val();
        if (PlantID.length > 0 || SupplierCode.length > 0) {
            $('.panel-loading').show();
            GetData(PlantID, SupplierCode, function (e) {
                e = JSON.parse(e.d);
                $('.panel-loading').hide();
                console.log(e);
                var iColumns = ['Plant', 'MaterialNumber', 'MaterialDescription', 'SupplierID', 'SupplierName', 'OwnStock', 'Stock',
                    'ReceIssue', 'Past'];
                var iTitles = ['Plant', 'Material Number', 'Material Description', 'Main Vendor', 'Vendor Name', 'Own stock', 'Stock',
                    'Rece./issue', 'Past'];
                var hiddenColumns = {};
                DrawTable(iColumns, iTitles, e, hiddenColumns);
            });
        } else {
            alert("Choose atleast one option");
        }
    });
    $('#txtUploadFile').off('change').on('change', function () {
        //$('.panel-loading').show();
        //ReadExcelFile(function (e) {
        //    console.log(e);
        //    var iColumns = ['Plant', 'Material Number', 'Material Description', 'Main Vendor', 'Vendor Name', 'Own stock', 'Stock',
        //        'Rece./issue', 'Past'];
        //    var iTitles = ['Plant', 'Material Number', 'Material Description', 'Main Vendor', 'Vendor Name', 'Own stock', 'Stock',
        //        'Rece./issue', 'Past'];
        //    var hiddenColumns = {};
        //    DrawTable(iColumns, iTitles, e, hiddenColumns);
        //});
    });
    $('#btnUploadFile').off('click').on('click', function () {
        UploadExcelFile(function (e) {
            SendEmailToVendor(function (e) {
                alert('Uploaded');
                console.log(e);
                //document.location.reload();
            });
        });
    });
});