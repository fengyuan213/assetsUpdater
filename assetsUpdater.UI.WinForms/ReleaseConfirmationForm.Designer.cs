namespace assetsUpdater.UI.WinForms
{
    partial class ReleaseConfirmationForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.OriginalVer_Label = new System.Windows.Forms.Label();
            this.CurrentVer_Label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Confirm_Btn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.UpdateUrl_Label = new System.Windows.Forms.Label();
            this.Cancel_Btn = new System.Windows.Forms.Button();
            this.dirListView = new System.Windows.Forms.ListView();
            this.typeModified_Col = new System.Windows.Forms.ColumnHeader();
            this.relativePath_Col = new System.Windows.Forms.ColumnHeader();
            this.debug_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(150, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "-->";
            // 
            // OriginalVer_Label
            // 
            this.OriginalVer_Label.AutoSize = true;
            this.OriginalVer_Label.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OriginalVer_Label.Location = new System.Drawing.Point(96, 26);
            this.OriginalVer_Label.Name = "OriginalVer_Label";
            this.OriginalVer_Label.Size = new System.Drawing.Size(48, 31);
            this.OriginalVer_Label.TabIndex = 1;
            this.OriginalVer_Label.Text = "0.0";
            // 
            // CurrentVer_Label
            // 
            this.CurrentVer_Label.AutoSize = true;
            this.CurrentVer_Label.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CurrentVer_Label.Location = new System.Drawing.Point(208, 26);
            this.CurrentVer_Label.Name = "CurrentVer_Label";
            this.CurrentVer_Label.Size = new System.Drawing.Size(48, 31);
            this.CurrentVer_Label.TabIndex = 2;
            this.CurrentVer_Label.Text = "0.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(12, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 31);
            this.label2.TabIndex = 3;
            this.label2.Text = "版本:";
            // 
            // Confirm_Btn
            // 
            this.Confirm_Btn.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Confirm_Btn.Location = new System.Drawing.Point(341, 514);
            this.Confirm_Btn.Name = "Confirm_Btn";
            this.Confirm_Btn.Size = new System.Drawing.Size(81, 39);
            this.Confirm_Btn.TabIndex = 4;
            this.Confirm_Btn.Text = "确定";
            this.Confirm_Btn.UseVisualStyleBackColor = true;
            this.Confirm_Btn.Click += new System.EventHandler(this.Confirm_Btn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(341, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 31);
            this.label3.TabIndex = 5;
            this.label3.Text = "更新地址:";
            // 
            // UpdateUrl_Label
            // 
            this.UpdateUrl_Label.AutoSize = true;
            this.UpdateUrl_Label.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.UpdateUrl_Label.Location = new System.Drawing.Point(463, 30);
            this.UpdateUrl_Label.Name = "UpdateUrl_Label";
            this.UpdateUrl_Label.Size = new System.Drawing.Size(345, 27);
            this.UpdateUrl_Label.TabIndex = 6;
            this.UpdateUrl_Label.Text = "http://www.tencent.com/update.cx";
            // 
            // Cancel_Btn
            // 
            this.Cancel_Btn.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Cancel_Btn.Location = new System.Drawing.Point(428, 514);
            this.Cancel_Btn.Name = "Cancel_Btn";
            this.Cancel_Btn.Size = new System.Drawing.Size(81, 39);
            this.Cancel_Btn.TabIndex = 7;
            this.Cancel_Btn.Text = "取消";
            this.Cancel_Btn.UseVisualStyleBackColor = true;
            this.Cancel_Btn.Click += new System.EventHandler(this.Cancel_Btn_Click);
            // 
            // dirListView
            // 
            this.dirListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.typeModified_Col,
            this.relativePath_Col});
            this.dirListView.HideSelection = false;
            this.dirListView.Location = new System.Drawing.Point(341, 85);
            this.dirListView.Name = "dirListView";
            this.dirListView.Size = new System.Drawing.Size(588, 400);
            this.dirListView.TabIndex = 8;
            this.dirListView.UseCompatibleStateImageBehavior = false;
            this.dirListView.View = System.Windows.Forms.View.Details;
            // 
            // typeModified_Col
            // 
            this.typeModified_Col.Text = "修改类型";
            // 
            // relativePath_Col
            // 
            this.relativePath_Col.Text = "相对路径";
            this.relativePath_Col.Width = 150;
            // 
            // debug_btn
            // 
            this.debug_btn.Location = new System.Drawing.Point(964, 46);
            this.debug_btn.Name = "debug_btn";
            this.debug_btn.Size = new System.Drawing.Size(86, 40);
            this.debug_btn.TabIndex = 9;
            this.debug_btn.Text = "Debug";
            this.debug_btn.UseVisualStyleBackColor = true;
            this.debug_btn.Click += new System.EventHandler(this.debug_btn_Click);
            // 
            // ReleaseConfirmationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 565);
            this.Controls.Add(this.debug_btn);
            this.Controls.Add(this.dirListView);
            this.Controls.Add(this.Cancel_Btn);
            this.Controls.Add(this.UpdateUrl_Label);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Confirm_Btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CurrentVer_Label);
            this.Controls.Add(this.OriginalVer_Label);
            this.Controls.Add(this.label1);
            this.Name = "ReleaseConfirmationForm";
            this.Text = "ReleaseConfirmationForm";
            this.Load += new System.EventHandler(this.ReleaseConfirmationForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label OriginalVer_Label;
        private System.Windows.Forms.Label CurrentVer_Label;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Confirm_Btn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label UpdateUrl_Label;
        private System.Windows.Forms.Button Cancel_Btn;
        private System.Windows.Forms.ListView dirListView;
        private System.Windows.Forms.ColumnHeader relativePath_Col;
        private System.Windows.Forms.ColumnHeader typeModified_Col;
        private System.Windows.Forms.Button debug_btn;
    }
}