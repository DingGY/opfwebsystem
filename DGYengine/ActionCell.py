from dwebsocket.decorators import *
from django.http import *
sys.path.append("e:\项目\python\web_project\DGYengine")
from LogicParse import LogicParse
class ActionCell:
    def __init__(self,web_client):
        self._serial_client_dic = {}
        self._serial_client_recv_data_dic = {} 
        self._web_client = web_client
        self._logic_list = []

    def _get_sckey(self,ip,com):
        return ip + ':' + com
    def add_serial_client(self,ip,com,request):
        self._serial_client_dic[self._get_sckey(ip,com)] = request
    def clear_serial_client(self):
        self._serial_client_dic.clear()
    def remove_serial_client(self,ip,com):
        del self._serial_client_dic[self._get_sckey(ip,com)]
    def set_web_client(self,web_client):
        self._web_client = web_client

    def add_logic(self,logic):
        self._logic_list.append(logic)
    def clear_logic(self):
        self._logic_list.clear()
    def send_web_client(self,data):
        self._web_client.send(data.encode())
    def send_serial_client(self,data):
        for sc_key, sc_val in self._serial_client_dic:
            sc_val.send(data.encode())
    def send_single_serial_client(self,ip,com,data):
        self._serial_client_dic[self._get_sckey(ip,com)].send(data.encode())
    
    def recv_serial_client(self):
        for sc_key, sc_val in self._serial_client_dic:
            self._serial_client_recv_data_dic[sc_key] = sc_val.read()

    def recv_single_serial_client(self, ip, com):
        self._serial_client_recv_data_dic[self._get_sckey(ip,com)] = \
            self._serial_client_dic[self._get_sckey(ip,com)].read()
        
    def recv_web_client(self):
        return self._web_client.read()
        

    def run(self):
        pass




