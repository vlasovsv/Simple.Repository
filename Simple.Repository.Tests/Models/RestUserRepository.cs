namespace Simple.Repository.Tests.Models
{
    class RestUserRepository : RestRepository<User, int>
    {
        public RestUserRepository(string host)
            : base((u) => u.id, host)
        {

        }
    }
}
