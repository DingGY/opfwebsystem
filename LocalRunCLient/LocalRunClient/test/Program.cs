using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LocalRunFunc;

namespace test
{
    class Program
    {

        static void Main(string[] args)
        {

            //var p = new FrameParse();
            string input = "[BG][ADDR][BG]11[LEN(VAL0+VAL1+VAL2)][VAL0][CS][ED]";
            //Console.WriteLine(p.parse_frame(input));
            Console.WriteLine(Convert.ToByte("ff",16));
            Console.ReadKey();
        }
    }
}
