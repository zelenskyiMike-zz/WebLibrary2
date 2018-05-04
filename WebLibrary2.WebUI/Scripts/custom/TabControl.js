$('#myTabs button').click(function (e) {

    $(this).tab('show');
    e.preventDefault();
});

//storing tab in hash value
$('ul.nav-tabs > li > button').on("show.bs.tab", function (e) {
    var id = $(e.target).attr("href").substr(1);
    localStorage.setItem('tabID', id);
});

var elementItem = localStorage.getItem('tabID');
$('#myTabs button').removeClass('active show');
$('.tab-content div').removeClass('active');
$('#myTabs button[href = "#' + elementItem + '"]').addClass('active show');
$('.tab-content div[id = "' + elementItem + '"]').addClass('active');
window.scrollTo(0, 0);
