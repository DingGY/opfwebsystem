from ClientParse import ClientParse
import threading,time,sys,json
from django.forms.models import model_to_dict
sys.path.append(r"e:\项目\python\web_project\DGY\bug_manage")

class _TaskFlow:
    def __init__(self):
        self.addr = ""
        self.recv_data = ""
        self.meter_num = ''
        '''
        0x00:finished
        0x01:running
        0x02:error current
        '''
        self.run_status = 0x00
        self.cmp_status = 0x00
        self.show_msg_list = []



class LogicParse:
    def __init__(self,task):
        self._task = task
    def parse_task(self):
        logic_list = []
        logic_list.clear()
        step_list = self._task.step.all().order_by('num')
        for step in step_list:
            step_dic = {}
            step_dic = model_to_dict(step.act)
            # get the func name not the func id
            step_dic['func'] = step.act.func.func_id
            step_dic['frame_set'] = step.act.func.frame_set
            logic_list.append(step_dic)
        return logic_list


class RemoteLogicParse(LogicParse):
    def __init__(self):
        self._wait_logic_event = threading.Event()
        self._wait_act_event = threading.Event()
        self.act_read_status = False
        self.act_write_status = False
        self.act_ui_status = False
        self.ui_msg = ''
        self.recv_data = ''
        self.send_data = ''
        self.is_finishrun = False
        return  
    def wait_logic(self):
        '''action and logic are mutex event'''
        self._wait_logic_event.clear()
        self._wait_act_event.set()
        self._wait_logic_event.wait()

    def wait_action(self):
        '''action and logic are mutex event'''
        self._wait_logic_event.set()
        self._wait_act_event.clear()
        self._wait_act_event.wait()
    def set_event(self):
        self._wait_logic_event.set()
        self._wait_act_event.set()
    def composite_frame(self,dic_data):
        frame = ''
        if dic_data.__contains__('FE_begin'):
            frame += 'FE FE FE FE'
        if dic_data.__contains__('addr'):
            frame += dic_data['addr']
        if dic_data.__contains__('frame'):
            frame += dic_data['frame']
        return frame

    def write_to_client(self,data):
        self.act_ui_status = False
        self.act_read_status = False
        self.act_write_status = True
        self.send_data = data
        self.wait_action()

    def read_from_client(self):
        self.act_write_status = False
        self.act_ui_status = False
        self.act_read_status = True
        self.wait_action()

    def reflash_action_ui(self,data):
        self.act_read_status = False
        self.act_write_status = False
        self.act_ui_status = True
        self.ui_msg = data
        self.wait_action()

    def single_step(self,front_data,step):
        '''group frame front_data is the data of front step'''
        frame_dic = {}
        if step.ischange_addr:
            frame_dic['addr'] = step.addr
        if step.isFE_begin:
            frame_dic['FE_begin'] = True
        '''write data to the client'''
        frame_dic['frame'] = step.frame
        ui_msg = "正在执行：" + step.display_msg
        self.reflash_action_ui(ui_msg)
        self.write_to_client(self.composite_frame(frame_dic))
        '''send finished delay time'''
        time.sleep(step.send_delay)
        act_ret = getattr(self,step.func_id)(front_data,step)  
        if act_ret.cmp_status == 0x00: 
            print("ret_data.cmp_status",act_ret.cmp_status)
        if act_ret.run_status == 0x00:
            print("ret_data.run_status",act_ret.run_status)
        return act_ret

    def read_data(self,front_data,step):
        act_ret = _TaskFlow()
        self.read_from_client()
        ui_msg = "读取数据：" + self.recv_data
        self.reflash_action_ui(ui_msg)
        return act_ret

    def run_remote_all(self,task):
        self.is_finishrun = False
        step_list = task.step.all().order_by('num')
        first_step = step_list[0]
        act_ret = self.single_step(_TaskFlow(),first_step.act)
        for step in step_list[1:]:
            act_ret = self.single_step(act_ret,step.act)
        self.is_finishrun = True
        self.set_event()

