
$(function () {

    function DisplayErrors(errors, $target) {
        $target.show();
        $target.html('');
        for (var i = 0; i < errors.length; i++) {
            $target.append('<p>' + errors[i].ErrorMessage + '</p>')
        }
    }

    function MoveShips() {
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
                UpdateControlPanel();
            },
            error: function (response) {
                DisplayErrors(response.responseJSON.errors, $errorMessages);
            }
        });
    }

    function UpdateControlPanel() {
        var $controlPanel = $('#ships-control-panel');
        $controlPanel.addClass('loading');
        $.ajax({
            type: 'GET',
            url: '/ShipTracking/RenderControlPanel',
            success: function (response) {
                $controlPanel.removeClass('loading');
                $controlPanel.html(response);
            }
        });
    }

    $(document).on('click', '#btn-move-ships', function () {
        MoveShips();
    });
});