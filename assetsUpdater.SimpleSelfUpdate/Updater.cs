using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace assetsUpdater.SimpleSelfUpdate
{
    public class Updater

    {
        public Func<string, bool> Comparer { get; set; }
        public string UpdateUrl { get; set; }
        public string CurrentVersion { get; set; }
        public string ProcPath { get; set; }
        public string ProgramUrl { get; set; }

        public Updater(string updateUrl, string programUrl, string currentVersion, string procPath, Func<string, bool> comparer = null)
        {
            UpdateUrl = updateUrl;
            CurrentVersion = currentVersion;
            ProcPath = procPath;
            ProgramUrl = programUrl;
            comparer ??= (s => s.TrimStart().TrimEnd() == CurrentVersion.TrimStart().TrimEnd());

            Comparer = comparer;
        }

        public async Task<string> FetchLastContent()
        {
            try
            {
                using var webClient = new WebClient();

                var str = await webClient.DownloadStringTaskAsync(new Uri(UpdateUrl)).ConfigureAwait(false);
                return str;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> IsUpdateRequired()
        {
            var str = await FetchLastContent().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new UpdateCheckFailedException("Failed to check update")
                {
                };
            }
            var isUpdateRequired = Comparer.Invoke(str);

            return isUpdateRequired;
        }

        private static void ExecuteCommand(string command)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            var process = Process.Start(processInfo);

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("output>>" + e.Data);
            process.BeginOutputReadLine();

            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("error>>" + e.Data);
            process.BeginErrorReadLine();

            process.WaitForExit();

            Console.WriteLine("ExitCode: {0}", process.ExitCode);
            process.Close();
        }

        public async Task Update()
        {
            var tmpFile = Path.GetTempFileName();

            try
            {
                Exception exception = null;
                bool isSucceed = true;
                using var webClient = new WebClient();

                AsyncCompletedEventHandler handler = (sender, args) =>
                {
                    if (args.Cancelled != false || args.Error != null)
                    {
                        isSucceed = false;
                        exception = args.Error;
                    }
                };

                webClient.DownloadFileCompleted += handler;
                webClient.DownloadFileAsync(new Uri(ProgramUrl), tmpFile);

                if (!isSucceed)
                {
                    throw new UpdateFailedException("Failed to download update program", exception)
                    {
                    };
                }

                webClient.DownloadFileCompleted -= handler;
                var exec = Path.GetFileName(tmpFile);
                string cmd =
                    $"taskkill /f /im {exec}&& xcopy {tmpFile} {ProcPath} /c /h /r && start {ProcPath} /D {Directory.GetCurrentDirectory()}";
                var batFile = Path.GetTempFileName() + ".bat";
                await using var sw = new StreamWriter(batFile, false)
                {
                    AutoFlush = true
                };
                await sw.WriteLineAsync(cmd);
                ExecuteCommand($"start {batFile}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}