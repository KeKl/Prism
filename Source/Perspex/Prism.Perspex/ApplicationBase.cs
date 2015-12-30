using System;
using Microsoft.Practices.ServiceLocation;
using Perspex;
using Perspex.Controls;
using Prism.Logging;
using Prism.Mvvm;

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
        /// Gets the main user interface.
        /// </summary>
        /// <value>The main user interface.</value>
        protected PerspexObject MainView { get; set; }

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
        /// Configures the <see cref="ViewModelLocator"/> used by Prism.
        /// </summary>
        protected virtual void ConfigureViewModelLocator()
        {
            ViewModelLocationProvider.SetDefaultViewModelFactory(
                (type) => ServiceLocator.Current.GetInstance(type));
        }

        /// <summary>
        /// Runs the bootstrapper process.
        /// </summary>
        public abstract void Run();

        /// <summary>
        /// Creates the main view.
        /// </summary>
        /// <returns>The main view of the application.</returns>
        protected virtual PerspexObject CreateMainView() => null;
      

        /// <summary>
        /// Initializes the main view.
        /// </summary>
        protected virtual void InitializeMainView()
        {
        }

        /// <summary>
        /// Registers the <see cref="System.Type"/>s of the Exceptions that are not considered 
        /// root exceptions by the <see cref="ExceptionExtensions"/>.
        /// </summary>
        protected virtual void RegisterFrameworkExceptionTypes()
        {
            ExceptionExtensions.RegisterFrameworkExceptionType(
                typeof(ActivationException));
        }

        /// <summary>
        /// Configures the LocatorProvider for the <see cref="ServiceLocator" />.
        /// </summary>
        protected abstract void ConfigureServiceLocator();
    }
}