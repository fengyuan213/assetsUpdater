using assertUpdater.DbModel;

namespace assertUpdater.StorageProvider
{
    public interface IStorageProvider : ICloneable
    {

        /// <summary>
        /// Reinitialize the storage provider with fresh data
        /// </summary>
     //   public DbData DbData { set; }
        public Task<DbData> RefreshAsync();
        public Task<DbFile> RetrieveAsync(string relativePath);

        public Task FlushAsync();
        public Task FlushAsync(DbData data);
        public Task InsertAsync(DbFile dbFile);
        
        



    }
}
