using System;

namespace Simple.Repository.Tests.Aspects
{
    public class LogAspect : RepositoryAspectBase
    {
        private MessageStorage _storage;

        public LogAspect(MessageStorage storage)
        {
            _storage = storage;
        }

        public override bool OnAddingEntity<TEnity>(TEnity entity)
        {
            _storage.Messages.Add(string.Format("Adding entity {0}", entity));
            return base.OnAddingEntity<TEnity>(entity);
        }

        public override void OnAddedEntity<TEnity>(TEnity entity)
        {
            _storage.Messages.Add(string.Format("Added entity {0}", entity));
            base.OnAddedEntity<TEnity>(entity);
        }

        public override bool OnRemoving<TEntity>(TEntity entity)
        {
            _storage.Messages.Add(string.Format("Removing entity {0}", entity));
            return base.OnRemoving<TEntity>(entity);
        }

        public override void OnRemoved<TEntity>(TEntity entity)
        {
            _storage.Messages.Add(string.Format("Removed entity {0}", entity));
            base.OnRemoved<TEntity>(entity);
        }

        public override void OnError(string message, Exception e)
        {
            _storage.Messages.Add(string.Format("Error Message {0}. Exception {1}", message, e));
            base.OnError(message, e);
        }
    }
}
