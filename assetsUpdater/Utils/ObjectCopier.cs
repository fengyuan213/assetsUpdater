using Newtonsoft.Json;

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace assetsUpdater.Utils
{
    /// <summary>
    /// Reference Article http://www.codeproject.com/KB/tips/SerializedObjectCloner.aspx
    /// Provides a method for performing a deep copy of an object.
    /// Binary Serialization is used to perform the copy.
    /// </summary>
    public static class ObjectCopier
    {
        /// <summary>
        /// Perform a deep Copy of the object, using Json as a serialization method. NOTE: Private members are not cloned using this method.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T CloneJson<T>(this T source, JsonSerializerSettings? settings = null)
        {
            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null)) return default;

            // initialize inner objects individually
            // for example in default constructor some list property initialized with some values,
            // but in 'source' these items are cleaned -
            // without ObjectCreationHandling.Replace default constructor values will be added to result
            settings ??= new JsonSerializerSettings();
            settings.ObjectCreationHandling = ObjectCreationHandling.Replace;

            var t = source.GetType();
            var value = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(value, settings) ?? throw new SerializationException($"Unable to clone {typeof(T)}");
        }

        /// <summary>
        /// Perform a deep copy of the object via serialization.
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
            if (ReferenceEquals(source, null)) return default;

            using MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011 // 类型或成员已过时
            formatter.Serialize(stream, source ?? throw new ArgumentNullException(nameof(source)));
#pragma warning restore SYSLIB0011 // 类型或成员已过时
            stream.Seek(0, SeekOrigin.Begin);
#pragma warning disable SYSLIB0011 // 类型或成员已过时
            return (T)formatter.Deserialize(stream);
#pragma warning restore SYSLIB0011 // 类型或成员已过时
        }
    }
}