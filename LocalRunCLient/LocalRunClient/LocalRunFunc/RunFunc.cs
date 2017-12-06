using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Drawing;
using System.Text.RegularExpressions;

namespace LocalRunFunc
{

    public class RunFunc
    {
        protected RunReturn _retVal;
        protected Logic _logic;
        public delegate void UIfunc(string text, Color colr,int index);
        protected UIfunc _UIDisplay;
        protected SerialPort _ComPort;
        protected int index;

        public RunFunc(int index)
        {
            _retVal = new RunReturn();
            _logic = new Logic();
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

        public string reg_replace(string patt, string input, string repstr)
        {
            var replaced = Regex.Replace(input, patt, repstr);
            return replaced;
        }
        //get logic value by index
        public string Get_LV(string index)
        {
            if (index == "VAL0")
            {
                return _logic.val0;
            }
            else if (index == "VAL1")
            {
                return _logic.val1;
            }
            else if (index == "VAL2")
            {
                return _logic.val2;
            }
            else if (index == "VAL3")
            {
                return _logic.val3;
            }
            else if (index == "VAL4")
            {
                return _logic.val4;
            }
            else if (index == "VAL5")
            {
                return _logic.val5;
            }
            else if (index == "VAL6")
            {
                return _logic.val6;
            }
            else if (index == "VAL7")
            {
                return _logic.val7;
            }
            else if (index == "VAL8")
            {
                return _logic.val8;
            }
            else if (index == "VAL9")
            {
                return _logic.val9;
            }
            else if (index == "VAL9")
            {
                return _logic.val9;
            }
            else if (index == "ADDR")
            {

                    return _logic.address;

            }
            return "";
        }
        public string reg_operate(string input)
        {
            //parse frame operate and conculate the result
            MatchCollection opt_match = Regex.Matches(input, @"(?<opt>[\+|\-|\*|/|%])", RegexOptions.ExplicitCapture);
            MatchCollection val_match = Regex.Matches(input, @"(?<val>\w+)", RegexOptions.ExplicitCapture);
            int index = 0;
            int opt_result = int.Parse(Get_LV(val_match[index].Groups["val"].Value));
            foreach (Match s in opt_match)
            {
                if (s.Groups["opt"].Value == "+")
                {
                    opt_result += int.Parse(Get_LV(val_match[++index].Groups["val"].Value));
                }
                if (s.Groups["opt"].Value == "-")
                {
                    opt_result -= int.Parse(Get_LV(val_match[++index].Groups["val"].Value));
                }
                if (s.Groups["opt"].Value == "*")
                {
                    opt_result *= int.Parse(Get_LV(val_match[++index].Groups["val"].Value));
                }
                if (s.Groups["opt"].Value == "/")
                {
                    opt_result /= int.Parse(Get_LV(val_match[++index].Groups["val"].Value));
                }
                if (s.Groups["opt"].Value == "%")
                {
                    opt_result %= int.Parse(Get_LV(val_match[++index].Groups["val"].Value));
                }

            }
            return "";
        }
        public string reg_callen(string input)
        {
            //parse frame operate and conculate the result
            MatchCollection opt_match = Regex.Matches(input, @"(?<opt>[\+|\-|\*|/|%])", RegexOptions.ExplicitCapture);
            MatchCollection val_match = Regex.Matches(input, @"(?<val>\w+)", RegexOptions.ExplicitCapture);
            int index = 0;
            int opt_result = Get_LV(val_match[index].Groups["val"].Value).Length;
            foreach (Match s in opt_match)
            {
                if (s.Groups["opt"].Value == "+")
                {
                    opt_result += Get_LV(val_match[++index].Groups["val"].Value).Length;
                }
                if (s.Groups["opt"].Value == "-")
                {
                    opt_result -= Get_LV(val_match[++index].Groups["val"].Value).Length;
                }
                if (s.Groups["opt"].Value == "*")
                {
                    opt_result *= Get_LV(val_match[++index].Groups["val"].Value).Length;
                }
                if (s.Groups["opt"].Value == "/")
                {
                    opt_result /= Get_LV(val_match[++index].Groups["val"].Value).Length;
                }
                if (s.Groups["opt"].Value == "%")
                {
                    opt_result %= Get_LV(val_match[++index].Groups["val"].Value).Length;
                }

            }
            return opt_result.ToString();
        }
        public string parse_frame(string frame)
        {
            //operate parse
            MatchCollection len_match = Regex.Matches(frame, @"LEN\((?<valopt>.*?)\)", RegexOptions.ExplicitCapture);
            string frame_len = reg_callen(len_match[0].Groups["valopt"].Value);
            Console.WriteLine(frame_len);
            //replace parse
            var frame_pattern = new string[][] { 
            new string[]{@"(\[BG\])","CS"},
            new string[]{@"(\[ED\])","68"},
            new string[]{@"(\[ADDR\])",Get_LV("ADDR")},
            new string[]{@"(\[VAL0\])",Get_LV("VAL0")}, 
            new string[]{@"(\[VAL1\])",Get_LV("VAL1")}, 
            new string[]{@"(\[VAL2\])",Get_LV("VAL2")}, 
            new string[]{@"(\[VAL3\])",Get_LV("VAL3")}, 
            new string[]{@"(\[VAL4\])",Get_LV("VAL4")}, 
            new string[]{@"(\[VAL5\])",Get_LV("VAL5")}, 
            new string[]{@"(\[VAL6\])",Get_LV("VAL6")}, 
            new string[]{@"(\[VAL7\])",Get_LV("VAL7")}, 
            new string[]{@"(\[VAL8\])",Get_LV("VAL8")}, 
            new string[]{@"(\[VAL9\])",Get_LV("VAL9")}, 
            new string[]{@"(\[LEN.*?\])",frame_len},
            };

            string pframe = frame;
            foreach (var s in frame_pattern)
            {
                pframe = reg_replace(s[0], pframe, s[1]);
            }
            //replace CS parse
            return pframe;

        }
        /// <summary>
        /// read data from serial
        /// </summary>
        /// <param name="logic">task logic</param>
        /// <returns></returns>
        public bool read_data(Logic logic)
        {
            string frame_write = "";
            this._logic = logic;
            UpdateUI(string.Format("正在执行：{0}",logic.name), Color.Green);
            Console.WriteLine(parse_frame(logic.frame_set));
            //if (logic.isFE_begin)
            //{
            //    frame_write += "FEFEFEFE";
            //}
            //frame_write += "68";
            //if (logic.ischange_addr)
            //{
            //    if (logic.address.Length == 12)
            //    {
            //        frame_write += logic.address;
            //    }
            //}
            //frame_write += "68";
            return true;
        }
    }
}
