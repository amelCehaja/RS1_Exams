$(document).ready(function () {
    $(".ajaxLink").click(function (e) {
        e.preventDefault()
        var url = $(this).attr("href")
        $.ajax({
            url: url,
            success: function (result) {
                $("#ajaxDiv").html(result)
            }
        })
    })
})