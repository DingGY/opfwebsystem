

function csrfSafeMethod(method) {
    // these HTTP methods do not require CSRF protection
    return (/^(GET|HEAD|OPTIONS|TRACE)$/.test(method));
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


function confirm_passwd(){
    var passwd = $("#set-login-password").val();
    var confirm_passwd = $("#set-repeat-login-password").val();
    if(passwd != confirm_passwd){
        alert("两次输入的密码不一致");
        return false;
    }
    return true;
}

function signupClick(e){  
    if(!confirm_passwd()){
        return
    }
    ajaxPostComm(
        "/ajax/signup/",
        {
            realname:$("#login-realname").val(),
            username:$("#set-login-username").val(),
            password:$("#set-login-password").val(),
            email:$("#set-login-email").val(),
            applypasswd:$("#login-apply-passwd").val(),
        },
        function(data, status){
            if(data =="error applypasswd"){
                alert("请向管理员申请激活密码");
            }else if(data =="not finished"){
                alert("用户名重复或信息填写不完整");
            }else if(data == "signuped"){
                alert("注册成功！");
            }
        }
    );
}

function loginClick()
{
    ajaxPostComm(
        "/ajax/check/",
        {
            username:$("#login-username").val(),
            password:$("#login-password").val(),
        },
        function(data, status){
            if(data =="username error"){
                alert("密码或用户名错误");
            }else if(data =="not active"){
                alert("用户未激活");
            }else if(data == "signined"){
                herf = window.location.protocol +"//" + window.location.host+"/bug_manage/index/";
                $(window).attr('location',herf);
            }
        }
    );
}

function bug_login_init() {
    $("#set-repeat-login-password").bind("change", confirm_passwd);
    $("#signup-submit").click(signupClick);
    $("#login-submit").click(loginClick);
}

