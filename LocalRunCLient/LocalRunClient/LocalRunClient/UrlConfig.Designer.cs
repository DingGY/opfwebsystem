namespace LocalRunClient
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
            this.label2 = new System.Windows.Forms.Label();
            this.encryptionIPText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.encryptionPortText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "URL:";
            // 
            // urlSetBox
            // 
            this.urlSetBox.Location = new System.Drawing.Point(52, 37);
            this.urlSetBox.Name = "urlSetBox";
            this.urlSetBox.Size = new System.Drawing.Size(437, 21);
            this.urlSetBox.TabIndex = 1;
            // 
            // URLYesButt
            // 
            this.URLYesButt.Location = new System.Drawing.Point(213, 64);
            this.URLYesButt.Name = "URLYesButt";
            this.URLYesButt.Size = new System.Drawing.Size(75, 23);
            this.URLYesButt.TabIndex = 2;
            this.URLYesButt.Text = "确定";
            this.URLYesButt.UseVisualStyleBackColor = true;
            this.URLYesButt.Click += new System.EventHandler(this.URLYesButt_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "加密机IP：";
            // 
            // encryptionIPText
            // 
            this.encryptionIPText.Location = new System.Drawing.Point(77, 6);
            this.encryptionIPText.Name = "encryptionIPText";
            this.encryptionIPText.Size = new System.Drawing.Size(162, 21);
            this.encryptionIPText.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(245, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "加密机端口：";
            // 
            // encryptionPortText
            // 
            this.encryptionPortText.Location = new System.Drawing.Point(328, 6);
            this.encryptionPortText.Name = "encryptionPortText";
            this.encryptionPortText.Size = new System.Drawing.Size(162, 21);
            this.encryptionPortText.TabIndex = 6;
            // 
            // UrlConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 98);
            this.Controls.Add(this.encryptionPortText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.encryptionIPText);
            this.Controls.Add(this.label2);
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox encryptionIPText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox encryptionPortText;
    }
}