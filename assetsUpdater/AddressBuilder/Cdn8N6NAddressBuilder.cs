#region Using

using System;
using System.IO;
using assetsUpdater.Interfaces;
using assetsUpdater.Utils;
using Newtonsoft.Json;

#endregion

namespace assetsUpdater.AddressBuilder
{
    public class Cdn8N6NAddressBuilder : IAddressBuilder
    {
        public Cdn8N6NAddressBuilder(string downloadLocalRoot, string apiRoot, string apiKey, string apiSecret)
        {
            RootDownloadAddress = apiRoot;
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            LocalRootPath = downloadLocalRoot;
        }


        [JsonIgnore] public string ApiKey { get; set; }

        [JsonIgnore] public string ApiSecret { get; set; }

        public string RootDownloadAddress { get; set; }
        public string LocalRootPath { get; set; }

        public Uri BuildUri(string relativePath)
        {
            relativePath = FileUtils.MakeStandardRelativePath(relativePath).Replace('\\', '/');

            relativePath = AlgorithmHelper.UrlEncodeUTF8(relativePath);

            var uri = new UriBuilder
            {
                Host = RootDownloadAddress,
                Scheme = "https://",
                Port = 443,
                Path = relativePath
            };
            return uri.Uri;
        }

        public string BuildDownloadLocalPath(string relativePath)
        {
            return Path.Join(LocalRootPath, relativePath);
        }
    }
}