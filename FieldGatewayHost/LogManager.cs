namespace FieldGatewayHost
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class LogManager : ILog
    {
        private readonly object syncRoot = new object();
        private Dictionary<string, NLog.Logger> loggers = new Dictionary<string, NLog.Logger>();

        /// <summary>
        /// Logs the specified message at debug level.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Debug(string message)
        {
            this.GetLogger().Debug(message);
        }

        /// <summary>
        /// Logs the specified message at info level.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(string message)
        {
            this.GetLogger().Info(message);
        }

        /// <summary>
        /// Logs the specified message at warning level.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warning(string message)
        {
            this.GetLogger().Warn(message);
        }

        /// <summary>
        /// Logs the specified message and exception at warning level.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The exception</param>
        public void Warning(string message, Exception ex)
        {
            this.GetLogger().Warn(ex, message);
        }

        /// <summary>
        /// Logs the specified message at error level.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(string message)
        {
            this.GetLogger().Error(message);
        }

        /// <summary>
        /// Logs the specified message and exception at error level.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The exception</param>
        public void Error(string message, Exception ex)
        {
            this.GetLogger().Error(ex, message);
        }

        /// <summary>
        /// Logs the specified message at fatal level.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Fatal(string message)
        {
            this.GetLogger().Fatal(message);
        }

        /// <summary>
        /// Logs the specified message and exception at fatal level.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public void Fatal(string message, Exception ex)
        {
            this.GetLogger().Fatal(ex, message);
        }

        /// <summary>
        /// Logs the specified exception at exception level.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void Exception(Exception exception)
        {
            this.GetLogger().Error(exception, exception.Message);
        }

        /// <summary>
        /// Logs the specified exception at exception level with additional message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        public void Exception(Exception exception, string message)
        {
            this.GetLogger().Error(exception, message);
        }

        private NLog.Logger GetLogger()
        {
            var frame = new StackFrame(2);
            string name = frame.GetMethod().DeclaringType.DeclaringType == null ?
                frame.GetMethod().DeclaringType.Name : frame.GetMethod().DeclaringType.DeclaringType.Name;

            if (name.Contains("Diagnostics.Log"))
            {
                frame = new StackFrame(3);
                name = frame.GetMethod().DeclaringType.FullName;
            }

            NLog.Logger log;
            if (!this.loggers.TryGetValue(name, out log))
            {
                log = NLog.LogManager.GetLogger(name);
                lock (this.syncRoot)
                {
                    if (!this.loggers.ContainsKey(name))
                    {
                        this.loggers.Add(name, log);
                    }
                }
            }

            return log;
        }
    }
}
