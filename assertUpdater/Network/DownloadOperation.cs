
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Threading;

using assertUpdater.DbModel;
using assertUpdater.Exceptions;
using assertUpdater.Extensions;
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
                DownloadConfig = DownloadConfig.DefaultConfig();
            }


            _httpClient = new HttpClient(new HttpClientHandler()
            {
                Credentials = DownloadConfig.Credentials
                
            });
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
            _httpClient.Timeout = TimeSpan.FromMinutes(5);

            _pgReporter = new Progress<float>();
            _pgReporter.ProgressChanged += _pgReporter_ProgressChanged;

            
            

        }

        private void _pgReporter_ProgressChanged(object sender, float e)
        {
            Progress = e*100;
        }

        public async Task Execute()
        {

            // This really can be any type of writeable stream.
            using (var file = new FileStream(LocalPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {

                // Use the custom extension method below to download the data.
                // The passed progress-instance will receive the download status updates.
            
                var token = new CancellationToken();


                var pgReporter = (IProgress<float>)_pgReporter;
                // Get the http headers first to examine the content length
                using (var response = await _httpClient.GetAsync(Url, HttpCompletionOption.ResponseHeadersRead))
                {
                    var contentLength = response.Content.Headers.ContentLength;
                    TotalBytesToReceive =  contentLength ?? 0;
                    using (var download = await response.Content.ReadAsStreamAsync())
                    {


                        // Convert absolute progress (bytes downloaded) into relative progress (0% - 100%)
                        var relativeProgress = new Progress<long>(totalBytes => pgReporter.Report((float)totalBytes / contentLength.Value));
                        // Use extension method to report progress while downloading

                        await download.CopyToAsync(file, 81920, relativeProgress, token);
                        //pgReporter.Report(1);
                    }
                }
               // await _httpClient.DownloadAsync(Url, file, _pgReporter, token);
            }
            //var task = _httpClient.DownloadFileTaskAsync(Url, LocalPath);
            //await task.ConfigureAwait(false);
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

        private HttpClient _httpClient;

        public string Url { get; set; }
        public string LocalPath { get; set; }
        public long TotalBytesToReceive { get; private set; }
        public long BytesReceived { get; private set; }
        public double Progress { get; set; }

        private  Progress<float> _pgReporter;
        ~DownloadOperation()
        {
            _httpClient.Dispose();
        }
    }
}
