using assetsUpdater.Model.Network;
using assetsUpdater.Network;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

using Telerik.JustMock;

namespace AssertsUpdaterTests.Network
{
    [TestClass]
    public class AsyncDownloadTests
    {
        private DownloadPackage mockDownloadPackage;
        private WebClient mockWebClient;

        [TestInitialize]
        public void TestInitialize()

        {

            this.mockDownloadPackage = Mock.Create<DownloadPackage>();
            this.mockWebClient = Mock.Create<WebClient>();
        }

        private AsyncDownload CreateAsyncDownload()
        {
            return new AsyncDownload(
                this.mockDownloadPackage, null,
                this.mockWebClient);
        }

        [TestMethod]
        public async Task Wait_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var asyncDownload = this.CreateAsyncDownload();

            // Act
            await asyncDownload.Wait();

            Debugger.Break();
            // Assert

        }

        [TestMethod]
        public async Task Start_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var asyncDownload = this.CreateAsyncDownload();

            // Act
            await asyncDownload.Start();
            Debugger.Break();
            // Assert

        }
    }
}
