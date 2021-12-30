using assetsUpdater;
using assetsUpdater.AddressBuilder;
using assetsUpdater.Model.StorageProvider;
using assetsUpdater.StorageProvider;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AssertsUpdaterTests
{


    [TestClass]
    public class RemoteDataManagerTests
    {
        private string _dbPath = "";
        string _dbRemoteUrl = null;
        [TestInitialize]
        public async void TestInitialize()
        {
            // Create & Export database
            var db = new FileDatabase { Data = GetExceptedDbData() };
            var path = Path.GetTempFileName();
            await db.Export(path);
            _dbPath = path;
        }
        private DbData GetExceptedDbData()
        {
            var config = new DbConfig("vcs")
            {
                DatabaseSchema = new DbSchema()
                {
                    DirList = new List<string>()
                    {
                        "dirlist"
                    },
                    FileList = new List<string>()
                    {
                        "fileList"
                    }
                },
                MajorVersion = 1,
                MirrorVersion = 1,
                DownloadAddressBuilder = new Cdn8N6NAddressBuilder(Directory.GetCurrentDirectory(), "apiRoot", "apiKey", "apiScret")

            };
            return new DbData(config)
            {

                DatabaseFiles = new List<DatabaseFile>()
                {
                    new DatabaseFile("Test\\data", "abcdefg", 123, "")
                }
            };
        }
        private RemoteDataManager CreateManager()
        {
            var dbm = new RemoteDataManager(_dbPath);


            return dbm;
        }

        [TestMethod]
        public async Task IsDataValid_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var manager = this.CreateManager();

            // Act
            var result = await manager.IsDataValid();

            // Assert
            Assert.IsTrue(result, "Data Invalid");
        }

        [TestMethod]
        public async void GetStorageProvider_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var manager = this.CreateManager();


            // Act
            var result = await RemoteDataManager.GetStorageProvider(
                _dbRemoteUrl);
            var rdm = new RemoteDataManager(result);


            // Assert
            Assert.IsTrue(await rdm.IsDataValid(), "DataInvalid");
        }
    }
}
