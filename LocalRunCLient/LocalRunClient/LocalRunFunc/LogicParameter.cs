using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalRunFunc
{
    public struct Logic
    {
        public int id;
        public string name;
        public string address;
        public bool ischange_addr;
        public bool isFE_begin;
        public int send_delay;
        public int read_delay;
        public string frame_set;
        public string func;
        public string display_msg;
        public string val0;
        public string val1;
        public string val2;
        public string val3;
        public string val4;
        public string val5;
        public string val6;
        public string val7;
        public string val8;
        public string val9;
    }

    public class TaskConfig
    {

        public List<Logic> logic_list;
        public TaskConfig()
        {
            logic_list = new List<Logic>();
        }

    }

    public class RunReturn
    {
        public string address;
        public bool cmpStatus;
    }
}
