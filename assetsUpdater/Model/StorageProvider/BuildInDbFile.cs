using assetsUpdater.Interfaces;

namespace assetsUpdater.Model
{
    public class BuildInDbFile
    {
        public BuildInDbFile(string relativePath, string hash, long fileSize, string fileName, string downloadAddress)
        {
            RelativePath = relativePath;
            Hash = hash;
            FileSize = fileSize;
            FileName = fileName;
            DownloadAddress = downloadAddress;
        }

        public string RelativePath { get; set; }
        public string Hash { get; set; }
        public long FileSize { get; set; }
        public string FileName { get; set; }
        public string DownloadAddress { get; set; }
    }
}