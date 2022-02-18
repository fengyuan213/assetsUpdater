using assetsUpdater.Utils;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertsUpdaterTests.Utils
{
    [TestClass]
    public class FileSizeParserTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void ParseAuto_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            double bytes = 0;

            // Act
            var result = FileSizeParser.ParseAuto(
                bytes);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ParseMb_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            long bytes = 0;

            // Act
            var result = FileSizeParser.ParseMb(
                bytes);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ParseGb_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            long bytes = 0;

            // Act
            var result = FileSizeParser.ParseGb(
                bytes);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ParseKb_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            long bytes = 0;

            // Act
            var result = FileSizeParser.ParseKb(
                bytes);

            // Assert
            Assert.Fail();
        }
    }
}