namespace FieldGatewayHost
{
    using System;

    /// <summary>
    /// Interface for logging provider.
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Logs the specified message at info level.
        /// </summary>
        /// <param name="message">The message.</param>
        void Info(string message);

        /// <summary>
        /// Logs the specified message at debug level.
        /// </summary>
        /// <param name="message">The message.</param>
        void Debug(string message);

        /// <summary>
        /// Logs the specified message at warning level.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warning(string message);

        /// <summary>
        /// Logs the specified message and exception at warning level.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The exception</param>
        void Warning(string message, Exception ex);

        /// <summary>
        /// Logs the specified message at error level.
        /// </summary>
        /// <param name="message">The message.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Justification = "Legacy", MessageId = "Error")]
        void Error(string message);

        /// <summary>
        /// Logs the specified message and exception at error level.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The exception</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Justification = "Legacy", MessageId = "Error")]
        void Error(string message, Exception ex);

        /// <summary>
        /// Logs the specified message at fatal level.
        /// </summary>
        /// <param name="message">The message.</param>
        void Fatal(string message);

        /// <summary>
        /// Logs the specified message and exception at fatal level.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        void Fatal(string message, Exception ex);

        /// <summary>
        /// Logs the specified exception at exception level.
        /// </summary>
        /// <param name="exception">The exception.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", Justification = "Legacy", MessageId = "0#")]
        void Exception(Exception exception);

        /// <summary>
        /// Logs the specified exception at exception level with additional message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", Justification = "Legacy", MessageId = "0#")]
        void Exception(Exception exception, string message);
    }
}
