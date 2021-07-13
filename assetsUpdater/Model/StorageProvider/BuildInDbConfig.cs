using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

using Newtonsoft.Json;

namespace assetsUpdater.Model.StorageProvider
{
    public class BuildInDbConfig
    {
        /// <summary>
        /// This constructor is used for creating local database
        /// </summary>
        /// <param name="versionControlFolder"></param>
        [JsonConstructor]
        public BuildInDbConfig([NotNull] string versionControlFolder)
        {

            VersionControlFolder = versionControlFolder;
        }
        /// <summary>
        /// This consuctor is used for creating remote database
        /// </summary>
        /// <param name="versionControlFolder"></param>
        /// <param name="rootDownloadAddress"></param>
        public BuildInDbConfig([NotNull] string versionControlFolder, [NotNull] string rootDownloadAddress)
        {
            VersionControlFolder = versionControlFolder;
            RootDownloadAddress = rootDownloadAddress;
        }

        [NotNull] public string VersionControlFolder { get; set; }
        [NotNull] public BuildInDbSchema DatabaseSchema { get; set; }
        /// <summary>
        ///     The major version of the database
        /// </summary>
        [AllowNull] public int MajorVersion { get; set; }
        /// <summary>
        ///     The mirror version of the database
        /// </summary>
        [AllowNull] public int MirrorVersion { get; set; }
        /// <summary>
        ///     The main download Address to download VersionControlFiles.
        ///     Usage format: RealDownloadUrl= &"{RootDownloadUrl}/version control filename"
        /// </summary>
        [NotNull] public string RootDownloadAddress { get; set; } = string.Empty;
    }
}
