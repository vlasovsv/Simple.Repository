using System.Collections.ObjectModel;

namespace Simple.Repository
{
    /// <summary>
    /// Provides basic operation for repository aspects.
    /// </summary>
    public interface IManageAspects
    {
        /// <summary>
        /// Adds an new aspect to the repository.
        /// </summary>
        /// <param name="aspect">The instance of the aspect.</param>
        void AddAspect(IRepositoryAspect aspect);

        /// <summary>
        /// Removes an existed aspect from the repository.
        /// </summary>
        /// <param name="aspect">The instance of the aspect.</param>
        void RemoveAspect(IRepositoryAspect aspect);

        /// <summary>
        /// Gets repository aspects.
        /// </summary>
        ReadOnlyCollection<IRepositoryAspect> Aspects
        {
            get;
        }
    }
}
