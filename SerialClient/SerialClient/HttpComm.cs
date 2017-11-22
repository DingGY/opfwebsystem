using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms; 
namespace SerialClient
{
    public class HttpComm
    {
        public string url;

        public string req_header;
        private HttpWebRequest req;
        private WebResponse resp;
        private string Local_ID;
        private string IOChannel_ID;
        private HardwareInfo GetID;
        
        
        public HttpComm()
        {
            GetID = new HardwareInfo();
            SetID();
        }
        public HttpComm(string url):this()
        {
            this.url = url;
        }
        public void SetID()
        {
            Local_ID = GetID.GetCpuID();
            IOChannel_ID = GetID.GetHostAddress()+"::"+MainForm.clientConfig.comPortConfig.comPort;
            req_header = Local_ID + "::" + IOChannel_ID;
        }

        public string FormatData(string data,OptType opt)
        {
            string format_data = "";
            switch (opt)
            {
                case OptType.SERIALDATA:
                    format_data = req_header + "::SERIALDATA::" + data;
                    break;
                default:
                    format_data = "";
                    break;
            }
            return format_data;
        }
        public string FormatData(OptType opt)
        {
            string format_data = "";
            switch (opt)
            {
                case OptType.CONNECT:
                    format_data = req_header + "::CONNECT";
                    break;
                case OptType.CLOSE:
                    format_data = req_header + "::CLOSE";
                    break;
                case OptType.RECV:
                    format_data = req_header + "::RECV";
                    break;
                default:
                    format_data = "";
                    break;
            }
            return format_data;
        }
        public string Send(string data,OptType opt)
        {
            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                var ReqStream = req.GetRequestStream();
                byte[] sendByte = encoding.GetBytes(FormatData(data,opt));
                ReqStream.Write(sendByte, 0, sendByte.Length);
                resp = req.GetResponse();
                var st = resp.GetResponseStream();
                StreamReader stread = new StreamReader(st);
                return stread.ReadToEnd();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "";
            }
        }
        public string Send(OptType opt)
        {
            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                var ReqStream = req.GetRequestStream();
                byte[] sendByte = encoding.GetBytes(FormatData(opt));
                ReqStream.Write(sendByte, 0, sendByte.Length);
                resp = req.GetResponse();
                var st = resp.GetResponseStream();
                StreamReader stread = new StreamReader(st);
                return stread.ReadToEnd();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "";
            }
        }
        public bool Recv(ref string recvData)
        {
            recvData = "";
            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                var ReqStream = req.GetRequestStream();
                byte[] sendByte = encoding.GetBytes(FormatData(OptType.RECV));
                ReqStream.Write(sendByte, 0, sendByte.Length);
                resp = req.GetResponse();
                var st = resp.GetResponseStream();
                StreamReader stread = new StreamReader(st);
                recvData = stread.ReadToEnd();
                return true;
            }
            catch (System.Exception ex)
            {
                recvData = "";
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
    }
}
