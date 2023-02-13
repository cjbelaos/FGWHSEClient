/*
 * Buyer Confirm
 * emon
 * 02/04/2021
 */

/**
 * get the list of material without dos based on supplier code
 */
function GetNoDOSMaterial(SupplierCode,callback) {
    $.ajax({
        url: GlobalURL + "Classes/SharedService.asmx/GetNoDOSMaterial",
        type: "POST",
        data: JSON.stringify({SupplierCode:SupplierCode}),
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
 * get the count of material without dos based on supplier code
 */
function GetNoDOSMaterialCount(SupplierCode,callback) {
    $.ajax({
        url: GlobalURL + "Classes/SharedService.asmx/GetNoDOSMaterialCount",
        type: "POST",
        data: JSON.stringify({SupplierCode:SupplierCode}),
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
 * get list of dos
 */
function GetDOS(callback) {
    $.ajax({
        url: GlobalURL + "Form/PSIDOSBuyerConfirm.aspx/GetDOS",
        type: "POST",
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (e) {
            console.log(e);
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
 * get list of plants
 */
function GetPlants(callback) {
    $.ajax({
        url: GlobalURL + "Classes/SharedService.asmx/GetPlants",
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
 * get list of suppliers
 */
function GetVendors(callback) {
    $.ajax({
        url: GlobalURL + "Classes/SharedService.asmx/GetVendors",
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
 * draw the status to screen
 */
function CreateStatus(SupplierID,SupplierName, Count, Status) {
    $('.ClearBoth').remove();
    var Achieved = '#3bff3b';
    var NotAchieved = 'red';
    var Percentage = (Status / Count) * 100;
    Percentage = Percentage.toFixed(2);
    var parent = $('#DOSContainer');
    var div = $('<div>', {
        css: {
            float: 'left',
            width: '30%',
            margin: '10px 1%'
        }
    }).appendTo(parent);
    var table = $('<table/>', {
        class: 'table', border: 1, css: {
            'font-size':'0.7em'
        }
    }).appendTo(div);
    var thead = $('<thead/>').appendTo(table);
    var tbody = $('<tbody/>').appendTo(table);
    $('<th/>', {
        html: SupplierName,
        css: {
            'text-align':'center'
        }
    }).appendTo(thead);
    $('<th/>', {
        html: 'STATUS',
        css: {
            'text-align':'center'
        }
    }).appendTo(thead);
    $('<th/>', {
        html: '%',
        css: {
            'text-align':'center'
        }
    }).appendTo(thead);
    $('<td>', {
        html: Count,
        css: {
            'text-align': 'center',
            'background':'yellow'
        }
    }).appendTo(tbody);
    $('<td>', {
        html: Status,
        class: 'for-viewing',
        data: {
            SupplierCode: SupplierID
        },
        css: {
            'cursor': 'pointer',
            'text-align': 'center',
            'background':Percentage == 100?Achieved:NotAchieved
        }
    }).appendTo(tbody);
    $('<td>', {
        html: Percentage + '%',
        css: {
            'text-align': 'center',
            'background':Percentage == 100?Achieved:NotAchieved
        }
    }).appendTo(tbody);
    $('<div/>', {
        class: 'ClearBoth',
        css: {
            clear:'both'
        }
    }).appendTo(parent);
}

$(document).ready(function () {
    $('.panel-loading').hide();
    $('#TableStatus').hide();
    //$('.icon-exporting').hide();
    GetDOS(function (e) {
        console.log(e);
        e = JSON.parse(e.d);
        var stat = [];
        var SLength = e.length;//shortagelist
        var LLength = 0;//live
        $('#DOSContainer').html('');
        for (var i in e) {
            CreateStatus(e[i]['SupplierID'], e[i]['SupplierName'], e[i]['SCount'], e[i]['LCount']);
            stat[stat.length] = {
                SupplierName:e[i]['SupplierName'],
                Count:e[i]['SCount'] - e[i]['LCount'],
                Percentage: ((e[i]['LCount'] / e[i]['SCount']) * 100).toFixed(2)
            };
            if (e[i]['SCount'] == e[i]['LCount']) {
                LLength++;
            }
        }
        $('#txtSupplierCount').on("click", function () {
            var table = $('#TableStatus table');
            var totalCount = 0;
            var totalPercentage = 0;
            $('tbody', table).html('');
            for (var i in stat) {
                var tr = $('<tr/>').appendTo($('tbody', table));
                $('<td/>', {
                    text:stat[i]['SupplierName']
                }).appendTo(tr);
                $('<td/>', {
                    text:stat[i]['Count']
                }).appendTo(tr);
                $('<td/>', {
                    text:stat[i]['Percentage']+'%'
                }).appendTo(tr);
                totalCount += stat[i]['Count'];
                totalPercentage += parseFloat(stat[i]['Percentage']);
            }
            $('tfoot', table).html('');
            var tfoottr = $('<tr/>').appendTo($('tfoot', table));
            $('<td>', {
                text:'TOTAL'
            }).appendTo(tfoottr);
            $('<td>', {
                text:totalCount
            }).appendTo(tfoottr);
            $('<td>', {
                text:(totalPercentage/stat.length).toFixed(2) + "%"
            }).appendTo(tfoottr);
            $('#TableStatus').show();
        });
        $('#TableStatus button').on('click', function () {
            $('#TableStatus').hide();
        });

        $('.for-viewing').on('click', function () {
            document.location = "PSIDOSVendorReply.aspx?SupplierCode=" + $(this).data("SupplierCode");
        });
        $('#txtSupplierCount').css('cursor','pointer');
        $('#txtSupplierCount').text(SLength);
        $('#txtSupplierStatus').text(LLength);
        var Percentage = (LLength / SLength)*100;
        $('#txtSupplierModulo').text(Percentage.toFixed(2) + '%');
        if (Percentage < 50) {
            $('#txtSupplierStatus').css('background','red');
        }
        $('#txtCurrentDate').text(HumanDateFormat(new Date()));
    });
});