#region Using

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using assetsUpdater.Interfaces;

#endregion

namespace assetsUpdater.Network
{
    public class UploadQueue
    {
        public UploadQueue(int maxParallelUploadCount, bool isAutoRestartFailedDownload = false)
        {
            MaxParallelUploadCount = maxParallelUploadCount == 0 ? 5 : maxParallelUploadCount;
            AutoRestartFailedDownload = isAutoRestartFailedDownload;
        }

        public int MaxParallelUploadCount { get; set; }
        public bool AutoRestartFailedDownload { get; set; }
        public ObservableCollection<IUploadUnit> AllUnits { get; } = new();
        public ObservableCollection<IUploadUnit> CurrentUploadingUnits { get; } = new();
        public ObservableCollection<IUploadUnit> WaitingUnits { get; } = new();
        public ObservableCollection<IUploadUnit> ErrorUnits { get; } = new();

        public double Progress
        {
            get
            {
                var progressTotal = AllUnits.Sum(uploadUnit => uploadUnit.Progress);

                return progressTotal / AllUnits.Count / 1d;
            }
        }

        public long BytesSent
        {
            get { return AllUnits.Sum(unit => unit.BytesSent); }
        }

        public long TotalBytesToUpload
        {
            get { return AllUnits.Sum(uploadUnit => uploadUnit.TotalBytes); }
        }


        public async Task WaitAll()
        {
            Start:
            if (CurrentUploadingUnits.Count < 1 && WaitingUnits.Count < 1)
            {
                //Upload Finished
                if (ErrorUnits.Count >= 1)
                    //Error occured
                    if (AutoRestartFailedDownload)
                    {
                        foreach (var uploadUnit in ErrorUnits) WaitingUnits.Add(uploadUnit);
                        goto Start;
                    }
            }
            else
            {
                do
                {
                    var tasks = CurrentUploadingUnits.Select(uploadUnit => uploadUnit.Wait()).ToArray();
                    await Task.WhenAll(tasks).ConfigureAwait(true);
                } while (WaitingUnits.Count >= 1);

                goto Start;
            }
        }

        public async Task QueueUpload(IEnumerable<IUploadUnit> uploadUnits)
        {
            foreach (var uploadUnit in uploadUnits) await QueueUpload(uploadUnit);
        }

        public async Task QueueUpload(IUploadUnit uploadUnit)
        {
            uploadUnit.UploadCompletedEventHandler += UploadCompletedEventHandler;


            AllUnits.Add(uploadUnit);
            if (CurrentUploadingUnits.Count < MaxParallelUploadCount)
            {
                CurrentUploadingUnits.Add(uploadUnit);

                await uploadUnit.Start().ConfigureAwait(true);
            }
            else
            {
                WaitingUnits.Add(uploadUnit);
            }
        }

        public async Task RestartFailedDownload(IUploadUnit uploadUnit)
        {
            if (ErrorUnits.Contains(uploadUnit))
            {
                if (CurrentUploadingUnits.Count < MaxParallelUploadCount)
                    await StartDownload(uploadUnit).ConfigureAwait(true);
                else
                    WaitingUnits.Add(uploadUnit);
            }
        }

        private async Task StartDownload(IUploadUnit uploadUnit)
        {
            WaitingUnits.Remove(uploadUnit);
            ErrorUnits.Remove(uploadUnit);

            await uploadUnit.Start().ConfigureAwait(true);

            CurrentUploadingUnits.Add(uploadUnit);
        }


        private async void UploadCompletedEventHandler(object sender, bool e)
        {
            lock (CurrentUploadingUnits)
            {
                for (var i = 0; i < CurrentUploadingUnits.Count; i++)
                {
                    //Identify Objects
                    if (!CurrentUploadingUnits[i].Equals(sender)) continue;


                    if (!e) ErrorUnits.Add(CurrentUploadingUnits[i]);

                    CurrentUploadingUnits.Remove(CurrentUploadingUnits[i]);
                }
            }

            if (CurrentUploadingUnits.Count < MaxParallelUploadCount)
            {
                if (WaitingUnits.Count >= 1)
                    await StartDownload(WaitingUnits[0]).ConfigureAwait(true);
                else if (AutoRestartFailedDownload)
                    if (ErrorUnits.Count >= 1)
                        await StartDownload(ErrorUnits[0]).ConfigureAwait(true);
            }
        }

        ~UploadQueue()
        {
            foreach (var uploadUnit in AllUnits) uploadUnit.UploadCompletedEventHandler -= UploadCompletedEventHandler;
        }
    }
}