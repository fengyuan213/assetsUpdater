using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assetsUpdater.Interfaces
{
    public class WorkerCompletedEventArgs :System.EventArgs
    {
        public bool Cancelled=false;
        public Exception? Error=null;

    }
}
