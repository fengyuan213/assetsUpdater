using assetsUpdater.EventArgs;
using assetsUpdater.Model.StorageProvider;

using System;
using System.Collections.Generic;

namespace assetsUpdater.Utils
{
    public class DbFileValueEqualityComparer : IEqualityComparer<DatabaseFile>
    {
        public bool Equals(DatabaseFile? x, DatabaseFile? y)
        {
            if ((x == null || y == null) && (x != null || y != null)) return false;
            if (x == null || y == null) return true;
            if (x.GetType() != y.GetType()) return false;

            if (x.Hash == y.Hash)
            {
                return true;
            }
            else if (string.IsNullOrWhiteSpace(x.Hash) || string.IsNullOrWhiteSpace(y.Hash))
            {
                if (x.FileSize == y.FileSize && x.FileName == y.FileName && x.RelativePath == y.RelativePath)
                {
                    AssertVerify.OnMessageNotify(this, $"Returning true for Less rigorous match detected x:{x.RelativePath},y:{y.RelativePath} ", MsgL.Warning);
                    return true;
                }
            }

            return false;
        }

        public int GetHashCode(DatabaseFile obj)
        {
            return HashCode.Combine(obj.RelativePath, obj.Hash, obj.FileSize, obj.DownloadAddress);
        }
    }
}