from django.shortcuts import render
from django.http import *
from django.views.decorators.csrf import *
from django.shortcuts import render
from django.utils import timezone
import sys,time,threading
#add the module path
sys.path.append("e:\项目\python\web_project\DGYengine")
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
        user_client_list = ClientUser.objects.filter(ip4_addr__exact=hand_data.client_ipv4).filter(com_id__exact=hand_data.client_comid)
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
        get_cell(user_client.ip4_addr,user_client.com_id).serial_event.wait()
    else:
        return 


def login_view(request):
    return render_to_response("bug_login.html")

def login_check(request):
    username = request.GET['username']
    password = request.GET['password']
    resp = HttpResponseRedirect("/bug_manage/index/")
    resp.set_cookie("username",username)
    resp.set_cookie("password",password)
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
    get_cell(ip,com).web_event.wait()
    return
def boot(request):
    return render_to_response('bug_index.html')

@ensure_csrf_cookie
def bug_manage(request):
    return render_to_response('bug_manage.html')

def add_bug(request):
    '''ajax add bug'''
    bug_name = request.POST['name']
    bug_founder = request.POST['founder']
    bug_list = Bug.objects.filter(name__exact=bug_name)
    if len(bug_list) == 0:
        bug = Bug(
            founder=bug_founder,
            name=bug_name,
        )
        bug.save()
        return HttpResponse("saved")
    else:
        return HttpResponse("same name")


def set_config(request):
    ip = request.GET['ip']
    com = request.GET['com'].upper()
    #send ok to web ajax
    resp = HttpResponse("ok")
    resp.set_cookie("ip",ip)
    resp.set_cookie("com",com)
    return resp