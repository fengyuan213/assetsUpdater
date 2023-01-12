using assertUpdater.DbModel;
using assertUpdater.Operations;
    
namespace assertUpdater
{
    public class AssertUpgradePackage
    {
        public IEnumerable<IOperation> Operations { get; set; } = new List<IOperation>();
        public IEnumerable<DbFile> AddFile { get; set; } = new List<DbFile>();
        public IEnumerable<DbFile> DifferFile { get; set; } = new List<DbFile>();
        public IEnumerable<DbFile> DeleteFile { get; set; } = new List<DbFile>();
    }
}
