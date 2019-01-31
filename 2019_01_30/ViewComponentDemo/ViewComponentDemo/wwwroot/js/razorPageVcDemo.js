$(document).ready(function () {
    $(".data-add").click(function (event) {
        var form = event.target.form;
        var available = $(form).find(".data-available");
        var selected = $(form).find(".data-selected");
        return !$(available).children(":selected").remove().appendTo(selected);
    });
    $(".data-remove").click(function (event) {
        var form = event.target.form;
        var available = $(form).find(".data-available");
        var selected = $(form).find(".data-selected");
        return !$(selected).children(":selected").remove().appendTo(available);
    });
});