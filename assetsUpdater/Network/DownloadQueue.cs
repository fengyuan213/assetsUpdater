#region Using

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using assetsUpdater.Interfaces;

#endregion

namespace assetsUpdater.Network
{
    public class DownloadQueue
    {
        public bool AutoRestartFailedDownload { get; set; } = true;
        public int MaxParallelAsyncDownloadCount { get; set; } = 50;
        public int MaxParallelMPartDownloadCount { get; set; } = 1;

        public virtual ObservableCollection<IDownloadUnit> CurrentDownloadingObj { get; } = new();

        public ObservableCollection<IDownloadUnit> ErrorList { get; } = new();

        public ObservableCollection<IDownloadUnit> WaitingList { get; } = new();

        public ObservableCollection<IDownloadUnit> AllDownloadObj { get; } = new();

        public double Progress
        {
            get
            {
                var progressTotal = AllDownloadObj.Sum(downloadUnit => downloadUnit.Progress);

                return progressTotal / AllDownloadObj.Count;
            }
        }

        public long BytesReceived
        {
            get

            {
                long receivedTotal = 0;
                foreach (var downloadUnit in AllDownloadObj)
                    receivedTotal += downloadUnit.BytesReceived;

                return receivedTotal;
            }
        }

        public long FileSizeTotal
        {
            get
            {
                long fileSizeTotal = 0;
                foreach (var downloadUnit in AllDownloadObj)
                    fileSizeTotal += downloadUnit.BytesToReceive;

                return fileSizeTotal;
            }
        }

        public async Task WaitAll()
        {
            Start:
            if (CurrentDownloadingObj.Count < 1 && WaitingList.Count < 1)
            {
                //Downlading finsihed
                if (ErrorList.Count >= 1)
                    //Error occured
                    if (AutoRestartFailedDownload)
                    {
                        foreach (var downloadUnit in ErrorList) WaitingList.Add(downloadUnit);
                        goto Start;
                    }
            }
            else
            {
                while (WaitingList.Count >= 1)
                {
                    var tasks = CurrentDownloadingObj.Select(downloadUnit => downloadUnit.Wait()).ToArray();
                    await Task.WhenAll(tasks).ConfigureAwait(true);
                }

                goto Start;
            }
        }

        public async Task QueueDownload(IEnumerable<IDownloadUnit> resultes)
        {
            foreach (var asyncDownload in resultes) await QueueDownload(asyncDownload).ConfigureAwait(false);
            await Task.CompletedTask.ConfigureAwait(false);
        }

        public async Task QueueDownload(IDownloadUnit downloadUnit)
        {
            var maxDownloadCount = downloadUnit.DownloadMode == DownloadMode.Async
                ? MaxParallelAsyncDownloadCount
                : MaxParallelMPartDownloadCount;

            if (downloadUnit.DownloadMode == DownloadMode.Async)
            {
                var asyncDownload = (AsyncDownload)downloadUnit;
                asyncDownload.WebClient.DownloadFileCompleted += Async_WebClient_DownloadFileCompleted;
            }
            else if (downloadUnit.DownloadMode == DownloadMode.MultiPart)
            {
                var mPartDownload = (MPartDownload)downloadUnit;

                //mPartDownload.DownloadService.ChunkDownloadProgressChanged += OnChunkDownloadProgressChanged;
                //mPartDownload.DownloadService.DownloadProgressChanged += OnDownloadProgressChanged;
                mPartDownload.DownloadService.DownloadFileCompleted += MPartDownload_DownloadCompleted;
                // mPartDownload.DownloadService.DownloadStarted += OnDownloadStarted;
            }
            else
            {
                throw new ArgumentException("DownloadMode不合法", nameof(DownloadMode));
            }

            AllDownloadObj.Add(downloadUnit);
            if (CurrentDownloadingObj.Count < maxDownloadCount)
            {
                CurrentDownloadingObj.Add(downloadUnit);

                await downloadUnit.Start().ConfigureAwait(true);
            }
            else
            {
                WaitingList.Add(downloadUnit);
            }
        }

        public Task RestartFailedDownload(IDownloadUnit downloadUnit)
        {
            var maxDownloadCount = downloadUnit.DownloadMode == DownloadMode.Async
                ? MaxParallelAsyncDownloadCount
                : MaxParallelMPartDownloadCount;

            if (ErrorList.Contains(downloadUnit))
            {
                if (CurrentDownloadingObj.Count < maxDownloadCount)
                    StartDownload(downloadUnit);
                else
                    WaitingList.Add(downloadUnit);
            }

            return Task.CompletedTask;
        }

        private async void StartDownload(IDownloadUnit downloadUnit)
        {
            WaitingList.Remove(downloadUnit);
            ErrorList.Remove(downloadUnit);
            await downloadUnit.Start().ConfigureAwait(true);

            CurrentDownloadingObj.Add(downloadUnit);
        }

        private void MPartDownload_DownloadCompleted(object? sender, AsyncCompletedEventArgs e)
        {
            lock (CurrentDownloadingObj)
            {
                for (var i = 0; i < CurrentDownloadingObj.Count; i++)
                {
                    var mPartDownload = (MPartDownload)CurrentDownloadingObj[i];
                    if (mPartDownload.DownloadService != sender) continue;
                    if (e.Cancelled || e.Error != null) ErrorList.Add(CurrentDownloadingObj[i]);

                    CurrentDownloadingObj.Remove(CurrentDownloadingObj[i]);
                }
            }

            if (CurrentDownloadingObj.Count < MaxParallelMPartDownloadCount)
            {
                if (WaitingList.Count >= 1)
                    StartDownload(WaitingList[0]);
                else if (AutoRestartFailedDownload)
                    if (ErrorList.Count >= 1)
                        StartDownload(ErrorList[0]);
            }
        }

        private void Async_WebClient_DownloadFileCompleted(object? sender, AsyncCompletedEventArgs e)
        {
            lock (CurrentDownloadingObj)
            {
                for (var i = 0; i < CurrentDownloadingObj.Count; i++)
                {
                    if (CurrentDownloadingObj[i].DownloadMode == DownloadMode.Async)
                    {
                        var asyncDownload = (AsyncDownload)CurrentDownloadingObj[i];

                        if (!asyncDownload.WebClient.Equals(sender)) continue;
                    }

                    if (!(e.Cancelled == false || e.Error == null)) ErrorList.Add(CurrentDownloadingObj[i]);
                    CurrentDownloadingObj.Remove(CurrentDownloadingObj[i]);
                }
            }

            if (CurrentDownloadingObj.Count < MaxParallelAsyncDownloadCount)
            {
                if (WaitingList.Count >= 1)
                    StartDownload(WaitingList[0]);
                else if (AutoRestartFailedDownload)
                    if (ErrorList.Count >= 1)
                        StartDownload(ErrorList[0]);
            }
        }

        #region Ctor

        public DownloadQueue()
        {
        }

        public DownloadQueue(int maxParallelAsyncDownloadCount)
        {
            MaxParallelAsyncDownloadCount = maxParallelAsyncDownloadCount;
        }

        #endregion
    }
}