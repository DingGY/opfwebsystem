from django.shortcuts import render
from django.http import *
from django.views.decorators.csrf import *
from django.shortcuts import render
from django.utils import timezone
import sys,time,threading
#add the module path
sys.path.append("e:\项目\python\web_project\DGYengine")
from ClientParse import ClientParse
from .models import *
from django.db import models
from django.contrib.auth import logout
from django.shortcuts import render_to_response
from django import template
from dwebsocket.decorators import *
import json
# Static value
send_event_dic = {}
recv_event_dic = {}
thread_run_status = {}
# Create your views here.
def send_to_client_task(request,client,index):
    while thread_run_status[index]:
        send_event_dic[index].clear()
        send_event_dic[index].wait()
        client.refresh_from_db()
        if client.send_status:
            client.send_status = False
            client.save()
            request.websocket.send(client.send_data.encode())
    del send_event_dic[index]
    del thread_run_status[index]
    return


@require_websocket
def test(request):
    print("come in")
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
        thread_index = user_client.ip4_addr + user_client.com_id
        send_event_dic[thread_index] = threading.Event()
        thread_run_status[thread_index] = True
        t = threading.Thread(target=send_to_client_task,args=(request,user_client,thread_index))
        t.start()
    else:
        return
    for m in request.websocket:
        try:
            client_data = ClientParse(str(m.decode('ascii')))
        except:
            continue
        opt = client_data.client_opt
        if opt == "CLOSE":
            request.websocket.send(b'ack')
            thread_run_status[thread_index] = False
            send_event_dic[thread_index].set()
            break
        elif opt == "SERIALDATA":
            user_client.get_data = client_data.recv_data
            user_client.get_status = True
            user_client.save() 
            if recv_event_dic.__contains__(thread_index):
                recv_event_dic[thread_index].set()        
            

def login_view(request,step):
    if step != 'welcome':
        return render_to_response('login/index.html')
    else:
        context = {'username':request.GET.get('username')}
        respones = render_to_response('userpage/userconfig.html',context)
        respones.set_cookie('username',request.GET.get('username'))
        respones.set_cookie('password',request.GET.get('password'))
        return respones

def user_view(request,step):
    ipaddr = ""
    com_id = ""
    send_data = ""
    recv_data = ""
    if step == 'userconfig':
        respones = render_to_response('userpage/userconfig.html')
        return respones
    elif step == "serialcomm":
        ipaddr = request.GET.get('ipaddr')
        com_id = request.GET.get('com_id').upper()
        request.COOKIES['com_id'] = com_id
        request.COOKIES['ipaddr'] = ipaddr
        thread_index = ipaddr + com_id
        recv_event_dic[thread_index] = threading.Event()
    elif step == "send":
        send_data = request.GET.get('serial_data').upper()
        com_id = request.COOKIES['com_id']
        ipaddr = request.COOKIES['ipaddr']
    else:
        return HttpResponse("Not Found")
    user_client_list = ClientUser.objects.filter(ip4_addr__exact=ipaddr).filter(com_id__exact=com_id)
    if len(user_client_list) > 0:
        user_client = user_client_list[0]
        user_client.send_data = send_data
        user_client.send_status = True
        thread_index = request.COOKIES['ipaddr'] + request.COOKIES['com_id']
        if send_event_dic.__contains__(thread_index):
            send_event_dic[thread_index].set()        
        if user_client.get_status:
            recv_data = user_client.get_data
            user_client.get_status = False
        else:
            recv_data = '无接收数据'
        user_client.save()
        connect_status = '连接正常'
    else:
        connect_status = '未连接'
    context = {
        'status':connect_status,
        'ipaddr':ipaddr,
        'com_id':com_id,
    }
    respones = render_to_response('userpage/serialcomm.html',context)
    if step == 'serialcomm':
        respones.set_cookie('ipaddr',ipaddr)
        respones.set_cookie('com_id',com_id)
    return respones



@require_websocket
def web_comm(request):
    ip = request.COOKIES['ipaddr']
    com_id = request.COOKIES['com_id']
    index = ip + com_id
    client = ClientUser.objects.filter(ip4_addr__exact=ip).filter(com_id__exact=com_id)[0]
    while True:
        recv_event_dic[index].wait()
        client.refresh_from_db()
        request.websocket.send(client.get_data.encode())
        recv_event_dic[index].clear()
def boot(request):
    return render_to_response('bug_index.html')

@ensure_csrf_cookie
def bug_manage(request):
    return render_to_response('bug_manage.html')

def add_bug(request):
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