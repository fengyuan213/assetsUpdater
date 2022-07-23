#region Using

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using assetsUpdater.Model.StorageProvider;
using NLog;

#endregion

namespace assetsUpdater.UI.WinForms
{
    public partial class ModifyDirListForm : Form
    {
        private const string DirListViewTypeFile = "文件";
        private const string DirListViewTypeFolder = "文件夹";
        private const int DirListViewSubItemCount = 2;
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public DbSchema DatabaseSchema;
        public string VcsRoot;

        public ModifyDirListForm(string vcsRoot, DbSchema dbSchema)
        {
            DatabaseSchema = dbSchema ?? new DbSchema();
            VcsRoot = vcsRoot;
            Initialize();
        }

        public ModifyDirListForm(string vcsFolder)
        {
            VcsRoot = vcsFolder;
            DatabaseSchema = new DbSchema();
            Initialize();
        }

        public ModifyDirListForm(DataManager dataManager)
        {
            VcsRoot = dataManager.StorageProvider.GetData().Config.VersionControlFolder;
            DatabaseSchema = dataManager.StorageProvider.GetData().Config.DatabaseSchema ?? new DbSchema();
            Initialize();
        }

        private void Initialize()
        {
            InitializeComponent();
            InitializeDialogs();
            InitializeUi();
        }

        private void debug_Btn_Click(object sender, System.EventArgs e)
        {
            if (DatabaseSchema == null) return;
            foreach (var path in DatabaseSchema.FileList) Logger.Trace($"FilePath:{path}");
            foreach (var path in DatabaseSchema.DirList) Logger.Trace($"FolderPath:{path}");
        }

        private void ModifyDirListForm_Load(object sender, System.EventArgs e)
        {
        }

        private void InitializeDialogs()
        {
            folderBrowserDialog.AutoUpgradeEnabled = true;
            openFileDialog.CheckFileExists = false;
        }

        private void InitializeUi()
        {
            //CheckDebugging
            //if (!Debugger.IsAttached) debug_Btn.Visible = false;

            VcsLabel.Text = VcsRoot;
            if (DatabaseSchema == null) return;
            var dirList = DatabaseSchema.DirList;
            var fileList = DatabaseSchema.FileList;

            foreach (var dataPath in dirList)
            {
                //Avoid Formatting Error
                var path = GetStandardRelativePath(dataPath);
                dirListView.Items.Add(new ListViewItem(new[] { path, DirListViewTypeFolder }));
            }

            foreach (var dataPath in fileList)
            {
                //Avoid Formatting Error
                var path = GetStandardRelativePath(dataPath);
                dirListView.Items.Add(new ListViewItem(new[] { path, DirListViewTypeFile }));
            }
        }

        private void Confirm_Btn_Click(object sender, System.EventArgs e)
        {
          /*  var result = MessageBox.Show("是否保存更改？", "?", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK) */
          Apply_Internal();

            Close();
        }

        private void ApplyChanges_Btn_Click(object sender, System.EventArgs e)
        {
        }

        private void DeleteSelectedBtn_Click(object sender, System.EventArgs e)
        {
            var selectedItem = dirListView.SelectedItems;
            if (selectedItem.Count >= 0)
            {
                var dr = MessageBox.Show("是否删除：" + selectedItem[0].Text, "!", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK) dirListView.Items.Remove(selectedItem[0]);
            }
        }

        private void ChangeVCSBtn_Click(object sender, System.EventArgs e)
        {
            var result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
                VcsRoot =
                    folderBrowserDialog.SelectedPath;

            //Update UI
            VcsLabel.Text = VcsRoot;
        }

        private void AddDir_Btn_Click(object sender, System.EventArgs e)
        {
            var result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var chosenFolder = folderBrowserDialog.SelectedPath;
                if (Directory.Exists(chosenFolder))
                {
                    var relative = GetRelativePathOfAbsolute(
                        VcsRoot ?? throw new ArgumentNullException(nameof(VcsRoot)), chosenFolder);
                    if (string.IsNullOrWhiteSpace(relative))
                    {
                        MessageBox.Show("所选目录不在数据库包含范围内");
                        return;
                    }

                    AddListViewItem(relative, DirListViewTypeFolder);

                    return;
                }

                MessageBox.Show("所选目录不存在");
            }
        }

        private void AddFile_Btn_Click(object sender, System.EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var chosenFile = openFileDialog.FileName;
                if (File.Exists(chosenFile))
                {
                    var relative = GetRelativePathOfAbsolute(
                        VcsRoot ?? throw new ArgumentNullException(nameof(VcsRoot)), chosenFile);
                    if (string.IsNullOrWhiteSpace(relative))
                    {
                        MessageBox.Show("所选文件不在数据库包含范围内");
                        return;
                    }

                    AddListViewItem(relative, DirListViewTypeFile);

                    return;
                }

                MessageBox.Show("所选文件不存在");
            }
        }

        private IEnumerable<(string RelativePath, string Type)> GetDirListViewData()
        {
            List<(string relativePath
                , string type)> schemasList = new();

            foreach (ListViewItem item in dirListView.Items)
            {
                if (item.SubItems.Count < DirListViewSubItemCount) continue;
                var data = (RelativePath: item.SubItems[0].Text, Type: item.SubItems[1].Text);
                schemasList.Add(data);
            }

            return schemasList;
        }

        private void Apply_Internal()
        {
            if (DatabaseSchema == null) return;
            //ReInitDbSchema
            DatabaseSchema.FileList = new List<string>();
            DatabaseSchema.DirList = new List<string>();
            var schemasList = GetDirListViewData();

            var dbSchemaFileList = DatabaseSchema.FileList as string[] ?? DatabaseSchema.FileList.ToArray();
            var dbSchemaDirList = DatabaseSchema.DirList as string[] ?? DatabaseSchema.DirList.ToArray();
            foreach (var valueTuple in schemasList)
                switch (valueTuple.Type)
                {
                    case DirListViewTypeFile:

                        if (!dbSchemaFileList.Contains(valueTuple.RelativePath))
                            DatabaseSchema.FileList = dbSchemaFileList.Append(valueTuple.RelativePath);

                        break;

                    case DirListViewTypeFolder:
                        if (!dbSchemaDirList.Contains(valueTuple.RelativePath))
                            DatabaseSchema.DirList = dbSchemaFileList.Append(valueTuple.RelativePath);

                        break;
                }

            foreach (var path in dbSchemaFileList) Logger.Trace($"DbSchema FilePath:{path}");
            foreach (var path in dbSchemaDirList) Logger.Trace($"DbSchema FolderPath:{path}");
        }

        private string GetStandardRelativePath(string relativePath)
        {
            relativePath = relativePath.Replace('/', '\\').TrimStart('\\').TrimEnd('\\');
            return relativePath;
        }

        /// <summary>
        ///     AbsRange: eg C:/User/fengy/Desktop/vscFolder
        ///     path: eg: C:/User/fengy/Desktop/vscFolder/folder/a.txt
        /// </summary>
        /// <param name="absRange"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private string GetRelativePathOfAbsolute(string absRange, string path)
        {
            var range = absRange.Replace('\\', '/');
            range = range.Replace('/', '\\').TrimEnd('\\');
            path = path.Replace('/', '\\').TrimEnd('\\');
            if (!path.Contains(range)) return "";

            var result = GetStandardRelativePath(path.Replace(range, ""));

            return result;
        }

        private bool IsDuplicateListViewItem(string relativePath, string type)
        {
            relativePath = GetStandardRelativePath(relativePath);
            var data = GetDirListViewData();

            if (data.Contains((relativePath, type)))
                return true;
            return false;
        }

        private void AddListViewItem_Internal(ListViewItem item)
        {
            dirListView.Items.Add(item);
        }

        private void AddListViewItem(string relativePath, string displayType)
        {
            var item = new ListViewItem(new[] { relativePath, displayType });
            if (IsDuplicateListViewItem(relativePath, displayType))
            {
                MessageBox.Show("已经添加!");
                return;
            }

            AddListViewItem_Internal(item);
        }
    }
}