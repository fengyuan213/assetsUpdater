using System;

namespace assetsUpdater.Interfaces
{
    public interface IAddressBuilder
    {
        public Uri BuildUri();
        public string BuildLocalPath();
    }
}