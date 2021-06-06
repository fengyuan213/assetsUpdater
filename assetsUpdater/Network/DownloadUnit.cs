using System.Reflection.Metadata;

using assetsUpdater.Interfaces;
using assetsUpdater.Model;

namespace assetsUpdater
{
    public abstract class DownloadUnit
    {
        public IDownloadPackage DownloadPackage { get; set; }


    }
}