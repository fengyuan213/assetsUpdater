using System;
using System.Runtime.Serialization;

namespace assetsUpdater.SimpleSelfUpdate
{
    [Serializable]
    public class UpdateFailedException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public UpdateFailedException()
        {
        }

        public UpdateFailedException(string message) : base(message)
        {
        }

        public UpdateFailedException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UpdateFailedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class UpdateCheckFailedException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public UpdateCheckFailedException()
        {
        }

        public UpdateCheckFailedException(string message) : base(message)
        {
        }

        public UpdateCheckFailedException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UpdateCheckFailedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}