using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Simple.Repository.Tests.Models;


namespace Simple.Repository.Tests
{
    [TestClass]
    public class PersonRepositoryTest
    {
        private PersonRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            _repository = new PersonRepository();
        }

        [TestMethod]
        public void AddPersonToRepositoryTest()
        {
            Person person = new Person("James");
            person.ID = Guid.NewGuid();

            _repository.Add(person);

            Assert.IsTrue(_repository.Count == 1);
        }

        [TestMethod]
        public void RemovePersonFromRepositoryTest()
        {
            Person person = new Person("James");
            person.ID = Guid.NewGuid();

            _repository.Add(person);
            _repository.Remove(person);

            Assert.IsTrue(_repository.Count == 0);
        }

        [TestMethod]
        public void RemovePersonByKeyFromRepository()
        {
            Person person = new Person("James");
            person.ID = Guid.NewGuid();
            _repository.Add(person);
            _repository.Remove(person.ID);

            Assert.IsTrue(_repository.Count == 0);
        }

        [TestMethod]
        public void GetPersonByIdTest()
        {
            Guid id = Guid.NewGuid();
            Person person = new Person("James");
            person.ID = id;
            _repository.Add(person);

            var requestedPerson = _repository.Get(id);
            Assert.AreEqual(person, requestedPerson);
        }
    }
}
