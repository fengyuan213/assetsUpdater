using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Telerik.JustMock;
using assetsUpdater.AddressBuilder;

namespace AssertsUpdaterTests.AddressBuilder
{
    [TestClass]
    public class Cdn8N6NAddressBuilderTests
    {


        [TestInitialize]
        public void TestInitialize()
        {

        }

        private Cdn8N6NAddressBuilder CreateCdn8N6NAddressBuilder()
        {
            return new Cdn8N6NAddressBuilder(
                TODO,
                TODO,
                TODO,
                TODO);
        }

        [TestMethod]
        public void BuildUri_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var cdn8N6NAddressBuilder = this.CreateCdn8N6NAddressBuilder();
            string relativePath = null;

            // Act
            var result = cdn8N6NAddressBuilder.BuildUri(
                relativePath);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void BuildDownloadLocalPath_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var cdn8N6NAddressBuilder = this.CreateCdn8N6NAddressBuilder();
            string relativePath = null;

            // Act
            var result = cdn8N6NAddressBuilder.BuildDownloadLocalPath(
                relativePath);

            // Assert
            Assert.Fail();
        }
    }
}
