using System;
using Autofac;
using HelloWorld.AutofacModules;
using HelloWorld.Logging;
using Perspex.Controls;
using Perspex.Markup.Xaml;
using Perspex.Themes.Default;
using Prism;
using Prism.Autofac;
using Prism.Logging;

namespace HelloWorld
{
    public class App : AutofacApplication
    {
        private readonly Log4NetLogger _logger = new Log4NetLogger();

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

        protected override ILoggerFacade CreateLogger()
        {
            return _logger;
        }

        /// <summary>
        /// Configures <see cref="ContainerBuilder"/>.
        /// </summary>
        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            base.ConfigureContainerBuilder(builder);

            builder.RegisterModule<MainModule>();
        }

        protected override IControl CreateShell()
        {
            return Container.Resolve<IControl>();
        }

        /// <summary>
        /// Initializes the Shell.
        /// </summary>
        protected override void InitializeShell()
        {
            base.InitializeShell();

            var window = (Window)this.Shell;

            //var regionManager = Container.Resolve<IRegionManager>();
            //regionManager.RegisterViewWithRegion(RegionNames.TopNavigationRegionName, typeof(ControlView));

            ////regionManager.RegisterViewWithRegion(RegionNames.ContentRegionName, typeof(ShellDockingManager));

            window.Show();

            Application.Current.Run(window);
        }
    }
}
