namespace assetsUpdater.UI.WinForms
{
    partial class ConfigModifyForm
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
            this.Confirm_Btn = new System.Windows.Forms.Button();
            this.MajV_TextBox = new System.Windows.Forms.TextBox();
            this.MirrorV_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ModifyDirList_Btn = new System.Windows.Forms.Button();
            this.AddressBuilder_ComboBox = new System.Windows.Forms.ComboBox();
            this.UpdateUrl_TextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ModifyAddressBuilder_Btn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Confirm_Btn
            // 
            this.Confirm_Btn.Location = new System.Drawing.Point(113, 190);
            this.Confirm_Btn.Name = "Confirm_Btn";
            this.Confirm_Btn.Size = new System.Drawing.Size(75, 23);
            this.Confirm_Btn.TabIndex = 0;
            this.Confirm_Btn.Text = "确定";
            this.Confirm_Btn.UseVisualStyleBackColor = true;
            this.Confirm_Btn.Click += new System.EventHandler(this.Confirm_Btn_Click);
            // 
            // MajV_TextBox
            // 
            this.MajV_TextBox.Location = new System.Drawing.Point(113, 66);
            this.MajV_TextBox.Name = "MajV_TextBox";
            this.MajV_TextBox.Size = new System.Drawing.Size(62, 23);
            this.MajV_TextBox.TabIndex = 1;
            // 
            // MirrorV_TextBox
            // 
            this.MirrorV_TextBox.Location = new System.Drawing.Point(113, 119);
            this.MirrorV_TextBox.Name = "MirrorV_TextBox";
            this.MirrorV_TextBox.Size = new System.Drawing.Size(62, 23);
            this.MirrorV_TextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "IAddressBuilder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "UpdateUrl:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ModifyAddressBuilder_Btn);
            this.groupBox1.Controls.Add(this.ModifyDirList_Btn);
            this.groupBox1.Controls.Add(this.AddressBuilder_ComboBox);
            this.groupBox1.Controls.Add(this.UpdateUrl_TextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(205, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(343, 222);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "高级设置";
            // 
            // ModifyDirList_Btn
            // 
            this.ModifyDirList_Btn.Location = new System.Drawing.Point(24, 154);
            this.ModifyDirList_Btn.Name = "ModifyDirList_Btn";
            this.ModifyDirList_Btn.Size = new System.Drawing.Size(180, 26);
            this.ModifyDirList_Btn.TabIndex = 11;
            this.ModifyDirList_Btn.Text = "修改DbSchema";
            this.ModifyDirList_Btn.UseVisualStyleBackColor = true;
            this.ModifyDirList_Btn.Click += new System.EventHandler(this.ModifyDirList_Btn_Click);
            // 
            // AddressBuilder_ComboBox
            // 
            this.AddressBuilder_ComboBox.FormattingEnabled = true;
            this.AddressBuilder_ComboBox.Location = new System.Drawing.Point(66, 87);
            this.AddressBuilder_ComboBox.Name = "AddressBuilder_ComboBox";
            this.AddressBuilder_ComboBox.Size = new System.Drawing.Size(121, 25);
            this.AddressBuilder_ComboBox.TabIndex = 10;
            // 
            // UpdateUrl_TextBox
            // 
            this.UpdateUrl_TextBox.Location = new System.Drawing.Point(101, 16);
            this.UpdateUrl_TextBox.Name = "UpdateUrl_TextBox";
            this.UpdateUrl_TextBox.Size = new System.Drawing.Size(100, 23);
            this.UpdateUrl_TextBox.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "主要版本:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "次要版本：";
            // 
            // ModifyAddressBuilder_Btn
            // 
            this.ModifyAddressBuilder_Btn.Location = new System.Drawing.Point(144, 58);
            this.ModifyAddressBuilder_Btn.Name = "ModifyAddressBuilder_Btn";
            this.ModifyAddressBuilder_Btn.Size = new System.Drawing.Size(141, 23);
            this.ModifyAddressBuilder_Btn.TabIndex = 12;
            this.ModifyAddressBuilder_Btn.Text = "修改AddressBuilder";
            this.ModifyAddressBuilder_Btn.UseVisualStyleBackColor = true;
            this.ModifyAddressBuilder_Btn.Click += new System.EventHandler(this.ModifyAddressBuilder_Btn_Click);
            // 
            // ConfigModifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 355);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.MirrorV_TextBox);
            this.Controls.Add(this.MajV_TextBox);
            this.Controls.Add(this.Confirm_Btn);
            this.Name = "ConfigModifyForm";
            this.Text = "ConfigModifyForm";
            this.Load += new System.EventHandler(this.ConfigModifyForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Confirm_Btn;
        private System.Windows.Forms.TextBox MajV_TextBox;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.TextBox MirrorV_TextBox;
        private System.Windows.Forms.ComboBox AddressBuilder_ComboBox;
        private System.Windows.Forms.TextBox UpdateUrl_TextBox;
        private System.Windows.Forms.Button ModifyDirList_Btn;
        private System.Windows.Forms.Button ModifyAddressBuilder_Btn;
    }
}