using assertUpdaterRefactor;
using assertUpdaterRefactor.StorageProvider;

using assertUpdaterTests.Mocked;

namespace assertUpdaterTests
{
    [TestClass]
    public class DataManagerTests
    {

        private DataGenerators dataGenerators = null!;

        [TestInitialize]
        public void Init()
        {
            dataGenerators = new DataGenerators();
            try
            {
                Directory.Delete(GeneratorConfig.TestDataPath, true);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            dataGenerators.CleanUp();
        }
        [TestMethod]
        public Task IsDataValid_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            DataGenerators dg = new();
            DataManager manager = dg.BuildDataManagerDefault();
            bool allowEmptyValidDb = true;




            // Act
            bool result = manager.IsDataValid(
                allowEmptyValidDb);

            // Assert
            Assert.IsTrue(result);
            return Task.CompletedTask;
        }

        [TestMethod]
        public Task BuildDatabase_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            DataGenerators dg = new();
            _ = dg.GenRandomConfig();

            //bool isBuildUniqueAddress = false;

            // Act

            IStorageProvider result = dg.BuildStorageProviderDefault();

            // Assert
            Assert.IsNotNull(result);
            return Task.CompletedTask;
        }
    }
}
