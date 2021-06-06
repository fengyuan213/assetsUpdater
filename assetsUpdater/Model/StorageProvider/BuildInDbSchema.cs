using System;
using System.Collections.Generic;

namespace assetsUpdater.Model.StorageProvider
{
    public class BuildInDbSchema
    {
        public IEnumerable<string> DirList { get; set; } = new List<string>();
    }
}
