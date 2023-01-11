using assertUpdaterRefactor.DbModel;

namespace assertUpdaterRefactor.StorageProvider
{
    public interface IStorageProvider : ICloneable
    {
        /// <summary>
        /// Reinitialize the storage provider with fresh data
        /// </summary>
     //   public DbData DbData { set; }
        public Task<DbData> Refresh();
        public Task<DbFile> Retrieve(string relativePath);

        public Task Flush();
        public Task Flush(DbData data);
        public Task Insert(DbFile dbFile);


    }
}
