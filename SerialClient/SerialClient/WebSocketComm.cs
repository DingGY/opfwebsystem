using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Net;
using System.Windows.Forms;
using System.Threading;

namespace SerialClient
{
    public enum OptType
    {
        CONNECT,
        RECV,
        CLOSE,
        SERIALDATA
    }
    public class WebSocketComm
    {
        public string url;

        public string ReqHeader;
        private string _LocalID;
        private string _IOChannelID;
        private HardwareInfo _GetID;
        private WebSocket _ws;
        public bool IsRecv = false;
        public bool IsPing = false;
        public string _RecvedData;
        public string RecvedData
        {
            get
            {
                IsRecv = false;
                return _RecvedData;
            }
            set
            {
                _RecvedData = value;
            }
        }
        public WebSocketComm(string url)
        {
            _ws = new WebSocket(url);
            _GetID = new HardwareInfo();
            _ws.OnMessage += (sender, e) =>
                {
                    IsRecv = true;
                    IsPing = e.IsPing;
                    RecvedData = e.Data;
                };
            _ws.OnError += (sender, e) =>
                {
                    throw new Exception("Error:"+ e.Message);
                };
            //_ws.OnClose += (sender, e) => 
                //Console.WriteLine("{0}-->{1}", e.Code, e.Reason);
            SetID();
            _ws.Connect();
        }
        public void SetID()
        {
            _LocalID = _GetID.GetCpuID();
            _IOChannelID = _GetID.GetHostAddress() + "::" + MainForm.clientConfig.comPortConfig.comPort;
            ReqHeader = _LocalID + "::" + _IOChannelID;
        }
        public string FormatData(string data,OptType opt)
        {
            string format_data = "";
            switch (opt)
            {
                case OptType.SERIALDATA:
                    format_data = ReqHeader + "::SERIALDATA::" + data;
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
                    format_data = ReqHeader + "::CONNECT";
                    break;
                case OptType.CLOSE:
                    format_data = ReqHeader + "::CLOSE";
                    break;
                case OptType.RECV:
                    format_data = ReqHeader + "::RECV";
                    break;
                default:
                    format_data = "";
                    break;
            }
            return format_data;
        }
        public void Send(string data, OptType opt)
        {
            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] sb = encoding.GetBytes(FormatData(data,opt));
                _ws.Send(sb);

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void Send(OptType opt)
        {
            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] sb = encoding.GetBytes(FormatData(opt));
                _ws.Send(sb);

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void Close()
        {
            _ws.Close();
        }

    }
}
