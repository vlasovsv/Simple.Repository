using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Simple.Repository
{
    /// <summary>
    /// Provides basic operations for repository entities.
    /// </summary>
    /// <typeparam name="TEntity">A type of a stored entity.</typeparam>
    /// <typeparam name="TKey">A type of an unique entity's key.</typeparam>
    public interface IRepository<TEntity, TKey> : IEnumerable<TEntity>, INotifyCollectionChanged, IManageAspects
        where TEntity : class
        where TKey : IEquatable<TKey>
    {
        #region Properties

        /// <summary>
        /// Gets the number of enitites.
        /// </summary>
        int Count
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an entity by its id.
        /// </summary>
        /// <param name="id">An identifier of the entity.</param>
        /// <returns>
        /// Returns the entity if the one is exists, otherwise - throws an exception.
        /// </returns>
        TEntity Get(TKey id);

        /// <summary>
        /// Add a new entity into the repository.
        /// </summary>
        /// <param name="entity">An instance of an entiny.</param>
        /// <returns></returns>
        bool Add(TEntity entity);

        /// <summary>
        /// Remove an existed entity from the repository.
        /// </summary>
        /// <param name="entity">An instance of an entiny.</param>
        /// <returns></returns>
        bool Remove(TEntity entity);

        /// <summary>
        /// Remove an existed entity from the repository by a key.
        /// </summary>
        /// <param name="key">The key of the entity.</param>
        /// <returns></returns>
        bool Remove(TKey key);

        /// <summary>
        /// Update an existed entity in the repository.
        /// </summary>
        /// <param name="entity">An instance of an entiny.</param>
        /// <returns></returns>
        bool Update(TEntity entity);

        #endregion
    }
}
