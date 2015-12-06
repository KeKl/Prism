using Autofac;
using Perspex.Controls;

namespace ModularityWithAutofac.AutofacModules
{
    public class MainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            builder.RegisterType<Shell>().SingleInstance();
        }
    }
}