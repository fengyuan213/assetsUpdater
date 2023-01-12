namespace assertUpdater.Tests.Mocked
{
    public static class GeneratorConfig
    {
        public static int MaxDepth = 3;
        public static string TestDataPath = Path.Join(Path.GetTempPath(), "assertUpdaterUnitTestsFolder");
        public static bool IsCreateDownloadAddress = false;

        public static string DbPath = Path.Join(Path.GetTempPath(), "TESTDB.zip");
    }
}
