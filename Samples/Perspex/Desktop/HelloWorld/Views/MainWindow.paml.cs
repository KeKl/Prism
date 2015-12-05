using HelloWorld.ViewModels;
using Perspex.Controls;
using Perspex.Markup.Xaml;

namespace HelloWorld.Views
{
    public class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            this.InitializeComponent();

            DataContext = viewModel;
        }

        private void InitializeComponent()
        {
            PerspexXamlLoader.Load(this);
        }
    }
}