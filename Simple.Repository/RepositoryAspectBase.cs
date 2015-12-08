using System;

namespace Simple.Repository
{
    /// <summary>
    /// An abstract aspect class.
    /// </summary>
    public abstract class RepositoryAspectBase : IRepositoryAspect
    {
        /// <summary>
        /// Handles the event before adding an entity.
        /// </summary>
        /// <typeparam name="TEnity">The type of the entity.</typeparam>
        /// <param name="entity">The instance of the entity.</param>
        /// <returns>
        /// Returns true if aspect's logic allows to add a new entity, otherwise return false.
        /// </returns>
        public virtual bool OnAddingEntity<TEnity>(TEnity entity)
        {
            return true;
        }

        /// <summary>
        /// Handles the event after adding an entity.
        /// </summary>
        /// <typeparam name="TEnity">The type of the entity.</typeparam>
        /// <param name="entity">The instance of the entity.</param>
        public virtual void OnAddedEntity<TEnity>(TEnity entity)
        {

        }

        /// <summary>
        /// Handles the event before removing an entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The instance of the entity.</param>
        /// <returns>
        /// Returns true if aspect's logic allows to remove a new entity, otherwise return false.
        /// </returns>
        public virtual bool OnRemoving<TEntity>(TEntity entity)
        {
            return true;
        }

        /// <summary>
        /// Handles the event after removing an entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The instance of the entity.</param>
        public virtual void OnRemoved<TEntity>(TEntity entity)
        {

        }

        /// <summary>
        /// Handles the event after an repository error.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="e">The instance of the exception.</param>
        public virtual void OnError(string message, Exception e)
        {

        }
    }
}
