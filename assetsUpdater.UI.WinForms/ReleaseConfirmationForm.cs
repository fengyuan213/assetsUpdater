#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using assetsUpdater.Model;
using assetsUpdater.StorageProvider;
using assetsUpdater.UI.WinForms.Utils;
using NLog;

#endregion

namespace assetsUpdater.UI.WinForms
{
    public partial class ReleaseConfirmationForm : Form
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly BackgroundWorker _initBg = new();

        public ReleaseConfirmationForm(DataManager currentDataManager, DataManager originalDataManager)
        {
            OriginalDataManager = originalDataManager;
            CurrentDataManager = currentDataManager;
            InitializeComponent();

            InitializeUi();
            Initialize();
        }

        public DataManager OriginalDataManager { get; set; }
        public DataManager CurrentDataManager { get; set; }
        public RemoteDataManager RemoteDataManager { get; set; }
        public AssertUpgradePackage AssertUpgradePackage { get; set; }

        private async void InitBgDoWork(object? sender, DoWorkEventArgs e)
        {
            Logger.Info("Thread name:");

            // SynchronizationContext.Current.CreateCop y()
            await InitializeData().ConfigureAwait(false);
        }

        private async void InitBgRunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            _initBg.DoWork -= InitBgDoWork;
            _initBg.ProgressChanged -= InitBgProgressChanged;
            _initBg.RunWorkerCompleted -= InitBgRunWorkerCompleted;
            await UpdateListView().ConfigureAwait(true);
        }

        private void InitBgProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
        }

        private void Initialize()
        {
            var parallelOptions = new ParallelOptions();

            //Parallel.Invoke(parallelOptions,);
        }

        private async Task InitializeData()
        {
            var vcs = CurrentDataManager.StorageProvider.GetBuildInDbData().Config.VersionControlFolder;

            try
            {
                var provider = await DataManager
                    .BuildDatabase<FileDatabase>(CurrentDataManager.StorageProvider.GetBuildInDbData().Config)
                    .ConfigureAwait(false);
                RemoteDataManager = new RemoteDataManager(provider);
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
                MessageBox.Show("Unable to build database");
                Close(DialogResult.Abort);
            }

            if (RemoteDataManager != null)
                if (!await RemoteDataManager.IsDataValid())
                {
                    MessageBox.Show("Invalid RDB");
                    Close(DialogResult.Abort);
                }

            /*//Remote database builder
            DatabaseBuilder db = new DatabaseBuilder(new FileDatabase()
            {
            });
            db.BuildDatabaseWithUrl();*/
        }

        private void InitializeUi()
        {
            _initBg.WorkerReportsProgress = true;
            _initBg.WorkerSupportsCancellation = true;
            _initBg.DoWork += InitBgDoWork;
            _initBg.ProgressChanged += InitBgProgressChanged;
            _initBg.RunWorkerCompleted += InitBgRunWorkerCompleted;
            _initBg.RunWorkerAsync();
            dirListView.AutoArrange = false;
            //Initialize Coloring Rule
            var originalV = GetVersionString(OriginalDataManager.StorageProvider.GetBuildInDbData().Config.MajorVersion,
                OriginalDataManager.StorageProvider.GetBuildInDbData().Config.MinorVersion);
            var currentV = GetVersionString(CurrentDataManager.StorageProvider.GetBuildInDbData().Config.MajorVersion,
                CurrentDataManager.StorageProvider.GetBuildInDbData().Config.MinorVersion);

            OriginalVer_Label.Text = originalV;
            CurrentVer_Label.Text = currentV;
            UpdateUrl_Label.Text = RemoteDataManager?.StorageProvider.GetBuildInDbData().Config.UpdateUrl;
        }

        private string GetVersionString(int major, int mirror)
        {
            var s = major + "." + mirror;
            return s;
        }

        private void Cancel_Btn_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void Confirm_Btn_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (await ReleasePreCheck().ConfigureAwait(false))
                {
                    var vFolderKey = "testdata";
                    var uploadKeys = new List<string>()
                        ;
                    uploadKeys.AddRange(AssertUpgradePackage?.AddFile.Select(file => file.RelativePath) ??
                                        Array.Empty<string>());
                    uploadKeys.AddRange(AssertUpgradePackage?.DifferFile.Select(file => file.RelativePath) ??
                                        Array.Empty<string>());

                    Logger.Debug("Files to upload:");
                    uploadKeys.ForEach(s => Logger.Debug(s));
                    var localRoot = RemoteDataManager?.StorageProvider.GetBuildInDbData().Config.DownloadAddressBuilder
                        .LocalRootPath;
                    if (string.IsNullOrWhiteSpace(localRoot))
                        localRoot = RemoteDataManager?.StorageProvider.GetBuildInDbData().Config.VersionControlFolder;

                    if (uploadKeys.Count < 1)
                    {
                        MessageBox.Show("没有文件需要更新！");
                        return;
                    }

                    var rpf = new ReleaseProcessingForm(uploadKeys,
                        RemoteDataManager?.StorageProvider.GetBuildInDbData().Config.DownloadAddressBuilder ??
                        throw new ArgumentNullException(nameof(RemoteDataManager)), vFolderKey);
                    var result = rpf.ShowDialog(this);
                    Logger.Info($"Release Window Result:{result}");
                    Close();
                }
                else
                {
                    MessageBox.Show("请检查重要参数是否缺失，发布前检查失败");
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }

        private async Task<bool> ReleasePreCheck()
        {
            //todo:

            if (AssertUpgradePackage == null) return false;

            if (RemoteDataManager == null) return false;
            await RemoteDataManager.IsDataValid().ConfigureAwait(false);
            return true;
        }

        private async void debug_btn_Click(object sender, System.EventArgs e)
        {
            await InitializeData();
        }

        private async Task UpdateListView()
        {
            if (RemoteDataManager == null) return;
            var data = RemoteDataManager.StorageProvider.GetBuildInDbData();
            AssertUpgradePackage = await AssertVerify
                .DatabaseCompare(RemoteDataManager.StorageProvider, OriginalDataManager.StorageProvider)
                .ConfigureAwait(false);

            foreach (var addFile in AssertUpgradePackage.AddFile)
                AddListViewItem(UpgradeFileType.AddFile, addFile.RelativePath);
            foreach (var differFile in AssertUpgradePackage.DifferFile)
                AddListViewItem(UpgradeFileType.DifferFile, differFile.RelativePath);
            foreach (var deleteFile in AssertUpgradePackage.DeleteFile)
                AddListViewItem(UpgradeFileType.DeleteFile, deleteFile.RelativePath);
        }

        private void AddListViewItem(UpgradeFileType type, string relativePath, string absPath = "null")
        {
            dirListView.Items.Add(
                new ListViewItem(new[] { UpgradeFileEnumStringConverter.Convert(type), relativePath }));
        }

        private void ReleaseConfirmationForm_Load(object sender, System.EventArgs e)
        {
        }

        private void Close(DialogResult e)
        {
            DialogResult = e;
            Close();
        }

        private void ReleaseConfirmationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect(3, GCCollectionMode.Forced, false);
        }
    }
}