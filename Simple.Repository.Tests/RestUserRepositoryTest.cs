using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.Repository.Tests.Models;

namespace Simple.Repository.Tests
{
    [TestClass]
    public class RestUserRepositoryTest
    {
        private const string HOST = "http://jsonplaceholder.typicode.com/users";
        private RestRepository<User, int> _repository;

        [TestInitialize]
        public void Initialize()
        {
            _repository = new RestRepository<User, int>((user) => user.id, HOST);
        }

        [TestMethod]
        public void GetUserByIdTest()
        {
            int id = 5;
            
            var requestedUser = _repository.Get(id);
            Assert.AreEqual("Chelsey Dietrich", requestedUser.name);
        }
    }
}
