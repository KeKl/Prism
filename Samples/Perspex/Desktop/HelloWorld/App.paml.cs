using System;
using Autofac;
using HelloWorld.AutofacModules;
using HelloWorld.Logging;
using Perspex.Controls;
using Perspex.Markup.Xaml;
using Perspex.Themes.Default;
using Prism;
using Prism.Autofac;
using Prism.Events;
using Prism.Logging;

namespace HelloWorld
{
    public class App : AutofacApplication
    {
        /// <summary>
        /// Creates new instance of App.
        /// </summary>
        public App()
        {
            RegisterServices();
            RegisterPlatform();
            Styles = new DefaultTheme();
            InitializeComponent();
        }

        private readonly Log4NetLogger _logger = new Log4NetLogger();

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
        /// Creates the Logger.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Creates the shell.
        /// </summary>
        /// <returns></returns>
        protected override IControl CreateMainView()
        {
            return Container.Resolve<IControl>();
        }

        /// <summary>
        /// Initializes the MainView.
        /// </summary>
        protected override void InitializeMainView()
        {
            base.InitializeMainView();

            var window = (Window)this.MainView;

            window.Show();

            Application.Current.Run(window);
        }
    }
}
