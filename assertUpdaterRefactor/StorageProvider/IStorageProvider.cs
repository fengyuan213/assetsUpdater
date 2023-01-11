using assertUpdaterRefactor.DbModel;

namespace assertUpdaterRefactor.StorageProvider
{
    public interface IStorageProvider : ICloneable
    {

        public Task<DbData> RefreshAsync();
        public Task<DbFile> RetrieveAsync(string relativePath);

        public Task FlushAsync(string path = "");
        public Task InsertAsync(DbFile dbFile);


    }
}
