using OmniXaml;
using OmniXaml.ObjectAssembler;
using OmniXaml.Parsers.ProtoParser;
using OmniXaml.Parsers.XamlInstructions;

namespace Prism.Modularity
{
    public class ModuleParserFactory : IXamlParserFactory
    {
        public ModuleParserFactory()
        {
        }

        public ModuleParserFactory(IWiringContext wiringContext)
        {
            this._wiringContext = wiringContext;
        }

        private readonly IWiringContext _wiringContext;

        public IXamlParser CreateForReadingFree()
        {
            var objectAssemblerForUndefinedRoot = GetObjectAssemblerForUndefinedRoot();

            return CreateParser(objectAssemblerForUndefinedRoot);
        }

        private IXamlParser CreateParser(IObjectAssembler objectAssemblerForUndefinedRoot)
        {
            var xamlInstructionParser = new OrderAwareXamlInstructionParser(new XamlInstructionParser(_wiringContext));

            var phaseParserKit = new PhaseParserKit(
                new XamlProtoInstructionParser(_wiringContext),
                xamlInstructionParser,
                objectAssemblerForUndefinedRoot);

            return new XamlXmlParser(phaseParserKit);
        }

        private IObjectAssembler GetObjectAssemblerForUndefinedRoot()
        {
            return new ObjectAssembler(_wiringContext, new TopDownValueContext());
        }

        public IXamlParser CreateForReadingSpecificInstance(object rootInstance)
        {
            var objectAssemblerForUndefinedRoot = GetObjectAssemblerForSpecificRoot(rootInstance);

            return CreateParser(objectAssemblerForUndefinedRoot);
        }

        private IObjectAssembler GetObjectAssemblerForSpecificRoot(object rootInstance)
        {
            return new ObjectAssembler(_wiringContext, new TopDownValueContext(), new ObjectAssemblerSettings { RootInstance = rootInstance });
        }
    }
}