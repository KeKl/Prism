using System;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using OmniXaml.ObjectAssembler.Commands;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Perspex.Tests.Mocks.ViewModels;
using Prism.Perspex.Tests.Mocks.Views;

namespace Prism.Perspex.Tests
{
    [TestFixture]
    public class ApplicationFixture
    {
        private readonly object _lock = new object();
        private readonly DefaultApp _app = new DefaultApp();

        [Test]
        public void CreateLogger()
        {
            var app = _app;

            lock (_lock)
            {
                Assert.Null(_app.BaseLogger);

                app.CallCreateLogger();

                Assert.NotNull(app.BaseLogger);

                Assert.IsInstanceOf<DebugLogger>(app.BaseLogger);
            }
        }

        [Test]
        public void ConfigureViewModelLocatorShouldUserServiceLocatorAsResolver()
        {
            var app = _app;

            CreateAndConfigureServiceLocatorForViewModelLocator();

            app.CallConfigureViewModelLocator();

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

        protected override void ConfigureServiceLocator()
        {
            throw new NotImplementedException();
        }
    }
}
