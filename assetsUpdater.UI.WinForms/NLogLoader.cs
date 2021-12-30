#region Using

using System;
using System.IO;
using System.Text;
using System.Threading;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using NLog.Targets.Wrappers;

#endregion

namespace assetsUpdater.UI.WinForms
{
    internal static class NLogLoader
    {
        public static string LoggingDirName="";
        public static string LogDirectory;
        public static string LogFileName = "App." + DateTime.Now.ToString("MM-dd-HHmmss") + ".log";
        private static int _wrapperIncrementer = 1;

        internal static void Load(in Thread uiThread,in string projectName)
        {
            uiThread.Name = "UI-Thread";
            Load(projectName);
        }
        internal static void Load(string logDirName)
        {
            LoggingDirName = logDirName;
            LogDirectory = Path.Join(Path.GetTempPath(), LoggingDirName);

            if (!Directory.Exists(LogDirectory)) Directory.CreateDirectory(LogDirectory);
#if DEBUG
            CleanLogDirectory();
#endif

            //Layouts
            var fileLayout = GetDefaultLayout();
            var sepFileLayout = GetDefaultLayout();
            var coloredConsoleLayout = GetDefaultLayout();

            //Targets
            var logFile = new FileTarget("logfile")
            {
                FileName = Path.Join(LogDirectory, LogFileName),
                EnableArchiveFileCompression = true,
                ArchiveAboveSize = 10240,
                FileNameKind = FilePathKind.Absolute,
                MaxArchiveFiles = 50,
                KeepFileOpen = true,
                OpenFileCacheTimeout = 30,
                Layout = fileLayout
            };
            var logSepFile = new FileTarget("logSepFile")
            {
                Layout = sepFileLayout,
                FileName = Path.Join(LogDirectory, "App.${level}.log")
            };
            var logColoredConsole = new ColoredConsoleTarget("logColoredConsole")
            {
                Encoding = Encoding.Default,
                Layout = coloredConsoleLayout,
                UseDefaultRowHighlightingRules = true,
                ErrorStream = true,
                EnableAnsiOutput = false,
                DetectConsoleAvailable = true,
                AutoFlush = true
            };

            //Wrappers
            var autoFlushSepFileWrapper = GetAsyncAutoFlushTargetWrapper(logFile, "FileWrapper");
            var autoFlushFileWrapper = GetAsyncAutoFlushTargetWrapper(logSepFile, "SepFileWrapper");
            
            //Configure LogLevels
            var minLevelSepFile = GetLogLevel(LogLevel.Trace, LogLevel.Debug);
            var minLevelConsole = GetLogLevel(LogLevel.Trace, LogLevel.Debug);
            var minLevelFile = GetLogLevel(LogLevel.Trace, LogLevel.Info);
            
            //NLog configuration section
            var config = new LoggingConfiguration();
            // Rules for mapping loggers to targets            
            config.AddRule(minLevelConsole, LogLevel.Fatal, logColoredConsole);
            config.AddRule(minLevelFile, LogLevel.Fatal, autoFlushFileWrapper);
            config.AddRule(minLevelSepFile, LogLevel.Fatal, autoFlushSepFileWrapper);
            // Apply config           
            LogManager.Configuration = config;
        }

        private static Layout GetDefaultLayout()
        {
            //"${date} ${level} [${threadname}] ${logger} - ${message} ${exception:format=ToString}"
            //{ gc: property = Enum}
            //"${longdate}|${level:uppercase=true}|${logger}|${message}${exception:format=ToString}",ConfigurationItemFactory.Default),

            //"${date} ${level} [${threadname}] ${logger} - ${message} ${exception:format=ToString}"
            //"${longdate}|${level:uppercase=true}|${logger}|${message}${exception:format=ToString}"

            var callSiteLayout =
                "${callsite:className=true:fileName=true:includeSourcePath=false:methodName=true:cleanNamesOfAsyncContinuations=true}";
            var level = "${level:uppercase=true}";
            //"${date} ${level} [${threadname}] ${logger} - ${message} ${exception:format=ToString}"
            // Targets where to log to: File and Console
            var layout = new SimpleLayout(
                "[${threadname}-ID:${threadid}]" +
                " | ${longdate} " +
                "| " + level + " |" +
                " ${logger} -> " +
                "${message}${exception:format=ToString}" +
                "  |" + callSiteLayout + "",
                ConfigurationItemFactory.Default);
            return layout;
        }

        private static LogLevel GetLogLevel(LogLevel debugLvl, LogLevel releaseLvl)
        {
            LogLevel lvl;
#if DEBUG
            lvl = debugLvl;
#else
            lvl = releaseLvl;
#endif
            return lvl;
        }


        private static AutoFlushTargetWrapper GetAsyncAutoFlushTargetWrapper(Target target, string name = "")
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = $"Wrapper[{_wrapperIncrementer}]";
                _wrapperIncrementer++;
            }

            var asyncWrapper = new AsyncTargetWrapper(string.Join("Async", name), target);
            var autoFlushWrapper = new AutoFlushTargetWrapper(string.Join("AsyncAutoFlush", name), asyncWrapper)
            {
                AsyncFlush = true,
                OptimizeBufferReuse = true
            };
            return autoFlushWrapper;
        }

        internal static void CleanLogDirectory()
        {
            try
            {
                Directory.Delete(LogDirectory, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            try
            {
                //Clean up
                var fileTrace = Path.Combine(LogDirectory, "App.Trace.log");
                var fileDebug = Path.Combine(LogDirectory, "App.Debug.log");
                var fileInfo = Path.Combine(LogDirectory, "App.Info.log");
                var fileWarn = Path.Combine(LogDirectory, "App.Warn.log");
                var fileError = Path.Combine(LogDirectory, "App.Error.log");
                var fileFatal = Path.Combine(LogDirectory, "App.Fatal.log");
                if (File.Exists(fileTrace)) File.Delete(fileTrace);
                if (File.Exists(fileDebug)) File.Delete(fileDebug);
                if (File.Exists(fileInfo)) File.Delete(fileInfo);
                if (File.Exists(fileWarn)) File.Delete(fileWarn);
                if (File.Exists(fileError)) File.Delete(fileError);
                if (File.Exists(fileFatal)) File.Delete(fileFatal);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Unable to Delete Previous Logging Cache");
                Console.Error.WriteLine(e);
            }
        }
    }
}