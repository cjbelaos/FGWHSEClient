/**
 Created By : Chris John Belaos
 Created Date: 12/06/2022
 */

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

function GetPlantsByUserId(callback) {
    $.ajax({
        type: "POST",
        url: GlobalURL + "Form/PSIPartsSimulationAssemblyParts.aspx/GetPlantsByUserId",
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
        url: GlobalURL + "Form/PSIPartsSimulationAssemblyParts.aspx/GetVendorsByUserId",
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
        url: GlobalURL + "Form/PSIPartsSimulationAssemblyParts.aspx/GetPartsCodeByPlantandVendors",
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

function GetPartsSimulationAssemblyPartsByPlant(plant, parts, vendors, callback) {
    $.ajax({
        url: GlobalURL + "Form/PSIPartsSimulationAssemblyParts.aspx/GetPartsSimulationAssemblyPartsByPlant",
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
            DTForExport = d;
            DTOrig = [];
            if (callback !== undefined) {
                callback(d);
            }
            if (d.length > 0) {
                columnNames = Object.keys(d[0]);
                var FirstColumnName;
                for (var i in columnNames) {
                    FirstColumnName = columnNames[15];
                }
                ShowButtons();
                for (var i in d) {
                    if (d[i]['PLAN/LOGICAL'] == 'SUPPLIER PLAN/DELIVERY') {
                        var newRow = {
                            Plant: d[i]['PLANT'],
                            MaterialNumber: d[i]['MATERIALNUMBER'],
                            MainVendor: d[i]['SUPPLIER'],
                            FirstColumnName: FirstColumnName,
                            Past: d[i]['PAST'],
                            DAY1: d[i][columnNames['15']],
                            DAY2: d[i][columnNames['16']],
                            DAY3: d[i][columnNames['17']],
                            DAY4: d[i][columnNames['18']],
                            DAY5: d[i][columnNames['19']],
                            DAY6: d[i][columnNames['20']],
                            DAY7: d[i][columnNames['21']],
                            DAY8: d[i][columnNames['22']],
                            DAY9: d[i][columnNames['23']],
                            DAY10: d[i][columnNames['24']],
                            DAY11: d[i][columnNames['25']],
                            DAY12: d[i][columnNames['26']],
                            DAY13: d[i][columnNames['27']],
                            DAY14: d[i][columnNames['28']],
                            DAY15: d[i][columnNames['29']],
                            DAY16: d[i][columnNames['30']],
                            DAY17: d[i][columnNames['31']],
                            DAY18: d[i][columnNames['32']],
                            DAY19: d[i][columnNames['33']],
                            DAY20: d[i][columnNames['34']],
                            DAY21: d[i][columnNames['35']],
                            DAY22: d[i][columnNames['36']],
                            DAY23: d[i][columnNames['37']],
                            DAY24: d[i][columnNames['38']],
                            DAY25: d[i][columnNames['39']],
                            DAY26: d[i][columnNames['40']],
                            DAY27: d[i][columnNames['41']],
                            DAY28: d[i][columnNames['42']],
                            DAY29: d[i][columnNames['43']],
                            DAY30: d[i][columnNames['44']],
                            DAY31: d[i][columnNames['45']],
                            DAY32: d[i][columnNames['46']],
                            DAY33: d[i][columnNames['47']],
                            DAY34: d[i][columnNames['48']],
                            DAY35: d[i][columnNames['49']],
                            DAY36: d[i][columnNames['50']],
                            DAY37: d[i][columnNames['51']],
                            DAY38: d[i][columnNames['52']],
                            DAY39: d[i][columnNames['53']],
                            DAY40: d[i][columnNames['54']],
                            DAY41: d[i][columnNames['55']],
                            DAY42: d[i][columnNames['56']],
                            DAY43: d[i][columnNames['57']],
                            DAY44: d[i][columnNames['58']],
                            DAY45: d[i][columnNames['59']],
                            DAY46: d[i][columnNames['60']],
                            DAY47: d[i][columnNames['61']],
                            DAY48: d[i][columnNames['62']],
                            DAY49: d[i][columnNames['63']],
                            DAY50: d[i][columnNames['64']],
                            DAY51: d[i][columnNames['65']],
                            DAY52: d[i][columnNames['66']],
                            DAY53: d[i][columnNames['67']],
                            DAY54: d[i][columnNames['68']],
                            DAY55: d[i][columnNames['69']],
                            DAY56: d[i][columnNames['70']],
                            DAY57: d[i][columnNames['71']],
                            DAY58: d[i][columnNames['72']],
                            DAY59: d[i][columnNames['73']],
                            DAY60: d[i][columnNames['74']],
                            DAY61: d[i][columnNames['75']],
                            DAY62: d[i][columnNames['76']],
                            DAY63: d[i][columnNames['77']],
                            DAY64: d[i][columnNames['78']],
                            DAY65: d[i][columnNames['79']],
                            DAY66: d[i][columnNames['80']],
                            DAY67: d[i][columnNames['81']],
                            DAY68: d[i][columnNames['82']],
                            DAY69: d[i][columnNames['83']],
                            DAY70: d[i][columnNames['84']],
                            DAY71: d[i][columnNames['85']],
                            DAY72: d[i][columnNames['86']],
                            DAY73: d[i][columnNames['87']],
                            DAY74: d[i][columnNames['88']],
                            DAY75: d[i][columnNames['89']],
                            DAY76: d[i][columnNames['90']],
                            DAY77: d[i][columnNames['91']],
                            DAY78: d[i][columnNames['92']],
                            DAY79: d[i][columnNames['93']],
                            DAY80: d[i][columnNames['94']],
                            DAY81: d[i][columnNames['95']],
                            DAY82: d[i][columnNames['96']],
                            DAY83: d[i][columnNames['97']],
                            DAY84: d[i][columnNames['98']],
                            DAY85: d[i][columnNames['99']],
                            DAY86: d[i][columnNames['100']],
                            DAY87: d[i][columnNames['101']],
                            DAY88: d[i][columnNames['102']],
                            DAY89: d[i][columnNames['103']],
                            DAY90: d[i][columnNames['104']],
                            DAY91: d[i][columnNames['105']],
                            DAY92: d[i][columnNames['106']],
                            DAY93: d[i][columnNames['107']],
                            DAY94: d[i][columnNames['108']],
                            DAY95: d[i][columnNames['109']],
                            DAY96: d[i][columnNames['110']],
                            DAY97: d[i][columnNames['111']],
                            DAY98: d[i][columnNames['112']],
                            DAY99: d[i][columnNames['113']],
                            DAY100: d[i][columnNames['114']],
                            DAY101: d[i][columnNames['115']],
                            DAY102: d[i][columnNames['116']],
                            DAY103: d[i][columnNames['117']],
                            DAY104: d[i][columnNames['118']],
                            DAY105: d[i][columnNames['119']],
                            DAY106: d[i][columnNames['120']],
                            DAY107: d[i][columnNames['121']],
                            DAY108: d[i][columnNames['122']],
                            DAY109: d[i][columnNames['123']],
                            DAY110: d[i][columnNames['124']],
                            DAY111: d[i][columnNames['125']],
                            DAY112: d[i][columnNames['126']],
                            DAY113: d[i][columnNames['127']],
                            DAY114: d[i][columnNames['128']],
                            DAY115: d[i][columnNames['129']],
                            DAY116: d[i][columnNames['130']],
                            DAY117: d[i][columnNames['131']],
                            DAY118: d[i][columnNames['132']],
                            DAY119: d[i][columnNames['133']],
                            DAY120: d[i][columnNames['134']],
                            DAY121: d[i][columnNames['135']],
                            DAY122: d[i][columnNames['136']],
                            DAY123: d[i][columnNames['137']],
                            DAY124: d[i][columnNames['138']],
                            DAY125: d[i][columnNames['139']],
                            DAY126: d[i][columnNames['140']],
                            DAY127: d[i][columnNames['141']],
                            DAY128: d[i][columnNames['142']],
                            DAY129: d[i][columnNames['143']],
                            DAY130: d[i][columnNames['144']],
                            DAY131: d[i][columnNames['145']],
                            DAY132: d[i][columnNames['146']],
                            DAY133: d[i][columnNames['147']],
                            DAY134: d[i][columnNames['148']],
                            DAY135: d[i][columnNames['149']],
                            DAY136: d[i][columnNames['150']],
                            DAY137: d[i][columnNames['151']],
                            DAY138: d[i][columnNames['152']],
                            DAY139: d[i][columnNames['153']],
                            DAY140: d[i][columnNames['154']],
                            DAY141: d[i][columnNames['155']],
                            DAY142: d[i][columnNames['156']],
                            DAY143: d[i][columnNames['157']],
                            DAY144: d[i][columnNames['158']],
                            DAY145: d[i][columnNames['159']],
                            DAY146: d[i][columnNames['160']],
                            DAY147: d[i][columnNames['161']],
                            DAY148: d[i][columnNames['162']],
                            DAY149: d[i][columnNames['163']],
                            DAY150: d[i][columnNames['164']],
                            DAY151: d[i][columnNames['165']],
                            DAY152: d[i][columnNames['166']],
                            DAY153: d[i][columnNames['167']],
                            DAY154: d[i][columnNames['168']],
                            DAY155: d[i][columnNames['169']],
                            DAY156: d[i][columnNames['170']],
                            DAY157: d[i][columnNames['171']],
                            DAY158: d[i][columnNames['172']],
                            DAY159: d[i][columnNames['173']],
                            DAY160: d[i][columnNames['174']],
                            DAY161: d[i][columnNames['175']],
                            DAY162: d[i][columnNames['176']],
                            DAY163: d[i][columnNames['177']],
                            DAY164: d[i][columnNames['178']],
                            DAY165: d[i][columnNames['179']],
                            DAY166: d[i][columnNames['180']],
                            DAY167: d[i][columnNames['181']],
                            DAY168: d[i][columnNames['182']],
                            DAY169: d[i][columnNames['183']],
                            DAY170: d[i][columnNames['184']],
                            DAY171: d[i][columnNames['185']],
                            DAY172: d[i][columnNames['186']],
                            DAY173: d[i][columnNames['187']],
                            DAY174: d[i][columnNames['188']],
                            DAY175: d[i][columnNames['189']],
                            DAY176: d[i][columnNames['190']],
                            DAY177: d[i][columnNames['191']],
                            DAY178: d[i][columnNames['192']],
                            DAY179: d[i][columnNames['193']],
                            DAY180: d[i][columnNames['194']],
                            DAY181: d[i][columnNames['195']],
                            DAY182: d[i][columnNames['196']],
                            DAY183: d[i][columnNames['197']],
                            DAY184: d[i][columnNames['198']],
                            DAY185: d[i][columnNames['199']],
                            DAY186: d[i][columnNames['200']],
                            DAY187: d[i][columnNames['201']],
                            DAY188: d[i][columnNames['202']],
                            DAY189: d[i][columnNames['203']],
                            DAY190: d[i][columnNames['204']],
                            DAY191: d[i][columnNames['205']],
                            DAY192: d[i][columnNames['206']],
                            DAY193: d[i][columnNames['207']],
                            DAY194: d[i][columnNames['208']],
                            DAY195: d[i][columnNames['209']],
                            DAY196: d[i][columnNames['210']],
                            DAY197: d[i][columnNames['211']],
                            DAY198: d[i][columnNames['212']],
                            User: userid
                        };
                        DTOrig.push(newRow);
                    }
                }
                DrawTable(d);
            }
            else {
                alert('No data has been found!')
                HideButtons();
                DrawTable(d);
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
                if (['EPPISTCK', 'SUPPLIERSTCK', 'TOTALSTCK'].includes(colName)) {
                    colType[j] = {
                        type: 'numeric', numericFormat: {
                            pattern: '0,00',
                            culture: 'en-US'
                        }, readOnly: true
                    };
                }
                if (j >= 14) {
                    colType[j] = {
                        type: 'numeric', numericFormat: {
                            pattern: '0,00',
                            culture: 'en-US'
                        }
                    };
                    if (data[i]['PLAN/LOGICAL'] == 'END STOCKS 1') {
                        if (colName == 'PAST') {
                            e[j] = e[j - 2] - data[i - 2][columns[j]];
                        } else {
                            e[j] = e[j - 1] - data[i - 2][columns[j]];
                        }
                    }
                    if (data[i]['PLAN/LOGICAL'] == 'END STOCKS 2') {
                        if (colName == 'PAST') {
                            e[j] = e[j - 2] + data[i - 2][columns[j]] - data[i - 3][columns[j]];
                        } else {
                            e[j] = e[j - 1] + data[i - 2][columns[j]] - data[i - 3][columns[j]];
                        }
                    }
                }
            }
            d[i] = e;
        }

        //var hiddenColumns = {
        //    columns: [2],
        //    indicators: false
        //};

        if ($('#MyTable').hasClass('handsontable htColumnHeaders')) {
            MyTable.updateSettings({
                fixedColumnsLeft: false
            })
        }

        var config = {
            data: d,
            colHeaders: columns,
            colWidths: xColWidth,
            columns: colType,
            //hiddenColumns: hiddenColumns,
            fixedColumnsLeft: 4,
            autoRowSize: true,
            csvHeaders: true,
            tableOverflow: true,
            beforeChange: BeforeChange,
            afterChange: AfterChange,
            dropdownMenu: true,
            filters: true,
            filteringCaseSensitive: true,
            height: '300px',
            width: $("#MyTable").css("width"),
            licenseKey: 'f8a35-a4d7b-607a2-f2225-39038'
        };
        MyTable = Handsontable(document.getElementById("MyTable"), config);

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
///**
// * draw excel table to screen
// */
function DrawNewTable(data, callback) {
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
                if (['EPPISTCK', 'SUPPLIERSTCK', 'TOTALSTCK'].includes(colName)) {
                    colType[j] = {
                        type: 'numeric', numericFormat: {
                            pattern: '0,00',
                            culture: 'en-US'
                        }, readOnly: true
                    };
                }
                if (j >= 15) {
                    colType[j] = {
                        type: 'numeric', numericFormat: {
                            pattern: '0,00',
                            culture: 'en-US'
                        }
                    };
                    if (data[i]['PLAN/LOGICAL'] == 'END STOCKS 1') {
                        if (colName == 'PAST') {
                            e[j] = e[j - 2] - data[i - 2][columns[j]];
                        } else {
                            e[j] = e[j - 1] - data[i - 2][columns[j]];
                        }
                    }
                    if (data[i]['PLAN/LOGICAL'] == 'END STOCKS 2') {
                        if (colName == 'PAST') {
                            e[j] = e[j - 2] + data[i - 2][columns[j]] - data[i - 3][columns[j]];
                        } else {
                            e[j] = e[j - 1] + data[i - 2][columns[j]] - data[i - 3][columns[j]];
                        }
                    }
                }
            }
            d[i] = e;
        }

        if ($('#MyTable').hasClass('handsontable htColumnHeaders')) {
            MyTable.updateSettings({
                fixedColumnsLeft: false
            })
        }

        var config = {
            data: d,
            colHeaders: columns,
            colWidths: xColWidth,
            columns: colType,
            fixedColumnsLeft: 4,
            autoRowSize: true,
            csvHeaders: true,
            tableOverflow: true,
            beforeChange: BeforeChange,
            afterChange: AfterChange,
            dropdownMenu: true,
            filters: true,
            filteringCaseSensitive: true,
            height: '300px',
            width: $("#MyTable").css("width"),
            licenseKey: 'f8a35-a4d7b-607a2-f2225-39038'
        };
        MyTable = Handsontable(document.getElementById("MyTable"), config);

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
    if (callback !== undefined) {
        callback(data.length);
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
            var PLIndex = headers.indexOf('PLAN/LOGICAL');
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
            var PLIndex = headers.indexOf('PLAN/LOGICAL');
            var PlanLogical = MyTable.getDataAtCell(row, PLIndex);
            if (PlanLogical == 'SUPPLIER PLAN/DELIVERY') {
                if (headers[col] == 'PAST') {
                    //console.log(row + 2, col - 2);
                    //console.log(row - 1, col);
                    //console.log(MyTable.getDataAtCell(row + 2, col - 2), nv, MyTable.getDataAtCell(row - 1, col));
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
 * to change the negative value to red
 */
function negativeValueRenderer(instance, td, row, col, prop, value, cellProperties) {
    Handsontable.renderers.TextRenderer.apply(this, arguments);
    instance.setCellMeta(row, col, 'className', 'htRight htNumeric NegativeCell');
    value = value.toString();
    $(td).text('(' + value.replace('-', '') + ')');
}
/**
 * creating the excel file based on data sent
 */
function Export(callback) {

    var currentDate = new Date();
    var filename = 'PartsSimulationPartsAssembly(' + moment(currentDate).format('YYYYMMDDhhmmss') + ').xlsx';

    var data = DTForExport;
    var columns = [];

    var headers = MyTable.getColHeader();

    for (var i in headers) {
        columns[columns.length] = {
            field: headers[i],
            title: headers[i]
        };
    }

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
    var worksheet = workbook.addWorksheet('Parts Simulation');
    //index for row and column of ExcelJS starts with 1
    var rowIndex = 1;
    var row = worksheet.getRow(rowIndex);
    var row2, row3, colTotalStock, strFormula = "";
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
        for (var j in columns) {
            var NumericColumns = ['EPPISTCK', 'SUPPLIERSTCK', 'TOTALSTCK'];
            var Title = columns[j].title;
            var val = data[i][columns[j].field];
            lastCell = row.getCell(parseInt(j) + 1);
            if (NumericColumns.includes(Title)) {
                lastCell.numFmt = NumberFormatString;
            }
            lastCell.value = val;
        }
        for (var j in data[i]) {
            if (data[i][j] == 'END STOCKS 1') {
                for (var j = 14; j <= 212; j++) {
                    var ColumnLetter = ExcelColumn(j).toString();
                    var cell = worksheet.getCell(ColumnLetter + rowIndex);
                    var FormulaColumnLetter = '';
                    if (ColumnLetter === 'O') {
                        FormulaColumnLetter = ExcelColumn(j - 2);
                        strFormula = String.format("{0}{1}-{2}{3}", FormulaColumnLetter, rowIndex, ColumnLetter, rowIndex - 2);
                        cell.value = { formula: strFormula };
                    }
                    else {
                        FormulaColumnLetter = ExcelColumn(j - 1);
                        strFormula = String.format("{0}{1}-{2}{3}", FormulaColumnLetter, rowIndex, ColumnLetter, rowIndex - 2);
                        cell.value = { formula: strFormula };
                    }
                }
            }
            if (data[i][j] == 'END STOCKS 2') {
                for (var j = 14; j <= 212; j++) {
                    var ColumnLetter = ExcelColumn(j).toString();
                    var cell = worksheet.getCell(ColumnLetter + rowIndex);
                    var FormulaColumnLetter = '';
                    if (ColumnLetter === 'O') {
                        FormulaColumnLetter = ExcelColumn(j - 2);
                        strFormula = String.format("{0}{1}+{2}{3}-{4}{5}", FormulaColumnLetter, rowIndex, ColumnLetter, rowIndex - 2, ColumnLetter, rowIndex - 3);
                        cell.value = { formula: strFormula };
                    }
                    else {
                        FormulaColumnLetter = ExcelColumn(j - 1);
                        strFormula = String.format("{0}{1}+{2}{3}-{4}{5}", FormulaColumnLetter, rowIndex, ColumnLetter, rowIndex - 2, ColumnLetter, rowIndex - 3);
                        cell.value = { formula: strFormula };
                    }
                }
            }
        }
    }


    worksheet.addConditionalFormatting({
        ref: 'K2:' + lastCell._address,
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
 * save the simulation in the database
 */
function Save() {
    var headers = MyTable.getColHeader();
    var data = MyTable.getSourceData();
    var newData = DataMapping(headers, data);
    var DataForSaving = [];
    for (var i in newData) {
        var FirstColumnName = headers[15];
        for (var j in headers) {
            var Title = headers[j];
            var Value = newData[i][Title];
        }
        if (newData[i]['PLAN/LOGICAL'] == 'SUPPLIER PLAN/DELIVERY') {
            var newRow = {
                Plant: newData[i]['PLANT'],
                MaterialNumber: newData[i]['MATERIALNUMBER'],
                MainVendor: newData[i]['SUPPLIER'],
                FirstColumnName: FirstColumnName,
                Past: newData[i]['PAST'],
                DAY1: newData[i][headers['15']],
                DAY2: newData[i][headers['16']],
                DAY3: newData[i][headers['17']],
                DAY4: newData[i][headers['18']],
                DAY5: newData[i][headers['19']],
                DAY6: newData[i][headers['20']],
                DAY7: newData[i][headers['21']],
                DAY8: newData[i][headers['22']],
                DAY9: newData[i][headers['23']],
                DAY10: newData[i][headers['24']],
                DAY11: newData[i][headers['25']],
                DAY12: newData[i][headers['26']],
                DAY13: newData[i][headers['27']],
                DAY14: newData[i][headers['28']],
                DAY15: newData[i][headers['29']],
                DAY16: newData[i][headers['30']],
                DAY17: newData[i][headers['31']],
                DAY18: newData[i][headers['32']],
                DAY19: newData[i][headers['33']],
                DAY20: newData[i][headers['34']],
                DAY21: newData[i][headers['35']],
                DAY22: newData[i][headers['36']],
                DAY23: newData[i][headers['37']],
                DAY24: newData[i][headers['38']],
                DAY25: newData[i][headers['39']],
                DAY26: newData[i][headers['40']],
                DAY27: newData[i][headers['41']],
                DAY28: newData[i][headers['42']],
                DAY29: newData[i][headers['43']],
                DAY30: newData[i][headers['44']],
                DAY31: newData[i][headers['45']],
                DAY32: newData[i][headers['46']],
                DAY33: newData[i][headers['47']],
                DAY34: newData[i][headers['48']],
                DAY35: newData[i][headers['49']],
                DAY36: newData[i][headers['50']],
                DAY37: newData[i][headers['51']],
                DAY38: newData[i][headers['52']],
                DAY39: newData[i][headers['53']],
                DAY40: newData[i][headers['54']],
                DAY41: newData[i][headers['55']],
                DAY42: newData[i][headers['56']],
                DAY43: newData[i][headers['57']],
                DAY44: newData[i][headers['58']],
                DAY45: newData[i][headers['59']],
                DAY46: newData[i][headers['60']],
                DAY47: newData[i][headers['61']],
                DAY48: newData[i][headers['62']],
                DAY49: newData[i][headers['63']],
                DAY50: newData[i][headers['64']],
                DAY51: newData[i][headers['65']],
                DAY52: newData[i][headers['66']],
                DAY53: newData[i][headers['67']],
                DAY54: newData[i][headers['68']],
                DAY55: newData[i][headers['69']],
                DAY56: newData[i][headers['70']],
                DAY57: newData[i][headers['71']],
                DAY58: newData[i][headers['72']],
                DAY59: newData[i][headers['73']],
                DAY60: newData[i][headers['74']],
                DAY61: newData[i][headers['75']],
                DAY62: newData[i][headers['76']],
                DAY63: newData[i][headers['77']],
                DAY64: newData[i][headers['78']],
                DAY65: newData[i][headers['79']],
                DAY66: newData[i][headers['80']],
                DAY67: newData[i][headers['81']],
                DAY68: newData[i][headers['82']],
                DAY69: newData[i][headers['83']],
                DAY70: newData[i][headers['84']],
                DAY71: newData[i][headers['85']],
                DAY72: newData[i][headers['86']],
                DAY73: newData[i][headers['87']],
                DAY74: newData[i][headers['88']],
                DAY75: newData[i][headers['89']],
                DAY76: newData[i][headers['90']],
                DAY77: newData[i][headers['91']],
                DAY78: newData[i][headers['92']],
                DAY79: newData[i][headers['93']],
                DAY80: newData[i][headers['94']],
                DAY81: newData[i][headers['95']],
                DAY82: newData[i][headers['96']],
                DAY83: newData[i][headers['97']],
                DAY84: newData[i][headers['98']],
                DAY85: newData[i][headers['99']],
                DAY86: newData[i][headers['100']],
                DAY87: newData[i][headers['101']],
                DAY88: newData[i][headers['102']],
                DAY89: newData[i][headers['103']],
                DAY90: newData[i][headers['104']],
                DAY91: newData[i][headers['105']],
                DAY92: newData[i][headers['106']],
                DAY93: newData[i][headers['107']],
                DAY94: newData[i][headers['108']],
                DAY95: newData[i][headers['109']],
                DAY96: newData[i][headers['110']],
                DAY97: newData[i][headers['111']],
                DAY98: newData[i][headers['112']],
                DAY99: newData[i][headers['113']],
                DAY100: newData[i][headers['114']],
                DAY101: newData[i][headers['115']],
                DAY102: newData[i][headers['116']],
                DAY103: newData[i][headers['117']],
                DAY104: newData[i][headers['118']],
                DAY105: newData[i][headers['119']],
                DAY106: newData[i][headers['120']],
                DAY107: newData[i][headers['121']],
                DAY108: newData[i][headers['122']],
                DAY109: newData[i][headers['123']],
                DAY110: newData[i][headers['124']],
                DAY111: newData[i][headers['125']],
                DAY112: newData[i][headers['126']],
                DAY113: newData[i][headers['127']],
                DAY114: newData[i][headers['128']],
                DAY115: newData[i][headers['129']],
                DAY116: newData[i][headers['130']],
                DAY117: newData[i][headers['131']],
                DAY118: newData[i][headers['132']],
                DAY119: newData[i][headers['133']],
                DAY120: newData[i][headers['134']],
                DAY121: newData[i][headers['135']],
                DAY122: newData[i][headers['136']],
                DAY123: newData[i][headers['137']],
                DAY124: newData[i][headers['138']],
                DAY125: newData[i][headers['139']],
                DAY126: newData[i][headers['140']],
                DAY127: newData[i][headers['141']],
                DAY128: newData[i][headers['142']],
                DAY129: newData[i][headers['143']],
                DAY130: newData[i][headers['144']],
                DAY131: newData[i][headers['145']],
                DAY132: newData[i][headers['146']],
                DAY133: newData[i][headers['147']],
                DAY134: newData[i][headers['148']],
                DAY135: newData[i][headers['149']],
                DAY136: newData[i][headers['150']],
                DAY137: newData[i][headers['151']],
                DAY138: newData[i][headers['152']],
                DAY139: newData[i][headers['153']],
                DAY140: newData[i][headers['154']],
                DAY141: newData[i][headers['155']],
                DAY142: newData[i][headers['156']],
                DAY143: newData[i][headers['157']],
                DAY144: newData[i][headers['158']],
                DAY145: newData[i][headers['159']],
                DAY146: newData[i][headers['160']],
                DAY147: newData[i][headers['161']],
                DAY148: newData[i][headers['162']],
                DAY149: newData[i][headers['163']],
                DAY150: newData[i][headers['164']],
                DAY151: newData[i][headers['165']],
                DAY152: newData[i][headers['166']],
                DAY153: newData[i][headers['167']],
                DAY154: newData[i][headers['168']],
                DAY155: newData[i][headers['169']],
                DAY156: newData[i][headers['170']],
                DAY157: newData[i][headers['171']],
                DAY158: newData[i][headers['172']],
                DAY159: newData[i][headers['173']],
                DAY160: newData[i][headers['174']],
                DAY161: newData[i][headers['175']],
                DAY162: newData[i][headers['176']],
                DAY163: newData[i][headers['177']],
                DAY164: newData[i][headers['178']],
                DAY165: newData[i][headers['179']],
                DAY166: newData[i][headers['180']],
                DAY167: newData[i][headers['181']],
                DAY168: newData[i][headers['182']],
                DAY169: newData[i][headers['183']],
                DAY170: newData[i][headers['184']],
                DAY171: newData[i][headers['185']],
                DAY172: newData[i][headers['186']],
                DAY173: newData[i][headers['187']],
                DAY174: newData[i][headers['188']],
                DAY175: newData[i][headers['189']],
                DAY176: newData[i][headers['190']],
                DAY177: newData[i][headers['191']],
                DAY178: newData[i][headers['192']],
                DAY179: newData[i][headers['193']],
                DAY180: newData[i][headers['194']],
                DAY181: newData[i][headers['195']],
                DAY182: newData[i][headers['196']],
                DAY183: newData[i][headers['197']],
                DAY184: newData[i][headers['198']],
                DAY185: newData[i][headers['199']],
                DAY186: newData[i][headers['200']],
                DAY187: newData[i][headers['201']],
                DAY188: newData[i][headers['202']],
                DAY189: newData[i][headers['203']],
                DAY190: newData[i][headers['204']],
                DAY191: newData[i][headers['205']],
                DAY192: newData[i][headers['206']],
                DAY193: newData[i][headers['207']],
                DAY194: newData[i][headers['208']],
                DAY195: newData[i][headers['209']],
                DAY196: newData[i][headers['210']],
                DAY197: newData[i][headers['211']],
                DAY198: newData[i][headers['212']],
                User: userid
            };
            DataForSaving.push(newRow);
        }
    }
    newData = DataForSaving;
    console.log("Old Data", DTOrig);
    console.log("New Data", DataForSaving);
    DataForSaving = CompareData(DTOrig, DataForSaving);
    if (isUploaded === true) {
        DataForSaving = DataForSaving.length == 0 ? newData : DataForSaving;
    }
    console.log(JSON.stringify({ PS: DataForSaving }));
    if (DataForSaving.length > 0) {
        $.ajax({
            url: GlobalURL + "Form/PSIPartsSimulationAssemblyParts.aspx/SaveData",
            type: "POST",
            data: JSON.stringify({ PS: DataForSaving }),
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
    else {
        alert('No data changes to be saved!')
    }
}
/**
 * to compare the old and new data
 */
function CompareData(oData, nData) {
    var data = [];
    for (var i in oData) {
        var oPlant = oData[i]['Plant'];
        var oMaterialNumber = oData[i]['MaterialNumber'];
        var oMainVendor = oData[i]['MainVendor'];
        var oFirstColumnName = oData[i]['FirstColumnName'];
        var oPast = oData[i]['Past'];
        var oDAY1 = oData[i]['DAY1'];
        var oDAY2 = oData[i]['DAY2'];
        var oDAY3 = oData[i]['DAY3'];
        var oDAY4 = oData[i]['DAY4'];
        var oDAY5 = oData[i]['DAY5'];
        var oDAY6 = oData[i]['DAY6'];
        var oDAY7 = oData[i]['DAY7'];
        var oDAY8 = oData[i]['DAY8'];
        var oDAY9 = oData[i]['DAY9'];
        var oDAY10 = oData[i]['DAY10'];
        var oDAY11 = oData[i]['DAY11'];
        var oDAY12 = oData[i]['DAY12'];
        var oDAY13 = oData[i]['DAY13'];
        var oDAY14 = oData[i]['DAY14'];
        var oDAY15 = oData[i]['DAY15'];
        var oDAY16 = oData[i]['DAY16'];
        var oDAY17 = oData[i]['DAY17'];
        var oDAY18 = oData[i]['DAY18'];
        var oDAY19 = oData[i]['DAY19'];
        var oDAY20 = oData[i]['DAY20'];
        var oDAY21 = oData[i]['DAY21'];
        var oDAY22 = oData[i]['DAY22'];
        var oDAY23 = oData[i]['DAY23'];
        var oDAY24 = oData[i]['DAY24'];
        var oDAY25 = oData[i]['DAY25'];
        var oDAY26 = oData[i]['DAY26'];
        var oDAY27 = oData[i]['DAY27'];
        var oDAY28 = oData[i]['DAY28'];
        var oDAY29 = oData[i]['DAY29'];
        var oDAY30 = oData[i]['DAY30'];
        var oDAY31 = oData[i]['DAY31'];
        var oDAY32 = oData[i]['DAY32'];
        var oDAY33 = oData[i]['DAY33'];
        var oDAY34 = oData[i]['DAY34'];
        var oDAY35 = oData[i]['DAY35'];
        var oDAY36 = oData[i]['DAY36'];
        var oDAY37 = oData[i]['DAY37'];
        var oDAY38 = oData[i]['DAY38'];
        var oDAY39 = oData[i]['DAY39'];
        var oDAY40 = oData[i]['DAY40'];
        var oDAY41 = oData[i]['DAY41'];
        var oDAY42 = oData[i]['DAY42'];
        var oDAY43 = oData[i]['DAY43'];
        var oDAY44 = oData[i]['DAY44'];
        var oDAY45 = oData[i]['DAY45'];
        var oDAY46 = oData[i]['DAY46'];
        var oDAY47 = oData[i]['DAY47'];
        var oDAY48 = oData[i]['DAY48'];
        var oDAY49 = oData[i]['DAY49'];
        var oDAY50 = oData[i]['DAY50'];
        var oDAY51 = oData[i]['DAY51'];
        var oDAY52 = oData[i]['DAY52'];
        var oDAY53 = oData[i]['DAY53'];
        var oDAY54 = oData[i]['DAY54'];
        var oDAY55 = oData[i]['DAY55'];
        var oDAY56 = oData[i]['DAY56'];
        var oDAY57 = oData[i]['DAY57'];
        var oDAY58 = oData[i]['DAY58'];
        var oDAY59 = oData[i]['DAY59'];
        var oDAY60 = oData[i]['DAY60'];
        var oDAY61 = oData[i]['DAY61'];
        var oDAY62 = oData[i]['DAY62'];
        var oDAY63 = oData[i]['DAY63'];
        var oDAY64 = oData[i]['DAY64'];
        var oDAY65 = oData[i]['DAY65'];
        var oDAY66 = oData[i]['DAY66'];
        var oDAY67 = oData[i]['DAY67'];
        var oDAY68 = oData[i]['DAY68'];
        var oDAY69 = oData[i]['DAY69'];
        var oDAY70 = oData[i]['DAY70'];
        var oDAY71 = oData[i]['DAY71'];
        var oDAY72 = oData[i]['DAY72'];
        var oDAY73 = oData[i]['DAY73'];
        var oDAY74 = oData[i]['DAY74'];
        var oDAY75 = oData[i]['DAY75'];
        var oDAY76 = oData[i]['DAY76'];
        var oDAY77 = oData[i]['DAY77'];
        var oDAY78 = oData[i]['DAY78'];
        var oDAY79 = oData[i]['DAY79'];
        var oDAY80 = oData[i]['DAY80'];
        var oDAY81 = oData[i]['DAY81'];
        var oDAY82 = oData[i]['DAY82'];
        var oDAY83 = oData[i]['DAY83'];
        var oDAY84 = oData[i]['DAY84'];
        var oDAY85 = oData[i]['DAY85'];
        var oDAY86 = oData[i]['DAY86'];
        var oDAY87 = oData[i]['DAY87'];
        var oDAY88 = oData[i]['DAY88'];
        var oDAY89 = oData[i]['DAY89'];
        var oDAY90 = oData[i]['DAY90'];
        var oDAY91 = oData[i]['DAY91'];
        var oDAY92 = oData[i]['DAY92'];
        var oDAY93 = oData[i]['DAY93'];
        var oDAY94 = oData[i]['DAY94'];
        var oDAY95 = oData[i]['DAY95'];
        var oDAY96 = oData[i]['DAY96'];
        var oDAY97 = oData[i]['DAY97'];
        var oDAY98 = oData[i]['DAY98'];
        var oDAY99 = oData[i]['DAY99'];
        var oDAY100 = oData[i]['DAY100'];
        var oDAY101 = oData[i]['DAY101'];
        var oDAY102 = oData[i]['DAY102'];
        var oDAY103 = oData[i]['DAY103'];
        var oDAY104 = oData[i]['DAY104'];
        var oDAY105 = oData[i]['DAY105'];
        var oDAY106 = oData[i]['DAY106'];
        var oDAY107 = oData[i]['DAY107'];
        var oDAY108 = oData[i]['DAY108'];
        var oDAY109 = oData[i]['DAY109'];
        var oDAY110 = oData[i]['DAY110'];
        var oDAY111 = oData[i]['DAY111'];
        var oDAY112 = oData[i]['DAY112'];
        var oDAY113 = oData[i]['DAY113'];
        var oDAY114 = oData[i]['DAY114'];
        var oDAY115 = oData[i]['DAY115'];
        var oDAY116 = oData[i]['DAY116'];
        var oDAY117 = oData[i]['DAY117'];
        var oDAY118 = oData[i]['DAY118'];
        var oDAY119 = oData[i]['DAY119'];
        var oDAY120 = oData[i]['DAY120'];
        var oDAY121 = oData[i]['DAY121'];
        var oDAY122 = oData[i]['DAY122'];
        var oDAY123 = oData[i]['DAY123'];
        var oDAY124 = oData[i]['DAY124'];
        var oDAY125 = oData[i]['DAY125'];
        var oDAY126 = oData[i]['DAY126'];
        var oDAY127 = oData[i]['DAY127'];
        var oDAY128 = oData[i]['DAY128'];
        var oDAY129 = oData[i]['DAY129'];
        var oDAY130 = oData[i]['DAY130'];
        var oDAY131 = oData[i]['DAY131'];
        var oDAY132 = oData[i]['DAY132'];
        var oDAY133 = oData[i]['DAY133'];
        var oDAY134 = oData[i]['DAY134'];
        var oDAY135 = oData[i]['DAY135'];
        var oDAY136 = oData[i]['DAY136'];
        var oDAY137 = oData[i]['DAY137'];
        var oDAY138 = oData[i]['DAY138'];
        var oDAY139 = oData[i]['DAY139'];
        var oDAY140 = oData[i]['DAY140'];
        var oDAY141 = oData[i]['DAY141'];
        var oDAY142 = oData[i]['DAY142'];
        var oDAY143 = oData[i]['DAY143'];
        var oDAY144 = oData[i]['DAY144'];
        var oDAY145 = oData[i]['DAY145'];
        var oDAY146 = oData[i]['DAY146'];
        var oDAY147 = oData[i]['DAY147'];
        var oDAY148 = oData[i]['DAY148'];
        var oDAY149 = oData[i]['DAY149'];
        var oDAY150 = oData[i]['DAY150'];
        var oDAY151 = oData[i]['DAY151'];
        var oDAY152 = oData[i]['DAY152'];
        var oDAY153 = oData[i]['DAY153'];
        var oDAY154 = oData[i]['DAY154'];
        var oDAY155 = oData[i]['DAY155'];
        var oDAY156 = oData[i]['DAY156'];
        var oDAY157 = oData[i]['DAY157'];
        var oDAY158 = oData[i]['DAY158'];
        var oDAY159 = oData[i]['DAY159'];
        var oDAY160 = oData[i]['DAY160'];
        var oDAY161 = oData[i]['DAY161'];
        var oDAY162 = oData[i]['DAY162'];
        var oDAY163 = oData[i]['DAY163'];
        var oDAY164 = oData[i]['DAY164'];
        var oDAY165 = oData[i]['DAY165'];
        var oDAY166 = oData[i]['DAY166'];
        var oDAY167 = oData[i]['DAY167'];
        var oDAY168 = oData[i]['DAY168'];
        var oDAY169 = oData[i]['DAY169'];
        var oDAY170 = oData[i]['DAY170'];
        var oDAY171 = oData[i]['DAY171'];
        var oDAY172 = oData[i]['DAY172'];
        var oDAY173 = oData[i]['DAY173'];
        var oDAY174 = oData[i]['DAY174'];
        var oDAY175 = oData[i]['DAY175'];
        var oDAY176 = oData[i]['DAY176'];
        var oDAY177 = oData[i]['DAY177'];
        var oDAY178 = oData[i]['DAY178'];
        var oDAY179 = oData[i]['DAY179'];
        var oDAY180 = oData[i]['DAY180'];
        var oDAY181 = oData[i]['DAY181'];
        var oDAY182 = oData[i]['DAY182'];
        var oDAY183 = oData[i]['DAY183'];
        var oDAY184 = oData[i]['DAY184'];
        var oDAY185 = oData[i]['DAY185'];
        var oDAY186 = oData[i]['DAY186'];
        var oDAY187 = oData[i]['DAY187'];
        var oDAY188 = oData[i]['DAY188'];
        var oDAY189 = oData[i]['DAY189'];
        var oDAY190 = oData[i]['DAY190'];
        var oDAY191 = oData[i]['DAY191'];
        var oDAY192 = oData[i]['DAY192'];
        var oDAY193 = oData[i]['DAY193'];
        var oDAY194 = oData[i]['DAY194'];
        var oDAY195 = oData[i]['DAY195'];
        var oDAY196 = oData[i]['DAY196'];
        var oDAY197 = oData[i]['DAY197'];
        var oDAY198 = oData[i]['DAY198'];
        var oUser = oData[i]['User'];
        for (var j in nData) {
            var nPlant = nData[j]['Plant'];
            var nMaterialNumber = nData[j]['MaterialNumber'];
            var nMainVendor = nData[j]['MainVendor'];
            var nFirstColumnName = nData[j]['FirstColumnName'];
            var nPast = nData[j]['Past'];
            var nDAY1 = nData[j]['DAY1'];
            var nDAY2 = nData[j]['DAY2'];
            var nDAY3 = nData[j]['DAY3'];
            var nDAY4 = nData[j]['DAY4'];
            var nDAY5 = nData[j]['DAY5'];
            var nDAY6 = nData[j]['DAY6'];
            var nDAY7 = nData[j]['DAY7'];
            var nDAY8 = nData[j]['DAY8'];
            var nDAY9 = nData[j]['DAY9'];
            var nDAY10 = nData[j]['DAY10'];
            var nDAY11 = nData[j]['DAY11'];
            var nDAY12 = nData[j]['DAY12'];
            var nDAY13 = nData[j]['DAY13'];
            var nDAY14 = nData[j]['DAY14'];
            var nDAY15 = nData[j]['DAY15'];
            var nDAY16 = nData[j]['DAY16'];
            var nDAY17 = nData[j]['DAY17'];
            var nDAY18 = nData[j]['DAY18'];
            var nDAY19 = nData[j]['DAY19'];
            var nDAY20 = nData[j]['DAY20'];
            var nDAY21 = nData[j]['DAY21'];
            var nDAY22 = nData[j]['DAY22'];
            var nDAY23 = nData[j]['DAY23'];
            var nDAY24 = nData[j]['DAY24'];
            var nDAY25 = nData[j]['DAY25'];
            var nDAY26 = nData[j]['DAY26'];
            var nDAY27 = nData[j]['DAY27'];
            var nDAY28 = nData[j]['DAY28'];
            var nDAY29 = nData[j]['DAY29'];
            var nDAY30 = nData[j]['DAY30'];
            var nDAY31 = nData[j]['DAY31'];
            var nDAY32 = nData[j]['DAY32'];
            var nDAY33 = nData[j]['DAY33'];
            var nDAY34 = nData[j]['DAY34'];
            var nDAY35 = nData[j]['DAY35'];
            var nDAY36 = nData[j]['DAY36'];
            var nDAY37 = nData[j]['DAY37'];
            var nDAY38 = nData[j]['DAY38'];
            var nDAY39 = nData[j]['DAY39'];
            var nDAY40 = nData[j]['DAY40'];
            var nDAY41 = nData[j]['DAY41'];
            var nDAY42 = nData[j]['DAY42'];
            var nDAY43 = nData[j]['DAY43'];
            var nDAY44 = nData[j]['DAY44'];
            var nDAY45 = nData[j]['DAY45'];
            var nDAY46 = nData[j]['DAY46'];
            var nDAY47 = nData[j]['DAY47'];
            var nDAY48 = nData[j]['DAY48'];
            var nDAY49 = nData[j]['DAY49'];
            var nDAY50 = nData[j]['DAY50'];
            var nDAY51 = nData[j]['DAY51'];
            var nDAY52 = nData[j]['DAY52'];
            var nDAY53 = nData[j]['DAY53'];
            var nDAY54 = nData[j]['DAY54'];
            var nDAY55 = nData[j]['DAY55'];
            var nDAY56 = nData[j]['DAY56'];
            var nDAY57 = nData[j]['DAY57'];
            var nDAY58 = nData[j]['DAY58'];
            var nDAY59 = nData[j]['DAY59'];
            var nDAY60 = nData[j]['DAY60'];
            var nDAY61 = nData[j]['DAY61'];
            var nDAY62 = nData[j]['DAY62'];
            var nDAY63 = nData[j]['DAY63'];
            var nDAY64 = nData[j]['DAY64'];
            var nDAY65 = nData[j]['DAY65'];
            var nDAY66 = nData[j]['DAY66'];
            var nDAY67 = nData[j]['DAY67'];
            var nDAY68 = nData[j]['DAY68'];
            var nDAY69 = nData[j]['DAY69'];
            var nDAY70 = nData[j]['DAY70'];
            var nDAY71 = nData[j]['DAY71'];
            var nDAY72 = nData[j]['DAY72'];
            var nDAY73 = nData[j]['DAY73'];
            var nDAY74 = nData[j]['DAY74'];
            var nDAY75 = nData[j]['DAY75'];
            var nDAY76 = nData[j]['DAY76'];
            var nDAY77 = nData[j]['DAY77'];
            var nDAY78 = nData[j]['DAY78'];
            var nDAY79 = nData[j]['DAY79'];
            var nDAY80 = nData[j]['DAY80'];
            var nDAY81 = nData[j]['DAY81'];
            var nDAY82 = nData[j]['DAY82'];
            var nDAY83 = nData[j]['DAY83'];
            var nDAY84 = nData[j]['DAY84'];
            var nDAY85 = nData[j]['DAY85'];
            var nDAY86 = nData[j]['DAY86'];
            var nDAY87 = nData[j]['DAY87'];
            var nDAY88 = nData[j]['DAY88'];
            var nDAY89 = nData[j]['DAY89'];
            var nDAY90 = nData[j]['DAY90'];
            var nDAY91 = nData[j]['DAY91'];
            var nDAY92 = nData[j]['DAY92'];
            var nDAY93 = nData[j]['DAY93'];
            var nDAY94 = nData[j]['DAY94'];
            var nDAY95 = nData[j]['DAY95'];
            var nDAY96 = nData[j]['DAY96'];
            var nDAY97 = nData[j]['DAY97'];
            var nDAY98 = nData[j]['DAY98'];
            var nDAY99 = nData[j]['DAY99'];
            var nDAY100 = nData[j]['DAY100'];
            var nDAY101 = nData[j]['DAY101'];
            var nDAY102 = nData[j]['DAY102'];
            var nDAY103 = nData[j]['DAY103'];
            var nDAY104 = nData[j]['DAY104'];
            var nDAY105 = nData[j]['DAY105'];
            var nDAY106 = nData[j]['DAY106'];
            var nDAY107 = nData[j]['DAY107'];
            var nDAY108 = nData[j]['DAY108'];
            var nDAY109 = nData[j]['DAY109'];
            var nDAY110 = nData[j]['DAY110'];
            var nDAY111 = nData[j]['DAY111'];
            var nDAY112 = nData[j]['DAY112'];
            var nDAY113 = nData[j]['DAY113'];
            var nDAY114 = nData[j]['DAY114'];
            var nDAY115 = nData[j]['DAY115'];
            var nDAY116 = nData[j]['DAY116'];
            var nDAY117 = nData[j]['DAY117'];
            var nDAY118 = nData[j]['DAY118'];
            var nDAY119 = nData[j]['DAY119'];
            var nDAY120 = nData[j]['DAY120'];
            var nDAY121 = nData[j]['DAY121'];
            var nDAY122 = nData[j]['DAY122'];
            var nDAY123 = nData[j]['DAY123'];
            var nDAY124 = nData[j]['DAY124'];
            var nDAY125 = nData[j]['DAY125'];
            var nDAY126 = nData[j]['DAY126'];
            var nDAY127 = nData[j]['DAY127'];
            var nDAY128 = nData[j]['DAY128'];
            var nDAY129 = nData[j]['DAY129'];
            var nDAY130 = nData[j]['DAY130'];
            var nDAY131 = nData[j]['DAY131'];
            var nDAY132 = nData[j]['DAY132'];
            var nDAY133 = nData[j]['DAY133'];
            var nDAY134 = nData[j]['DAY134'];
            var nDAY135 = nData[j]['DAY135'];
            var nDAY136 = nData[j]['DAY136'];
            var nDAY137 = nData[j]['DAY137'];
            var nDAY138 = nData[j]['DAY138'];
            var nDAY139 = nData[j]['DAY139'];
            var nDAY140 = nData[j]['DAY140'];
            var nDAY141 = nData[j]['DAY141'];
            var nDAY142 = nData[j]['DAY142'];
            var nDAY143 = nData[j]['DAY143'];
            var nDAY144 = nData[j]['DAY144'];
            var nDAY145 = nData[j]['DAY145'];
            var nDAY146 = nData[j]['DAY146'];
            var nDAY147 = nData[j]['DAY147'];
            var nDAY148 = nData[j]['DAY148'];
            var nDAY149 = nData[j]['DAY149'];
            var nDAY150 = nData[j]['DAY150'];
            var nDAY151 = nData[j]['DAY151'];
            var nDAY152 = nData[j]['DAY152'];
            var nDAY153 = nData[j]['DAY153'];
            var nDAY154 = nData[j]['DAY154'];
            var nDAY155 = nData[j]['DAY155'];
            var nDAY156 = nData[j]['DAY156'];
            var nDAY157 = nData[j]['DAY157'];
            var nDAY158 = nData[j]['DAY158'];
            var nDAY159 = nData[j]['DAY159'];
            var nDAY160 = nData[j]['DAY160'];
            var nDAY161 = nData[j]['DAY161'];
            var nDAY162 = nData[j]['DAY162'];
            var nDAY163 = nData[j]['DAY163'];
            var nDAY164 = nData[j]['DAY164'];
            var nDAY165 = nData[j]['DAY165'];
            var nDAY166 = nData[j]['DAY166'];
            var nDAY167 = nData[j]['DAY167'];
            var nDAY168 = nData[j]['DAY168'];
            var nDAY169 = nData[j]['DAY169'];
            var nDAY170 = nData[j]['DAY170'];
            var nDAY171 = nData[j]['DAY171'];
            var nDAY172 = nData[j]['DAY172'];
            var nDAY173 = nData[j]['DAY173'];
            var nDAY174 = nData[j]['DAY174'];
            var nDAY175 = nData[j]['DAY175'];
            var nDAY176 = nData[j]['DAY176'];
            var nDAY177 = nData[j]['DAY177'];
            var nDAY178 = nData[j]['DAY178'];
            var nDAY179 = nData[j]['DAY179'];
            var nDAY180 = nData[j]['DAY180'];
            var nDAY181 = nData[j]['DAY181'];
            var nDAY182 = nData[j]['DAY182'];
            var nDAY183 = nData[j]['DAY183'];
            var nDAY184 = nData[j]['DAY184'];
            var nDAY185 = nData[j]['DAY185'];
            var nDAY186 = nData[j]['DAY186'];
            var nDAY187 = nData[j]['DAY187'];
            var nDAY188 = nData[j]['DAY188'];
            var nDAY189 = nData[j]['DAY189'];
            var nDAY190 = nData[j]['DAY190'];
            var nDAY191 = nData[j]['DAY191'];
            var nDAY192 = nData[j]['DAY192'];
            var nDAY193 = nData[j]['DAY193'];
            var nDAY194 = nData[j]['DAY194'];
            var nDAY195 = nData[j]['DAY195'];
            var nDAY196 = nData[j]['DAY196'];
            var nDAY197 = nData[j]['DAY197'];
            var nDAY198 = nData[j]['DAY198'];
            var nUser = nData[j]['User'];
            if (oPlant == nPlant && oMaterialNumber == nMaterialNumber && oMainVendor == nMainVendor && oFirstColumnName == nFirstColumnName && oUser == nUser) {
                if (
                    oPast != nPast ||
                    oDAY1 != nDAY1 ||
                    oDAY2 != nDAY2 ||
                    oDAY3 != nDAY3 ||
                    oDAY4 != nDAY4 ||
                    oDAY5 != nDAY5 ||
                    oDAY6 != nDAY6 ||
                    oDAY7 != nDAY7 ||
                    oDAY8 != nDAY8 ||
                    oDAY9 != nDAY9 ||
                    oDAY10 != nDAY10 ||
                    oDAY11 != nDAY11 ||
                    oDAY12 != nDAY12 ||
                    oDAY13 != nDAY13 ||
                    oDAY14 != nDAY14 ||
                    oDAY15 != nDAY15 ||
                    oDAY16 != nDAY16 ||
                    oDAY17 != nDAY17 ||
                    oDAY18 != nDAY18 ||
                    oDAY19 != nDAY19 ||
                    oDAY20 != nDAY20 ||
                    oDAY21 != nDAY21 ||
                    oDAY22 != nDAY22 ||
                    oDAY23 != nDAY23 ||
                    oDAY24 != nDAY24 ||
                    oDAY25 != nDAY25 ||
                    oDAY26 != nDAY26 ||
                    oDAY27 != nDAY27 ||
                    oDAY28 != nDAY28 ||
                    oDAY29 != nDAY29 ||
                    oDAY30 != nDAY30 ||
                    oDAY31 != nDAY31 ||
                    oDAY32 != nDAY32 ||
                    oDAY33 != nDAY33 ||
                    oDAY34 != nDAY34 ||
                    oDAY35 != nDAY35 ||
                    oDAY36 != nDAY36 ||
                    oDAY37 != nDAY37 ||
                    oDAY38 != nDAY38 ||
                    oDAY39 != nDAY39 ||
                    oDAY40 != nDAY40 ||
                    oDAY41 != nDAY41 ||
                    oDAY42 != nDAY42 ||
                    oDAY43 != nDAY43 ||
                    oDAY44 != nDAY44 ||
                    oDAY45 != nDAY45 ||
                    oDAY46 != nDAY46 ||
                    oDAY47 != nDAY47 ||
                    oDAY48 != nDAY48 ||
                    oDAY49 != nDAY49 ||
                    oDAY50 != nDAY50 ||
                    oDAY51 != nDAY51 ||
                    oDAY52 != nDAY52 ||
                    oDAY53 != nDAY53 ||
                    oDAY54 != nDAY54 ||
                    oDAY55 != nDAY55 ||
                    oDAY56 != nDAY56 ||
                    oDAY57 != nDAY57 ||
                    oDAY58 != nDAY58 ||
                    oDAY59 != nDAY59 ||
                    oDAY60 != nDAY60 ||
                    oDAY61 != nDAY61 ||
                    oDAY62 != nDAY62 ||
                    oDAY63 != nDAY63 ||
                    oDAY64 != nDAY64 ||
                    oDAY65 != nDAY65 ||
                    oDAY66 != nDAY66 ||
                    oDAY67 != nDAY67 ||
                    oDAY68 != nDAY68 ||
                    oDAY69 != nDAY69 ||
                    oDAY70 != nDAY70 ||
                    oDAY71 != nDAY71 ||
                    oDAY72 != nDAY72 ||
                    oDAY73 != nDAY73 ||
                    oDAY74 != nDAY74 ||
                    oDAY75 != nDAY75 ||
                    oDAY76 != nDAY76 ||
                    oDAY77 != nDAY77 ||
                    oDAY78 != nDAY78 ||
                    oDAY79 != nDAY79 ||
                    oDAY80 != nDAY80 ||
                    oDAY81 != nDAY81 ||
                    oDAY82 != nDAY82 ||
                    oDAY83 != nDAY83 ||
                    oDAY84 != nDAY84 ||
                    oDAY85 != nDAY85 ||
                    oDAY86 != nDAY86 ||
                    oDAY87 != nDAY87 ||
                    oDAY88 != nDAY88 ||
                    oDAY89 != nDAY89 ||
                    oDAY90 != nDAY90 ||
                    oDAY91 != nDAY91 ||
                    oDAY92 != nDAY92 ||
                    oDAY93 != nDAY93 ||
                    oDAY94 != nDAY94 ||
                    oDAY95 != nDAY95 ||
                    oDAY96 != nDAY96 ||
                    oDAY97 != nDAY97 ||
                    oDAY98 != nDAY98 ||
                    oDAY99 != nDAY99 ||
                    oDAY100 != nDAY100 ||
                    oDAY101 != nDAY101 ||
                    oDAY102 != nDAY102 ||
                    oDAY103 != nDAY103 ||
                    oDAY104 != nDAY104 ||
                    oDAY105 != nDAY105 ||
                    oDAY106 != nDAY106 ||
                    oDAY107 != nDAY107 ||
                    oDAY108 != nDAY108 ||
                    oDAY109 != nDAY109 ||
                    oDAY110 != nDAY110 ||
                    oDAY111 != nDAY111 ||
                    oDAY112 != nDAY112 ||
                    oDAY113 != nDAY113 ||
                    oDAY114 != nDAY114 ||
                    oDAY115 != nDAY115 ||
                    oDAY116 != nDAY116 ||
                    oDAY117 != nDAY117 ||
                    oDAY118 != nDAY118 ||
                    oDAY119 != nDAY119 ||
                    oDAY120 != nDAY120 ||
                    oDAY121 != nDAY121 ||
                    oDAY122 != nDAY122 ||
                    oDAY123 != nDAY123 ||
                    oDAY124 != nDAY124 ||
                    oDAY125 != nDAY125 ||
                    oDAY126 != nDAY126 ||
                    oDAY127 != nDAY127 ||
                    oDAY128 != nDAY128 ||
                    oDAY129 != nDAY129 ||
                    oDAY130 != nDAY130 ||
                    oDAY131 != nDAY131 ||
                    oDAY132 != nDAY132 ||
                    oDAY133 != nDAY133 ||
                    oDAY134 != nDAY134 ||
                    oDAY135 != nDAY135 ||
                    oDAY136 != nDAY136 ||
                    oDAY137 != nDAY137 ||
                    oDAY138 != nDAY138 ||
                    oDAY139 != nDAY139 ||
                    oDAY140 != nDAY140 ||
                    oDAY141 != nDAY141 ||
                    oDAY142 != nDAY142 ||
                    oDAY143 != nDAY143 ||
                    oDAY144 != nDAY144 ||
                    oDAY145 != nDAY145 ||
                    oDAY146 != nDAY146 ||
                    oDAY147 != nDAY147 ||
                    oDAY148 != nDAY148 ||
                    oDAY149 != nDAY149 ||
                    oDAY150 != nDAY150 ||
                    oDAY151 != nDAY151 ||
                    oDAY152 != nDAY152 ||
                    oDAY153 != nDAY153 ||
                    oDAY154 != nDAY154 ||
                    oDAY155 != nDAY155 ||
                    oDAY156 != nDAY156 ||
                    oDAY157 != nDAY157 ||
                    oDAY158 != nDAY158 ||
                    oDAY159 != nDAY159 ||
                    oDAY160 != nDAY160 ||
                    oDAY161 != nDAY161 ||
                    oDAY162 != nDAY162 ||
                    oDAY163 != nDAY163 ||
                    oDAY164 != nDAY164 ||
                    oDAY165 != nDAY165 ||
                    oDAY166 != nDAY166 ||
                    oDAY167 != nDAY167 ||
                    oDAY168 != nDAY168 ||
                    oDAY169 != nDAY169 ||
                    oDAY170 != nDAY170 ||
                    oDAY171 != nDAY171 ||
                    oDAY172 != nDAY172 ||
                    oDAY173 != nDAY173 ||
                    oDAY174 != nDAY174 ||
                    oDAY175 != nDAY175 ||
                    oDAY176 != nDAY176 ||
                    oDAY177 != nDAY177 ||
                    oDAY178 != nDAY178 ||
                    oDAY179 != nDAY179 ||
                    oDAY180 != nDAY180 ||
                    oDAY181 != nDAY181 ||
                    oDAY182 != nDAY182 ||
                    oDAY183 != nDAY183 ||
                    oDAY184 != nDAY184 ||
                    oDAY185 != nDAY185 ||
                    oDAY186 != nDAY186 ||
                    oDAY187 != nDAY187 ||
                    oDAY188 != nDAY188 ||
                    oDAY189 != nDAY189 ||
                    oDAY190 != nDAY190 ||
                    oDAY191 != nDAY191 ||
                    oDAY192 != nDAY192 ||
                    oDAY193 != nDAY193 ||
                    oDAY194 != nDAY194 ||
                    oDAY195 != nDAY195 ||
                    oDAY196 != nDAY196 ||
                    oDAY197 != nDAY197 ||
                    oDAY198 != nDAY198) {
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
 * This function will use the exceljs.min.js
 * this is the library which will be use
 * for reading the excel file
 * in this function it will convert the file
 * into a JSON object
 * then draw the data to table
 */
function ReadExcelFile(callback) {
    var selectedFile = $("#fileUpload")[0].files[0];
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

function validate(element, plant) {
    return element;
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
        var vendor = $.map($("#selectVendor option:selected"), function (e) {
            return $(e).val();
        });
        vendor = vendor.toString().replace(/,/g, "/");
        if (vendor === "") {
            vendor = AssignedVendor === 'ALL' ? Vendors : AssignedVendor;
        }
        GetPartsCodeByPlantandVendors(plant, vendor, function (d) {
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

        if ($("#selectVendor").val().length !== 0) {
            if ($("#selectVendor").val().length === 1) {
                vendor = $("#selectVendor option:selected").val();
            }
            else {
                vendor = $.map($("#selectVendor option:selected"), function (e) {
                    return $(e).val();
                });
                vendor = vendor.toString().replace(/,/g, "/");
            }
        }
        else {
            vendor = AssignedVendor !== 'ALL' ? Vendors : AssignedVendor;
        }

        if ($("#selectPartsCode").val().length !== 0) {
            if ($("#selectPartsCode").val().length === 1) {
                parts = $("#selectPartsCode option:selected").val();
            }
            else {
                parts = $.map($("#selectPartsCode option:selected"), function (e) {
                    return $(e).val();
                });
                parts = parts.toString().replace(/,/g, "/");
            }
        }
        else {
            parts = "";
        }

        GetPartsSimulationAssemblyPartsByPlant(plant, parts, vendor);
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
            ReadExcelFile(function (data) {
                $('#fileUpload').val('');
                DTForExport = data;
                var arrPlant = [];
                for (var i in data) {
                    arrPlant.push(data[i]['PLANT']);
                }
                var isValid = arrPlant.every( (val) => val === plant)
                if (isValid === true) {
                    DrawNewTable(data, function (e) {
                        if (e > 0) {
                            isUploaded = true;
                            ShowButtons();
                            Save();
                        }
                        else {
                            HideButtons();
                        }
                    });
                }
                else {
                    alert('Incorrect plant detected!');
                }
            });
        }
        else {
            alert('Please choose file to upload!');
        }
    });

    $('#btnExport').click(function () {
        Export();
    });

    $('#btnSave').click(function () {
        Save();
    });
});