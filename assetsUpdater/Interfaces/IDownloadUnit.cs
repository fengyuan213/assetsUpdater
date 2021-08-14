using System.Threading.Tasks;
using assetsUpdater.Model;
using assetsUpdater.Model.Network;
using assetsUpdater.Network;

namespace assetsUpdater.Interfaces
{
    public enum DownloadMode
    {
        Async,MultiPart
    }
    public interface IDownloadUnit
    {
        public DownloadMode DownloadMode { get; set; }
        public double BytesReceivedPerSec
        {
            get; set;
        }
        public double Progress { get; }

        public long BytesReceived { get; }
     
        public long BytesToReceive {get; }
        public Task Wait();

        public Task Start();


        public DownloadSetting DownloadSetting { get; set; }

        public DownloadPackage DownloadPackage { get; set; }
    }
}