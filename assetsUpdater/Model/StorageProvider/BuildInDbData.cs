using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace assetsUpdater.Model.StorageProvider
{

    public class BuildInDbData
    {
        [JsonConstructor]
        public BuildInDbData(BuildInDbConfig config)
        {
            DatabaseFiles = new List<BuildInDbFile>();
            Config = config;

        }
        public IEnumerable<BuildInDbFile> DatabaseFiles { get; set; }
        public BuildInDbConfig Config { get; set; }

    }
}
