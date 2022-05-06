#region Using

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using assetsUpdater.EventArgs;
using assetsUpdater.Interfaces;
using assetsUpdater.Model.StorageProvider;
using assetsUpdater.StorageProvider;

#endregion

namespace assetsUpdater
{
    public abstract class DataManager
    {
        public string DatabasePath;

        protected DataManager(IStorageProvider storageProvider)
        {
            StorageProvider = storageProvider;
        }


        protected DataManager(string dbPath, bool isAsync = false)

        {
            DatabasePath = dbPath;
            if (!string.IsNullOrWhiteSpace(dbPath)) StorageProvider = new FileDatabase(dbPath, isAsync);
        }


        protected DataManager(Stream dbStream)
        {
            if (dbStream.CanRead) StorageProvider = new FileDatabase(dbStream);
        }

        public IStorageProvider StorageProvider { get; }

        public virtual Task<bool> IsDataValid()
        {
            var data = StorageProvider.GetBuildInDbData();

            if (!data.DatabaseFiles.Any() ||
                data.DatabaseFiles == null ||
                (data.Config.MinorVersion == 0 &&
                 data.Config.MajorVersion == 0) ||
                string.IsNullOrWhiteSpace(data.Config.VersionControlFolder) ||
                data.Config.DatabaseSchema == null
               )
                return Task.FromResult(false);
            foreach (var df in data.DatabaseFiles)
                if (
                    string.IsNullOrWhiteSpace(df.FileName) ||
                    string.IsNullOrWhiteSpace(df.Hash) ||
                    string.IsNullOrWhiteSpace(df.RelativePath))
                    return Task.FromResult(false);


            return Task.FromResult(true);
        }

        /// <summary>
        ///     IsBuildUniqueADdress: Allow to build a unique download address for each file (can't use for links are temporary)
        /// </summary>
        /// <typeparam name="TStorageProvider"></typeparam>
        /// <param name="config"></param>
        /// <param name="isBuildUniqueAddress"></param>
        /// <exception cref="InvalidDataException"></exception>
        /// <returns></returns>
        public static async Task<IStorageProvider> BuildDatabase<TStorageProvider>(DbConfig config,
            bool isBuildUniqueAddress = false) where TStorageProvider : IStorageProvider
        {
            var t = typeof(TStorageProvider);
            AssertVerify.OnMessageNotify(null, $"Building Database of type:{t.FullName}");
            var storageProvider = (IStorageProvider)Activator.CreateInstance(t);
            if (storageProvider != null)
            {
                await storageProvider.Create(config).ConfigureAwait(false);
                if (isBuildUniqueAddress)
                {
                    AssertVerify.OnMessageNotify(MethodBase.GetCurrentMethod(), MsgL.Info,
                        "Database are built by unique urls", false, null);
                    var data = storageProvider.GetBuildInDbData();
                    foreach (var dataDatabaseFile in data.DatabaseFiles)
                        dataDatabaseFile.DownloadAddress =
                            config.DownloadAddressBuilder.BuildUri(dataDatabaseFile.RelativePath).ToString();
                }
            }

            AssertVerify.OnMessageNotify(null, $"Database Build finished type:{t.FullName}");
            return storageProvider;
        }

        /*public virtual Task<IStorageProvider> BuildDatabase(IStorageProvider storageProvider)
        {
        }*/
    }
}