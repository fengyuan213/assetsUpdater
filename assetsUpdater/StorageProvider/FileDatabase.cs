
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

        public BuildInDbData Data { get; set; }

        //public static NetworkCredential NutstoreDownloadCredential { get; } = new NetworkCredential("liufengyuan45@outlook.com", "ags7xjgzuhr2ig3d");
        #endregion

        public FileDatabase()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">IEnumerable<object>,obj[0] download address obj[1] NetworkCredential}</param>
        /// <returns></returns>
        public async Task Download(object obj)
        {
            //  [NotNull] string url, NetworkCredential networkCredential = null;
            var enumrObj = obj as IEnumerable<object>;
            var url = enumrObj.ToList()[0] as string;
            var networkCredential = enumrObj.ToList()[1] as NetworkCredential;
            using (var webclient = new WebClient())
            {
                //Nutstore services creditionals
                webclient.Credentials = networkCredential ?? CredentialCache.DefaultNetworkCredentials;
                var data = webclient.DownloadData(url);
                var stream = new MemoryStream(data);

                using (var zipArchive = new ZipArchive(stream, ZipArchiveMode.Read))
                {
                    Data = await DbDataReader(zipArchive).ConfigureAwait(true);
                }

            }

        }

        private Task<BuildInDbData> DbDataReader(ZipArchive zipArchive)
        {
            var stream = zipArchive.GetEntry("data.json")?.Open();
            using var streamReader = new StreamReader(stream ?? new MemoryStream());
            var content = streamReader.ReadToEnd();
            Data = JsonConvert.DeserializeObject<BuildInDbData>(content);
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
        public Task Create([NotNull] BuildInDbConfig config)
        {
            var files = new List<string>();
            foreach (var directory in config.DatabaseSchema.DirList)
                files.AddRange(FileUtils.GetAllFilesInADirectory(config.VersionControlFolder, directory ?? ""));

            var databseFiles = new List<BuildInDbFile>();
            foreach (var file in files)
            {
                var absolutePath = Path.Combine(config.VersionControlFolder, file);
                var vcf= new BuildInDbFile(file, FileUtils.Sha1File(absolutePath),
                    FileUtils.GetFileSize(absolutePath), null);
                //var vcf = new BuildInDbFile(file, Path.GetFileName(absolutePath), FileUtils.GetFileSize(absolutePath), FileUtils.Sha1File(absolutePath), null);
                databseFiles.Add(vcf);
            }

            Data = new BuildInDbData(config)
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

        public IDictionary<string, BuildInDbFile> ConvertToDictionary()
        {

            return Data?.DatabaseFiles?.ToDictionary(versionControlFile => versionControlFile.RelativePath);
        }

        public BuildInDbData GetBuildInDbData()
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
    }
}