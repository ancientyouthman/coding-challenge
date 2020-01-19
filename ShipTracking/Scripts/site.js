
$(function () {

    function DisplayErrors(errors, $target) {
        $target.show();
        $target.html();
        for (var i = 0; i < errors.length; i++) {
            $target.append('<p>' + errors[i].ErrorMessage  + '</p>')
        }
    }

    $('#btn-move-ships').on('click', function (e) {
        e.preventDefault();
        var $form = $('#form-move-ships');
        var $grid = $('#ship-tracking-grid');
        var $errorMessages = $('#move-ships-errors');
        $errorMessages.hide();
        $grid.addClass('loading');
        var formData = $form.serialize();
        $.ajax({
            type: 'POST',
            url: '/ShipTracking/MoveShips',
            data: formData,
            success: function (response) {
                $grid.removeClass('loading');
                $grid.html(response);
            },
            error: function (response) {
                DisplayErrors(response.responseJSON.errors, $errorMessages);
                }
        });
    });
});