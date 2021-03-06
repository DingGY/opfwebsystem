﻿using System;
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
        protected Logic _logic;
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
        protected Frame645 comRead()
        {

            int the68Pos = 0;
            int theAdderNum = 0;
            int maxReciveLength = 0;
            byte currentData;
            List<byte> recvList = new List<byte>();
            Frame645 frame = new Frame645();
             while (true)
            {
                try
                {
                    currentData = (byte)_ComPort.ReadByte();
                }
                 catch
                {
                    return frame;
                }
                // 接收字符
                
                // 接收字头
                if (the68Pos == 0)
                {
                    if (currentData != 0x68)
                    {
                        continue;
                    }
                    recvList.Add(currentData);
                    the68Pos++;
                }
                // 收到协议头
                else if (the68Pos == 0x01)
                {
                    recvList.Add(currentData);
                    frame.address += Util.byteToString(currentData);
                    if (++theAdderNum >= 6)
                    {
                        frame.address = Util.ReverseStrByByte(frame.address);
                        the68Pos++;
                    }
                }
                // 地址收全
                else if (the68Pos == 0x02)
                {
                    the68Pos++;
                    recvList.Add(currentData);
                }
                else if (the68Pos == 0x03)
                {
                    // 反馈结果
                    frame.status_word = Util.byteToString(currentData);
                    the68Pos = 0x04;
                    recvList.Add(currentData);
                }
                else if (the68Pos == 0x04)
                {
                    // 接收长度
                    //连同结束符一块接收
                    frame.len = Util.byteToString(currentData);
                    maxReciveLength = currentData;
                    //maxReciveLength -= 2;
                    if (maxReciveLength == 0)
                    {
                        the68Pos = 6;
                    } 
                    else
                    {
                        the68Pos = 5;
                    }
                    
                    recvList.Add(currentData);
                }
                else if (the68Pos == 0x05)
                {
                    frame.data += Util.byteToString((byte)(currentData - 0x33));
                    recvList.Add(currentData);
                    // 接收反馈数据
                    if (maxReciveLength-- == 1)
                    {
                        frame.data = Util.ReverseStrByByte(frame.data);
                        the68Pos = 0x06;
                    }
                    
                }
                else if (the68Pos == 6)
                {
                    //cs
                    frame.cs = Util.byteToString(currentData);
                    recvList.Add(currentData);
                    the68Pos = 0x07;
                }
                 else if(the68Pos == 7)
                {
                     //end
                    recvList.Add(currentData);
                    _ComPort.Close();
                    
                    frame.recv_data = Util.byteArrayToString(
                        recvList.ToArray(), recvList.ToArray().Length
                        );
                    frame.isFinish = true;
                    return frame;
                }
            }
        }
        protected bool comWrite()
        {
            
            string frame_send = "";
            if (!_logic.ischange_addr)
            {
                _logic.address = _retVal.address;
            }
            var frameInterpreter = new FrameParse(_logic);
            if (_logic.isFE_begin)
            {
                frame_send = "FEFEFEFE";
            }
            try
            {
                frame_send += frameInterpreter.parse_frame(_logic.frame_set);
                Console.WriteLine(frame_send);
                byte[] frame_sdbyte = Util.stringToByteArray(frame_send);
                Thread.Sleep(_logic.send_delay * 1000);
                if (!_ComPort.IsOpen)
                {
                    _ComPort.Open();
                }
                _ComPort.Write(frame_sdbyte, 0, frame_sdbyte.Length);
                Thread.Sleep(_logic.read_delay*1000);
            }
            catch (System.Exception ex)
            {
                _ComPort.Close();
                return false;
            }
            return true;
        }
        /// <summary>
        /// read data from serial
        /// </summary>
        /// <param name="logic">task logic</param>
        /// <returns></returns>
        public bool read_data(Logic logic)
        {
            this._logic = logic;
            UpdateUI(
                string.Format("正在执行：{0}\n",logic.name), 
                Color.Green
                );
            comWrite();
            var frame_read = comRead();
            if (!frame_read.isFinish)
            {
                return false;
            }
            UpdateUI(
                string.Format("{0}：{1}\n", logic.display_msg,frame_read.getNoTagData),
                Color.Green
                );
            return true;
        }
        /// <summary>
        /// read data from serial
        /// </summary>
        /// <param name="logic">task logic</param>
        /// <returns></returns>
        public bool read_addr(Logic logic)
        {
            this._logic = logic;
            UpdateUI(
                string.Format("正在执行：{0}\n", logic.name),
                Color.Green
                );
            comWrite();
            var frame_read = comRead();
            if (!frame_read.isFinish)
            {
                return false;
            }
            _retVal.address = frame_read.address;
            UpdateUI(
                string.Format("{0}：{1}\n", logic.display_msg, frame_read.address),
                Color.Green
                );
            return true;
        }
        /// <summary>
        /// read data from serial
        /// </summary>
        /// <param name="logic">task logic</param>
        /// <returns></returns>
        public bool cmp_data(Logic logic)
        {
            this._logic = logic;
            UpdateUI(
                string.Format("正在执行：{0}\n", logic.name),
                Color.Green
                );
            comWrite();
            var frame_read = comRead();
            if (!frame_read.isFinish)
            {
                return false;
            }
            UpdateUI(
                string.Format("{0}：{1}\n", logic.display_msg, frame_read.getNoTagData),
                Color.Green
                );
            if (logic.val1 == frame_read.getNoTagData)
            {
                _retVal.cmpStatus = true;
                UpdateUI(
                    string.Format("匹配 {0}：{1}\n", logic.name, logic.display_msg),
                    Color.Green
                );
            }
            else
            {
                _retVal.cmpStatus = false;
                UpdateUI(
                    string.Format("不匹配 {0}：{1}\n", logic.name, logic.display_msg),
                    Color.Red
                );
            }
            return _retVal.cmpStatus;
        }
        /// <summary>
        /// read data from serial
        /// </summary>
        /// <param name="logic">task logic</param>
        /// <returns></returns>
        public bool set_data(Logic logic)
        {
            this._logic = logic;
            UpdateUI(
                string.Format("正在执行：{0}\n", logic.name),
                Color.Green
                );
            comWrite();
            var frame_read = comRead();
            if (!frame_read.isFinish)
            {
                return false;
            }
            UpdateUI(
                string.Format("{0}\n", logic.display_msg),
                Color.Green
                );
            _retVal.cmpStatus = true;
            return _retVal.cmpStatus;
        }
        /// <summary>
        /// read data from serial
        /// </summary>
        /// <param name="logic">task logic</param>
        /// <returns></returns>
        public bool boardcast(Logic logic)
        {
            this._logic = logic;
            UpdateUI(
                string.Format("正在执行：{0}\n", logic.name),
                Color.Green
                );
            comWrite();
            UpdateUI(
                string.Format("{0}\n", logic.display_msg),
                Color.Green
                );
            _retVal.cmpStatus = true;
            return _retVal.cmpStatus;
        }
    }
}
