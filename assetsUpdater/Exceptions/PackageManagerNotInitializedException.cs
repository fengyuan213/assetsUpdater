#region Using

using System;
using System.Runtime.Serialization;

#endregion

namespace assetsUpdater.Exceptions
{
    [Serializable]
    public class PackageManagerNotInitializedException : Exception
    {
        public PackageManagerNotInitializedException()
        {
        }

        public PackageManagerNotInitializedException(string message) : base(message)
        {
        }

        public PackageManagerNotInitializedException(string message, Exception inner) : base(message, inner)
        {
        }

        protected PackageManagerNotInitializedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}