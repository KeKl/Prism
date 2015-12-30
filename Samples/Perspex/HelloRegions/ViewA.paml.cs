using Perspex.Controls;
using Perspex.Markup.Xaml;

namespace HelloRegions
{
    public class ViewA : UserControl
    {
        public ViewA()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            PerspexXamlLoader.Load(this);
        }
    }
}
