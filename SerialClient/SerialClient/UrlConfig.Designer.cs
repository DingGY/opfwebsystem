namespace SerialClient
{
    partial class UrlConfig
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
            this.label1 = new System.Windows.Forms.Label();
            this.urlSetBox = new System.Windows.Forms.TextBox();
            this.URLYesButt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "URL:";
            // 
            // urlSetBox
            // 
            this.urlSetBox.Location = new System.Drawing.Point(44, 20);
            this.urlSetBox.Name = "urlSetBox";
            this.urlSetBox.Size = new System.Drawing.Size(437, 21);
            this.urlSetBox.TabIndex = 1;
            // 
            // URLYesButt
            // 
            this.URLYesButt.Location = new System.Drawing.Point(205, 47);
            this.URLYesButt.Name = "URLYesButt";
            this.URLYesButt.Size = new System.Drawing.Size(75, 23);
            this.URLYesButt.TabIndex = 2;
            this.URLYesButt.Text = "确定";
            this.URLYesButt.UseVisualStyleBackColor = true;
            this.URLYesButt.Click += new System.EventHandler(this.URLYesButt_Click);
            // 
            // UrlConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 81);
            this.Controls.Add(this.URLYesButt);
            this.Controls.Add(this.urlSetBox);
            this.Controls.Add(this.label1);
            this.Name = "UrlConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UrlConfig";
            this.Load += new System.EventHandler(this.UrlConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox urlSetBox;
        private System.Windows.Forms.Button URLYesButt;
    }
}