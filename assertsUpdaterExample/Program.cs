using System;
using assetsUpdater;
using assetsUpdater.Interfaces;
using assetsUpdater.StorageProvider;

namespace assertsUpdaterExample
{
    class Program
    {
        static async void AssertVerifyMain()
        {

            var url = "";
            var dbPath = "";
            LocalDataManager ldm = new LocalDataManager(dbPath);
            IStorageProvider localStorageProvider = ldm.StorageProvider;
            var result = await AssertVerify.Check_Update(localStorageProvider,url);

            if (result== (null,false))
            {
                Console.WriteLine("Failed to Fetch Update");
            }

            if (!result.isUpdateRequired)
            {
                return;
            }
            var assertUpgradePackage = AssertVerify.DatabaseCompare(result.remoteDataManager.StorageProvider, localStorageProvider);

            foreach (var databaseFile in assertUpgradePackage.AddFile)
            {
                Console.WriteLine("File to Add:{0}",databaseFile.FileName);
            }
            foreach (var databaseFile in assertUpgradePackage.DeleteFile)
            {
                Console.WriteLine("File to Delete:{0}", databaseFile.FileName);
            }
            foreach (var databaseFile in assertUpgradePackage.DifferFile)
            {
                Console.WriteLine("File to Change:{0}", databaseFile.FileName);
            }

            string localRootPath= localStorageProvider.GetBuildInDbData().Config.VersionControlFolder;
            PackageManager pm = new PackageManager(localRootPath, assertUpgradePackage);
            pm.MessageNotify += Pm_MessageNotify;
            Console.WriteLine("PackageManager Channel Subscribed");
            var downloadQueue = await pm.Apply();

            Console.WriteLine("Start Waiting.....");
            
            await downloadQueue.WaitAll();

        }

        private static void Pm_MessageNotify(object sender, assetsUpdater.EventArgs.MessageNotifyEventArgs e)
        {
            Console.WriteLine("Level:{0}|Message:{1},|Exception:{2}",e.MessageLevel,e.Message,e.Exception);
        }

        static void Main(string[] args)
        {
            try
            {
                AssertVerifyMain();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            Console.WriteLine("Hello World!");
        }
    }
}
