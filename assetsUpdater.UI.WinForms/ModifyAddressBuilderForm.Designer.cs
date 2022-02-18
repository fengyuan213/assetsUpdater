namespace assetsUpdater.UI.WinForms
{
    partial class ModifyAddressBuilderForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.RootDownloadAddr_TextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CurrentRootAddress_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Confirm_Btn
            // 
            this.Confirm_Btn.Location = new System.Drawing.Point(160, 149);
            this.Confirm_Btn.Name = "Confirm_Btn";
            this.Confirm_Btn.Size = new System.Drawing.Size(75, 23);
            this.Confirm_Btn.TabIndex = 0;
            this.Confirm_Btn.Text = "确认";
            this.Confirm_Btn.UseVisualStyleBackColor = true;
            this.Confirm_Btn.Click += new System.EventHandler(this.Confirm_Btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "RootDownloadAddress:";
            // 
            // RootDownloadAddr_TextBox
            // 
            this.RootDownloadAddr_TextBox.Location = new System.Drawing.Point(12, 58);
            this.RootDownloadAddr_TextBox.Name = "RootDownloadAddr_TextBox";
            this.RootDownloadAddr_TextBox.Size = new System.Drawing.Size(400, 23);
            this.RootDownloadAddr_TextBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(243, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "示例：https://pokecity.file.myqcloud.com";
            // 
            // CurrentRootAddress_Label
            // 
            this.CurrentRootAddress_Label.AutoSize = true;
            this.CurrentRootAddress_Label.Location = new System.Drawing.Point(44, 120);
            this.CurrentRootAddress_Label.Name = "CurrentRootAddress_Label";
            this.CurrentRootAddress_Label.Size = new System.Drawing.Size(43, 17);
            this.CurrentRootAddress_Label.TabIndex = 4;
            this.CurrentRootAddress_Label.Text = "label3";
            this.CurrentRootAddress_Label.Click += new System.EventHandler(this.CurrentRootAddress_Label_Click);
            // 
            // ModifyAddressBuilderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 184);
            this.Controls.Add(this.CurrentRootAddress_Label);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RootDownloadAddr_TextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Confirm_Btn);
            this.Name = "ModifyAddressBuilderForm";
            this.Text = "ModifyAddressBuilderForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Confirm_Btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox RootDownloadAddr_TextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label CurrentRootAddress_Label;
    }
}