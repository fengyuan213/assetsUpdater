using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace assetsUpdater.Interfaces
{
    public class WorkerUnit
    {
        public virtual event EventHandler<WorkerCompletedEventArgs> OnWorkCompleted;

        public void Start()
        {

            OnWorkCompleted?.Invoke(this,null);

            
        }

        public Task Wait()
        {
            return Task.CompletedTask;
        }
    }
}
