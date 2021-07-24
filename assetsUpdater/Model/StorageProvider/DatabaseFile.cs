using System.IO;

namespace assetsUpdater.Model.StorageProvider
{
    public class DatabaseFile
    {
        public DatabaseFile(string relativePath, string hash, long fileSize, string downloadAddress)
        {
            RelativePath = relativePath;
            Hash = hash;
            FileSize = fileSize;
            DownloadAddress = downloadAddress;
        }

        public string RelativePath { get; set; }
        public string Hash { get; set; }
        public long FileSize { get; set; }

        public string FileName
        {
            get
            {
                return Path.GetFileName(RelativePath);
            }
        }
        public string DownloadAddress { get; set; }
    }
}