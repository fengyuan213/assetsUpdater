using assetsUpdater.Utils;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AssertsUpdaterTests.Utils
{
    public static class Utils
    {
        public static string WorkingDir => AppDomain.CurrentDomain.BaseDirectory;

        public static int GenTestFiles(string subDir, out IEnumerable<string> fileNames, out IEnumerable<string> fileHashes, out IEnumerable<int> fileSizeInMbs)
        {
            Random r = new Random();
            var upper = r.Next(10, 20);
            fileNames = new List<string>();
            fileHashes = new List<string>();
            fileSizeInMbs = new List<int>();
            var fileDir = Path.Join(WorkingDir, subDir);

            for (int i = 0; i < upper; i++)
            {
                CreateRandomFile(fileDir, out string fileName, out string fileHash, out int fileSizeMb);
                fileNames = fileNames.Append(fileName);
                fileHashes = fileHashes.Append(fileHash);
                fileSizeInMbs = fileSizeInMbs.Append(fileSizeMb);
            }

            return upper;
        }

        public static void CreateRandomFile(string fileDir, out string fileName, out string fileHash, out int fileSizeInKb)
        {
            fileName = Path.GetRandomFileName();
            var random = new Random();
            Directory.CreateDirectory(fileDir);

            var sizeInKb = random.Next(1 * 1000, 5 * 1000); // Up to many Gb
            var fileRoot = Path.Join(fileDir, fileName);
            using FileStream stream = new FileStream(fileRoot, FileMode.Create);
            using BinaryWriter writer = new BinaryWriter(stream);
            while (writer.BaseStream.Length <= sizeInKb * 1000)
            {
                writer.Write("akjgfcdaudwaaaaaaaaaaaaaaaaaadwadwdawdasdzwsadawdzawsaaaaaaaaaagdwawwvabdo;ucgvab;bfou"); //This could be random. Also, larger strings improve performance obviously
            }
            writer.Close();
            fileHash = FileUtils.Sha1File(fileRoot);
            fileSizeInKb = sizeInKb;
        }
    }
}