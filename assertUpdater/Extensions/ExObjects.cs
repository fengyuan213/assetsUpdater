using Newtonsoft.Json;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace assertUpdater.Extensions
{
    public static class ExObjects
    {
        public static bool ObjectsAreEqual<T>(this T obj1, T obj2)
        {
            string obj1Serialized = JsonConvert.SerializeObject(obj1);
            string obj2Serialized = JsonConvert.SerializeObject(obj2);

            return obj1Serialized == obj2Serialized;
        }

        /// <summary>
        ///     Reference Article http://www.codeproject.com/KB/tips/SerializedObjectCloner.aspx
        ///     Provides a method for performing a deep copy of an object.
        ///     Binary Serialization is used to perform the copy.
        /// </summary>
        /// <summary>
        ///     Execute a deep Copy of the object, using Json as a serialization method. NOTE: Private members are not cloned using
        ///     this method.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <param name="settings"></param>
        /// <returns>The copied object.</returns>
        public static T CloneJson<T>(this T source, JsonSerializerSettings? settings = null)
        {
            // Don't serialize a null object, simply return the default for that object
            if (source is null)
            {
                return default!;
            }

            // initialize inner objects individually
            // for example in default constructor some list property initialized with some values,
            // but in 'source' these items are cleaned -
            // without ObjectCreationHandling.Replace default constructor values will be added to result
            settings ??= new JsonSerializerSettings();
            settings.ObjectCreationHandling = ObjectCreationHandling.Replace;
            _ = source.GetType();
            string value = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(value, settings) ??
                   throw new SerializationException($"Unable to clone {typeof(T)}");
        }

        /// <summary>
        ///     Execute a deep copy of the object via serialization.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>A deep copy of the object.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static T CloneBinary<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", nameof(source));
            }


            // Don't serialize a null object, simply return the default for that object
            if (source is null)
            {
                return default!;
            }

            using MemoryStream stream = new();
            IFormatter formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011 // 类型或成员已过时
            formatter.Serialize(stream, source ?? throw new ArgumentNullException(nameof(source)));
#pragma warning restore SYSLIB0011 // 类型或成员已过时
            _ = stream.Seek(0, SeekOrigin.Begin);
#pragma warning disable SYSLIB0011 // 类型或成员已过时
            return (T)formatter.Deserialize(stream);
#pragma warning restore SYSLIB0011 // 类型或成员已过时
        }
    }

}
