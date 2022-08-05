#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using assetsUpdater.Model.StorageProvider;

#endregion

namespace assetsUpdater.Interfaces
{
    public interface IDbData : IEquatable<object>, ICloneable
    {
        DbData Data();
        
    }
}