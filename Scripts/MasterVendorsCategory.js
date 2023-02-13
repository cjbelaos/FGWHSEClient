$('.chosen').chosen({ width: "300px" });

var userid;
var userName;
var DTOrig = [];
var arrVendors = [];

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

function GetSuppliers(vendor, callback) {
    $.ajax({
        type: "POST",
        url: GlobalURL + "Form/PSI_MASTER_VENDORS_CATEGORY.aspx/GetSuppliersCategory",
        data: JSON.stringify({ strVENDOR: vendor }),
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

function DrawTable(data) {
    for (var i in data) {
        var newRow = {
            SupplierCode: data[i]['SUPPLIER CODE'],
            SupplierCategory: data[i]['SUPPLIER CATEGORY'],
            User: userid
        };
        DTOrig.push(newRow);
    }
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
                xColWidth[j] = 300;
                e[j] = data[i][colName];
                colType[j] = { type: 'text', readOnly: true };
                if (columns[j] == 'SUPPLIER CATEGORY') {
                    colType[j] = {
                        type: 'dropdown',
                        source: ["LOCAL", "IMPORT"],
                        allowInvalid: false,
                    };
                }
            }
            d[i] = e;
        }

        if ($('#MyTable').hasClass('handsontable htColumnHeaders')) {
            MyTable.destroy();
            var config = {
                colHeaders: [],
                data: [],
                licenseKey: 'f8a35-a4d7b-607a2-f2225-39038'
            };
            MyTable = new Handsontable(document.getElementById("MyTable"), config);
        }

        var config = {
            data: d,
            colHeaders: columns,
            colWidths: xColWidth,
            columns: colType,
            className: 'custom-table',
            stretchH: 'all',
            csvHeaders: true,
            tableOverflow: true,
            dropdownMenu: true,
            filters: true,
            filteringCaseSensitive: true,
            height: '300px',
            width: $("#MyTable").css("width"),
            cells: function (row, col) {
                var cp = {}
                cp.className = 'myClass'
                return cp
            },
            licenseKey: 'f8a35-a4d7b-607a2-f2225-39038'
        };
        MyTable = Handsontable(document.getElementById("MyTable"), config);
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

function SaveSuppliersCategory() {
    var Data = MyTable.getSourceData();
    var Header = MyTable.getColHeader();
    var newData = DataMapping(Header, Data);
    var DataForSaving = [];

    for (var i in newData) {
        var newRow = {
            SupplierCode: newData[i]['SUPPLIER CODE'],
            SupplierCategory: newData[i]['SUPPLIER CATEGORY'],
            User: userid
        };
        DataForSaving.push(newRow);
    }
    newData = DataForSaving;
    ////for debug mode
    //console.log("Old Data", DTOrig);
    //console.log("New Data", DataForSaving);
    DataForSaving = CompareData(DTOrig, DataForSaving);
    console.log("DataForSaving : ", DataForSaving);
    if (DataForSaving.length > 0) {
        $.ajax({
            url: GlobalURL + "Form/PSI_MASTER_VENDORS_CATEGORY.aspx/SaveSuppliersCategory",
            type: "POST",
            data: JSON.stringify({ VC: DataForSaving }),
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

function CompareData(oData, nData) {
    var data = [];
    for (var i in oData) {
        var oSupplierCode = oData[i]['SupplierCode'];
        var oSupplierCategory = oData[i]['SupplierCategory'];
        var oUser = oData[i]['User'];
        for (var j in nData) {
            var nSupplierCode = nData[j]['SupplierCode'];
            var nSupplierCategory = nData[j]['SupplierCategory'];
            var nUser = nData[j]['User'];
            if (oSupplierCode == nSupplierCode && oUser == nUser) {
                if (oSupplierCategory != nSupplierCategory) {
                    data[data.length] = nData[j];
                    break;
                }
                break;
            }
        }
    }
    return data;
}

/*
 * Backup Code for DataTables
 */
function DrawDataTable(data) {
    columnNames = Object.keys(data[0]);
    columns = [];
    for (var i in columnNames) {
        columns.push({
            title: columnNames[i],
            data: columnNames[i]
        });
    }

    if (MyTable !== undefined && MyTable !== null) {
        MyTable.clear().destroy();
    }

    var MyTable = $('#MyTable').DataTable({
        paging: true,
        filtering: true,
        info: true,
        searching: true,
        "bLengthChange": true,
        "deferRender": true,
        data: data,
        columns: [
            {
                data: 'ID', title: 'EDIT', render: function () {
                    return '<button type="button" data-toggle="modal" data-target="#MyModal" class="btn btn-sm btn-primary btn-update-row" title="Update"><i class="fas fa-edit"></i></button>';
                }
            },
            { data: 'SUPPLIERID', title: 'SUPPLIER CODE' },
            { data: 'SUPPLIERNAME', title: 'SUPPLIER NAME' },
            { data: 'SUPPLIERCATEGORY', title: 'SUPPLIER CATEGORY' }
        ],
        columnDefs: [{ className: "dt-nowrap", targets: "_all" }],
    });
}

$(function () {
    GetSessions(function (e) {
        userid = e["UserID"];
        userName = e["UserName"];
    });

    var vendor = "";
    GetSuppliers(vendor, function (data) {
        if (data.length > 0) {
            data = data.map(function (obj) {
                obj['SUPPLIER CODE'] = obj['SUPPLIERID']; // Assign new key
                obj['SUPPLIER NAME'] = obj['SUPPLIERNAME']; // Assign new key
                obj['SUPPLIER CATEGORY'] = obj['SUPPLIERCATEGORY']; // Assign new key
                delete obj['SUPPLIERID']; // Delete old key
                delete obj['SUPPLIERNAME']; // Delete old key
                delete obj['SUPPLIERCATEGORY']; // Delete old key
                return obj;
            });

            for (var i in data) {
                arrVendors.push(data[i]['SUPPLIER CODE']); //insert all suppliercode into array
                $("<option>", {
                    value: data[i]["SUPPLIER CODE"],
                    text: data[i]["SUPPLIER CODE"] + " | " + data[i]["SUPPLIER NAME"]
                }).appendTo($("#selectVendor"));
            }
            $("#selectVendor").attr("data-placeholder", "Select Vendor Options").trigger("chosen:updated");
            DrawTable(data);
            $('#btnSave').css('display', 'inline-block');
        }
        else {
            alert('No Data Found');
            $('#btnSave').css('display', 'none');
        }
    });

    $("#btnFilter").click(function () {
        var vendor = $.map($("#selectVendor option:selected"), function (e) {
            return $(e).val();
        });
        vendor = vendor.toString().replace(/,/g, "/");
        if (vendor === "") {
            vendor = arrVendors.join("/");
        }
        GetSuppliers(vendor, function (data) {
            if (data.length > 0) {
                data = data.map(function (obj) {
                    obj['SUPPLIER CODE'] = obj['SUPPLIERID']; // Assign new key
                    obj['SUPPLIER NAME'] = obj['SUPPLIERNAME']; // Assign new key
                    obj['SUPPLIER CATEGORY'] = obj['SUPPLIERCATEGORY']; // Assign new key
                    delete obj['SUPPLIERID']; // Delete old key
                    delete obj['SUPPLIERNAME']; // Delete old key
                    delete obj['SUPPLIERCATEGORY']; // Delete old key
                    return obj;
                });
                DrawTable(data);
                $('#btnSave').css('display', 'inline-block');
            }
            else {
                alert('No Data Found');
                $('#btnSave').css('display', 'none');
            }
        });
    });

    $("#btnSave").click(function () {
        SaveSuppliersCategory();
    });
})