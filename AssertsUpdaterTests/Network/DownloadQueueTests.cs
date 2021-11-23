using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Telerik.JustMock;
using assetsUpdater.Network;
using System.Collections.Generic;
using assetsUpdater.Interfaces;

namespace AssertsUpdaterTests.Network
{
    [TestClass]
    public class DownloadQueueTests
    {


        [TestInitialize]
        public void TestInitialize()
        {

        }

        private DownloadQueue CreateDownloadQueue()
        {
            return new DownloadQueue();
        }

        [TestMethod]
        public async Task WaitAll_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var downloadQueue = this.CreateDownloadQueue();

            // Act
            await downloadQueue.WaitAll();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public async Task QueueDownload_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var downloadQueue = this.CreateDownloadQueue();
            IEnumerable<IDownloadUnit> resultes = null;

            // Act
       
            await downloadQueue.QueueDownload(
                resultes);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public async Task QueueDownload_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var downloadQueue = this.CreateDownloadQueue();
            IDownloadUnit downloadUnit = null;

            // Act
            await downloadQueue.QueueDownload(
                downloadUnit);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public async Task RestartFailedDownload_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var downloadQueue = this.CreateDownloadQueue();
            IDownloadUnit downloadUnit = null;

            // Act
            await downloadQueue.RestartFailedDownload(
                downloadUnit);

            // Assert
            Assert.Fail();
        }
    }
}
