#region Using

using System;
using System.Runtime.Serialization;

#endregion

namespace assetsUpdater.Exceptions
{
    [Serializable]
    public class FailedDeletionException : Exception
#pragma warning restore RCS1194 // Implement exception constructors.
    {
        public FailedDeletionException(string? message, string? filePath) : base(message)
        {
            FilePath = filePath;
        }

        public FailedDeletionException(string? message, string? filePath, Exception? inner) : base(message, inner)
        {
            FilePath = filePath;
        }

        protected FailedDeletionException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public FailedDeletionException(string? message) : base(message)
        {
        }

        public FailedDeletionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public string? FilePath { get; set; }
    }
}