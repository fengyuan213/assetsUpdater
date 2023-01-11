using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security.Cryptography;

namespace assertUpdaterRefactor.Utils
{
    public class FileUtils
    {
        public static string Sha1File([NotNull] string path)
        {
            try
            {
                using SHA1 hash = SHA1.Create();
                using FileStream stream = new(path, FileMode.Open);
                byte[] hashByte = hash.ComputeHash(stream);
                return BitConverter.ToString(hashByte).Replace("-", "");
            }
            catch (Exception ex)
            {
                throw new CryptographicException("Failed to calculate sha1", ex);
            }
        }

        public static long GetFileSize([NotNull] string path)
        {
            FileInfo fileInfo = new(path);
            return fileInfo.Exists ? fileInfo.Length : 0;
        }

        public static async Task<long> GetFileSizeRemote(string url)
        {
#pragma warning disable SYSLIB0014
            HttpWebRequest request = WebRequest.CreateHttp(url);
#pragma warning restore SYSLIB0014
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:40.0) Gecko/20100101 Firefox/40.1";
            request.Method = "HEAD";
            using WebResponse response = await request.GetResponseAsync().ConfigureAwait(true);
            long length = response.ContentLength;
            return length;
        }

        public static string MakeStandardRelativePath(string relativePath)
        {
            char dirSeparator = Path.DirectorySeparatorChar;
            relativePath = relativePath.Replace('\\', dirSeparator);
            relativePath = relativePath.Replace('/', dirSeparator);
            relativePath = relativePath.TrimStart(dirSeparator).TrimEnd(dirSeparator);
            return relativePath;
        }

        /// <summary>
        ///     May occur Directory not Found Exception
        /// </summary>
        /// <param name="rootFolder"></param>
        /// <param name="directory"></param>
        /// <returns></returns>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public static string[] GetAllFilesInADirectory(string rootFolder, string directory)
        {
            if (string.IsNullOrWhiteSpace(rootFolder))
            {
                throw new ArgumentNullException(nameof(rootFolder));
            }

            if (string.IsNullOrWhiteSpace(directory))
            {
                return Array.Empty<string>();
            }

            string directoryPath = Path.Join(rootFolder, directory);

            if (!Directory.Exists(directoryPath))
            {
                return Array.Empty<string>();
            }

            DirectoryInfo dirInfo = new(directoryPath);
            FileInfo[] fileInfos = dirInfo.GetFiles("*.*", SearchOption.AllDirectories);
            //var a = fileInfos.Select(fileInfo => fileInfo.FullName.Remove(0, Path.GetFullPath(rootFolder).Length + 1)).ToArray();
            string[] a = fileInfos.Select(fileInfo =>
                    MakeStandardRelativePath(fileInfo.FullName.Remove(0, Path.GetFullPath(rootFolder).Length)))
                .ToArray();
            return a;
        }
    }
}
