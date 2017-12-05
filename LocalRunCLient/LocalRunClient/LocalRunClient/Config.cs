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
        public int clockNum;
    }
    public class Config
    {
        public string[] comPortArr;
        public SerialPortConfig comPortConfig;
        public string url;
        public string encryptionIP;
        public string encryptionPort;
        public string program;
        //[XmlIgnore]
        //
        public Config()
        {
            comPortArr = new string[3];
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
