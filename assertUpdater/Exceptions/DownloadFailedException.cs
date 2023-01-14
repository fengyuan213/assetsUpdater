using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assertUpdater.Exceptions
{
    public class DownloadFailedException : Exception
    {
        public DownloadFailedException()
        {
        }

        public DownloadFailedException(string message) : base(message)
        {
        }

        public DownloadFailedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
