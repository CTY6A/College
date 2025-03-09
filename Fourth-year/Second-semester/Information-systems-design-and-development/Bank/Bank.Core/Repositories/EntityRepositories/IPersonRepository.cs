using Bank.Core.Entities;
using System;

namespace Bank.Core.Repositories.EntityRepositories
{
    public interface IPersonRepository : IRepository<Person, Guid>
    {
    }
}
