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
            this.DbModify_Btn = new System.Windows.Forms.Button();
            this.VcsFolder_Textbox = new System.Windows.Forms.TextBox();
            this.vcs_folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.BrowseVcsFolder_Btn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
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
            this.ChooseLdb_Btn.Location = new System.Drawing.Point(261, 32);
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
            this.ldbName_Label.Location = new System.Drawing.Point(145, 35);
            this.ldbName_Label.Name = "ldbName_Label";
            this.ldbName_Label.Size = new System.Drawing.Size(43, 17);
            this.ldbName_Label.TabIndex = 1;
            this.ldbName_Label.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "你当前i选择了：";
            // 
            // Release_Btn
            // 
            this.Release_Btn.Location = new System.Drawing.Point(404, 154);
            this.Release_Btn.Name = "Release_Btn";
            this.Release_Btn.Size = new System.Drawing.Size(115, 35);
            this.Release_Btn.TabIndex = 3;
            this.Release_Btn.Text = "发布（第一阶段）";
            this.Release_Btn.UseVisualStyleBackColor = true;
            this.Release_Btn.Click += new System.EventHandler(this.Release_Btn_Click);
            // 
            // DbModify_Btn
            // 
            this.DbModify_Btn.Location = new System.Drawing.Point(22, 154);
            this.DbModify_Btn.Name = "DbModify_Btn";
            this.DbModify_Btn.Size = new System.Drawing.Size(159, 34);
            this.DbModify_Btn.TabIndex = 6;
            this.DbModify_Btn.Text = "修改版本信息";
            this.DbModify_Btn.UseVisualStyleBackColor = true;
            this.DbModify_Btn.Click += new System.EventHandler(this.DbModify_Btn_Click);
            // 
            // VcsFolder_Textbox
            // 
            this.VcsFolder_Textbox.Location = new System.Drawing.Point(119, 81);
            this.VcsFolder_Textbox.Name = "VcsFolder_Textbox";
            this.VcsFolder_Textbox.Size = new System.Drawing.Size(294, 23);
            this.VcsFolder_Textbox.TabIndex = 10;
            // 
            // BrowseVcsFolder_Btn
            // 
            this.BrowseVcsFolder_Btn.Location = new System.Drawing.Point(435, 81);
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
            this.label3.Location = new System.Drawing.Point(21, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "当前本地目录：";
            // 
            // DownloadDb_Btn
            // 
            this.DownloadDb_Btn.Location = new System.Drawing.Point(391, 32);
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
            this.label4.Location = new System.Drawing.Point(362, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "或";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 17);
            this.label5.TabIndex = 16;
            this.label5.Text = "注意：必须为创建时选择的文件夹";
            // 
            // ReleaseVersionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 197);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DownloadDb_Btn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BrowseVcsFolder_Btn);
            this.Controls.Add(this.VcsFolder_Textbox);
            this.Controls.Add(this.DbModify_Btn);
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
        private System.Windows.Forms.Button DbModify_Btn;
        private System.Windows.Forms.TextBox VcsFolder_Textbox;
        private System.Windows.Forms.FolderBrowserDialog vcs_folderBrowserDialog;
        private System.Windows.Forms.Button BrowseVcsFolder_Btn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button DownloadDb_Btn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}