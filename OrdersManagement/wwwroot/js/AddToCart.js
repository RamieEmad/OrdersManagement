<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>


$(function () {
    $('.add-to-cart-button').on('click', function () {
        var productId = $(this).data('product-id');

        $.ajax({
            url: '/Product/AddToCart',
            type: 'POST',
            data: { id: productId },
            success: function (response) {
                if (response.success) {
                    console.log('Product added successfully');
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert('An error occurred while adding the product to the cart.');
            }
        });
    });
});