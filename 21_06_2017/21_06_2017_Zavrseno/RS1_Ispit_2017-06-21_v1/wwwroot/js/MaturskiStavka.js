$(document).ready(function () {
    $(".editBtn").click(function () {
        $.ajax({
            url: "/ajax/uredi?maturskiStavkaID=" + $(this).data("id"),
            success: function (result) {
                $("#ajaxDiv").html(result)
            }
        })
    })
    $(".forma").submit(function (e) {
        e.preventDefault()
        $.ajax({
            url: "/ajax/spremi2",
            data: $(this).serialize(),
            success: function (result) {
                LoadTable()
            }
        })
    })
    $("#forma").submit(function (e) {
        e.preventDefault()
        $.ajax({
            url: "/ajax/spremi2",
            data: $("#forma").serialize(),
            success: function (result) {
                LoadTable()
            }
        })
    })
    $(".changeBtn").click(function () {
        $.ajax({
            url: "/ajax/promijeniOsloboden?id=" + $(this).data("id"),
            success: function (result) {
                LoadTable()
            }
        })
    })
    function LoadTable() {
        $.ajax({
            url: "/ajax/Index?maturskiID=" + $("#id").val(),
            success: function (result) {
                $("#ajaxDiv").html(result)
            }
        })
    }
})