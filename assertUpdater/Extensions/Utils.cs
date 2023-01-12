using System.Runtime.Serialization.Formatters.Binary;

namespace assertUpdater.Extensions
{
    public static class Utils
    {
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
