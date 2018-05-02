$(function () {
    var selector = '@ViewBag.ActiveTab';
    if (selector) {
        $("#link-tab" + selector).tab('show');
    }
});