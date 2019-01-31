basket = {};
basket.form = basket.form || {};
basket.form.debugMode = false;

basket.isFirstLoad = function (namespace) {
    var isFirst = namespace.firstLoad === undefined;
    namespace.firstLoad = false;
    if (!isFirst) {
        console.log("File already loaded: ignoring");
    }
    return isFirst;
};

$(document).ready(function () {
    if (!basket.isFirstLoad(basket.form)) {
        return;
    }
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
    function selectAll() {
        var form = event.target.form;
        var available = $(form).find(".data-available");
        var selected = $(form).find(".data-selected");
        $(available).children("option").prop("selected", false);
        $(selected).children("option").prop("selected", true);
    }
    function updateAll() {
        var forms = $(".fruitForm");
        var filteredForms = forms.filter(function () {
            if (this === basket.postForm) {
                console.log("same thing, discard");
                return false;
            } else {
                console.log("not same thing, keep and update");
                return true;
            }
        });
        filteredForms.each(function () {
            var avail = basket.postForm.available;
            var select = basket.postForm.selected;

            var availOpts = $(avail).find("option");
            var selectOpts = $(select).find("option");

            var a = $(this).find(".data-available");
            var s = $(this).find(".data-selected");
            $(a).find("option").remove();
            $(s).find("option").remove();

            $(availOpts).clone().appendTo(a);
            $(selectOpts).clone().appendTo(s);
            

        });
    }
    $(".data-submit").click(function (event) {
        selectAll();
        basket.postForm = event.target.form;
        var myData = $(event.target.form).serialize();
        $.ajax({
            url: "/api/Fruits/Post",
            type: "POST",
            data: myData,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function () {
                updateAll();
            },
            error: function () { alert("error!)") }
        });
    });
});