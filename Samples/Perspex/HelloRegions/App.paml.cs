using System;
using Autofac;
using Autofac.Core;
using Perspex;
using Perspex.Controls;
using Perspex.Markup.Xaml;
using Perspex.Themes.Default;
using Prism.Autofac;
using Prism.Logging;
using Prism.Regions;

namespace HelloRegions
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
        /// Creates the shell.
        /// </summary>
        /// <returns></returns>
        protected override PerspexObject CreateMainView()
        {
            return new MainWindow();
        }

        /// <summary>
        /// Initializes the MainView.
        /// </summary>
        protected override void InitializeMainView()
        {
            var window = (Window)this.MainView;

            var regionManager = Container.Resolve<RegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof (ViewA));

            window.Show();
        }
    }
}
