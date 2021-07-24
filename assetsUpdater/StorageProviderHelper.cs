using System;
using System.Threading.Tasks;
using assetsUpdater.Interfaces;
using assetsUpdater.Model.StorageProvider;

namespace assetsUpdater
{
    public class StorageProviderHelper
    {
        private IStorageProvider storageProvider = null;
        public StorageProviderHelper(IStorageProvider storageProvider)
        {
            this.storageProvider = storageProvider;
        }

        public async Task BuildRemoteDatabase(IAddressBuilder downloadAddressBuilder,DbConfig config,string exportPath)
        {
            await storageProvider.Create(config).ConfigureAwait(true);
            var data = storageProvider.GetBuildInDbData();
            foreach (var dataDatabaseFile in data.DatabaseFiles)
            {
                dataDatabaseFile.DownloadAddress= downloadAddressBuilder.BuildUri(dataDatabaseFile.RelativePath).ToString();
                Console.WriteLine(dataDatabaseFile);
            }
            
            await storageProvider.Export(exportPath);
        }
    }
}
