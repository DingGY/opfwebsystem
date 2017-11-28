function csrfSafeMethod(method) {
    // these HTTP methods do not require CSRF protection
    return (/^(GET|HEAD|OPTIONS|TRACE)$/.test(method));
}
function err_alert(data) {
    var err_alert =
        '<div class="alert alert-danger" role="alert" id="add-new-bug-alert">' +
        '<span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>' +
        '<span class="sr-only">Error:</span>' +
        data +
        '</div>';
    return err_alert
}
function add_bug_item(data){
    var bug_item = 
    '<a href="' + data +
    '" class="list-group-item">' + 
    data + 
    '</a>';
    return bug_item
    
}
function addNewBug() {
    var csrftoken = $.cookie('csrftoken');
    $.ajaxSetup({
        beforeSend: function (xhr, settings) {
            if (!csrfSafeMethod(settings.type) && !this.crossDomain) {
                xhr.setRequestHeader("X-CSRFToken", csrftoken);
            }
        }
    });
    $.post(
        "/ajax/add_bug/",
        {
            name: $('#bug_name').val(),
            founder: $('#bug_founder').val(),
        },
        function (data, status) {
            if (data == "saved") {
                $('#add-new-bug').modal('hide');
                $('#bugmanage-list-group').append(add_bug_item($('#bug_name').val()));
            }
            if (data == "same name") {
                $("#add-new-bug-lable-name").before(err_alert("Bug名称重复"));
            }
        }
    );

}



function bug_manage_init() {
    $("#add-new-bug-save").click(addNewBug);
    $('#add-new-bug').on('show.bs.modal', function (event) {
        $("#add-new-bug-alert").remove();
    });
}
