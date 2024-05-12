$(function () {
    $('#datepicker_start').datepicker();
});

$(function () {
    $('#datepicker_end').datepicker();
});

$("#button_add_student").on('click', function () {
    $.ajax({
        async: true,
        data: $('#form').serialize(),
        type: "POST",
        url: '/ActivityCreate/AddStudent',
        success: function (partialView) {
            $('#students_container').html(partialView);
        }
    });
});

$("#button_delete_student").on('click', function () {
    $.ajax({
        async: true,
        data: $('#form').serialize(),
        type: "POST",
        url: '/ActivityCreate/RemoveStudent',
        success: function (partialView) {
            $('#students_container').html(partialView);
        }
    });
});