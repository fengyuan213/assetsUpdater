using System.Runtime.Serialization.Formatters.Binary;

namespace assertUpdater.Extensions
{
    public static class Utils
    {
        public static async Task CopyToAsync(this Stream source, Stream destination, int bufferSize, IProgress<long> progress = null, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (!source.CanRead)
                throw new ArgumentException("Has to be readable", nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (!destination.CanWrite)
                throw new ArgumentException("Has to be writable", nameof(destination));
            if (bufferSize < 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            var buffer = new byte[bufferSize];
            long totalBytesRead = 0;
            int bytesRead;
            while ((bytesRead = await source.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) != 0)
            {
                await destination.WriteAsync(buffer, 0, bytesRead, cancellationToken).ConfigureAwait(false);
                totalBytesRead += bytesRead;
                progress?.Report(totalBytesRead);
            }
        }
        [Obsolete("Obsoleted BinaryFormatter")]
        public static T DeepClone<T>(this T obj)
        {
            object? objResult = null;

            using (MemoryStream ms = new())
            {
                BinaryFormatter bf = new();
                bf.Serialize(ms, obj ?? throw new ArgumentNullException(nameof(obj)));

                ms.Position = 0;
                objResult = bf.Deserialize(ms);
            }

            return (T)objResult;
        }
    }
}
