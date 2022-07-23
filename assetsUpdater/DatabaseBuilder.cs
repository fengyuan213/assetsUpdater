#region Using

using System.Threading.Tasks;
using assetsUpdater.Interfaces;
using assetsUpdater.Model.StorageProvider;

#endregion

namespace assetsUpdater
{
    public class DatabaseBuilder
    {
        private readonly IStorageProvider storageProvider;

        public DatabaseBuilder(IStorageProvider storageProvider)
        {
            this.storageProvider = storageProvider;
        }

        public async Task BuildDatabase(DbConfig config, string exportPath)
        {
            await storageProvider.Create(config).ConfigureAwait(false);

            await storageProvider.Export(exportPath);
        }

        public async Task BuildDatabaseWithUrl(DbConfig config, string exportPath)
        {
            await storageProvider.Create(config).ConfigureAwait(false);
            var data = storageProvider.GetData();
            foreach (var dataDatabaseFile in data.DatabaseFiles)
                dataDatabaseFile.DownloadAddress =
                    config.DownloadAddressBuilder.BuildUri(dataDatabaseFile.RelativePath).ToString();

            await storageProvider.Export(exportPath);
        }
    }
}