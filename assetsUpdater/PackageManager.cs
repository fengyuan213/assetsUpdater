using System;
using System.IO;
using System.Threading.Tasks;
using assetsUpdater.EventArgs;
using assetsUpdater.Exceptions;
using assetsUpdater.Model;

namespace assetsUpdater
{
  
    public class PackageManager
    {
      
        public event EventHandler<MessageNotifyEventArgs> MessageNotify;
        public AssertUpgradePackage AssertUpgradePackage { get; set; }
        public string LocalRootPath { get; set; }
        public PackageManager(string localRootPath,AssertUpgradePackage assertUpgradePackage)
        {
            AssertUpgradePackage = assertUpgradePackage;
            LocalRootPath = localRootPath;
            MessageNotify += PackageManager_MessageNotify;
        }

        private void PackageManager_MessageNotify(object sender, MessageNotifyEventArgs e)
        {
       
        }

        protected virtual void OnDeletionFailed(string msg,string filepath,Exception e =null)
        {
            MessageNotify?.Invoke(this,new MessageNotifyEventArgs(MsgL.Error,msg,true,e??new FileDeletionException("未能删除:"+filepath,e)));
        }

        private void RemoveFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            if (File.Exists(path))
            {
                File.Delete(path);

            }

            if (File.Exists(path))
            {
                OnDeletionFailed("删除文件失败",path);
            }
        }

        public async Task Apply()
        {
            await Apply_Non_Download();
        }

        public async Task ApplyDownload()
        {

        }
        public  Task Apply_Non_Download()
        {
            foreach (var deleteFile in AssertUpgradePackage.DeleteFile)       
            {
                RemoveFile(Path.Combine(LocalRootPath,deleteFile.RelativePath));
            }

            foreach (var databaseFile in AssertUpgradePackage.DifferFile)
            {
                RemoveFile(Path.Combine(LocalRootPath, databaseFile.RelativePath));
            }
            return Task.CompletedTask;
        }
        public static async Task RequestServer()
        {
            await RequestInternal();
            await GatherLocalData();

        }
        public static async Task<AssertUpgradePostProcess> UpgradeAsync(AssertUpgradePackage assertUpgradePackage)
        {
            return null;
        }
        private static Task RequestInternal()
        {
            return null;
        }
        private static Task GatherLocalData()
        {
            return null;
        }
    }
}
