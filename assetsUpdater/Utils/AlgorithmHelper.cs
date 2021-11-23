#region Using

using System.Security.Cryptography;
using System.Text;
using System.Web;

#endregion

namespace assetsUpdater.Utils
{
    public static class AlgorithmHelper
    {
        public static string Md5StringASCII(string data)
        {
            // Use input string to calculate MD5 hash
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.ASCII.GetBytes(data);
                var hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                var sb = new StringBuilder();
                for (var i = 0; i < hashBytes.Length; i++) sb.Append(hashBytes[i].ToString("X2"));
                return sb.ToString();
            }
        }

        public static string UrlEncodeUTF8(string url)
        {
            var result = HttpUtility.UrlEncode(url);

            return result;
        }
    }
}