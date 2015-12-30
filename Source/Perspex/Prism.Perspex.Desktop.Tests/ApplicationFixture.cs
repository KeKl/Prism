using System;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using Perspex;
using Perspex.Controls;
using Prism.Logging;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Perspex.Desktop.Tests.Mocks.ViewModels;
using Prism.Perspex.Desktop.Tests.Mocks.Views;
using Prism.Regions;

namespace Prism.Perspex.Desktop.Tests
{
    [TestFixture]
    public class BootstrapperFixture
    {
        private DefaultApplication _app = new DefaultApplication();
        
        [Test]
        public void ShellDefaultsToNull()
        {
            Assert.IsNull(_app.BaseMainView);
        }

        [Test]
        public void CreateLoggerInitializesLogger()
        {
            _app.CallCreateLogger();

            Assert.IsNotNull(_app.BaseLogger);

            Assert.IsInstanceOf<DebugLogger>(_app.BaseLogger);
        }

        [Test]
        public void ConfigureViewModelLocatorShouldUserServiceLocatorAsResolver()
        {
            CreateAndConfigureServiceLocatorForViewModelLocator();

            _app.CallConfigureViewModelLocator();

            var view = new MockView();

            ViewModelLocationProvider.AutoWireViewModelChanged(view, (v, vm) =>
                {
                    Assert.IsNotNull(v);
                    Assert.IsNotNull(vm);
                    Assert.IsInstanceOf<MockViewModel>(vm);
                });
        }

        private static void CreateAndConfigureServiceLocatorForViewModelLocator()
        {
            var serviceLocator = new ServiceLocatorExtensionsFixture.MockServiceLocator(
                () => new MockViewModel());
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }

        [Test]
        public void CreateModuleCatalogShouldInitializeModuleCatalog()
        {
            Assert.IsNull(_app.BaseModuleCatalog);

            _app.CallCreateModuleCatalog();

            Assert.IsNotNull(_app.BaseModuleCatalog);
        }

        [Test]
        public void RegisterFrameworkExceptionTypesShouldRegisterActivationException()
        {
            _app.CallRegisterFrameworkExceptionTypes();

            Assert.IsTrue(ExceptionExtensions.IsFrameworkExceptionRegistered(
                typeof(ActivationException)));
        }

        //[Test]
        //public void ConfigureRegionAdapterMappingsShouldRegisterItemsControlMapping()
        //{
        //    var bootstrapper = new DefaultApplication();

        //    CreateAndConfigureServiceLocatorWithRegionAdapters();

        //    var regionAdapterMappings = bootstrapper.CallConfigureRegionAdapterMappings();

        //    Assert.IsNotNull(regionAdapterMappings);
        //    Assert.IsNotNull(regionAdapterMappings.GetMapping(typeof(ItemsControl)));
        //}

        //[Test]
        //public void ConfigureRegionAdapterMappingsShouldRegisterContentControlMapping()
        //{
        //    var bootstrapper = new DefaultApplication();

        //    CreateAndConfigureServiceLocatorWithRegionAdapters();

        //    var regionAdapterMappings = bootstrapper.CallConfigureRegionAdapterMappings();

        //    Assert.IsNotNull(regionAdapterMappings);
        //    Assert.IsNotNull(regionAdapterMappings.GetMapping(typeof(ContentControl)));
        //}

        //[Test]
        //public void ConfigureRegionAdapterMappingsShouldRegisterSelectorMapping()
        //{
        //    var bootstrapper = new DefaultApplication();

        //    CreateAndConfigureServiceLocatorWithRegionAdapters();

        //    var regionAdapterMappings = bootstrapper.CallConfigureRegionAdapterMappings();

        //    Assert.IsNotNull(regionAdapterMappings);
        //    Assert.IsNotNull(regionAdapterMappings.GetMapping(typeof(Selector)));
        //}

        //private static void CreateAndConfigureServiceLocatorWithRegionAdapters()
        //{
        //    Mock<ServiceLocatorImplBase> serviceLocator = new Mock<ServiceLocatorImplBase>();
        //    var regionBehaviorFactory = new RegionBehaviorFactory(serviceLocator.Object);
        //    serviceLocator.Setup(sl => sl.GetInstance<RegionAdapterMappings>()).Returns(new RegionAdapterMappings());
        //    serviceLocator.Setup(sl => sl.GetInstance<SelectorRegionAdapter>()).Returns(new SelectorRegionAdapter(regionBehaviorFactory));
        //    serviceLocator.Setup(sl => sl.GetInstance<ItemsControlRegionAdapter>()).Returns(new ItemsControlRegionAdapter(regionBehaviorFactory));
        //    serviceLocator.Setup(sl => sl.GetInstance<ContentControlRegionAdapter>()).Returns(new ContentControlRegionAdapter(regionBehaviorFactory));

        //    ServiceLocator.SetLocatorProvider(() => serviceLocator.Object);
        //}

        //[Test]
        //public void ConfigureDefaultRegionBehaviorsShouldAddSevenDefaultBehaviors()
        //{
        //    var bootstrapper = new DefaultApplication();

        //    CreateAndConfigureServiceLocatorWithDefaultRegionBehaviors();

        //    bootstrapper.CallConfigureDefaultRegionBehaviors();

        //    Assert.AreEqual(7, bootstrapper.DefaultRegionBehaviorTypes.Count());
        //}

        //private static void CreateAndConfigureServiceLocatorWithDefaultRegionBehaviors()
        //{
        //    Mock<ServiceLocatorImplBase> serviceLocator = new Mock<ServiceLocatorImplBase>();
        //    var regionBehaviorFactory = new RegionBehaviorFactory(serviceLocator.Object);
        //    serviceLocator.Setup(sl => sl.GetInstance<IRegionBehaviorFactory>()).Returns(new RegionBehaviorFactory(serviceLocator.Object));

        //    ServiceLocator.SetLocatorProvider(() => serviceLocator.Object);
        //}

        //[Test]
        //public void ConfigureDefaultRegionBehaviorsShouldAddAutoPopulateRegionBehavior()
        //{
        //    var bootstrapper = new DefaultApplication();

        //    CreateAndConfigureServiceLocatorWithDefaultRegionBehaviors();

        //    bootstrapper.CallConfigureDefaultRegionBehaviors();

        //    Assert.IsTrue(bootstrapper.DefaultRegionBehaviorTypes.ContainsKey(AutoPopulateRegionBehavior.BehaviorKey));
        //}

        //[Test]
        //public void ConfigureDefaultRegionBehaviorsShouldBindRegionContextToDependencyObjectBehavior()
        //{
        //    var bootstrapper = new DefaultApplication();

        //    CreateAndConfigureServiceLocatorWithDefaultRegionBehaviors();

        //    bootstrapper.CallConfigureDefaultRegionBehaviors();

        //    Assert.IsTrue(bootstrapper.DefaultRegionBehaviorTypes.ContainsKey(BindRegionContextToDependencyObjectBehavior.BehaviorKey));
        //}

        //[Test]
        //public void ConfigureDefaultRegionBehaviorsShouldAddRegionActiveAwareBehavior()
        //{
        //    var bootstrapper = new DefaultApplication();

        //    CreateAndConfigureServiceLocatorWithDefaultRegionBehaviors();

        //    bootstrapper.CallConfigureDefaultRegionBehaviors();

        //    Assert.IsTrue(bootstrapper.DefaultRegionBehaviorTypes.ContainsKey(RegionActiveAwareBehavior.BehaviorKey));
        //}

        //[Test]
        //public void ConfigureDefaultRegionBehaviorsShouldAddSyncRegionContextWithHostBehavior()
        //{
        //    var bootstrapper = new DefaultApplication();

        //    CreateAndConfigureServiceLocatorWithDefaultRegionBehaviors();

        //    bootstrapper.CallConfigureDefaultRegionBehaviors();

        //    Assert.IsTrue(bootstrapper.DefaultRegionBehaviorTypes.ContainsKey(SyncRegionContextWithHostBehavior.BehaviorKey));
        //}

        //[Test]
        //public void ConfigureDefaultRegionBehaviorsShouldAddRegionManagerRegistrationBehavior()
        //{
        //    var bootstrapper = new DefaultApplication();

        //    CreateAndConfigureServiceLocatorWithDefaultRegionBehaviors();

        //    bootstrapper.CallConfigureDefaultRegionBehaviors();

        //    Assert.IsTrue(bootstrapper.DefaultRegionBehaviorTypes.ContainsKey(RegionManagerRegistrationBehavior.BehaviorKey));
        //}

        //[Test]
        //public void ConfigureDefaultRegionBehaviorsShouldAddRegionLifetimeBehavior()
        //{
        //    var bootstrapper = new DefaultApplication();

        //    CreateAndConfigureServiceLocatorWithDefaultRegionBehaviors();

        //    bootstrapper.CallConfigureDefaultRegionBehaviors();

        //    Assert.IsTrue(bootstrapper.DefaultRegionBehaviorTypes.ContainsKey(RegionMemberLifetimeBehavior.BehaviorKey));
        //}
    }

    internal class DefaultApplication : Application
    {
        public IRegionBehaviorFactory DefaultRegionBehaviorTypes;

        public ILoggerFacade BaseLogger
        {
            get
            {
                return base.Logger;
            }
        }

        public IModuleCatalog BaseModuleCatalog
        {
            get { return base.ModuleCatalog; }
            set { base.ModuleCatalog = value; }
        }

        public PerspexObject BaseMainView
        {
            get { return base.MainView; }
            set { base.MainView = value; }
        }

        public void CallCreateLogger()
        {
            this.Logger = base.CreateLogger();
        }

        public void CallCreateModuleCatalog()
        {
            this.ModuleCatalog = base.CreateModuleCatalog();
        }

        //public RegionAdapterMappings CallConfigureRegionAdapterMappings()
        //{
        //    return base.ConfigureRegionAdapterMappings();
        //}

        public void CallConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
        }

        public override void Run(bool runWithDefaultConfiguration)
        {
            throw new NotImplementedException();
        }
        
        protected override void ConfigureServiceLocator()
        {
            throw new NotImplementedException();
        }

        public void CallRegisterFrameworkExceptionTypes()
        {
            base.RegisterFrameworkExceptionTypes();
        }

        //public IRegionBehaviorFactory CallConfigureDefaultRegionBehaviors()
        //{
        //    this.DefaultRegionBehaviorTypes = base.ConfigureDefaultRegionBehaviors();
        //    return this.DefaultRegionBehaviorTypes;
        //}
    }

}
