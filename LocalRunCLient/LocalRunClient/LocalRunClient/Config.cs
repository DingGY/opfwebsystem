using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Threading;//用于启用线程类；
using System.IO.Ports;//用于调用串口类函数

namespace LocalRunClient
{
    public struct SerialPortConfig
    {
        public int bound;
        public int comTimeout;
        public StopBits stopBit;
        public Parity parity;
        public int dataBits;
        public string comPort1;
        public string comPort2;
        public string comPort3;
        public int clockNum;
    }
    public class Config
    {

        public SerialPortConfig comPortConfig;
        public string url;
        public string encryptionIP;
        public string encryptionPort;
        public string program;
        //[XmlIgnore]
        //
        public Config()
        {
        }
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="obj"></param>
        public void SaveConfig()
        {
            StreamWriter sw = new StreamWriter(@"LocalRunConfig.xml");
            XmlSerializer xs = new XmlSerializer(typeof(Config),new XmlRootAttribute("CONFIG"));
            xs.Serialize(sw, this);
            sw.Flush();
            sw.Close();
        }
        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static Config LoadConfig(string file)
        {
            StreamReader sr = new StreamReader(file);
            XmlSerializer xs = new XmlSerializer(typeof(Config), new XmlRootAttribute("CONFIG"));
            Config obj = xs.Deserialize(sr) as Config;
            sr.Close();
            if (obj != null)
            {
                return obj;
            }
            return null;
        }
    }
}
public struct tt
{
    public string name;
}
public class TaskConfig
{
    //public class Logic
    //{
    //    public int id;
    //    public string name;
    //    public string address;
    //    public bool ischange_addr;
    //    public bool isFE_begin;
    //    public int send_delay;
    //    public int read_delay;
    //    public string frame;
    //    public string func_id;
    //    public string display_msg;
    //    public string val0;
    //    public string val1;
    //    public string val2;
    //    public string val3;
    //    public string val4;
    //    public string val5;
    //    public string val6;
    //    public string val7;
    //    public string val8;
    //    public string val9;
    //}
    //public List<Logic> logicList;
    //public string name1;

    public List<tt> ll;
    public TaskConfig()
    {
        ll = new List<tt>();
        //logicList = new List<Logic>();
    }

}