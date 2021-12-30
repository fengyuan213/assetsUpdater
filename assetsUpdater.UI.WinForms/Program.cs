#region Using

using System;
using System.Threading;
using System.Windows.Forms;
using assetsUpdater.EventArgs;
using NLog;

using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

using System.Threading.Tasks;
using System.Windows;

#endregion

namespace assetsUpdater.UI.WinForms
{
    internal static class Program
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static readonly Logger KernelLogger= LogManager.GetLogger("AssertVerifyKernel");
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            #region WinFormAppUI Config
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            #endregion

            #region Setup Logging
            Console.WriteLine("Initialize Logging...");
            //NLogStartup.LogDirectory = "";
            //NLogStartup.LogFileName = "";
            NLogLoader.Load(Thread.CurrentThread,"assertUpdater.WinForm");
            Logger.Info("Logging System Initialized...");
            AssertVerify.MessageNotify += AssertVerify_LogNotify;
            Logger.Info("AssertUpdater Message Channel Subscribed");
            #endregion

            Logger.Info("Initializeing TencentCos");
            TencentCosHelper.Init(TencentCosHelper.DefaultCosConfiguration());

            DisplaySampleLog();
       
            Application.Run(new MainForm());
        }

        public static void DisplaySampleLog()
        {
            Logger.Trace("Sample Trace");
            Logger.Debug("Sample Debug");
            Logger.Info("Sample Info");
            Logger.Warn("Sample Warn");
            Logger.Error("Sample Error");
            Logger.Fatal("Sample Fatal");
        }
        private static void AssertVerify_LogNotify(object? sender, MessageNotifyEventArgs e)
        {
            LogLevel nLogLevel=LogLevel.Trace;

            switch (e.MessageLevel)
            {
                case MsgL.Debug:
                    nLogLevel=LogLevel.Debug;
                    break;
                case MsgL.Info:
                    nLogLevel=LogLevel.Info;
                    break;
                case MsgL.Error:
                    nLogLevel=LogLevel.Error;
                    break;
                case MsgL.Serve:
                    nLogLevel=LogLevel.Fatal;
                    break;
                case MsgL.Critical:
                    nLogLevel = LogLevel.Fatal;
                    break;
            }
            KernelLogger.Log(nLogLevel,e.Message,e.Exception,e.Obj);
             
            //Console.WriteLine("{0}:{1}| {2} ", e.MessageLevel, e.Message, e.Exception);
        }
    }
}