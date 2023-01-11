using assertUpdaterRefactor.DbModel;

namespace assertUpdaterRefactor.StorageProvider
{
    public interface IStorageProvider : ICloneable
    {
<<<<<<< HEAD
        /// <summary>
        /// Reinitialize the storage provider with fresh data
        /// </summary>
     //   public DbData DbData { set; }
        public Task<DbData> Refresh();
        public Task<DbFile> Retrieve(string relativePath);

        public Task Flush();
        public Task Flush(DbData data);
        public Task Insert(DbFile dbFile);
=======

        public Task<DbData> RefreshAsync();
        public Task<DbFile> RetrieveAsync(string relativePath);

        public Task FlushAsync(string path = "");
        public Task InsertAsync(DbFile dbFile);
>>>>>>> 36095c5312f4bb80dd49ff8a7e22db8c42f24285


    }
}
