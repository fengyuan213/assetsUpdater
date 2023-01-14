using assertUpdater;
using assertUpdater.DbModel;
using assertUpdater.Tests.Mocked;

namespace assertUpdater.Tests
{
    [TestClass]
    public class AssertVerifierTests
    {
        [TestInitialize]
        public void Init()
        {
            try
            {

                Directory.Delete(GeneratorConfig.TestDataPath, true);
                File.Delete(GeneratorConfig.DbPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [TestMethod]
        public void DatabaseCompare_FileDatabase_ExpectedBehavior()
        {
            // Arrange
            DataGenerators dg = new();
            /*
            var config = dg.GenRandomConfig();
            var dbPath=GeneratorConfig.DbPath;
            var provider = DataManager.BuildDatabase<FileDatabase>(config,false,dbPath).Result;
            
        
            DataManager local = new CachedDataManager((IStorageProvider)provider.Clone());
            DataManager remote = new CachedDataManager((IStorageProvider)provider.Clone());
            */
            DataManager local = dg.BuildDataManagerDefault();
            DataManager remote = (DataManager)local.Clone();
            // pull data out for simplicity
            List<DbFile> localData = local.Data.DatabaseFiles.ToList();
            List<DbFile> remoteData = remote.Data.DatabaseFiles.ToList();

            //localData.CopyTo(tmp);
            //Pre modification Assertion
            Assert.AreNotSame(localData, remoteData);
            CollectionAssert.AreEqual(localData, remoteData);

            //Simulate Deleted File
            DbFile deletedFileA = remoteData[0];
            DbFile deletedFileB = remoteData[1];
            DbFile deletedFileC = remoteData[2];
            remoteData.RemoveRange(0, 3);

            //Simulate Changed File
            DbFile differFileA = remoteData[0];
            DbFile differFileB = remoteData[1];
            DbFile differFileC = remoteData[2];
            remoteData[0].Hash = "ChangedHashA";
            remoteData[1].Hash = "ChangedHashB";
            remoteData[2].Hash = "ChangedHashC";


            //Simulate Added File
            DbFile addFileA = new("newFileA", "newFileHashA", 123 );
            DbFile addFileB = new("newFileB", "newFileHashB", 123);
            DbFile addFileC = new("newFileC", "newFileHashC", 123);
            remoteData.Add(addFileA);
            remoteData.Add(addFileB);
            remoteData.Add(addFileC);


            // push changes to the original list
            local.Data.DatabaseFiles = localData;
            remote.Data.DatabaseFiles = remoteData;


            // Excepted AssertUpgradePackage:
            // Added 3 file
            // Changed 3 file
            // Deleted 3 file

            // Act  w
            AssertUpgradePackage result = AssertVerifier.DatabaseCompare(
                local,
                remote);


            Assert.AreEqual(3, result.AddFile.Count(), "Add File not equal");
            Assert.AreEqual(3, result.DifferFile.Count(), "Differ File not equal");
            Assert.AreEqual(3, result.DeleteFile.Count(), "Deleted File not equal");

            Assert.AreEqual(9, result.Operations.Count(), "Operations Count Does not met");

            Assert.IsTrue(result.DeleteFile.Contains(deletedFileA), $"deleted file does not contain {deletedFileA.RelativePath}");
            Assert.IsTrue(result.DeleteFile.Contains(deletedFileB), $"deleted file does not contain {deletedFileB.RelativePath}");
            Assert.IsTrue(result.DeleteFile.Contains(deletedFileC), $"deleted file does not contain {deletedFileC.RelativePath}");

            Assert.IsTrue(result.DifferFile.Contains(differFileA), $"differ file does not contain {differFileA.RelativePath}");
            Assert.IsTrue(result.DifferFile.Contains(differFileB), $"differ file does not contain {differFileB.RelativePath}");
            Assert.IsTrue(result.DifferFile.Contains(differFileC), $"differ file does not contain {differFileC.RelativePath}");

            Assert.IsTrue(result.AddFile.Contains(addFileA), $"add file does not contain {addFileA.RelativePath}");
            Assert.IsTrue(result.AddFile.Contains(addFileB), $"add file does not contain {addFileB.RelativePath}");
            Assert.IsTrue(result.AddFile.Contains(addFileC), $"add file does not contain {addFileC.RelativePath}");
        }
    }
}
