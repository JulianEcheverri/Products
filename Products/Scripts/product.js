$(function () {
    $('.amount').on('change', function () {
        var $this = $(this);
        var amount = parseInt($this.val());
        if (amount < 0) {
            $this.val(parseInt($this.parent().data('amount')));
            return;
        }
        if (!keyVerification()) return;

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
        $.ajax({
            type: 'POST',
            url: $('#Products').data('updateamount'),
            data: {
                Products: $('#Products').data('productlist')
            },
            success: function (data) {


            },
            error: function () { onFailure(errorMessage); }
        });
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


function keyVerification() {
    $.ajax({
        type: 'POST',
        async: false,
        url: $(this).data('url'),
        data: {
            User: $('#User').val(),
            VerificationCode: $('#VerificationCode').val()
        },
        success: function (check) {
            if (check == 'True') {
                onSuccess('Correct verification');
                return;
            }
            onFailure('Incorrect verification data');
        },
        error: function () { onFailure('Incorrect verification data'); }
    });
}