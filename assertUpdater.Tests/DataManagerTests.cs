using assertUpdater;
using assertUpdater.StorageProvider;
using assertUpdater.Tests.TestData;

namespace assertUpdater.Tests
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
        public async Task IsDataValid_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            DataGenerators dg = new();
            DataManager manager = await dg.BuildDataManagerDefault();
            bool allowEmptyValidDb = true;




            // Act
            bool result = manager.IsDataValid(
                allowEmptyValidDb);

            // Assert
            Assert.IsTrue(result);
          
        }

        [TestMethod]
        public async Task BuildDatabase_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            DataGenerators dg = new();
            _ = dg.GenRandomConfig();

            //bool isBuildUniqueAddress = false;

            // Act

            IStorageProvider result = await dg.BuildStorageProviderDefault();

            // Assert
            Assert.IsNotNull(result);
     
        }
    }
}
