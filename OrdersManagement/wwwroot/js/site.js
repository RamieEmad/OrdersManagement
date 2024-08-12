$(document).ready(function () {

    // Show the modal when the delete button is clicked

    $('.delete-button').on('click', function () {
        var id = $(this).data('id');
        $('#deleteModal').modal('show');
        $('#deleteButton').on('click', function () {

            // Call the delete action
            $.ajax({
                type: "POST",
                url: "/Product/Delete/" + id,

                success: function () {

                    // Refresh the page or redirect to another page
                    location.reload();
                }
            });
        });
    });
});