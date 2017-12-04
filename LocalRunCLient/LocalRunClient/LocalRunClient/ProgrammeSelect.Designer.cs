namespace LocalRunClient
{
    partial class ProgrammeSelect
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
            this.programeSelectBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.programReflash = new System.Windows.Forms.Button();
            this.programSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // programeSelectBox
            // 
            this.programeSelectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.programeSelectBox.FormattingEnabled = true;
            this.programeSelectBox.Location = new System.Drawing.Point(60, 12);
            this.programeSelectBox.Name = "programeSelectBox";
            this.programeSelectBox.Size = new System.Drawing.Size(372, 20);
            this.programeSelectBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "方案：";
            // 
            // programReflash
            // 
            this.programReflash.Location = new System.Drawing.Point(109, 50);
            this.programReflash.Name = "programReflash";
            this.programReflash.Size = new System.Drawing.Size(75, 23);
            this.programReflash.TabIndex = 2;
            this.programReflash.Text = "刷新";
            this.programReflash.UseVisualStyleBackColor = true;
            this.programReflash.Click += new System.EventHandler(this.programReflash_Click);
            // 
            // programSave
            // 
            this.programSave.Location = new System.Drawing.Point(243, 50);
            this.programSave.Name = "programSave";
            this.programSave.Size = new System.Drawing.Size(75, 23);
            this.programSave.TabIndex = 3;
            this.programSave.Text = "确定";
            this.programSave.UseVisualStyleBackColor = true;
            this.programSave.Click += new System.EventHandler(this.programSave_Click);
            // 
            // ProgrammeSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 86);
            this.Controls.Add(this.programSave);
            this.Controls.Add(this.programReflash);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.programeSelectBox);
            this.Name = "ProgrammeSelect";
            this.Text = "方案选择";
            this.Load += new System.EventHandler(this.ProgrammeSelect_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox programeSelectBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button programReflash;
        private System.Windows.Forms.Button programSave;
    }
}