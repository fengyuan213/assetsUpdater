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
        public IEnumerable<BuildInDbFile> AddFile { get; set; } = new List<BuildInDbFile>();
        public IEnumerable<BuildInDbFile> DifferFile { get; set; } = new List<BuildInDbFile>();
        public IEnumerable<BuildInDbFile> DeleteFile { get; set; } = new List<BuildInDbFile>();
    }
}