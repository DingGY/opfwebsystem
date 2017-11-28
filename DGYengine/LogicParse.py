from ClientParse import ClientParse
import threading,time,sys
sys.path.append(r"e:\项目\python\web_project\DGY\bug_manage")

class _TaskFlow:
    def __init__(self):
        self.addr = ""
        self.recv_data = ""
        '''
        0x00:finished
        0x01:running
        0x02:error current
        '''
        self.run_status = 0x00
        self.cmp_status = 0x00
        self.show_msg_list = []


class LogicParse:
    def __init__(self):
        self._wait_logic_event = threading.Event()
        self._wait_act_event = threading.Event()
        self.act_read_status = False
        self.act_write_status = False
        self.act_ui_status = False
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
    
    def change_addr(self,frame,addr):
        frame[1:6] = addr

    def write_to_client(self):
        self.act_ui_status = False
        self.act_read_status = False
        self.act_write_status = True
        self.wait_action()

    def read_from_client(self):
        self.act_write_status = False
        self.act_ui_status = False
        self.act_read_status = True
        self.wait_action()

    def reflash_action_ui(self):
        self.act_read_status = False
        self.act_write_status = False
        self.act_ui_status = True
        self.wait_action()

    def single_step(self,front_data,logic):
        '''group frame'''
        if logic.ischange_addr:
            self.change_addr(logic.frame,front_data.addr)
        '''write data to the client'''
        self.write_to_client()
        '''send finished delay time'''
        if logic.issend_finish_delay:
            time.sleep(logic.send_finish_delay)
        ret_data = getattr(self,logic.func_id)(front_data,logic)  
        if ret_data.cmp_status == 0x00: 
            print("ret_data.cmp_status",ret_data.cmp_status)
        if ret_data.run_status == 0x00:
            print("ret_data.run_status",ret_data.run_status)
        return ret_data


