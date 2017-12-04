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


namespace LocalRunClient
{
    public partial class MainForm : Form
    {
        
        public List<RichTextBox> showInfoBoxList;
        public List<Thread> runnerList;
        public static Config clientConfig;
        public MainForm()
        {
            InitializeComponent();
            clientConfig = Config.LoadConfig(@"LocalRunConfig.xml"); ;
            showInfoBoxList = new List<RichTextBox>();
            runnerList = new List<Thread>();
            showInfoBoxList.Add(richTextBox1);
            showInfoBoxList.Add(richTextBox2);
            showInfoBoxList.Add(richTextBox3);
            foreach (RichTextBox box in showInfoBoxList)
            {
                box.SelectionFont = new Font("宋体",15,FontStyle.Bold);
                box.SelectionColor = Color.Green;
            }
            setRichText("给我一杯忘情水", Color.Green,2);
        }



        public void setRichText(string text,Color colr,int index)
        {
            showInfoBoxList[index].SelectionColor = colr;
            showInfoBoxList[index].AppendText(text);
        }


        void runner(object sender)
        {
            int index = (int)(sender);
            var comm = new ClientComm(clientConfig.url);
            comm.send("get",MainForm.clientConfig.program);
            string json = comm.recv();
            Console.WriteLine(json);
            TaskConfig task = JsonConvert.DeserializeObject<TaskConfig>(json);
            foreach (tt s in task.ll)
            {
                Console.WriteLine(s.name);
            }
            //JsonReader reader = new JsonTextReader(new StringReader());
            //while (reader.Read())
            //{
            //    Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
            //}
            return;
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\r')
            {
                runnerList.Clear();
                for (int i = 0; i < 3;i++ )
                {
                    
                    runnerList.Add(new Thread(new ParameterizedThreadStart(runner)));
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
            var form = new SerialConfig();
            form.Show();
        }

        private void 网络设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new UrlConfig();
            form.Show();
        }

        private void 方案选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ProgrammeSelect();
            form.Show();
        }
    }
}
