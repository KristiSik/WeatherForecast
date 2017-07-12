$(document).ready(function () {
    console.log("ready!");
    $("#drop_menu a").on("click", function () {
        $('input[id="Name"]').val(this.text);
        $("#searchButton").click();
    });

    $(".history_link").on("click", function () {
        var text = $(this).text();
        $('input[id="Name"]').val(text);
        $("#searchButton").click();
    });
});
