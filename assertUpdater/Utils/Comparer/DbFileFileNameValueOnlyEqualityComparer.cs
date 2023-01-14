using assertUpdater.DbModel;

namespace assertUpdater.Utils.Comparer
{

    public class DbFileFileNameValueOnlyEqualityComparer : IEqualityComparer<DbFile>
    {
        /// <summary>
        ///  Used only when comparing with database with Added/Differed/Deleted files
        /// </summary>
        /// <param name="allowLessRigorous"> If the file has empty hash but  same size, path ,name are considered equal</param>
        public DbFileFileNameValueOnlyEqualityComparer(bool allowLessRigorous = true)
        {

        }
        public bool Equals(DbFile? x, DbFile? y)
        {

            return (x != null && y != null || x == null && y == null)
&& (x == null || y == null || (x.GetType() == y.GetType() && x.RelativePath == y.RelativePath));
            /*   if (x.Hash == y.Hash && x.FileSize==y.FileSize)
                   return true;
               if (string.IsNullOrWhiteSpace(x.Hash) || string.IsNullOrWhiteSpace(y.Hash))
                   if (x.FileSize == y.FileSize && x.FileName == y.FileName && x.RelativePath == y.RelativePath)
                   {
                       Console.WriteLine( $"Returning true for Less rigorous match detected x:{x.RelativePath},y:{y.RelativePath} ");
                       return true;
                   }

               return false;
             */

        }

        public int GetHashCode(DbFile obj)
        {
            return HashCode.Combine(obj.RelativePath, obj.Hash, obj.FileSize);
        }
    }
}
