var secs = 0;
var timerID = null;
var timerRunning = false;
var delay = 1; //millisec
var first = false;

function InitTimer() {
    if (document.getElementById("txtItemCode").value.length >= 1) {
        if (first <> true) {
            secs = 0;
            timerID = null;
            timerRunning = true;
            first = true;
            StartTimer();
        }
        else {
            timerRunning = true;
            first = true;
            StartTimer();
        }
    }
}
function StartTimer() {
    if (timerRunning) {
        secs++;
        timerID = self.setTimeout("StartTimer()", delay);
    }
    else { StopTimer(); }
}

function StopTimer() {
    timerID = null;
    timerRunning = false;
    if (document.getElementById("txtItemCode").value.length > 1)
    { first = false; }
    else {
        first = false;
        secs = 0;
    }
}

function getbc() {
    if (document.getElementById("hType").value != "barcode") {
        StopTimer();
        if (secs > 100)
        { document.getElementById("hType").value = "keyboard"; }
        else
        { document.getElementById("hType").value = "barcode"; }
        secs = 0;
    }
}

function SendTab(objForm, strField, evtKeyPress) {
    var aKey = evtKeyPress.keyCode ?
        evtKeyPress.keyCode : evtKeyPress.which ?
            evtKeyPress.which : evtKeyPress.charCode;

    if (aKey == 13 || aKey == 9)
    { getbc(); }
}

