#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using assetsUpdater;
using assetsUpdater.Interfaces;
using assetsUpdater.Model.StorageProvider;
using assetsUpdater.StorageProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace AssertsUpdaterTests
{
    [TestClass]
    public class AssertVerifyTests
    {
        private const string Check_UpdateUrl = "http://text.com/testdb.pokedb";

        [TestInitialize]
        public void TestInitialize()
        {
        }


        [TestMethod]
        public void DatabaseCompare_FileConstructor_ExpectedBehavior()
        {
            // Arrange

            var remoteFiles = new List<DatabaseFile>();
            var localFiles = new List<DatabaseFile>();
            var buildInData1 = new DatabaseFile("testfolder/ac/w.a", "ad123456", 1234567, "w.c");
            var buildInData2 = new DatabaseFile("testfolder/ac/w.b", "ba123456", 1234567, "w.c");
            var buildInData3 = new DatabaseFile("testfolder/ac/w.c", "cb123456", 1234567, "w.c");
            var buildInData4 = new DatabaseFile("testfolder/ac/w.d", "dc123456", 1234567, "w.c");
            var buildInData5 = new DatabaseFile("testfolder/ac/w.d", "da123456", 1234567, "w.c");
            //deletedFile
            localFiles.Add(buildInData1);
            localFiles.Add(buildInData2);
            remoteFiles.Add(buildInData2);
            //DifferFile
            localFiles.Add(buildInData4);
            remoteFiles.Add(buildInData5);
            //AddFile
            remoteFiles.Add(buildInData3);

            // Act
            var result = AssertVerify.DatabaseCompare(
                remoteFiles,
                localFiles);

            // Assert
            Assert.AreEqual(buildInData3, result.AddFile.First());
            Assert.IsTrue(result.AddFile.Count() == 1);
            if (result.DeleteFile.Count() != 2 || !result.DeleteFile.Contains(buildInData1) ||
                !result.DeleteFile.Contains(buildInData4)) Assert.Fail("DeleteFileFileTest Failed");
            Assert.IsTrue(result.DifferFile.Count() == 1);
            Assert.AreEqual(buildInData5, result.DifferFile.First());

            Console.WriteLine("Test Succeed");
        }

        [TestMethod]
        public void DatabaseCompare_IStorageProviderConstructor()
        {
            // Arrange

            var remoteFiles = new List<DatabaseFile>();
            var localFiles = new List<DatabaseFile>();
            var buildInData1 = new DatabaseFile("testfolder/ac/w.a", "ad123456", 1234567, "w.c");
            var buildInData2 = new DatabaseFile("testfolder/ac/w.b", "ba123456", 1234567, "w.c");
            var buildInData3 = new DatabaseFile("testfolder/ac/w.c", "cb123456", 1234567, "w.c");
            var buildInData4 = new DatabaseFile("testfolder/ac/w.d", "dc123456", 1234567, "w.c");
            var buildInData5 = new DatabaseFile("testfolder/ac/w.d", "da123456", 1234567, "w.c");
            //deletedFile
            localFiles.Add(buildInData1);
            localFiles.Add(buildInData2);
            remoteFiles.Add(buildInData2);
            //DifferFile
            localFiles.Add(buildInData4);
            remoteFiles.Add(buildInData5);
            //AddFile
            remoteFiles.Add(buildInData3);
            //dbConfig
            var config = new DbConfig(Directory.GetCurrentDirectory());
            config.MirrorVersion = 1;
            config.MajorVersion = 2;


            var localStorageProvider = new FileDatabase { Data = new DbData(config) { DatabaseFiles = localFiles } };
            var remoteStorageProvider = new FileDatabase { Data = new DbData(config) { DatabaseFiles = remoteFiles } };


            // Act
            var result =  AssertVerify.DatabaseCompare(remoteStorageProvider, localStorageProvider).Result;


            // Assert
            Assert.AreEqual(buildInData3, result.AddFile.First());
            Assert.IsTrue(result.AddFile.Count() == 1);
            if (result.DeleteFile.Count() != 2 || !result.DeleteFile.Contains(buildInData1) ||
                !result.DeleteFile.Contains(buildInData4)) Assert.Fail("DeleteFileFileTest Failed");
            Assert.IsTrue(result.DifferFile.Count() == 1);
            Assert.AreEqual(buildInData5, result.DifferFile.First());

            Console.WriteLine("Test Succeed");
        }

        private IStorageProvider GetStorageProvider_Check_Update()
        {
            var dbConfig = new DbConfig(Directory.GetCurrentDirectory())
            {
                MajorVersion = 111111111,
                MirrorVersion = 1212212
            };

            var localProvider = new FileDatabase
            {
                Data = new DbData(dbConfig)
            };


            return localProvider;
        }

        [TestMethod]
        public async void Check_Update_IStorageProvider_InvalidProviderTest()
        {
            var provider = new FileDatabase();


            var dbManager = await AssertVerify.Check_Update(provider, Check_UpdateUrl);

            if (dbManager.remoteDataManager != null) Assert.Fail("DBManager Should return null   ");
        }

        [TestMethod]
        public async void CheckUpdate_DbPath_FakeTest()
        {
            var dbManager = await AssertVerify.Check_Update("ad", Check_UpdateUrl);

            if (dbManager.remoteDataManager != null) Assert.Fail("DBManager Should return null   ");
        }

        [TestMethod]
        public async void CheckUpdate_IStorageProvider_RealTest()
        {
            var provider = GetStorageProvider_Check_Update();
            var remoteDbManager = await AssertVerify.Check_Update(provider, Check_UpdateUrl);

            Assert.IsTrue(await remoteDbManager.remoteDataManager.IsDataValid());

            Assert.IsFalse(remoteDbManager.remoteDataManager.StorageProvider.GetBuildInDbData().Config.MajorVersion ==
                           provider.GetBuildInDbData().Config.MajorVersion);
        }
    }
}