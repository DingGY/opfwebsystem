using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;

namespace LocalRunFunc
{

    public class RunFunc
    {
        protected RunReturn _retVal;
        public delegate void UIfunc(string text, Color colr,int index);
        protected UIfunc _UIDisplay;
        protected SerialPort _ComPort;
        protected int index;

        public RunFunc(int index)
        {
            _retVal = new RunReturn();
            this.index = index;
            //test
            _retVal.address = "AAAAAAAAAAAA";
            
        }
        private void UpdateUI(string text, Color colr)
        {
            _UIDisplay(text, colr, index);
        }

        public void SetUI(UIfunc func)
        {
            _UIDisplay = new UIfunc(func);
        }
        public void SetIO(SerialPort com)
        {
            _ComPort = com;
        }
        protected byte[] commandResponseHandler()
        {

            int the68Pos = 0;
            int theAdderNum = 0;
            int maxReciveLength = 0;
            byte currentData;
            List<byte> recvList = new List<byte>();
             while (true)
            {
                try
                {
                    currentData = (byte)_ComPort.ReadByte();
                }
                 catch
                {
                    return recvList.ToArray();
                }
                // 接收字符
                recvList.Add(currentData);
                // 接收字头
                if (the68Pos == 0)
                {
                    if (currentData != 0x68)
                    {
                        continue;
                    }
                    the68Pos++;
                }
                // 收到协议头
                else if (the68Pos == 0x01)
                {
                    if (++theAdderNum >= 6)
                    {
                        the68Pos++;
                    }
                }
                // 地址收全
                else if (the68Pos == 0x02)
                {
                    the68Pos++;
                }
                else if (the68Pos == 0x03)
                {
                    // 真正字长 
                    maxReciveLength += 2;
                    the68Pos++;
                    // 反馈结果
                    if ((currentData & 0x40) != 0x40)
                    {
                        return recvList.ToArray();
                    }
                    else
                    {
                        return recvList.ToArray();
                    }
                    the68Pos = 0x04;
                }
                else if (the68Pos == 0x04)
                {
                    // 接收长度
                    maxReciveLength = currentData;
                    //maxReciveLength -= 2;
                    the68Pos = 0x05;
                }
                else if (the68Pos == 0x05)
                {
                    // 接收反馈数据
                    if (maxReciveLength-- == 0)
                    {
                        _ComPort.Close();
                        return recvList.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// read data from serial
        /// </summary>
        /// <param name="logic">task logic</param>
        /// <returns></returns>
        public bool read_data(Logic logic)
        {
            var frameInterpreter = new FrameParse();
            UpdateUI(string.Format("正在执行：{0}\n",logic.name), Color.Green);
            string frame_send = "";
            if (logic.isFE_begin)
            {
                frame_send = "FEFEFEFE";
            }
            if (!logic.ischange_addr)
            {
                logic.address = _retVal.address;
            }
            frame_send += logic.frame_set;
            frameInterpreter.SetLogic(logic);
            frame_send = frameInterpreter.parse_frame(frame_send);
            Console.WriteLine(frame_send);
            byte[] frame_sdbyte = Util.stringToByteArray(frame_send);
            if (!_ComPort.IsOpen)
            {
                _ComPort.Open();
            }
            _ComPort.Write(frame_sdbyte, 0, frame_sdbyte.Length);
            Thread.Sleep(logic.send_delay);
            byte[] frame_rdbyte;
            frame_rdbyte = commandResponseHandler();
            Thread.Sleep(logic.read_delay);
            UpdateUI(Util.byteArrayToString(frame_rdbyte,frame_rdbyte.Length),Color.Green);
            return _retVal.cmpStatus;
        }
    }
}
