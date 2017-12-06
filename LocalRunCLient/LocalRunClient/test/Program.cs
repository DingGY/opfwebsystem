using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace test
{
    class Program
    {

        public string reg_replace(string patt, string input,string repstr)
        {
            Regex reg = new Regex(patt);
            string replaced = reg.Replace(input, repstr);
            return replaced;
        }
        public string Get_LV(string index)
        {
            if (index == "VAL0")
            {
                return "1";
            }
            else if (index == "VAL1")
            {
                return "1";
            }
            else if (index == "VAL2")
            {
                return "2";
            }
            return "";
        }
        public string reg_operate(string input)
        {
            //parse frame operate and conculate the result
            List<string> val_list = new List<string>();
            Regex reg_opt = new Regex(@"(?<opt>[\+|\-|\*|/|%])", RegexOptions.ExplicitCapture);
            Regex reg_val = new Regex(@"(?<val>\w+)", RegexOptions.ExplicitCapture);
            MatchCollection opt_match = reg_opt.Matches(input);
            MatchCollection val_match = reg_val.Matches(input);
            Console.WriteLine(val_match[2]);
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
        public string parse_frame(string frame)
        {
            //operate parse
            var reg_len = Regex.Matches(frame, @"LEN\((?<name>.*?)\)",RegexOptions.ExplicitCapture);
            foreach (Match s in reg_len)
            {
                reg_operate(s.Groups["name"].Value);
            }
            //replace parse
            var frame_pattern = new string[][] { 
            new string[]{@"(\[BG\])","CS"},
            new string[]{@"(\[ED\])","68"},
            new string[]{@"(\[ADDR\])","AAAAAAAAAAAA"},
            new string[]{@"(\[VAL0\])","000"}, 
            new string[]{@"(\[CS\])","66"},
            new string[]{@"(\[LEN.*?\])","77"},
            };

            string pframe = frame;
            foreach (var s in frame_pattern)
            {
                pframe = reg_replace(s[0], pframe, s[1]);
            }
            //replace CS parse
            return pframe;
            
        }
        static void Main(string[] args)
        {
            var p = new Program();
            string input = "[BG][ADDR][BG]11[LEN(VAL0+VAL1+VAL2)][VAL0][CS][ED]";
            Console.WriteLine(p.parse_frame(input));
            Console.ReadKey();
        }
    }
}
