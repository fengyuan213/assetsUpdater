namespace assetsUpdater.UI.WinForms.Dialogs
{
    partial class DownloadDatabaseForm
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
            this.Download_Btn = new System.Windows.Forms.Button();
            this.Address_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.IsTencentCdn_CheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Download_Btn
            // 
            this.Download_Btn.Location = new System.Drawing.Point(499, 61);
            this.Download_Btn.Name = "Download_Btn";
            this.Download_Btn.Size = new System.Drawing.Size(75, 23);
            this.Download_Btn.TabIndex = 0;
            this.Download_Btn.Text = "下载";
            this.Download_Btn.UseVisualStyleBackColor = true;
            this.Download_Btn.Click += new System.EventHandler(this.Download_Btn_Click);
            // 
            // Address_TextBox
            // 
            this.Address_TextBox.Location = new System.Drawing.Point(82, 26);
            this.Address_TextBox.Name = "Address_TextBox";
            this.Address_TextBox.Size = new System.Drawing.Size(492, 23);
            this.Address_TextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "地址：";
            // 
            // IsTencentCdn_CheckBox
            // 
            this.IsTencentCdn_CheckBox.AutoSize = true;
            this.IsTencentCdn_CheckBox.Location = new System.Drawing.Point(55, 63);
            this.IsTencentCdn_CheckBox.Name = "IsTencentCdn_CheckBox";
            this.IsTencentCdn_CheckBox.Size = new System.Drawing.Size(135, 21);
            this.IsTencentCdn_CheckBox.TabIndex = 3;
            this.IsTencentCdn_CheckBox.Text = "地址为对象存储地址";
            this.IsTencentCdn_CheckBox.UseVisualStyleBackColor = true;
            // 
            // DownloadDatabaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 108);
            this.Controls.Add(this.IsTencentCdn_CheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Address_TextBox);
            this.Controls.Add(this.Download_Btn);
            this.Name = "DownloadDatabaseForm";
            this.Text = "DownloadDatabaseForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Download_Btn;
        private System.Windows.Forms.TextBox Address_TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox IsTencentCdn_CheckBox;
    }
}