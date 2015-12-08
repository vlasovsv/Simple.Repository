using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Simple.Repository
{
    /// <summary>
    /// Provides basic operations for repository entities.
    /// </summary>
    /// <typeparam name="TEntity">A type of a stored entity.</typeparam>
    /// <typeparam name="TKey">A type of an unique entity's key.</typeparam>
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
        where TKey : IEquatable<TKey>
    {
        #region Private fields

        private ConcurrentDictionary<TKey, TEntity> _entities;
        private Func<TEntity, TKey> _keySelector;
        private List<IRepositoryAspect> _aspects;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of a repository.
        /// </summary>
        public Repository(Func<TEntity, TKey> keySelector)
        {
            Contract.Requires(keySelector != null);
            _aspects = new List<IRepositoryAspect>();
            _entities = new ConcurrentDictionary<TKey, TEntity>();
            _keySelector = keySelector;
            OnInitialize();
        }

        #endregion

        #region Events

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a dictionary of stored entities.
        /// </summary>
        protected ConcurrentDictionary<TKey, TEntity> Entities
        {
            get
            {
                return _entities;
            }
        }

        /// <summary>
        /// Gets a key selector for stored objects.
        /// </summary>
        protected Func<TEntity, TKey> KeySelector
        {
            get
            {
                return _keySelector;
            }
        }

        /// <summary>
        /// Gets the number of entities.
        /// </summary>
        public int Count
        {
            get
            {
                return _entities.Count;
            }
        }

        /// <summary>
        /// Gets repository aspects.
        /// </summary>
        public ReadOnlyCollection<IRepositoryAspect> Aspects
        {
            get
            {
                return _aspects.AsReadOnly();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the event of initialization of the repository.
        /// </summary>
        protected virtual void OnInitialize()
        {

        }

        /// <summary>
        /// Gets an entity by its id.
        /// </summary>
        /// <param name="id">An identifier of the entity.</param>
        /// <returns>
        /// Returns the entity if the one is exists, otherwise - null.
        /// </returns>
        public TEntity Get(TKey id)
        {
            TEntity entity = null;
            try
            {
                if (!Entities.TryGetValue(id, out entity))
                {
                    entity = OnGetNotFound(id);
                }
            }
            catch (Exception ex)
            {
                RunAspect(aspect => aspect.OnError("Could not get the entity from the repository", ex));
            }
            return entity;
        }

        /// <summary>
        /// Gets an entity by its id.
        /// </summary>
        /// <param name="id">An identifier of the entity.</param>
        /// <returns>
        /// Returns the entity if the one is exists, otherwise - null.
        /// </returns>
        protected virtual TEntity OnGetNotFound(TKey id)
        {
            return null;
        }

        /// <summary>
        /// Add a new entity into the repository.
        /// </summary>
        /// <param name="entity">The instance of the entity.</param>
        /// <returns>
        /// Returns true if the entity was added, otherwise returns false.
        /// </returns>
        public bool Add(TEntity entity)
        {
            bool result = false;
            try
            {
                if (OnAdding(entity) && RunAspect(aspect => aspect.OnAddingEntity(entity)))
                {
                    var key = KeySelector(entity);
                    if (Entities.TryAdd(key, entity))
                    {
                        result = true;
                        OnAdded(entity);
                        RunAspect(aspect => aspect.OnAddedEntity(entity));
                        OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                            NotifyCollectionChangedAction.Add, 
                            entity));
                    }
                }
            }
            catch (Exception ex)
            {
                RunAspect(aspect => aspect.OnError("Could not add the entity into the repository", ex));
            }
            return result;
        }

        /// <summary>
        /// Handles the event before adding an entity.
        /// </summary>
        /// <param name="entity">The instance of the entity.</param>
        /// <returns>
        /// Returns true if the logic allows to add the entity.
        /// </returns>
        protected virtual bool OnAdding(TEntity entity)
        {
            return true;
        }

        /// <summary>
        /// Handles the event after adding an entity.
        /// </summary>
        /// <param name="entity">The instance of the entity.</param>
        protected virtual void OnAdded(TEntity entity)
        {

        }

        /// <summary>
        /// Removes an entity by the key from the repository.
        /// </summary>
        /// <param name="key">The key of the entity.</param>
        /// <returns>
        /// Returns true if the entity was deleted, otherwise returns false.
        /// </returns>
        public bool Remove(TKey key)
        {
            bool result = false;
            TEntity entity = null;
            if (Entities.TryGetValue(key, out entity))
            {
                result = Remove(entity);
            }
            return result;
        }

        /// <summary>
        /// Removes an entity by the instance from the repository.
        /// </summary>
        /// <param name="entity">The isntance of the entity.</param>
        /// <returns>
        /// Returns true if the entity was deleted, otherwise returns false.
        /// </returns>
        public bool Remove(TEntity entity)
        {
            bool result = false;
            try
            {
                if (RunAspect(aspect => aspect.OnRemoving(entity) && OnRemoving(entity)))
                {
                    TEntity removedEntity = null;
                    var key = KeySelector(entity);
                    if (Entities.TryRemove(key, out removedEntity))
                    {
                        result = true;
                        OnRemoved(entity);
                        RunAspect(aspect => aspect.OnRemoved(entity));
                        OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                            NotifyCollectionChangedAction.Remove, 
                            entity));
                    }
                }
            }
            catch (Exception ex)
            {
                RunAspect(aspect => aspect.OnError("Could not remove the entity from the repository", ex));
            }
            return result;
        }

        /// <summary>
        /// Handles the event before removing an entity.
        /// </summary>
        /// <param name="entity">The instance of the entity.</param>
        /// <returns>
        /// Returns true if the logic allows to remove the entity.
        /// </returns>
        protected virtual bool OnRemoving(TEntity entity)
        {
            return true;
        }

        /// <summary>
        /// Handles the event after removing an entity.
        /// </summary>
        /// <param name="entity">The instance of the entity.</param>
        protected virtual void OnRemoved(TEntity entity)
        {

        }

        /// <summary>
        /// Updates an existed entity.
        /// </summary>
        /// <param name="entity">The instance of the entity.</param>
        /// <returns>
        /// Returns true if the entity was updated, otherwise returns false.
        /// </returns>
        public bool Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A System.Collections.Generic.IEnumerator`1 that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<TEntity> GetEnumerator()
        {
            return Entities.Values.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An System.Collections.IEnumerator object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Executes a functor for all repository aspects.
        /// </summary>
        /// <param name="func">The functor.</param>
        /// <returns>
        /// Returns true if all aspects return true, otherwise returns false.
        /// </returns>
        protected bool RunAspect(Func<IRepositoryAspect, bool> func)
        {
            return _aspects.All(func);
        }

        /// <summary>
        /// Executes an action for all repository aspects.
        /// </summary>
        /// <param name="action">The action.</param>
        protected void RunAspect(Action<IRepositoryAspect> action)
        {
            _aspects.ForEach(action);
        }

        /// <summary>
        /// Adds an new aspect to the repository.
        /// </summary>
        /// <param name="aspect">The instance of the aspect.</param>
        public void AddAspect(IRepositoryAspect aspect)
        {
            Contract.Requires(aspect != null);
            if (!_aspects.Contains(aspect))
            {
                _aspects.Add(aspect);
            }
        }

        /// <summary>
        /// Removes an existed aspect from the repository.
        /// </summary>
        /// <param name="aspect">The instance of the aspect.</param>
        public void RemoveAspect(IRepositoryAspect aspect)
        {
            Contract.Requires(aspect != null);
            _aspects.Remove(aspect);
        }

        /// <summary>
        /// Handles an event of the collection changed.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        protected void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            var handler = CollectionChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion
    }
}
