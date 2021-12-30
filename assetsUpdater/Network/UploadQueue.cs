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
                        lock (WaitingUnits)
                        {
                            foreach (var uploadUnit in ErrorUnits) WaitingUnits.Add(uploadUnit);

                        }
                        goto Start;
                    }
                return;
            }
            else
            {
                do
                {   
                    List<Task> tasks = new List<Task>();
                    lock (CurrentUploadingUnits)
                    {
                        tasks.AddRange(CurrentUploadingUnits.Select((unit => unit.UploadTask)));

                    }

                    //! Must take a copy of tasks (taken by to list) to avoid modification from another thread
                    //System.InvalidOperationException: Collection was modified; enumeration operation may not execute.

                    // tasks = CurrentUploadingUnits.Select(uploadUnit => uploadUnit.Wait()).ToList();



                    await Task.WhenAll(tasks).ConfigureAwait(false);
                } while (WaitingUnits.Count >= 1 || CurrentUploadingUnits.Count>=1);

                goto Start;
            }
        }

        public async Task QueueUpload(IEnumerable<IUploadUnit> uploadUnits)
        {
            foreach (var uploadUnit in uploadUnits) await QueueUpload(uploadUnit).ConfigureAwait(false);
        }

        public async Task QueueUpload(IUploadUnit uploadUnit)
        {
            uploadUnit.UploadCompletedEventHandler += UploadCompletedEventHandler;

            
            AllUnits.Add(uploadUnit);
            if (CurrentUploadingUnits.Count < MaxParallelUploadCount)
            {
                CurrentUploadingUnits.Add(uploadUnit);

                await uploadUnit.Start().ConfigureAwait(false);
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
                    await StartDownload(uploadUnit).ConfigureAwait(false);
                else
                    WaitingUnits.Add(uploadUnit);
            }
        }

        private async Task StartDownload(IUploadUnit uploadUnit)
        {
            if (uploadUnit==null)
            {
                return;
            }
            WaitingUnits.Remove(uploadUnit);
            ErrorUnits.Remove(uploadUnit);

            await uploadUnit.Start().ConfigureAwait(false);

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

                    //Got objects
                    if (!e) ErrorUnits.Add(CurrentUploadingUnits[i]);

                    CurrentUploadingUnits.Remove(CurrentUploadingUnits[i]);
                }
            }
            //Queue

            if (CurrentUploadingUnits.Count < MaxParallelUploadCount)
            {
                if (WaitingUnits.Count >= 1)
                {
                    /*IUploadUnit unit=null;
                    lock (WaitingUnits)
                    {
                        unit= WaitingUnits.FirstOrDefault();
                    }
                    */

                    await StartDownload(WaitingUnits.FirstOrDefault()).ConfigureAwait(false);

                }
              
            }
        }

        ~UploadQueue()
        {
            foreach (var uploadUnit in AllUnits) uploadUnit.UploadCompletedEventHandler -= UploadCompletedEventHandler;
        }
    }
}