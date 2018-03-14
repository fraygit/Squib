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