using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace assertUpdater.Network
{
    public static class DownloadOnetimeInit
    {
        private static bool IsInited = false;

        public static void Init()
        {
            if (IsInited)
            {
                return;
            }
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) =>
            {
                // local dev, just approve all certs
                var development = true;
                if (development) return true;
                return errors == SslPolicyErrors.None;
            };
            IsInited = true;
        }
    }
}
