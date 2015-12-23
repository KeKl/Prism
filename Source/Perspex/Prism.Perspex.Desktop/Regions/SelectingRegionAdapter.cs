using Perspex.Controls.Primitives;
using Prism.Regions.Behaviors;

namespace Prism.Regions
{
    /// <summary>
    /// Adapter that creates a new <see cref="Region"/> and binds all
    /// the views to the adapted <see cref="SelectingItemsControl"/>.
    /// It also keeps the <see cref="IRegion.ActiveViews"/> and the selected items
    /// of the <see cref="SelectingItemsControl"/> in sync.
    /// </summary>
    public class SelectingRegionAdapter : RegionAdapterBase<SelectingItemsControl>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SelectingRegionAdapter"/>.
        /// </summary>
        /// <param name="regionBehaviorFactory">The factory used to create the region behaviors to attach to the created regions.</param>
        public SelectingRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }

        /// <summary>
        /// Adapts an <see cref="SelectingItemsControl"/> to an <see cref="IRegion"/>.
        /// </summary>
        /// <param name="region">The new region being used.</param>
        /// <param name="regionTarget">The object to adapt.</param>
        protected override void Adapt(IRegion region, SelectingItemsControl regionTarget)
        {
        }

        /// <summary>
        /// Attach new behaviors.
        /// </summary>
        /// <param name="region">The region being used.</param>
        /// <param name="regionTarget">The object to adapt.</param>
        /// <remarks>
        /// This class attaches the base behaviors and also listens for changes in the
        /// activity of the region or the control selection and keeps the in sync.
        /// </remarks>
        protected override void AttachBehaviors(IRegion region, SelectingItemsControl regionTarget)
        {
            if (region == null) throw new System.ArgumentNullException(nameof(region));
            // Add the behavior that syncs the items source items with the rest of the items
            region.Behaviors.Add(SelectingItemsSourceSyncBehavior.BehaviorKey, new SelectingItemsSourceSyncBehavior()
                                                                                  {
                                                                                      HostControl = regionTarget
                                                                                  });

            base.AttachBehaviors(region, regionTarget);
        }

        /// <summary>
        /// Creates a new instance of <see cref="Region"/>.
        /// </summary>
        /// <returns>A new instance of <see cref="Region"/>.</returns>
        protected override IRegion CreateRegion()
        {
            return new Region();
        }
    }
}