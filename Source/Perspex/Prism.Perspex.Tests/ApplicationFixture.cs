using System;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Perspex.Tests.Mocks.ViewModels;
using Prism.Perspex.Tests.Mocks.Views;

namespace Prism.Perspex.Tests
{
    [TestFixture]
    public class BootstrapperFixture
    {
        [Test]
        public void LoggerDefaultsToNull()
        {
            var bootstrapper = new DefaultApp();
            Assert.Null(bootstrapper.BaseLogger);
        }

        [Test]
        public void CreateLoggerInitializesLogger()
        {
            var bootstrapper = new DefaultApp();
            bootstrapper.CallCreateLogger();

            Assert.NotNull(bootstrapper.BaseLogger);

            Assert.IsInstanceOf<DebugLogger>(bootstrapper.BaseLogger);
        }

        [Test]
        public void ConfigureViewModelLocatorShouldUserServiceLocatorAsResolver()
        {
            var bootstrapper = new DefaultApp();

            CreateAndConfigureServiceLocatorForViewModelLocator();

            bootstrapper.CallConfigureViewModelLocator();

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
            var serviceLocator = new ServiceLocatorExtensionsFixture.MockServiceLocator(() => new MockViewModel());
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }
    }

    internal class DefaultApp : ApplicationBase
    {
        public ILoggerFacade BaseLogger
        {
            get
            {
                return base.Logger;
            }
        }

        public void CallCreateLogger()
        {
            this.Logger = base.CreateLogger();
        }

        public void CallConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}
