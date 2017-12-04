using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace LocalRunClient
{
    public partial class ProgrammeSelect : Form
    {
        ClientComm comm;
        public ProgrammeSelect()
        {
            InitializeComponent();
            
        }

        private void ProgrammeSelect_Load(object sender, EventArgs e)
        {
            programeSelectBox.Text = MainForm.clientConfig.program;
            comm = new ClientComm(MainForm.clientConfig.url);
            reflashPrograme();
            programeSelectBox.Text = MainForm.clientConfig.program;
        }

        private void programSave_Click(object sender, EventArgs e)
        {
            MainForm.clientConfig.program = programeSelectBox.Text;
            this.Close();
        }

        private void programReflash_Click(object sender, EventArgs e)
        {
            reflashPrograme();
        }

        private void reflashPrograme()
        {
            programeSelectBox.Items.Clear();
            comm.send("reflash", "");
            JsonReader reader = new JsonTextReader(new StringReader(comm.recv()));
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.String)
                {
                    programeSelectBox.Items.Add(reader.Value);
                }
                else if (reader.TokenType == JsonToken.EndArray)
                {
                    break;
                }
            }
        }
    }
}
