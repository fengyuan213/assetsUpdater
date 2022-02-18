using assetsUpdater.Interfaces;
using assetsUpdater.StorageProvider;

using System;
using System.IO;
using System.Threading.Tasks;

namespace assetsUpdater
{
    public class LocalDataManager : DataManager
    {
        public LocalDataManager(IStorageProvider storageProvider) : base(storageProvider)
        {
        }

        public LocalDataManager(string dbPath, bool isAsync = false) : base(dbPath, isAsync)
        {
            /*DatabasePath = dbPath;
            if (!string.IsNullOrWhiteSpace(dbPath))
            {
                StorageProvider = new FileDatabase(dbPath);
            }*/
        }

        public LocalDataManager(Stream stream) : base(stream)
        {
        }

        public override async Task<bool> IsDataValid()
        {
            if (!await base.IsDataValid().ConfigureAwait(false))

            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(DatabasePath))
            {
                if (!File.Exists(DatabasePath)) return false;
                try
                {
                    FileDatabase database = new FileDatabase(StorageProvider);
                    if (string.IsNullOrWhiteSpace(database.Data.Config.VersionControlFolder))
                    {
                        return false;
                    }
                    return database.IsValidDb();
                }
                catch (Exception e)
                {
                    AssertVerify.OnMessageNotify(this, "Error on Validating database", e);

                    return false;
                }
            }

            return true;
        }
    }
}