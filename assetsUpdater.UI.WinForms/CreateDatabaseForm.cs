#region Using

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using assetsUpdater.AddressBuilder;
using assetsUpdater.Model.StorageProvider;
using assetsUpdater.StorageProvider;
using assetsUpdater.Tencent.AddressBuilders;
using NLog;

#endregion

namespace assetsUpdater.UI.WinForms
{
    public partial class CreateDatabaseForm : Form
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private DbConfig _dbConfigInstance;
        private string _vcsPath;
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的 字段“_vcsPath”必须包含非 null 值。请考虑将 字段 声明为可以为 null。
        public CreateDatabaseForm()
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的 字段“_vcsPath”必须包含非 null 值。请考虑将 字段 声明为可以为 null。
        {
            InitializeComponent();
            InitializeUi();
        }

        public DbConfig DbConfig
        {
            get { return _dbConfigInstance ??= new DbConfig(_vcsPath); }
        }

        private void InitializeUi()
        {
            Vcs_label.Text = "无";
            dbSaveFileDialog.CreatePrompt = false;
            dbSaveFileDialog.OverwritePrompt = true;
            dbSaveFileDialog.AddExtension = true;
            dbSaveFileDialog.DefaultExt = "pokedb";
            dbSaveFileDialog.RestoreDirectory = true;
            dbSaveFileDialog.ValidateNames = true;
            dbSaveFileDialog.Filter = "数据|.pokedb";
            dbSaveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            /*dbs
            dbOpenFileDialog.
                InputDb_openFileDialog.Filter = "数据文件|*.pokedb";

            InputDb_openFileDialog.CheckFileExists = true;
            InputDb_openFileDialog.CheckPathExists = true;


            vcs_folderBrowserDialog.AutoUpgradeEnabled = true;

            vcs_folderBrowserDialog.ShowNewFolderButton = true;

            vcs_folderBrowserDialog.Description = "请选择根目录，通常为(.minecraft)或.minecraft上级目录";
            vcs_folderBrowserDialog.UseDescriptionForTitle = true;
            ldbName_Label.Text = _ldbNameUi;
            */
        }

        private Type GetSelectedAddressBuilderType()
        {
            Type t;
            switch (AddressBuilderType_ComboBox.SelectedItem)
            {
                case "TencentCloud":
                    t = typeof(TencentAddressBuilder);

                    break;
                case "General":
                    t = typeof(DefaultAddressBuilder);

                    break;
                default:
                    t = typeof(DefaultAddressBuilder);
                    break;
            }

            return t;
        }

        private void UpdateUi()
        {
            try
            {
                //Update data
                Vcs_label.Text = DbConfig.VersionControlFolder;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (int.TryParse(MajorV_TextBox.Text, out var major)) DbConfig.MajorVersion = major;
            if (int.TryParse(MinorV_TestBox.Text, out var minor)) DbConfig.MajorVersion = minor;
            DisplayVersion_Label.Text = major + "." + minor;
            DbConfig.UpdateUrl = UpdateUrl_TextBox.Text;

            //Logger.Info($"UI-{MethodBase.GetCurrentMethod()?.DeclaringType?.Name} Updated");

            Logger.Info("UI Updated");
        }


        private async Task<RemoteDataManager> ConstructDatabase()
        {
            var addrBuilderType = GetSelectedAddressBuilderType();
            if (addrBuilderType == null)
            {
                Logger.Info("There is no selected address builder, returning");
                return null;
            }

            var rootDownloadAddr = RootAddress_TextBox.Text;
            var typeD = typeDSercet_Textbox.Text;
            if (addrBuilderType == typeof(TencentAddressBuilder))
                DbConfig.DownloadAddressBuilder =
                    new TencentAddressBuilder(rootDownloadAddr, DbConfig.VersionControlFolder, typeD);
            else if (addrBuilderType == typeof(DefaultAddressBuilder))
                DbConfig.DownloadAddressBuilder =
                    new DefaultAddressBuilder(rootDownloadAddr, DbConfig.VersionControlFolder);

            Logger.Info($"Build database of type {addrBuilderType.Name} started:");
            try
            {
                var db = await DataManager.BuildDatabase<FileDatabase>(DbConfig).ConfigureAwait(false);

                var rdm = new RemoteDataManager(db);

                var isValid = await rdm.IsDataValid();

                if (!isValid) MessageBox.Show("构建成功，但有些数据校验失败，有些数据可能不正确");
                Logger.Info("Database construction finished");
                return rdm;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);

                throw;
            }
        }

        private async void CreateDbBtn_Click(object sender, System.EventArgs e)
        {
            try
            {
                var rdm = await ConstructDatabase().ConfigureAwait(false);
                if (
                    dbSaveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    var path = dbSaveFileDialog.FileName;
                    Logger.Info($"Selected export path:{path},exporting...");

                    await rdm.StorageProvider.Export(path);
                    Logger.Info($"Export to {path} completed...");
                    MessageBox.Show("导出成功!");
                    /*    var r = MessageBox.Show("你想打开输出文件所在目录吗?", "信息", MessageBoxButtons.YesNo);
                        if (r == DialogResult.Yes)
                        {
                            using var p = Process.Start($"explorer {path}");
                        }*/
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show(exception.ToString());
            }
        }


        #region UI Event Handlers

        private void ModifyDirlist_Btn_Click(object sender, System.EventArgs e)
        {
            var form = new ModifyDirListForm(DbConfig.VersionControlFolder, DbConfig.DatabaseSchema);
            form.ShowDialog(this);
        }


        private void ChooseVcs_Btn_Click(object sender, System.EventArgs e)
        {
            var r = vcs_BrowserDialog.ShowDialog(this);
            switch (r)
            {
                case DialogResult.OK:

                    _vcsPath = vcs_BrowserDialog.SelectedPath.TrimEnd('\\').TrimEnd('/');

                    break;
            }

            UpdateUi();
        }

        private async void debug_LoadDefaultBtn_Click(object sender, System.EventArgs e)
        {
            await ConstructDatabase().ConfigureAwait(false);
        }

        private async void CreateDbTestBtn_Click(object sender, System.EventArgs e)
        {
           
        }

        private void CreateDatabaseForm_Load(object sender, System.EventArgs e)
        {
        }

        private void MajorV_TextBox_Leave(object sender, System.EventArgs e)
        {
            UpdateUi();
        }


        private void MinorV_TestBox_Leave(object sender, System.EventArgs e)
        {
            UpdateUi();
        }

        private void UpdateUrl_TextBox_Leave(object sender, System.EventArgs e)
        {
            UpdateUi();
        }

        private void RootAddress_TextBox_Leave(object sender, System.EventArgs e)
        {
            UpdateUi();
        }

        #endregion
    }
}