
$(function () {

    function DisplayErrors(errors) {
        for (var i = 0; i < errors.length; i++) {
            $('<label for="' + errors[i].Key + '" class="error"></label>')
            .html(errors[i].Value[0]).appendTo($("input#" + errors[i].Key).parent());
        }
    }

    $('#btn-move-ships').on('click', function (e) {
        e.preventDefault();
        var $form = $('#form-move-ships');
        var $grid = $('#ship-tracking-grid');
        var formData = $form.serialize();
        $.ajax({
            type: 'POST',
            url: '/ShipTracking/MoveShips',
            data: formData,
            dataType: 'json',
            success: function (result) {
                $grid.html(result);
            },
            error: function (errors) {
                DisplayErrors(errors);
                }
        });
    });
});