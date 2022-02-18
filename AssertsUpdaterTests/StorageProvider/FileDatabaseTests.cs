﻿#region Using

using assetsUpdater.AddressBuilder;
using assetsUpdater.Model.StorageProvider;
using assetsUpdater.StorageProvider;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

#endregion

namespace AssertsUpdaterTests.StorageProvider
{
    [TestClass]
    public class FileDatabaseTests
    {
        public static DbData FileDatabaseData { get; set; }
        public static DbConfig FileDatabaseConfig { get; set; }
        public int FileCount { get; set; }
        public IEnumerable<string> FileNames { get; set; }
        public IEnumerable<string> FileHashes { get; set; }
        public IEnumerable<int> FileSizeInKbs { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            var isMac = false;
            var vCFolder = Path.Join(Utils.Utils.WorkingDir);
            if (isMac) vCFolder = "/Users/fengyuan/Data/1.16.5虚无3 现代整合（JAVA8版本虚无更新到beta3/ 1.16.5虚无3/";

            FileDatabaseConfig = new DbConfig(vCFolder)
            {
                DatabaseSchema = new DbSchema
                {
                    DirList = new List<string> { "testFiles" },
                    FileList = new List<string>() { "FileItem1" }
                },

                MajorVersion = 1,
                MinorVersion = 2,
                DownloadAddressBuilder =
                    new Cdn8N6NAddressBuilder(Directory.GetCurrentDirectory(), "apiroot", "apikey", "secret")
            };

            FileDatabaseData = new DbData(FileDatabaseConfig);
            FileCount = Utils.Utils.GenTestFiles("testFiles", out var fileNames, out var fileHashes,
                out var fileSizeInMbs);

            FileNames = fileNames.ToList();
            FileHashes = fileHashes.ToList();
            FileSizeInKbs = fileSizeInMbs.ToList();
        }

        [TestCleanup]
        public void CleanUp()
        {
            try
            {
                Directory.Delete(Utils.Utils.WorkingDir, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private FileDatabase CreateFileDatabase()
        {
            return new FileDatabase { Data = FileDatabaseData };
        }

        private FileDatabase CreateFileDatabase(DbData dbData)
        {
            return new FileDatabase { Data = dbData };
        }

        [TestMethod]
        public async Task Export()
        {
            var exceptedFileDatafilesDatabase = GetExceptedFileDatabase_ForDownload();

            await exceptedFileDatafilesDatabase
                .Export(Path.Join(Directory.GetCurrentDirectory(), "expectedFileDatabase.zip")).ConfigureAwait(true);

            Assert.IsTrue(true);
        }

        private DbData GetExceptedDbData()
        {
            var config = new DbConfig("vcs")
            {
                DatabaseSchema = new DbSchema
                {
                    DirList = new List<string> { "testFiles" },
                    FileList = new List<string>() { "FileItem1" }
                },
                MajorVersion = 1,
                MinorVersion = 1,
                DownloadAddressBuilder =
                    new Cdn8N6NAddressBuilder(Directory.GetCurrentDirectory(), "apiRoot", "apiKey", "apiScret")
            };
            return new DbData(config)
            {
                DatabaseFiles = new List<DatabaseFile>
                {
                    new("Test\\data", "abcdefg", 123, "")
                }
            };
        }

        private FileDatabase GetExceptedFileDatabase_ForDownload()
        {
            var exceptedFileDatafilesDatabase = CreateFileDatabase(GetExceptedDbData());
            return exceptedFileDatafilesDatabase;
        }

        [TestMethod]
        public async Task Download_StateUnderTest_DatabaseAreSame()
        {
            // Arrange
            var fileDatabase = CreateFileDatabase();

            var exceptedFileDatafilesDatabase = GetExceptedFileDatabase_ForDownload();

            var url = "https://files.catbox.moe/yil2l6.zip";

            var obj = new List<object>(2) { url, new NetworkCredential() };

            // Act

            await fileDatabase.Download(obj).ConfigureAwait(true);

            // Assert

            Assert.IsTrue(fileDatabase.Equals(exceptedFileDatafilesDatabase));
        }

        [TestMethod]
        public async Task Read_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var fileDatabase = CreateFileDatabase();
            var exceptedFileDatabase = GetExceptedFileDatabase_ForDownload();
            var path = Path.Join(Path.GetTempPath(), Path.GetRandomFileName());

            await exceptedFileDatabase.Export(path).ConfigureAwait(true);

            // Act
            await fileDatabase.Read(path).ConfigureAwait(true);

            // Assert
            Assert.IsTrue(fileDatabase.Equals(exceptedFileDatabase));
        }

        [TestMethod]
        public async Task Create_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var fileDatabase = CreateFileDatabase();
            var config = FileDatabaseConfig;

            // Act
            await fileDatabase.Create(
                config).ConfigureAwait(true);

            // Assert
            Assert.AreEqual(fileDatabase.Data.DatabaseFiles.Count(), FileCount);
            /*for (var i = 0; i < FileHashes.Count(); i++)
                if (fileDatabase.Data.DatabaseFiles.ToList()[0].Hash == FileHashes.ToList()[i]) return;
                else if (i == FileHashes.Count()) Assert.Fail("没有找到匹配的database hash");*/

            if (!Match(fileDatabase.Data.DatabaseFiles.ToList()[0].Hash, FileHashes))
                Assert.Fail("没有找到匹配的database hash");
            if (!Match(fileDatabase.Data.DatabaseFiles.ToList()[0].FileName, FileNames))
                Assert.Fail("没有找到匹配的database FileNames");
            if (!Match(fileDatabase.Data.DatabaseFiles.ToList()[0].FileSize, FileSizeInKbs))
                Assert.Fail("没有找到匹配的database FileSize");

            static bool Match<T>(object source, T data)
            {
                var o2 = ((IEnumerable)data).OfType<object>().ToList();
                //var o2 = ((IEnumerable<object>) data).ToList();

                for (var i = 0; i < o2.Count; i++)
                    if (source == o2[i]) break;
                    else if (i == o2.Count) return false;
                return true;
            }

            //Assert.AreEqual(fileDatabase.Data.DatabaseFiles.ToList()[0].FileSize, FileSizeInKbs.ToList()[0]);
            //Assert.AreEqual(fileDatabase.Data.DatabaseFiles.ToList()[0].FileName, FileNames.ToList()[0]);
        }

        [TestMethod]
        public void Equals_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var fileDatabaseA = CreateFileDatabase(GetExceptedDbData());
            var fileDatabaseB = CreateFileDatabase(GetExceptedDbData());

            // Act
            var result = fileDatabaseA.Equals(fileDatabaseB);

            // Assert

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ConvertToDictionary_StateUnderTest_AreSameFileCount()
        {
            // Arrange
            var fileDatabase = CreateFileDatabase(GetExceptedDbData());

            // Act
            var result = fileDatabase.ConvertToDictionary();

            // Assert
            Assert.AreEqual(result.Count, GetExceptedDbData().DatabaseFiles.Count());
            //Assert.AreSame(,result);
        }

        [TestMethod]
        public void GetBuildInDbData_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var fileDatabase = CreateFileDatabase();

            // Act
            var result = fileDatabase.GetBuildInDbData();

            // Assert
            Assert.AreEqual(result.Config, FileDatabaseConfig);
        }
    }
}