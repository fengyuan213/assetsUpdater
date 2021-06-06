using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace assetsUpdater.Model
{
    /// <summary>
    ///  One hole AssertUpgradePackage
    /// </summary>
    public class AssertUpgradePackage
    {
        public IEnumerable<BuildInDbFile> AddFile { get; set; }
        public IEnumerable<BuildInDbFile> DifferFile { get; set; }
        public IEnumerable<BuildInDbFile> DeleteFile { get; set; }
    }
}