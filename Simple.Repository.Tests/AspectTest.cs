using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Simple.Repository.Tests.Aspects;
using Simple.Repository.Tests.Models;

namespace Simple.Repository.Tests
{
    [TestClass]
    public class AspectTest
    {
        private PersonRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            _repository = new PersonRepository();
        }

        [TestMethod]
        public void AddAspectTest()
        {
            _repository.AddAspect(new SimpleAspect());

            Assert.IsTrue(_repository.Aspects.Count == 1);
        }

        [TestMethod]
        public void RemoveAspectTest()
        {
            var aspect = new SimpleAspect();
            _repository.AddAspect(aspect);
            _repository.RemoveAspect(aspect);

            Assert.IsTrue(_repository.Aspects.Count == 0);
        }

        [TestMethod]
        public void AspectHandleAddTest()
        {
            var storage = new MessageStorage();
            var aspect = new LogAspect(storage);
            _repository.AddAspect(aspect);
            Person person = new Person("Jack");
            person.ID = Guid.NewGuid();
            _repository.Add(person);

            Assert.IsTrue(storage.Messages.Count == 2);
        }

        [TestMethod]
        public void AspectHandleRemoveTest()
        {
            var storage = new MessageStorage();
            var aspect = new LogAspect(storage);
            Person person = new Person("Jack");
            person.ID = Guid.NewGuid();
            _repository.Add(person);
            _repository.AddAspect(aspect);
            _repository.Remove(person);

            Assert.IsTrue(storage.Messages.Count == 2);
        }
    }
}
