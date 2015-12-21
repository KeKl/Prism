using HelloWorld.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace HelloWorld.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private string _temp = "Hello You!";
        private string _testText;
        
        public string TestText
        {
            get { return _testText; }
            set
            {
                SetProperty(ref _testText, value);
            }
        }

        public DelegateCommand ExitCommand { get; private set; }

        public DelegateCommand ChangeTextCommand { get; private set; }

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            TestText = "Hello World!";

            ExitCommand = new DelegateCommand(() => eventAggregator.GetEvent<AppEvent>().Publish(StandardEvents.Exit));

            ChangeTextCommand = new DelegateCommand(() =>
            {
                var temp = TestText;
                TestText = _temp;
                _temp = temp;
            });
        }
    }
}