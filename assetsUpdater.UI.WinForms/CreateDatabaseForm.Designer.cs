namespace assetsUpdater.UI.WinForms
{
    partial class CreateDatabaseForm
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
            this.Vcs_label = new System.Windows.Forms.Label();
            this.ChooseVcs_Btn = new System.Windows.Forms.Button();
            this.ModifyDirlist_Btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.MajorV_TextBox = new System.Windows.Forms.TextBox();
            this.MinorV_TestBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RootAddress_TextBox = new System.Windows.Forms.TextBox();
            this.UpdateUrl_TextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.DisplayVersion_Label = new System.Windows.Forms.Label();
            this.CreateDbBtn = new System.Windows.Forms.Button();
            this.CreateDbTestBtn = new System.Windows.Forms.Button();
            this.debug_LoadDefaultBtn = new System.Windows.Forms.Button();
            this.dbOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.dbSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.vcs_BrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.AddressBuilderType_ComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.typeDSercet_Textbox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Vcs_label
            // 
            this.Vcs_label.AutoSize = true;
            this.Vcs_label.Location = new System.Drawing.Point(158, 45);
            this.Vcs_label.Name = "Vcs_label";
            this.Vcs_label.Size = new System.Drawing.Size(148, 17);
            this.Vcs_label.TabIndex = 0;
            this.Vcs_label.Text = "C:/Users/fengy/Desktop";
            // 
            // ChooseVcs_Btn
            // 
            this.ChooseVcs_Btn.Location = new System.Drawing.Point(66, 42);
            this.ChooseVcs_Btn.Name = "ChooseVcs_Btn";
            this.ChooseVcs_Btn.Size = new System.Drawing.Size(75, 23);
            this.ChooseVcs_Btn.TabIndex = 1;
            this.ChooseVcs_Btn.Text = "选择VCS";
            this.ChooseVcs_Btn.UseVisualStyleBackColor = true;
            this.ChooseVcs_Btn.Click += new System.EventHandler(this.ChooseVcs_Btn_Click);
            // 
            // ModifyDirlist_Btn
            // 
            this.ModifyDirlist_Btn.Location = new System.Drawing.Point(66, 87);
            this.ModifyDirlist_Btn.Name = "ModifyDirlist_Btn";
            this.ModifyDirlist_Btn.Size = new System.Drawing.Size(180, 26);
            this.ModifyDirlist_Btn.TabIndex = 2;
            this.ModifyDirlist_Btn.Text = "修改包含配置(DirList)";
            this.ModifyDirlist_Btn.UseVisualStyleBackColor = true;
            this.ModifyDirlist_Btn.Click += new System.EventHandler(this.ModifyDirlist_Btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "版本:";
            // 
            // MajorV_TextBox
            // 
            this.MajorV_TextBox.Location = new System.Drawing.Point(75, 144);
            this.MajorV_TextBox.Name = "MajorV_TextBox";
            this.MajorV_TextBox.Size = new System.Drawing.Size(57, 23);
            this.MajorV_TextBox.TabIndex = 4;
            this.MajorV_TextBox.Leave += new System.EventHandler(this.MajorV_TextBox_Leave);
            // 
            // MinorV_TestBox
            // 
            this.MinorV_TestBox.Location = new System.Drawing.Point(146, 144);
            this.MinorV_TestBox.Name = "MinorV_TestBox";
            this.MinorV_TestBox.Size = new System.Drawing.Size(61, 23);
            this.MinorV_TestBox.TabIndex = 5;
            this.MinorV_TestBox.Leave += new System.EventHandler(this.MinorV_TestBox_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "UpdateUrl:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "RootDownloadAddress:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RootAddress_TextBox);
            this.groupBox1.Controls.Add(this.UpdateUrl_TextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(27, 233);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(578, 122);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "高级";
            // 
            // RootAddress_TextBox
            // 
            this.RootAddress_TextBox.Location = new System.Drawing.Point(186, 63);
            this.RootAddress_TextBox.Name = "RootAddress_TextBox";
            this.RootAddress_TextBox.Size = new System.Drawing.Size(345, 23);
            this.RootAddress_TextBox.TabIndex = 9;
            this.RootAddress_TextBox.Leave += new System.EventHandler(this.RootAddress_TextBox_Leave);
            // 
            // UpdateUrl_TextBox
            // 
            this.UpdateUrl_TextBox.Location = new System.Drawing.Point(186, 26);
            this.UpdateUrl_TextBox.Name = "UpdateUrl_TextBox";
            this.UpdateUrl_TextBox.Size = new System.Drawing.Size(346, 23);
            this.UpdateUrl_TextBox.TabIndex = 10;
            this.UpdateUrl_TextBox.Leave += new System.EventHandler(this.UpdateUrl_TextBox_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(134, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = ".";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(213, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "-->";
            // 
            // DisplayVersion_Label
            // 
            this.DisplayVersion_Label.AutoSize = true;
            this.DisplayVersion_Label.Location = new System.Drawing.Point(246, 147);
            this.DisplayVersion_Label.Name = "DisplayVersion_Label";
            this.DisplayVersion_Label.Size = new System.Drawing.Size(25, 17);
            this.DisplayVersion_Label.TabIndex = 11;
            this.DisplayVersion_Label.Text = "1.0";
            this.DisplayVersion_Label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // CreateDbBtn
            // 
            this.CreateDbBtn.Location = new System.Drawing.Point(550, 361);
            this.CreateDbBtn.Name = "CreateDbBtn";
            this.CreateDbBtn.Size = new System.Drawing.Size(75, 23);
            this.CreateDbBtn.TabIndex = 12;
            this.CreateDbBtn.Text = "创建";
            this.CreateDbBtn.UseVisualStyleBackColor = true;
            this.CreateDbBtn.Click += new System.EventHandler(this.CreateDbBtn_Click);
            // 
            // CreateDbTestBtn
            // 
            this.CreateDbTestBtn.Location = new System.Drawing.Point(460, 361);
            this.CreateDbTestBtn.Name = "CreateDbTestBtn";
            this.CreateDbTestBtn.Size = new System.Drawing.Size(75, 23);
            this.CreateDbTestBtn.TabIndex = 13;
            this.CreateDbTestBtn.Text = "构建测试";
            this.CreateDbTestBtn.UseVisualStyleBackColor = true;
            this.CreateDbTestBtn.Click += new System.EventHandler(this.CreateDbTestBtn_Click);
            // 
            // debug_LoadDefaultBtn
            // 
            this.debug_LoadDefaultBtn.Location = new System.Drawing.Point(61, 361);
            this.debug_LoadDefaultBtn.Name = "debug_LoadDefaultBtn";
            this.debug_LoadDefaultBtn.Size = new System.Drawing.Size(132, 22);
            this.debug_LoadDefaultBtn.TabIndex = 14;
            this.debug_LoadDefaultBtn.Text = "debug";
            this.debug_LoadDefaultBtn.UseVisualStyleBackColor = true;
            this.debug_LoadDefaultBtn.Click += new System.EventHandler(this.debug_LoadDefaultBtn_Click);
            // 
            // dbOpenFileDialog
            // 
            this.dbOpenFileDialog.FileName = "openFileDialog1";
            // 
            // AddressBuilderType_ComboBox
            // 
            this.AddressBuilderType_ComboBox.FormattingEnabled = true;
            this.AddressBuilderType_ComboBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.AddressBuilderType_ComboBox.Items.AddRange(new object[] {
            "TencentCloud",
            "General"});
            this.AddressBuilderType_ComboBox.Location = new System.Drawing.Point(90, 189);
            this.AddressBuilderType_ComboBox.Name = "AddressBuilderType_ComboBox";
            this.AddressBuilderType_ComboBox.Size = new System.Drawing.Size(168, 25);
            this.AddressBuilderType_ComboBox.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 192);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 17);
            this.label6.TabIndex = 16;
            this.label6.Text = "提供商:";
            // 
            // typeDSercet_Textbox
            // 
            this.typeDSercet_Textbox.Location = new System.Drawing.Point(497, 189);
            this.typeDSercet_Textbox.Name = "typeDSercet_Textbox";
            this.typeDSercet_Textbox.Size = new System.Drawing.Size(175, 23);
            this.typeDSercet_Textbox.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(284, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(207, 17);
            this.label7.TabIndex = 18;
            this.label7.Text = "TypeD Secret(Tencent Cloud only):";
            // 
            // CreateDatabaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 425);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.typeDSercet_Textbox);
            this.Controls.Add(this.AddressBuilderType_ComboBox);
            this.Controls.Add(this.debug_LoadDefaultBtn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CreateDbTestBtn);
            this.Controls.Add(this.CreateDbBtn);
            this.Controls.Add(this.DisplayVersion_Label);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.MinorV_TestBox);
            this.Controls.Add(this.MajorV_TextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ModifyDirlist_Btn);
            this.Controls.Add(this.ChooseVcs_Btn);
            this.Controls.Add(this.Vcs_label);
            this.Name = "CreateDatabaseForm";
            this.Text = "CreateDatabaseForm";
            this.Load += new System.EventHandler(this.CreateDatabaseForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Vcs_label;
        private System.Windows.Forms.Button ChooseVcs_Btn;
        private System.Windows.Forms.Button ModifyDirlist_Btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MajorV_TextBox;
        private System.Windows.Forms.TextBox MinorV_TestBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox RootAddress_TextBox;
        private System.Windows.Forms.TextBox UpdateUrl_TextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label DisplayVersion_Label;
        private System.Windows.Forms.Button CreateDbBtn;
        private System.Windows.Forms.Button CreateDbTestBtn;
        private System.Windows.Forms.Button debug_LoadDefaultBtn;
        private System.Windows.Forms.OpenFileDialog dbOpenFileDialog;
        private System.Windows.Forms.SaveFileDialog dbSaveFileDialog;
        private System.Windows.Forms.FolderBrowserDialog vcs_BrowserDialog;
        private System.Windows.Forms.ComboBox AddressBuilderType_ComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox typeDSercet_Textbox;
        private System.Windows.Forms.Label label7;
    }
}