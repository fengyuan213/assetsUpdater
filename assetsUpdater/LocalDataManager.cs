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
        public LocalDataManager(string dbPath) : base(dbPath)
        {
            /*DatabasePath = dbPath;
            if (!string.IsNullOrWhiteSpace(dbPath))
            {
                StorageProvider = new FileDatabase(dbPath);
            }*/


        }


        public override async Task<bool> IsDataValid()
        {

            if (!string.IsNullOrWhiteSpace(DatabasePath))
            {
                if (!File.Exists(DatabasePath)) return false;
                try
                {
                    FileDatabase database = new FileDatabase(DatabasePath);
                    return database.IsValidDb();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
            return await base.IsDataValid(); ;

        }
    }
}
