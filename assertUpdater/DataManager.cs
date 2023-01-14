using assertUpdater.DbModel;
using assertUpdater.StorageProvider;
using assertUpdater.Utils;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace assertUpdater
{
    public abstract class DataManager : ICloneable
    {

        public abstract DbConfig Config
        {

            get;
            set;
            //  private set { _dbConfig = value; }
        }

        //

        public abstract DbData Data
        {
            get;
            set;
        }

        public IStorageProvider StorageProvider { get; set; }
        /*    public CachedDataManager CachedDataManager { get; }

            protected DataManager(CachedDataManager cachedDataManager)
            {
                CachedDataManager = cachedDataManager;

            }*/
        protected abstract void SetDbData(DbData data);
        //_dbData = data;

        //StorageProvider.
        //LazyInitializer la = new Lazy<DataManager>();
        //LazyInitializer.
        //refreshdata
        protected virtual DbData GetDbData()
        {

            return StorageProvider.RefreshAsync().Result;
            /* if (Data==DbData.Empty)
             {
                 return StorageProvider.RefreshAsync().Result;
             }
             else
             {
                 return _dbData;
             }
             return DbData.Empty;
             */

        }
        protected DataManager(IStorageProvider storageProvider)
        {
            StorageProvider = storageProvider;

        }

        public virtual bool IsDataValid(bool allowEmptyValidDb = false)
        {

            if (Data?.DatabaseFiles == null || Data.Config is not { DatabaseSchema: { } })
            {
                return false;
            }

            bool allowEmptyDb = allowEmptyValidDb || Data.DatabaseFiles.Any();
            _ = allowEmptyDb &&
                         !string.IsNullOrWhiteSpace(Data.Config.DownloadAddressBuilder.RootDownloadAddress);

            if (!Data.DatabaseFiles.Any() ||
                Data.DatabaseFiles == null ||
                (Data.Config.MinorVersion == 0 &&
                 Data.Config.MajorVersion == 0) ||
                string.IsNullOrWhiteSpace(Data.Config.VersionControlFolder) ||
                Data.Config.DatabaseSchema == null
               )
            {
                return false;
            }

            foreach (DbFile df in Data.DatabaseFiles)
            {
                if (
                    string.IsNullOrWhiteSpace(df.FileName) ||
                    string.IsNullOrWhiteSpace(df.Hash) ||
                    string.IsNullOrWhiteSpace(df.RelativePath))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     IsBuildUniqueADdress: Allow to build a unique download address for each file (can't use for links are temporary)
        /// </summary>
        /// <typeparam name="TStorageProvider"></typeparam>
        /// <param name="config"></param>
        /// <param name="isBuildUniqueAddress"></param>
        /// <param name="buildParam"></param>
        /// <exception cref="InvalidDataException"></exception>
        /// <returns></returns>
        /// 
        [return: NotNull]
        public static async Task<IStorageProvider> BuildDatabase<TStorageProvider>(DbConfig config, params object[] buildParam) where TStorageProvider : IStorageProvider
        {

            Type t = typeof(TStorageProvider);
            //merging params
            DbConfig buildConfig = config;
            buildParam = buildParam.ToList().Prepend(buildConfig).ToArray();

            Console.WriteLine($"Building Database of type:{t.FullName}");
            //new object[]{config,"teststring"}
            IStorageProvider? storageProvider = (IStorageProvider?)Activator.CreateInstance(t, buildParam);
            if (storageProvider != null)
            {


                await DbCreateInternal(config, storageProvider).ConfigureAwait(false);

              
                Console.WriteLine("Refreshing Database...");
                await storageProvider.FlushAsync().ConfigureAwait(false);

                Console.WriteLine("Finished Refreshing Database..");
                return storageProvider;
            }
            else
            {
                throw new NullReferenceException(nameof(storageProvider));
            }


        }

        private static async Task DbCreateInternal(DbConfig config, IStorageProvider storageProvider)
        {
            config = (DbConfig)config.Clone();
            List<string> files = new();



            foreach (string file in config.DatabaseSchema.FileList)
            {
                try
                {
                    //ForSafetyReasons to a String.Replace
                    string fileValidated = FileUtils.MakeStandardRelativePath(file);

                    string path = Path.Join(config.VersionControlFolder, fileValidated);

                    if (!File.Exists(path))
                    {
                        //throw new FileNotFoundException("FileList配置的文件未找到:"+path, file);
                    }
                    else
                    {
                        files.Add(fileValidated);
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                    // AssertVerifier.OnMessageNotify(this, e.Message, e);
                }
            }

            foreach (string directory in config.DatabaseSchema.DirList)
            {
                string absRoot = Path.Join(config.VersionControlFolder, directory);
                if (Directory.Exists(absRoot))
                {
                    try
                    {
                        string[] paths = FileUtils.GetAllFilesInADirectory(config.VersionControlFolder, directory);

                        files.AddRange(paths);
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine($"Failed creating database, dirList not found Ex:\n{e}");
                        //AssertVerifier.OnMessageNotify(this, "DirList寻找失败", e);
                    }
                }

            }


            foreach (string file in files)
            {
                string absolutePath = Path.Join(config.VersionControlFolder, file);
                //AssertVerifier.OnMessageNotify(this, "Absolute Path:" + absolutePath, MsgL.Debug);
                Console.WriteLine("Absolute Path:{0}", absolutePath);
                //eg file before: /.minecraft/data.json
                //eg file after: /.minecraft/data.json
                //var path = file.Substring(1);
                DbFile vcf = new(file, FileUtils.Sha1File(absolutePath),
                    FileUtils.GetFileSize(absolutePath));
                //var vcf = new BuildInDbFile(file, Path.GetFileName(absolutePath), FileUtils.GetFileSize(absolutePath), FileUtils.Sha1File(absolutePath), null);
                await storageProvider.InsertAsync(vcf).ConfigureAwait(false);
            }


        }
        /*public virtual Task<IStorageProvider> BuildDatabase(IStorageProvider storageProvider)
        {
        }*/

        public virtual object Clone()
        {
            throw new InvalidOperationException("Can not clone a pure abstract object");

        }
    }
}
