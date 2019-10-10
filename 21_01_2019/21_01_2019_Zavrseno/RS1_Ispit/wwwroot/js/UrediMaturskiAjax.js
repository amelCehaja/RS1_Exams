$(document).ready(function () {
    $.ajax({
        url: "/ajax/index?id=" + $("#id").val(),
        success: function (result) {
            $("#ajaxDiv").html(result)
        }
    })
})