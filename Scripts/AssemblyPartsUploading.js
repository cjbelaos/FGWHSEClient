$('.chosen').chosen({ width: "300px" });
var userid = null;
var DTOrig = null;

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
        url: GlobalURL + "Form/AssemblyPartsUploading.aspx/GetPlantsByUserId",
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
        url: GlobalURL + "Form/AssemblyPartsUploading.aspx/GetVendorsByUserId",
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

function GetUploadedBOMAssemblyByPlant(plant, vendor, callback) {
    $.ajax({
        type: "POST",
        url: GlobalURL + "Form/AssemblyPartsUploading.aspx/GetUploadedBOMAssemblyByPlant",
        data: JSON.stringify({ strPlant: plant, strVendor: vendor }),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $(".overlay").show();
        },
        complete: function () {
            $(".overlay").hide();
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

function FormatData(data, callback) {
    if (data.length > 0) {
        columnNames = Object.keys(data[0]);
        var columns = [];

        for (var i in columnNames) {
            columns.push(columnNames[i]);
        }

        var newData = [];
        var fg_parent = data[0]['Itemcode'];
        var fg_parent_supplier = data[0]['Sub-con Supplier Code']
        for (var i in data) {
            let e = [columns.length];
            for (var j in columns) {
                var colName = columns[j];
                e[j] = data[i][colName];
                if (j == 4) {
                    var level = e[j];
                    if (level != 0) {
                        var parent_index;
                        data.findIndex(function (key, i) {
                            if (key.Level == (level - 1).toString()) {
                                parent_index = i;
                                return true;
                            }
                        });
                        var newRow = {
                            Plant: data[i]['Plant'],
                            FG_Parent: fg_parent,
                            FG_Parent_Supplier: fg_parent_supplier,
                            Parent_Supplier: data[parent_index]['Vendor'],
                            Parent_Level: data[parent_index]['Level'],
                            Assy_Level: data[i]['Assy Level'],
                            Parent_ItemCode: data[parent_index]['Itemcode'],
                            Child_Level: data[i]['Level'],
                            Child_ItemCode: data[i]['Itemcode'],
                            Usage: data[i]['Usage'],
                            Child_SupplierCode: data[i]['Vendor'],
                            Valid_From: data[i]['Valid from'],
                            Valid_To: data[i]['Valid to'],
                            DOS_Level: data[i]['DOS Level'],
                            User: userid
                        };
                        newData.push(newRow);
                    }
                }
            }
        }
    }
    if (callback !== undefined) {
        callback(newData);
    }
}

function DrawTable(data, callback) {
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
                if (['Parent_Level', 'Assy_Level', 'Child_Level', 'Usage', 'Valid_From', 'Valid_To', 'DOS_Level'].includes(colName)) {
                    colType[j] = {type: 'text'};
                }
            }
            d[i] = e;
        }

        var config = {
            data: d,
            colHeaders: columns,
            colWidths: xColWidth,
            columns: colType,
            autoRowSize: true,
            csvHeaders: true,
            tableOverflow: true,
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
        callback(data);
    }
}

function Save(isUploaded) {
    var headers = MyTable.getColHeader();
    var data = MyTable.getSourceData();
    var newData = DataMapping(headers, data);
    var DataForSaving = [];
    for (var i in newData) {
        var newRow = {
            Plant: newData[i]['Plant'],
            FG_Parent: newData[i]['FG_Parent'],
            FG_Parent_Supplier: newData[i]['FG_Parent_Supplier'],
            Parent_Supplier: newData[i]['Parent_Supplier'],
            Parent_Level: newData[i]['Parent_Level'],
            Assy_Level: newData[i]['Assy_Level'],
            Parent_ItemCode: newData[i]['Parent_ItemCode'],
            Child_Level: newData[i]['Child_Level'],
            Child_ItemCode: newData[i]['Child_ItemCode'],
            Usage: newData[i]['Usage'],
            Child_SupplierCode: newData[i]['Child_SupplierCode'],
            Valid_From: newData[i]['Valid_From'],
            Valid_To: newData[i]['Valid_To'],
            DOS_Level: newData[i]['DOS_Level'],
            User: userid
        };
        DataForSaving.push(newRow);
    }
    newData = DataForSaving;
    //console.log("Old Data", DTOrig);
    //console.log("New Data", DataForSaving);
    DataForSaving = CompareData(DTOrig, DataForSaving);
    console.log(DataForSaving);
    if (isUploaded === true) {
        DataForSaving = DataForSaving.length == 0 ? newData : DataForSaving;
    }
    //console.log(DataForSaving);
    if (DataForSaving.length > 0) {
        $.ajax({
            url: GlobalURL + "Form/AssemblyPartsUploading.aspx/SaveData",
            type: "POST",
            data: JSON.stringify({ BA: DataForSaving }),
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

function CompareData(oData, nData) {
    var data = [];
    for (var i in oData) {
        var oPlant = oData[i]['Plant'];
        var oFG_Parent = oData[i]['FG_Parent'];
        var oFG_Parent_Supplier = oData[i]['FG_Parent_Supplier'];
        var oParent_Supplier = oData[i]['Parent_Supplier'];
        var oParent_Level = oData[i]['Parent_Level'];
        var oAssy_Level = oData[i]['Assy_Level'];
        var oParent_ItemCode = oData[i]['Parent_ItemCode'];
        var oChild_Level = oData[i]['Child_Level'];
        var oChild_ItemCode = oData[i]['Child_ItemCode'];
        var oUsage = oData[i]['Usage'];
        var oChild_SupplierCode = oData[i]['Child_SupplierCode'];
        var oValid_From = oData[i]['Valid_From'];
        var oValid_To = oData[i]['Valid_To'];
        var oDOS_Level = oData[i]['DOS_Level'];
        var oUser = oData[i]['User'];
        for (var j in nData) {
            var nPlant = nData[j]['Plant'];
            var nFG_Parent = nData[j]['FG_Parent'];
            var nFG_Parent_Supplier = nData[j]['FG_Parent_Supplier'];
            var nParent_Supplier = nData[j]['Parent_Supplier'];
            var nParent_Level = nData[j]['Parent_Level'];
            var nAssy_Level = nData[j]['Assy_Level'];
            var nParent_ItemCode = nData[j]['Parent_ItemCode'];
            var nChild_Level = nData[j]['Child_Level'];
            var nChild_ItemCode = nData[j]['Child_ItemCode'];
            var nUsage = nData[j]['Usage'];
            var nChild_SupplierCode = nData[j]['Child_SupplierCode'];
            var nValid_From = nData[j]['Valid_From'];
            var nValid_To = nData[j]['Valid_To'];
            var nDOS_Level = nData[j]['DOS_Level'];
            var nUser = nData[j]['User'];
            if (oPlant == nPlant && oFG_Parent == nFG_Parent && oFG_Parent_Supplier == nFG_Parent_Supplier && oParent_Supplier == nParent_Supplier && oParent_ItemCode == nParent_ItemCode && oChild_ItemCode == nChild_ItemCode && oChild_SupplierCode == nChild_SupplierCode && oUser == nUser) {
                if (
                    oParent_Level != nParent_Level ||
                    oAssy_Level != nAssy_Level ||
                    oChild_Level != nChild_Level ||
                    oUsage != nUsage ||
                    oValid_From != nValid_From ||
                    oValid_To != nValid_To ||
                    oDOS_Level != nDOS_Level) {
                    data[data.length] = nData[j];
                    break;
                }
                break;
            }
        }
    }
    return data;
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

function GetRowIndex(value) {
    var index;
    newData.findIndex(function (entry, i) {
        if (entry.Level == value) {
            index = i;
            return true;
        }
    });
}

function ConvertToCalendarDate(date) {

    var year = date.substring(0, 4);
    var month = date.substring(4, 6);
    var day = date.substring(6, 8);

    let newDate = `${month}/${day}/${year}`

    return newDate;
}

$(function () {
    GetSessions(function (e) {
        userid = e["UserID"];
        userName = e["UserName"];
    });

    GetPlantsByUserId(function (e) {
        for (var i in e) {
            $("<option>", {
                value: e[i]["PlantCode"],
                text: e[i]["Plant"]
            }).appendTo($("#selectPlant"));
        }
        $("#selectPlant").trigger("chosen:updated");
        //var plant = $('#selectPlant').val();
        GetVendorsByUserId(function (d) {
            if (d.length === 0) {
                $("#selectVendor").attr("data-placeholder", "No Vendor Options").trigger("chosen:updated");
                $("#selectPartsCode").attr("data-placeholder", "No PartsCode Options").trigger("chosen:updated");
            }
            else {
                userVendor = d[0]['VENDORS'];
                arrVendors = [];
                for (var i in d) {
                    arrVendors.push(d[i]['SupplierCode']); //insert all suppliercode into array
                    $('<option>', {
                        value: d[i]['SupplierCode'],
                        text: d[i]['Supplier']
                    }).appendTo($('#selectVendor'));
                }
                $("#selectVendor").attr("data-placeholder", "Select Vendor Options").trigger("chosen:updated");
                vendors = arrVendors.join('/');
            }
        });
    });
    
    $("#btnFilter").click(function () {
        var plant = $("#selectPlant").val();
        var vendor = $.map($("#selectVendor option:selected"), function (e) {
            return $(e).val();
        });
        vendor = vendor.toString().replace(/,/g, "/");
        if (vendor === '') {
            vendor = userVendor !== 'ALL' ? vendors : userVendor;
        }
        GetUploadedBOMAssemblyByPlant(plant, vendor, function (e) {
            if (e.length > 0) {
                result = e;
                /*
                 *  Renaming Object Keys
                 */
                result = result.map(function (obj) {
                    obj['FG_Parent'] = obj['FG_PARENT']; // Assign new key
                    obj['FG_Parent_Supplier'] = obj['FG_PARENT_SUPPLIERCODE']; // Assign new key
                    obj['Parent_Supplier'] = obj['PARENT_SUPPLIERCODE']; // Assign new key
                    obj['Parent_Level'] = obj['PARENT_LEVEL']; // Assign new key
                    obj['Assy_Level'] = obj['ASSY_LEVEL']; // Assign new key
                    obj['Parent_ItemCode'] = obj['PARENT_ITEMCODE']; // Assign new key
                    obj['Child_Level'] = obj['CHILD_LEVEL']; // Assign new key
                    obj['Child_ItemCode'] = obj['CHILD_ITEMCODE']; // Assign new key
                    obj['Usage'] = obj['USAGE']; // Assign new key
                    obj['Child_SupplierCode'] = obj['CHILD_SUPPLIERCODE']; // Assign new key
                    obj['Valid_From'] = obj['VALIDFROM']; // Assign new key
                    obj['Valid_To'] = obj['VALIDTO']; // Assign new key
                    obj['DOS_Level'] = obj['DOSLEVEL']; // Assign new key
                    obj['Created_By'] = obj['CREATEDBY']; // Assign new key
                    obj['Created_Date'] = obj['CREATEDDATE']; // Assign new key
                    obj['Updated_By'] = obj['UPDATEDBY']; // Assign new key
                    obj['Updated_Date'] = obj['UPDATEDDATE']; // Assign new key
                    delete obj['FG_PARENT']; // Delete old key
                    delete obj['FG_PARENT_SUPPLIERCODE']; // Delete old key
                    delete obj['PARENT_SUPPLIERCODE']; // Delete old key
                    delete obj['PARENT_LEVEL']; // Delete old key
                    delete obj['ASSY_LEVEL']; // Delete old key
                    delete obj['PARENT_ITEMCODE']; // Delete old key
                    delete obj['CHILD_LEVEL']; // Delete old key
                    delete obj['CHILD_ITEMCODE']; // Delete old key
                    delete obj['USAGE']; // Delete old key
                    delete obj['CHILD_SUPPLIERCODE']; // Delete old key
                    delete obj['VALIDFROM']; // Delete old key
                    delete obj['VALIDTO']; // Delete old key
                    delete obj['DOSLEVEL']; // Delete old key
                    delete obj['CREATEDBY']; // Delete old key
                    delete obj['CREATEDDATE']; // Delete old key
                    delete obj['UPDATEDBY']; // Delete old key
                    delete obj['UPDATEDDATE']; // Delete old key
                    return obj;
                });

                DTOrig = result;
                for (var i = 0; i < result.length; i++) {
                    result[i]['Valid_From'] = moment(result[i]['Valid_From'].toString()).format('MM/DD/YYYY');
                    result[i]['Valid_To'] = moment(result[i]['Valid_To'].toString()).format('MM/DD/YYYY');
                    result[i]['Created_Date'] = moment(result[i]['Created_Date'].toString()).format('MM/DD/YYYY');
                    result[i]['Updated_Date'] = moment(result[i]['Updated_Date'].toString()).format('MM/DD/YYYY');
                }
                DrawTable(result, function (data) {
                    if (data.length > 0) {
                        $('#btnSave').css('display', 'inline-block');
                    }
                    else {
                        $('#btnSave').css('display', 'none');
                    }
                });
                var newData = [];
                for (var i in result) {
                    var newRow = {
                        Plant: result[i]['Plant'],
                        FG_Parent: result[i]['FG_Parent'],
                        FG_Parent_Supplier: result[i]['FG_Parent_Supplier'],
                        Parent_Supplier: result[i]['Parent_Supplier'],
                        Parent_Level: result[i]['Parent_Level'],
                        Assy_Level: result[i]['Assy_Level'],
                        Parent_ItemCode: result[i]['Parent_ItemCode'],
                        Child_Level: result[i]['Child_Level'],
                        Child_ItemCode: result[i]['Child_ItemCode'],
                        Usage: result[i]['Usage'],
                        Child_SupplierCode: result[i]['Child_SupplierCode'],
                        Valid_From: result[i]['Valid_From'],
                        Valid_To: result[i]['Valid_To'],
                        DOS_Level: result[i]['DOS_Level'],
                        User: userid
                    };
                    newData.push(newRow);
                }
                DTOrig = newData;
            }
            else {
                alert('No data has been found!');
            }
        });
    });
    
    $('#btnUpload').click(function () {
        var isUploaded = true;
        if ($("#fileUpload").val() !== '') {
            var formData = new FormData();
            formData.append("file", $("#fileUpload")[0].files[0]);
            ReadExcelFile(function (data) {
                for (var i = 0; i < data.length; i++) {
                    //data[i]['Level'] = data[i]['Level'].replace(/[^a-z0-9\s]/gi, '').replace(/[_\s]/g, '-');
                    data[i]['Valid from'] = moment(data[i]['Valid from'].toString()).format('MM/DD/YYYY');
                    data[i]['Valid to'] = moment(data[i]['Valid to'].toString()).format('MM/DD/YYYY');
                }
                $('#fileUpload').val('');
                var arrPlant = [];
                for (var i in data) {
                    arrPlant.push(data[i]['Plant']);
                }
                var isValid = arrPlant.every((val) => val === arrPlant[0])
                if (isValid === true) {
                    FormatData(data, function (e) {
                        DTOrig = [];
                        DTOrig = e;
                        console.log(DTOrig);
                        result = e.map(o => {
                            let obj = Object.assign({}, o);
                            delete obj['User'];
                            return obj;
                        });
                        DrawTable(result, function (e) {
                            if (e.length > 0) {
                                $('#btnSave').css('display', 'inline-block');
                                Save(isUploaded);
                            }
                            else {
                                $('#btnSave').css('display', 'none');
                            }
                        });
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

    $("#btnDLFormat").click(function () {
        window.open('/TEMPLATE/TEMPLATE_ASSEMBLY_PARTS.xlsx');
    });

    $('#btnSave').click(function () {
        Save(false);
    });
})