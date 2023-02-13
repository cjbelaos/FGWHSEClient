/**
 Created By : Chris John Belaos
 Created Date: 12/06/2022
 */

var isForViewing = null;
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

function CheckIfForViewing(callback) {
    $.ajax({
        type: "POST",
        url: GlobalURL + "Form/PSI_SHORTAGELIST_BY_PLANT.aspx/CheckIfForViewing",
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
        url: GlobalURL + "Form/PSI_SHORTAGELIST_BY_PLANT.aspx/GetPlantsByUserId",
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
        url: GlobalURL + "Form/PSI_SHORTAGELIST_BY_PLANT.aspx/GetVendorsByUserId",
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
        url: GlobalURL + "Form/PSI_SHORTAGELIST_BY_PLANT.aspx/GetShortageListByPlant",
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

function GetShortageListByPlantUpdatedDate(callback) {
    $.ajax({
        url: GlobalURL + "Form/PSI_SHORTAGELIST_BY_PLANT.aspx/GetShortageListByPlantUpdatedDate",
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
                        }, readOnly: true
                    };
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

        if ($('#MyTable').hasClass('handsontable htColumnHeaders')) {
            MyTable.updateSettings({
                fixedColumnsLeft: false
            })
        }

        var config = {
            data: d,
            colHeaders: columns,
            columns: colType,
            hiddenColumns: hiddenColumns,
            csvHeaders: true,
            tableOverflow: true,
            fixedColumnsLeft: 4,
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


$(function () {
    GetSessions(function (e) {
        userid = e["UserID"];
        userName = e["UserName"];

        CheckIfForViewing(function (e) {
            isForViewing = e;

            if (userid === 'GUEST' || isForViewing === true) {
                $('#divUpload').hide();
            }
        });
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
        var vendor = $.map($("#selectVendor option:selected"), function (e) {
            return $(e).val();
        });
        vendor = vendor.toString().replace(/,/g, "/");
        if (vendor === '') {
            vendor = AssignedVendor !== 'ALL' ? Vendors : AssignedVendor;
        }
        GetShortageListByPlant(plant, vendor, function (e) {
            if (e.length > 0) {
                DrawTable(e);
                GetShortageListByPlantUpdatedDate(function (e) {
                    var LastUpdatedDate = '(Last Update - ' + e + ')';
                    $('#lblUpdatedDate').text(LastUpdatedDate);
                })
            }
            else {
                alert('No data has been found!')
                DrawTable(e);
            }
        });
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
            UploadFile(userid, plant, formData, function () {
                var plant = $('#selectPlant').val();
                var vendor = $.map($("#selectVendor option:selected"), function (e) {
                    return $(e).val();
                });
                vendor = vendor.toString().replace(/,/g, "/");
                if (vendor === '') {
                    vendor = AssignedVendor !== 'ALL' ? Vendors : AssignedVendor;
                }
                GetShortageListByPlant(plant, vendor, function (e) {
                    if (e.length > 0) {
                        DrawTable(e);
                        GetShortageListByPlantUpdatedDate(function (e) {
                            var LastUpdatedDate = '(Last Update - ' + e + ')';
                            $('#lblUpdatedDate').text(LastUpdatedDate);
                        })
                    }
                    else {
                        alert('No data has been found!')
                        DrawTable(e);
                    }
                });
            });
        }
        else {
            alert('Please choose file to upload!');
        }
    });
});