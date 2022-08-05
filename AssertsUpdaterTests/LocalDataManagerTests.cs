using assetsUpdater;
using assetsUpdater.Interfaces;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Threading.Tasks;

using Telerik.JustMock;

namespace AssertsUpdaterTests
{
    [TestClass]
    public class LocalDataManagerTests
    {
        private IDbData _mockDbData;

        [TestInitialize]
        public void TestInitialize()
        {
            this._mockDbData = Mock.Create<IDbData>();
        }

        private LocalDataManager CreateManager()
        {
            return new LocalDataManager(
                this._mockDbData);
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