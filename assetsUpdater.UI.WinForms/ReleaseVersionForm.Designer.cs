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
            this.SuspendLayout();
            // 
            // InputDb_openFileDialog
            // 
            this.InputDb_openFileDialog.FileName = "openFileDialog1";
            // 
            // ChooseLdb_Btn
            // 
            this.ChooseLdb_Btn.Location = new System.Drawing.Point(279, 84);
            this.ChooseLdb_Btn.Name = "ChooseLdb_Btn";
            this.ChooseLdb_Btn.Size = new System.Drawing.Size(134, 25);
            this.ChooseLdb_Btn.TabIndex = 0;
            this.ChooseLdb_Btn.Text = "Choose Ldb";
            this.ChooseLdb_Btn.UseVisualStyleBackColor = true;
            this.ChooseLdb_Btn.Click += new System.EventHandler(this.Ldb_Btn_Click);
            // 
            // ldbName_Label
            // 
            this.ldbName_Label.AutoSize = true;
            this.ldbName_Label.Location = new System.Drawing.Point(191, 88);
            this.ldbName_Label.Name = "ldbName_Label";
            this.ldbName_Label.Size = new System.Drawing.Size(43, 17);
            this.ldbName_Label.TabIndex = 1;
            this.ldbName_Label.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "你当前i选择了：";
            // 
            // Release_Btn
            // 
            this.Release_Btn.Location = new System.Drawing.Point(558, 400);
            this.Release_Btn.Name = "Release_Btn";
            this.Release_Btn.Size = new System.Drawing.Size(92, 26);
            this.Release_Btn.TabIndex = 3;
            this.Release_Btn.Text = "Release";
            this.Release_Btn.UseVisualStyleBackColor = true;
            this.Release_Btn.Click += new System.EventHandler(this.Release_Btn_Click);
            // 
            // PartialUpload_radioBtn
            // 
            this.PartialUpload_radioBtn.AutoSize = true;
            this.PartialUpload_radioBtn.Location = new System.Drawing.Point(515, 359);
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
            this.FullUpload_RadioBtn.Location = new System.Drawing.Point(644, 359);
            this.FullUpload_RadioBtn.Name = "FullUpload_RadioBtn";
            this.FullUpload_RadioBtn.Size = new System.Drawing.Size(74, 21);
            this.FullUpload_RadioBtn.TabIndex = 5;
            this.FullUpload_RadioBtn.Text = "完整上传";
            this.FullUpload_RadioBtn.UseVisualStyleBackColor = true;
            // 
            // DbModify_Btn
            // 
            this.DbModify_Btn.Location = new System.Drawing.Point(68, 211);
            this.DbModify_Btn.Name = "DbModify_Btn";
            this.DbModify_Btn.Size = new System.Drawing.Size(114, 34);
            this.DbModify_Btn.TabIndex = 6;
            this.DbModify_Btn.Text = "修改基本信息";
            this.DbModify_Btn.UseVisualStyleBackColor = true;
            this.DbModify_Btn.Click += new System.EventHandler(this.DbModify_Btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(388, 363);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "文件上传方式：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(685, 116);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(663, 180);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // VcsFolder_Textbox
            // 
            this.VcsFolder_Textbox.Location = new System.Drawing.Point(178, 147);
            this.VcsFolder_Textbox.Name = "VcsFolder_Textbox";
            this.VcsFolder_Textbox.Size = new System.Drawing.Size(294, 23);
            this.VcsFolder_Textbox.TabIndex = 10;
            // 
            // BrowseVcsFolder_Btn
            // 
            this.BrowseVcsFolder_Btn.Location = new System.Drawing.Point(491, 150);
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
            this.label3.Location = new System.Drawing.Point(68, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "本地文件根目录：";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(644, 222);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // ReleaseVersionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
            this.Load += new System.EventHandler(this.ReleaseVersionForm_Load);
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
    }
}