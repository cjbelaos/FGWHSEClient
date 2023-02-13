/*
 * Script for other useful functions
 * emon
 * 01/28/2021
 */

/*
 * like the syntax in C#
 * adding day(s) to a particular date
 * today.AddDays(1) [01/01/2021]
 * O: 01/02/2021
 */
Date.prototype.AddDays = function (days) {
    var date = new Date(this.valueOf());
    date.setDate(date.getDate() + days);
    return date;
}
/**
 * like the syntax in C#
 * building the string from the format provided
 * String.format("Hello {0}!","world")
 * O: Hello world!
 */
if (!String.format) {
    String.format = function (format) {
        var args = Array.prototype.slice.call(arguments, 1);
        return format.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
                ? args[number]
                : match
                ;
        });
    };
}
/*
 * By passing a date it will return the date in SQL Format
 * yyyy-MM-dd
 * 2021-01-21
 */
function SQLDateFormat(d) {
    var _d = new Date(Date.parse(d));
    return String.format("{0}-{1}-{2}", _d.getFullYear(), ("0" + (_d.getMonth() + 1)).slice(-2), ("0" + _d.getDate()).slice(-2));
}
/*
 * Returning the date in neutral format
 * MM/dd/yyyy
 * 01/21/2021
 */
function HumanDateFormat(d) {
    var _d = new Date(Date.parse(d));
    return String.format("{1}/{2}/{0}", _d.getFullYear(), ("0" + (_d.getMonth() + 1)).slice(-2), ("0" + _d.getDate()).slice(-2));
}
/*
 * Returning the date in underscore format
 * _MM_dd_yyyy
 * _01_27_2021
 */
function UnderDateFormat(d) {
    var _d = new Date(Date.parse(d));
    return String.format("_{0}_{1}_{2}", ("0" + (_d.getMonth() + 1)).slice(-2), ("0" + _d.getDate()).slice(-2), _d.getFullYear());
}
/*
 * To check the string if it is a valid date
 * 02/02/2021
 */
function IsDate(value) {
    var currVal = value;
    if (currVal == '') return false;
    
    var _isValid = false;
    var _regEx = [];

    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{2,4})$/; //Declare Regex
    var dtArray = currVal.match(rxDatePattern); // is format OK?

    if (dtArray == null)
        return false;

    //Checks for mm/dd/yyyy format.
    dtMonth = dtArray[1];
    dtDay = dtArray[3];
    dtYear = dtArray[5];

    if (dtMonth < 1 || dtMonth > 12)
        return false;
    else if (dtDay < 1 || dtDay > 31)
        return false;
    else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
        return false;
    else if (dtMonth == 2) {
        var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
        if (dtDay > 29 || (dtDay == 29 && !isleap))
            return false;
    }
    return true;

}
/**
 * round the number to its nearest n
 */
function ROUND(x,n) {
    return Number.parseFloat(x).toFixed(n);
}
/**
 * show a modal to act as the message box
 */
function MessageBox(title, message) {
    var modal = $('<div/>', {
        id: 'MessageBox', class: "modal"
    }).appendTo('body');
    $('<h3/>', { text: title }).appendTo(modal);
    $('<div/>', {
        html: message
    }).appendTo(modal);
    modal.modal({
        escapeClose: false,
        clickClose: false,
        show: true
    });
    modal.on('hidden.bs.modal', function () {
        modal.remove();
    });
}

/**
 * Select a range of cells
 * @param  {Object}   sheet - Excel Worksheet
 * @param  {int}   startRow - First Row
 * @param  {int}   startColumn - First Column
 * @param  {int}   endRow - Last Row
 * @param  {int}   endColumn - Last Column
 * @return {Object[]}           - Selected cells
 */
function selectRange(sheet, startRow, startColumn, endRow = -1, endColumn = -1) {
    endRow = endRow == -1 ? startRow : endRow;
    endColumn = endColumn == -1 ? startColumn : endColumn;
    const cells = [];
    for (let y = startRow; y <= endRow; y++) {
        const row = sheet.getRow(y);

        for (let x = startColumn; x <= endColumn; x++) {
            cells.push(row.getCell(x));
        }
    }

    return cells;
};

/**
 * Takes a positive integer and returns the corresponding column name.
 * @param {number} num  The positive integer to convert to a column name.
 * @return {string}  The column name.
 */
function toColumnName(num) {
    for (var ret = '', a = 1, b = 26; (num -= a) >= 0; a = b, b *= 26) {
        ret = String.fromCharCode(parseInt((num % b) / a) + 65) + ret;
    }
    return ret;
}
/**
 * get current session
 */
function GetSession(callback) {
    $.ajax({
        url: GlobalURL + "Classes/SharedService.asmx/GetSession", type: "POST",
        data: '',
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
        },
        error: function (e) {
            console.log(e);
        }
    });
}