#region Using

using assetsUpdater.Interfaces;

using Downloader;

using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

using DownloadPackage = assetsUpdater.Model.Network.DownloadPackage;
using DownloadProgressChangedEventArgs = Downloader.DownloadProgressChangedEventArgs;

#endregion

namespace assetsUpdater.Network
{
    public class MPartDownload : IDownloadUnit
    {
        private bool _isFinishedDownload;

        public MPartDownload(DownloadPackage downloadPackage, DownloadSetting? downloadSetting = null)
        {
            DownloadPackage = downloadPackage;
            DownloadSetting = downloadSetting ?? new DownloadSetting { IsDownloadTempEnabled = true };

            DownloadService = new DownloadService(GetDownloadConfiguration());
            DownloadService.ChunkDownloadProgressChanged += OnChunkDownloadProgressChanged;
            DownloadService.DownloadProgressChanged += OnDownloadProgressChanged;
            DownloadService.DownloadFileCompleted += OnDownloadFileCompleted;
            DownloadService.DownloadStarted += OnDownloadStarted;
        }

        public DownloadService DownloadService { get; private set; }
        public DownloadSetting DownloadSetting { get; set; }

        public DownloadPackage DownloadPackage { get; set; }
        public DownloadMode DownloadMode { get; set; }
        public double BytesReceivedPerSec { get; set; }
        public double Progress { get; private set; }

        public long BytesReceived { get; private set; }
        public long BytesToReceive => DownloadPackage.SizeTotal - BytesReceived;

        public Task Wait()
        {
            while (true)
                if (_isFinishedDownload)
                    return Task.CompletedTask;
        }

        public async Task Start()
        {
            await DownloadService.DownloadFileTaskAsync(DownloadPackage.Uri.OriginalString, DownloadPackage.LocalPath)
                .ConfigureAwait(false);

            //return Task.CompletedTask;
        }

        private static DownloadConfiguration GetDownloadConfiguration()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "1";
            var cookies = new CookieContainer();
            //cookies.Add(new Cookie("download-type", "test") { Domain = "domain.com" });

            return new DownloadConfiguration
            {
                BufferBlockSize = 8000, // usually, hosts support max to 8000 bytes, default values is 8000
                ChunkCount = 16, // file parts to download, default value is 1
                MaximumBytesPerSecond = -1, // download speed limited to 1MB/s, default values is zero or unlimited
                MaxTryAgainOnFailover = int.MaxValue, // the maximum number of times to fail
                OnTheFlyDownload = true, // caching in-memory or not? default values is true
                ParallelDownload = true, // download parts of file as parallel or not. Default value is false
                TempDirectory =
                    Path.GetTempPath(), // Set the temp path for buffering chunk files, the default path is Path.GetTempPath()
                Timeout = 1000, // timeout (millisecond) per stream block reader, default values is 1000
                RequestConfiguration =
                {
                    // config and customize request headers
                    Accept = "*/*",
                    CookieContainer = cookies,
                    Headers = new WebHeaderCollection(), // { Add your custom headers }
                    KeepAlive = true,
                    ProtocolVersion = HttpVersion.Version11, // Default value is HTTP 1.1
                    UseDefaultCredentials = false,
                    UserAgent = $"AssertsUpdater/{version}"
                }
            };
        }

        private void OnDownloadStarted(object? sender, DownloadStartedEventArgs e)
        {
            Console.WriteLine($"Downloading {Path.GetFileName(e.FileName)} ...TotalSize:{e.TotalBytesToReceive}");
        }

        private async void OnDownloadFileCompleted(object? sender, AsyncCompletedEventArgs e)
        {
            //if (e.Cancelled == true) return;
            _isFinishedDownload = true;

            if (DownloadSetting.IsDownloadTempEnabled) await DownloadPackage.CleanDownloadCache();
            if (e.Cancelled)
                Console.WriteLine("Download canceled!");
            else if (e.Error != null)
                Console.Error.WriteLine(e.Error);
            else
                Console.WriteLine("Download completed successfully.");
        }

        private void OnChunkDownloadProgressChanged(object? sender, DownloadProgressChangedEventArgs e)
        {
            // progress.Tick((int)(e.ProgressPercentage * 100));
        }

        private void OnDownloadProgressChanged(object? sender, DownloadProgressChangedEventArgs e)
        {
            BytesReceived = e.ReceivedBytesSize;
            BytesReceivedPerSec = e.BytesPerSecondSpeed;

            Progress = e.ProgressPercentage;
        }

        private static void UpdateTitleInfo(DownloadProgressChangedEventArgs e)
        {
            var nonZeroSpeed = e.BytesPerSecondSpeed + 0.0001;
            var estimateTime = (int)((e.TotalBytesToReceive - e.ReceivedBytesSize) / nonZeroSpeed);
            var isMinutes = estimateTime >= 60;
            var timeLeftUnit = "seconds";

            if (isMinutes)
            {
                timeLeftUnit = "minutes";
                estimateTime /= 60;
            }

            if (estimateTime < 0)
            {
                estimateTime = 0;
                timeLeftUnit = "unknown";
            }

            var avgSpeed = CalcMemoryMensurableUnit(e.AverageBytesPerSecondSpeed);
            var speed = CalcMemoryMensurableUnit(e.BytesPerSecondSpeed);
            var bytesReceived = CalcMemoryMensurableUnit(e.ReceivedBytesSize);
            var totalBytesToReceive = CalcMemoryMensurableUnit(e.TotalBytesToReceive);
            var progressPercentage = $"{e.ProgressPercentage:F3}".Replace("/", ".");

            Console.Title = $"{progressPercentage}%  -  " +
                            $"{speed}/s (avg: {avgSpeed}/s)  -  " +
                            $"{estimateTime} {timeLeftUnit} left    -  " +
                            $"[{bytesReceived} of {totalBytesToReceive}]";
        }

        private static string CalcMemoryMensurableUnit(double bytes)
        {
            var kb = bytes / 1024; // · 1024 Bytes = 1 Kilobyte
            var mb = kb / 1024; // · 1024 Kilobytes = 1 Megabyte
            var gb = mb / 1024; // · 1024 Megabytes = 1 Gigabyte
            var tb = gb / 1024; // · 1024 Gigabytes = 1 Terabyte

            var result =
                tb > 1 ? $"{tb:0.##}TB" :
                gb > 1 ? $"{gb:0.##}GB" :
                mb > 1 ? $"{mb:0.##}MB" :
                kb > 1 ? $"{kb:0.##}KB" :
                $"{bytes:0.##}B";

            result = result.Replace("/", ".");
            return result;
        }
    }
}