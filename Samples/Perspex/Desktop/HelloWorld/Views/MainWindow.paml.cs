using HelloWorld.ViewModels;
using Perspex;
using Perspex.Controls;
using Perspex.Markup.Xaml;

namespace HelloWorld.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        // With autofac DI
        //public MainWindow(MainViewModel viewModel)
        //{
        //    this.InitializeComponent();

        //    DataContext = viewModel;
        //}

        private void InitializeComponent()
        {
            PerspexXamlLoader.Load(this);
        }
    }
}