namespace assertUpdater.DbModel
{
    [Serializable]
    public class DbFile : IEquatable<DbFile>
    {
        /// <summary>
        /// Inherited Default Constructor
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected DbFile()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
        protected void Init(string relativePath, string hash, long fileSize, string downloadAddress = "")
        {
            RelativePath = relativePath;
            Hash = hash;
            FileSize = fileSize;
            DownloadAddress = downloadAddress;
        }
        public DbFile(string relativePath, string hash, long fileSize, string downloadAddress = "")
        {
            RelativePath = relativePath;
            Hash = hash;
            FileSize = fileSize;
            DownloadAddress = downloadAddress;
        }


        public string RelativePath { get; private set; }
        public string Hash { get; set; }
        public long FileSize { get; set; }

        public string FileName => Path.GetFileName(RelativePath);

        public string DownloadAddress { get; set; }

        public bool Equals(DbFile? other)
        {
            return other is not null
&& (ReferenceEquals(this, other) || (RelativePath == other.RelativePath && Hash == other.Hash && FileSize == other.FileSize));
        }

        public override bool Equals(object? obj)
        {
            return obj is not null && (ReferenceEquals(this, obj) || (obj.GetType() == GetType() && Equals((DbFile)obj)));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RelativePath);
        }
    }
}
