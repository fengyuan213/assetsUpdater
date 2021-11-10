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
        
        public IStorageProvider StorageProvider { get; private set; }

        protected DataManager(IStorageProvider storageProvider)
        {
            StorageProvider = storageProvider;

        }
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
            var data = StorageProvider?.GetBuildInDbData();

            if (data != null)
            {
                if (data.DatabaseFiles?.Count() <= 0 ||
                    data.DatabaseFiles == null ||
                    data.Config.MirrorVersion == 0 ||
                    data.Config.MajorVersion == 0 ||
                    string.IsNullOrWhiteSpace(data.Config.VersionControlFolder) ||
                    data.Config.DatabaseSchema == null 
                    
                )
                {
                    return Task.FromResult(false);
                }
                foreach (var df in data.DatabaseFiles)
                {
                    if (
                        string.IsNullOrWhiteSpace(df.FileName) ||
                        string.IsNullOrWhiteSpace(df.Hash) ||
                        string.IsNullOrWhiteSpace(df.RelativePath))
                    {
                        return Task.FromResult(false);
                    }
                }
            }

            return Task.FromResult(true);
        }
    }
}
