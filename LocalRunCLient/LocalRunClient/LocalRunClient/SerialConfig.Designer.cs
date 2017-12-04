namespace LocalRunClient
{
    partial class SerialConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SerialSelectBox1 = new System.Windows.Forms.ComboBox();
            this.SerialSelectLable = new System.Windows.Forms.Label();
            this.BoundSelectLable = new System.Windows.Forms.Label();
            this.BoundSelectBox = new System.Windows.Forms.TextBox();
            this.ParitySelectLable = new System.Windows.Forms.Label();
            this.ParitySelectBox = new System.Windows.Forms.ComboBox();
            this.StopBitSelectBox = new System.Windows.Forms.ComboBox();
            this.StopBitLable = new System.Windows.Forms.Label();
            this.OutTimeSetLable = new System.Windows.Forms.Label();
            this.TimeOutSetBox = new System.Windows.Forms.TextBox();
            this.SerialYesButt = new System.Windows.Forms.Button();
            this.SerialNoButt = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SerialSelectBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SerialSelectBox3 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // SerialSelectBox1
            // 
            this.SerialSelectBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SerialSelectBox1.FormattingEnabled = true;
            this.SerialSelectBox1.Location = new System.Drawing.Point(105, 12);
            this.SerialSelectBox1.Name = "SerialSelectBox1";
            this.SerialSelectBox1.Size = new System.Drawing.Size(121, 20);
            this.SerialSelectBox1.TabIndex = 0;
            // 
            // SerialSelectLable
            // 
            this.SerialSelectLable.AutoSize = true;
            this.SerialSelectLable.Location = new System.Drawing.Point(12, 20);
            this.SerialSelectLable.Name = "SerialSelectLable";
            this.SerialSelectLable.Size = new System.Drawing.Size(29, 12);
            this.SerialSelectLable.TabIndex = 1;
            this.SerialSelectLable.Text = "COM1";
            // 
            // BoundSelectLable
            // 
            this.BoundSelectLable.AutoSize = true;
            this.BoundSelectLable.Location = new System.Drawing.Point(14, 163);
            this.BoundSelectLable.Name = "BoundSelectLable";
            this.BoundSelectLable.Size = new System.Drawing.Size(65, 12);
            this.BoundSelectLable.TabIndex = 2;
            this.BoundSelectLable.Text = "波特率选择";
            // 
            // BoundSelectBox
            // 
            this.BoundSelectBox.Location = new System.Drawing.Point(107, 154);
            this.BoundSelectBox.Name = "BoundSelectBox";
            this.BoundSelectBox.Size = new System.Drawing.Size(100, 21);
            this.BoundSelectBox.TabIndex = 3;
            // 
            // ParitySelectLable
            // 
            this.ParitySelectLable.AutoSize = true;
            this.ParitySelectLable.Location = new System.Drawing.Point(14, 110);
            this.ParitySelectLable.Name = "ParitySelectLable";
            this.ParitySelectLable.Size = new System.Drawing.Size(65, 12);
            this.ParitySelectLable.TabIndex = 4;
            this.ParitySelectLable.Text = "奇偶校验位";
            // 
            // ParitySelectBox
            // 
            this.ParitySelectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ParitySelectBox.FormattingEnabled = true;
            this.ParitySelectBox.Location = new System.Drawing.Point(107, 102);
            this.ParitySelectBox.Name = "ParitySelectBox";
            this.ParitySelectBox.Size = new System.Drawing.Size(121, 20);
            this.ParitySelectBox.TabIndex = 5;
            // 
            // StopBitSelectBox
            // 
            this.StopBitSelectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StopBitSelectBox.FormattingEnabled = true;
            this.StopBitSelectBox.Location = new System.Drawing.Point(107, 128);
            this.StopBitSelectBox.Name = "StopBitSelectBox";
            this.StopBitSelectBox.Size = new System.Drawing.Size(121, 20);
            this.StopBitSelectBox.TabIndex = 7;
            // 
            // StopBitLable
            // 
            this.StopBitLable.AutoSize = true;
            this.StopBitLable.Location = new System.Drawing.Point(14, 136);
            this.StopBitLable.Name = "StopBitLable";
            this.StopBitLable.Size = new System.Drawing.Size(41, 12);
            this.StopBitLable.TabIndex = 6;
            this.StopBitLable.Text = "停止位";
            // 
            // OutTimeSetLable
            // 
            this.OutTimeSetLable.AutoSize = true;
            this.OutTimeSetLable.Location = new System.Drawing.Point(14, 190);
            this.OutTimeSetLable.Name = "OutTimeSetLable";
            this.OutTimeSetLable.Size = new System.Drawing.Size(77, 12);
            this.OutTimeSetLable.TabIndex = 8;
            this.OutTimeSetLable.Text = "串口超时时间";
            // 
            // TimeOutSetBox
            // 
            this.TimeOutSetBox.Location = new System.Drawing.Point(107, 181);
            this.TimeOutSetBox.Name = "TimeOutSetBox";
            this.TimeOutSetBox.Size = new System.Drawing.Size(100, 21);
            this.TimeOutSetBox.TabIndex = 9;
            // 
            // SerialYesButt
            // 
            this.SerialYesButt.Location = new System.Drawing.Point(16, 213);
            this.SerialYesButt.Name = "SerialYesButt";
            this.SerialYesButt.Size = new System.Drawing.Size(75, 23);
            this.SerialYesButt.TabIndex = 10;
            this.SerialYesButt.Text = "确定";
            this.SerialYesButt.UseVisualStyleBackColor = true;
            this.SerialYesButt.Click += new System.EventHandler(this.SerialYesButt_Click);
            // 
            // SerialNoButt
            // 
            this.SerialNoButt.Location = new System.Drawing.Point(132, 213);
            this.SerialNoButt.Name = "SerialNoButt";
            this.SerialNoButt.Size = new System.Drawing.Size(75, 23);
            this.SerialNoButt.TabIndex = 11;
            this.SerialNoButt.Text = "取消";
            this.SerialNoButt.UseVisualStyleBackColor = true;
            this.SerialNoButt.Click += new System.EventHandler(this.SerialNoButt_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "COM2";
            // 
            // SerialSelectBox2
            // 
            this.SerialSelectBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SerialSelectBox2.FormattingEnabled = true;
            this.SerialSelectBox2.Location = new System.Drawing.Point(105, 38);
            this.SerialSelectBox2.Name = "SerialSelectBox2";
            this.SerialSelectBox2.Size = new System.Drawing.Size(121, 20);
            this.SerialSelectBox2.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "COM3";
            // 
            // SerialSelectBox3
            // 
            this.SerialSelectBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SerialSelectBox3.FormattingEnabled = true;
            this.SerialSelectBox3.Location = new System.Drawing.Point(105, 64);
            this.SerialSelectBox3.Name = "SerialSelectBox3";
            this.SerialSelectBox3.Size = new System.Drawing.Size(121, 20);
            this.SerialSelectBox3.TabIndex = 14;
            // 
            // SerialConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 248);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SerialSelectBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SerialSelectBox2);
            this.Controls.Add(this.SerialNoButt);
            this.Controls.Add(this.SerialYesButt);
            this.Controls.Add(this.TimeOutSetBox);
            this.Controls.Add(this.OutTimeSetLable);
            this.Controls.Add(this.StopBitSelectBox);
            this.Controls.Add(this.StopBitLable);
            this.Controls.Add(this.ParitySelectBox);
            this.Controls.Add(this.ParitySelectLable);
            this.Controls.Add(this.BoundSelectBox);
            this.Controls.Add(this.BoundSelectLable);
            this.Controls.Add(this.SerialSelectLable);
            this.Controls.Add(this.SerialSelectBox1);
            this.Name = "SerialConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SerialConfig";
            this.Load += new System.EventHandler(this.SerialConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox SerialSelectBox1;
        private System.Windows.Forms.Label SerialSelectLable;
        private System.Windows.Forms.Label BoundSelectLable;
        private System.Windows.Forms.TextBox BoundSelectBox;
        private System.Windows.Forms.Label ParitySelectLable;
        private System.Windows.Forms.ComboBox ParitySelectBox;
        private System.Windows.Forms.ComboBox StopBitSelectBox;
        private System.Windows.Forms.Label StopBitLable;
        private System.Windows.Forms.Label OutTimeSetLable;
        private System.Windows.Forms.TextBox TimeOutSetBox;
        private System.Windows.Forms.Button SerialYesButt;
        private System.Windows.Forms.Button SerialNoButt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox SerialSelectBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox SerialSelectBox3;
    }
}