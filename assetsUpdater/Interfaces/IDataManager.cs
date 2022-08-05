using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assetsUpdater.Interfaces
{
    public interface IDataManager
    {
        public IDataProvider Provider { get; }
      //  public IDbData Data { get; }

        void Update(IDbData Data);
    }
}
