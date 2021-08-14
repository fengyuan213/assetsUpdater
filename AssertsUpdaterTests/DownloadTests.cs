using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using assetsUpdater.Interfaces;
using assetsUpdater.Network;
using assetsUpdater.Utils;
using Downloader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using DownloadPackage = assetsUpdater.Model.Network.DownloadPackage;

namespace AssertsUpdaterTests
{
    [TestClass]
    public class DownloadTests
    {
        private DownloadPackage mockdownloadPackageExternal;
        private WebClient mockWebClient;
        private DownloadService mockDownloadService;
        private DownloadSetting mockDownloadSetting;
        [TestInitialize]
        public async Task TestInitialize()

        {

            this.mockdownloadPackageExternal = Mock.Create<DownloadPackage>();
            mockdownloadPackageExternal.DownloadMode = DownloadMode.Async;
            mockdownloadPackageExternal.Uri=new Uri("https://ipv4.download.thinkbroadband.com:8080/5MB.zip");
            mockdownloadPackageExternal.LocalPath=Path.GetTempFileName();
            var fileSize = await FileUtils.GetFileSizeRemote(mockdownloadPackageExternal.Uri.ToString());
            
            Mock.Arrange(() => mockdownloadPackageExternal.SizeTotal).Returns(fileSize);

            mockdownloadPackageExternal.ExceptedHash = "";

           //this.mockWebClient = Mock.Create<WebClient>();
           this.mockWebClient = new WebClient();

            this.mockDownloadService = Mock.Create<DownloadService>();
            this.mockDownloadSetting = Mock.Create<DownloadSetting>();


        }

        private async Task<bool> AsyncDownload_Act(AsyncDownload asyncDownload)
        {

            await asyncDownload.Start().ConfigureAwait(true);



            await asyncDownload.Wait().ContinueWith((task =>
            {
                if (!task.IsCompletedSuccessfully)
                {
                    Assert.Fail("Download UNCOMPLECTED");
                }
            })).ConfigureAwait(true);
            return true;
        }
        [TestMethod]
        public async Task AsyncDownload_DownloadTest_ExternalProvider()
        { 
            mockdownloadPackageExternal.DownloadMode = DownloadMode.Async;
            var asyncDownload =new AsyncDownload(mockdownloadPackageExternal, mockDownloadSetting, mockWebClient);
            Mock.Arrange(() => asyncDownload.DownloadPackage).Returns(mockdownloadPackageExternal);
           

            await asyncDownload.Start().ConfigureAwait(true);


            await asyncDownload.Wait().ContinueWith((task =>
            {
                if (!task.IsCompletedSuccessfully)
                {
                    Assert.Fail("Download UNCOMPLECTED");
                }
            })).ConfigureAwait(true);
            //await AsyncDownload_Act(asyncDownload).ConfigureAwait(true);
            Console.WriteLine(asyncDownload.DownloadPackage.LocalPath);
           // Assert.AreEqual(100.00, asyncDownload.Progress);
 

        }

        [TestMethod]
        public async Task MPartDownload_DownloadTest_ExternalProvider()
        {
            mockdownloadPackageExternal.DownloadMode = DownloadMode.MultiPart;
            var mPartDownload = new MPartDownload(mockdownloadPackageExternal, mockDownloadSetting);
            Mock.Arrange(() => mPartDownload.DownloadPackage).Returns(mockdownloadPackageExternal);


            await mPartDownload.Start().ConfigureAwait(true);
            await mPartDownload.Wait().ConfigureAwait(true);
        }

        [TestMethod]
        public void AsyncDownload_DownloadTest_QiniuProvider()
        {

        }

        [TestMethod]
        public void MPartDownload_DownloadTest_QiniuProvider()
        {

        }
  

 
        
    }
}
