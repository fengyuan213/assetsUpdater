using assertUpdater.DbModel;
using assertUpdater.Network;
using assertUpdater.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assertUpdater.AddressBuilder;

namespace assertUpdater.Tests.TestData
{
    public static class StaticTestData
    {
        public static RemoteDbFile GetTestRemoteDbFile()
        {

            //Url: http://ipv4.download.thinkbroadband.com/5MB.zip
            var downloadRoot = "http://ipv4.download.thinkbroadband.com";
            var relativePath = "5MB.zip";
            var vcsRoot = Path.Join(Path.GetTempPath(),"AssertUpdaterDownloadTests");
            var shaExcepted = "0CC897BE1958C0F44371A8FF3DDDBC092FF530D0";
            var fileSize = 5 * 1024 * 1024;
            var addrBuilder = new DefaultAddressBuilder(downloadRoot, vcsRoot);

            var dbFile = new DbFile(relativePath, shaExcepted, fileSize);

            var remoteDbFile = new RemoteDbFile(addrBuilder,dbFile);
            return remoteDbFile;
        }
    }
}
