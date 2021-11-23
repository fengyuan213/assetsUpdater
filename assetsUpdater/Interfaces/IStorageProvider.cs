#region Using

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using assetsUpdater.Model.StorageProvider;

#endregion

namespace assetsUpdater.Interfaces
{
    public interface IStorageProvider : IEquatable<object>
    {
        DbData GetBuildInDbData();
        Task Create(DbConfig config);
        IDictionary<string, DatabaseFile> ConvertToDictionary();
        Task Export(string path);
        Task Read(object obj);
        Task Download(object obj);
    }
}