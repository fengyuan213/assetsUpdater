using assertUpdater.AddressBuilder;
using assertUpdater.Extensions;

using Newtonsoft.Json;

using System.Diagnostics.CodeAnalysis;
using assertUpdater.Utils.JsonConverters;

namespace assertUpdater.DbModel
{

    public class DbConfig : ICloneable
    {
        private string _versionControlFolder = "";

        /// <summary>
        ///     This constructor is used for creating local database
        /// </summary>
        /// <param name="versionControlFolder"></param>
        [System.Text.Json.Serialization.JsonConstructor]
        public DbConfig([NotNull] string versionControlFolder)
        {
            VersionControlFolder = versionControlFolder;
            DatabaseSchema = new DbSchema();
        }

        [NotNull]
        public string VersionControlFolder
        {
            get => _versionControlFolder;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !Directory.Exists(value))
                {
                    throw new ArgumentException("Invalid Version Control Folder", nameof(VersionControlFolder));
                }

                _versionControlFolder = value;
                //TODO:: Clarefity datamanager constructor  Config.DownloadAddressBuilder.LocalRootPath = Config.VersionControlFolder;
                if (DownloadAddressBuilder != null)
                {
                    DownloadAddressBuilder.LocalRootPath = value;
                }

            }
        }


        [NotNull] public DbSchema DatabaseSchema { get; set; }

        /// <summary>
        ///     The major version of the database
        /// </summary>
        [AllowNull]
        public int MajorVersion { get; set; }

        /// <summary>
        ///     The mirror version of the database
        /// </summary>
        [AllowNull]
        public int MinorVersion { get; set; }

        public string UpdateUrl { get; set; } = "";

        [AllowNull] public IAddressBuilder DownloadAddressBuilder { get; set; }

        public object Clone()
        {
            DbConfig dbConfig = new(VersionControlFolder)
            {
                DatabaseSchema = DatabaseSchema.CloneJson(),
                UpdateUrl = UpdateUrl.CloneJson(),
                MajorVersion = MajorVersion,
                MinorVersion = MinorVersion,
                VersionControlFolder = VersionControlFolder.CloneJson()
            };


            JsonSerializerSettings settings = new();
            Type t = DownloadAddressBuilder.GetType();
            settings.Converters.Add(new ConcreteTypeConverter<IAddressBuilder>(t));
            dbConfig.DownloadAddressBuilder = DownloadAddressBuilder.CloneJson(settings);
            return dbConfig;
        }

        public sealed class Builder
        {
            [System.Text.Json.Serialization.JsonIgnore] private readonly Builder _builder = new();

            [System.Text.Json.Serialization.JsonIgnore] private readonly DbConfig _config = new("");

            public Builder SetDatabaseSchema()
            {
                return _builder;
            }
        }
    }


}
