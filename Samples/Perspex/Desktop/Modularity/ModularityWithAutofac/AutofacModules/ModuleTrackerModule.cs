using Autofac;
using ModuleTracking;

namespace ModularityWithAutofac.AutofacModules
{
    public class ModuleTrackerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ModuleTracker>().As<IModuleTracker>().SingleInstance();
        }
    }
}