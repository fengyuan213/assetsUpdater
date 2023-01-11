using Newtonsoft.Json;

using System.Diagnostics.CodeAnalysis;

namespace assertUpdaterRefactor.AddressBuilder
{
    public interface IAddressBuilder
    {
        /// <summary>
        ///     The main download Address to download VersionControlFiles.
        ///     Usage format: RealDownloadUrl= &"{RootDownloadUrl}/version control filename"
        /// </summary>
        [NotNull]

        public string RootDownloadAddress { get; set; }

        [JsonIgnore] public string LocalRootPath { get; set; }

        public Uri BuildUri(string relativePath);

        public string BuildDownloadLocalPath(string relativePath);
    }
}
