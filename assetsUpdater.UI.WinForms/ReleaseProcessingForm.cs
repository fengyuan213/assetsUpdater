using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using assetsUpdater.Interfaces;
using assetsUpdater.Model;
using assetsUpdater.Model.StorageProvider;
using assetsUpdater.Network;
using assetsUpdater.Tencent.Network;
using assetsUpdater.Utils;
using NLog;

namespace assetsUpdater.UI.WinForms
{
    public partial class ReleaseProcessingForm : Form
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
      
        public IEnumerable<string> UnitKeys { get;  }
        public UploadUnitBuilder UploadUnitBuilder { get;  }
        public ReleaseProcessingForm(IEnumerable<string> keys,IAddressBuilder addressBuilder,string vFolderKey)
        {
            UnitKeys = keys;
            UploadUnitBuilder = new UploadUnitBuilder(addressBuilder, vFolderKey);
            InitializeComponent();
        }
        
        public UploadQueue UploadQueue { get; set; } = new UploadQueue(10);
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
            var units = await UploadUnitBuilder.Build(UnitKeys).ConfigureAwait(false);

            return units;
        }

        private Task UpdateStatus()
        {
            //alias
            var progress = UploadQueue.Progress;
            var uploadedB = UploadQueue.BytesSent;
            var totalB = UploadQueue.TotalBytesToUpload;
            var allUnits = UploadQueue.AllUnits;
            var waitingUnits = UploadQueue.WaitingUnits;
            var errorUnits= UploadQueue.ErrorUnits;
            var currentUnits = UploadQueue.CurrentUploadingUnits;
            Logger.Debug("--- Upload Trace Data Start ---");
            Logger.Debug($"Progress:{progress},Uploaded:{FileSizeParser.ParseAuto(uploadedB)}, Total:{FileSizeParser.ParseAuto(totalB)}");
            Logger.Debug($"Uploaded Bytes:{uploadedB}, Total Bytes:{totalB}");
            Logger.Debug($"Units Count- Waiting:{waitingUnits.Count} All:{allUnits.Count} Current:{currentUnits.Count} Error:{errorUnits.Count}");
            Logger.Debug("Lists of waiting, error and current");
            LogUnits("Waiting",waitingUnits);
            LogUnits("Error", errorUnits);
            LogUnits("Current", waitingUnits);
           
            Logger.Debug("--- Upload Trace Data End ---");
            void LogUnits(in string unitName,in IEnumerable<IUploadUnit> units)
            {
                foreach (var unit in units)
                {
                 
                    //alias
                    var key = unit.UploadPackage.ResourceKey;
                    var localPath = unit.UploadPackage.FileLocalPath;
                    var p = unit.Progress;
                    var byteSent = unit.BytesSent;
                    var totalByte = unit.TotalBytes;

                    Logger.Debug($"Status:{unitName}->Key:{key} localPath:{localPath}\n Byte Sent:{byteSent}, Byte Total:{totalByte}, Progress:{p}");
                }
            }

            return Task.CompletedTask;
        }
        private async void UpdateStatus_Timer_Tick(object sender, System.EventArgs e)
        {
            //UpdateStatus_Timer.Enabled=false;
            //await UpdateStatus().ConfigureAwait(true);
            UpdateStatus().Wait();

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
           
            try
            {
               // UpdateStatus_Timer.Stop();
                //UpdateStatus_Timer.Interval = 3000;
                //UpdateStatus_Timer.Start();

                UpdateStatus().Wait();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                
            }
           
        }
    }
}
