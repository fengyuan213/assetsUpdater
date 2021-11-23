using System.Diagnostics;
using assetsUpdater.Interfaces;
using assetsUpdater.Model.Network;
using COSXML;
using COSXML.Callback;
using COSXML.CosException;
using COSXML.Model;
using COSXML.Transfer;

namespace assetsUpdater.Tencent.Network
{
    public class TencentUploadUnit:IUploadUnit
    {
        public TencentUploadUnit(CosXmlServer cosXml, string tencentBucketAppId, UploadPackage uploadPackage)
        {
            CosXml = cosXml;
            UploadPackage = uploadPackage;
            TencentBucketAppId = tencentBucketAppId;
            TotalBytes = Utils.FileUtils.GetFileSize(UploadPackage.FileLocalPath);

        }

        public event EventHandler<bool>? UploadCompletedEventHandler;

        public COSXMLUploadTask CosXmlUploadTask { get; private set; }
        //public IEnumerable<COSXMLUploadTask> AllUploadTasks { get; set; } = new List<COSXMLUploadTask>();
        public long BytesSent { get; private set; }
        public CosXmlServer CosXml { get; }
        public Task<COSXMLUploadTask.UploadTaskResult>? CurrentUploadingTask { get; private set; }
        public double Progress { get; private set; }
        /// <summary>
        /// 存储桶名称，此处填入格式必须为 bucketname-APPID, 其中 APPID 获取参考 https://console.cloud.tencent.com/developer
        /// eg: examplebucket-1250000000
        /// </summary>
        public string TencentBucketAppId { get; }

        public long TotalBytes { get; private set; }
        public UploadPackage UploadPackage { get; }
        private bool _isOnCompleteHandlerCalled = false;
        /// <summary>
        /// May throw exception
        /// </summary>
        /// <returns></returns>
        public Task Start()
        {
            // 初始化 TransferConfig
            TransferConfig transferConfig = new TransferConfig();
            // 初始化 TransferManager
            TransferManager transferManager = new TransferManager(CosXml, transferConfig);
            // 存储桶名称，此处填入格式必须为 bucketname-APPID, 其中 APPID 获取参考 https://console.cloud.tencent.com/developer
            
            String bucket = TencentBucketAppId;
            String cosPath = UploadPackage.ResourceKey; //对象在存储桶中的位置标识符，即称对象键
            String srcPath = UploadPackage.FileLocalPath;//本地文件绝对路径

            // 上传对象
            COSXMLUploadTask uploadTask = new COSXMLUploadTask(bucket, cosPath);
           
            uploadTask.SetSrcPath(srcPath);
            uploadTask.failCallback = FailCallback;
            uploadTask.successCallback = UploadSucceedCallBack;
            uploadTask.progressCallback = UploadProgressCallBack;
            CosXmlUploadTask = uploadTask;
            CurrentUploadingTask = transferManager.UploadAsync(uploadTask);
            //AllUploadTasks= AllUploadTasks.Append(uploadTask);

        
           
            return Task.CompletedTask;
        }

 

        /// <summary>
        /// May throw exception
        /// </summary>
        /// <returns></returns>
        public  Task Wait()
        {
            if (CurrentUploadingTask==null)
            {
                return Task.CompletedTask;
            }

            
            var result =  CurrentUploadingTask.Result;

            //Debug.WriteLine("Upload Result Info{0}", result.GetResultInfo());
            
            string eTag = result.eTag;
           // Debug.WriteLine("Upload Etag:{0}", eTag);
            return Task.CompletedTask;
        }

        public Task Cancel()
        { 
            CosXmlUploadTask.Cancel();
            return Task.CompletedTask;
        }
        protected virtual void OnUploadCompleted(bool isSucceed)
        {
          
            UploadCompletedEventHandler?.Invoke(this, isSucceed);
        }

        private void UploadProgressCallBack(long completed, long total)
        {
            TotalBytes = total;
            BytesSent = completed;
           
            Progress = (double)completed/(double)TotalBytes/1d;
            
            if (Progress==1)
            {
                
                if (!_isOnCompleteHandlerCalled)
                {
                    OnUploadCompleted(true);
                    _isOnCompleteHandlerCalled=true;
                }
            }
           
            //Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
        }

        private void UploadSucceedCallBack(CosResult result)
        {
            Console.WriteLine("Upload Succeed CallBack:{0}", result.Key);
            if (!_isOnCompleteHandlerCalled)
            {
                OnUploadCompleted(result.IsSuccessful());
                _isOnCompleteHandlerCalled = true;
            }

            Debug.WriteLine("Upload Succeed CallBack:{0}",result.Key);
        }
        private void FailCallback(CosClientException clientexception, CosServerException serverexception)
        {
            Console.WriteLine(clientexception);
            Console.WriteLine(serverexception);
            if (!_isOnCompleteHandlerCalled)
            {
                OnUploadCompleted(false);
                _isOnCompleteHandlerCalled = true;

            }

        }
    }
}
