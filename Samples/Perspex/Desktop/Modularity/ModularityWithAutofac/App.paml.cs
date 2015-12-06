using System;
using Autofac;
using Microsoft.Practices.ServiceLocation;
using ModularityWithAutofac.AutofacModules;
using ModuleTracking;
using Perspex.Controls;
using Perspex.Markup.Xaml;
using Perspex.Themes.Default;
using Prism;
using Prism.Autofac;
using Prism.Logging;
using Prism.Modularity;

namespace ModularityWithAutofac
{
    public class App : AutofacApplication
    {
        private readonly CallbackLogger _logger = new CallbackLogger();

        public App()
        {
            RegisterServices();
            RegisterPlatform();
            Styles = new DefaultTheme();
            InitializeComponent();
        }

        private void RegisterPlatform()
        {
            InitializeSubsystems((int) Environment.OSVersion.Platform);
        }

        private void InitializeComponent()
        {
            var loader = new PerspexXamlLoader();
            loader.Load(typeof (App), this);
        }

        /// <summary>
        /// Create the <see cref="ILoggerFacade"/> used by the bootstrapper.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// The base implementation returns a new TextLogger.
        /// </remarks>
        protected override ILoggerFacade CreateLogger()
        {
            // Because the Shell is displayed after most of the interesting boostrapper work has been performed,
            // this quickstart uses a special logger class to hold on to early log entries and display them 
            // after the UI is visible.
            return _logger;
        }

        /// <summary>
        /// Configures <see cref="ContainerBuilder"/>.
        /// </summary>
        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            base.ConfigureContainerBuilder(builder);

            builder.RegisterModule<MainModule>();
            builder.RegisterModule<ModuleTrackerModule>();
        }

        /// <summary>
        /// Returns the module catalog that will be used to initialize the modules.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="IModuleCatalog"/> that will be used to initialize the modules.
        /// </returns>
        /// <remarks>
        /// When using the default initialization behavior, this method must be overwritten by a derived class.
        /// </remarks>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new AggregateModuleCatalog();
        }

        protected override void ConfigureModuleCatalog()
        {
            // Module A is defined in the code.
            Type moduleAType = typeof(ModuleA.ModuleA);
            ModuleCatalog.AddModule(new ModuleInfo(moduleAType.Name, moduleAType.AssemblyQualifiedName));

            //// Module C is defined in the code.
            //Type moduleCType = typeof(ModuleC);
            //ModuleCatalog.AddModule(new ModuleInfo()
            //{
            //    ModuleName = moduleCType.Name,
            //    ModuleType = moduleCType.AssemblyQualifiedName,
            //    InitializationMode = InitializationMode.OnDemand
            //});

            // Module B and Module D are copied to a directory as part of a post-build step.
            // These modules are not referenced in the project and are discovered by inspecting a directory.
            // Both projects have a post-build step to copy themselves into that directory.
            DirectoryModuleCatalog directoryCatalog = new DirectoryModuleCatalog() { ModulePath = @".\DirectoryModules" };
            ((AggregateModuleCatalog)ModuleCatalog).AddCatalog(directoryCatalog);

            // Module E and Module F are defined in configuration.
            ConfigurationModuleCatalog configurationCatalog = new ConfigurationModuleCatalog();
            ((AggregateModuleCatalog)ModuleCatalog).AddCatalog(configurationCatalog);

        }

        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns>The shell of the application.</returns>
        protected override IControl CreateShell()
        {
            return ServiceLocator.Current.GetInstance<Shell>();
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        /// <remarks>
        /// The base implemention ensures the shell is composed in the container.
        /// </remarks>
        protected override void InitializeShell()
        {
            base.InitializeShell();

            var window = (Window)this.Shell;
            window.Show();

            Application.Current.Run(window);
        }
    }
}
