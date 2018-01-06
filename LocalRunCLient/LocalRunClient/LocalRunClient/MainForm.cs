using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
using LocalRunFunc;
using System.Reflection;
namespace LocalRunClient
{
    public partial class MainForm : Form
    {
        
        public List<RichTextBox> showInfoBoxList;
        public List<SerialPort> comList;
        public List<Thread> runnerList;
        public static Config clientConfig;
        public delegate void UIfunc(string text, Color colr, int index);
        public UIfunc UIDisplay;
        public MainForm()
        {
            InitializeComponent();
            clientConfig = Config.LoadConfig(@"LocalRunConfig.xml"); ;
            showInfoBoxList = new List<RichTextBox>();
            runnerList = new List<Thread>();
            comList = new List<SerialPort>();
            showInfoBoxList.Add(richTextBox1);
            showInfoBoxList.Add(richTextBox2);
            showInfoBoxList.Add(richTextBox3);
            comList.Add(new SerialPort());
            comList.Add(new SerialPort());
            comList.Add(new SerialPort());
            UIDisplay = new UIfunc(UpdateUI_Handle);

        }


        public void initComPort(int index)
        {
            comList[index].Close();
            comList[index].PortName = MainForm.clientConfig.comPortArr[index];
            comList[index].BaudRate = MainForm.clientConfig.comPortConfig.bound;
            comList[index].DataBits = 8;
            comList[index].Parity = MainForm.clientConfig.comPortConfig.parity;
            comList[index].ReadTimeout = MainForm.clientConfig.comPortConfig.comTimeout;
            comList[index].StopBits = MainForm.clientConfig.comPortConfig.stopBit;
            
        }
        public void UpdateUI_Handle(string text, Color colr, int index)
        {
            showInfoBoxList[index].SelectionFont = new Font("宋体", 15, FontStyle.Bold);
            showInfoBoxList[index].SelectionColor = colr;
            showInfoBoxList[index].SelectedText = text;
            showInfoBoxList[index].Focus();
            showInfoBoxList[index].Select(showInfoBoxList[0].TextLength, 0);
            showInfoBoxList[index].ScrollToCaret();
        }
        public void setRichText(string text,Color colr,int index)
        {
            this.BeginInvoke(UIDisplay, text, colr, index);
        }


        private void _runner(object sender)
        {
            int index = (int)(sender);
            var comm = new ClientComm(clientConfig.url);
            var runFunc = new RunFunc(index);
            runFunc.SetUI(setRichText);
            runFunc.SetIO(comList[index]);
            comm.send("get",MainForm.clientConfig.program);
            string json = comm.recv();
            Console.WriteLine(json);
            TaskConfig task = JsonConvert.DeserializeObject<TaskConfig>(json);
            initComPort(index);
            foreach (Logic logic in task.logic_list)
            {
                MethodInfo logicFunc = runFunc.GetType().GetMethod(logic.func);
                if (!(bool)(logicFunc.Invoke(runFunc, new object[] { logic })))
                {
                    break;
                }
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\r')
            {
                runnerList.Clear();
                for (int i = 0; i < 3;i++ )
                {
                    //the com port can be runed which has been seted
                    if (MainForm.clientConfig.comPortArr[i] == "None")
                    {
                        continue;
                    }
                    runnerList.Add(new Thread(new ParameterizedThreadStart(_runner)));
                    runnerList[i].Start(i);
                }
            }
            else
            {
                return;
            }
        }

        private void 串口设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SerialConfig().Show();

        }

        private void 网络设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new UrlConfig().Show();

        }

        private void 方案选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ProgrammeSelect().Show();
        }
    }
}
