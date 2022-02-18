using assetsUpdater.Model.Network;
using assetsUpdater.Network;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Threading.Tasks;

using Telerik.JustMock;

namespace AssertsUpdaterTests.Network
{
    [TestClass]
    public class MPartDownloadTests
    {
        private DownloadPackage mockDownloadPackage;
        private DownloadSetting mockDownloadSetting;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockDownloadPackage = Mock.Create<DownloadPackage>();
            this.mockDownloadSetting = Mock.Create<DownloadSetting>();
        }

        private MPartDownload CreateMPartDownload()
        {
            return new MPartDownload(
                this.mockDownloadPackage,
                this.mockDownloadSetting);
        }

        [TestMethod]
        public async Task Wait_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var mPartDownload = this.CreateMPartDownload();

            // Act
            await mPartDownload.Wait();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public async Task Start_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var mPartDownload = this.CreateMPartDownload();

            // Act
            await mPartDownload.Start();

            // Assert
            Assert.Fail();
        }
    }
}