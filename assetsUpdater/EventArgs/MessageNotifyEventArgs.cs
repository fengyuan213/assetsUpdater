using System;

namespace assetsUpdater.EventArgs
{
    public enum MsgL
    {
        Debug,
        Info,
        Warning,
        Error,
        Serve,
        Critical
    }

    public class MessageNotifyEventArgs : System.EventArgs
    {
        public Exception? Exception;
        public string Message;
        public bool HasError = false;
        public MsgL MessageLevel;
        public object? Obj = null;//Extra object pass through

        public MessageNotifyEventArgs(MsgL level, string message, bool hasError = false, Exception? exception = null, object? obj = null)
        {
            Exception = exception;
            Message = message;
            HasError = hasError;
            MessageLevel = level;
            Obj = obj;
        }
    }
}