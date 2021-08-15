using System;
using System.IO;
using System.Threading.Tasks;
using assetsUpdater.Interfaces;

namespace assetsUpdater.Model.Network
{
    public class DownloadPackage
    {
        public long SizeTotal { get; private set; }
        public Uri Uri { get; set; }
        public string LocalPath { get; set; }
        public string ExceptedHash { get; set; }
        public DownloadMode DownloadMode { get; set; }
       // public IAddressBuilder AddressBuilder { get; private set; }
        public DownloadPackage(Uri uri, string localPath,long fileSize,string exceptedHash, DownloadMode downloadMode)
        {

            ExceptedHash = exceptedHash;
            SizeTotal = fileSize;
            Uri = uri;
            LocalPath = localPath;
            
            DownloadMode = downloadMode;
        }
        public  Task CleanDownloadCache()
        {
            var tmpFile =LocalPath + ".tmp";
            if (System.IO.File.Exists(tmpFile))
            {
                File.Copy(tmpFile,LocalPath,true);
                System.IO.File.Delete(tmpFile);
            }
            return Task.CompletedTask;
        }

    }


}
