using assetsUpdater;
using assetsUpdater.AddressBuilder;
using assetsUpdater.Interfaces;
using assetsUpdater.Model.StorageProvider;
using assetsUpdater.StorageProvider;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Telerik.JustMock;

namespace AssertsUpdaterTests
{
    [TestClass]
    public class DatabaseBuilderTests
    {
        private IStorageProvider mockStorageProvider;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockStorageProvider = Mock.Create<IStorageProvider>();
        }

        private DatabaseBuilder CreateDatabaseBuilder()
        {
            return new DatabaseBuilder(
                this.mockStorageProvider);
        }

        [TestMethod]
        public async Task BuildDatabase_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var databaseBuilder = this.CreateDatabaseBuilder();
            DbConfig config = GetDbConfig();
            string exportPath = Path.GetTempFileName();

            // Act
            await databaseBuilder.BuildDatabase(
                config,
                exportPath);
            // Assert
            var db = new FileDatabase(exportPath);
            Assert.IsTrue(db.IsValidDb());
            Assert.IsTrue(string.IsNullOrWhiteSpace(db.Data.DatabaseFiles.ToArray()[0].DownloadAddress));

        }

        [TestMethod]
        public async Task BuildDatabaseWithUrl_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var databaseBuilder = this.CreateDatabaseBuilder();
            DbConfig config = GetDbConfig();
            string exportPath = Path.GetTempFileName();

            // Act
            await databaseBuilder.BuildDatabaseWithUrl(
                config,
                exportPath);

            // Assert
            var db = new FileDatabase(exportPath);
            Assert.IsTrue(db.IsValidDb());
            Assert.IsFalse(string.IsNullOrWhiteSpace(db.Data.DatabaseFiles.ToArray()[0].DownloadAddress));

        }

        private DbConfig GetDbConfig()
        {
            var tmpPath = Path.GetTempPath();
            var config = new DbConfig(tmpPath)
            {
                MirrorVersion = 0,
                MajorVersion = 1,
                VersionControlFolder = tmpPath,
                DatabaseSchema = new DbSchema() { DirList = new List<string>() },
                DownloadAddressBuilder = GetAddressBuilder()
            };



            return config;

        }

        private IAddressBuilder GetAddressBuilder()
        {
            var downloadLocalRoot = Path.Join(Path.GetTempPath(), "LocalDownloadRoot");
            const string apiRoot = "http://downloads.pokecity.club";
            const string apiKey = "apiKey";
            const string apiSecret = "apiSecret";

            return new Cdn8N6NAddressBuilder(downloadLocalRoot, apiRoot, apiKey, apiSecret);

        }
    }
}
