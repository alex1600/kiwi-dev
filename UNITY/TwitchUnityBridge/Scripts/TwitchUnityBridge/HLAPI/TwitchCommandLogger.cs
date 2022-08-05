#region

using System;

#endregion

namespace TwitchUnityBridge.HLAPI
{
    public class TwitchCommandLogger
    {
        public Action<string> ErrorHandler;
        public Action<Exception> ExceptionHandler;
        public Action<string> InfoHandler;
        public Action<string> WarningHandler;

        internal void Info(string message)
        {
            InfoHandler?.Invoke(message);
        }

        internal void Error(string message)
        {
            ErrorHandler?.Invoke(message);
        }

        internal void Warning(string message)
        {
            WarningHandler?.Invoke(message);
        }

        internal void Exception(Exception exception)
        {
            ExceptionHandler?.Invoke(exception);
        }
    }
}
