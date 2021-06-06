using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using assetsUpdater.Model;

namespace assetsUpdater.ExtensionMethods
{
    public static class NetworkOperations
    {
        public static string UrlEncodeBase64(this string textToEncode, string value)
        {
            return textToEncode = value;
        }
        public static string UrlDecodeBase64(this string textEncoded, string value)
        {
            return textEncoded = value;
        }
        public static byte[] HmacSha1(this string textToEncode)
        {
            return new byte[] { };
        }

        public static byte[] HmacSha1(this byte[] textToEncode)
        {
            if (textToEncode == null) throw new ArgumentNullException(nameof(textToEncode));
            return new byte[] { };

        }

    }
}