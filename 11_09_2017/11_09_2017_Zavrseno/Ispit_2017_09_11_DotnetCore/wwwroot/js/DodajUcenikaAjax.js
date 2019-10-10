$(document).ready(function () {
    $("#dodaj").click(function () {
        LoadForm()
    })
    $("#forma").submit(function (e) {
        e.preventDefault()
        Spremi()
    })
    $(".deleteBtn").click(function(){
        $.ajax({
            url: "/ajax/ObrisiOdjeljenjeStavka?odjeljenjeStavkaID=" + $(this).data("id"),
            success: function (result) {
                LoadTable()
            }
        })
    })
    $(".editBtn").click(function () {
        $.ajax({
            url: "/ajax/UrediOdjeljenjeStavka?odjeljenjeStavkaID=" + $(this).data("id"),
            success: function (result) {
                $("#ajaxDiv").html(result)
            }
        })
    })
    function LoadForm() {
        $.ajax({
            url: "/ajax/DodajUcenika?odjeljenjeID=" + $("#odjeljenjeID").val(),
            success: function (result) {
                $("#ajaxDiv").html(result);
            }
        })
    }
    function Spremi() {
        $.ajax({
            url: "/ajax/SpremiUcenika",
            data: $("#forma").serialize(),
            success: function (result) {
               LoadTable()
            }
        })
    }
    function LoadTable() {
        $.ajax({
            url: "/ajax/index?odjeljenjeID=" + $("#odjeljenjeID").val(),
            success: function (result) {
                $("#ajaxDiv").html(result);
            }
        })
    }
    

})