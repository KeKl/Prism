using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Prism.Modularity;

namespace Prism.Perspex.Desktop.Tests.Modularity
{
    [TestFixture]
    public class ModuleCatalogFixture
    {
        [Test]
        public void CanLoadCatalogFromXaml()
        {
            Stream stream =
                Assembly.GetExecutingAssembly().GetManifestResourceStream(
                    "Prism.Perspex.Desktop.Tests.Modularity.ModuleCatalogXaml.SimpleModuleCatalog.xaml");

            var catalog = ModuleCatalog.CreateFromXaml(stream);
            Assert.IsNotNull(catalog);

            Assert.AreEqual(4, catalog.Modules.Count());
        }

        [Test]
        public void CanLoadCatalogFromXamlFromUri()
        {
            var uri = new Uri("pack://application:,,,/Modularity/ModuleCatalogXaml/SimpleModuleCatalog.xaml");
            
            var catalog = ModuleCatalog.CreateFromXaml(uri);
            Assert.IsNotNull(catalog);

            Assert.AreEqual(4, catalog.Modules.Count());
        }
    }
}
