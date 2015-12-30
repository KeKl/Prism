using System.IO;
using OmniXaml;
using OmniXaml.Services.DotNetFx;

namespace Prism.Modularity
{
    public class ModuleXamlLoader : IXamlLoader
    {
        public ModuleXamlLoader(IWiringContext wiringContext)
        {
            IXamlParserFactory parserFactory = new ModuleParserFactory(wiringContext);
            _xamlXmlLoader = new XamlXmlLoader(parserFactory);
        }

        private readonly XamlXmlLoader _xamlXmlLoader;

        public object Load(Stream stream)
        {
            return _xamlXmlLoader.Load(stream);
        }

        public object Load(Stream stream, object instance)
        {
            return _xamlXmlLoader.Load(stream, instance);
        }
    }
}