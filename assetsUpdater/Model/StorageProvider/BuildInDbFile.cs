using System.IO;
using assetsUpdater.Interfaces;

namespace assetsUpdater.Model
{
    public class BuildInDbFile
    {
        public BuildInDbFile(string relativePath, string hash, long fileSize, string downloadAddress)
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