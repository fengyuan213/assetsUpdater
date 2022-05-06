#region Using

using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

#endregion

namespace assetsUpdater.Interfaces
{
    public interface IAddressBuilder
    {
        /// <summary>
        ///     The main download Address to download VersionControlFiles.
        ///     Usage format: RealDownloadUrl= &"{RootDownloadUrl}/version control filename"
        /// </summary>
        [NotNull]
        [JsonProperty("RootDownloadAddress")]
        public string RootDownloadAddress { get; set; }

        [JsonIgnore] public string LocalRootPath { get; set; }

        public Uri BuildUri(string relativePath);

        public string BuildDownloadLocalPath(string relativePath);
    }
}