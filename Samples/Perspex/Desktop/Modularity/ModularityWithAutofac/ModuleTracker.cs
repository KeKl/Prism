﻿using System;
using System.Globalization;
using ModularityWithAutofac.Properties;
using ModuleTracking;
using Prism.Logging;
using Prism.Modularity;

namespace ModularityWithAutofac
{
    /// <summary>
    /// Provides tracking of modules for the quickstart.
    /// </summary>
    /// <remarks>
    /// This class is for demonstration purposes for the quickstart and not expected to be used in a real world application.
    /// This class exports the interface for modules and the concrete type for the shell.    
    /// </remarks>    
    public class ModuleTracker : IModuleTracker
    {
        private ILoggerFacade logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleTracker"/> class.
        /// </summary>
        public ModuleTracker(ILoggerFacade logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            this.logger = logger;

            // These states are defined specifically for the desktop version of the quickstart.
            this.ModuleATrackingState = new ModuleTrackingState
            {
                ModuleName = WellKnownModuleNames.ModuleA,
                ExpectedDiscoveryMethod = DiscoveryMethod.ApplicationReference,
                ExpectedInitializationMode = InitializationMode.WhenAvailable,
                ExpectedDownloadTiming = DownloadTiming.WithApplication,
                ConfiguredDependencies = WellKnownModuleNames.ModuleD,
            };

            this.ModuleBTrackingState = new ModuleTrackingState
            {
                ModuleName = WellKnownModuleNames.ModuleB,
                ExpectedDiscoveryMethod = DiscoveryMethod.DirectorySweep,
                ExpectedInitializationMode = InitializationMode.OnDemand,
                ExpectedDownloadTiming = DownloadTiming.InBackground,
            };
            this.ModuleCTrackingState = new ModuleTrackingState
            {
                ModuleName = WellKnownModuleNames.ModuleC,
                ExpectedDiscoveryMethod = DiscoveryMethod.ApplicationReference,
                ExpectedInitializationMode = InitializationMode.OnDemand,
                ExpectedDownloadTiming = DownloadTiming.WithApplication,
            };
            this.ModuleDTrackingState = new ModuleTrackingState
            {
                ModuleName = WellKnownModuleNames.ModuleD,
                ExpectedDiscoveryMethod = DiscoveryMethod.DirectorySweep,
                ExpectedInitializationMode = InitializationMode.WhenAvailable,
                ExpectedDownloadTiming = DownloadTiming.InBackground,
            };
            this.ModuleETrackingState = new ModuleTrackingState
            {
                ModuleName = WellKnownModuleNames.ModuleE,
                ExpectedDiscoveryMethod = DiscoveryMethod.ConfigurationManifest,
                ExpectedInitializationMode = InitializationMode.OnDemand,
                ExpectedDownloadTiming = DownloadTiming.InBackground,
            };
            this.ModuleFTrackingState = new ModuleTrackingState
            {
                ModuleName = WellKnownModuleNames.ModuleF,
                ExpectedDiscoveryMethod = DiscoveryMethod.ConfigurationManifest,
                ExpectedInitializationMode = InitializationMode.OnDemand,
                ExpectedDownloadTiming = DownloadTiming.InBackground,
                ConfiguredDependencies = WellKnownModuleNames.ModuleE,
            };
        }

        /// <summary>
        /// Gets the tracking state of module A.
        /// </summary>
        /// <value>A ModuleTrackingState.</value>
        /// <remarks>
        /// This is exposed as a specific property for data-binding purposes in the quickstart UI.
        /// </remarks>
        public ModuleTrackingState ModuleATrackingState { get; }

        /// <summary>
        /// Gets the tracking state of module B.
        /// </summary>
        /// <value>A ModuleTrackingState.</value>
        /// <remarks>
        /// This is exposed as a specific property for data-binding purposes in the quickstart UI.
        /// </remarks>
        public ModuleTrackingState ModuleBTrackingState { get; }

        /// <summary>
        /// Gets the tracking state of module C.
        /// </summary>
        /// <value>A ModuleTrackingState.</value>
        /// <remarks>
        /// This is exposed as a specific property for data-binding purposes in the quickstart UI.
        /// </remarks>
        public ModuleTrackingState ModuleCTrackingState { get; }

        /// <summary>
        /// Gets the tracking state of module D.
        /// </summary>
        /// <value>A ModuleTrackingState.</value>
        /// <remarks>
        /// This is exposed as a specific property for data-binding purposes in the quickstart UI.
        /// </remarks>
        public ModuleTrackingState ModuleDTrackingState { get; }

        /// <summary>
        /// Gets the tracking state of module E.
        /// </summary>
        /// <value>A ModuleTrackingState.</value>
        /// <remarks>
        /// This is exposed as a specific property for data-binding purposes in the quickstart UI.
        /// </remarks>
        public ModuleTrackingState ModuleETrackingState { get; }

        /// <summary>
        /// Gets the tracking state of module F.
        /// </summary>
        /// <value>A ModuleTrackingState.</value>
        /// <remarks>
        /// This is exposed as a specific property for data-binding purposes in the quickstart UI.
        /// </remarks>
        public ModuleTrackingState ModuleFTrackingState { get; }

        /// <summary>
        /// Records the module is loading.
        /// </summary>
        /// <param name="moduleName">The <see cref="WellKnownModuleNames">well-known name</see> of the module.</param>
        /// <param name="bytesReceived">The number of bytes downloaded.</param>
        /// <param name="totalBytesToReceive">The total number of bytes received.</param>
        public void RecordModuleDownloading(string moduleName, long bytesReceived, long totalBytesToReceive)
        {
            ModuleTrackingState moduleTrackingState = this.GetModuleTrackingState(moduleName);
            if (moduleTrackingState != null)
            {
                moduleTrackingState.BytesReceived = bytesReceived;
                moduleTrackingState.TotalBytesToReceive = totalBytesToReceive;

                if (bytesReceived < totalBytesToReceive)
                {
                    moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Downloading;
                }
                else
                {
                    moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Downloaded;
                }
            }

            this.logger.Log(
                string.Format(CultureInfo.CurrentCulture, Resources.ModuleIsLoadingProgress, moduleName, bytesReceived,
                    totalBytesToReceive), Category.Debug, Priority.Low);
        }

        /// <summary>
        /// Records the module has been constructed.
        /// </summary>
        /// <param name="moduleName">The <see cref="WellKnownModuleNames">well-known name</see> of the module.</param>
        public void RecordModuleConstructed(string moduleName)
        {
            ModuleTrackingState moduleTrackingState = this.GetModuleTrackingState(moduleName);
            if (moduleTrackingState != null)
            {
                moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Constructed;
            }

            this.logger.Log(string.Format(CultureInfo.CurrentCulture, Resources.ModuleConstructed, moduleName),
                Category.Debug, Priority.Low);
        }

        /// <summary>
        /// Records the module has been initialized.
        /// </summary>
        /// <param name="moduleName">The <see cref="WellKnownModuleNames">well-known name</see> of the module.</param>
        public void RecordModuleInitialized(string moduleName)
        {
            ModuleTrackingState moduleTrackingState = this.GetModuleTrackingState(moduleName);
            if (moduleTrackingState != null)
            {
                moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Initialized;
            }


            this.logger.Log(string.Format(CultureInfo.CurrentCulture, Resources.ModuleIsInitialized, moduleName),
                Category.Debug, Priority.Low);
        }

        /// <summary>
        /// Records the module is loaded.
        /// </summary>
        /// <param name="moduleName">The <see cref="WellKnownModuleNames">well-known name</see> of the module.</param>
        public void RecordModuleLoaded(string moduleName)
        {
            this.logger.Log(string.Format(CultureInfo.CurrentCulture, Resources.ModuleLoaded, moduleName),
                Category.Debug, Priority.Low);
        }

        // A helper to make updating specific property instances by name easier.
        private ModuleTrackingState GetModuleTrackingState(string moduleName)
        {
            switch (moduleName)
            {
                case WellKnownModuleNames.ModuleA:
                    return this.ModuleATrackingState;
                case WellKnownModuleNames.ModuleB:
                    return this.ModuleBTrackingState;
                case WellKnownModuleNames.ModuleC:
                    return this.ModuleCTrackingState;
                case WellKnownModuleNames.ModuleD:
                    return this.ModuleDTrackingState;
                case WellKnownModuleNames.ModuleE:
                    return this.ModuleETrackingState;
                case WellKnownModuleNames.ModuleF:
                    return this.ModuleFTrackingState;
                default:
                    return null;
            }
        }
    }
}