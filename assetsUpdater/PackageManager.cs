#region Using

using assetsUpdater.EventArgs;
using assetsUpdater.Exceptions;
using assetsUpdater.Interfaces;
using assetsUpdater.Model;
using assetsUpdater.Model.Network;
using assetsUpdater.Model.StorageProvider;
using assetsUpdater.Network;
using assetsUpdater.Utils;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace assetsUpdater
{
    public class PackageManager
    {
        private readonly IAddressBuilder _addressBuilder;
        private bool _isInitialized;

        public PackageManager(AssertUpgradePackage assertUpgradePackage, IAddressBuilder addressBuilder)
        {
            AssertUpgradePackage = assertUpgradePackage;

            _addressBuilder = addressBuilder;
        }

        public AssertUpgradePackage AssertUpgradePackage { get; set; }

        public void Init()
        {
            _isInitialized = true;
        }

        /// <summary>
        ///     This will apply both Local and Remote (Means start download queue)
        /// </summary>
        /// <returns></returns>
        public async Task<DownloadQueue> Apply()
        {
            await Apply_Local();
            var downloadQueue = await Apply_Remote();
            return downloadQueue;
        }

        protected virtual IEnumerable<IDownloadUnit> BuildDownloadUnits(int asyncThresholdSizeMb = 5)
        {
            var downloadUnits = new List<IDownloadUnit>();
            downloadUnits.AddRange(AssertUpgradePackage.AddFile.Select(databaseFile =>
                BuildDownloadUnit(databaseFile, asyncThresholdSizeMb)).ToList());
            downloadUnits.AddRange(AssertUpgradePackage.DifferFile.Select(databaseFile =>
                BuildDownloadUnit(databaseFile, asyncThresholdSizeMb)));
            return downloadUnits;
        }

        protected virtual IDownloadUnit BuildDownloadUnit(DatabaseFile databaseFile,
            int asyncThresholdSize)
        {
            var downloadPackage = BuildDownloadPackage(databaseFile);
            if (FileSizeParser.ParseMb(databaseFile.FileSize) > asyncThresholdSize)
            {
                downloadPackage.DownloadMode = DownloadMode.MultiPart;
                var mPartUnit = new MPartDownload(downloadPackage);
                return mPartUnit;
            }

            downloadPackage.DownloadMode = DownloadMode.Async;
            var asyncUnit = new AsyncDownload(downloadPackage);
            return asyncUnit;
        }

        protected virtual DownloadPackage BuildDownloadPackage(DatabaseFile databaseFile)
        {
            Uri uri;

            if (string.IsNullOrWhiteSpace(databaseFile.DownloadAddress))
                uri = _addressBuilder.BuildUri(databaseFile.RelativePath);
            else
                uri = new Uri(databaseFile.DownloadAddress);

            var localPath = _addressBuilder.BuildDownloadLocalPath(databaseFile.RelativePath);
            var fileSize = databaseFile.FileSize;
            var exceptedHash = databaseFile.Hash;
            var downloadMode = DownloadMode.Async;
            var downloadPackage = new DownloadPackage(uri, localPath, fileSize, exceptedHash, downloadMode);

            return downloadPackage;
        }

        protected virtual DownloadQueue BuildDownloadConfig()
        {
            var total = AssertUpgradePackage.DifferFile.Count() + AssertUpgradePackage.AddFile.Count();
            var parallelCount = 5;
            if (total > 100)
                parallelCount = 80;
            else if (total > 50)
                parallelCount = 20;
            else if (total > 20)
                parallelCount = 10;
            else if (total > 10) parallelCount = 9;

            var downloadQueue = new DownloadQueue(parallelCount);
            downloadQueue.MaxParallelMPartDownloadCount = 1;
            return downloadQueue;
        }

        public virtual async Task<DownloadQueue> Apply_Remote()
        {
            if (!_isInitialized) throw new PackageManagerNotInitializedException("Package Manager not initialized");

            var downloadQueue = BuildDownloadConfig();
            var downloadUnits = BuildDownloadUnits();
            await downloadQueue.QueueDownload(downloadUnits).ConfigureAwait(true);
            return downloadQueue;
        }

        public Task Apply_Local()
        {
            if (!_isInitialized) throw new PackageManagerNotInitializedException("Package Manager not initialized");

            foreach (var deleteFile in AssertUpgradePackage.DeleteFile)
                RemoveFile(_addressBuilder.BuildDownloadLocalPath(deleteFile.RelativePath));

            foreach (var databaseFile in AssertUpgradePackage.DifferFile)

                RemoveFile(_addressBuilder.BuildDownloadLocalPath(databaseFile.RelativePath));
            return Task.CompletedTask;
        }

        private void OnDeletionFailed(string msg, string filepath, Exception? e = null)
        {
            var ex = new FailedDeletionException($"Failed to remove {filepath}, Please delete it manually", filepath);
            AssertVerify.OnMessageNotify(this,
                new MessageNotifyEventArgs(MsgL.Critical, msg, true, ex));
            throw ex;
        }

        private void RemoveFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return;

            try
            {
                if (File.Exists(path)) File.Delete(path);
            }
            catch (Exception e)
            {
                OnDeletionFailed($"删除文件失败,请手动删除:{path}", path, e);
            }
        }
    }
}