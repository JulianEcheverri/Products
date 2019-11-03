$(function () {
    $('#UpdatePersonInfo').on('click', function () {
        var $AboutPerson = $('#AboutPerson');
        var $form = $AboutPerson.find('form');
        if ($form.length <= 0) return;

        saveOrUpdateUser($form.attr('id'));
    });
});

function saveOrUpdateUser(formId) {
    var $form = $('#' + formId);
    if ($form.valid()) {
        var successMessage = $form.data('success');
        var errorMessage = $form.data('failure');
        $.ajax({
            type: 'POST',
            url: $form.attr('action'),
            data: $form.serialize(),
            success: function (personid) {
                if (personid == '') {
                    onFailure('The document already registered', true);
                    return;
                }
                if (personid == '0') {
                    onInfo('The username already registered', true);
                    return;
                }
                if (personid != null && personid != '') {
                    onSuccess(successMessage, true);
                    return;
                }
                onFailure(errorMessage);
            },
            error: function () { onFailure(errorMessage); }
        });
    }
}