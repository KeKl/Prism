using System;
using System.Linq;
using Perspex.Controls;
using Prism.Properties;

namespace Prism.Regions
{
    /// <summary>
    /// Adapter that creates a new <see cref="AllActiveRegion"/> and binds all
    /// the views to the adapted <see cref="ItemsControl"/>. 
    /// </summary>
    public class ItemsControlRegionAdapter : RegionAdapterBase<ItemsControl>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ItemsControlRegionAdapter"/>.
        /// </summary>
        /// <param name="regionBehaviorFactory">The factory used to create the region behaviors to attach to the created regions.</param>
        public ItemsControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }

        /// <summary>
        /// Adapts an <see cref="ItemsControl"/> to an <see cref="IRegion"/>.
        /// </summary>
        /// <param name="region">The new region being used.</param>
        /// <param name="regionTarget">The object to adapt.</param>
        protected override void Adapt(IRegion region, ItemsControl regionTarget)
        {
            if (region == null) throw new ArgumentNullException(nameof(region));
            if (regionTarget == null) throw new ArgumentNullException(nameof(regionTarget));

            bool itemsSourceIsSet = regionTarget.Items != null;
            // TODO this is different to the original implementation
            //itemsSourceIsSet = itemsSourceIsSet || (BindingOperations.GetBinding(regionTarget, ItemsControl.ItemsSourceProperty) != null);

            if (itemsSourceIsSet)
            {
                throw new InvalidOperationException(Resources.ItemsControlHasItemsSourceException);
            }
            
            foreach (var childItem in regionTarget.Items)
            {
                region.Add(childItem);
            }

            // TODO remove items from target?

            regionTarget.Items = region.Views;
        }

        /// <summary>
        /// Creates a new instance of <see cref="AllActiveRegion"/>.
        /// </summary>
        /// <returns>A new instance of <see cref="AllActiveRegion"/>.</returns>
        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }
    }
}