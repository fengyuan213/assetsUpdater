using assertUpdaterRefactor.DbModel;
using assertUpdaterRefactor.Operations;

namespace assertUpdaterRefactor
{
    public class AssertUpgradePackage
    {
        public IEnumerable<IOperation> Operations { get; set; } = new List<IOperation>();
        public IEnumerable<DbFile> AddFile { get; set; } = new List<DbFile>();
        public IEnumerable<DbFile> DifferFile { get; set; } = new List<DbFile>();
        public IEnumerable<DbFile> DeleteFile { get; set; } = new List<DbFile>();
    }
}
