using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace assertUpdater.Utils
{
    public static class AlgorithmUtils
    {
        public static string Md5StringASCII(string data)
        {
            // Use input string to calculate MD5 hash
            using MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(data);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                _ = sb.Append(hashBytes[i].ToString("X2"));
            }

            return sb.ToString();
        }

        public static string UrlEncodeUTF8(string url)
        {
            string result = HttpUtility.UrlEncode(url);

            return result;
        }
    }
}
