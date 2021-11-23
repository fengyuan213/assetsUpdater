#region Using

using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Threading.Tasks;
using assetsUpdater.Interfaces;
using assetsUpdater.Model.Network;

#endregion

namespace assetsUpdater.Network
{
    public class AsyncDownload : IDownloadUnit
    {
        public AsyncDownload(DownloadPackage downloadPackage, DownloadSetting downloadSetting = null,
            WebClient webClient = null)
        {
            DownloadPackage = downloadPackage;

            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) =>
            {
                // local dev, just approve all certs
                var development = true;
                if (development) return true;
                return errors == SslPolicyErrors.None;
            };
            DownloadSetting = downloadSetting ?? new DownloadSetting
            {
                IsDownloadTempEnabled = true
            };
            WebClient = webClient ?? new WebClient();
            WebClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
            WebClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
        }


        public WebClient WebClient { get; set; }
        public Task CurrentDownloadingTask { get; set; }
        public string DisplaySpeed => null;
        public DownloadPackage DownloadPackage { get; set; }


        public DownloadMode DownloadMode { get; set; }
        public double BytesReceivedPerSec { get; set; }
        public double Progress { get; private set; }
        public long BytesReceived { get; private set; }
        public long BytesToReceive => DownloadPackage.SizeTotal - BytesReceived;

        public Task Wait()
        {
            return CurrentDownloadingTask ?? Task.CompletedTask;
        }

        public Task Start()
        {
            CurrentDownloadingTask = WebClient.DownloadFileTaskAsync(DownloadPackage.Uri.OriginalString,
                DownloadPackage.LocalPath);


            return Task.CompletedTask;
        }


        public DownloadSetting DownloadSetting { get; set; }


        private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Debugger.Break();
            if (!(e.Error != null || e.Cancelled))
                if (DownloadSetting.IsDownloadTempEnabled)
                    DownloadPackage.CleanDownloadCache();
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            BytesReceived = e.BytesReceived;
            Progress = e.ProgressPercentage;
        }
    }
}