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
        DbData GetBuildInDbData();
        Task Create(DbConfig config); 
        IDictionary<string, DatabaseFile> ConvertToDictionary();
        Task Export(string path);
        Task Read(Object obj);
        Task Download(object obj);
    }
}