using System;
using System.IO.IsolatedStorage;

using assetsUpdater.Interfaces;

namespace assetsUpdater.Model
{
    public abstract class DownloadPackage
    {
        public long SizeTotal { get; private set; }
        public long SizeDownloaded { get; private set; }


        public int Progress { get; private set; }
        public long SpeedInSecByte { get; private set; }
        public Uri Uri { get; set; }
        public string LocalPath { get; set; }

        public DownloadConfiguration DownloadConfiguration { get; set; }
        public Downloadtype TypeOfDownloadPackage { get; set; }
       // public IAddressBuilder AddressBuilder { get; private set; }
        public DownloadPackage(Uri uri, string localPath, Downloadtype downloadtype, DownloadConfiguration downloadConfiguration = null)
        {

            Uri = uri;
            LocalPath = localPath;
            
            DownloadConfiguration = downloadConfiguration ?? new DownloadConfiguration()
            {
                IsOverwrite = true
            };
            TypeOfDownloadPackage = downloadtype;
        }

    }
    public enum Downloadtype
    {
        Async, MultiPart
    }


}
