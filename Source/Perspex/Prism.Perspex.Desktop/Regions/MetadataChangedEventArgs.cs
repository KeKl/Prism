using System;
using Perspex;

namespace Prism.Regions
{
    /// <summary>
    /// Provides information for a metadata change.
    /// </summary>
    public class MetadataChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the <see cref="PerspexObject"/> that the property changed on.
        /// </summary>
        /// <value>
        /// The sender object.
        /// </value>
        public PerspexObject Sender { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Perspex.PerspexPropertyChangedEventArgs"/> class.
        /// </summary>
        /// <param name="sender">The object that the property changed on.</param>
        public MetadataChangedEventArgs(PerspexObject sender)
        {
            Sender = sender;
        }
    }
}