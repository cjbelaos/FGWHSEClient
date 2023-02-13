/**
 Created By : Chris John Belaos
 Created Date: 12/06/2022
 */

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
        url: GlobalURL + "Form/PSI_DOS_BUYER_CONFIRM_BY_PLANT.aspx/CheckIfForViewing",
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
        url: GlobalURL + "Form/PSI_DOS_BUYER_CONFIRM_BY_PLANT.aspx/GetPlantsByUserId",
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

function GetDosBuyerConfirmByPlant(plant, callback) {
    $.ajax({
        type: "POST",
        url: GlobalURL + "Form/PSI_DOS_BUYER_CONFIRM_BY_PLANT.aspx/GetDosBuyerConfirmByPlant",
        data: JSON.stringify({ strPlant: plant }),
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

function CreateStatus(SupplierCategory, SupplierID, SupplierName, Count, Status) {
    $('.ClearBoth').remove();
    var Achieved = '#4bc0c0';
    var NotAchieved = '#ff6384';
    var Percentage = (Status / Count) * 100;
    Percentage = Percentage.toFixed(2);
    var parent = $('#DOSContainer' + SupplierCategory);
    if (SupplierCategory === "All") {
        var div = $('<div>', {
            css: {
                margin: '10px 1%'
            }
        }).appendTo(parent);
    }
    else {
        var div = $('<div>', {
            css: {
                float: 'left',
                width: '80%',
                margin: '10px 1%'
            }
        }).appendTo(parent);
    }
    var table = $('<table/>', {
        class: 'table'/*, border: 1*/, css: {
            'font-size': '16px',
            'font-family': 'Poppins',
            'border': '1px solid black',
            'border-collapse': 'collapse'
        },
    }).appendTo(div);
    var thead = $('<thead/>').appendTo(table);
    var tbody = $('<tbody/>').appendTo(table);
    $('<th/>', {
        html: SupplierName,
        css: {
            'color': '#222',
            'text-align': 'center',
            'border': '1px solid black',
            'border-collapse': 'collapse'
        }
    }).appendTo(thead);
    $('<th/>', {
        html: 'STATUS',
        css: {
            'color': '#222',
            'text-align': 'center',
            'border': '1px solid black',
            'border-collapse': 'collapse'
        }
    }).appendTo(thead);
    $('<th/>', {
        html: '%',
        css: {
            'color': '#222',
            'text-align': 'center',
            'border': '1px solid black',
            'border-collapse': 'collapse'
        }
    }).appendTo(thead);
    $('<td>', {
        html: Count,
        css: {
            'color': '#444',
            'text-align': 'center',
            'background': '#ffcd56',
            'border': '1px solid black',
            'border-collapse': 'collapse'
        }
    }).appendTo(tbody);
    if (SupplierCategory === "Local" || SupplierCategory === "Import") {
        $('<td>', {
            html: Status,
            class: 'for-viewing',
            data: {
                SupplierCode: SupplierID
            },
            css: {
                'color': '#0d6efd',
                'cursor': 'pointer',
                'text-align': 'center',
                'background': Percentage == 100 ? Achieved : NotAchieved,
                'border': '1px solid black',
                'border-collapse': 'collapse'
            }
        }).appendTo(tbody);
    }
    else {
        $('<td>', {
            html: Status,
            css: {
                'color': '#444',
                'text-align': 'center',
                'background': Percentage == 100 ? Achieved : NotAchieved,
                'border': '1px solid black',
                'border-collapse': 'collapse'
            }
        }).appendTo(tbody);
    }
    
    $('<td>', {
        html: Percentage + '%',
        css: {
            'color': '#444',
            'text-align': 'center',
            'background': Percentage == 100 ? Achieved : NotAchieved,
            'border': '1px solid black',
            'border-collapse': 'collapse'
        }
    }).appendTo(tbody);
    $('<div/>', {
        class: 'ClearBoth',
        css: {
            clear: 'both'
        }
    }).appendTo(parent);
}


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
    });

    $('#btnFilter').click(function () {

        var plant = $('#selectPlant').val();
        GetDosBuyerConfirmByPlant(plant, function (e) {
            console.log(e);
            var All = e["Table"];
            var AllLocal = e["Table1"];
            var AllImport = e["Table2"]
            var Local = e["Table3"];
            var Import = e["Table4"]

            $("#DOSContainerAll").empty();
            $("#DOSContainerAllLocal").empty();
            $("#DOSContainerLocal").empty();
            $("#DOSContainerAllImport").empty();
            $("#DOSContainerImport").empty();

            for (var i in All) {
                var category = "All";
                var supplierCode = null;
                var supplier = "ALL SUPPLIERS";
                var total = All[i]["TOTAL"];
                var achieved = All[i]["ACHIEVED"];
                CreateStatus(category, supplierCode, supplier, total, achieved);
            }

            for (var i in AllLocal) {
                var category = "AllLocal";
                var supplierCode = null;
                var supplier = "ALL LOCAL SUPPLIERS";
                var total = AllLocal[i]["TOTAL"];
                var achieved = AllLocal[i]["ACHIEVED"];
                CreateStatus(category, supplierCode, supplier, total, achieved);
            }

            for (var i in AllImport) {
                var category = "AllImport";
                var supplierCode = null;
                var supplier = "ALL IMPORT SUPPLIERS";
                var total = AllImport[i]["TOTAL"];
                var achieved = AllImport[i]["ACHIEVED"];
                CreateStatus(category, supplierCode, supplier, total, achieved);
            }

            for (var i in Local) {
                var category = "Local";
                var supplierCode = Local[i]["SUPPLIERCODE"];
                var supplier = Local[i]["SUPPLIER"];
                var total = Local[i]["TOTAL"];
                var achieved = Local[i]["ACHIEVED"];
                CreateStatus(category, supplierCode, supplier, total, achieved);
            }

            for (var i in Import) {
                var category = "Import";
                var supplierCode = Import[i]["SUPPLIERCODE"];
                var supplier = Import[i]["SUPPLIER"];
                var total = Import[i]["TOTAL"];
                var achieved = Import[i]["ACHIEVED"];
                CreateStatus(category, supplierCode, supplier, total, achieved);
            }

            $("#lblUpdatedDate").text("As of (" + moment().format("MM/DD/YYYY hh:mm:ss A" + " )"));

            $('.for-viewing').on('click', function () {
                window.open("PSI_DOS_VENDOR_REPLY_BY_PLANT.aspx?SupplierCode=" + $(this).data("SupplierCode"));
            });
        });
    });

   
});



