using System;
using System.IO;
using System.Threading.Tasks;
using assetsUpdater.Interfaces;
using assetsUpdater.StorageProvider;

namespace assetsUpdater
{
    public class LocalDataManager
    {
        private static LocalDataManager localDataManager = null;
        public static LocalDataManager GetInstance
        {
            get
            {
                return LocalDataManager.localDataManager?? throw new  NullReferenceException(nameof(localDataManager)+":can't be null");
            }
        }

        public IStorageProvider StorageProvider { get; set; }
        public void SetLocalDataManager(string dbPath)
        {
            localDataManager = new LocalDataManager(dbPath);
        }

        public LocalDataManager(string dbPath)
        {
            DatabasePath = dbPath;
            if (!string.IsNullOrWhiteSpace(dbPath))
            {
                StorageProvider = new FileDatabase(dbPath);
            }
           
            
        }
        
        public string DatabasePath = null;
        
        public Task<bool> IsLocalDataValid()
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
