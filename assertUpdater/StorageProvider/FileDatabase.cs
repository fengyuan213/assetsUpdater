using assertUpdater.AddressBuilder;
using assertUpdater.DbModel;
using assertUpdater.Extensions;
using assertUpdater.Utils;

using Newtonsoft.Json;

using System.IO.Compression;
using assertUpdater.Utils.JsonConverters;

namespace assertUpdater.StorageProvider
{
    public class FileDatabase : IEquatable<DbData>, IStorageProvider
    {
        private string _tmpPath = "";
        private DbData _dbData;

        /*     public DbData DbData
             {
                 set
                 {
                     _dbData = value;
                 }
             }*/

        public string DbPath
        {
            get => _tmpPath;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(value);
                }

                if (!File.Exists(value))
                {
                    Console.Error.WriteLine($"DbPath:{value} were set, however, it does not exist in system");
                    //throw new FileNotFoundException(value);
                }

                _tmpPath = value;

            }
        }




        public FileDatabase(DbData data)
        {
            _dbData = data;
        }
        public FileDatabase(DbConfig config)
        {

            _dbData = new DbData(config);
            // ThreadPool.QueueUserWorkItem(async (o) => { await Create(config).ConfigureAwait(false); });

        }

        public FileDatabase(DbConfig config, string dbPath)
        {

            _dbData = new DbData(config);
            DbPath = dbPath;

            if (!File.Exists(dbPath))
            {
                async void Init()
                {
                    await FlushAsync().ConfigureAwait(false);
                }

                Init();
            }


            // ThreadPool.QueueUserWorkItem(async (o) => { await Create(config).ConfigureAwait(false); });

        }

        /*public FileDatabase(string url,NetworkCredential credentials)
        {
            this._tmpData = tmpData;
            //  [NotNull] string url, NetworkCredential networkCredential = null;
            var arrObj = obj as object[];
            var url = arrObj?[0] as string;
            var networkCredential = arrObj?[1] as NetworkCredential;
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));

            using var webClient = new WebClient
            {
                Credentials = networkCredential ?? CredentialCache.DefaultNetworkCredentials
            };
            await using var stream = new MemoryStream(await webClient.DownloadDataTaskAsync(url).ConfigureAwait(true));
            using var zipArchive = new ZipArchive(stream, ZipArchiveMode.Read);
            Data = await DbDataReader(zipArchive).ConfigureAwait(false);

        }*/
        private FileDatabase(string fileName)

        {
            DbPath = fileName;
            /* ThreadPool.QueueUserWorkItem( (o) =>
            {

                _ = RefreshAsync().Result;
              
            });*/
            _ = RefreshAsync().Result;
            _dbData ??= DbData.Empty;
        }

        public async Task<DbData> RefreshAsync()
        {

            try
            {

                using ZipArchive zipArchive = await GetZipArchive().ConfigureAwait(false);
                await using Stream stream = await GetContentStream().ConfigureAwait(false);
                using StreamReader streamReader = new(stream);
                string content = await streamReader.ReadToEndAsync();
                JsonSerializerSettings settings = GetJsonSerializerSettings();
                

                var tmp = JsonConvert.DeserializeObject<DbData>(content, settings) ??
                          throw new ArgumentNullException(nameof(DbData));
                _dbData = tmp;

                return _dbData;

                Task<ZipArchive> GetZipArchive()
                {
                    if (string.IsNullOrWhiteSpace(DbPath))
                    {
                        throw new FileNotFoundException("Unable to find database file");
                    }

                    ZipArchive archive = ZipFile.Open(DbPath, ZipArchiveMode.Update);
                    return Task.FromResult(archive);
                }

                Task<Stream> GetContentStream()
                {
                    Stream contentStream = zipArchive.GetEntry("data.json")?.Open() ??
                                 throw new IOException("can't open db data.json");

                    return Task.FromResult(contentStream);
                }
                JsonSerializerSettings GetJsonSerializerSettings()
                {

                    JsonSerializerSettings settings = new()
                    {
                        NullValueHandling = NullValueHandling.Include,
                        ObjectCreationHandling = ObjectCreationHandling.Replace,
                        TypeNameHandling = TypeNameHandling.Auto
                    };
                    settings.Converters.Add(new ConcreteTypeConverter<DefaultAddressBuilder, IAddressBuilder>());

                    return settings;
                }
            }
            catch (FileNotFoundException e)
            {
                Console.Error.WriteLine($"Database not found,{e},returning cached data");
                return _dbData;
            }
            catch (InvalidDataException e)
            {
                Console.Error.WriteLine($"Unknown Database format, {e}");
                // AssertVerifier.OnMessageNotify(this, "数据库不合法", e);
                throw;
            }
            catch (IOException e)
            {
                Console.Error.WriteLine($"Failed to read db,{e},returning cached data");
                return _dbData;
            }
        }

        public async Task<DbData> RetrieveAll()
        {
            _ = await RefreshAsync().ConfigureAwait(false);
            return _dbData;
        }


        public Task<DbFile> RetrieveAsync(string filePath)
        {
            IEnumerable<DbFile> dbFile = _dbData.DatabaseFiles.Where((file, i) =>
                FileUtils.MakeStandardRelativePath(file.RelativePath) ==
                            FileUtils.MakeStandardRelativePath(filePath)
                );


            return Task.FromResult(dbFile.First());
        }


        public Task FlushAsync(DbData data)
        {
            _dbData = data;
            return FlushAsync();

        }
        public Task InsertAsync(DbFile dbFile)
        {

            _dbData.DatabaseFiles = _dbData.DatabaseFiles.Append(dbFile);

            return Task.CompletedTask;
        }
        public Task FlushAsync()
        {

            var path = DbPath;

            string json = JsonConvert.SerializeObject(_dbData);
            using (FileStream stream = new(path, FileMode.Create))
            {
                using ZipArchive zipArchive = new(stream, ZipArchiveMode.Create);
                ZipArchiveEntry entry = zipArchive.CreateEntry("data.json");

                using StreamWriter streamWriter = new(entry.Open());
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            return Task.CompletedTask;
        }


        public bool Equals(DbData? other)
        {
            return other == null ? _dbData.DatabaseFiles.Count() <= 1 : other.ObjectsAreEqual(_dbData);
        }
        /// <summary>
        ///     Clones this object
        /// </summary>
        /// <returns></returns>


        public object Clone()
        {

            return new FileDatabase(new DbData((DbConfig)_dbData.Config.Clone())
            {
                DatabaseFiles = _dbData.DatabaseFiles.CloneJson()
            })
            {
                DbPath = DbPath
            };

        }





    }

}