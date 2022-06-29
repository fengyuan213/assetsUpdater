namespace assetsUpdater.UI.WinForms
{
    partial class ReleaseProcessingForm
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
            this.components = new System.ComponentModel.Container();
            this.currentProcessBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.totalProccessBar = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.currentStatus_Label = new System.Windows.Forms.Label();
            this.log_listView = new System.Windows.Forms.ListView();
            this.UpdateStatus_Timer = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Start_Btn = new System.Windows.Forms.Button();
            this.PrintStatus_Btn = new System.Windows.Forms.Button();
            this.AllUnitCount_Label = new System.Windows.Forms.Label();
            this.WaitingUnitCount_Label = new System.Windows.Forms.Label();
            this.ErrorUnitCount_Label = new System.Windows.Forms.Label();
            this.CurrentUnitCount_Label = new System.Windows.Forms.Label();
            this.UploadProgress_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // currentProcessBar
            // 
            this.currentProcessBar.Location = new System.Drawing.Point(118, 50);
            this.currentProcessBar.Name = "currentProcessBar";
            this.currentProcessBar.Size = new System.Drawing.Size(365, 22);
            this.currentProcessBar.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "当前进度:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "总进度:";
            // 
            // totalProccessBar
            // 
            this.totalProccessBar.Location = new System.Drawing.Point(118, 98);
            this.totalProccessBar.Name = "totalProccessBar";
            this.totalProccessBar.Size = new System.Drawing.Size(365, 22);
            this.totalProccessBar.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "当前状态：";
            // 
            // currentStatus_Label
            // 
            this.currentStatus_Label.AutoSize = true;
            this.currentStatus_Label.Location = new System.Drawing.Point(266, 141);
            this.currentStatus_Label.Name = "currentStatus_Label";
            this.currentStatus_Label.Size = new System.Drawing.Size(44, 17);
            this.currentStatus_Label.TabIndex = 5;
            this.currentStatus_Label.Text = "上传中";
            // 
            // log_listView
            // 
            this.log_listView.HideSelection = false;
            this.log_listView.Location = new System.Drawing.Point(53, 199);
            this.log_listView.Name = "log_listView";
            this.log_listView.Size = new System.Drawing.Size(568, 247);
            this.log_listView.TabIndex = 6;
            this.log_listView.UseCompatibleStateImageBehavior = false;
            // 
            // UpdateStatus_Timer
            // 
            this.UpdateStatus_Timer.Enabled = true;
            this.UpdateStatus_Timer.Interval = 1000;
            this.UpdateStatus_Timer.Tick += new System.EventHandler(this.UpdateStatus_Timer_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(509, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "所有任务:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(510, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "剩余：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(509, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "上传中：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(509, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "错误数量：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(194, 169);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 17);
            this.label8.TabIndex = 11;
            this.label8.Text = "已上传/ 总共：";
            // 
            // Start_Btn
            // 
            this.Start_Btn.Location = new System.Drawing.Point(98, 452);
            this.Start_Btn.Name = "Start_Btn";
            this.Start_Btn.Size = new System.Drawing.Size(75, 23);
            this.Start_Btn.TabIndex = 12;
            this.Start_Btn.Text = "Start";
            this.Start_Btn.UseVisualStyleBackColor = true;
            this.Start_Btn.Click += new System.EventHandler(this.Start_Btn_Click);
            // 
            // PrintStatus_Btn
            // 
            this.PrintStatus_Btn.Location = new System.Drawing.Point(491, 452);
            this.PrintStatus_Btn.Name = "PrintStatus_Btn";
            this.PrintStatus_Btn.Size = new System.Drawing.Size(112, 23);
            this.PrintStatus_Btn.TabIndex = 13;
            this.PrintStatus_Btn.Text = "Print Status";
            this.PrintStatus_Btn.UseVisualStyleBackColor = true;
            this.PrintStatus_Btn.Click += new System.EventHandler(this.PrintStatus_Btn_Click);
            // 
            // AllUnitCount_Label
            // 
            this.AllUnitCount_Label.AutoSize = true;
            this.AllUnitCount_Label.Location = new System.Drawing.Point(574, 45);
            this.AllUnitCount_Label.Name = "AllUnitCount_Label";
            this.AllUnitCount_Label.Size = new System.Drawing.Size(29, 17);
            this.AllUnitCount_Label.TabIndex = 14;
            this.AllUnitCount_Label.Text = "100";
            // 
            // WaitingUnitCount_Label
            // 
            this.WaitingUnitCount_Label.AutoSize = true;
            this.WaitingUnitCount_Label.Location = new System.Drawing.Point(551, 103);
            this.WaitingUnitCount_Label.Name = "WaitingUnitCount_Label";
            this.WaitingUnitCount_Label.Size = new System.Drawing.Size(22, 17);
            this.WaitingUnitCount_Label.TabIndex = 15;
            this.WaitingUnitCount_Label.Text = "68";
            // 
            // ErrorUnitCount_Label
            // 
            this.ErrorUnitCount_Label.AutoSize = true;
            this.ErrorUnitCount_Label.Location = new System.Drawing.Point(578, 130);
            this.ErrorUnitCount_Label.Name = "ErrorUnitCount_Label";
            this.ErrorUnitCount_Label.Size = new System.Drawing.Size(15, 17);
            this.ErrorUnitCount_Label.TabIndex = 16;
            this.ErrorUnitCount_Label.Text = "3";
            // 
            // CurrentUnitCount_Label
            // 
            this.CurrentUnitCount_Label.AutoSize = true;
            this.CurrentUnitCount_Label.Location = new System.Drawing.Point(566, 74);
            this.CurrentUnitCount_Label.Name = "CurrentUnitCount_Label";
            this.CurrentUnitCount_Label.Size = new System.Drawing.Size(15, 17);
            this.CurrentUnitCount_Label.TabIndex = 17;
            this.CurrentUnitCount_Label.Text = "2";
            // 
            // UploadProgress_Label
            // 
            this.UploadProgress_Label.AutoSize = true;
            this.UploadProgress_Label.Location = new System.Drawing.Point(292, 169);
            this.UploadProgress_Label.Name = "UploadProgress_Label";
            this.UploadProgress_Label.Size = new System.Drawing.Size(96, 17);
            this.UploadProgress_Label.TabIndex = 18;
            this.UploadProgress_Label.Text = "10MB /100 MB";
            // 
            // ReleaseProcessingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 519);
            this.Controls.Add(this.UploadProgress_Label);
            this.Controls.Add(this.CurrentUnitCount_Label);
            this.Controls.Add(this.ErrorUnitCount_Label);
            this.Controls.Add(this.WaitingUnitCount_Label);
            this.Controls.Add(this.AllUnitCount_Label);
            this.Controls.Add(this.PrintStatus_Btn);
            this.Controls.Add(this.Start_Btn);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.log_listView);
            this.Controls.Add(this.currentStatus_Label);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.totalProccessBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.currentProcessBar);
            this.Name = "ReleaseProcessingForm";
            this.Text = "ReleaseProcessingForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReleaseProcessingForm_FormClosing);
            this.Load += new System.EventHandler(this.ReleaseProcessingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar currentProcessBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar totalProccessBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label currentStatus_Label;
        private System.Windows.Forms.ListView log_listView;
        private System.Windows.Forms.Timer UpdateStatus_Timer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button Start_Btn;
        private System.Windows.Forms.Button PrintStatus_Btn;
        private System.Windows.Forms.Label AllUnitCount_Label;
        private System.Windows.Forms.Label WaitingUnitCount_Label;
        private System.Windows.Forms.Label ErrorUnitCount_Label;
        private System.Windows.Forms.Label CurrentUnitCount_Label;
        private System.Windows.Forms.Label UploadProgress_Label;
    }
}