using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assetsUpdater.Interfaces;

namespace assetsUpdater.AddressBuilder
{
    public class DefaultAddressBuilder :IAddressBuilder
    {
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

        public DefaultAddressBuilder(string rootDownloadAddress, string localRootPath)
        {
            RootDownloadAddress = rootDownloadAddress;
            LocalRootPath = localRootPath;
        }
    }
}
