
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection;
using assetsUpdater.Interfaces;
using assetsUpdater.Model;
using assetsUpdater.Model.StorageProvider;
using assetsUpdater.Utils;
using Newtonsoft.Json;
#region 引用

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
#endregion

namespace assetsUpdater.StorageProvider
{
    /// <summary>
    ///     FileDatabase give access to the version control system keneral
    /// </summary>
    public class FileDatabase : IEquatable<FileDatabase>, IStorageProvider, ICloneable
    {
        #region Prop

        /// <summary>
        ///     Represents the data
        /// </summary>

        public DbData Data { get; set; }

        //public static NetworkCredential NutstoreDownloadCredential { get; } = new NetworkCredential("liufengyuan45@outlook.com", "ags7xjgzuhr2ig3d");
        #endregion

        public FileDatabase(string path)
        {
            this.Read(path).RunSynchronously();

        }
        public FileDatabase()
        {
        }

        /// <summary>
        /// 
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
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            using var webClient = new WebClient
            {
                Credentials = networkCredential ?? CredentialCache.DefaultNetworkCredentials
            };
            await using var stream = new MemoryStream(await webClient.DownloadDataTaskAsync(url).ConfigureAwait(true));
            using var zipArchive = new ZipArchive(stream, ZipArchiveMode.Read);
            Data = await DbDataReader(zipArchive).ConfigureAwait(true);
        }


        private Task<DbData> DbDataReader(ZipArchive zipArchive)
        {
            var stream = zipArchive.GetEntry("data.json")?.Open();
            using var streamReader = new StreamReader(stream ?? new MemoryStream());
            var content = streamReader.ReadToEnd();
            Data = JsonConvert.DeserializeObject<DbData>(content);
            return Task.FromResult(Data);
        }
        /// <summary>
        ///     Open and read the database data to the current class.
        /// </summary>
        /// <param name="path">Database Path, Could be relative(reference to current directory) or absolute </param>
        /// <exception cref="IOException"> When Database path not valid</exception>

        public async Task Read([NotNull] object obj)
        {
            var path = obj as string;
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(path, "数据库路径不能为空");
            if (!File.Exists(path)) throw new IOException("Database can't be found");
            try
            {
                
                using (var zipArchive = ZipFile.OpenRead(path))
                {
                    await DbDataReader(zipArchive).ConfigureAwait(true);

                }
            }
            catch (InvalidDataException e)
            {
                Console.WriteLine(e);
                throw new IOException("数据库不合法", e);
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
            var files = new List<string>();
            foreach (var directory in config.DatabaseSchema.DirList)
                files.AddRange(FileUtils.GetAllFilesInADirectory(config.VersionControlFolder, directory ?? ""));

            var databseFiles = new List<DatabaseFile>();
            foreach (var file in files)
            {
                var absolutePath = Path.Combine(config.VersionControlFolder, file);
                var vcf= new DatabaseFile(file, FileUtils.Sha1File(absolutePath),
                    FileUtils.GetFileSize(absolutePath), null);
                //var vcf = new BuildInDbFile(file, Path.GetFileName(absolutePath), FileUtils.GetFileSize(absolutePath), FileUtils.Sha1File(absolutePath), null);
                databseFiles.Add(vcf);
            }

            Data = new DbData(config)
            {
                DatabaseFiles = databseFiles,

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
            var stream = new FileStream(path, FileMode.Create);
            using (var zipArchive = new ZipArchive(stream, ZipArchiveMode.Create))
            {
                var entry = zipArchive.CreateEntry("data.json");
                using (var streamWriter = new StreamWriter(entry.Open()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
            }
            return Task.CompletedTask;
        }


        /// <summary>
        ///     Check if the content of the two database are equal
        /// </summary>s
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(FileDatabase other)
        {
            return ObjectComparerUtility.ObjectsAreEqual(Data, other?.Data);

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


        public object Clone()
        {
            return new FileDatabase()
            {
                Data = Data

            };
        }

        public bool IsValidDb()
        {
            if (Data.DatabaseFiles == null || Data.Config is not {DatabaseSchema: { }}) return false;
            return Data.DatabaseFiles.Any()&&!string.IsNullOrWhiteSpace(Data.Config.VersionControlFolder)&&string.IsNullOrWhiteSpace(Data.Config.DownloadAddressBuilder.RootDownloadAddress);
        }
  
    }
}