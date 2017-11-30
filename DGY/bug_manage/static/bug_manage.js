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
function add_list_item(data) {
    var bug_item =
        '<a href="#" class="list-group-item">' +
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

function addNewStep() {
    ajaxPostComm(
        "/ajax/step/add/",
        getStepInfo(),
        function (data, status) {
            if (data == "saved") {
                $('#bugmanage-step-list').append((add_list_item($('#step-name').val())));
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
    if (data['isFE_begin'] == 'True') {
        $("#step-select-fe").find("option[text='不使用']").attr("selected", true);
    } else {
        $("#step-select-fe").find("option[text='使用4个FE']").attr("selected", true);
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
        "/ajax/step/get/",
        {
            name: $(e.target).text(),
        },
        function (data, status) {
            if (data != "not found") {
                setStepInfo(data);
            }
        }
    );
}
function changeStep() {
    step_name = $('#step-name').val();
    ajaxPostComm(
        "/ajax/step/change/",
        getStepInfo(),
        function (data, status) {
            if (data == "changed") {
                alert_msg('修改', '成功', 'step-setting-panel', 'success');
            }
            if (data == "not found") {
                alert_msg('修改', '未找到' + step_name, 'step-setting-panel', 'danger');
            }
        }
    );
}
function delStep() {
    step_name = $('#step-name').val();
    ajaxPostComm(
        "/ajax/step/del/",
        {
            name: step_name,
        },
        function (data, status) {
            if (data == "deleted") {
                $('#bugmanage-step-list').find(":contains('" + step_name + "')").remove();
                alert_msg('删除', '成功', 'set-func-panel', 'success');
            }
            if (data == "not found") {
                alert_msg('删除', '未找到' + step_name, 'set-step-panel', 'danger');
            }
        }
    );
}
function setTaskInfo(recv) {
    var data = JSON.parse(recv);
    $('#task-name').text(data['name']);
    $('#task-founder').text(data['founder']);
    $('#task-date').text(data['create_date']);
    $('#task-msg').text(data['msg']);
    $('#task-input-name').val(data['name']);
    $('#task-input-founder').val(data['founder']);
    $('#task-message-text').val(data['msg']);
}

function showTaskInfo(e) {
    ajaxPostComm(
        "/ajax/task/get/",
        {
            name: $(e.target).text(),
        },
        function (data, status) {
            if (data != "not found") {
                setTaskInfo(data);
            }
        }
    );
}
function addNewTask() {
    ajaxPostComm(
        "/ajax/task/add/",
        {
            name: $('#task-input-name').val(),
            founder: $('#task-input-founder').val(),
            msg: $('#task-message-text').val(),
        },
        function (data, status) {
            if (data == "saved") {
                $('#bugmanage-task-list').append((add_list_item($('#task-input-name').val())));
                alert_msg('添加', '成功', 'show-task-msg-text', 'success');
            } else {
                alert_msg('添加', '失败名称重复', 'show-task-msg-text', 'danger');
            }
        }
    );
}

function delTask() {
    task_name = $('#task-input-name').val();
    ajaxPostComm(
        "/ajax/task/del/",
        {
            name: $('#task-input-name').val(),
        },
        function (data, status) {
            if (data == "deleted") {
                $('#bugmanage-task-list').find(":contains('" + task_name + "')").remove();
                alert_msg('删除', '成功', 'show-task-msg-text', 'success');
            }
            if (data == "not found") {
                alert_msg('删除', '未找到' + func_name, 'show-task-msg-text', 'danger');
            }
        }
    );
}
function changeTask() {
    task_name = $('#task-input-name').val();
    ajaxPostComm(
        "/ajax/task/change/",
        {
            name: $('#task-input-name').val(),
            founder: $('#task-input-founder').val(),
            msg: $('#task-message-text').val(),
        },
        function (data, status) {
            if (data == "changed") {
                alert_msg('修改', '成功', 'show-task-msg-text', 'success');
            }
            if (data == "not found") {
                alert_msg('修改', '未找到' + task_name, 'show-task-msg-text', 'danger');
            }
        }
    );
}




function setFuncInfo(recv) {
    var data = JSON.parse(recv);
    $('#func-input-name').val(data['name']);
    $('#func-input-id').val(data['func_id']);
    $('#func-message-text').text(data['msg']);
    $('#func-creat-time').text(data['create_date']);
}
function showFuncInfo(e) {
    ajaxPostComm(
        "/ajax/func/get/",
        {
            name: $(e.target).text(),
        },
        function (recv, status) {
            if (recv != "not found") {
                setFuncInfo(recv);
            }
        }
    );
}
function addNewFunc() {
    ajaxPostComm(
        "/ajax/func/add/",
        {
            name: $('#func-input-name').val(),
            func_id: $('#func-input-id').val(),
            msg: $('#func-message-text').val(),
        },
        function (data, status) {
            if (data == "saved") {
                $('#bugmanage-func-list').append((add_list_item($('#func-input-name').val())));
                alert_msg('添加', '成功', 'set-func-panel', 'success');
            }
            if (data == "not found") {
                alert_msg('添加', '失败名称重复', 'set-func-panel', 'danger');
            }
        }
    );
}

function delFunc() {
    func_name = $('#func-input-name').val();
    ajaxPostComm(
        "/ajax/func/del/",
        {
            name: $('#func-input-name').val(),
        },
        function (data, status) {
            if (data == "deleted") {
                $('#bugmanage-func-list').find(":contains('" + func_name + "')").remove();
                alert_msg('删除', '成功', 'set-func-panel', 'success');
            }
            if (data == "not found") {
                alert_msg('删除', '未找到' + func_name, 'set-func-panel', 'danger');
            }
        }
    );
}
function changeFunc() {
    func_name = $('#func-input-name').val();
    ajaxPostComm(
        "/ajax/func/change/",
        {
            name: func_name,
            func_id: $('#func-input-id').val(),
            msg: $('#func-message-text').val(),
        },
        function (data, status) {
            if (data == "changed") {
                alert_msg('修改', '成功', 'set-func-panel', 'success');
            }
            if (data == "not found") {
                alert_msg('修改', '未找到' + func_name, 'set-func-panel', 'danger');
            }
        }
    );
}
function addTaskLogic() {
    task_id = $.cookie()['task'];
    ajaxPostComm(
        "/ajax/task/addstep/",
        {
            id: task_id,
            num: $('#task-input-num').val(),
            logic_name: $.cookie()['step'],
        },
        function (data, status) {
            if (data == "addsteped") {
                alert_msg('添加步骤', '成功添加' + $('#step-name').val(), 'show-task-msg-text', 'success');
            }
            if (data == "not found") {
                alert_msg('添加步骤', '未找到 ' + $('#step-name').val(), 'show-task-msg-text', 'danger');
            }
        }
    );
}
function bug_manage_init() {
    $("#add-new-func-btn").click(addNewFunc);
    $("#add-new-task-btn").click(addNewTask);
    $('#add-new-step-btn').click(addNewStep);
    $("#change-new-func-btn").click(changeFunc);
    $("#change-new-task-btn").click(changeTask);
    $('#change-new-step-btn').click(changeStep);
    $("#del-new-func-btn").click(delFunc);
    $("#del-new-task-btn").click(delTask);
    $('#del-new-step-btn').click(delStep);
    $('#bugmanage-step-list').click(showStepInfo);
    $('#bugmanage-task-list').click(showTaskInfo);
    $('#bugmanage-func-list').click(showFuncInfo);
    $('#add-task-logic-num').click(addTaskLogic);
}
