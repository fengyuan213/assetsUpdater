namespace assetsUpdater.Utils
{
    public static class FileSizeParser
    {
        /// <summary>
        ///  eg, 1TB,1MB,1GB,1KB
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ParseAuto(double bytes)
        {
            double kb = bytes / 1024; // · 1024 Bytes = 1 Kilobyte
            double mb = kb / 1024; // · 1024 Kilobytes = 1 Megabyte
            double gb = mb / 1024; // · 1024 Megabytes = 1 Gigabyte
            double tb = gb / 1024; // · 1024 Gigabytes = 1 Terabyte

            string result =
                tb > 1 ? $"{tb:0.##}TB" :
                gb > 1 ? $"{gb:0.##}GB" :
                mb > 1 ? $"{mb:0.##}MB" :
                kb > 1 ? $"{kb:0.##}KB" :
                $"{bytes:0.##}B";

            result = result.Replace("/", ".");
            return result;
        }

        public static int ParseMb(long bytes)
        {
            double kb = bytes / 1024; // · 1024 Bytes = 1 Kilobyte
            double mb = kb / 1024; // · 1024 Kilobytes = 1 Megabyte
            return (int)mb;
        }

        public static int ParseGb(long bytes)
        {
            double kb = bytes / 1024; // · 1024 Bytes = 1 Kilobyte
            double mb = kb / 1024; // · 1024 Kilobytes = 1 Megabyte
            double gb = mb / 1024; // · 1024 Megabytes = 1 Gigabyte
            return (int)gb;
        }

        public static int ParseKb(long bytes)
        {
            double kb = bytes / 1024; // · 1024 Bytes = 1 Kilobyte
            return (int)kb;
        }
    }
}