using Bank.Core.Entities;
using Bank.Core.Repositories.EntityRepositories;
using System;

namespace Bank.Data.Repositories.Entities
{
    internal class AccountTypeRepository : Repository<AccountType, int>, IAccountTypeRepository
    {
        public AccountTypeRepository(BankContext context) : base(context)
        {
        }
    }
}
