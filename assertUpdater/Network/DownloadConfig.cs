using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace assertUpdater.Network
{
    public class DownloadConfig
    {
        public static DownloadConfig DefaultConfig()
        {
            var downloadConfig = new DownloadConfig();
            downloadConfig.Credentials = CredentialCache.DefaultNetworkCredentials;
            return downloadConfig;
        }
        public DownloadConfig()
        {
            //Init Defaults
            Credentials = CredentialCache.DefaultNetworkCredentials;
        }
        public NetworkCredential Credentials { get; set; }
        
    }
}
