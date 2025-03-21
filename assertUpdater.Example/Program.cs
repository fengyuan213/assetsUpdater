// See https://aka.ms/new-console-template for more information

using assertUpdater.DbModel;
using assertUpdater.Tests;
using assertUpdater.Tests.TestData;
using assertUpdater.Utils;

using Microsoft.VisualStudio.TestTools.UnitTesting;

Console.WriteLine("Hello, World!");



RemoteDbFile _remoteDbFile = StaticTestData.GetTestRemoteDbFile();
var _localPath = _remoteDbFile.AddressBuilder.BuildDownloadLocalPath(_remoteDbFile.RelativePath);

var downloadOperation = _remoteDbFile.BuildDownloadOperation();


var timer = new Timer(Callback, null, 0, 300);


await downloadOperation.Execute();




Assert.IsTrue(File.Exists(_localPath), "Downloaded file does not exist");
Assert.IsTrue(File.ReadAllBytes(_localPath).Length == _remoteDbFile.FileSize, "Downloaded file is not 5MB");
Assert.AreEqual(FileUtils.Sha1File(_localPath), _remoteDbFile.Hash);

Assert.IsTrue(downloadOperation.Progress == 100, "Downloaded progress is not 100%");


void Callback(object? state)
{
    Console.WriteLine($"Total: {downloadOperation.TotalBytesToReceive} ,Received: {downloadOperation.BytesReceived}, Progress: {downloadOperation.Progress}");
}
