$(document).ready(function () {
    $("#forma").submit(function (e) {
        e.preventDefault()
        $.ajax({
            url: "/ajax/spremi",
            data: $("#forma").serialize(),
            success: function (result) {
                $.ajax({
                    url: "/ajax/Index?oznacenDogadajID=" + $("#id").val(),
                    success: function (result) {
                        $("#ajaxDiv").html(result)
                    }
                })
            }
        })
    })
    $(".editBtn").click(function () {
        $.ajax({
            url: "/ajax/Uredi?stanjeObavezaID=" + $(this).data("id"),
            success: function (result) {
                $("#ajaxDiv").html(result)
            }
        })
    })
})