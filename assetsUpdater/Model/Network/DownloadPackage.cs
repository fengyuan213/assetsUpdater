#region Using

using System;
using System.IO;
using System.Threading.Tasks;
using assetsUpdater.Interfaces;

#endregion

namespace assetsUpdater.Model.Network
{
    public class DownloadPackage
    {
        // public IAddressBuilder AddressBuilder { get; private set; }
        public DownloadPackage(Uri uri, string localPath, long fileSize, string exceptedHash, DownloadMode downloadMode)
        {
            ExceptedHash = exceptedHash;
            SizeTotal = fileSize;
            Uri = uri;
            LocalPath = localPath;

            DownloadMode = downloadMode;
        }

        public long SizeTotal { get; }
        public Uri Uri { get; set; }
        public string LocalPath { get; set; }
        public string ExceptedHash { get; set; }
        public DownloadMode DownloadMode { get; set; }

        public Task CleanDownloadCache()
        {
            var tmpFile = LocalPath + ".tmp";
            if (File.Exists(tmpFile))
            {
                File.Copy(tmpFile, LocalPath, true);
                File.Delete(tmpFile);
            }

            return Task.CompletedTask;
        }
    }
}