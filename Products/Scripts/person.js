$(function () {
    $('#BtnCreateUser').on('click', function (e) {
        e.preventDefault();
        $.ajax({
            type: 'POST',
            url: $('#Users').data('create'),
            success: function (view) {
                var $ModalUserManage = $('#ModalUserManage');
                $ModalUserManage.find('.modal-body').html(view);
                $ModalUserManage.find('.modal-title').text('User Creation');
                $ModalUserManage.modal();
                $.validator.unobtrusive.parse('#FormCreateUser');
                $('#DeleteUser').addClass('hide');
                $('#SaveOrUdpateUser').removeClass('hide');
            },
            error: function (xhr) {
                onFailure('Bad error'); console.log(xhr.responseText);
            }
        });
    });

    $(document).on('click', '.editUser', function () {
        $.ajax({
            type: 'GET',
            url: $('#Users').data('edit'),
            data: {
                personid: $(this).data('personid')
            },
            success: function (view) {
                var $ModalUserManage = $('#ModalUserManage');
                $ModalUserManage.find('.modal-body').html(view);
                $ModalUserManage.find('.modal-title').text('User Edition');
                $ModalUserManage.modal();
                $.validator.unobtrusive.parse('#FormEditUser');
                $('#DeleteUser').addClass('hide');
                $('#SaveOrUdpateUser').removeClass('hide'); 
            },
            error: function () { }
        });
    });

    $(document).on('click', '.deleteUser', function () {
        var $ModalUserManage = $('#ModalUserManage');
        $ModalUserManage.find('.modal-body').html('');
        $ModalUserManage.find('.modal-body').append(
            $('<div>', { "class": 'divConfirmDelete text-center', html: 'Are you sure you want to delete the user?', 'data-personid': $(this).data('personid') })
        );
        $ModalUserManage.find('.modal-title').text('User Delete');
        $ModalUserManage.modal();
        $('#DeleteUser').removeClass('hide');
        $('#SaveOrUdpateUser').addClass('hide');
    });

    $('#SaveOrUdpateUser').on('click', function () {
        var $ModalUserManage = $('#ModalUserManage');
        var $form = $ModalUserManage.find('.modal-body').find('form');
        if ($form.length <= 0) {
            $ModalUserManage.modal('hide');
            return;
        }
        saveOrUpdateUser($form.attr('id'));
    });

    $(document).on('click', '#DeleteUser', function () {
        $.ajax({
            type: 'GET',
            url: $('#Users').data('delete'),
            data: {
                Id: $('.divConfirmDelete').data('personid')
            },
            success: function (userEliminatedId) {
                if (userEliminatedId == '') {
                    onFailure('The user was not eliminated');
                    return;
                }
                if (userEliminatedId == '-1') {
                    onFailure('The user cannot be deleted because it is associated with other records');
                    return;
                }
                if (userEliminatedId == '-2') {
                    onFailure('You cannot delete your own user');
                    return;
                }
                $('#TableUsers tbody').find('button[data-personid="' + userEliminatedId + '"]').parents('tr').remove();
                onSuccess('User eliminated');
                $('#ModalUserManage').modal('hide');
            },
            error: function () { }
        });
    });

    $(document).on('change', '#UserName', function () {
        $.ajax({
            type: 'GET',
            url: $('#ModalUserManage').find('.modal-body').find('form').data('usernameverification'),
            data: {
                UserName: $(this).val()
            },
            success: function (result) {
                if (result == 'True') {
                    onInfo('Username already exist. Please enter another', true);
                    $('#SaveOrUdpateUser').addClass('disabled');
                }
                else {
                    $('#SaveOrUdpateUser').removeClass('disabled');
                }
            },
            error: function (xhr) {
                onFailure('Bad error'); console.log(xhr.responseText);
            }
        });
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
                    var $TableUsers = $('#TableUsers').find('tbody');
                    var name = $('#Name').val();
                    var lastName = $('#LastName').val();
                    var dateBirth = $('#DateBirth').val();
                    var dateBirthObject = new Date(dateBirth);
                    var age = new Date().getFullYear() - dateBirthObject.getFullYear();
                    var gender = $('#Gender option:selected').text();
                    var nationality = $('#NationalityId option:selected').text();
                    var email = $('#Email').val();
                    var role = $('#RoleId option:selected').text();
                    var userName = $('#UserName').val();

                    if (successMessage.indexOf('edit') < 0) {
                        $TableUsers.append(
                            $('<tr>', { "class": 'text-left' }).append(
                                $('<td>', { "class": 'name', html: name }),
                                $('<td>', { "class": 'lastName', html: lastName }),
                                $('<td>', { "class": 'dateBirth', html: dateBirth }),
                                $('<td>', { "class": 'age', html: age }),
                                $('<td>', { "class": 'gender', html: gender }),
                                $('<td>', { "class": 'nationality', html: nationality }),
                                $('<td>', { "class": 'email', html: email }),
                                $('<td>', { "class": 'role', html: role }),
                                $('<td>', { "class": 'userName', html: userName }),
                                $('<td>').append(
                                    $('<button>', { "class": 'btn btn-info btn-sm editUser', text: 'Edit', 'data-personid': personid }),
                                    $('<span> </span>'),
                                    $('<button>', { "class": 'btn btn-danger btn-sm deleteUser', text: 'Delete', 'data-personid': personid }),
                                )
                            )
                        );
                    } else {
                        var $TableUsersToEdit = $TableUsers.find('button[data-personid="' + personid + '"]').parents('tr');
                        $TableUsersToEdit.find('.name').html(name);
                        $TableUsersToEdit.find('.lastName').html(lastName);
                        $TableUsersToEdit.find('.dateBirth').html(dateBirth);
                        $TableUsersToEdit.find('.age').html(age);
                        $TableUsersToEdit.find('.gender').html(gender);
                        $TableUsersToEdit.find('.nationality').html(nationality);
                        $TableUsersToEdit.find('.email').html(email);
                        $TableUsersToEdit.find('.role').html(role);
                        $TableUsersToEdit.find('.userName').html(userName);
                    }
                    $('#ModalUserManage').modal('hide');
                    onSuccess(successMessage, true);
                }
                else {
                    onFailure(errorMessage);
                    return;
                }
            },
            error: function () { onFailure(errorMessage); }
        });
    }
}