
using System.ComponentModel;
using System.Diagnostics;
using System.Net;

using assertUpdater.DbModel;
using assertUpdater.Exceptions;
using assertUpdater.Operations;


namespace assertUpdater.Network
{
    public class DownloadOperation : IOperation
    {

        public DownloadConfig DownloadConfig { get; set; }
        public DownloadOperation(string url, string path, DownloadConfig config = null)
        {

            Url = url;
            DownloadConfig = config;
            LocalPath = path;

            Init();


        }

        private void Init()
        {
            DownloadOnetimeInit.Init();

            if (DownloadConfig == null)
            {
                //Default Config
                DownloadConfig = new DownloadConfig()
                {
                    Credentials = CredentialCache.DefaultNetworkCredentials
                };
            }


            _webClient = new WebClient();
            _webClient.Headers.Add("User-Agent: Other");
            _webClient.Credentials = DownloadConfig.Credentials;
            _webClient.DownloadProgressChanged += OnDlPgChanged;
            _webClient.DownloadFileCompleted += OnDlCompleted;

        }

        public async Task Execute()
        {
            var task = _webClient.DownloadFileTaskAsync(Url, LocalPath);
            await task.ConfigureAwait(false);
        }

        private void OnDlCompleted(object? sender, AsyncCompletedEventArgs e)
        {

            Debugger.Break();
            var s = (WebClient)sender;
        
            if (e.Cancelled)
            {
                Console.Error.WriteLine($"Client: {s}, Download has been canceled {e.Error}");
                return;
            }
            if (e.Error != null)
            {
                Console.Error.WriteLine($"Client: {s}, Downloading file with error:  {e.Error}");
                throw new DownloadFailedException("Client: {s}, Downloading file with error", e.Error);
            }
            Console.WriteLine($"Client: {s}, Downloading file completed");
            
       
        }

        private void OnDlPgChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            BytesReceived = e.BytesReceived;
            TotalBytesToReceive = e.TotalBytesToReceive;
            Progress = e.ProgressPercentage;

        }

        private WebClient _webClient;

        public string Url { get; set; }
        public string LocalPath { get; set; }
        public double TotalBytesToReceive { get; private set; }
        public double BytesReceived { get; private set; }
        public double Progress { get; set; }


        ~DownloadOperation()
        {
            _webClient.Dispose();
        }
    }
}
