#region Using

using assetsUpdater.Model.StorageProvider;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

#endregion

namespace assetsUpdater.Interfaces
{
    public interface IStorageProvider : IEquatable<object>, ICloneable
    {
        DbData GetBuildInDbData();

        Task Create(DbConfig config);

        IDictionary<string, DatabaseFile> ConvertToDictionary();

        Task Export(string path);

        Task Read(string ldbPath);

        Task Read(Stream stream);

        Task Download(object obj);
    }
}