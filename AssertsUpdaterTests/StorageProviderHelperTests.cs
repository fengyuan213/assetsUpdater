#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using assetsUpdater;
using assetsUpdater.AddressBuilder;
using assetsUpdater.Interfaces;
using assetsUpdater.Model.StorageProvider;
using assetsUpdater.StorageProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

#endregion

namespace AssertsUpdaterTests
{
    [TestClass]
    public class StorageProviderHelperTests
    {
        private IStorageProvider mockStorageProvider;

        [TestInitialize]
        public void TestInitialize()
        {
            mockStorageProvider = Mock.Create<FileDatabase>();
        }

        private DatabaseBuilder CreateStorageProviderHelper()
        {
            return new DatabaseBuilder(
                mockStorageProvider);
        }

        [TestMethod]
        public async Task BuildRemoteDatabase_FileDatabase_Cdn8N6NBuilder_ExpectedBehavior()
        {
            // Arrange
            var apiRoot = "api_Root";
            var accessKey = "accessKey";
            var secretKey = "secretKey";
            var storageProviderHelper = CreateStorageProviderHelper();
            IAddressBuilder downloadAddressBuilder =
                new Cdn8N6NAddressBuilder(Directory.GetCurrentDirectory(), apiRoot, accessKey, secretKey);
            var vCFolder = Path.Join(Utils.Utils.WorkingDir);
            var config = new DbConfig(vCFolder)
            {
                DatabaseSchema = new DbSchema
                {
                    DirList = new List<string> { "testFiles" }
                },

                MajorVersion = 1,
                MirrorVersion = 2,
                DownloadAddressBuilder =
                    new Cdn8N6NAddressBuilder(Directory.GetCurrentDirectory(), "apiRoot", "apiKey", "apiScret")
            };


            var data = new DbData(config);
            var fileCounts = Utils.Utils.GenTestFiles("testFiles", out var fileNames, out var fileHashes,
                out var fileSizeInMbs);

            fileNames = fileNames.ToList();
            fileHashes = fileHashes.ToList();
            fileSizeInMbs = fileSizeInMbs.ToList();

            var exportPath = Path.GetTempFileName();

            // Act
            await storageProviderHelper.BuildDatabaseWithUrl(
                config,
                exportPath);

            // Assert
            Console.WriteLine(exportPath);
        }
    }
}