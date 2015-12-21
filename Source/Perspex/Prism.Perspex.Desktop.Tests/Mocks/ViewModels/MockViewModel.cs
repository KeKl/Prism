using Prism.Mvvm;

namespace Prism.Perspex.Desktop.Tests.Mocks.ViewModels
{
    public class MockViewModel : BindableBase
    {
        private int _mockProperty;

        public int MockProperty
        {
            get
            {
                return this._mockProperty;
            }

            set
            {
                this.SetProperty(ref _mockProperty, value);
            }
        }

        internal void InvokeOnPropertyChanged()
        {
            this.OnPropertyChanged(() => this.MockProperty);
        }
    }
}
