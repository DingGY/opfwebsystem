using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LocalRunClient
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
             MainForm.clientConfig.encryptionIP = encryptionIPText.Text;
             MainForm.clientConfig.encryptionPort = encryptionPortText.Text;
             this.Close();
        }
        private void UrlConfig_Load(object sender, EventArgs e)
        {
            urlSetBox.Text = MainForm.clientConfig.url;
            encryptionIPText.Text = MainForm.clientConfig.encryptionIP;
            encryptionPortText.Text = MainForm.clientConfig.encryptionPort;
        }
    }
}
