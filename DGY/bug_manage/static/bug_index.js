
function info_modal(title,body){
    $("#modal-title-text").text(title);
    $("#modal-info-text").text(body);
    $("#set-config-confirm").modal("show");
}
function add_output_text(text){
    var add_text = 
        '<p>' +
        text +
        '</p>';
    $('#output-mesge-scroll').append(add_text);
}
function setConfig(){
    ip = $("#set_ip").val();
    com = $("#set_com").val();
    send_data = {
        "ip":ip,
        "com":com
    };
    $.get("/ajax/set_config/",send_data,function(data,status){
        if(data == "ok"){
            info_modal(
                "设置","设置成功!"
            );
        }
        else{
            info_modal(
                "设置","设置失败!"
            );
        }
    });
}
function init_websocket() {
    var wsUri = "ws://127.0.0.1:8000/websocket/webcomm/";
    if("undefined" != typeof wsHandle){
        wsHandle.close();
    }
    wsHandle = new WebSocket(wsUri);
    wsHandle.onopen = function () {
        // Web Socket is connected, send data using send()
        wsHandle.send("Message to send");
    };
    wsHandle.onmessage = function (evt) {
        
        add_output_text(evt.data);
    };
    wsHandle.onclose = function () {
        // websocket is closed.
        info_modal("WebSocket","关闭");
    };
    wsHandle.onerror = function(evt){
        info_modal("WebSocket","打开失败"+evt);
    };
}
function selectTask(e){
    task_name = $(e.target).parent().find("td:first-child").text();
    send_data ={
        name:task_name
    }

    $.get("/ajax/index/get_task/",send_data,function(data,status){
        if(data != 'not found'){
            var data_parsed = JSON.parse(data);
            $('#task-show-name').text(data_parsed['name']);
            $('#task-show-information').text(data_parsed['msg']);
        }
    });
}
function jobRunning(){
    init_websocket();
}

function bug_index_init() {
    $('[data-toggle="tooltip"]').tooltip();
    $("#set-config").click(setConfig);
    $("#start-running").click(jobRunning);
    $('#task-name-table').click(selectTask);
}