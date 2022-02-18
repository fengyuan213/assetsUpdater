namespace assetsUpdater.Model.Network
{
    public struct UploadPackage
    {
        /// <summary>
        /// Resource key to access object storage
        /// </summary>
        public string ResourceKey { get; set; }

        /// <summary>
        ///  Local Absolute path to a file to upload
        /// </summary>
        public string FileLocalPath { get; set; }

        public UploadPackage(string resourceKey, string fileLocalPath)
        {
            ResourceKey = resourceKey;
            FileLocalPath = fileLocalPath;
        }
    }
}