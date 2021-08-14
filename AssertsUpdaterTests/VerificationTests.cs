using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock;
using assetsUpdater;
using assetsUpdater.Model;
using assetsUpdater.Model.StorageProvider;

namespace AssertsUpdaterTests
{
    [TestClass]
    public class VerificationTests
    {


        [TestInitialize]
        public void TestInitialize()
        {

        }

        private AssertVerify CreateVerification()
        {
            return new AssertVerify();
        }

        [TestMethod]
        public void DatabaseCompare_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var verification = this.CreateVerification();
            List<DatabaseFile> remoteFiles = new List<DatabaseFile>();
            List<DatabaseFile> localFiles = new List<DatabaseFile>();
            var buildInData1 = new DatabaseFile("testfolder/ac/w.a","ad123456",1234567,"w.c");

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
            var result = verification.DatabaseCompare(
                remoteFiles,
                localFiles);
            
            
            Assert.AreEqual(buildInData3,result.AddFile.First());
            Assert.IsTrue(result.AddFile.Count()==1);
            if (result.DeleteFile.Count()!=2|| !result.DeleteFile.Contains(buildInData1)||! result.DeleteFile.Contains(buildInData4))
            {
                Assert.Fail("DeleteFileFileTest Failed");
            }
            Assert.IsTrue(result.DifferFile.Count()==1);
            Assert.AreEqual(buildInData5,result.DifferFile.First());
            // Assert
            Console.WriteLine("Test Succeed");
        }

 
    }
}
