


function toggleActivation(button, url) {
    var productId = $(button).data('product-id');
    var currentState = $(button).text() === 'Active';
    var confirmMessage = currentState ? "Are you sure you want to deactivate this product?" : "Are you sure you want to activate this product?";

    if (confirm(confirmMessage)) {
        // Call the toggle activation action
        $.ajax({
            type: 'GET',
            url: url,
            data: { productId: productId },
            success: function (data) {
                if (data.success) {
                    $(button).text(data.product.isActive ? 'Active' : 'Inactive');
                    $(button).toggleClass('btn-success', data.product.isActive).toggleClass('btn-danger', !data.product.isActive);
                    window.location.href = data.redirectUrl;
                } else {
                    alert("Error: " + data.error);
                }
            },
        });
    }
}

