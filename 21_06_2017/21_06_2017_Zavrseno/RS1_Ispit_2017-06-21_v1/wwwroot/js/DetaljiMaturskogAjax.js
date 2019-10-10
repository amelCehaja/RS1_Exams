$(document).ready(function () {
    $.ajax({
        url: "/ajax/Index?maturskiID=" + $("#id").val(),
        success: function (result) {
            $("#ajaxDiv").html(result)
        }
    })
})