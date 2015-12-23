using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Perspex;
using Perspex.Controls;

namespace Prism.Regions
{
    /// <summary>
    /// Defines a class that wraps an item and adds metadata for it.
    /// </summary>
    public class ItemMetadata : PerspexObject
    {
        static ItemMetadata()
        {
            // TODO dispose?
            var d = IsActiveProperty.Changed.Subscribe(
                args => (args.Sender as ItemMetadata)?.InvokeMetadataChanged());
        }

        /// <summary>
        /// The name of the wrapped item.
        /// </summary>
        public static readonly PerspexProperty<string> NameProperty =
            PerspexProperty.Register<ItemMetadata, string>(nameof(Name));

        /// <summary>
        /// Value indicating whether the wrapped item is considered active.
        /// </summary>
        public static readonly PerspexProperty<bool> IsActiveProperty =
            PerspexProperty.Register<ItemMetadata, bool>(nameof(IsActive));
        
        /// <summary>
        /// Initializes a new instance of <see cref="ItemMetadata"/>.
        /// </summary>
        /// <param name="item">The item to wrap.</param>
        public ItemMetadata(object item)
        {
            // check for null
            this.Item = item;
        }

        private readonly Subject<MetadataChangedEventArgs> _metadataChanged = new Subject<MetadataChangedEventArgs>();

        /// <summary>
        /// Gets the wrapped item.
        /// </summary>
        /// <value>The wrapped item.</value>
        public object Item { get; private set; }

        /// <summary>
        /// Gets or sets a name for the wrapped item.
        /// </summary>
        /// <value>The name of the wrapped item.</value>
        public string Name
        {
            get { return GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the wrapped item is considered active.
        /// </summary>
        /// <value><see langword="true" /> if the item should be considered active; otherwise <see langword="false" />.</value>
        public bool IsActive
        {
            get { return GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        /// <summary>
        /// Occurs when metadata on the item changes.
        /// </summary>
        public IObservable<MetadataChangedEventArgs> MetadataChanged => this._metadataChanged.AsObservable();
        
        /// <summary>
        /// Explicitly invokes <see cref="MetadataChanged"/> to notify listeners.
        /// </summary>
        public void InvokeMetadataChanged()
        {
            // TODO correct?
            _metadataChanged.OnNext(
                new MetadataChangedEventArgs(this, IsActiveProperty));
        }
    }
}