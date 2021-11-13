using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Timers;
using assetsUpdater;
using assetsUpdater.AddressBuilder;
using assetsUpdater.Interfaces;
using assetsUpdater.Model.StorageProvider;
using assetsUpdater.StorageProvider;

namespace assertsUpdaterExample
{
    class Program
    {
        static async void AssertVerifyPokecityMain()
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

        static DbConfig GetDbConfig()
        {
            var vcsFolder= @"C:\Users\fengy\Desktop\[低配]PoKeCitY客户端-公测 v1.2.6.3";
            var dbSchemas = new List<string>();
            dbSchemas.Add(".minecraft/versions");
            dbSchemas.Add("HMCL.exe");
    
            var config = new DbConfig( vcsFolder)
            {
                MajorVersion = 1,
                MirrorVersion = 1,
                DatabaseSchema = new DbSchema()
                {
                    DirList = dbSchemas.ToArray()
                },
                DownloadAddressBuilder =  new Cdn8N6NAddressBuilder("downloadLocalRoot","apiRoot","apiKey","apiSecret")
            };
         
            return config;
        }
        static async void BuildLocalDb()
        {
            var exportPath = @"C:\Users\fengy\Desktop\ldb.zip";
            DatabaseBuilder db = new DatabaseBuilder(new FileDatabase());
            await  db.BuildDatabase(GetDbConfig(),exportPath);
        }
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Building Local DB");
                var sw = new Stopwatch();
                sw.Start();
                BuildLocalDb();
                sw.Stop();
                Console.WriteLine("Local Db Build Finished,Time:{0}",sw.Elapsed.Milliseconds);
                //AssertVerifyPokecityMain();
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
