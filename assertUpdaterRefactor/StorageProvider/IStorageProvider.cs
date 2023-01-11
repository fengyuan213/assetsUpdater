using assertUpdaterRefactor.DbModel;

namespace assertUpdaterRefactor.StorageProvider
{
    public interface IStorageProvider : ICloneable
    {

        public Task<DbData> Refresh();
        public Task<DbFile> Retrieve(string relativePath);

        public Task Flush(string path = "");
        public Task Insert(DbFile dbFile);


    }
}
