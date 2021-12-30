using System;
using System.IO;
using assetsUpdater.Interfaces;
using assetsUpdater.StorageProvider;

using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using assetsUpdater.Model.StorageProvider;

namespace assetsUpdater
{
    public abstract class DataManager
    {
      
        public IStorageProvider StorageProvider { get; private set; }

        protected DataManager(IStorageProvider storageProvider)
        {
            StorageProvider = storageProvider;

        }

        protected DataManager(string dbPath, bool isAsync = false)
        {
            DatabasePath = dbPath;
            if (!string.IsNullOrWhiteSpace(dbPath))
            {
                StorageProvider = new FileDatabase(dbPath,isAsync);
            }
        }

        protected DataManager(Stream dbStream)
        {
            if (dbStream.CanRead)
            {
                StorageProvider = new FileDatabase(dbStream);
                
            }

           
        }
        public string DatabasePath = null;
        
        public virtual Task<bool> IsDataValid()
        {
            var data = StorageProvider?.GetBuildInDbData();

            if (data != null)
            {
                if (data.DatabaseFiles?.Count() <= 0 ||
                    data.DatabaseFiles == null ||
                    data.Config.MirrorVersion == 0 ||
                    data.Config.MajorVersion == 0 ||
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
        /// 
        /// </summary>
        /// <typeparam name="TStorageProvider"></typeparam>
        /// <param name="config"></param>
        /// <exception cref="InvalidDataException"></exception>
        /// <returns></returns>
        public static async Task<IStorageProvider> BuildDatabase<TStorageProvider>(DbConfig config,bool isBuildUrl=false) where  TStorageProvider:IStorageProvider
        {
            
            var t = typeof(TStorageProvider);
            AssertVerify.OnMessageNotify(null,$"Building Database of type:{t.FullName}");
            var storageProvider = (IStorageProvider)Activator.CreateInstance(t);
            if (storageProvider!=null)
            {
                await storageProvider.Create(config).ConfigureAwait(false);
                if (isBuildUrl)
                {
                    var data = storageProvider.GetBuildInDbData();
                    foreach (var dataDatabaseFile in data.DatabaseFiles)
                    {
      
                        dataDatabaseFile.DownloadAddress =
                            config.DownloadAddressBuilder.BuildUri(dataDatabaseFile.RelativePath).ToString();
                    }


                }

            }
            AssertVerify.OnMessageNotify(null,$"Database Build finished type:{t.FullName}");
            return storageProvider;
        }
        /*public virtual Task<IStorageProvider> BuildDatabase(IStorageProvider storageProvider)
        {

        }*/
    }
}
