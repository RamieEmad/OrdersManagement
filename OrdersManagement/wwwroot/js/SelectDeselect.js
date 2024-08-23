$(document).ready(function () {
    $('#selectAll').click(function () {
        if ($(this).is(':checked')) {
            $('.productCheckbox').prop('checked', true);
        } else {
            $('.productCheckbox').prop('checked', false);
        }
    });

    $('.productCheckbox').change(function () {
        var allChecked = true;
        $('.productCheckbox').each(function () {
            if (!$(this).is(':checked')) {
                allChecked = false;
                return false;
            }
        });

        if (allChecked) {
            $('#selectAll').prop('checked', true);
        } else {
            $('#selectAll').prop('checked', false);
        }
    });


    $('#deleteButton').click(function () {
        debugger;
        console.log('Delete button clicked');
        var checkedProductIds = [];
        $('.productCheckbox:checked').each(function () {
            checkedProductIds.push($(this).val());
        });
        console.log('Selected product IDs:', checkedProductIds);

        if (checkedProductIds.length > 0) {
            $.ajax({
                type: 'POST',
                url: '/Product/DeleteProducts',
                data: { productIds: checkedProductIds },
                success: function () {
                    console.log('Products deleted successfully');
                    location.reload(); // reload the page to reflect the changes
                },
                error: function (xhr, status, error) {
                    console.log('Error deleting products:', error);
                }
            });
        } else {
            console.log('No products selected');
        }
    });
});
