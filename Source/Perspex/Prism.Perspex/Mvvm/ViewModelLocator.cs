﻿using System;
using Perspex;
using Perspex.Controls;

namespace Prism.Mvvm
{
    /// <summary>
    /// This class defines the attached property and related change handler that calls the <see cref="ViewModelLocationProvider"/>.
    /// </summary>
    public static class ViewModelLocator
    {
        static ViewModelLocator()
        {
            // TODO dispose?
            var d = AutoWireViewModelProperty.Changed.Subscribe(args =>
            {
                if ((bool)args.NewValue)
                    ViewModelLocationProvider.AutoWireViewModelChanged(args.Sender, Bind);
            });
        }

        /// <summary>
        /// The AutoWireViewModel attached property.
        /// </summary>
        public static readonly PerspexProperty<bool> AutoWireViewModelProperty =
            PerspexProperty.RegisterAttached<Control, bool>("AutoWireViewModel", typeof(ViewModelLocator));

        public static bool GetAutoWireViewModel(Control control)
        {
            return control.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(Control control, bool value)
        {
            control.SetValue(AutoWireViewModelProperty, value);
        }
        
        /// <summary>
        /// Sets the DataContext of a View.
        /// </summary>
        /// <param name="view">The View to set the on.</param>
        /// <param name="context">The object to use as the DataContext for the View.</param>
        static void Bind(object view, object context)
        {
            var control = view as IControl;
            if (control != null)
                control.DataContext = context;
        }
    }
}