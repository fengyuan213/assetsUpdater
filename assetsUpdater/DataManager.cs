using assetsUpdater.EventArgs;
using assetsUpdater.Interfaces;
using assetsUpdater.Model.StorageProvider;
using assetsUpdater.StorageProvider;

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace assetsUpdater
{
    public abstract class DataManager
    {
        public IStorageProvider StorageProvider { get; private set; }

        protected DataManager(IStorageProvider storageProvider)
        {
            StorageProvider = storageProvider;
        }

#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的 属性“StorageProvider”必须包含非 null 值。请考虑将 属性 声明为可以为 null。

        protected DataManager(string dbPath, bool isAsync = false)
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的 属性“StorageProvider”必须包含非 null 值。请考虑将 属性 声明为可以为 null。
        {
            DatabasePath = dbPath;
            if (!string.IsNullOrWhiteSpace(dbPath))
            {
                StorageProvider = new FileDatabase(dbPath, isAsync);
            }
        }

#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的 属性“StorageProvider”必须包含非 null 值。请考虑将 属性 声明为可以为 null。

        protected DataManager(Stream dbStream)
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的 属性“StorageProvider”必须包含非 null 值。请考虑将 属性 声明为可以为 null。
        {
            if (dbStream.CanRead)
            {
                StorageProvider = new FileDatabase(dbStream);
            }
        }

        public string? DatabasePath = null;

        public virtual Task<bool> IsDataValid()
        {
            var data = StorageProvider?.GetBuildInDbData();

            if (data != null)
            {
                if (data.DatabaseFiles?.Count() <= 0 ||
                    data.DatabaseFiles == null ||
                    (data.Config.MinorVersion == 0 &&
                    data.Config.MajorVersion == 0) ||
                    string.IsNullOrWhiteSpace(data.Config.VersionControlFolder) ||
                    data.Config.DatabaseSchema == null

                )
                {
                    return Task.FromResult(false);
                }
                foreach (var df in data.DatabaseFiles)
                {
                    if (
                        string.IsNullOrWhiteSpace(df.FileName) ||
                        string.IsNullOrWhiteSpace(df.Hash) ||
                        string.IsNullOrWhiteSpace(df.RelativePath))
                    {
                        return Task.FromResult(false);
                    }
                }
            }

            return Task.FromResult(true);
        }

        /// <summary>
        /// IsBuildUniqueADdress: Allow to build a unique download address for each file (can't use for links are temporary)
        /// </summary>
        /// <typeparam name="TStorageProvider"></typeparam>
        /// <param name="config"></param>
        /// <exception cref="InvalidDataException"></exception>
        /// <returns></returns>
        ///
        public static async Task<IStorageProvider> BuildDatabase<TStorageProvider>(DbConfig config, bool isBuildUniqueAddress = false) where TStorageProvider : IStorageProvider
        {
            var t = typeof(TStorageProvider);
            AssertVerify.OnMessageNotify(null, $"Building Database of type:{t.FullName}");
            var storageProvider = (IStorageProvider?)Activator.CreateInstance(t);
            if (storageProvider != null)
            {
                await storageProvider.Create(config).ConfigureAwait(false);
                if (isBuildUniqueAddress)
                {
                    AssertVerify.OnMessageNotify(MethodBase.GetCurrentMethod(), MsgL.Info, "Database are built by unique urls", false, null);
                    var data = storageProvider.GetBuildInDbData();
                    foreach (var dataDatabaseFile in data.DatabaseFiles)
                    {
                        dataDatabaseFile.DownloadAddress =
                            config.DownloadAddressBuilder.BuildUri(dataDatabaseFile.RelativePath).ToString();
                    }
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