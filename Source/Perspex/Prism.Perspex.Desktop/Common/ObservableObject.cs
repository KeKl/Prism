

using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Perspex;
using Perspex.Controls;
using Prism.Mvvm;

namespace Prism.Common
{
    /// <summary>
    /// Class that wraps an object, so that other classes can notify for Change events. Typically, this class is set as 
    /// a Dependency Property on DependencyObjects, and allows other classes to observe any changes in the Value. 
    /// </summary>
    /// <remarks>
    /// This class is required, because in Silverlight, it's not possible to receive Change notifications for Dependency properties that you do not own. 
    /// </remarks>
    /// <typeparam name="T">The type of the property that's wrapped in the Observable object</typeparam>
    public class ObservableObject<T> : Control, INotifyPropertyChanged
    {
        static ObservableObject()
        {
            // TODO dispose?
            var d = ValueProperty.Changed.Subscribe(args =>
            {
                ValueChangedCallback(args.Sender, args);
            });
        }

        /// <summary>
        /// Identifies the Value property of the ObservableObject
        /// </summary>
        public static readonly PerspexProperty<T> ValueProperty =
            PerspexProperty.Register<ObservableObject<T>, T>(nameof(Value));

        /// <summary>
        /// The value that's wrapped inside the ObservableObject.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public T Value
        {
            get { return this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }

        private readonly Subject<PerspexPropertyChangedEventArgs> _propertyChanged = new Subject<PerspexPropertyChangedEventArgs>();

        public IObservable<PerspexPropertyChangedEventArgs> ValuePropertyChanged
        {
            get { return this._propertyChanged.AsObservable(); }
        }

        private static void ValueChangedCallback(PerspexObject d, PerspexPropertyChangedEventArgs e)
        {
            ((ObservableObject<T>)d)._propertyChanged.OnNext(e);
        }
    }
}