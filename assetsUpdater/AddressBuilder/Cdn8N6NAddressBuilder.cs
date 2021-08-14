using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assetsUpdater.Interfaces;
using Newtonsoft.Json;

namespace assetsUpdater.AddressBuilder
{
    
    public class Cdn8N6NAddressBuilder:IAddressBuilder
    {


        [JsonIgnore]
        public string ApiKey { get; set; }
        [JsonIgnore]
        public string ApiSecret { get; set; }
       

        public Cdn8N6NAddressBuilder(string downloadLocalRoot,string apiRoot, string apiKey, string apiSecret)
        {
            RootDownloadAddress = apiRoot;
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            LocalRootPath = downloadLocalRoot;
        }
        
        public string RootDownloadAddress { get; set; }
        public string LocalRootPath { get; set; }

        public  Uri BuildUri(string relativePath)
        {
            var uri = new UriBuilder
            {
                Host = RootDownloadAddress, Scheme = "https://",
                Port = 443, 
                Path = relativePath
            };
            return uri.Uri;
        }
       
        public  string BuildDownloadLocalPath(string relativePath)
        {
            return Path.Combine(LocalRootPath, relativePath);
        }
    }
}
