using System;

namespace Simple.Repository.Tests.Models
{
    public class PersonRepository : Repository<Person, Guid>
    {
        public PersonRepository()
            : base((p) => p.ID)
        {

        }
    }
}
