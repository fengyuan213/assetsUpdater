using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using assetsUpdater.Model;
using assetsUpdater.Model.StorageProvider;
using assetsUpdater.StorageProvider;
using assetsUpdater.UI.WinForms.Utils;
using assetsUpdater.Utils;
using NLog;

namespace assetsUpdater.UI.WinForms
{
    public partial class ReleaseConfirmationForm : Form
    {
        private BackgroundWorker _initBg = new BackgroundWorker();
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public DataManager OriginalDataManager { get; set; }
        public DataManager CurrentDataManager { get; set; }
        public RemoteDataManager? RemoteDataManager { get; set; } = null;
        public AssertUpgradePackage? AssertUpgradePackage { get; set; }
        public ReleaseConfirmationForm(DataManager currentDataManager,DataManager originalDataManager)
        {
            OriginalDataManager= originalDataManager;
            CurrentDataManager= currentDataManager;
            InitializeComponent();
           
            InitializeUi();
            Initialize();
      
        }
        private void InitBgDoWork(object? sender, DoWorkEventArgs e)
        {
            
        }
        private void InitBgRunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            _initBg.DoWork -= InitBgDoWork;

        }

        private void InitBgProgressChanged(object? sender, ProgressChangedEventArgs e)
        {

        }

    

        private void Initialize()
        {
            var parallelOptions = new ParallelOptions();
            
            //Parallel.Invoke(parallelOptions,);
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
            var originalV = GetVersionString(OriginalDataManager.StorageProvider.GetBuildInDbData().Config.MajorVersion, OriginalDataManager.StorageProvider.GetBuildInDbData().Config.MirrorVersion);
            var currentV = GetVersionString(CurrentDataManager.StorageProvider.GetBuildInDbData().Config.MajorVersion, CurrentDataManager.StorageProvider.GetBuildInDbData().Config.MirrorVersion);

            OriginalVer_Label.Text = originalV;
            CurrentVer_Label.Text = currentV;
            UpdateUrl_Label.Text = RemoteDataManager?.StorageProvider.GetBuildInDbData().Config.UpdateUrl;
        }
        private string GetVersionString(int major, int mirror)
        {
            var s = major.ToString() + "." + mirror.ToString();
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
                DialogResult = DialogResult.OK;
                if (await ReleasePreCheck().ConfigureAwait(false))
                {
                    string vFolderKey = "testdata";
                    List<string> keys = new List<string>()
                    {

                    };
                    keys.AddRange(AssertUpgradePackage?.AddFile.Select((file => file.RelativePath)) ?? Array.Empty<string>());
                    keys.AddRange(AssertUpgradePackage?.DifferFile.Select((file => file.RelativePath)) ?? Array.Empty<string>());

                    Logger.Debug($"Files to upload:");
                    keys.ForEach((s => Logger.Debug(s)));

                    var rpf = new ReleaseProcessingForm(keys, RemoteDataManager?.StorageProvider.GetBuildInDbData().Config.DownloadAddressBuilder ?? throw new ArgumentNullException(nameof(RemoteDataManager)), vFolderKey);
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
                Logger.Error(e);
                
            }
           
            
        }

        private async Task<bool> ReleasePreCheck()
        {
            //todo:
            
            if (AssertUpgradePackage==null)
            {
                return false;
            }

            if (RemoteDataManager == null) return false;
            await RemoteDataManager.IsDataValid().ConfigureAwait(false);
            return true;
        }

        private async void debug_btn_Click(object sender, System.EventArgs e)
        {
            var vcs = CurrentDataManager.StorageProvider.GetBuildInDbData().Config.VersionControlFolder;

            try
            {

                var provider =await DataManager.BuildDatabase<FileDatabase>(CurrentDataManager.StorageProvider.GetBuildInDbData().Config,true).ConfigureAwait(false);
                RemoteDataManager = new RemoteDataManager(provider);
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
                MessageBox.Show("Unable to build database");
            }
          

            if (RemoteDataManager != null)
            {
                if (!await RemoteDataManager.IsDataValid())
                {
                    MessageBox.Show("Invalid RDB");

                }
                await UpdateListView().ConfigureAwait(true);

            }


            /*//Remote database builder
            DatabaseBuilder db = new DatabaseBuilder(new FileDatabase()
            {

            });
            db.BuildDatabaseWithUrl();*/


        }

        private async Task UpdateListView()
        {
            if (RemoteDataManager== null)
            {
                
                 return ;

            }
            DbData data = RemoteDataManager.StorageProvider.GetBuildInDbData();
            AssertUpgradePackage = await AssertVerify.DatabaseCompare(RemoteDataManager.StorageProvider, OriginalDataManager.StorageProvider).ConfigureAwait(false);
            
            foreach (var addFile in AssertUpgradePackage.AddFile)
            {
                AddListViewItem(UpgradeFileType.AddFile,addFile.RelativePath);
            }
            foreach (var differFile in AssertUpgradePackage.DifferFile)
            {
                AddListViewItem(UpgradeFileType.DifferFile,differFile.RelativePath);
            }
            foreach (var deleteFile in AssertUpgradePackage.DeleteFile)
            {
                AddListViewItem(UpgradeFileType.DeleteFile, deleteFile.RelativePath);
                
            }
            return;

        }
        private void AddListViewItem(UpgradeFileType type,string relativePath,string absPath="null")
        {
            
            dirListView.Items.Add(new ListViewItem(new[] { UpgradeFileEnumStringConverter.Convert(type), relativePath }));
        }

        private void ReleaseConfirmationForm_Load(object sender, System.EventArgs e)
        {

        }
    }
}
