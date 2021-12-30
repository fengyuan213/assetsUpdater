#region Using
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using assetsUpdater.Interfaces;
using assetsUpdater.Model.Network;
using assetsUpdater.Model.StorageProvider;
using assetsUpdater.Utils;
using COSXML;
using COSXML.CosException;
using COSXML.Model;
using COSXML.Transfer;

#endregion

namespace assetsUpdater.Tencent.Network

{
    public class TencentUploadUnit : IUploadUnit
    {
        private bool _isOnCompleteHandlerCalled;

        public TencentUploadUnit(CosXmlServer cosXml, string tencentBucketAppId, UploadPackage uploadPackage)
        {
            CosXml = cosXml;
            UploadPackage = uploadPackage;
            TencentBucketAppId = tencentBucketAppId;
            TotalBytes = FileUtils.GetFileSize(UploadPackage.FileLocalPath);
            CosXmlUploadTask = null;
        }

        public COSXMLUploadTask? CosXmlUploadTask { get; private set; }
        public CosXmlServer CosXml { get; }
        public Task<COSXMLUploadTask.UploadTaskResult>? CurrentUploadingTask { get; private set; }

        /// <summary>
        ///     存储桶名称，此处填入格式必须为 bucketname-APPID, 其中 APPID 获取参考 https://console.cloud.tencent.com/developer
        ///     eg: examplebucket-1250000000
        /// </summary>
        public string TencentBucketAppId { get; }

        public event EventHandler<bool>? UploadCompletedEventHandler;

        //public IEnumerable<COSXMLUploadTask> AllUploadTasks { get; set; } = new List<COSXMLUploadTask>();
        public Task UploadTask
        {
            get { return CurrentUploadingTask ?? Task.CompletedTask; }
        }
        public long BytesSent { get; private set; }
        public double Progress { get; private set; }

        public long TotalBytes { get; private set; }
        public UploadPackage UploadPackage { get; }

        /// <summary>
        ///     May throw exception
        /// </summary>
        /// <returns></returns>
        public Task Start()
        {

            // 初始化 TransferConfig
            var transferConfig = new TransferConfig();
            // 初始化 TransferManager
            var transferManager = new TransferManager(CosXml, transferConfig);
            // 存储桶名称，此处填入格式必须为 bucketname-APPID, 其中 APPID 获取参考 https://console.cloud.tencent.com/developer

            var bucket = TencentBucketAppId;
            var cosPath = UploadPackage.ResourceKey; //对象在存储桶中的位置标识符，即称对象键
            var srcPath = UploadPackage.FileLocalPath; //本地文件绝对路径

            // 上传对象
            var uploadTask = new COSXMLUploadTask(bucket, cosPath);

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
        ///     May throw exception
        /// </summary>
        /// <returns></returns>
        public Task Wait()
        {
            if (CurrentUploadingTask == null) return Task.CompletedTask;


            var result = CurrentUploadingTask.Result;

            //Debug.WriteLine("Upload Result Info{0}", result.GetResultInfo());

            var eTag = result.eTag;
            // Debug.WriteLine("Upload Etag:{0}", eTag);
            return Task.CompletedTask;
        }

        public Task Cancel()
        {
            CosXmlUploadTask?.Cancel();
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

            Progress = completed / (double)TotalBytes / 1d;

            if (Progress == 1)
                if (!_isOnCompleteHandlerCalled)
                {
                    OnUploadCompleted(true);
                    _isOnCompleteHandlerCalled = true;
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

            Console.WriteLine("Upload Succeed CallBack:{0}", result.Key);
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