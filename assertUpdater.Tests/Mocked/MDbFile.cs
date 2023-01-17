using assertUpdater.DbModel;
using assertUpdater.Tests.TestData;
using assertUpdater.Utils;
using System.Diagnostics;

namespace assertUpdater.Tests.Mocked
{
    public class MDbFile : DbFile
    {
        /// <summary>
        /// 
        /// Actual Path: Path.Join(GeneratorConfig.TestDataPath,optionalAppendRelativePath)
        /// </summary>
        /// <param name="optionalAppendRelativePath"></param>
        public MDbFile(string optionalAppendRelativePath)
        {
            int maxDepth = GeneratorConfig.MaxDepth;
            string testDataPath = GeneratorConfig.TestDataPath;

            Init(testDataPath, maxDepth, optionalAppendRelativePath);
        }
        public MDbFile()
        {
            int maxDepth = GeneratorConfig.MaxDepth;
            string testDataPath = GeneratorConfig.TestDataPath;

            Init(testDataPath, maxDepth);
        }

        private void Init(string testDataPath, int maxDepth, string optionalAppendRelativePath = "")
        {
            Random rnd = new();
            int depth = rnd.Next(1, maxDepth);

            string relativePath = optionalAppendRelativePath;
            for (int j = 0; j < depth; j++)
            {
                relativePath = Path.Join(relativePath, Path.GetRandomFileName());
                if (j + 1 == depth)//if the last path, create actual file
                {
                    using StreamWriter sw = File.CreateText(Path.Join(testDataPath, relativePath));
                    sw.WriteLine(Path.GetRandomFileName()); //Just random string
                    sw.Close();
                }
                else
                {
                    _ = Directory.CreateDirectory(Path.Join(testDataPath, relativePath));
                }
            }
            Debug.Print($"RelativePath: {relativePath}");
            string fileSha1 = FileUtils.Sha1File(Path.Join(testDataPath, relativePath));
            long fileSize = FileUtils.GetFileSize(Path.Join(testDataPath, relativePath));
            string downloadAddress = "";
            if (GeneratorConfig.IsCreateDownloadAddress)
            {
                downloadAddress = "https://www.example.com/" + relativePath;
            }
            base.Init(relativePath, fileSha1, fileSize);
        }
    }
}
