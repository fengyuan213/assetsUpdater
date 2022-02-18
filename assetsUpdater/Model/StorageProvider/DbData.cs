using Newtonsoft.Json;

using System.Collections.Generic;

namespace assetsUpdater.Model.StorageProvider
{
    public class DbData
    {
        [JsonConstructor]
        public DbData(DbConfig config)
        {
            DatabaseFiles = new List<DatabaseFile>();
            Config = config;
        }

        public IEnumerable<DatabaseFile> DatabaseFiles { get; set; }
        public DbConfig Config { get; set; }
    }
}