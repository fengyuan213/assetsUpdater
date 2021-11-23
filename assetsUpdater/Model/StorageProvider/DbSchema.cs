using System.Collections.Generic;

namespace assetsUpdater.Model.StorageProvider
{
    public class DbSchema
    {
        public IEnumerable<string> DirList { get; set; } = new List<string>();
    }
}
