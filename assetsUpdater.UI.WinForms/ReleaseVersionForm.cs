using assetsUpdater.Interfaces;
using assetsUpdater.UI.WinForms.Dialogs;

using NLog;

using System;
using System.IO;
using System.Windows.Forms;

namespace assetsUpdater.UI.WinForms
{
    public partial class ReleaseVersionForm : Form
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public DataManager ChosenDataManager { get; set; }
        public DataManager OriginalDataManager { get; set; }
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的 属性“OriginalDataManager”必须包含非 null 值。请考虑将 属性 声明为可以为 null。
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的 属性“ChosenDataManager”必须包含非 null 值。请考虑将 属性 声明为可以为 null。

        public ReleaseVersionForm()
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的 属性“ChosenDataManager”必须包含非 null 值。请考虑将 属性 声明为可以为 null。
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的 属性“OriginalDataManager”必须包含非 null 值。请考虑将 属性 声明为可以为 null。
        {
            InitializeComponent();
            InitUi();
        }

        private void ConditionIsEnableControl(bool condition)
        {
            VcsFolder_Textbox.Enabled = condition;
            BrowseVcsFolder_Btn.Enabled = condition;
            DbModify_Btn.Enabled = condition;
            Release_Btn.Enabled = condition;
        }

        private string _ldbNameUi = "";
        private string _vcsFolderUi = "";

        private void UpdateUi()
        {
            ldbName_Label.Text = _ldbNameUi;
            VcsFolder_Textbox.Text = _vcsFolderUi;
            //ConditionIsEnableControl(true);
        }

        private void InitUi()
        {
            //Disable Default Control
            ConditionIsEnableControl(false);
            InputDb_openFileDialog.Filter = "数据文件|*.pokedb";

            InputDb_openFileDialog.CheckFileExists = true;
            InputDb_openFileDialog.CheckPathExists = true;

            vcs_folderBrowserDialog.AutoUpgradeEnabled = true;

            vcs_folderBrowserDialog.ShowNewFolderButton = true;

            vcs_folderBrowserDialog.Description = "请选择根目录，通常为(.minecraft)或.minecraft上级目录";
            vcs_folderBrowserDialog.UseDescriptionForTitle = true;
            ldbName_Label.Text = _ldbNameUi;
        }

        private void Ldb_Btn_Click(object sender, System.EventArgs e)
        {
            var r = InputDb_openFileDialog.ShowDialog();

            if (r == DialogResult.OK)
            {
                Logger.Info("User Chosen file:{InputDb_openFileDialog.FileName}");
                try
                {
                    ChosenDataManager = new LocalDataManager(InputDb_openFileDialog.FileName);
                    var result = ChosenDataManager.IsDataValid().Result;

                    if (!result)
                    {
                        MessageBox.Show("数据文件不存在或不合法");
                        return;
                    }
                }
                catch (Exception exception)
                {
                    Logger.Error($"读取数据文件失败,{exception}");
                    return;
                }

                _ldbNameUi = Path.GetFileName(ChosenDataManager.DatabasePath) ?? "";
                _vcsFolderUi = ChosenDataManager.DbData.Data().Config.VersionControlFolder;

                OriginalDataManager = new LocalDataManager((IDbData)ChosenDataManager.DbData.Clone());            //ReEnableControl

                ConditionIsEnableControl(true);
                UpdateUi();
            }
        }

        private void Release_Btn_Click(object sender, System.EventArgs e)
        {
            try
            {
                var result = new ReleaseConfirmationForm(ChosenDataManager ?? throw new ArgumentNullException(nameof(ChosenDataManager), "Db can't be null"), OriginalDataManager ?? throw new ArgumentNullException(nameof(OriginalDataManager), "Db can't be null")).ShowDialog(this);
                Logger.Info($"ReleaseConfirmationForm return:{result}");

                if (result == DialogResult.OK)
                {
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }

        private void DbModify_Btn_Click(object sender, System.EventArgs e)
        {
            if (ChosenDataManager == null)
            {
                MessageBox.Show("请选择一个版本的数据库.");
                return;
            }
            var result = new ConfigModifyForm(ChosenDataManager).ShowDialog(this);

            Logger.Info($"ConfigModifyForm return:{result}");

            if (result == DialogResult.OK)
            {
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            GC.Collect();
        }

        private void BrowseVcsFolder_Btn_Click(object sender, System.EventArgs e)
        {
            if (ChosenDataManager == null)
            {
                MessageBox.Show("请先选择一个数据库");

                return;
            }
            var result = vcs_folderBrowserDialog.ShowDialog();
            Logger.Debug("BrowseVcsFolder_Btn_Click Result: {0}", result);
            if (result == DialogResult.OK)
            {
                //Update Properties

                ChosenDataManager.DbData.Data().Config.VersionControlFolder = vcs_folderBrowserDialog.SelectedPath;

                //Update UI
                _ldbNameUi = vcs_folderBrowserDialog.SelectedPath;
            }
            UpdateUi();
        }

        private void ReleaseVersionForm_Activated(object sender, System.EventArgs e)
        {
            UpdateUi();
        }

        private void DownloadDb_Btn_Click(object sender, System.EventArgs e)
        {
            using var df = new DownloadDatabaseForm();
            if (df.ShowDialog(this) == DialogResult.OK)
            {
                OriginalDataManager = new LocalDataManager(df.DbData)
                {
                };

                //Update UI
                ldbName_Label.Text = vcs_folderBrowserDialog.SelectedPath;
            }
            else
            {
                MessageBox.Show("下载失败!");
            }
        }
    }
}