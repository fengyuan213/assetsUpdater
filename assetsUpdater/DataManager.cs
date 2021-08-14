using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assetsUpdater.Interfaces;
using assetsUpdater.StorageProvider;

namespace assetsUpdater
{
    public abstract class DataManager
    {
        
        public IStorageProvider StorageProvider { get; set; }

        protected DataManager(string dbPath)
        {
            DatabasePath = dbPath;
            if (!string.IsNullOrWhiteSpace(dbPath))
            {
                StorageProvider = new FileDatabase(dbPath);
            }
        }

        public string DatabasePath = null;
            
        public virtual Task<bool> IsDataValid()
        {
            return Task.FromResult(false);
        }
    }
}
