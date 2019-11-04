$(function () {
    $('.amountInput').on('change', function () {
        var $this = $(this);
        var amount = parseInt($this.val());
        if (amount < 0) {
            $this.val(parseInt($this.parent().data('amount')));
            return;
        }

        var products = {};
        var $tr = $this.parents('.trProduct');
        var productsList = $('#Products').data('productlist');
        products["Id"] = $tr.data('productid');
        products["Amount"] = amount;

        var productInObject = productsList.findIndex(x => x.Id === products.Id);
        if (productInObject !== -1) {
            productsList[productInObject] = products;
        }
        else {
            productsList.push(products);
        }
    });

    $('#UpdateProductAmount').on('click', function () {
        var $ModalKeyVerification = $('#ModalKeyVerification');

        var productlist = $('#Products').data('productlist');
        if (productlist.length <= 0) {
            onInfo('You must modify some value');
            return;
        }
        $.ajax({
            type: 'POST',
            url: $ModalKeyVerification.data('url'),
            success: function (view) {
                $ModalKeyVerification.find('.modal-title').text('Verification');
                $ModalKeyVerification.find('.modal-body').html(view);
                $ModalKeyVerification.modal();
                $.validator.unobtrusive.parse('#FormVerificationKeyValue');
            },
            error: function () { onFailure(errorMessage); }
        });
    });

    $('#Verify').on('click', function () {
        var $ModalKeyVerification = $('#ModalKeyVerification');
        var $form = $ModalKeyVerification.find('.modal-body').find('form');
        if ($form.length <= 0) {
            $ModalKeyVerification.modal('hide');
            return;
        }
        var productlist = $('#Products').data('productlist');
        if (productlist.length <= 0) {
            onInfo('You must modify some value');
            return;
        }
        if ($form.valid()) {
            if (keyVerification($form) == 'False') {
                onFailure('Incorrect verification data');
                return;
            }               
            $.ajax({
                type: 'POST',
                url: $('#Products').data('updateamount'),
                data: {
                    Products: productlist
                },
                success: function (result) {
                    $('#Products').data('productlist', []);
                    if (result == 'True') {
                        onSuccess('Amount modified correctly');
                        $ModalKeyVerification.modal('hide');
                        return;
                    }
                    onFailure('Could not change amount');
                },
                error: function () { onFailure(errorMessage); }
            });
        }
    });

    $('.reserveProduct').on('click', function () {
        var $this = $(this);
        $.ajax({
            type: 'POST',
            url: $('#Products').data('reserve'),
            data: {
                ProductId: $this.data('productid'),
            },
            success: function (productAmount) {
                if (productAmount == '') {
                    onFailure('Could not make reservation');
                    return;
                }
                if (productAmount == '-1') {
                    onFailure('There are no products');
                    return;
                }
                if (productAmount == '-2') {
                    onFailure('You have already booked this product');
                    return;
                }
                var $td = $this.parent();
                $td.siblings('.amount').html(productAmount);
                $td.html('<span>You have already booked this product</span>');
            },
            error: function () { onFailure(errorMessage); }
        });
    });
});


function keyVerification($form) {
    var verification = 'False';
    if ($form.valid()) {
        $.ajax({
            type: 'POST',
            async: false,
            url: $form.attr('action'),
            data: {
                VerificationName: $('#VerificationName').val(),
                Key: $('#Key').val()
            },
            success: function (check) {
                verification = check;
            },
            error: function () { onFailure('Incorrect verification data'); }
        });
        return verification; 
    }
}