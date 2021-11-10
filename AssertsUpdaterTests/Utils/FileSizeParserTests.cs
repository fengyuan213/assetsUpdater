using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Telerik.JustMock;
using assetsUpdater.Utils;

namespace AssertsUpdaterTests.Utils
{
    [TestClass]
    public class FileSizeParserTests
    {


        [TestInitialize]
        public void TestInitialize()
        {

        }

        private FileSizeParser CreateFileSizeParser()
        {
            return new FileSizeParser();
        }

        [TestMethod]
        public void ParseAuto_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var fileSizeParser = this.CreateFileSizeParser();
            double bytes = 0;

            // Act
            var result = fileSizeParser.ParseAuto(
                bytes);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ParseMb_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var fileSizeParser = this.CreateFileSizeParser();
            long bytes = 0;

            // Act
            var result = fileSizeParser.ParseMb(
                bytes);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ParseGb_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var fileSizeParser = this.CreateFileSizeParser();
            long bytes = 0;

            // Act
            var result = fileSizeParser.ParseGb(
                bytes);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ParseKb_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var fileSizeParser = this.CreateFileSizeParser();
            long bytes = 0;

            // Act
            var result = fileSizeParser.ParseKb(
                bytes);

            // Assert
            Assert.Fail();
        }
    }
}
