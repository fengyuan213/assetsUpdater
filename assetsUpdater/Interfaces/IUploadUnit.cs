using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assetsUpdater.Model.Network;

namespace assetsUpdater.Interfaces
{
    public interface IUploadUnit
    {
        public long BytesSent { get; }  
        public long TotalBytes { get; }
        public double Progress { get;  }
        public UploadPackage UploadPackage { get; }
        public Task Start();
        public Task Wait();
        public event EventHandler<bool> UploadCompletedEventHandler;


    }
}
