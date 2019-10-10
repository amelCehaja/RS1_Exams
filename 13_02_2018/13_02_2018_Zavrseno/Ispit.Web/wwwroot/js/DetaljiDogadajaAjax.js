$(document).ready(function () {
    $.ajax({
        url: "/ajax/Index?oznacenDogadajID=" + $("#id").val(),
        success: function (result) {
            $("#ajaxDiv").html(result)
        }
    })
})