using Bank.Core.Entities;
using Bank.Core.Repositories.EntityRepositories;
using System;

namespace Bank.Data.Repositories.Entities
{
    internal class AccountRepository : Repository<Account, Guid>, IAccountRepository
    {
        public AccountRepository(BankContext context) : base(context)
        {
        }
    }
}
