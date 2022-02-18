using assetsUpdater.Interfaces;
using assetsUpdater.Utils;

using Newtonsoft.Json;

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace assetsUpdater.Model.StorageProvider
{
    public partial class DbConfig
    {
    }

    public partial class DbConfig : ICloneable
    {
        private string? _versionControlFolder = null;

        /// <summary>
        /// This constructor is used for creating local database
        /// </summary>
        /// <param name="versionControlFolder"></param>
        [JsonConstructor]
        public DbConfig([NotNull] string versionControlFolder)
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的 属性“UpdateUrl”必须包含非 null 值。请考虑将 属性 声明为可以为 null。
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
                    _versionControlFolder = null;
                    throw new ArgumentException("Invalid Version Control Folder", nameof(VersionControlFolder));
                }
                else
                {
                    _versionControlFolder = value;
                }
            }
        }

        [NotNull] public DbSchema DatabaseSchema { get; set; }

        /// <summary>
        ///     The major version of the database
        /// </summary>
        [AllowNull] public int MajorVersion { get; set; }

        /// <summary>
        ///     The mirror version of the database
        /// </summary>
        [AllowNull] public int MinorVersion { get; set; }

        public string UpdateUrl { get; set; }

        [AllowNull] public IAddressBuilder DownloadAddressBuilder { get; set; }

        public sealed class Builder
        {
            public Builder()
            {
            }

            [JsonIgnore]
            private DbConfig.Builder _builder = new Builder();

            [JsonIgnore]
            private DbConfig _config = new DbConfig("");

            public DbConfig.Builder SetDatabaseSchema()
            {
                return _builder;
            }
        }

        public object Clone()
        {
            var dbConfig = new DbConfig(VersionControlFolder);
            dbConfig.DatabaseSchema = DatabaseSchema.CloneJson();
            dbConfig.UpdateUrl = UpdateUrl.CloneJson() ?? "null";
            dbConfig.MajorVersion = MajorVersion;
            dbConfig.MinorVersion = MinorVersion;
            dbConfig.VersionControlFolder = VersionControlFolder.CloneJson() ?? "null";

            var settings = new JsonSerializerSettings();
            var t = DownloadAddressBuilder.GetType();
            settings.Converters.Add(new ConcreteTypeConverter<IAddressBuilder>(t));
            dbConfig.DownloadAddressBuilder = DownloadAddressBuilder.CloneJson(settings);
            return dbConfig;
        }
    }
}