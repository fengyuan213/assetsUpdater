#region Using

using System.Threading.Tasks;
using assetsUpdater.Model.Network;
using assetsUpdater.Network;

#endregion

namespace assetsUpdater.Interfaces
{
    public enum DownloadMode
    {
        Async,
        MultiPart
    }

    public interface IDownloadUnit
    {
        public DownloadMode DownloadMode { get; set; }

        public double BytesReceivedPerSec { get; set; }

        public double Progress { get; }

        public long BytesReceived { get; }

        public long BytesToReceive { get; }

        public DownloadSetting DownloadSetting { get; set; }

        public DownloadPackage DownloadPackage { get; set; }

        public Task Wait();

        public Task Start();
    }
}