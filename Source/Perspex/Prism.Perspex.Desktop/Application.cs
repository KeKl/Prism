﻿using System;
using Microsoft.Practices.ServiceLocation;
using Perspex.Controls;
using Prism.Modularity;
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
        /// Gets the default <see cref="IModuleCatalog"/> for the application.
        /// </summary>
        /// <value>The default <see cref="IModuleCatalog"/> instance.</value>
        protected IModuleCatalog ModuleCatalog { get; set; }

        /// <summary>
        /// Gets the shell user interface.
        /// </summary>
        /// <value>The shell user interface.</value>
        protected IControl Shell { get; set; }

        /// <summary>
        /// Runs the bootstrapper process.
        /// </summary>
        public override void Run()
        {
            this.Run(true);
        }

        /// <summary>
        /// Creates the <see cref="IModuleCatalog"/> used by Prism.
        /// </summary>
        ///  <remarks>
        /// The base implementation returns a new ModuleCatalog.
        /// </remarks>
        protected virtual IModuleCatalog CreateModuleCatalog()
        {
            throw new NotImplementedException();
            //return new ModuleCatalog();
        }

        /// <summary>
        /// Configures the <see cref="IModuleCatalog"/> used by Prism.
        /// </summary>
        protected virtual void ConfigureModuleCatalog()
        {
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
        /// Initializes the modules. May be overwritten in a derived class to use a custom Modules Catalog
        /// </summary>
        protected virtual void InitializeModules()
        {
            IModuleManager manager = ServiceLocator.Current.GetInstance<IModuleManager>();
            manager.Run();
        }

        ///// <summary>
        ///// Configures the default region adapter mappings to use in the application, in order
        ///// to adapt UI controls defined in XAML to use a region and register it automatically.
        ///// May be overwritten in a derived class to add specific mappings required by the application.
        ///// </summary>
        ///// <returns>The <see cref="RegionAdapterMappings"/> instance containing all the mappings.</returns>
        //protected virtual RegionAdapterMappings ConfigureRegionAdapterMappings()
        //{
        //    RegionAdapterMappings regionAdapterMappings = ServiceLocator.Current.GetInstance<RegionAdapterMappings>();
        //    if (regionAdapterMappings != null)
        //    {
        //        regionAdapterMappings.RegisterMapping(typeof(Selector), ServiceLocator.Current.GetInstance<SelectorRegionAdapter>());
        //        regionAdapterMappings.RegisterMapping(typeof(ItemsControl), ServiceLocator.Current.GetInstance<ItemsControlRegionAdapter>());
        //        regionAdapterMappings.RegisterMapping(typeof(ContentControl), ServiceLocator.Current.GetInstance<ContentControlRegionAdapter>());
        //    }

        //    return regionAdapterMappings;
        //}

        ///// <summary>
        ///// Configures the <see cref="IRegionBehaviorFactory"/>. 
        ///// This will be the list of default behaviors that will be added to a region. 
        ///// </summary>
        //protected virtual IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        //{
        //    var defaultRegionBehaviorTypesDictionary = ServiceLocator.Current.GetInstance<IRegionBehaviorFactory>();

        //    if (defaultRegionBehaviorTypesDictionary != null)
        //    {
        //        defaultRegionBehaviorTypesDictionary.AddIfMissing(BindRegionContextToDependencyObjectBehavior.BehaviorKey,
        //                                                          typeof(BindRegionContextToDependencyObjectBehavior));

        //        defaultRegionBehaviorTypesDictionary.AddIfMissing(RegionActiveAwareBehavior.BehaviorKey,
        //                                                          typeof(RegionActiveAwareBehavior));

        //        defaultRegionBehaviorTypesDictionary.AddIfMissing(SyncRegionContextWithHostBehavior.BehaviorKey,
        //                                                          typeof(SyncRegionContextWithHostBehavior));

        //        defaultRegionBehaviorTypesDictionary.AddIfMissing(RegionManagerRegistrationBehavior.BehaviorKey,
        //                                                          typeof(RegionManagerRegistrationBehavior));

        //        defaultRegionBehaviorTypesDictionary.AddIfMissing(RegionMemberLifetimeBehavior.BehaviorKey,
        //                                          typeof(RegionMemberLifetimeBehavior));

        //        defaultRegionBehaviorTypesDictionary.AddIfMissing(ClearChildViewsRegionBehavior.BehaviorKey,
        //                                          typeof(ClearChildViewsRegionBehavior));

        //        defaultRegionBehaviorTypesDictionary.AddIfMissing(AutoPopulateRegionBehavior.BehaviorKey,
        //                                          typeof(AutoPopulateRegionBehavior));
        //    }

        //    return defaultRegionBehaviorTypesDictionary;
        //}

        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns>The shell of the application.</returns>
        /// <remarks>
        /// If the returned instance is a <see cref="IControl"/>, the
        /// <see cref="Application"/> will attach the default <see cref="IRegionManager"/> of
        /// the application in its <see cref="RegionManager.RegionManagerProperty"/> attached property
        /// in order to be able to add regions by using the <see cref="RegionManager.RegionNameProperty"/>
        /// attached property from XAML.
        /// </remarks>
        protected virtual IControl CreateShell()
        {
            return null;
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        protected virtual void InitializeShell()
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