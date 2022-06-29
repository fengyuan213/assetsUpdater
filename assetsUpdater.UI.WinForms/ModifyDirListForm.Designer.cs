namespace assetsUpdater.UI.WinForms
{
    partial class ModifyDirListForm
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
            this.dirListView = new System.Windows.Forms.ListView();
            this.relativePath_Col = new System.Windows.Forms.ColumnHeader();
            this.fileType_Col = new System.Windows.Forms.ColumnHeader();
            this.AddDir_Btn = new System.Windows.Forms.Button();
            this.DeleteSelectedBtn = new System.Windows.Forms.Button();
            this.Confirm_Btn = new System.Windows.Forms.Button();
            this.AddFile_Btn = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.VcsLabel = new System.Windows.Forms.Label();
            this.ChangeVCSBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dirListView
            // 
            this.dirListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.relativePath_Col,
            this.fileType_Col});
            this.dirListView.HideSelection = false;
            this.dirListView.Location = new System.Drawing.Point(12, 23);
            this.dirListView.Name = "dirListView";
            this.dirListView.Size = new System.Drawing.Size(588, 404);
            this.dirListView.TabIndex = 0;
            this.dirListView.UseCompatibleStateImageBehavior = false;
            this.dirListView.View = System.Windows.Forms.View.Details;
            // 
            // relativePath_Col
            // 
            this.relativePath_Col.Text = "相对路径";
            this.relativePath_Col.Width = 100;
            // 
            // fileType_Col
            // 
            this.fileType_Col.Text = "类型";
            // 
            // AddDir_Btn
            // 
            this.AddDir_Btn.Location = new System.Drawing.Point(621, 123);
            this.AddDir_Btn.Name = "AddDir_Btn";
            this.AddDir_Btn.Size = new System.Drawing.Size(126, 23);
            this.AddDir_Btn.TabIndex = 1;
            this.AddDir_Btn.Text = "添加文件夹";
            this.AddDir_Btn.UseVisualStyleBackColor = true;
            this.AddDir_Btn.Click += new System.EventHandler(this.AddDir_Btn_Click);
            // 
            // DeleteSelectedBtn
            // 
            this.DeleteSelectedBtn.Location = new System.Drawing.Point(621, 152);
            this.DeleteSelectedBtn.Name = "DeleteSelectedBtn";
            this.DeleteSelectedBtn.Size = new System.Drawing.Size(126, 23);
            this.DeleteSelectedBtn.TabIndex = 2;
            this.DeleteSelectedBtn.Text = "删除所选";
            this.DeleteSelectedBtn.UseVisualStyleBackColor = true;
            this.DeleteSelectedBtn.Click += new System.EventHandler(this.DeleteSelectedBtn_Click);
            // 
            // Confirm_Btn
            // 
            this.Confirm_Btn.Location = new System.Drawing.Point(621, 404);
            this.Confirm_Btn.Name = "Confirm_Btn";
            this.Confirm_Btn.Size = new System.Drawing.Size(126, 23);
            this.Confirm_Btn.TabIndex = 3;
            this.Confirm_Btn.Text = "确定";
            this.Confirm_Btn.UseVisualStyleBackColor = true;
            this.Confirm_Btn.Click += new System.EventHandler(this.Confirm_Btn_Click);
            // 
            // AddFile_Btn
            // 
            this.AddFile_Btn.Location = new System.Drawing.Point(621, 94);
            this.AddFile_Btn.Name = "AddFile_Btn";
            this.AddFile_Btn.Size = new System.Drawing.Size(126, 23);
            this.AddFile_Btn.TabIndex = 5;
            this.AddFile_Btn.Text = "添加文件";
            this.AddFile_Btn.UseVisualStyleBackColor = true;
            this.AddFile_Btn.Click += new System.EventHandler(this.AddFile_Btn_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // VcsLabel
            // 
            this.VcsLabel.AutoSize = true;
            this.VcsLabel.Location = new System.Drawing.Point(45, 434);
            this.VcsLabel.Name = "VcsLabel";
            this.VcsLabel.Size = new System.Drawing.Size(43, 17);
            this.VcsLabel.TabIndex = 6;
            this.VcsLabel.Text = "label1";
            // 
            // ChangeVCSBtn
            // 
            this.ChangeVCSBtn.Location = new System.Drawing.Point(456, 434);
            this.ChangeVCSBtn.Name = "ChangeVCSBtn";
            this.ChangeVCSBtn.Size = new System.Drawing.Size(144, 24);
            this.ChangeVCSBtn.TabIndex = 7;
            this.ChangeVCSBtn.Text = "修改当前数据库根目录";
            this.ChangeVCSBtn.UseVisualStyleBackColor = true;
            this.ChangeVCSBtn.Click += new System.EventHandler(this.ChangeVCSBtn_Click);
            // 
            // ModifyDirListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 460);
            this.Controls.Add(this.ChangeVCSBtn);
            this.Controls.Add(this.VcsLabel);
            this.Controls.Add(this.AddFile_Btn);
            this.Controls.Add(this.Confirm_Btn);
            this.Controls.Add(this.DeleteSelectedBtn);
            this.Controls.Add(this.AddDir_Btn);
            this.Controls.Add(this.dirListView);
            this.Name = "ModifyDirListForm";
            this.Text = "ModifyDirListForm";
            this.Load += new System.EventHandler(this.ModifyDirListForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView dirListView;
        private System.Windows.Forms.Button AddDir_Btn;
        private System.Windows.Forms.ColumnHeader relativePath_Col;
        private System.Windows.Forms.ColumnHeader fileType_Col;
        private System.Windows.Forms.Button DeleteSelectedBtn;
        private System.Windows.Forms.Button Confirm_Btn;
        private System.Windows.Forms.Button AddFile_Btn;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label VcsLabel;
        private System.Windows.Forms.Button ChangeVCSBtn;
    }
}