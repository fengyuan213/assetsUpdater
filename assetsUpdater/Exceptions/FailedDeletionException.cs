using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace assetsUpdater.Exceptions
{
    [Serializable]
    public class FailedDeletionException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public string FilePath { get; set; }
        public FailedDeletionException(string message,string filePath) : base(message)
        {
            FilePath = filePath;
        }

        public FailedDeletionException(string message,string filePath, Exception inner) : base(message, inner)
        {
            FilePath = filePath;
        }

        protected FailedDeletionException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
   
}
