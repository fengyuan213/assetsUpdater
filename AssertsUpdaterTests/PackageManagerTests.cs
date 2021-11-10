using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Telerik.JustMock;
using assetsUpdater;
using assetsUpdater.Model;

namespace AssertsUpdaterTests
{
    [TestClass]
    public class PackageManagerTests
    {
        private AssertUpgradePackage mockAssertUpgradePackage;
        private string _localRootPath = Path.Combine(Path.GetTempPath(),"PackageManagerTests");
        [TestInitialize]
        public void TestInitialize()
        {
            this.mockAssertUpgradePackage = Mock.Create<AssertUpgradePackage>();
        }

        private PackageManager CreateManager()
        {
            return new PackageManager(_localRootPath,this.mockAssertUpgradePackage);
        }

        [TestMethod]
        public async Task Apply_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var manager = this.CreateManager();
            
            // Act
            var result = await manager.Apply();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public async Task Apply_Remote_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var manager = this.CreateManager();

            // Act
            var result = await manager.Apply_Remote();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public async Task Apply_Local_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var manager = this.CreateManager();

            // Act
            await manager.Apply_Local();

            // Assert
            Assert.Fail();
        }
    }
}
