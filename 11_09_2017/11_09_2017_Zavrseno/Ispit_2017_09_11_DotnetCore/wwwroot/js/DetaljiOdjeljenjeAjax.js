$(document).ready(function () {
    LoadTable();
    function LoadTable() {
        $.ajax({
            url: "/ajax/index?odjeljenjeID=" + $("#odjeljenjeID").val(),
            success: function (result) {
                $("#ajaxDiv").html(result);
            } 
        })
    }
});