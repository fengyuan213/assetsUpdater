using System;
using System.Threading.Tasks;
using assetsUpdater.Interfaces;
using assetsUpdater.Model.StorageProvider;

namespace assetsUpdater
{
    public class DatabaseBuilder
    {
        private IStorageProvider storageProvider = null;
        public DatabaseBuilder(IStorageProvider storageProvider)
        {
            this.storageProvider = storageProvider;
        }

        public async Task BuildDatabase(DbConfig config,string exportPath)
        {
            await storageProvider.Create(config).ConfigureAwait(true);
            await storageProvider.Export(exportPath);
        
        }

        public async Task BuildDatabaseWithUrl(DbConfig config, string exportPath)
        {
            await storageProvider.Create(config).ConfigureAwait(true);
            var data = storageProvider.GetBuildInDbData();
            foreach (var dataDatabaseFile in data.DatabaseFiles)
            {
                dataDatabaseFile.DownloadAddress = config.DownloadAddressBuilder.BuildUri(dataDatabaseFile.RelativePath).ToString();

            }

            await storageProvider.Export(exportPath);
        }
    }
}
