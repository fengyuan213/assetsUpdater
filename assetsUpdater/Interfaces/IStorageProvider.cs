using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using assetsUpdater.Model;
using assetsUpdater.Model.StorageProvider;
using assetsUpdater.StorageProvider;

namespace assetsUpdater.Interfaces
{
    public interface IStorageProvider : IEquatable<Object>
    {
        BuildInDbData GetBuildInDbData();
        Task Create(BuildInDbConfig config);
        IDictionary<string, BuildInDbFile> ConvertToDictionary();
        Task Export(string path);
        Task Read(Object obj);
        Task Download(object obj);
    }
}