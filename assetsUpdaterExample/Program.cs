#region Using

using assetsUpdater;
using assetsUpdater.AddressBuilder;
using assetsUpdater.EventArgs;
using assetsUpdater.Interfaces;
using assetsUpdater.Model.Network;
using assetsUpdater.Model.StorageProvider;
using assetsUpdater.Network;
using assetsUpdater.StorageProvider;
using assetsUpdater.Tencent.AddressBuilders;
using assetsUpdater.Tencent.Network;
using assetsUpdater.Utils;

using COSXML;
using COSXML.Auth;
using COSXML.CosException;
using COSXML.Model.Tag;

using System.Diagnostics;

#endregion

namespace assetsUpdaterExample;

internal static class Program
{
    private static async void TencentUploadQueueTest()
    {
        /*var cosXml = GetCosServer();
        var tencentBucketId = "pokecity-1251938563";

        var fileNameA = "deepin-boot-maker.exe";
        var fileNameB = "openwrt-x86-64-squashfs-combined-201231-Mask.img.gz";

        var uploadPackageA =
            BuildUploadPackage(Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileNameA),
                fileNameA);
        var uploadPackageB =
            BuildUploadPackage(Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileNameB),
                fileNameB);
        //Upload will override existing file if exist
        var unit = new TencentUploadUnit(cosXml, tencentBucketId, uploadPackageA);
        var unit2 = new TencentUploadUnit(cosXml, tencentBucketId, uploadPackageB);
        */

        var queue = new UploadQueue(2);

        await using var timer = new Timer(UploadQueueTimerCallback, queue, 0, 500);

        var vcsRoot = @"C:\Users\fengy\Desktop\Pokecity";
        var typeDSecret = "iua6dfbq85glwowm03gvnlpx7wctx7l";

        var addressBuilder = new TencentAddressBuilder("https://pokecity-1251938563.file.myqcloud.com", vcsRoot, typeDSecret);

        UploadUnitBuilder unitsBuilder = new UploadUnitBuilder(addressBuilder, "assertUpdaterExample");
        var relativeKeys = FileUtils.GetAllFilesInADirectory(vcsRoot, "kcptun").ToList().Select((s => s.Replace('\\', '/')));
        var units = await unitsBuilder.Build(relativeKeys).ConfigureAwait(false);

        await queue.QueueUpload(units);

        /*
        Console.WriteLine("Unit Progress:{0}", unit.Progress);
        Console.WriteLine("Unit TotalBytes:{0}", unit.TotalBytes);
        Console.WriteLine("Unit BytesSent:{0}", unit.BytesSent);*/
        await queue.WaitAll();
    }

    private static async void AssertVerifyPokecityMain()
    {
        var url = "";
        var dbPath = "";
        var ldm = new LocalDataManager(dbPath);

        var localStorageProvider = ldm.StorageProvider;
        var result = await AssertVerify.Check_Update(localStorageProvider, url);

        if (result == (null, false)) Console.WriteLine("Failed to Fetch Update");

        if (!result.isUpdateRequired) return;
        var assertUpgradePackage =
           await AssertVerify.DatabaseCompare(result.remoteDataManager.StorageProvider, localStorageProvider);

        foreach (var databaseFile in assertUpgradePackage.AddFile)
            Console.WriteLine("File to Add:{0}", databaseFile.FileName);
        foreach (var databaseFile in assertUpgradePackage.DeleteFile)
            Console.WriteLine("File to Delete:{0}", databaseFile.FileName);
        foreach (var databaseFile in assertUpgradePackage.DifferFile)
            Console.WriteLine("File to Change:{0}", databaseFile.FileName);

        var localRootPath = localStorageProvider.GetData().Config.VersionControlFolder;
        IAddressBuilder addressBuilder = new DefaultAddressBuilder("", "");

        var pm = new PackageManager(assertUpgradePackage, addressBuilder);

        Console.WriteLine("PackageManager Channel Subscribed");
        var downloadQueue = await pm.Apply();

        Console.WriteLine("Start Waiting.....");

        await downloadQueue.WaitAll();
    }

    private static void Pm_MessageNotify(object sender, MessageNotifyEventArgs e)
    {
        Console.WriteLine("Level:{0}|Message:{1},|Exception:{2}", e.MessageLevel, e.Message, e.Exception);
    }

    private static DbConfig GetDbConfig()
    {
        var vcsFolder = @"C:\Users\fengy\Desktop\[低配]PoKeCitY客户端-公测 v1.2.6.3";
        var dbSchemasDir = new List<string>();
        var dbSchemasFile = new List<string>();
        dbSchemasDir.Add(".minecraft/versions");
        dbSchemasFile.Add("HMCL.exe");

        var config = new DbConfig(vcsFolder)
        {
            MajorVersion = 1,
            MinorVersion = 1,
            DatabaseSchema = new DbSchema
            {
                DirList = dbSchemasDir.ToArray(),
                FileList = dbSchemasFile.ToArray()
            },
            DownloadAddressBuilder = new Cdn8N6NAddressBuilder("downloadLocalRoot", "apiRoot", "apiKey", "apiSecret")
        };

        return config;
    }

    private static async void BuildLocalDb()
    {
        try
        {
            Console.WriteLine("Building Local DB");
            var sw = new Stopwatch();
            sw.Start();

            var exportPath = @"C:\Users\fengy\Desktop\ldb.zip";
            var db = new DatabaseBuilder(new FileDatabase());
            await db.BuildDatabase(GetDbConfig(), exportPath).ConfigureAwait(false);

            sw.Stop();
            Console.WriteLine("Local Db Build Finished,Time:{0}", sw.Elapsed.Milliseconds);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static void QCloudCosInit()
    {
        try
        {
            var bucketName = "pokecity-1251938563";
            var appid = "1251938563"; //设置腾讯云账户的账户标识 APPID
            var region = "ap-shanghai-fsi"; //设置一个默认的存储桶地域
            var config = new CosXmlConfig.Builder()
                .IsHttps(true) //设置默认 HTTPS 请求
                .SetRegion(region) //设置一个默认的存储桶地域
                .SetDebugLog(true) //显示日志
                .Build(); //创建 CosXmlConfig 对象+

            var secretId = "AKIDXwCMPLte1CC2i9tbfXFxUpzylPRLDI0W"; //"云 API 密钥 SecretId";
            var secretKey = "Z2JOG1cO4d0a0FKH4iBTZUEm8KNI4e6O"; //"云 API 密钥 SecretKey";
            long durationSecond = 600; //每次请求签名有效时长，单位为秒
            QCloudCredentialProvider cosCredentialProvider = new DefaultQCloudCredentialProvider(
                secretId, secretKey, durationSecond);
            var cosXml = new CosXmlServer(config, cosCredentialProvider);

            var preSignatureStruct = new PreSignatureStruct();
            // APPID 获取参考 https://console.cloud.tencent.com/developer
            preSignatureStruct.appid = appid;
            // 存储桶所在地域, COS 地域的简称请参照 https://cloud.tencent.com/document/product/436/6224
            preSignatureStruct.region = region;
            // 存储桶名称，此处填入格式必须为 bucketname-APPID, 其中 APPID 获取参考 https://console.cloud.tencent.com/developer
            preSignatureStruct.bucket = bucketName;
            preSignatureStruct.key = "exampleobject"; //对象键
            preSignatureStruct.httpMethod = "GET"; //HTTP 请求方法
            preSignatureStruct.isHttps = true; //生成 HTTPS 请求 URL
            preSignatureStruct.signDurationSecond = 600; //请求签名时间为600s
            preSignatureStruct.headers = null; //签名中需要校验的 header
            preSignatureStruct.queryParameters = null; //签名中需要校验的 URL 中请求参数

            var requestSignURL = cosXml.GenerateSignURL(preSignatureStruct);
        }
        catch (CosClientException e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static void TencentAddressBuilder()
    {
        try
        {
            Console.WriteLine("Building Local DB");
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < 500000; i++)
            {
                var rootDownloadAddress = "https://pokecity-1251938563.file.myqcloud.com";
                var localSaveRoot = @"C:\Users\fengy\Desktop\";

                var typeDSecret = "iua6dfbq85glwowm03gvnlpx7wctx7l";

                var addressBuilder = new TencentAddressBuilder(rootDownloadAddress, localSaveRoot, typeDSecret);
                //var resourceKey = "rdb.zip";
                var resourceKey = "[中文测试]wd%#.zip";
                var url = addressBuilder.BuildUri(resourceKey);
                //Console.WriteLine("Builded Url:{0}", url.OriginalString);
                //Console.WriteLine("TencentAddressBuilder Finished,Time:{0}", sw.Elapsed.Milliseconds);
            }

            sw.Stop();

            Console.WriteLine("TencentAddressBuilder Finished,Time:{0}", sw.Elapsed.TotalMilliseconds);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static void Main(string[] args)
    {
        TencentCosHelper.Init(TencentCosHelper.DefaultCosConfiguration());

        /*var sw = new Stopwatch();
        sw.Start();

        sw.Stop();
        Console.WriteLine("Request Time:{0}", sw.Elapsed.Milliseconds);*/
        //TencentUploadQueueTest();
        Console.WriteLine("Hello World!");
        Console.ReadLine();
        TencentUploadQueueTest();
        Console.WriteLine("Hello World!");
        GC.Collect(50, GCCollectionMode.Forced);
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        Console.ReadLine();
        Console.ReadLine();
    }

    private static void UploadQueueTimerCallback(object? queue)
    {
        UploadQueue? uploadQueue = queue as UploadQueue;
        if (uploadQueue == null)
        {
            return;
        }

        //alias
        var progress = uploadQueue.Progress;
        var uploadedB = uploadQueue.BytesSent;
        var totalB = uploadQueue.TotalBytesToUpload;
        var allUnits = uploadQueue.AllUnits;
        var waitingUnits = uploadQueue.WaitingUnits;
        var errorUnits = uploadQueue.ErrorUnits;
        var currentUnits = uploadQueue.CurrentUploadingUnits;
        Console.WriteLine("--- Upload Trace Data Start ---");
        Console.WriteLine(
            $"Progress:{progress},Uploaded:{FileSizeParser.ParseAuto(uploadedB)}, Total:{FileSizeParser.ParseAuto(totalB)}");
        Console.WriteLine($"Uploaded Bytes:{uploadedB}, Total Bytes:{totalB}");
        Console.WriteLine(
            $"Units Count- Waiting:{waitingUnits.Count} All:{allUnits.Count} Current:{currentUnits.Count} Error:{errorUnits.Count}");
        Console.WriteLine("Lists of waiting, error and current");
        LogUnits("Waiting", waitingUnits);
        LogUnits("Error", errorUnits);
        LogUnits("Current", waitingUnits);

        Console.WriteLine("--- Upload Trace Data End ---");

        void LogUnits(in string unitName, in IEnumerable<IUploadUnit> units)
        {
            foreach (var unit in units)
            {
                //alias
                var key = unit.UploadPackage.ResourceKey;
                var localPath = unit.UploadPackage.FileLocalPath;
                var p = unit.Progress;
                var byteSent = unit.BytesSent;
                var totalByte = unit.TotalBytes;

                Console.WriteLine(
                    $"Status:{unitName}->Key:{key} localPath:{localPath}\n Byte Sent:{byteSent}, Byte Total:{totalByte}, Progress:{p}");
            }
        }

        /*
        Console.WriteLine("Upload Progress:{0}", uploadQueue?.Progress);
        Console.WriteLine("TotalBytes To Upload:{0}", uploadQueue?.TotalBytesToUpload);
        Console.WriteLine("Bytes Sent:{0}", uploadQueue?.BytesSent);
        Console.WriteLine("There are total of {0} unit, {1} uploading ,{2} waiting and {3} unit has error",
            uploadQueue?.AllUnits.Count,
            uploadQueue?.CurrentUploadingUnits.Count,
            uploadQueue?.WaitingUnits.Count,
            uploadQueue?.ErrorUnits.Count);*/
    }

    private static CosXmlServer GetCosServer()
    {
        //string bucketName = "pokecity-1251938563";
        var appid = "1251938563"; //设置腾讯云账户的账户标识 APPID
        var region = "ap-shanghai-fsi"; //设置一个默认的存储桶地域

        var config = new CosXmlConfig.Builder()
            .SetRegion(region) // 设置默认的区域, COS 地域的简称请参照 https://cloud.tencent.com/document/product/436/6224
            .SetDebugLog(true)
            .SetAppid(appid)
            .SetAllowAutoRedirect(true)
            .IsHttps(true)
            .Build();

        var secretId =
            "AKIDXwCMPLte1CC2i9tbfXFxUpzylPRLDI0W"; // 云 API 密钥 SecretId, 获取 API 密钥请参照 https://console.cloud.tencent.com/cam/capi
        var secretKey =
            "Z2JOG1cO4d0a0FKH4iBTZUEm8KNI4e6O"; // 云 API 密钥 SecretKey, 获取 API 密钥请参照 https://console.cloud.tencent.com/cam/capi
        long durationSecond = 600; //每次请求签名有效时长，单位为秒
        QCloudCredentialProvider qCloudCredentialProvider = new DefaultQCloudCredentialProvider(secretId,
            secretKey, durationSecond);

        return new CosXmlServer(config, qCloudCredentialProvider);
    }

    private static UploadPackage BuildUploadPackage(string localPath, string resourceKey)
    {
        return new UploadPackage(resourceKey, localPath);
    }

    private static async void TencentUploadUnitUploadTest()
    {
        var cosXml = GetCosServer();
        var tencentBucketId = "pokecity-1251938563";

        var localPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "SpaceSniffer.exe");
        var resourceKey = Path.GetFileName(localPath);
        Console.WriteLine("LocalPath:{0}", localPath);
        Console.WriteLine("ResourceKey:{0}", resourceKey);

        var uploadPackage = new UploadPackage(resourceKey, localPath);

        //Configure default override existing file
        var unit = new TencentUploadUnit(cosXml, tencentBucketId, uploadPackage);

        await unit.Start().ConfigureAwait(true);
        await unit.Wait().ConfigureAwait(true);

        Console.WriteLine("Unit Progress:{0}", unit.Progress);
        Console.WriteLine("Unit TotalBytes:{0}", unit.TotalBytes);
        Console.WriteLine("Unit BytesSent:{0}", unit.BytesSent);
    }
}