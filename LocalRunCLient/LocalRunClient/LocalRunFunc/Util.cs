using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace LocalRunFunc
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Util
    {
        /// <summary>
        /// 二进制转为BCD码
        /// </summary>
        /// <param name="hex">十进制数</param>
        ///<returns>压缩BCD</returns>
        
        public static ushort[] fcstab = {
            0x0000, 0x1189, 0x2312, 0x329b, 0x4624, 0x57ad, 0x6536, 0x74bf,
            0x8c48, 0x9dc1, 0xaf5a, 0xbed3, 0xca6c, 0xdbe5, 0xe97e, 0xf8f7,
            0x1081, 0x0108, 0x3393, 0x221a, 0x56a5, 0x472c, 0x75b7, 0x643e,
            0x9cc9, 0x8d40, 0xbfdb, 0xae52, 0xdaed, 0xcb64, 0xf9ff, 0xe876,
            0x2102, 0x308b, 0x0210, 0x1399, 0x6726, 0x76af, 0x4434, 0x55bd,
            0xad4a, 0xbcc3, 0x8e58, 0x9fd1, 0xeb6e, 0xfae7, 0xc87c, 0xd9f5,
            0x3183, 0x200a, 0x1291, 0x0318, 0x77a7, 0x662e, 0x54b5, 0x453c,
            0xbdcb, 0xac42, 0x9ed9, 0x8f50, 0xfbef, 0xea66, 0xd8fd, 0xc974,
            0x4204, 0x538d, 0x6116, 0x709f, 0x0420, 0x15a9, 0x2732, 0x36bb,
            0xce4c, 0xdfc5, 0xed5e, 0xfcd7, 0x8868, 0x99e1, 0xab7a, 0xbaf3,
            0x5285, 0x430c, 0x7197, 0x601e, 0x14a1, 0x0528, 0x37b3, 0x263a,
            0xdecd, 0xcf44, 0xfddf, 0xec56, 0x98e9, 0x8960, 0xbbfb, 0xaa72,
            0x6306, 0x728f, 0x4014, 0x519d, 0x2522, 0x34ab, 0x0630, 0x17b9,
            0xef4e, 0xfec7, 0xcc5c, 0xddd5, 0xa96a, 0xb8e3, 0x8a78, 0x9bf1,
            0x7387, 0x620e, 0x5095, 0x411c, 0x35a3, 0x242a, 0x16b1, 0x0738,
            0xffcf, 0xee46, 0xdcdd, 0xcd54, 0xb9eb, 0xa862, 0x9af9, 0x8b70,
            0x8408, 0x9581, 0xa71a, 0xb693, 0xc22c, 0xd3a5, 0xe13e, 0xf0b7,
            0x0840, 0x19c9, 0x2b52, 0x3adb, 0x4e64, 0x5fed, 0x6d76, 0x7cff,
            0x9489, 0x8500, 0xb79b, 0xa612, 0xd2ad, 0xc324, 0xf1bf, 0xe036,
            0x18c1, 0x0948, 0x3bd3, 0x2a5a, 0x5ee5, 0x4f6c, 0x7df7, 0x6c7e,
            0xa50a, 0xb483, 0x8618, 0x9791, 0xe32e, 0xf2a7, 0xc03c, 0xd1b5,
            0x2942, 0x38cb, 0x0a50, 0x1bd9, 0x6f66, 0x7eef, 0x4c74, 0x5dfd,
            0xb58b, 0xa402, 0x9699, 0x8710, 0xf3af, 0xe226, 0xd0bd, 0xc134,
            0x39c3, 0x284a, 0x1ad1, 0x0b58, 0x7fe7, 0x6e6e, 0x5cf5, 0x4d7c,
            0xc60c, 0xd785, 0xe51e, 0xf497, 0x8028, 0x91a1, 0xa33a, 0xb2b3,
            0x4a44, 0x5bcd, 0x6956, 0x78df, 0x0c60, 0x1de9, 0x2f72, 0x3efb,
            0xd68d, 0xc704, 0xf59f, 0xe416, 0x90a9, 0x8120, 0xb3bb, 0xa232,
            0x5ac5, 0x4b4c, 0x79d7, 0x685e, 0x1ce1, 0x0d68, 0x3ff3, 0x2e7a,
            0xe70e, 0xf687, 0xc41c, 0xd595, 0xa12a, 0xb0a3, 0x8238, 0x93b1,
            0x6b46, 0x7acf, 0x4854, 0x59dd, 0x2d62, 0x3ceb, 0x0e70, 0x1ff9,
            0xf78f, 0xe606, 0xd49d, 0xc514, 0xb1ab, 0xa022, 0x92b9, 0x8330,
            0x7bc7, 0x6a4e, 0x58d5, 0x495c, 0x3de3, 0x2c6a, 0x1ef1, 0x0f78
            };
        public static byte ByteBin2Bcd(byte hex)
        {
            byte byteHi = (byte)(((hex / 10) % 10) << 4);
            byte byteLow = (byte)(hex % 10);
            byte returnByte = (byte)(byteHi | byteLow);
            return returnByte;
        }
        /// <summary>
        /// BCD码转为二进制
        /// </summary>
        /// <param name="bcd">压缩BCD数</param>
        /// <returns>压缩十进制</returns>
        public static byte ByteBcd2Bin(byte bcd)
        {
            byte hi = (byte)((bcd >> 4) * 10);
            byte low = (byte)(bcd & 0x0F);
            byte returnByte = (byte)(hi + low);
            return returnByte;
        }

        /// <summary>
        /// 计算校验和
        /// </summary>
        /// <param name="buff">校验和数组</param>
        /// <param name="n">长度</param>
        /// <returns>校验和</returns>
        public static byte GetBuffCs(byte[] buff, byte n, byte pos)
        {
            byte cs = 0;
            byte i;
            for (i = 0; i < n; i++)
            {
                cs += buff[i + pos];
            }
            return (cs);
        }
        /// <summary>
        /// 数组自动+NUM
        /// </summary>
        /// <param name="buff">操作数组</param>
        /// <param name="num">要累加的数</param>
        /// <param name="cnt">操作尺寸</param>
        /// <param name="beginPos">开始位置</param>
        public static void MemAddNum(ref byte[] buff, byte num, byte cnt, int beginPos)
        {
            byte len;
            for (len = 0; len < cnt; len++)
            {
                buff[beginPos] += num;
                beginPos++;
            }
        }
        /// <summary>
        /// 数组自动减去NUM
        /// </summary>
        /// <param name="buff">操作数组</param>
        /// <param name="num">要减去的数</param>
        /// <param name="cnt">操作尺寸</param>
        /// <param name="beginPos">起始位置</param>
        public static void MemSubNum(ref byte[] buff, byte num, int cnt, int beginPos)
        {
            byte len;
            for (len = 0; len < cnt; len++)
            {
                buff[beginPos] -= num;
                beginPos++;
            }
        }
        /// <summary>
        /// 字符串倒序
        /// </summary>
        /// <param name="text">要改变的字符串</param>
        /// <returns>字符串</returns>
        public static string ReverseStr(string text)
        {
            char[] charArray = text.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        /// <summary>
        /// 字符串倒序
        /// </summary>
        /// <param name="text">要改变的字符串</param>
        /// <returns>字符串</returns>
        public static string ReverseStrByByte(string text)
        {
            var charArray = Util.stringToByteArray(text);
            Array.Reverse(charArray);
            return Util.byteArrayToString(charArray,charArray.Length);
        }
        /// <summary>
        /// 字符转byte值
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>无符号数</returns>
        public static byte charToByte(char value)
        {
            byte returnByte;
            if (value >= 'A')
            {
                value -= 'A';
                returnByte = 10;
                returnByte += (byte)value;
            }
            else
            {
                value -= '0';
                returnByte = 0;
                returnByte += (byte)value;
            }

            return returnByte;
        }
        public static string GetCsByStr(string buff)
        {
            //List<byte> bytelist = new List<byte>();
            //for (int i = 0; i < (buff.Length / 2); i += 2)
            //{
            //    bytelist.Add(Convert.ToByte(buff.Substring(i, 2), 16));
            //}
            var bs = stringToByteArray(buff);
            byte cs = 0;
            foreach (byte b in bs)
            {
                cs += b;
            }
            return byteToString(cs);
        }
        /// <summary>
        /// hex 转到字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string byteToString(byte value)
        {
            char[] str = new char[2];
            byte hi = (byte)(value >> 4);
            byte low = (byte)(value & 0x0F);
            byte changeValue = 0x30;

            if (hi >= 10)
            {
                changeValue = 65;
                hi -= 10;
                changeValue += hi;
            }
            else
            {
                changeValue = 0x30;
                changeValue += hi;
            }
            str[0] = (char)changeValue;

            if (low >= 10)
            {
                changeValue = 65;
                low -= 10;
                changeValue += low;
            }
            else
            {
                changeValue = 0x30;
                changeValue += low;
            }
            str[1] = (char)changeValue;

            return new string(str);
        }
        /// <summary>
        /// 非压缩BCD到压缩BCD
        /// </summary>
        /// <param name="nozipBcd">非压缩BCD</param>
        /// <param name="zipBcd">压缩BCD</param>
        /// <param name="length">非压缩BCD长度</param>
        public static void noZipBcdToZipBcd(byte[] nozipBcd, ref byte[] zipBcd, int length)
        {
            int i = 0;
            int pos = 0;
            foreach (byte value in nozipBcd)
            {

                byte trulyValue = value;
                if (trulyValue >= 0x41)
                {
                    trulyValue -= 0x41;
                    trulyValue += 10;
                }
                else if (trulyValue >= 0x30)
                {
                    trulyValue -= 0x30;
                }
                i++;
                if (i == 1)
                {
                    zipBcd[pos] = (byte)(trulyValue << 4);
                }
                else
                {
                    zipBcd[pos] |= (trulyValue);
                    i = 0;
                    pos++;
                }
            }
        }
        /// <summary>
        /// 字符串转为BYTE
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static byte theStrToByte(String str)
        {

            String optStr = str.ToUpper();
            int pos = 0;
            byte returnByte = 0;

            if (optStr.Length == 0x01)
            {
                optStr = "0" + optStr;
            }

            foreach (char vl in optStr)
            {
                char value = vl;
                byte optbyte;
                pos++;
                if (value >= 'A')
                {
                    value -= 'A';
                    optbyte = 10;
                    optbyte += (byte)value;
                }
                else
                {
                    value -= '0';
                    optbyte = 0;
                    optbyte += (byte)value;
                }

                if (pos == 1)
                {
                    returnByte = (byte)(optbyte << 4);
                }
                else
                {
                    returnByte += optbyte;
                }

            }
            return returnByte;
        }
        public static string Add33ToStr(string s)
        {
            var bs = stringToByteArray(s);
            for (int i = 0; i < bs.Length;i++ )
            {
                bs[i] += 0x33;
            }
            return byteArrayToString(bs, bs.Length);
        }
        public static string BcdFormateReverse(string text)
        {
            StringBuilder note = new StringBuilder();

            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            int length = text.Length;

            for (int i = length; i > 0; i -= 2)
            {
                note.Append(text.Substring(i - 2, 2));
            }
            return note.ToString();
        }

        /// <summary>
        ///  获得每月的天数
        /// </summary>
        /// <param name="mm">月</param>
        /// <param name="yy">年</param>
        /// <returns>天数</returns>
        static byte GetMaxDay(byte mm, byte yy)
        {
            byte[] MONTH_MAX_DAY = new byte[12];
            byte day = 31;
            //{31,28,31,30,31,30,31,31,30,31,30,31};
            MONTH_MAX_DAY[0] = 31;
            MONTH_MAX_DAY[1] = 28;
            MONTH_MAX_DAY[2] = 31;
            MONTH_MAX_DAY[3] = 30;
            MONTH_MAX_DAY[4] = 31;
            MONTH_MAX_DAY[5] = 30;
            MONTH_MAX_DAY[6] = 31;
            MONTH_MAX_DAY[7] = 31;
            MONTH_MAX_DAY[8] = 30;
            MONTH_MAX_DAY[9] = 31;
            MONTH_MAX_DAY[10] = 31;
            MONTH_MAX_DAY[11] = 31;

            if (mm < 12)
            {
                day = MONTH_MAX_DAY[mm - 1];
                //表计的寿命只有十年，可采用此算法，否则要考虑能被100整除不能被400整除的情况
                if ((mm == 2) && ((yy % 4) == 0))
                {
                    day++;
                }
            }
            return (day);
        }

        /// <summary>
        /// 获得从2000年到现在天数(排除当前日)
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>天数</returns>
        static int GetDaysFromYearStart(byte[] time)
        {
            byte i = 0, month = 0, day = 0;
            int result = 0;

            month = ByteBcd2Bin(time[1]);
            day = ByteBcd2Bin(time[0]);

            for (i = 1; i < month; i++)
            {
                result += (int)(GetMaxDay(i, ByteBcd2Bin(time[2])));
            }

            result += (int)(day - 1);
            return (result);
        }

        /// <summary>
        /// 从2000年到现在的小时数
        /// </summary>
        static long GetHoursFromYearStart(byte[] time)
        {
            byte hour = 0;
            long result = 0;
            byte[] changeTime = new byte[4];

            hour = ByteBcd2Bin(time[0]);
            for (int i = 0; i < 3; i++)
            {
                changeTime[i] = time[1 + i];
            }
            //获取天数 * 24
            result = GetDaysFromYearStart(changeTime) * 24u;	

            result += hour;

            return (result);
        }

        /// <summary>
        /// 2000年到现在的秒数
        /// </summary>
        /// <param name="buff">时间字符串</param>
        /// <returns></returns>
        public static long GetSecFrom2000(byte[] buff)
        {
            byte[] time = new byte[6];
            byte[] secondSet = new byte[4];
            byte i = 0;
            long seconds = 0;

            //memcpy(time, buff, 6);

            for (i = 0; i < 6; i++)
            {
                time[i] = buff[i];
            }

            for (i = 0; i < 4; i++)
            {
                secondSet[i] = time[2 + i];
            }

            seconds += ((long)GetHoursFromYearStart(secondSet) * 3600);
            seconds += ((long)ByteBcd2Bin(time[1]) * 60);
            seconds += ((long)ByteBcd2Bin(time[0]));

            for (i = 0; i < ByteBcd2Bin(time[5]); i++)
            {
                if ((i % 4) == 0)
                {
                    seconds += ((long)366 * 24 * 3600);
                }
                else
                {
                    seconds += ((long)365 * 24 * 3600);
                }
            }

            return (seconds);
        }
        public static ushort calcFCS(byte[] cp, int start,int len)
        {
            ushort fcs = 0xffff;
            for (int i = 0; i < len;i++ )
            {
                fcs = (ushort)((fcs >> 8) ^ fcstab[(fcs ^ cp[i + start]) & 0xff]);
            }
            fcs ^= 0xffff;
            return fcs;
        }

        public static void tryfcs16(byte[] cp, int start,int len)
        {
            ushort trialfcs;
            /* add on output */
            trialfcs = calcFCS(cp, start, len);
            trialfcs ^= 0xffff; /* complement */
            cp[len]=(byte)(trialfcs & 0x00ff); /* least significant byte first */
            cp[len + 1] = (byte)((trialfcs >> 8) & 0x00ff);
            /* check on input */
            trialfcs = calcFCS(cp, start, len + 2);
            //if (trialfcs == 0xf0b8)
            //    Console.WriteLine("Good FCS\n");
        }
        public static byte[] stringToByteArray(string s)
        {
            List<byte> b = new List<byte>();
            for (int i = 0; i < s.Length/2; i++)
            {
                b.Add(Convert.ToByte(s.ToLower().Substring(i * 2, 2), 16));
            }
            return b.ToArray();
        }
        public static void stringToByteArray(ref byte[] b, string s, int len)
        {
            for (int i = 0; i < len; i++)
            {
                b[i] = Convert.ToByte(s.ToLower().Substring(i * 2, 2), 16);
            }
        }
        public static string byteArrayToString(byte[] b, int len)
        {
            string s = "";
            for (int i = 0; i < len; i++)
            {
                if (b[i] == 0)
                {
                    s += "00";
                }
                else if (b[i] <= 0x0f)
                {
                    s += "0" + Convert.ToString(b[i], 16).ToUpper();
                }
                else
                {
                    s += Convert.ToString(b[i], 16).ToUpper();
                }

            }
            return s;
        }
        public void outputByte(byte[] by)
        {
            FileStream fs = new FileStream(@"E:\项目\上位机\698单相资产管理编号\698单相资产管理编号\222.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            foreach (byte i in by)
            {
                if (i <= 0x0f)
                {
                    string st = "0" + Convert.ToString(i, 16).ToUpper();
                    sw.Write(" {0}", st);
                }
                else
                {
                    sw.Write(" {0}", Convert.ToString(i, 16).ToUpper());
                }
            }
            sw.WriteLine();
            sw.Flush();
            sw.Close();
            fs.Close();
        }
    }
}
