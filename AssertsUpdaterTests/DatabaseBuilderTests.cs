#region Using

using assetsUpdater;
using assetsUpdater.AddressBuilder;
using assetsUpdater.EventArgs;
using assetsUpdater.Interfaces;
using assetsUpdater.Model.StorageProvider;
using assetsUpdater.StorageProvider;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Telerik.JustMock;

#endregion

namespace AssertsUpdaterTests
{
    [TestClass]
    public class DatabaseBuilderTests
    {
        private IStorageProvider mockStorageProvider;
        private const string testDir = "testFiles";
        private const string testDirWithRoot = "/rootDirTest";
        private const string testSFDir = "singleFile";
        private const string testSFDirWithRoot = "/root/rootedSingleFile";

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
            var databaseBuilder = CreateStorageProviderHelper();
            IAddressBuilder downloadAddressBuilder =
                new Cdn8N6NAddressBuilder(Directory.GetCurrentDirectory(), apiRoot, accessKey, secretKey);
            var vCFolder = Path.Join(Utils.Utils.WorkingDir);

            var config = new DbConfig(vCFolder)
            {
                DatabaseSchema = new DbSchema
                {
                    DirList = new List<string> { testDir, testDirWithRoot },
                    FileList = new List<string>()
                },

                MajorVersion = 1,
                MinorVersion = 2,
                DownloadAddressBuilder =
                    new Cdn8N6NAddressBuilder(Directory.GetCurrentDirectory(), "apiRoot", "apiKey", "apiScret")
            };

            var data = new DbData(config);
            var fileCounts = Utils.Utils.GenTestFiles(testDir, out var fileNames, out var fileHashes,
                out var fileSizeInMbs);

            fileNames = fileNames.ToList();
            fileHashes = fileHashes.ToList();
            fileSizeInMbs = fileSizeInMbs.ToList();

            var rootTestFileCounts = Utils.Utils.GenTestFiles(testDirWithRoot.Substring(1), out var rootTestFileNames,
                out var rootTestFileHashes,
                out var rootTestFileSizeInMbs);
            config.DatabaseSchema.FileList =
                config.DatabaseSchema.FileList.Append(Path.Join(testDir, fileNames.FirstOrDefault()));
            config.DatabaseSchema.FileList =
                config.DatabaseSchema.FileList.Append(Path.Join(testSFDir, fileNames.FirstOrDefault()));
            config.DatabaseSchema.FileList =
                config.DatabaseSchema.FileList.Append(Path.Join(testSFDirWithRoot, fileNames.FirstOrDefault()));
            //prepare environment
            var a = Path.Join(vCFolder, testDir);
            var b = Path.Join(vCFolder, testSFDir);
            var c = Path.Join(vCFolder, testSFDirWithRoot);
            if (!Directory.Exists(a)) Directory.CreateDirectory(a);
            if (!Directory.Exists(b)) Directory.CreateDirectory(b);
            if (!Directory.Exists(c)) Directory.CreateDirectory(c);

            File.Copy(Path.Join(vCFolder, testDir, fileNames.FirstOrDefault()),
                Path.Join(vCFolder, testSFDirWithRoot, fileNames.FirstOrDefault()), true);
            File.Copy(Path.Join(vCFolder, testDir, fileNames.FirstOrDefault()),
                Path.Join(vCFolder, testSFDir, fileNames.FirstOrDefault()), true);
            var exportPath = Path.GetTempFileName();
            Console.WriteLine(exportPath);
            // Act
            AssertVerify.MessageNotify += AssertVerify_MessageNotify;
            await databaseBuilder.BuildDatabaseWithUrl(
                config,
                exportPath);

            File.Copy(exportPath, Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "rdb.zip"),
                true);

            // Assert
            var db = new FileDatabase(exportPath);
            Assert.IsTrue(db.IsValidDb(true));
            Assert.IsFalse(string.IsNullOrWhiteSpace(db.Data.DatabaseFiles.ToArray()[0].DownloadAddress));

            Assert.IsTrue(db.Data.DatabaseFiles.Select((file, i) => file.RelativePath)
                .Contains(Path.Join("testFiles".Replace('/', '\\'), fileNames.FirstOrDefault())));
            Assert.IsTrue(db.Data.DatabaseFiles.Select((file, i) => file.RelativePath)
                .Contains(Path.Join(testSFDirWithRoot.Replace('/', '\\'), fileNames.FirstOrDefault())));
            Assert.IsTrue(db.Data.DatabaseFiles.Select((file, i) => file.RelativePath)
                .Contains(Path.Join(testSFDir.Replace('/', '\\'), fileNames.FirstOrDefault())));

            Assert.Inconclusive("请详细查看数据库:{0}", exportPath);
        }

        [TestCleanup]
        private void CleanUp()
        {
            try
            {
                Directory.Delete(testSFDir, true);
                Directory.Delete(testSFDirWithRoot, true);
                Directory.Delete(testDir, true);
                Directory.Delete(testDirWithRoot, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void AssertVerify_MessageNotify(object sender, MessageNotifyEventArgs e)
        {
            Console.WriteLine("Message:{0},e:{1}", e.Message, e.Exception.Message);
        }
    }
}