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
function add_bug_item(data) {
    var bug_item =
        '<a href="' + data +
        '" class="list-group-item">' +
        data +
        '</a>';
    return bug_item
}
function alert_msg(title, data, postion, status) {
    $('#myAlert').remove();
    var alert_html =
        '<div id="myAlert" class="alert alert-' + status + '">' +
        //must have &times; or havent clost button
        '<a href="#" class="close" data-dismiss="alert">&times;</a>' +
        '<strong>' + title + ': </strong>' +
        data +
        '</div>';
    $('#' + postion).prepend(alert_html);
}
function getStepInfo() {
    var fe_begin_flag = $("#step-select-fe  option:selected").text();
    if (fe_begin_flag == "不使用") {
        fe_begin_flag = "False";
    } else {
        fe_begin_flag = "True";
    }
    var step_info = {
        name: $('#step-name').val(),
        address: $('#step-address').val(),
        isFE_begin: fe_begin_flag,
        send_delay: $('#set-send-delay').val(),
        read_delay: $('#set-read-delay').val(),
        frame: $('#set-frame-text').val(),
        func_id: $('#func-id').text(),
        display_msg: $('#step-message-text').val(),
        val0: $('#val0').val(),
        val1: $('#val1').val(),
        val2: $('#val2').val(),
        val3: $('#val3').val(),
        val4: $('#val4').val(),
        val5: $('#val5').val(),
        val6: $('#val6').val(),
        val7: $('#val7').val(),
        val8: $('#val8').val(),
        val9: $('#val9').val(),
    };
    return step_info
}
function ajaxPostComm(url, data, callfunc) {
    var csrftoken = $.cookie('csrftoken');
    $.ajaxSetup({
        beforeSend: function (xhr, settings) {
            if (!csrfSafeMethod(settings.type) && !this.crossDomain) {
                xhr.setRequestHeader("X-CSRFToken", csrftoken);
            }
        }
    });
    $.post(
        url,
        data,
        callfunc
    );
}
function addNewBug() {
    ajaxPostComm(
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

function addNewStep() {

    ajaxPostComm(
        "/ajax/add_step/",
        getStepInfo(),
        function (data, status) {
            if (data == "saved") {
                alert_msg('添加', '成功', 'step-setting-panel', 'success');
            } else {
                alert_msg('添加', '失败，名称不对', 'step-setting-panel', 'danger');
            }
        }
    );
}
function setStepInfo(recv) {
    var data = JSON.parse(recv);
    $('#step-name').val(data['name']);
    $('#step-address').val(data['address']);
    if(data['isFE_begin'] == 'True'){
        $("#step-select-fe").find("option[text='不使用']").attr("selected",true); 
    }else{
        $("#step-select-fe").find("option[text='使用4个FE']").attr("selected",true); 
    }
    
    $('#set-send-delay').val(data['send_delay']);
    $('#set-read-delay').val(data['read_delay']);
    $('#set-frame-text').val(data['frame']);
    $('#func-id').text(data['func_id']);
    $('#step-message-text').val(data['display_msg']);
    $('#val0').val(data['val0']);
    $('#val1').val(data['val1']);
    $('#val2').val(data['val2']);
    $('#val3').val(data['val3']);
    $('#val4').val(data['val4']);
    $('#val5').val(data['val5']);
    $('#val6').val(data['val6']);
    $('#val7').val(data['val7']);
    $('#val8').val(data['val8']);
    $('#val9').val(data['val9']);
}
function showStepInfo(e) {
    ajaxPostComm(
        "/ajax/get_step/",
        {
            name: $(e.target).text(),
        },
        function (data, status) {
            if (data != "not found") {
                setStepInfo(data);
            }
        }
    );
    setStepInfo
}
function showBugInfo(e) {

}
function showFuncInfo(e) {

}
function bug_manage_init() {
    $("#add-new-bug-save").click(addNewBug);
    $('#add-new-bug').on('show.bs.modal', function (event) {
        $("#add-new-bug-alert").remove();
    });
    $('#add-new-step-btn').click(addNewStep);
    $('#bugmanage-step-list').click(showStepInfo);
    $('#bugmanage-bug-list').click(showBugInfo);
    $('#bugmanage-func-list').click(showFuncInfo);
}
