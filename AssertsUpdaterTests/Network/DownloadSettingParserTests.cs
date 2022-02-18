using assetsUpdater.Network;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertsUpdaterTests.Network
{
    [TestClass]
    public class DownloadSettingParserTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        private DownloadSettingParser CreateDownloadSettingParser()
        {
            return new DownloadSettingParser();
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var downloadSettingParser = this.CreateDownloadSettingParser();

            // Act

            // Assert
            Assert.Fail();
        }
    }
}