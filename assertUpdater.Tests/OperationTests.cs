using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using assertUpdater.DbModel;
using assertUpdater.Network;
using assertUpdater.Operations;
using assertUpdater.Tests.TestData;
using assertUpdater.Utils;

namespace assertUpdater.Tests
{
    [TestClass]
    public class OperationTests
    {
        DataGenerators dg = new DataGenerators();

        static  RemoteDbFile _remoteDbFile = StaticTestData.GetTestRemoteDbFile();
        string  _localPath = _remoteDbFile.AddressBuilder.BuildDownloadLocalPath(_remoteDbFile.RelativePath);
        [TestInitialize]
        public void Init()
        {
            try
            {
                Directory.Delete(_remoteDbFile.AddressBuilder.LocalRootPath, true);
              
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception e)
            {
              
            }
            Directory.CreateDirectory(_remoteDbFile.AddressBuilder.LocalRootPath);
        }
        [TestMethod]
        public async Task TestDownloadOperation()
        {
           

            var downloadOperation = _remoteDbFile.BuildDownloadOperation();


            await downloadOperation.Execute();


          
      
            Assert.IsTrue(File.Exists(_localPath), "Downloaded file does not exist");
            Assert.IsTrue(File.ReadAllBytes(_localPath).Length == _remoteDbFile.FileSize, "Downloaded file is not 5MB");
            Assert.AreEqual(FileUtils.Sha1File(_localPath), _remoteDbFile.Hash);

            Assert.IsTrue(downloadOperation.Progress == 100, "Downloaded progress is not 100%");
        }

        [TestMethod]
        public async Task TestDifferFileOperation()
        {
            var rootPath = _remoteDbFile.AddressBuilder.LocalRootPath;
            var sw = File.Create(_localPath);
            sw.Write(Encoding.UTF8.GetBytes("Hello World"));
            sw.Flush();
            sw.Close();
            var differOperation = new DifferFileAssertOperation(rootPath, _remoteDbFile);


            Assert.IsTrue(File.Exists(_localPath),"Test file does not exist");
            await differOperation.Execute().ConfigureAwait(false);

            Assert.IsTrue(File.Exists(_localPath), "Downloaded file does not exist");
            Assert.IsTrue(File.ReadAllBytes(_localPath).Length == _remoteDbFile.FileSize, "Downloaded file is not 5MB");
            Assert.AreEqual(FileUtils.Sha1File(_localPath), _remoteDbFile.Hash);



        }

        [TestMethod]
        public async Task TestDeleteFileOperation()
        {
            var dg = new DataGenerators();
            var file=dg.GenRandomFile();
            var deleteOperation = new DeleteFileAssertOperation(GeneratorConfig.TestDataPath, file);
            var absPath = Path.Join(GeneratorConfig.TestDataPath, file.RelativePath);

           
            Assert.IsTrue(File.Exists(absPath), "File does not exist");
      
         
         
            await deleteOperation.Execute();

            Assert.IsFalse(File.Exists(absPath), "File  exist, Delete operation failed to complete");

        }


        [TestMethod]
        public async Task TestAddFileOperation()
        {

           
            var addFileOperation = new AddFileAssertOperation(_remoteDbFile);

            var localRoot = _remoteDbFile.AddressBuilder.LocalRootPath;

            try
            {

               await  addFileOperation.Execute();
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                throw;
            }

            Assert.IsTrue(File.Exists(_localPath), "Downloaded file does not exist");
            Assert.IsTrue(File.ReadAllBytes(_localPath).Length == _remoteDbFile.FileSize, "Downloaded file is not 5MB");
            Assert.AreEqual(FileUtils.Sha1File(_localPath), _remoteDbFile.Hash);
    
            //Check Directory
            var dirs=  Directory.EnumerateDirectories(localRoot);
            var files= Directory.EnumerateFiles(localRoot);
            Assert.IsTrue(!dirs.Any());
            Assert.IsTrue( files.Count()==1);
        }
    }
}
