using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using assetsUpdater.Interfaces;
using Newtonsoft.Json;

namespace assetsUpdater.Model.StorageProvider
{
    public class DbConfig
    {
        /// <summary>
        /// This constructor is used for creating local database
        /// </summary>
        /// <param name="versionControlFolder"></param>
        [JsonConstructor]
        public DbConfig([NotNull] string versionControlFolder)
        {

            VersionControlFolder = versionControlFolder;
        }

        [NotNull] public string VersionControlFolder { get; set; }
        [NotNull] public DbSchema DatabaseSchema { get; set; }
        /// <summary>
        ///     The major version of the database
        /// </summary>
        [AllowNull] public int MajorVersion { get; set; }
        /// <summary>
        ///     The mirror version of the database
        /// </summary>
        [AllowNull] public int MirrorVersion { get; set; }


        [AllowNull] public IAddressBuilder DownloadAddressBuilder { get; set; }
    }
}
