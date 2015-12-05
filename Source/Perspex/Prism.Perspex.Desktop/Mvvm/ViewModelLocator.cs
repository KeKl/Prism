using System;
using System.Drawing;
using System.Reactive;
using Perspex;
using Perspex.Controls;

namespace Prism.Mvvm
{
    /// <summary>
    /// This class defines the attached property and related change handler that calls the <see cref="Prism.Mvvm.ViewModelLocationProvider"/>.
    /// </summary>
    public static class ViewModelLocator
    {
        static ViewModelLocator()
        {
            AutoWireViewModelProperty.Changed.Subscribe(args =>
            {
                if ((bool)args.NewValue)
                    ViewModelLocationProvider.AutoWireViewModelChanged(args.Sender, Bind);
            });
        }
        
        public static readonly PerspexProperty<bool> AutoWireViewModelProperty =
            PerspexProperty.RegisterAttached<IControl, bool>("AutoWireViewModel", typeof(ViewModelLocator));

        public static bool GetAutoWireViewModel(IControl obj)
        {
            return obj.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(IControl obj, bool value)
        {
            obj.SetValue(AutoWireViewModelProperty, value);
        }
        
        /// <summary>
        /// Sets the DataContext of a View.
        /// </summary>
        /// <param name="view">The View to set the on.</param>
        /// <param name="viewModel">The object to use as the DataContext for the View</param>
        static void Bind(object view, object viewModel)
        {
            var control = view as IControl;
            if (control != null)
                control.DataContext = viewModel;
        }
    }
}