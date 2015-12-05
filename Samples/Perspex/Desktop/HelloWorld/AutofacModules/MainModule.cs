using Autofac;
using HelloWorld.ViewModels;
using HelloWorld.Views;
using Perspex.Controls;

namespace HelloWorld.AutofacModules
{
    public class MainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            builder.RegisterType<MainWindow>().As<IControl>().SingleInstance();
            builder.RegisterType<MainViewModel>().SingleInstance();
        }
    }
}