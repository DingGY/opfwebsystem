using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Drawing;

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

        public string parse_frame(string frame)
        {
            //{BG}{ADDR}{BG}{STATUS-READ}{DATA LEN(USER+VAL1+VAL2))}{USER}{VAL1}[VAL2]{ED}
            return "";
        }
        /// <summary>
        /// read data from serial
        /// </summary>
        /// <param name="logic">task logic</param>
        /// <returns></returns>
        public bool read_data(Logic logic)
        {
            string frame_write = "";
            UpdateUI(string.Format("正在执行：{0}",logic.name), Color.Green);
            if (logic.isFE_begin)
            {
                frame_write += "FEFEFEFE";
            }
            frame_write += "68";
            if (logic.ischange_addr)
            {
                if (logic.address.Length == 12)
                {
                    frame_write += logic.address;
                }

            }
            frame_write += "68";
            return true;
        }
    }
}
