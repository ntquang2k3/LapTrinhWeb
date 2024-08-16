//Load data in Table when documents is ready
$(document).ready(function () {
    loadData();
})

//Load Data function
function loadData() {
    $.ajax({
        url: "~/AjaxSanPham/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.MALAP + '</td>';
                html += '<td>' + item.TENLAP + '</td>';
                html += '<td>' + item.MAHANG + '</td>';
                html += '<td>' + item.GIABAN + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.MALAP + ')">Edit</a> |'
                    + ' <a href="#" onclick= "Delete(' + item.MALAP + ')">Delete</a></td>';
                html += '/tr';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}