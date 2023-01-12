using assertUpdater.Extensions;

using Newtonsoft.Json;

namespace assertUpdater.DbModel
{
    public sealed class DbData : ICloneable
    {
        [JsonConstructor]
        public DbData(DbConfig config)
        {
            DatabaseFiles = new List<DbFile>();
            Config = config;
        }

        public IEnumerable<DbFile> DatabaseFiles { get; set; }
        public DbConfig Config { get; set; }
        public static DbData Empty { get; set; } = new DbData(new DbConfig(Path.GetTempPath()));
        public object Clone()
        {
            //_ = new();
            DbData dbData = new((DbConfig)Config.Clone())
            {
                DatabaseFiles = DatabaseFiles.CloneJson()
            };


            return dbData;
        }
    }
}
