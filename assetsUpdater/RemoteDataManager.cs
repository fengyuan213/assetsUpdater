#region Using

using System.IO;
using System.Net;
using System.Threading.Tasks;
using assetsUpdater.EventArgs;
using assetsUpdater.Interfaces;
using assetsUpdater.StorageProvider;

#endregion

namespace assetsUpdater
{
    public class RemoteDataManager : DataManager
    {
        public RemoteDataManager(string dbPath,bool isAsync=false) : base(dbPath,isAsync)
        {
        }

        public RemoteDataManager(IStorageProvider storageProvider) : base(storageProvider)
        {
        }

        public RemoteDataManager(Stream stream):base(stream)
        {
            
        }
        public override async Task<bool> IsDataValid()
        {
            var data = StorageProvider?.GetBuildInDbData();

            if (data == null) return await base.IsDataValid();
            if (data.Config.DownloadAddressBuilder == null) return false;
            if (string.IsNullOrWhiteSpace(data.Config.UpdateUrl))
            {
                AssertVerify.OnMessageNotify(this, "Empty update url", MsgL.Debug);

                return false;
            }

            if (string.IsNullOrWhiteSpace(data.Config.DownloadAddressBuilder.RootDownloadAddress))
            {
                AssertVerify.OnMessageNotify(this,"Invalid RDB AddressBuilder-> RootDownloadAddress null",MsgL.Debug);
                return false;
            }
            ;
            
            /*foreach (var df in data.DatabaseFiles)
                if (string.IsNullOrWhiteSpace(df.DownloadAddress))
                    return false;*/

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