using HelloWorld.Events;
using HelloWorld.ViewModels;
using Perspex;
using Perspex.Controls;
using Perspex.Markup.Xaml;
using Prism.Events;

namespace HelloWorld.Views
{
    public class MainWindow : Window
    {
        private IEventAggregator _eventAggregator;

        public MainWindow(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            this.InitializeComponent();

            HandleEvents();
        }
        
        private void InitializeComponent()
        {
            PerspexXamlLoader.Load(this);
        }

        private void HandleEvents()
        {
            _eventAggregator.GetEvent<AppEvent>().Subscribe(a =>
            {
                switch (a)
                {
                    case StandardEvents.Exit:
                        this.Close();
                        break;

                    default:
                        this.Close();
                        break;
                }
            });
        }
    }
}