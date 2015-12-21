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
        /// Runs the bootstrapper process.
        /// </summary>
        public override void Run()
        {
            this.Run(true);
        }
        
        /// <summary>
        /// Run the bootstrapper process.
        /// </summary>
        /// <param name="runWithDefaultConfiguration">If <see langword="true"/>, registers default 
        /// Prism Library services in the container. This is the default behavior.</param>
        public abstract void Run(bool runWithDefaultConfiguration);
    }
}