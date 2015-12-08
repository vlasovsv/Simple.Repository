using System;

using RestSharp;

namespace Simple.Repository
{
    public class RestRepository<TEntity, TKey> : Repository<TEntity, TKey> 
        where TEntity : class, new()
        where TKey : IEquatable<TKey>
    {
        #region Private fields

        private readonly string _endpoint;

        #endregion

        public RestRepository(Func<TEntity, TKey> keySelector, string endpoint)
            : base(keySelector)
        {
            _endpoint = endpoint;
        }

        public string Endpoint
        {
            get
            {
                return _endpoint;
            }
        }

        protected override TEntity OnGetNotFound(TKey id)
        {
            var client = new RestClient(Endpoint);
            var request = new RestRequest("/{id}", Method.GET);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            var response = client.Execute<TEntity>(request);
            return response.Data;
        }

        
    }
}
