using assetsUpdater.Model.StorageProvider;

using System.Collections.Generic;

namespace assetsUpdater.Model
{
    /// <summary>
    ///  One hole AssertUpgradePackage
    /// </summary>
    public class AssertUpgradePackage
    {
        public IEnumerable<DatabaseFile> AddFile { get; set; } = new List<DatabaseFile>();
        public IEnumerable<DatabaseFile> DifferFile { get; set; } = new List<DatabaseFile>();
        public IEnumerable<DatabaseFile> DeleteFile { get; set; } = new List<DatabaseFile>();
    }
}