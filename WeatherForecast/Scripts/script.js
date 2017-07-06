$(document).ready(function () {
    console.log("ready!");
    $(".dropdown-menu a").on("click", function () {
        $('input[id="Name"]').val(this.text);
        $("#searchButton").click();
    });
});
