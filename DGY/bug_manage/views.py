from django.shortcuts import render
from django.http import *
from django.views.decorators.csrf import *
from django.shortcuts import render
from django.utils import timezone
import sys
import json
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
        pass


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
    task = Task.objects.get(name='Yujie')
    get_cell(ip, com).run_remote_task(task)
    get_cell(ip, com).web_event.wait()
    
    return


def boot(request):
    resp = render_to_response('bug_index.html')
    task_list = Task.objects.all()
    context = {
        'task_list':task_list
    } 
    return render_to_response('bug_index.html',context)


@ensure_csrf_cookie
def bug_manage(request):
    bug_list = Task.objects.all()
    step_list = Logic.objects.all()
    func_list = FuncMessage.objects.all()
    context = {
        'bug_list': bug_list,
        'step_list': step_list,
        'func_list': func_list
    }
    return render_to_response('bug_manage.html', context=context)


def set_config(request):
    ip = request.GET['ip']
    com = request.GET['com'].upper()
    # send ok to web ajax
    resp = HttpResponse("ok")
    resp.set_cookie("ip", ip)
    resp.set_cookie("com", com)
    return resp
def get_task_info(request):
    task_name = request.GET['name']
    try:
        task_dic = Task.objects.filter(name=task_name).values()[0]
        task_dic['create_date'] = task_dic['create_date'].strftime(
            '%Y-%m-%d %H:%M:%S')
        resp = HttpResponse(json.dumps(task_dic))
        resp.set_cookie('run_task',task_name)
        return resp
    except:
        return HttpResponse('not found')
    


def step_action(request, action):
    if action == 'addfunc':
        logic = Logic.objects.get(id=int(request.POST['logic_id']))
        func = FuncMessage.objects.get(id=int(request.POST['act_id']))
    else:
        step_name = request.POST['name']
        logic_list = Logic.objects.filter(name__exact=step_name)
    if action == 'add':
        if len(logic_list) == 0 and len(step_name) >= 1:
            logic = Logic(
                name=request.POST['name'],
                address=request.POST['address'],
                isFE_begin=request.POST['isFE_begin'],
                ischange_addr=request.POST['ischange_addr'],
                send_delay=request.POST['send_delay'],
                read_delay=request.POST['read_delay'],
                #func=FuncMessage.objects.get(func_id=request.POST['func_id']),
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
            resp =  HttpResponse('saved')
            resp.set_cookie("step", logic.id)
            return resp
        else:
            return HttpResponse('name unqualified')
    elif action == 'get':
        if len(logic_list) == 1:    
            logic = logic_list[0]
            logic_dic = logic_list.values()[0]
            # get the func of step 
            func_id = logic_dic['func_id']
            if func_id != None:
                logic_dic['func_id'] = FuncMessage.objects.get(id=func_id).func_id
                logic_dic['frame_set'] = FuncMessage.objects.get(id=func_id).frame_set
            resp = HttpResponse(json.dumps(logic_dic))
            resp.set_cookie("step", logic.id)
            return resp
    elif action == 'del':  
        if len(logic_list) != 0:    
            logic_list[0].delete()  
            return HttpResponse("deleted")
    elif action == 'change':
        if len(logic_list) != 0:
            logic = logic_list[0]
            logic.name=request.POST['name']
            logic.address=request.POST['address']
            logic.isFE_begin=request.POST['isFE_begin']
            logic.send_delay=request.POST['send_delay']
            logic.read_delay=request.POST['read_delay']
            logic.func=FuncMessage.objects.get(func_id=request.POST['func_id']) 
            logic.display_msg=request.POST['display_msg']
            logic.val0=request.POST['val0']
            logic.val1=request.POST['val1']
            logic.val2=request.POST['val2']
            logic.val3=request.POST['val3']
            logic.val4=request.POST['val4']
            logic.val5=request.POST['val5']
            logic.val6=request.POST['val6']
            logic.val7=request.POST['val7']
            logic.val8=request.POST['val8']
            logic.val9=request.POST['val9']
            logic.save()
            return HttpResponse("changed")
    elif action == 'addfunc':
        logic.func = func
        logic.save()
        return HttpResponse("addfunced")
    else:
        return HttpResponse("step_action failed")
    return HttpResponse("not found")

def get_task_step(task):
    '''get the task step info'''
    step_list = task.step.all().order_by('num')
    logic_list= []
    for step in step_list:
        step_dic = {}
        step_dic["step_num"] = step.num
        step_dic["step_head"] = step.act.name
        step_dic["step_text"] = step.act.display_msg
        logic_list.append(step_dic)
        print(step.num)
    return logic_list

def task_action(request, action):
    if action != 'addstep' and action != 'delstep':
        task_name = request.POST['name']
        task_list = Task.objects.filter(name__exact=task_name)
    if action == 'get':
        task = task_list[0]
        task_dic = task_list.values()[0]
        task_dic['create_date'] = task_dic['create_date'].strftime(
            '%Y-%m-%d %H:%M:%S')
        task_dic['step_info_list'] = get_task_step(task)
        resp = HttpResponse(json.dumps(task_dic))
        resp.set_cookie("task", task.id)
        return resp
    elif action == 'add':
        if len(task_list) == 0 and len(task_name) >= 1:
            task = Task(
                founder=request.POST['founder'],
                name=task_name,
                msg=request.POST['msg'],
            )
            task.save()
            resp = HttpResponse("saved")
            resp.set_cookie("task", task.id)
            return resp
    elif action == 'del':
        if len(task_list) != 0:
            task_list[0].delete()
            return HttpResponse("deleted")
    elif action == 'change':
        if len(task_list) != 0:
            task = task_list[0]
            task.founder=request.POST['founder']
            task.name=task_name
            task.msg=request.POST['msg']
            task.save()
            return HttpResponse('changed')
    elif action == 'addstep':

        task = Task.objects.get(id=int(request.POST['id']))
        if len(task.step.filter(num=int(request.POST['num']))) != 0:
            return HttpResponse('same num')
        task_step = StepAction(
            num = int(request.POST['num']),
            act = Logic.objects.get(id=int(request.POST['logic_name']))
        )
        task_step.save()
        task.step.add(task_step)
        task.save()
        return HttpResponse("addsteped")
    elif action == 'delstep':
        task = Task.objects.get(id=int(request.POST['id']))
        step_action = StepAction.objects.get(num=int(request.POST['step_num']))
        task.step.remove(step_action)
        step_action.delete()
        return HttpResponse("deleted")
    else:
        return HttpResponse("task_action failed")
    return HttpResponse("not found")

def func_action(request, action):
    func_name = request.POST['name']
    func_list = FuncMessage.objects.filter(name__exact=func_name)
    if action == 'get':
        func = func_list[0]
        func_dic = func_list.values()[0]
        func_dic['create_date'] = func_dic['create_date'].strftime(
            '%Y-%m-%d %H:%M:%S')
        resp = HttpResponse(json.dumps(func_dic))
        resp.set_cookie("func", func.id)
        return resp

    elif action == 'add':
        if len(func_list) == 0 and len(func_name) >= 1:
            func = FuncMessage(
                name=func_name,
                func_id=request.POST['func_id'],
                msg=request.POST['msg'],
                frame_set=request.POST['frame_set'],
            )
            func.save()
            resp = HttpResponse("saved")
            resp.set_cookie("func", func.id)
            return resp
    elif action == 'del':
        if len(func_list) != 0:
            func_list[0].delete()
            return HttpResponse("deleted")
    elif action == 'change':
        if len(func_list) != 0:
            func = func_list[0]
            func.name = func_name
            func.func_id=request.POST['func_id']
            func.msg=request.POST['msg']
            func.frame_set=request.POST['frame_set']
            func.save()
            return HttpResponse("changed")
    else:
        return HttpResponse("task_action failed")
    return HttpResponse("not found")

@csrf_exempt
def local_client(request):
    client_opt = request.POST['opt']
    client_data = request.POST['data']
    print(request.POST['data'])
    if client_opt == 'reflash':
        task_name_list = []
        task_name_dic = Task.objects.values('name')
        for name in task_name_dic:
            task_name_list.append(name['name'])
        return HttpResponse(json.dumps(task_name_list))
    if client_opt == 'get':
        # try:
        task = Task.objects.get(name=client_data)
        logic_list = LogicParse(task).parse_task()
        return HttpResponse(json.dumps({'logic_list':logic_list}))
        # except:
        #     pass    
    return HttpResponse('not found') 