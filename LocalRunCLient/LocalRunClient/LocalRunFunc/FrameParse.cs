using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LocalRunFunc
{
    public class FrameParse
    {
        protected Logic _logic;
        public FrameParse()
        {
        }
        public FrameParse(Logic logic):base()
        {
            _logic = logic;
        }
        //public string reg_replace(string patt, string input, string repstr)
        //{
        //    var replaced = Regex.Replace(input, patt, repstr);
        //    return replaced;
        //}
        //get logic value by index

        public void SetLogic(Logic logic)
        {
            _logic = logic;
        }
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
        public int reg_operate(string input)
        {
            //parse frame operate and conculate the result
            MatchCollection opt_match = Regex.Matches(input, @"(?<opt>[\+|\-|\*|/|%])", RegexOptions.ExplicitCapture);
            MatchCollection val_match = Regex.Matches(input, @"(?<val>\w+)", RegexOptions.ExplicitCapture);
            int index = 0;
            int opt_result = int.Parse(Get_LV(val_match[index].Groups["val"].Value));
            foreach (Match s in opt_match)
            {
                opt_result = calcOpt(
                    s.Groups["opt"].Value,
                    opt_result,
                    int.Parse(Get_LV(val_match[++index].Groups["val"].Value))
                    );
            }
            return opt_result;
        }
        public int calcOpt(string opt, int val0, int val2)
        {
            int val1 = val0;

            if (opt == "+")
            {
                val1 += val2;
            }
            if (opt == "-")
            {
                val1 -= val2;
            }
            if (opt == "*")
            {
                val1 *= val2;
            }
            if (opt == "/")
            {
                val1 /= val2;
            }
            if (opt == "%")
            {
                val1 %= val2;
            }
            return val1;
        }
        public byte reg_calclen(string input)
        {
            //parse frame operate and conculate the result
            MatchCollection opt_match = Regex.Matches(input, @"(?<opt>[\+|\-|\*|/|%])", RegexOptions.ExplicitCapture);
            MatchCollection val_match = Regex.Matches(input, @"(?<val>\w+)", RegexOptions.ExplicitCapture);
            int index = 0;
            int opt_result = Get_LV(val_match[index].Groups["val"].Value).Length;
            foreach (Match s in opt_match)
            {
                opt_result = calcOpt(
                    s.Groups["opt"].Value,
                    opt_result,
                    Get_LV(val_match[++index].Groups["val"].Value).Length
                    );
            }
            return (byte)(opt_result / 2);
        }
        public string reg_calcCS(string input)
        {
            Regex cs_match = new Regex(@"(?<csbytes>\w*?)\[CS\]");
            Regex csrep_match = new Regex(@"\[CS\]");
            string result = input;
            while (cs_match.IsMatch(result))
            {
                result = csrep_match.Replace(
                    result,
                    Util.GetCsByStr(cs_match.Match(result).Groups["csbytes"].Value),
                    1);
            }
            return result;
        }
        public string reg_add33(string input)
        {
            Regex add_match = new Regex(@"\[ADD\((?<add>\w*?)\)\]");
            Regex csrep_match = new Regex(@"\[addrep\(.*?\)\]");
            string result = input;
            //Console.WriteLine(add_match.Match(result).Groups["add"].Value);
            
            while (add_match.IsMatch(result))
            {
                result = csrep_match.Replace(
                    result,
                    Util.GetCsByStr(add_match.Match(result).Groups["csbytes"].Value),
                    1);
            }
            return result;
        }

        public string parse_frame(string frame)
        {
            //operate parse
            MatchCollection len_match = Regex.Matches(frame, @"LEN\((?<valopt>.*?)\)", RegexOptions.ExplicitCapture);
            string frame_len = Util.byteToString(reg_calclen(len_match[0].Groups["valopt"].Value));
            //replace parse
            var frame_pattern = new string[][] { 
            new string[]{@"(\[BG\])","68"},
            new string[]{@"(\[ED\])","16"},
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
                pframe = Regex.Replace(pframe, s[0], s[1]);
            }
            //add 33 byte
            reg_add33(pframe);
            //replace CS parse
            pframe = reg_calcCS(pframe);
            return pframe;
        }
    }
}
