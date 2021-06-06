using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace assetsUpdater.Utils
{
    public class FileUtils
    {
        public static string Sha1File([NotNull] string path)
        {
            try
            {
                using var hash = SHA1.Create();
                using var stream = new FileStream(path, FileMode.Open);
                var hashByte = hash.ComputeHash(stream);
                return BitConverter.ToString(hashByte).Replace("-", "");
            }
            catch (Exception ex)
            {
                throw new CryptographicException("计算Sha256错误", ex);
            }
        }

        public static long GetFileSize([NotNull] string path)
        {
            var fileInfo = new FileInfo(path);
            if (fileInfo.Exists) return fileInfo.Length;
            return 0;
        }

        public static string[] GetAllFilesInADirectory(string rootFolder, string directory)
        {
            if (string.IsNullOrWhiteSpace(rootFolder)) rootFolder = AppDomain.CurrentDomain.BaseDirectory;
            if (string.IsNullOrWhiteSpace(directory)) return Array.Empty<string>();
            var directoryPath = Path.Combine(rootFolder, directory);

            //rootFolder.TrimEnd('/', '\\');
            /*if (rootFolder.Last() == '/' || rootFolder.Last() == '\\')
                rootFolder = rootFolder.Remove(rootFolder.Length - 1);*/
            if (File.Exists(directoryPath)) return new[] { directory };

            var dirInfo = new DirectoryInfo(directoryPath);
            var fileInfos = dirInfo.GetFiles("*.*", SearchOption.AllDirectories);
            //var a = fileInfos.Select(fileInfo => fileInfo.FullName.Remove(0, Path.GetFullPath(rootFolder).Length + 1)).ToArray();
            var a = fileInfos.Select(fileInfo => fileInfo.FullName.Remove(0, Path.GetFullPath(rootFolder).Length))
                .ToArray();
            return a;
        }
    }
}