$(function () {
    $('#BtnRegister').on('click', function (e) {
        e.preventDefault();
        var $form = $('#FormRegister');
        if ($form.valid()) {
            $.ajax({
                type: 'POST',
                url: $form.attr('action'),
                data: $form.serialize(),
                success: function (result) {
                    if (result == '')
                        onFailure('The document already registered', true);
                    else if (result == '0')
                        onInfo('The username already registered', true);
                    else
                        onSuccess('You are registered!', true);
                },
                error: function (xhr) {
                    onFailure('Bad error'); console.log(xhr.responseText);
                }
            });
        }
    });

    $(document).on('change', '#UserName', function () {
        $.ajax({
            type: 'GET',
            url: $('#FormRegister').data('usernameverification'),
            data: {
                UserName: $(this).val()
            },
            success: function (result) {
                if (result == 'True') {
                    onInfo('Username already exist. Please enter another', true);
                    $('#BtnRegister').addClass('disabled');
                }
                else {
                    $('#BtnRegister').removeClass('disabled');
                }
            },
            error: function (xhr) {
                onFailure('Bad error'); console.log(xhr.responseText);
            }
        });
    });

    $(document).on('change', '#IdentificacionNueva, #TipoDeIdentificacionNuevaId', function () {
        if ($('#IdentificacionNueva').val() == $('#Identificacion').val() && $('#TipoDeIdentificacionNuevaId').val() == $('#TipoDeIdentificacionId').val()) {
            onInfo('La identificación anterior y la nueva son iguales. Por favor, ingrese una nueva');
            $('#BtnGuardarCambioDeDocumentoDePersona').addClass('hide');
        }
        else {
            $('#BtnGuardarCambioDeDocumentoDePersona').removeClass('hide');
        }
    });
});