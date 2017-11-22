using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace SerialClient
{
    public partial class MainForm : Form
    {
        public SerialPort comPort;
        public Thread connThread;
        public Thread recvThread;
        public bool _Connect_Status;
        public WebSocketComm webSocket;
        static public Config clientConfig;
        
        public MainForm()
        {
            InitializeComponent();

            comPort = new SerialPort();

            clientConfig = Config.LoadConfig(@"SerialClient.xml");
            Control.CheckForIllegalCrossThreadCalls = false;
            _Connect_Status = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
 
        }

        private void connectButt_Click(object sender, EventArgs e)
        {
            _Connect_Status = true;
            if (connThread != null)
            {
                if (connThread.IsAlive)
                {
                    return;
                }
            }
            connThread = new Thread(new ParameterizedThreadStart(Connect_Task));
            connThread.Start();

        }
        private void closeButt_Click(object sender, EventArgs e)
        {
            _Connect_Status = false;   
        }
        void Connect_Task(object sender)
        {
            string sendResult = "";
            string recvResult = "";
            bool isSend = false;
            try
            {
                comPort.PortName = clientConfig.comPortConfig.comPort;
                comPort.BaudRate = clientConfig.comPortConfig.bound;
                comPort.Parity = clientConfig.comPortConfig.parity;
                comPort.StopBits = clientConfig.comPortConfig.stopBit;
                comPort.ReadTimeout = clientConfig.comPortConfig.comTimeout;
                webSocket = new WebSocketComm(clientConfig.url);

                if (!comPort.IsOpen)
                {
                    comPort.Open();
                }
                webSocket.Send(OptType.CONNECT);
                while (!webSocket.IsRecv)
                {
                    Thread.Sleep(100);
                }
                if (webSocket.RecvedData != "ack")
                {
                    throw new Exception("服务器接收错误");
                }
                connStatus.ForeColor = Color.Green;
                connStatus.Text = "服务器已连接";
                recvThread = new Thread(new ParameterizedThreadStart(Recv_Task));
                recvThread.Start();
                while (_Connect_Status)
                {
                    //Thread.Sleep(500);
                    sendResult = "";
                    isSend = false;
                    while (comPort.BytesToRead != 0)
                    {
                        var readByteStr = Convert.ToString(comPort.ReadByte(), 16);
                        if (readByteStr.Length == 1)
                        {
                            readByteStr = "0" + readByteStr;
                        }
                        sendResult += readByteStr + " ";
                        isSend = true;
                    }
                    if (isSend)
                    {
                        webSocket.Send(sendResult, OptType.SERIALDATA);
                    }


                }
                webSocket.Send(OptType.CLOSE);
                int count = 0;
                while (!webSocket.IsRecv && count < 100)
                {
                    count++;
                    Thread.Sleep(10);
                }
                webSocket.Close();
                if (webSocket.RecvedData != "ack")
                {
                    throw new Exception("关闭连接错误");
                }
                connStatus.ForeColor = Color.Black;
                connStatus.Text = "未连接";
            }
            catch (System.Exception ex)
            {
                
                connStatus.ForeColor = Color.Black;
                connStatus.Text = "连接失败";
            }
            finally
            {
                _Connect_Status = false;
                if (comPort.IsOpen)
                {
                    comPort.Close();
                }
            }
        }
        void Recv_Task(object sender)
        {    

            //心跳
            while (_Connect_Status)
            {
                Thread.Sleep(20);
                if (webSocket.IsRecv)
                {
                    Console.WriteLine(webSocket.RecvedData);
                }
            }

            //if (recvResult.Length != 0)
            //{
            //    foreach (string s in recvResult.Split(' '))
            //    {
            //        Console.WriteLine("{0:x}", Convert.ToByte(s, 16));
            //    }
            //}
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            clientConfig.SaveConfig();
        }
        private void 串口设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SerialConfig().ShowDialog();
        }

        private void 网络设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new UrlConfig().ShowDialog();
        }
    }
}
