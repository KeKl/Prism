using System;
using Prism.Logging;
using Serilog;
using Serilog.Debugging;

namespace HelloWorld.Logging
{
    /// <summary>
    ///     The SeriLog Logger for Prism LoggerFacade.
    /// </summary>
    public class SerilogLogger : ILoggerFacade
    {
        #region Constructors

        /// <summary>
        ///     Creates a new instance of the SeriLog Logger.
        /// </summary>
        public SerilogLogger()
        {
            _logger = new LoggerConfiguration()
                .CreateLogger();
        }

        #endregion

        #region Fields

        private readonly ILogger _logger;

        #endregion

        #region ILoggerFacade Members

        /// <summary>
        ///     Writes a log message.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="category">The message category.</param>
        /// <param name="priority">Not used by Log4Net; pass Priority.None.</param>
        public void Log(string message, Category category, Priority priority)
        {
            switch (category)
            {
                case Category.Debug:
                    _logger.Debug(message);
                    break;
                case Category.Exception:
                    if (priority == Priority.High)
                        _logger.Fatal(message);
                    _logger.Error(message);
                    break;
                case Category.Info:
                    _logger.Information(message);
                    break;
                case Category.Warn:
                    _logger.Warning(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(category));
            }
        }

        #endregion
    }
}