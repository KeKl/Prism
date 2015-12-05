using Perspex;
using Prism.Logging;

namespace Prism
{
    /// <summary>
    /// Base class that provides a basic bootstrapping sequence and hooks
    /// that specific implementations can override
    /// </summary>
    /// <remarks>
    /// This class must be overridden to provide application specific configuration.
    /// </remarks>
    public abstract class ApplicationBase : Application
    {
        /// <summary>
        /// Gets the <see cref="ILoggerFacade"/> for the application.
        /// </summary>
        /// <value>A <see cref="ILoggerFacade"/> instance.</value>
        protected ILoggerFacade Logger { get; set; }
        
        /// <summary>
        /// Create the <see cref="ILoggerFacade" /> used by the bootstrapper.
        /// </summary>
        /// <remarks>
        /// The base implementation returns a new TextLogger.
        /// </remarks>
        protected virtual ILoggerFacade CreateLogger()
        {
            return new DebugLogger();
        }

        /// <summary>
        /// Runs the bootstrapper process.
        /// </summary>
        public abstract void Run();
    }
}