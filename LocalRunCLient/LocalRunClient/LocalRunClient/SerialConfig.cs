﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace LocalRunClient
{
    public partial class SerialConfig : Form
    {
        public SerialConfig()
        {
            InitializeComponent();
        }

        private void SerialConfig_Load(object sender, EventArgs e)
        {
            string[] portList = SerialPort.GetPortNames();
            foreach (string iterm in portList)
            {
                SerialSelectBox1.Items.Add(iterm);
                SerialSelectBox2.Items.Add(iterm);
                SerialSelectBox3.Items.Add(iterm);
            }
            SerialSelectBox1.Items.Add("None");
            SerialSelectBox2.Items.Add("None");
            SerialSelectBox3.Items.Add("None");
            SerialSelectBox1.Text = MainForm.clientConfig.comPortArr[0];
            SerialSelectBox2.Text = MainForm.clientConfig.comPortArr[1];
            SerialSelectBox3.Text = MainForm.clientConfig.comPortArr[2];
            string[] parityList = { Parity.Even.ToString(), Parity.Odd.ToString(), Parity.None.ToString() };
            foreach (string iterm in parityList)
            {
                ParitySelectBox.Items.Add(iterm);
            }

            ParitySelectBox.Text = MainForm.clientConfig.comPortConfig.parity.ToString();
            string[] stopBitList = { StopBits.None.ToString(), StopBits.One.ToString(), StopBits.OnePointFive.ToString(), StopBits.Two.ToString() };
            foreach (string iterm in stopBitList)
            {
                StopBitSelectBox.Items.Add(iterm);
            }
            StopBitSelectBox.Text = MainForm.clientConfig.comPortConfig.stopBit.ToString();
            BoundSelectBox.Text = MainForm.clientConfig.comPortConfig.bound.ToString();
            TimeOutSetBox.Text = MainForm.clientConfig.comPortConfig.comTimeout.ToString();
        }

        private void SerialYesButt_Click(object sender, EventArgs e)
        {
            MainForm.clientConfig.comPortArr[0] = SerialSelectBox1.Text;
            MainForm.clientConfig.comPortArr[1] = SerialSelectBox2.Text;
            MainForm.clientConfig.comPortArr[2] = SerialSelectBox3.Text;
            if (ParitySelectBox.Text == Parity.Even.ToString())
            {
                MainForm.clientConfig.comPortConfig.parity = Parity.Even;
            }
            else if (ParitySelectBox.Text == Parity.Odd.ToString())
            {
                MainForm.clientConfig.comPortConfig.parity = Parity.Odd;
            }
            else if (ParitySelectBox.Text == Parity.None.ToString())
            {
                MainForm.clientConfig.comPortConfig.parity = Parity.None;
            }
            if (StopBitSelectBox.Text == StopBits.None.ToString())
            {
                MainForm.clientConfig.comPortConfig.stopBit = StopBits.None;
            }
            else if (StopBitSelectBox.Text == StopBits.One.ToString())
            {
                MainForm.clientConfig.comPortConfig.stopBit = StopBits.One;
            }
            else if (StopBitSelectBox.Text == StopBits.OnePointFive.ToString())
            {
                MainForm.clientConfig.comPortConfig.stopBit = StopBits.OnePointFive;
            }
            else if (StopBitSelectBox.Text == StopBits.Two.ToString())
            {
                MainForm.clientConfig.comPortConfig.stopBit = StopBits.Two;
            }
            MainForm.clientConfig.comPortConfig.bound = int.Parse(BoundSelectBox.Text);
            MainForm.clientConfig.comPortConfig.comTimeout = int.Parse(TimeOutSetBox.Text);
            MainForm.clientConfig.SaveConfig();
            this.Close();
        }

        private void SerialNoButt_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
