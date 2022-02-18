using assetsUpdater.Model.Network;
using assetsUpdater.Tencent.Network;

using COSXML;
using COSXML.Auth;

namespace assetsUpdaterExample
{
    public struct TencentCosConfiguration
    {
        public TencentCosConfiguration(string bucketAppid, string appid, string defaultRegion, string secretId, string secretKey, int signatureDuration)
        {
            BucketAppid = bucketAppid;
            Appid = appid;
            DefaultRegion = defaultRegion;
            SecretId = secretId;
            SecretKey = secretKey;
            SignatureDuration = signatureDuration;
        }

        /// <summary>
        /// pokecity-1251938563
        /// </summary>
        public string BucketAppid { get; set; }

        /// <summary>
        ///  "1251938563" 设置腾讯云账户的账户标识 APPID
        /// </summary>
        public string Appid { get; set; }

        /// <summary>
        /// "ap-shanghai-fsi" 设置一个默认的存储桶地域
        /// </summary>
        public string DefaultRegion { get; set; }

        /// <summary>
        ///  "AKIDXwCMPLte1CC2i9tbfXFxUpzylPRLDI0W"
        /// // 云 API 密钥 SecretId, 获取 API 密钥请参照 https://console.cloud.tencent.com/cam/capi
        /// </summary>
        public string SecretId { get; set; }

        /// <summary>
        ///   "Z2JOG1cO4d0a0FKH4iBTZUEm8KNI4e6O"; // 云 API 密钥 SecretKey, 获取 API 密钥请参照 https://console.cloud.tencent.com/cam/capi
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 每次请求签名有效时长，单位为秒
        /// Unit: Seconds, recommend:600
        /// </summary>
        public int SignatureDuration { get; set; }
    }

    public class TencentCosHelper
    {
        private static bool _isInitialized = false;

        public static TencentCosConfiguration DefaultCosConfiguration()
        {
            var bucketId = "pokecity-1251938563";
            var appId = "1251938563";
            var region = "ap-shanghai-fsi";
            var ak = "AKIDXwCMPLte1CC2i9tbfXFxUpzylPRLDI0W";
            var sk = "Z2JOG1cO4d0a0FKH4iBTZUEm8KNI4e6O";
            var sigDuration = 600; //unit s
            var config = new TencentCosConfiguration(bucketId, appId, region, ak, sk, sigDuration);
            return config;
        }

        public static void Init(TencentCosConfiguration config)
        {
            _cosConfiguration = config;

            _isInitialized = true;
        }

        private static void IsInitializeCheck()
        {
            if (!_isInitialized)
            {
                throw new Exception("TencentCosHelper not initialized")
                {
                };
            }
        }

        private static TencentCosConfiguration _cosConfiguration;
        private static CosXmlServer? _cosXmlServer;

        public static CosXmlServer CosXmlServer
        {
            get
            {
                if (_cosXmlServer == null)
                {
                    _cosXmlServer = GetCosServer();
                }
                return _cosXmlServer;
            }
        }

        private static CosXmlServer GetCosServer()
        {
            //string bucketName = "";
            var appid = _cosConfiguration.Appid; //设置腾讯云账户的账户标识 APPID
            var region = _cosConfiguration.DefaultRegion; //设置一个默认的存储桶地域

            var secretId =
                _cosConfiguration.SecretId; // 云 API 密钥 SecretId, 获取 API 密钥请参照 https://console.cloud.tencent.com/cam/capi
            var secretKey =
                _cosConfiguration.SecretKey; // 云 API 密钥 SecretKey, 获取 API 密钥请参照 https://console.cloud.tencent.com/cam/capi
            long durationSecond = _cosConfiguration.SignatureDuration; //每次请求签名有效时长，单位为秒
            bool isDebug = false;
#if DEBUG
            isDebug = true;
#endif
            var config = new CosXmlConfig.Builder()
                .SetRegion(region) // 设置默认的区域, COS 地域的简称请参照 https://cloud.tencent.com/document/product/436/6224
                .SetDebugLog(isDebug)
                .SetAppid(appid)
                .SetAllowAutoRedirect(true)
                .IsHttps(true)
                .Build();

            QCloudCredentialProvider qCloudCredentialProvider = new DefaultQCloudCredentialProvider(secretId,
                secretKey, durationSecond);

            return new CosXmlServer(config, qCloudCredentialProvider);
        }

        public static TencentUploadUnit BuildUploadUnit(UploadPackage up)
        {
            var unit = new TencentUploadUnit(CosXmlServer, _cosConfiguration.BucketAppid, up);
            return unit;
        }

        /*
        private static async void TencentUploadQueueTest()
        {
            var cosXml = GetCosServer();
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

            var queue = new UploadQueue(1);

            await using var timer = new Timer(UploadQueueTimerCallback, queue, 0, 500);

            await queue.QueueUpload(unit);
            await queue.QueueUpload(unit2);
            Console.WriteLine("Unit Progress:{0}", unit.Progress);
            Console.WriteLine("Unit TotalBytes:{0}", unit.TotalBytes);
            Console.WriteLine("Unit BytesSent:{0}", unit.BytesSent);
            await queue.WaitAll();
        }
        */
    }
}