using assertUpdater;
using assertUpdater.AddressBuilder;
using assertUpdater.DbModel;
using assertUpdater.StorageProvider;
using assertUpdater.Tests.Mocked;

namespace assertUpdater.Tests
{
    public class DataGenerators
    {

        public DataGenerators()
        {
            Init();
        }

        public IStorageProvider BuildStorageProviderDefault()
        {
            DataGenerators dg = new();
            DbConfig config = dg.GenRandomConfig();
            string dbPath = GeneratorConfig.DbPath;
            IStorageProvider provider = DataManager.BuildDatabase<FileDatabase>(config, dbPath).Result;


            return provider;
        }
        public DataManager BuildDataManagerDefault()
        {
            IStorageProvider provider = BuildStorageProviderDefault();
            var clonedProvider = (IStorageProvider)provider.Clone();
            DataManager dataManager = new CachedDataManager(clonedProvider);
            return dataManager;
        }

        public void Init()
        {
            _ = Directory.CreateDirectory(GeneratorConfig.TestDataPath);
        }
        public void CleanUp()
        {
            Directory.Delete(GeneratorConfig.TestDataPath, true);
            /*     for (int i = 0; i < _cleanUpList.Count; i++)
                 {

                     Directory.Delete(TempPath, true);
                 }*/
        }
        public List<string> GetRandomString(int count = 10)
        {
            List<string> res = new()
            {
            };
            for (int i = 0; i < count; i++)
            {
                res.Add(Path.GetRandomFileName());
            }

            return res;
        }


        public DbFile GenRandomFile()
        {
            MDbFile file = new();
            return file;
        }
        public List<DbFile> GenRandomFiles(int count = 10, int maxDepth = 3)
        {
            GeneratorConfig.MaxDepth = maxDepth;

            List<DbFile> result = new();
            for (int i = 0; i < count; i++)
            {
                DbFile dbFile = GenRandomFile();
                result.Add(dbFile);
            }

            return result;
        }
        /*
        public List<string> GenRandomFiles(string vcs="", int count = 10, int maxDepth = 3)
        {
            var list = new List<string>();

            var rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                var depth=rnd.Next(1,maxDepth);
                if (string.IsNullOrWhiteSpace(vcs))
                {
                    vcs = TestDataPath;
                }
                
                var relativePath = "";
                for (int j = 0; j < depth; j++)
                {
                    relativePath = Path.Join(relativePath, Path.GetRandomFileName());
                    if (j+1==depth)//if the last path, create actual file
                    {
                        using var sw= File.CreateText(Path.Join(vcs,relativePath));
                        sw.WriteLine(Path.GetRandomFileName()); //Just random string
                        sw.Close();
                    }
                    else
                    {
                        Directory.CreateDirectory(Path.Join(vcs, relativePath));
                    }
                    
                   
                    
                }

                list.Add(relativePath);
                
            }
            return list;
        }*/
        public DbSchema GenDbSchema()
        {
            DbSchema schema = new()
            {
                DirList = CreateDirList(),
                FileList = CreateFileList(),

            };

            IEnumerable<string> CreateFileList()
            {

                return GenRandomFiles().Select((file, i) => file.RelativePath);
            }
            IEnumerable<string> CreateDirList()
            {
                int count = 10;
                int maxDepth = 3;
                List<string> result = new();
                Random rnd = new();
                for (int j = 0; j < count; j++)
                {
                    string vcs = GeneratorConfig.TestDataPath;
                    string relativePath = "";

                    int depth = rnd.Next(1, maxDepth);
                    for (int i = 0; i < depth; i++)
                    {

                        relativePath = Path.Join(relativePath, Path.GetRandomFileName());
                        _ = Directory.CreateDirectory(Path.Join(vcs, relativePath));

                    }

                    result.Add(relativePath);
                }

                //gen files
                foreach (string res in result)
                {
                    count = rnd.Next(1, count); //Randomize count
                    for (int i = 0; i < count; i++)
                    {
                        MDbFile dbFile = new(res);


                    }
                }


                return result;
            }
            return schema;
        }
        public DbConfig GenRandomConfig()
        {

            string vcsRoot = GeneratorConfig.TestDataPath;

            DbConfig config = new(vcsRoot)
            {
                DatabaseSchema = GenDbSchema(),
                MajorVersion = 1,
                MinorVersion = 2,
                UpdateUrl = "https://thisisdbupdateurl.net",
                DownloadAddressBuilder = new DefaultAddressBuilder("https://rootdownloadAddress.net", vcsRoot)
            };


            return config;
        }

    }
}
