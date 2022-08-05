#region Using

using System.Threading.Tasks;
using assetsUpdater.Interfaces;
using assetsUpdater.Model.StorageProvider;

#endregion

namespace assetsUpdater
{
    public class DatabaseBuilder
    {
        private readonly IDbData _dbData;

        public DatabaseBuilder(IDbData dbData)
        {
            this._dbData = dbData;
        }

        public async Task BuildDatabase(DbConfig config, string exportPath)
        {
            await _dbData.Create(config).ConfigureAwait(false);

            await _dbData.Export(exportPath);
        }

        public async Task BuildDatabaseWithUrl(DbConfig config, string exportPath)
        {
            await _dbData.Create(config).ConfigureAwait(false);
            var data = _dbData.Data();
            foreach (var dataDatabaseFile in data.DatabaseFiles)
                dataDatabaseFile.DownloadAddress =
                    config.DownloadAddressBuilder.BuildUri(dataDatabaseFile.RelativePath).ToString();

            await _dbData.Export(exportPath);
        }
    }
}