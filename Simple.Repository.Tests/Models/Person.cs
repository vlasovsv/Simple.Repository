using System;

namespace Simple.Repository.Tests.Models
{
    public class Person : Human
    {
        public Person(string name)
            : base(name)
        {
            Name = name;
        }

        public Guid ID { get; set; }
    }

    public class Human
    {
        public Human(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
