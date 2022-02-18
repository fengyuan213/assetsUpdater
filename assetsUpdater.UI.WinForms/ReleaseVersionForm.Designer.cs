namespace assetsUpdater.UI.WinForms
{
    partial class ReleaseVersionForm
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
            this.InputDb_openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ChooseLdb_Btn = new System.Windows.Forms.Button();
            this.ldbName_Label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Release_Btn = new System.Windows.Forms.Button();
            this.PartialUpload_radioBtn = new System.Windows.Forms.RadioButton();
            this.FullUpload_RadioBtn = new System.Windows.Forms.RadioButton();
            this.DbModify_Btn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.VcsFolder_Textbox = new System.Windows.Forms.TextBox();
            this.vcs_folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.BrowseVcsFolder_Btn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.DownloadDb_Btn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // InputDb_openFileDialog
            // 
            this.InputDb_openFileDialog.FileName = "openFileDialog1";
            // 
            // ChooseLdb_Btn
            // 
            this.ChooseLdb_Btn.Location = new System.Drawing.Point(298, 67);
            this.ChooseLdb_Btn.Name = "ChooseLdb_Btn";
            this.ChooseLdb_Btn.Size = new System.Drawing.Size(95, 23);
            this.ChooseLdb_Btn.TabIndex = 0;
            this.ChooseLdb_Btn.Text = "从本地选择";
            this.ChooseLdb_Btn.UseVisualStyleBackColor = true;
            this.ChooseLdb_Btn.Click += new System.EventHandler(this.Ldb_Btn_Click);
            // 
            // ldbName_Label
            // 
            this.ldbName_Label.AutoSize = true;
            this.ldbName_Label.Location = new System.Drawing.Point(182, 70);
            this.ldbName_Label.Name = "ldbName_Label";
            this.ldbName_Label.Size = new System.Drawing.Size(43, 17);
            this.ldbName_Label.TabIndex = 1;
            this.ldbName_Label.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "你当前i选择了：";
            // 
            // Release_Btn
            // 
            this.Release_Btn.Location = new System.Drawing.Point(247, 335);
            this.Release_Btn.Name = "Release_Btn";
            this.Release_Btn.Size = new System.Drawing.Size(126, 26);
            this.Release_Btn.TabIndex = 3;
            this.Release_Btn.Text = "发布（第一阶段）";
            this.Release_Btn.UseVisualStyleBackColor = true;
            this.Release_Btn.Click += new System.EventHandler(this.Release_Btn_Click);
            // 
            // PartialUpload_radioBtn
            // 
            this.PartialUpload_radioBtn.AutoSize = true;
            this.PartialUpload_radioBtn.Location = new System.Drawing.Point(247, 284);
            this.PartialUpload_radioBtn.Name = "PartialUpload_radioBtn";
            this.PartialUpload_radioBtn.Size = new System.Drawing.Size(106, 21);
            this.PartialUpload_radioBtn.TabIndex = 4;
            this.PartialUpload_radioBtn.Text = "部分上传(推荐)";
            this.PartialUpload_radioBtn.UseVisualStyleBackColor = true;
            // 
            // FullUpload_RadioBtn
            // 
            this.FullUpload_RadioBtn.AllowDrop = true;
            this.FullUpload_RadioBtn.AutoSize = true;
            this.FullUpload_RadioBtn.Location = new System.Drawing.Point(376, 284);
            this.FullUpload_RadioBtn.Name = "FullUpload_RadioBtn";
            this.FullUpload_RadioBtn.Size = new System.Drawing.Size(74, 21);
            this.FullUpload_RadioBtn.TabIndex = 5;
            this.FullUpload_RadioBtn.Text = "完整上传";
            this.FullUpload_RadioBtn.UseVisualStyleBackColor = true;
            // 
            // DbModify_Btn
            // 
            this.DbModify_Btn.Location = new System.Drawing.Point(259, 215);
            this.DbModify_Btn.Name = "DbModify_Btn";
            this.DbModify_Btn.Size = new System.Drawing.Size(114, 34);
            this.DbModify_Btn.TabIndex = 6;
            this.DbModify_Btn.Text = "修改版本信息";
            this.DbModify_Btn.UseVisualStyleBackColor = true;
            this.DbModify_Btn.Click += new System.EventHandler(this.DbModify_Btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(142, 286);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "文件上传方式：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(498, 226);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(498, 255);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // VcsFolder_Textbox
            // 
            this.VcsFolder_Textbox.Location = new System.Drawing.Point(156, 116);
            this.VcsFolder_Textbox.Name = "VcsFolder_Textbox";
            this.VcsFolder_Textbox.Size = new System.Drawing.Size(294, 23);
            this.VcsFolder_Textbox.TabIndex = 10;
            // 
            // BrowseVcsFolder_Btn
            // 
            this.BrowseVcsFolder_Btn.Location = new System.Drawing.Point(472, 116);
            this.BrowseVcsFolder_Btn.Name = "BrowseVcsFolder_Btn";
            this.BrowseVcsFolder_Btn.Size = new System.Drawing.Size(97, 23);
            this.BrowseVcsFolder_Btn.TabIndex = 11;
            this.BrowseVcsFolder_Btn.Text = "选择文件夹";
            this.BrowseVcsFolder_Btn.UseVisualStyleBackColor = true;
            this.BrowseVcsFolder_Btn.Click += new System.EventHandler(this.BrowseVcsFolder_Btn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "当前本地目录：";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(498, 282);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // DownloadDb_Btn
            // 
            this.DownloadDb_Btn.Location = new System.Drawing.Point(428, 67);
            this.DownloadDb_Btn.Name = "DownloadDb_Btn";
            this.DownloadDb_Btn.Size = new System.Drawing.Size(128, 23);
            this.DownloadDb_Btn.TabIndex = 14;
            this.DownloadDb_Btn.Text = "从远程下载数据库";
            this.DownloadDb_Btn.UseVisualStyleBackColor = true;
            this.DownloadDb_Btn.Click += new System.EventHandler(this.DownloadDb_Btn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(399, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "或";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 17);
            this.label5.TabIndex = 16;
            this.label5.Text = "注意：必须为创建时选择的文件夹";
            // 
            // ReleaseVersionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 373);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DownloadDb_Btn);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BrowseVcsFolder_Btn);
            this.Controls.Add(this.VcsFolder_Textbox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DbModify_Btn);
            this.Controls.Add(this.FullUpload_RadioBtn);
            this.Controls.Add(this.PartialUpload_radioBtn);
            this.Controls.Add(this.Release_Btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ldbName_Label);
            this.Controls.Add(this.ChooseLdb_Btn);
            this.Name = "ReleaseVersionForm";
            this.Text = "ReleaseVersionForm";
            this.Activated += new System.EventHandler(this.ReleaseVersionForm_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog InputDb_openFileDialog;
        private System.Windows.Forms.Button ChooseLdb_Btn;
        private System.Windows.Forms.Label ldbName_Label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Release_Btn;
        private System.Windows.Forms.RadioButton PartialUpload_radioBtn;
        private System.Windows.Forms.RadioButton FullUpload_RadioBtn;
        private System.Windows.Forms.Button DbModify_Btn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox VcsFolder_Textbox;
        private System.Windows.Forms.FolderBrowserDialog vcs_folderBrowserDialog;
        private System.Windows.Forms.Button BrowseVcsFolder_Btn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button DownloadDb_Btn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}