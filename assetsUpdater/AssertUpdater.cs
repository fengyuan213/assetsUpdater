using System;
using System.Threading.Tasks;

using assetsUpdater.Model;

namespace assetsUpdater
{
    public class AssertUpdater
    {
        public AssertUpdater()
        {
        }
        public static async Task<RequestResult> RequestServer()
        {
            await RequestInternal();
            await GatherLocalData();
            return null;
        }
        public static async Task<AssertUpgradePostProcess> UpgradeAsync(AssertUpgradePackage assertUpgradePackage)
        {
            return null;
        }
        private static Task RequestInternal()
        {
            return null;
        }
        private static Task GatherLocalData()
        {
            return null;
        }
    }
}
