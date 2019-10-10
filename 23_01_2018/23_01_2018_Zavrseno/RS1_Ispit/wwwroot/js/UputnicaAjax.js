$(document).ready(function () {
    LoadTable();
    function LoadTable() {
        var id = $("#uputnicaID").val();
        console.log(id);
        $.ajax({
            url: "/ajax/rezultatiPretrage?uputnicaID=" + id,
            success: function (resutlt) {                
                $("#ajaxDiv").html(resutlt)
            }
        })
    }
});