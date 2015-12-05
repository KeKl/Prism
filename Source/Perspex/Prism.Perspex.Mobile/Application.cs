using System;
using Microsoft.Practices.ServiceLocation;
using Perspex.Controls;
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
    public abstract class Application : ApplicationBase
    {
        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <value>The shell user interface.</value>
        protected IControl Page { get; set; }

        /// <summary>
        /// Runs the bootstrapper process.
        /// </summary>
        public override void Run()
        {
            this.Run(true);
        }
        
        /// <summary>
        /// Configures the <see cref="ViewModelLocator"/> used by Prism.
        /// </summary>
        protected virtual void ConfigureViewModelLocator()
        {
            ViewModelLocationProvider.SetDefaultViewModelFactory((type) => ServiceLocator.Current.GetInstance(type));
        }

        /// <summary>
        /// Registers the <see cref="Type"/>s of the Exceptions that are not considered 
        /// root exceptions by the <see cref="ExceptionExtensions"/>.
        /// </summary>
        protected virtual void RegisterFrameworkExceptionTypes()
        {
            ExceptionExtensions.RegisterFrameworkExceptionType(
                typeof(ActivationException));
        }

        /// <summary>
        /// Creates the first page.
        /// </summary>
        /// <returns>The shell of the application.</returns>
        protected virtual IControl CreatePage()
        {
            return null;
        }

        /// <summary>
        /// Initializes the first page.
        /// </summary>
        protected virtual void InitializePage()
        {
        }

        /// <summary>
        /// Run the bootstrapper process.
        /// </summary>
        /// <param name="runWithDefaultConfiguration">If <see langword="true"/>, registers default 
        /// Prism Library services in the container. This is the default behavior.</param>
        public abstract void Run(bool runWithDefaultConfiguration);

        /// <summary>
        /// Configures the LocatorProvider for the <see cref="Microsoft.Practices.ServiceLocation.ServiceLocator" />.
        /// </summary>
        protected abstract void ConfigureServiceLocator();
    }
}