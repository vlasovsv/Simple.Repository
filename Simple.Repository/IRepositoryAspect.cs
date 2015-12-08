using System;

namespace Simple.Repository
{
    /// <summary>
    /// Provides basic aspect operations.
    /// </summary>
    public interface IRepositoryAspect
    {
        /// <summary>
        /// Handles the event before adding an entity.
        /// </summary>
        /// <typeparam name="TEnity">The type of the entity.</typeparam>
        /// <param name="entity">The instance of the entity.</param>
        /// <returns>
        /// Returns true if aspect's logic allows to add a new entity, otherwise return false.
        /// </returns>
        bool OnAddingEntity<TEnity>(TEnity entity);

        /// <summary>
        /// Handles the event after adding an entity.
        /// </summary>
        /// <typeparam name="TEnity">The type of the entity.</typeparam>
        /// <param name="entity">The instance of the entity.</param>
        void OnAddedEntity<TEnity>(TEnity entity);

        /// <summary>
        /// Handles the event before removing an entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The instance of the entity.</param>
        /// <returns>
        /// Returns true if aspect's logic allows to remove a new entity, otherwise return false.
        /// </returns>
        bool OnRemoving<TEntity>(TEntity entity);

        /// <summary>
        /// Handles the event after removing an entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The instance of the entity.</param>
        void OnRemoved<TEntity>(TEntity entity);

        /// <summary>
        /// Handles the event after an repository error.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="e">The instance of the exception.</param>
        void OnError(string message, Exception e);
    }
}
