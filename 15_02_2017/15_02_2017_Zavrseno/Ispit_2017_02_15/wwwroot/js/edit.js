$(document).ready(function () {
    LoadTable();
   
})
function LoadTable() {
    $.ajax({
        url: "/ajax/prikazucenika?id=" + $('#ajaxID').val(),
        success: function (result) {
            $('#ajaxDiv').html(result);
        }
    });
}