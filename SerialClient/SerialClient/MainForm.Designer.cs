namespace SerialClient
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.connectButt = new System.Windows.Forms.Button();
            this.mainStrip = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.串口设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.网络设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.软件更新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeButt = new System.Windows.Forms.Button();
            this.connStatus = new System.Windows.Forms.Label();
            this.mainStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectButt
            // 
            this.connectButt.Location = new System.Drawing.Point(12, 37);
            this.connectButt.Name = "connectButt";
            this.connectButt.Size = new System.Drawing.Size(75, 23);
            this.connectButt.TabIndex = 0;
            this.connectButt.Text = "连接服务器";
            this.connectButt.UseVisualStyleBackColor = true;
            this.connectButt.Click += new System.EventHandler(this.connectButt_Click);
            // 
            // mainStrip
            // 
            this.mainStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem});
            this.mainStrip.Location = new System.Drawing.Point(0, 0);
            this.mainStrip.Name = "mainStrip";
            this.mainStrip.Size = new System.Drawing.Size(289, 25);
            this.mainStrip.TabIndex = 2;
            this.mainStrip.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.串口设置ToolStripMenuItem,
            this.网络设置ToolStripMenuItem,
            this.软件更新ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 串口设置ToolStripMenuItem
            // 
            this.串口设置ToolStripMenuItem.Name = "串口设置ToolStripMenuItem";
            this.串口设置ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.串口设置ToolStripMenuItem.Text = "串口设置";
            this.串口设置ToolStripMenuItem.Click += new System.EventHandler(this.串口设置ToolStripMenuItem_Click);
            // 
            // 网络设置ToolStripMenuItem
            // 
            this.网络设置ToolStripMenuItem.Name = "网络设置ToolStripMenuItem";
            this.网络设置ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.网络设置ToolStripMenuItem.Text = "网络设置";
            this.网络设置ToolStripMenuItem.Click += new System.EventHandler(this.网络设置ToolStripMenuItem_Click);
            // 
            // 软件更新ToolStripMenuItem
            // 
            this.软件更新ToolStripMenuItem.Name = "软件更新ToolStripMenuItem";
            this.软件更新ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.软件更新ToolStripMenuItem.Text = "软件更新";
            // 
            // closeButt
            // 
            this.closeButt.Location = new System.Drawing.Point(12, 77);
            this.closeButt.Name = "closeButt";
            this.closeButt.Size = new System.Drawing.Size(75, 23);
            this.closeButt.TabIndex = 3;
            this.closeButt.Text = "断开连接";
            this.closeButt.UseVisualStyleBackColor = true;
            this.closeButt.Click += new System.EventHandler(this.closeButt_Click);
            // 
            // connStatus
            // 
            this.connStatus.AutoSize = true;
            this.connStatus.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.connStatus.Location = new System.Drawing.Point(107, 41);
            this.connStatus.Name = "connStatus";
            this.connStatus.Size = new System.Drawing.Size(66, 19);
            this.connStatus.TabIndex = 4;
            this.connStatus.Text = "未连接";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 110);
            this.Controls.Add(this.connStatus);
            this.Controls.Add(this.closeButt);
            this.Controls.Add(this.connectButt);
            this.Controls.Add(this.mainStrip);
            this.MainMenuStrip = this.mainStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "串口客户端";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainStrip.ResumeLayout(false);
            this.mainStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectButt;
        private System.Windows.Forms.MenuStrip mainStrip;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 串口设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 网络设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 软件更新ToolStripMenuItem;
        private System.Windows.Forms.Button closeButt;
        private System.Windows.Forms.Label connStatus;
    }
}

