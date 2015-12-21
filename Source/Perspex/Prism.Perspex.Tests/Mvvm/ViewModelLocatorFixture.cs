using System;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Prism.Mvvm;
using Prism.Perspex.Tests.Mocks.ViewModels;
using Prism.Perspex.Tests.Mocks.Views;

namespace Prism.Perspex.Tests.Mvvm
{
    [TestFixture]
    public class ViewModelLocatorFixture
    {
        [Test]
        public void ShouldLocateViewModelWithDefaultSettings()
        {
            ResetViewModelLocationProvider();

            Mock view = new Mock();
            Assert.IsNull(view.DataContext);

            ViewModelLocator.SetAutoWireViewModel(view, true);
            Assert.IsNotNull(view.DataContext);
            Assert.IsInstanceOf<MockViewModel>(view.DataContext);
        }

        [Test]
        public void ShouldLocateViewModelWithDefaultSettingsForViewsThatEndWithView()
        {
            ResetViewModelLocationProvider();

            MockView view = new MockView();
            Assert.IsNull(view.DataContext);

            ViewModelLocator.SetAutoWireViewModel(view, true);
            Assert.IsNotNull(view.DataContext);
            Assert.IsInstanceOf<MockViewModel>(view.DataContext);
        }

        [Test]
        public void ShouldUseCustomDefaultViewModelFactoryWhenSet()
        {
            ResetViewModelLocationProvider();

            Mock view = new Mock();
            Assert.IsNull(view.DataContext);

            object mockObject = new object();
            ViewModelLocationProvider.SetDefaultViewModelFactory(viewType => mockObject);

            ViewModelLocator.SetAutoWireViewModel(view, true);
            Assert.IsNotNull(view.DataContext);
            ReferenceEquals(view.DataContext, mockObject);
        }

        [Test]
        public void ShouldUseCustomDefaultViewTypeToViewModelTypeResolverWhenSet()
        {
            ResetViewModelLocationProvider();

            Mock view = new Mock();
            Assert.IsNull(view.DataContext);

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType => typeof(ViewModelLocatorFixture));

            ViewModelLocator.SetAutoWireViewModel(view, true);            
            Assert.IsNotNull(view.DataContext);
            Assert.IsInstanceOf<ViewModelLocatorFixture>(view.DataContext);
        }

        [Test]
        public void ShouldUseCustomFactoryWhenSet()
        {
            ResetViewModelLocationProvider();

            Mock view = new Mock();
            Assert.IsNull(view.DataContext);

            string viewModel = "Test String";
            ViewModelLocationProvider.Register(view.GetType().ToString(), () => viewModel);
            
            ViewModelLocator.SetAutoWireViewModel(view, true);
            Assert.IsNotNull(view.DataContext);
            ReferenceEquals(view.DataContext, viewModel);
        }

        private static void ResetViewModelLocationProvider()
        {
            Type staticType = typeof(ViewModelLocationProvider);
            ConstructorInfo ci = staticType.TypeInitializer;
            object[] parameters = new object[0];
            ci.Invoke(null, parameters);
        }
    }
}
