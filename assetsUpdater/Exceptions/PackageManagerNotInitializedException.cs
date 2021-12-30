using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assetsUpdater.Exceptions
{

    [Serializable]
    public class PackageManagerNotInitializedException : Exception
    {
        public PackageManagerNotInitializedException() { }
        public PackageManagerNotInitializedException(string message) : base(message) { }
        public PackageManagerNotInitializedException(string message, Exception inner) : base(message, inner) { }
        protected PackageManagerNotInitializedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}
