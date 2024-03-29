﻿using assertUpdater.Utils;

namespace assertUpdater.AddressBuilder
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
            relativePath = FileUtils.MakeStandardRelativePath(relativePath).Replace('\\', '/');

            relativePath = AlgorithmUtils.UrlEncodeUTF8(relativePath);

            return new Uri(RootDownloadAddress + '/' + relativePath);
        }

        public string BuildDownloadLocalPath(string relativePath)
        {
            return Path.Join(LocalRootPath, relativePath);
        }
    }
}
