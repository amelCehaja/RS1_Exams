$(document).ready(function () {
    $('#form').submit(function (e) {
        e.preventDefault();
        var form = $(this);
        $.ajax({
            type: 'POST',
            url: '/ajax/saveEdit',
            data: form.serialize(),
            success: function (result) {
                $('#ajaxDiv').html(result);
            }
        })
    })
})