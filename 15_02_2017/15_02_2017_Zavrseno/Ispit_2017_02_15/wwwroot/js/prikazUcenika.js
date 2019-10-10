$(document).ready(function () {
    $(".prisutan").on("click", function () { Odsutan($(this).data("id")); });
    $(".odsutan").click(function () { Prisutan($(this).data("id")); });
    $(".uredi").click(function () { Uredi($(this).data("id")); });
    function Odsutan(id) {
        $.ajax({
            url: "/ajax/ucenikjeodsutan?id=" + id,
            success: function (result) {
                LoadTable();
            }
        });
    }
    function Prisutan(id) {
        $.ajax({
            url: "/ajax/ucenikjeprisutan?id=" + id,
            success: function (result) {
                LoadTable();
            }
        })
    }
    function Uredi(id) {
        $.ajax({
            url: "/ajax/uredi?id=" + id,
            success: function (result) {
                $('#ajaxDiv').html(result);
            }
        })
    }
});
