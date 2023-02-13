/**
 Created By : Chris John Belaos
 Created Date: 12/06/2022
 */

var isForViewing = null;
var MyTable = null;
var Vendors = null;
var Parts = null;
var AssignedVendor = null;
var DTForExport = null;
var DTOrig = [];
var NumberFormatString = '#,##0.00_);[Red](#,##0.00)';
var isUploaded = false;
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
        url: GlobalURL + "Form/PartsSimulationV2.aspx/CheckIfForViewing",
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
        url: GlobalURL + "Form/PartsSimulationV2.aspx/GetPlantsByUserId",
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
        url: GlobalURL + "Form/PartsSimulationV2.aspx/GetVendorsByUserId",
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

function GetPartsCodeByPlantandVendors(plant, vendors, callback) {
    $.ajax({
        type: "POST",
        url: GlobalURL + "Form/PartsSimulationV2.aspx/GetPartsCodeByPlantandVendors",
        data: JSON.stringify({ strPLANT: plant, strVENDORS: vendors }),
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

function GetPartsSimulationByPlant(plant, parts, vendors, callback) {
    $.ajax({
        url: GlobalURL + "Form/PartsSimulationV2.aspx/GetPartsSimulationByPlant",
        type: "POST",
        data: JSON.stringify({ strPLANT: plant, strPARTS: parts, strVENDORS: vendors }),
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

function ShowButtons() {
    $('#btnExport').css('display', 'inline-block');
    $('#btnSave').css('display', 'inline-block');
}

function HideButtons() {
    $('#btnExport').css('display', 'none');
    $('#btnSave').css('display', 'none');
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
                var plant = $('#selectPlant').val();
                GetPartsCodeByPlantandVendors(plant, Vendors, function (d) {
                    if (d.length === 0) {
                        $("#selectPartsCode").attr("data-placeholder", "No PartsCode Options").trigger("chosen:updated");
                    }
                    else {
                        arrParts = [];
                        for (var i in d) {
                            arrParts.push(d[i]['MATERIALCODE']);
                            $('<option>', {
                                value: d[i]['MATERIALCODE'],
                                text: d[i]['MATERIAL']
                            }).appendTo($('#selectPartsCode'));
                        }
                        $("#selectPartsCode").attr("data-placeholder", "Select PartsCode Options").trigger("chosen:updated");
                        Parts = arrParts.join('/');
                    }
                });
            }
        });
    });

    $('#selectVendor').change(function () {
        $('#selectPartsCode').empty();
        var plant = $('#selectPlant').val();
        vendor = $(this).val();
        if (vendor === null) {
            Vendors = arrVendors.join('/');
        }
        else {
            vendors = vendor + ""
            vendors = vendors.replace(/,/g, "/");
        }
        GetPartsCodeByPlantandVendors(plant, vendors, function (d) {
            if (d.length === 0) {
                $("#selectPartsCode").attr("data-placeholder", "No PartsCode Options").trigger("chosen:updated");
            }
            else {
                for (var i in d) {
                    $('<option>', {
                        value: d[i]['MATERIALCODE'],
                        text: d[i]['MATERIAL']
                    }).appendTo($('#selectPartsCode'));
                }
                $("#selectPartsCode").attr("data-placeholder", "Select PartsCode Options").trigger("chosen:updated");
            }
        });
    });

    $('#btnFilter').click(function () {
        isUploaded = false;
        var plant = $('#selectPlant').val();
        var vendors = $.map($("#selectVendor option:selected"), function (e) {
            return $(e).val();
        });
        vendors = vendors.toString().replace(/,/g, "/");
        if (vendors === '') {
            vendors = AssignedVendor !== 'ALL' ? Vendors : AssignedVendor;
        }
        var parts = $.map($("#selectPartsCode option:selected"), function (e) {
            return $(e).val();
        });
        parts = parts.toString().replace(/,/g, "/");
        GetPartsSimulationByPlant(plant, parts, vendors, function (e) {
            data = e;
            columnNames = Object.keys(data[0]);
            FirstColumnName = columnNames[10];

            for (var i in data) {
                for (var j in columnNames) {
                    var Title = columnNames[j];
                    var Value = data[i][Title];

                    if (Title == 'PLAN/LOGICAL' && Value == 'SUPPLIER PLAN/DELIVERY') {
                        newRow = {};
                        for (var k = 10; k <= 207; k ++) {
                            newColumn = "DAY" + k.toString();
                            console.log(newColumn);
                            //newRow["DAY"]
                        }
                        
                    }
                }
            }
            //for (var i in d) {
            //    if (d[i]['PLAN/LOGICAL'] == 'SUPPLIER PLAN/DELIVERY') {
            //        var newRow = {
            //            Plant: d[i]['PLANT'],
            //            MaterialNumber: d[i]['MATERIALNUMBER'],
            //            MainVendor: d[i]['SUPPLIER'],
            //            FirstColumnName: FirstColumnName,
            //            Past: d[i]['PAST'],
            //            DAY1: d[i][columnNames['10']],
            //            DAY2: d[i][columnNames['11']],
            //            DAY3: d[i][columnNames['12']],
            //            DAY4: d[i][columnNames['13']],
            //            DAY5: d[i][columnNames['14']],
            //            DAY6: d[i][columnNames['15']],
            //            DAY7: d[i][columnNames['16']],
            //            DAY8: d[i][columnNames['17']],
            //            DAY9: d[i][columnNames['18']],
            //            DAY10: d[i][columnNames['19']],
            //            DAY11: d[i][columnNames['20']],
            //            DAY12: d[i][columnNames['21']],
            //            DAY13: d[i][columnNames['22']],
            //            DAY14: d[i][columnNames['23']],
            //            DAY15: d[i][columnNames['24']],
            //            DAY16: d[i][columnNames['25']],
            //            DAY17: d[i][columnNames['26']],
            //            DAY18: d[i][columnNames['27']],
            //            DAY19: d[i][columnNames['28']],
            //            DAY20: d[i][columnNames['29']],
            //            DAY21: d[i][columnNames['30']],
            //            DAY22: d[i][columnNames['31']],
            //            DAY23: d[i][columnNames['32']],
            //            DAY24: d[i][columnNames['33']],
            //            DAY25: d[i][columnNames['34']],
            //            DAY26: d[i][columnNames['35']],
            //            DAY27: d[i][columnNames['36']],
            //            DAY28: d[i][columnNames['37']],
            //            DAY29: d[i][columnNames['38']],
            //            DAY30: d[i][columnNames['39']],
            //            DAY31: d[i][columnNames['40']],
            //            DAY32: d[i][columnNames['41']],
            //            DAY33: d[i][columnNames['42']],
            //            DAY34: d[i][columnNames['43']],
            //            DAY35: d[i][columnNames['44']],
            //            DAY36: d[i][columnNames['45']],
            //            DAY37: d[i][columnNames['46']],
            //            DAY38: d[i][columnNames['47']],
            //            DAY39: d[i][columnNames['48']],
            //            DAY40: d[i][columnNames['49']],
            //            DAY41: d[i][columnNames['50']],
            //            DAY42: d[i][columnNames['51']],
            //            DAY43: d[i][columnNames['52']],
            //            DAY44: d[i][columnNames['53']],
            //            DAY45: d[i][columnNames['54']],
            //            DAY46: d[i][columnNames['55']],
            //            DAY47: d[i][columnNames['56']],
            //            DAY48: d[i][columnNames['57']],
            //            DAY49: d[i][columnNames['58']],
            //            DAY50: d[i][columnNames['59']],
            //            DAY51: d[i][columnNames['60']],
            //            DAY52: d[i][columnNames['61']],
            //            DAY53: d[i][columnNames['62']],
            //            DAY54: d[i][columnNames['63']],
            //            DAY55: d[i][columnNames['64']],
            //            DAY56: d[i][columnNames['65']],
            //            DAY57: d[i][columnNames['66']],
            //            DAY58: d[i][columnNames['67']],
            //            DAY59: d[i][columnNames['68']],
            //            DAY60: d[i][columnNames['69']],
            //            DAY61: d[i][columnNames['70']],
            //            DAY62: d[i][columnNames['71']],
            //            DAY63: d[i][columnNames['72']],
            //            DAY64: d[i][columnNames['73']],
            //            DAY65: d[i][columnNames['74']],
            //            DAY66: d[i][columnNames['75']],
            //            DAY67: d[i][columnNames['76']],
            //            DAY68: d[i][columnNames['77']],
            //            DAY69: d[i][columnNames['78']],
            //            DAY70: d[i][columnNames['79']],
            //            DAY71: d[i][columnNames['80']],
            //            DAY72: d[i][columnNames['81']],
            //            DAY73: d[i][columnNames['82']],
            //            DAY74: d[i][columnNames['83']],
            //            DAY75: d[i][columnNames['84']],
            //            DAY76: d[i][columnNames['85']],
            //            DAY77: d[i][columnNames['86']],
            //            DAY78: d[i][columnNames['87']],
            //            DAY79: d[i][columnNames['88']],
            //            DAY80: d[i][columnNames['89']],
            //            DAY81: d[i][columnNames['90']],
            //            DAY82: d[i][columnNames['91']],
            //            DAY83: d[i][columnNames['92']],
            //            DAY84: d[i][columnNames['93']],
            //            DAY85: d[i][columnNames['94']],
            //            DAY86: d[i][columnNames['95']],
            //            DAY87: d[i][columnNames['96']],
            //            DAY88: d[i][columnNames['97']],
            //            DAY89: d[i][columnNames['98']],
            //            DAY90: d[i][columnNames['99']],
            //            DAY91: d[i][columnNames['100']],
            //            DAY92: d[i][columnNames['101']],
            //            DAY93: d[i][columnNames['102']],
            //            DAY94: d[i][columnNames['103']],
            //            DAY95: d[i][columnNames['104']],
            //            DAY96: d[i][columnNames['105']],
            //            DAY97: d[i][columnNames['106']],
            //            DAY98: d[i][columnNames['107']],
            //            DAY99: d[i][columnNames['108']],
            //            DAY100: d[i][columnNames['109']],
            //            DAY101: d[i][columnNames['110']],
            //            DAY102: d[i][columnNames['111']],
            //            DAY103: d[i][columnNames['112']],
            //            DAY104: d[i][columnNames['113']],
            //            DAY105: d[i][columnNames['114']],
            //            DAY106: d[i][columnNames['115']],
            //            DAY107: d[i][columnNames['116']],
            //            DAY108: d[i][columnNames['117']],
            //            DAY109: d[i][columnNames['118']],
            //            DAY110: d[i][columnNames['119']],
            //            DAY111: d[i][columnNames['120']],
            //            DAY112: d[i][columnNames['121']],
            //            DAY113: d[i][columnNames['122']],
            //            DAY114: d[i][columnNames['123']],
            //            DAY115: d[i][columnNames['124']],
            //            DAY116: d[i][columnNames['125']],
            //            DAY117: d[i][columnNames['126']],
            //            DAY118: d[i][columnNames['127']],
            //            DAY119: d[i][columnNames['128']],
            //            DAY120: d[i][columnNames['129']],
            //            DAY121: d[i][columnNames['130']],
            //            DAY122: d[i][columnNames['131']],
            //            DAY123: d[i][columnNames['132']],
            //            DAY124: d[i][columnNames['133']],
            //            DAY125: d[i][columnNames['134']],
            //            DAY126: d[i][columnNames['135']],
            //            DAY127: d[i][columnNames['136']],
            //            DAY128: d[i][columnNames['137']],
            //            DAY129: d[i][columnNames['138']],
            //            DAY130: d[i][columnNames['139']],
            //            DAY131: d[i][columnNames['140']],
            //            DAY132: d[i][columnNames['141']],
            //            DAY133: d[i][columnNames['142']],
            //            DAY134: d[i][columnNames['143']],
            //            DAY135: d[i][columnNames['144']],
            //            DAY136: d[i][columnNames['145']],
            //            DAY137: d[i][columnNames['146']],
            //            DAY138: d[i][columnNames['147']],
            //            DAY139: d[i][columnNames['148']],
            //            DAY140: d[i][columnNames['149']],
            //            DAY141: d[i][columnNames['150']],
            //            DAY142: d[i][columnNames['151']],
            //            DAY143: d[i][columnNames['152']],
            //            DAY144: d[i][columnNames['153']],
            //            DAY145: d[i][columnNames['154']],
            //            DAY146: d[i][columnNames['155']],
            //            DAY147: d[i][columnNames['156']],
            //            DAY148: d[i][columnNames['157']],
            //            DAY149: d[i][columnNames['158']],
            //            DAY150: d[i][columnNames['159']],
            //            DAY151: d[i][columnNames['160']],
            //            DAY152: d[i][columnNames['161']],
            //            DAY153: d[i][columnNames['162']],
            //            DAY154: d[i][columnNames['163']],
            //            DAY155: d[i][columnNames['164']],
            //            DAY156: d[i][columnNames['165']],
            //            DAY157: d[i][columnNames['166']],
            //            DAY158: d[i][columnNames['167']],
            //            DAY159: d[i][columnNames['168']],
            //            DAY160: d[i][columnNames['169']],
            //            DAY161: d[i][columnNames['170']],
            //            DAY162: d[i][columnNames['171']],
            //            DAY163: d[i][columnNames['172']],
            //            DAY164: d[i][columnNames['173']],
            //            DAY165: d[i][columnNames['174']],
            //            DAY166: d[i][columnNames['175']],
            //            DAY167: d[i][columnNames['176']],
            //            DAY168: d[i][columnNames['177']],
            //            DAY169: d[i][columnNames['178']],
            //            DAY170: d[i][columnNames['179']],
            //            DAY171: d[i][columnNames['180']],
            //            DAY172: d[i][columnNames['181']],
            //            DAY173: d[i][columnNames['182']],
            //            DAY174: d[i][columnNames['183']],
            //            DAY175: d[i][columnNames['184']],
            //            DAY176: d[i][columnNames['185']],
            //            DAY177: d[i][columnNames['186']],
            //            DAY178: d[i][columnNames['187']],
            //            DAY179: d[i][columnNames['188']],
            //            DAY180: d[i][columnNames['189']],
            //            DAY181: d[i][columnNames['190']],
            //            DAY182: d[i][columnNames['191']],
            //            DAY183: d[i][columnNames['192']],
            //            DAY184: d[i][columnNames['193']],
            //            DAY185: d[i][columnNames['194']],
            //            DAY186: d[i][columnNames['195']],
            //            DAY187: d[i][columnNames['196']],
            //            DAY188: d[i][columnNames['197']],
            //            DAY189: d[i][columnNames['198']],
            //            DAY190: d[i][columnNames['199']],
            //            DAY191: d[i][columnNames['200']],
            //            DAY192: d[i][columnNames['201']],
            //            DAY193: d[i][columnNames['202']],
            //            DAY194: d[i][columnNames['203']],
            //            DAY195: d[i][columnNames['204']],
            //            DAY196: d[i][columnNames['205']],
            //            DAY197: d[i][columnNames['206']],
            //            DAY198: d[i][columnNames['207']],
            //            User: userid
            //        };
            //        DTOrig.push(newRow);
            //    }
            //}
        });
    });

});