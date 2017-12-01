from dwebsocket.decorators import *
from django.http import *
import sys,threading,time
sys.path.append("e:\项目\python\web_project\DGYengine")
from LogicParse import  *
from ClientParse import ClientParse

action_cell_dic = {}

def join_web_cell(req,user,ip,com):
    cell_index = ip+com
    if action_cell_dic.__contains__(cell_index):
        action_cell_dic[cell_index].set_webreq(req,user)
    else:
        action_cell_dic[cell_index] = ActionCell()
        action_cell_dic[cell_index].set_webreq(req,user)


def join_serial_cell(req,ip,com):
    cell_index = ip+com
    if action_cell_dic.__contains__(cell_index):
        action_cell_dic[cell_index].set_serialreq(req)
    else:
        action_cell_dic[cell_index] = ActionCell()
        action_cell_dic[cell_index].set_serialreq(req)

def get_cell(ip,com):
    cell_index = ip+com
    return action_cell_dic[cell_index]
def dell_cell(ip,com):
    cell_index = ip+com
    action_cell_dic[cell_index].close_webreq()
    del action_cell_dic[cell_index]
class ActionCell:
    def __init__(self):
        '''the web is leading action client'''
        self.isSerialConn = False
        self.isWebConn = False
        self.web_event = threading.Event()
        self.serial_event = threading.Event()
        self.user_id = ""

    def close_webreq(self):
        self.web_event.set()
        self.serial_event.set()

    def set_webreq(self,req,user):
        if self.isWebConn and self.user_id != user:
            raise Exception('the web cell is used')
        else:
            if self.isWebConn:
                self.web_event.set()
                # wait to the old websocket finished 
                time.sleep(1)
            self.web_event.clear()
            self._web_req = req
            self.user_id = user
            self.isWebConn = True

    def set_serialreq(self,req):
        if self.isSerialConn:
            raise Exception('the serial cell is used')
        else:
            self.serial_event.clear()
            self._serial_req = req
            self.isSerialConn = True
    

    def send_serial(self,data):
        if self.isSerialConn:
            self._serial_req.websocket.send(data.encode())
        else:
            raise Exception("serial not connect")

    def recv_serial(self):
        if self.isSerialConn:
            recv_pack = str(self._serial_req.websocket.wait().decode('ascii'))
            if recv_pack is None:
                raise Exception("not recv a data from serial")
            recv_unpacked = ClientParse(recv_pack)
            return recv_unpacked.recv_data
        else:
            raise Exception("serial not connect")

    def send_web(self,data):
        if self.isWebConn:
            self._web_req.websocket.send(data.encode())
        else:
            raise Exception("web client not connect")

    def recv_web(self):
        if self.isWebConn:
            recv_pack = str(self._web_req.websocket.wait().decode('ascii'))
            if recv_pack is None:
                raise Exception("not recv a data from web")
            # add websocket ui parse here
            recv_unpacked = recv_pack
            return recv_unpacked
        else:
            raise Exception("web client not connect")

    def send_web_to_serial(self):
        '''passthrough'''
        send_data = self.recv_web()
        self.send_serial(send_data)

    def recv_serial_to_web(self):
        '''passthrough'''
        send_data = self.recv_serial()
        self.send_web(send_data)
    
    def task(self,task,runner):
        runner.run_all(task)

    def refresh_ui(self,ui):
        self.send_web(ui)
        return

    def run_task(self,task):
        task_runner = LogicParse()
        t = threading.Thread(target=self.task,args=(task,task_runner,))
        t.start()
        while True:
            task_runner.wait_logic()
            if task_runner.act_read_status:
                task_runner.recv_data = self.recv_serial()
            if task_runner.act_write_status:
                self.send_serial(task_runner.send_data)
            if task_runner.act_ui_status:
                self.refresh_ui(task_runner.ui_msg)
            if task_runner.is_finishrun:
                break
        self.close_webreq()
        return

    






    




