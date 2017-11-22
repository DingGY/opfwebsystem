using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SerialClient
{
    public partial class UrlConfig : Form
    {
        public UrlConfig()
        {
            InitializeComponent();
        }

        private void URLYesButt_Click(object sender, EventArgs e)
        {
             MainForm.clientConfig.url = urlSetBox.Text;
             this.Close();
        }
        private void UrlConfig_Load(object sender, EventArgs e)
        {
            urlSetBox.Text = MainForm.clientConfig.url;
        }
    }
}
