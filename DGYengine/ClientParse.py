class ClientParse:
    def __init__(self,data):
        self.parse_data = data.split("::")
        self.client_user_id = self.parse_data[0]
        self.client_ipv4 = self.parse_data[1]
        self.client_comid = self.parse_data[2]
        self.client_opt = self.parse_data[3]
        self.meter_addr = ""
        self.meter_cs = 0
        self.meter_data = ""
        self.meter_data_len = 0
        self.meter_status = 0
        if len(self.parse_data) > 4:
            self.recv_data = self.parse_data[4]
            self.decode_645data(self.recv_data)
    def has_recv_data(self):
        if self.recv_data == "":
            return False
        else:
            return True
    def decode_645data(self,data):
        '''parse 645 data frame'''
        step = 0
        data_len = 0
        for s in data.split(" "):
            if s == '68' and step == 0:
                step += 1
                continue
            if step == 1:
                self.meter_addr = s + self.meter_addr
                if len(self.meter_addr) == 12:
                    step += 1
                    continue
                elif len(self.meter_addr) > 12:
                    raise Exception("meter address length error")
            if step == 2:
                if s == '68':
                    step += 1
                    continue
                else:
                    raise Exception("sceond 68 error")
            if step == 3:
                self.meter_status = int(s,16)
                step += 1
                continue
            if step == 4:
                self.meter_data_len = int(s,16)
                data_len = self.meter_data_len
                step += 1
                continue
            if step == 5:
                if data_len > 0:
                    data_byte = hex((int(s,16)-0x33) & 0xff)[2:]
                    if len(data_byte) == 1:
                        data_byte = '0' + data_byte
                    self.meter_data = \
                        data_byte + self.meter_data 
                    data_len -= 1
                else:
                    step += 1
            if step == 6:
                self.meter_data
                self.meter_cs = hex(int(s,16))[2:]
                step += 1
                continue
            if step == 7:
                print(s)
                if s != '16':
                    raise Exception('end byte is not 16')
                else:
                    step += 1
                    continue
            if step == 8:
                step = 0
        if data_len != 0 or step != 0:
            print(data_len)
            print(step)
            raise Exception('meter data felid error')

#client_data = ParseClient(str(request.body.decode('utf-8')))

             


        
