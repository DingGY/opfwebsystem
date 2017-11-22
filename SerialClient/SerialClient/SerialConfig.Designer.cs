namespace SerialClient
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
            this.SerialSelectBox = new System.Windows.Forms.ComboBox();
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
            this.SuspendLayout();
            // 
            // SerialSelectBox
            // 
            this.SerialSelectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SerialSelectBox.FormattingEnabled = true;
            this.SerialSelectBox.Location = new System.Drawing.Point(105, 12);
            this.SerialSelectBox.Name = "SerialSelectBox";
            this.SerialSelectBox.Size = new System.Drawing.Size(121, 20);
            this.SerialSelectBox.TabIndex = 0;
            // 
            // SerialSelectLable
            // 
            this.SerialSelectLable.AutoSize = true;
            this.SerialSelectLable.Location = new System.Drawing.Point(12, 20);
            this.SerialSelectLable.Name = "SerialSelectLable";
            this.SerialSelectLable.Size = new System.Drawing.Size(53, 12);
            this.SerialSelectLable.TabIndex = 1;
            this.SerialSelectLable.Text = "串口选择";
            // 
            // BoundSelectLable
            // 
            this.BoundSelectLable.AutoSize = true;
            this.BoundSelectLable.Location = new System.Drawing.Point(12, 99);
            this.BoundSelectLable.Name = "BoundSelectLable";
            this.BoundSelectLable.Size = new System.Drawing.Size(65, 12);
            this.BoundSelectLable.TabIndex = 2;
            this.BoundSelectLable.Text = "波特率选择";
            // 
            // BoundSelectBox
            // 
            this.BoundSelectBox.Location = new System.Drawing.Point(105, 90);
            this.BoundSelectBox.Name = "BoundSelectBox";
            this.BoundSelectBox.Size = new System.Drawing.Size(100, 21);
            this.BoundSelectBox.TabIndex = 3;
            // 
            // ParitySelectLable
            // 
            this.ParitySelectLable.AutoSize = true;
            this.ParitySelectLable.Location = new System.Drawing.Point(12, 46);
            this.ParitySelectLable.Name = "ParitySelectLable";
            this.ParitySelectLable.Size = new System.Drawing.Size(65, 12);
            this.ParitySelectLable.TabIndex = 4;
            this.ParitySelectLable.Text = "奇偶校验位";
            // 
            // ParitySelectBox
            // 
            this.ParitySelectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ParitySelectBox.FormattingEnabled = true;
            this.ParitySelectBox.Location = new System.Drawing.Point(105, 38);
            this.ParitySelectBox.Name = "ParitySelectBox";
            this.ParitySelectBox.Size = new System.Drawing.Size(121, 20);
            this.ParitySelectBox.TabIndex = 5;
            // 
            // StopBitSelectBox
            // 
            this.StopBitSelectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StopBitSelectBox.FormattingEnabled = true;
            this.StopBitSelectBox.Location = new System.Drawing.Point(105, 64);
            this.StopBitSelectBox.Name = "StopBitSelectBox";
            this.StopBitSelectBox.Size = new System.Drawing.Size(121, 20);
            this.StopBitSelectBox.TabIndex = 7;
            // 
            // StopBitLable
            // 
            this.StopBitLable.AutoSize = true;
            this.StopBitLable.Location = new System.Drawing.Point(12, 72);
            this.StopBitLable.Name = "StopBitLable";
            this.StopBitLable.Size = new System.Drawing.Size(41, 12);
            this.StopBitLable.TabIndex = 6;
            this.StopBitLable.Text = "停止位";
            // 
            // OutTimeSetLable
            // 
            this.OutTimeSetLable.AutoSize = true;
            this.OutTimeSetLable.Location = new System.Drawing.Point(12, 126);
            this.OutTimeSetLable.Name = "OutTimeSetLable";
            this.OutTimeSetLable.Size = new System.Drawing.Size(77, 12);
            this.OutTimeSetLable.TabIndex = 8;
            this.OutTimeSetLable.Text = "串口超时时间";
            // 
            // TimeOutSetBox
            // 
            this.TimeOutSetBox.Location = new System.Drawing.Point(105, 117);
            this.TimeOutSetBox.Name = "TimeOutSetBox";
            this.TimeOutSetBox.Size = new System.Drawing.Size(100, 21);
            this.TimeOutSetBox.TabIndex = 9;
            // 
            // SerialYesButt
            // 
            this.SerialYesButt.Location = new System.Drawing.Point(14, 149);
            this.SerialYesButt.Name = "SerialYesButt";
            this.SerialYesButt.Size = new System.Drawing.Size(75, 23);
            this.SerialYesButt.TabIndex = 10;
            this.SerialYesButt.Text = "确定";
            this.SerialYesButt.UseVisualStyleBackColor = true;
            this.SerialYesButt.Click += new System.EventHandler(this.SerialYesButt_Click);
            // 
            // SerialNoButt
            // 
            this.SerialNoButt.Location = new System.Drawing.Point(130, 149);
            this.SerialNoButt.Name = "SerialNoButt";
            this.SerialNoButt.Size = new System.Drawing.Size(75, 23);
            this.SerialNoButt.TabIndex = 11;
            this.SerialNoButt.Text = "取消";
            this.SerialNoButt.UseVisualStyleBackColor = true;
            this.SerialNoButt.Click += new System.EventHandler(this.SerialNoButt_Click);
            // 
            // SerialConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 183);
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
            this.Controls.Add(this.SerialSelectBox);
            this.Name = "SerialConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SerialConfig";
            this.Load += new System.EventHandler(this.SerialConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox SerialSelectBox;
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
    }
}