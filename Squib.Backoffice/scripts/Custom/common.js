function isBlank(str) {
    return (!str || /^\s*$/.test(str));
}

function AlertWarning(message) {
    toastr.warning(message);
}

function AlertError(message) {
    toastr.error(message);
}

function AlertSuccess(message) {
    toastr.success(message);
}

function ToJavaScriptDate(value) {
  var pattern = /Date\(([^)]+)\)/;
  var results = pattern.exec(value);
  var dt = new Date(parseFloat(results[1]));
  return dt;
}

function pad0(str, len) {
    var zeros = "00000000000000000000000000";
    if (str.length < len) {
        return zeros.substr(0, len - str.length) + str;
    }

    return str;
}


function ObjectIdToString(mongoId) {
    var result =
        pad0(mongoId.Timestamp.toString(16), 8) +
        pad0(mongoId.Machine.toString(16), 6) +
        pad0(mongoId.Pid.toString(16), 4) +
        pad0(mongoId.Increment.toString(16), 6);

    return result;
}