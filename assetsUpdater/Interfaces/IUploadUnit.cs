using assetsUpdater.Model.Network;

using System;
using System.Threading.Tasks;

namespace assetsUpdater.Interfaces
{
    public interface IUploadUnit
    {
        public long BytesSent { get; }
        public long TotalBytes { get; }
        public double Progress { get; }
        public UploadPackage UploadPackage { get; }
        public Task Start();
        public Task Wait();
        public event EventHandler<bool> UploadCompletedEventHandler;


    }
}
