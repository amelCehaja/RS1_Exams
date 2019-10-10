$(document).ready(function () {
    $.ajax({
        url: "/ajax/Index?id=" + $("#id").val(),
        success: function (result) {
            $("#ajaxDiv").html(result)
        }
    })
})