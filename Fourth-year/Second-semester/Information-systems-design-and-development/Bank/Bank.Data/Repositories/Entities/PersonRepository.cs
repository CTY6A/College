using Bank.Core.Entities;
using Bank.Core.Repositories.EntityRepositories;
using System;

namespace Bank.Data.Repositories.Entities
{
    internal class PersonRepository : Repository<Person, Guid>, IPersonRepository
    {
        public PersonRepository(BankContext context) : base(context)
        {
        }
    }
}
