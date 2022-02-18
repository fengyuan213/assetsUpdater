using assetsUpdater;
using assetsUpdater.AddressBuilder;
using assetsUpdater.Model;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;
using System.Threading.Tasks;

using Telerik.JustMock;

namespace AssertsUpdaterTests
{
    [TestClass]
    public class PackageManagerTests
    {
        private AssertUpgradePackage mockAssertUpgradePackage;
        private string _localRootPath = Path.Join(Path.GetTempPath(), "PackageManagerTests");

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockAssertUpgradePackage = Mock.Create<AssertUpgradePackage>();
        }

        private PackageManager CreateManager()
        {
            return new PackageManager(this.mockAssertUpgradePackage,
                new DefaultAddressBuilder("defautRootAddress", _localRootPath));
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