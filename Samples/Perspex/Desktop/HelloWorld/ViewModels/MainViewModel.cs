using Prism.Mvvm;

namespace HelloWorld.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private string _testText;

        public string TestText
        {
            get { return _testText; }
            set
            {
                SetProperty(ref _testText, value);
            }
        }

        public MainViewModel()
        {
            TestText = "Hello World!";
        }
    }
}