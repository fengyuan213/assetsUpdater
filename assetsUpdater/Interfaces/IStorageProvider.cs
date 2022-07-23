#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using assetsUpdater.Model.StorageProvider;

#endregion

namespace assetsUpdater.Interfaces
{
    public interface IStorageProvider : IEquatable<object>, ICloneable
    {
        DbData GetData();

        Task Create(DbConfig config);

        IDictionary<string, DatabaseFile> ConvertToDictionary();

        Task Export(string path);

        Task Read(string ldbPath);

        Task Read(Stream stream);

        Task Download(object obj);
    }
}