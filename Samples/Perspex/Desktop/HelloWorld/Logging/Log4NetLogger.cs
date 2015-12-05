using System;
using log4net;
using Prism.Logging;

namespace HelloWorld.Logging
{
    /// <summary>
    ///     The Log4Net Logger for Prism LoggerFacade.
    /// </summary>
    public class Log4NetLogger : ILoggerFacade
    {
        #region Constructors

        /// <summary>
        ///     Creates a new instance of the Log4Net Logger.
        /// </summary>
        public Log4NetLogger()
        {
            _logger = LogManager.GetLogger(typeof(Log4NetLogger));
        }

        #endregion

        #region Fields

        private readonly ILog _logger;

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
                    _logger.Info(message);
                    break;
                case Category.Warn:
                    _logger.Warn(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("category");
            }
        }

        #endregion
    }
}
