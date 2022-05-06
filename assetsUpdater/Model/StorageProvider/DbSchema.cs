#region Using

using System.Collections.Generic;

#endregion

namespace assetsUpdater.Model.StorageProvider
{
    public class DbSchema
    {
        public IEnumerable<string> DirList { get; set; } = new List<string>();
        public IEnumerable<string> FileList { get; set; } = new List<string>();
    }
}