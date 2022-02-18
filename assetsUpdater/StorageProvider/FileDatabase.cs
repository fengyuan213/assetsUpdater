#region Using

#region Using

using assetsUpdater.AddressBuilder;
using assetsUpdater.EventArgs;
using assetsUpdater.Exceptions;
using assetsUpdater.Interfaces;
using assetsUpdater.Model.StorageProvider;
using assetsUpdater.Utils;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

#endregion Using

#endregion

namespace assetsUpdater.StorageProvider
{
    /// <summary>
    ///     FileDatabase give access to the version control system keneral
    /// </summary>
    public class FileDatabase : IEquatable<FileDatabase>, IStorageProvider, ICloneable
    {
        /// <summary>
        ///     May throw exception
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isAsync"></param>
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的 属性“Data”必须包含非 null 值。请考虑将 属性 声明为可以为 null。

        public FileDatabase(string path, bool isAsync = false)
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的 属性“Data”必须包含非 null 值。请考虑将 属性 声明为可以为 null。
        {
            if (isAsync)
                Task.Run(async delegate { await Read(path).ConfigureAwait(false); });
            else
                Read(path).Wait();
        }

#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的 属性“Data”必须包含非 null 值。请考虑将 属性 声明为可以为 null。

        public FileDatabase()
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的 属性“Data”必须包含非 null 值。请考虑将 属性 声明为可以为 null。
        {
        }

#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的 属性“Data”必须包含非 null 值。请考虑将 属性 声明为可以为 null。

        public FileDatabase(Stream stream)
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的 属性“Data”必须包含非 null 值。请考虑将 属性 声明为可以为 null。
        {
            Task.Run(async delegate { await Read(stream).ConfigureAwait(false); });
        }

        public FileDatabase(IStorageProvider storageProvider)
        {
            Data = storageProvider.GetBuildInDbData();
        }

        #region Prop

        /// <summary>
        ///     Represents the data
        /// </summary>

        public DbData Data { get; set; }

        //public static NetworkCredential NutstoreDownloadCredential { get; } = new NetworkCredential("liufengyuan45@outlook.com", "ags7xjgzuhr2ig3d");

        #endregion

        /// <summary>
        ///     Check if the content of the two database are equal
        /// </summary>
        /// s
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(FileDatabase? other)
        {
            return ObjectComparerUtility.ObjectsAreEqual(Data, other?.Data);
        }

        /// <summary>
        ///     Clones this object
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            //settings.Converters.Add(new ConcreteTypeConverter<DefaultAddressBuilder, IAddressBuilder>());
            //settings.
            return new FileDatabase
            {
                Data = CopyData(Data)
            };

            DbData CopyData(DbData data)
            {
                List<DatabaseFile> files;
                files = Data.DatabaseFiles.ToList().CloneJson();

                return new DbData((DbConfig)Data.Config.Clone()) { DatabaseFiles = files };
            }
        }

        /// <summary>
        ///     Download a remote database
        /// </summary>
        /// <param name="obj">IEnumerable<object>,obj[0] download address obj[1] NetworkCredential}</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public async Task Download(object obj)
        {
            //  [NotNull] string url, NetworkCredential networkCredential = null;
            var arrObj = obj as object[];
            var url = arrObj?[0] as string;
            var networkCredential = arrObj?[1] as NetworkCredential;
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));

            using var webClient = new WebClient
            {
                Credentials = networkCredential ?? CredentialCache.DefaultNetworkCredentials
            };
            await using var stream = new MemoryStream(await webClient.DownloadDataTaskAsync(url).ConfigureAwait(true));
            using var zipArchive = new ZipArchive(stream, ZipArchiveMode.Read);
            Data = await DbDataReader(zipArchive).ConfigureAwait(false);
        }

        /// <summary>
        ///     Open and Read the database in in to Memory
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task Read(Stream stream)
        {
            throw new NotImplementedException();
#pragma warning disable CS0162 // 检测到无法访问的代码
            try
#pragma warning restore CS0162 // 检测到无法访问的代码
            {
                //var ms = new MemoryStream();

                //await stream.CopyToAsync(ms);

                var zipArchive = new ZipArchive(stream);

                await DbDataReader(zipArchive);
            }
            catch (InvalidDataException e)
            {
                Console.WriteLine(e);
                throw new IOException("数据库不合法", e);
            }

            await stream.DisposeAsync();
        }

        /// <summary>
        ///     Open and read the database data to the current class.
        /// </summary>
        /// <param name="dbPath">Database Path, Could be relative(reference to current directory) or absolute </param>
        /// <exception cref="IOException"> When Database path not valid</exception>
        public async Task Read([NotNull] string dbPath)
        {
            if (string.IsNullOrWhiteSpace(dbPath)) throw new ArgumentNullException(dbPath, "数据库路径不能为空");
            if (!File.Exists(dbPath)) throw new IOException("Database can't be found");
            try
            {
                using (var zipArchive = ZipFile.OpenRead(dbPath))
                {
                    await DbDataReader(zipArchive);
                }
            }
            catch (InvalidDataException e)
            {
                AssertVerify.OnMessageNotify(this, "数据库不合法", e);
                throw;
            }
        }

        /// <summary>
        ///     Create the FileDatabase and export to the specified destination
        /// </summary>
        /// <param name="config"></param>
        /// <param name="path"></param>
        /*//public void CreateToFile([NotNull] BuildInDbConfig config, [NotNull] string path)
        //{
        //    Create(config);
        //    Export(path);
        //}*/

        /// <summary>
        ///     Create FileDatabase and save the result to the current class
        /// </summary>
        /// <param name="config"></param>
        public Task Create([NotNull] DbConfig config)
        {
            config = (DbConfig)config.Clone();
            var files = new List<string>();

            //TODO: Build database Testing with FileList and DirList
            foreach (var file in config.DatabaseSchema.FileList)
                try
                {
                    //ForSafetyReasons to a String.Replace
                    var fileValidated = FileUtils.MakeStandardRelativePath(file);

                    var path = Path.Join(config.VersionControlFolder, fileValidated);

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
                    AssertVerify.OnMessageNotify(this, e.Message, e);
                }

            foreach (var directory in config.DatabaseSchema.DirList)
                if (Directory.Exists(Path.Join(config.VersionControlFolder, directory)))
                    try
                    {
                        var paths = FileUtils.GetAllFilesInADirectory(config.VersionControlFolder, directory ?? "");

                        files.AddRange(paths);
                    }
                    catch (Exception e)
                    {
                        AssertVerify.OnMessageNotify(this, "DirList寻找失败", e);
                    }

            var databaseFiles = new List<DatabaseFile>();
            foreach (var file in files)
            {
                var absolutePath = Path.Join(config.VersionControlFolder, file);
                AssertVerify.OnMessageNotify(this, "Absolute Path:" + absolutePath, MsgL.Debug);
                //Console.WriteLine("Absolute Path:{0}",absolutePath);
                //eg file before: /.minecraft/data.json
                //eg file after: /.minecraft/data.json
                //var path = file.Substring(1);
                var vcf = new DatabaseFile(file, FileUtils.Sha1File(absolutePath),
                    FileUtils.GetFileSize(absolutePath), null);
                //var vcf = new BuildInDbFile(file, Path.GetFileName(absolutePath), FileUtils.GetFileSize(absolutePath), FileUtils.Sha1File(absolutePath), null);
                databaseFiles.Add(vcf);
            }

            //! Important, given default localRoot to vcs folder
            //! Prevent argument null when manager uploading database
            config.DownloadAddressBuilder.LocalRootPath = config.VersionControlFolder;

            Data = new DbData(config)
            {
                DatabaseFiles = databaseFiles
            };

            return Task.CompletedTask;
        }

        /// <summary>
        ///     Export the FileDatabase to your specified path
        /// </summary>
        /// <param name="path"></param>
        public Task Export([NotNull] string path)
        {
            var json = JsonConvert.SerializeObject(Data);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                using (var zipArchive = new ZipArchive(stream, ZipArchiveMode.Create))
                {
                    var entry = zipArchive.CreateEntry("data.json");

                    using (var streamWriter = new StreamWriter(entry.Open()))
                    {
                        streamWriter.Write(json);
                        streamWriter.Flush();
                    }
                }
            }

            return Task.CompletedTask;
        }

        /// <summary>
        ///     Convert Database files into Dictionary style
        /// </summary>
        /// <returns>string:RelativePath, VersionControlFile: The file in the vc </returns>
        public IDictionary<string, DatabaseFile> ConvertToDictionary()
        {
            return Data?.DatabaseFiles?.ToDictionary(versionControlFile => versionControlFile.RelativePath);
        }

        public DbData GetBuildInDbData()
        {
            return Data;
        }

        private Task<DbData> DbDataReader(ZipArchive zipArchive)
        {
            try
            {
                using var stream = zipArchive.GetEntry("data.json")?.Open() ??
                                   throw new FileDatabaseInvalidException("can't open db data.json");
                using var streamReader = new StreamReader(stream);
                var content = streamReader.ReadToEnd();
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                };
                settings.Converters.Add(new ConcreteTypeConverter<DefaultAddressBuilder, IAddressBuilder>());
                //settings.
                //Data = JsonConvert.DeserializeObject<DbData>(content);

                Data = JsonConvert.DeserializeObject<DbData>(content, settings) ??
                       throw new ArgumentNullException(nameof(DbData));

                //AssertVerify.OnMessageNotify(this, "Unable to Deserialize db json to Object", MsgL.Error);
                return Task.FromResult(Data);
            }
            catch (Exception e)
            {
                AssertVerify.OnMessageNotify(this, "Unable to read database", MsgL.Error, e);
                throw;
            }
        }

        public bool IsValidDb(bool allowEmptyValidDb = false)
        {
            if (Data?.DatabaseFiles == null || Data.Config is not { DatabaseSchema: { } }) return false;
            var allowEmptyDb = allowEmptyValidDb || Data.DatabaseFiles.Any();
            var result = allowEmptyDb &&
                         !string.IsNullOrWhiteSpace(Data.Config.DownloadAddressBuilder.RootDownloadAddress);
            return result;
        }
    }
}