#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using assetsUpdater.EventArgs;
using assetsUpdater.Exceptions;
using assetsUpdater.Interfaces;
using assetsUpdater.Model;
using assetsUpdater.Model.Network;
using assetsUpdater.Model.StorageProvider;
using assetsUpdater.Network;
using assetsUpdater.Utils;

#endregion

namespace assetsUpdater
{
    public class PackageManager
    {
        public PackageManager(string localRootPath, AssertUpgradePackage assertUpgradePackage)
        {
            AssertUpgradePackage = assertUpgradePackage;
            LocalRootPath = localRootPath;
        }
        public AssertUpgradePackage AssertUpgradePackage { get; set; }
        public string LocalRootPath { get; set; }
        public event EventHandler<MessageNotifyEventArgs> MessageNotify;


        /// <summary>
        /// This will apply both Local and Remote (Means start download queue)
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
                BuildDownloadUnit(databaseFile, LocalRootPath, asyncThresholdSizeMb)).ToList());
            downloadUnits.AddRange(AssertUpgradePackage.DifferFile.Select(databaseFile =>
                BuildDownloadUnit(databaseFile, LocalRootPath, asyncThresholdSizeMb)));
            return downloadUnits;
        }

        protected virtual IDownloadUnit BuildDownloadUnit(DatabaseFile databaseFile, string localRootPath,
            int asyncThresholdSize)
        {
            var downloadPackage = BuildDownloadPackage(databaseFile, localRootPath);
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

        protected virtual DownloadPackage BuildDownloadPackage(DatabaseFile databaseFile, string localRootPath)
        {
            var uri = new Uri(databaseFile.DownloadAddress);
            var localPath = Path.Join(localRootPath, databaseFile.RelativePath);
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
            var downloadQueue = BuildDownloadConfig();
            var downloadUnits = BuildDownloadUnits();
            await downloadQueue.QueueDownload(downloadUnits).ConfigureAwait(true);
            return downloadQueue;
        }

        public Task Apply_Local()
        {
            foreach (var deleteFile in AssertUpgradePackage.DeleteFile)
                RemoveFile(Path.Join(LocalRootPath, deleteFile.RelativePath));

            foreach (var databaseFile in AssertUpgradePackage.DifferFile)
                RemoveFile(Path.Join(LocalRootPath, databaseFile.RelativePath));
            return Task.CompletedTask;
        }


        private void OnDeletionFailed(string msg, string filepath, Exception e = null)
        {
            MessageNotify?.Invoke(this,
                new MessageNotifyEventArgs(MsgL.Error, msg, true, new FileDeletionException("未能删除:" + filepath, e)));
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
                OnDeletionFailed("删除文件失败", path, e);
            }
        }
    }
}