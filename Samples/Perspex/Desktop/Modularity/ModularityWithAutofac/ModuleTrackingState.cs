using System.ComponentModel;
using Prism.Modularity;
using Prism.Mvvm;

namespace ModularityWithAutofac
{
    /// <summary>
    /// Provides tracking of a module's state for the quickstart.
    /// </summary>
    /// <remarks>
    /// This class is for demonstration purposes for the quickstart and not expected to be used in a real world application.
    /// </remarks>
    public class ModuleTrackingState : BindableBase
    {
        private string _moduleName;
        private ModuleInitializationStatus _moduleInitializationStatus;
        private DiscoveryMethod _expectedDiscoveryMethod;
        private InitializationMode _expectedInitializationMode;
        private DownloadTiming _expectedDownloadTiming;
        private string _configuredDependencies = "(none)";
        private long _bytesReceived;
        private long _totalBytesToReceive;

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        /// <value>A string.</value>
        /// <remarks>
        /// This is a display string.
        /// </remarks>
        public string ModuleName
        {
            get { return this._moduleName; }
            set { SetProperty(ref _moduleName, value); }
        }

        /// <summary>
        /// Gets or sets the current initialization status of the module.
        /// </summary>
        /// <value>A ModuleInitializationStatus value.</value>
        public ModuleInitializationStatus ModuleInitializationStatus
        {
            get { return this._moduleInitializationStatus; }
            set { SetProperty(ref _moduleInitializationStatus, value); }
           
        }

        /// <summary>
        /// Gets or sets how the module is expected to be discovered.
        /// </summary>
        /// <value>A DiscoveryMethod value.</value>
        /// <remarks>
        /// The actual discovery method is determined by the ModuleCatalog.
        /// </remarks>
        public DiscoveryMethod ExpectedDiscoveryMethod
        {
            get { return this._expectedDiscoveryMethod; }
            set { SetProperty(ref _expectedDiscoveryMethod, value); }
        }

        /// <summary>
        /// Gets or sets how the module is expected to be initialized.
        /// </summary>
        /// <value>An InitializationModev value.</value>
        /// <remarks>
        /// The actual initialization mode is determiend by the ModuleCatalog.
        /// </remarks>
        public InitializationMode ExpectedInitializationMode
        {
            get { return this._expectedInitializationMode; }
            set { SetProperty(ref _expectedInitializationMode, value); }
        }

        /// <summary>
        /// Gets or sets how the module is expected to be downloaded.
        /// </summary>
        /// <value>A DownloadTiming value.</value>
        /// <remarks>
        /// The actual download timing is determiend by the ModuleCatalog.
        /// </remarks>        
        public DownloadTiming ExpectedDownloadTiming
        {
            get { return this._expectedDownloadTiming; }
            set { SetProperty(ref _expectedDownloadTiming, value); }
        }

        /// <summary>
        /// Gets or sets the list of modules the module depends on.
        /// </summary>
        /// <value>A string description of module dependencies.</value>
        /// <remarks>
        /// This is a display string.
        /// </remarks>
        public string ConfiguredDependencies
        {
            get { return this._configuredDependencies; }
            set { SetProperty(ref _configuredDependencies, value); }
        }

        /// <summary>
        /// Gets or sets the number of bytes received as the module is loaded.
        /// </summary>
        /// <value>A number of bytes.</value>
        public long BytesReceived
        {
            get { return this._bytesReceived; }
            set
            {
                SetProperty(ref _bytesReceived, value);
                OnPropertyChanged(nameof(DownloadProgressPercentage));
            }
        }

        /// <summary>
        /// Gets or sets the total bytes to receive to load the module.
        /// </summary>
        /// <value>A number of bytes.</value>
        public long TotalBytesToReceive
        {
            get { return this._totalBytesToReceive; }
            set
            {
                SetProperty(ref _totalBytesToReceive, value);
                OnPropertyChanged(nameof(DownloadProgressPercentage));
            }
        }

        /// <summary>
        /// Gets the percentage of BytesReceived/TotalByteToReceive.
        /// </summary>
        /// <value>A percentage number between 0 and 100.</value>
        public int DownloadProgressPercentage
        {
            get
            {
                if (this._bytesReceived < this._totalBytesToReceive)
                    return (int)(this._bytesReceived * 100.0 / this._totalBytesToReceive);
                else
                    return 100;
            }
        }
    }
}