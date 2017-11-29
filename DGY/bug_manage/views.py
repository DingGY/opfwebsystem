from django.shortcuts import render
from django.http import *
from django.views.decorators.csrf import *
from django.shortcuts import render
from django.utils import timezone
import sys,json
import time
import threading
# add the module path
sys.path.append(r"e:\项目\python\web_project\DGYengine")
from ClientParse import ClientParse
from ActionCell import *
from .models import *
from django.db import models
from django.contrib.auth import logout
from django.shortcuts import render_to_response
from django import template
from dwebsocket.decorators import *
import json
# Static value


# Create your views here.

@require_websocket
def test(request):
    print('come in')
    hand_data = ClientParse(
        str(request.websocket.wait().decode('ascii'))
    )
    if hand_data.client_opt == "CONNECT":
        request.websocket.send(b"ack")
        user_client_list = ClientUser.objects.filter(
            ip4_addr__exact=hand_data.client_ipv4).filter(com_id__exact=hand_data.client_comid)
        if len(user_client_list) == 0:
            user_client = ClientUser.create(hand_data)
            user_client.save()
        else:   
            user_client = user_client_list[0]
        join_serial_cell(
            request,
            user_client.ip4_addr,
            user_client.com_id
        )
        get_cell(user_client.ip4_addr, user_client.com_id).serial_event.wait()
    else:
        return


def login_view(request):
    return render_to_response("bug_login.html")


def login_check(request):
    username = request.GET['username']
    password = request.GET['password']
    resp = HttpResponseRedirect("/bug_manage/index/")
    resp.set_cookie("username", username)
    resp.set_cookie("password", password)
    return resp


@require_websocket
def web_comm(request):
    ip = request.COOKIES["ip"]
    com = request.COOKIES["com"]
    username = request.COOKIES["username"]
    request.websocket.send(b"hello")
    join_web_cell(
        request,
        username,
        ip,
        com
    )
    # add action here
    get_cell(ip, com).web_event.wait()
    return


def boot(request):
    return render_to_response('bug_index.html')


@ensure_csrf_cookie
def bug_manage(request):
    bug_list = Task.objects.all()
    step_list = Logic.objects.all()
    func_list = FuncMessage.objects.all()
    context = {
        'bug_list': bug_list,
        'step_list':step_list,
        'func_list':func_list
    }
    return render_to_response('bug_manage.html', context=context)


def add_bug(request):
    '''ajax add bug'''
    bug_name = request.POST['name']
    bug_founder = request.POST['founder']
    bug_list = Task.objects.filter(name__exact=bug_name)
    if len(bug_list) == 0 and len(bug_name) >= 1:
        task = Task(
            founder=bug_founder,
            name=bug_name,
        )
        task.save()
        return HttpResponse("saved")
    else:
        return HttpResponse("same name")


def add_step(request):
    step_name = request.POST['name']
    logic_list = Logic.objects.filter(name__exact=step_name)
    if len(logic_list) == 0 and len(step_name) >= 1:
        logic = Logic(
            name=request.POST['name'],
            address=request.POST['address'],
            isFE_begin=request.POST['isFE_begin'],
            send_delay=request.POST['send_delay'],
            read_delay=request.POST['read_delay'],
            step_num=request.POST['step_num'],
            frame=request.POST['frame'],
            func_id=request.POST['func_id'],
            display_msg=request.POST['display_msg'],
            val0=request.POST['val0'],
            val1=request.POST['val1'],
            val2=request.POST['val2'],
            val3=request.POST['val3'],
            val4=request.POST['val4'],
            val5=request.POST['val5'],
            val6=request.POST['val6'],
            val7=request.POST['val7'],
            val8=request.POST['val8'],
            val9=request.POST['val9'],
        )
        logic.save()
        return HttpResponse('saved')
    else:
        return HttpResponse('name unqualified')


def set_config(request):
    ip = request.GET['ip']
    com = request.GET['com'].upper()
    # send ok to web ajax
    resp = HttpResponse("ok")
    resp.set_cookie("ip", ip)
    resp.set_cookie("com", com)
    return resp
def get_step(request):
    try:
        logic_dic = Logic.objects.filter(name=request.POST['name']).values()[0]
        return HttpResponse(json.dumps(logic_dic))
    except:
        return HttpResponse("not found")
    
    # send_data["name"] = logic.name
    # send_data["address"] = logic.address
    # send_data["ischange_addr"] = logic.ischange_addr
