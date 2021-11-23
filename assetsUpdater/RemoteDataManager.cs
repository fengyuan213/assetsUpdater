#region Using

using System.Net;
using System.Threading.Tasks;
using assetsUpdater.Interfaces;
using assetsUpdater.StorageProvider;

#endregion

namespace assetsUpdater
{
    public class RemoteDataManager : DataManager
    {
        public RemoteDataManager(string dbPath) : base(dbPath)
        {
        }

        public RemoteDataManager(IStorageProvider storageProvider) : base(storageProvider)
        {
        }

        public override async Task<bool> IsDataValid()
        {
            var data = StorageProvider?.GetBuildInDbData();

            if (data == null) return await base.IsDataValid();
            if (data.Config.DownloadAddressBuilder == null) return false;

            foreach (var df in data.DatabaseFiles)
                if (string.IsNullOrWhiteSpace(df.DownloadAddress))
                    return false;

            return await base.IsDataValid();
        }

        public static async Task<IStorageProvider> GetStorageProvider(string dbUrl)
        {
            var fileDatabase = new FileDatabase();

            var p = new object[2] { dbUrl, CredentialCache.DefaultNetworkCredentials };
            await fileDatabase.Download(p);
            return fileDatabase;
        }
    }
}