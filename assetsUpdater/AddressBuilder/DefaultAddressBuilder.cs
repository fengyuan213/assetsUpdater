#region Using

using System;
using System.IO;
using assetsUpdater.Interfaces;

#endregion

namespace assetsUpdater.AddressBuilder
{
    public class DefaultAddressBuilder : IAddressBuilder
    {
        public DefaultAddressBuilder(string rootDownloadAddress, string localRootPath)
        {
            RootDownloadAddress = rootDownloadAddress;
            LocalRootPath = localRootPath;
        }

        public string RootDownloadAddress { get; set; }
        public string LocalRootPath { get; set; }

        public Uri BuildUri(string relativePath)
        {
            return new Uri(RootDownloadAddress + relativePath);
        }

        public string BuildDownloadLocalPath(string relativePath)
        {
            return Path.Join(LocalRootPath, relativePath);
        }
    }
}