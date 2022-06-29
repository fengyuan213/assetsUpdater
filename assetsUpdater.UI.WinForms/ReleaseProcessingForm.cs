#region Using

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows.Forms;
using assetsUpdater.Interfaces;
using assetsUpdater.Network;
using assetsUpdater.Tencent;
using assetsUpdater.Tencent.Network;
using assetsUpdater.Utils;
using NLog;

#endregion

namespace assetsUpdater.UI.WinForms
{
    public partial class ReleaseProcessingForm : Form
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        public IEnumerable<string> UnitKeys { get; }
        public TencentUnitBuilder TencentUnitBuilder { get; }

        public UploadQueue UploadQueue { get; set; } = new(10);
        public ReleaseProcessingForm(IEnumerable<string> keys, IAddressBuilder addressBuilder, string vFolderKey)
        {
            UnitKeys = keys;
            TencentUnitBuilder = new TencentUnitBuilder(addressBuilder, vFolderKey);
            InitializeComponent();
            InitializeUi();
        }
        private void InitializeUi()
        {
            UpdateStatus_Timer.Stop();
            UpdateStatus_Timer.Enabled = true;
            UpdateStatus_Timer.Interval = 1000;
            UpdateStatus_Timer.Start();

            UploadQueue.ErrorUnits.CollectionChanged += ErrorUnits_CollectionChanged;
        }

        private void ErrorUnits_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add || e.NewItems?.Count > 0)
                foreach (IUploadUnit eNewItem in e.NewItems ?? new List<IUploadUnit>())
                    AddLogListViewMessage(
                        $"无法上传文件：{eNewItem.UploadPackage.FileLocalPath}{eNewItem} 请稍后重试或访问调试Log来获得更多信息",
                        LogLevel.Error);
        }

        private void AddLogListViewMessage(string msg, LogLevel lvl)
        {
            Logger.Log(lvl, msg);
            log_listView.Items.Add(new ListViewItem(new[] { msg }));
        }

        private async Task StartUpload()
        {
            Logger.Info("Start Building Upload Units");
            var units = await BuildUploadUnits().ConfigureAwait(false);
            Logger.Info("Upload Units Build Finished, queueing upload");
            await UploadQueue.QueueUpload(units).ConfigureAwait(false);
            Logger.Info($"Queueing finished, upload started, Files Count:{UploadQueue.AllUnits.Count}");
        }

        private async Task WaitUpload()
        {
            Logger.Info("Start Waiting Upload Loop");
            await UploadQueue.WaitAll().ConfigureAwait(false);
            Logger.Info("Upload Queue upload finished");
        }

        private async Task<IEnumerable<TencentUploadUnit>> BuildUploadUnits()
        {
            var units = TencentUnitBuilder.Build(UnitKeys);

            return units;
        }

        private double GetTotalProgressBarProgress()
        {
            //upload 70 100
            //BuildUploadUnits()50 100
            //80
            var uploadProgress = UploadQueue.Progress * 100;
            var progress = uploadProgress;
            //progress fallback
            if (progress<0)
            {
                return 0;
            }
            return progress
                ;
        }

        private async Task UpdateStatus()
        {
            //Update Ui Control

            AllUnitCount_Label.Text = UploadQueue.AllUnits.Count.ToString();
            CurrentUnitCount_Label.Text = UploadQueue.CurrentUploadingUnits.Count.ToString();
            WaitingUnitCount_Label.Text = UploadQueue.WaitingUnits.Count.ToString();
            ErrorUnitCount_Label.Text = UploadQueue.ErrorUnits.Count.ToString();
            UploadProgress_Label.Text =
                $@"{FileSizeParser.ParseAuto(UploadQueue.BytesSent)}/{FileSizeParser.ParseAuto(UploadQueue.TotalBytesToUpload)}";
            if (UploadQueue.CurrentUploadingUnits.Count<1)
            {
                return;
            }

            if (UploadQueue.CurrentUploadingUnits.Count > 0)
                currentProcessBar.Value = (int)(UploadQueue.CurrentUploadingUnits[0].Progress*100);
            else
                currentProcessBar.Value = totalProccessBar.Value;
            totalProccessBar.Value = (int)GetTotalProgressBarProgress();

            await LogUpdateStatus().ConfigureAwait(false);
        }

        private Task LogUpdateStatus()
        {
            //alias
            var progress = UploadQueue.Progress;
            var uploadedB = UploadQueue.BytesSent;
            var totalB = UploadQueue.TotalBytesToUpload;
            var allUnits = UploadQueue.AllUnits;
            var waitingUnits = UploadQueue.WaitingUnits;
            var errorUnits = UploadQueue.ErrorUnits;
            var currentUnits = UploadQueue.CurrentUploadingUnits;
            Logger.Debug("--- Upload Trace Data Start ---");
            Logger.Debug(
                $"Progress:{progress},Uploaded:{FileSizeParser.ParseAuto(uploadedB)}, Total:{FileSizeParser.ParseAuto(totalB)}");
            Logger.Info($"Uploaded Bytes:{uploadedB}, Total Bytes:{totalB}");
            Logger.Debug(
                $"Units Count- Waiting:{waitingUnits.Count} All:{allUnits.Count} Current:{currentUnits.Count} Error:{errorUnits.Count}");
            Logger.Debug("Lists of waiting, error and current");
            LogUnits("Waiting", waitingUnits);
            LogUnits("Error", errorUnits);
            LogUnits("Current", waitingUnits);

            Logger.Debug("--- Upload Trace Data End ---");

            return Task.CompletedTask;
        }

        private void LogUnits(in string unitName, in IEnumerable<IUploadUnit> units)
        {
            foreach (var unit in units)
            {
                //alias
                var key = unit.UploadPackage.ResourceKey;
                var localPath = unit.UploadPackage.FileLocalPath;
                var p = unit.Progress;
                var byteSent = unit.BytesSent;
                var totalByte = unit.TotalBytes;

                Logger.Debug(
                    $"Status:{unitName}->Key:{key} localPath:{localPath}\n Byte Sent:{byteSent}, Byte Total:{totalByte}, Progress:{p}");
            }
        }

        private async void UpdateStatus_Timer_Tick(object sender, System.EventArgs e)
        {
            //UpdateStatus_Timer.Enabled=false;
            await UpdateStatus().ConfigureAwait(true);
        }

        private async void Start_Btn_Click(object sender, System.EventArgs e)
        {
            await StartUpload().ConfigureAwait(false);
            await WaitUpload().ConfigureAwait(false);
            

        }

        private void ReleaseProcessingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateStatus_Timer.Dispose();
        }

        private void PrintStatus_Btn_Click(object sender, System.EventArgs e)
        {
            //UpdateStatus_Timer.Interval = 500;
            //UpdateStatus_Timer.Start();
            GC.Collect(1);
            GC.WaitForPendingFinalizers();
            UpdateStatus().Wait();
        }

        private void ReleaseProcessingForm_Load(object sender, System.EventArgs e)
        {
        }
    }
}