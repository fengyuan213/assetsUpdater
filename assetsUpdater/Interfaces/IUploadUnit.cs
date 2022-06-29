#region Using

using System;
using System.Threading.Tasks;
using assetsUpdater.Model.Network;

#endregion

namespace assetsUpdater.Interfaces
{
    public interface IUploadUnit
    {



        /// <summary>
        ///     Task represent current uploading task of upload unit
        /// </summary>
        public Task UploadTask { get; }

        public long BytesSent { get; }
        public long TotalBytes { get; }
        public double Progress { get; }
        public UploadPackage UploadPackage { get; }

        public Task Start();

        public Task Wait();

        public event EventHandler<bool> UploadCompletedEventHandler;
    }
}