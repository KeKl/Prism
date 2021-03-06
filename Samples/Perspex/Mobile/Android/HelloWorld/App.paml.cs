﻿using System;
using System.Diagnostics;
using Autofac;
using HelloWorld.AutofacModules;
using HelloWorld.Logging;
using Perspex;
using Perspex.Controls;
using Perspex.Markup.Xaml;
using Perspex.Themes.Default;
using Prism.Autofac;
using Prism.Logging;
using Application = Prism.Application;

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

        private readonly SerilogLogger _logger = new SerilogLogger();

        private void RegisterPlatform()
        {
            InitializeSubsystems((int) Environment.OSVersion.Platform);
        }

        private void InitializeComponent()
        {
            var loader = new PerspexXamlLoader();
            loader.Load(typeof(App), this);
        }

        /// <summary>
        /// Creates the Logger.
        /// </summary>
        /// <returns></returns>
        protected override ILoggerFacade CreateLogger()
        {
            return new DebugLogger();
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

        protected override PerspexObject CreateMainView()
        {
            return (PerspexObject) Container.Resolve<IControl>();
        }

        /// <summary>
        /// Initializes the Shell.
        /// </summary>
        protected override void InitializeMainView()
        {
            base.InitializeMainView();

            var window = (Window)this.MainView;

            window.Show();
        }
    }
}
