using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Telerik.JustMock;
using assetsUpdater;
using assetsUpdater.Interfaces;

namespace AssertsUpdaterTests
{
    [TestClass]
    public class LocalDataManagerTests
    {
        private IStorageProvider mockStorageProvider;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockStorageProvider = Mock.Create<IStorageProvider>();
        }

        private LocalDataManager CreateManager()
        {
            return new LocalDataManager(
                this.mockStorageProvider);
        }

        [TestMethod]
        public async Task IsDataValid_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var manager = this.CreateManager();

            // Act
            var result = await manager.IsDataValid();

            // Assert
            Assert.Fail();
        }
    }
}
