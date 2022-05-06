#region Using

using System.Collections.Generic;
using assetsUpdater.Model.StorageProvider;

#endregion

namespace assetsUpdater.Model
{
    /// <summary>
    ///     One hole AssertUpgradePackage
    /// </summary>
    public class AssertUpgradePackage
    {
        public IEnumerable<DatabaseFile> AddFile { get; set; } = new List<DatabaseFile>();
        public IEnumerable<DatabaseFile> DifferFile { get; set; } = new List<DatabaseFile>();
        public IEnumerable<DatabaseFile> DeleteFile { get; set; } = new List<DatabaseFile>();
    }
}