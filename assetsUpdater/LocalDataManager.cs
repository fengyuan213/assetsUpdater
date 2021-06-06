using System;
using System.Threading.Tasks;

namespace assetsUpdater
{
    public class LocalDataManager
    {

        public static LocalDataManager GetLocalDataManager
        {
            get
            {
                return LocalDataManager.localDataManager ?? new LocalDataManager();
            }
        }
        private static LocalDataManager localDataManager = null;
        public Task<bool> IsLocalDataValid()
        {
            return Task.FromResult<bool>(false);
        }
    }
}
