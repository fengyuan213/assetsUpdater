#region Using

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;

#endregion

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

        public static async Task<long> GetFileSizeRemote(string url)
        {
            var request = WebRequest.CreateHttp(url);
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:40.0) Gecko/20100101 Firefox/40.1";
            request.Method = "HEAD";
            using var response = await request.GetResponseAsync().ConfigureAwait(true);
            var length = response.ContentLength;
            return length;
        }

        public static string MakeStandardRelativePath(string relativePath)
        {
            var dirSeparator = Path.DirectorySeparatorChar;
            relativePath = relativePath.Replace('\\', dirSeparator);
            relativePath = relativePath.Replace('/', dirSeparator);
            relativePath = relativePath.TrimStart(dirSeparator).TrimEnd(dirSeparator);
            return relativePath;
        }

        /// <summary>
        ///     May occur Directory not Found Exception called when creating database
        /// </summary>
        /// <param name="rootFolder"></param>
        /// <param name="directory"></param>
        /// <returns></returns>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public static string[] GetAllFilesInADirectory(string rootFolder, string directory)
        {
            if (string.IsNullOrWhiteSpace(rootFolder)) throw new ArgumentNullException(nameof(rootFolder));
            if (string.IsNullOrWhiteSpace(directory)) return Array.Empty<string>();
            var directoryPath = Path.Join(rootFolder, directory);
            if (!Directory.Exists(directoryPath))
                return Array.Empty<string>();
            //throw new DirectoryNotFoundException(directoryPath);
            //rootFolder.TrimEnd('/', '\\');
            /*if (rootFolder.Last() == '/' || rootFolder.Last() == '\\')
                rootFolder = rootFolder.Remove(rootFolder.Length - 1);*/
            //if (File.Exists(directoryPath)) return new[] { directory };
            /*string processedFilename;
            processedFilename = file.Replace('\\', Path.DirectorySeparatorChar);
            processedFilename = file.Replace('/', Path.DirectorySeparatorChar);
            */

            var dirInfo = new DirectoryInfo(directoryPath);
            var fileInfos = dirInfo.GetFiles("*.*", SearchOption.AllDirectories);
            //var a = fileInfos.Select(fileInfo => fileInfo.FullName.Remove(0, Path.GetFullPath(rootFolder).Length + 1)).ToArray();

           // var re1s = fileInfos.AsParallel().Select(fileInfo =>
            //        MakeStandardRelativePath(fileInfo.FullName.Remove(0, Path.GetFullPath(rootFolder).Length))).ToArray();
            var res = fileInfos.AsParallel().Select(fileInfo =>
                    fileInfo.FullName.Remove(0, Path.GetFullPath(rootFolder).Length))
                .ToArray();
            return res;
        }
    }
}