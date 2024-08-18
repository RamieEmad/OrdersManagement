
function confirmDelete(uniqueId, isDeleteClicked) {
    var deleteSpan = 'deleteSpan_' + uniqueId;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + uniqueId;

    if (isDeleteClicked) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    } else {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }
}





//$(document).ready(function () {
//    $('.delete-product').click(function (e) {
//        e.preventDefault();
//        var productId = $(this).data('product-id');
//        $('#deleteProductModal').data('productId', productId); // Store product ID in modal data
//        $('#deleteProductModal').modal('show');
//    });

//    $('.delete-product-confirm').click(function () {
//        var productId = $('#deleteProductModal').data('productId');
//        // Handle deletion logic here, similar to previous response
//        $.ajax({
//            // ...
//        });
//        $('#deleteProductModal').modal('hide');
//    });
//});
