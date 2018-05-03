$('#myTabs a').click(function (e) {
    e.preventDefault();
    $(this).tab('show');
});

//storing tab in hash value
$('ul.nav-tabs > li > button').on("show.bs.tab", function (e) {
    var id = $(e.target).attr("href").substr(1);
    window.location.hash = id;
});

var hash = window.location.hash;
$('#myTabs button[href = "' + hash + '"]').tab('show');
localStorage.setItem('tabID', hash);
window.scrollTo(0, 0);