using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using assertUpdater.Network;
using assertUpdater.Utils;

namespace assertUpdater.Tests
{
    [TestClass]
    public class OperationTests
    {
        DataGenerators dg = new DataGenerators();
        private string testLocalPath5Mb = Path.Join(Path.GetTempPath(), "testDownload5MB.zip");

        [TestInitialize]
        public void Init()
        {
            try
            {
                File.Delete(testLocalPath5Mb);
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception e)
            {
              
            }
        }
        [TestMethod]
        public void TestDownloadOperation()
        {
            var testUrl = "http://ipv4.download.thinkbroadband.com/5MB.zip";
    
            var downloadConfig = new DownloadConfig();
            var downloadOperation = new DownloadOperation(testUrl, testLocalPath5Mb, downloadConfig);

            downloadOperation.Execute().GetAwaiter().GetResult();


            Assert.IsTrue(File.Exists(testLocalPath5Mb), "Downloaded file does not exist");
            Assert.IsTrue(File.ReadAllBytes(testLocalPath5Mb).Length == 5 * 1024 * 1024, "Downloaded file is not 5MB");
            Assert.IsTrue(downloadOperation.Progress == 100, "Downloaded progress is not 100%");
            var shaCalculated = FileUtils.Sha1File(testLocalPath5Mb);
            var shaExcepted = "0CC897BE1958C0F44371A8FF3DDDBC092FF530D0";



            Assert.AreEqual(shaCalculated, shaExcepted);
        }
    }
}
