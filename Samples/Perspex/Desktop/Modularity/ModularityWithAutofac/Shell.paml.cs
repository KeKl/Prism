using System;
using System.Globalization;
using ModuleTracking;
using Perspex.Controls;
using Perspex.Interactivity;
using Perspex.Markup.Xaml;
using Prism.Logging;
using Prism.Modularity;

namespace ModularityWithAutofac
{
    public class Shell : Window
    {
        private readonly IModuleTracker _moduleTracker;
        private readonly IModuleManager _moduleManager;
        private readonly CallbackLogger _logger;

        public Shell(IModuleManager moduleManager, IModuleTracker moduleTracker, CallbackLogger logger)
        {
            if (moduleManager == null)
            {
                throw new ArgumentNullException(nameof(moduleManager));
            }

            if (moduleTracker == null)
            {
                throw new ArgumentNullException(nameof(moduleTracker));
            }

            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            this._moduleManager = moduleManager;
            this._moduleTracker = moduleTracker;
            this._logger = logger;

            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            PerspexXamlLoader.Load(this);
        }

        /// <summary>
        /// Logs the specified message.  Called by the CallbackLogger.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="priority">The priority.</param>
        public void Log(string message, Category category, Priority priority)
        {
            //this.TraceTextBox.AppendText(string.Format(CultureInfo.CurrentUICulture, "[{0}][{1}] {2}\r\n", category, priority, message));
        }

        /// <summary>
        /// Handles the RequestModuleLoad event of the ModuleB control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ModuleB_RequestModuleLoad(object sender, EventArgs e)
        {
            // The ModuleManager uses the Async Events Pattern.
            this._moduleManager.LoadModule(WellKnownModuleNames.ModuleB);
        }

        /// <summary>
        /// Handles the RequestModuleLoad event of the ModuleC control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ModuleC_RequestModuleLoad(object sender, EventArgs e)
        {
            // The ModuleManager uses the Async Events Pattern.
            this._moduleManager.LoadModule(WellKnownModuleNames.ModuleC);
        }

        /// <summary>
        /// Handles the RequestModuleLoad event of the ModuleE control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ModuleE_RequestModuleLoad(object sender, EventArgs e)
        {
            // The ModuleManager uses the Async Events Pattern.
            this._moduleManager.LoadModule(WellKnownModuleNames.ModuleE);
        }

        /// <summary>
        /// Handles the RequestModuleLoad event of the ModuleF control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ModuleF_RequestModuleLoad(object sender, EventArgs e)
        {
            // The ModuleManager uses the Async Events Pattern.
            this._moduleManager.LoadModule(WellKnownModuleNames.ModuleF);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // I use the IModuleTracker as the data-context for data-binding.
            // This quickstart only demonstrates modularity for Prism and does not use data-binding patterns such as MVVM.
            this.DataContext = this._moduleTracker;

            // I set this shell's Log method as the callback for receiving log messages.
            this._logger.Callback = this.Log;
            this._logger.ReplaySavedLogs();

            // I subscribe to events to help track module loading/loaded progress.
            // The ModuleManager uses the Async Events Pattern.
            this._moduleManager.LoadModuleCompleted += this.ModuleManager_LoadModuleCompleted;
            this._moduleManager.ModuleDownloadProgressChanged += this.ModuleManager_ModuleDownloadProgressChanged;
        }

        /// <summary>
        /// Handles the LoadModuleProgressChanged event of the ModuleManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Prism.Modularity.LoadModuleProgressChangedEventArgs"/> instance containing the event data.</param>
        void ModuleManager_ModuleDownloadProgressChanged(object sender, ModuleDownloadProgressChangedEventArgs e)
        {
            this._moduleTracker.RecordModuleDownloading(e.ModuleInfo.ModuleName, e.BytesReceived, e.TotalBytesToReceive);
        }

        /// <summary>
        /// Handles the LoadModuleCompleted event of the ModuleManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Prism.Modularity.LoadModuleCompletedEventArgs"/> instance containing the event data.</param>
        void ModuleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            this._moduleTracker.RecordModuleLoaded(e.ModuleInfo.ModuleName);
        }
    }
}
