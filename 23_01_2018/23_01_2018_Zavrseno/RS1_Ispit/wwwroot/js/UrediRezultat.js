$(document).ready(function () {
    function LoadTable() {
        var id = $("#uputnicaID").val();
        $.ajax({
            url: "/ajax/rezultatiPretrage?uputnicaID=" + id,
            success: function (resutlt) {
                $("#ajaxDiv").html(resutlt)
            }
        })
    }
    $("#spremi").click(function () {       
        $.ajax({
            url: "/ajax/Spremi",
            data: $("#form").serialize(),
            success: function (result) {
                LoadTable()
            }
        })

    })

})