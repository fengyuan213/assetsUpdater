﻿namespace assetsUpdater.UI.WinForms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ReleaseNewVersion_Btn = new System.Windows.Forms.Button();
            this.CreateFScrach_Btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ReleaseNewVersion_Btn
            // 
            this.ReleaseNewVersion_Btn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ReleaseNewVersion_Btn.Location = new System.Drawing.Point(34, 42);
            this.ReleaseNewVersion_Btn.Name = "ReleaseNewVersion_Btn";
            this.ReleaseNewVersion_Btn.Size = new System.Drawing.Size(159, 30);
            this.ReleaseNewVersion_Btn.TabIndex = 0;
            this.ReleaseNewVersion_Btn.Text = "发布新版本";
            this.ReleaseNewVersion_Btn.UseVisualStyleBackColor = true;
            this.ReleaseNewVersion_Btn.Click += new System.EventHandler(this.ReleaseNewVersion_Btn_Click);
            // 
            // CreateFScrach_Btn
            // 
            this.CreateFScrach_Btn.Location = new System.Drawing.Point(34, 106);
            this.CreateFScrach_Btn.Name = "CreateFScrach_Btn";
            this.CreateFScrach_Btn.Size = new System.Drawing.Size(159, 30);
            this.CreateFScrach_Btn.TabIndex = 1;
            this.CreateFScrach_Btn.Text = "重新创建";
            this.CreateFScrach_Btn.UseVisualStyleBackColor = true;
            this.CreateFScrach_Btn.Click += new System.EventHandler(this.CreateFScrach_Btn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 189);
            this.Controls.Add(this.CreateFScrach_Btn);
            this.Controls.Add(this.ReleaseNewVersion_Btn);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button ReleaseNewVersion_Btn;
        private System.Windows.Forms.Button CreateFScrach_Btn;
    }
}
