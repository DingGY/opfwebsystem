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
function delStepForTask() {
    task_id = $.cookie()['task'];
    var num = $(this).parents(".bg-warning").prev().find("#task-step-list-num").text();
    ajaxPostComm(
        "/ajax/task/delstep/",
        {
            id: task_id,
            step_num: num,
        },
        function (data, status) {
            if (data == "deleted") {
                delete_step_list_view(num);
            }
        }
    );
}
function changeStepForTask() {
    task_id = $.cookie()['task'];
    var num_input = $(this).parents(".input-group").find("#input-change-num").val();
    var num = $(this).parents(".bg-warning").prev().find("#task-step-list-num").text();
    ajaxPostComm(
        "/ajax/task/changestep/",
        {
            id: task_id,
            step_num: num,
            step_change_num: num_input,
        },
        function (data, status) {
            if (data != "not found") {
                flash_step_list_view(true);
                var recv = JSON.parse(data);
                for (var i in recv) {
                    add_step_list_item(
                        recv[i]['step_head'],
                        recv[i]['step_text'],
                        recv[i]['step_num']);
                }
                flash_step_list_view(false);
            }
        }
    );
}
var StepDic = {};
function add_step_list_item(head, text, num) {
    var step_item =
        '<div class="panel-heading " role="tab" id="step-show-heading-' + num + '">' +
        '<h4 class="panel-title ">' +
        '<a  role="button" data-toggle="collapse" data-parent="#accordion" href="#step-show-list-' + num + '"  aria-expanded="true">' +
        '<span class="glyphicon " aria-hidden="true"><font size=12 id="task-step-list-num">' + num + '</font>  ' + head + '</span>' +
        '</a>' +
        '</h4>' +
        '</div>' +
        '<div id="step-show-list-' + num + '" class="bg-warning panel-collapse collapse" role="tabpanel">' +
        '<div class="panel-body">' + text +
        '<div class="raw">' +
        '<div class="col-xs-5 col-xs-offset-7">' +
        '<div class="input-group">' +
        '<span class="input-group-btn">' +
        '<button class="btn btn-danger" type="button" id="step-num-del-' + num + '">删除</button>' +
        '</span>' +
        '<span class="input-group-btn">' +
        '<button class="btn btn-success" type="button" id="step-num-change-' + num + '">修改</button>' +
        '</span>' +
        '<input type="text" id="input-change-num" class="form-control" placeholder="序号">' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>';
    StepDic[num] = step_item;
}

//add the new list for the task
function flash_step_list_view(clearflag) {
    $('#show-task-every-step').find('.panel').children().remove();
    if (StepDic == {}) {
        return;
    }
    var dic = Object.keys(StepDic).sort(function (a, b) {
        return a - b;
    });
    for (var i in dic) {
        $('#show-task-every-step').find('.panel').append(StepDic[dic[i]]);
        $('#step-num-del-' + dic[i]).click(delStepForTask);
        $('#step-num-change-' + dic[i]).click(changeStepForTask);
    }
    if (clearflag == true) {
        StepDic = {};
    }

}
function delete_step_list_view(num) {
    $('#show-task-every-step').find('#step-show-heading-' + num).remove();
    $('#show-task-every-step').find('#step-show-list-' + num).remove();
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
    var addr_useflag = $("#step-address").val();
    if (addr_useflag == "上一步地址") {
        addr_useflag = "False";
    } else {
        addr_useflag = "True";
    }
    var step_info = {
        name: $('#step-name').val(),
        ischange_addr: addr_useflag,
        address: $('#step-address').val(),
        isFE_begin: fe_begin_flag,
        send_delay: $('#set-send-delay').val(),
        read_delay: $('#set-read-delay').val(),
        frame: $('#set-frame-text').val(),
        // func_id: $('#func-id').text(),
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
    $('#set-frame-text').val(data['frame_set']);
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

    for (var i in data['step_info_list']) {
        add_step_list_item(
            data['step_info_list'][i]['step_head'],
            data['step_info_list'][i]['step_text'],
            data['step_info_list'][i]['step_num']);
    }
    flash_step_list_view(false);
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
                flash_step_list_view(true);
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
                flash_step_list_view(true);
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

function getFuncInfo() {
    var data = {
        name: $('#func-input-name').val(),
        func_id: $('#func-input-id').val(),
        msg: $('#func-message-text').val(),
        frame_set: $('#func-frameset-text').val(),

    };
    return data
}


function setFuncInfo(recv) {
    var data = JSON.parse(recv);
    $('#func-input-name').val(data['name']);
    $('#func-input-id').val(data['func_id']);
    $('#func-message-text').text(data['msg']);
    $('#func-frameset-text').text(data['frame_set']);
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
        getFuncInfo(),
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
        getFuncInfo(),
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
    var flash_status = false;
    ajaxPostComm(
        "/ajax/task/addstep/",
        {
            id: task_id,
            num: $('#task-input-num').val(),
            logic_name: $.cookie()['step'],
        },
        function (data, status) {
            if (data == "same num") {
                alert_msg('添加步骤', '序号重复 ' + $('#step-name').val(), 'show-task-msg-text', 'danger');
            } else if(data == "not found") {
                alert_msg('添加步骤', '未找到 ' + $('#step-name').val(), 'show-task-msg-text', 'danger');
            } else {
                var recv = JSON.parse(data);
                alert_msg('添加步骤', '成功添加' + $('#step-name').val(), 'show-task-msg-text', 'success');
                add_step_list_item(recv["step_head"],recv["step_text"],recv["step_num"]);
                flash_step_list_view(false);
            }
        }
    );
}



function addStepFunc() {
    func_id = $.cookie()['func'];
    step_id = $.cookie()['step'];
    ajaxPostComm(
        "/ajax/step/addfunc/",
        {
            logic_id: step_id,
            act_id: func_id,
        },
        function (data, status) {
            func_name = $('#func-input-name').val()
            func_frameset = $('#func-frameset-text').val()
            if (data == "addfunced") {
                $('#func-id').text(func_name);
                $('#set-frame-text').text(func_frameset);
                alert_msg('添加动作', '成功添加' + func_name, 'step-setting-panel', 'success');
            }
            if (data == 'not found') {
                alert_msg('添加动作', '未找到 ' + func_name, 'step-setting-panel', 'danger');
            }
        }
    );
}

function init_addr_select() {
    $("#before-addr").click(function () {
        $("#step-address").val("上一步地址");
        $("#step-address").attr("readonly", "true");
    });
    $("#allaa-addr").click(function () {
        $("#step-address").val("AAAAAAAAAAAA");
        $("#step-address").attr("readonly", "true");
    });
    $("#all11-addr").click(function () {
        $("#step-address").val("111111111111");
        $("#step-address").attr("readonly", "true");
    });
    $("#customer-addr").click(function () {
        $("#step-address").val("");
        $("#step-address").removeAttr("readonly");
    });
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
    $('#add-step-func').click(addStepFunc);
    init_addr_select();

}
