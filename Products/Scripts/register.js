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
                    if (result == '') {
                        onFailure('The document already registered', true);

                    }
                    if (result == '0') {
                        onInfo('The username already registered', true); 
                    }
                    onSuccess('You are registered!', true);
                    setTimeout(function () { window.location.href = $form.data('loginroute'); }, 3000);
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
});