                                         
function onSuccess(message, permanent) {
    if (permanent) {
        $.ambiance({
            message: message,
            title: 'Success!',
            type: 'success',
            timeout: 0
        });
    }
    else {
        $.ambiance({
            message: message,
            title: 'Success!',
            type: 'success',
            fade: true
        });
    }
}

function onFailure(message, permanent) {
    if (permanent) {
        $.ambiance({
            message: message,
            title: 'Error',
            type: 'error',
            timeout: 0
        });
    }
    else {
        $.ambiance({
            message: message,
            title: 'Error',
            type: 'error',
            fade: true,
            timeout: 5
        });
    }
}

function onInfo(message, permanent) {
    if (permanent) {
        $.ambiance({
            message: message,
            title: 'Info',
            type: 'info',
            timeout: 0
        });
    }
    else {
        $.ambiance({
            message: message,
            title: 'Info',
            type: 'info',
            fade: true,
            timeout: 5
        });
    }
}