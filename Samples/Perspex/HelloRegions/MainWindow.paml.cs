using System.Windows.Forms;
using Perspex.Controls;
using Perspex.Markup.Xaml;

namespace HelloRegions
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            PerspexXamlLoader.Load(this);
        }
    }
}
