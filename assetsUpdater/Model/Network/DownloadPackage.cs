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
        public IAddressBuilder AddressBuilder { get; private set; }
        public DownloadPackage(IAddressBuilder addressBuilder, Downloadtype downloadtype, DownloadConfiguration downloadConfiguration = null)
        {
            AddressBuilder = addressBuilder;
            Uri = AddressBuilder.BuildUri();
            LocalPath = AddressBuilder.BuildLocalPath();
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
