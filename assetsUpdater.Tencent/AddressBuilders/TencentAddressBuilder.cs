#region Using

using assetsUpdater.Interfaces;
using assetsUpdater.Utils;

using global::System;

using System.IO;

#endregion

namespace assetsUpdater.Tencent.AddressBuilders
{
    public class TencentAddressBuilder : IAddressBuilder
    {
        public TencentAddressBuilder(string rootDownloadAddress, string localRootPath, string typeDSecret)
        {
            RootDownloadAddress = rootDownloadAddress;
            LocalRootPath = localRootPath;
            TypeDSecret = typeDSecret;
        }

        public string TypeDSecret { get; set; }
        public string RootDownloadAddress { get; set; }
        public string LocalRootPath { get; set; }

        public Uri BuildUri(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath)) throw new ArgumentNullException(relativePath, "relativePath null");
            relativePath = FileUtils.MakeStandardRelativePath(relativePath).Replace('\\', '/');

            relativePath = AlgorithmHelper.UrlEncodeUTF8(relativePath);

            //ExpiryTime for the cdn resource, unit seconds can't be 0 or negative
            const int expiryTime = 600;

            var timeStamp = TimeUtil.GetCurrentUtc1();

            timeStamp += expiryTime;
            var md5 = GetMd5SignedHashTypeD(relativePath, timeStamp);
            if (!RootDownloadAddress.EndsWith('/')) RootDownloadAddress += '/';
            //pokecity-1251938563.file.myqcloud.com
            //http://DomainName/FileName?sign=md5hash&t=timestamp
            var uri = new Uri(RootDownloadAddress + relativePath + "?sign=" + md5 + "&t=" + timeStamp);
            return uri;
        }

        public string BuildDownloadLocalPath(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath)) throw new ArgumentNullException(relativePath, "relativePath null");
            if (string.IsNullOrWhiteSpace(LocalRootPath))
                throw new ArgumentNullException(LocalRootPath, "localrootpath null");

            return Path.Join(LocalRootPath, relativePath);
        }

        private string GetMd5SignedHashTypeD(string relativePath, long timestamp)
        {
            switch (relativePath[0])
            {
                case '/':
                    break;

                default:
                    relativePath = '/' + relativePath;
                    break;
            }

            var md5 = AlgorithmHelper.Md5StringASCII(TypeDSecret + relativePath + timestamp);
            return md5.ToLower();
        }
    }
}