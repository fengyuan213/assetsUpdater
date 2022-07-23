using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using assetsUpdater.Network;

namespace assetsUpdater.Interfaces
{
    public class WorkerQueue
    {
        public virtual List<WorkerUnit> ProcessingUnits { get; private set; } = new List<WorkerUnit>();
        public virtual List<WorkerUnit> AllWorkerUnits { get; private set; } = new List<WorkerUnit>();
        public virtual List<WorkerUnit> WaitingUnits { get; private set; } = new List<WorkerUnit>();
        public virtual List<WorkerUnit> ErrorUnits { get; private set; } = new List<WorkerUnit>();
        public  int ConcurrentAmount = 5;
        public  bool ConfigureAwait= false;
        public bool RestartOnFail = false;
        public IProgress<double> Progress { get; private set; }=new Progress<double>();

      
        public virtual async Task WaitAll()
        {
            try
            {
                while (WaitingUnits.Count >= 1)
                {
                    var tasks = ProcessingUnits.Select(downloadUnit => downloadUnit.Wait()).ToArray();
                    await Task.WhenAll(tasks).ConfigureAwait(ConfigureAwait);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

         


       
        }

       
        public void QueueWork(WorkerUnit workerUnit)
        {
            workerUnit.OnWorkCompleted += OnWorkCompleted;
            AllWorkerUnits.Add(workerUnit);
            if (ProcessingUnits.Count()<ConcurrentAmount)
            {
               ProcessingUnits.Add(workerUnit);
               workerUnit.Start();
            }
            else
            {
              WaitingUnits.Add(workerUnit);
            }
        }

    
        public async Task RestartFailedWork(WorkerUnit workerUnit)
        {
           

            if (ErrorUnits.Contains(workerUnit))
            {
                if (ProcessingUnits.Count < ConcurrentAmount)
                    await TryStartWork(workerUnit).ConfigureAwait(ConfigureAwait);
                else
                    WaitingUnits.Add(workerUnit);
            }
            
        }

        protected  Task TryStartWork(WorkerUnit workerUnit)
        {
            lock (WaitingUnits)
            {
                WaitingUnits.Remove(workerUnit);

            }

            lock (ErrorUnits)
            {
                ErrorUnits.Remove(workerUnit);

            }
            workerUnit.Start();
            lock (ProcessingUnits)
            {
                ProcessingUnits.Add(workerUnit);
            }
            return Task.CompletedTask;
        
        }
        protected  virtual async  void OnWorkCompleted(object? sender, WorkerCompletedEventArgs e)
        {
            for (int i = 0; i < ProcessingUnits.Count; i++)
            {
                var workerUnit = ProcessingUnits[i];
                if (workerUnit != sender) continue;

                if (e.Cancelled || e.Error != null)
                {
                    ErrorUnits.Add(workerUnit);
                }

                lock (ProcessingUnits)
                {
                    ProcessingUnits.Remove(workerUnit);

                }
            }

            if (ProcessingUnits.Count < ConcurrentAmount)
            {

                if (WaitingUnits.Count >= 1)
                    await TryStartWork(WaitingUnits.FirstOrDefault()).ConfigureAwait(ConfigureAwait);
                else if (RestartOnFail)
                {

                    if (ErrorUnits.Count >= 1)
                    { await TryStartWork(ErrorUnits.FirstOrDefault()).ConfigureAwait(ConfigureAwait); }
                }
            }
        }
      


        ~WorkerQueue()
        {
            foreach (var workerUnit in AllWorkerUnits)
            {
                workerUnit.OnWorkCompleted-=OnWorkCompleted;

            }
        }

        WorkerQueue()
        {
           
        }


	}
}
