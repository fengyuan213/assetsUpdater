using System;
using System.IO;
using System.Threading.Tasks;
using assetsUpdater.Interfaces;
using assetsUpdater.StorageProvider;

namespace assetsUpdater
{
    public class LocalDataManager :DataManager
    {
    
        public LocalDataManager(string dbPath) : base(dbPath)
        {
            /*DatabasePath = dbPath;
            if (!string.IsNullOrWhiteSpace(dbPath))
            {
                StorageProvider = new FileDatabase(dbPath);
            }*/


        }
        

        public override Task<bool> IsDataValid()
        {
            if (string.IsNullOrWhiteSpace(DatabasePath))
            {
                return Task.FromResult(false);

            }
            if (!File.Exists(DatabasePath))
            {
                return Task.FromResult(false);
            }

            try
            {

                FileDatabase database = new FileDatabase(DatabasePath);
                return Task.FromResult(database.IsValidDb());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult(false);
            }
        }
    }
}
