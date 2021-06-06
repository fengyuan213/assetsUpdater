using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace assetsUpdater.Model
{
    public class OneAssertUpgrade
    {
        public IEnumerable<Patch> AddFile { get; set; }
        public IEnumerable<Patch> DifferFile { get; set; }
        public IEnumerable<Patch> DeleteFile { get; set; }
    }
}