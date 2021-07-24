using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AssertsUpdaterTests.StorageProvider;
using Telerik.JustMock;
using assetsUpdater;
using assetsUpdater.AddressBuilder;
using assetsUpdater.Interfaces;
using assetsUpdater.Model.StorageProvider;
using assetsUpdater.StorageProvider;

namespace AssertsUpdaterTests
{
    [TestClass]
    public class StorageProviderHelperTests
    {
        private IStorageProvider mockStorageProvider;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockStorageProvider = Mock.Create<FileDatabase>();
        }

        private StorageProviderHelper CreateStorageProviderHelper()
        {
            return new StorageProviderHelper(
                this.mockStorageProvider);
        }

        [TestMethod]
        public async Task BuildRemoteDatabase_FileDatabase_Cdn8N6NBuilder_ExpectedBehavior()
        {
            // Arrange
            var apiRoot = "api_Root";
            var accessKey = "accessKey";
            var secretKey = "secretKey";
            var storageProviderHelper = this.CreateStorageProviderHelper();
            IAddressBuilder downloadAddressBuilder =new Cdn8N6NAddressBuilder(Directory.GetCurrentDirectory(),apiRoot,accessKey,secretKey);
            var vCFolder = Path.Combine(Utils.Utils.WorkingDir);
            DbConfig config = new DbConfig(vCFolder)
            {
                DatabaseSchema = new DbSchema
                {
                    DirList = new List<string> { "testFiles" }
                },

                MajorVersion = 1,
                MirrorVersion = 2,
                DownloadAddressBuilder = new Cdn8N6NAddressBuilder(Directory.GetCurrentDirectory(),"apiRoot","apiKey","apiScret")
            };
            var isMac = false;
            
          

            var data = new DbData(config);
            var fileCounts = Utils.Utils.GenTestFiles("testFiles", out var fileNames, out var fileHashes,
                out var fileSizeInMbs);

            fileNames = fileNames.ToList();
            fileHashes = fileHashes.ToList();
            fileSizeInMbs = fileSizeInMbs.ToList();

            string exportPath = Path.GetTempFileName();
     
            // Act
            await storageProviderHelper.BuildRemoteDatabase(
                downloadAddressBuilder,
                config,
                exportPath);

            // Assert
            Console.WriteLine(exportPath);
        }
    }
}
