#region Using

using System;
using System.IO;
using System.Threading.Tasks;
using assetsUpdater.Interfaces;
using assetsUpdater.StorageProvider;

#endregion

namespace assetsUpdater
{
    public class LocalDataManager : DataManager
    {
        public LocalDataManager(IStorageProvider storageProvider) : base(storageProvider)
        {
            MessageQueue
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
            if (!await base.IsDataValid().ConfigureAwait(false)) return false;

            if (!string.IsNullOrWhiteSpace(DatabasePath))
            {
                if (!File.Exists(DatabasePath)) return false;
                try
                {
                    if (string.IsNullOrWhiteSpace(StorageProvider.GetBuildInDbData().Config.VersionControlFolder)) return false;
                    if (StorageProvider is FileDatabase db)
                    {
                        return db.IsValidDb();
                    }
                    else
                    {
                        return true;
                    }
                   
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